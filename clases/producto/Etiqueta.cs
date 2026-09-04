using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
using System.ComponentModel;
using System.Data;
using System.Collections;
using System.Threading;
using System.Net;
using System.Xml;
using lebrun.clases.complementos;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using lebrun.clasesData;

namespace lebrun.clases.producto
{
    class Etiqueta
    {
        private string sIni;
        private string sIni2;
        private string sIni3;
        private ConexionBD databaseConection;
        public Principal SistemaUs;// llamado al la informacion de la pataklla principal
        public string undmed1, undmed2, undmed3, undmed4, undmed5;
        public string inv_codigo1, inv_codigo2, inv_codigo3, inv_codigo4, inv_codigo5;
        public string conten1, conten2, conten3, conten4, conten5;
        public string montob1, montob2, montob3, montob4, montob5;
        public string civa1, civa2, civa3, civa4, civa5;
        public string precio1, precio2, precio3, precio4, precio5;
        public string opcion;
        public string ip;
        public string id;
        public string sentenciaSql;

        public Etiqueta() 
        {
          databaseConection=new ConexionBD();
          SistemaUs = new Principal();

        }
        public string SIni
        {
            get { return sIni; }
            set { sIni = value; }
        }

        public string SIni2
        {
            get { return sIni2; }
            set { sIni2 = value; }
        }

        public string SIni3
        {
            get { return sIni3; }
            set { sIni3 = value; }
        }

        Producto objeto = new Producto();

        public const short FILE_ATTRIBUTE_NORMAL = 0x80;
        public const short INVALID_HANDLE_VALUE = -1;
        public const uint GENERIC_READ = 0x80000000;
        public const uint GENERIC_WRITE = 0x40000000;
        public const uint CREATE_NEW = 1;
        public const uint CREATE_ALWAYS = 2;
        public const uint OPEN_EXISTING = 3;

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr CreateFile(string lpFileName, uint dwDesiredAccess,
            uint dwShareMode, IntPtr lpSecurityAttributes, uint dwCreationDisposition,
            uint dwFlagsAndAttributes, IntPtr hTemplateFile);

        //Metodos para la clase Etiqueta a deltal

