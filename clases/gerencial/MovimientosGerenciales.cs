using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using lebrun.clasesData;

namespace lebrun.clases.gerencial
{
    // Consultas de movimientos, ventas y compras del modulo gerencial (etapa 3). Sustituyen a FRMMOVFACTU, FRMMOVPROD, FRMMOVCLIENTES,
    // FRMMOVPRODUCTOS, FRMPROXVENDE, FRMVENTAXVENDEDORLEBRUN2 y FRMCOMPRASXPROVEEDOR del sistema anterior.
    // Se mantienen sus criterios: la filial son los vendedores con codigo 80 o mayor, las ventas se miden con la base neta de la factura,
    // y las compras son los documentos tipo 37 menos las notas de credito tipo 43. Los proveedores importados ya no se distinguen por el
    // codigo (el sistema anterior usaba 8.900.000 o mayor, pero hoy los codigos de proveedores venezolanos tambien pasan de ese numero y
    // todos saldrian como importados): se distinguen por su pais.
    public class MovimientosGerenciales
    {
        public const int PrimerVendedorFilial = 80;
        public const string CodigoPaisLocal = "0089";  // Venezuela en admpais
        public const int LimiteFilas = 5000;

        // Grupos de las consultas por mes y por dia: todos, clientes (vendedores < 80) y filial (vendedores >= 80).
        public static readonly string[] Grupos = { "CLI", "FIL", "GEN" };

        private readonly ConexionBD baseDatos = new ConexionBD();

        private DataTable Ejecutar(string sql)
        {
            DataTable tabla = baseDatos.fDataTable(sql);
            baseDatos.cerrarConexion();
            return tabla;
        }

        private static string Texto(string valor)
        {
            return (valor ?? "").Trim().Replace("\\", "\\\\").Replace("'", "''");
        }

        private static string F(DateTime fecha)
        {
            return "'" + fecha.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture) + "'";
        }

        private static string CondicionGrupo(string grupo, string columnaVendedor)
        {
            string numero = "CAST(" + columnaVendedor + " AS UNSIGNED)";
            switch (grupo)
            {
                case "CLI": return " AND " + numero + " < " + PrimerVendedorFilial;
                case "FIL": return " AND " + numero + " >= " + PrimerVendedorFilial;
                default: return "";
            }
        }

        // Medidas por grupo de una consulta de facturas (cantidad de documentos y montos) o de lineas de producto (lineas, unidades y montos).
        // Nombres: CANT_FAV_CLI, MONTO_NETO_FIL, UND_NETO_GEN, etc. El neto es lo facturado menos lo devuelto.
        private static string Bloques(string tipo, string vendedor, string monto, string unidades)
        {
            List<string> partes = new List<string>();
            string cantidad = unidades == null ? "CANT" : "LINEAS";
            foreach (string g in Grupos)
            {
                string c = CondicionGrupo(g, vendedor);
                string fav = tipo + " = 'FAV'" + c;
                string dev = tipo + " = 'DEV'" + c;
                partes.Add("SUM(IF(" + fav + ", 1, 0)) AS " + cantidad + "_FAV_" + g);
                partes.Add("SUM(IF(" + dev + ", 1, 0)) AS " + cantidad + "_DEV_" + g);
                if (unidades != null)
                {
                    partes.Add("SUM(IF(" + fav + ", " + unidades + ", 0)) AS UND_FAV_" + g);
                    partes.Add("SUM(IF(" + dev + ", " + unidades + ", 0)) AS UND_DEV_" + g);
                    partes.Add("SUM(IF(" + fav + ", " + unidades + ", 0)) - SUM(IF(" + dev + ", " + unidades + ", 0)) AS UND_NETO_" + g);
                }
                partes.Add("SUM(IF(" + fav + ", " + monto + ", 0)) AS MONTO_FAV_" + g);
                partes.Add("SUM(IF(" + dev + ", " + monto + ", 0)) AS MONTO_DEV_" + g);
                partes.Add("SUM(IF(" + fav + ", " + monto + ", 0)) - SUM(IF(" + dev + ", " + monto + ", 0)) AS MONTO_NETO_" + g);
            }
            return string.Join(", ", partes.ToArray());
        }

        // ---------------- Movimiento Facturacion (documentos de venta) ----------------

