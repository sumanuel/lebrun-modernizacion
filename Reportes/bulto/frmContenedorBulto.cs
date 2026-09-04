using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using lebrun.Reportes.bulto.rpt;
using lebrun.Reportes.bulto.dataset;

namespace lebrun.Reportes.bulto
{
    public partial class frmContenedorBulto : Form
    {
        public frmContenedorBulto()
        {
            InitializeComponent();
        }

        rptBultos reporte;

        private void frmContenedorBulto_Load(object sender, EventArgs e){}

        public frmContenedorBulto armarReporte(DataSet dts)
        {
            reporte = new rptBultos();
            reporte.SetDataSource(dts);
            this.crystalReportViewer1.ReportSource = reporte;
            return this;
        }

    }
}
