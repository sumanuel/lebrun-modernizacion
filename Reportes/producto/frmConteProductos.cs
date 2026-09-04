using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using lebrun.clasesData;
using lebrun.Reportes.producto.rpt;
using lebrun.Reportes.producto.dataset;

namespace lebrun.Reportes.producto
{
    public partial class frmConteProductos : Form
    {

        private DataTable tabla;
        private rptListadoPrecio rptListaPrecio;
        private Compania compania;
        private string reporte;


        public frmConteProductos()
        {
            InitializeComponent();
        }

        public frmConteProductos(string rpt, DataTable data)
        {
            InitializeComponent();
            reporte = rpt;
            tabla = data;
        }

        private void imprimirReporte()
        {
            if (reporte.Equals("CambioPrecio"))
            {

                rptListaPrecio = new rptListadoPrecio();
                rptListaPrecio.SetDataSource(tabla);
                rptListaPrecio.SetParameterValue("NombreCompania", compania.Nombre);
                this.crystalReportViewer1.ReportSource = rptListaPrecio;
                crystalReportViewer1.Visible = true;

            }
            else
            {
                this.Close();

            }

        }

        private void frmConteProductos_Load(object sender, EventArgs e)
        {
            compania = new Compania();
            compania.obtenerCodigoCompaniaActual();
            imprimirReporte();
        }


    }
}
