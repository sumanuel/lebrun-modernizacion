using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using lebrun.clasesData;
using lebrun.clases.contabilidad;
using lebrun.Reportes.MovContab.dataset;
using lebrun.Reportes.MovContab.rpt;

namespace lebrun.Reportes.MovContab
{
    public partial class frmConteMovContab : Form
    {

        private DataTable tabla;
        private Compania compania;
        private rptMovContab rtpContabilidad;
        private movimientoContable movContab;
        private rptMovContaCompra rptMovContabCompra;

        private string comprobante;
        private string cliente;
        private string reporte;
        private string caso = "Normal";
        private string reimp = "";

        public frmConteMovContab()
        {
            InitializeComponent();
        }

        public frmConteMovContab(string rpt, string comprob, string clien)
        {
            InitializeComponent();
            tabla = new DataTable();
            reporte = rpt;
            comprobante = comprob;
            cliente = clien;
        }

        public frmConteMovContab(string rpt, string comprob)
        {
            InitializeComponent();
            tabla = new DataTable();
            reporte = rpt;
            comprobante = comprob;
            caso = "Reimprimir";
            reimp = "Reimpresion";

        }

        public frmConteMovContab(DataTable dtReporte)
        {
            InitializeComponent();
            tabla = new DataTable();
            tabla = dtReporte;
            caso = "Nuevo";

        }

        public frmConteMovContab(DataTable dtReporte, string opcion)
        {
            InitializeComponent();
            tabla = new DataTable();
            tabla = dtReporte;
            caso = opcion;

        }

        private void imprimirReporte()
        {

            if (reporte.Equals("MovContaRecibo"))
            {

                rtpContabilidad = new rptMovContab();
                movContab = new movimientoContable();

                tabla.Clear();
                tabla.Reset();
               
                tabla = movContab.datosComprobante(comprobante, compania.BaseDatosActual, cliente);
                
                if (tabla.Rows.Count > 0)
                {
                    rtpContabilidad.SetDataSource(tabla);
                    rtpContabilidad.SetParameterValue("NombreCompania", compania.Nombre);
                    rtpContabilidad.SetParameterValue("reimpresion", reimp);
                    //reportePagare.SetParameterValue("rifCompania", compania.Rif);
                    this.crystalReportViewer1.ReportSource = rtpContabilidad;
                    crystalReportViewer1.Visible = true;
                }
                else
                {

                    MessageBox.Show("No existen datos para generar este reporte", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
            else
            {
                this.Close();

            }
        }


        private void reImprimirReporte()
        {

            if (reporte.Equals("MovContaRecibo"))
            {

                rtpContabilidad = new rptMovContab();
                movContab = new movimientoContable();

                tabla.Clear();
                tabla.Reset();

                tabla = movContab.ReimprimirComprobante(comprobante, compania.BaseDatosActual);
                if (tabla.Rows.Count > 0)
                {
                    rtpContabilidad.SetDataSource(tabla);
                    rtpContabilidad.SetParameterValue("NombreCompania", compania.Nombre);
                    rtpContabilidad.SetParameterValue("reimpresion", reimp);
                    //reportePagare.SetParameterValue("rifCompania", compania.Rif);
                    this.crystalReportViewer1.ReportSource = rtpContabilidad;
                    crystalReportViewer1.Visible = true;
                }
                else
                {

                    MessageBox.Show("No existen datos para generar este reporte", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
            else
            {
                this.Close();

            }
        }


        private void nuevoReporte()
        {
            if (tabla.Rows.Count > 0)
            {
                rtpContabilidad = new rptMovContab();

                rtpContabilidad.SetDataSource(tabla);
                rtpContabilidad.SetParameterValue("NombreCompania", compania.Nombre);
                rtpContabilidad.SetParameterValue("reimpresion", reimp);
                this.crystalReportViewer1.ReportSource = rtpContabilidad;
                crystalReportViewer1.Visible = true;
            }
            else
            {
                MessageBox.Show("No existen datos para generar este reporte", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }

        private void reporteCompra()
        {
            if (tabla.Rows.Count > 0)
            {

                rptMovContabCompra = new rptMovContaCompra();

                rptMovContabCompra.SetDataSource(tabla);
                rptMovContabCompra.SetParameterValue("NombreCompania", compania.Nombre);
                rptMovContabCompra.SetParameterValue("RifCompania", compania.Rif);
                this.crystalReportViewer1.ReportSource = rptMovContabCompra;
                crystalReportViewer1.Visible = true;
            }
            else
            {
                MessageBox.Show("No existen datos para generar este reporte", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }

        private void frmConteMovContab_Load(object sender, EventArgs e)
        {
            
            compania = new Compania();
            compania.obtenerCodigoCompaniaActual();

            if (caso == "Normal")
            {
                imprimirReporte();
            }
            if (caso == "Reimprimir")
            {
                reImprimirReporte();
            }
            if (caso == "Nuevo")
            {
                nuevoReporte();
            }

            if (caso == "Servicios")
            {
                reporteCompra();
            }
        }


    }
}
