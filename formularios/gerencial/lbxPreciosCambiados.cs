using System;
using System.Data;
using System.Windows.Forms;

namespace lebrun.formularios.gerencial
{
    // Precios Cambiados: lineas de factura vendidas a un precio distinto del original. Sustituye a FRMARTIPRECAMB del sistema anterior.
    public class lbxPreciosCambiados : lbxListadoGerencial
    {
        private readonly DateTimePicker dtpDesde = NuevaFecha(new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1));
        private readonly DateTimePicker dtpHasta = NuevaFecha(DateTime.Today);
        private readonly ComboBox cboVendedor = new ComboBox();
        private readonly CheckBox chkCeros = new CheckBox();

        protected override string TituloPantalla { get { return "Precios cambiados"; } }
        protected override string DescripcionPantalla { get { return "Productos facturados a un precio distinto del original, con la diferencia y el porcentaje. Las líneas con precio o cantidad en cero (mercancía retirada) se ocultan salvo que se pida lo contrario."; } }
        protected override string TituloGrilla { get { return "Facturas con precio cambiado"; } }

        protected override void CrearFiltros()
        {
            Filtro("Desde", dtpDesde);
            Filtro("hasta", dtpHasta);
            cboVendedor.DropDownStyle = ComboBoxStyle.DropDownList;
            cboVendedor.Width = 180;
            DataTable vendedores = datos.Vendedores();
            DataRow todos = vendedores.NewRow();
            todos["Codigo"] = "";
            todos["Nombre"] = "(Todos los vendedores)";
            vendedores.Rows.InsertAt(todos, 0);
            cboVendedor.DataSource = vendedores;
            cboVendedor.DisplayMember = "Nombre";
            cboVendedor.ValueMember = "Codigo";
            Filtro("Vendedor", cboVendedor);
            chkCeros.Text = "Incluir líneas con precio o cantidad en cero";
            chkCeros.AutoSize = true;
            Filtro(null, chkCeros);
        }

        protected override DataTable Consultar()
        {
            if (dtpDesde.Value.Date > dtpHasta.Value.Date)
            {
                throw new ArgumentException("La fecha «desde» no puede ser mayor que la fecha «hasta».");
            }
            string vendedor = Convert.ToString(cboVendedor.SelectedValue);
            return datos.PreciosCambiados(dtpDesde.Value.Date, dtpHasta.Value.Date, vendedor, chkCeros.Checked);
        }

        protected override void DarFormato()
        {
            ColumnaFecha("Fecha", 8);
            ColumnaTexto("Documento", null, 10);
            ColumnaTexto("Codigo", "Código", 10);
            ColumnaTexto("Descripcion", "Descripción", 24);
            ColumnaTexto("Vendedor", null, 10);
            ColumnaTexto("Cliente", null, 18);
            ColumnaNumerica("Cantidad", "n2", 7);
            ColumnaNumerica("PrecioOriginal", "n2", 9);
            ColumnaNumerica("PrecioVenta", "n2", 9);
            ColumnaNumerica("Diferencia", "n2", 8);
            ColumnaNumerica("Porcentaje", "n2", 7);
            grilla.Columns["PrecioOriginal"].HeaderText = "Precio original";
            grilla.Columns["PrecioVenta"].HeaderText = "Precio de venta";
            grilla.Columns["Porcentaje"].HeaderText = "% dif.";
        }

        protected override string ResumenDe(DataTable tabla)
        {
            int bajas = 0;
            int altas = 0;
            foreach (DataRow fila in tabla.Rows)
            {
                decimal dif = Convert.ToDecimal(fila["Diferencia"]);
                if (dif < 0m)
                {
                    bajas++;
                }
                else if (dif > 0m)
                {
                    altas++;
                }
            }
            return tabla.Rows.Count + (tabla.Rows.Count == 1 ? " línea" : " líneas") + "   ·   " + bajas + " a menor precio   ·   " + altas + " a mayor precio";
        }
    }
}
