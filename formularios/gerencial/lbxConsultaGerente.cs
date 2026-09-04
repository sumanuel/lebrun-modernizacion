using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using lebrun.clases.gerencial;
using lebrun.formularios.productos;
using lebrun.clasesData;

namespace lebrun.formularios.gerencial
{
    public partial class lbxConsultaGerente : Form
    {
        lbxMantProductos productos;
        ConsultaGeneral consulGeneral;
        

        public lbxConsultaGerente()
        {
            InitializeComponent();
        }

        private void lbxConsultaGerente_Load(object sender, EventArgs e)
        {
            productos= new lbxMantProductos();
            consulGeneral = new ConsultaGeneral(); 
        }

        private void button3_Click(object sender, EventArgs e)
        {
            productos.frmProduct = "Producto";
            productos.proCual="Gerente";
            productos.productoPasado += new lbxMantProductos.gerenteAdmin(ejecutarProductSelec);
            productos.ShowDialog();
            txtCodigo.Focus();
            txtCodigo.SelectionStart = 0;
            txtCodigo.SelectionLength = txtCodigo.Text.Length;
        }

        public void ejecutarProductSelec(string datoproducto)
        {
            txtCodigo.Text = datoproducto;
        }

        private void txtCodigo_KeyPress_1(object sender, KeyPressEventArgs e)
        {
           
            e.Handled = char.IsNumber(e.KeyChar) || e.KeyChar == 8 ? false : true;
            if (e.KeyChar == (char)(Keys.Enter))
            {
                txtCodigo.Text = string.Format("{0:000000000000000}", Convert.ToDouble(txtCodigo.Text));
                txtCodigo.SelectionStart = 0;
                txtCodigo.SelectionLength = txtCodigo.Text.Length;

                label22.Text = "Esperado Codigo";
                txtCateg.Clear();
                txtSubCat.Clear();
                txtUniM.Clear();
                txtExist.Clear();
                txtPrefa.Clear();
                txtPedid.Clear();
                txtDispon.Text = "";
                txtPrecio.Clear();
                txtUCost.Clear();
                txtOferta.Clear();
                txtEstado.Clear();
                txtUltCompra.Clear();
                txtCantComp.Clear();
                txtProve.Clear();
                txtDocComp.Clear();
                txtUltVent.Clear();
                txtClient.Clear();
                txtCantVent.Clear();
                txtDocumVent.Clear();
                txtUltAjus.Clear();
                txtCantidad.Clear();
                txtDocum.Clear();
                txtPreci.Clear();
                consulGeneral.consultaGereneralProducto(txtCodigo.Text);
                if (consulGeneral.Vacio != "Vacio")
                {
                   
                    label22.Text = consulGeneral.DescProduc;
                    txtCateg.Text = consulGeneral.DescGrupProduc;
                    txtSubCat.Text = consulGeneral.DescSubGrupProduc;
                    txtUniM.Text = consulGeneral.UndMedProduc;
                    txtExist.Text = consulGeneral.ExisProduc;
                    txtPrefa.Text = consulGeneral.PrefacProduc;
                    txtPedid.Text = consulGeneral.PedidoProduc;
                    txtDispon.Text = "";
                    txtPrecio.Text = consulGeneral.PrecioProducto;
                    txtUCost.Text = consulGeneral.UltCostProduc;
                    txtOferta.Text = consulGeneral.PromProduc;
                    if (consulGeneral.EstadoProduc == "Activo")
                    {
                        txtEstado.Text = consulGeneral.EstadoProduc;
                        txtEstado.ForeColor = Color.Lime;
                    }
                    else
                    {
                        txtEstado.Text = consulGeneral.EstadoProduc;
                        txtEstado.ForeColor = Color.Red;
                    }
                    txtUltCompra.Text = consulGeneral.FecUltCompProduc;
                    txtCantComp.Text = consulGeneral.UltCantCompProduc;
                    txtProve.Text = consulGeneral.UltProveProduc;
                    txtDocComp.Text = consulGeneral.UltRefCompProduc;
                    txtUltVent.Text = consulGeneral.FecUltVentProduc;
                    txtClient.Text = consulGeneral.UltClienProduc;
                    txtCantVent.Text = consulGeneral.UultCantVent;
                    txtDocumVent.Text = consulGeneral.UltRefVentProduc;
                    txtUltAjus.Text = consulGeneral.FecUltAjusProduc;
                    txtCantidad.Text = consulGeneral.UltCantAjustProduc;
                    txtDocum.Text = consulGeneral.UltRefAjusProduc;
                    txtPreci.Text = consulGeneral.PrecioProducto;
                }
                else
                {
                    txtCodigo.Focus();
                }
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtCodigo_TextChanged(object sender, EventArgs e)
        {

        }

        private void lbxConsultaGerente_Shown(object sender, EventArgs e)
        {
            txtCodigo.Focus();
        }
    }
}
