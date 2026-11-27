using System;
using System.Data;
using System.Globalization;
using lebrun.clasesData;

namespace lebrun.clases.complementos
{
    // Las horas de los cambios (admprecios.pre_hora, admhistodivi.histo_hora) estan guardadas como texto y su forma depende de la configuracion de Windows
    // del equipo que las grabo: "11:25:19 a. m." (con cualquier tipo de espacio), "05:15:35 p.m.", "03:36:38 PM" o "16:40:28".
    public static class HoraGuardada
    {
        // Expresion SQL (MySQL 5.5 y MariaDB) que convierte la columna a un TIME: toma los numeros separados por ':' y mira solo si el texto dice 'p' o 'a'.
        public static string Sql(string columna)
        {
            string texto = "LOWER(IFNULL(" + columna + ", ''))";
            string hora = "CAST(SUBSTRING_INDEX(" + texto + ", ':', 1) AS UNSIGNED)";
            string minuto = "CAST(SUBSTRING_INDEX(SUBSTRING_INDEX(" + texto + ", ':', 2), ':', -1) AS UNSIGNED)";
            string segundo = "IF(LENGTH(" + texto + ") - LENGTH(REPLACE(" + texto + ", ':', '')) > 1, CAST(SUBSTRING_INDEX(" + texto + ", ':', -1) AS UNSIGNED), 0)";
            string hora24 = "CASE WHEN " + texto + " LIKE '%p%' THEN MOD(" + hora + ", 12) + 12 WHEN " + texto + " LIKE '%a%' THEN MOD(" + hora + ", 12) ELSE " + hora + " END";
            return "SEC_TO_TIME((" + hora24 + ") * 3600 + " + minuto + " * 60 + " + segundo + ")";
        }
    }

    // Tasa de cambio del dia: cuantos bolivares vale una unidad de divisa. Sale del historial de divisas (admhistodivi). Las monedas son USD (dolar BCV, moneda $8)
    // y EUR (euro, moneda del euro con el 8). Se usa para grabar la tasa en cada factura (admdoccli.dcli_valcamb) y para convertir a bolivares lo que el cliente
    // paga en divisa. Si el dia tiene varias tasas se usa la ultima por hora.
    public static class TasaCambio
    {
        public const string MonedaTasa = "$8";
        public const string Dolar = "USD";
        public const string Euro = "EUR";

        // Codigo de la moneda en admhistodivi, en hexadecimal: el euro esta guardado como un solo byte (0x80), por eso se compara en binario y no como texto.
        private static string CodigoHistorial(string moneda)
        {
            return string.Equals(moneda, Euro, StringComparison.OrdinalIgnoreCase) ? "0x8038" : "0x2438";
        }

        // Simbolo y nombre de la moneda para mostrar en pantalla.
        public static string Simbolo(string moneda)
        {
            return string.Equals(moneda, Euro, StringComparison.OrdinalIgnoreCase) ? "€" : "$";
        }

        public static string Nombre(string moneda)
        {
            return string.Equals(moneda, Euro, StringComparison.OrdinalIgnoreCase) ? "euro" : "dolar";
        }

        // La tasa mas reciente hasta la fecha indicada (si ese dia no se cargo tasa, por ejemplo un fin de semana, se usa la ultima anterior).
        // Devuelve null si el historial no tiene ninguna tasa para esa moneda.
        public static decimal? Vigente(DateTime fecha, string moneda)
        {
            ConexionBD baseDatos = new ConexionBD();
            DataTable dt = baseDatos.fDataTable("SELECT histo_valor FROM admhistodivi WHERE BINARY histo_moneda = " + CodigoHistorial(moneda) + " AND histo_fecha <= '" +
                                                fecha.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture) + "' ORDER BY histo_fecha DESC, " +
                                                HoraGuardada.Sql("histo_hora") + " DESC, histo_hora DESC LIMIT 1");
            baseDatos.cerrarConexion();
            if (dt.Rows.Count == 0 || dt.Rows[0][0] == DBNull.Value)
            {
                return null;
            }
            decimal valor = Convert.ToDecimal(dt.Rows[0][0]);
            return valor > 0m ? valor : (decimal?)null;
        }

        // La del dolar, que es la que se graba en las facturas.
        public static decimal? Vigente(DateTime fecha)
        {
            return Vigente(fecha, Dolar);
        }

        // La tasa se guarda y se usa con todos sus decimales (el BCV publica 8): nunca se redondea a 2.
        public const string FormatoTasa = "0.########";

        // Texto de la tasa con punto decimal y hasta 8 decimales, sin ceros de relleno (36,5 -> "36.5").
        public static string Texto(decimal tasa)
        {
            return tasa.ToString(FormatoTasa, CultureInfo.InvariantCulture);
        }

        // Texto listo para ponerlo en una sentencia SQL: el numero con punto decimal, o NULL si no hay tasa.
        public static string ParaSql(DateTime fecha)
        {
            decimal? tasa = Vigente(fecha);
            return tasa.HasValue ? Texto(tasa.Value) : "NULL";
        }
    }
}
