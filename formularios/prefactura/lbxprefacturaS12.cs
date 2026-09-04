using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using lebrun.clases.facturacion;
using lebrun.Reportes.prefactura;
using lebrun.formularios.bultos;

namespace lebrun.formularios.prefactura
{
    public partial class lbxprefacturaS12 : Form
    {
        private Factura prefact1;
        private Principal referenciaPrincipal;
        private DataTable dtPrefact;
        private DataView dv;
        private frmPrefactura frmPre1;
        private frmPrefactura frmpre2 = null;
        private string tipoOperacion = null;
        private Factura documento = null;
        private frmReportePrefactura frmReporte;
        private lbxBultos frmBultos;

        public lbxprefacturaS12()
        {
            InitializeComponent();
        }

        private void lbxprefacturaS12_Load(object sender, EventArgs e)
        {
            dtPrefact = new DataTable();
            referenciaPrincipal = (Principal)this.MdiParent;
            prefact1 = new Factura(200, "PRE", referenciaPrincipal.usuarioActual.IpPc);//por efectos de actualizar lbx


            dtPrefact = prefact1.lbxOPRE("PRE", "1");
            dtPrefact.Merge(prefact1.lbxOPRE("PRE", "2"));
            dv = new DataView(dtPrefact);
            this.dataGridView1.DataSource = dtPrefact;
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

        private void cmdAgregar_Click(object sender, EventArgs e)
        {
            /*if (dataGridView1.CurrentRow.Cells["dcli_impreso"].Value.ToString().Equals("1"))
            {
                MessageBox.Show("La prefactura ya se encuentra Modificada", "Aviso ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {*/
                //if (dataGridView1.CurrentRow.Cells["dcli_estado"].Value.ToString().Equals("2"))
                //{
                //    MessageBox.Show("Aviso la prefactura cerro la carga de bultos", "Aviso ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //}
                pictureBox1.Visible = true;
                tipoOperacion = "editar";
                frmPre1 = new frmPrefactura();
                frmPre1.MdiParent = this.MdiParent;
                backgroundWorker1.RunWorkerAsync(frmPre1);
            /*}*/
            
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            if (tipoOperacion.Equals("ver"))
            {
                System.ComponentModel.BackgroundWorker worker;
                worker = (System.ComponentModel.BackgroundWorker)sender;

                frmPrefactura fc = (frmPrefactura)e.Argument;
                fc.frmPreFac_Load(new object(), new EventArgs(), true);
                this.frmpre2 = fc.soloVista(dataGridView1.CurrentRow.Cells["dcli_numero"].Value.ToString());
                documento = frmpre2.fac1;
            }

            if (tipoOperacion.Equals("imprimirAlma") || tipoOperacion.Equals("imprimirVen"))
            {
                System.ComponentModel.BackgroundWorker worker;
                worker = (System.ComponentModel.BackgroundWorker)sender;
                frmPrefactura fc = (frmPrefactura)e.Argument;
                fc.frmPreFac_Load(new object(), new EventArgs(), true);
                fc.MdiParent = this.MdiParent;
                documento = fc.soloDocumento(dataGridView1.CurrentRow.Cells["dcli_numero"].Value.ToString());
            }
            if (tipoOperacion.Equals("editar"))
            {
                System.ComponentModel.BackgroundWorker worker;
                worker = (System.ComponentModel.BackgroundWorker)sender;
                frmPrefactura fpre = (frmPrefactura)e.Argument;
                fpre.frmPreFac_Load(new object(), new EventArgs(), true);
                this.frmpre2 = fpre.cargarModificar((dataGridView1.CurrentRow.Cells["dcli_numero"].Value.ToString()), "PRE");
                documento = frmpre2.fac1;
            }

        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (tipoOperacion.Equals("ver"))
            {
                if (frmpre2 != null)
                {
                    pictureBox1.Visible = false;
                    this.frmpre2.Show();
                    this.frmpre2.cargarComboTransporte(documento.ClienteFacturar);
                    this.frmpre2.referencia12 = this;
                    this.frmpre2.asignarCondicionPago(documento.ClienteFacturar.CondicionPago);
                    tipoOperacion = null;
                    return;
                }
            }

            if (tipoOperacion.Equals("imprimirAlma"))
             {
                if (documento != null)
                {
                    pictureBox1.Visible = false;
                    tipoOperacion = null;
                    frmReporte = new frmReportePrefactura();
                    frmReporte.MdiParent = this.MdiParent;
                    frmReporte.armarReporte(documento.rptPedidoM(documento.ClienteFacturar, referenciaPrincipal.usuarioActual.Login, documento, documento.ClienteFacturar.Transporte,true), referenciaPrincipal.empresaActual.Nombre,
                        referenciaPrincipal.empresaActual.Rif, false);
                    frmReporte.Show();

                    return;
                }
            }

            if (tipoOperacion.Equals("imprimirVen"))
            {
                if (documento != null)
                {
                    pictureBox1.Visible = false;
                    tipoOperacion = null;
                    frmReporte = new frmReportePrefactura();
                    frmReporte.MdiParent = this.MdiParent;
                    frmReporte.armarReporte(documento.rptPedidoM(documento.ClienteFacturar, referenciaPrincipal.usuarioActual.Login, documento, documento.ClienteFacturar.Transporte,false), referenciaPrincipal.empresaActual.Nombre,
                        referenciaPrincipal.empresaActual.Rif, true);
                    frmReporte.Show();

                    return;
                }
            }

            if (tipoOperacion.Equals("editar"))
            {
                pictureBox1.Visible = false;
                tipoOperacion = null;
                this.frmpre2.Show();
                Factura factTemp = frmpre2.fac1;
                frmpre2.fac1 = documento;
                frmpre2.txtCodCliente_KeyPress(new object(), new KeyPressEventArgs((char)(Keys.Enter)));
                frmpre2.txtCodVendedor_KeyPress(new object(), new KeyPressEventArgs((char)(Keys.Enter)));
                frmpre2.txtnumCont.Focus();
                frmpre2.operacion = "editar";
                frmpre2.label35.Text = "MODIFICAR PREFACTURA";
                frmpre2.label35.Visible = true;
                frmpre2.fac1.CorrelativoInterno = frmpre2.txtCorrelativo.Text;
                frmpre2.txtnumCont.Enabled = false;
                frmpre2.txtDirEntrega.Enabled = true;
                frmpre2.txtCodProducto.Focus();
                frmpre2.referencia12 = this;
                this.frmpre2.cargarComboTransporte(documento.ClienteFacturar);
                frmpre2.cmdRegresar.Visible = true;
                //if (dataGridView1.CurrentRow.Cells["dcli_estado"].Value.ToString().Equals("2"))
                //{
                //    frmpre2.cmdRegresar.Visible = true;
                //    frmpre2.cmdConfirmar.Visible = false;
                //    frmpre2.txtCodVendedor.Enabled = true;
                //    frmpre2.cmdBuscarVendedor.Enabled = true;
                //    frmpre2.txtNombreVendedor.Enabled = true;
                //    frmpre2.button2.Enabled = true;
                    

                //    frmpre2.cboCondicionPago.Enabled = false;
                //    frmpre2.cboTransporte.Enabled = false;
                //    frmpre2.txtDirEntrega.Enabled = false;
                //}
                //else {
                //    frmpre2.cmdRegresar.Visible = false;
                //    frmpre2.cmdConfirmar.Visible = true;

                //    frmpre2.txtCodVendedor.Enabled = false;
                //    frmpre2.cmdBuscarVendedor.Enabled = false;
                //    frmpre2.txtNombreVendedor.Enabled = false;
                //    frmpre2.button2.Enabled = false;
                    

                //    frmpre2.cboCondicionPago.Enabled = true;
                //    frmpre2.cboTransporte.Enabled = true;
                //    frmpre2.txtDirEntrega.Enabled = true;
                //}

                
                this.frmpre2.asignarCondicionPago(documento.ClienteFacturar.CondicionPago);
            }
        }

        private void cmdVer_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                pictureBox1.Visible = true;
                frmPre1 = new frmPrefactura();
                frmPre1.MdiParent = this.MdiParent;
                tipoOperacion = "ver";
                backgroundWorker1.RunWorkerAsync(frmPre1);
            }
            else
            {
                MessageBox.Show("Debe Existir una Prefactura Para Mostrar!!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            using (Font myFont = new Font("Arial", 24))
            {
                e.Graphics.DrawString("Cargando", myFont, Brushes.Black, new Point(2, 2));
            }
        }

        private void cmdImprimir_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                pictureBox1.Visible = true;
                frmPre1 = new frmPrefactura();
                frmPre1.MdiParent = this.MdiParent;
                tipoOperacion = "imprimirAlma";
                backgroundWorker1.RunWorkerAsync(frmPre1);
            }
            else
            {
                MessageBox.Show("Debe existir una prefactura Para imprimir", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                pictureBox1.Visible = true;
                frmPre1 = new frmPrefactura();
                frmPre1.MdiParent = this.MdiParent;
                tipoOperacion = "imprimirVen";
                backgroundWorker1.RunWorkerAsync(frmPre1);
            }
            else
            {
                MessageBox.Show("Debe existir una prefactura Para imprimir", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                    sentencia = "cli_nombre LIKE '" + filtro + "%'";
                    dv.RowFilter = sentencia;
                    if (dv.Count == 0)
                    {
                        sentencia = "dcli_numfis LIKE '" + filtro + "%'";
                        dv.RowFilter = sentencia;
                    }
                }
                if (dv.Count > 0)
                {
                    dataGridView1.DataSource = dv;
                    dataGridView1.Update();
                }
                else
                {
                    temp = new DataTable();
                    if (this.Name.Equals("Pedido"))
                    {
                        temp = prefact1.prefacturaBuscado(txtBuscar.Text, "PRE","1");
                        temp.Merge(prefact1.prefacturaBuscado(txtBuscar.Text, "PRE","2"));
                    }
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
            dtemp = prefact1.prefacturaBuscado2(dateTimePicker1.Value.Year + "-" + dateTimePicker1.Value.Month + "-" + dateTimePicker1.Value.Day,
                dateTimePicker2.Value.Year + "-" + dateTimePicker2.Value.Month + "-" + dateTimePicker2.Value.Day, "PRE","1");
            dtemp.Merge(prefact1.prefacturaBuscado2(dateTimePicker1.Value.Year + "-" + dateTimePicker1.Value.Month + "-" + dateTimePicker1.Value.Day,
                dateTimePicker2.Value.Year + "-" + dateTimePicker2.Value.Month + "-" + dateTimePicker2.Value.Day, "PRE", "2"));

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
            dtPrefact = null;
            dataGridView1.DataSource = null;
            txtBuscar.Clear();
            chkFecha.Checked = false;
            dtPrefact = prefact1.lbxOPRE("PRE", "1");
            dtPrefact.Merge(prefact1.lbxOPRE("PRE", "2"));
            dv = new DataView(dtPrefact);
            this.dataGridView1.DataSource = dtPrefact;
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

        private void cmdBultos_Click(object sender, EventArgs e)
        {
            frmBultos = new lbxBultos();
            frmBultos.MdiParent = this.MdiParent;
            frmBultos.Show();
            frmBultos.cargarPrefactura(dataGridView1.CurrentRow.Cells["dcli_numero"].Value.ToString());
            frmBultos.asignarDatos();
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

    }
}
