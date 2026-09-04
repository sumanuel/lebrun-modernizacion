using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;
using System.Configuration;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace lebrun.clasesData
{
    public class ConexionBD
    {
        public MySqlDataReader dr;
        private MySqlConnection mySqlConnection;
        private Boolean conexionExitosa = true;
        private string strDatabase1;
        private string strDatabase2;
        private string strLogin;
        private string strServer;
        private string strPassword;
        private string strPort;
        private string strConnectionString;
        private int idConection;
        

        public ConexionBD(){
            try
            {
                strServer = ConfigurationManager.AppSettings.Get("server");

                strLogin = ConfigurationManager.AppSettings.Get("login");
                strPassword = ConfigurationManager.AppSettings.Get("password");
                strPort = ConfigurationManager.AppSettings.Get("port");
                strDatabase1 = ConfigurationManager.AppSettings.Get("db1");
                strDatabase2 = ConfigurationManager.AppSettings.Get("db2");
                strConnectionString = "Server=" + strServer + "; " +
                                              "Port=" + strPort + "; " +
                                              "Database=" + strDatabase1 + "; " +
                                              "Uid=" + strLogin + "; " +
                                              "Pwd=" + strPassword + "; ";
                
            }
            catch (System.Configuration.ConfigurationException e)
            {
                MessageBox.Show("Error de conexion");
                MessageBox.Show(e.Message);
            }
            idConection = -1;
        }

        public ConexionBD(int tipoBaseDatos) {
            modificarConexionString(tipoBaseDatos);
        }


        public ConexionBD(string timeoutConection) {
            try
            {
                strServer = ConfigurationManager.AppSettings.Get("server");

                strLogin = ConfigurationManager.AppSettings.Get("login");
                strPassword = ConfigurationManager.AppSettings.Get("password");
                strPort = ConfigurationManager.AppSettings.Get("port");
                strDatabase1 = ConfigurationManager.AppSettings.Get("db1");
                strDatabase2 = ConfigurationManager.AppSettings.Get("db2");
                strConnectionString = "Server=" + strServer + "; " +
                                              "Port=" + strPort + "; " +
                                              "Database=" + strDatabase1 + "; " +
                                              "Uid=" + strLogin + "; " +
                                              "Pwd=" + strPassword + "; "+
                                              "Connect Timeout="+timeoutConection+"; ";
            }
            catch (System.Configuration.ConfigurationException e) {
                MessageBox.Show("Error de conexion");
                MessageBox.Show(e.Message);
            }
            idConection = -1;
        }

        public void modificarConexionString(int baseDatos)
        {
            try
            {
                strServer = ConfigurationManager.AppSettings.Get("server");

                strLogin = ConfigurationManager.AppSettings.Get("login");
                strPassword = ConfigurationManager.AppSettings.Get("password");
                strPort = ConfigurationManager.AppSettings.Get("port");
                switch (baseDatos)
                {
                    case 1:
                        strDatabase1 = ConfigurationManager.AppSettings.Get("db1");
                        break;
                    case 2:
                        strDatabase1 = ConfigurationManager.AppSettings.Get("db2");
                        break;
                    case 3:
                        strDatabase1 = ConfigurationManager.AppSettings.Get("db3");
                        break;
                }
                strConnectionString = "Server=" + strServer + "; " +
                                             "Port=" + strPort + "; " +
                                             "Database=" + strDatabase1 + "; " +
                                             "Uid=" + strLogin + "; " +
                                             "Pwd=" + strPassword + "; ";
            }
            catch (System.Configuration.ConfigurationException e)
            {
                MessageBox.Show("Error de conexion contabilidad");
                MessageBox.Show(e.Message);
            }

        }

        public void modificarConexionString2(int baseDatos)
        {
            try
            {
                strServer = ConfigurationManager.AppSettings.Get("server");

                strLogin = ConfigurationManager.AppSettings.Get("login");
                strPassword = ConfigurationManager.AppSettings.Get("password");
                strPort = ConfigurationManager.AppSettings.Get("port");
                switch (baseDatos)
                {
                    case 1:
                        strDatabase1 = ConfigurationManager.AppSettings.Get("db1");
                        break;
                    case 2:
                        strDatabase1 = ConfigurationManager.AppSettings.Get("db2");
                        break;
                    case 3:
                        strDatabase1 = ConfigurationManager.AppSettings.Get("db3");
                        break;

                }
                strConnectionString = "Server=" + strServer + "; " +
                                             "Port=" + strPort + "; " +
                                             "Database=" + strDatabase1 + ";pooling=false;Allow Zero Datetime=True;Convert Zero Datetime=true;" +
                                             "Uid=" + strLogin + "; " +
                                             "Pwd=" + strPassword + "; ";

                //Convert Zero Datetime=True;
            }
            catch (System.Configuration.ConfigurationException e)
            {
                MessageBox.Show("Error de conexion");
                MessageBox.Show(e.Message);
            }

        }

        public void conectionStringSysconta(string bd1) { 
             try
            {
                strServer = ConfigurationManager.AppSettings.Get("server");
                strLogin = ConfigurationManager.AppSettings.Get("login");
                strPassword = ConfigurationManager.AppSettings.Get("password");
                strPort = ConfigurationManager.AppSettings.Get("port");
                strDatabase1 = ConfigurationManager.AppSettings.Get("db3");

                //switch (bd1)
                //{   
                //    case "02":
                //        strDatabase1 = strDatabase1 + "_02";
                //        break;

                //    case "03":
                //        strDatabase1 = strDatabase1 + "_03";
                //        break;
                    
                //    case "04":
                //        strDatabase1 = strDatabase1 + "_04";
                //        break;

                //    case "05":
                //        strDatabase1 = strDatabase1 + "_05";
                //        break;

                //    case "06":
                //        strDatabase1 = strDatabase1 + "_06";
                //        break;
                //}
                if (!(bd1.Equals("01")))
                {
                    strDatabase1 = strDatabase1 + "_" + bd1;
                }
                strConnectionString = "Server=" + strServer + "; " +
                                             "Port=" + strPort + "; " +
                                             "Database=" + strDatabase1 + "; " +
                                             "Uid=" + strLogin + "; " +
                                             "Pwd=" + strPassword + "; ";


            }
             catch (System.Configuration.ConfigurationException e)
             {
                 MessageBox.Show("Error de conectionStringSysconta");
                 MessageBox.Show(e.Message);
             }
        }


        /* No Implementado CERRAR EL DATAREADER POR CUENTA PROPIA 23/05/2012 */
        public MySqlDataReader ejecutarQueryDr(string sentenciaSql)
        {
            //retorna un DataReader
            mySqlConnection = new MySqlConnection(strConnectionString);

            try
            {
                mySqlConnection.Open();

                if (conexionExitosa == true)
                {

                    MySqlCommand sql = new MySqlCommand();
                    sql.Connection = mySqlConnection;
                    sql.CommandText = sentenciaSql;
                    sql.CommandType = CommandType.Text;
                    dr = sql.ExecuteReader();
                }
                return dr;
            }
            catch (MySqlException e)
            {
                MessageBox.Show("Código de error:" + e.ErrorCode + " Mensaje:" + e.Message + " ConexionBD ejecutarQuery");
                conexionExitosa = false;
                mySqlConnection.Close();
                mySqlConnection.Dispose();
                return dr;
            }
        }// fin ejecutarQuery

        public MySqlDataReader ejecutarQueryDr(string sentenciaSql, int timeOut)
        {
            //retorna un DataReader
            mySqlConnection = new MySqlConnection(strConnectionString);

            try
            {
                mySqlConnection.Open();

                if (conexionExitosa == true)
                {

                    MySqlCommand sql = new MySqlCommand();
                    sql.Connection = mySqlConnection;
                    sql.CommandTimeout = timeOut;
                    sql.CommandText = sentenciaSql;
                    sql.CommandType = CommandType.Text;
                    dr = sql.ExecuteReader();
                }
                return dr;
            }
            catch (MySqlException e)
            {
                MessageBox.Show("Código de error:" + e.ErrorCode + " Mensaje:" + e.Message + " ConexionBD ejecutarQuery");
                conexionExitosa = false;
                mySqlConnection.Close();
                mySqlConnection.Dispose();
                return dr;
            }
        }// fin ejecutarQuery


        public DataSet ejecutarQueryDs(string sentenciaSql)
        {
            //Retorna un dataset
            DataSet resultDataSet = new DataSet();
            int temp;
            try
            {
                mySqlConnection = new MySqlConnection(strConnectionString);
                MySqlDataAdapter Adaptador = new MySqlDataAdapter(sentenciaSql, mySqlConnection);
                mySqlConnection.Open();
                temp = mySqlConnection.ServerThread;
                Adaptador.Fill(resultDataSet,"tablas");             
                return resultDataSet;
            }
            catch (MySqlException e)
            {
                MessageBox.Show("Código de error:" + e.ErrorCode + " Mensaje:" + e.Message + " ConexionBD ejecutarQuery");
                return resultDataSet;
            }
            finally
            {
                mySqlConnection.Close();
                mySqlConnection.Dispose();
            } // end finally
        }// fin ejecutarQuery

        public int sentenciasNumeroFilas(string sentenciaSql)
        {
            int filas = 0;
            try
            {
                mySqlConnection = new MySqlConnection(strConnectionString);
                mySqlConnection.Open();
                if (conexionExitosa == true)
                {
                    MySqlCommand comando = new MySqlCommand(sentenciaSql, mySqlConnection);                    
                    filas = comando.ExecuteNonQuery();
                }
                return filas;
            }
            catch (MySqlException e)
            {
                MessageBox.Show("Código de error:" + e.ErrorCode + " Mensaje:" + e.Message + " ConexionBD ejecutarQuery");
                return filas;
            }
            finally {
                mySqlConnection.Close();
                mySqlConnection.Dispose();
            }
        }//fin sentenciasNumeroFilas


        public DataTable fDataTable(string sentenciaSql, int timeOut)
        {
            DataTable Dt = new DataTable();
            MySqlDataAdapter Da;

            try
            {
                mySqlConnection = new MySqlConnection(strConnectionString);
                mySqlConnection.Open();
                Da = new MySqlDataAdapter();
                MySqlCommand comandoT = new MySqlCommand(sentenciaSql, mySqlConnection);
                //comandoT.CommandTimeout = 200;
                Da.SelectCommand = comandoT;
                Da.Fill(Dt);
                mySqlConnection.Close();
                return Dt;


                //mySqlConnection.Open();
                //Da = new MySqlDataAdapter(sentenciaSql, mySqlConnection);
                //Da.Fill(Dt);              
                //return Dt;
            }
            catch (MySqlException e)
            {
                MessageBox.Show("Código de error:" + e.ErrorCode + " Mensaje:" + e.Message + " ConexionBD ejecutarQuery");
                return Dt;
            }
            finally
            {
                mySqlConnection.Close();
                mySqlConnection.Dispose();
            } // end finally
        }



        public DataTable fDataTable(string sentenciaSql) {            
            DataTable Dt = new DataTable();
            MySqlDataAdapter Da;
            
            try
            {
                mySqlConnection = new MySqlConnection(strConnectionString);
                mySqlConnection.Open();
                Da = new MySqlDataAdapter();
                MySqlCommand comandoT = new MySqlCommand(sentenciaSql, mySqlConnection);
                comandoT.CommandTimeout = 200;
                Da.SelectCommand = comandoT;
                Da.Fill(Dt);
                mySqlConnection.Close();
                return Dt;

                
                //mySqlConnection.Open();
                //Da = new MySqlDataAdapter(sentenciaSql, mySqlConnection);
                //Da.Fill(Dt);              
                //return Dt;
            }
            catch (MySqlException e) {
                MessageBox.Show("Código de error:" + e.ErrorCode + " Mensaje:" + e.Message + " ConexionBD ejecutarQuery");                
                return Dt;
            }
            finally
            {   mySqlConnection.Close();
                mySqlConnection.Dispose();
            } // end finally
        }

        public void insertDataTable(DataTable data, string[]parametros, string sentenciaSql,int timeOut) {
            mySqlConnection = new MySqlConnection(strConnectionString);
            try
            {
                mySqlConnection.Open();
                MySqlCommand cmd = mySqlConnection.CreateCommand();
                cmd.CommandTimeout = timeOut;

                foreach (DataRow fila in data.Rows)
                {  
                    
                    cmd.CommandText = sentenciaSql;
                    for (int i = 0; i < parametros.Length; i++)
                    {
                        cmd.Parameters.AddWithValue(parametros[i], fila[i]);
                    }
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();

                }

            }
            catch (MySqlException e)
            {
                MessageBox.Show("Código de error:" + e.ErrorCode + " Mensaje:" + e.Message + " ConexionBD ejecutarQuery");
            }
            finally {
                mySqlConnection.Close();
                mySqlConnection.Dispose();
            }
        }

        public void insertDataTable(DataTable data, string[] parametros, string sentenciaSql)
        {
            mySqlConnection = new MySqlConnection(strConnectionString);
            try
            {
                mySqlConnection.Open();

                foreach (DataRow fila in data.Rows)
                {

                    MySqlCommand cmd = mySqlConnection.CreateCommand();
                    cmd.CommandText = sentenciaSql;
                    for (int i = 0; i < parametros.Length; i++)
                    {
                        cmd.Parameters.AddWithValue(parametros[i], fila[i]);
                    }
                    cmd.ExecuteNonQuery();
                }

            }
            catch (MySqlException e)
            {
                MessageBox.Show("Código de error:" + e.ErrorCode + " Mensaje:" + e.Message + " ConexionBD ejecutarQuery");
            }
            finally
            {
                mySqlConnection.Close();
                mySqlConnection.Dispose();
            }
        }




        //public void insertDataTable(DataGridView dataGV, string[] parametros, string sentenciaSql)
        //{
        //    mySqlConnection = new MySqlConnection(strConnectionString);
        //    try
        //    {
        //        mySqlConnection.Open();

        //        for (int x = 0; x < dataGV.Rows.Count; x++) {
        //            MySqlCommand cmd = mySqlConnection.CreateCommand();
        //            cmd.CommandText = sentenciaSql;
        //            for (int i = 0; i < parametros.Length; i++)
        //            {
        //                cmd.Parameters.AddWithValue(parametros[i], dataGV.Rows[x].Cells[i].Value.ToString());
        //            }
        //        }


        //            foreach (DataRow fila in data.Rows)
        //            {

        //                MySqlCommand cmd = mySqlConnection.CreateCommand();
        //                cmd.CommandText = sentenciaSql;
        //                for (int i = 0; i < parametros.Length; i++)
        //                {
        //                    cmd.Parameters.AddWithValue(parametros[i], fila[i]);
        //                }
        //                cmd.ExecuteNonQuery();
        //            }

        //    }
        //    catch (MySqlException e)
        //    {
        //        MessageBox.Show("Código de error:" + e.ErrorCode + " Mensaje:" + e.Message + " ConexionBD ejecutarQuery");
        //    }
        //    finally
        //    {
        //        mySqlConnection.Close();
        //        mySqlConnection.Dispose();
        //    }
        //}



        public bool ejecutarInsert(string sentenciaSql, int timeOut) {
            mySqlConnection = new MySqlConnection(strConnectionString);
            Boolean centinela = true;
            try
            {
                mySqlConnection.Open();
                if (conexionExitosa == true)
                {
                    MySqlCommand sql = new MySqlCommand(sentenciaSql, mySqlConnection);
                    sql.CommandTimeout = timeOut;
                    sql.ExecuteNonQuery();
                }
                else
                {
                    centinela = false;
                }
                return centinela;
            }
            catch (MySqlException e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
            finally
            {
                mySqlConnection.Close();
                mySqlConnection.Dispose();
            }
        }



        public Boolean ejecutarInsert(string sentenciaSql){
            mySqlConnection = new MySqlConnection(strConnectionString);
            Boolean centinela = true;
            try
            {
                mySqlConnection.Open();
                if (conexionExitosa == true)
                {
                    MySqlCommand sql = new MySqlCommand(sentenciaSql, mySqlConnection);
                    sql.ExecuteNonQuery();                
                }else{
                    centinela = false;
                }
                return centinela;
            }
            catch (MySqlException e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
            finally {
                mySqlConnection.Close();
                mySqlConnection.Dispose();
            }
        }//fin ejecutarInsert

        public int ejecutarInsert2(string sentenciaSql)
        {
            mySqlConnection = new MySqlConnection(strConnectionString);
            int resultado = -1;
            try
            {
                mySqlConnection.Open();
                if (conexionExitosa == true)
                {
                    MySqlCommand sql = new MySqlCommand(sentenciaSql, mySqlConnection);
                    sql.ExecuteNonQuery();
                }

                return resultado;
            }
            catch (MySqlException e)
            {
                resultado = e.Number;
                StreamWriter sw;
                sw = null;
                string ruta = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
                ruta = ruta.Remove(0, 6);
                if (resultado == 1062)
                {
                    MessageBox.Show("Por Favor Comunicarse Con el departo de Sistemas!!!!", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                try
                {
                    if (File.Exists(ruta + "\\log.txt"))
                    {
                        sw = File.AppendText(ruta + "\\log.txt");
                    }
                    else
                    {
                        sw = File.CreateText(ruta + "\\log.txt");
                    }
                    sw.WriteLine("------------------------------------------------------");
                    sw.WriteLine("------------------------------------------------------");
                    sw.WriteLine();
                    sw.WriteLine("Error Fecha " + DateTime.Now);
                    sw.WriteLine();
                    sw.WriteLine("Sentencia Involucrada:");
                    sw.WriteLine(sentenciaSql);
                    sw.WriteLine("Codigo de error: " + e.Number);
                    sw.WriteLine("Mensaje del Error: " + e.Message);
                    sw.WriteLine();
                }
                catch (Exception x) { }
                finally
                {
                    sw.Close();
                }
                return resultado;
            }
            finally
            {
                mySqlConnection.Close();
                mySqlConnection.Dispose();
            }
        }//fin ejecutarInsert



        public void cerrarConexion() {
            try
            {
                mySqlConnection.Close();
                mySqlConnection.Dispose();
            }
            catch (MySqlException e) {
                MessageBox.Show("Problema al cerrar la conexion ");
            }
        }

        //troll prada
        public void insertdataGridView(DataGridView data, string[] parametros, string sentenciaSql)
        {

            mySqlConnection = new MySqlConnection(strConnectionString);
            try
            {
                mySqlConnection.Open();

                foreach (DataGridViewRow fila in data.Rows)
                {

                    MySqlCommand cmd = mySqlConnection.CreateCommand();
                    cmd.CommandText = sentenciaSql;
                    for (int i = 0; i < parametros.Length; i++)
                    {
                        cmd.Parameters.AddWithValue(parametros[i], fila.Cells[i].Value);

                    }

                    cmd.ExecuteNonQuery();
                }

            }
            catch (MySqlException e)
            {
                MessageBox.Show("Código de error:" + e.ErrorCode + " Mensaje:" + e.Message + " ConexionBD ejecutarQuery");
            }
            finally
            {
                mySqlConnection.Close();
                mySqlConnection.Dispose();
            }
        }

        public void insertdataGridViewConNombre(DataGridView data, string[] parametros, string sentenciaSql)
        {
            
            mySqlConnection = new MySqlConnection(strConnectionString);
            try
            {
                mySqlConnection.Open();

                foreach (DataGridViewRow fila in data.Rows)
                {

                    MySqlCommand cmd = mySqlConnection.CreateCommand();
                    cmd.CommandText = sentenciaSql;
                    for (int i = 0; i < parametros.Length; i++)
                    {
                        cmd.Parameters.AddWithValue(parametros[i], fila.Cells[(parametros[i])].Value);

                    }

                    cmd.ExecuteNonQuery();
                }

            }
            catch (MySqlException e)
            {
                MessageBox.Show("Código de error:" + e.ErrorCode + " Mensaje:" + e.Message + " ConexionBD ejecutarQuery");
            }
            finally
            {
                mySqlConnection.Close();
                mySqlConnection.Dispose();
            }
        }


        public void insertDataGridViewRowConNombre(DataGridViewRow fila, string[] parametros, string sentenciaSql)
        {

            mySqlConnection = new MySqlConnection(strConnectionString);
            try
            {
                mySqlConnection.Open();

                MySqlCommand cmd = mySqlConnection.CreateCommand();
                cmd.CommandText = sentenciaSql;

                for (int i = 0; i < parametros.Length; i++)
                {
                    cmd.Parameters.AddWithValue(parametros[i], fila.Cells[(parametros[i])].Value);
                }

                cmd.ExecuteNonQuery();

            }
            catch (MySqlException e)
            {
                MessageBox.Show("Código de error:" + e.ErrorCode + " Mensaje:" + e.Message + " ConexionBD ejecutarQuery");
            }
            finally
            {
                mySqlConnection.Close();
                mySqlConnection.Dispose();
            }
        }

        public int insertDataGridViewRow(DataGridViewRow fila, string[] parametros, string sentenciaSql)
        {
            int resultado = -1;
            mySqlConnection = new MySqlConnection(strConnectionString);
            try
            {
                mySqlConnection.Open();

                MySqlCommand cmd = mySqlConnection.CreateCommand();
                cmd.CommandText = sentenciaSql;

                for (int i = 0; i < parametros.Length; i++)
                {
                    cmd.Parameters.AddWithValue(parametros[i], fila.Cells[i].Value);

                }

                cmd.ExecuteNonQuery();
                return resultado;
            }
            catch (MySqlException e)
            {

                resultado = e.ErrorCode;
                return resultado;
            }
            finally
            {
                mySqlConnection.Close();
                mySqlConnection.Dispose();
            }
        }

        public void insertArray(string[,] valores, string sentenciaSql, string[] parametros)
        {
            mySqlConnection = new MySqlConnection(strConnectionString);
            try
            {
                mySqlConnection.Open();
                MySqlCommand cmd = mySqlConnection.CreateCommand();
                for (int z = 0; z < valores.GetLength(0); z++) {
                    cmd = mySqlConnection.CreateCommand();
                    cmd.CommandText = sentenciaSql;
                    for (int x = 0; x < valores.GetLength(1); x++) {
                        cmd.Parameters.AddWithValue(parametros[x], valores[z, x]);
                    }
                }
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException e)
            {
                MessageBox.Show("Código de error:" + e.ErrorCode + " Mensaje:" + e.Message + " ConexionBD ejecutarQuery");
                
            }
            finally
            {
                mySqlConnection.Close();
                mySqlConnection.Dispose();
            }
        }

  
    }
}