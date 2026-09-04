using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using lebrun.clases.pedidos;
using lebrun.clasesData;

namespace lebrun.formularios.pedidos
{
    public partial class frmAprobacionGerencial : Form
    {
        AprobacionGerencial aprob;
        FuncionesTexbox funtext;
        lbxAprobacionGerencial referenciaLbx;
        private static frmAprobacionGerencial m_FormDefInstance;
        private string operacion;

        public static frmAprobacionGerencial DefInstance
        {
            get
            {
                if (m_FormDefInstance == null || m_FormDefInstance.IsDisposed)
                    m_FormDefInstance = new frmAprobacionGerencial();
                return m_FormDefInstance;
            }
            set
            {
                m_FormDefInstance = value;
            }
        }

        public frmAprobacionGerencial()
        {
            InitializeComponent();
        }

        private void cmdAgregar_Click(object sender, EventArgs e)
        {   
        }

        private void cmdSalir_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void frmAprobacionGerencial_Load(object sender, EventArgs e)
        {
            aprob = new AprobacionGerencial();
            funtext = new FuncionesTexbox();
            this.dataGridView1.DataSource = aprob.listaReglas();
            this.dataGridView1.ClearSelection();
        }

        

        private void cmdAgregarItem_Click(object sender, EventArgs e)
        {

        }

        public void asignarModificar() 
        {
            //dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["@mov_ivatip"].Value.ToString());

        }

        public void asignarAgregar() 
        {
            int index=0;
            dataGridView1.Rows.Add();
            index = dataGridView1.Rows.Count - 1;

            dataGridView1.Rows[index].Cells["@id"].Value = dataGridView1.Rows.Count;
            dataGridView1.Rows[index].Cells["@status"].Value = checkBox2.Checked;
            dataGridView1.Rows[index].Cells["@descripcion"].Value = txtDescripcion.Text;
            dataGridView1.Rows[index].Cells["@monto"].Value = txtMonto.Text;
            limpiar();
            txtDescripcion.Focus();
        }

        public void limpiar() 
        {
            txtDescripcion.Clear();
            txtMonto.Clear();
            checkBox2.Checked = false;
            operacion = null;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)(Keys.Enter)))
            {
                if(txtDescripcion.Text != "")
                    txtMonto.Focus();
                else
                    MessageBox.Show("La descripcion no puede quedar vacia!!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Space))
            {
                e.Handled = true;
            }
            else
            {
                if ((e.KeyChar == (char)(Keys.Enter)) && txtMonto.Text != null)
                {
                    SendKeys.Send("{TAB}");
                }
                else
                {
                    funtext.conDecimalesDefinidos(sender, e, this.txtMonto, 2);
                }
            }
        }

        private void checkBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)(Keys.Enter)))
                {
                    SendKeys.Send("{TAB}");
                }
        }

        public void actualizarReferencia(lbxAprobacionGerencial referen, string ope) 
        {
            this.referenciaLbx = referen;
            this.operacion = ope;
            refereOperacion();
        }

        private void refereOperacion()
        {
            if (this.operacion.Equals("modificar"))
            {
                cmdAgregarItem.Visible = false;
                buscarAsignar(referenciaLbx.dataGridView1.CurrentRow.Cells["@descripcion"].Value.ToString());
            }
            if (this.operacion.Equals("agregar"))
            {
                cmdModificar.Visible = false;
            }

        }

        private void buscarAsignar(string des)
        {
            List<DataGridViewRow> filas = (from item in this.dataGridView1.Rows.Cast<DataGridViewRow>()
                                           where item.Cells["@descripcion"].Value.ToString() == des
                                           select item).ToList<DataGridViewRow>();
            aprob.Id = Convert.ToInt32(filas.FirstOrDefault().Cells["@id"].Value.ToString());
            txtDescripcion.Text = filas.FirstOrDefault().Cells["@descripcion"].Value.ToString();
            txtMonto.Text = ""+Convert.ToDecimal(filas.FirstOrDefault().Cells["@monto"].Value.ToString().Replace(".","."));
            checkBox2.Checked = Convert.ToBoolean(filas.FirstOrDefault().Cells["@status"].Value);
        }

        private void cmdModificar_Click(object sender, EventArgs e)
        {
            //verificar siempre aprob.Id es la Bandera del boton tambien para saber si modifico por otro elemento del grid o
            //solo modifico ese solo
            if (this.operacion.Equals("modificar"))
            {
                asignarAprob();
                if (aprob.modificarRegla())
                {
                    MessageBox.Show("Regla modificada Existosamente", "Exito ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    aprob.limpiar();                    
                    limpiar();
                    this.referenciaLbx.actualizarGrid();
                    this.Dispose();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Error al modificar Regla, este modulo se cerrara!", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.referenciaLbx.Close();
                    this.Close();
                }
            }
        }

        public void asignarAprob() {
            aprob.Descripcion = this.txtDescripcion.Text;
            aprob.Monto = Convert.ToDecimal(this.txtMonto.Text.Replace(",","."));
            aprob.Status = checkBox2.Checked;
        }


    }
}