        public void GenerarArchivo(string codigo, string contenido, string unidad, string precio,
            string nombre, string rif, string pvj1, string codigo2, string contenido2, string unidad2,
            string precio2, string pvj2, string codigo3, string contenido3, string unidad3, string precio3, string pvj3,
            string codigo4, string contenido4, string unidad4, string precio4, string pvj4, string codigo5,
            string contenido5, string unidad5, string precio5, string pvj5, string fecha, string iva1,int contandor)
        {
            string primera1 = "  " + codigo + " " + contenido + " " + unidad;
            string primera2 = " " + codigo2 + " " + contenido2 + " " + unidad2;
            string primera3 = " " + codigo3 + " " + contenido3 + " " + unidad3;
            string primera4 = " " + codigo4 + " " + contenido4 + " " + unidad4;
            string primera5 = " " + codigo5 + " " + contenido5 + " " + unidad5;
            string justo1 = " " + pvj1 + "" + precio;
            string justo2 = "" + pvj2 + "" + precio2;
            string justo3 = "" + pvj3 + "" + precio3;
            string justo4 = "" + pvj4 + "" + precio4;
            string justo5 = "" + pvj5 + "" + precio5;
            string data = "   " + fecha + "" + iva1;
            string data2 = "   " + fecha + "" + iva1;
            string data3 = "   " + fecha + "" + iva1;
            string data4 = "   " + fecha + "" + iva1;
            string data5 = "   " + fecha + "" + iva1;
            string complete = "" + nombre + " Rif" + rif;
            string complete2 = "" + nombre + " Rif" + rif;
            string complete3 = "" + nombre + " Rif" + rif;
            string complete4 = "" + nombre + " Rif" + rif;
            string complete5 = "" + nombre + " Rif" + rif;
            string liniaBlanco1 = "                    ";
            string liniaBlanco2 = "                    ";
            string liniaBlanco3 = "                    ";
            string liniaBlanco4 = "                    ";
            string liniaBlanco5 = "                    ";

            int a = justo1.Length;
            int b = justo2.Length;
            int c = justo3.Length;
            int d = justo4.Length;
            int e = justo5.Length;

            int a_a = primera1.Length;
            int b_b = primera2.Length;
            int c_c = primera3.Length;
            int d_d = primera4.Length;
            int e_e = primera5.Length;

            int resultado;

            // Write the string to a file.
            this.SIni = Application.StartupPath + "\\Etiqueta1.txt";
            System.IO.StreamWriter file = new System.IO.StreamWriter(sIni);
            if(contandor==1)
            {
                file.Write("\r\n");
            }

            file.Write(primera1);
            if ((a_a > 0) && (a_a != 4))
            {
                for (int i = 0; i <= 11; ++i)
                {
                    file.Write((char)32);
                }
            }
            else if (a_a == 4)
            {
                for (int i = 0; i <= 24; ++i)
                {
                    file.Write((char)32);
                }
            }
            file.Write(primera2);
            if (((a_a > 0) && (a_a != 4)) && (b_b != 3))
            {
                for (int i = 1; i <= 11; ++i)
                {
                    file.Write((char)32);
                }
            }
            else if ((a_a == 4) && ((b_b > 0) && (b_b != 3)))
            {
                for (int i = 1; i <= 11; ++i)
                {
                    file.Write((char)32);
                }
            }
            else if ((a_a > 0) && (a_a != 4) && (b_b == 3))
            {
                for (int i = 1; i <= 24; ++i)
                {
                    file.Write((char)32);
                }
            }

            else if ((a_a == 4) && (b_b == 3))
            {
                for (int i = 1; i <= 24; ++i)
                {
                    file.Write((char)32);
                }
            }

            file.Write(primera3);
            if (((a_a > 0) && (a_a != 4)) && ((b_b > 0) && (b_b != 3)) && ((c_c > 0) && (c_c != 3)))
            {
                for (int i = 1; i <= 11; ++i)
                {
                    file.Write((char)32);
                }
            }

            if (((a_a > 0) && (a_a != 4)) && (b_b == 3) && (c_c == 3))
            {
                for (int i = 1; i <= 24; ++i)
                {
                    file.Write((char)32);
                }
            }

            if (((b_b > 0) && (b_b != 3)) && (a_a == 4) && (c_c == 3))
            {
                for (int i = 1; i <= 24; ++i)
                {
                    file.Write((char)32);
                }
            }
            if (((c_c > 0) && (c_c != 3)) && (a_a == 4) && (b_b == 3))
            {
                for (int i = 1; i <= 11; ++i)
                {
                    file.Write((char)32);
                }
            }
            if (((a_a > 0) && (a_a != 4)) && ((b_b > 0) && (b_b != 3)) && (c_c == 3))
            {
                for (int i = 1; i <= 24; ++i)
                {
                    file.Write((char)32);
                }
            }
            if (((a_a > 0) && (a_a != 4)) && ((c_c > 0) && (c_c != 3)) && (b_b == 3))
            {
                for (int i = 1; i <= 11; ++i)
                {
                    file.Write((char)32);
                }
            }

            if (((b_b > 0) && (b_b != 3)) && ((c_c > 0) && (c_c != 3)) && (a_a == 4))
            {
                for (int i = 1; i <= 11; ++i)
                {
                    file.Write((char)32);
                }
            }
            if ((a_a == 4) && (b_b == 3) && (c_c == 3))
            {
                for (int i = 1; i <= 24; ++i)
                {
                    file.Write((char)32);
                }
            }

            file.Write(primera4); 
            if (((a_a > 0) && (a_a != 4)) && ((b_b > 0) && (b_b != 3)) && ((c_c > 0) && (c_c != 3)) &&
                ((d_d > 0) && (d_d != 3)))
            {
                for (int i = 1; i <= 11; ++i)
                {
                    file.Write((char)32);
                }
            }

            if (((a_a > 0) && (a_a != 4)) && (b_b == 3) && (c_c == 3) && (d_d == 3))
            {
                for (int i = 1; i <= 24; ++i)
                {
                    file.Write((char)32);
                }
            }

            if (((b_b > 0) && (b_b != 3)) && (a_a == 4) && (c_c == 3) && (d_d == 3))
            {
                for (int i = 1; i <= 24; ++i)
                {
                    file.Write((char)32);
                }
            }

            if (((c_c > 0) && (c_c != 3)) && (a_a == 4) && (b_b == 3) && (d_d == 3))
            {
                for (int i = 1; i <= 24; ++i)
                {
                    file.Write((char)32);
                }
            }

            if (((d_d > 0) && (d_d != 3)) && (a_a == 4) && (b_b == 3) && (c_c == 3))
            {
                for (int i = 1; i <= 11; ++i)
                {
                    file.Write((char)32);
                }
            }

            if (((a_a > 0) && (a_a != 4)) && (d_d == 3) && ((b_b > 0) && (b_b != 3)) && (c_c == 3))
            {
                for (int i = 1; i <= 24; ++i)
                {
                    file.Write((char)32);
                }
            }

            if (((a_a > 0) && (a_a != 4)) && (d_d == 3) && ((c_c > 0) && (c_c != 3)) && (b_b == 3))
            {
                for (int i = 1; i <= 24; ++i)
                {
                    file.Write((char)32);
                }
            }

            if (((a_a > 0) && (a_a != 4)) && (c_c == 3) && ((d_d > 0) && (d_d != 3)) && (b_b == 3))
            {
                for (int i = 1; i <= 11; ++i)
                {
                    file.Write((char)32);
                }
            }

            if (((b_b > 0) && (b_b != 3)) && (d_d == 3) && ((c_c > 0) && (c_c != 3)) && (a_a == 4))
            {
                for (int i = 1; i <= 24; ++i)
                {
                    file.Write((char)32);
                }
            }

            if (((b_b > 0) && (b_b != 3)) && (c_c == 3) && ((d_d > 0) && (d_d != 3)) && (a_a == 4))
            {
                for (int i = 1; i <= 11; ++i)
                {
                    file.Write((char)32);
                }
            }

            if (((c_c > 0) && (c_c != 3)) && (b_b == 3) && ((d_d > 0) && (d_d != 3)) && (a_a == 4))
            {
                for (int i = 1; i <= 11; ++i)
                {
                    file.Write((char)32);
                }
            }

            if ((a_a == 4) && (b_b == 3) && (c_c == 3) && (d_d == 3))
            {
                for (int i = 1; i <= 24; ++i)
                {
                    file.Write((char)32);
                }
            }
            if ((a_a == 4) && (b_b != 3) && (c_c != 3) && (d_d != 3) && (c_c != 3) && (b_b != 3) && (d_d!= 3))
             {
                 for (int i = 1; i <= 11; ++i)
                 {
                     file.Write((char)32);
                 }
             }
            if ((a_a != 4) && (b_b == 3) && (c_c != 3) && (d_d != 3) && (c_c != 3) && (b_b == 3) && (d_d != 3))
            {
                for (int i = 1; i <= 11; ++i)
                {
                    file.Write((char)32);
                }
            }
            if ((a_a > 0)&&(a_a != 4) && (b_b != 3) && (c_c == 3) && (d_d != 3) && (c_c == 3) && (b_b != 3) && (d_d != 3))
            {
                for (int i = 1; i <= 11; ++i)
                {
                    file.Write((char)32);
                }
            }
            if ((a_a > 0) && (a_a != 4) && (b_b != 3) && (c_c != 3) && (d_d == 3) && (c_c != 3) && (b_b != 3) && (d_d == 3))
            {
                for (int i = 1; i <= 24; ++i)
                {
                    file.Write((char)32);
                }
            }

            file.Write(primera5);
            file.WriteLine((char)32);
            
            file.Write(justo1);
            resultado = 27 - b;
            resultado = resultado - (a - b);
            for (int i = 1; i <= resultado; ++i)
            {
                file.Write((char)32);
            }
            file.Write(justo2);
            resultado = 27 - c;
            resultado = resultado - (b - c);
            for (int i = 1; i <= resultado; ++i)
            {
                file.Write((char)32);
            }
            file.Write(justo3);
            resultado = 27 - d;
            resultado = resultado - (c - d);
            for (int i = 1; i <= resultado; ++i)
            {
                file.Write((char)32);
            }
            file.Write(justo4);
            resultado = 27 - e;
            resultado = resultado - (d - e);
            for (int i = 1; i <= resultado; ++i)
            {
                file.Write((char)32);
            }
            file.Write(justo5);
            file.WriteLine((char)32);
//aqui
            if (string.IsNullOrEmpty(codigo))
            {
                if ((string.IsNullOrEmpty(codigo) && string.IsNullOrEmpty(codigo5)) && (!(string.IsNullOrEmpty(codigo2)&&(string.IsNullOrEmpty(codigo3)&&(string.IsNullOrEmpty(codigo4))))))
                {
                    data = "                 ";
                    file.Write(data);
                    for (int i = 1; i <= 8; ++i)
                    {
                        file.Write((char)32);
                    }
                } 
                else
                {
                    data = "                  ";
                    file.Write(data);
                    for (int i = 1; i <= 8; ++i)
                    {
                        file.Write((char)32);
                    }
                }  
            }
            else
            {
                file.Write(data);
                for (int i = 1; i <= 8; ++i)
                {
                    file.Write((char)32);
                }
            }
            if (string.IsNullOrEmpty(codigo2))
            {
                data2 = "                  ";
                    file.Write(data2);
                    for (int i = 1; i <= 8; ++i)
                    {
                        file.Write((char)32);
                    }
            }
            else
            {
                file.Write(data2);
                for (int i = 1; i <= 9; ++i)
                {
                    file.Write((char)32);
                }
            }
            if (string.IsNullOrEmpty(codigo3))
            {
                data3 = "                  ";
                file.Write(data3);
                for (int i = 1; i <= 9; ++i)
                {
                    file.Write((char)32);
                }
            }
            else
            {
                file.Write(data3);
                for (int i = 1; i <= 9; ++i)
                {
                    file.Write((char)32);
                }
            }
            
            if (string.IsNullOrEmpty(codigo4))
            {
                data4 = "                  ";
                file.Write(data4);
                for (int i = 1; i <= 9; ++i)
                {
                    file.Write((char)32);
                }
            }
            else
            {
                file.Write(data4);
                for (int i = 1; i <= 9; ++i)
                {
                    file.Write((char)32);
                }
            }
            
            
            if (string.IsNullOrEmpty(codigo5))
            {
                data5 = "                  ";
                file.Write(data5);
                for (int i = 1; i <= 9; ++i)
                {
                    file.Write((char)32);
                }
            }
            else
            {
                file.Write(data5);
                for (int i = 1; i <= 9; ++i)
                {
                    file.Write((char)32);
                }
            }
          //if (string.IsNullOrEmpty(codigo))
          //{
          //    file.Write("\r\n");
          //    datas1 = "                        ";
          //   file.Write(datas1);
          //    for (int i = 1; i <= 6; ++i)
          //    {
          //      file.Write((char)32);
          //      }
          //}
          //else
          // {
          //     file.Write("\r\n");
          //  file.Write(datas1);
          //  for (int i = 1; i <= 9; ++i)
          //     {
          //          file.Write((char)32);
          //     }
          // }
            

            if (string.IsNullOrEmpty(codigo))
             {
                     file.Write("\r\n");
                     complete ="                            ";
                     file.Write(complete);
                     for (int i = 1; i <= 26 - (complete.Length - 1); ++i)
                     {
                         file.Write((char)32);
                     }      
             }
             else
             {
                 file.Write("\r\n");
                 file.Write(complete);
                 for (int i = 1; i <= 26 - (complete.Length - 1); ++i)
                 {
                     file.Write((char)32);
                 }
             }
             if (string.IsNullOrEmpty(codigo2))
             {
                     complete2 ="                        ";
                     file.Write(complete2);
                     for (int i = 1; i <= 25 - (complete2.Length - 1); ++i)
                     {
                         file.Write((char)32);
                     }
             }
             else
             {
                 file.Write(complete2);
                 for (int i = 1; i <= 25 - (complete2.Length - 1); ++i)
                 {
                     file.Write((char)32);
                 }
             }
            if (string.IsNullOrEmpty(codigo3))
             {
                     complete3 ="                       ";
                     file.Write(complete3);
                     for (int i = 1; i <= 26 - (complete3.Length - 1); ++i)
                     {
                         file.Write((char)32);
                     }      
             }
             else
             {
                 file.Write(complete3);
                 for (int i = 1; i <= 26 - (complete3.Length - 1); ++i)
                 {
                     file.Write((char)32);
                 }
             }
            if (string.IsNullOrEmpty(codigo4))
             {
                     complete4 ="                        ";
                     file.Write(complete4);
                     for (int i = 1; i <= 26 - (complete4.Length - 1); ++i)
                     {
                         file.Write((char)32);
                     }      
             }
             else
             {
                 file.Write(complete4);
                 for (int i = 1; i <= 26 - (complete4.Length - 1); ++i)
                 {
                     file.Write((char)32);
                 }
             }
            if (string.IsNullOrEmpty(codigo5))
             {
                     complete5 = "                      ";
                     file.Write(complete5);
                     for (int i = 1; i <= 26 - (complete5.Length - 1); ++i)
                     {
                         file.Write((char)32);
                     }      
             }
             else
             {
                 file.Write(complete5);
                 //for (int i = 1; i <= 26 - (complete5.Length - 1); ++i)
                 //{
                 //    file.Write((char)32);
                 //}
             }
            if (liniaBlanco1.Length==20)
            {
                file.Write("\r\n");
                file.Write(liniaBlanco1);
                for (int i = 1; i <= 26 - (liniaBlanco1.Length - 1); ++i)
                {
                    file.Write((char)32);
                }
            }
            if (liniaBlanco2.Length==20)
            {
                file.Write(liniaBlanco2);
                for (int i = 1; i <= 26 - (liniaBlanco2.Length - 1); ++i)
                {
                    file.Write((char)32);
                }
            }
            if (liniaBlanco3.Length==20)
            {
                file.Write(liniaBlanco3);
                for (int i = 1; i <= 26 - (liniaBlanco3.Length - 1); ++i)
                {
                    file.Write((char)32);
                }
            }
            if (liniaBlanco4.Length==20)
            {
                file.Write(liniaBlanco4);
                for (int i = 1; i <= 26 - (liniaBlanco4.Length - 1); ++i)
                {
                    file.Write((char)32);
                }
            }
            else
            {
            file.Write(liniaBlanco5);
            }
            file.Write("\r\n\n");
            file.Close();
        }
        //codigo viejo de la linia 4 de la fecha
        /* 
          file.Write(data1);
         for (int i = 1; i <= 6; ++i)
         {
             file.Write((char)32);
         }
         * file.Write(data2);
         for (int i = 1; i <= 6; ++i)
         {
             file.Write((char)32);
         }
         file.Write(data3);
         for (int i = 1; i <= 6; ++i)
         {
             file.Write((char)32);
         }
         file.Write(data4);
         for (int i = 1; i <= 6; ++i)
         {
             file.Write((char)32);
         }
         file.Write(data5);
         for (int i = 1; i <= 6; ++i)
         {  
             file.Write((char)32);
         }*/
        /* file.Write("\r\n");
         file.Write(complete);
        for (int i = 1; i <= 26 - (complete.Length - 1); ++i)
         {
             file.Write((char)32);
         }
         file.Write(complete);
        */
             // Codigo viejo de la linia 4 de rif
             /*file.Write(complete2);
             for (int i = 1; i <= 26 - (complete2.Length - 1); ++i)
             {
                 file.Write((char)32);
             }
             file.Write(complete3);
             for (int i = 1; i <= 26 - (complete3.Length - 1); ++i)
             {
                 file.Write((char)32);
             }
             file.Write(complete4);
             for (int i = 1; i <= 26 - (complete4.Length - 1); ++i)
             {
                 file.Write((char)32);
             }
             file.Write(complete5);
             for (int i = 1; i <= 26 - (complete5.Length - 1); ++i)
             {
                 file.Write((char)32);
             }
             for (int i = 1; i <= 3; ++i)
             {
                 file.Write("\r\n");
             }
             file.Close();
        }*/

