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
    public partial class frmEdicionArancel : Form
    {
        public string Eleccion;
        Productos Editor;
        public string idPc;
        public string ipPc;
        public string modulo;
        string accion;

        public frmEdicionArancel()
        {
            InitializeComponent();
            this.ttmensaje.SetToolTip(BtnRegistra, "Registar");
            this.ttmensaje.SetToolTip(btnActulizar, "Modificar");
            this.ttmensaje.SetToolTip(btnCancelar, "Cancelar");
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
        //eventoN de accion a realizar puede ser Agregar o Modificar
        private void frmEdicionArancel_Shown(object sender, EventArgs e)
        {
           if (Eleccion== "Agregar")
           {
                 BtnRegistra.Visible = true;
                 btnActulizar.Visible=false;
           }
         else if (Eleccion == "Modificar")
           {
               txtCodigoArancel.Enabled = false;
               BtnRegistra.Visible = false;
               btnActulizar.Visible = true;    
           }
           else if (Eleccion == "Visor")
           {
                btnActulizar.Visible = false;
                BtnRegistra.Visible=false;
           }
        }
        //Validacion de Campos
        public bool validacionCampos() 
        {
            bool Ciclope = true;
            if (txtCodigoArancel.Text == "") 
            {
                MessageBox.Show("El CODIGO es un campo obligatorio!!","ERROR",MessageBoxButtons.OK,MessageBoxIcon.Error);
                txtCodigoArancel.Focus();
                Ciclope = false;
                return Ciclope;
            }
            if (txtDescricionArancel.Text == "")
            {
                MessageBox.Show("La DESCRIPCION es un campo obligatorio!!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtDescricionArancel.Focus();
                Ciclope = false;
                return Ciclope;
            }
            return Ciclope;  
        }

        private void limpiarCaja()
        {
            txtCodigoArancel.Clear();
            txtDescricionArancel.Clear();
            txtImpuesto.Clear();
            txtKg.Clear();
            txtProcentajeArancel.Clear();  
        }

        private void frmEdicionArancel_Load(object sender, EventArgs e)
        {
            Editor = new Productos();
        }

        private void btnActulizar_Click(object sender, EventArgs e)
        {
            accion = "Modi";
            Editor.codigoA = txtCodigoArancel.Text;
            Editor.descripA = txtDescricionArancel.Text;
            if (txtKg.Text == "")
            {
                Editor.kgA = Convert.ToString(0);
            }
            else
            {
                Editor.kgA = txtKg.Text;
            }
            if (txtProcentajeArancel.Text == "")
            {
                Editor.proceA = Convert.ToString(0.00);
            }
            else
            {
                Editor.proceA = txtProcentajeArancel.Text;
            }
            if (txtImpuesto.Text == "")
            {
                Editor.impuestoA = Convert.ToString(0.00);
            }
            else
            {
                Editor.impuestoA = txtImpuesto.Text;
            }
            if (MessageBox.Show("Esta Seguro Que Quiere Modificar", "Agregar Nueva Informacion Data", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (Editor.existeArancel() == false)
                {
                    MessageBox.Show("El Arancel  No Existe!!", "Invalido", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
                else if (Editor.actulizarArancel())
                {
                    Editor.trazUsuario(idPc, ipPc, modulo, accion, txtCodigoArancel.Text);
                    MessageBox.Show("El Arancel  Actulizado Exitosamente!!", "Exito!! ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
        }

        private void frmEdicionArancel_FormClosed(object sender, FormClosedEventArgs e)
        {
            lbxArancel RetornoAracel = new lbxArancel();
            RetornoAracel.frmProduct = "Producto";
            RetornoAracel.ipMaq = ipPc;
            RetornoAracel.IdMaq = idPc;
            RetornoAracel.modulo = modulo;
            RetornoAracel.Show();
        }

        private void BtnRegistra_Click(object sender, EventArgs e)
        {
            accion = "Agre";
            Editor.codigoA = txtCodigoArancel.Text;
            Editor.descripA = txtDescricionArancel.Text;
            Editor.kgA = txtKg.Text;
            if (txtKg.Text == "")
            {
               Editor.kgA = Convert.ToString(0);
            }
            else
            {
                Editor.kgA = txtKg.Text;
            }
            if (txtProcentajeArancel.Text == "")
            {
                Editor.proceA = Convert.ToString(0.00);
            }
            else
            {
                Editor.proceA = txtProcentajeArancel.Text;
            }
            if (txtImpuesto.Text == "")
            {
                Editor.impuestoA = Convert.ToString(0.00);
            }
            else
            {
                Editor.impuestoA = txtImpuesto.Text;
            }
            
            if (MessageBox.Show("Desea Ingresar Un Nuevo Arancel Al Sistema", "Agregar Nueva Informacion Data", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (validacionCampos())
                {
                    if (Editor.existeArancel())
                    {
                        MessageBox.Show("El Arancel existe!!", "Invalido", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        limpiarCaja();
                    }
                    else if (Editor.ingresarArancel())
                    {
                        Editor.trazUsuario(idPc, ipPc, modulo, accion, txtCodigoArancel.Text);
                        MessageBox.Show("El Arancel Guardado Exitosamente!!", "Exito!! ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }

                }
            }
        }

        private void txtCodigoArancel_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsNumber(e.KeyChar) || e.KeyChar == 8 ? false : true;
            txtCodigoArancel.MaxLength = 15;
            if (e.KeyChar == (char)(Keys.Enter)) 
            {
               
                txtDescricionArancel.Focus();
                 
            }
        }

        private void txtDescricionArancel_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtDescricionArancel.MaxLength = 50;
            if (e.KeyChar == (char)(Keys.Enter))
            {
                txtProcentajeArancel.Focus();
            }
        }

        private void txtProcentajeArancel_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsNumber(e.KeyChar) || e.KeyChar == 8 || e.KeyChar == 44 || e.KeyChar == 46  ? false : true;
            txtProcentajeArancel.MaxLength = 6;
            if (e.KeyChar == (char)(Keys.Enter))
            {
                txtImpuesto.Focus();
            }
        }
        private void txtKg_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsNumber(e.KeyChar) || e.KeyChar == 8 || e.KeyChar == 44 || e.KeyChar == 46 ? false : true;
            txtKg.MaxLength = 6;
            if (e.KeyChar == (char)(Keys.Enter))
            {
                if (Eleccion == "Agregar") BtnRegistra.Focus();
                else if (Eleccion == "Modificar") btnActulizar.Focus();
            }
        }

        private void txtImpuesto_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsNumber(e.KeyChar) || e.KeyChar == 8 || e.KeyChar==44 ||e.KeyChar==46 ? false : true;
            txtImpuesto.MaxLength = 6;
            if (e.KeyChar == (char)(Keys.Enter))
            {
                txtKg.Focus();
            }
            
        }


        private void frmEdicionArancel_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                BtnRegistra.PerformClick();
            }
        }
    }
}
