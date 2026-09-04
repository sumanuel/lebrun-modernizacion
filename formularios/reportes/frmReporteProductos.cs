using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using lebrun.clases.Reportes;
using lebrun.Reportes;

namespace lebrun.formularios.reportes
{
    public partial class frmReporteProductos : Form
    {

      //  private frmReporte rptReporte;
        private frmContenedorProductos rptReporte;

        public frmReporteProductos()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            rptReporte = new frmContenedorProductos("ListadoProductos");
            rptReporte.Show();
        }

        private void frmReporteProductos_MouseClick(object sender, MouseEventArgs e)
        {

        }
    }
}
