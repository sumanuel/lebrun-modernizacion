using System;
using System.Data;
using lebrun.clasesData;

namespace lebrun.clases.complementos
{
    // Origen de la venta (tienda fisica, Instagram, Facebook, WhatsApp, ...): sisadm.admorigenventa. Cada factura guarda el codigo en
    // admdoccli.dcli_origenventa, para poder consultar y sacar estadisticas por canal. Los origenes no se borran: se desactivan, porque las
    // facturas antiguas los tienen guardados. (dcli_origen no sirve para esto: ya guarda 'Nacional', el origen de la mercancia.)
    public class OrigenVenta
    {
        // Origen que aparece elegido al abrir la factura (Tienda fisica); la cajera lo cambia si la venta vino por otro canal.
        public const string CodigoPredeterminado = "01";

        private readonly ConexionBD sisadm = new ConexionBD();

        private DataTable Ejecutar(string sql)
        {
            DataTable dt = sisadm.fDataTable(sql);
            sisadm.cerrarConexion();
            return dt;
        }

        private static string Texto(string valor)
        {
            return (valor ?? "").Trim().Replace("\\", "\\\\").Replace("'", "''");
        }

        // Para el mantenimiento: todos los origenes, activos o no.
        public DataTable Todos()
        {
            return Ejecutar("SELECT orig_codigo, orig_descripcion, orig_status FROM admorigenventa ORDER BY orig_codigo");
        }

        // Para la factura: los origenes activos (y el indicado, aunque se haya desactivado despues, para que se siga viendo).
        public DataTable Activos(string codigoActual)
        {
            return Ejecutar("SELECT orig_codigo, orig_descripcion FROM admorigenventa WHERE orig_status = '1' OR orig_codigo = '" + Texto(codigoActual) + "' ORDER BY orig_codigo");
        }

        public bool ExisteCodigo(string codigo)
        {
            return Ejecutar("SELECT orig_codigo FROM admorigenventa WHERE orig_codigo = '" + Texto(codigo) + "'").Rows.Count > 0;
        }

        public bool DescripcionEnUso(string descripcion, string exceptoCodigo)
        {
            return Ejecutar("SELECT orig_codigo FROM admorigenventa WHERE orig_descripcion = '" + Texto(descripcion) + "' AND orig_codigo <> '" + Texto(exceptoCodigo) + "'").Rows.Count > 0;
        }

        public bool Guardar(bool nuevo, string codigo, string descripcion, bool activo)
        {
            string sql = nuevo
                ? "INSERT INTO admorigenventa (orig_codigo, orig_descripcion, orig_status) VALUES ('" + Texto(codigo) + "', '" + Texto(descripcion) + "', '" + (activo ? "1" : "0") + "')"
                : "UPDATE admorigenventa SET orig_descripcion = '" + Texto(descripcion) + "', orig_status = '" + (activo ? "1" : "0") + "' WHERE orig_codigo = '" + Texto(codigo) + "'";
            bool ok = sisadm.ejecutarInsert(sql);
            sisadm.cerrarConexion();
            return ok;
        }

        // Origen guardado en una factura (por su numero interno); null si la factura no tiene (las anteriores a este cambio).
        public string DeFactura(string numeroInterno)
        {
            DataTable dt = Ejecutar("SELECT dcli_origenventa FROM admdoccli WHERE dcli_numero = '" + Texto(numeroInterno) + "'");
            if (dt.Rows.Count == 0 || dt.Rows[0][0] == DBNull.Value || string.IsNullOrWhiteSpace(Convert.ToString(dt.Rows[0][0])))
            {
                return null;
            }
            return Convert.ToString(dt.Rows[0][0]).Trim();
        }

        // Texto listo para la sentencia SQL de la factura: el codigo entre comillas, o NULL si no se eligio ninguno.
        public static string ParaSql(string codigo)
        {
            return string.IsNullOrWhiteSpace(codigo) ? "NULL" : "'" + Texto(codigo) + "'";
        }
    }
}
