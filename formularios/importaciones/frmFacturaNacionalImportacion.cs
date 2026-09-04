using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using lebrun.formularios.complementos;
using lebrun.formularios.contabilidad;
using lebrun.clasesData;
using lebrun.clases.complementos;

namespace lebrun.formularios.importaciones
{


    public partial class frmFacturaNacionalImportacion : Form
    {
        public decimal sinDerechoImportacion;
        private lbxTarifaRetenc tarifaRetencion;
        private lbxAuxiliarContable cuentaContable;
        private DataTable retenciones;
        private DataTable importaciones;
        public DataTable movContab;
        private DataTable gastosImportaciones;
        private TextBox[, ,] array3;
        private Label[] arrayEtiquetas;
        private FuncionesTexbox textBox;
        private lbxProveedores proveedores;
        private proveedor cproveedores;
        private string StrSql;
        private cuentasContables cc;
        public decimal montoD = 0;
        public decimal montoC = 0;

        private lbxPlanCuentas planCuenta;
        private Compania compania;
        private ConexionBD database;
        


        public frmFacturaNacionalImportacion()
        {
            InitializeComponent();
            tarifaRetencion = new lbxTarifaRetenc();
            retenciones = new DataTable();
            movContab = new DataTable();
            gastosImportaciones = new DataTable();
            textBox = new FuncionesTexbox();
            cproveedores = new proveedor();
            database = new ConexionBD();
            compania = new Compania();

        }

        private void button29_Click(object sender, EventArgs e)
        {
            Button a = (Button)sender;
            tarifaRetencion = lbxTarifaRetenc.DefInstance;
            tarifaRetencion.destino(this, this.Name, a.Name);
            tarifaRetencion.ShowDialog();
            //tarifaRetencion.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Button b = (Button)sender;
            //cuentaContable = lbxAuxiliarContable.DefInstance;
            //cuentaContable.soloVista(this,this.Name,b.Name);
            //cuentaContable.ShowDialog();
            //cuentaContable.Show();

            compania.obtenerCodigoCompaniaActual();
            Button b = (Button)sender;
            planCuenta = lbxPlanCuentas.DefInstance;
            planCuenta.soloVista2(this, this.Name, compania.BaseDatosActual, b.Name);//trolleo swicth DB
            planCuenta.Show();

        }

        public decimal calcularSinDerechoImportacion() {
            sinDerechoImportacion = 0;
            sinDerechoImportacion = 
            Convert.ToDecimal(textBox2.Text) + Convert.ToDecimal(textBox3.Text) + 
            Convert.ToDecimal(textBox4.Text) + Convert.ToDecimal(textBox5.Text) +
            Convert.ToDecimal(textBox6.Text) + Convert.ToDecimal(textBox7.Text) +
            Convert.ToDecimal(textBox8.Text) + Convert.ToDecimal(textBox9.Text) +
            Convert.ToDecimal(textBox10.Text) + Convert.ToDecimal(textBox11.Text) +
            Convert.ToDecimal(textBox12.Text) + Convert.ToDecimal(textBox13.Text) +
            Convert.ToDecimal(textBox14.Text) + Convert.ToDecimal(textBox15.Text) +
            Convert.ToDecimal(textBox16.Text) + Convert.ToDecimal(textBox17.Text) +
            Convert.ToDecimal(textBox18.Text) + Convert.ToDecimal(textBox19.Text);

            return sinDerechoImportacion;
        }

        private void TablaMovContab() {

            int items = 1;

            retenciones.Reset();

            retenciones.Columns.Add("item"); //1
            retenciones.Columns.Add("proveedor"); //2
            retenciones.Columns.Add("numeroDoc"); //3
            retenciones.Columns.Add("sufijpDoc"); //4
            retenciones.Columns.Add("tipoDoc"); //5
            retenciones.Columns.Add("comprobante"); //6
            retenciones.Columns.Add("descripcion"); //7
            retenciones.Columns.Add("cuenta"); //8
            retenciones.Columns.Add("tipo"); //9
            retenciones.Columns.Add("tipoLetra"); //10
            retenciones.Columns.Add("fecha"); //11
            retenciones.Columns.Add("hora"); //12
            retenciones.Columns.Add("monto"); //13
            retenciones.Columns.Add("estatus"); //14
            retenciones.Columns.Add("login"); //15
            retenciones.Columns.Add("referencia"); //16
            retenciones.Columns.Add("islr"); //17
            retenciones.Columns.Add("idSistemas"); //18
            retenciones.Columns.Add("auxiliar"); //19
            retenciones.Columns.Add("rif"); //20
            retenciones.Columns.Add("nombre"); //21

        
        }

