using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using lebrun.clasesData;
using lebrun.Reportes.compras.dataset;
using lebrun.Reportes.compras.rpt;

namespace lebrun.Reportes.compras
{
    public partial class frmContenedorCompras : Form
    {

        private string tabla1;
        private string tabla2;
        private string StrSql;

        private string documento;
        private string sufijo;
        private string tipo;
        private string proveedor;
        private string status;
        private string origen;

        private ConexionBD database;
        private DataTable tabla;
        private DataTable tabla3;
        private rptCompras reportCompras;
        private Compania compania;

        public frmContenedorCompras()
        {
            InitializeComponent();
        }

        public frmContenedorCompras(string doc, string suf, string tip, string pro, int st)
        {
            //servicios y Compras

            InitializeComponent();
            documento = doc;
            sufijo = suf;
            tipo = tip;
            proveedor = pro;
            status = Convert.ToString(st);
            database = new ConexionBD();
            tabla = new DataTable();
            tabla3 = new DataTable();
        }

        public frmContenedorCompras(string doc, string suf, string tip, string pro, string orig)
        {
            //orden de compra
            //origen es parar saber si es importado o nacional

            InitializeComponent();
            documento = doc;
            sufijo = suf;
            tipo = tip;
            proveedor = pro;
            origen = orig;
            database = new ConexionBD();
            tabla = new DataTable();
            tabla3 = new DataTable();
        }

        private DataTable armarReporte()
        {

            if (tipo == "30")
            {
                tabla1 = "admdocpro_temp";
                tabla2 = "adminvmov_temp";
            }

            if (tipo == "37" || tipo == "43")
            {
                if (status == "1")
                {
                    tabla1 = "admdocpro_temp";
                    tabla2 = "adminvmov_temp";
                }
                if (status == "4")
                {
                    tabla1 = "admdocpro";
                    tabla2 = "adminvmov2";
                }

            }

            StrSql = "select " +
                      "dif_Inv,doc_numfis, doc_numerodoc,doc_concep, doc_fechEmis, doc_fechven, conp_descripcion as doc_condic, " +
                      "doc_plazo, doc_basnet, doc_mtoiva, doc_neto, doc_recibidopor,tdc_descri,doc_status,doc_sufiOrden, " +
                      "pro_codigo, pro_nombre, pro_rif, pro_direcc1, pro_telefono1, pro_telefono2, pro_telefono3, " +
                      "mov_codigo, inv_descri ,mov_memo,mov_undmed,mov_cant,mov_costo,mov_total,doc_numcontrol,doc_numfis,usu_nombre " +
                      "from " +
                      tabla1 + " " +
                      "left join sysconf.confusuario on doc_usuario=usu_codigo " +
                      "left join admproveedor on doc_codigo=pro_codigo " +
                      "left join admtipdocpro on tdc_codigo=doc_tipdoc " +
                      "left join " + tabla2 + " on mov_docume=doc_numerodoc and mov_tipdoc=doc_tipdoc and mov_codcta=doc_codigo and ";


            if (tipo == "37" || tipo == "43")
            {
                StrSql = StrSql + "mov_sufijo=doc_sufijo";
            }

            if (tipo == "30")
            {
                StrSql = StrSql + "mov_sufijo=doc_sufiOrden ";
            }

            StrSql = StrSql + "left join admcondpago on conp_codigo= doc_condic " +
                      "left join adminv on mov_codigo=inv_codigo " +
                    "where " +
                    "doc_numerodoc='$1'";

            if (tipo == "37" || tipo == "43")
            {
                StrSql = StrSql + "and doc_sufijo='$2' ";
            }

            if (tipo == "30")
            {
                StrSql = StrSql + "and doc_sufiOrden='$2' ";
            }


            StrSql = StrSql + "and doc_tipdoc='$3' and doc_codigo='$4' order by length( mov_item), mov_item;";

            StrSql = StrSql.Replace("$1", documento);
            StrSql = StrSql.Replace("$2", sufijo);
            StrSql = StrSql.Replace("$3", tipo);
            StrSql = StrSql.Replace("$4", proveedor);

            tabla = database.fDataTable(StrSql);
            database.cerrarConexion();
            return tabla;

        }

        private void frmContenedorCompras_Load(object sender, EventArgs e)
        {
            compania = new Compania();
            compania.obtenerCodigoCompaniaActual();

            tabla3 = armarReporte();

            if (tabla3.Rows.Count > 0)
            {
                reportCompras = new rptCompras();

                reportCompras.SetDataSource(tabla3);
                reportCompras.SetParameterValue("NombreCompania", compania.Nombre);
                reportCompras.SetParameterValue("RifCompania", compania.Rif);
                this.crystalReportViewer1.ReportSource = reportCompras;
                crystalReportViewer1.Visible = true;
            }
            else
            {
                MessageBox.Show("No existen datos para generar este reporte", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }

    }
}
