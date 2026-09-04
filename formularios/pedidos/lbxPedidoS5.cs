using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using lebrun.clases.facturacion;

namespace lebrun.formularios.pedidos
{
    public partial class lbxPedidoS5 : Form
    {
        private DataView dv;
        private DataTable dtFact;
        private Principal referenciaPrincipal;
        private frmPedido frmPed;
        private Factura documento;
        private Factura fact1;
        private string tipoOperacion;

        public lbxPedidoS5()
        {
            InitializeComponent();
        }

        private void lbxPedidoS5_Load(object sender, EventArgs e)
        {
            dtFact = new DataTable();
            referenciaPrincipal = (Principal)this.MdiParent;
            fact1 = new Factura(200, "PED", referenciaPrincipal.usuarioActual.IpPc);//por efectos de actualizar lbx
            dtFact = fact1.lbxPed("PED", "5");
            dv = new DataView(dtFact);
            this.dataGridView1.DataSource = dtFact;
            this.Text = this.Text + this.Name;
            dataGridView1.Columns[0].HeaderText = "Numero";
            dataGridView1.Columns[1].HeaderText = "Codigo";
            dataGridView1.Columns[2].HeaderText = "Cliente";
            dataGridView1.Columns[3].HeaderText = "Fecha";
            dataGridView1.Columns[4].HeaderText = "Estado";
            dataGridView1.Columns[5].HeaderText = "Monto";
            dataGridView1.Columns[8].HeaderText = "Num Fiscal";
            dataGridView1.Columns[9].HeaderText = "Imp";
            dataGridView1.Columns[9].Width = 30;
            dataGridView1.Columns[6].Visible = false;
            dataGridView1.Columns[7].Visible = false;
            dataGridView1.Columns[10].Visible = false;
            dataGridView1.Columns["dcli_codmon"].Visible = false;
            dataGridView1.Columns["dcli_facafe"].Visible = false;
        }

        private void cmdVer_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                pictureBox1.Visible = true;
                frmPed = new frmPedido();
                frmPed.MdiParent = this.MdiParent;
                tipoOperacion = "ver";
                backgroundWorker1.RunWorkerAsync(frmPed);
            }
            else
            {
                MessageBox.Show("Debe Existir un Pedido para mostrar!!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        

        private void cmdBuscar_Click(object sender, EventArgs e)
        {
            if (!chkFecha.Checked)
            {
                if (txtBuscar.Text != "")
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
            string sentencia;
            DataTable temp;
            try
            {
                sentencia = "dcli_numero LIKE '" + filtro + "%'";
                dv.RowFilter = sentencia;
                if (dv.Count == 0)
                {
                    progressBar1.Value = 2;
                    sentencia = "cli_nombre LIKE '" + filtro + "%'";
                    dv.RowFilter = sentencia;
                }
                if (dv.Count > 0)
                {
                    progressBar1.Value = 6;
                    dataGridView1.DataSource = dv;
                    dataGridView1.Update();
                }
                else
                {
                    temp = new DataTable();
                    temp = fact1.pedidoBuscado4(txtBuscar.Text, "PED","5");
                    
                    progressBar1.Value = 6;
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
                        dataGridView1.Columns[5].HeaderText = "Monto";
                        dataGridView1.Columns[8].HeaderText = "Num Fiscal";
                        dataGridView1.Columns[9].HeaderText = "Imp";
                        dataGridView1.Columns[9].Width = 30;
                        dataGridView1.Columns[6].Visible = false;
                        dataGridView1.Columns[7].Visible = false;
                        dataGridView1.Columns[10].Visible = false;
                        dataGridView1.Columns["dcli_codmon"].Visible = false;
                        dataGridView1.Columns["dcli_facafe"].Visible = false;
                        dataGridView1.Update();
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error buscar " + e.Message);
            }
        }

        private void filtrarFecha()
        {
            DataTable dtemp;
            dtemp = fact1.pedidoBuscado3(dateTimePicker1.Value.Year + "-" + dateTimePicker1.Value.Month + "-" + dateTimePicker1.Value.Day,
                dateTimePicker2.Value.Year + "-" + dateTimePicker2.Value.Month + "-" + dateTimePicker2.Value.Day, "PED","5");

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
                dataGridView1.Columns[5].HeaderText = "Monto";
                dataGridView1.Columns[8].HeaderText = "Num Fiscal";
                dataGridView1.Columns[9].HeaderText = "Imp";
                dataGridView1.Columns[9].Width = 30;
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

        private void cmdSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void limpiarForm()
        {
            dtFact = null;
            dataGridView1.DataSource = null;
            progressBar1.Value = 1;
            txtBuscar.Clear();
            chkFecha.Checked = false;
            dtFact = fact1.lbxPed("PED", "5");

            dv = new DataView(dtFact);
            this.dataGridView1.DataSource = dtFact;
            dataGridView1.Columns[0].HeaderText = "Numero";
            dataGridView1.Columns[1].HeaderText = "Codigo";
            dataGridView1.Columns[2].HeaderText = "Cliente";
            dataGridView1.Columns[3].HeaderText = "Fecha";
            dataGridView1.Columns[4].HeaderText = "Estado";
            dataGridView1.Columns[5].HeaderText = "Monto";
            dataGridView1.Columns[8].HeaderText = "Num Fiscal";
            dataGridView1.Columns[9].HeaderText = "Imp";
            dataGridView1.Columns[9].Width = 30;
            dataGridView1.Columns[6].Visible = false;
            dataGridView1.Columns[7].Visible = false;
            dataGridView1.Columns[10].Visible = false;
            dataGridView1.Columns["dcli_codmon"].Visible = false;
            dataGridView1.Columns["dcli_facafe"].Visible = false;
            dataGridView1.Update();
        }

        private void cmdLimpiar_Click(object sender, EventArgs e)
        {
            limpiarForm();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            if (tipoOperacion.Equals("ver"))
            {
                System.ComponentModel.BackgroundWorker worker;
                worker = (System.ComponentModel.BackgroundWorker)sender;

                frmPedido fc = (frmPedido)e.Argument;
                fc.frmPreFac_Load(new object(), new EventArgs(), true);
                this.frmPed = fc.soloVista3(dataGridView1.CurrentRow.Cells["dcli_numero"].Value.ToString());
                documento = this.frmPed.fac1;
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (tipoOperacion.Equals("ver"))
            {
                if (this.frmPed != null)
                {
                    pictureBox1.Visible = false;
                    Factura factTemp = frmPed.fac1;
                    this.frmPed.Show();
                    this.frmPed.fac1 = factTemp;
                    this.frmPed.vende1 = factTemp.VendedorFactura;
                    this.frmPed.client1 = factTemp.ClienteFacturar;
                    this.frmPed.referenciaLbxPedidoS5 = this;
                    this.frmPed.asignarCondicionPago(documento.ClienteFacturar.CondicionPago);
                    this.frmPed.label40.Visible = true;
                }
                tipoOperacion = null;
                return;
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (tipoOperacion.Equals("ver"))
            {
                using (Font myFont = new Font("Arial", 24))
                {
                    e.Graphics.DrawString("Cargando", myFont, Brushes.Black, new Point(2, 2));
                }
            }
        }
    }
}
