using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using lebrun.clases.contabilidad;

namespace lebrun.formularios.contabilidad
{
    public partial class frmComprobanteCierre : Form
    {
        ComprobanteCierre cierrePrueba;
        private Principal referenciaPrincipal;

        public frmComprobanteCierre()
        {
            InitializeComponent();
        }

        private void frmComprobanteCierre_Load(object sender, EventArgs e)
        {
            referenciaPrincipal = (Principal)this.MdiParent;
            cierrePrueba = new ComprobanteCierre(referenciaPrincipal.empresaActual.BaseDatosActual);
            this.statusPasos(referenciaPrincipal.empresaActual.BaseDatosActual);
        }

        private void cmdAceptarComprobante_Click(object sender, EventArgs e)
        {   
            /*if(!cierrePrueba.existeComprobantesRevision(referenciaPrincipal.empresaActual.BaseDatosActual,
                cierrePrueba.fechaInicioCierre(referenciaPrincipal.empresaActual.BaseDatosActual),
                "" + (dateTimePicker1.Value.Year + "-" + dateTimePicker1.Value.Month + "-" + dateTimePicker1.Value.Day))){*/
            
                cierrePrueba.balanceCierre(3, "" + (dateTimePicker1.Value.Year + "-" + dateTimePicker1.Value.Month + "-" + dateTimePicker1.Value.Day),
                                                        referenciaPrincipal.empresaActual.BaseDatosActual, referenciaPrincipal.usuarioActual.Id, referenciaPrincipal.empresaActual.Codigo,
                                                        ""+dateTimePicker1.Value.Year);
                
                if((cierrePrueba.updateStatusCierreContable(referenciaPrincipal.empresaActual.BaseDatosActual,"4"))){
                    MessageBox.Show("Comprobante Cierre generado Exitosamente!!", "Exito!! ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    MessageBox.Show("Debe Imprimir el Folio de este Comprobante!!", "Aviso!! ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    panel1.Visible = false;
                    this.statusPasos(referenciaPrincipal.empresaActual.BaseDatosActual);
                }else{
                    MessageBox.Show("Ha ocurrido un Error al actualizar Status del comprobante, por favor Llamar al departamento de Sistemas!!!", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    panel1.Visible = false;
                }
        }

        private void cmdSalirCompro_Click(object sender, EventArgs e)
        {
            this.panel1.Visible = false;
        }

        public void statusPasos(string idDataBase) {
            
            string status2 = cierrePrueba.statusCierreContable(idDataBase);
            switch(status2){
                case("1"):
                    this.b1.Visible = true;
                    break;
                case("2"):
                    this.b1.Visible = true;
                    this.b2.Visible = true;
                    break;
                case ("3"):
                    this.b1.Visible = true;
                    break;
                case ("4"):
                    this.b1.Visible = true;
                    this.b2.Visible = true;
                    this.b3.Visible = true;
                    break;
            }
            //cierrePrueba.actualizarSaldoActualComprobanteCierre(idDataBase);
        }

        private void cmdSiguiente_Click(object sender, EventArgs e)
        {
            string status2 = cierrePrueba.statusCierreContable(referenciaPrincipal.toolStripComboBox1.ComboBox.SelectedValue.ToString());

            switch (status2) { 
                case("1"):
                    panel1.Visible = true;
                    break;
                case("0"):
                    MessageBox.Show("Necesita generar El Balance de Comprobacion !!", "Aviso!! ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    break;
                case("4"):
                    this.status2();
                    MessageBox.Show("Este Proceso Tardara unos Minutos Por favor Espere!!", "Aviso!! ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //cierrePrueba.updateSaldoAuxiliaresContables("" + (dateTimePicker1.Value.Year + "-" + dateTimePicker1.Value.Month + "-" + dateTimePicker1.Value.Day),
                    //referenciaPrincipal.empresaActual.BaseDatosActual);
                    cierrePrueba.updateSaldoAuxiliaresContables("" + (dateTimePicker1.Value.Year + "-" + dateTimePicker1.Value.Month + "-" + dateTimePicker1.Value.Day),
                    referenciaPrincipal.toolStripComboBox1.ComboBox.SelectedValue.ToString());
                    cierrePrueba.actualizarSaldoActualComprobanteCierre(referenciaPrincipal.toolStripComboBox1.ComboBox.SelectedValue.ToString());
                    cierrePrueba.actualizarDiferenciaCuenta(referenciaPrincipal.toolStripComboBox1.ComboBox.SelectedValue.ToString());
                    cierrePrueba.updateStatusCierreContable(referenciaPrincipal.toolStripComboBox1.ComboBox.SelectedValue.ToString(), "0");
                    cierrePrueba.borrarTemporalBalanceCierre(referenciaPrincipal.toolStripComboBox1.ComboBox.SelectedValue.ToString());
                    cierrePrueba.borrarTablas(referenciaPrincipal.toolStripComboBox1.ComboBox.SelectedValue.ToString(), "" + (dateTimePicker1.Value.Year + "-" + dateTimePicker1.Value.Month + "-" + dateTimePicker1.Value.Day),
                        referenciaPrincipal.toolStripComboBox1.ComboBox.SelectedValue.ToString());
                    cierrePrueba.updateUltCierreAnual(referenciaPrincipal.toolStripComboBox1.ComboBox.SelectedValue.ToString(), "" + (dateTimePicker1.Value.Year + "-" + dateTimePicker1.Value.Month + "-" + dateTimePicker1.Value.Day));
                    statusFull();
                    MessageBox.Show("Actualizado Saldo!!", "Exito!! ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    MessageBox.Show("Cierre Efectuado Con Exito!!", "Exito!! ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
            }

        }

        private void cmdSalir_Click(object sender, EventArgs e)
        {
            //Application.Exit();
            this.Close();
        }

        private void statusFull() {
            this.b1.Visible = true;
            this.b2.Visible = true;
            this.b3.Visible = true;
            this.b4.Visible = true;
        }

        private void status2() {
            this.b1.Visible = true;
            this.b2.Visible = true;
        }

        
    }
}

