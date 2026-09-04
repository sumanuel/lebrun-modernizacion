using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using lebrun.clasesData;

namespace lebrun.formularios.importaciones
{
    public partial class frmOtrosDatos : Form
    {
        private FuncionesTexbox textBox;

        public frmOtrosDatos()
        {
            InitializeComponent();
            textBox = new FuncionesTexbox();
        }

        private void txtNumExpediente_KeyPress(object sender, KeyPressEventArgs e)
        {
            textBox.sinCharEspeciales(sender, e);
        }

        private void txtMontoAutori_KeyPress(object sender, KeyPressEventArgs e)
        {
            textBox.conDecimalesDefinidos(sender, e, txtMontoAutori, 10);
        }

        private void txtBultos_KeyPress(object sender, KeyPressEventArgs e)
        {
            textBox.onlyNumberWithTab(sender, e);
        }

        private void txtTasaCambio_KeyPress(object sender, KeyPressEventArgs e)
        {
            textBox.conDecimalesDefinidos(sender, e, txtTasaCambio, 10);
        }

        private void txtNumExpediente_MouseUp(object sender, MouseEventArgs e)
        {
           
            TextBox a = (TextBox)sender;
            a.SelectAll();
            
        }

        private void txtNumExpediente_Enter(object sender, EventArgs e)
        {
            txtNumExpediente.SelectAll();
        }

        private void txtMontoAutori_Leave(object sender, EventArgs e)
        {
            TextBox a = (TextBox)sender;
            if (a.Text == "")
            {
                a.Text = "0,00000000";
            }
            a.Text = string.Format("{0:0.00000000}", Convert.ToDecimal(a.Text));
        }

        private void txtBultos_Leave(object sender, EventArgs e)
        {
            if (txtBultos.Text == "") {
                txtBultos.Text = "0";
            }
        }


























 

        
















    }
}
