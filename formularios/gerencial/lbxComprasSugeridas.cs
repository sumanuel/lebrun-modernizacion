using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using lebrun.clases.gerencial;
using lebrun.clasesData;
using static lebrun.formularios.gerencial.EstiloGerencial;

namespace lebrun.formularios.gerencial
{
    // Compras Sugeridas: para un proveedor, cuanto conviene comprar de cada producto segun lo vendido y la existencia.
    // Sustituye a FRMCOMPSUGE del sistema anterior (ver ComprasSugeridas).
    public class lbxComprasSugeridas : lbxListadoGerencial
    {
        private sealed class ItemProveedor
        {
            public readonly string Codigo;
            public readonly string Nombre;

            public ItemProveedor(string codigo, string nombre)
            {
                Codigo = codigo;
                Nombre = nombre;
            }

            public override string ToString()
            {
                return Nombre + "  ·  " + Codigo;
            }
        }

        private readonly ComprasSugeridas compras = new ComprasSugeridas();
        private readonly ComboBox cboProveedor = new ComboBox();
        private readonly ComboBox cboBase = new ComboBox();
        private readonly NumericUpDown nudCobertura = new NumericUpDown();
        private readonly ComboBox cboCosto = new ComboBox();
        private readonly CheckBox chkSoloComprar = new CheckBox();
        private string resumenProveedor;

        protected override string TituloPantalla { get { return "Compras sugeridas"; } }
        protected override string DescripcionPantalla { get { return "Elija un proveedor y el período de ventas que sirve de base: se sugiere comprar lo que falta para cubrir los meses deseados con el promedio mensual vendido."; } }
        protected override string TituloGrilla { get { return "Productos del proveedor (promedios = unidades vendidas por mes)"; } }

        protected override void CrearFiltros()
        {
            cboProveedor.DropDownStyle = ComboBoxStyle.DropDown;
            cboProveedor.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cboProveedor.AutoCompleteSource = AutoCompleteSource.ListItems;
            cboProveedor.Width = 280;
            try
            {
                foreach (DataRow r in compras.Proveedores().Rows)
                {
                    cboProveedor.Items.Add(new ItemProveedor(Convert.ToString(r["Codigo"]).Trim(), Convert.ToString(r["Nombre"]).Trim()));
                }
            }
            catch (Exception ex)
            {
                RegistroErrores.Registrar(ex, "Proveedores de " + TituloPantalla);
                MessageBox.Show("No se pudo cargar la lista de proveedores.\n\n" + ex.Message, TituloPantalla, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            Filtro("Proveedor", cboProveedor);

            cboBase.DropDownStyle = ComboBoxStyle.DropDownList;
            cboBase.Items.AddRange(ComprasSugeridas.Bases);
            cboBase.Width = 90;
            Filtro("Promedio de", cboBase);

            nudCobertura.DecimalPlaces = 1;
            nudCobertura.Increment = 0.5m;
            nudCobertura.Minimum = 0.5m;
            nudCobertura.Maximum = 24m;
            nudCobertura.Width = 60;
            Filtro("Meses a cubrir", nudCobertura);
            // por omision se cubre el mismo tiempo del periodo base, como en el sistema anterior; se puede cambiar
            cboBase.SelectedIndexChanged += delegate { nudCobertura.Value = ComprasSugeridas.MesesDeBase[cboBase.SelectedIndex]; };
            cboBase.SelectedIndex = 2;

            cboCosto.DropDownStyle = ComboBoxStyle.DropDownList;
            cboCosto.Items.AddRange(new object[] { "Último costo", "Reposición" });
            cboCosto.SelectedIndex = 0;
            cboCosto.Width = 100;
            Filtro("Valorar con", cboCosto);

            chkSoloComprar.Text = "Solo los que hay que comprar";
            chkSoloComprar.AutoSize = true;
            Filtro(null, chkSoloComprar);
        }

        protected override DataTable Consultar()
        {
            ItemProveedor proveedor = cboProveedor.SelectedItem as ItemProveedor;
            if (proveedor == null)
            {
                foreach (object o in cboProveedor.Items)
                {
                    if (string.Equals(o.ToString(), cboProveedor.Text.Trim(), StringComparison.CurrentCultureIgnoreCase))
                    {
                        proveedor = (ItemProveedor)o;
                    }
                }
            }
            if (proveedor == null)
            {
                throw new ArgumentException("Elija un proveedor de la lista.");
            }
            return compras.Calcular(proveedor.Codigo, cboBase.SelectedIndex, nudCobertura.Value, cboCosto.SelectedIndex == 1, chkSoloComprar.Checked, DateTime.Today, out resumenProveedor);
        }

        protected override void DarFormato()
        {
            ColumnaTexto("Codigo", "Código", 17);
            ColumnaTexto("Descripcion", "Descripción", 32);
            ColumnaTexto("Unidad", "Und.", 5);
            ColumnaNumerica("Existencia", "n2", 11);
            ColumnaNumerica("Prom15D", "n2", 8);
            ColumnaNumerica("Prom1M", "n2", 8);
            ColumnaNumerica("Prom3M", "n2", 8);
            ColumnaNumerica("Prom6M", "n2", 8);
            ColumnaNumerica("Prom12M", "n2", 8);
            ColumnaNumerica("VendidoMes", "n2", 8);
            ColumnaNumerica("MesesExistencia", "n2", 8);
            ColumnaNumerica("AComprar", "n2", 9);
            ColumnaNumerica("Costo", "n2", 10);
            ColumnaTexto("BaseCosto", "Costo según", 13);
            ColumnaNumerica("PrecioLista", "n2", 9);
            ColumnaNumerica("TotalAComprar", "n2", 12);
            Encabezado("Prom15D", "Prom. 15d");
            Encabezado("Prom1M", "Prom. 1m");
            Encabezado("Prom3M", "Prom. 3m");
            Encabezado("Prom6M", "Prom. 6m");
            Encabezado("Prom12M", "Prom. 12m");
            Encabezado("VendidoMes", "Vendido mes");
            Encabezado("MesesExistencia", "Meses exist.");
            Encabezado("AComprar", "A comprar");
            Encabezado("PrecioLista", "Precio lista");
            Encabezado("TotalAComprar", "Total");
            grilla.CellFormatting -= Formatear;
            grilla.CellFormatting += Formatear;
        }

        private void Encabezado(string columna, string texto)
        {
            if (grilla.Columns.Contains(columna))
            {
                grilla.Columns[columna].HeaderText = texto;
            }
        }

        // La base del costo sale con su nombre y lo que hay que comprar, en azul y negrita.
        private void Formatear(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0 || e.Value == null || e.Value == DBNull.Value)
            {
                return;
            }
            string columna = grilla.Columns[e.ColumnIndex].Name;
            if (columna == "BaseCosto")
            {
                int baseCosto = Convert.ToInt32(e.Value);
                e.Value = baseCosto == 1 ? "Reposición" : baseCosto == 2 ? "Último costo" : "Sin costo";
                e.FormattingApplied = true;
            }
            else if ((columna == "AComprar" || columna == "TotalAComprar") && Convert.ToDecimal(e.Value) > 0m)
            {
                e.CellStyle.ForeColor = Azul;
                e.CellStyle.Font = new Font(Fuente, 9F, FontStyle.Bold);
            }
        }

        protected override string ResumenDe(DataTable tabla)
        {
            return resumenProveedor;
        }
    }
}
