using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using lebrun.Reportes.pagos.rpt;
using lebrun.clasesData;

namespace lebrun.Reportes.compras
{
    public partial class frmContendorRptCompras : Form
    {
        private DataTable tabla;
        private string reporte;
        private rptVoucher voucher;
        private Compania compania;

        public frmContendorRptCompras()
        {
            InitializeComponent();
        }

        public frmContendorRptCompras(DataTable dt, string rpt)
        {
            InitializeComponent();
            tabla = dt;
            reporte = rpt;
        }

        private void imprimirReporte()
        {
            if (reporte.Equals("voucher"))
            {
                voucher = new rptVoucher();
                voucher.SetDataSource(tabla);
                compania.obtenerCompanias();
                voucher.SetParameterValue("NombreCompania", compania.Nombre);
                voucher.SetParameterValue("Origen", "Cheques");
                this.crystalReportViewer1.ReportSource = voucher;
                crystalReportViewer1.Visible = true;
            }

            if (reporte.Equals("voucherManual"))
            {
                voucher = new rptVoucher();
                voucher.SetDataSource(tabla);
                compania.obtenerCompanias();
                voucher.SetParameterValue("NombreCompania", compania.Nombre);
                voucher.SetParameterValue("Origen", "Cheque Manual");
                this.crystalReportViewer1.ReportSource = voucher;
                crystalReportViewer1.Visible = true;
            }
        }

        private void frmContendorRptCompras_Load(object sender, EventArgs e)
        {
            compania = new Compania();
            compania.obtenerCodigoCompaniaActual();
            imprimirReporte();
        }

    }
}
