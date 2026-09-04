using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using lebrun.Reportes.pedido.rpt;

namespace lebrun.Reportes.pedido
{
    public partial class frmReportePedido : Form
    {
        pedido.rpt.pedido ped;

        public frmReportePedido()
        {
            InitializeComponent();
        }

        private static frmReportePedido m_FormDefInstance;

        public static frmReportePedido DefInstance
        {
            get
            {
                if (m_FormDefInstance == null || m_FormDefInstance.IsDisposed)
                    m_FormDefInstance = new frmReportePedido();
                return m_FormDefInstance;
            }
            set
            {
                m_FormDefInstance = value;
            }
        }


        public frmReportePedido armarReporte(DataSet dts, string nombreE, string rifE, bool centinela)
        {
            ped = new lebrun.Reportes.pedido.rpt.pedido();

            for (int z = 0; z < dts.Tables["DataTable2"].Rows.Count; z++)
            {
                dts.Tables["DataTable2"].Rows[z]["@mov_precio"] = dts.Tables["DataTable2"].Rows[z]["@mov_precio"].ToString().Replace(".", ",");
                dts.Tables["DataTable2"].Rows[z]["@mov_desc"] = dts.Tables["DataTable2"].Rows[z]["@mov_desc"].ToString().Replace(".", ",");
                dts.Tables["DataTable2"].Rows[z]["@mov_cant"] = dts.Tables["DataTable2"].Rows[z]["@mov_cant"].ToString().Replace(".", ",");
            }
            dts.Tables["DataTable1"].Rows[0]["cliente.Des1"] = dts.Tables["DataTable1"].Rows[0]["cliente.Des1"].ToString().Replace(".", ",");
            dts.Tables["DataTable1"].Rows[0]["cliente.Des2"] = dts.Tables["DataTable1"].Rows[0]["cliente.Des2"].ToString().Replace(".", ",");
            dts.Tables["DataTable1"].Rows[0]["cliente.Des3"] = dts.Tables["DataTable1"].Rows[0]["cliente.Des3"].ToString().Replace(".", ",");

            ped.SetDataSource(dts);
            ped.SetParameterValue("rifComp", rifE);
            ped.SetParameterValue("nombreCompania", nombreE);
            ped.SetParameterValue("centinela", centinela);
            this.crystalReportViewer1.ReportSource = ped;
            return this;
        }


       
    }
}
