using System;
using System.Data;
using lebrun.clasesData;

namespace lebrun.clases.gerencial
{
    // Consultas de los listados del modulo gerencial (Articulos Agotados, Articulos Negativos, Art. Sin Vender, Precios Cambiados...).
    // Reescritas a partir de las del sistema anterior: sin GROUP BY de columnas sueltas (MySQL 8 las rechaza), con las fechas 0000-00-00
    // leidas como vacias, con el nombre del vendedor y sin repetir filas. Se piden LimiteFilas + 1 filas para saber si hay mas de las que
    // se muestran.
    public class ListadosGerenciales
    {
        public const int LimiteFilas = 5000;

        private readonly ConexionBD baseDatos = new ConexionBD();

        private static string Fecha(DateTime fecha)
        {
            return fecha.ToString("yyyy-MM-dd");
        }

        // Fecha de una columna DATE, vacia cuando el sistema guardo 0000-00-00 (el conector no lee las fechas cero).
        private static string FechaOVacia(string columna, string alias)
        {
            return "IF(" + columna + " > '1900-01-01', " + columna + ", NULL) AS " + alias;
        }

        private DataTable Ejecutar(string sql)
        {
            DataTable tabla = baseDatos.fDataTable(sql);
            baseDatos.cerrarConexion();
            return tabla;
        }

        // Productos con existencia 0 (negativos = false) o menor que 0 (negativos = true) cuya ultima venta cae entre desde y hasta.
        public DataTable ArticulosSinExistencia(bool negativos, DateTime desde, DateTime hasta, bool soloActivos)
        {
            string sql =
                "SELECT a.inv_codigo AS Codigo, a.inv_descri AS Descripcion, a.inv_estado AS Estado, " +
                FechaOVacia("b.fec_ult_vent", "UltimaVenta") + ", " +
                FechaOVacia("b.fec_ult_compra", "UltimaCompra") + ", " +
                "(SELECT t.tra_nombre FROM admdoccli d INNER JOIN admtrabajador t ON t.tra_codigo = d.dcli_codven " +
                "WHERE d.dcli_numero = b.ult_refer_vent ORDER BY (d.dcli_tipdoc = 'FAV') DESC LIMIT 1) AS Vendedor, " +
                "(SELECT p.pro_nombre FROM adminvparam q INNER JOIN admproveedor p ON p.pro_codigo = q.pin_proveedor " +
                "WHERE q.pin_codigo = a.inv_codigo LIMIT 1) AS Proveedor, " +
                "b.existencia AS Existencia " +
                "FROM adminv2 b INNER JOIN adminv a ON a.inv_codigo = b.inv2_codigo " +
                "WHERE b.existencia " + (negativos ? "< 0" : "= 0") + " " +
                (soloActivos ? "AND a.inv_estado = 'Activo' " : "") +
                "AND b.fec_ult_vent BETWEEN '" + Fecha(desde) + "' AND '" + Fecha(hasta) + "' " +
                "ORDER BY b.fec_ult_vent DESC, a.inv_descri LIMIT " + (LimiteFilas + 1);
            return Ejecutar(sql);
        }

        // Productos con existencia que no se venden desde hace al menos 'dias' dias (los nunca vendidos, si se pide, tambien).
        public DataTable ArticulosSinVender(int dias, bool incluirNuncaVendidos, bool soloActivos)
        {
            string sql =
                "SELECT a.inv_codigo AS Codigo, a.inv_descri AS Descripcion, a.inv_capaci AS Contenido, m.ime_undmed AS Unidad, " +
                "b.existencia AS Existencia, p.pre_precio AS Precio, b.ult_costo AS Costo, b.existencia * b.ult_costo AS Valorizado, " +
                FechaOVacia("b.fec_ult_vent", "UltimaVenta") + ", " +
                "IF(b.fec_ult_vent > '1900-01-01', DATEDIFF(CURDATE(), b.fec_ult_vent), NULL) AS Dias " +
                "FROM adminv2 b INNER JOIN adminv a ON a.inv_codigo = b.inv2_codigo " +
                "LEFT JOIN adminvmed m ON m.ime_codigo = a.inv_codigo AND m.ime_principal = '1' " +
                "LEFT JOIN admprecios p ON p.pre_codigo = a.inv_codigo AND p.pre_undmed = m.ime_undmed AND p.pre_act = 1 AND p.pre_lista = 'A' " +
                "WHERE b.existencia > 0 " +
                (soloActivos ? "AND a.inv_estado = 'Activo' " : "") +
                "AND (b.fec_ult_vent <= DATE_SUB(CURDATE(), INTERVAL " + dias + " DAY)" +
                (incluirNuncaVendidos ? " OR b.fec_ult_vent IS NULL OR b.fec_ult_vent < '1900-01-01'" : "") + ") " +
                "ORDER BY b.existencia * b.ult_costo DESC, a.inv_codigo " +
                "LIMIT " + (LimiteFilas + 1);
            // primero los de mayor valor en inventario (el capital mas inmovilizado); en pantalla se puede reordenar por cualquier columna
            return Ejecutar(sql);
        }

        // Vendedores (para el filtro de Precios Cambiados): codigo y nombre.
        public DataTable Vendedores()
        {
            return Ejecutar("SELECT tra_codigo AS Codigo, tra_nombre AS Nombre FROM admtrabajador WHERE tra_cargo = 'VENDEDOR' ORDER BY tra_nombre");
        }

        // Lineas de factura cuyo precio de venta es distinto del precio original, entre dos fechas (vendedor vacio = todos).
        public DataTable PreciosCambiados(DateTime desde, DateTime hasta, string codigoVendedor, bool incluirPrecioCero)
        {
            string sql =
                "SELECT m.mov_fecha AS Fecha, m.mov_docume AS Documento, m.mov_codigo AS Codigo, a.inv_descri AS Descripcion, " +
                "(SELECT t.tra_nombre FROM admtrabajador t WHERE t.tra_codigo = m.mov_vendedor) AS Vendedor, " +
                "c.cli_nombre AS Cliente, m.mov_cant AS Cantidad, m.mov_precio_ini AS PrecioOriginal, m.mov_precio AS PrecioVenta, " +
                "m.mov_precio - m.mov_precio_ini AS Diferencia, " +
                "ROUND((m.mov_precio - m.mov_precio_ini) * 100 / m.mov_precio_ini, 2) AS Porcentaje " +
                "FROM adminvmov m LEFT JOIN admclientes c ON c.cli_codigo = m.mov_codcta " +
                "LEFT JOIN adminv a ON a.inv_codigo = m.mov_codigo " +
                "WHERE m.mov_tipdoc = 'FAV' AND m.mov_precio_ini > 0 AND m.mov_precio <> m.mov_precio_ini " +
                (incluirPrecioCero ? "" : "AND m.mov_precio > 0 AND m.mov_cant > 0 ") +
                "AND m.mov_fecha BETWEEN '" + Fecha(desde) + "' AND '" + Fecha(hasta) + "' " +
                (string.IsNullOrEmpty(codigoVendedor) ? "" : "AND m.mov_vendedor = '" + codigoVendedor.Replace("'", "''") + "' ") +
                "ORDER BY m.mov_fecha DESC, m.mov_docume DESC LIMIT " + (LimiteFilas + 1);
            return Ejecutar(sql);
        }
    }
}
