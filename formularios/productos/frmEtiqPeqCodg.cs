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
namespace lebrun.formularios.productos
{
    public partial class frmEtiqPeqCodg : Form
    {
        ConexionBD databaseConection;// llamada a la ventana conexion a la base de datos
        Etiqueta etiq;
        Producto objeto = new Producto();
        FuncionesTexbox funcion = new FuncionesTexbox();
        Etiqueta imprimir = new Etiqueta();
        Compania datos = new Compania();
        string[] Registro = new string[28];
        int i = 0;
        int a;
        public string ip;
        public string id;
        public string opcion;
        int contador;


        public frmEtiqPeqCodg()
        {
            InitializeComponent();
        }
        public string[] Registros
        {
            get { return Registro; }
            set { Registro = value; }
        }


        private void frmEtiqPeqCodg_Load(object sender, EventArgs e)
        {
            databaseConection = new ConexionBD();
            etiq = new Etiqueta();
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

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            datos.obtenerCodigoCompaniaActual();
            a = Convert.ToInt16(textBox1.Text);
            funcion.txtOnlyDecimal(this, e, textBox3);
            if ((e.KeyChar == 13) && (textBox3.Text.Length > 0 && textBox1.Text.Length > 0 &&
                textBox2.Text.Length > 0))
            {
                if (textBox1.Text == "1")
                {
                    i = 0;
                    label11.Visible = true;
                    label11.Text = string.Format("{0:000000000}", Convert.ToDouble(textBox2.Text));
                    Registro[i] = label11.Text;
                    textBox1.Focus();
                    textBox2.Text = "";
                    textBox3.Text = "";
                    label31.Text = "";
                    ++a;
                    textBox1.Text = a.ToString();
                    textBox3.Text = "";
                }
                else if (textBox1.Text == "2")
                {
                    i = 1;
                    label15.Visible = true;
                    label15.Text = string.Format("{0:000000000}", Convert.ToDouble(textBox2.Text));
                    Registro[i] = label15.Text;
                    textBox1.Focus();
                    textBox2.Text = "";
                    textBox3.Text = "";
                    label31.Text = "";
                    ++a;
                    textBox1.Text = a.ToString();
                    textBox3.Text = "";
                }
                else if (textBox1.Text == "3")
                {
                    i = 2;
                    label19.Visible = true;
                    label19.Text = string.Format("{0:000000000}", Convert.ToDouble(textBox2.Text));
                    Registro[i] = label19.Text;
                    textBox1.Focus();
                    textBox2.Text = "";
                    textBox3.Text = "";
                    label31.Text = "";
                    ++a;
                    textBox1.Text = a.ToString();
                    textBox3.Text = "";
                }

                else if (textBox1.Text == "4")
                {
                    i = 3;
                    label23.Visible = true;
                    label23.Text = string.Format("{0:000000000}", Convert.ToDouble(textBox2.Text));
                    Registro[i] = label23.Text;
                    textBox1.Focus();
                    textBox2.Text = "";
                    textBox3.Text = "";
                    label31.Text = "";
                    ++a;
                    textBox1.Text = a.ToString();
                    textBox3.Text = "";
                }
                else if (textBox1.Text == "5")
                {
                    i = 4;
                    label27.Visible = true;
                    label27.Text = string.Format("{0:000000000}", Convert.ToDouble(textBox2.Text));
                    Registro[i] = label27.Text;
                    textBox1.Focus();
                    textBox2.Text = "";
                    textBox3.Text = "";
                    label31.Text = "";
                    a = 1;
                    textBox1.Text = a.ToString();
                    textBox3.Text = "";
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
                    if (e.KeyChar == 13 && textBox3.Text.Length > 0 && textBox1.Text.Length > 0 && textBox2.Text.Length > 0)
                    {
                        textBox2.Focus();
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            contador = contador + 1;
            imprimir.GeneraArchivo1_2(Registro[0], Registro[1], Registro[2], Registro[3], Registro[4],contador);
            imprimir.ImprimirArchivo(File.ReadAllText(imprimir.SIni));
            textBox2.Focus();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmEtiqPeqCodg_FormClosing(object sender, FormClosingEventArgs e)
        {
            etiq.opcion = opcion;
            etiq.ip = ip;
            etiq.actulizarStadoconecion();
        }

        private void label27_Click(object sender, EventArgs e)
        {

        }
    }
}