        public void ImprimirArchivo(String receiptText)
        {
            IntPtr ptr = CreateFile("LPT1", GENERIC_WRITE, 0,
                     IntPtr.Zero, OPEN_EXISTING, 0, IntPtr.Zero);

            /* Is bad handle? INVALID_HANDLE_VALUE */
            if (ptr.ToInt32() == -1)
            {
                /* ask the framework to marshall the win32 error code to an exception */
                Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
            }
            else
            {
                FileStream lpt = new FileStream(ptr, FileAccess.ReadWrite);
                Byte[] buffer = new Byte[2048];
                //Check to see if your printer support ASCII encoding or Unicode.
                //If unicode is supported, use the following:
                //buffer = System.Text.Encoding.Unicode.GetBytes(Temp);
                buffer = System.Text.Encoding.ASCII.GetBytes(receiptText);
                lpt.Write(buffer, 0, buffer.Length);
                lpt.Close();

            }
        }
        public void GenerarArchivo1_1(string codigo, string contenido, string unidad, string precio,
            string nombre, string rif,string fecha, string iva1, string montoIva1,string totalPag1,string pvj1, string codigo2, string contenido2, string unidad2,
            string precio2,string montoIva2,string totalPag2, string pvj2, string codigo3, string contenido3, string unidad3, string precio3,string montoIva3,string totalPag3,
            string pvj3,string codigo4, string contenido4, string unidad4,string precio4,string montoIva4,string totalPag4,string pvj4, string codigo5,
            string contenido5, string unidad5, string precio5,string montoIva5,string totalPag5,string pvj5,int contador) 
        {
            string primera1 = " " + codigo + " " + unidad + " "  + fecha;
            string primera2 = " " + codigo2 + " " + unidad2 + " " + fecha;
            string primera3 = " " + codigo3 + " " + unidad3 + " " + fecha;
            string primera4 = " " + codigo4 + " " + unidad4 + " " + fecha;
            string primera5 = " " + codigo5 + " " + unidad5 + " " + fecha;
            string justo1 = " " + pvj1 + "" + precio ;
            string justo2 = "" + pvj2 + "" + precio2;
            string justo3 = "" + pvj3 + "" + precio3;
            string justo4 = "" + pvj4 + "" + precio4;
            string justo5 = "" + pvj5 + "" + precio5;
            string data =  "" + contenido + "" + montoIva1+""+"Total";
            string data2 = "" + contenido2 + "" + montoIva2 +""+ "Total";
            string data3 = "" + contenido3 + "" + montoIva3 +""+ "Total";
            string data4 = "" + contenido4 + "" + montoIva4 +""+ "Total";
            string data5 = "  " + contenido5 + "" + montoIva5 +""+ "Total";
            string datas1 ="" + "aPagarBs" + "" +totalPag1;
            string datas2 ="" + "aPagarBs" + "" +totalPag2;
            string datas3 ="" + "aPagarBs" + "" +totalPag3;
            string datas4 ="" + "aPagarBs" + "" +totalPag4;
            string datas5 ="" + "aPagarBs" + "" +totalPag5;
            string complete = "" + nombre + "" + rif;
            string complete2 = "" + nombre + "" + rif;
            string complete3 = "" + nombre + "" + rif;
            string complete4 = "" + nombre + "" + rif;
            string complete5 = "" + nombre + "" + rif;

            int a = justo1.Length;
            int b = justo2.Length;
            int c = justo3.Length;
            int d = justo4.Length;
            int e = justo5.Length;

            int a_a = primera1.Length;
            int b_b = primera2.Length;
            int c_c = primera3.Length;
            int d_d = primera4.Length;
            int e_e = primera5.Length;
            int resultado;
            // Write the string to a file.
            this.SIni = Application.StartupPath + "\\Etiqueta1.txt";
            System.IO.StreamWriter file = new System.IO.StreamWriter(sIni);
            if (contador == 1)
            {
                file.Write("\r\n");
            }
            if (string.IsNullOrEmpty(codigo))
            {
                primera1 = "                      ";
                file.Write(primera1);
                resultado = 8;
                for (int i = 1; i <= resultado; ++i)
                {
                    file.Write((char)32);
                }
            }
            else 
            {
                file.Write(primera1);
                resultado = 27 - b_b;
                resultado = resultado-(a_a - b_b);
                for (int i = 1; i <= resultado; ++i)
                {
                    file.Write((char)32);
                }
            }
            if (string.IsNullOrEmpty(codigo2))
            {
                primera2 = "                 ";
                file.Write(primera2);
                resultado = 8;
                for (int i = 1; i <= resultado; ++i)
                {
                    file.Write((char)32);
                }
            }
            else
            {
                file.Write(primera2);
                resultado = 27 - c_c;
                resultado = resultado - (b_b - c_c);
                for (int i = 1; i <= resultado; ++i)
                {
                    file.Write((char)32);
                }
            }
            if (string.IsNullOrEmpty(codigo3))
            {
                primera3 = "                  ";
                file.Write(primera3);
                resultado = 8;
                for (int i = 1; i <= resultado; ++i)
                {
                    file.Write((char)32);
                }
            }
            else
            {
                file.Write(primera3);
                resultado = 27 - d_d;
                resultado = resultado - (c_c - d_d);
                for (int i = 1; i <= resultado; ++i)
                {
                    file.Write((char)32);
                }
            }
            if (string.IsNullOrEmpty(codigo4))
            {
                primera4 = "                  ";
                file.Write(primera4);
                resultado = 8;
                for (int i = 1; i <= resultado; ++i)
                {
                    file.Write((char)32);
                }
            }
            else
            {
                file.Write(primera4);
                resultado = 27 - e_e;
                resultado = resultado - (d_d - e_e);
                for (int i = 1; i <= resultado; ++i)
                {
                    file.Write((char)32);
                }
            }
            if (string.IsNullOrEmpty(codigo5))
            {
                primera5 = "                  ";
                file.Write(primera5);
                file.WriteLine((char)32);
            }
            else
            {
                file.Write(primera5);
                file.WriteLine((char)32);
            }
            
            file.Write(justo1);
            resultado = 29 - b;
            resultado = resultado - (a - b);
            for (int i = 1; i <= resultado; ++i)
            {
                file.Write((char)32);
            }
            file.Write(justo2);
            resultado = 27 - c;
            resultado = resultado - (b - c);
            for (int i = 1; i <= resultado; ++i)
            {
                file.Write((char)32);
            }
            file.Write(justo3);
            resultado = 27 - d;
            resultado = resultado - (c - d);
            for (int i = 1; i <= resultado; ++i)
            {
                file.Write((char)32);
            }
            file.Write(justo4);
            resultado = 27 - e;
            resultado = resultado - (d - e);
            for (int i = 1; i <= resultado; ++i)
            {
                file.Write((char)32);
            }
            file.Write(justo5);
            file.WriteLine((char)32);
//aqui
            if (string.IsNullOrEmpty(codigo))
            {
                if ((string.IsNullOrEmpty(codigo) && string.IsNullOrEmpty(codigo5)) && (!(string.IsNullOrEmpty(codigo2)&&(string.IsNullOrEmpty(codigo3)&&(string.IsNullOrEmpty(codigo4))))))
                {
                    data = "                 ";
                    file.Write(data);
                    resultado = 27 - data2.Length;
                    resultado = resultado - (data.Length-data2.Length);
                    for (int i = 1; i <= resultado; ++i)
                    {
                        file.Write((char)32);
                    }
                } 
                else
                {
                    data = "                 ";
                    file.Write(data);
                    resultado = 27 - data2.Length;
                    resultado = resultado - (data.Length - data2.Length);
                    for (int i = 1; i <= resultado; ++i)
                    {
                        file.Write((char)32);
                    }
                }  
            }
            else
            {
                file.Write(data);
                resultado = 27 - data2.Length;
                resultado = resultado - (data.Length - data2.Length);
                    for (int i = 1; i <= resultado; ++i)
                    {
                        file.Write((char)32);
                    }
            }
            if (string.IsNullOrEmpty(codigo2))
            {
                    data2 = "                      ";
                    file.Write(data2);
                    resultado = 27 - data3.Length;
                    resultado = resultado - (data2.Length - data3.Length);
                    for (int i = 1; i <= resultado; ++i)
                    {
                        file.Write((char)32);
                    }
            }
            else
            {
                file.Write(data2);
                resultado = 27 - data3.Length;
                resultado = resultado - (data2.Length - data3.Length);
                for (int i = 1; i <= resultado; ++i)
                {
                    file.Write((char)32);
                }
            }
            if (string.IsNullOrEmpty(codigo3))
            {
                data3 = "                    ";
                file.Write(data3);
                resultado = 27 - data4.Length;
                resultado = resultado - (data3.Length-data4.Length);
                for (int i = 1; i <= resultado; ++i)
                {
                    file.Write((char)32);
                }
            }
            else
            {
                file.Write(data3);
                resultado = 27 - data4.Length;
                resultado = resultado - (data3.Length - data4.Length);
                for (int i = 1; i <= resultado; ++i)
                {
                    file.Write((char)32);
                }
            }
            
            if (string.IsNullOrEmpty(codigo4))
            {
                data4 = "                    ";
                file.Write(data4);
                resultado = 27 - data5.Length;
                resultado = resultado - (data4.Length-data5.Length);
                for (int i = 1; i <= resultado; ++i)
                {
                    file.Write((char)32);
                }
            }
            else
            {
                file.Write(data4);
                resultado = 27 - data5.Length;
                resultado = resultado - (data4.Length - data5.Length);
                for (int i = 1; i <= resultado; ++i)
                {
                    file.Write((char)32);
                }
            }
            
            
            if (string.IsNullOrEmpty(codigo5))
            {
                data5 = "                        ";
                primera5 = " ";
                fecha = " ";
                file.Write(data5);
                for (int i = 1; i <= 9; ++i)
                {
                    file.Write((char)32);
                }
            }
            else
            {
                file.Write(data5);
                for (int i = 1; i <= 7; ++i)
                {
                    file.Write((char)32);
                }
            }
          if (string.IsNullOrEmpty(codigo))
          {
              datas1 = "              ";
              file.Write(datas1);
              resultado = 28 - datas2.Length;
              resultado = resultado - (datas1.Length - datas2.Length);
              for (int i = 1; i <= resultado; ++i)
                     {
                        file.Write((char)32);
                     }
          }
          else
           {
            file.Write(datas1);
            resultado = 28 - datas2.Length;
            resultado = resultado - (datas1.Length - datas2.Length);
            for (int i = 1; i <= resultado; ++i)
            {
                file.Write((char)32);
            }
           }
          if (string.IsNullOrEmpty(codigo2))
          {
              datas2 = "              ";
              file.Write(datas2);
              resultado = 28 - datas3.Length;
              resultado = resultado - (datas2.Length-datas3.Length);
              for (int i = 1; i <= resultado; ++i)
              {
                  file.Write((char)32);
              }
          }
          else
          {
              file.Write(datas2);
              resultado = 28 - datas3.Length;
              resultado = resultado - (datas2.Length - datas3.Length);
              for (int i = 1; i <= resultado; ++i)
              {
                  file.Write((char)32);
              }
          }
          if (string.IsNullOrEmpty(codigo3))
          {
              datas3 = "              ";
              file.Write(datas3);
              resultado = 25 - datas4.Length;
              resultado = resultado - (datas3.Length-datas4.Length);
              for (int i = 1; i <= resultado; ++i)
              {
                  file.Write((char)32);
              }
          }
          else
          {
              file.Write(datas3);
              resultado = 25 - datas4.Length;
              resultado = resultado - (datas3.Length - datas4.Length);
              for (int i = 1; i <= resultado; ++i)
              {
                  file.Write((char)32);
              }
          }
          if (string.IsNullOrEmpty(codigo4))
          {
              datas4 = "              ";
              file.Write(datas4);
              resultado = 26 - datas5.Length;
              resultado = resultado - (datas4.Length-datas5.Length);
              for (int i = 1; i <= resultado; ++i)
              {
                  file.Write((char)32);
              }
          }
          else
          {
              file.Write(datas4);
              resultado = 28 - datas5.Length;
              resultado = resultado - (datas4.Length - datas5.Length);
              for (int i = 1; i <= resultado; ++i)
              {
                  file.Write((char)32);
              }
          }
          if (string.IsNullOrEmpty(codigo5))
          {
              datas5 = "              ";
              file.Write(datas5);
              for (int i = 1; i <= 10; ++i)
              {
                  file.Write((char)32);
              }
          }
          else
          {
              file.Write(datas5);
              for (int i = 1; i <= 7; ++i)
              {
                  file.Write((char)32);
              }
          }

            if (string.IsNullOrEmpty(codigo))
             {
                 file.Write("\r\n");
                     complete ="                            ";
                     file.Write(complete);
                     for (int i = 1; i <= 26 - (complete.Length - 1); ++i)
                     {
                         file.Write((char)32);
                     }      
             }
             else
             {
                 file.Write("\r\n");
                 file.Write(complete);
                 for (int i = 1; i <= 26 - (complete.Length - 1); ++i)
                 {
                     file.Write((char)32);
                 }
             }
             if (string.IsNullOrEmpty(codigo2))
             {
                     complete2 ="                        ";
                     file.Write(complete2);
                     for (int i = 1; i <= 25 - (complete2.Length - 1); ++i)
                     {
                         file.Write((char)32);
                     }      
             }
             else
             {
                 file.Write(complete2);
                 for (int i = 1; i <= 26 - (complete2.Length - 1); ++i)
                 {
                     file.Write((char)32);
                 }
             }
            if (string.IsNullOrEmpty(codigo3))
             {
                     complete3 ="                       ";
                     file.Write(complete3);
                     for (int i = 1; i <= 26 - (complete3.Length - 1); ++i)
                     {
                         file.Write((char)32);
                     }      
             }
             else
             {
                 file.Write(complete3);
                 for (int i = 1; i <= 27 - (complete3.Length - 1); ++i)
                 {
                     file.Write((char)32);
                 }
             }
            if (string.IsNullOrEmpty(codigo4))
             {
                     complete4 ="                        ";
                     file.Write(complete4);
                     for (int i = 1; i <= 27 - (complete4.Length - 1); ++i)
                     {
                         file.Write((char)32);
                     }      
             }
             else
             {
                 file.Write(complete4);
                 for (int i = 1; i <= 26 - (complete4.Length - 1); ++i)
                 {
                     file.Write((char)32);
                 }
             }
            if (string.IsNullOrEmpty(codigo5))
             {
                     complete5 = "                      ";
                     file.Write(complete5);
                     for (int i = 1; i <= 26 - (complete5.Length - 1); ++i)
                     {
                         file.Write((char)32);
                     }      
             }
             else
             {
                 file.Write(complete5);
                 for (int i = 1; i <= 26 - (complete5.Length - 1); ++i)
                 {
                     file.Write((char)32);
                 }
             }
            file.Write("\r\n\n");
            file.Close();
        }

