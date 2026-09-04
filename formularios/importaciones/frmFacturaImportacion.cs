using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using lebrun.clases.importaciones;
using lebrun.clasesData;
using lebrun.formularios.complementos;

namespace lebrun.formularios.importaciones
{
    public partial class frmFacturaImportacion : Form
    {

        public decimal totalBolivares;
        private Importacion cabeceraImportacion;
        private FuncionesTexbox textBox;
        private lbxProveedores proveedores;

        public frmFacturaImportacion()
        {
            InitializeComponent();
            cabeceraImportacion = new Importacion();
            textBox = new FuncionesTexbox();
            proveedores = new lbxProveedores();
        }


        public void calcularFOB(){


            if (txtCambioLiquidar.Text != "")
            {
                if (txtNetoFOB.Text != "")
                {
                    txtNetoBs.Text = "" + Convert.ToDecimal(txtNetoFOB.Text) * Convert.ToDecimal(txtCambioLiquidar.Text);
                }
                if (txtFlete.Text != "")
                {
                    txtFleteBs.Text = "" + Convert.ToDecimal(txtFlete.Text) * Convert.ToDecimal(txtCambioLiquidar.Text);
                }
                if (txtSeguro.Text != "")
                {
                    txtSeguroBs.Text = "" + Convert.ToDecimal(txtSeguro.Text) * Convert.ToDecimal(txtCambioLiquidar.Text);
                }
                if (txtCostoMaterial.Text != "")
                {
                    txtCostoMaterialBs.Text = "" + Convert.ToDecimal(txtCostoMaterial.Text) * Convert.ToDecimal(txtCambioLiquidar.Text);
                }

                if (txtExtra1.Text != "")
                {
                    txtExtra1Bs.Text = "" + Convert.ToDecimal(txtExtra1.Text) * Convert.ToDecimal(txtCambioLiquidar.Text);
                }
                if (txtExtra2.Text != "")
                {
                    txtExtra2Bs.Text = "" + Convert.ToDecimal(txtExtra2.Text) * Convert.ToDecimal(txtCambioLiquidar.Text);
                }

                totalBolivares = Convert.ToDecimal(txtNetoBs.Text) + Convert.ToDecimal(txtFleteBs.Text) + Convert.ToDecimal(txtSeguroBs.Text); //+ Convert.ToDecimal(txtCostoMaterialBs.Text);
                if (txtNetoBs.Text!="0,000")
                {
                txtFactorCIF.Text = "" + totalBolivares / Convert.ToDecimal(txtNetoBs.Text);
                txtTotalCIF.Text = ""+totalBolivares;
                txtCostoCIF.Text = "" + (Convert.ToDecimal(txtFactorCIF.Text)-1) * 100;
                }
            }

        }


        //public void asignadoValoresCabecera() {

        //    cabeceraImportacion.FactorCif = txtFactorCIF.Text.Replace(",",".");
        //    cabeceraImportacion.Documento = txtDocumento.Text;
        //    cabeceraImportacion.Sufijo = txtSufijoDoc.Text;

        //    cabeceraImportacion.MontoFob = txtNetoFOB.Text.Replace(",", ".");
        //    cabeceraImportacion.SeguroFob = txtSeguro.Text.Replace(",", ".");
        //    cabeceraImportacion.FleteFob = txtFlete.Text.Replace(",", ".");
        //    cabeceraImportacion.CostoMaterialFob = txtCostoMaterial.Text.Replace(",", ".");
        //    cabeceraImportacion.Otros1Fob = txtExtra1.Text.Replace(",", ".");
        //    cabeceraImportacion.Otros2Fob = txtExtra2.Text.Replace(",", ".");

        //    cabeceraImportacion.MontoBs = txtNetoBs.Text.Replace(",", ".");
        //    cabeceraImportacion.SeguroBs = txtSeguroBs.Text.Replace(",", ".");
        //    cabeceraImportacion.FleteBs = txtFleteBs.Text.Replace(",", ".");
        //    cabeceraImportacion.CostoMaterialBs = txtCostoMaterialBs.Text.Replace(",", ".");
        //    cabeceraImportacion.Otros1Bs = txtExtra1Bs.Text.Replace(",", ".");
        //    cabeceraImportacion.Otros2Bs = txtExtra2Bs.Text.Replace(",", ".");

        //    cabeceraImportacion.TotalCif = txtTotalCIF.Text.Replace(",", ".");
        //    cabeceraImportacion.CostoCif = txtCostoCIF.Text.Replace(",", ".");

        //    cabeceraImportacion.CambioLiqui = txtCambioLiquidar.Text.Replace(",", ".");
        //    cabeceraImportacion.CambioOc = txtCambioOrden.Text.Replace(",", ".");

        //    cabeceraImportacion.Fechaimport = dateTimePicker1.Value.Year.ToString() + "-" + dateTimePicker1.Value.Month.ToString() + "-" + dateTimePicker1.Value.Day.ToString();
            
            
        ////
        
        //}

        //public void guardarCabecera(string factorGasto, int opc) {
        //    cabeceraImportacion.guardarCabeceraImportacion(factorGasto, opc);
        //}

        private void txtNetoFOB_Leave(object sender, EventArgs e)
        {
            TextBox a = (TextBox)sender;
            if (a.Text == "")
            {
                a.Text = "0,00";
            }

            a.Text = string.Format("{0:0.00000000}", Convert.ToDecimal(a.Text));
        }

        private void txtNetoFOB_MouseUp(object sender, MouseEventArgs e)
        {
            TextBox a = (TextBox)sender;
            a.SelectAll();
        }

        private void txtNetoFOB_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox a = (TextBox)sender;
            textBox.conDecimalesDefinidos(sender, e, a, 10);

            //if (a.Name.Equals("txtCambioLiquidar")) { txtCambioProducto.Focus(); }
            //if (a.Name.Equals("txtCambioProducto")) { txtNetoFOB.Focus(); }
            //if (a.Name.Equals("txtNetoFOB")) { txtFlete.Focus(); }
            //if (a.Name.Equals("txtFlete")) { txtSeguro.Focus(); }
            //if (a.Name.Equals("txtSeguro")) { txtCostoMaterial.Focus(); }
            //if (a.Name.Equals("txtCostoMaterial")) { txtExtra1.Focus(); }
            //if (a.Name.Equals("txtExtra1")) { txtExtra2.Focus(); }
            if (a.Name.Equals("txtExtra2")) { txtIvaImportacion.Focus(); }
            //txtCambioProducto.Focus();
        }

        private void txtNetoBs_TextChanged(object sender, EventArgs e)
        {
            TextBox a = (TextBox)sender;
            if (a.Text == "")
            {
                a.Text = "0,00000000";
            }

            a.Text = string.Format("{0:0.00000000}", Convert.ToDecimal(a.Text));
        }

        private void dateTimePicker1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtCambioLiquidar.Focus();
            }
        }

        private void txtCambioLiquidar_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox a = (TextBox)sender;
            textBox.conDecimalesDefinidos(sender, e, a, 10);
            //txtCambioProducto.Focus();
        }

        private void txtCambioProducto_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox a = (TextBox)sender;
            textBox.conDecimalesDefinidos(sender, e, a, 10);

            if (e.KeyChar == 13)
            {
                groupBox3.Select();
            }
            //txtNetoFOB.Focus();
        }

        private void bqdProveeImpor_Click(object sender, EventArgs e)
        {
            proveedores = lbxProveedores.DefInstance;
            proveedores.destino(this, this.Name);
            proveedores.ShowDialog();
        }

        private void txtControlImpor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) { txtCambioLiquidar.Focus(); }
        }

















 

    }
}
