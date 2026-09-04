using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using lebrun.clases.producto;
using lebrun.formularios.productos;


namespace lebrun.formularios.productos
{
    public partial class lbxALICU_IVA : Form
    {
        Principal SistemaUsuario;
        private Productos ALicu_IVA;
        private DataView dv;
        private DataTable dt;
        public string frmProduct;
        public string ipMaq;
        public string IdMaq;
        string modulo = "Alicuota";
        string accion;

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
       


        public lbxALICU_IVA()
        {
            InitializeComponent();
            txtCodigo.Enabled = false;
            ttMensajes.SetToolTip(btnAgregar,"Agregar");
            ttMensajes.SetToolTip(btnModificar, "Modificar");
            ttMensajes.SetToolTip(btnSalir, "Salir");
            ttMensajes.SetToolTip(btnLimpiar, "Actualizar");
            ttMensajes.SetToolTip(btnVisor, "Visor");
        }

        private void lbxALICU_IVA_Load(object sender, EventArgs e)
        {
            txtCodigo.ReadOnly = true;
            ALicu_IVA=new Productos();
            dt=ALicu_IVA.lbxALICU_IVA();
            dv = new DataView(dt);
            this.dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].HeaderText = "Tipo De I.V.A";
            dataGridView1.Columns[1].HeaderText = "% Ventas";
            dataGridView1.Columns[2].HeaderText = "% Compra";
            dataGridView1.Columns[3].HeaderText = "% Zona Libre";
            if (frmProduct != "Producto")
            {
                SistemaUsuario = (Principal)this.MdiParent;
                IdMaq = Convert.ToString(SistemaUsuario.usuarioActual.Id);
                ipMaq = Convert.ToString(SistemaUsuario.usuarioActual.IpPc);
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmEdicionALICU_IVA AlicuIva = new frmEdicionALICU_IVA();
            AlicuIva.Eleccion = "Agregar";
            AlicuIva.idPC = IDMAQ;
            AlicuIva.ipPc = IPMAQ;
            AlicuIva.modulo = modulo;
            AlicuIva.Show();
            this.Close();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            frmEdicionALICU_IVA modificarlbx = new frmEdicionALICU_IVA();
            modificarlbx.txtTipoIVA.Text = dataGridView1.CurrentRow.Cells["alict_tipodiva"].Value.ToString();
            modificarlbx.txtCompra.Text=dataGridView1.CurrentRow.Cells["alict_compra"].Value.ToString();
            modificarlbx.txtZonaLibre.Text=dataGridView1.CurrentRow.Cells["alict_zonalibre"].Value.ToString();
            modificarlbx.txtVenta.Text=dataGridView1.CurrentRow.Cells["alict_venta"].Value.ToString();
            modificarlbx.Eleccion = "Modificar";
            modificarlbx.modulo = modulo;
            modificarlbx.ipPc = IPMAQ;
            modificarlbx.idPC = IDMAQ;
            modificarlbx.Show();
            Close();

      }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtCodigo.ReadOnly = true;
            ALicu_IVA = new Productos();
            dt = ALicu_IVA.lbxALICU_IVA();
            dv = new DataView(dt);
            this.dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].HeaderText = "Tipo De I.V.A";
            dataGridView1.Columns[1].HeaderText = "% Ventas";
            dataGridView1.Columns[2].HeaderText = "% Compra";
            dataGridView1.Columns[3].HeaderText = "% Zona Libre";  
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {

        }

        private void btnVisor_Click(object sender, EventArgs e)
        {
            frmEdicionALICU_IVA Visor =new frmEdicionALICU_IVA();
            Visor.txtTipoIVA.Text = dataGridView1.CurrentRow.Cells["alict_tipodiva"].Value.ToString();
            Visor.txtCompra.Text = dataGridView1.CurrentRow.Cells["alict_compra"].Value.ToString();
            Visor.txtZonaLibre.Text = dataGridView1.CurrentRow.Cells["alict_zonalibre"].Value.ToString();
            Visor.txtVenta.Text = dataGridView1.CurrentRow.Cells["alict_venta"].Value.ToString();
            Visor.Eleccion = "Visor";
            Visor.Show();
            Close();
        }
    }
}
