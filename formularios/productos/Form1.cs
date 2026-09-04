using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using lebrun.clasesData;
using lebrun.clases.complementos;
using lebrun.clases.producto;
using System.IO;
using lebrun.formularios.facturacion;
using lebrun;
using MySql.Data.MySqlClient;

//Etiqueta Pequeña Vercion 2.0 Del 10/09/2015 Fabian Hooker

namespace lebrun.formularios.productos
{
    public partial class Form1 : Form
    {
        Principal SistemaUs;// llamado al la informacion de la pataklla principal
        ConexionBD databaseConection;// llamada a la ventana conexion a la base de datos
        Form1 objFrmGrafik;
       Etiqueta etiq;
        //Solo monto de precio
        public decimal Etiprecio1 = 0;
        public decimal Etiprecio2 = 0;
        public decimal Etiprecio3 = 0;
        public decimal Etiprecio4 = 0;
        public decimal Etiprecio5 = 0;
        public string opcion;//control de cierre
        public string updatesql;
        public string ip;
        public string id;
        public int contador;
        
        int i = 1;
        int a;
        int j = 1;
        public Form1()
        {
            InitializeComponent();
        }
        string[] Registro = new string[28];
        string[] ivas = new string[2];

        public string[] Registros
        {
            get { return Registro; }
            set { Registro = value; }
        }

        public string[] Ivas
        {
            get { return ivas; }
            set { ivas = value; }
        }

        public int J
        {
            get { return j; }
            set { j = value; }
        }

