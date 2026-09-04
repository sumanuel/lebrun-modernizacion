using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using lebrun.formularios.bultos;
using lebrun.clases.facturacion;
using lebrun.clases.bulto;
using lebrun.clases.clientes;
using lebrun.clases.vendedores;
using lebrun.Reportes.bulto;

namespace lebrun.formularios.bultos
{
    public partial class lbxBulto2 : Form
    {
        private lbxBultos bult;
        private Principal referenciaPrincipal;
        private Factura preBulto;
        private Bulto bul1;
        private Clientes cli1;
        private Vendedor vende1;
        private frmContenedorBulto frmReporte;
        
        public lbxBulto2()
        {
            InitializeComponent();
        }

        private void cmdAgregar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow.Cells["dcli_estado"].Value.ToString().Equals("2"))
            {
                MessageBox.Show("El bulto seleccionado se encuentra cerrado");
                return;
            }

            if (bul1.existeBulto(dataGridView1.CurrentRow.Cells["dcli_numero"].Value.ToString())) 
            {
                bult = new lbxBultos();
                bult.MdiParent = this.MdiParent;
                bult.Show();
                bult.cargarPrefactura(dataGridView1.CurrentRow.Cells["dcli_numero"].Value.ToString());
                bult.asignarDatos();
                bult.pesoBulto();
                bult.cargarDetalle(dataGridView1.CurrentRow.Cells["dcli_numero"].Value.ToString());
                bult.textBox2.Focus();
                bult.textBox1.ReadOnly = true;
                bult.textBox4.ReadOnly = true;
                bult.button1.Enabled = false;
                bult.cmdConfirmar.Text = "Guardar Temporal";
                bult.cmdConfirmar.Image = lebrun.Properties.Resources.Edit_24x24;
                bult.referencialbxBultos = this;
            }
            else
            {
                bult = new lbxBultos();
                bult.MdiParent = this.MdiParent;
                bult.Show();
                bult.cargarPrefactura(dataGridView1.CurrentRow.Cells["dcli_numero"].Value.ToString());
                bult.asignarDatos();
                bult.referencialbxBultos = this;
            }
        }

        private void lbxBulto2_Load(object sender, EventArgs e)
        {
            referenciaPrincipal = (Principal)this.MdiParent;
            preBulto = new Factura(200, "PRE", referenciaPrincipal.usuarioActual.IpPc);
            cli1 = new Clientes();
            vende1 = new Vendedor();
            bul1 = new Bulto();
            this.dataGridView1.DataSource = preBulto.lbxPreBulto();
            dataGridView1.Columns[0].HeaderText = "Numero";
            dataGridView1.Columns[1].HeaderText = "Codigo";
            dataGridView1.Columns[2].HeaderText = "Cliente";
            dataGridView1.Columns[3].HeaderText = "Fecha";
            dataGridView1.Columns[4].HeaderText = "Estado";
        }

        private void cmdSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdBuscar_Click(object sender, EventArgs e)
        {
            if (!chkFecha.Checked)
            {
                if (!(String.IsNullOrEmpty(txtBuscar.Text)))
                {
                    filtrar(txtBuscar.Text);
                }
                else
                {
                    MessageBox.Show("Debe Poner un Dato Para buscar!!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtBuscar.Focus();
                }
            }
            else
            {
                filtrarFecha();
            }
        }

        private void filtrar(string filtro)
        { 
            DataTable temp;
            temp = new DataTable();
            temp = preBulto.prefacturaBuscado(txtBuscar.Text, "PRE","1");
            temp.Merge(preBulto.prefacturaBuscado(txtBuscar.Text, "PRE","2"));
            temp.Merge(preBulto.prefacturaBuscado(txtBuscar.Text, "PRE", "3"));
            if (temp.Rows.Count == 0)
            {
                MessageBox.Show("No se consiguieron Resultados!!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = temp;
                dataGridView1.Columns[0].HeaderText = "Numero";
                dataGridView1.Columns[1].HeaderText = "Codigo";
                dataGridView1.Columns[2].HeaderText = "Cliente";
                dataGridView1.Columns[3].HeaderText = "Fecha";
                dataGridView1.Columns[4].HeaderText = "Estado";
                dataGridView1.Columns[5].Visible = false;
                dataGridView1.Columns[8].Visible = false;
                dataGridView1.Columns[9].Visible = false;
                dataGridView1.Columns[9].Visible = false;
                dataGridView1.Columns[6].Visible = false;
                dataGridView1.Columns[7].Visible = false;
                dataGridView1.Columns[10].Visible = false;
                dataGridView1.Columns["dcli_codmon"].Visible = false;
                dataGridView1.Columns["dcli_facafe"].Visible = false;
                dataGridView1.Update();
            }

        }

        private void filtrarFecha()
        {
            DataTable dtemp;
            dtemp = preBulto.prefacturaBuscado2(dateTimePicker1.Value.Year + "-" + dateTimePicker1.Value.Month + "-" + dateTimePicker1.Value.Day,
                dateTimePicker2.Value.Year + "-" + dateTimePicker2.Value.Month + "-" + dateTimePicker2.Value.Day, "PRE", "1");

            dtemp.Merge(preBulto.prefacturaBuscado2(dateTimePicker1.Value.Year + "-" + dateTimePicker1.Value.Month + "-" + dateTimePicker1.Value.Day,
                dateTimePicker2.Value.Year + "-" + dateTimePicker2.Value.Month + "-" + dateTimePicker2.Value.Day, "PRE", "2"));

            dtemp.Merge(preBulto.prefacturaBuscado2(dateTimePicker1.Value.Year + "-" + dateTimePicker1.Value.Month + "-" + dateTimePicker1.Value.Day,
                dateTimePicker2.Value.Year + "-" + dateTimePicker2.Value.Month + "-" + dateTimePicker2.Value.Day, "PRE", "3"));
            if (dtemp.Rows.Count == 0)
            {
                MessageBox.Show("No se consiguieron Resultados!!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = dtemp;//ml 370 gs
                dataGridView1.Columns[0].HeaderText = "Numero";
                dataGridView1.Columns[1].HeaderText = "Codigo";
                dataGridView1.Columns[2].HeaderText = "Cliente";
                dataGridView1.Columns[3].HeaderText = "Fecha";
                dataGridView1.Columns[4].HeaderText = "Estado";
                dataGridView1.Columns[5].Visible = false;
                dataGridView1.Columns[8].Visible = false;
                dataGridView1.Columns[9].Visible = false;
                dataGridView1.Columns[9].Visible = false;
                dataGridView1.Columns[6].Visible = false;
                dataGridView1.Columns[7].Visible = false;
                dataGridView1.Columns[10].Visible = false;
                dataGridView1.Columns["dcli_codmon"].Visible = false;
                dataGridView1.Columns["dcli_facafe"].Visible = false;
                dataGridView1.Update();
            }
        }

        private void chkFecha_CheckedChanged(object sender, EventArgs e)
        {
            if (chkFecha.Checked)
            {
                dateTimePicker1.Enabled = true;
                dateTimePicker2.Enabled = true;
            }
            else
            {
                dateTimePicker1.Enabled = false;
                dateTimePicker2.Enabled = false;
            }
        }

        public void cmdLimpiar_Click(object sender, EventArgs e)
        {
            limpiarForm();
        }

        public void limpiarForm()
        {   
            dataGridView1.DataSource = null;
            txtBuscar.Clear();
            chkFecha.Checked = false;
            this.dataGridView1.DataSource = preBulto.lbxPreBulto();
            dataGridView1.Columns[0].HeaderText = "Numero";
            dataGridView1.Columns[1].HeaderText = "Codigo";
            dataGridView1.Columns[2].HeaderText = "Cliente";
            dataGridView1.Columns[3].HeaderText = "Fecha";
            dataGridView1.Columns[4].HeaderText = "Estado";
            dataGridView1.Update();
        }

        private void cmdImprimir_Click(object sender, EventArgs e)
        {
            if (bul1.existeBulto(dataGridView1.CurrentRow.Cells["dcli_numero"].Value.ToString()))
            {
                frmReporte = new frmContenedorBulto();
                preBulto.cargarPrefactura(dataGridView1.CurrentRow.Cells["dcli_numero"].Value.ToString(), cli1, vende1);
                cli1.cargarDatosCliente();
                frmReporte.armarReporte(bul1.datasetReporteBulto(preBulto, cli1, referenciaPrincipal.empresaActual.Nombre, referenciaPrincipal.empresaActual.Rif,
                    bul1));
                frmReporte.Show();
                frmReporte.MdiParent = this.MdiParent;
            }
            else
            {
                MessageBox.Show("No existe bulto cargado para esta prefactura", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
