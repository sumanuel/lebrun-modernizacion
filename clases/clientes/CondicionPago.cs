using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Globalization;
using lebrun.clasesData;

namespace lebrun.clases.clientes
{
    // Condiciones de pago (Contado, 30 dias, ...) y formas de pago (Efectivo, Tarjeta, ...), todas en sisadm.admcondpago.
    // conp_tipo distingue las condiciones de credito (C) de las formas de pago (F); conp_status = 1 las deja activas.
    // Lo que se guarda en los clientes y en los documentos es siempre el codigo (conp_codigo); Contado es el 07.
    public class CondicionPago
    {
        public const string CodigoContado = "07";
        public const string TipoCondicion = "C";
        public const string TipoFormaPago = "F";

        public string conp_codigo { get; set; }
        public string conp_descripcion { get; set; }
        public decimal conp_cant_dias { get; set; }
        public string conp_observ { get; set; }
        public string conp_status { get; set; }
        public int mora_fact1 { get; set; }
        private string sentenciaSql;
        private ConexionBD sisadm;

        public CondicionPago()
        {
            sisadm = new ConexionBD();
            sentenciaSql = null;
        }

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

        // Todas las filas (condiciones y formas de pago, activas o no), en orden de codigo.
        public DataTable lbxCondPag()
        {
            // solo las condiciones de credito activas: las formas de pago (Efectivo, Divisa $, ...) viven en la misma tabla pero no son condiciones
            return Ejecutar("select conp_codigo,conp_descripcion, conp_cant_dias from admcondpago where conp_tipo = 'C' and conp_status = '1' order by conp_codigo;");
        }

        // Para el mantenimiento: todas las filas con sus siglas, tipo, dias, mora y estado.
        public DataTable Todas()
        {
            return Ejecutar("SELECT conp_codigo, conp_descripcion, conp_siglas, conp_tipo, conp_cant_dias, mora_fact1, conp_observ, conp_status, conp_req_banco, conp_grupo, conp_moneda " +
                            "FROM admcondpago ORDER BY conp_tipo, conp_codigo");
        }

        // Condiciones de credito activas para elegir en un cliente o en un documento. Si el cliente ya tiene una condicion que se
        // desactivo despues, se incluye tambien para que se siga viendo (codigoActual).
        public DataTable Condiciones(string codigoActual)
        {
            return Ejecutar("SELECT conp_codigo, conp_descripcion, conp_siglas, conp_cant_dias FROM admcondpago " +
                            "WHERE conp_tipo = 'C' AND (conp_status = '1' OR conp_codigo = '" + Texto(codigoActual) + "') ORDER BY conp_codigo");
        }

        // Formas de pago activas (Efectivo, Tarjeta de Debito, ...) con si piden banco y numero y en que casilla de la factura se suman.
        public DataTable FormasPago()
        {
            return Ejecutar("SELECT conp_codigo, conp_descripcion, conp_siglas, conp_req_banco, conp_grupo, conp_moneda FROM admcondpago " +
                            "WHERE conp_tipo = 'F' AND conp_status = '1' ORDER BY conp_codigo");
        }

        // Moneda en que se cobra una forma de pago: USD o EUR; vacio = bolivares (se guarda NULL).
        private static string MonedaSql(string moneda)
        {
            string m = (moneda ?? "").Trim().ToUpper();
            return m == "USD" || m == "EUR" ? "'" + m + "'" : "NULL";
        }

        public bool ExisteCodigo(string codigo)
        {
            return Ejecutar("SELECT conp_codigo FROM admcondpago WHERE conp_codigo = '" + Texto(codigo) + "'").Rows.Count > 0;
        }

        // Las siglas no pueden repetirse entre las filas (exceptoCodigo es la fila que se esta modificando).
        public bool SiglasEnUso(string siglas, string exceptoCodigo)
        {
            return Ejecutar("SELECT conp_codigo FROM admcondpago WHERE conp_siglas = '" + Texto(siglas) + "' AND conp_codigo <> '" + Texto(exceptoCodigo) + "'").Rows.Count > 0;
        }

        // Crea o modifica una condicion o forma de pago. No se borran: se desactivan, porque los documentos antiguos las usan.
        public bool Guardar(bool nueva, string codigo, string descripcion, string siglas, string tipo, decimal dias, int mora, string observacion, bool activa, bool pideBanco, string grupo, string moneda)
        {
            string dec = dias.ToString("0.00", CultureInfo.InvariantCulture);
            string sql;
            if (nueva)
            {
                sql = "INSERT INTO admcondpago (conp_codigo, conp_descripcion, conp_siglas, conp_tipo, conp_cant_dias, mora_fact1, conp_observ, conp_status, conp_req_banco, conp_grupo, conp_moneda) VALUES ('" +
                      Texto(codigo) + "', '" + Texto(descripcion) + "', '" + Texto(siglas) + "', '" + Texto(tipo) + "', " + dec + ", " + mora + ", '" + Texto(observacion) + "', '" + (activa ? "1" : "0") +
                      "', '" + (pideBanco ? "1" : "0") + "', '" + Texto(grupo) + "', " + MonedaSql(moneda) + ")";
            }
            else
            {
                sql = "UPDATE admcondpago SET conp_descripcion = '" + Texto(descripcion) + "', conp_siglas = '" + Texto(siglas) + "', conp_tipo = '" + Texto(tipo) + "', conp_cant_dias = " + dec +
                      ", mora_fact1 = " + mora + ", conp_observ = '" + Texto(observacion) + "', conp_status = '" + (activa ? "1" : "0") + "', conp_req_banco = '" + (pideBanco ? "1" : "0") +
                      "', conp_grupo = '" + Texto(grupo) + "', conp_moneda = " + MonedaSql(moneda) + " WHERE conp_codigo = '" + Texto(codigo) + "'";
            }
            bool ok = sisadm.ejecutarInsert(sql);
            sisadm.cerrarConexion();
            return ok;
        }
    }
}
