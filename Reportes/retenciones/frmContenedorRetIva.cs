using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using lebrun.Reportes.retenciones.rpt;
using lebrun.clasesData;
using lebrun.clases.compras;


namespace lebrun.Reportes.retenciones
{
    public partial class frmContenedorRetIva : Form
    {

        private rptRetencionIva reporteRetencion;
        private Compania compania;
        private retencionesIva retIva;
        private DataTable tabla;

        public frmContenedorRetIva()
        {
            InitializeComponent();
            retIva = new retencionesIva();
            compania = new Compania();
            reporteRetencion = new rptRetencionIva();

        }

        public frmContenedorRetIva(DataTable tab)
        {
            InitializeComponent();
            retIva = new retencionesIva();
            compania = new Compania();
            reporteRetencion = new rptRetencionIva();
            tabla = new DataTable();
            tabla = tab;
        }


        private void frmContenedorRetIva_Load(object sender, EventArgs e)
        {
            
            compania.obtenerCodigoCompaniaActual();
            armarReporte();
        }

        private void armarReporte()
        {
            reporteRetencion.SetDataSource(tabla);
            reporteRetencion.SetParameterValue("NombreCompania", compania.Nombre);
            reporteRetencion.SetParameterValue("RifCompania", compania.Rif);
            reporteRetencion.SetParameterValue("DireccionCompania", compania.Direccion);
            this.crystalReportViewer1.ReportSource = reporteRetencion;
            crystalReportViewer1.Visible = true;
        }
    }
}
