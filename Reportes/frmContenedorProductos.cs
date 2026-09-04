using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using lebrun.clases.importaciones;
using lebrun.clasesData;
using lebrun.clases.Reportes;


namespace lebrun.Reportes
{
    public partial class frmContenedorProductos : Form
    {
        private string docu;
        private string sufi;
        private string codProvee;
        private int estado;
        private string reporte;
        private CrystalReport1 rptImportacion;
        private rptProductoLista rptProductoLista;
        private Importacion importacion;
        private Compania compania;
        private ReportesProductos reporteProducto; 

        public frmContenedorProductos()
        {
            InitializeComponent();
        }

        public frmContenedorProductos(string doc, string suf, string provee, int estatus, string rpt)
        {
            InitializeComponent();
            docu = doc;
            sufi = suf;
            codProvee = provee;
            estado = estatus;
            reporte = rpt;
            importacion = new Importacion();

        }

        public frmContenedorProductos(string rpt)  //para el listado Haydee
        {
            InitializeComponent(); //faltaba esto
            reporte = rpt;
            reporteProducto = new ReportesProductos();
        }
        private void imprimirReporte()
        {

            if (reporte.Equals("importacion"))
            {
                rptImportacion = new CrystalReport1();
                rptImportacion.SetDataSource(importacion.imprimirReporte(docu, sufi, codProvee, estado));
                rptImportacion.SetParameterValue("NombreCompania", compania.Nombre);
                rptImportacion.SetParameterValue("rifCompania", compania.Rif);
                this.crystalReportViewer1.ReportSource = rptImportacion;
                crystalReportViewer1.Visible = true;

            }
            else if (reporte.Equals("ListadoProductos")) //para el listado Haydee
            {
                rptProductoLista = new rptProductoLista();
                rptProductoLista.SetDataSource(reporteProducto.ListadoProductos());
                this.crystalReportViewer1.ReportSource = rptProductoLista;
                crystalReportViewer1.Visible = true;
            }
            else
            {
                this.Close();

            }
        }

        private void frmContenedorProductos_Load(object sender, EventArgs e)
        {
            compania = new Compania();
            compania.obtenerCodigoCompaniaActual();
            imprimirReporte();


        }








    }
}
