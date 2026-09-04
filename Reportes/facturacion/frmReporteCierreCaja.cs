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

            this.crystalReportViewer1.ReportSource=reporteCrystal;
        return this;
        }
     
    }
}
