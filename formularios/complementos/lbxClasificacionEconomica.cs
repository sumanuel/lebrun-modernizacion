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
    public partial class lbxClasificacionEconomica : Form
    {
        Principal SistemaUsuario;
        frmClasificacionEconomica economica;
        proveedor proveedor;
        private DataView dv;
        private DataTable dt;
        public string Eleccion;
        public string accion;
        public string Proviene = "0";
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

        public lbxClasificacionEconomica()
        {
            InitializeComponent();
        }

        private void lbxClasificacionEconomica_Load(object sender, EventArgs e)
        {
            economica = new frmClasificacionEconomica();
            proveedor = new proveedor();
            dt = proveedor.clasificacionEconomica();
            dv = new DataView(dt);
            this.dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].HeaderText = "Codigo";
            dataGridView1.Columns[1].HeaderText = "Descripcion";
            dataGridView1.Columns[2].HeaderText = "Comentario";
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

        private void btnSubCategoria_Click(object sender, EventArgs e)
        {

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            economica.Show();
            economica.Eleccion = "Agregar";
            economica.ipPc = IPMAQ;
            economica.idPc = IDMAQ;
            this.Close();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            economica.Show();
            economica.Eleccion = "Modificar";
            economica.ipPc = IPMAQ;
            economica.idPc = IDMAQ;
            this.Close();
        }
    }
}
