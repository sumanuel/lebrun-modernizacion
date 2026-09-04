using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using lebrun.clasesData;
using System.Data;
using MySql.Data.MySqlClient;

namespace lebrun.clases.proveedores
{
    class Proveedores
    {
        private string StrSql;
        private DataTable tabla;
        private ConexionBD database;

        public Proveedores()
        {
            database = new ConexionBD();
            tabla = new DataTable();

        }

        public DataTable obtenerCuentasproveedor(string codigoProveedor, string tipoDoc)
        {
            StrSql = "SELECT n_cuenta,n_auxiliar FROM admtipdocpro2 WHERE n_tipo='$2' AND n_proveedor='$1'";
            
            StrSql = StrSql.Replace("$1", codigoProveedor);
            StrSql = StrSql.Replace("$2", tipoDoc);

            tabla = database.fDataTable(StrSql);
            database.cerrarConexion();

            return tabla;

        }
      

    }
}
