using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using lebrun.clases.compras;
using lebrun.clases.contabilidad;
using lebrun.formularios.contabilidad;
using lebrun.clasesData;
using lebrun.clases.proveedores;
using lebrun.Reportes.MovContab;
using lebrun.formularios.complementos;

namespace lebrun.formularios.compras
{
    public partial class frmCruceDeSaldo : Form
    {
        private frmConteMovContab frmMovContab;
        private int centinelaFila = -1;
        private Pagos pago;
        private DataTable tabla;
        private DataTable tablaB;
        private AuxiliarContable au;
        private Compania compania;
        private lbxAuxiliarContable planCuenta;
        private lbxAuxiliaresOnly auxiliar;
        private FuncionesTexbox ftextbox;
        private DataTable tablaPago;
        private Principal Principal;
        private DataTable tablaMovcontab;
        private movimientoContable movContab;
        private Proveedores clasePro;
        private decimal montoDC;
        private decimal montoD;
        private decimal montoC;
        private decimal montoC2 = 0;
        private decimal montoD2 = 0;
        private string comprobante;
        private string mayorProveedor;
        private string auxiliarProveedor;
        private DataTable tablaCabacera;
        private bool guard2 = false;
        private lbxProveedores proveedores;
        private DataTable tabla2;
        private string compañiaActual;
 
        

        public frmCruceDeSaldo()
        {
            InitializeComponent();
            pago = new Pagos();
            tabla = new DataTable();
            au = new AuxiliarContable();
            compania = new Compania();
            ftextbox = new FuncionesTexbox();
            tablaPago = new DataTable();
            tablaMovcontab = new DataTable();
            movContab = new movimientoContable();
            tablaB = new DataTable();
            tablaCabacera = new DataTable();
            clasePro = new Proveedores();
            tabla2 = new DataTable();
        }

        private void bntCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            dataGridView2.Rows.Clear();

            tabla = pago.cargarDocCruce(txtCodigoProveedor.Text);

