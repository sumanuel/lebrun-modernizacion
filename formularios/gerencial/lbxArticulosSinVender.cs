using System;
using System.Data;
using System.Windows.Forms;

namespace lebrun.formularios.gerencial
{
    // Art. Sin Vender: productos con existencia que no se venden desde hace N dias o N meses. Sustituye a FRMARTSINVENDER del sistema anterior.
    public class lbxArticulosSinVender : lbxListadoGerencial
    {
        private readonly NumericUpDown numTiempo = new NumericUpDown();
        private readonly RadioButton optDias = new RadioButton();
        private readonly RadioButton optMeses = new RadioButton();
        private readonly CheckBox chkNunca = new CheckBox();
        private readonly CheckBox chkActivos = new CheckBox();

        protected override string TituloPantalla { get { return "Artículos sin vender"; } }
        protected override string DescripcionPantalla { get { return "Productos con existencia que no se venden desde hace el tiempo indicado, con su valor en inventario."; } }
        protected override string TituloGrilla { get { return "Productos sin movimiento de venta"; } }

        protected override void CrearFiltros()
        {
            numTiempo.Minimum = 1;
            numTiempo.Maximum = 3650;
            numTiempo.Value = 90;
            numTiempo.Width = 70;
            Filtro("Sin vender desde hace", numTiempo);
            optDias.Text = "días";
            optDias.Checked = true;
            optDias.AutoSize = true;
            optMeses.Text = "meses";
            optMeses.AutoSize = true;
            optDias.Margin = new Padding(0, 6, 6, 0);
            optMeses.Margin = new Padding(0, 6, 16, 0);
            panelFiltros.Controls.Add(optDias);
            panelFiltros.Controls.Add(optMeses);
            chkNunca.Text = "Incluir los nunca vendidos";
            chkNunca.Checked = true;
            chkNunca.AutoSize = true;
            Filtro(null, chkNunca);
            chkActivos.Text = "Solo productos activos";
            chkActivos.Checked = true;
            chkActivos.AutoSize = true;
            Filtro(null, chkActivos);
        }

        protected override DataTable Consultar()
        {
            int dias = (int)numTiempo.Value * (optMeses.Checked ? 30 : 1);
            return datos.ArticulosSinVender(dias, chkNunca.Checked, chkActivos.Checked);
        }

        protected override void DarFormato()
        {
            ColumnaTexto("Codigo", "Código", 11);
            ColumnaTexto("Descripcion", "Descripción", 34);
            ColumnaNumerica("Contenido", "n2", 8);
            ColumnaTexto("Unidad", null, 7);
            ColumnaNumerica("Existencia", "n2", 10);
            ColumnaNumerica("Precio", "n2", 10);
            ColumnaNumerica("Costo", "n2", 10);
            ColumnaNumerica("Valorizado", "n2", 12);
            ColumnaFecha("UltimaVenta", 10);
            ColumnaNumerica("Dias", "n0", 7);
            grilla.Columns["UltimaVenta"].HeaderText = "Última venta";
            grilla.Columns["Dias"].HeaderText = "Días";
            // Los nunca vendidos no tienen fecha ni dias: se marcan con un texto en lugar de dejar la celda vacia.
            grilla.CellFormatting -= MarcarNuncaVendido;
            grilla.CellFormatting += MarcarNuncaVendido;
        }

        private void MarcarNuncaVendido(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0 || !grilla.Columns.Contains("UltimaVenta"))
            {
                return;
            }
            if (grilla.Columns[e.ColumnIndex].Name == "UltimaVenta" && (e.Value == null || e.Value == DBNull.Value))
            {
                e.Value = "Nunca vendido";
                e.FormattingApplied = true;
            }
        }

        protected override string ResumenDe(DataTable tabla)
        {
            decimal total = 0m;
            int nunca = 0;
            foreach (DataRow fila in tabla.Rows)
            {
                if (fila["Valorizado"] != DBNull.Value)
                {
                    total += Convert.ToDecimal(fila["Valorizado"]);
                }
                if (fila["UltimaVenta"] == DBNull.Value)
                {
                    nunca++;
                }
            }
            return tabla.Rows.Count + (tabla.Rows.Count == 1 ? " producto" : " productos") + "   ·   " + nunca + " nunca vendidos   ·   " +
                   (ResultadoRecortado ? "Valor de los productos mostrados: " : "Valor en inventario: ") + total.ToString("n2");
        }
    }
}