        private void tablaImportaciones() {

            importaciones.Reset();

            importaciones.Columns.Add("orden");//1
            importaciones.Columns.Add("tipo");//2
            importaciones.Columns.Add("numero");//3
            importaciones.Columns.Add("fecha");//4
            importaciones.Columns.Add("fechaCarga");//5
            importaciones.Columns.Add("fechaInven");//6
            importaciones.Columns.Add("monto");//7
            importaciones.Columns.Add("montoFlete");//8
            importaciones.Columns.Add("montoTransMa");//9
            importaciones.Columns.Add("montoPop");//10
            importaciones.Columns.Add("montoGasBs");//11
            importaciones.Columns.Add("cambio");//12
            importaciones.Columns.Add("cambioLiq");//13
            importaciones.Columns.Add("monImpoAdu");//14
            importaciones.Columns.Add("monTasaAdu");//15
            importaciones.Columns.Add("monSeniatAdu");//16
            importaciones.Columns.Add("montoServAdu");//17
            importaciones.Columns.Add("montOtrosGastAdu");//18
            importaciones.Columns.Add("montCostOperAdu");//19
            importaciones.Columns.Add("montTimbFisAdu");//20
            importaciones.Columns.Add("montManejAdu");//21
            importaciones.Columns.Add("montObrePreAdu");//22
            importaciones.Columns.Add("montObrePrecioAdu");//23
            importaciones.Columns.Add("montCostAlmAdu");//24
            importaciones.Columns.Add("montAyudAdu");//25
            importaciones.Columns.Add("montMultAdu");//26
            importaciones.Columns.Add("montFletAdu");//27
            importaciones.Columns.Add("montInvFinAdu");//28
            importaciones.Columns.Add("montDepGarAdu");//29
            importaciones.Columns.Add("montTHadu");//30
            importaciones.Columns.Add("montSupAdu");//31
            importaciones.Columns.Add("montIDBAdu");//32
            importaciones.Columns.Add("montOtrosAdu");//33
            importaciones.Columns.Add("montTotalGastAdu");//34
            importaciones.Columns.Add("montVerifMerAdu");//35
            importaciones.Columns.Add("montHonorarios");//36
            importaciones.Columns.Add("montGastCAD");//37
            importaciones.Columns.Add("montSeguroMerc");//38
            importaciones.Columns.Add("montOtros");//39
            importaciones.Columns.Add("montoCIF");//40
            importaciones.Columns.Add("montDifArancel");//41
            importaciones.Columns.Add("montFactorCIF");//42
            importaciones.Columns.Add("montFactorGast");//43
            importaciones.Columns.Add("observaciones");//44
            importaciones.Columns.Add("actPrecioLista");//45
            importaciones.Columns.Add("actCIF");//46
            importaciones.Columns.Add("status");//47
            importaciones.Columns.Add("login");//48

        }

        public void armarDataTableMovContab()
        {
            movContab.Reset();
            movContab.Clear();

            movContab.Columns.Add("item");              //0
            movContab.Columns.Add("codprove");          //1
            movContab.Columns.Add("numeroDoc");         //2
            movContab.Columns.Add("sufiDoc");           //3
            movContab.Columns.Add("tipoDoc");           //4
            movContab.Columns.Add("numComprobante");    //5
            movContab.Columns.Add("descripcion");       //6
            movContab.Columns.Add("cuenta");            //7
            movContab.Columns.Add("tipo");              //8
            movContab.Columns.Add("baseTipo");          //9
            movContab.Columns.Add("fecha");             //10
            movContab.Columns.Add("hora");              //11
            movContab.Columns.Add("monto");             //12
            movContab.Columns.Add("status");            //13
            movContab.Columns.Add("login");             //14
            movContab.Columns.Add("idSistemas");        //15
            movContab.Columns.Add("auxiliar");          //16
            movContab.Columns.Add("rif");               //17
            movContab.Columns.Add("nombre");            //18
            movContab.Columns.Add("tipoLetra");         //19
            movContab.Columns.Add("islr");              //20

        }

