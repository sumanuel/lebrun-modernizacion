using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using lebrun.clases.complementos;

namespace lebrun.formularios.complementos
{
    public partial class frmEditorMoneda : Form
    {
        proveedor proveedor;
        lbxMonedad moneda;
        public string Eleccion;
        public string ipPc;
        public string idPc;
        public string modulo= "Divisa";

        public frmEditorMoneda()
        {
            InitializeComponent();
        }

        private void frmEditorMoneda_Load(object sender, EventArgs e)
        {
            moneda = new lbxMonedad();
            proveedor = new proveedor();
        }

        private void frmEditorMoneda_Shown(object sender, EventArgs e)
        {
            if (Eleccion == "Visor")
            {
                txtCodMoned.ReadOnly = true;
                txtDesMoned.ReadOnly = true;
            }
        }

        private void frmEditorMoneda_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.moneda.Show();
            moneda.ipMaq = ipPc;
            moneda.IdMaq = idPc;
            moneda.frmProduct = "Producto";
        }

        private void BtnRegistra_Click(object sender, EventArgs e)

        {
            string accion = "Agre";
            if (txtCodMoned.Text == "") 
            {
                MessageBox.Show("El campo codigo de moneda es OBLIGATORIO", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtCodMoned.Focus();
            }
            if (txtDesMoned.Text == "")
            {
                MessageBox.Show("El campo descripcion de moneda es OBLIGATORIO!!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtDesMoned.Focus();
            }
            else 
            {
                proveedor.insertMoneda(txtCodMoned.Text, txtDesMoned.Text);
                proveedor.trazUsuario(idPc, ipPc, modulo, accion, txtCodMoned.Text);
                MessageBox.Show("Se Agregado Con EXITO!!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtCodMoned_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((txtCodMoned.Text.Length > 0 && e.KeyChar == 13))
            {
                txtDesMoned.Focus();
            }
        }

        private void txtDesMoned_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((txtDesMoned.Text.Length > 0 && e.KeyChar == 13))
            {
                BtnRegistra.Focus();
            }
        }




    }
}
