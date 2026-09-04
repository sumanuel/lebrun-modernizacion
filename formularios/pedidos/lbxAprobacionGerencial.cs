using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using lebrun.clases.pedidos;

namespace lebrun.formularios.pedidos
{
    public partial class lbxAprobacionGerencial : Form
    {
        AprobacionGerencial aprob;
        private static lbxAprobacionGerencial m_FormDefInstance;
        frmAprobacionGerencial formAprobGerencial;

        public static lbxAprobacionGerencial DefInstance
        {
            get
            {
                if (m_FormDefInstance == null || m_FormDefInstance.IsDisposed)
                    m_FormDefInstance = new lbxAprobacionGerencial();
                return m_FormDefInstance;
            }
            set
            {
                m_FormDefInstance = value;
            }
        }

        public lbxAprobacionGerencial()
        {
            InitializeComponent();
        }

        private void lbxAprobacionGerencial_Load(object sender, EventArgs e)
        {
            aprob = new AprobacionGerencial();
            this.dataGridView1.DataSource = aprob.listaReglas();
        }

        private void cmdSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdAgregar_Click(object sender, EventArgs e)
        {
            frmAprobacionGerencial.DefInstance.Dispose();
            formAprobGerencial = frmAprobacionGerencial.DefInstance;
            formAprobGerencial.MdiParent = this.MdiParent;
            formAprobGerencial.Show();
            formAprobGerencial.actualizarReferencia(this, "agregar");
        }

        private void cmdModificar_Click(object sender, EventArgs e)
        {
            frmAprobacionGerencial.DefInstance.Dispose();
            formAprobGerencial = frmAprobacionGerencial.DefInstance;
            formAprobGerencial.MdiParent = this.MdiParent;
            formAprobGerencial.Show();
            formAprobGerencial.actualizarReferencia(this, "modificar");
        }

        public void actualizarGrid() {
            this.dataGridView1.DataSource = null;
            this.dataGridView1.DataSource = aprob.listaReglas();
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].HeaderText = "Status";
            dataGridView1.Columns[2].HeaderText = "Descripcion";
            dataGridView1.Columns[3].HeaderText = "Monto";

            
            
            

        }
    }
}