        public void armarDataTableGastos()
        {
            gastosImportaciones.Reset();
            gastosImportaciones.Clear();

            gastosImportaciones.Columns.Add("documento");
            gastosImportaciones.Columns.Add("sufijo");
            gastosImportaciones.Columns.Add("gasto");
            gastosImportaciones.Columns.Add("monto");
            gastosImportaciones.Columns.Add("cuenta");
            gastosImportaciones.Columns.Add("islr");
            gastosImportaciones.Columns.Add("fecha");

        }


        public void armarArray() {
            array3 = new TextBox[,,]    {{{textBox1,button1txt,button20txt}},
                                        {{textBox2,button2txt,button21txt}},
                                        {{textBox3,button3txt,button22txt}},
                                        {{textBox4,button4txt,button23txt}},
                                        {{textBox5,button5txt,button24txt}},
                                        {{textBox6,button6txt,button25txt}},
                                        {{textBox7,button7txt,button26txt}},
                                        {{textBox8,button8txt,button27txt}},
                                        {{textBox9,button9txt,button28txt}},
                                        {{textBox10,button10txt,button29txt}},
                                        {{textBox11,button11txt,button30txt}},
                                        {{textBox12,button12txt,button31txt}},
                                        {{textBox13,button13txt,button32txt}},
                                        {{textBox14,button14txt,button33txt}},
                                        {{textBox15,button15txt,button34txt}},
                                        {{textBox16,button16txt,button35txt}},
                                        {{textBox17,button17txt,button36txt}},
                                        {{textBox18,button18txt,button37txt}},
                                        {{textBox19,button19txt,button38txt}}};
        
        }

        public void armarArrayEtiquetas() {

            arrayEtiquetas = new Label[]{label1,label2,label3,label4,label5,
                                         label6,label7,label8,label9,label10,
                                         label11,label12,label13,label14,label15,
                                         label16,label17,label18,label19};
            }