        public DataTable FacturacionPorMes(int anio)
        {
            return Ejecutar("SELECT MONTH(dcli_fecha) AS Mes, " + Bloques("dcli_tipdoc", "dcli_codven", "dcli_baseneta", null) +
                            " FROM admdoccli WHERE dcli_tipdoc IN ('FAV', 'DEV') AND dcli_fecha >= " + F(new DateTime(anio, 1, 1)) +
                            " AND dcli_fecha < " + F(new DateTime(anio + 1, 1, 1)) + " GROUP BY MONTH(dcli_fecha) ORDER BY Mes");
        }

        public DataTable FacturacionPorDia(int anio, int mes)
        {
            DateTime primero = new DateTime(anio, mes, 1);
            return Ejecutar("SELECT DAY(dcli_fecha) AS Dia, " + Bloques("dcli_tipdoc", "dcli_codven", "dcli_baseneta", null) +
                            " FROM admdoccli WHERE dcli_tipdoc IN ('FAV', 'DEV') AND dcli_fecha >= " + F(primero) +
                            " AND dcli_fecha < " + F(primero.AddMonths(1)) + " GROUP BY DAY(dcli_fecha) ORDER BY Dia");
        }

        public DataTable FacturasDelDia(DateTime dia)
        {
            return Ejecutar("SELECT d.dcli_tipdoc AS Tipo, d.dcli_numero AS Documento, d.dcli_codigo AS Cliente, c.cli_nombre AS Nombre, " +
                            "d.dcli_caja AS Caja, d.dcli_codven AS CodVendedor, t.tra_nombre AS Vendedor, d.dcli_baseneta AS Monto " +
                            "FROM admdoccli d LEFT JOIN admclientes c ON c.cli_codigo = d.dcli_codigo LEFT JOIN admtrabajador t ON t.tra_codigo = d.dcli_codven " +
                            "WHERE d.dcli_fecha = " + F(dia) + " AND d.dcli_tipdoc IN ('FAV', 'DEV') " +
                            "ORDER BY d.dcli_tipdoc DESC, d.dcli_numero DESC LIMIT " + (LimiteFilas + 1));
        }

        // ---------------- Movimiento Producto (lineas de venta) ----------------

        public DataTable ProductosPorMes(int anio)
        {
            return Ejecutar("SELECT MONTH(mov_fecha) AS Mes, " + Bloques("mov_tipdoc", "mov_vendedor", "mov_total", "mov_cant") +
                            " FROM adminvmov WHERE mov_tipdoc IN ('FAV', 'DEV') AND mov_fecha >= " + F(new DateTime(anio, 1, 1)) +
                            " AND mov_fecha < " + F(new DateTime(anio + 1, 1, 1)) + " GROUP BY MONTH(mov_fecha) ORDER BY Mes");
        }

        public DataTable ProductosPorDia(int anio, int mes)
        {
            DateTime primero = new DateTime(anio, mes, 1);
            return Ejecutar("SELECT DAY(mov_fecha) AS Dia, " + Bloques("mov_tipdoc", "mov_vendedor", "mov_total", "mov_cant") +
                            " FROM adminvmov WHERE mov_tipdoc IN ('FAV', 'DEV') AND mov_fecha >= " + F(primero) +
                            " AND mov_fecha < " + F(primero.AddMonths(1)) + " GROUP BY DAY(mov_fecha) ORDER BY Dia");
        }

        public DataTable LineasDelDia(DateTime dia)
        {
            return Ejecutar("SELECT m.mov_tipdoc AS Tipo, m.mov_docume AS Documento, m.mov_codigo AS Codigo, i.inv_descri AS Descripcion, " +
                            "m.mov_vendedor AS CodVendedor, t.tra_nombre AS Vendedor, m.mov_undmed AS Unidad, m.mov_cant AS Cantidad, " +
                            "m.mov_costo AS Costo, m.mov_precio AS Precio, m.mov_total AS Total " +
                            "FROM adminvmov m LEFT JOIN adminv i ON i.inv_codigo = m.mov_codigo LEFT JOIN admtrabajador t ON t.tra_codigo = m.mov_vendedor " +
                            "WHERE m.mov_fecha = " + F(dia) + " AND m.mov_tipdoc IN ('FAV', 'DEV') " +
                            "ORDER BY m.mov_tipdoc DESC, m.mov_docume DESC, m.mov_codigo LIMIT " + (LimiteFilas + 1));
        }

