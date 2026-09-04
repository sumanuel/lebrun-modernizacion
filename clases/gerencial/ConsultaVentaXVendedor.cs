using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;
using lebrun.clasesData;
using lebrun.formularios.productos;
using lebrun.formularios.gerencial;
using System.Windows.Forms;
using System.Configuration;


namespace lebrun.clases.gerencial
{
    class ConsultaVentaXVendedor
    {
        public lbxCosultaVentaXVendedor ConsultaVentaVendedor;
        private ConexionBD dataBaseConection;
        private MySqlDataReader dr;
        private string senteciaSql;
        
        //private DataTable dt;
        int i = 0;
        int a = 0;
        int j = 0;
        int k = 0;
        int c = 0;
        double result1;
        double result2;
        //totales General
        private double totalVenDia;
        private double totalVenSem;
        private double totalVenMes;
        private double totalVenAño;
        private double totalVen3Mes;
        private double totalVen6Mes;
        private double totalVen9Mes;
        private double totalVen12Mes;
        //Porcentage General
        private double porcenGenDia;
        private double porcenGenSem;
        private double porcenGenMes;
        private double porcenGenAño;
        private double porcenGen3Mes;
        private double porcenGen6Mes;
        private double porcenGen9Mes;
        private double porcenGen12Mes;
       
        //totales Vendedor 
        private double totalVenDiaV;
        private double totalVenSemV;
        private double totalVenMesV;
        private double totalVenAñoV;
        private double totalVen3MesV;
        private double totalVen6MesV;
        private double totalVen9MesV;
        private double totalVen12MesV;
        //Porcentage Vendedor;

        private double porcenGenDiaV;
        private double porcenGenSemV;
        private double porcenGenMesV;
        private double porcenGenAñoV;
        private double porcenGen3MesV;
        private double porcenGen6MesV;
        private double porcenGen9MesV;
        private double porcenGen12MesV;

        //totales Filial
        private double totalVenDiaT;
        private double totalVenSemT;
        private double totalVenMesT;
        private double totalVenAñoT;
        private double totalVen3MesT;
        private double totalVen6MesT;
        private double totalVen9MesT;
        private double totalVen12MesT;
        
        //Porcentage Filial

        private double porcenGenDiaT;
        private double porcenGenSemT;
        private double porcenGenMesT;
        private double porcenGenAñoT;
        private double porcenGen3MesT;
        private double porcenGen6MesT;
        private double porcenGen9MesT;
        private double porcenGen12MesT;

       // Procetaje dia Seleccionado
        private double porcenGenDiaSelec;
        private double porcenGenDiaSelecV;
        private double porcenGenDiaSelecF;
        private double porcenGenMesSelec;
        private double porcenGenMesSelecV;
        private double porcenGenMesSelecF;
        private double porcenGenAñoSelec;
        private double porcenGenAñoSelecV;
        private double porcenGenAñoSelecF;

        // Procetaje dia Seleccionado get set
        public double PorcenGenAñoSelecF
        {
            get { return porcenGenAñoSelecF; }
            set { porcenGenAñoSelecF = value; }
        }
        public double PorcenGenAñoSelecV
        {
            get { return porcenGenAñoSelecV; }
            set { porcenGenAñoSelecV = value; }
        }
        public double PorcenGenAñoSelec
        {
            get { return porcenGenAñoSelec; }
            set { porcenGenAñoSelec = value; }
        }
        public double PorcenGenMesSelecF
        {
            get { return porcenGenMesSelecF; }
            set { porcenGenMesSelecF = value; }
        }

        public double PorcenGenMesSelecV
        {
            get { return porcenGenMesSelecV; }
            set { porcenGenMesSelecV = value; }
        }
        public double PorcenGenMesSelec
        {
            get { return porcenGenMesSelec; }
            set { porcenGenMesSelec = value; }
        }
        public double PorcenGenDiaSelecV
        {
            get { return porcenGenDiaSelecV; }
            set { porcenGenDiaSelecV = value; }
        }
        public double PorcenGenDiaSelec
        {
            get { return porcenGenDiaSelec; }
            set { porcenGenDiaSelec = value; }
        }
        public double PorcenGenDiaSelecF
        {
            get { return porcenGenDiaSelecF; }
            set { porcenGenDiaSelecF = value; }
        }

        //General
        public double TotalVenDia
        {
            get { return totalVenDia; }
            set { totalVenDia = value; }
        }
        public double TotalVenSem
        {
            get { return totalVenSem; }
            set { totalVenSem = value; }
        }
        public double TotalVenMes
        {
            get { return totalVenMes; }
            set { totalVenMes = value; }
        }
        public double TotalVen3Mes
        {
            get { return totalVen3Mes; }
            set { totalVen3Mes = value; }
        }
        public double TotalVen6Mes
        {
            get { return totalVen6Mes; }
            set { totalVen6Mes = value; }
        }
        public double TotalVen9Mes
        {
            get { return totalVen9Mes; }
            set { totalVen9Mes = value; }
        }
        public double TotalVen12Mes
        {
            get { return totalVen12Mes; }
            set { totalVen12Mes = value; }
        }
        public double PorcenGenDia
        {
            get { return porcenGenDia; }
            set { porcenGenDia = value; }
        }
        public double PorcenGenSem
        {
            get { return porcenGenSem; }
            set { porcenGenSem = value; }
        }
        public double PorcenGenMes
        {
            get { return porcenGenMes; }
            set { porcenGenMes = value; }
        }
        public double PorcenGenAño
        {
            get { return porcenGenAño; }
            set { porcenGenAño = value; }
        }
        public double PorcenGen3Mes
        {
            get { return porcenGen3Mes; }
            set { porcenGen3Mes = value; }
        }
        public double PorcenGen6Mes
        {
            get { return porcenGen6Mes; }
            set { porcenGen6Mes = value; }
        }
        public double PorcenGen9Mes
        {
            get { return porcenGen9Mes; }
            set { porcenGen9Mes = value; }
        }
        public double PorcenGen12Mes
        {
            get { return porcenGen12Mes; }
            set { porcenGen12Mes = value; }
        }



