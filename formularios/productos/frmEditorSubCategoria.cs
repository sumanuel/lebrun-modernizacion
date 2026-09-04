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
    public partial class frmEditorSubCategoria : Form
    {
        Productos Editor;
        public string Eleccion;
        public string idPc;
        public string ipPc;
        string modulo = "SubCategoria";
        string accion;
        
        public frmEditorSubCategoria()
        {
            InitializeComponent();
            ttMensaje.SetToolTip(btnActulizar,"Actulizar");
            ttMensaje.SetToolTip(BtnRegistra, "Registrar");
            ttMensaje.SetToolTip(btnCancelar, "Cancelar");
        }

        private void frmEditorSubCategoria_Load(object sender, EventArgs e)
        {
            Editor = new Productos();
        }
        private bool validacionCampoSubCateg() 
        {
            bool Ciclope = true;
            if (txtDescripSubCategoria.Text == "")
            {
                MessageBox.Show("El CODIGO es un campo obligatorio!!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCodigoCategoria.Focus();
                Ciclope = false;
                return Ciclope;
            } 
            return Ciclope;  
        }

        private void frmEditorSubCategoria_Shown(object sender, EventArgs e)
        {
            if (Eleccion == "Agregar")
            {
                txtCodigoCategoria.Enabled = false;
                txtDescripCategoria.Enabled = false;
                txtCodigoSubCategoria.Focus();
                btnActulizar.Visible = false;
                BtnRegistra.Visible = true;
            
            }
            else if(Eleccion=="Modificar")
            {
                txtCodigoCategoria.Enabled = false;
                txtCodigoSubCategoria.Enabled = false;
                label2.Visible = false;
                BtnRegistra.Visible = false;
                txtDescripCategoria.Visible = false;
            }
            else if (Eleccion == "Visor")
            {
                txtCodigoCategoria.Enabled = false;
                txtCodigoSubCategoria.Enabled = false;
                label2.Visible = false;
                btnActulizar.Visible = false;
                txtDescripCategoria.Visible = false;
            }
        }

     
        private void button1_Click(object sender, EventArgs e)
        {
            lbxCategoria Modificarlbx = new lbxCategoria();
            Modificarlbx.Eleccion = "Agregar";
            Modificarlbx.Show();
            this.Hide();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmEditorSubCategoria_FormClosed(object sender, FormClosedEventArgs e)
        {
            lbxSubCategoria retour = new lbxSubCategoria();
            retour.IdMaq = idPc;
            retour.ipMaq = ipPc;
            retour.frmProduct = "Producto";
            retour.Show();
        }

        private void BtnRegistra_Click(object sender, EventArgs e)
        {
            accion = "Agre";
            Editor.codigoCategoria = txtCodigoCategoria.Text;
            Editor.codigoSubCat = txtCodigoSubCategoria.Text;
            Editor.descripSubCat = txtDescripSubCategoria.Text;
            if (txtComVentasSubCategoria.Text == "")
            {
                Editor.comvSubCat = Convert.ToString(0.00);
            }
            else 
            {
                Editor.comvSubCat = txtComVentasSubCategoria.Text;
            }
            if (txtComCompraSubCategoria.Text == "")
            {
                Editor.comcSubCat = Convert.ToString(0.00);
            }
            else
            {
                Editor.comcSubCat = txtComCompraSubCategoria.Text;
            }
            if (MessageBox.Show("Desea Ingresar Un Nueva Sub-Categoria  Al Sistema", "Agregar Nueva Informacion Data", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (validacionCampoSubCateg())
                {
                    if (Editor.existeSubCategoria())
                    {
                        MessageBox.Show("La Sub-Categoria Ya Existe!!", "Invalido", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        limpiarCajas();
                    }
                    else if (Editor.ingresarSubCategoria())
                    {
                        Editor.trazUsuario(idPc, ipPc, modulo, accion, txtCodigoSubCategoria.Text);
                        MessageBox.Show("La Sub-Categoria Se Guardado Exitosamente!!", "Exito!! ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                }
            }
        }
        private void btnActulizar_Click(object sender, EventArgs e)
        {
            accion = "Modi";
            Editor.codigoCategoria = txtCodigoCategoria.Text;
            Editor.codigoSubCat = txtCodigoSubCategoria.Text;
            Editor.descripSubCat = txtDescripSubCategoria.Text;
            if (txtComVentasSubCategoria.Text == "")
            {
                Editor.comvSubCat = Convert.ToString(0.00);
            }
            else
            {
                Editor.comvSubCat = txtComVentasSubCategoria.Text;
            }
            if (txtComCompraSubCategoria.Text == "")
            {
                Editor.comcSubCat = Convert.ToString(0.00);
            }
            else
            {
                Editor.comcSubCat = txtComCompraSubCategoria.Text;
            }

            if (MessageBox.Show("Desea Actulizar Esta Sub Categoria Al Sistema", "Agregar Nueva Informacion Data", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (Editor.existeSubCategoria())
                {
                    MessageBox.Show("La Categoria  No Existe!!", "Invalido", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
                else if (Editor.actulizarSubCategoria())
                {
                    Editor.trazUsuario(idPc, ipPc, modulo, accion, txtCodigoSubCategoria.Text);
                    MessageBox.Show("La Categoria  Actulizado Exitosamente!!", "Exito!! ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }

        }
        private void limpiarCajas() 
        {
            txtComCompraSubCategoria.Clear();
            txtComVentasSubCategoria.Clear();
            txtDescripSubCategoria.Clear();

        }

        private void txtCodigoSubCategoria_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                txtDescripSubCategoria.Focus();
            }
        }

        private void txtDescripSubCategoria_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                txtComVentasSubCategoria.Focus();
            }
        }

        private void txtComVentasSubCategoria_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                txtComCompraSubCategoria.Focus();
            }
        }

        private void txtComCompraSubCategoria_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                if (Eleccion == "Agregar")
                {
                    BtnRegistra.Focus();
                }
                if (Eleccion == "Modificar")
                {
                    btnActulizar.Focus();
                }
            }
        }
    }
}