            if (tabla.Rows.Count > 0)
            {
                lblNombrePro.Text = tabla.Rows[0][0].ToString();

                for (int i = 0; i < tabla.Rows.Count; i++)
                {

                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[i].Cells[0].Value = i + 1;
                    dataGridView1.Rows[i].Cells[1].Value = tabla.Rows[i][2].ToString();
                    dataGridView1.Rows[i].Cells[2].Value = tabla.Rows[i][3].ToString();
                    dataGridView1.Rows[i].Cells[3].Value = tabla.Rows[i][4].ToString();
                    dataGridView1.Rows[i].Cells[4].Value = tabla.Rows[i][5].ToString();
                    dataGridView1.Rows[i].Cells[5].Value = Convert.ToDateTime(tabla.Rows[i][6].ToString()).ToString("dd-MM-yyyy"); ;
                    dataGridView1.Rows[i].Cells[6].Value = tabla.Rows[i][7].ToString();
                    dataGridView1.Rows[i].Cells[7].Value = Convert.ToDateTime(tabla.Rows[i][8].ToString()).ToString("dd-MM-yyyy"); ;

                    dataGridView1.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dataGridView1.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dataGridView1.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dataGridView1.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dataGridView1.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                    dataGridView1.AllowUserToResizeColumns = false;
                    dataGridView1.AllowUserToResizeRows = false;
                }
            }
            else
            {
                lblNombrePro.Text = "...";
            }

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentRow.DefaultCellStyle.ForeColor != Color.Red)
            {
                panel1.Visible = true;
                txtMontoDocP.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                txtMontoCanDocP.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                txtMontoCanDocP.Focus();
                dataGridView1.Enabled = false;
            }
            else
            {
                MessageBox.Show("El documento seleccionado ya ha sido agregado", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnCancMonto_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            dataGridView1.Enabled = true;
            txtMontoCanDocP.Text = "0";
        }

        private void btnAceptarMonto_Click(object sender, EventArgs e)
        {
            if (centinelaFila >= 0)
            {
                agregarFila(centinelaFila, dataGridView2);
                centinelaFila = -1;
            }
            else
            {
                agregarFila(-1, dataGridView2);
            }

            panel1.Visible = false;
            dataGridView1.CurrentRow.DefaultCellStyle.ForeColor = Color.Red;
            dataGridView1.Enabled = true;

            txtMontoTotal.Text = "" + Convert.ToString(dataGridView2.Rows.Cast<DataGridViewRow>().Sum(x => Convert.ToDouble(x.Cells[5].Value)));

        }

        public void agregarFila(int fila, DataGridView grid)
        {

            if (grid.Name.Equals("dataGridView2"))
            {
                if (fila < 0)
                {
                    grid.Rows.Add();
                    fila = grid.Rows.Count - 1;
                }

                grid.Rows[fila].Cells[0].Value = fila + 1;
                grid.Rows[fila].Cells[1].Value = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                grid.Rows[fila].Cells[2].Value = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                grid.Rows[fila].Cells[3].Value = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                grid.Rows[fila].Cells[4].Value = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                grid.Rows[fila].Cells[5].Value = txtMontoCanDocP.Text;
                grid.Rows[fila].Cells[6].Value = "" + Convert.ToDateTime(dataGridView1.CurrentRow.Cells[5].Value.ToString()).Day + "-" + Convert.ToDateTime(dataGridView1.CurrentRow.Cells[5].Value.ToString()).Month.ToString().PadLeft(2, '0') + "-" + Convert.ToDateTime(dataGridView1.CurrentRow.Cells[5].Value.ToString()).Year;
                grid.Rows[fila].Cells[7].Value = dataGridView1.CurrentRow.Cells[6].Value.ToString();
                grid.Rows[fila].Cells[8].Value = "" + Convert.ToDateTime(dataGridView1.CurrentRow.Cells[7].Value.ToString()).Day + "-" + Convert.ToDateTime(dataGridView1.CurrentRow.Cells[7].Value.ToString()).Month.ToString().PadLeft(2, '0') + "-" + Convert.ToDateTime(dataGridView1.CurrentRow.Cells[7].Value.ToString()).Year;
                grid.Rows[fila].Cells[9].Value = "" + Convert.ToDateTime(dataGridView1.CurrentRow.Cells[5].Value.ToString()).Year + "-" + Convert.ToDateTime(dataGridView1.CurrentRow.Cells[5].Value.ToString()).Month.ToString().PadLeft(2, '0') + "-" + Convert.ToDateTime(dataGridView1.CurrentRow.Cells[5].Value.ToString()).Day;
                grid.Rows[fila].Cells[10].Value = "" + Convert.ToDateTime(dataGridView1.CurrentRow.Cells[7].Value.ToString()).Year + "-" + Convert.ToDateTime(dataGridView1.CurrentRow.Cells[7].Value.ToString()).Month.ToString().PadLeft(2, '0') + "-" + Convert.ToDateTime(dataGridView1.CurrentRow.Cells[7].Value.ToString()).Day;
                grid.Rows[fila].Cells[11].Value = "" + dataGridView1.CurrentRow.Index;
                grid.Rows[fila].Cells[12].Value = "" + (Convert.ToDecimal(txtMontoDocP.Text) - Convert.ToDecimal(txtMontoCanDocP.Text));

                if (Convert.ToDecimal(dataGridView1.CurrentRow.Cells[4].Value.ToString()) - Convert.ToDecimal(txtMontoCanDocP.Text) == 0)
                {
                    grid.Rows[fila].Cells[13].Value = "1";
                }
                else
                {
                    grid.Rows[fila].Cells[13].Value = "0";
                }


                grid.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                grid.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                grid.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                grid.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                grid.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                grid.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                
            }

            dataGridView2.AllowUserToResizeColumns = false;
            dataGridView2.AllowUserToResizeRows = false;

            if (grid.Name.Equals("gridMovContab"))
            {
                if (fila < 0)
                {
                    grid.Rows.Add();
                    fila = grid.Rows.Count - 1;
                }

                grid.Rows[fila].Cells[0].Value = fila + 1;
                grid.Rows[fila].Cells[1].Value = txtDescripcion.Text;
                grid.Rows[fila].Cells[2].Value = txtCuenta.Text;

                if (rbDebito.Checked)
                {
                    grid.Rows[fila].Cells[3].Value = "D";
                    grid.Rows[fila].Cells[6].Value = "1";
                    grid.Rows[fila].Cells[4].Value = txtMontoMC.Text;
                }

                if (rbCredito.Checked)
                {
                    grid.Rows[fila].Cells[3].Value = "C";
                    grid.Rows[fila].Cells[6].Value = "-1";
                    grid.Rows[fila].Cells[5].Value = txtMontoMC.Text;
                }

                grid.Rows[fila].Cells[7].Value = txtAuxiliar.Text;
                grid.Rows[fila].Cells[8].Value = txtMontoMC.Text;

                limpiarMovcontab();
            }
        }

        private void btnEliminarDoc_Click(object sender, EventArgs e)
        {
            if (dataGridView2.Rows.Count > 0)
            {
                dataGridView1.Rows[Convert.ToInt16(dataGridView2.CurrentRow.Cells[11].Value)].DefaultCellStyle.ForeColor = Color.Black;
                dataGridView2.Rows.RemoveAt(dataGridView2.CurrentRow.Index);
                reorganizarItems();
                txtMontoTotal.Text = "" + Convert.ToString(dataGridView2.Rows.Cast<DataGridViewRow>().Sum(x => Convert.ToDouble(x.Cells[5].Value)));
            }
        }

        private void btnAddMovContab_Click(object sender, EventArgs e)
        {
            if (validacionMovContab())
            {
                if (centinelaFila >= 0)
                {
                    agregarFila(centinelaFila, gridMovContab);
                    centinelaFila = -1;
                }
                else
                {
                    agregarFila(-1, gridMovContab);
                }
                sumaMovimientoContable();
            }
        }

        public void sumaMovimientoContable()
        {
            montoC = 0;
            montoD = 0;
            for (int i = 0; i < gridMovContab.Rows.Count; i++)
            {

                if (gridMovContab.Rows[i].Cells[3].Value.ToString() == "D")
                {
                    montoD = (montoD + Convert.ToDecimal(gridMovContab.Rows[i].Cells[4].Value));
                }
                if (gridMovContab.Rows[i].Cells[3].Value.ToString() == "C")
                {
                    montoC = (montoC + Convert.ToDecimal(gridMovContab.Rows[i].Cells[5].Value));
                }

            }
            txtDebito.Text = "" + montoD;
            txtCredito.Text = "" + montoC;
            montoDC = montoD - montoC;
        }

        private void limpiarMovcontab()
        {

            txtCuenta.Text = "";
            txtAuxiliar.Text = "";
            txtMontoMC.Text = "";
            btnAuxiliar.Enabled = false;

        }

        private bool validacionMovContab()
        {

            if (txtCuenta.Text == "")
            {
                MessageBox.Show("Debe seleccionar una cuenta contable", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCuenta.Focus();
                return false;

            }
            else if (btnAuxiliar.Enabled == true && txtAuxiliar.Text == "")
            {
                MessageBox.Show("Este cuenta contable maneja auxiliar, seleccione uno", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtAuxiliar.Focus();
                return false;
            }
            else if (txtDescripcion.Text == "")
            {
                MessageBox.Show("Agregue una descripción a este movimiento", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDescripcion.Focus();
                return false;
            }
            else if (txtMontoMC.Text == "")
            {
                MessageBox.Show("Debe ingresar un monto para este movimiento", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMontoMC.Focus();
                return false;
            }
            else
            {
                return true;
            }

        }

        private void buscarVerificarPlanC()
        {
            if (txtCuenta.Text != "")
            {
                au.CodigoPlanCuentas = txtCuenta.Text;
                
                //compania.BaseDatosActual
                //if (au.validarPlanCuenta(10, "01"))

                if (au.validarPlanCuenta(10, compania.BaseDatosActual))
                {
                    //txtNombreCuenta.Text = au.getDescripcionPlanC(au.CodigoPlanCuentas, "01");
                    
                    //if (au.poseeAuxilar("01", au.CodigoPlanCuentas))
                    if (au.poseeAuxilar(compania.BaseDatosActual, au.CodigoPlanCuentas))
                    {
                        btnAuxiliar.Enabled = true;
                        txtAuxiliar.Enabled = true;

                        txtAuxiliar.Focus();

                    }
                    else
                    {
                        btnAuxiliar.Enabled = false;

                        txtAuxiliar.Enabled = false;
                        txtDescripcion.Focus();
                    }
                }
                else
                {

                    MessageBox.Show("El numero de cuenta ingresado no existe", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCuenta.Text = "";
                    txtAuxiliar.Text = "";

                    guard2 = false;
                    txtCuenta.Focus();
                }
            }
        }

        private void txtCuenta_Leave(object sender, EventArgs e)
        {
            buscarVerificarPlanC();
        }

        private void txtAuxiliar_Leave(object sender, EventArgs e)
        {
            if (txtAuxiliar.Text != "")
            {
                if (!au.permisoAuxiliar(txtCuenta.Text, txtAuxiliar.Text, compania.BaseDatosActual))
                {
                    MessageBox.Show("El auxiliar seleccionado no existe", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtAuxiliar.Focus();
                    txtAuxiliar.Text = "";
                }
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            compañiaActual = null;
            auxiliar.CompañiaActual = null;
            compania.obtenerCodigoCompaniaActual();
            compañiaActual = compania.BaseDatosActual;
            planCuenta = lbxAuxiliarContable.DefInstance;
            planCuenta.soloVista(this, this.Name, compañiaActual);
            planCuenta.Show();
            planCuenta.lbxComprobante1(false, compañiaActual);
            planCuenta.cmdAgregar.Visible = false;
            planCuenta.cmdBotonModificar.Visible = false;
        }

        private void btnAuxiliar_Click(object sender, EventArgs e)
        {
            if (txtCuenta.Text != "")
            {
                compania.obtenerCodigoCompaniaActual();
                auxiliar = lbxAuxiliaresOnly.DefInstance;
                auxiliar.guardarCompania(compania.BaseDatosActual);
                auxiliar.destino(this, this.Name);
                auxiliar.codCuenta(txtCuenta.Text);
                //auxiliar.llenarAuxiliares();
                auxiliar.Show();
                auxiliar.llenarAuxiliares();
            }
            else
            {
                MessageBox.Show("Debe seleccionar una cuenta antes de elegir un auxiliar", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void txtCuenta_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)(Keys.Enter)) && txtCuenta.Text != "")
            {
                buscarVerificarPlanC();
            }
            else
            {
                if (e.KeyChar == (char)(Keys.Enter))
                {
                    e.Handled = true; SendKeys.Send("{TAB}");

                }
            }
        }

        private void txtAuxiliar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                ftextbox.tab(sender, e);
            }
        }

        private void txtDescripcion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtMontoMC.Focus();
            }
        }

        private void txtMontoMC_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox a = (TextBox)sender;
            ftextbox.conDecimalesDefinidosNegativo(sender, e, a, 2);
        }

        private void txtMontoCanDocP_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox a = (TextBox)sender;
            ftextbox.conDecimalesDefinidosNegativo(sender, e, a, 2);
        }

        private void armarTablePago()
        {

            tablaPago.Reset();
            tablaPago.Clear();

            tablaPago.Columns.Add("codigoProveedor");               //0
            tablaPago.Columns.Add("numeroDocum");                   //1
            tablaPago.Columns.Add("sufijoDoc");                     //2
            tablaPago.Columns.Add("tipoDoc");                       //3
            tablaPago.Columns.Add("NombreProveedor");               //4
            tablaPago.Columns.Add("fechaCarga");                    //5
            tablaPago.Columns.Add("MontoDocum");                    //6
            tablaPago.Columns.Add("MontoCheque");                   //7
            tablaPago.Columns.Add("idBanco");                       //8
            tablaPago.Columns.Add("Login");                         //9
            tablaPago.Columns.Add("numeroCheque");                  //10
            tablaPago.Columns.Add("status");                        //11
            tablaPago.Columns.Add("fechaPago");                     //12
            tablaPago.Columns.Add("numeroComprobante");             //13
            tablaPago.Columns.Add("cuentaBancaria");                //14
            tablaPago.Columns.Add("descripcion");                   //15
            tablaPago.Columns.Add("vencimiento");                   //16
            tablaPago.Columns.Add("correlativo");                   //17
            tablaPago.Columns.Add("restante");                      //18

        }

        private DataTable contenidoTablaPago()
        {
            armarTablePago();

            for (int i = 0; i < dataGridView2.Rows.Count; i++)
            {
                DataRow fila = tablaPago.NewRow();

                fila[0] = txtCodigoProveedor.Text;
                fila[1] = dataGridView2.Rows[i].Cells[1].Value.ToString();
                fila[2] = dataGridView2.Rows[i].Cells[2].Value.ToString();
                fila[3] = dataGridView2.Rows[i].Cells[3].Value.ToString();
                fila[4] = lblNombrePro.Text;
                fila[5] = "" + DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day;
                fila[6] = dataGridView2.Rows[i].Cells[4].Value.ToString().Replace(",", ".");
                fila[7] = "" + Convert.ToString(dataGridView2.Rows.Cast<DataGridViewRow>().Sum(x => Convert.ToDouble(x.Cells[5].Value))).Replace(",", ".");
                fila[8] = "";
                fila[9] = Principal.usuarioActual.Id;
                fila[10] = "";
                fila[11] = dataGridView2.Rows[i].Cells[13].Value.ToString();//status salpro
                fila[12] = "" + DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day;
                fila[13] = comprobante;
                fila[14] = "";
                fila[15] = lblNombrePro.Text;
                fila[16] = "" + DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day;
                fila[17] = "";
                fila[18] = dataGridView2.Rows[i].Cells[12].Value.ToString();

                tablaPago.Rows.Add(fila);
            }

            return tablaPago;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (validarCamposProceso())
            {
                obtenerComprobanteContable();
                compania.obtenerCodigoCompaniaActual();

                contenidoTablaPago();
                pago.insertarProcesoPago(tablaPago);
                pago.actualizarSaldo(tablaPago);

                contenidoMovContab();
                movContab.ingresarMovContab(tablaMovcontab);
                movContab.detallesSyscontab(tablaMovcontab, compania.BaseDatosActual);

                contenidoCabecera();
                movContab.cabeceraSyscontab(tablaCabacera, compania.BaseDatosActual);

                MessageBox.Show(" Cruce efectuado exitosamente ", " Exito ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();

                frmMovContab = new frmConteMovContab(movContab.datosComprobante(comprobante, compania.BaseDatosActual));
                frmMovContab.Show();
            }
        }

        private void frmCruceDeSaldo_Load(object sender, EventArgs e)
        {
            Principal = (Principal)this.MdiParent;
            compania.obtenerCodigoCompaniaActual();
            auxiliar = new lbxAuxiliaresOnly();
            planCuenta = new lbxAuxiliarContable();
        }
           
        private DataTable contenidoMovContab()
        {
            tablaMovcontab = movContab.armarDataTableMovContab();
            int items = 0;

            for (int i = 0; i < dataGridView2.RowCount; i++)
            {
                obtenerCuentasPro(dataGridView2.Rows[i].Cells[3].Value.ToString());

                items = items + 1;
                DataRow fila = tablaMovcontab.NewRow();

                fila[0] = items;
                fila[1] = txtCodigoProveedor.Text;
                //fila[2] = dataGridView3.Rows[i].Cells[1].Value.ToString(); //recibo
                fila[2] = dataGridView2.Rows[i].Cells[1].Value.ToString();
                fila[3] = dataGridView2.Rows[i].Cells[2].Value.ToString();
                fila[4] = dataGridView2.Rows[i].Cells[3].Value.ToString();
                fila[5] = comprobante;            //COMPROBANTE
                fila[6] = lblNombrePro.Text + " ";
                fila[7] = mayorProveedor; //mayor proveedor                              //cc.CuentaProveedor;//CODIGO CUENTA

                if (Convert.ToDecimal(dataGridView2.Rows[i].Cells[5].Value) < 0)
                {
                    fila[8] = "-1";
                    fila[19] = "C";
                    montoC2 = montoC2 + Convert.ToDecimal(dataGridView2.Rows[i].Cells[5].Value);
                }
                else
                {
                    fila[8] = "1";
                    fila[19] = "D";
                    montoD2 = montoD2 + Convert.ToDecimal(dataGridView2.Rows[i].Cells[5].Value);
                }

                fila[9] = "";                                   //AQUI NO VA UN COÑO
                fila[10] = "" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString().PadLeft(2, '0') + "-" + DateTime.Now.Day.ToString();
                fila[11] = "" + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString();//me da lala esta parte copy and paste                                          //hora
                fila[12] = dataGridView2.Rows[i].Cells[5].Value.ToString().Replace(",", ".").Replace("-", "");
                fila[13] = "0";
                fila[14] = Principal.usuarioActual.Id;
                fila[15] = "CRUCE";
                fila[16] = auxiliarProveedor;                                //AUXILIAR  PROVEEDOR                                 
                fila[17] = txtCodigoProveedor.Text;                     //CODIGO
                fila[18] = lblNombrePro.Text;               //NOMBRE CLIENTE                     
                fila[20] = "";
                fila[21] = dataGridView2.Rows[i].Cells[1].Value.ToString(); // interno
                fila[22] = ""; // interno

                tablaMovcontab.Rows.Add(fila);
            }


            for (int i = 0; i < gridMovContab.RowCount; i++)
            {
                items = items + 1;
                DataRow fila = tablaMovcontab.NewRow();

                fila[0] = items;
                fila[1] = txtCodigoProveedor.Text;
                //fila[2] = dataGridView3.Rows[0].Cells[1].Value.ToString();
                fila[2] = comprobante; //aqui debe ir algún documento por los momentos tiene el numero del comprobante
                fila[3] = "0";
                fila[4] = dataGridView2.Rows[0].Cells[3].Value.ToString();
                fila[5] = comprobante;                     //COMPROBANTE
                fila[6] = lblNombrePro.Text + " ";
                fila[7] = gridMovContab.Rows[i].Cells[2].Value.ToString();  ////CODIGO CUENTA
                fila[8] = gridMovContab.Rows[i].Cells[6].Value.ToString();  //numero (1 o -1)
                fila[9] = "";                                               //AQUI NO VA UN COÑO
                fila[10] = "" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString().PadLeft(2, '0') + "-" + DateTime.Now.Day.ToString();
                fila[11] = "" + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString();//me da lala esta parte copy and paste                                          //hora
                fila[12] = gridMovContab.Rows[i].Cells[8].Value.ToString().Replace(",", ".");
                fila[13] = "0";
                fila[14] = Principal.usuarioActual.Id;
                fila[15] = "CRUCE";
                fila[16] = gridMovContab.Rows[i].Cells[7].Value.ToString();     //AUXILIAR                                   
                fila[17] = txtCodigoProveedor.Text;                             //CODIGO
                fila[18] = lblNombrePro.Text;                             //NOMBRE CLIENTE                     
                fila[19] = gridMovContab.Rows[i].Cells[3].Value.ToString();     //letra (D o C)
                fila[20] = "";
                fila[21] = "";
                fila[22] = "";

                tablaMovcontab.Rows.Add(fila);
            }

            return tablaMovcontab;
        }

        private void obtenerCuentasPro(string tipoDoc)
        {
            tablaB.Reset();
            tablaB.Clear();
            tablaB = clasePro.obtenerCuentasproveedor(txtCodigoProveedor.Text, tipoDoc);

            if (tablaB.Rows.Count > 0)
            {
                mayorProveedor = tablaB.Rows[0][0].ToString();
                auxiliarProveedor = tablaB.Rows[0][1].ToString();
            }
        }

        private void obtenerComprobanteContable()
        {
            tablaB.Reset();
            tablaB.Clear();
            compania.obtenerCodigoCompaniaActual();
            tablaB = movContab.obtenerNumeroComprobante(compania.BaseDatosActual, "04");

            comprobante = tablaB.Rows[0][1].ToString();
        }

        private bool validarCamposProceso()
        {
            if (dataGridView2.Rows.Count == 0)
            {
                MessageBox.Show("Debe seleccionar al menos un documento a procesar", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            if (dataGridView2.Rows.Cast<DataGridViewRow>().Sum(x => Convert.ToDouble(x.Cells[5].Value)) != 0 && gridMovContab.Rows.Count == 0)
            {
                MessageBox.Show("Debe ingresar un movimiento contable", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            if ((Convert.ToDecimal(dataGridView2.Rows.Cast<DataGridViewRow>().Sum(x => Convert.ToDouble(x.Cells[5].Value))) + montoDC) != 0)
            {
                MessageBox.Show("Descuadrado", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            return true;
        }


        private DataTable contenidoCabecera()
        {

            tablaCabacera.Clear();
            tablaCabacera.Reset();
            tablaCabacera = movContab.armarDataTableCabeceraMovContab();

            DataRow fila = tablaCabacera.NewRow();

            fila[0] = comprobante;
            fila[1] = "" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString().PadLeft(2, '0') + "-" + DateTime.Now.Day.ToString();
            fila[2] = compania.BaseDatosActual;
            fila[3] = Convert.ToString(dataGridView2.Rows.Cast<DataGridViewRow>().Sum(x => Convert.ToDouble(x.Cells[5].Value))).Replace(",", ".");
            fila[4] = Convert.ToString(dataGridView2.Rows.Cast<DataGridViewRow>().Sum(x => Convert.ToDouble(x.Cells[5].Value))).Replace(",", ".");
            fila[5] = "1";
            fila[6] = Principal.usuarioActual.Id;
            fila[7] = Principal.usuarioActual.Id;
            fila[8] = compania.BaseDatosActual;
            fila[9] = "CRUCE";
            fila[10] = "" + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString();
            fila[11] = "" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString().PadLeft(2, '0') + "-" + DateTime.Now.Day.ToString();
            fila[12] = "CRUCE " + lblNombrePro.Text;

            tablaCabacera.Rows.Add(fila);

            return tablaCabacera;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (gridMovContab.Rows.Count > 0)
            {
                gridMovContab.Rows.RemoveAt(gridMovContab.CurrentRow.Index);
            }
            sumaMovimientoContable();
        }

        private void btnBuscarPro_Click(object sender, EventArgs e)
        {
            proveedores = lbxProveedores.DefInstance;
            proveedores.destino(this, this.Name);
            proveedores.frmProduct = "Producto";//para invocar cuando lo invoca  otro modulo
            proveedores.Eleccion = "1";
            proveedores.Show();
        }

        private void reorganizarItems()
        {
            for (int i = 0; i < dataGridView2.Rows.Count; i++)
            {
                dataGridView2.Rows[i].Cells[0].Value = i + 1;
            }
        }

        private void txtCodigoProveedor_KeyPress(object sender, KeyPressEventArgs e)
        {
            ftextbox.OnlyNumbers(sender, e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtNumeroComprobante.Text != "")
            {
                if (txtNumeroComprobante.Text.Substring(0, 1) == "4" && txtNumeroComprobante.Text.Length == 10)
                {
                    compania.obtenerCodigoCompaniaActual();
                    tabla2 = movContab.ReimprimirComprobante(txtNumeroComprobante.Text, compania.BaseDatosActual, "CRUCE");
                    if (tabla2.Rows.Count > 0)
                    {
                        frmMovContab = new frmConteMovContab(tabla2);
                        frmMovContab.Show();
                    }
                    else
                    {
                        MessageBox.Show("No hay datos para reimprimir este reporte, verifique el numero", "    Advertencia    ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("No es un comprobante", "    Advertencia    ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
        }



    }
}
