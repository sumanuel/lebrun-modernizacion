using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using lebrun.clases.complementos;
using lebrun.formularios.reportes;

namespace lebrun.formularios.complementos
{
    public partial class lbxTrabjadores : Form
    {
        private trabajador trabajadores;
        private static lbxTrabjadores m_FormDefInstance;
        private frmEmisionARC refEmisionArc;
        private string origen;

        public lbxTrabjadores()
        {
            InitializeComponent();
            trabajadores = new trabajador();
        }

        public static lbxTrabjadores DefInstance
        {
            get
            {
                if (m_FormDefInstance == null || m_FormDefInstance.IsDisposed)
                    m_FormDefInstance = new lbxTrabjadores();
                return m_FormDefInstance;
            }
            set
            {
                m_FormDefInstance = value;
            }
        }

        private void lbxTrabjadores_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = trabajadores.cargarTrabajadoresLBX();
            dataGridView1.Columns[0].Width = 125;
            dataGridView1.Columns[1].Width = 350;
            dataGridView1.Columns[2].Width = 125;
        }

        public void destino(Form referencia, string opcion)
        {

            if (opcion.Equals("frmEmisionARC"))
            {
                origen = opcion;
                refEmisionArc = (frmEmisionARC)referencia;
            }
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (origen.Equals("frmEmisionARC"))
            {
                refEmisionArc.txtCodigo.Text = this.dataGridView1.CurrentRow.Cells[0].Value.ToString();
                refEmisionArc.txtCodigo.Focus();
                this.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = trabajadores.buscarTrabajador(textBox1.Text);
            dataGridView1.Columns[0].Width = 125;
            dataGridView1.Columns[1].Width = 350;
            dataGridView1.Columns[2].Width = 125;
        }
    }
}