        public DataTable contenidoArrayMovContab(string comprobante,string doc, string suf, string proImportacion,string proNacional, string txtIva,string txtTotal, string nombreProveedor, string txtIvaImport,string docNacional, string nomProNacional)
        {
            cc = new cuentasContables();

            if (!cc.consultarTipDocProBool("35", proImportacion))
            {
                cc.consultarTipDocPro("35");
                cc.asignarCuentaContable(nombreProveedor, proImportacion);
            }


            int items = 0;

            armarDataTableMovContab();
            armarArray();  //DEBE LLAMARSE UNA SOLA VEZ, SE DEBE CAMBIAR DE AQUI

            //aqui se debe verificar si hay IVA
            //tambien verificar si hay RETENCION IVA(¿?)
            if (Convert.ToDecimal(txtTotal) != 0)
            {
                cc.consultarTipDocPro2("35", proImportacion);

                items = items + 1;
                DataRow fila = movContab.NewRow();

                fila[0] = items;
                fila[1] = proImportacion;
                fila[2] = doc;
                fila[3] = suf;
                fila[4] = "35";
                fila[5] = comprobante;                  //Buscar en parametros contables(ready)
                fila[6] = nombreProveedor + " " + doc;                  //crear variable
                fila[7] = cc.CuentaProveedor;                         //array3[i, 0, 1].Text;     // cuenta contable (queda igual)

                if (Convert.ToDecimal(txtTotal) > 0)    //aqui se array3[i, 0, 0].Text por el monto txtIva
                {
                    fila[8] = "-1";                     //depende del monto
                    fila[20] = "C";
                    montoC = montoC + Convert.ToDecimal(txtTotal);
                }
                if (Convert.ToDecimal(txtTotal) < 0)
                {
                    fila[8] = "1";                      //depende del monto
                    fila[20] = "D";
                    montoD = montoD + Convert.ToDecimal(txtTotal);
                }
                fila[9] = "";
                fila[10] = "" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString();
                fila[11] = "" + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString();//me da lala esta parte copy and paste                                          //hora
                fila[12] = txtTotal.Replace(",", ".");  //array3[i, 0, 0].Text.Replace(",", "."); //monto
                fila[13] = "0";                         //estatus
                fila[14] = items;                       //login se pasa por parametros
                fila[15] = "IMP";                       //siglas
                fila[16] = "";                          //buscar en admtipdocpro2       'listo falta asignar
                fila[17] = "rif";
                fila[18] = items;                       //nombre
                fila[19] = "";                          //array3[i, 0, 2].Text; //es el auxiliar y va en blanco

                movContab.Rows.Add(fila);

            }

            if (Convert.ToDecimal(txtTotal) != 0)////contraparte total importacion (C)
            {
                //cc.consultarTipDocPro2("35", proNacional);

                cc.consultarTipDocPro("98");

                items = items + 1;
                DataRow fila = movContab.NewRow();

                fila[0] = items;
                fila[1] = proImportacion;
                fila[2] = doc;
                fila[3] = suf;
                fila[4] = "35";
                fila[5] = comprobante;                  //Buscar en parametros contables
                fila[6] = nombreProveedor + " " + doc;                         //crear variable
                fila[7] = cc.CuentaTipDocPro;           //array3[i, 0, 1].Text;     // cuenta contable (queda igual)

                if (Convert.ToDecimal(txtTotal) < 0)    //aqui se array3[i, 0, 0].Text por el monto txtIva
                {
                    fila[8] = "-1";                     //depende del monto
                    fila[20] = "C";
                    montoC = montoC + Convert.ToDecimal(txtTotal);
                }
                if (Convert.ToDecimal(txtTotal) > 0)
                {
                    fila[8] = "1";                      //depende del monto
                    fila[20] = "D";
                    montoD = montoD + Convert.ToDecimal(txtTotal);
                }
                fila[9] = "";
                fila[10] = "" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString();
                fila[11] = "" + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString();//me da lala esta parte copy and paste                                          //hora
                fila[12] = txtTotal.Replace(",", ".");  //array3[i, 0, 0].Text.Replace(",", "."); //monto
                //fila[12] = "0";                         //estatus
                fila[13] = "0";                         //estatus
                fila[14] = items;                       //login se pasa por parametros
                fila[15] = "IMP";                       //siglas
                fila[16] = "";                          //buscar en admtipdocpro2       'listo falta asignar
                fila[17] = "rif";
                fila[18] = items;                       //nombre
                fila[19] = "";                          //array3[i, 0, 2].Text; //es el auxiliar y va en blanco

                movContab.Rows.Add(fila);

            }                 /////////contraparte total importacion (C)


            if (Convert.ToDecimal(txtTotalNacional.Text) != 0)
            {
                //cc.consultarTipDocPro2("35", proNacional);

                cc.consultarTipDocPro2("35", proNacional);
                items = items + 1;
                DataRow fila = movContab.NewRow();

                fila[0] = items;
                fila[1] = proImportacion;
                fila[2] = doc;
                fila[3] = suf;
                fila[4] = "35";
                fila[5] = comprobante;                  //Buscar en parametros contables
                fila[6] = nomProNacional + " FACT. " + docNacional + "(" + nombreProveedor + ")";                     //crear variable
                fila[7] = cc.CuentaProveedor;           //array3[i, 0, 1].Text;     // cuenta contable (queda igual)

                if (Convert.ToDecimal(txtTotalNacional.Text) > 0)    //aqui se array3[i, 0, 0].Text por el monto txtIva
                {
                    fila[8] = "-1";                     //depende del monto
                    fila[20] = "C";
                    montoC = montoC + Convert.ToDecimal(txtTotalNacional.Text);
                }
                if (Convert.ToDecimal(txtTotalNacional.Text) < 0)
                {
                    fila[8] = "1";                      //depende del monto
                    fila[20] = "D";
                    montoD = montoD + Convert.ToDecimal(txtTotalNacional);
                }
                fila[9] = "";
                fila[10] = "" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString();
                fila[11] = "" + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString();//me da lala esta parte copy and paste                                          //hora
                fila[12] = txtTotalNacional.Text.Replace(",", ".");  //array3[i, 0, 0].Text.Replace(",", "."); //monto
                fila[13] = "0";                         //estatus
                fila[14] = items;                       //login se pasa por parametros
                fila[15] = "IMP";                       //siglas
                fila[16] = "";                          //buscar en admtipdocpro2       'listo falta asignar
                fila[17] = "rif";
                fila[18] = items;                       //nombre
                fila[19] = "";                          //array3[i, 0, 2].Text; //es el auxiliar y va en blanco

                movContab.Rows.Add(fila);

            }   

            if (Convert.ToDecimal(txtIva) != 0)
            {
                //cc.consultarTipDocPro("96");
                cc.consultarTipDocPro("42");

                items = items + 1;
                DataRow fila = movContab.NewRow();

                fila[0] = items;
                fila[1] = proImportacion;
                fila[2] = doc;
                fila[3] = suf;
                fila[4] = "35";
                fila[5] = comprobante;                  //Buscar en parametros contables
                fila[6] = nomProNacional + " FACT. " + docNacional + "(" + nombreProveedor + ")";                     //crear variable                        //crear variable
                fila[7] = cc.CuentaTipDocPro_2;                //array3[i, 0, 1].Text;     // cuenta contable (queda igual)

                if (Convert.ToDecimal(txtIva) < 0)      //aqui se array3[i, 0, 0].Text por el monto txtIva
                {
                    fila[8] = "-1";                     //depende del monto
                    fila[20] = "C";
                    montoC = montoC + Convert.ToDecimal(txtIva);
                }
                if (Convert.ToDecimal(txtIva) > 0)
                {
                    fila[8] = "1";                      //depende del monto
                    fila[20] = "D";
                    montoD = montoD + Convert.ToDecimal(txtIva);
                }
                fila[9] = "";
                fila[10] = "" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString();
                fila[11] = "" + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString();//me da lala esta parte copy and paste                                          //hora
                fila[12] = txtIva.Replace(",", ".");    //array3[i, 0, 0].Text.Replace(",", "."); //monto
                fila[13] = "0";                         //estatus
                fila[14] = items;                       //login se pasa por parametros
                fila[15] = "IMP";                       //siglas
                fila[16] = "";                          //buscar en admtipdocpro2       'listo falta asignar
                fila[17] = "rif";
                fila[18] = items;                       //nombre
                fila[19] = "";                          //array3[i, 0, 2].Text; //es el auxiliar y va en blanco

                movContab.Rows.Add(fila);

            }


            if (Convert.ToDecimal(txtIvaImport) != 0)
            {
                //cc.consultarTipDocPro("96");
                cc.consultarTipDocPro("42");

                items = items + 1;
                DataRow fila = movContab.NewRow();

                fila[0] = items;
                fila[1] = proImportacion;
                fila[2] = doc;
                fila[3] = suf;
                fila[4] = "35";
                fila[5] = comprobante;                  //Buscar en parametros contables
                fila[6] = nomProNacional + " FACT. " + docNacional + "(" + nombreProveedor + ")";                          //crear variable
                fila[7] = cc.CuentaTipDocPro;                //array3[i, 0, 1].Text;     // cuenta contable (queda igual)

                if (Convert.ToDecimal(txtIvaImport) < 0)      //aqui se array3[i, 0, 0].Text por el monto txtIva
                {
                    fila[8] = "-1";                     //depende del monto
                    fila[20] = "C";
                    montoC = montoC + Convert.ToDecimal(txtIvaImport);
                }
                if (Convert.ToDecimal(txtIvaImport) > 0)
                {
                    fila[8] = "1";                      //depende del monto
                    fila[20] = "D";
                    montoD = montoD + Convert.ToDecimal(txtIvaImport);
                }
                fila[9] = "";
                fila[10] = "" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString();
                fila[11] = "" + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString();//me da lala esta parte copy and paste                                          //hora
                fila[12] = txtIvaImport.Replace(",", ".");    //array3[i, 0, 0].Text.Replace(",", "."); //monto
                fila[13] = "0";                         //estatus
                fila[14] = items;                       //login se pasa por parametros
                fila[15] = "IMP";                       //siglas
                fila[16] = "";                          //buscar en admtipdocpro2       'listo falta asignar
                fila[17] = "rif";
                fila[18] = items;                       //nombre
                fila[19] = "";                          //array3[i, 0, 2].Text; //es el auxiliar y va en blanco

                movContab.Rows.Add(fila);

            }


            for (int i = 0; i < 19; i++){

                if (Convert.ToDecimal(array3[i, 0, 0].Text) != 0)
                {

                    items = items + 1;
                    DataRow fila = movContab.NewRow();

                    fila[0] = items;
                    fila[1] = proImportacion;
                    fila[2] = doc;
                    fila[3] = suf;
                    fila[4] = "35";
                    fila[5] = comprobante;              //Buscar en parametros contables
                    fila[6] = nomProNacional + " FACT. " + docNacional + "(" + nombreProveedor + ")";                     //crear variable
                    fila[7] = array3[i, 0, 1].Text;     //cuenta contable       

                    if (Convert.ToDecimal(array3[i, 0, 0].Text) < 0)  //validar por el monto
                    {
                        fila[8] = "-1";                 //depende del monto
                        fila[20] = "C";
                        montoC = montoC + Convert.ToDecimal(array3[i, 0, 0].Text);
                    }
                    if (Convert.ToDecimal(array3[i, 0, 0].Text) > 0)
                    {
                        fila[8] = "1";                  //depende del monto
                        fila[20] = "D";
                        montoD = montoD + Convert.ToDecimal(array3[i, 0, 0].Text);
                    }
                    fila[9] = "";
                    fila[10] = "" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString();
                    fila[11] = "" + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString();//me da lala esta parte copy and paste                                          //hora
                    fila[12] = array3[i, 0, 0].Text.Replace(",",".");//monto
                    fila[13] = "0";
                    fila[14] = items;                   //se pasa por parametros
                    fila[15] = "IMP";
                    fila[16] = "";//     //"";     //buscar en admtipdocpro2 'listo falta asignar
                    fila[17] = "rif";
                    fila[18] = items;
                    fila[19] = "";// array3[i, 0, 2].Text;                  //auxiliar contable(va en blanco)

                    movContab.Rows.Add(fila);

                    }

            }
          
            return movContab;

         }