        Producto objeto = new Producto();
        FuncionesTexbox funcion = new FuncionesTexbox();
        Etiqueta imprimir = new Etiqueta();
        Compania datos = new Compania();
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
                if ((i > 5) || (i < 1))
                {
                    MessageBox.Show("El numero de Item debe estar entre 1 y 5", "Mensaje del Sistema");
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
               
                string valor;
                string ultCosto;
                string exis;
                string medUni;
                valor = objeto.Validar_Existencia(textBox2.Text);
                ultCosto = objeto.Validar_Costo(textBox2.Text);
                exis = objeto.Validar_Diponible(textBox2.Text);
                medUni = objeto.Validar_UnidaPrincipal(textBox2.Text);
                if (rows.Rows.Count > 0)
                {
                    if ((rows.Rows[0][0].ToString() == textBox2.Text))
                        if (valor == "0")
                        {

                            MessageBox.Show("Producto no Disponible ", "Mensaje del Sistema");
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
                            comboBox1.Text = objeto.UnidadMedida[0].ToString();
                            comboBox1.Items.Add(objeto.UnidadMedida[0].ToString());
                            label31.Visible = true;
                            label31.Text = objeto.Descripcion;
                            textBox3.Text = "1";
                            textBox3.ReadOnly = true;
                        }
                }
                else
                {
                    MessageBox.Show("Producto no Existe ", "Mensaje del Sistema");
                    textBox2.Focus();
                }

            }
        }

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            funcion.txtWithoutNumber(this, e);
            funcion.txtWithoutPunctuation_Symbol_Letter(this, e);
            if (e.KeyChar == 13)
            {
                textBox3.Focus();
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            datos.obtenerCodigoCompaniaActual();
            a = Convert.ToInt16(textBox1.Text);
            funcion.txtOnlyDecimal(this, e, textBox3);
            if ((e.KeyChar == 13) && (textBox3.Text.Length > 0 && textBox1.Text.Length > 0 &&
                textBox2.Text.Length > 0 && comboBox1.Text.Length > 0))
            {

                if (textBox1.Text == "1")
                {
                    string iva;
                    iva = objeto.ValidarIva(textBox2.Text);
                    ivas[1] = iva;
                    i = 1;
                    label11.Visible = true;
                    label11.Text = string.Format("{0:000000000}", Convert.ToDouble(textBox2.Text));
                    Registro[i] = label11.Text;
                    ++i;
                    label12.Visible = true;
                    label12.Text = textBox3.Text;
                    Registro[i] = label12.Text;
                    ++i;
                    label13.Visible = true;
                    label13.Text = objeto.UnidadMedida[0].ToString();
                    Registro[i] = label13.Text;
                    ++i;
                    label14.Visible = true;
                    label14.Text = "PVJ:" + objeto.Precio;
                    Etiprecio1 = objeto.Precio;
                    Registro[i] = objeto.Precio.ToString();
                    ++i;
                    Registro[i] = "PVJusto.Bs.";
                    textBox1.Focus();
                    textBox2.Text = "";
                    comboBox1.Text = "";
                    comboBox1.Items.Clear();
                    textBox3.Text = "";
                    label31.Text = "";
                    ++a;
                    textBox1.Text = a.ToString();
                    textBox3.Text = "";
                }

                else if (textBox1.Text == "2")
                {
                    string iva;
                    iva = objeto.ValidarIva(textBox2.Text);
                    ivas[1] = iva;
                    i = 6;
                    label15.Visible = true;
                    label15.Text = string.Format("{0:000000000}", Convert.ToDouble(textBox2.Text));
                    Registro[i] = label15.Text;
                    ++i;
                    label16.Visible = true;
                    label16.Text = textBox3.Text;
                    Registro[i] = label16.Text;
                    ++i;
                    label17.Visible = true;
                    label17.Text = objeto.UnidadMedida[0].ToString();
                    Registro[i] = label17.Text;
                    ++i;
                    label18.Visible = true;
                    label18.Text = "PVJ:" + objeto.Precio;
                    Etiprecio2 = objeto.Precio;
                    Registro[i] = objeto.Precio.ToString();
                    ++i;
                    Registro[i] = "PVJusto.Bs.";
                    textBox1.Focus();
                    textBox2.Text = "";
                    comboBox1.Text = "";
                    comboBox1.Items.Clear();
                    textBox3.Text = "";
                    label31.Text = "";
                    ++a;
                    textBox1.Text = a.ToString();
                    textBox3.Text = "";
                }

                else if (textBox1.Text == "3")
                {
                    string iva;
                    iva = objeto.ValidarIva(textBox2.Text);
                    ivas[1] = iva;
                    i = 11;
                    label19.Visible = true;
                    label19.Text = string.Format("{0:000000000}", Convert.ToDouble(textBox2.Text));
                    Registro[i] = label19.Text;
                    ++i;
                    label20.Visible = true;
                    label20.Text = textBox3.Text;
                    Registro[i] = label20.Text;
                    ++i;
                    label21.Visible = true;
                    label21.Text = objeto.UnidadMedida[0].ToString();
                    Registro[i] = label21.Text;
                    ++i;
                    label22.Visible = true;
                    label22.Text = "PVJ:" + objeto.Precio;
                    Etiprecio3 = objeto.Precio;
                    Registro[i] = objeto.Precio.ToString();
                    ++i;
                    Registro[i] = "PVJusto.Bs.";
                    textBox1.Focus();
                    textBox2.Text = "";
                    comboBox1.Text = "";
                    comboBox1.Items.Clear();
                    textBox3.Text = "";
                    label31.Text = "";
                    ++a;
                    textBox1.Text = a.ToString();
                    textBox3.Text = "";
                }

                else if (textBox1.Text == "4")
                {
                    string iva;
                    iva = objeto.ValidarIva(textBox2.Text);
                    ivas[1] = iva;
                    i = 16;
                    label23.Visible = true;
                    label23.Text = string.Format("{0:000000000}", Convert.ToDouble(textBox2.Text));
                    Registro[i] = label23.Text;
                    ++i;
                    label24.Visible = true;
                    label24.Text = textBox3.Text;
                    Registro[i] = label24.Text;
                    ++i;
                    label25.Visible = true;
                    label25.Text = objeto.UnidadMedida[0].ToString();
                    Registro[i] = label25.Text;
                    ++i;
                    label26.Visible = true;
                    label26.Text = "PVJ:" + objeto.Precio;
                    Etiprecio4 = objeto.Precio;
                    Registro[i] = objeto.Precio.ToString();
                    ++i;
                    Registro[i] = "PVJusto.Bs.";
                    textBox1.Focus();
                    textBox2.Text = "";
                    comboBox1.Text = "";
                    comboBox1.Items.Clear();
                    textBox3.Text = "";
                    label31.Text = "";
                    ++a;
                    textBox1.Text = a.ToString();
                    textBox3.Text = "";
                }

                else if (textBox1.Text == "5")
                {
                    string iva;
                    iva = objeto.ValidarIva(textBox2.Text);
                    ivas[1] = iva;
                    i = 21;
                    label27.Visible = true;
                    label27.Text = string.Format("{0:000000000}", Convert.ToDouble(textBox2.Text));
                    Registro[i] = label27.Text;
                    ++i;
                    label28.Visible = true;
                    label28.Text = textBox3.Text;
                    Registro[i] = label28.Text;
                    ++i;
                    label29.Visible = true;
                    label29.Text = objeto.UnidadMedida[0].ToString();
                    Registro[i] = label29.Text;
                    ++i;
                    label30.Visible = true;
                    label30.Text = "PVJ:" + objeto.Precio;
                    Etiprecio5 = objeto.Precio;
                    Registro[i] = objeto.Precio.ToString();
                    ++i;
                    Registro[i] = "PVJusto.Bs.";
                    textBox1.Focus();
                    textBox2.Text = "";
                    comboBox1.Text = "";
                    comboBox1.Items.Clear();
                    textBox3.Text = "";
                    label31.Text = "";
                    a = 1;
                    textBox1.Text = a.ToString();
                    textBox3.Text = "";

                }
            }

            else
            {
                if (e.KeyChar == 13 && textBox3.Text.Length == 0)
                {
                    MessageBox.Show("Debe completar el contenido del articulo", "Mensaje del Sistema");
                }
                else if (e.KeyChar == 13 && textBox2.Text.Length == 0)
                {
                    MessageBox.Show("Debe ingresar el codigo del articulo", "Mensaje del Sistema");
                }
                else if (e.KeyChar == 13 && textBox1.Text.Length == 0)
                {
                    MessageBox.Show("Debe ingresar el numero de item", "Mensaje del Sistema");
                }
                else if (e.KeyChar == 13 && comboBox1.Text.Length == 0)
                {
                    MessageBox.Show("Debe contener la unidad del articulo", "Mensaje del Sistema");
                }
                if (e.KeyChar == 13 && textBox3.Text.Length > 0 && textBox1.Text.Length > 0 && textBox2.Text.Length > 0)
                {
                    textBox2.Focus();
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            contador = contador + 1;
            SistemaUs = (Principal)this.MdiParent;
            if (label11.Text == "label11")
            {
                for (i = 1; i <= 5; ++i)
                {
                    Registro[i] = string.Format("{0}", Convert.ToString(Registro[i]));
                }
            }
            if (label15.Text == "label15")
            {
                for (i = 6; i <= 10; ++i)
                {
                    Registro[i] = string.Format("{0}", Convert.ToString(Registro[i]));
                }
            }
            if (label19.Text == "label19")
            {
                for (i = 11; i <= 15; ++i)
                {
                    Registro[i] = string.Format("{0}", Convert.ToString(Registro[i]));
                }
            }
            if (label23.Text == "label23")
            {
                for (i = 16; i <= 20; ++i)
                {
                    Registro[i] = string.Format("{0}", Convert.ToString(Registro[i]));
                }
            }
            if (label27.Text == "label27")
            {
                for (i = 21; i <= 25; ++i)
                {
                    Registro[i] = string.Format("{0}", Convert.ToString(Registro[i]));
                }
            }

            string sigla = datos.obtenerSiglasEmpresa();
            string rif = datos.Rif;
            DateTime Hoy = DateTime.Today;
            string fecha_actual = Hoy.ToString("MM/yy");
            if (rif == null)
            {
                MessageBox.Show("Debe de completar al menos un cuadro", "Mensaje del Sistema");
            }
            else
            {
                etiq.ip = ip;
                etiq.id = id;
                if (label13.Text == "")
                {
                    etiq.undmed1 = "";
                    etiq.inv_codigo1 ="";
                    etiq.conten1 = "";
                    etiq.precio1 = "0.00";
                }
                else
                {
                    etiq.undmed1 = this.label13.Text;
                    etiq.inv_codigo1 = this.label11.Text;
                    etiq.conten1 = this.label12.Text;
                    etiq.precio1 = Etiprecio1.ToString().Replace(",", ".");
                }
                if (label17.Text == "")
                {
                    etiq.undmed2 = "";
                    etiq.inv_codigo2 = "";
                    etiq.conten2 = "";
                    etiq.precio2 = "0.00";
                }
                else
                {
                    etiq.undmed2 = label17.Text;
                    etiq.inv_codigo2 = label15.Text;
                    etiq.conten2 = label16.Text;
                    etiq.precio2 = Etiprecio2.ToString().Replace(",", ".");
                }
                if (label21.Text == "")
                {
                    etiq.undmed3 = "";
                    etiq.inv_codigo3 = "";
                    etiq.conten3 = "";
                    etiq.precio3 = "0.00";
                }
                else
                {
                    etiq.undmed3 = label21.Text;
                    etiq.inv_codigo3 = label19.Text;
                    etiq.conten3 = label20.Text;
                    etiq.precio3 = Etiprecio3.ToString().Replace(",", ".");
                }
                if (label25.Text == "")
                {
                    etiq.undmed4 = "";
                    etiq.inv_codigo4 = "";
                    etiq.conten4 = "";
                    etiq.precio4 = "0.00";
                }
                else
                {
                    etiq.undmed4 = label23.Text;
                    etiq.inv_codigo4 = label25.Text;
                    etiq.conten4 = label24.Text;
                    etiq.precio4 = Etiprecio4.ToString().Replace(",", ".");
                }
                if (label29.Text == "")
                {
                    etiq.precio5 = "";
                    etiq.inv_codigo5 = "";
                    etiq.conten5 = "";
                    etiq.precio5 = "0.00";
                }
                else
                {
                    etiq.undmed5 = this.label29.Text;
                    etiq.inv_codigo5 = this.label27.Text;
                    etiq.conten5 = this.label28.Text;
                    etiq.precio5 = Etiprecio5.ToString().Replace(",", ".");
                }
                etiq.insertDatosSistemas();
                imprimir.GenerarArchivo(Registros[1].ToString(), Registros[2].ToString(), Registros[3].ToString(), Registros[4].ToString(), sigla, rif,
                   Registros[5].ToString(), Registros[6].ToString(), Registros[7].ToString(), Registros[8].ToString(), Registros[9].ToString(), Registros[10].ToString(), Registros[11].ToString(), Registros[12].ToString(),
                   Registros[13].ToString(), Registros[14].ToString(), Registros[15].ToString(), Registros[16].ToString(), Registros[17].ToString(), Registros[18].ToString(), Registros[19].ToString(), Registros[20].ToString(),
                   Registros[21].ToString(), Registros[22].ToString(), Registros[23].ToString(), Registros[24].ToString(), Registros[25].ToString(), fecha_actual, ivas[1],contador
                   );

                imprimir.ImprimirArchivo(File.ReadAllText(imprimir.SIni));
                textBox2.Focus();


               // //Query de insert a la base de la table de historia de Etiquetas pequenas 

               // sentenciaSql = "INSERT INTO admhistoetiqpeq(ip_estacion,fechaEjecucion,horaEjecucion,usuario,usuario_sistema,item1,undmed1," +
               // "inv_codigo1,conten1,precio1,item2,undmed2,inv_codigo2,conten2,precio2,item3,undmed3,inv_codigo3,conten3," +
               // "precio3,item4,inv_codigo4,undmed4,conten4,precio4,item5,undmed5,inv_codigo5,conten5,precio5)VALUES('" + SistemaUs.usuarioActual.IpPc + "','" + DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day + "'," +
               // "'" + DateTime.Now.ToString("hh:mm:ss") + "','@usuario','@usuario','1','@undmed1','@inv_codigo1','@conten1','@precio1','2','@undmed2'," +
               // "'@inv_codigo2','@conten2','@precio2','3','@undmed3','@inv_codigo3','@conten3','@precio3','4','@undmed4'," +
               // "'@inv_codigo4','@conten4','@precio4','5','@undmed5','@inv_codigo5','@conten5','@precio5');";

               // // Replazar Por La Informacion DE la Etiquetas

               // /*sentenciaSql = sentenciaSql.Replace("@fechaEjecucion", DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day);
               //  sentenciaSql = sentenciaSql.Replace("@horaEjecucion", DateTime.Now.ToString("hh:mm:ss"));*/
               //  sentenciaSql = sentenciaSql.Replace("@usuario", SistemaUs.usuarioActual.Id);
               //  sentenciaSql = sentenciaSql.Replace("@usuariosistema", SistemaUs.usuarioActual.Id);
               //if(label13.Text=="")  
               // {
               //     sentenciaSql = sentenciaSql.Replace("@undmed1","");
               //     sentenciaSql = sentenciaSql.Replace("@inv_codigo1","");
               //     sentenciaSql = sentenciaSql.Replace("@conten1", "");
               //     sentenciaSql = sentenciaSql.Replace("@precio1","0.00");
               // }
               // else
               // {
               //     sentenciaSql = sentenciaSql.Replace("@undmed1", this.label13.Text);    
               //     sentenciaSql = sentenciaSql.Replace("@inv_codigo1", this.label11.Text);
               //     sentenciaSql = sentenciaSql.Replace("@conten1", this.label12.Text);
               //     sentenciaSql = sentenciaSql.Replace("@precio1", Etiprecio1.ToString().Replace(",", "."));
               // }
               //if (label17.Text == "")
               //{
               //    sentenciaSql = sentenciaSql.Replace("@undmed2", "");
               //    sentenciaSql = sentenciaSql.Replace("@inv_codigo2", "");
               //    sentenciaSql = sentenciaSql.Replace("@conten2", "");
               //    sentenciaSql = sentenciaSql.Replace("@precio2","0.00");
               //}
               //else
               //{
               //    sentenciaSql = sentenciaSql.Replace("@undmed2",label17.Text);
               //    sentenciaSql = sentenciaSql.Replace("@inv_codigo2",label15.Text);
               //    sentenciaSql = sentenciaSql.Replace("@conten2", label16.Text);
               //    sentenciaSql = sentenciaSql.Replace("@precio2", Etiprecio2.ToString().Replace(",", "."));
               //}

               //if (label21.Text == "")
               //{
               //    sentenciaSql = sentenciaSql.Replace("@undmed3", "");
               //    sentenciaSql = sentenciaSql.Replace("@inv_codigo3", "");
               //    sentenciaSql = sentenciaSql.Replace("@conten3", "");
               //    sentenciaSql = sentenciaSql.Replace("@precio3","0.00");
               //}
               //else
               //{
               //    sentenciaSql = sentenciaSql.Replace("@undmed3",label21.Text);
               //    sentenciaSql = sentenciaSql.Replace("@inv_codigo3", label19.Text);
               //    sentenciaSql = sentenciaSql.Replace("@conten3", label20.Text);
               //    sentenciaSql = sentenciaSql.Replace("@precio3", Etiprecio3.ToString().Replace(",", "."));
               //}

               //if (label25.Text == "")
               //{
               //    sentenciaSql = sentenciaSql.Replace("@undmed4", "");
               //    sentenciaSql = sentenciaSql.Replace("@inv_codigo4", "");
               //    sentenciaSql = sentenciaSql.Replace("@conten4", "");
               //    sentenciaSql = sentenciaSql.Replace("@precio4","0.00");
               //}
               //else
               //{
               //    sentenciaSql = sentenciaSql.Replace("@undmed4",label23.Text);
               //    sentenciaSql = sentenciaSql.Replace("@inv_codigo4", label25.Text);
               //    sentenciaSql = sentenciaSql.Replace("@conten4",label24.Text);
               //    sentenciaSql = sentenciaSql.Replace("@precio4", Etiprecio4.ToString().Replace(",", "."));
               //}
               //if (label29.Text == "")
               //{
               //    sentenciaSql = sentenciaSql.Replace("@undmed5", "");
               //    sentenciaSql = sentenciaSql.Replace("@inv_codigo5", "");
               //    sentenciaSql = sentenciaSql.Replace("@conten5", "");
               //    sentenciaSql = sentenciaSql.Replace("@precio5","0.00");
               //}
               //else
               //{
               //    sentenciaSql = sentenciaSql.Replace("@undmed5", this.label29.Text);
               //    sentenciaSql = sentenciaSql.Replace("@inv_codigo5", this.label27.Text);
               //    sentenciaSql = sentenciaSql.Replace("@conten5", this.label28.Text);
               //    sentenciaSql = sentenciaSql.Replace("@precio5", Etiprecio5.ToString().Replace(",", "."));
               //}
               //databaseConection.ejecutarInsert2(sentenciaSql);
               //databaseConection.cerrarConexion();

                //sentenciaSql = null;
                //sentenciaSql = sentenciaSql.Replace("@undmed1", this.label13.Text);
                //sentenciaSql = sentenciaSql.Replace("@inv_codigo1", this.label11.Text);
                //sentenciaSql = sentenciaSql.Replace("@conten1", this.label12.Text);
                //sentenciaSql = sentenciaSql.Replace("@precio1", Etiprecio1.ToString().Replace(",", "."));
                //sentenciaSql = sentenciaSql.Replace("@undmed2", this.label17.Text);
                //sentenciaSql = sentenciaSql.Replace("@inv_codigo2", this.label15.Text);
                //sentenciaSql = sentenciaSql.Replace("@conten2", this.label16.Text);
                //sentenciaSql = sentenciaSql.Replace("@precio2", Etiprecio2.ToString().Replace(",", "."));
                //sentenciaSql = sentenciaSql.Replace("@undmed3", this.label21.Text);
                //sentenciaSql = sentenciaSql.Replace("@inv_codigo3", this.label19.Text);
                //sentenciaSql = sentenciaSql.Replace("@conten3", this.label20.Text);
                //sentenciaSql = sentenciaSql.Replace("@precio3", Etiprecio3.ToString().Replace(",", "."));
                //sentenciaSql = sentenciaSql.Replace("@undmed4", this.label23.Text);
                //sentenciaSql = sentenciaSql.Replace("@inv_codigo4", this.label25.Text);
                //sentenciaSql = sentenciaSql.Replace("@conten4", this.label24.Text);
                //sentenciaSql = sentenciaSql.Replace("@precio4", Etiprecio4.ToString().Replace(",", "."));
                //sentenciaSql = sentenciaSql.Replace("@undmed5", this.label29.Text);
                //sentenciaSql = sentenciaSql.Replace("@inv_codigo5", this.label27.Text);
                //sentenciaSql = sentenciaSql.Replace("@conten5", this.label28.Text);
                //sentenciaSql = sentenciaSql.Replace("@precio5", Etiprecio5.ToString().Replace(",", "."));
  
            }
        }

        private void panel11_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            databaseConection = new ConexionBD();
            SistemaUs = new Principal();
            etiq = new Etiqueta();
            objFrmGrafik = new Form1();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            button4.Visible = false;
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
            for (i = 0; i <= 24; ++i)
            {
                Registro[i] = "";
            }
        }

        private void label31_Click(object sender, EventArgs e)
        {

        }
        //evento de apa
        //private void Form1_Shown(object sender, EventArgs e)
        //{
        //    //consulta verficacion de usuario
        //    SistemaUs = (Principal)this.MdiParent;
        //    string updatesql;
        //    DataTable dt;
        //    DataTable dt1;
        //    DataTable dt2;
        //    string consultasql = "SELECT status,ip_estacion FROM admserietiqpeq WHERE status='1'AND usuario='" + SistemaUs.usuarioActual.Id + "';";
        //    string consultasqlm = "SELECT status FROM admserietiqmed WHERE status='1'AND usuario='" + SistemaUs.usuarioActual.Id + "';";
        //    string consultasqlg = "SELECT status FROM admserietiqgrd WHERE status='1'AND usuario='" + SistemaUs.usuarioActual.Id + "';";
        //    dt = databaseConection.fDataTable(consultasql);
        //    dt1 = databaseConection.fDataTable(consultasqlm);
        //    dt2 = databaseConection.fDataTable(consultasqlg);
        //    if (dt.Rows.Count > 0)
        //    {
        //        if (dt.Rows[0]["status"].ToString().Equals("1"))
        //        {
        //            opcion = "1";
        //            Close();
        //            frmValidacionUserTimer timer = new frmValidacionUserTimer();
        //            timer.Show();
        //        }
        //    }
        //    else if (dt1.Rows.Count > 0)
        //    {
        //        if (dt1.Rows[0]["status"].ToString().Equals("1"))
        //        {
        //            Close();
        //            frmValidacionUserTimer timer = new frmValidacionUserTimer();
        //            timer.Show();
        //        }
        //    }
        //    else if (dt2.Rows.Count > 0)
        //    {
        //        if (dt2.Rows[0]["status"].ToString().Equals("1"))
        //        {
        //            Close();
        //            frmValidacionUserTimer timer = new frmValidacionUserTimer();
        //            timer.Show();
        //        }
        //    }
        //    else
        //    {
        //        updatesql = "UPDATE admserietiqpeq SET usuario='" + SistemaUs.usuarioActual.Id + "',usuario_sistema='" + SistemaUs.usuarioActual.Id + "',status='1' WHERE status='0' AND ip_estacion='" + SistemaUs.usuarioActual.IpPc + "';";
        //        databaseConection.fDataTable(updatesql);        
        //    }
        //}

        

        private void label1_Click(object sender, EventArgs e)
        {

        }
        //evento de cierre


        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            etiq.opcion = opcion;
            etiq.ip = ip;
            etiq.actulizarStadoconecion();
        }

        private void panel11_Paint_1(object sender, PaintEventArgs e)
        {

        }

    }
}