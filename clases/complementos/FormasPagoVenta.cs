using System;
using System.Collections.Generic;
using System.Data;
using lebrun.clases.clientes;
using lebrun.clasesData;

namespace lebrun.clases.complementos
{
    // Una forma de pago de las que se eligen al cancelar una factura (Efectivo, Tarjeta de Debito, Divisa $, ...): una fila de admcondpago
    // de tipo F. PideBanco indica si al cobrar con ella se piden el banco y el numero; Grupo es la casilla de la factura donde se suma
    // lo cobrado (E = Efectivo, T = Tarjetas, C = Cheques).
    public class FormaPagoVenta
    {
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public string Siglas { get; set; }
        public bool PideBanco { get; set; }
        public char Grupo { get; set; }
        // USD o EUR si se cobra en divisa (Divisa $, Divisa Euro); null si se cobra en bolivares
        public string Moneda { get; set; }

        public bool EsDivisa
        {
            get { return !string.IsNullOrEmpty(Moneda); }
        }

        public override string ToString()
        {
            return Descripcion;
        }
    }

    // Formas de pago de la venta. En admmovcaja.mocv_forpag se guarda el codigo de la forma (F01, F02, ...). Los movimientos anteriores a este
    // cambio guardaban un nombre (EFECTIVO, TARJETA-D, TARJETA-C, CHEQUE): CodigoDeValorGuardado los reconoce.
    public static class FormasPagoVenta
    {
        public const string Efectivo = "F01";
        public const string TarjetaDebito = "F02";
        public const string TarjetaCredito = "F03";
        public const string Cheque = "F04";
        // el vuelto que se le da al cliente se guarda como un renglon mas de la factura, pero no es una forma de pago
        public const string Cambio = "CAMBIO";

        // Las formas de pago activas. Si la tabla aun no tiene formas (no se ejecuto el script 04_formas_pago.sql) se usan las cuatro de siempre.
        public static List<FormaPagoVenta> Cargar()
        {
            List<FormaPagoVenta> lista = new List<FormaPagoVenta>();
            try
            {
                DataTable dt = new CondicionPago().FormasPago();
                foreach (DataRow r in dt.Rows)
                {
                    lista.Add(new FormaPagoVenta
                    {
                        Codigo = Convert.ToString(r["conp_codigo"]).Trim(),
                        Descripcion = Convert.ToString(r["conp_descripcion"]).Trim(),
                        Siglas = r["conp_siglas"] == DBNull.Value ? "" : Convert.ToString(r["conp_siglas"]).Trim(),
                        PideBanco = Convert.ToString(r["conp_req_banco"]) == "1",
                        Grupo = Convert.ToString(r["conp_grupo"]).Length > 0 ? Convert.ToString(r["conp_grupo"])[0] : 'E',
                        Moneda = r["conp_moneda"] == DBNull.Value || Convert.ToString(r["conp_moneda"]).Trim().Length == 0 ? null : Convert.ToString(r["conp_moneda"]).Trim().ToUpper()
                    });
                }
            }
            catch (Exception ex)
            {
                RegistroErrores.Registrar(ex, "Formas de pago");
            }
            if (lista.Count == 0)
            {
                lista.Add(new FormaPagoVenta { Codigo = Efectivo, Descripcion = "Efectivo", Siglas = "EFE", PideBanco = false, Grupo = 'E' });
                lista.Add(new FormaPagoVenta { Codigo = TarjetaDebito, Descripcion = "Tarjeta de Debito", Siglas = "TD", PideBanco = true, Grupo = 'T' });
                lista.Add(new FormaPagoVenta { Codigo = TarjetaCredito, Descripcion = "Tarjeta de Credito", Siglas = "TC", PideBanco = true, Grupo = 'T' });
                lista.Add(new FormaPagoVenta { Codigo = Cheque, Descripcion = "Cheque", Siglas = "CHQ", PideBanco = true, Grupo = 'C' });
            }
            return lista;
        }

        // Convierte lo guardado en mocv_forpag (codigo nuevo o nombre antiguo) en el codigo de la forma de pago.
        public static string CodigoDeValorGuardado(string valor)
        {
            switch ((valor ?? "").Trim().ToUpper())
            {
                case "EFECTIVO": return Efectivo;
                case "TARJETA-D": return TarjetaDebito;
                case "TARJETA-C": return TarjetaCredito;
                case "CHEQUE": return Cheque;
                default: return (valor ?? "").Trim();
            }
        }

        public static FormaPagoVenta Buscar(List<FormaPagoVenta> formas, string valorGuardado)
        {
            string codigo = CodigoDeValorGuardado(valorGuardado);
            foreach (FormaPagoVenta f in formas)
            {
                if (string.Equals(f.Codigo, codigo, StringComparison.OrdinalIgnoreCase))
                {
                    return f;
                }
            }
            return null;
        }

