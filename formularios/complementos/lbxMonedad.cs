using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using lebrun.clases.complementos;
using lebrun.formularios.complementos;

namespace lebrun.formularios.complementos
{
    public partial class lbxMonedad : Form
    {
        Principal SistemaUsuario;
        frmEditorMoneda moneda;
        proveedor proveedor;
        frmProveedores frmProvee;
        private DataView dv;
        private DataTable dt;
        public string Eleccion;
        public string accion;
        public string Proviene="0";
        public string frmProduct;
        public string ipMaq;
        public string IdMaq;
        public delegate void lbxmoneda(string monedaSigno);
        public event lbxmoneda signoMoney;

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
        public lbxMonedad()
        {
            InitializeComponent();
        }

        private void lbxMonedad_Load(object sender, EventArgs e)
        {
            frmProvee = new frmProveedores();
            moneda = new frmEditorMoneda();
            proveedor=new proveedor();
            dt = proveedor.monedaOdivisa();
            dv = new DataView(dt);
            this.dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].HeaderText = "Codigo";
            dataGridView1.Columns[1].HeaderText = "Descripcion";
            if (frmProduct != "Producto")
            {
                SistemaUsuario = (Principal)this.MdiParent;
                IdMaq = Convert.ToString(SistemaUsuario.usuarioActual.Id);
                ipMaq = Convert.ToString(SistemaUsuario.usuarioActual.IpPc);
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            moneda.Show();
            moneda.Eleccion = "Agregar";
            moneda.idPc = IDMAQ;
            moneda.ipPc = IPMAQ;
            this.Close();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            moneda.Show();
            moneda.Eleccion = "Visor";
            moneda.txtCodMoned.Text= this.dataGridView1.CurrentRow.Cells[0].Value.ToString();
            moneda.txtDesMoned.Text=this.dataGridView1.CurrentRow.Cells[1].Value.ToString();
            moneda.idPc = IDMAQ;
            moneda.ipPc = IPMAQ;
            this.Close();
        }

        private void lbxMonedad_Shown(object sender, EventArgs e)
        {
            if (Proviene == "1")
            {
                btnAgregar.Visible = false;
                btnModificar.Visible = false;
                btnImprimir.Visible = false;
                btnSubCategoria.Visible = true;
            }
            else 
            {
                btnAgregar.Visible = true;
                btnModificar.Visible = true;
                btnImprimir.Visible = true;
                btnSubCategoria.Visible = false;
            }
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            signoMoney(dataGridView1.CurrentRow.Cells["mon_codigo"].Value.ToString());
            this.Close();
        }

        private void lbxMonedad_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }
    }
}