        public DataTable contenidoArrayGastos(string doc, string suf) {
            armarDataTableGastos();
            armarArray();
            armarArrayEtiquetas();

            for (int i = 0; i < 19;i++ )
            {
                DataRow fila = gastosImportaciones.NewRow();

                //necesito un arreglo de etiquetas

                fila[0] = doc; 
                fila[1] = suf;     
                fila[2] = arrayEtiquetas[i].Text;
                fila[3] = array3[i, 0, 0].Text.Replace(",",".");
                fila[4] = array3[i, 0, 1].Text;
                fila[5] = array3[i, 0, 2].Text;
                fila[6] = ""+DateTime.Now.Year.ToString()+"-"+DateTime.Now.Month.ToString()+"-"+DateTime.Now.Day.ToString();

                gastosImportaciones.Rows.Add(fila);

            } return gastosImportaciones;
        
        }
        
        private void button20txt_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Clicks == 1) {
                
                TextBox a = (TextBox)sender;
                a.Clear();
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            TextBox a = (TextBox)sender;
            if (a.Text == "") {
                a.Text = "0,00000000"; 
            }
            a.Text = string.Format("{0:0.00000000}", Convert.ToDecimal(a.Text));
        }

        private void textBox1_MouseUp(object sender, MouseEventArgs e)
        {
            TextBox a = (TextBox)sender;
            a.SelectAll();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox a = (TextBox)sender;

            textBox.conDecimalesDefinidos(sender, e, a, 10);
            
        }