        //Fillial
        public double TotalVenDiaT 
        {
            get { return totalVenDiaT; }
            set { totalVenDiaT = value; }
        }
        public double TotalVenSemT
        {
            get { return totalVenSemT; }
            set { totalVenSemT = value; }
        }
        public double TotalVenMesT
        {
            get { return totalVenMesT; }
            set { totalVenMesT = value; }
        }
        public double TotalVen3MesT
        {
            get { return totalVen3MesT; }
            set { totalVen3MesT = value; }
        }
        public double TotalVen6MesT
        {
            get { return totalVen6MesT; }
            set { totalVen6MesT = value; }
        }
        public double TotalVen9MesT
        {
            get { return totalVen9MesT; }
            set { totalVen9MesT = value; }
        }
        public double TotalVen12MesT
        {
            get { return totalVen12MesT; }
            set { totalVen12MesT = value; }
        }
        public double PorcenGenDiaT
        {
            get { return porcenGenDiaT; }
            set { porcenGenDiaT = value; }
        }
        public double PorcenGenSemT
        {
            get { return porcenGenSemT; }
            set { porcenGenSemT = value; }
        }
        public double PorcenGenMesT
        {
            get { return porcenGenMesT; }
            set { porcenGenMesT = value; }
        }
        public double PorcenGenAñoT
        {
            get { return porcenGenAñoT; }
            set { porcenGenAñoT = value; }
        }
        public double PorcenGen3MesT
        {
            get { return porcenGen3MesT; }
            set { porcenGen3MesT = value; }
        }
        public double PorcenGen6MesT
        {
            get { return porcenGen6MesT; }
            set { porcenGen6MesT = value; }
        }
        public double PorcenGen9MesT
        {
            get { return porcenGen9MesT; }
            set { porcenGen9MesT = value; }
        }
        public double PorcenGen12MesT
        {
            get { return porcenGen12MesT; }
            set { porcenGen12MesT = value; }
        }
          

        //vendedor
        public double TotalVenDiaV
        {
            get { return totalVenDiaV; }
            set { totalVenDiaV = value; }
        }
        public double TotalVenSemV
        {
            get { return totalVenSemV; }
            set { totalVenSemV = value; }
        }
        public double TotalVenMesV
        {
            get { return totalVenMesV; }
            set { totalVenMesV = value; }
        }
        public double TotalVen3MesV
        {
            get { return totalVen3MesV; }
            set { totalVen3MesV = value; }
        }
        public double TotalVen6MesV
        {
            get { return totalVen6MesV; }
            set { totalVen6MesV = value; }
        }
        public double TotalVen9MesV
        {
            get { return totalVen9MesV; }
            set { totalVen9MesV = value; }
        }
        public double TotalVen12MesV
        {
            get { return totalVen12MesV; }
            set { totalVen12MesV = value; }
        }
        public double PorcenGenDiaV
        {
            get { return porcenGenDiaV; }
            set { porcenGenDiaV = value; }
        }
        public double PorcenGenSemV
        {
            get { return porcenGenSemV; }
            set { porcenGenSemV = value; }
        }
        public double PorcenGenMesV
        {
            get { return porcenGenMesV; }
            set { porcenGenMesV = value; }
        }
        public double PorcenGenAñoV
        {
            get { return porcenGenAñoV; }
            set { porcenGenAñoV = value; }
        }
        public double PorcenGen3MesV
        {
            get { return porcenGen3MesV; }
            set { porcenGen3MesV = value; }
        }
        public double PorcenGen6MesV
        {
            get { return porcenGen6MesV; }
            set { porcenGen6MesV = value; }
        }
        public double PorcenGen9MesV
        {
            get { return porcenGen9MesV; }
            set { porcenGen9MesV = value; }
        }
        public double PorcenGen12MesV
        {
            get { return porcenGen12MesV; }
            set { porcenGen12MesV = value; }
        }

        string[] Registro = new string[18];

        public string[] Registros
        {
            get { return Registro; }
            set { Registro = value; }
        }
        string[] datos = new string[4];

        public string[] Datos
        {
            get { return datos; }
            set { datos = value; }
        }