        public void GeneraArchivo1_2(string codigo1, string codigo2, string codigo3, string codigo4, string codigo5,int contador) 
        {
            string primera1 = "  "+ codigo1 + " ";
            string primera2 = " " + codigo2 + " ";
            string primera3 = " " + codigo3 + " ";
            string primera4 = " " + codigo4 + " ";
            string primera5 = " " + codigo5 + " ";
            string pblanco1 = "                    ";
            string pblanco2 = "                    ";
            string pblanco3 = "                    ";
            string pblanco4 = "                    ";
            string pblanco5 = "                    ";
            string sblanco1 = "                    ";
            string sblanco2 = "                    ";
            string sblanco3 = "                    ";
            string sblanco4 = "                    ";
            string sblanco5 = "                    ";
            string tblanco1 = "                    ";
            string tblanco2 = "                    ";
            string tblanco3 = "                    ";
            string tblanco4 = "                    ";
            string tblanco5 = "                    ";
            string cblanco1 = "                    ";
            string cblanco2 = "                    ";
            string cblanco3 = "                    ";
            string cblanco4 = "                    ";
            string cblanco5 = "                    ";

            int a = primera1.Length;
            int b = primera2.Length;
            int c = primera3.Length;
            int d = primera4.Length;
            int e = primera5.Length;
            int resultado;
            // Write the string to a file.
            this.SIni = Application.StartupPath + "\\Etiqueta1.txt";
            System.IO.StreamWriter file = new System.IO.StreamWriter(sIni);

            if (contador == 1) 
            {
                file.Write("\r");
            }
            else if (contador > 1) 
            {
                file.Write("\r\n");
            }
            if (pblanco1.Length == 20)
            {
                file.Write(pblanco1);
                for (int i = 1; i <= 26 - (pblanco1.Length - 1); ++i)
                {
                    file.Write((char)32);
                }
            }
            if (pblanco2.Length == 20)
            {
                file.Write(pblanco2);
                for (int i = 1; i <= 26 - (pblanco2.Length - 1); ++i)
                {
                    file.Write((char)32);
                }
            }
            if (pblanco3.Length == 20)
            {
                file.Write(pblanco3);
                for (int i = 1; i <= 26 - (pblanco3.Length - 1); ++i)
                {
                    file.Write((char)32);
                }
            }
            if (pblanco4.Length == 20)
            {
                file.Write(pblanco4);
                for (int i = 1; i <= 26 - (pblanco4.Length - 1); ++i)
                {
                    file.Write((char)32);
                }
            }
            else
            {
                file.Write(pblanco5);
                file.Write((char)32);
            }
            if (sblanco1.Length == 20)
            {
                file.Write("\r\n");
                file.Write(sblanco1);
                for (int i = 1; i <= 26 - (sblanco1.Length - 1); ++i)
                {
                    file.Write((char)32);
                }
            }
            if (sblanco2.Length == 20)
            {
                file.Write(sblanco2);
                for (int i = 1; i <= 26 - (sblanco2.Length - 1); ++i)
                {
                    file.Write((char)32);
                }
            }
            if (sblanco3.Length == 20)
            {
                file.Write(sblanco3);
                for (int i = 1; i <= 26 - (sblanco3.Length - 1); ++i)
                {
                    file.Write((char)32);
                }
            }
            if (sblanco4.Length == 20)
            {
                file.Write(sblanco4);
                for (int i = 1; i <= 26 - (sblanco4.Length - 1); ++i)
                {
                    file.Write((char)32);
                }
            }
            else
            {
                file.Write(sblanco5);
                file.Write((char)32);
            }
            if (string.IsNullOrEmpty(codigo1))
            {
                file.Write("\r\n");
                primera1 = "            ";
                file.Write(primera1);
                resultado = 16;
                for (int i = 1; i <= resultado; ++i)
                {
                    file.Write((char)32);
                }
            }
            else
            {
                file.Write("\r\n");
                file.Write(primera1);
                resultado = 28 - b;
                resultado = resultado - (a - b);
                for (int i = 1; i <= resultado; ++i)
                {
                    file.Write((char)32);
                }
            }
            if (string.IsNullOrEmpty(codigo2))
            {
                primera2 = "           ";
                file.Write(primera2);
                resultado = 16;
                for (int i = 1; i <= resultado; ++i)
                {
                    file.Write((char)32);
                }
            }
            else
            {
                file.Write(primera2);
                resultado = 27 - c;
                resultado = resultado - (b - c);
                for (int i = 1; i <= resultado; ++i)
                {
                    file.Write((char)32);
                }
            }
            if (string.IsNullOrEmpty(codigo3))
            {
                primera3 = "           ";
                file.Write(primera3);
                resultado = 16;
                for (int i = 1; i <= resultado; ++i)
                {
                    file.Write((char)32);
                }
            }
            else
            {
                file.Write(primera3);
                resultado = 28 - d;
                resultado = resultado - (c - d);
                for (int i = 1; i <= resultado; ++i)
                {
                    file.Write((char)32);
                }
            }
            if (string.IsNullOrEmpty(codigo4))
            {
                primera4 = "           ";
                file.Write(primera4);
                resultado = 16;
                for (int i = 1; i <= resultado; ++i)
                {
                    file.Write((char)32);
                }
            }
            else
            {
                file.Write(primera4);
                resultado = 28 - e;
                resultado = resultado - (d - e);
                for (int i = 1; i <= resultado; ++i)
                {
                    file.Write((char)32);
                }
            }
            if (string.IsNullOrEmpty(codigo5))
            {
                primera5 = "           ";
                file.Write(primera5);
                file.WriteLine((char)32);
            }
            else
            {
                file.Write(primera5);
                file.WriteLine((char)32);
            }
            if (tblanco1.Length == 20)
            {
                file.Write("\r\n");
                file.Write(tblanco1);
                for (int i = 1; i <= 26 - (tblanco1.Length - 1); ++i)
                {
                    file.Write((char)32);
                }
            }
            if (tblanco2.Length== 20)
            {
                file.Write(tblanco2);
                for (int i = 1; i <= 26 - (tblanco2.Length - 1); ++i)
                {
                    file.Write((char)32);
                }
            }
            if (tblanco3.Length== 20)
            {
                file.Write(tblanco3);
                for (int i = 1; i <= 26 - (tblanco3.Length - 1); ++i)
                {
                    file.Write((char)32);
                }
            }
            if (tblanco4.Length== 20)
            {
                file.Write(tblanco4);
                for (int i = 1; i <= 26 - (tblanco4.Length - 1); ++i)
                {
                    file.Write((char)32);
                }
            }
            else
            {
                file.Write(tblanco5);
                file.Write((char)32);
            }
            if (cblanco1.Length == 20)
            {
                file.Write("\r\n");
                file.Write(tblanco1);
                for (int i = 1; i <= 26 - (cblanco1.Length - 1); ++i)
                {
                    file.Write((char)32);
                }
            }
            if (cblanco2.Length== 20)
            {
                file.Write(tblanco2);
                for (int i = 1; i <= 26 - (cblanco2.Length - 1); ++i)
                {
                    file.Write((char)32);
                }
            }
            if (cblanco3.Length == 20)
            {
                file.Write(cblanco3);
                for (int i = 1; i <= 26 - (cblanco3.Length - 1); ++i)
                {
                    file.Write((char)32);
                }
            }
            if (cblanco4.Length == 20)
            {
                file.Write(cblanco4);
                for (int i = 1; i <= 26 - (cblanco4.Length - 1); ++i)
                {
                    file.Write((char)32);
                }
            }
            else
            {
                file.Write(cblanco5);
                file.Write((char)32);
            }
            file.Close();
        }




