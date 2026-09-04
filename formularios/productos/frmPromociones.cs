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
using lebrun.formularios.complementos;

namespace lebrun.formularios.productos
{
    public partial class frmPromociones : Form
    {

        private int centinelaFila = -1;
        private Producto cproducto;
        private FuncionesTexbox textBox;
        private frmConsultaArticulos productos;
        private DataTable tabla;

        

        public frmPromociones()
        {
            InitializeComponent();
            cproducto = new Producto();
            textBox = new FuncionesTexbox();
            tabla = new DataTable();
        }

        private void frmPromociones_Load(object sender, EventArgs e)
        {
            cmbTipoPrecio.SelectedIndex = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void agregarFilas(int fila, DataGridView grid)
        {

            if (grid.Name.Equals("gridPromoNueva"))
            {
                if (fila < 0)
                {
                    grid.Rows.Add();
                    fila = grid.Rows.Count - 1;
                }

                grid.Rows[fila].Cells[0].Value = fila + 1;
                grid.Rows[fila].Cells[1].Value = cmbTipoPrecio.Text;
                grid.Rows[fila].Cells[2].Value = txtCodigo.Text;
                grid.Rows[fila].Cells[3].Value = lblDescripcionPro.Text;
                grid.Rows[fila].Cells[4].Value = lblUnidad.Text;
                grid.Rows[fila].Cells[5].Value = fechaInicio.Value.Day.ToString().PadLeft(2, '0') + "/" + fechaInicio.Value.Month.ToString().PadLeft(2, '0') + "/" + fechaInicio.Value.Year;
                grid.Rows[fila].Cells[6].Value = fechaFin.Value.Day.ToString().PadLeft(2, '0') + "/" + fechaFin.Value.Month.ToString().PadLeft(2, '0') + "/" + fechaFin.Value.Year;
                grid.Rows[fila].Cells[7].Value = fechaFin.Value.Year + "-" + fechaFin.Value.Month.ToString().PadLeft(2, '0') + "-" + fechaFin.Value.Day.ToString().PadLeft(2, '0');
                grid.Rows[fila].Cells[8].Value = fechaInicio.Value.Year + "-" + fechaInicio.Value.Month.ToString().PadLeft(2, '0') + "-" + fechaInicio.Value.Day.ToString().PadLeft(2, '0');
                grid.Rows[fila].Cells[9].Value = txtPrecio.Text;
                grid.Rows[fila].Cells[10].Value = txtCantRequerida.Text;
                grid.Rows[fila].Cells[11].Value = txtCantOtorgada.Text;
                //grid.Rows[fila].Cells[12].Value = Principal.usuarioActual.Id;

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

        private void bloquearPrecio()
        {
            if (gridPromoNueva.Rows.Count > 0)
            {
                cmbTipoPrecio.Enabled = false;
            }
            else
            {
                cmbTipoPrecio.Enabled = true;
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

            if (txtDescuento.Text != "")
            {
                flag = true;
            }
            else { flag = false; MessageBox.Show("El descuento no debe quedar vacio", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information); txtDescuento.Focus(); return false; }

            if (txtCantRequerida.Text !="")
            {
                flag = true;
            }
            else { flag = false; MessageBox.Show("La cantidad requerida no debe quedar en blanco", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information); txtCantRequerida.Focus(); return false; }

            if (txtMultiplo.Text == "0")
            {
                if (txtCantRequerida.Text != "0")
                {
                    flag = true;
                }
                else { flag = false; MessageBox.Show("La cantidad requerida no debe ser igual a cero(0)", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information); txtCantRequerida.Focus(); return false; }

            }

            if (txtMultiplo.Text != "0")
            {
                if (txtCantRequerida.Text == "0")
                {
                    flag = true;
                }
                else { flag = false; MessageBox.Show("Al existir Multiplos la cantidad requerida debe ser igual a cero(0)", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information); txtCantRequerida.Focus(); return false; }

            }

            if (txtCantOtorgada.Text != "")
            {
                flag = true;
            }
            else { flag = false; MessageBox.Show("La cantidad otorgada no debe quedar en blanco", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information); txtCantOtorgada.Focus(); return false; }


            if (txtMultiplo.Text == "0")
            {
                if (txtCantOtorgada.Text != "0")
                {
                    flag = true;
                }
                else { flag = false; MessageBox.Show("La cantidad otorgada no debe ser igual a cero(0)", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information); txtCantOtorgada.Focus(); return false; }
            }

            if (txtMultiplo.Text != "0")
            {
                if (txtCantOtorgada.Text == "0")
                {
                    flag = true;
                }
                else { flag = false; MessageBox.Show("Al existir Multiplos la cantidad otorgada debe ser igual a cero(0)", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information); txtCantOtorgada.Focus(); return false; }
            }

            return flag;

        }

        private void limpiarCampos()
        {
            lblPrecioActivo.Text = "...";
            txtCodigo.Text = "";
            lblDescripcionPro.Text = "...";
            lblDescriUni.Text = "...";
            lblUnidad.Text = "...";
            txtPrecio.Text = "0";
            txtCantOtorgada.Text = "0";
            txtCantRequerida.Text = "0";
            gridPromoActiva.DataSource = null;

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (validarCamposGrid())
            {
                if (PromocionAgregada() || validarPromoActivas())
                {
                }
                else
                {
                    if (centinelaFila >= 0)
                    {

                        agregarFilas(centinelaFila, gridPromoNueva);
                        centinelaFila = -1;
                    }
                    else
                    {
                        agregarFilas(-1, gridPromoNueva);
                    }

                }
                limpiarCampos();
                bloquearPrecio();
                txtCodigo.Focus();
            }
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            eliminarItems(gridPromoNueva);
            bloquearPrecio();
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
                llenarOfertasActivas();
                cproducto.buscarPrecios(txtCodigo.Text, cmbTipoPrecio.Text);
                lblPrecioActivo.Text = "" + cproducto.Precio;

                tabla = cproducto.buscarMultiplo(txtCodigo.Text.PadLeft(15, '0'),cmbTipoPrecio.Text);
                
                if (tabla.Rows.Count > 0)
                {
                    txtMultiplo.Text = tabla.Rows[0][0].ToString();
                }
                
            }
        }

        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox a = (TextBox)sender;
            textBox.conDecimalesDefinidos(sender, e, a, 2);
        }

        private void txtCantRequerida_KeyPress(object sender, KeyPressEventArgs e)
        {
            textBox.onlyNumberWithTab(sender, e);
        }

        private void txtCantOtorgada_KeyPress(object sender, KeyPressEventArgs e)
        {
            textBox.onlyNumberWithTab(sender, e);
        }

        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            textBox.onlyNumberWithTab(sender, e);
        }

        private void fechaInicio_KeyPress(object sender, KeyPressEventArgs e)
        {
            textBox.tab(sender, e);
        }

        private void fechaFin_KeyPress(object sender, KeyPressEventArgs e)
        {
            textBox.tab(sender, e);
        }

        private void txtPrecio_Leave(object sender, EventArgs e)
        {
            if (txtPrecio.Text == "") { txtPrecio.Text = "0"; }

            if (Convert.ToDecimal(txtPrecio.Text) != 0)
            {
                txtPrecio.Text = string.Format("{0:0.00}", Convert.ToDecimal(txtPrecio.Text));
            }
        }

        private void llenarOfertasActivas() {

            DataTable tabla = new DataTable();
            tabla = cproducto.off(txtCodigo.Text.PadLeft(15, '0'));

            gridPromoActiva.Rows.Clear();
            gridPromoActiva.DataSource = null;

            if (tabla.Rows.Count > 0)
            {
                for (int i = 0; i < tabla.Rows.Count; i++)
                {
                    gridPromoActiva.Rows.Add();
                    gridPromoActiva.Rows[i].Cells[0].Value = tabla.Rows[i]["prom_undpro"].ToString();
                    gridPromoActiva.Rows[i].Cells[1].Value = tabla.Rows[i]["porm_precio"].ToString();
                    gridPromoActiva.Rows[i].Cells[2].Value = tabla.Rows[i]["prom_lista"].ToString();
                    gridPromoActiva.Rows[i].Cells[3].Value = Convert.ToDateTime(tabla.Rows[i]["prom_fchedes"].ToString()).ToShortDateString();
                    gridPromoActiva.Rows[i].Cells[4].Value = Convert.ToDateTime(tabla.Rows[i]["prom_fchfin"].ToString()).ToShortDateString();
                    gridPromoActiva.Rows[i].Cells[5].Value = tabla.Rows[i]["prom_canreq"].ToString();
                    gridPromoActiva.Rows[i].Cells[6].Value = tabla.Rows[i]["prom_cantpro"].ToString();
                    

                }
            }
        }

        private bool validarPromoActivas()
        {

            for (int i = 0; i < gridPromoActiva.Rows.Count; i++)
            {
                if ((Convert.ToDateTime(gridPromoActiva.Rows[i].Cells[3].Value.ToString()) == Convert.ToDateTime(fechaInicio.Text) && Convert.ToDateTime(gridPromoActiva.Rows[i].Cells[4].Value.ToString()) == Convert.ToDateTime(fechaFin.Text) && Convert.ToDecimal(gridPromoActiva.Rows[i].Cells[5].Value.ToString()) == Convert.ToDecimal(txtCantOtorgada.Text) && Convert.ToDecimal(gridPromoActiva.Rows[i].Cells[6].Value.ToString()) == Convert.ToDecimal(txtCantRequerida.Text)))
                {
                    MessageBox.Show("Esta oferta ya esta Activa", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
                }
            }
            return false;
        }

        private bool PromocionAgregada()
        {

            for (int i = 0; i < gridPromoNueva.Rows.Count; i++)
            {
                if ((Convert.ToDateTime(gridPromoNueva.Rows[i].Cells[6].Value.ToString()) == Convert.ToDateTime(fechaInicio.Text) && Convert.ToDateTime(gridPromoNueva.Rows[i].Cells[5].Value.ToString()) == Convert.ToDateTime(fechaFin.Text) && Convert.ToDecimal(gridPromoNueva.Rows[i].Cells[10].Value.ToString()) == Convert.ToDecimal(txtCantRequerida.Text) && Convert.ToDecimal(gridPromoNueva.Rows[i].Cells[11].Value.ToString()) == Convert.ToDecimal(txtCantOtorgada.Text) && gridPromoNueva.Rows[i].Cells[2].Value.ToString() == txtCodigo.Text ))
                {
                    MessageBox.Show("Esta promocion ya fue agregada", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
                }
            }
            return false;
        }

        private void reemplazando()
        {
            for (int i = 0; i < gridPromoNueva.Rows.Count; i++)
            {
                gridPromoNueva.Rows[i].Cells[9].Value = gridPromoNueva.Rows[i].Cells[9].Value.ToString().Replace(",", ".");
                gridPromoNueva.Rows[i].Cells[10].Value = gridPromoNueva.Rows[i].Cells[10].Value.ToString().Replace(",", ".");
                gridPromoNueva.Rows[i].Cells[11].Value = gridPromoNueva.Rows[i].Cells[11].Value.ToString().Replace(",", ".");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            reemplazando();
            cproducto.registrarNuevasOfertas(gridPromoNueva);
            this.Close();
        }

        private void cmbTipoPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                SendKeys.Send("{TAB}");

                //DateTime diaAnterior = new DateTime();
                //diaAnterior =Convert.ToDateTime(DateTime.Now.AddDays(-1).ToShortDateString());

                //diaAnterior.Year + "-" + diaAnterior.Month + "-" + diaAnterior.Day;

                //MessageBox.Show("" + diaAnterior.Year + "-" + diaAnterior.Month.ToString().PadLeft(2,'0') + "-" + diaAnterior.Day);
            }
            else { e.Handled = true; }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (gridPromoActiva.Rows.Count > 0)
            {
                    cproducto.eliminarOferta(txtCodigo.Text,
                    Convert.ToDateTime(gridPromoActiva.CurrentRow.Cells[4].Value.ToString()).ToString("yyyy-MM-dd"),
                    Convert.ToDateTime(gridPromoActiva.CurrentRow.Cells[3].Value.ToString()).ToString("yyyy-MM-dd"),
                    gridPromoActiva.CurrentRow.Cells[6].Value.ToString(),
                    gridPromoActiva.CurrentRow.Cells[5].Value.ToString());
            }

            llenarOfertasActivas();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            productos = frmConsultaArticulos.DefInstance;
            productos.destino(this, this.Name);
            productos.MdiParent = this.MdiParent;
            productos.Show();
        }



    }
}
