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
    class ReportesProductos
    {
        private ConexionBD database;
        private string sentencia;
        private DataTable tabla;  //se creo para almacenar el retorno de la consulta

        public ReportesProductos()
        { 
            database = new ConexionBD();
        }

        public DataTable ListadoProductos() {
            sentencia = "select inv_codigo, inv_descri, inv_estado from adminv";


            tabla = database.fDataTable(sentencia); //la datatable Tabla se utiliza para poder cerrar la conexion
            database.cerrarConexion();
            return tabla;
            
        }





    }
}
