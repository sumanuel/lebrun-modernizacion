using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using lebrun.clases.importaciones;
using lebrun.clases.complementos;
using lebrun.clasesData;
using lebrun.clases.tesoreria;
using lebrun.Reportes.tesoreria.rpt;
using lebrun.Reportes.tesoreria.dataset;

namespace lebrun.Reportes
{
    public partial class frmReporte : Form
    {
        private string docu;
        private string sufi;
        private string codProvee;
        private  int estado;
        private string reporte;
        private CrystalReport1 rptImportacion;
        private Importacion importacion;
        private pagare pagare;
        private reciboDeingreso recibo;
        private Compania compania;
        private rptPagare reportePagare;
        private rptReciboIngreso reporteRecibo;
        private string comprobante;
        private DataTable tabla;
        private string cliente;

        public frmReporte()
        {
            InitializeComponent();
            
        }

        public frmReporte(string doc, string suf, string provee, int estatus, string rpt)
        {
            InitializeComponent();
            docu = doc;
            sufi = suf;
            codProvee = provee;
            estado = estatus;
            reporte = rpt;
            
        }

        public frmReporte(string rpt, string comprob)
        {
            InitializeComponent();
            reporte = rpt;
            comprobante = comprob;
           
        }

        public frmReporte(string rpt, string comprob, string clien)
        {
            InitializeComponent();
            reporte = rpt;
            comprobante = comprob;
            cliente = clien;
        }

        //private void imprimirReporte()
        //{

        //    if (reporte.Equals("importacion"))
        //    {
        //        importacion = new Importacion();
        //        rptImportacion = new CrystalReport1();
        //        rptImportacion.SetDataSource(importacion.imprimirReporte(docu, sufi, codProvee, estado));
        //        rptImportacion.SetParameterValue("NombreCompania", compania.Nombre);
        //        rptImportacion.SetParameterValue("rifCompania", compania.Rif);
        //        this.crystalReportViewer1.ReportSource = rptImportacion;
        //        crystalReportViewer1.Visible = true;

        //    }else if (reporte.Equals("Pagare"))
        //    {
        //        pagare = new pagare();
        //        reportePagare = new rptPagare();
        //        reportePagare.SetDataSource(pagare.datosComprobante(comprobante,compania.BaseDatosActual));
        //        reportePagare.SetParameterValue("NombreCompania", compania.Nombre);
        //        //reportePagare.SetParameterValue("rifCompania", compania.Rif);
        //        this.crystalReportViewer1.ReportSource = reportePagare;
        //        crystalReportViewer1.Visible = true;
            
        //    }
        //    else
        //    {
        //        this.Close();

        //    }
        //}

        private void imprimirReporte()
        {

            if (reporte.Equals("importacion"))
            {
                importacion = new Importacion();
                rptImportacion = new CrystalReport1();
                rptImportacion.SetDataSource(importacion.imprimirReporte(docu, sufi, codProvee, estado));
                rptImportacion.SetParameterValue("NombreCompania", compania.Nombre);
                rptImportacion.SetParameterValue("rifCompania", compania.Rif);
                this.crystalReportViewer1.ReportSource = rptImportacion;
                crystalReportViewer1.Visible = true;

            }
            else if (reporte.Equals("Pagare"))
            {
                pagare = new pagare();
                reportePagare = new rptPagare();
                reportePagare.SetDataSource(pagare.datosComprobante(comprobante, compania.BaseDatosActual));
                reportePagare.SetParameterValue("NombreCompania", compania.Nombre);
                //reportePagare.SetParameterValue("rifCompania", compania.Rif);
                this.crystalReportViewer1.ReportSource = reportePagare;
                crystalReportViewer1.Visible = true;

            }
            else if (reporte.Equals("MovContaRecibo"))
            {
                recibo = new reciboDeingreso();
                reportePagare = new rptPagare();
                tabla = recibo.datosComprobante(comprobante, compania.BaseDatosActual, cliente);

                if (tabla.Rows.Count > 0)
                {
                    reportePagare.SetDataSource(tabla);
                    reportePagare.SetParameterValue("NombreCompania", compania.Nombre);
                    //reportePagare.SetParameterValue("rifCompania", compania.Rif);
                    this.crystalReportViewer1.ReportSource = reportePagare;
                    crystalReportViewer1.Visible = true;
                }
                else
                {

                    MessageBox.Show("No existen datos para generar este reporte", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
            else if (reporte.Equals("ReciboIngreso"))
            {
                recibo = new reciboDeingreso();
                reporteRecibo = new rptReciboIngreso();
                tabla = recibo.rptReciboCabecera(comprobante);
                tabla.Merge(recibo.rptReciboFormaPago(comprobante));
                tabla.Merge(recibo.rptReciboDocu(comprobante));


                reporteRecibo.SetDataSource(tabla);
                reporteRecibo.SetParameterValue("NombreCompania", compania.Nombre);
                reporteRecibo.SetParameterValue("rifCompania", compania.Rif);
                this.crystalReportViewer1.ReportSource = reporteRecibo;
                crystalReportViewer1.Visible = true;

            }
            else
            {
                this.Close();

            }
        }

        private void frmReporte_Load(object sender, EventArgs e)
        {
            compania = new Compania();
            compania.obtenerCodigoCompaniaActual();
            imprimirReporte();

        }


    }
}