        // Casilla de la factura (E, T o C) donde se suma una forma ya guardada; lo desconocido se suma como efectivo.
        public static char Grupo(List<FormaPagoVenta> formas, string valorGuardado)
        {
            FormaPagoVenta f = Buscar(formas, valorGuardado);
            return f == null ? 'E' : f.Grupo;
        }

        // Texto de la etiqueta del monto en la pantalla de cobro: en divisa el cajero escribe lo que recibio en dolares o euros.
        public static string EtiquetaMonto(FormaPagoVenta forma)
        {
            return forma != null && forma.EsDivisa ? "Monto en " + TasaCambio.Simbolo(forma.Moneda) : "Monto Abonar";
        }

        // Pago en divisa: el cajero escribe el monto en la divisa, que se convierte a bolivares con la tasa del dia. Calcula los bolivares; devuelve false
        // (y un mensaje) si el monto no es valido o no hay tasa cargada para hoy.
        public static bool ConvertirADivisa(FormaPagoVenta forma, string textoMonto, out decimal original, out decimal tasa, out decimal bolivares, out string error)
        {
            original = 0m;
            tasa = 0m;
            bolivares = 0m;
            error = null;
            string moneda = TasaCambio.Nombre(forma.Moneda);
            if (!decimal.TryParse((textoMonto ?? "").Trim().Replace(".", ","), out original) || original <= 0m)
            {
                error = "Escriba el monto recibido en " + (forma.Moneda == TasaCambio.Euro ? "euros" : "dolares") + ".";
                return false;
            }
            decimal? vigente = TasaCambio.Vigente(DateTime.Today, forma.Moneda);
            if (!vigente.HasValue)
            {
                error = "No hay tasa de cambio del " + moneda + " cargada. Cargue la tasa del dia antes de cobrar en " + (forma.Moneda == TasaCambio.Euro ? "euros" : "dolares") + ".";
                return false;
            }
            tasa = vigente.Value;
            bolivares = Math.Round(original * tasa, 2, MidpointRounding.AwayFromZero);
            return true;
        }

        // Columnas de la grilla de pagos para el monto original en divisa: tres ocultas que se guardan en admmovcaja y una visible que muestra
        // lo recibido (20,00 $). Se agregan por codigo para no tocar el diseno de cada formulario.
        public static void AsegurarColumnasDivisa(System.Windows.Forms.DataGridView grilla)
        {
            AgregarColumna(grilla, "@movc_montodivisa", "", false, 0);
            AgregarColumna(grilla, "@movc_monedadivisa", "", false, 0);
            AgregarColumna(grilla, "@movc_tasadivisa", "", false, 0);
            AgregarColumna(grilla, "divisaTexto", "Divisa", true, 70);
        }

        private static void AgregarColumna(System.Windows.Forms.DataGridView grilla, string nombre, string encabezado, bool visible, int ancho)
        {
            if (grilla.Columns.Contains(nombre))
            {
                return;
            }
            System.Windows.Forms.DataGridViewTextBoxColumn c = new System.Windows.Forms.DataGridViewTextBoxColumn();
            c.Name = nombre;
            c.HeaderText = encabezado;
            c.Visible = visible;
            c.ReadOnly = true;
            c.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            if (ancho > 0)
            {
                c.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
                c.Width = ancho;
            }
            grilla.Columns.Add(c);
        }

        // Anota en la fila de pago recien agregada lo recibido en divisa, la moneda y la tasa usada.
        public static void AnotarDivisa(System.Windows.Forms.DataGridView grilla, System.Windows.Forms.DataGridViewRow fila, decimal original, string moneda, decimal tasa)
        {
            AsegurarColumnasDivisa(grilla);
            fila.Cells["@movc_montodivisa"].Value = original.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture);
            fila.Cells["@movc_monedadivisa"].Value = moneda;
            fila.Cells["@movc_tasadivisa"].Value = TasaCambio.Texto(tasa);
            fila.Cells["divisaTexto"].Value = original.ToString("n2") + " " + TasaCambio.Simbolo(moneda);
        }

        // Valor que se guarda en admmovcaja.movc_tipoctaban, igual que antes: DEBITO, CREDITO, CHEQUE o N/A.
        public static string TipoCuentaBancaria(FormaPagoVenta forma)
        {
            switch (forma.Codigo)
            {
                case TarjetaDebito: return "DEBITO";
                case TarjetaCredito: return "CREDITO";
                case Cheque: return "CHEQUE";
                default: return "N/A";
            }
        }
    }
}
