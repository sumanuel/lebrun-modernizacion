using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using lebrun.clases.producto;

namespace lebrun.formularios.productos
{
    public partial class frmUnidaXProducto : Form
    {
        Productos producto;
        private DataView dv;
        private DataTable dt;
        public string codigoProd;
        string Estado;
        public string idPc;
        public string ipPc;
        string modulos = "Unidad De Medida";
        string accion;
        string visCompra = "0";
        string visVenta = "0";
        string uniPrinpal = "0";
        public string modulo;
        public frmUnidaXProducto()
        {
            InitializeComponent();
            this.ttmensajes.SetToolTip(this.btnSalir, "Salir");
            this.ttmensajes.SetToolTip(this.btnCancelar, "Cancelar");
            this.ttmensajes.SetToolTip(this.btnEliminar, "Eliminar");
            this.ttmensajes.SetToolTip(this.btnAgreUnidad, "Aceptar");
            this.ttmensajes.SetToolTip(this.btnAgregar, "Agregar");
            this.ttmensajes.SetToolTip(this.btnModificar, "Modificar");
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            groupBox4.Visible = true;
            btnAgreUnidad.Visible = true;
            btnCancelar.Visible = true;
            btnAgregar.Visible = false;
            btnModificar.Visible = false;
            btnEliminar.Visible = false;
            btnSalir.Visible = false;
            cmbUnidad.Focus();
            producto.llenarUnidad(cmbUnidad);
            producto.llenarPresentacion(cmbUnidad.Text);
            txtPresentecion.Text = producto.UndDescrip;
            Estado = "Agregar";
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            groupBox4.Visible = false;
            btnAgreUnidad.Visible = false;
            btnCancelar.Visible = false;
            btnAgregar.Visible = true;
            btnModificar.Visible = true;
            btnEliminar.Visible = true;
            btnSalir.Visible = true;
            if (dataGridView1.Rows.Count != 0)
            {
                btnAgregar.Visible = false;
            }
        }

        private void frmUnidaXProducto_Load(object sender, EventArgs e)
        {
            producto = new Productos();
            dt = producto.uniMedida(codigoProd);
            dv = new DataView(dt);
            this.dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].HeaderText = "Unidad";
            dataGridView1.Columns[1].HeaderText = "Cantidad Por Unidad";
            dataGridView1.Columns[2].HeaderText = "Presentacion";
            dataGridView1.Columns[3].HeaderText = "Peso";
            dataGridView1.Columns[4].HeaderText = "Volumen";
            dataGridView1.Columns[5].HeaderText = "Compra";
            dataGridView1.Columns[6].HeaderText = "Venta";
            dataGridView1.Columns[7].HeaderText = "Signo";
            dataGridView1.Columns[8].HeaderText = "Principal";
            if (dataGridView1.Rows.Count != 0) 
            {
                btnAgregar.Visible = false;
            }
            else
            {
               btnModificar.Visible = false;
               btnEliminar.Visible = false;
            }
        }

        private void frmUnidaXProducto_Shown(object sender, EventArgs e)
        {
            groupBox4.Visible = false;
        }

