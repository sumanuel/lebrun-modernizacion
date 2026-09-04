using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using lebrun.clases.complementos;

namespace lebrun.formularios.productos
{
    public partial class lbxVisor3 : Form
    {
        private Producto producto;
        private DataView dv;
        private DataTable dt;
        public lbxVisor3()
        {
            InitializeComponent();
        }

        private void lbxVisor3_Load(object sender, EventArgs e)
        {
            producto = new Producto();
            dt = producto.cargarProductoVisor3();
            dv = new DataView(dt);
            this.dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].HeaderText = "Codigo Producto";
            dataGridView1.Columns[1].HeaderText = "Descripcion producto";
            dataGridView1.Columns[2].HeaderText = "Unidad Medida";
            dataGridView1.Columns[0].Width = 110;
            dataGridView1.Columns[1].Width = 382;
            dataGridView1.Columns[3].Visible = false;
            dataGridView1.Columns[4].Visible = false;
            dataGridView1.Columns[5].Visible = false; 
            dataGridView1.Columns[6].Visible = false; 
            dataGridView1.Columns[7].Visible = false;
            dataGridView1.Columns[8].Visible = false; 

        }

        private void cmdSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataTable Temp;
            if (textBox1.Text != "")
            {
                RadioButton radio = (groupBox3.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked));

                //dataGridView1.DataSource = null;
                if (producto.buscarProductos(textBox1.Text, radio.Name).Rows.Count > 0)
                {
                    Temp = producto.busquedaProductoVisor3(textBox1.Text, radio.Name);
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = Temp;
                    dataGridView1.Columns[0].HeaderText = "Codigo Producto";
                    dataGridView1.Columns[1].HeaderText = "Descripcion producto";
                    dataGridView1.Columns[2].HeaderText = "Unidad Medida";
                    dataGridView1.Columns[3].Visible = false;
                    dataGridView1.Columns[4].Visible = false;
                    dataGridView1.Columns[5].Visible = false;
                    dataGridView1.Columns[6].Visible = false;
                    dataGridView1.Columns[7].Visible = false;
                    dataGridView1.Columns[0].Width = 110;
                    dataGridView1.Columns[1].Width = 400;
 
                    dataGridView1.Update();
                }
                else
                {
                    MessageBox.Show("No existe ningún producto que cumpla con el parámetro suministrado", "Aviso ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    textBox1.Focus();
                }
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                button1.PerformClick();
            }
        }



    }
}