        public ConsultaVentaXVendedor()
        {
            dataBaseConection = new ConexionBD();
            ConsultaVentaVendedor = new lbxCosultaVentaXVendedor();
        }
        public void cargarDataGridAño(DataGridView dv, DataGridView dv1, DataGridView dv2, string fecha)
        {
            k = 0;
            i = 0;
            senteciaSql = null;
            senteciaSql = "SELECT tra_codigo,tra_nombre, " +
              "SUM(IF(dcli_tipdoc ='FAV' AND YEAR(dcli_fchven) = YEAR('" + fecha + "'),dcli_baseneta,0) + IF(dcli_tipdoc ='DEV' AND YEAR(dcli_fchven) = YEAR('" + fecha + "'),dcli_baseneta*-1,0)) AS monto_VENTA_ANOACT," +
              "SUM(IF(dcli_tipdoc ='FAV' AND YEAR(dcli_fchven) = YEAR('" + fecha + "'),dcli_cosfac,0) + IF(dcli_tipdoc ='DEV' AND YEAR(dcli_fchven) = YEAR('" + fecha + "'),dcli_cosfac*-1,0)) AS monto_COSTO_ANOACT " +
               "FROM admtrabajador LEFT JOIN admdoccli ON tra_codigo=dcli_codven WHERE tra_cargo='VENDEDOR' AND dcli_codven > '0' GROUP BY tra_codigo ORDER BY tra_codigo;";
            dr = dataBaseConection.ejecutarQueryDr(senteciaSql);
            datos[0] = "tra_codigo";
            datos[1] = "tra_nombre";
            datos[2] = "monto_VENTA_ANOACT";
            datos[3] = "monto_COSTO_ANOACT";
            while (dr.Read())
            {
                if (Convert.ToString(dv.Rows[i].Cells[0].Value) == dr[Datos[0]].ToString())
                {
                    dv.Rows[i].Cells[8].Value = Convert.ToDecimal(dr[Datos[2]].ToString());
                    
                    if (Convert.ToInt32(dr[Datos[0]].ToString()) < 80)
                    {
                        dv1.Rows[i].Cells[8].Value = Convert.ToDecimal(dr[Datos[2]].ToString());
                        
                    }
                    if (Convert.ToInt32(dr[Datos[0]].ToString()) > 79)
                    {
                        dv2.Rows[k].Cells[8].Value = Convert.ToDecimal(dr[Datos[2]].ToString());
                        
                    }
                    result1 = Convert.ToDouble(dr[Datos[2]].ToString());
                    result2 = Convert.ToDouble(dr[Datos[3]].ToString());
                    if (result1 != Convert.ToDouble(0.00))
                    {
                        dv.Rows[i].Cells[9].Value = Convert.ToDecimal(((result1 - result2) * 100.00) / result1);
                        porcenGenAñoSelec += result2;
                       
                        if (Convert.ToInt32(dr[Datos[0]].ToString()) < 80)
                        {
                            dv1.Rows[i].Cells[9].Value = Convert.ToDecimal(((result1 - result2) * 100.00) / result1);
                            porcenGenAñoSelecV += result2;
                           
                        }
                        if (Convert.ToInt32(dr[Datos[0]].ToString()) > 79)
                        {
                            dv2.Rows[k].Cells[9].Value = Convert.ToDecimal(((result1 - result2) * 100.00) / result1);
                            porcenGenAñoSelecF += result2;
                            k++;
                        }
                    }
                    else
                    {
                        dv.Rows[i].Cells[9].Value = Convert.ToDecimal(((result1 - result2) * 100.00) / 1);
                        porcenGenAñoSelec += result2;
                        if (Convert.ToInt32(dr[Datos[0]].ToString()) < 80)
                        {
                            dv1.Rows[i].Cells[9].Value = Convert.ToDecimal(((result1 - result2) * 100.00) / 1);
                            porcenGenAñoSelecV += result2;
                        }
                        if (Convert.ToInt32(dr[Datos[0]].ToString()) > 79)
                        {
                            dv2.Rows[k].Cells[9].Value = Convert.ToDecimal(((result1 - result2) * 100.00) / 1);
                            porcenGenAñoSelecF += result2;
                            k++;
                        }
                    }
                   
                }
                i++;
            }

            
        }


