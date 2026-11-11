using System;
using lebrun.clasesData;

namespace lebrun.clases.facturacion
{
    // Estado de cobro de un documento de venta, guardado en admdoccli.dcli_estatus (y en su copia de trabajo admdoccli2):
    //   Pagado     no tiene saldo por cobrar (contado, o credito ya cobrado por completo).
    //   Parcial    es a credito y se ha cobrado una parte.
    //   Pendiente  es a credito y no se ha cobrado nada.
    // Es independiente de dcli_estado y dcli_expexp, que siguen guardando Pagado/Activo como antes (el cuadre de caja y las devoluciones dependen de ellos).
    public static class EstadoCobro
    {
        public const string Pagado = "Pagado";
        public const string Parcial = "Parcial";
        public const string Pendiente = "Pendiente";

        // Al facturar: estadoFactura es el Pagado/Activo de siempre (Pagado = lo pagado cubre el neto); si es a credito se mira cuanto se pago.
        public static string Calcular(string estadoFactura, decimal neto, decimal pagado)
        {
            if (!string.Equals(estadoFactura, "Activo", StringComparison.OrdinalIgnoreCase))
            {
                return Pagado;
            }
            if (pagado <= 0m)
            {
                return Pendiente;
            }
            return pagado >= neto ? Pagado : Parcial;
        }

        // La misma regla que usa el script 08_estado_cobro.sql, con lo que dice cuentas por cobrar (admsalcli).
        private const string CasoSql = "CASE WHEN s.NroDocum IS NULL THEN 'Pagado' " +
                                       "WHEN s.Status = 1 OR s.sal_actual = 0 THEN 'Pagado' " +
                                       "WHEN ABS(IFNULL(s.MontoTotal, 0)) - ABS(s.sal_actual) > 0.005 THEN 'Parcial' " +
                                       "ELSE 'Pendiente' END";

        // Despues de cobrar (recibo de ingreso, retencion): vuelve a calcular el estado de cobro del documento con su registro de cuentas por cobrar.
        public static void Actualizar(string numeroDocumento)
        {
            if (string.IsNullOrWhiteSpace(numeroDocumento))
            {
                return;
            }
            string numero = numeroDocumento.Trim().Replace("\\", "\\\\").Replace("'", "''");
            ConexionBD baseDatos = new ConexionBD();
            foreach (string tabla in new[] { "admdoccli", "admdoccli2" })
            {
                baseDatos.ejecutarInsert("UPDATE " + tabla + " d LEFT JOIN admsalcli s ON s.NroDocum = d.dcli_numero AND s.cli_codigo = d.dcli_codigo " +
                                         "SET d.dcli_estatus = " + CasoSql + " WHERE d.dcli_numero = '" + numero + "'");
            }
            baseDatos.cerrarConexion();
        }
    }
}
