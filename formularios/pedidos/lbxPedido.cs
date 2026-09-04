using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using lebrun.clases.facturacion;
using System.Threading;
using System.Runtime.InteropServices;
using lebrun.formularios.facturacion;

namespace lebrun.formularios.pedidos
{
    public partial class lbxPedido : Form
    {
        lbxPedido m_FormDefInstance;
        private DataView dv;
        private DataTable dtFact;
        private Principal referenciaPrincipal;
        private string tipoOperacion;
        private bool centinelaReimpresion;
        public SerieFiscal serFis;
        private frmClaveConfirmacion2 claveDev;
        private Factura fact1;
        private frmPedido frmPed;
        private Factura documento;

        public lbxPedido()
        {
            InitializeComponent();
        }

        private void lbxPrefacturaCtz_Load(object sender, EventArgs e)
        {   
            dtFact = new DataTable();
            referenciaPrincipal = (Principal)this.MdiParent;
            serFis = new SerieFiscal(referenciaPrincipal.usuarioActual.IpPc);
            cmdAgregar.Enabled = true;
            fact1 = new Factura(200, "PED", referenciaPrincipal.usuarioActual.IpPc);//por efectos de actualizar lbx
            dtFact = fact1.lbxPed("PED","Activo");
            dtFact.Merge(fact1.lbxPed("PED", "1"));
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

        private void cmdSalir_Click(object sender, EventArgs e)
        {
            this.Close();
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
                    if (dv.Count == 0)
                    {
                        progressBar1.Value = 4;
                        sentencia = "dcli_numfis LIKE '" + filtro + "%'";
                        dv.RowFilter = sentencia;
                    }
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
                    if (this.Name.Equals("Pedido"))
                    {
                        temp = fact1.pedidoBuscado(txtBuscar.Text, "PED");
                    }
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
            dtemp = fact1.pedidoBuscado2(dateTimePicker1.Value.Year + "-" + dateTimePicker1.Value.Month + "-" + dateTimePicker1.Value.Day,
                dateTimePicker2.Value.Year + "-" + dateTimePicker2.Value.Month + "-" + dateTimePicker2.Value.Day, this.Name.Equals("Pedido") ? "PED" : "CTZ");

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

        internal void cmdLimpiar_Click(object sender, EventArgs e)
        {
            limpiarForm();
        }

        public void limpiarForm()
        {
            dtFact = null;
            dataGridView1.DataSource = null;
            progressBar1.Value = 1;
            txtBuscar.Clear();
            chkFecha.Checked = false;
            dtFact = fact1.lbxPed("PED", "Activo");
            dtFact.Merge(fact1.lbxPed("PED", "1"));
            
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

        [DllImport("user32.DLL")]
        public static extern void SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        private void cmdAgregar_Click_1(object sender, EventArgs e)
        {   
            frmPed = new frmPedido();
            frmPed.MdiParent = this.MdiParent;
            frmPed.Show();
            frmPed.fac1.TipoDocumento = "PED";
            frmPed.txtnumCont.Focus();
            frmPed.Text = "Lebrun - Pedido";
            frmPed.fac1.maximaCantidadItems("PED");
            frmPed.label35.Text = "PEDIDO";
            frmPed.label35.ForeColor = Color.FromArgb(0, 128, 128);
            frmPed.label35.Visible = true;
            frmPed.referenciaLbxPedido = this;
        }

        public void despuesConfirmacionLbx(bool valor)
        {
            if (valor)
            {
                this.Enabled = true;
                //frmFacturacion = frmFactura.DefInstance;
                //frmFacturacion.Dispose();
                //frmFacturacion = frmFactura.DefInstance;
                //esto es para que puedan ejecutar  mas de 1 FRM
                frmPed = new frmPedido();
                frmPed.MdiParent = this.MdiParent;
                frmPed.Show();
                //frmFacturacion.asignarCorrelativo("DEV");
                frmPed.fac1.TipoDocumento = "PED";
                frmPed.txtCodCliente.Focus();
                frmPed.label35.Visible = true;
            }
            else
            {
                MessageBox.Show("Debe Colocar la clave para la devolucion!!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Enabled = true;
            }
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
                MessageBox.Show("Debe Existir una Factura Para Mostrar!!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            if (tipoOperacion.Equals("ver"))
            {
                System.ComponentModel.BackgroundWorker worker;
                worker = (System.ComponentModel.BackgroundWorker)sender;

                frmPedido fc = (frmPedido)e.Argument;
                fc.frmPreFac_Load(new object(), new EventArgs(), true);
                this.frmPed = fc.soloVista(dataGridView1.CurrentRow.Cells["dcli_numero"].Value.ToString());
                documento = this.frmPed.fac1;
            }

            if (tipoOperacion.Equals("reimp"))
            {
                System.ComponentModel.BackgroundWorker worker;
                worker = (System.ComponentModel.BackgroundWorker)sender;
                frmPedido fc = (frmPedido)e.Argument;
                fc.frmPreFac_Load(new object(), new EventArgs(), true);
                this.frmPed = fc.soloVista2(dataGridView1.CurrentRow.Cells["dcli_numero"].Value.ToString(), "PED", dataGridView1.CurrentRow.Cells["dcli_numero"].Value.ToString());
                documento = this.frmPed.fac1;
            }

            if (tipoOperacion.Equals("reimpFAV"))
            {
                System.ComponentModel.BackgroundWorker worker;
                worker = (System.ComponentModel.BackgroundWorker)sender;

                frmPedido fc = (frmPedido)e.Argument;
                centinelaReimpresion = fc.reimprimirFAV();
                fc.limpiarCompleto();
            }

            if (tipoOperacion.Equals("reimpDEV"))
            {
                System.ComponentModel.BackgroundWorker worker;
                worker = (System.ComponentModel.BackgroundWorker)sender;

                frmFactura fc = (frmFactura)e.Argument;
                centinelaReimpresion = fc.reimprimirDEV(dataGridView1.CurrentRow.Cells["dcli_facafe"].Value.ToString());
                if (fc.fac1.Dcli_estado.Equals("Activo"))
                {
                    fc.fac1.guardarNumeroFiscalSalcli();
                }
                fc.limpiarCompleto();

            }

            if (tipoOperacion.Equals("editar"))
            {
                System.ComponentModel.BackgroundWorker worker;
                worker = (System.ComponentModel.BackgroundWorker)sender;
                frmPedido fped = (frmPedido)e.Argument;
                fped.frmPreFac_Load(new object(), new EventArgs(), true);
                fped = fped.cargarModificar((dataGridView1.CurrentRow.Cells["dcli_numero"].Value.ToString()), "PED");
                documento = fped.fac1;
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (tipoOperacion.Equals("ver"))
            {
                if (this.frmPed == null)
                {
                    pictureBox1.Visible = true;
                }
            }
            if (tipoOperacion.Equals("editar"))
            {
                pictureBox1.Visible = true;
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            string respuesta = null;
            bool cen;
            if (tipoOperacion.Equals("ver"))
            {
                if (this.frmPed != null)
                {
                    pictureBox1.Visible = false;
                    this.frmPed.referenciaLbxPedido = this;
                    this.frmPed.Show();
                    this.frmPed.asignarCondicionPago(documento.ClienteFacturar.CondicionPago);
                }
                tipoOperacion = null;
                centinelaReimpresion = false;
                return;
            }

            if (tipoOperacion.Equals("reimp"))
            {
                if (this.frmPed != null)
                {

                    tipoOperacion = "reimpFAV";
                    pictureBox1.Visible = false;
                    //pictureBox1.Visible = true;
                    //backgroundWorker1.RunWorkerAsync(frmFacturacion);
                    this.frmPed.reimprimirFAV();
                    return;
                }
            }

            if (tipoOperacion.Equals("reimpFAV"))
            {
                if (centinelaReimpresion)
                {
                    pictureBox1.Visible = false;
                    //MessageBox.Show("Pedido Reimpreso Existosamente", "Exito ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tipoOperacion = null;
                    centinelaReimpresion = false;
                    //limpiarForm();
                    return;
                }
                else
                {
                    MessageBox.Show("Error al Reimprimir Pedido", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            if (tipoOperacion.Equals("reimpDEV"))
            {
                if (centinelaReimpresion)
                {
                    pictureBox1.Visible = false;
                    MessageBox.Show("Devolucion Reimpresa Existosamente", "Exito ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tipoOperacion = null;
                    centinelaReimpresion = false;
                    limpiarForm();
                    return;
                }
                else
                {
                    MessageBox.Show("Error al Remprimir Devolucion", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            if (tipoOperacion.Equals("editar")) 
            {
                pictureBox1.Visible = false;
                Factura factTemp = frmPed.fac1;
                frmPed.Show();
                frmPed.fac1 = factTemp;
                frmPed.txtCodCliente_KeyPress(new object(), new KeyPressEventArgs((char)(Keys.Enter)));
                frmPed.txtCodVendedor_KeyPress(new object(), new KeyPressEventArgs((char)(Keys.Enter)));
                frmPed.txtnumCont.Focus();
                frmPed.operacion = "editar";
                frmPed.label35.Text = "MODIFICAR PEDIDO";
                frmPed.label35.ForeColor = Color.FromArgb(0, 128, 128);
                frmPed.label35.Visible = true;
                frmPed.fac1.CorrelativoInterno = frmPed.txtCorrelativo.Text;
                frmPed.referenciaLbxPedido = this;
                this.frmPed.asignarCondicionPago(documento.ClienteFacturar.CondicionPago);
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (tipoOperacion.Equals("ver"))
            {
                using (Font myFont = new Font("Arial", 24))
                {
                    e.Graphics.DrawString("Cargando!", myFont, Brushes.Black, new Point(2, 2));
                }
            }

            if (tipoOperacion.Equals("reimp"))
            {
                using (Font myFont = new Font("Arial", 24))
                {
                    e.Graphics.DrawString("Cargando Datos Por Favor Espere!", myFont, Brushes.Black, new Point(2, 2));
                }
            }

            if (tipoOperacion.Equals("reimpFAV"))
            {
                using (Font myFont = new Font("Arial", 24))
                {
                    e.Graphics.DrawString("Imprimiendo Factura Por Favor Espere!", myFont, Brushes.Black, new Point(2, 2));
                }
            }

            if (tipoOperacion.Equals("reimpDEV"))
            {
                using (Font myFont = new Font("Arial", 24))
                {
                    e.Graphics.DrawString("Imprimiendo Dev. Por Favor Espere!", myFont, Brushes.Black, new Point(2, 2));
                }
            }

            if (tipoOperacion.Equals("editar"))
            {
                using (Font myFont = new Font("Arial", 24))
                {
                    e.Graphics.DrawString("Cargando. Por Favor Espere!", myFont, Brushes.Black, new Point(2, 2));
                }
            }
        }

        private void cmdImprimir_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                DialogResult respuesta;
                respuesta = MessageBox.Show("¿Desea Imprimir Documento?", "Imprimir Documento", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (respuesta == System.Windows.Forms.DialogResult.Yes)
                {
                    tipoOperacion = "reimp";
                    frmPed = new frmPedido();
                    frmPed.MdiParent = this.MdiParent;
                    pictureBox1.Visible = true;
                    backgroundWorker1.RunWorkerAsync(frmPed);
                }
                else
                {
                    return;
                }
            }
            else
            {
                MessageBox.Show("Debe Existir un Documento Para Imprimir!!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void cmdDevolucion_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow.Cells["dcli_estado"].Value.ToString().Equals("1"))
            {
                MessageBox.Show("No se Puede modificar un Pedido en Prefacturación!!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else
            {
                pictureBox1.Visible = true;
                tipoOperacion = "editar";
                frmPed = new frmPedido();
                frmPed.MdiParent = this.MdiParent;
                backgroundWorker1.RunWorkerAsync(frmPed);
            }
        }

        private void cmdLimpiar_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(this.cmdLimpiar, "Refrescar Lbx");
        }

        private void lbxPrefacturaCtz_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.serFis.setActivate(0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cierreCaja cie = new cierreCaja();
            cie.MdiParent = this.MdiParent;
            cie.Show();
        }


    }
}
