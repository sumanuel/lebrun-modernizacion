using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using lebrun.clases.complementos;
using lebrun.clasesData;
using lebrun.clases.producto;
using System.IO;

namespace lebrun.formularios.productos
{
    public partial class frmEtiqMed : Form
    {
        Principal SistemaUs;// llamado al la informacion de la pataklla principal
        ConexionBD databaseConection;// llamada a la ventana conexion a la base de datos
        //Solo monto de precio
        public decimal Etiprecio1 = 0;
        public decimal Etiprecio2 = 0;
        public decimal Etiprecio3 = 0;
        public decimal Etiprecio4 = 0;
        public string codigo1 = "";
        public string codigo2 = "";
        public string codigo3 = "";
        public string codigo4 = "";
        public string UnidadM1 ="";
        public string UnidadM2 ="";
        public string UnidadM3 ="";
        public string UnidadM4 ="";
        string updatesql;
        public string opcion = "0";//control de cierre
        int j = 25;
        int i = 1;
        int a;
        public frmEtiqMed()
        {
            InitializeComponent();
        }

        string[] Registro = new string[26];

        public string[] Registros
        {
            get { return Registro; }
            set { Registro = value; }
        }

        string[] descripcion = new string[80];
        string[] temporal = new string[80];

        Producto objeto = new Producto();
        FuncionesTexbox funcion = new FuncionesTexbox();
        Compania datos = new Compania();
        Etiqueta imprimir = new Etiqueta();


        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "1";
            textBox2.Focus();
            label8.Text = "";
            label9.Text = "";
            label10.Text = "";
            label11.Text = "";
            label12.Text = "";
            label13.Text = "";
            label14.Text = "";
            label15.Text = "";
            label16.Text = "";
            label17.Text = "";
            label18.Text = "";
            label19.Text = "";
            label20.Text = "";
            label21.Text = "";
            label22.Text = "";
            label23.Text = "";
            label24.Text = "";
            label25.Text = "";
            label26.Text = "";
            label27.Text = "";
            label28.Text = "";
            label29.Text = "";
            label30.Text = "";
            label31.Text = "";
            label32.Text = "";
            label33.Text = "";

