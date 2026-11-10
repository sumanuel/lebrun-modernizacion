using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using lebrun.clases.complementos;
using lebrun.Reportes.facturacion.rpt;
using lebrun.clasesData;
using CrystalDecisions.CrystalReports.Engine;
using Ras = CrystalDecisions.ReportAppServer.ReportDefModel;
using CrystalDecisions.ReportAppServer.Controllers;

namespace lebrun.Reportes.facturacion
{
    public partial class frmReporteCierreCaja : Form
    {
        rptCierreCaja reporteCrystal;
        private static frmReporteCierreCaja m_FormDefInstance;

        public frmReporteCierreCaja()
        {
            InitializeComponent();
        }
        public static frmReporteCierreCaja DefInstance
        {
            get
            {
                if (m_FormDefInstance == null || m_FormDefInstance.IsDisposed)
                    m_FormDefInstance = new frmReporteCierreCaja();
                return m_FormDefInstance;
            }
            set
            {
                m_FormDefInstance = value;
            }
        }
        private void frmReporteCierreCaja_Load(object sender, EventArgs e){}


        // El Resumen General del reporte trae fijas las filas de Efectivo, Cheque y Tarjetas. Las formas de pago adicionales (Divisa $, Divisa Euro,
        // Cachea, ...) se agregan aqui como filas nuevas, solo las que tuvieron movimiento: ocupan el lugar de "Total Otros Pagos" (que no se
        // usaba) y "Total Cuadre Caja" baja lo que haga falta. Si no se pueden agregar, el reporte sale como siempre y el error queda registrado.
        private static void AgregarFilasFormasPago(ReportDocument reporte, List<TotalFormaPago> filas)
        {
            if (filas == null || filas.Count == 0)
            {
                return;
            }
            try
            {
                const int paso = 360;
                const int primeraFila = 3480;
                var definicion = reporte.ReportClientDocument.ReportDefController;
                ReportObjectController controlador = definicion.ReportObjectController;
                Ras.Section seccion = definicion.ReportDefinition.ReportFooterArea.Sections[0];
                int desplazar = (filas.Count - 1) * paso;

                Ras.ReportObject modelo = null;
                List<Ras.ReportObject> sobrantes = new List<Ras.ReportObject>();
                List<Ras.ReportObject> bajan = new List<Ras.ReportObject>();
                for (int i = 0; i < seccion.ReportObjects.Count; i++)
                {
                    Ras.ReportObject o = (Ras.ReportObject)seccion.ReportObjects[i];
                    if (o.Name == "Text2")
                    {
                        modelo = o;
                    }
                    if (o.Name == "Text13" || o.Name == "totalCredito1")
                    {
                        sobrantes.Add(o);
                    }
                    else if (o.Top > primeraFila && o.Left >= 3000 && o.Top < 9000)
                    {
                        bajan.Add(o);
                    }
                }
                if (modelo == null)
                {
                    throw new InvalidOperationException("El reporte de cierre de caja no tiene el objeto base para las filas nuevas.");
                }
                foreach (Ras.ReportObject o in sobrantes)
                {
                    Ras.ReportObject copia = (Ras.ReportObject)o.Clone(true);
                    copia.Format.EnableSuppress = true;
                    controlador.Modify(o, copia);
                }
                foreach (Ras.ReportObject o in bajan)
                {
                    Ras.ReportObject copia = (Ras.ReportObject)o.Clone(true);
                    copia.Top = o.Top + desplazar;
                    controlador.Modify(o, copia);
                }
                for (int k = 0; k < filas.Count; k++)
                {
                    AgregarTexto(controlador, seccion, modelo, "formaPago_lbl" + k, 3000, primeraFila + k * paso, 2040, filas[k].Descripcion + ":", Ras.CrAlignmentEnum.crAlignmentLeft, 10);
                    AgregarTexto(controlador, seccion, modelo, "formaPago_val" + k, 5400, primeraFila + k * paso, 2040, filas[k].Neto.ToString("n"), Ras.CrAlignmentEnum.crAlignmentRight, 8);
                }
            }
            catch (Exception ex)
            {
                RegistroErrores.Registrar(ex, "Reporte de cierre de caja: formas de pago adicionales");
            }
        }

        private static void AgregarTexto(ReportObjectController controlador, Ras.Section seccion, Ras.ReportObject modelo, string nombre, int izquierda, int arriba, int ancho,
                                         string texto, Ras.CrAlignmentEnum alineacion, decimal tamano)
        {
            Ras.TextObject t = (Ras.TextObject)modelo.Clone(true);
            t.Name = nombre;
            t.Left = izquierda;
            t.Top = arriba;
            t.Width = ancho;
            t.Height = 221;
            t.Format.HorizontalAlignment = alineacion;
            ((Ras.ParagraphTextElement)t.Paragraphs[0].ParagraphElements[0]).Text = texto;
            t.Paragraphs[0].Alignment = alineacion;
            t.FontColor.Font.Size = tamano;
            t.FontColor.Font.Bold = true;
            controlador.Add(t, seccion, -1);
        }

        public frmReporteCierreCaja armarReporte(Caja cajaCerrada, string numCaja, string cajera, string nombreC, string login, bool facNoImp)
        {
            reporteCrystal = new rptCierreCaja();
            reporteCrystal.SetDataSource(cajaCerrada.dtReporte);

            reporteCrystal.SetParameterValue("nombreCompania", ((Principal)this.MdiParent).empresaActual.Nombre);
            reporteCrystal.SetParameterValue("susbtotalPVIva", cajaCerrada.TotalIva.ToString("n"));
            reporteCrystal.SetParameterValue("subTotalPV", cajaCerrada.SubTotalPV.ToString("n"));
            reporteCrystal.SetParameterValue("totalEfectivo", cajaCerrada.TotalEfectivo.ToString("n"));
            reporteCrystal.SetParameterValue("totalCheque", cajaCerrada.TotalCheque .ToString("n"));
            reporteCrystal.SetParameterValue("totalTDebito", cajaCerrada.TotalTDebito.ToString("n"));
            reporteCrystal.SetParameterValue("totalTCredito", cajaCerrada.TotalTCredito.ToString("n"));
            reporteCrystal.SetParameterValue("totalOtrosP", "0,00");
            reporteCrystal.SetParameterValue("totalConEsp", "0,00");
            reporteCrystal.SetParameterValue("totalCuadreCaja", cajaCerrada.TotalCuadre.ToString("n"));
            reporteCrystal.SetParameterValue("numCaja", numCaja);
            reporteCrystal.SetParameterValue("cajera", cajera);
            reporteCrystal.SetParameterValue("nombreCajera", nombreC);
            reporteCrystal.SetParameterValue("login", login);
            reporteCrystal.SetParameterValue("facNoImp", facNoImp);
            AgregarFilasFormasPago(reporteCrystal, cajaCerrada.FormasAdicionales);

            this.crystalReportViewer1.ReportSource=reporteCrystal;
        return this;
        }
     
    }
}
