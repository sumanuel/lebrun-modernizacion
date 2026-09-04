using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using lebrun.clasesData;
using lebrun.formularios.facturacion;

namespace lebrun.formularios.usuarios
{
    public partial class lbxUsuarios : Form
    {
        UsuarioSistema usuLbx;
        private static lbxUsuarios m_FormDefInstance;
        private cierreCaja referenciaCierreCaja;

        public lbxUsuarios()
        {
            InitializeComponent();
        }

        public static lbxUsuarios DefInstance
        {
            get
            {
                if (m_FormDefInstance == null || m_FormDefInstance.IsDisposed)
                    m_FormDefInstance = new lbxUsuarios();
                return m_FormDefInstance;
            }
            set
            {
                m_FormDefInstance = value;
            }
        }

        private void lbxUsuarios_Load(object sender, EventArgs e)
        {
            usuLbx = new UsuarioSistema(2);
            dataGridView1.DataSource = usuLbx.lbxUsuarios();
            formatoDataGrid();
        }

        private void cmdSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void formatoDataGrid() {
            dataGridView1.Columns[0].HeaderText = "Codigo";
            dataGridView1.Columns[1].HeaderText = "Nombre";
            dataGridView1.Columns[2].HeaderText = "Cargo";
            dataGridView1.Columns[3].HeaderText = "Mapa Menu";
            dataGridView1.Columns[4].HeaderText = "Per. Contabilidad";
            dataGridView1.Columns[5].HeaderText = "Per. Administración";
        }

        private void txtBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            RadioButton radio = (groupBox2.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked));
            if (radio.Name.Equals("codigo"))
            {
                if ((e.KeyChar == '.') || (e.KeyChar == ','))
                {
                    e.Handled = true;
                    return;
                }

                if (Char.IsSymbol(e.KeyChar))
                {
                    e.Handled = true;
                    return;
                }

                if (Char.IsPunctuation(e.KeyChar))
                {
                    e.Handled = true;
                    return;
                }

                if ((e.KeyChar == (char)(Keys.Enter)) && txtBuscar.Text != "")
                {
                    if (usuLbx.buscarUsuarios(txtBuscar.Text, radio.Name).Rows.Count > 0)
                    {
                        dataGridView1.DataSource = null;
                        dataGridView1.DataSource = usuLbx.buscarUsuarios(txtBuscar.Text, radio.Name);
                        formatoDataGrid();
                        dataGridView1.ClearSelection();
                        dataGridView1.Update();
                    }
                    else {
                        MessageBox.Show("No existe ningún usuario que cumpla con el parámetro suministrado", "Aviso ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtBuscar.Focus();
                    }
                    return;
                }
            }
            if ((e.KeyChar == (char)(Keys.Enter)) && txtBuscar.Text != "")
            {
                if (usuLbx.buscarUsuarios(txtBuscar.Text, radio.Name).Rows.Count > 0)
                {
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = usuLbx.buscarUsuarios(txtBuscar.Text, radio.Name);
                    formatoDataGrid();
                    dataGridView1.ClearSelection();
                    dataGridView1.Update();
                }
                else
                {
                    MessageBox.Show("No existe ningún usuario que cumpla con el parámetro suministrado", "Aviso ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtBuscar.Focus();
                }
                return;
            }
        }

        private void cmdBuscar_Click(object sender, EventArgs e)
        {   
            txtBuscar_KeyPress(new object(), new KeyPressEventArgs((char)(Keys.Enter)));
        }

        private void cmdLimpiar_Click(object sender, EventArgs e)
        {
            txtBuscar.Clear();
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = usuLbx.lbxUsuarios();
            formatoDataGrid();
            dataGridView1.ClearSelection();
            dataGridView1.Update();
        }

        public void soloBusqueda(bool valor) { 
            cmdAgregar.Enabled = valor;
            cmdModificarCli.Enabled = valor;
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (referenciaCierreCaja != null) {
                referenciaCierreCaja.txtUsuario.Text = dataGridView1.CurrentRow.Cells["usu_codigo"].Value.ToString();
                referenciaCierreCaja.lblNombre.Text = dataGridView1.CurrentRow.Cells["usu_nombre"].Value.ToString();
                referenciaCierreCaja.lblNombre.Text = referenciaCierreCaja.lblNombre.Text.ToUpper();
                referenciaCierreCaja.despuesDeLbx();
                this.Close();
            }
        }

        public void actualizarReferenciaCierre(cierreCaja refCierre) {
            referenciaCierreCaja = refCierre;
        }
    }
}