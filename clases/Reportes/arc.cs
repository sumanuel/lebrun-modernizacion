using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using lebrun.clasesData;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace lebrun.clases.Reportes
{
    class arc
    {
        private string StrSql;
        private ConexionBD database;
        private DataTable tabla;

        public arc() {

            database = new ConexionBD();
            tabla = new DataTable();
        }

        public DataTable armarTablaProveedor(string fechaDesde, string fechaHasta, string codProveedor) { 
        
            StrSql ="SELECT  "+
                "CASE " +
                "WHEN MONTH(lislr_fechaEmisionFac) = 1 THEN concat('Ene-',right(year(lislr_fechaEmisionFac),2)) " +
                "WHEN MONTH(lislr_fechaEmisionFac) = 2 THEN concat('Feb-',right(year(lislr_fechaEmisionFac),2)) " +
                "WHEN MONTH(lislr_fechaEmisionFac) = 3 THEN concat('Mar-',right(year(lislr_fechaEmisionFac),2)) " +
                "WHEN MONTH(lislr_fechaEmisionFac) = 4 THEN concat('Abr-',right(year(lislr_fechaEmisionFac),2)) " +
                "WHEN MONTH(lislr_fechaEmisionFac) = 5 THEN concat('May-',right(year(lislr_fechaEmisionFac),2)) " +
                "WHEN MONTH(lislr_fechaEmisionFac) = 6 THEN concat('Jun-',right(year(lislr_fechaEmisionFac),2)) " +
                "WHEN MONTH(lislr_fechaEmisionFac) = 7 THEN concat('Jul-',right(year(lislr_fechaEmisionFac),2)) " +
                "WHEN MONTH(lislr_fechaEmisionFac) = 8 THEN concat('Ago-',right(year(lislr_fechaEmisionFac),2)) " +
                "WHEN MONTH(lislr_fechaEmisionFac) = 9 THEN concat('Sep-',right(year(lislr_fechaEmisionFac),2)) " +
                "WHEN MONTH(lislr_fechaEmisionFac) = 10 THEN concat('Oct-',right(year(lislr_fechaEmisionFac),2)) " +
                "WHEN MONTH(lislr_fechaEmisionFac) = 11 THEN concat('Nov-',right(year(lislr_fechaEmisionFac),2)) " +
                "WHEN MONTH(lislr_fechaEmisionFac) = 12 THEN concat('Dic-',right(year(lislr_fechaEmisionFac),2)) " +
                "ELSE '' END AS MES,lislr_totalFac,lislr_totalBase,lislr_porRetenc,lislr_retenido,pro_nombre,pro_rif,pro_codigo    FROM admlibroislr LEFT JOIN admproveedor ON lislr_proveedor=pro_codigo " +
                "WHERE lislr_proveedor='$3' " +
                "AND lislr_conceXml<>'001' " +
                "AND lislr_fechaEmisionFac " +
                "BETWEEN '$1' AND '$2'  order by lislr_fechaEmisionFac";

            StrSql = StrSql.Replace("$1",fechaDesde);
            StrSql = StrSql.Replace("$2",fechaHasta);
            StrSql = StrSql.Replace("$3",codProveedor);

            tabla =database.fDataTable(StrSql);
            database.cerrarConexion();
            return tabla;


        }


        public DataTable armarTablaTrabajadores(string fechaDesde, string fechaHasta, string codProveedor)
        {

            StrSql = "SELECT  " +
                "CASE " +
                "WHEN MONTH(lislr_fechaEmisionFac) = 1 THEN concat('Ene-',right(year(lislr_fechaEmisionFac),2)) " +
                "WHEN MONTH(lislr_fechaEmisionFac) = 2 THEN concat('Feb-',right(year(lislr_fechaEmisionFac),2)) " +
                "WHEN MONTH(lislr_fechaEmisionFac) = 3 THEN concat('Mar-',right(year(lislr_fechaEmisionFac),2)) " +
                "WHEN MONTH(lislr_fechaEmisionFac) = 4 THEN concat('Abr-',right(year(lislr_fechaEmisionFac),2)) " +
                "WHEN MONTH(lislr_fechaEmisionFac) = 5 THEN concat('May-',right(year(lislr_fechaEmisionFac),2)) " +
                "WHEN MONTH(lislr_fechaEmisionFac) = 6 THEN concat('Jun-',right(year(lislr_fechaEmisionFac),2)) " +
                "WHEN MONTH(lislr_fechaEmisionFac) = 7 THEN concat('Jul-',right(year(lislr_fechaEmisionFac),2)) " +
                "WHEN MONTH(lislr_fechaEmisionFac) = 8 THEN concat('Ago-',right(year(lislr_fechaEmisionFac),2)) " +
                "WHEN MONTH(lislr_fechaEmisionFac) = 9 THEN concat('Sep-',right(year(lislr_fechaEmisionFac),2)) " +
                "WHEN MONTH(lislr_fechaEmisionFac) = 10 THEN concat('Oct-',right(year(lislr_fechaEmisionFac),2)) " +
                "WHEN MONTH(lislr_fechaEmisionFac) = 11 THEN concat('Nov-',right(year(lislr_fechaEmisionFac),2)) " +
                "WHEN MONTH(lislr_fechaEmisionFac) = 12 THEN concat('Dic-',right(year(lislr_fechaEmisionFac),2)) " +
                "ELSE '' END AS MES,lislr_totalFac,lislr_totalBase,lislr_porRetenc,lislr_retenido,tra_nombre as pro_nombre,tra_rif as pro_rif,tra_rif as pro_codigo    FROM admlibroislr LEFT JOIN admtrabajador2 ON lislr_proveedor=tra_rif " +
                "WHERE lislr_proveedor='$3' " +
                "AND lislr_conceXml='001' " +
                "AND lislr_fechaEmisionFac " +
                "BETWEEN '$1' AND '$2' order by lislr_fechaEmisionFac";

            StrSql = StrSql.Replace("$1", fechaDesde);
            StrSql = StrSql.Replace("$2", fechaHasta);
            StrSql = StrSql.Replace("$3", codProveedor);

            tabla = database.fDataTable(StrSql);
            database.cerrarConexion();
            return tabla;
            

        }



        public DataTable armarTablaProveedor(string fechaDesde, string fechaHasta)
        {

            StrSql = "SELECT  " +
                "CASE " +
                "WHEN MONTH(lislr_fechaEmisionFac) = 1 THEN concat('Ene-',right(year(lislr_fechaEmisionFac),2)) " +
                "WHEN MONTH(lislr_fechaEmisionFac) = 2 THEN concat('Feb-',right(year(lislr_fechaEmisionFac),2)) " +
                "WHEN MONTH(lislr_fechaEmisionFac) = 3 THEN concat('Mar-',right(year(lislr_fechaEmisionFac),2)) " +
                "WHEN MONTH(lislr_fechaEmisionFac) = 4 THEN concat('Abr-',right(year(lislr_fechaEmisionFac),2)) " +
                "WHEN MONTH(lislr_fechaEmisionFac) = 5 THEN concat('May-',right(year(lislr_fechaEmisionFac),2)) " +
                "WHEN MONTH(lislr_fechaEmisionFac) = 6 THEN concat('Jun-',right(year(lislr_fechaEmisionFac),2)) " +
                "WHEN MONTH(lislr_fechaEmisionFac) = 7 THEN concat('Jul-',right(year(lislr_fechaEmisionFac),2)) " +
                "WHEN MONTH(lislr_fechaEmisionFac) = 8 THEN concat('Ago-',right(year(lislr_fechaEmisionFac),2)) " +
                "WHEN MONTH(lislr_fechaEmisionFac) = 9 THEN concat('Sep-',right(year(lislr_fechaEmisionFac),2)) " +
                "WHEN MONTH(lislr_fechaEmisionFac) = 10 THEN concat('Oct-',right(year(lislr_fechaEmisionFac),2)) " +
                "WHEN MONTH(lislr_fechaEmisionFac) = 11 THEN concat('Nov-',right(year(lislr_fechaEmisionFac),2)) " +
                "WHEN MONTH(lislr_fechaEmisionFac) = 12 THEN concat('Dic-',right(year(lislr_fechaEmisionFac),2)) " +
                "ELSE '' END AS MES,lislr_totalFac,lislr_totalBase,lislr_porRetenc,lislr_retenido,pro_nombre,pro_rif,pro_codigo   FROM admlibroislr LEFT JOIN admproveedor ON lislr_proveedor=pro_codigo " +
                "WHERE lislr_conceXml<>'001' AND lislr_fechaEmisionFac " +
                "BETWEEN '$1' AND '$2' order by pro_codigo, lislr_fechaEmisionFac";

            StrSql = StrSql.Replace("$1", fechaDesde);
            StrSql = StrSql.Replace("$2", fechaHasta);


            tabla = database.fDataTable(StrSql);
            database.cerrarConexion();
            return tabla;


        }


        public DataTable armarTablaTrabajadores(string fechaDesde, string fechaHasta)
        {

            StrSql = "SELECT  " +
                "CASE " +
                "WHEN MONTH(lislr_fechaEmisionFac) = 1 THEN concat('Ene-',right(year(lislr_fechaEmisionFac),2)) " +
                "WHEN MONTH(lislr_fechaEmisionFac) = 2 THEN concat('Feb-',right(year(lislr_fechaEmisionFac),2)) " +
                "WHEN MONTH(lislr_fechaEmisionFac) = 3 THEN concat('Mar-',right(year(lislr_fechaEmisionFac),2)) " +
                "WHEN MONTH(lislr_fechaEmisionFac) = 4 THEN concat('Abr-',right(year(lislr_fechaEmisionFac),2)) " +
                "WHEN MONTH(lislr_fechaEmisionFac) = 5 THEN concat('May-',right(year(lislr_fechaEmisionFac),2)) " +
                "WHEN MONTH(lislr_fechaEmisionFac) = 6 THEN concat('Jun-',right(year(lislr_fechaEmisionFac),2)) " +
                "WHEN MONTH(lislr_fechaEmisionFac) = 7 THEN concat('Jul-',right(year(lislr_fechaEmisionFac),2)) " +
                "WHEN MONTH(lislr_fechaEmisionFac) = 8 THEN concat('Ago-',right(year(lislr_fechaEmisionFac),2)) " +
                "WHEN MONTH(lislr_fechaEmisionFac) = 9 THEN concat('Sep-',right(year(lislr_fechaEmisionFac),2)) " +
                "WHEN MONTH(lislr_fechaEmisionFac) = 10 THEN concat('Oct-',right(year(lislr_fechaEmisionFac),2)) " +
                "WHEN MONTH(lislr_fechaEmisionFac) = 11 THEN concat('Nov-',right(year(lislr_fechaEmisionFac),2)) " +
                "WHEN MONTH(lislr_fechaEmisionFac) = 12 THEN concat('Dic-',right(year(lislr_fechaEmisionFac),2)) " +
                "ELSE '' END AS MES,lislr_totalFac,lislr_totalBase,lislr_porRetenc,lislr_retenido,tra_nombre as pro_nombre,tra_rif as pro_rif,tra_rif as pro_codigo   FROM admlibroislr LEFT JOIN admtrabajador2 ON lislr_proveedor=tra_rif " +
                "WHERE lislr_conceXml='001' AND lislr_fechaEmisionFac " +
                "BETWEEN '$1' AND '$2'";

            StrSql = StrSql.Replace("$1", fechaDesde);
            StrSql = StrSql.Replace("$2", fechaHasta);


            tabla = database.fDataTable(StrSql);
            database.cerrarConexion();
            return tabla;


        }














    }


}
