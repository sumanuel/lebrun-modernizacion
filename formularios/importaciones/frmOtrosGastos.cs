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
    public partial class frmOtrosGastos : Form
    {
        private decimal totalGastos;
        //private decimal subTotalGastos;
        private FuncionesTexbox textBox;


        public frmOtrosGastos()
        {
            InitializeComponent();
            textBox = new FuncionesTexbox();
        }


        public decimal CalcularTotalGastos()
        {
            txtTotalGastos.Text = "";
            totalGastos = 0;


            if (Convert.ToDecimal(txtVerificarMercancia.Text) != 0)
            {
                totalGastos = ((totalGastos) + Convert.ToDecimal(txtVerificarMercancia.Text));
            }

            if (Convert.ToDecimal(txtHononarios.Text) != 0)
            {
                totalGastos = ((totalGastos) + Convert.ToDecimal(txtHononarios.Text));
            }

            if (Convert.ToDecimal(txtTramiteCadivi.Text) != 0)
            {
                totalGastos = ((totalGastos) + Convert.ToDecimal(txtTramiteCadivi.Text));
            }

            if (Convert.ToDecimal(txtSeguroMercancia.Text) != 0)
            {
                totalGastos = ((totalGastos) + Convert.ToDecimal(txtSeguroMercancia.Text));
            }
            if (Convert.ToDecimal(txtOtrosGastos.Text) != 0)
            {
                totalGastos = ((totalGastos) + Convert.ToDecimal(txtOtrosGastos.Text));
            }

            return totalGastos;
               
        }

        private void txtVerificarMercancia_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox a = (TextBox)sender;
            textBox.conDecimalesDefinidos(sender, e, a, 10);


        }

        private void txtVerificarMercancia_MouseUp(object sender, MouseEventArgs e)
        {
            TextBox a = (TextBox)sender;
            a.SelectAll();
        }

        private void txtVerificarMercancia_Leave(object sender, EventArgs e)
        {
            TextBox a = (TextBox)sender;

            if (a.Text == "") { a.Text = "0"; }
            a.Text = string.Format("{0:0.00000000}", Convert.ToDecimal(a.Text));
        }

        private void txtValorImportacion_TextChanged(object sender, EventArgs e)
        {
            TextBox a = (TextBox)sender;
            if (a.Text == "")
            {
                a.Text = "0,00000000";
            }

            a.Text = string.Format("{0:0.00000000}", Convert.ToDecimal(a.Text));
        }













    }
}
