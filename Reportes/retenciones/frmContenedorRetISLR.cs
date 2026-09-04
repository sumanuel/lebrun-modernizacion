using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using lebrun.Reportes;
using lebrun.clasesData;
using lebrun.clases.compras;
using lebrun.Reportes.retenciones.rpt;
namespace lebrun.Reportes.retenciones
{
    public partial class frmContenedorRetISLR : Form
    {

        private rptRetencionISLR reporteISLR;
        private Compania compania;
        private retencionISLR retISLR;
        private DataTable tabla;


        public frmContenedorRetISLR()
        {
            InitializeComponent();
            reporteISLR = new rptRetencionISLR();
            compania = new Compania();
            retISLR = new retencionISLR();
            
        }

        public frmContenedorRetISLR(DataTable tab)
        {
            InitializeComponent();
            reporteISLR = new rptRetencionISLR();
            compania = new Compania();
            retISLR = new retencionISLR();
            tabla = new DataTable();
            tabla = tab;
        }

        private void armarReporte()
        {
            reporteISLR.SetDataSource(tabla);
            reporteISLR.SetParameterValue("NombreCompania", compania.Nombre);
            reporteISLR.SetParameterValue("RifCompania", compania.Rif);
            reporteISLR.SetParameterValue("DireccionCompania", compania.Direccion);
            this.crystalReportViewer1.ReportSource = reporteISLR;
            crystalReportViewer1.Visible = true;
        }

        private void frmContenedorRetISLR_Load(object sender, EventArgs e)
        {
            compania.obtenerCodigoCompaniaActual();
            armarReporte();
        }
    }
}
