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
    public partial class frmEdicionALICU_IVA : Form
    {
        string accion;
        public string ipPc;
        public string idPC;
        public string modulo;
        public string Eleccion;
        public frmEdicionALICU_IVA()
        {
            InitializeComponent();
            ttMensaje.SetToolTip(btnActulizar, "Actulziar");
            ttMensaje.SetToolTip(BtnRegistra, "Registrar");
            ttMensaje.SetToolTip(btnCancelar, "Cancelar");
        }

   

        private void frmEdicionALICU_IVA_Load(object sender, EventArgs e)
        {

        }

        private void frmEdicionALICU_IVA_Shown(object sender, EventArgs e)
        {
            
            if (Eleccion == "Agregar") 
            {
                btnActulizar.Visible = false;
            }
            else if (Eleccion == "Modificar")
            {
                BtnRegistra.Visible = false;
                txtTipoIVA.Enabled = false;
            }
            else if (Eleccion == "Visor") 
            {
                BtnRegistra.Visible = false;
                btnActulizar.Visible = false;
                txtTipoIVA.ReadOnly = true;
            }
        }

        private void btnActulizar_Click(object sender, EventArgs e)
        {
            accion="Modi";
            if (MessageBox.Show("¿Desea Actulizar Este Alicuota?", "Actulizar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (txtTipoIVA.Text != "")
                {
                    Productos ModificAlic = new Productos();
                    ModificAlic.tipoAlicIVA = txtTipoIVA.Text;
                    if (txtVenta.Text == "")
                    {
                        ModificAlic.ventaAlicIVA = Convert.ToString(0.00);
                    }
                    else 
                    {
                        ModificAlic.ventaAlicIVA = txtVenta.Text;
                    }
                    if (txtCompra.Text == "")
                    {
                        ModificAlic.compraAlicIVA = Convert.ToString(0.00);
                    }
                    else
                    {
                        ModificAlic.compraAlicIVA = txtCompra.Text;
                    }
                    if (txtZonaLibre.Text == "")
                    {
                        ModificAlic.zonalibreAlicIVA = Convert.ToString(0.00);
                    }
                    else
                    {
                        ModificAlic.zonalibreAlicIVA = txtZonaLibre.Text;
                    }
                    ModificAlic.actulizarAlic_IVA();
                    ModificAlic.trazUsuario(idPC, ipPc, modulo, accion, txtTipoIVA.Text);
                    MessageBox.Show("La Categoria Se Actulizado Exitosamente!!", "Exito!! ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();

                }
                else
                {
                    MessageBox.Show("Debe Introduccir Datos El Tipo De IVA", "Importante", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtTipoIVA.Focus();
                }
            }
        }

        private void BtnRegistra_Click(object sender, EventArgs e)
        {
           
            Productos AgregarAlic = new Productos();
            accion = "Agre";
            AgregarAlic.tipoAlicIVA = txtTipoIVA.Text;
            if (txtVenta.Text == "")
            {
                AgregarAlic.ventaAlicIVA = Convert.ToString(0.00);
            }
            else
            {
                AgregarAlic.ventaAlicIVA = txtVenta.Text;
            }
            if (txtCompra.Text == "")
            {
                AgregarAlic.compraAlicIVA = Convert.ToString(0.00);
            }
            else
            {
                AgregarAlic.compraAlicIVA = txtCompra.Text;
            }
            if (txtZonaLibre.Text == "")
            {
                AgregarAlic.zonalibreAlicIVA = Convert.ToString(0.00);
            }
            else
            {
                AgregarAlic.zonalibreAlicIVA = txtZonaLibre.Text;
            }
           
            if (txtTipoIVA.Text != "")
            {
                if (MessageBox.Show("Desea Ingresar Un Nuevo Alicuota Al Sistema", "Agregar Nueva Informacion Data", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    AgregarAlic.agregarAlic_IVA();
                    AgregarAlic.trazUsuario(idPC,ipPc,modulo,accion,txtTipoIVA.Text);
                    MessageBox.Show("Se Guardado Exitosamente!!", "Exito!! ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
            else 
            {
                MessageBox.Show("El Tipo De Categoria Es Un Campo Obligatorio!!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
          
        }

        private void frmEdicionALICU_IVA_FormClosing(object sender, FormClosingEventArgs e)
        { 
            lbxALICU_IVA retorno = new lbxALICU_IVA();
            retorno.IdMaq = idPC;
            retorno.ipMaq = ipPc;
            retorno.frmProduct = "Producto";
            retorno.Show(); 
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtTipoIVA_KeyPress(object sender, KeyPressEventArgs e)

        {
            txtTipoIVA.MaxLength = 2;
            if (e.KeyChar == (char)(Keys.Enter))
            {
                txtVenta.Focus();
            }

        }

        private void txtVenta_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsNumber(e.KeyChar) || e.KeyChar == 46 || e.KeyChar == 44 || e.KeyChar == 8 ? false : true;
            txtVenta.MaxLength = 5;
            if (e.KeyChar == (char)(Keys.Enter)) 
            {
                txtCompra.Focus();
            }
        }

        private void txtCompra_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsNumber(e.KeyChar) || e.KeyChar == 46 || e.KeyChar == 44 || e.KeyChar == 8 ? false : true;
            txtCompra.MaxLength = 5;
            if (e.KeyChar == (char)(Keys.Enter)) 
            {
                txtZonaLibre.Focus();
            }
        }


        private void frmEdicionALICU_IVA_KeyPress(object sender, KeyPressEventArgs e)
        {


        }

        private void txtZonaLibre_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsNumber(e.KeyChar) || e.KeyChar == 8 ? false : true;
            txtZonaLibre.MaxLength = 5;
            if (e.KeyChar == (char)(Keys.Enter)) 
            {
                if (Eleccion == "Modificar")
                {
                    btnActulizar.Focus();
                }
                if (Eleccion == "Agregar") 
                {
                    BtnRegistra.Focus();
                }
            }
        }
    }
}
