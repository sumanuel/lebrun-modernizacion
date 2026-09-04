using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using lebrun.clases.facturacion;

namespace lebrun.formularios.facturacion
{
    public partial class LbxSerieFiscal : Form
    {
        public LbxSerieFiscal()
        {
            InitializeComponent();
        }

        private SerieFiscal serFis;
        frmSerieFiscal frmSer;
        Principal referenciaPrincipal;

        private void LbxSerieFiscal_Load(object sender, EventArgs e)
        {
            serFis = new SerieFiscal();
            dataGridView1.DataSource = serFis.lbxSerieFis();
            estiloDataGrid();
            referenciaPrincipal = (Principal)this.MdiParent;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void estiloDataGrid() {
            dataGridView1.Columns[0].HeaderText = "Ip";
            dataGridView1.Columns[1].HeaderText = "Impresora";
            dataGridView1.Columns[2].HeaderText = "Serial";
            dataGridView1.Columns[3].HeaderText = "Serie";
            dataGridView1.Columns[4].HeaderText = "Punto de venta Act";
            dataGridView1.Columns[5].HeaderText = "Caja";
        }

        private void cmdBuscar_Click(object sender, EventArgs e)
        {
            if (txtBuscar.Text != "")
            {
                RadioButton radio = (groupBox1.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked));
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = serFis.buscarLbxSeriFis(radio.Name, txtBuscar.Text);
                estiloDataGrid();
            }
            else {
                MessageBox.Show("Debe Poner algun dato para Buscar", "Aviso ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtBuscar.Focus();
            }

        }

        internal void cmdLimpiar_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = serFis.lbxSerieFis();
            estiloDataGrid();
            txtBuscar.Clear();
        }

        public void actualizarLbx() {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = serFis.lbxSerieFis();
            estiloDataGrid();
            txtBuscar.Clear();
        }

        private void txtBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)(Keys.Enter)) && txtBuscar.Text != "") {
                cmdBuscar_Click(new object(), new EventArgs());
            }
        }

        private void cmdAgregar_Click(object sender, EventArgs e)
        {
            frmSer = frmSerieFiscal.DefInstance;
            frmSer.MdiParent = this.MdiParent;
            frmSer.Show();
            frmSer.actualizarReferencia("guardar",this);
        }

        private void cmdModificar_Click(object sender, EventArgs e)
        {
            frmSer = frmSerieFiscal.DefInstance;
            frmSer.MdiParent = this.MdiParent;
            frmSer.Show();
            frmSer.actualizarReferencia("modificar", this);
            frmSer.cargarSerieFis(dataGridView1.CurrentRow.Cells[0].Value.ToString(), dataGridView1.CurrentRow.Cells[3].Value.ToString());
        }


    }
}