            for (i = 0; i <= 24; ++i)
            {
                Registro[i] = "";
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            funcion.txtWithoutPunctuation_Symbol_Letter(this, e);
            if (e.KeyChar == 13 && textBox1.Text.Length == 0)
            {
                MessageBox.Show("Debe ingresar un numero de Item", "Mensaje del Sistema");
            }
            if (e.KeyChar == 13 && textBox1.Text.Length != 0)
            {
                int i;
                i = int.Parse(textBox1.Text);
                if ((i > 4) || (i < 1))
                {
                    MessageBox.Show("El numero de Item debe estar entre 1 y 4", "Mensaje del Sistema");
                }

                else
                {
                    textBox2.Focus();
                }
            }
        }


        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((textBox2.Text.Length == 0) && (e.KeyChar == 13))
            {
                MessageBox.Show("Ingrese el codigo del articulo", "Mensaje del Sistema");
                textBox1.Focus();
            }
            funcion.txtOnlyDecimal(this, e, textBox2);
            if ((textBox2.Text.Length > 0 && e.KeyChar == 13))
            {
                textBox2.Text = string.Format("{0:000000000000000}", Convert.ToDouble(textBox2.Text));
                DataTable rows;
                rows = objeto.buscarProductoUnico(textBox2.Text);
                if (rows.Rows.Count > 0)
                {
                    if ((rows.Rows[0][0].ToString() == textBox2.Text))
                    {

                        string valor;
                        string ultCosto;
                        string exis;
                        string medUni;
                        valor = objeto.Validar_Existencia(textBox2.Text);
                        ultCosto = objeto.Validar_Costo(textBox2.Text);
                        exis = objeto.Validar_Diponible(textBox2.Text);
                        medUni = objeto.Validar_UnidaPrincipal(textBox2.Text);
                        if (valor == "0")
                        {
                            MessageBox.Show("Producto no Disponible", "Mensaje del Sistema");
                            textBox2.Text = "";
                            textBox2.Focus();
                        }

                        else if (ultCosto == "0")
                        {
                            MessageBox.Show("El Producto Se Encuentra En Estado CP. ", "Mensaje del Sistema");
                            textBox2.Text = "";
                            textBox2.Focus();

                        }
                        else if (exis == "0")
                        {
                            MessageBox.Show("El Producto No Esta Disponible", "Mensaje del Sistema");
                            textBox2.Text = "";
                            textBox2.Focus();
                        }
                        else if (medUni == "0")
                        {
                            MessageBox.Show("NO tiene UND asisnada", "Mensaje del Sistema");
                            textBox2.Text = "";
                            textBox2.Focus();
                        }
                        else
                        {
                            objeto.cargarDatosProd(textBox2.Text);
                            objeto.buscarPrecios(textBox2.Text, "A");
                            label29.Text = objeto.obtenerImePresen(textBox2.Text);
                            comboBox1.Text = objeto.UnidadMedida[0].ToString();
                            comboBox1.Items.Add(objeto.UnidadMedida[0].ToString());
                            label28.Visible = true;
                            label28.Text = objeto.Descripcion;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Producto no Existe", "Mensaje del Sistema");
                    textBox2.Focus();
                }
            }
        }

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            funcion.txtWithoutNumber(this, e);
            funcion.txtWithoutPunctuation_Symbol_Letter(this, e);
            datos.obtenerCodigoCompaniaActual();
            a = Convert.ToInt16(textBox1.Text);
            string rif = datos.Rif;
            if ((e.KeyChar == 13) && (textBox2.Text.Length > 0 && textBox1.Text.Length > 0 &&
            comboBox1.Text.Length > 0))
            {
                if (textBox1.Text == "1")
                {
                    i = 1;
                    label8.Visible = true;
                    label8.Text = datos.Nombre;
                    Registro[i] = label8.Text;
                    ++i;
                    label9.Visible = true;
                    label9.Text = "Rif:" + datos.Rif;
                    Registro[i] = label9.Text;
                    ++i;
                    label10.Visible = true;
                    if (objeto.Descripcion.Length >= 25)
                    {
                        label10.Text = string.Format(objeto.Descripcion[0].ToString() + objeto.Descripcion[1].ToString() + objeto.Descripcion[2].ToString() + objeto.Descripcion[3].ToString() + objeto.Descripcion[4].ToString() + objeto.Descripcion[5].ToString() +
                    objeto.Descripcion[6].ToString() + objeto.Descripcion[7].ToString() + objeto.Descripcion[8].ToString() + objeto.Descripcion[9].ToString() + objeto.Descripcion[10].ToString() + objeto.Descripcion[11].ToString() + objeto.Descripcion[12].ToString() + objeto.Descripcion[13].ToString() +
                    objeto.Descripcion[14].ToString() + objeto.Descripcion[15].ToString() + objeto.Descripcion[16].ToString() + objeto.Descripcion[17].ToString() + objeto.Descripcion[18].ToString() + objeto.Descripcion[19].ToString() + objeto.Descripcion[20].ToString() + objeto.Descripcion[21].ToString() +
                    objeto.Descripcion[22].ToString() + objeto.Descripcion[23].ToString() + objeto.Descripcion[24].ToString());
                        Registro[i] = label10.Text;
                        ++i;
                    }

                    if (objeto.Descripcion.Length > 25)
                    {
                        while (j < objeto.Descripcion.Length)
                        {
                            descripcion[j] = objeto.Descripcion[j].ToString();
                            j = j + 1;
                        }
                        while (j <= 49)
                        {
                            descripcion[j] = "";
                            j = j + 1;
                        }
                        label30.Visible = true;
                        label30.Text = string.Format(descripcion[25] + descripcion[26] + descripcion[27].ToString() + descripcion[28] + descripcion[29] + descripcion[30] +
                descripcion[31] + descripcion[32] + descripcion[33] + descripcion[34] + descripcion[35] + descripcion[36] + descripcion[37] + descripcion[38] +
                descripcion[39] + descripcion[40] + descripcion[41] + descripcion[42] + descripcion[43] + descripcion[44] + descripcion[45] + descripcion[46] +
                descripcion[47] + descripcion[48] + descripcion[49]);
                        j = 25;
                        Registro[i] = label30.Text;
                        ++i;
                    }
                    else
                    {
                        label10.Text = objeto.Descripcion;
                        Registro[i] = label10.Text;
                        ++i;
                    }
                    label11.Visible = true;
                    label11.Text = string.Format("{0:000000000}", Convert.ToDouble(textBox2.Text));
                    Etiprecio1 = objeto.Precio;
                    codigo1 = label11.Text;
                    label11.Text = "Codigo:" + label11.Text;
                    Registro[i] = label11.Text;
                    ++i;
                    label12.Visible = true;
                    UnidadM1 = objeto.UnidadMedida[0].ToString();
                    label12.Text = "Unidad:" + objeto.UnidadMedida[0].ToString();
                    Registro[i] = label12.Text;
                    textBox2.Text = "";
                    textBox2.Focus();
                    comboBox1.Text = "";
                    comboBox1.Items.Clear();
                    label28.Text = "";
                    label29.Text = "";
                    ++a;
                    textBox1.Text = a.ToString();
                }

                else if (textBox1.Text == "2")
                {
                    i = 7;
                    label13.Visible = true;
                    label13.Text = datos.Nombre;
                    Registro[i] = label13.Text;
                    ++i;
                    label14.Visible = true;
                    label14.Text = "Rif:" + datos.Rif;
                    Registro[i] = label14.Text;
                    ++i;
                    label15.Visible = true;
                    if (objeto.Descripcion.Length >= 25)
                    {
                        label15.Text = string.Format(objeto.Descripcion[0].ToString() + objeto.Descripcion[1].ToString() + objeto.Descripcion[2].ToString() + objeto.Descripcion[3].ToString() + objeto.Descripcion[4].ToString() + objeto.Descripcion[5].ToString() +
                    objeto.Descripcion[6].ToString() + objeto.Descripcion[7].ToString() + objeto.Descripcion[8].ToString() + objeto.Descripcion[9].ToString() + objeto.Descripcion[10].ToString() + objeto.Descripcion[11].ToString() + objeto.Descripcion[12].ToString() + objeto.Descripcion[13].ToString() +
                    objeto.Descripcion[14].ToString() + objeto.Descripcion[15].ToString() + objeto.Descripcion[16].ToString() + objeto.Descripcion[17].ToString() + objeto.Descripcion[18].ToString() + objeto.Descripcion[19].ToString() + objeto.Descripcion[20].ToString() + objeto.Descripcion[21].ToString() +
                    objeto.Descripcion[22].ToString() + objeto.Descripcion[23].ToString() + objeto.Descripcion[24].ToString());
                        Registro[i] = label15.Text;
                        ++i;
                    }
                    if (objeto.Descripcion.Length > 25)
                    {
                        while (j < objeto.Descripcion.Length)
                        {
                            descripcion[j] = objeto.Descripcion[j].ToString();
                            j = j + 1;
                        }
                        while (j <= 49)
                        {
                            descripcion[j] = "";
                            j = j + 1;
                        }
                        label31.Visible = true;
                        label31.Text = string.Format(descripcion[25] + descripcion[26] + descripcion[27].ToString() + descripcion[28] + descripcion[29] + descripcion[30] +
                descripcion[31] + descripcion[32] + descripcion[33] + descripcion[34] + descripcion[35] + descripcion[36] + descripcion[37] + descripcion[38] +
                descripcion[39] + descripcion[40] + descripcion[41] + descripcion[42] + descripcion[43] + descripcion[44] + descripcion[45] + descripcion[46] +
                descripcion[47] + descripcion[48] + descripcion[49]);
                        Registro[i] = label31.Text;
                        ++i;
                        j = 25;
                    }
                    else
                    {
                        label15.Text = objeto.Descripcion;
                        Registro[i] = label15.Text;
                        ++i;
                    }
                    label16.Visible = true;
                    label16.Text = string.Format("{0:000000000}", Convert.ToDouble(textBox2.Text));
                    Etiprecio2 = objeto.Precio;
                    codigo2 = label16.Text;
                    label16.Text = "Codigo:" + label16.Text;
                    Registro[i] = label16.Text;
                    ++i;
                    label17.Visible = true;
                    UnidadM2 = objeto.UnidadMedida[0].ToString();
                    label17.Text = "Unidad:" + objeto.UnidadMedida[0].ToString();
                    Registro[i] = label17.Text;
                    textBox2.Text = "";
                    textBox2.Focus();
                    comboBox1.Text = "";
                    comboBox1.Items.Clear();
                    label28.Text = "";
                    label29.Text = "";
                    ++a;
                    textBox1.Text = a.ToString();
                }

                else if (textBox1.Text == "3")
                {
                    i = 13;
                    label18.Visible = true;
                    label18.Text = datos.Nombre;
                    Registro[i] = label18.Text;
                    ++i;
                    label19.Visible = true;
                    label19.Text = "Rif:" + datos.Rif;
                    Registro[i] = label19.Text;
                    ++i;
                    label20.Visible = true;
                    if (objeto.Descripcion.Length >= 25)
                    {
                        label20.Text = string.Format(objeto.Descripcion[0].ToString() + objeto.Descripcion[1].ToString() + objeto.Descripcion[2].ToString() + objeto.Descripcion[3].ToString() + objeto.Descripcion[4].ToString() + objeto.Descripcion[5].ToString() +
                    objeto.Descripcion[6].ToString() + objeto.Descripcion[7].ToString() + objeto.Descripcion[8].ToString() + objeto.Descripcion[9].ToString() + objeto.Descripcion[10].ToString() + objeto.Descripcion[11].ToString() + objeto.Descripcion[12].ToString() + objeto.Descripcion[13].ToString() +
                    objeto.Descripcion[14].ToString() + objeto.Descripcion[15].ToString() + objeto.Descripcion[16].ToString() + objeto.Descripcion[17].ToString() + objeto.Descripcion[18].ToString() + objeto.Descripcion[19].ToString() + objeto.Descripcion[20].ToString() + objeto.Descripcion[21].ToString() +
                    objeto.Descripcion[22].ToString() + objeto.Descripcion[23].ToString() + objeto.Descripcion[24].ToString());
                        Registro[i] = label20.Text;
                        ++i;
                    }
                    if (objeto.Descripcion.Length > 25)
                    {
                        while (j < objeto.Descripcion.Length)
                        {
                            descripcion[j] = objeto.Descripcion[j].ToString();
                            j = j + 1;
                        }
                        while (j <= 49)
                        {
                            descripcion[j] = "";
                            j = j + 1;
                        }
                        label32.Visible = true;
                        label32.Text = string.Format(descripcion[25] + descripcion[26] + descripcion[27].ToString() + descripcion[28] + descripcion[29] + descripcion[30] +
                descripcion[31] + descripcion[32] + descripcion[33] + descripcion[34] + descripcion[35] + descripcion[36] + descripcion[37] + descripcion[38] +
                descripcion[39] + descripcion[40] + descripcion[41] + descripcion[42] + descripcion[43] + descripcion[44] + descripcion[45] + descripcion[46] +
                descripcion[47] + descripcion[48] + descripcion[49]);
                        Registro[i] = label32.Text;
                        ++i;
                        j = 25;
                    }
                    else
                    {
                        label20.Text = objeto.Descripcion;
                        Registro[i] = label20.Text;
                        ++i;
                    }
                    label21.Visible = true;
                    label21.Text = string.Format("{0:000000000}", Convert.ToDouble(textBox2.Text));
                    codigo3 = label21.Text;
                    Etiprecio3= objeto.Precio;
                    label21.Text = "Codigo:" + label21.Text;
                    Registro[i] = label21.Text;
                    ++i;
                    label22.Visible = true;
                    UnidadM3=objeto.UnidadMedida[0].ToString();
                    label22.Text = "Unidad:" + objeto.UnidadMedida[0].ToString();
                    Registro[i] = label22.Text;
                    textBox2.Text = "";
                    textBox2.Focus();
                    comboBox1.Text = "";
                    comboBox1.Items.Clear();
                    label28.Text = "";
                    label29.Text = "";
                    ++a;
                    textBox1.Text = a.ToString();
                }

                else if (textBox1.Text == "4")
                {
                    i = 19;
                    label23.Visible = true;
                    label23.Text = datos.Nombre;
                    Registro[i] = label23.Text;
                    ++i;
                    label24.Visible = true;
                    label24.Text = "Rif:" + datos.Rif;
                    Registro[i] = label24.Text;
                    ++i;
                    label25.Visible = true;
                    if (objeto.Descripcion.Length >= 25)
                    {
                        label25.Text = string.Format(objeto.Descripcion[0].ToString() + objeto.Descripcion[1].ToString() + objeto.Descripcion[2].ToString() + objeto.Descripcion[3].ToString() + objeto.Descripcion[4].ToString() + objeto.Descripcion[5].ToString() +
                    objeto.Descripcion[6].ToString() + objeto.Descripcion[7].ToString() + objeto.Descripcion[8].ToString() + objeto.Descripcion[9].ToString() + objeto.Descripcion[10].ToString() + objeto.Descripcion[11].ToString() + objeto.Descripcion[12].ToString() + objeto.Descripcion[13].ToString() +
                    objeto.Descripcion[14].ToString() + objeto.Descripcion[15].ToString() + objeto.Descripcion[16].ToString() + objeto.Descripcion[17].ToString() + objeto.Descripcion[18].ToString() + objeto.Descripcion[19].ToString() + objeto.Descripcion[20].ToString() + objeto.Descripcion[21].ToString() +
                    objeto.Descripcion[22].ToString() + objeto.Descripcion[23].ToString() + objeto.Descripcion[24].ToString());
                        Registro[i] = label25.Text;
                        ++i;
                    }
                    if (objeto.Descripcion.Length > 25)
                    {
                        while (j < objeto.Descripcion.Length)
                        {
                            descripcion[j] = objeto.Descripcion[j].ToString();
                            j = j + 1;
                        }
                        while (j <= 49)
                        {
                            descripcion[j] = "";
                            j = j + 1;
                        }
                        label33.Visible = true;
                        label33.Text = string.Format(descripcion[25] + descripcion[26] + descripcion[27].ToString() + descripcion[28] + descripcion[29] + descripcion[30] +
                descripcion[31] + descripcion[32] + descripcion[33] + descripcion[34] + descripcion[35] + descripcion[36] + descripcion[37] + descripcion[38] +
                descripcion[39] + descripcion[40] + descripcion[41] + descripcion[42] + descripcion[43] + descripcion[44] + descripcion[45] + descripcion[46] +
                descripcion[47] + descripcion[48] + descripcion[49]);
                        j = 25;
                        Registro[i] = label33.Text;
                        ++i;
                    }
                    else
                    {
                        label25.Text = objeto.Descripcion;
                        Registro[i] = label25.Text;
                        ++i;
                    }
                    label26.Visible = true;
                    label26.Text = string.Format("{0:000000000}", Convert.ToDouble(textBox2.Text));
                    codigo4 = label26.Text;
                    Etiprecio4 = objeto.Precio;
                    label26.Text = "Codigo:" + label26.Text;
                    Registro[i] = label26.Text;
                    ++i;
                    label27.Visible = true;
                    UnidadM4 = objeto.UnidadMedida[0].ToString();
                    label27.Text = "Unidad:" + objeto.UnidadMedida[0].ToString();
                    Registro[i] = label27.Text;
                    textBox2.Text = "";
                    textBox2.Focus();
                    comboBox1.Text = "";
                    comboBox1.Items.Clear();
                    label28.Text = "";
                    label29.Text = "";
                    ++a;
                    textBox1.Text = a.ToString();
                    a = 1;
                    textBox1.Text = a.ToString();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SistemaUs = (Principal)this.MdiParent;
            string sentenciaSql;
            if (label11.Text.Length > 0)
            {
                for (i = 1; i <= 6; ++i)
                {
                    Registro[i] = string.Format("{0}", Convert.ToString(Registro[i]));
                }
            }

            if (label15.Text.Length > 0)
            {
                for (i = 7; i <= 12; ++i)
                {
                    Registro[i] = string.Format("{0}", Convert.ToString(Registro[i]));
                }
            }

            if (label19.Text.Length > 0)
            {
                for (i = 13; i <= 18; ++i)
                {
                    Registro[i] = string.Format("{0}", Convert.ToString(Registro[i]));
                }
            }

            if (label23.Text.Length > 0)
            {
                for (i = 19; i <= 24; ++i)
                {
                    Registro[i] = string.Format("{0}", Convert.ToString(Registro[i]));
                }
            }

            string rif = datos.Rif;
            
            if ((((Registro[1] == "") || (Registro[1] == null)) && ((Registro[7] == "") || (Registro[7] == null)) && (Registro[13] == "") || (Registro[13] == null)) && (Registro[19] == "") || (Registro[19] == null))
            {
                MessageBox.Show("Debe de completar al menos un cuadro", "Mensaje del Sistema");
            }
            else
            {
                sentenciaSql = "INSERT INTO admhistoetiqmed(ip_estacion,fechaEjecucion,horaEjecucion,usuario,usuario_sistema,item1,undmed1," +
                     "inv_codigo1,conten1,precio1,item2,undmed2,inv_codigo2,conten2,precio2,item3,undmed3,inv_codigo3,conten3," +
                     "precio3,item4,undmed4,inv_codigo4,conten4,precio4)VALUES('" + SistemaUs.usuarioActual.IpPc + "','" + DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day + "'," +
                     "'" + DateTime.Now.ToString("hh:mm:ss") + "','@usuario','@usuario','1','@undmed1','@inv_codigo1','@conten1','@precio1','2','@undmed2'," +
                     "'@inv_codigo2','@conten2','@precio2','3','@undmed3','@inv_codigo3','@conten3','@precio3','4','@undmed4'," +
                     "'@inv_codigo4','@conten4','@precio4');";

                sentenciaSql = sentenciaSql.Replace("@usuario", SistemaUs.usuarioActual.Id);
                sentenciaSql = sentenciaSql.Replace("@usuariosistema", SistemaUs.usuarioActual.Id);
                if (label13.Text == "")
                {
                    sentenciaSql = sentenciaSql.Replace("@undmed1", "");
                    sentenciaSql = sentenciaSql.Replace("@inv_codigo1", "");
                    sentenciaSql = sentenciaSql.Replace("@conten1", "");
                    sentenciaSql = sentenciaSql.Replace("@precio1", "0.00");
                }
                else
                {
                    sentenciaSql = sentenciaSql.Replace("@undmed1", UnidadM1);
                    sentenciaSql = sentenciaSql.Replace("@inv_codigo1",codigo1);
                    sentenciaSql = sentenciaSql.Replace("@conten1", "0.00");
                    sentenciaSql = sentenciaSql.Replace("@precio1", Etiprecio1.ToString().Replace(",", "."));
                }
                if (label17.Text == "")
                {
                    sentenciaSql = sentenciaSql.Replace("@undmed2", "");
                    sentenciaSql = sentenciaSql.Replace("@inv_codigo2", "");
                    sentenciaSql = sentenciaSql.Replace("@conten2", "");
                    sentenciaSql = sentenciaSql.Replace("@precio2", "0.00");
                }
                else
                {
                    sentenciaSql = sentenciaSql.Replace("@undmed2", UnidadM2);
                    sentenciaSql = sentenciaSql.Replace("@inv_codigo2",codigo2);
                    sentenciaSql = sentenciaSql.Replace("@conten2", "0.00");
                    sentenciaSql = sentenciaSql.Replace("@precio2", Etiprecio2.ToString().Replace(",", "."));
                }

                if (label21.Text == "")
                {
                    sentenciaSql = sentenciaSql.Replace("@undmed3", "");
                    sentenciaSql = sentenciaSql.Replace("@inv_codigo3", "");
                    sentenciaSql = sentenciaSql.Replace("@conten3", "");
                    sentenciaSql = sentenciaSql.Replace("@precio3", "0.00");
                }
                else
                {
                    sentenciaSql = sentenciaSql.Replace("@undmed3", UnidadM3);
                    sentenciaSql = sentenciaSql.Replace("@inv_codigo3", codigo3);
                    sentenciaSql = sentenciaSql.Replace("@conten3", "0.00");
                    sentenciaSql = sentenciaSql.Replace("@precio3", Etiprecio3.ToString().Replace(",", "."));
                }

                if (label25.Text == "")
                {
                    sentenciaSql = sentenciaSql.Replace("@undmed4", "");
                    sentenciaSql = sentenciaSql.Replace("@inv_codigo4", "");
                    sentenciaSql = sentenciaSql.Replace("@conten4", "");
                    sentenciaSql = sentenciaSql.Replace("@precio4", "0.00");
                }
                else
                {
                    sentenciaSql = sentenciaSql.Replace("@undmed4", UnidadM4);
                    sentenciaSql = sentenciaSql.Replace("@inv_codigo4",codigo4);
                    sentenciaSql = sentenciaSql.Replace("@conten4", "0.00");
                    sentenciaSql = sentenciaSql.Replace("@precio4", Etiprecio4.ToString().Replace(",", "."));
                }
              
                databaseConection.ejecutarInsert2(sentenciaSql);
                databaseConection.cerrarConexion();
                imprimir.GenerarArchivo2(Registros[1].ToString(), Registros[2].ToString(), Registros[3].ToString(), Registros[4].ToString(),
                    Registros[5].ToString(), Registros[6].ToString(), Registros[7].ToString(), Registros[8].ToString(), Registros[9].ToString(), Registros[10].ToString(),
                    Registros[11].ToString(), Registros[12].ToString(), Registros[13].ToString(), Registros[14].ToString(), Registros[15].ToString(), Registros[16].ToString(),
                    Registros[17].ToString(), Registros[18].ToString(), Registros[19].ToString(), Registros[20].ToString(), Registros[21].ToString(), Registros[22].ToString(),
                    Registros[23].ToString(), Registros[24].ToString());

                imprimir.ImprimirArchivo(File.ReadAllText(imprimir.SIni2));
                textBox2.Focus();
            }
        }

        private void frmEtiqMed_Load(object sender, EventArgs e)
        {
            databaseConection = new ConexionBD();
        }

        private void label34_Click(object sender, EventArgs e)
        {

        }

        private void frmEtiqMed_FormClosed(object sender, FormClosedEventArgs e)
        {
            switch (opcion)
            {
                case "1":
                    updatesql = "UPDATE admserietiqmed SET status='1' WHERE status='1' AND ip_estacion='" + SistemaUs.usuarioActual.IpPc + "';";
                    databaseConection.fDataTable(updatesql);
                    break;
                case "0":
                    updatesql = "UPDATE admserietiqmed SET status='0' WHERE status='1' AND ip_estacion='" + SistemaUs.usuarioActual.IpPc + "';";
                    databaseConection.fDataTable(updatesql);
                    break;
            }
        }

        private void frmEtiqMed_Shown(object sender, EventArgs e)
        {
            //consulta verficacion de usuario
            SistemaUs = (Principal)this.MdiParent;
            string updatesql;
            DataTable dt;
            DataTable dt1;
            DataTable dt2;
            string consultasql  = "SELECT status FROM admserietiqpeq WHERE status='1'AND usuario='" + SistemaUs.usuarioActual.Id + "';";
            string consultasqlm = "SELECT status FROM admserietiqmed WHERE status='1'AND usuario='" + SistemaUs.usuarioActual.Id + "';";
            string consultasqlg = "SELECT status FROM admserietiqgrd WHERE status='1'AND usuario='" + SistemaUs.usuarioActual.Id + "';";
            dt = databaseConection.fDataTable(consultasql);
            dt1 = databaseConection.fDataTable(consultasqlm);
            dt2 = databaseConection.fDataTable(consultasqlg);
            if (dt.Rows.Count > 0 )
            {
                if (dt.Rows[0]["status"].ToString().Equals("1"))
                {
                    Close();                 
                    frmValidacionUserTimer timer = new frmValidacionUserTimer();
                    timer.Show();
                }        
            }
            else if (dt1.Rows.Count > 0)
            {
                if (dt1.Rows[0]["status"].ToString().Equals("1"))
                {
                    opcion = "1";
                    Close();
                    frmValidacionUserTimer timer = new frmValidacionUserTimer();
                    timer.Show();
                }
            }
            else if (dt2.Rows.Count > 0)
            {
                if (dt2.Rows[0]["status"].ToString().Equals("1"))
                {
                    Close();
                    frmValidacionUserTimer timer = new frmValidacionUserTimer();
                    timer.Show();
                }          
            }
            else
            {
                updatesql = "UPDATE admserietiqmed SET usuario='" + SistemaUs.usuarioActual.Id + "',usuario_sistema='" + SistemaUs.usuarioActual.Id + "',status='1' WHERE status='0' AND ip_estacion='" + SistemaUs.usuarioActual.IpPc + "';";
                databaseConection.fDataTable(updatesql);
            }
        }
    }

}