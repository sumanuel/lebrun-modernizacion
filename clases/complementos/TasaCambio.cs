using System;
using System.Data;
using System.Globalization;
using lebrun.clasesData;

namespace lebrun.clases.complementos
{
    // Tasa de cambio del dia que se graba en cada factura (admdoccli.dcli_valcamb): cuantos bolivares vale un dolar. Sale del historial de divisas
    // (admhistodivi), de la moneda $8 (dolar BCV), que es la que se actualiza todos los dias. Si el dia tiene varias tasas se usa la ultima por hora.
    public static class TasaCambio
    {
        public const string MonedaTasa = "$8";

        // La tasa mas reciente hasta la fecha indicada (si ese dia no se cargo tasa, por ejemplo un fin de semana, se usa la ultima anterior).
        // Devuelve null si el historial no tiene ninguna tasa para esa moneda.
        public static decimal? Vigente(DateTime fecha)
        {
            ConexionBD baseDatos = new ConexionBD();
            DataTable dt = baseDatos.fDataTable("SELECT histo_valor FROM admhistodivi WHERE histo_moneda = '" + MonedaTasa + "' AND histo_fecha <= '" +
                                                fecha.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture) + "' ORDER BY histo_fecha DESC, " +
                                                "STR_TO_DATE(REPLACE(REPLACE(histo_hora, 'a.m.', 'AM'), 'p.m.', 'PM'), '%h:%i:%s %p') DESC, histo_hora DESC LIMIT 1");
            baseDatos.cerrarConexion();
            if (dt.Rows.Count == 0 || dt.Rows[0][0] == DBNull.Value)
            {
                return null;
            }
            decimal valor = Convert.ToDecimal(dt.Rows[0][0]);
            return valor > 0m ? valor : (decimal?)null;
        }

        // Texto listo para ponerlo en una sentencia SQL: el numero con punto decimal, o NULL si no hay tasa.
        public static string ParaSql(DateTime fecha)
        {
            decimal? tasa = Vigente(fecha);
            return tasa.HasValue ? tasa.Value.ToString("0.00", CultureInfo.InvariantCulture) : "NULL";
        }
    }
}
