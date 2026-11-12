using System;
using System.Data;
using System.Globalization;
using lebrun.clasesData;

namespace lebrun.clases.complementos
{
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
                                                "STR_TO_DATE(REPLACE(REPLACE(histo_hora, 'a.m.', 'AM'), 'p.m.', 'PM'), '%h:%i:%s %p') DESC, histo_hora DESC LIMIT 1");
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
