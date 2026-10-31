using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using static lebrun.formularios.gerencial.EstiloGerencial;

namespace lebrun.formularios.gerencial
{
    // Consulta Cliente: busca un cliente y muestra su ficha, sus documentos pendientes (estado de cuenta) y el historial de sus documentos.
    // Sustituye a LBXCLIENTES_LEBRUN y FRMEDOCTACLIENTES del sistema anterior.
    public class lbxConsultaCliente : lbxConsultaTercero
    {
        protected override string TituloPantalla { get { return "Consulta de clientes"; } }
        protected override string DescripcionPantalla { get { return "Busque un cliente por código, nombre o RIF para ver sus datos, su saldo pendiente y sus documentos."; } }
        protected override string Persona { get { return "cliente"; } }

        protected override DataTable Buscar(string codigo, string nombre, string rif)
        {
            return datos.BuscarClientes(codigo, nombre, rif);
        }

        protected override List<KeyValuePair<string, string>> Ficha(string codigo)
        {
            DataTable t = datos.FichaCliente(codigo);
            List<KeyValuePair<string, string>> f = new List<KeyValuePair<string, string>>();
            if (t.Rows.Count == 0)
            {
                return f;
            }
            DataRow r = t.Rows[0];
            f.Add(Dato("Código", r["cli_codigo"]));
            f.Add(Dato("Nombre", r["cli_nombre"]));
            f.Add(Dato("RIF", r["cli_rif"]));
            f.Add(Dato("Situación", r["cli_situacion"]));
            f.Add(Dato("Dirección", Unir(r["cli_direc1"], r["cli_direc2"])));
            f.Add(Dato("Teléfono", Unir(r["cli_codarea"], r["cli_telefono"])));
            f.Add(Dato("Celular", r["cli_celular"]));
            f.Add(Dato("Correo", r["cli_correo"]));
            f.Add(Dato("Vendedor", r["vendedor"]));
            f.Add(Dato("Condición de pago", r["cli_condipag"]));
            f.Add(Dato("Límite de crédito", r["cli_lincredito"] == DBNull.Value ? "" : Convert.ToDecimal(r["cli_lincredito"]).ToString("n2")));
            f.Add(Dato("Cliente desde", r["fechareg"] == DBNull.Value ? "" : Convert.ToDateTime(r["fechareg"]).ToString("dd/MM/yyyy")));
            return f;
        }

        protected override DataTable Pendientes(string codigo) { return datos.PendientesCliente(codigo); }
        protected override DataTable Historial(string codigo) { return datos.HistorialCliente(codigo); }

        protected override void FormatoPendientes(DataGridView g)
        {
            ColFecha(g, "Emision", "Emisión", 11);
            ColFecha(g, "Vencimiento", "Vencimiento", 12);
            ColTexto(g, "Tipo", null, 7);
            ColTexto(g, "Documento", null, 16);
            ColTexto(g, "NumeroFiscal", "N° fiscal", 14);
            ColNumerica(g, "Monto", "n2", 14);
            ColNumerica(g, "Saldo", "n2", 14);
            ColNumerica(g, "DiasVencido", "n0", 9);
            if (g.Columns.Contains("DiasVencido")) { g.Columns["DiasVencido"].HeaderText = "Días vencido"; }
        }

        protected override void FormatoHistorial(DataGridView g)
        {
            ColFecha(g, "Fecha", null, 11);
            ColTexto(g, "Tipo", null, 7);
            ColTexto(g, "Documento", null, 16);
            ColNumerica(g, "Monto", "n2", 14);
            ColTexto(g, "FacturaAplica", "Factura que aplica", 16);
            ColTexto(g, "NumeroFiscal", "N° fiscal", 14);
        }
    }

