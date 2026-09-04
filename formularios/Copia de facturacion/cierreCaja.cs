using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using lebrun.clases.complementos;
using System.Globalization;
using lebrun.formularios.usuarios;
using System.Runtime.InteropServices;
using lebrun.clasesData;
using lebrun.clases.facturacion;
using lebrun.Reportes.facturacion;
using lebrun.Reportes.facturacion.rpt;
using lebrun.Reportes.MovContab;

namespace lebrun.formularios.facturacion
{
    public partial class cierreCaja : Form
    {
        Caja cajaCerrar;
        lbxUsuarios lbxUsu;
        UsuarioSistema usuSystem;
        SerieFiscal seriFisCerrar;
        frmReporteCierreCaja reporteCierre;
        Principal referenciaPrincipal;
        bool proceso;
        bool proceso2;
        frmConteMovContab frm;
        string tempt;

        public cierreCaja()
        {
            InitializeComponent();
        }

        private void cuadreCaja_Load(object sender, EventArgs e)
        {   
            cajaCerrar = new Caja();
            usuSystem = new UsuarioSistema(2);
            referenciaPrincipal = (Principal)this.MdiParent;
            seriFisCerrar = new SerieFiscal(referenciaPrincipal.usuarioActual.IpPc);
            DataTable tablasCaja = cajaCerrar.cajas();
            comboBox1.DataSource = tablasCaja;
            comboBox1.DisplayMember = "usu_caja";
            comboBox1.ValueMember = "usu_caja";
            comboBox1.Focus();
            proceso = false;
            proceso2 = false;
            seriFisCerrar.setCierreCaja(1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tempt = comboBox1.Text;
            if (seriFisCerrar.isActivate(comboBox1.Text, usuSystem.Id))
            {
                MessageBox.Show("No se puede realizar Cierre de Caja, ya que la Misma esta Activa", "Aviso ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                DialogResult respuesta;
                respuesta = MessageBox.Show("¿Desea cerrar la caja Nº " + comboBox1.Text + "", "Cuandre de Caja", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (respuesta == System.Windows.Forms.DialogResult.Yes)
                {
                    if ((usuSystem.Id == null) || (usuSystem.Id == ""))
                    {
                        txtUsuario_KeyPress(new object(), new KeyPressEventArgs((char)(Keys.Enter)));
                    }
                    if ((usuSystem.Id == null) || (usuSystem.Id == ""))
                    {
                        MessageBox.Show("Falta el codigo del Usuario por favor repita el proceso nuevamente", "Aviso ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    if (cajaCerrar.facturasCierre(comboBox1.Text, usuSystem.Id))
                    {
                        cajaCerrar.facturasSinImprimir(comboBox1.Text, usuSystem.Id);
                        proceso = true;
                        cajaCerrar.IdCaja = comboBox1.Text;
                        cajaCerrar.datosPrincipal(comboBox1.Text, usuSystem.Id);
                        txtFacturas.Text = cajaCerrar.TotalFacturas.ToString("n");
                        txtDevolucion.Text = cajaCerrar.TotalDevolucion.ToString("n");
                        txtTotalCuadre.Text = cajaCerrar.TotalCuadre.ToString("n");
                        txtEfectivo.Text = cajaCerrar.TotalEfectivo.ToString("n");
                        txtCheque.Text = cajaCerrar.TotalCheque.ToString("n");
                        txtDebito.Text = cajaCerrar.TotalTDebito.ToString("n");
                        txtCredito.Text = cajaCerrar.TotalTCredito.ToString("n");
                        txtIva.Text = cajaCerrar.TotalIva.ToString("n");
                        txtTotal.Text = cajaCerrar.Total1.ToString("n");

                        reporteCierre = frmReporteCierreCaja.DefInstance;
                        reporteCierre.MdiParent = this.MdiParent;
                        pictureBox1.Visible = true;
                        this.Enabled = false;
                        backgroundWorker1.RunWorkerAsync(reporteCierre);
                        cajaCerrar.updateCierreDiario(referenciaPrincipal.empresaActual.BaseDatosActual);
                        return;
                    }
                    else
                    {
                        MessageBox.Show("No existen Facturas para Cerrar en esta Caja", "Aviso ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                else
                {
                    if ((usuSystem.Id == null) || (usuSystem.Id == ""))
                    {
                        txtUsuario_KeyPress(new object(), new KeyPressEventArgs((char)(Keys.Enter)));
                    }
                    if ((usuSystem.Id == null) || (usuSystem.Id == ""))
                    {
                        MessageBox.Show("Falta el codigo del Usuario por favor repita el proceso nuevamente", "Aviso ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    if (cajaCerrar.facturasCierre(comboBox1.Text, usuSystem.Id))
                    {
                        cajaCerrar.facturasSinImprimir(comboBox1.Text, usuSystem.Id);
                        cajaCerrar.IdCaja = comboBox1.Text;
                        cajaCerrar.datosPrincipal(comboBox1.Text, usuSystem.Id);
                        txtFacturas.Text = cajaCerrar.TotalFacturas.ToString("n");
                        txtDevolucion.Text = cajaCerrar.TotalDevolucion.ToString("n");
                        txtTotalCuadre.Text = cajaCerrar.TotalCuadre.ToString("n");
                        txtEfectivo.Text = cajaCerrar.TotalEfectivo.ToString("n");
                        txtCheque.Text = cajaCerrar.TotalCheque.ToString("n");
                        txtDebito.Text = cajaCerrar.TotalTDebito.ToString("n");
                        txtCredito.Text = cajaCerrar.TotalTCredito.ToString("n");
                        txtIva.Text = cajaCerrar.TotalIva.ToString("n");
                        txtTotal.Text = cajaCerrar.Total1.ToString("n");

                        reporteCierre = frmReporteCierreCaja.DefInstance;
                        reporteCierre.MdiParent = this.MdiParent;
                        this.Enabled = false;
                        backgroundWorker1.RunWorkerAsync(reporteCierre);
                        return;
                    }
                    else
                    {
                        MessageBox.Show("No existen Facturas para Cerrar en esta Caja", "Aviso ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
        }

        private void button4_Click(object sender, EventArgs e){}
        private void button3_Click(object sender, EventArgs e){}

        private void cmdSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdlbxBancos_Click(object sender, EventArgs e)
        {
            lbxUsu = lbxUsuarios.DefInstance;
            lbxUsu.Show();
            lbxUsu.actualizarReferenciaCierre(this);
            cierreCaja.SetParent(lbxUsu.Handle, this.MdiParent.Handle);
        }
        private void button2_Click(object sender, EventArgs e) {
            if ((proceso) && (proceso2)) {
                MessageBox.Show("El cierre de caja ya se realizo no se puede Cancelar", "Aviso ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                proceso = false;
                proceso2 = false;
            }

            comboBox1.SelectedIndex = 0;
            txtUsuario.Clear();
            lblNombre.Text = "";
            usuSystem.Id = "";
            usuSystem.Nombre = "";

            txtFacturas.Text = "0,00";
            txtDevolucion.Text = "0,00";
            txtTotalCuadre.Text = "0,00";
            txtEfectivo.Text = "0,00";
            txtCheque.Text = "0,00";
            txtDebito.Text = "0,00";
            txtCredito.Text = "0,00";
            txtIva.Text = "0,00";
            txtTotal.Text = "0,00";
        }

        [DllImport("user32.DLL")]
        public static extern void SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
                SendKeys.Send("{TAB}");
        }

        private void txtUsuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)(Keys.Enter)) && txtUsuario.Text != "") {
                if (usuSystem.existeUsuario2(txtUsuario.Text))
                {
                    usuSystem.Id = txtUsuario.Text;
                    usuSystem.Nombre = usuSystem.nombreCodUsu(usuSystem.Id);
                    lblNombre.Text = usuSystem.Nombre;
                }
                else {
                    MessageBox.Show("El Usuario no existe", "Aviso ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtUsuario.Focus();
                }
            }
        }

        public void despuesDeLbx() {
            usuSystem.Id = txtUsuario.Text;
            usuSystem.Nombre=lblNombre.Text;
        }

       private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {   
            if ((reporteCierre != null) && (e.Argument.GetType() ==typeof(frmReporteCierreCaja)))
            {
               System.ComponentModel.BackgroundWorker worker;
               worker = (System.ComponentModel.BackgroundWorker)sender;
               frmReporteCierreCaja temp = (frmReporteCierreCaja)e.Argument;
               // usuSystem.nombreCodUsu(usuSystem.Id);
                referenciaPrincipal.usuarioActual.Nombre= referenciaPrincipal.usuarioActual.nombreCodUsu(referenciaPrincipal.usuarioActual.Id,2);
                reporteCierre = temp.armarReporte(cajaCerrar, tempt, usuSystem.Id, usuSystem.Nombre,
                   referenciaPrincipal.usuarioActual.Nombre, cajaCerrar.FacturasNoImpresas);
               return;
            }

            if ((proceso) && (e.Argument.GetType() ==typeof(Caja)))
            {   
                System.ComponentModel.BackgroundWorker worker;
                worker = (System.ComponentModel.BackgroundWorker)sender;
                Caja cTemp = (Caja)e.Argument;
                cajaCerrar = cTemp.cierreCaja(referenciaPrincipal.empresaActual.BaseDatosActual, referenciaPrincipal.usuarioActual.Id);
                bool resultado = cTemp.existeCierreCaja(referenciaPrincipal.empresaActual.BaseDatosActual);
                if (resultado)
                {
                    cajaCerrar.cleanDoccli2(usuSystem.Id);
                    cajaCerrar.cerrarFacturas(usuSystem.Id);
                    this.proceso2 = true;
                }
                this.proceso2 = true;
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
           
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if ((reporteCierre != null) && !(proceso)){
                reporteCierre.Show();
                this.pictureBox1.Visible = false;
                this.Enabled = true;
                //seriFisCerrar.setCierreCaja(0);
            }

            if( (proceso) && !(this.proceso2))
            {   
                backgroundWorker1.RunWorkerAsync(cajaCerrar);
            }

            if ((this.proceso) && (this.proceso2)) {
                this.pictureBox1.Visible = false;
                reporteCierre.Show();
                frm = new frmConteMovContab(cajaCerrar.MovimientoS.datosComprobante(cajaCerrar.Correlativos.Rows[0]["pc_CorrCompAut+1"].ToString(), referenciaPrincipal.empresaActual.BaseDatosActual));
                frm.Show();
                proceso2 = false;
                proceso = false;
                this.Enabled = true;
                //seriFisCerrar.setCierreCaja(0);
                return;
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {   
            using (Font myFont = new Font("Arial", 24))
            {
                e.Graphics.DrawString("Cerrando Caja", myFont, Brushes.Black, new Point(2, 2));
            }
        }

        private void cierreCaja_FormClosing(object sender, FormClosingEventArgs e)
        {
            seriFisCerrar.setCierreCaja(0);
        }
    }
}