        // ---------------- Movimientos por cliente, por producto y por vendedor ----------------

        // Medidas de un grupo de lineas: unidades, lineas y monto (precio por cantidad) facturados, devueltos y netos; con costo si se pide.
        private static string MedidasMovimiento(bool conCosto)
        {
            string fav = "m.mov_tipdoc = 'FAV'";
            string dev = "m.mov_tipdoc = 'DEV'";
            string monto = "m.mov_precio * m.mov_cant";
            List<string> partes = new List<string>();
            partes.Add("SUM(IF(" + fav + ", m.mov_cant, 0)) AS UndFav");
            partes.Add("SUM(IF(" + dev + ", m.mov_cant, 0)) AS UndDev");
            partes.Add("SUM(IF(" + fav + ", m.mov_cant, 0)) - SUM(IF(" + dev + ", m.mov_cant, 0)) AS UndNeto");
            partes.Add("SUM(IF(" + fav + ", 1, 0)) AS LineasFav");
            partes.Add("SUM(IF(" + dev + ", 1, 0)) AS LineasDev");
            partes.Add("SUM(IF(" + fav + ", 1, 0)) - SUM(IF(" + dev + ", 1, 0)) AS LineasNeto");
            partes.Add("SUM(IF(" + fav + ", " + monto + ", 0)) AS MontoFav");
            partes.Add("SUM(IF(" + dev + ", " + monto + ", 0)) AS MontoDev");
            partes.Add("SUM(IF(" + fav + ", " + monto + ", 0)) - SUM(IF(" + dev + ", " + monto + ", 0)) AS MontoNeto");
            if (conCosto)
            {
                partes.Add("SUM(IF(" + fav + ", m.mov_costo * m.mov_cant, 0)) - SUM(IF(" + dev + ", m.mov_costo * m.mov_cant, 0)) AS Costo");
            }
            return string.Join(", ", partes.ToArray());
        }

        private static string FiltroMovimiento(DateTime desde, DateTime hasta, string producto)
        {
            string sql = " WHERE m.mov_tipdoc IN ('FAV', 'DEV') AND m.mov_fecha BETWEEN " + F(desde) + " AND " + F(hasta);
            if (!string.IsNullOrWhiteSpace(producto))
            {
                sql += " AND m.mov_codigo = '" + Texto(producto) + "'";
            }
            return sql;
        }

        // Cada cliente con lo que se le facturo y devolvio en el rango (opcionalmente solo de un producto).
        public DataTable MovimientosPorCliente(DateTime desde, DateTime hasta, string producto)
        {
            return Ejecutar("SELECT m.mov_codcta AS Codigo, c.cli_nombre AS Nombre, " + MedidasMovimiento(false) +
                            " FROM adminvmov m LEFT JOIN admclientes c ON c.cli_codigo = m.mov_codcta" + FiltroMovimiento(desde, hasta, producto) +
                            " GROUP BY m.mov_codcta, c.cli_nombre ORDER BY MontoNeto DESC LIMIT " + (LimiteFilas + 1));
        }

        // Productos que compro un cliente en el rango.
        public DataTable ProductosDeCliente(string cliente, DateTime desde, DateTime hasta, string producto)
        {
            return Ejecutar("SELECT m.mov_codigo AS Codigo, i.inv_descri AS Nombre, " + MedidasMovimiento(false) +
                            " FROM adminvmov m LEFT JOIN adminv i ON i.inv_codigo = m.mov_codigo" + FiltroMovimiento(desde, hasta, producto) +
                            " AND m.mov_codcta = '" + Texto(cliente) + "' GROUP BY m.mov_codigo, i.inv_descri ORDER BY MontoNeto DESC LIMIT " + (LimiteFilas + 1));
        }

        // Cada producto con sus ventas, devoluciones y costo en el rango.
        public DataTable MovimientosPorProducto(DateTime desde, DateTime hasta, string producto)
        {
            return Ejecutar("SELECT m.mov_codigo AS Codigo, i.inv_descri AS Nombre, " + MedidasMovimiento(true) +
                            " FROM adminvmov m LEFT JOIN adminv i ON i.inv_codigo = m.mov_codigo" + FiltroMovimiento(desde, hasta, producto) +
                            " GROUP BY m.mov_codigo, i.inv_descri ORDER BY MontoNeto DESC LIMIT " + (LimiteFilas + 1));
        }

