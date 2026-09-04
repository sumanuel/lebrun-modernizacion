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
    public partial class frmEstructuraCostos : Form
    {

        Balance balanceEstruturaCosto;
        frmVisorEstructura reporteHtml;
        Principal referenciaPrincipal;
        public frmEstructuraCostos()
        {
            InitializeComponent();
        }

        private void cmdSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void cmdAgregar_Click(object sender, EventArgs e)
        {
            Principal temp = (Principal)this.MdiParent;
            string fechaHasta = "" + dateTimePicker1.Value.Year.ToString() + "/" + dateTimePicker1.Value.Month.ToString().PadLeft(2, '0') + "/" + dateTimePicker1.Value.Day.ToString().PadLeft(2, '0');
            if ((balanceEstruturaCosto.isFechaUltCierreA(fechaHasta, "08")))
            {
                MessageBox.Show("La Fecha Hasta no puede ser igual a la fecha del cierre del ejercicio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {

                if (!(balanceEstruturaCosto.definitivaEstructura(fechaHasta)))
                {
                    balanceEstruturaCosto.dtConTotal(balanceEstruturaCosto.dtSaldoActualEC(fechaHasta));
                    DialogResult result = MessageBox.Show("¿Esta es la estructura de costos definitiva?", "Pregunta", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        balanceEstruturaCosto.guardarHistorico(fechaHasta, temp.usuarioActual.Id, 1);
                        balanceEstruturaCosto.modificarFechaParametros(fechaHasta);
                    }
                    else
                    {
                        balanceEstruturaCosto.guardarHistorico(fechaHasta, temp.usuarioActual.Id, 0);
                    }
                    balanceEstruturaCosto.setCostosOperativos(referenciaPrincipal.empresaActual.Codigo);

                    EstructuraDeCostosHtml a = new EstructuraDeCostosHtml(balanceEstruturaCosto.repoteEstructuraC(), temp.empresaActual.Nombre, ("" + dateTimePicker1.Value.Day.ToString().PadLeft(2, '0') + "/" + dateTimePicker1.Value.Month.ToString().PadLeft(2, '0') + "/" + dateTimePicker1.Value.Year), temp.empresaActual.Rif);
                    a.reporte();
                    reporteHtml = new frmVisorEstructura();
                    reporteHtml.MdiParent = this.MdiParent;
                    reporteHtml.Show();
                }
                else
                {
                    MessageBox.Show("Ya se encuentra una Estructura de Costos Definitiva", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void frmEstructuraCostos_Load(object sender, EventArgs e)
        {

            referenciaPrincipal = (Principal)this.MdiParent;
            string[] parametros;
            parametros = Environment.GetCommandLineArgs();
            if (parametros.Length >= 3)
            {
                if (parametros[3] != null)
                {
                    balanceEstruturaCosto = new Balance(parametros[3]);
                }
                else
                {
                    balanceEstruturaCosto = new Balance();
                }
            }
            else
            {
                referenciaPrincipal = (Principal)this.MdiParent;
                balanceEstruturaCosto = new Balance(referenciaPrincipal.empresaActual.Codigo);
            }
        }
    }
}
