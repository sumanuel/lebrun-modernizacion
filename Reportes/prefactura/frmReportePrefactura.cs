using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace lebrun.Reportes.prefactura
{
    public partial class frmReportePrefactura : Form
    {
        //pedido.rpt.pedido ped;
        prefactura.rpt.prefactura pre;
        public frmReportePrefactura()
        {
            InitializeComponent();
        }

        private static frmReportePrefactura m_FormDefInstance;

        public static frmReportePrefactura DefInstance
        {
            get
            {
                if (m_FormDefInstance == null || m_FormDefInstance.IsDisposed)
                    m_FormDefInstance = new frmReportePrefactura();
                return m_FormDefInstance;
            }
            set
            {
                m_FormDefInstance = value;
            }
        }

        public frmReportePrefactura armarReporte(DataSet dts, string nombreE, string rifE, bool centinela)
        {
            pre = new lebrun.Reportes.prefactura.rpt.prefactura();

            for (int z = 0; z < dts.Tables["DataTable2"].Rows.Count; z++)
            {
                dts.Tables["DataTable2"].Rows[z]["@mov_precio"] = dts.Tables["DataTable2"].Rows[z]["@mov_precio"].ToString().Replace(".", ",");
                dts.Tables["DataTable2"].Rows[z]["@mov_desc"] = dts.Tables["DataTable2"].Rows[z]["@mov_desc"].ToString().Replace(".", ",");
                dts.Tables["DataTable2"].Rows[z]["@mov_cant"] = dts.Tables["DataTable2"].Rows[z]["@mov_cant"].ToString().Replace(".", ",");
            }
            dts.Tables["DataTable1"].Rows[0]["cliente.Des1"] = dts.Tables["DataTable1"].Rows[0]["cliente.Des1"].ToString().Replace(".", ",");
            dts.Tables["DataTable1"].Rows[0]["cliente.Des2"] = dts.Tables["DataTable1"].Rows[0]["cliente.Des2"].ToString().Replace(".", ",");
            dts.Tables["DataTable1"].Rows[0]["cliente.Des3"] = dts.Tables["DataTable1"].Rows[0]["cliente.Des3"].ToString().Replace(".", ",");

            pre.SetDataSource(dts);
            pre.SetParameterValue("rifComp", rifE);
            pre.SetParameterValue("nombreCompania", nombreE);
            pre.SetParameterValue("centinela", centinela);
            this.crystalReportViewer1.ReportSource = pre;
            return this;
        }
    }
}
