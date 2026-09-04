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

namespace lebrun.formularios.cotizacion
{
    public partial class lbxPrefacturaCtz : Form
    {
        private static lbxPrefacturaCtz m_FormDefInstance;
        private Factura fact1;
        ///private frmFactura frmFacturacion;
        private frmPreFac frmFacturacion;
        private DataView dv;
        private DataTable dtFact;
        private Principal referenciaPrincipal;
        private string tipoOperacion;
        private bool centinelaReimpresion;
        private frmImportarDev importDevolucion;
        public SerieFiscal serFis;
        private frmClaveConfirmacion2 claveDev;
        private frmPreFac frmPrefactura;

        public lbxPrefacturaCtz()
        {
            InitializeComponent();
        }

        public static lbxPrefacturaCtz DefInstance
        {
            get
            {
                if (m_FormDefInstance == null || m_FormDefInstance.IsDisposed)
                    m_FormDefInstance = new lbxPrefacturaCtz();
                return m_FormDefInstance;
            }
            set
            {
                m_FormDefInstance = value;
            }
        }

        private void lbxPrefacturaCtz_Load(object sender, EventArgs e)
        {
            dtFact = new DataTable();
            referenciaPrincipal = (Principal)this.MdiParent;
            serFis = new SerieFiscal(referenciaPrincipal.usuarioActual.IpPc);
            cmdAgregar.Enabled = true;
            if (this.Name.Equals("Cotizacion"))
            {
                fact1 = new Factura(200, "CTZ", referenciaPrincipal.usuarioActual.IpPc);//por efectos de actualizar lbx
                dtFact = fact1.lbxO("CTZ");
                dv = new DataView(dtFact);
                this.dataGridView1.DataSource = dtFact;
                this.Text = this.Text + this.Name;
            }
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
                dateTimePicker2.Value.Year + "-" + dateTimePicker2.Value.Month + "-" + dateTimePicker2.Value.Day, this.Name.Equals("Pedido")?"PED":"CTZ");

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

        private void cmdLimpiar_Click(object sender, EventArgs e)
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
            if (this.Name.Equals("Cotizacion"))
            {   
                dtFact = fact1.lbxO("CTZ");
            }

            if (this.Name.Equals("Devolucion de Venta"))
            {
                dtFact = fact1.lbxDev(referenciaPrincipal.usuarioActual.NumeroCaja);
            }
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
            if (this.Name.Equals("Cotizacion"))
            {   
                frmPrefactura = new frmPreFac();
                frmPrefactura.MdiParent = this.MdiParent;
                frmPrefactura.Show();
                frmPrefactura.fac1.TipoDocumento = "CTZ";
                frmPrefactura.txtnumCont.Focus();
                frmPrefactura.Text = "Lebrun - Cotizacion";
                frmPrefactura.fac1.maximaCantidadItems("CTZ");
                frmPrefactura.label35.Text = "Cotización";
                frmPrefactura.label35.ForeColor = Color.FromArgb(0, 128, 128);
                frmPrefactura.label35.Visible = true;
            }
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
                frmFacturacion = new frmPreFac();
                frmFacturacion.MdiParent = this.MdiParent;
                frmFacturacion.Show();
                //frmFacturacion.asignarCorrelativo("DEV");
                frmFacturacion.fac1.TipoDocumento = "DEV";
                frmFacturacion.txtCodCliente.Focus();
                frmFacturacion.label35.Visible = true;
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
                frmPrefactura = new frmPreFac();
                frmPrefactura.MdiParent = this.MdiParent;
                tipoOperacion = "ver";
                backgroundWorker1.RunWorkerAsync(frmPrefactura);
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

                frmPreFac fc = (frmPreFac)e.Argument;
                fc.frmPreFac_Load(new object(), new EventArgs(), true);
                this.frmPrefactura = fc.soloVista(dataGridView1.CurrentRow.Cells["dcli_numero"].Value.ToString());
            }

            if (tipoOperacion.Equals("reimp"))
            {
                System.ComponentModel.BackgroundWorker worker;
                worker = (System.ComponentModel.BackgroundWorker)sender;
                frmPreFac fc = (frmPreFac)e.Argument;
                fc.frmPreFac_Load(new object(), new EventArgs(), true);
                this.frmPrefactura = fc.soloVista2(dataGridView1.CurrentRow.Cells["dcli_numero"].Value.ToString(), "CTZ", dataGridView1.CurrentRow.Cells["dcli_numero"].Value.ToString());
            }

            if (tipoOperacion.Equals("reimpFAV"))
            {
                System.ComponentModel.BackgroundWorker worker;
                worker = (System.ComponentModel.BackgroundWorker)sender;

                frmPreFac fc = (frmPreFac)e.Argument;
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
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (tipoOperacion.Equals("ver"))
            {
                if (this.frmFacturacion == null)
                {
                    pictureBox1.Visible = true;
                }
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            string respuesta = null;
            bool cen;
            if (tipoOperacion.Equals("ver"))
            {
                if (this.frmPrefactura != null)
                {
                    pictureBox1.Visible = false;
                    this.frmPrefactura.Show();
                }
                tipoOperacion = null;
                centinelaReimpresion = false;
                return;
            }

            if (tipoOperacion.Equals("reimp"))
            {
                if (this.frmFacturacion != null)
                {

                    tipoOperacion = "reimpFAV";
                    pictureBox1.Visible = false;
                    //pictureBox1.Visible = true;
                    //backgroundWorker1.RunWorkerAsync(frmFacturacion);
                    this.frmFacturacion.reimprimirFAV();
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
                    frmFacturacion = new  frmPreFac();
                    frmFacturacion.MdiParent = this.MdiParent;
                    pictureBox1.Visible = true;
                    backgroundWorker1.RunWorkerAsync(frmFacturacion);
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
            if ((dataGridView1.Rows.Count > 0) && (dataGridView1.CurrentRow.Cells["dcli_impreso"].Value.ToString().Equals("1")) && (dataGridView1.CurrentRow.Cells["dcli_numfis"].Value.ToString() != "") && !(dataGridView1.CurrentRow.Cells["dcli_estado"].Value.ToString().Equals("Exportado")))
            {
                importDevolucion = frmImportarDev.DefInstance;
                importDevolucion.MdiParent = this.MdiParent;
                importDevolucion.Show();
                importDevolucion.cargarFacDev((dataGridView1.CurrentRow.Cells["dcli_numero"].Value.ToString()), (dataGridView1.CurrentRow.Cells["dcli_codigo"].Value.ToString()), (dataGridView1.CurrentRow.Cells["cli_nombre"].Value.ToString()), (dataGridView1.CurrentRow.Cells["dcli_codmon"].Value.ToString()));
            }
            else if ((dataGridView1.Rows.Count == 0))
            {
                MessageBox.Show("Debe Existir un Documento Para Procesar Devolución!!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (!(dataGridView1.CurrentRow.Cells["dcli_impreso"].Value.ToString().Equals("1")))
            {
                MessageBox.Show("No se Puede hacer una Devolucion a una Factura No Impresa!!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
            else if ((dataGridView1.CurrentRow.Cells["dcli_numfis"].Value.ToString() == ""))
            {
                MessageBox.Show("No se puede hacer una Devolucion a una Factura Sin numero Fiscal!!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (dataGridView1.CurrentRow.Cells["dcli_estado"].Value.ToString().Equals("Exportado"))
            {
                MessageBox.Show("No se puede hacer una Devolucion a una Factura en estado Exportado!!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