        // Lineas de venta y devolucion de un producto en el rango.
        public DataTable LineasDeProducto(string producto, DateTime desde, DateTime hasta)
        {
            return Ejecutar("SELECT m.mov_fecha AS Fecha, m.mov_tipdoc AS Tipo, m.mov_docume AS Documento, c.cli_nombre AS Cliente, " +
                            "t.tra_nombre AS Vendedor, m.mov_cant AS Cantidad, m.mov_precio AS Precio, m.mov_precio * m.mov_cant AS Monto " +
                            "FROM adminvmov m LEFT JOIN admclientes c ON c.cli_codigo = m.mov_codcta LEFT JOIN admtrabajador t ON t.tra_codigo = m.mov_vendedor" +
                            FiltroMovimiento(desde, hasta, producto) + " ORDER BY m.mov_fecha DESC, m.mov_docume DESC LIMIT " + (LimiteFilas + 1));
        }

        // Vendedores que vendieron un producto en el rango.
        public DataTable ProductoPorVendedor(string producto, DateTime desde, DateTime hasta)
        {
            return Ejecutar("SELECT m.mov_vendedor AS Codigo, t.tra_nombre AS Nombre, " + MedidasMovimiento(false) +
                            " FROM adminvmov m LEFT JOIN admtrabajador t ON t.tra_codigo = m.mov_vendedor" + FiltroMovimiento(desde, hasta, producto) +
                            " GROUP BY m.mov_vendedor, t.tra_nombre ORDER BY MontoNeto DESC LIMIT " + (LimiteFilas + 1));
        }

        // Lineas de un producto vendidas por un vendedor en el rango.
        public DataTable LineasDeProductoPorVendedor(string producto, string vendedor, DateTime desde, DateTime hasta)
        {
            return Ejecutar("SELECT m.mov_fecha AS Fecha, m.mov_tipdoc AS Tipo, m.mov_docume AS Documento, c.cli_nombre AS Cliente, " +
                            "m.mov_cant AS Cantidad, m.mov_precio AS Precio, m.mov_precio * m.mov_cant AS Monto " +
                            "FROM adminvmov m LEFT JOIN admclientes c ON c.cli_codigo = m.mov_codcta" + FiltroMovimiento(desde, hasta, producto) +
                            " AND m.mov_vendedor = '" + Texto(vendedor) + "' ORDER BY m.mov_fecha DESC, m.mov_docume DESC LIMIT " + (LimiteFilas + 1));
        }

        public string DescripcionProducto(string producto)
        {
            DataTable t = Ejecutar("SELECT inv_descri FROM adminv WHERE inv_codigo = '" + Texto(producto) + "'");
            return t.Rows.Count == 0 || t.Rows[0][0] == DBNull.Value ? null : Convert.ToString(t.Rows[0][0]).Trim();
        }

        // ---------------- Ventas por vendedor y compras por proveedor ----------------

        // Periodos de comparacion respecto a un dia: el dia, la semana (de lunes al dia), el mes y el anio hasta el dia, y los 3, 6, 9 y 12
        // meses completos anteriores al mes del dia.
        public static List<KeyValuePair<string, DateTime[]>> Periodos(DateTime dia)
        {
            dia = dia.Date;
            DateTime primeroDelMes = new DateTime(dia.Year, dia.Month, 1);
            DateTime lunes = dia.AddDays(-(((int)dia.DayOfWeek + 6) % 7));
            List<KeyValuePair<string, DateTime[]>> p = new List<KeyValuePair<string, DateTime[]>>();
            p.Add(new KeyValuePair<string, DateTime[]>("Dia", new[] { dia, dia }));
            p.Add(new KeyValuePair<string, DateTime[]>("Semana", new[] { lunes, dia }));
            p.Add(new KeyValuePair<string, DateTime[]>("Mes", new[] { primeroDelMes, dia }));
            p.Add(new KeyValuePair<string, DateTime[]>("Anio", new[] { new DateTime(dia.Year, 1, 1), dia }));
            foreach (int meses in new[] { 3, 6, 9, 12 })
            {
                p.Add(new KeyValuePair<string, DateTime[]>("M" + meses, new[] { primeroDelMes.AddMonths(-meses), primeroDelMes.AddDays(-1) }));
            }
            return p;
        }

