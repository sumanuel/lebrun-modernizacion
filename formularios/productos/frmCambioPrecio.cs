using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using lebrun.clases.complementos;
using lebrun.formularios.complementos;
using lebrun.clasesData;
using lebrun.Reportes;
using lebrun.Reportes.producto;

namespace lebrun.formularios.productos
{
    public partial class frmCambioPrecio : Form
    {

        private frmConsultaArticulos productos;
        private Producto cproducto;
        private FuncionesTexbox textBox;
        private int centinelaFila = -1;
        private Principal Principal;
        private DataTable tabla;
        //private frmReporte contenedor;
        private frmConteProductos contenedor;

        public frmCambioPrecio()
        {
            InitializeComponent();
            productos = new frmConsultaArticulos();
            cproducto = new Producto();
            textBox = new FuncionesTexbox();
            tabla = new DataTable();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            productos = frmConsultaArticulos.DefInstance;
            productos.destino(this, this.Name);
            productos.MdiParent = this.MdiParent;
            productos.Show();
        
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool consultaRapidaProducto() //PRODUCTO
        {
            bool val = false;

            DataTable dproducto = new DataTable();
            dproducto = cproducto.buscarProductoUnico(txtCodigo.Text);

            if (dproducto.Rows.Count != 0)
            {
                txtCodigo.Text = dproducto.Rows[0]["Codigo"].ToString();
                lblDescripcionPro.Text = dproducto.Rows[0]["Descripcion"].ToString();
                lblUnidad.Text = dproducto.Rows[0]["Unidad"].ToString();
                lblDescriUni.Text = dproducto.Rows[0]["Presentacion"].ToString();

                val = true;

            }
            else
            {
                MessageBox.Show("No existe artículos registrado con este codigo ó está inactivo", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCodigo.Text = "";
                lblDescripcionPro.Text = "...";
                lblUnidad.Text = "...";
                lblDescriUni.Text = "...";

                val = false;

            }
            return val;
        }

        private void txtCodigo_Leave(object sender, EventArgs e)
        {
            if (txtCodigo.Text != "")
            {
                if (!consultaRapidaProducto()) { txtCodigo.Focus(); }

                tabla = cproducto.off(txtCodigo.Text.PadLeft(15, '0'));
                if (tabla.Rows.Count > 0) { lblAdvertencia.Visible = true; } else { lblAdvertencia.Visible = false; }
               
            }
        }

        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {

            textBox.onlyNumberWithTab(sender, e);
            
        }

        private bool documentoAgregado()
        {

            for (int i = 0; i < gridArticulos.Rows.Count; i++)
            {
                if ((gridArticulos.Rows[i].Cells[2].Value.ToString() == txtCodigo.Text && gridArticulos.Rows[i].Cells[1].Value.ToString()==cmbTipoPrecio.Text))
                {
                    MessageBox.Show("Este articulo ya fue agregado", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
                }
            }
            return false;
        }

        public void agregarFilas(int fila, DataGridView grid)
        {

            if (grid.Name.Equals("gridArticulos"))
            {
                if (fila < 0)
                {
                    grid.Rows.Add();
                    fila = grid.Rows.Count - 1;
                }

                cproducto.buscarPrecios(txtCodigo.Text, cmbTipoPrecio.Text);

                grid.Rows[fila].Cells[0].Value = fila + 1;
                grid.Rows[fila].Cells[1].Value = cmbTipoPrecio.Text;
                grid.Rows[fila].Cells[2].Value = txtCodigo.Text;
                grid.Rows[fila].Cells[3].Value = lblDescripcionPro.Text;
                grid.Rows[fila].Cells[4].Value = lblUnidad.Text;
                grid.Rows[fila].Cells[5].Value = "" + cproducto.Precio;
                grid.Rows[fila].Cells[6].Value = txtPrecio.Text;
                grid.Rows[fila].Cells[7].Value = txtUtilidad.Text;
                grid.Rows[fila].Cells[8].Value = txtDescuento.Text;
                grid.Rows[fila].Cells[9].Value = txtDescMax.Text;
                grid.Rows[fila].Cells[10].Value = (Convert.ToInt16(fechaVencimiento.Value.Year) + 20) + "-" + fechaVencimiento.Value.Month.ToString().PadLeft(2, '0') + "-" + fechaVencimiento.Value.Day.ToString().PadLeft(2, '0');
                grid.Rows[fila].Cells[11].Value = fechaVencimiento.Value.Day.ToString().PadLeft(2, '0') + "-" + fechaVencimiento.Value.Month.ToString().PadLeft(2, '0') + "-" + fechaVencimiento.Value.Year;
                grid.Rows[fila].Cells[12].Value = "1";//Principal.usuarioActual.Id;
                grid.Rows[fila].Cells[13].Value = txt_precioDolar.Text;
                grid.Rows[fila].Cells[14].Value = txt_precioA.Text;
            }
        }

        private bool validarCamposGrid()
        {
            bool flag;
            flag = false;

            if (checkBox1.Checked == false)
            {
                if (cmbTipoPrecio.Text != "")
                {
                    flag = true;
                }
                else { flag = false; MessageBox.Show("Debe seleccionar un precio lista", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information); cmbTipoPrecio.Focus(); return false; }

                if (txtCodigo.Text != "")
                {
                    flag = true;
                }
                else { flag = false; MessageBox.Show("Debe selecionar algun articulo", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information); txtCodigo.Focus(); return false; }


                if (txtPrecio.Text != "")
                {
                    flag = true;
                }
                else { flag = false; MessageBox.Show("Debe agregar un precio al articulo", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information); txtPrecio.Focus(); return false; }

                if (Convert.ToDecimal(txtPrecio.Text) != 0)
                {
                    flag = true;
                }
                else { flag = false; MessageBox.Show("El precio del articulo no debe ser igual a cero(0)", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information); txtPrecio.Focus(); return false; }

                if (txtUtilidad.Text != "")
                {
                    flag = true;
                }
                else { flag = false; MessageBox.Show("La utilidad no debe quedar vacia", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information); txtUtilidad.Focus(); return false; }


                if (txtDescuento.Text != "")
                {
                    flag = true;
                }
                else { flag = false; MessageBox.Show("El descuento no debe quedar vacio", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information); txtDescuento.Focus(); return false; }

                if (txtDescMax.Text != "")
                {
                    flag = true;
                }
                else { flag = false; MessageBox.Show("El descuento maximo no debe quedar vacio", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information); txtDescMax.Focus(); return false; }

                if (lblUnidad.Text != "")
                {
                    flag = true;
                }
                else { flag = false; MessageBox.Show("El codigo seleccionado no tiene unidad principal asignada", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information); txtDescMax.Focus(); return false; }

                if (txt_precioDolar.Text != "")
                {
                    flag = true;
                }
                else { flag = false; MessageBox.Show("El precio_ext no debe quedar vacio", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information); txtDescMax.Focus(); return false; }

                if (txt_precioA.Text != "")
                {
                    flag = true;
                }
                else { flag = false; MessageBox.Show("El precio_A no debe quedar vacio", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information); txtDescMax.Focus(); return false; }

            }
            else
            {
                if (cmbTipoPrecio.Text != "")
                {
                    flag = true;
                }
                else { flag = false; MessageBox.Show("Debe seleccionar un precio lista", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information); cmbTipoPrecio.Focus(); return false; }

                if (txtCodigo.Text != "")
                {
                    flag = true;
                }
                else { flag = false; MessageBox.Show("Debe selecionar algun articulo", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information); txtCodigo.Focus(); return false; }

                if (txtUtilidad.Text != "")
                {
                    flag = true;
                }
                else { flag = false; MessageBox.Show("La utilidad no debe quedar vacia", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information); txtUtilidad.Focus(); return false; }


                if (txtDescuento.Text != "")
                {
                    flag = true;
                }
                else { flag = false; MessageBox.Show("El descuento no debe quedar vacio", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information); txtDescuento.Focus(); return false; }

                if (txtDescMax.Text != "")
                {
                    flag = true;
                }
                else { flag = false; MessageBox.Show("El descuento maximo no debe quedar vacio", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information); txtDescMax.Focus(); return false; }

                if (lblUnidad.Text != "")
                {
                    flag = true;
                }
                else { flag = false; MessageBox.Show("El codigo seleccionado no tiene unidad principal asignada", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information); txtDescMax.Focus(); return false; }

                if (txt_precioDolar.Text != "")
                {
                    flag = true;
                }
                else { flag = false; MessageBox.Show("El precio_ext no debe quedar vacio", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information); txtDescMax.Focus(); return false; }

                if (txt_precioA.Text != "")
                {
                    flag = true;
                }
                else { flag = false; MessageBox.Show("El precio_A no debe quedar vacio", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information); txtDescMax.Focus(); return false; }
            }


            return flag;

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (validarCamposGrid())
            {
                if (documentoAgregado())
                {
                }
                else
                {
                    if (centinelaFila >= 0)
                    {

                        agregarFilas(centinelaFila, gridArticulos);
                        centinelaFila = -1;
                    }
                    else
                    {
                        agregarFilas(-1, gridArticulos);
                    }

                }
                limpiarCampos();
                bloquearPrecio();
                txtCodigo.Focus();
            }
        }



        private void actualizarNumeroItems(DataGridView grid)
        {
            for (int i = 0; i < grid.Rows.Count; i++)
            {
                grid.Rows[i].Cells[0].Value = i + 1;
            }
        }

        private void eliminarItems(DataGridView grid)
        {

            if (grid.Rows.Count > 0)
            {
                grid.Rows.RemoveAt(grid.CurrentRow.Index);
                actualizarNumeroItems(grid);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            eliminarItems(gridArticulos);
            bloquearPrecio();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (gridArticulos.Rows.Count > 0)
            {

                if (checkBox1.Checked == false)
                {
                    reemplazando();
                    cproducto.inhabilitarPrecio(gridArticulos);
                    cproducto.habilitarNuevoPrecio(gridArticulos);
                }
                else
                {
                    reemplazando();
                    cproducto.actualizarPrecio_ext(gridArticulos);
                }

                

                contenedor = new frmConteProductos("CambioPrecio", contenidoDataTable());
                contenedor.Show();

                this.Close();
            }
        }

        private void limpiarCampos()
        {

            txtCodigo.Text = "";
            lblDescripcionPro.Text = "...";
            lblDescriUni.Text = "...";
            lblUnidad.Text = "...";
            txtPrecio.Text = "0";
            txtUtilidad.Text = "0";
            txtDescMax.Text = "0";
            txt_precioA.Text = "0";
            txt_precioDolar.Text = "0";

        }

        private void cmbTipoPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                SendKeys.Send("{TAB}");
            }
            else { e.Handled = true; }
        }

        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox a = (TextBox)sender;
            textBox.conDecimalesDefinidos(sender, e, a, 2);
        }

        private void reemplazando()
        {
            for (int i = 0; i < gridArticulos.Rows.Count; i++)
            {
                gridArticulos.Rows[i].Cells[14].Value = gridArticulos.Rows[i].Cells[6].Value.ToString().Replace(",", ".");
            }
        }

        private void bloquearPrecio()
        {
            if (gridArticulos.Rows.Count > 0)
            {
                cmbTipoPrecio.Enabled = false;
            }
            else
            {
                cmbTipoPrecio.Enabled = true;
            }

        }

        private void frmCambioPrecio_Load(object sender, EventArgs e)
        {
            Principal = (Principal)this.MdiParent;
            cmbTipoPrecio.SelectedIndex = 0;
        }

        private void txtPrecio_Leave(object sender, EventArgs e)
        {
            if (txtPrecio.Text == "") { txtPrecio.Text = "0"; }
            
            if (Convert.ToDecimal(txtPrecio.Text) != 0)
            {
                txtPrecio.Text = string.Format("{0:0.00}", Convert.ToDecimal(txtPrecio.Text));
            }
            
        }

        private void armarDataTable()
        {
            tabla.Reset();

            tabla.Columns.Add("Tipo");//1
            tabla.Columns.Add("Codigo");//2
            tabla.Columns.Add("Descripcion");//3
            tabla.Columns.Add("Unidad");//4
            tabla.Columns.Add("PrecioAnterior");//5
            tabla.Columns.Add("PrecioNuevo");//6
            tabla.Columns.Add("FechaSistema");//7
            tabla.Columns.Add("HoraSistema");//8
        }

        private DataTable contenidoDataTable() {

            armarDataTable();

            for (int i = 0; i < gridArticulos.Rows.Count; i++) {

                DataRow fila = tabla.NewRow();

                fila[0] = gridArticulos.Rows[i].Cells[1].Value;
                fila[1] = gridArticulos.Rows[i].Cells[2].Value;
                fila[2] = gridArticulos.Rows[i].Cells[3].Value;
                fila[3] = gridArticulos.Rows[i].Cells[4].Value;
                fila[4] = gridArticulos.Rows[i].Cells[5].Value;
                fila[5] = gridArticulos.Rows[i].Cells[6].Value;
                fila[6] = "" + DateTime.Now.Day.ToString() + "/" + DateTime.Now.Month.ToString().PadLeft(2,'0') + "/" + DateTime.Now.Year.ToString();
                fila[7] = DateTime.Now.ToString("hh:mm:ss tt");
   
                tabla.Rows.Add(fila);
            }
            return tabla;
        }

        private void txtDescuento_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_precioDolar_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox a = (TextBox)sender;
            textBox.conDecimalesDefinidos(sender, e, a, 2);
        }

        private void txt_precioA_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox a = (TextBox)sender;
            textBox.conDecimalesDefinidos(sender, e, a, 2);
        }

        private void btn_masiva_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("¿Esta seguro de actualizar todos los precios de venta? esta operación es irreversible", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                //cproducto.habilitarNuevoPrecioMasivo("1");//Principal.usuarioActual.Id;
                Control.CheckForIllegalCrossThreadCalls = false;
                if (!backgroundWorker1.IsBusy)
                {
                    this.progressBar1.Visible = true;

                    backgroundWorker1.RunWorkerAsync();
                }
                else
                {
                    this.progressBar1.Visible = false;
                }
            }
            else if (dialogResult == DialogResult.No)
            {
                //do something else
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                cproducto.habilitarNuevoPrecioMasivo("1");//Principal.usuarioActual.Id;
            }
            catch
            {

            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
                MessageBox.Show("Se ha cancelado el proceso.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (e.Error != null)
                MessageBox.Show("ha ocurrido un error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                MessageBox.Show("Se realizó la actualización correctamente", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.progressBar1.Visible = false;
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }








    }
}