        public void GenerarArchivo2(string nombre, string rif, string descripcion, string descripcion_1, string codigo,
            string unidad, string nombre2, string rif2, string descripcion2, string descripcion2_2, string codigo2, string unidad2,
            string nombre3, string rif3, string descripcion3, string descripcion3_3, string codigo3, string unidad3, string nombre4, string rif4,
            string descripcion4, string descripcion4_4, string codigo4, string unidad4)
        {
            string nombre1 = " " + nombre;
            string rif1 = " " + rif;
            string descripcion1_1_1 = descripcion;
            string linea1 = descripcion_1;
            string codigo1 = codigo;
            string unidad1 = " " + unidad;
            string linea_abajo = " " + "_________________________";
            string nombre2_2 = " " + nombre2;
            string rif2_2 = " " + rif2;
            string descripcion2_2_2 = descripcion2;
            string linea2 = descripcion2_2;
            string codigo2_2 = codigo2;
            string unidad2_2 = " " + unidad2;
            string linea_abajo2 = " " + "_________________________";
            string nombre3_3 = " " + nombre3;
            string rif3_3 = " " + rif3;
            string descripcion3_3_3 = descripcion3;
            string linea3 = descripcion3_3;
            string codigo3_3 = codigo3;
            string unidad3_3 = " " + unidad3;
            string linea_abajo3 = " " + "_________________________";
            string nombre4_4 = " " + nombre4;
            string rif4_4 = " " + rif4;
            string descripcion4_4_4 = descripcion4;
            string linea4 = descripcion4_4;
            string codigo4_4 = codigo4;
            string unidad4_4 = unidad4;
            string linea_abajo4 = " " + "_________________________";

            int a = nombre.Length;
            int b = nombre2.Length;
            int c = nombre3.Length;
            int d = nombre4.Length;
            int a_a = descripcion_1.Length;
            int b_b = descripcion2_2.Length;
            int c_c = descripcion3_3.Length;
            int d_d = descripcion4_4.Length;
            int resultado;

            this.SIni2 = Application.StartupPath + "\\Etiqueta2.txt";
            System.IO.StreamWriter file = new System.IO.StreamWriter(sIni2);
            file.Write(nombre1);
            resultado = 34 - b;
            resultado = resultado - (a - b);
            for (int i = 1; i <= resultado; ++i) //ciclos para dar espacios a la otra etiqueta
            {
                file.Write((char)32);
            }
            file.Write(nombre2_2);
            resultado = 34 - c;
            resultado = resultado - (b - c);
            for (int i = 1; i <= resultado; ++i) //ciclos para dar espacios a la otra etiqueta
            {
                file.Write((char)32);
            }
            file.Write(nombre3_3);
            resultado = 34 - d;
            resultado = resultado - (c - d);
            for (int i = 1; i <= resultado; ++i) //ciclos para dar espacios a la otra etiqueta
            {
                file.Write((char)32);
            }
            file.Write(nombre4_4);
            file.Write("\r\n");
            file.Write(rif1);
            resultado = 36 - rif2.Length;
            resultado = resultado - (rif1.Length - rif2.Length);
            for (int i = 1; i <= resultado; ++i) //ciclos para dar espacios a la otra etiqueta
            {
                file.Write((char)32);
            }
            file.Write(rif2);
            resultado = 35 - rif3.Length;
            resultado = resultado - (rif2.Length - rif3.Length);
            for (int i = 1; i <= resultado; ++i) //ciclos para dar espacios a la otra etiqueta
            {
                file.Write((char)32);
            }
            file.Write(rif3);
            resultado = 35 - rif4.Length;
            resultado = resultado - (rif3.Length - rif4.Length);
            for (int i = 1; i <= resultado; ++i) //ciclos para dar espacios a la otra etiqueta
            {
                file.Write((char)32);
            }
            file.Write(rif4);
            file.Write("\r\n");
            file.Write("\r\n");
            file.Write((char)32);
            file.Write(descripcion1_1_1);
            resultado = 34 - b;
            resultado = resultado - (a - b);
            for (int i = 1; i <= resultado + 1; ++i) //ciclos para dar espacios a la otra etiqueta
            {
                file.Write((char)32);
            }
            file.Write(descripcion2_2_2);
            resultado = 34 - c;
            resultado = resultado - (b - c);
            for (int i = 1; i <= resultado + 1; ++i) //ciclos para dar espacios a la otra etiqueta
            {
                file.Write((char)32);
            }
            file.Write(descripcion3_3_3);
            resultado = 34 - d;
            resultado = resultado - (c - d);
            for (int i = 1; i <= resultado + 1; ++i) //ciclos para dar espacios a la otra etiqueta
            {
                file.Write((char)32);
            }
            file.Write(descripcion4_4_4);
            file.Write("\r\n");
            file.Write((char)32);
            file.Write(linea1);
            resultado = 35 - linea2.Length;
            resultado = resultado - (linea1.Length - linea2.Length);
            for (int i = 1; i <= resultado; ++i) //ciclos para dar espacios a la otra etiqueta
            {
                file.Write((char)32);
            }
            file.Write(linea2);
            resultado = 34 - linea3.Length;
            resultado = resultado - (linea2.Length - linea3.Length);
            for (int i = 1; i <= resultado + 1; ++i) //ciclos para dar espacios a la otra etiqueta
            {
                file.Write((char)32);
            }
            file.Write(linea3);
            resultado = 34 - linea4.Length;
            resultado = resultado - (linea3.Length - linea4.Length);
            for (int i = 1; i <= resultado + 1; ++i) //ciclos para dar espacios a la otra etiqueta
            {
                file.Write((char)32);
            }
            file.Write(linea4);
            file.Write("\r\n");
            file.Write("\r\n");
            file.Write((char)32);
            file.Write(codigo1);
            resultado = 35 - codigo2.Length;
            resultado = resultado - (codigo1.Length - codigo2.Length);
            for (int i = 1; i <= resultado; ++i) //ciclos para dar espacios a la otra etiqueta
            {
                file.Write((char)32);
            }
            file.Write(codigo2_2);
            resultado = 34 - codigo3.Length;
            resultado = resultado - (codigo2.Length - codigo3.Length);
            for (int i = 1; i <= resultado + 1; ++i) //ciclos para dar espacios a la otra etiqueta
            {
                file.Write((char)32);
            }
            file.Write(codigo3_3);
            resultado = 34 - codigo4.Length;
            resultado = resultado - (codigo3.Length - codigo4.Length);
            for (int i = 1; i <= resultado + 1; ++i) //ciclos para dar espacios a la otra etiqueta
            {
                file.Write((char)32);
            }
            file.Write(codigo4_4);
            file.Write("\r\n");
            file.Write(unidad1);
            resultado = 35 - unidad2.Length;
            resultado = resultado - (unidad1.Length - unidad2.Length);
            for (int i = 1; i <= resultado; ++i) //ciclos para dar espacios a la otra etiqueta
            {
                file.Write((char)32);
            }
            file.Write(unidad2_2);
            resultado = 34 - unidad3.Length;
            resultado = resultado - (unidad2.Length - unidad3.Length);
            for (int i = 1; i <= resultado; ++i) //ciclos para dar espacios a la otra etiqueta
            {
                file.Write((char)32);
            }
            file.Write(unidad3_3);
            resultado = 34 - unidad4.Length;
            resultado = resultado - (unidad3.Length - unidad4.Length);
            for (int i = 1; i <= resultado + 1; ++i) //ciclos para dar espacios a la otra etiqueta
            {
                file.Write((char)32);
            }
            file.Write(unidad4_4);
            file.Write("\r\n");
            file.Write(linea_abajo);
            resultado = 35 - linea_abajo2.Length;
            resultado = resultado - (linea_abajo.Length - linea_abajo2.Length);
            for (int i = 1; i <= resultado; ++i) //ciclos para dar espacios a la otra etiqueta
            {
                file.Write((char)32);
            }
            file.Write(linea_abajo2);
            resultado = 35 - linea_abajo3.Length;
            resultado = resultado - (linea_abajo2.Length - linea_abajo3.Length);
            for (int i = 1; i <= resultado; ++i) //ciclos para dar espacios a la otra etiqueta
            {
                file.Write((char)32);
            }
            file.Write(linea_abajo3);
            resultado = 35 - linea_abajo4.Length;
            resultado = resultado - (linea_abajo3.Length - linea_abajo4.Length);
            for (int i = 1; i <= resultado; ++i) //ciclos para dar espacios a la otra etiqueta
            {
                file.Write((char)32);
            }
            file.Write(linea_abajo4);
            for (int i = 1; i <= 3; ++i)
            {
                file.Write("\r\n");
            }
            file.Close();
        }

