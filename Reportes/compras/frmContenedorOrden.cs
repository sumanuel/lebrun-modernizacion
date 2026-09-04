using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using lebrun.clasesData;
using lebrun.Reportes.compras.rpt;

namespace lebrun.Reportes.compras
{
    public partial class frmContenedorOrden : Form
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
        private Compania compania;
        private rptOrden rptOrdenCompra;

        public frmContenedorOrden()
        {
            InitializeComponent();
        }

        public frmContenedorOrden(string doc, string suf, string tip, string pro, string orig)
        {
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

        public DataTable armarReporte()
        {

            StrSql = "";
            StrSql = "  SELECT ord_correlativo,ord_ordencompra,ord_documento,ord_sufijo,ord_tipdoc,ord_concep,ord_codigo,ord_origen,ord_fechCarg,ord_total,tdc_descri, " +
                        "pro_codigo, pro_nombre, pro_rif, pro_direcc1, " +
                        "pro_telefono1, pro_telefono2, pro_telefono3, mov_codigo, " +
                        "inv_descri,mov_memo,mov_undmed,mov_cant,mov_costo, " +
                        "mov_total,usu_nombre " +
                        "FROM admordencompra " +
                        "LEFT JOIN sysconf.confusuario ON ord_usuario=usu_codigo " +
                        "LEFT JOIN admproveedor ON ord_codigo=pro_codigo " +
                        "LEFT JOIN admtipdocpro ON tdc_codigo=ord_tipdoc " +
                        "LEFT JOIN adminvmov_temp ON mov_docume=ord_correlativo AND mov_tipdoc=ord_tipdoc AND mov_codcta=ord_codigo AND mov_sufijo=ord_sufijo " +
                        "LEFT JOIN adminv ON mov_codigo=inv_codigo " +
                        "WHERE ord_correlativo='$1' AND ord_sufijo='$2' AND ord_tipdoc='$3' AND ord_codigo='$4' " +
                        "ORDER BY LENGTH(mov_item), mov_item;";

            StrSql = StrSql.Replace("$1", documento);
            StrSql = StrSql.Replace("$2", sufijo);
            StrSql = StrSql.Replace("$3", tipo);
            StrSql = StrSql.Replace("$4", proveedor);
            StrSql = StrSql.Replace("$5", origen);

            tabla = database.fDataTable(StrSql);
            database.cerrarConexion();
            return tabla;
        }

        private void frmContenedorOrden_Load(object sender, EventArgs e)
        {
            compania = new Compania();
            compania.obtenerCodigoCompaniaActual();

            tabla3 = armarReporte();

            if (tabla3.Rows.Count > 0)
            {
                rptOrdenCompra = new rptOrden();

                rptOrdenCompra.SetDataSource(tabla3);
                rptOrdenCompra.SetParameterValue("NombreCompania", compania.Nombre);
                rptOrdenCompra.SetParameterValue("RifCompania", compania.Rif);
                this.crystalReportViewer1.ReportSource = rptOrdenCompra;
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
