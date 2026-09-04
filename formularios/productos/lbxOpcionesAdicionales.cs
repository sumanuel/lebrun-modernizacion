using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace lebrun.formularios.productos
{
    public partial class lbxOpcionesAdicionales : Form
    {
        public string frmProduct;
        public string ipMaq;
        public string IdMaq;


        public string IPMAQ
        {
            get { return ipMaq; ;}
            set { ipMaq = value; }
        }
        public string IDMAQ
        {
            get { return IdMaq; ;}
            set { IdMaq = value; }
        }
        public lbxOpcionesAdicionales()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmUnidaXProducto unixProd = new frmUnidaXProducto();
            unixProd.txtCodProd.Text = txtCodProd.Text;
            unixProd.txtDesProd.Text = txtDescProduc.Text;
            unixProd.codigoProd = txtCodProd.Text;
            unixProd.ipPc = IPMAQ;
            unixProd.idPc = IDMAQ;
            unixProd.modulo = "opciones adicionales";
            unixProd.Show();
            this.Close();
        }

        private void lbxOpcionesAdicionales_Load(object sender, EventArgs e)
        {

        }
    }
}