        public void GenerarArchivo3(string nombre, string rif, string descripcion, string descripcion_1, string codigo,
            string unidad, string precio1, string nombre2, string rif2, string descripcion2, string descripcion2_2, string codigo2,
            string unidad2, string fecha, string precio2)
        {
            string nombre1 = nombre;
            string rif1 = rif;
            string data = fecha;
            string descripcion1_1_1 = descripcion;
            string linea1 = descripcion_1;
            string codigo1 = codigo;
            string unidad1 = unidad;
            string justo1 = precio1;
            string linea_abajo = "__________________________________";
            string nombre2_2 = nombre2;
            string rif2_2 = rif2;
            string descripcion2_2_2 = descripcion2;
            string linea2 = descripcion2_2;
            string codigo2_2 = codigo2;
            string unidad2_2 = unidad2;
            string justo2 = precio2;
            string linea_abajo2 = "__________________________________";

            int a = nombre.Length;
            int b = nombre2.Length;

            int a_a = descripcion_1.Length;
            int b_b = descripcion2_2.Length;

            this.SIni3 = Application.StartupPath + "\\Etiqueta3.html";
            System.IO.StreamWriter file = new System.IO.StreamWriter(sIni3);
            file.Write("<html>");
            file.Write("\r\n");
            file.Write("<head>");
            file.Write("\r\n");
            //file.Write("<title>FLebrun - Etiquetas Grandes</title>");
            file.Write("\r\n");
            file.Write("<style type='text/css'>@media print {.oculto{display:none} table { border-spacing: 0;border-collapse: collapse;}}</style>");
            file.Write("\r\n");
            file.Write("</head>");
            file.Write("\r\n");
            file.Write("<body>");
            file.Write("\r\n");
            file.Write("<table border='0' cellspacing='0'>");
            file.Write("\r\n");
            file.Write("<tr>");
            file.Write("\r\n");
            file.Write("<td>");
            file.Write("\r\n");
            file.Write("<font size='2'>");
            file.Write("\r\n");
            file.Write(nombre);
            file.Write("\r\n");
            file.Write("<br>");
            file.Write("\r\n");
            file.Write(rif1);
            for (int i = 1; i <= 4; ++i)
            {
                file.Write("&nbsp");
            }
            if (codigo1 == "")
            {
                file.Write("\r\n");
            }
            else
            {
                file.Write(data);
                file.Write("\r\n");
            }
            file.Write("<br>");
            file.Write("\r\n");
            file.Write(descripcion1_1_1 + "\r\n");
            file.Write("<br>");
            file.Write("\r\n");
            file.Write(linea1);
            file.Write("\r\n");
            file.Write("<br>");
            file.Write("\r\n");
            if (codigo1 == "")
            {
                file.Write("&nbsp<font size='6'>" + codigo1 + "</font>&nbsp");
                file.Write("&nbsp<font size='6'>" + unidad1 + "</font>");
            }
            else
            {
                file.Write("Cod:&nbsp<font size='6'>" + codigo1 + "</font>&nbsp");
                file.Write("Und:&nbsp<font size='6'>" + unidad1 + "</font>");
            }
            file.Write("\r\n");
            file.Write("<br>");
            file.Write("\r\n");
            if (codigo2 == "")
            {

                decimal valor1 = 0;
                DateTime fechaActual = DateTime.Now.Date;
                if (fechaActual <= Convert.ToDateTime("30/09/2021"))
                {
                    valor1 = (Convert.ToDecimal(justo1) / 1000000);
                }
                if (fechaActual > Convert.ToDateTime("30/09/2021"))
                {
                    //                    valor1 = (Convert.ToDecimal(justo1) * 1000000);
                    valor1 = (Convert.ToDecimal(justo1) * 1000000);
                }
                decimal just1 = Convert.ToDecimal(justo1);
                file.Write("&nbsp<font size='6'>" + just1.ToString("N2") + "</font>");
                file.Write("<br>");
                file.Write("&nbsp<font size='5'>" + valor1.ToString("N2") + "</font>");
            }
            else
            {
                decimal valor2 = 0;
                DateTime fechaActual = DateTime.Now.Date;
                if (fechaActual <= Convert.ToDateTime("30/09/2021"))
                {
                    valor2 = (Convert.ToDecimal(justo1) / 1000000);
                }
                if (fechaActual > Convert.ToDateTime("30/09/2021 "))
                {
                    valor2 = (Convert.ToDecimal(justo1) * 1000000);
                }
                decimal just1 = Convert.ToDecimal(justo1);
                file.Write("PVJusto.BsS.:&nbsp<font size='6'>" + just1.ToString("N2") + "</font>");
                file.Write("<br>");
                file.Write("PVJusto.BsS.:&nbsp<font size='5'>" + valor2.ToString("N2") + "</font>");
            }
            file.Write("\r\n");
            file.Write("<br>");
            if (codigo1 == "")
            {
                file.Write("\r\n");
            }
            else
            {
                file.Write(linea_abajo + "\r\n");
            }
            file.Write("<br>");
            file.Write("\r\n");
            file.Write("</td><td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
            file.Write("\r\n");
            file.Write("<br>");
            file.Write("\r\n");
            file.Write("<td>");
            file.Write("\r\n");
            file.Write("<font size='2'>");
            file.Write("\r\n");
            file.Write(nombre2_2);
            file.Write("\r\n");
            file.Write("<br>");
            file.Write("\r\n");
            file.Write(rif2_2);
            for (int i = 1; i <= 4; ++i)
            {
                file.Write("&nbsp");
            }
            if (codigo2 == "")
            {
                file.Write("\r\n");
            }
            else
            {
                file.Write(data);
                file.Write("\r\n");
            }
            file.Write("<br>");
            file.Write("\r\n");
            file.Write(descripcion2_2_2 + "\r\n");
            file.Write("<br>");
            file.Write("\r\n");
            file.Write(linea2 + "\r\n");
            file.Write("<br>");
            file.Write("\r\n");
            if (codigo2 == "")
            {
                file.Write("&nbsp<font size='6'>" + codigo2_2 + "</font>&nbsp");
                file.Write("&nbsp<font size='6'>" + unidad2_2 + "</font>");
            }
            else
            {
                file.Write("Cod:&nbsp<font size='6'>" + codigo2_2 + "</font>&nbsp");
                file.Write("Und:&nbsp<font size='6'>" + unidad2_2 + "</font>");
            }
            file.Write("\r\n");
            file.Write("<br>");
            file.Write("\r\n");
            if (codigo2 == "")
            {
                decimal valor3 = 0;
                DateTime fechaActual = DateTime.Now.Date;
                if (fechaActual <= Convert.ToDateTime("30/09/2021"))
                {
                    valor3 = (Convert.ToDecimal(justo2) / 1000000);
                }
                if (fechaActual < Convert.ToDateTime("30/09/2021"))
                {
                    //                    valor3 = (Convert.ToDecimal(justo2) * 1000000);
                    valor3 = (Convert.ToDecimal(justo2) * 1000000);
                }
                decimal just2 = Convert.ToDecimal(justo2);
                file.Write("&nbsp<font size='6'>" + just2.ToString("N2") + "</font>");
                file.Write("<br>");
                file.Write("&nbsp<font size='5'>" + valor3.ToString("N2") + "</font>");
            }
            else
            {
                decimal valor4 = 0;
                DateTime fechaActual = DateTime.Now.Date;
                if (fechaActual <= Convert.ToDateTime("30/09/2021"))
                {
                    valor4 = (Convert.ToDecimal(justo2) / 1000000);
                }
                if (fechaActual > Convert.ToDateTime("30/09/2021 "))
                {
                    valor4 = (Convert.ToDecimal(justo2) * 1000000);
                }
                decimal just2 = Convert.ToDecimal(justo2);
                file.Write("PVJusto.BsS.:&nbsp<font size='6'>" + just2.ToString("N2") + "</font>");
                file.Write("<br>");
                file.Write("PVJusto.BsS.:&nbsp<font size='5'>" + valor4.ToString("N2") + "</font>");
            }
            file.Write("\r\n");
            file.Write("<br>");
            file.Write("\r\n");
            if (codigo2 == "")
            {
                file.Write("\r\n");
            }
            else
            {
                file.Write(linea_abajo2 + "\r\n");
            }
            file.Write("<br>");
            file.Write("\r\n");
            file.Write("</td>");
            file.Write("\r\n");
            file.Write("</tr>");
            file.Write("\r\n");
            file.Write("</table>");
            file.Write("\r\n");
            file.Write("<br>");
            file.Write("\r\n");
            file.Write("<center><input class='oculto' type='button' name='imprimir' value='Imprimir' onclick='window.print();'></center>");

            file.Write("</body>");
            file.Write("\r\n");
            file.Write("</html>");
            file.Close();
        }

        public void GenerarArchivo3_dolares(string nombre, string rif, string descripcion, string descripcion_1, string codigo,
    string unidad, string precio1, string nombre2, string rif2, string descripcion2, string descripcion2_2, string codigo2,
    string unidad2, string fecha, string precio2)
        {
            string nombre1 = nombre;
            string rif1 = rif;
            string data = fecha;
            string descripcion1_1_1 = descripcion;
            string linea1 = descripcion_1;
            string codigo1 = codigo;
            string unidad1 = unidad;
            string justo1 = precio1;
            string linea_abajo = "__________________________________";
            string nombre2_2 = nombre2;
            string rif2_2 = rif2;
            string descripcion2_2_2 = descripcion2;
            string linea2 = descripcion2_2;
            string codigo2_2 = codigo2;
            string unidad2_2 = unidad2;
            string justo2 = precio2;
            string linea_abajo2 = "__________________________________";

            int a = nombre.Length;
            int b = nombre2.Length;

            int a_a = descripcion_1.Length;
            int b_b = descripcion2_2.Length;

            this.SIni3 = Application.StartupPath + "\\Etiqueta3_Dolares.html";
            System.IO.StreamWriter file = new System.IO.StreamWriter(sIni3);
            file.Write("<html>");
            file.Write("\r\n");
            file.Write("<head>");
            file.Write("\r\n");
            //file.Write("<title>FLebrun - Etiquetas Grandes</title>");
            file.Write("\r\n");
            file.Write("<style type='text/css'>@media print {.oculto{display:none} table { border-spacing: 0;border-collapse: collapse;}}</style>");
            file.Write("\r\n");
            file.Write("</head>");
            file.Write("\r\n");
            file.Write("<body>");
            file.Write("\r\n");
            file.Write("<table border='0' cellspacing='0'>");
            file.Write("\r\n");
            file.Write("<tr>");
            file.Write("\r\n");
            file.Write("<td>");
            file.Write("\r\n");
            file.Write("<font size='2'>");
            file.Write("\r\n");
            file.Write(nombre);
            file.Write("\r\n");
            file.Write("<br>");
            file.Write("\r\n");
            file.Write(rif1);
            for (int i = 1; i <= 4; ++i)
            {
                file.Write("&nbsp");
            }
            if (codigo1 == "")
            {
                file.Write("\r\n");
            }
            else
            {
                file.Write(data);
                file.Write("\r\n");
            }
            file.Write("<br>");
            file.Write("\r\n");
            file.Write(descripcion1_1_1 + "\r\n");
            file.Write("<br>");
            file.Write("\r\n");
            file.Write(linea1);
            file.Write("\r\n");
            file.Write("<br>");
            file.Write("\r\n");
            if (codigo1 == "")
            {
                file.Write("&nbsp<font size='6'>" + codigo1 + "</font>&nbsp");
                file.Write("&nbsp<font size='6'>" + unidad1 + "</font>");
            }
            else
            {
                file.Write("Cod:&nbsp<font size='6'>" + codigo1 + "</font>&nbsp");
                file.Write("Und:&nbsp<font size='6'>" + unidad1 + "</font>");
            }
            file.Write("\r\n");
            file.Write("<br>");
            file.Write("\r\n");
            if (codigo2 == "")
            {

                //decimal valor1 = 0;
                //DateTime fechaActual = DateTime.Now.Date;
                //if (fechaActual <= Convert.ToDateTime("19/08/2018"))
                //{
                //    valor1 = (Convert.ToDecimal(justo1) / 100000);
                //}
                //if (fechaActual > Convert.ToDateTime("19/08/2018"))
                //{
                //    //                    valor1 = (Convert.ToDecimal(justo1) * 100000);
                //    valor1 = (Convert.ToDecimal(justo1) * 1);
                //}
                DataTable tablaDolar8;
                DataTable tablaeuro8;

                decimal valorDolar8 = 0;
                decimal valorEuro8 = 0;

                string sqldolar8 = "select histo_valor from admhistodivi where histo_moneda='$8' order by histo_fecha desc, histo_hora desc  limit 1";
                string sqleuro8 = "select histo_valor from admhistodivi where histo_moneda='€8' order by histo_fecha desc, histo_hora desc  limit 1";

                tablaDolar8 = databaseConection.fDataTable(sqldolar8);
                tablaeuro8 = databaseConection.fDataTable(sqleuro8);

                valorDolar8 = Convert.ToDecimal(tablaDolar8.Rows[0]["histo_valor"].ToString());
                valorEuro8 = Convert.ToDecimal(tablaeuro8.Rows[0]["histo_valor"].ToString());

                decimal precioMonedaDolar = Convert.ToDecimal(precio1.Replace(".", ","));
                decimal precioMonedaEuro = (Convert.ToDecimal(precio1.Replace(".", ",")) * valorDolar8) / valorEuro8;

            //  file.Write("Precio $.:&nbsp<font size='6'>" + precioMonedaDolar.ToString("N2") + "</font>");
                file.Write(" Refer.1.:&nbsp<font size='6'>" + precioMonedaDolar.ToString("N2") + "</font>");
                file.Write("<br>");
             //  file.Write("Precio E.:&nbsp<font size='5'>" + precioMonedaEuro.ToString("N2") + "</font>");
            }
            else
            {
                //decimal valor2 = 0;
                //DateTime fechaActual = DateTime.Now.Date;
                //if (fechaActual <= Convert.ToDateTime("19/08/2018"))
                //{
                //    valor2 = (Convert.ToDecimal(justo1) / 100000);
                //}
                //if (fechaActual > Convert.ToDateTime("19/08/2018 "))
                //{
                //    valor2 = (Convert.ToDecimal(justo1) * 1);
                //}
                //decimal just1 = Convert.ToDecimal(justo1);
                //file.Write("Precio $.:&nbsp<font size='6'>" + just1.ToString("N2") + "</font>");
                //file.Write("<br>");
                //file.Write("Precio E.:&nbsp<font size='5'>" + valor2.ToString("N2") + "</font>");

                //DataTable tablaDolar8;
                //decimal valorDolar8 = 0;
                //string dolar8 = "select histo_valor from admhistodivi where histo_moneda='$8' order by histo_fecha desc, histo_hora desc  limit 1";
                //tablaDolar8 = databaseConection.fDataTable(dolar8);

                //valorDolar8 = Convert.ToDecimal(tablaDolar8.Rows[0]["histo_valor"].ToString().Replace(",", "."));

                //decimal precioMonedaExtrajera = Convert.ToDecimal(precio1);
                //decimal precioMonedaExtrajeraxlocal = Convert.ToDecimal(precio1) * valorDolar8;

                //file.Write("&nbsp<font size='6'>" + precioMonedaExtrajera.ToString("N2") + "</font>");
                //file.Write("<br>");
                //file.Write("&nbsp<font size='5'>" + precioMonedaExtrajeraxlocal.ToString("N2") + "</font>");

                DataTable tablaDolar8;
                DataTable tablaeuro8;

                decimal valorDolar8 = 0;
                decimal valorEuro8 = 0;

                string sqldolar8 = "select histo_valor from admhistodivi where histo_moneda='$8' order by histo_fecha desc, histo_hora desc  limit 1";
                string sqleuro8 = "select histo_valor from admhistodivi where histo_moneda='€8' order by histo_fecha desc, histo_hora desc  limit 1";

                tablaDolar8 = databaseConection.fDataTable(sqldolar8);
                tablaeuro8 = databaseConection.fDataTable(sqleuro8);

                valorDolar8 = Convert.ToDecimal(tablaDolar8.Rows[0]["histo_valor"].ToString());
                valorEuro8 = Convert.ToDecimal(tablaeuro8.Rows[0]["histo_valor"].ToString());

                decimal precioMonedaDolar = Convert.ToDecimal(precio1.Replace(".",","));
                decimal precioMonedaEuro = (Convert.ToDecimal(precio1.Replace(".", ",")) * valorDolar8) / valorEuro8;

            //  file.Write("Precio $.:&nbsp<font size='6'>" + precioMonedaDolar.ToString("N2") + "</font>");
                file.Write(" Refer.1.:&nbsp<font size='6'>" + precioMonedaDolar.ToString("N2") + "</font>");
                file.Write("<br>");
            //  file.Write("Precio E.:&nbsp<font size='5'>" + precioMonedaEuro.ToString("N2") + "</font>");

            }
            file.Write("\r\n");
            file.Write("<br>");
            if (codigo1 == "")
            {
                file.Write("\r\n");
            }
            else
            {
                file.Write(linea_abajo + "\r\n");
            }
            file.Write("<br>");
            file.Write("\r\n");
            file.Write("</td><td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
            file.Write("\r\n");
            file.Write("<br>");
            file.Write("\r\n");
            file.Write("<td>");
            file.Write("\r\n");
            file.Write("<font size='2'>");
            file.Write("\r\n");
            file.Write(nombre2_2);
            file.Write("\r\n");
            file.Write("<br>");
            file.Write("\r\n");
            file.Write(rif2_2);
            for (int i = 1; i <= 4; ++i)
            {
                file.Write("&nbsp");
            }
            if (codigo2 == "")
            {
                file.Write("\r\n");
            }
            else
            {
                file.Write(data);
                file.Write("\r\n");
            }
            file.Write("<br>");
            file.Write("\r\n");
            file.Write(descripcion2_2_2 + "\r\n");
            file.Write("<br>");
            file.Write("\r\n");
            file.Write(linea2 + "\r\n");
            file.Write("<br>");
            file.Write("\r\n");
            if (codigo2 == "")
            {
                file.Write("&nbsp<font size='6'>" + codigo2_2 + "</font>&nbsp");
                file.Write("&nbsp<font size='6'>" + unidad2_2 + "</font>");
            }
            else
            {
                file.Write("Cod:&nbsp<font size='6'>" + codigo2_2 + "</font>&nbsp");
                file.Write("Und:&nbsp<font size='6'>" + unidad2_2 + "</font>");
            }
            file.Write("\r\n");
            file.Write("<br>");
            file.Write("\r\n");
            if (codigo2 == "")
            {
                //decimal valor3 = 0;
                //DateTime fechaActual = DateTime.Now.Date;
                //if (fechaActual <= Convert.ToDateTime("19/08/2018"))
                //{
                //    valor3 = (Convert.ToDecimal(justo2) / 100000);
                //}
                //if (fechaActual < Convert.ToDateTime("19/08/2018"))
                //{
                //    //                    valor3 = (Convert.ToDecimal(justo2) * 100000);
                //    valor3 = (Convert.ToDecimal(justo2) * 1);
                //}
                //decimal just2 = Convert.ToDecimal(justo2);
                //file.Write("&nbsp<font size='6'>" + just2.ToString("N2") + "</font>");
                //file.Write("<br>");
                //file.Write("&nbsp<font size='5'>" + valor3.ToString("N2") + "</font>");

                //DataTable tablaDolar8;
                //decimal valorDolar8 = 0;
                //string dolar8 = "select histo_valor from admhistodivi where histo_moneda='$8' order by histo_fecha desc, histo_hora desc  limit 1";
                //tablaDolar8 = databaseConection.fDataTable(dolar8);

                //valorDolar8 = Convert.ToDecimal(tablaDolar8.Rows[0]["histo_valor"].ToString().Replace(",", "."));

                //decimal precioMonedaExtrajera = Convert.ToDecimal(precio2);
                //decimal precioMonedaExtrajeraxlocal = Convert.ToDecimal(precio2) * valorDolar8;

                //file.Write("&nbsp<font size='6'>" + precioMonedaExtrajera.ToString("N2") + "</font>");
                //file.Write("<br>");
                //file.Write("&nbsp<font size='5'>" + precioMonedaExtrajeraxlocal.ToString("N2") + "</font>");

                DataTable tablaDolar8;
                DataTable tablaeuro8;

                decimal valorDolar8 = 0;
                decimal valorEuro8 = 0;

                string sqldolar8 = "select histo_valor from admhistodivi where histo_moneda='$8' order by histo_fecha desc, histo_hora desc  limit 1";
                string sqleuro8 = "select histo_valor from admhistodivi where histo_moneda='€8' order by histo_fecha desc, histo_hora desc  limit 1";

                tablaDolar8 = databaseConection.fDataTable(sqldolar8);
                tablaeuro8 = databaseConection.fDataTable(sqleuro8);

                valorDolar8 = Convert.ToDecimal(tablaDolar8.Rows[0]["histo_valor"].ToString());
                valorEuro8 = Convert.ToDecimal(tablaeuro8.Rows[0]["histo_valor"].ToString());

                decimal precioMonedaDolar = Convert.ToDecimal(precio2.Replace(".", ","));
                decimal precioMonedaEuro = (Convert.ToDecimal(precio2.Replace(".", ",")) * valorDolar8) / valorEuro8;

         //     file.Write("Precio $.:&nbsp<font size='6'>" + precioMonedaDolar.ToString("N2") + "</font>");
                file.Write(" Refer.1.:&nbsp<font size='6'>" + precioMonedaDolar.ToString("N2") + "</font>");
                file.Write("<br>");
        //      file.Write("Precio E.:&nbsp<font size='5'>" + precioMonedaEuro.ToString("N2") + "</font>");
            }
            else
            {
                //decimal valor4 = 0;
                //DateTime fechaActual = DateTime.Now.Date;
                //if (fechaActual <= Convert.ToDateTime("19/08/2018"))
                //{
                //    valor4 = (Convert.ToDecimal(justo2) / 100000);
                //}
                //if (fechaActual > Convert.ToDateTime("19/08/2018 "))
                //{
                //    valor4 = (Convert.ToDecimal(justo2) * 1);
                //}
                //decimal just2 = Convert.ToDecimal(justo2);
                //file.Write("Precio $.:&nbsp<font size='6'>" + just2.ToString("N2") + "</font>");
                //file.Write("<br>");
                //file.Write("Precio E.:&nbsp<font size='5'>" + valor4.ToString("N2") + "</font>");

                //DataTable tablaDolar8;
                //decimal valorDolar8 = 0;
                //string dolar8 = "select histo_valor from admhistodivi where histo_moneda='$8' order by histo_fecha desc, histo_hora desc  limit 1";
                //tablaDolar8 = databaseConection.fDataTable(dolar8);

                //valorDolar8 = Convert.ToDecimal(tablaDolar8.Rows[0]["histo_valor"].ToString().Replace(",", "."));

                //decimal precioMonedaExtrajera = Convert.ToDecimal(precio2);
                //decimal precioMonedaExtrajeraxlocal = Convert.ToDecimal(precio2) * valorDolar8;

                //file.Write("&nbsp<font size='6'>" + precioMonedaExtrajera.ToString("N2") + "</font>");
                //file.Write("<br>");
                //file.Write("&nbsp<font size='5'>" + precioMonedaExtrajeraxlocal.ToString("N2") + "</font>");

                DataTable tablaDolar8;
                DataTable tablaeuro8;

                decimal valorDolar8 = 0;
                decimal valorEuro8 = 0;

                string sqldolar8 = "select histo_valor from admhistodivi where histo_moneda='$8' order by histo_fecha desc, histo_hora desc  limit 1";
                string sqleuro8 = "select histo_valor from admhistodivi where histo_moneda='€8' order by histo_fecha desc, histo_hora desc  limit 1";

                tablaDolar8 = databaseConection.fDataTable(sqldolar8);
                tablaeuro8 = databaseConection.fDataTable(sqleuro8);

                valorDolar8 = Convert.ToDecimal(tablaDolar8.Rows[0]["histo_valor"].ToString());
                valorEuro8 = Convert.ToDecimal(tablaeuro8.Rows[0]["histo_valor"].ToString());

                decimal precioMonedaDolar = Convert.ToDecimal(precio2.Replace(".", ","));
                decimal precioMonedaEuro = (Convert.ToDecimal(precio2.Replace(".", ",")) * valorDolar8) / valorEuro8;

            //  file.Write("Precio $.:&nbsp<font size='6'>" + precioMonedaDolar.ToString("N2") + "</font>");
                file.Write(" Refer.1.:&nbsp<font size='6'>" + precioMonedaDolar.ToString("N2") + "</font>");
                file.Write("<br>");
            //    file.Write("Precio E.:&nbsp<font size='5'>" + precioMonedaEuro.ToString("N2") + "</font>");
            }
            file.Write("\r\n");
            file.Write("<br>");
            file.Write("\r\n");
            if (codigo2 == "")
            {
                file.Write("\r\n");
            }
            else
            {
                file.Write(linea_abajo2 + "\r\n");
            }
            file.Write("<br>");
            file.Write("\r\n");
            file.Write("</td>");
            file.Write("\r\n");
            file.Write("</tr>");
            file.Write("\r\n");
            file.Write("</table>");
            file.Write("\r\n");
            file.Write("<br>");
            file.Write("\r\n");
            file.Write("<center><input class='oculto' type='button' name='imprimir' value='Imprimir' onclick='window.print();'></center>");

            file.Write("</body>");
            file.Write("\r\n");
            file.Write("</html>");
            file.Close();
        }
        public bool actulizarStadoconecion()
        { 
            bool Ciclope = true;
            sentenciaSql = null;
            try
            {
                switch (opcion)
                {
                    case "1":
                        sentenciaSql = "UPDATE admserietiqpeq SET status='1' WHERE status='1' AND ip_estacion='" + ip + "';";
                        databaseConection.fDataTable(sentenciaSql);
                        break;
                    case "0":
                        sentenciaSql = "UPDATE admserietiqpeq SET status='0' WHERE status='1' AND ip_estacion='" + ip + "';";
                        databaseConection.fDataTable(sentenciaSql);
                        break;
                }
            }
            catch (Exception e)
            {
            }
            databaseConection.cerrarConexion();
            return Ciclope;
        }
        public bool insertDatosSistemas() 
        {
            bool Ciclope = true;
            sentenciaSql = null;
            try
            {
                sentenciaSql = "INSERT INTO admhistoetiqpeq(ip_estacion,fechaEjecucion,horaEjecucion,usuario,usuario_sistema,item1,undmed1," +
                "inv_codigo1,conten1,precio1,item2,undmed2,inv_codigo2,conten2,precio2,item3,undmed3,inv_codigo3,conten3," +
                "precio3,item4,inv_codigo4,undmed4,conten4,precio4,item5,undmed5,inv_codigo5,conten5,precio5)VALUES('" + ip+ "','" + DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day + "'," +
                "'" + DateTime.Now.ToString("hh:mm:ss") + "','" + id + "','" + id+ "','1','"+undmed1+"','"+@inv_codigo1+"','"+conten1+"','"+precio1+"','2','"+undmed2+"'," +
                "'"+inv_codigo2+"','"+conten2+"','"+precio2+"','3','"+undmed3+"','"+inv_codigo3+"','"+conten3+"','"+precio3+"','4','"+undmed4+"'," +
                "'" + inv_codigo4 + "','" + conten4 + "','" + @precio4 + "','5','" + undmed5+ "','" + inv_codigo5 + "','" + conten5 + "','" + precio5 + "');";
                // Replazar Por La Informacion DE la Etiquetas
                /*sentenciaSql = sentenciaSql.Replace("@fechaEjecucion", DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day);
                 sentenciaSql = sentenciaSql.Replace("@horaEjecucion", DateTime.Now.ToString("hh:mm:ss"));*/
                databaseConection.ejecutarInsert2(sentenciaSql);
                databaseConection.cerrarConexion();
            }
            catch (Exception e)
            {
            }
            databaseConection.cerrarConexion();
            return Ciclope;
        }
        public bool insertDatosEtiqpeqDesg() 
        {
            bool Ciclope = true;
            sentenciaSql = null;
            try
            {
                sentenciaSql = "INSERT INTO admhistoetiqpeqdeg(ip_estacion,fechaEjecucion,horaEjecucion,usuario,usuario_sistema," +
                    "item1,undmed1,inv_codigo1,conten1,mbase1,iva1,precio1,item2,undmed2,inv_codigo2,conten2,"+
                    "mbase2, iva2,precio2,item3,undmed3,inv_codigo3,conten3,mbase3,iva3,precio3,item4,inv_codigo4,"+
                    "undmed4,conten4,mbase4,iva4,precio4,item5,undmed5,inv_codigo5,conten5,mbase5,iva5,precio5)VALUE('" + ip + "','" + DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day + "'," +
                "'" + DateTime.Now.ToString("hh:mm:ss") + "','" + id + "','" + id + "','1','" + undmed1 + "','" + @inv_codigo1 + "','" + conten1 + "','" + montob1+ "','" + civa1 + "','" + precio1 + "','2','" + undmed2 + "'," +
                "'" + inv_codigo2 + "','" + conten2 + "','" + montob2 + "','" + civa2 + "','" + precio2 + "','3','" + undmed3 + "','" + inv_codigo3 + "','" + conten3 + "','" + montob3 + "','" + civa3 + "','" + precio3 + "','4','" + undmed4 + "'," +
                "'" + inv_codigo4 + "','" + conten4 + "','" + montob4 + "','" + civa4 + "','" + @precio4 + "','5','" + undmed5 + "','" + inv_codigo5 + "','" + conten5 + "','" + montob5 + "','" + civa5 + "','" + precio5 + "')";
                databaseConection.ejecutarInsert2(sentenciaSql);
                databaseConection.cerrarConexion();
            }
            catch (Exception e)
            {
            }
            databaseConection.cerrarConexion();
            return Ciclope;
        }
        public decimal decimalRedondear(decimal Argunmento, int Digitos) 
        {
            decimal redondeo = decimal.Round(Argunmento, Digitos);
            return redondeo;
        }
        //public static void ShowDecimalRound(decimal Argument, int Digits)
        //{
        //    decimal rounded = decimal.Round(Argument, Digits);
        //}
    }
}