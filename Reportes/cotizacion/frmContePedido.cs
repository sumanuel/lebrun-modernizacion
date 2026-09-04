using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using lebrun.Reportes.cotizacion.rpt;

namespace lebrun.Reportes.pedido
{
    public partial class frmContePedido : Form
    {
        RPTCTZ rptPedido;
        public frmContePedido()
        {
            InitializeComponent();
        }

        private static frmContePedido m_FormDefInstance;

        public static frmContePedido DefInstance
        {
            get
            {
                if (m_FormDefInstance == null || m_FormDefInstance.IsDisposed)
                    m_FormDefInstance = new frmContePedido();
                return m_FormDefInstance;
            }
            set
            {
                m_FormDefInstance = value;
            }
        }

        public frmContePedido armarReporte(DataSet datos, string nombreComp, string rif)
        {
            rptPedido = new RPTCTZ();
            for (int i = 0; i < datos.Tables["DataTable1"].Rows.Count; i++)
            {
                datos.Tables["DataTable1"].Rows[i]["@mov_cant"] = datos.Tables["DataTable1"].Rows[i]["@mov_cant"].ToString().Replace(".", ",");
                datos.Tables["DataTable1"].Rows[i]["@mov_precio"] = datos.Tables["DataTable1"].Rows[i]["@mov_precio"].ToString().Replace(".", ",");
                datos.Tables["DataTable1"].Rows[i]["@mov_total"] = datos.Tables["DataTable1"].Rows[i]["@mov_total"].ToString().Replace(".", ",");
            }
            datos.Tables["DataTable2"].Rows[0]["subTotal"] = datos.Tables["DataTable2"].Rows[0]["subTotal"].ToString().Replace(".", ",");
            datos.Tables["DataTable2"].Rows[0]["totalDoc"] = datos.Tables["DataTable2"].Rows[0]["totalDoc"].ToString().Replace(".", ",");
            datos.Tables["DataTable2"].Rows[0]["iva"] = datos.Tables["DataTable2"].Rows[0]["iva"].ToString().Replace(".", ",");

            rptPedido.SetDataSource(datos);
            rptPedido.SetParameterValue("nombreCompania", nombreComp);
            rptPedido.SetParameterValue("rifComp", rif);
            this.crystalReportViewer1.ReportSource = rptPedido;
            return this;
        }
    }
}