        private void frmFacturaNacionalImportacion_Load(object sender, EventArgs e)
        {
            datosProveedorNacional();
        }

        private void button39_MouseEnter(object sender, EventArgs e)
        {
            //this.button39.FlatAppearance.BorderColor = Color.Red;
            //this.button39.BackColor = Color.Red;
        }

        //private void textBox20_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    textBox.onlyNumberWithTab(sender, e);
        //}

        private void textBox21_KeyPress(object sender, KeyPressEventArgs e)
        {
            textBox.tab(sender, e);
        }

        private void textBox22_KeyPress(object sender, KeyPressEventArgs e)
        {
            textBox.tab(sender, e);
        }

        private void dateTimePicker1_KeyPress(object sender, KeyPressEventArgs e)
        {
           if (e.KeyChar ==13) { textBox.tab(sender, e); }
        }

        private void dateTimePicker2_KeyPress(object sender, KeyPressEventArgs e)
        {
           if (e.KeyChar == 13) { textBox.tab(sender, e); }
        }

        private void textBox23_KeyPress(object sender, KeyPressEventArgs e)
        {
            //TextBox a = (TextBox)sender;

            //textBox.conDecimalesDefinidos(sender, e, a, 3);

            //TextBox a = (TextBox)sender;

            //textBox.conDecimalesDefinidos(sender, e, a, 10);

        }

