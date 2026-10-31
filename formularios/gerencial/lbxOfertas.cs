using System;
using System.Data;
using System.Windows.Forms;

namespace lebrun.formularios.gerencial
{
    // Ofertas: promociones vigentes en una fecha, con el detalle de las ventas hechas durante la promocion seleccionada.
    // Sustituye a FRMCONSULTAOFERTAS del sistema anterior.
    public class lbxOfertas : lbxListadoGerencial
    {
        private readonly DateTimePicker dtpFecha = NuevaFecha(DateTime.Today);
        private readonly CheckBox chkTodas = new CheckBox();

        protected override string TituloPantalla { get { return "Ofertas"; } }
        protected override string DescripcionPantalla { get { return "Promociones vigentes en la fecha elegida. Al seleccionar una, abajo aparecen las facturas vendidas durante la promoción."; } }
        protected override string TituloGrilla { get { return "Promociones"; } }
        protected override bool TieneDetalle { get { return true; } }
        protected override string TituloDetalle { get { return "Ventas durante la promoción"; } }

        protected override void CrearFiltros()
        {
            Filtro("Vigentes al", dtpFecha);
            chkTodas.Text = "Mostrar todas las promociones (ignora la fecha)";
            chkTodas.AutoSize = true;
            chkTodas.CheckedChanged += delegate { dtpFecha.Enabled = !chkTodas.Checked; };
            Filtro(null, chkTodas);
        }

        protected override DataTable Consultar()
        {
            return datos.Ofertas(dtpFecha.Value.Date, chkTodas.Checked);
        }

        protected override void DarFormato()
        {
            ColumnaTexto("Codigo", "Código", 11);
            ColumnaTexto("Descripcion", "Descripción", 28);
            ColumnaTexto("Unidad", null, 6);
            ColumnaNumerica("Existencia", "n2", 8);
            ColumnaFecha("Desde", 8);
            ColumnaFecha("Hasta", 8);
            ColumnaNumerica("DiasRestantes", "n0", 7);
            ColumnaNumerica("Compra", "n2", 6);
            ColumnaNumerica("Lleva", "n2", 6);
            ColumnaNumerica("PorcDescuento", "n2", 7);
            ColumnaNumerica("PrecioNormal", "n2", 9);
            ColumnaNumerica("PrecioOferta", "n2", 9);
            ColumnaFecha("UltimaVenta", 9);
            grilla.Columns["DiasRestantes"].HeaderText = "Días restantes";
            grilla.Columns["PorcDescuento"].HeaderText = "% desc.";
            grilla.Columns["PrecioNormal"].HeaderText = "Precio normal";
            grilla.Columns["PrecioOferta"].HeaderText = "Precio oferta";
            grilla.Columns["UltimaVenta"].HeaderText = "Última venta";
            // las promociones ya vencidas (dias restantes negativos) se resaltan en rojo
            grilla.CellFormatting -= ResaltarVencidas;
            grilla.CellFormatting += ResaltarVencidas;
        }

        private void ResaltarVencidas(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0 || e.Value == null || e.Value == DBNull.Value || !grilla.Columns.Contains("DiasRestantes"))
            {
                return;
            }
            if (grilla.Columns[e.ColumnIndex].Name == "DiasRestantes" && Convert.ToInt64(e.Value) < 0)
            {
                e.CellStyle.ForeColor = System.Drawing.Color.FromArgb(192, 0, 0);
            }
        }

        protected override DataTable ConsultarDetalle(DataRow fila)
        {
            return datos.VentasDuranteOferta(Convert.ToString(fila["Codigo"]), Convert.ToDateTime(fila["Desde"]), Convert.ToDateTime(fila["Hasta"]));
        }

        protected override void DarFormatoDetalle()
        {
            ColumnaFecha(grillaDetalle, "Fecha", 12);
            ColumnaTexto(grillaDetalle, "Documento", null, 16);
            ColumnaTexto(grillaDetalle, "Vendedor", null, 30);
            ColumnaNumerica(grillaDetalle, "Cantidad", "n2", 12);
            ColumnaNumerica(grillaDetalle, "Precio", "n2", 14);
        }

        protected override string ResumenDe(DataTable tabla)
        {
            return tabla.Rows.Count + (tabla.Rows.Count == 1 ? " promoción" : " promociones");
        }
    }
}
