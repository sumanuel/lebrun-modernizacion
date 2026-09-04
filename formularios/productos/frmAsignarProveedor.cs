using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using lebrun.formularios.complementos;
using lebrun.clases.producto;

namespace lebrun.formularios.productos
{
    public partial class frmAsignarProveedor : Form
    {
        Productos producto;
        lbxProveedores proveedores;
        public string idPc;
        public string ipPc;
        public string modulo;
        public string codpais;


        public frmAsignarProveedor()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            proveedores = lbxProveedores.DefInstance;
            proveedores.destino(this, this.Name);
            proveedores.Proviene = "Asginar Proveedor";
            proveedores.Show();
            
        }

        private void frmAsignarProveedor_Load(object sender, EventArgs e)
        {
            proveedores = new lbxProveedores();
            producto = new Productos();
            producto.llenarComboxPais(cmdPdO);
            producto.llenarComboxMoneda(cmdMoneda);
        }

        private void frmAsignarProveedor_FormClosed(object sender, FormClosedEventArgs e)
        {
            lbxMantProductos mantProd = new lbxMantProductos();
            mantProd.ipMaq = ipPc;
            mantProd.IdMaq = idPc;
            mantProd.frmProduct = "Producto";
            mantProd.Show();
        }

        private void frmAsignarProveedor_Shown(object sender, EventArgs e)
        {
            txtCodProd.ReadOnly=true;
            txtDesProd.ReadOnly = true;
            txtCodProv.ReadOnly = true;
            txtDesProv.ReadOnly = true;
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtCodProv.Clear();
            txtDesProv.Clear();
        }

        private void BtnRegistra_Click(object sender, EventArgs e)
        {
            producto.codPaises(cmdPdO.Text);
            codpais = producto.CodPais;
            if (txtCodProv.Text == "" || txtDesProv.Text == "")
            {
                MessageBox.Show("El Proveedor Es Un Campo Obligatorio!!", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                button5.Focus();
            }
            else 
            {
                producto.inserAsigProvee(txtCodProd.Text, txtCodProv.Text, codpais, cmdMoneda.Text);
                MessageBox.Show("Se Creado El Producto Con Exito!!", "EXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }

        private void txtCodProv_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((txtCodProv.Text.Length > 0 && e.KeyChar == 13))
            {
                producto.codPaisProv(txtCodProv.Text);
                cmdPdO.Text = producto.NombrePais;
                cmdMoneda.Text = producto.MonedaPais;
                cmdPdO.Focus();
            }
        }

        private void cmdPdO_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((txtCodProv.Text.Length > 0 && e.KeyChar == 13))
            {
                cmdMoneda.Focus();
            }
        }

        private void cmdMoneda_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((txtCodProv.Text.Length > 0 && e.KeyChar == 13))
            {
                BtnRegistra.Focus();
            }
        }
    }
}