using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using lebrun.clases.tesoreria;
using lebrun.Reportes;

namespace lebrun.formularios.tesoreria
{
    public partial class lbxReciboIngreso : Form
    {
        private reciboDeingreso recibo;
        private frmRecibodeIngreso frmReciboIngreso;
        private frmReporte frmReporteReciboMovConta;
        private frmReporte frmReporteRecibo;
        //private DataTable tabla;
        private Principal Principal;

        public lbxReciboIngreso()
        {
            InitializeComponent();
            recibo = new reciboDeingreso();
        }

        private void lbxReciboIngreso_Load(object sender, EventArgs e)
        {
            Principal = (Principal)this.MdiParent;
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = recibo.armarLbxReciboIngreso();
            dataGridView1.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns[0].Width = 120;
            cambiarSigno();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            frmReciboIngreso = frmRecibodeIngreso.DefInstance;
            frmReciboIngreso.login(Principal.usuarioActual.Id);
            frmReciboIngreso.referenciaRefrescar(this);
            frmReciboIngreso.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;

            if (textBox1.Text != "")
            {

                if (radioButton1.Checked)
                {
                    dataGridView1.DataSource = recibo.busquedaLbxRecibo(1, textBox1.Text);
                    dataGridView1.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dataGridView1.Columns[0].Width = 120;
                }
                if (radioButton2.Checked)
                {
                    dataGridView1.DataSource = recibo.busquedaLbxRecibo(2, textBox1.Text);
                    dataGridView1.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dataGridView1.Columns[0].Width = 120;
                }
                if (radioButton3.Checked)
                {
                    dataGridView1.DataSource = recibo.busquedaLbxRecibo(3, textBox1.Text);
                    dataGridView1.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dataGridView1.Columns[0].Width = 120;
                }
            }
            else
            {
                refrescar(sender, e);
            }

            cambiarSigno();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmReporteReciboMovConta = new frmReporte("MovContaRecibo", dataGridView1.CurrentRow.Cells[0].Value.ToString(), dataGridView1.CurrentRow.Cells[2].Value.ToString());
            frmReporteReciboMovConta.Show();

            frmReporteRecibo = new frmReporte("ReciboIngreso", dataGridView1.CurrentRow.Cells[0].Value.ToString());
            frmReporteRecibo.Show();
            

        }

        private void cambiarSigno() {

            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                if (dataGridView1.Rows[i].Cells[3].Value.ToString().Equals("Anticipo"))
                {
                    dataGridView1.Rows[i].Cells[4].Value = "" + (Convert.ToDecimal(dataGridView1.Rows[i].Cells[4].Value) * -1);
                }
            }
        
        }

        public void refrescar(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = recibo.armarLbxReciboIngreso();
            dataGridView1.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns[0].Width = 120;
            cambiarSigno();
        }




    }
}