        private void frmUnidaXProducto_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (modulo == "opciones adicionales")
            {
                lbxOpcionesAdicionales opciones = new lbxOpcionesAdicionales();
                opciones.ipMaq = ipPc;
                opciones.IdMaq = idPc;
                opciones.txtCodProd.Text = txtCodProd.Text;
                opciones.txtDescProduc.Text = txtDesProd.Text;
                opciones.Show();
            }

        }

        private void cmbUnidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            if ((cmbUnidad.Text.Length > 0 && e.KeyChar == 13))
            {
                txtPresentecion.Focus();
            }
        }

        private void txtPresentecion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((txtPresentecion.Text.Length > 0 && e.KeyChar == 13))
            {
                txtCantXUnidad.Focus();

            }
        }

        private void txtCantXUnidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((txtCantXUnidad.Text.Length > 0 && e.KeyChar == 13))
            {
                cmdSigno.Focus();
            }
        }

        private void cmdSigno_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((cmdSigno.Text.Length > 0 && e.KeyChar == 13))
            {
                txtPeso.Focus();
            }
        }

        private void txtVolumen_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((txtVolumen.Text.Length > 0 && e.KeyChar == 13))
            {
                btnAgreUnidad.Focus();
            }
        }

        private void btnAgreUnidad_Click(object sender, EventArgs e)
        {
            if (Estado == "Agregar")
            {
                accion = "Agre";
                validarChe();
                producto.insertUnidaMedida(txtCodProd.Text, cmbUnidad.Text, txtCantXUnidad.Text,
                txtPresentecion.Text, txtPeso.Text, txtVolumen.Text, visCompra, visVenta, cmdSigno.Text, uniPrinpal);
                producto.trazUsuario(idPc, ipPc, modulos, accion, txtCodProd.Text);
                dt = producto.uniMedida(codigoProd);
                dv = new DataView(dt);
                this.dataGridView1.DataSource = dt;
                dataGridView1.Columns[0].HeaderText = "Unidad";
                dataGridView1.Columns[1].HeaderText = "Cantidad Por Unidad";
                dataGridView1.Columns[2].HeaderText = "Presentacion";
                dataGridView1.Columns[3].HeaderText = "Peso";
                dataGridView1.Columns[4].HeaderText = "Volumen";
                dataGridView1.Columns[5].HeaderText = "Compra";
                dataGridView1.Columns[6].HeaderText = "Venta";
                dataGridView1.Columns[7].HeaderText = "Signo";
                dataGridView1.Columns[8].HeaderText = "Principal";
                if (dataGridView1.Rows.Count != 0)
                {
                    btnAgregar.Visible = false;
                    btnModificar.Visible = true;
                    btnEliminar.Visible = true;
                }
                else
                {
                    btnAgregar.Visible = true;
                    btnModificar.Visible = false;
                    btnEliminar.Visible = false;
                }
                groupBox4.Visible = false;
                btnAgreUnidad.Visible = false;
                btnCancelar.Visible = false;
                btnSalir.Visible = true;

            }
            if (Estado == "Modificar")
            {
                accion = "Modi";
                validarChe();
                producto.actulizarUniMed(txtCodProd.Text, cmbUnidad.Text, txtCantXUnidad.Text,
                txtPresentecion.Text, txtPeso.Text, txtVolumen.Text, visCompra, visVenta, cmdSigno.Text, uniPrinpal);
                producto.trazUsuario(idPc, ipPc, modulos, accion, txtCodProd.Text);

                dt = producto.uniMedida(codigoProd);
                dv = new DataView(dt);
                this.dataGridView1.DataSource = dt;
                dataGridView1.Columns[0].HeaderText = "Unidad";
                dataGridView1.Columns[1].HeaderText = "Cantidad Por Unidad";
                dataGridView1.Columns[2].HeaderText = "Presentacion";
                dataGridView1.Columns[3].HeaderText = "Peso";
                dataGridView1.Columns[4].HeaderText = "Volumen";
                dataGridView1.Columns[5].HeaderText = "Compra";
                dataGridView1.Columns[6].HeaderText = "Venta";
                dataGridView1.Columns[7].HeaderText = "Signo";
                dataGridView1.Columns[8].HeaderText = "Principal";
                if (dataGridView1.Rows.Count != 0)
                {
                    btnAgregar.Visible = false;
                }
                groupBox4.Visible = false;
                btnAgreUnidad.Visible = false;
                btnCancelar.Visible = false;
                btnAgregar.Visible = true;
                btnModificar.Visible = true;
                btnEliminar.Visible = true;
                btnSalir.Visible = true;
                if (dataGridView1.Rows.Count != 0)
                {
                    btnAgregar.Visible = false;
                    btnModificar.Visible = true;
                    btnEliminar.Visible = true;
                }
                else
                {
                    btnAgregar.Visible = true;
                    btnModificar.Visible = false;
                    btnEliminar.Visible = false;
                }
            }
        }



        private void btnModificar_Click(object sender, EventArgs e)
        {
            Estado = "Modificar";

            producto.modificarUniMed(txtCodProd.Text);
            cmbUnidad.Text = producto.UndMed;
            txtPresentecion.Text = producto.UndPrese;
            txtCantXUnidad.Text= producto.UndCant;
            cmdSigno.Text = producto.UndSig;
            txtPeso.Text = producto.UndPeso;
            txtVolumen.Text = producto.UndVol;
            cmbUnidad.Focus();
            if (producto.UndCompr == "1")
            {
                chbVisCompras.Checked = true;
            }
            else 
            {
                chbVisCompras.Checked = false;
            }
            if (producto.UndVent == "1")
            {
                chVisVentas.Checked = true;
            }
            else 
            {
                chVisVentas.Checked = false;
            }
            if (producto.UndPrin=="1")
            {
                chUniPrin.Checked = true;
            }
            else 
            {
                chUniPrin.Checked = false;
            }
            groupBox4.Visible = true;
            btnAgreUnidad.Visible = true;
            btnCancelar.Visible = true;
            btnAgregar.Visible = false;
            btnModificar.Visible = false;
            btnEliminar.Visible = false;
            btnSalir.Visible = false;
            producto.llenarUnidad(cmbUnidad);
            producto.llenarPresentacion(cmbUnidad.Text);
            txtPresentecion.Text = producto.UndDescrip;
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            accion = "Elim";
            producto.precioActivo(txtCodProd.Text);
            if (producto.CodiPreExite == "1")
            {
                if (producto.PreciosActivo != "1")
                {
                    if (MessageBox.Show("Desea Eliminar La Unidad de Medida De Este Producto", "Eliminar Informacion Data", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        producto.eliminarUniMedida(txtCodProd.Text);
                        producto.trazUsuario(idPc,ipPc,modulos, accion,txtCodProd.Text);
                        dt = producto.uniMedida(codigoProd);
                        dv = new DataView(dt);
                        this.dataGridView1.DataSource = dt;
                        dataGridView1.Columns[0].HeaderText = "Unidad";
                        dataGridView1.Columns[1].HeaderText = "Cantidad Por Unidad";
                        dataGridView1.Columns[2].HeaderText = "Presentacion";
                        dataGridView1.Columns[3].HeaderText = "Peso";
                        dataGridView1.Columns[4].HeaderText = "Volumen";
                        dataGridView1.Columns[5].HeaderText = "Compra";
                        dataGridView1.Columns[6].HeaderText = "Venta";
                        dataGridView1.Columns[7].HeaderText = "Signo";
                        dataGridView1.Columns[8].HeaderText = "Principal";
                        if (dataGridView1.Rows.Count != 0)
                        {
                            btnAgregar.Visible = false;
                            btnModificar.Visible = true;
                            btnEliminar.Visible = true;
                        }
                        else
                        {
                            btnAgregar.Visible = true;
                            btnModificar.Visible = false;
                            btnEliminar.Visible = false;
                        }
                        groupBox4.Visible = false;
                        btnAgreUnidad.Visible = false;
                        btnCancelar.Visible = false;
                        btnSalir.Visible = true;
                    }
                }
                else
                {
                    MessageBox.Show("No Se Puede Eliminar Unidad Medida Con el Precio Activo!!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                MessageBox.Show("No Exite Data De Precios", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        public bool validarChe() 
        {
            bool Ciclope = true;
            if (chbVisCompras.Checked == true) 
            {
                visCompra = "1";
            }
            if (chVisVentas.Checked == true) 
            {
                visVenta = "1";
            }
            if (chUniPrin.Checked == true) 
            {
                uniPrinpal = "1";
            }
            return Ciclope;
        }

        private void cmbUnidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            producto.llenarPresentacion(cmbUnidad.Text);
            txtPresentecion.Text = producto.UndDescrip;
        }

        private void txtPeso_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((txtPeso.Text.Length > 0 && e.KeyChar == 13))
            {
                txtVolumen.Focus();
            }
        }
    }
}
