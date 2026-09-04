using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using lebrun.clasesData;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace lebrun.clases.importaciones
{
    class factores
    {
        private ConexionBD database;
        //private DataSet ds;
        private MySqlDataReader dr;

        private string ultimoFactorCif;
        private string ultimoFactorGasto;
        private string ultimoFactorFin;
        private string ultimoFactorOtros;
        private string StrSql;


        public string UltimoFactorCif
        {
            get { return ultimoFactorCif; }
            set { ultimoFactorCif = value; }
        }

        public string UltimoFactorGasto
        {
            get { return ultimoFactorGasto; }
            set { ultimoFactorGasto = value; }
        }

        public string UltimoFactorFin
        {
            get { return ultimoFactorFin; }
            set { ultimoFactorFin = value; }
        }

        public string UltimoFactorOtros
        {
            get { return ultimoFactorOtros; }
            set { ultimoFactorOtros = value; }
        }

        public factores() {

            database = new ConexionBD();
        }

        public void buscarUltimosFactores(string codProveedor) {

            StrSql = "SELECT Factor_CIF,Factor_FIN,Factor_GAS,Factor_Otros FROM admproveedor2 WHERE pro2_codigo='$1'";
            StrSql = StrSql.Replace("$1", codProveedor);

            dr = database.ejecutarQueryDr(StrSql);

            if (dr.HasRows)
            {
                dr.Read();

                this.ultimoFactorCif = dr.GetString("Factor_CIF");
                this.ultimoFactorGasto = dr.GetString("Factor_GAS");
                this.ultimoFactorFin = dr.GetString("Factor_FIN");
                this.ultimoFactorOtros = dr.GetString("Factor_Otros");
            }

            database.cerrarConexion();
            dr.Close();

        }


    }
}