        private void textBox23_Leave(object sender, EventArgs e)
        {
            TextBox a = (TextBox)sender;
            if (a.Text == "")
            {
                a.Text = "0,00";
            }
            a.Text = string.Format("{0:0.00}", Convert.ToDecimal(a.Text));
        }

        private void txtMontoBase_MouseUp(object sender, MouseEventArgs e)
        {
            TextBox a = (TextBox)sender;
            a.SelectAll();
        }

        private void txtMontoBase_Leave(object sender, EventArgs e)
        {
            TextBox a = (TextBox)sender;
            if (a.Text == "")
            {
                a.Text = "0,00";
            }
            a.Text = string.Format("{0:0.00}", Convert.ToDecimal(a.Text));
        }

        private void button39_Click(object sender, EventArgs e)
        {
            proveedores = lbxProveedores.DefInstance;
            proveedores.destino(this, this.Name);
            proveedores.ShowDialog();
            //proveedores.Show();
        }

        /* Para calcular el ISLR
         * 
         * Guardar porcentaje de la base
         * (baseReten-porceReten/100)-sustraendo
         
         */
        private bool consultaRapidaProveedor() //PROVEEDOR
        {
            bool val2 = false;

            DataTable dproveedor = new DataTable();
            dproveedor = cproveedores.consultarProveedorUnico(txtCodigoProveedor.Text);

            if (dproveedor.Rows.Count != 0)
            {

                lblNomProveNac.Text = dproveedor.Rows[0][1].ToString();
                lblRifProvNac.Text = dproveedor.Rows[0][2].ToString();
               
                
                val2 = true;
            }
            else
            {
                MessageBox.Show("No existe proveedor con este codigo", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                val2 = false;
                txtCodigoProveedor.Text = "";
                lblNomProveNac.Text = "...";
                lblRifProvNac.Text = "...";

            }
            return val2;

        }

        private void txtCodigoProveedor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (e.KeyChar == '\b')
            {
                e.Handled = false;
            }
            else if (e.KeyChar == '\r')
            {

                if (txtCodigoProveedor.Text != "")
                {
                    consultaRapidaProveedor();
                    txtNumeroFactura.Focus();
                }
                else
                {
                    lblNomProveNac.Text = "...";
                    lblRifProvNac.Text = "...";
                    SendKeys.Send("{TAB}");
                    e.Handled = false;
                }
            }
            else if (Char.IsSeparator(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtCodigoProveedor_Leave(object sender, EventArgs e)
        {
                  
            if (txtCodigoProveedor.Text != "")
            {
                if (!consultaRapidaProveedor()) { txtCodigoProveedor.Focus(); }
            }
            else 
            { 
                lblNomProveNac.Text = "...";
                lblRifProvNac.Text = "...";
            }
        
        }

        private void datosProveedorNacional() {

            if (txtCodigoProveedor.Text != "") {
                DataTable tabla = new DataTable();

                StrSql = "SELECT pro_nombre, pro_rif FROM admproveedor WHERE pro_codigo='$1'";
                StrSql= StrSql.Replace("$1",txtCodigoProveedor.Text);
                tabla = database.fDataTable(StrSql);
                lblNomProveNac.Text = tabla.Rows[0][0].ToString();
                lblRifProvNac.Text = tabla.Rows[0][1].ToString();
                
            }
        
        }

        private void txtSufijoNacional_KeyPress(object sender, KeyPressEventArgs e)
        {
            textBox.onlyNumberWithTab(sender, e);
        }

        private void txtIvaNacional_KeyPress(object sender, KeyPressEventArgs e)
        {
            textBox.conDecimalesDefinidos(sender, e, txtIvaNacional, 10);

            if (e.KeyChar == 13) {

                textBox1.Focus();
            }
        }

        private void txtSufijoNacional_Leave(object sender, EventArgs e)
        {
            if (txtSufijoNacional.Text == "")
            {
                txtSufijoNacional.Text = "0";
            }
        }



        //public void asignarCuentaContable(string nombreProveedor) {


        //    if (!cc.consultarTipDocProBool("35", nombreProveedor))
        //    {
        //        cc.consultarTipDocPro("35");
        //        cc.asignarCuentaContable(nombreProveedor);
        //    }
        //}


    }
}