        public void cargarDataGridMes(DataGridView dv, DataGridView dv1, DataGridView dv2,string fecha)
        {
            i = 0;
            k = 0;
            senteciaSql = null;
            senteciaSql = "SELECT tra_codigo,tra_nombre, " +
               "SUM(IF(dcli_tipdoc ='FAV' AND MONTH(dcli_fchven) = MONTH('" + fecha + "') AND YEAR(dcli_fchven)=YEAR('" + fecha + "'),dcli_baseneta,0) + IF(dcli_tipdoc ='DEV' AND MONTH(dcli_fchven) = MONTH('" + fecha + "') AND YEAR(dcli_fchven)=YEAR('" + fecha + "'),dcli_baseneta*-1,0)) AS monto_VENTA_MES," +
               "SUM(IF(dcli_tipdoc ='FAV' AND MONTH(dcli_fchven) = MONTH('" + fecha + "') AND YEAR(dcli_fchven)=YEAR('" + fecha + "'),dcli_cosfac,0) + IF(dcli_tipdoc ='DEV' AND MONTH(dcli_fchven) = MONTH('" + fecha + "') AND YEAR(dcli_fchven)=YEAR('" + fecha + "'),dcli_cosfac*-1,0)) AS monto_COSTO_MES " +
               "FROM admtrabajador LEFT JOIN admdoccli ON tra_codigo=dcli_codven WHERE tra_cargo='VENDEDOR' AND dcli_codven > '0' GROUP BY tra_codigo ORDER BY tra_codigo;";
            dr = dataBaseConection.ejecutarQueryDr(senteciaSql);
            datos[0] = "tra_codigo";
            datos[1] = "tra_nombre";
            datos[2] = "monto_VENTA_MES";
            datos[3] = "monto_COSTO_MES";
            while (dr.Read())
            {
                if (Convert.ToString(dv.Rows[i].Cells[0].Value) == dr[Datos[0]].ToString())
                {
                    dv.Rows[i].Cells[6].Value =Convert.ToDecimal(dr[Datos[2]].ToString());
                    if (Convert.ToInt32(dr[Datos[0]].ToString()) < 80)
                    {
                        dv1.Rows[i].Cells[6].Value =Convert.ToDecimal(dr[Datos[2]].ToString());
                    }
                    if (Convert.ToInt32(dr[Datos[0]].ToString()) > 79)
                    {
                        dv2.Rows[k].Cells[6].Value = Convert.ToDecimal(dr[Datos[2]].ToString());
                    }
                    result1 = Convert.ToDouble(dr[Datos[2]].ToString());
                    result2 = Convert.ToDouble(dr[Datos[3]].ToString());
                    if (result1 != Convert.ToDouble(0.00))
                    {
                        dv.Rows[i].Cells[7].Value = Convert.ToDecimal(((result1 - result2) * 100.00) / result1);
                        porcenGenMesSelec += result2;
                        if (Convert.ToInt32(dr[Datos[0]].ToString()) < 80)
                        {
                            dv1.Rows[i].Cells[7].Value = Convert.ToDecimal(((result1 - result2) * 100.00) / result1);
                            porcenGenMesSelecV += result2;
                        }
                        if (Convert.ToInt32(dr[Datos[0]].ToString()) > 79)
                        {
                            dv2.Rows[k].Cells[7].Value = Convert.ToDecimal(((result1 - result2) * 100.00) / result1);
                            porcenGenMesSelecF += result2;
                            k++;
                        }
                    }
                    else
                    {
                        dv.Rows[i].Cells[7].Value = Convert.ToDecimal(((result1 - result2) * 100.00) / 1);
                        porcenGenMesSelec += result2;
                        if (Convert.ToInt32(dr[Datos[0]].ToString()) < 80)
                        {
                            dv1.Rows[i].Cells[7].Value = Convert.ToDecimal(((result1 - result2) * 100.00) / 1);
                            porcenGenMesSelecV += result2;
                        }
                        if (Convert.ToInt32(dr[Datos[0]].ToString()) > 79)
                        {
                            dv2.Rows[k].Cells[7].Value = Convert.ToDecimal(((result1 - result2) * 100.00) / 1);
                            porcenGenMesSelecF += result2;
                            k++;
                        }
                    }
                    
                }
                i++;
            }


        }
        public void cargarDataGridDia(DataGridView dv, DataGridView dv1, DataGridView dv2,string fecha)
        {
            i = 0;
            k = 0;
            senteciaSql = null;
            senteciaSql = "SELECT tra_codigo,tra_nombre, " +
               "SUM(IF(dcli_tipdoc ='FAV' AND dcli_fecha = '" + fecha + "',dcli_baseneta,0) + IF(dcli_tipdoc ='DEV' AND dcli_fecha = '" + fecha + "',dcli_baseneta*-1,0)) AS monto_VENTA_Dia," +
               "SUM(IF(dcli_tipdoc ='FAV' AND dcli_fecha = '" + fecha + "',dcli_cosfac,0) + IF(dcli_tipdoc ='DEV' AND dcli_fecha = '" + fecha + "',dcli_cosfac*-1,0)) AS monto_COSTO_Dia " +
               "FROM admtrabajador LEFT JOIN admdoccli ON tra_codigo=dcli_codven WHERE tra_cargo='VENDEDOR' AND dcli_codven > '0' GROUP BY tra_codigo ORDER BY tra_codigo;";
            dr = dataBaseConection.ejecutarQueryDr(senteciaSql);
            datos[0] = "tra_codigo";
            datos[1] = "tra_nombre";
            datos[2] = "monto_VENTA_Dia";
            datos[3] = "monto_COSTO_Dia";
            while (dr.Read())
            {
                if (Convert.ToString(dv.Rows[i].Cells[0].Value) == dr[Datos[0]].ToString())
                {
                    dv.Rows[i].Cells[2].Value = Convert.ToDecimal(dr[Datos[2]].ToString());
                    if (Convert.ToInt32(dr[Datos[0]].ToString()) < 80)
                    {
                        dv1.Rows[i].Cells[2].Value = Convert.ToDecimal(dr[Datos[2]].ToString());
                    }
                    if (Convert.ToInt32(dr[Datos[0]].ToString()) > 79)
                    {
                        dv2.Rows[k].Cells[2].Value = Convert.ToDecimal(dr[Datos[2]].ToString());
                    }
                     result1 = Convert.ToDouble(dr[Datos[2]].ToString());
                     result2 = Convert.ToDouble(dr[Datos[3]].ToString());
                     if (result1 != Convert.ToDouble(0.00))
                        {
                                dv.Rows[i].Cells[3].Value = Convert.ToDecimal(((result1 - result2) * 100.00) / result1);
                                porcenGenDiaSelec += result2;
                                if (Convert.ToInt32(dr[Datos[0]].ToString()) < 80)
                                {
                                    dv1.Rows[i].Cells[3].Value = Convert.ToDecimal(((result1 - result2) * 100.00) / result1);
                                    porcenGenDiaSelecV += result2;
                                }
                                if (Convert.ToInt32(dr[Datos[0]].ToString()) > 79)
                                {
                                    dv2.Rows[k].Cells[3].Value = Convert.ToDecimal(((result1 - result2) * 100.00) / result1);
                                    porcenGenDiaSelecF += result2;
                                    k++;
                                }
                        }
                     else
                        {
                                dv.Rows[i].Cells[3].Value = Convert.ToDecimal(((result1 - result2) * 100.00) / 1);
                                porcenGenDiaSelec += result2;
                                if (Convert.ToInt32(dr[Datos[0]].ToString()) < 80)
                                {
                                    dv1.Rows[i].Cells[3].Value = Convert.ToDecimal(((result1 - result2) * 100.00) / 1);
                                    porcenGenDiaSelecV += result2;
                                }
                                if (Convert.ToInt32(dr[Datos[0]].ToString()) > 79)
                                {
                                    dv2.Rows[k].Cells[3].Value = Convert.ToDecimal(((result1 - result2) * 100.00) / 1);
                                    porcenGenDiaSelecF += result2;
                                    k++;
                                }
                        }
                }
                i++;
            }
            
            
        }
        public void cargarDataGrid(DataGridView dv,DataGridView dv1,DataGridView dv2) 
        {
            i = 0;
            k = 0;
            int dia1 =Convert.ToInt32(DateTime.Now.ToString("dd"));
            senteciaSql = null;
            senteciaSql = "SELECT tra_codigo,tra_nombre, " +
               "SUM(IF(dcli_tipdoc ='FAV' AND dcli_fecha = CURDATE(),dcli_baseneta,0) + IF(dcli_tipdoc ='DEV' AND dcli_fecha = CURDATE(),dcli_baseneta*-1,0)) AS monto_VENTA_Dia," +
               "SUM(IF(dcli_tipdoc ='FAV' AND dcli_fecha = CURDATE(),dcli_cosfac,0) + IF(dcli_tipdoc ='DEV' AND dcli_fecha = CURDATE(),dcli_cosfac*-1,0)) AS monto_COSTO_Dia," +
               "SUM(IF(dcli_tipdoc ='FAV' AND WEEK(dcli_fecha, 0) = WEEK(CURDATE(), 0) AND MONTH(dcli_fecha)=MONTH(CURDATE()) AND YEAR(dcli_fecha)=YEAR(CURDATE()),dcli_baseneta,0) + IF(dcli_tipdoc ='DEV' AND WEEK(dcli_fecha, 0) = WEEK(CURDATE(), 0) AND MONTH(dcli_fecha)=MONTH(CURDATE()) AND YEAR(dcli_fecha)=YEAR(CURDATE()),dcli_baseneta*-1,0)) AS monto_VENTA_SEMANA," +
               "SUM(IF(dcli_tipdoc ='FAV' AND WEEK(dcli_fecha, 0) = WEEK(CURDATE(), 0) AND MONTH(dcli_fecha)=MONTH(CURDATE()) AND YEAR(dcli_fecha)=YEAR(CURDATE()),dcli_cosfac,0) + IF(dcli_tipdoc ='DEV' AND WEEK(dcli_fecha, 0) = WEEK(CURDATE(), 0) AND MONTH(dcli_fecha)=MONTH(CURDATE()) AND YEAR(dcli_fecha)=YEAR(CURDATE()),dcli_cosfac*-1,0)) AS monto_COSTO_SEMANA," +
               "SUM(IF(dcli_tipdoc ='FAV' AND MONTH(dcli_fchven) = MONTH(CURDATE()) AND YEAR(dcli_fchven)=YEAR(CURDATE()),dcli_baseneta,0) + IF(dcli_tipdoc ='DEV' AND MONTH(dcli_fchven) = MONTH(CURDATE()) AND YEAR(dcli_fchven)=YEAR(CURDATE()),dcli_baseneta*-1,0)) AS monto_VENTA_MES," +
               "SUM(IF(dcli_tipdoc ='FAV' AND MONTH(dcli_fchven) = MONTH(CURDATE()) AND YEAR(dcli_fchven)=YEAR(CURDATE()),dcli_cosfac,0) + IF(dcli_tipdoc ='DEV' AND MONTH(dcli_fchven) = MONTH(CURDATE()) AND YEAR(dcli_fchven)=YEAR(CURDATE()),dcli_cosfac*-1,0)) AS monto_COSTO_MES," +
               "SUM(IF(dcli_tipdoc ='FAV' AND YEAR(dcli_fchven) = YEAR(CURDATE()),dcli_baseneta,0) + IF(dcli_tipdoc ='DEV' AND YEAR(dcli_fchven) = YEAR(CURDATE()),dcli_baseneta*-1,0)) AS monto_VENTA_ANOACT,SUM(IF(dcli_tipdoc ='FAV' AND YEAR(dcli_fchven) = YEAR(CURDATE()),dcli_cosfac,0) + IF(dcli_tipdoc ='DEV' AND YEAR(dcli_fchven) = YEAR(CURDATE()),dcli_cosfac*-1,0)) AS monto_COSTO_ANOACT," +
               "SUM(IF(dcli_tipdoc ='FAV' AND dcli_fchven BETWEEN '" + (DateTime.Now.AddMonths(-3)).ToString("yyyy-MM-01") + "' AND '" + (DateTime.Now.AddDays(-dia1)).ToString("yyyy-MM-dd") + "',dcli_baseneta,0) + IF(dcli_tipdoc ='DEV' AND dcli_fchven BETWEEN '" + (DateTime.Now.AddMonths(-3)).ToString("yyyy-MM-01") + "' AND '" + (DateTime.Now.AddDays(-dia1)).ToString("yyyy-MM-dd") + "',dcli_baseneta*-1,0)) AS monto_VENTA_3MESES," +
               "SUM(IF(dcli_tipdoc ='FAV' AND dcli_fchven BETWEEN '" + (DateTime.Now.AddMonths(-3)).ToString("yyyy-MM-01") + "' AND '" + (DateTime.Now.AddDays(-dia1)).ToString("yyyy-MM-dd") + "',dcli_cosfac,0) + IF(dcli_tipdoc ='DEV' AND dcli_fchven BETWEEN '" + (DateTime.Now.AddMonths(-3)).ToString("yyyy-MM-01") + "' AND '" + (DateTime.Now.AddDays(-dia1)).ToString("yyyy-MM-dd") + "',dcli_cosfac*-1,0)) AS monto_COSTO_3MESES," +
               "SUM(IF(dcli_tipdoc ='FAV' AND dcli_fchven BETWEEN '" + (DateTime.Now.AddMonths(-6)).ToString("yyyy-MM-01") + "' AND '" + (DateTime.Now.AddDays(-dia1)).ToString("yyyy-MM-dd") + "',dcli_baseneta,0) + IF(dcli_tipdoc ='DEV' AND dcli_fchven BETWEEN '" + (DateTime.Now.AddMonths(-6)).ToString("yyyy-MM-01") + "' AND '" + (DateTime.Now.AddDays(-dia1)).ToString("yyyy-MM-dd") + "',dcli_baseneta*-1,0)) AS monto_VENTA_6MESES," +
               "SUM(IF(dcli_tipdoc ='FAV' AND dcli_fchven BETWEEN '" + (DateTime.Now.AddMonths(-6)).ToString("yyyy-MM-01") + "' AND '" + (DateTime.Now.AddDays(-dia1)).ToString("yyyy-MM-dd") + "',dcli_cosfac,0) + IF(dcli_tipdoc ='DEV' AND dcli_fchven BETWEEN '" + (DateTime.Now.AddMonths(-6)).ToString("yyyy-MM-01") + "' AND '" + (DateTime.Now.AddDays(-dia1)).ToString("yyyy-MM-dd") + "',dcli_cosfac*-1,0)) AS monto_COSTO_6MESES, " +
               "SUM(IF(dcli_tipdoc ='FAV' AND dcli_fchven BETWEEN '" + (DateTime.Now.AddMonths(-9)).ToString("yyyy-MM-01") + "' AND '" + (DateTime.Now.AddDays(-dia1)).ToString("yyyy-MM-dd") + "',dcli_baseneta,0) + IF(dcli_tipdoc ='DEV' AND dcli_fchven BETWEEN '" + (DateTime.Now.AddMonths(-9)).ToString("yyyy-MM-01") + "' AND '" + (DateTime.Now.AddDays(-dia1)).ToString("yyyy-MM-dd") + "',dcli_baseneta*-1,0)) AS monto_VENTA_9MESES," +
               "SUM(IF(dcli_tipdoc ='FAV' AND dcli_fchven BETWEEN '" + (DateTime.Now.AddMonths(-9)).ToString("yyyy-MM-01") + "' AND '" + (DateTime.Now.AddDays(-dia1)).ToString("yyyy-MM-dd") + "',dcli_cosfac,0) + IF(dcli_tipdoc ='DEV' AND dcli_fchven BETWEEN '" + (DateTime.Now.AddMonths(-9)).ToString("yyyy-MM-01") + "' AND '" + (DateTime.Now.AddDays(-dia1)).ToString("yyyy-MM-dd") + "',dcli_cosfac*-1,0)) AS monto_COSTO_9MESES,"+
               "SUM(IF(dcli_tipdoc ='FAV' AND dcli_fchven BETWEEN '" + (DateTime.Now.AddMonths(-12)).ToString("yyyy-MM-01") + "' AND '" + (DateTime.Now.AddDays(-dia1)).ToString("yyyy-MM-dd") + "',dcli_baseneta,0) + IF(dcli_tipdoc ='DEV' AND dcli_fchven BETWEEN '" + (DateTime.Now.AddMonths(-12)).ToString("yyyy-MM-01") + "' AND '" + (DateTime.Now.AddDays(-dia1)).ToString("yyyy-MM-dd") + "',dcli_baseneta*-1,0)) AS monto_VENTA_12MESES," +
               "SUM(IF(dcli_tipdoc ='FAV' AND dcli_fchven BETWEEN '" + (DateTime.Now.AddMonths(-12)).ToString("yyyy-MM-01") + "' AND '" + (DateTime.Now.AddDays(-dia1)).ToString("yyyy-MM-dd") + "',dcli_cosfac,0) + IF(dcli_tipdoc ='DEV' AND dcli_fchven BETWEEN '" + (DateTime.Now.AddMonths(-12)).ToString("yyyy-MM-01") + "' AND '" + (DateTime.Now.AddDays(-dia1)).ToString("yyyy-MM-dd") + "',dcli_cosfac*-1,0)) AS monto_COSTO_12MESES " +
              "FROM admtrabajador LEFT JOIN admdoccli ON tra_codigo=dcli_codven WHERE tra_cargo='VENDEDOR' AND dcli_codven > '0' GROUP BY tra_codigo ORDER BY tra_codigo;";
            dr = dataBaseConection.ejecutarQueryDr(senteciaSql);
            Registro[0]  = "tra_codigo";
            Registro[1]  = "tra_nombre";
            Registro[2]  = "monto_VENTA_Dia";
            Registro[3]  = "monto_COSTO_Dia";
            Registro[4]  = "monto_VENTA_SEMANA";
            Registro[5]  = "monto_COSTO_SEMANA";
            Registro[6]  = "monto_VENTA_MES";
            Registro[7]  = "monto_COSTO_MES";
            Registro[8]  = "monto_VENTA_ANOACT";
            Registro[9]  = "monto_COSTO_ANOACT";
            Registro[10] = "monto_VENTA_3MESES";
            Registro[11] = "monto_VENTA_6MESES";
            Registro[12] = "monto_VENTA_9MESES";
            Registro[13] = "monto_VENTA_12MESES";
            Registro[14] = "monto_COSTO_3MESES";
            Registro[15] = "monto_COSTO_6MESES";
            Registro[16] = "monto_COSTO_9MESES";
            Registro[17] = "monto_COSTO_12MESES";
            while (dr.Read())
            {
                j = 0;
                a = 0;
                
                dv.Rows.Add();
                if (Convert.ToInt32(dr[Registros[0]].ToString())< 80)
                {
                    dv1.Rows.Add();
                }
                if (Convert.ToInt32(dr[Registros[0]].ToString()) > 79)
                {
                    dv2.Rows.Add();
                }
              

                    for (j = 0; j <= 13; j++)
                    {
                        result1 = 0;
                        result2 = 0;
                        if (Registros[a] == Registros[0])
                        {
                            dv.Rows[i].Cells[j].Value = dr[Registros[a]].ToString();
                            if (Convert.ToInt32(dr[Registros[0]].ToString()) < 80 )
                            {
                                dv1.Rows[i].Cells[j].Value = dr[Registros[a]].ToString();
                            }
                            if (Convert.ToInt32(dr[Registros[0]].ToString())  > 79)
                            {
                                dv2.Rows.Add();
                                dv2.Rows[k].Cells[j].Value = dr[Registros[a]].ToString();
                            }
                        }
                        if (Registros[a] == Registros[1])
                        {
                            dv.Rows[i].Cells[j].Value = dr[Registros[a]].ToString();
                            if (Convert.ToInt32(dr[Registros[0]].ToString()) < 80)
                            {
                                dv1.Rows[i].Cells[j].Value = dr[Registros[a]].ToString();
                            }
                            if (Convert.ToInt32(dr[Registros[0]].ToString()) > 79)
                            {
                                dv2.Rows.Add();
                                dv2.Rows[k].Cells[j].Value = dr[Registros[a]].ToString();
                            }
                        }
                        if (Registros[a] == Registros[2]) 
                        {
                             dv.Rows[i].Cells[j].Value = Convert.ToDecimal(dr[Registros[a]].ToString());
                             if (Convert.ToInt32(dr[Registros[0]].ToString()) < 80)
                             {
                                 dv1.Rows[i].Cells[j].Value = Convert.ToDecimal(dr[Registros[a]].ToString());
                             }
                             if (Convert.ToInt32(dr[Registros[0]].ToString()) > 79)
                             {

                                 dv2.Rows[k].Cells[j].Value = Convert.ToDecimal(dr[Registros[a]].ToString());
                             }
                        }
                        if (Registros[a] == Registros[3])
                        {
                            result1 = Convert.ToDouble(dr[Registros[2]].ToString());
                            result2 = Convert.ToDouble(dr[Registros[3]].ToString());

                            if (result1 != Convert.ToDouble(0.00))
                            {
                                dv.Rows[i].Cells[j].Value = Convert.ToDecimal(((result1 - result2) * 100.00) / result1);
                                porcenGenDia += result2;
                                if (Convert.ToInt32(dr[Registros[0]].ToString()) < 80)
                                {
                                    dv1.Rows[i].Cells[j].Value = Convert.ToDecimal(((result1 - result2) * 100.00) / result1);
                                    porcenGenDiaV += result2;
                                }
                                if (Convert.ToInt32(dr[Registros[0]].ToString()) > 79)
                                {
                                    dv2.Rows[k].Cells[j].Value = Convert.ToDecimal(((result1 - result2) * 100.00) / result1);
                                    porcenGenDiaT += result2;
                                }
                            }
                            else
                            {
                                dv.Rows[i].Cells[j].Value = Convert.ToDecimal(((result1 - result2) * 100.00) / 1);
                                porcenGenDia += result2;
                                if (Convert.ToInt32(dr[Registros[0]].ToString()) < 80)
                                {
                                    dv1.Rows[i].Cells[j].Value = Convert.ToDecimal(((result1 - result2) * 100.00) / 1);
                                    porcenGenDiaV += result2;
                                }
                                if (Convert.ToInt32(dr[Registros[0]].ToString()) > 79)
                                {
                                    dv2.Rows[k].Cells[j].Value = Convert.ToDecimal(((result1 - result2) * 100.00) / 1);
                                    porcenGenDiaT += result2;
                                }
                            }
                           
                        }
                        ////aqui
                        if (Registros[a] == Registros[4])
                        {
                            dv.Rows[i].Cells[j].Value = Convert.ToDecimal(dr[Registros[a]].ToString());
                            if (Convert.ToInt32(dr[Registros[0]].ToString()) < 80)
                            {
                                dv1.Rows[i].Cells[j].Value = Convert.ToDecimal(dr[Registros[a]].ToString());
                            }
                            if (Convert.ToInt32(dr[Registros[0]].ToString())> 79)
                            {
                                dv2.Rows[k].Cells[j].Value = Convert.ToDecimal(dr[Registros[a]].ToString());
                            }
                        }

                        if (Registros[a] == Registros[5])
                        {
                            result1 = Convert.ToDouble(dr[Registros[4]].ToString());
                            result2 = Convert.ToDouble(dr[Registros[5]].ToString());

                            if (result1 != Convert.ToDouble(0.00))
                            {
                                dv.Rows[i].Cells[j].Value = Convert.ToDecimal(((result1 - result2) * 100.00) / result1);
                                this.porcenGenSem += result2;
                                if (Convert.ToInt32(dr[Registros[0]].ToString()) < 80)
                                {
                                    dv1.Rows[i].Cells[j].Value = Convert.ToDecimal(((result1 - result2) * 100.00) / result1);
                                    this.porcenGenSemV += result2;
                                }
                                if (Convert.ToInt32(dr[Registros[0]].ToString()) > 79)
                                {
                                    dv2.Rows[k].Cells[j].Value = Convert.ToDecimal(((result1 - result2) * 100.00) / result1);
                                    this.porcenGenSemT += result2;
                                }
                            }
                            else
                            {
                                dv.Rows[i].Cells[j].Value = Convert.ToDecimal(((result1 - result2) * 100.00) / 1);
                                if (Convert.ToInt32(dr[Registros[0]].ToString()) < 80)
                                {
                                    dv1.Rows[i].Cells[j].Value = Convert.ToDecimal(((result1 - result2) * 100.00) / 1);
                                }
                                if (Convert.ToInt32(dr[Registros[0]].ToString()) > 79)
                                {
                                    dv2.Rows[k].Cells[j].Value = Convert.ToDecimal(((result1 - result2) * 100.00) / 1);
                                }
                            }
                        }
                        if (Registros[a] == Registros[6])
                        {
                            dv.Rows[i].Cells[j].Value = Convert.ToDecimal(dr[Registros[a]].ToString());
                            if (Convert.ToInt32(dr[Registros[0]].ToString()) < 80)
                            {
                                dv1.Rows[i].Cells[j].Value = Convert.ToDecimal(dr[Registros[a]].ToString());
                            }
                            if (Convert.ToInt32(dr[Registros[0]].ToString()) > 79)
                            {
                                dv2.Rows[k].Cells[j].Value = Convert.ToDecimal(dr[Registros[a]].ToString());
                            }
                        }
                        if (Registros[a] == Registros[7])
                        {
                            result1 = Convert.ToDouble(dr[Registros[6]].ToString());
                            result2 = Convert.ToDouble(dr[Registros[7]].ToString());
                            if (result1 != Convert.ToDouble(0.00))
                            {
                                dv.Rows[i].Cells[j].Value = Convert.ToDecimal(((result1 - result2) * 100.00) / result1);
                                this.porcenGenMes += result2;
                                if (Convert.ToInt32(dr[Registros[0]].ToString()) < 80)
                                {
                                    dv1.Rows[i].Cells[j].Value = Convert.ToDecimal(((result1 - result2) * 100.00) / result1);
                                    this.porcenGenMesV += result2;
                                }
                                if (Convert.ToInt32(dr[Registros[0]].ToString()) > 79)
                                {
                                    dv2.Rows[k].Cells[j].Value = Convert.ToDecimal(((result1 - result2) * 100.00) / result1);
                                    this.porcenGenMesT += result2;
                                }
                            }
                            else
                            {
                                dv.Rows[i].Cells[j].Value = Convert.ToDecimal(((result1 - result2) * 100.00) / 1);
                                if (Convert.ToInt32(dr[Registros[0]].ToString()) < 80)
                                {
                                    dv1.Rows[i].Cells[j].Value = Convert.ToDecimal(((result1 - result2) * 100.00) / 1);
                                }
                                if (Convert.ToInt32(dr[Registros[0]].ToString()) > 79)
                                {
                                    dv2.Rows[k].Cells[j].Value = Convert.ToDecimal(((result1 - result2) * 100.00) / 1);
                                }
                            }
                        }
                        if (Registros[a] == Registros[8])
                        {
                            dv.Rows[i].Cells[j].Value = Convert.ToDecimal(dr[Registros[a]].ToString());
                            if (Convert.ToInt32(dr[Registros[0]].ToString()) < 80)
                            {
                                dv1.Rows[i].Cells[j].Value = Convert.ToDecimal(dr[Registros[a]].ToString());
                            }
                            if (Convert.ToInt32(dr[Registros[0]].ToString())  > 79)
                            {
                                dv2.Rows[k].Cells[j].Value = Convert.ToDecimal(dr[Registros[a]].ToString());
                            }
                        }
                        if (Registros[a] == Registros[9])
                        {
                            result1 = Convert.ToDouble(dr[Registros[8]].ToString());
                            result2 = Convert.ToDouble(dr[Registros[9]].ToString());

                            if (result1 != Convert.ToDouble(0.00))
                            {
                                dv.Rows[i].Cells[j].Value = Convert.ToDecimal(((result1 - result2) * 100.00) / result1);
                                this.porcenGenAño += result2;
                                if (Convert.ToInt32(dr[Registros[0]].ToString()) < 80)
                                {
                                    dv1.Rows[i].Cells[j].Value = Convert.ToDecimal(((result1 - result2) * 100.00) / result1);
                                    this.porcenGenAñoV += result2;
                                }
                                if (Convert.ToInt32(dr[Registros[0]].ToString()) > 79)
                                {
                                    dv2.Rows[k].Cells[j].Value = Convert.ToDecimal(((result1 - result2) * 100.00) / result1);
                                    this.porcenGenAñoT += result2;
                                }
                            }
                            else
                            {
                                dv.Rows[i].Cells[j].Value = Convert.ToDecimal(((result1 - result2) * 100.00) / 1);
                                if (Convert.ToInt32(dr[Registros[0]].ToString()) < 80)
                                {
                                    dv1.Rows[i].Cells[j].Value = Convert.ToDecimal(((result1 - result2) * 100.00) / 1);
                                }
                                if (Convert.ToInt32(dr[Registros[0]].ToString()) > 79)
                                {
                                    dv2.Rows[k].Cells[j].Value = Convert.ToDecimal(((result1 - result2) * 100.00) / 1);
                                }
                            }
                        }
                        if (Registros[a] == Registros[10])
                        {
                            dv.Rows[i].Cells[j].Value = Convert.ToDecimal(dr[Registros[a]].ToString());
                            this.porcenGen3Mes += Convert.ToDouble(dr[Registros[14]].ToString());
                            if (Convert.ToInt32(dr[Registros[0]].ToString()) < 80)
                            {
                                dv1.Rows[i].Cells[j].Value = Convert.ToDecimal(dr[Registros[a]].ToString());
                                this.porcenGen3MesV += Convert.ToDouble(dr[Registros[14]].ToString());
                            }
                            if (Convert.ToInt32(dr[Registros[0]].ToString()) > 79)
                            {
                                dv2.Rows[k].Cells[j].Value = Convert.ToDecimal(dr[Registros[a]].ToString());
                                this.porcenGen3MesT += Convert.ToDouble(dr[Registros[14]].ToString());
                            }
                        }
                        if (Registros[a] == Registros[11])
                        {
                            dv.Rows[i].Cells[j].Value = Convert.ToDecimal(dr[Registros[a]].ToString());
                            this.porcenGen6Mes += Convert.ToDouble(dr[Registros[15]].ToString());
                            if (Convert.ToInt32(dr[Registros[0]].ToString()) < 80)
                            {
                                dv1.Rows[i].Cells[j].Value = Convert.ToDecimal(dr[Registros[a]].ToString());
                                this.porcenGen6MesV += Convert.ToDouble(dr[Registros[15]].ToString());
                            }
                            if (Convert.ToInt32(dr[Registros[0]].ToString()) > 79)
                            {
                                dv2.Rows[k].Cells[j].Value = Convert.ToDecimal(dr[Registros[a]].ToString());
                                this.porcenGen6MesT += Convert.ToDouble(dr[Registros[15]].ToString());
                            }
                        }
                        if (Registros[a] == Registros[12])
                        {
                            dv.Rows[i].Cells[j].Value = Convert.ToDecimal(dr[Registros[a]].ToString());
                            this.porcenGen9Mes += Convert.ToDouble(dr[Registros[16]].ToString());
                            if (Convert.ToInt32(dr[Registros[0]].ToString()) < 80)
                            {
                                dv1.Rows[i].Cells[j].Value = Convert.ToDecimal(dr[Registros[a]].ToString());
                                this.porcenGen9MesV += Convert.ToDouble(dr[Registros[16]].ToString());
                            }
                            if (Convert.ToInt32(dr[Registros[0]].ToString()) > 79)
                            {
                                dv2.Rows[k].Cells[j].Value = Convert.ToDecimal(dr[Registros[a]].ToString());
                                this.porcenGen9MesT += Convert.ToDouble(dr[Registros[16]].ToString());
                            }
                        }
                        if (Registros[a] == Registros[13])
                        {
                            dv.Rows[i].Cells[j].Value = Convert.ToDecimal(dr[Registros[a]].ToString());
                            this.porcenGen12Mes += Convert.ToDouble(dr[Registros[17]].ToString());
                            if (Convert.ToInt32(dr[Registros[0]].ToString()) < 80)
                            {
                                dv1.Rows[i].Cells[j].Value = Convert.ToDecimal(dr[Registros[a]].ToString());
                                this.porcenGen12MesV += Convert.ToDouble(dr[Registros[17]].ToString());
                            }
                            if (Convert.ToInt32(dr[Registros[0]].ToString()) > 79)
                            {
                                dv2.Rows[k].Cells[j].Value = Convert.ToDecimal(dr[Registros[a]].ToString());
                                this.porcenGen12MesT += Convert.ToDouble(dr[Registros[17]].ToString());
                                k++;
                            }
                           
                        }
                        a++;
                    }
                    i++;
            }
            i = 0;
            dataBaseConection.cerrarConexion();

        }
    }
}
