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
using lebrun.Reportes.facturacion;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.ReportAppServer.Controllers;
using Ras = CrystalDecisions.ReportAppServer.ReportDefModel;

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
                        "ord_subtot,ord_desc1,ord_desc2,ord_desc3, " +
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

        // Cuando la orden tiene descuentos, sobre el total se agregan el subtotal (la suma de las lineas) y lo descontado, para que el impreso cuadre.
        // El cuadro del total del reporte tiene ese espacio libre. Si no se pueden agregar, el reporte sale como siempre y el error queda registrado.
        private static void AgregarDescuentos(ReportDocument reporte, DataRow orden)
        {
            decimal subtotal = orden["ord_subtot"] == DBNull.Value ? 0m : Convert.ToDecimal(orden["ord_subtot"]);
            decimal total = orden["ord_total"] == DBNull.Value ? 0m : Convert.ToDecimal(orden["ord_total"]);
            if (subtotal - total < 0.005m)
            {
                return;
            }
            List<string> porcentajes = new List<string>();
            foreach (string columna in new[] { "ord_desc1", "ord_desc2", "ord_desc3" })
            {
                if (orden[columna] != DBNull.Value && Convert.ToDecimal(orden[columna]) != 0m)
                {
                    porcentajes.Add(Convert.ToDecimal(orden[columna]).ToString("0.##") + "%");
                }
            }
            try
            {
                var definicion = reporte.ReportClientDocument.ReportDefController;
                ReportObjectController controlador = definicion.ReportObjectController;
                Ras.Section pie = definicion.ReportDefinition.PageFooterArea.Sections[0];
                Ras.ReportObject modelo = null;
                for (int i = 0; i < pie.ReportObjects.Count; i++)
                {
                    Ras.ReportObject o = (Ras.ReportObject)pie.ReportObjects[i];
                    if (o.Name == "Text16")   // el rotulo "Total:"
                    {
                        modelo = o;
                    }
                }
                if (modelo == null)
                {
                    throw new InvalidOperationException("El reporte de la orden de compra no tiene el objeto base para las filas de descuento.");
                }
                frmReporteCierreCaja.AgregarTexto(controlador, pie, modelo, "ocSubtotalRotulo", 8160, 1450, 1200, "Sub-total:", Ras.CrAlignmentEnum.crAlignmentLeft, 8);
                frmReporteCierreCaja.AgregarTexto(controlador, pie, modelo, "ocSubtotalValor", 9390, 1450, 1470, subtotal.ToString("n"), Ras.CrAlignmentEnum.crAlignmentRight, 8);
                frmReporteCierreCaja.AgregarTexto(controlador, pie, modelo, "ocDescuentoRotulo", 8160, 1750, 2700,
                                                  "Descuento" + (porcentajes.Count > 0 ? " " + string.Join(" + ", porcentajes.ToArray()) : "") + ":", Ras.CrAlignmentEnum.crAlignmentLeft, 8);
                frmReporteCierreCaja.AgregarTexto(controlador, pie, modelo, "ocDescuentoValor", 9390, 2010, 1470, "-" + (subtotal - total).ToString("n"), Ras.CrAlignmentEnum.crAlignmentRight, 8);
            }
            catch (Exception ex)
            {
                RegistroErrores.Registrar(ex, "Orden de compra: descuentos en el impreso");
            }
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
                AgregarDescuentos(rptOrdenCompra, tabla3.Rows[0]);
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
