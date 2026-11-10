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
                        Grupo = Convert.ToString(r["conp_grupo"]).Length > 0 ? Convert.ToString(r["conp_grupo"])[0] : 'E'
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