        private static DateTime PrimeraFecha(List<KeyValuePair<string, DateTime[]>> periodos)
        {
            DateTime primera = DateTime.MaxValue;
            foreach (KeyValuePair<string, DateTime[]> p in periodos)
            {
                if (p.Value[0] < primera)
                {
                    primera = p.Value[0];
                }
            }
            return primera;
        }

        // Suma de un valor en cada periodo; el valor se resta cuando el documento es el de devolucion o nota de credito.
        private static string SumasPorPeriodo(List<KeyValuePair<string, DateTime[]>> periodos, string columnaFecha, string condicionSuma, string valor, string prefijo)
        {
            List<string> partes = new List<string>();
            foreach (KeyValuePair<string, DateTime[]> p in periodos)
            {
                partes.Add("SUM(IF(" + columnaFecha + " BETWEEN " + F(p.Value[0]) + " AND " + F(p.Value[1]) + ", IF(" + condicionSuma + ", " + valor + ", -" + valor + "), 0)) AS " + prefijo + p.Key);
            }
            return string.Join(", ", partes.ToArray());
        }

        // Ventas netas (facturas menos devoluciones) de cada vendedor por periodo. Grupo: 0 = clientes, 1 = filial. Incluye el costo del mes y del anio.
        public DataTable VentasPorVendedor(DateTime dia)
        {
            List<KeyValuePair<string, DateTime[]>> periodos = Periodos(dia);
            string costo = "SUM(IF(d.dcli_fecha BETWEEN " + F(periodos[2].Value[0]) + " AND " + F(periodos[2].Value[1]) + ", IF(d.dcli_tipdoc = 'FAV', d.dcli_cosfac, -d.dcli_cosfac), 0)) AS CostoMes, " +
                           "SUM(IF(d.dcli_fecha BETWEEN " + F(periodos[3].Value[0]) + " AND " + F(periodos[3].Value[1]) + ", IF(d.dcli_tipdoc = 'FAV', d.dcli_cosfac, -d.dcli_cosfac), 0)) AS CostoAnio";
            return Ejecutar("SELECT t.tra_codigo AS Codigo, t.tra_nombre AS Nombre, IF(CAST(t.tra_codigo AS UNSIGNED) >= " + PrimerVendedorFilial + ", 1, 0) AS Grupo, " +
                            SumasPorPeriodo(periodos, "d.dcli_fecha", "d.dcli_tipdoc = 'FAV'", "d.dcli_baseneta", "") + ", " + costo +
                            " FROM admdoccli d INNER JOIN admtrabajador t ON t.tra_codigo = d.dcli_codven" +
                            " WHERE t.tra_cargo = 'VENDEDOR' AND d.dcli_tipdoc IN ('FAV', 'DEV') AND d.dcli_fecha BETWEEN " + F(PrimeraFecha(periodos)) + " AND " + F(dia) +
                            " GROUP BY t.tra_codigo, t.tra_nombre ORDER BY Grupo, Anio DESC, t.tra_codigo");
        }

        // Compras netas (comprobantes tipo 37 menos notas de credito tipo 43) de cada proveedor por periodo. Grupo: 0 = nacional (pais local), 1 = del exterior.
        public DataTable ComprasPorProveedor(DateTime dia)
        {
            List<KeyValuePair<string, DateTime[]>> periodos = Periodos(dia);
            return Ejecutar("SELECT p.pro_codigo AS Codigo, p.pro_nombre AS Nombre, IF(p.pro_pais = '" + CodigoPaisLocal + "', 0, 1) AS Grupo, " +
                            SumasPorPeriodo(periodos, "d.doc_fechCarg", "d.doc_tipdoc = '37'", "d.doc_basnet", "") +
                            " FROM admdocpro d INNER JOIN admproveedor p ON p.pro_codigo = d.doc_codigo" +
                            " WHERE d.doc_tipdoc IN ('37', '43') AND d.doc_fechCarg BETWEEN " + F(PrimeraFecha(periodos)) + " AND " + F(dia) +
                            " GROUP BY p.pro_codigo, p.pro_nombre ORDER BY Grupo, Anio DESC, p.pro_codigo");
        }
    }
}
