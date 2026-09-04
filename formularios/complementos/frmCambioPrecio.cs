using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using lebrun.clases.complementos;
using lebrun.clasesData;

namespace lebrun.formularios.complementos
{
    public partial class frmCambioPrecio : Form
    {

        private frmConsultaArticulos productos;
        private Producto cproducto;
        private FuncionesTexbox textBox;
        private int centinelaFila = -1;
        private Principal Principal;
        private DataTable tabla;

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
                grid.Rows[fila].Cells[12].Value = Principal.usuarioActual.Id;
            }
        }

        private bool validarCamposGrid()
        {

            bool flag;
            flag = false;

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
            reemplazando();
            cproducto.inhabilitarPrecio(gridArticulos);
            cproducto.habilitarNuevoPrecio(gridArticulos);
            this.Close();
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
                gridArticulos.Rows[i].Cells[6].Value = gridArticulos.Rows[i].Cells[6].Value.ToString().Replace(",", ".");
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
        }

        private void txtPrecio_Leave(object sender, EventArgs e)
        {
            if (txtPrecio.Text == "") { txtPrecio.Text = "0"; }
            
            if (Convert.ToDecimal(txtPrecio.Text) != 0)
            {
                txtPrecio.Text = string.Format("{0:0.00}", Convert.ToDecimal(txtPrecio.Text));
            }
            
        }







    }
}