    // Consulta Proveedor: busca un proveedor y muestra su ficha, sus documentos por pagar y el historial de registros y pagos.
    // Sustituye a LBXPROVEEDORES_LEBRUN y FRMEDOCTAPROVEEDOR del sistema anterior.
    public class lbxConsultaProveedor : lbxConsultaTercero
    {
        protected override string TituloPantalla { get { return "Consulta de proveedores"; } }
        protected override string DescripcionPantalla { get { return "Busque un proveedor por código, nombre o RIF para ver sus datos, lo que se le debe y el historial de sus documentos y pagos."; } }
        protected override string Persona { get { return "proveedor"; } }

        protected override DataTable Buscar(string codigo, string nombre, string rif)
        {
            return datos.BuscarProveedores(codigo, nombre, rif);
        }

        protected override List<KeyValuePair<string, string>> Ficha(string codigo)
        {
            DataTable t = datos.FichaProveedor(codigo);
            List<KeyValuePair<string, string>> f = new List<KeyValuePair<string, string>>();
            if (t.Rows.Count == 0)
            {
                return f;
            }
            DataRow r = t.Rows[0];
            f.Add(Dato("Código", r["pro_codigo"]));
            f.Add(Dato("Nombre", r["pro_nombre"]));
            f.Add(Dato("RIF", r["pro_rif"]));
            f.Add(Dato("Situación", r["pro_situacion"]));
            f.Add(Dato("País", r["pais_descri"]));
            f.Add(Dato("Dirección", Unir(r["pro_direcc1"], r["pro_direcc2"])));
            f.Add(Dato("Teléfonos", Unir(r["pro_codarea"], r["pro_telefono1"], r["pro_telefono2"])));
            f.Add(Dato("Celular", r["pro_cel1"]));
            f.Add(Dato("Correo", r["pro_correo"]));
            f.Add(Dato("Condición de pago", r["pro_condipago"]));
            f.Add(Dato("Límite de crédito", r["pro_limcre"] == DBNull.Value ? "" : Convert.ToDecimal(r["pro_limcre"]).ToString("n2")));
            f.Add(Dato("Días de plazo", r["pro_diaspla"] == DBNull.Value ? "" : Convert.ToDecimal(r["pro_diaspla"]).ToString("n0")));
            f.Add(Dato("Moneda", r["pro_divisa"]));
            return f;
        }

        protected override DataTable Pendientes(string codigo) { return datos.PendientesProveedor(codigo); }
        protected override DataTable Historial(string codigo) { return datos.HistorialProveedor(codigo); }

        protected override void FormatoPendientes(DataGridView g)
        {
            ColFecha(g, "Emision", "Emisión", 11);
            ColFecha(g, "Vencimiento", "Vencimiento", 12);
            ColTexto(g, "Tipo", null, 6);
            ColTexto(g, "Documento", null, 14);
            ColTexto(g, "Sufijo", "Suf.", 6);
            ColTexto(g, "Descripcion", "Descripción", 26);
            ColNumerica(g, "Monto", "n2", 13);
            ColNumerica(g, "Saldo", "n2", 13);
            ColNumerica(g, "DiasVencido", "n0", 9);
            if (g.Columns.Contains("DiasVencido")) { g.Columns["DiasVencido"].HeaderText = "Días vencido"; }
        }

        protected override void FormatoHistorial(DataGridView g)
        {
            ColFecha(g, "Fecha", null, 10);
            ColTexto(g, "Operacion", "Operación", 10);
            ColTexto(g, "Tipo", null, 7);
            ColTexto(g, "Documento", null, 14);
            ColTexto(g, "Descripcion", "Descripción", 34);
            ColNumerica(g, "Monto", "n2", 13);
            g.CellFormatting -= NombreOperacion;
            g.CellFormatting += NombreOperacion;
        }

        // La consulta devuelve la operacion como numero (1 = registro, 2 = pago): se muestra con su nombre.
        private void NombreOperacion(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridView g = (DataGridView)sender;
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && g.Columns[e.ColumnIndex].Name == "Operacion" && e.Value != null && e.Value != DBNull.Value)
            {
                e.Value = Convert.ToInt32(e.Value) == 2 ? "Pago" : "Registro";
                e.FormattingApplied = true;
            }
        }
    }
}
