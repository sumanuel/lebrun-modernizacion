using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using lebrun.clases.retenciones;
using lebrun.clasesData;
using lebrun.clases.contabilidad;
using lebrun.formularios.facturacion;
using lebrun.formularios.contabilidad;
using lebrun.clases.clientes;
using lebrun.Reportes.MovContab;


namespace lebrun.formularios.tesoreria
{
    public partial class frmRetIvaCli : Form
    {
        private frmConteMovContab rptMovContab;
        private Clientes cliente;
        private AuxiliarContable au;
        private lbxAuxiliaresOnly auxiliar;
        private lbxPlanCuentas planCuentaLbx;
        private lbxClientes refClientes;
        private retenciones ret;
        private Compania compania;
        private movimientoContable movConta;
        private FuncionesTexbox textBox;
        private PlanCuentas planCuenta;
        private DataTable tabla;
        private DataTable tablaMovcontabC;
        private DataTable tablaMovcontabD;
        private DataTable tablaCabecera;
        private DataTable tablaDetalles;
        private Principal Principal;
        private string saldo;
        private string nuevoSaldo;
        private string status;
        private string statusSalcli;
        private string alicuota;
        private decimal montoC2 = 0;
        private decimal montoD2 = 0;
        private int retExiste = 0;
        private string mes;
        private string anio;
        private string mayorCliente;
        private string auxiliarClientes;
        private string cuentaDoc;
        private string tipoDoc;
        private string comprobante;
        private string tipoDoc2 = "";

        public frmRetIvaCli()
        {
            InitializeComponent();
            //au = new AuxiliarContable();
            ret = new retenciones();
            textBox = new FuncionesTexbox();
            planCuenta = new PlanCuentas();
            tablaMovcontabC = new DataTable();
            tablaMovcontabD = new DataTable();
            movConta = new movimientoContable();
            compania = new Compania();
            au = new AuxiliarContable();
            cliente = new Clientes();
            tabla = new DataTable();
        }

        private void frmRetIvaCli_Load(object sender, EventArgs e)
        {
            cmbPorcRet.SelectedIndex = 0;
            Principal = (Principal)this.MdiParent;
        }

        private void cmbPorcRet_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == (char)(Keys.Enter))
            {
                SendKeys.Send("{TAB}");
            }
            else { e.Handled = true; }

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (retExiste != 1)
            {
                if (validarCampos() && validarFecha() && validarFechaRecibido() && validarCuentaContable())
                {
                    calcularSaldoDoc();
                    lblMontoRetenc.Text = "" + Convert.ToDecimal(txtMontoRet.Text) + " Bs";
                    panel1.Visible = true;
                    button3.Focus();
                }
            }

            if (retExiste == 1)
            {
                //ret.actualizarFechas(Convert.ToDateTime(fecha.Value).ToString("yyyy-MM-dd"), Convert.ToDateTime(fechaRecibido.Value).ToString("yyyy-MM-dd"));
                //MessageBox.Show("Actualización realizada con exito", " Exito ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                desbloquear();
            }
        }

        private void btnImportar_Click(object sender, EventArgs e)
        {
            fechaLibroCerrado();
            if (validarBusqueda())
            {

                tabla = ret.cargarDocumentoSalcli(txtCodigo.Text, txtControlFiscal.Text);

                if (tabla.Rows.Count > 0)
                {
                    txtTipoDoc.Text = tabla.Rows[0][0].ToString();
                    txtNumDoc.Text = tabla.Rows[0][1].ToString();
                    txtEstado.Text = tabla.Rows[0][2].ToString();
                    txtFecha.Text = Convert.ToDateTime(tabla.Rows[0][3].ToString()).ToString("dd/MM/yyyy");
                    txtNeto.Text = tabla.Rows[0][4].ToString();
                    txtExento.Text = "0,00";
                    txtBaseImponible.Text = tabla.Rows[0][5].ToString();
                    txtIva.Text = tabla.Rows[0][6].ToString();
                    saldo = tabla.Rows[0][8].ToString();
                    statusSalcli = tabla.Rows[0][9].ToString();

                    bloquear();
                    bloquearCuenta();
                }
                else
                {
                    tabla = ret.cargarDocumentoDocCli(txtCodigo.Text, txtControlFiscal.Text);

                    if (tabla.Rows.Count > 0)
                    {
                        txtTipoDoc.Text = tabla.Rows[0][0].ToString();
                        txtNumDoc.Text = tabla.Rows[0][1].ToString();
                        txtEstado.Text = tabla.Rows[0][2].ToString();
                        txtFecha.Text = Convert.ToDateTime(tabla.Rows[0][3].ToString()).ToString("dd/MM/yyyy");

                        if (txtTipoDoc.Text == "DEV")
                        {
                            if (Convert.ToDecimal(tabla.Rows[0][4].ToString()) > 0)
                            {
                                txtNeto.Text = "" + Convert.ToDecimal(tabla.Rows[0][4].ToString()) * -1;
                            }
                            else
                            {
                                txtNeto.Text = "0,00";
                            }

                            txtExento.Text = "0,00";

                            if (Convert.ToDecimal(tabla.Rows[0][5].ToString()) > 0)
                            {
                                txtBaseImponible.Text = "" + Convert.ToDecimal(tabla.Rows[0][5].ToString()) * -1;
                            }
                            else
                            {
                                txtBaseImponible.Text = "0,00";
                            }
                            if (Convert.ToDecimal(tabla.Rows[0][6].ToString()) > 0)
                            {
                                txtIva.Text = "" + Convert.ToDecimal(tabla.Rows[0][6].ToString()) * -1;
                            }
                            else
                            {
                                txtIva.Text = "0,00";
                            }

                            if (Convert.ToDecimal(tabla.Rows[0][8].ToString()) > 0)
                            {
                                saldo = "" + Convert.ToDecimal(tabla.Rows[0][8].ToString()) * -1;
                            }
                            else
                            {
                                saldo = "0";
                            }
                        }

                        if (txtTipoDoc.Text == "FAV")
                        {
                            txtNeto.Text = tabla.Rows[0][4].ToString();
                            txtExento.Text = "0,00";
                            txtBaseImponible.Text = tabla.Rows[0][5].ToString();
                            txtIva.Text = tabla.Rows[0][6].ToString();
                            saldo = tabla.Rows[0][8].ToString();
                        }

                        statusSalcli = tabla.Rows[0][9].ToString();

                        bloquear();
                        bloquearCuenta();
                    }
                    else
                    {
                        MessageBox.Show("No existe ningun documento con este numero", " Aviso ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                tabla.Clear();
                tabla.Reset();

                tabla = ret.buscarSalcli(txtCodigo.Text, txtNumDoc.Text);

                if (tabla.Rows.Count > 0)
                {
                    txtNumRetenc.Text = tabla.Rows[0][0].ToString();
                    txtMontoRet.Text = tabla.Rows[0][1].ToString();
                    cmbPorcRet.Text = "" + Convert.ToInt16(Convert.ToDecimal(tabla.Rows[0][2].ToString()));
                    retExiste = 1;

                    MessageBox.Show("Este Documento ya tiene una retencion asociada", " Aviso ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    soloLectura();
                    button2.Focus();

                }
            }

        }

        private void cmbPorcRet_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtIva.Text != "" && cmbPorcRet.Text != "")
            {
                txtMontoRet.Text = "" + (Convert.ToDecimal(txtIva.Text) * Convert.ToDecimal(cmbPorcRet.Text)) / 100;
                txtMontoRet.Text = string.Format("{0:0.00}", Convert.ToDecimal(txtMontoRet.Text));
                calcularSaldoDoc();
                alicuota = ret.obtenerAlicuota(txtCodigo.Text, txtNumDoc.Text, txtTipoDoc.Text);
            }
        }

        private void cmbPorcRet_Leave(object sender, EventArgs e)
        {
            if (txtIva.Text != "" && cmbPorcRet.Text != "")
            {
                txtMontoRet.Text = "" + (Convert.ToDecimal(txtIva.Text) * Convert.ToDecimal(cmbPorcRet.Text)) / 100;
                txtMontoRet.Text = string.Format("{0:0.00}", Convert.ToDecimal(txtMontoRet.Text));
                calcularSaldoDoc();
                alicuota = ret.obtenerAlicuota(txtCodigo.Text, txtNumDoc.Text, txtTipoDoc.Text);
            }
        }

        private void calcularSaldoDoc() {

            //if (txtTipoDoc.Text == "DEV")
            //{
            //    saldo = "-" + saldo;
            //}

            nuevoSaldo = "" + (Convert.ToDecimal(saldo) - Convert.ToDecimal(txtMontoRet.Text));

            if (Convert.ToDecimal(nuevoSaldo) == 0){status = "1";}else { status = "0"; }
            
            //if (txtTipoDoc.Text == "DEV")
            //{
            //    nuevoSaldo = "-" + nuevoSaldo;
            //}
        }

        private void armarDataTable()
        {
            tabla.Reset();

            tabla.Columns.Add("codigo");        //1
            tabla.Columns.Add("sucursal");      //2
            tabla.Columns.Add("cliConta");      //3
            tabla.Columns.Add("numeroDocum");   //4
            tabla.Columns.Add("tipoDoc");       //5
            tabla.Columns.Add("numReten");      //6
            tabla.Columns.Add("cliDocCli");     //7
            tabla.Columns.Add("TipoTrans");     //8
            tabla.Columns.Add("fecha");         //9
            tabla.Columns.Add("porceReten");    //10
            tabla.Columns.Add("pIva");          //11
            tabla.Columns.Add("montoRetenc");   //12
            tabla.Columns.Add("Recibido");      //13
            tabla.Columns.Add("sistema");       //14

        }
        private DataTable contenidoDataTable()
        {
            armarDataTable();

                DataRow fila = tabla.NewRow();

                fila[0] = txtCodigo.Text;
                fila[1] = "000001";
                fila[2] = "0";
                fila[3] = txtNumDoc.Text;
                fila[4] = txtTipoDoc.Text;
                fila[5] = txtNumRetenc.Text;
                fila[6] = "RIVA";
                if (txtTipoDoc.Text == "FAV")
                {
                    fila[7] = "-1";
                }
                else {
                    fila[7] = "1";
                }

                fila[8] = Convert.ToDateTime(fecha.Value).ToString("yyyy-MM-dd");
                fila[9] = cmbPorcRet.Text;
                fila[10] = alicuota.Replace(",", ".");
                fila[11] = txtMontoRet.Text.Replace(",", ".");
                fila[12] = "" + Convert.ToDateTime(fechaRecibido.Value).ToString("yyyy-MM-dd");    
                fila[13] = "" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString().PadLeft(2, '0') + "-" + DateTime.Now.Day.ToString();

                tabla.Rows.Add(fila);
           
            return tabla;
        }

        private bool validarCampos()
        {
            bool flag;
            flag = false;

            if (txtNumDoc.Text != "")
            {
                flag = true;
            }
            else { flag = false; MessageBox.Show("Debe seleccionar un documento a procesar", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information); return false; }


            if (cmbPorcRet.Text != "")
            {
                flag = true;
            }
            else { flag = false; MessageBox.Show("Debe seleccionar un porcentaje de retención", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information); return false; }


            if (txtNumRetenc.Text != "")
            {
                flag = true;
            }
            else { flag = false; MessageBox.Show("El numero de la retencion no debe quedar vacio", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information); txtNumRetenc.Focus(); return false; }

            if (txtNumRetenc.Text.Length < 14)
            {
                MessageBox.Show("El numero de la retencion no debe ser menor de 14 digitos", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtNumRetenc.Focus();
                return false;
            }
            else
            {
                flag = true;
            }

            if (Convert.ToDecimal(txtMontoRet.Text) != 0)
            {
                flag = true;
            }
            else { flag = false; MessageBox.Show("El monto de la retencion no debe ser igual a cero(0)", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);  return false; }

            return flag;
        }

        private void bloquear() {

            groupBox1.Enabled = false;
        }

        private void bloquearCuenta() {

            if (statusSalcli == "1" || statusSalcli == "")
            {
                groupBox4.Enabled = true;
            }
            else
            {
                groupBox4.Enabled = false;
            }
        
        }

        private void desbloquear() {

            groupBox4.Enabled = true;
            groupBox1.Enabled = true;
            txtBaseImponible.Text = "";
            txtEstado.Text = "";
            txtExento.Text = "";
            txtFecha.Text = "";
            txtIva.Text = "";
            txtMontoRet.Text = "0,00";
            txtNeto.Text = "";
            txtNumDoc.Text = "";
            txtNumRetenc.Text = "";
            txtTipoDoc.Text = "";
            txtCuenta.Text = "";
            txtAuxiliar.Text = "";
            lblDescripcionCuenta.Text = "";
            cmbPorcRet.Text = "";

            txtTipoDoc.ReadOnly = false;
            txtNumDoc.ReadOnly = false;
            txtEstado.ReadOnly = false;
            txtFecha.ReadOnly = false;
            txtNeto.ReadOnly = false;
            txtExento.ReadOnly = false;
            txtBaseImponible.ReadOnly = false;
            txtIva.ReadOnly = false;
            txtNumRetenc.ReadOnly = false;
            cmbPorcRet.Enabled = true;
            txtMontoRet.ReadOnly = false;
            txtCuenta.ReadOnly = false;
            groupBox4.Enabled = false;
            retExiste = 0;
            btnAceptar.Enabled = true;

        }

        private void soloLectura()
        {
            txtTipoDoc.ReadOnly = true;
            txtNumDoc.ReadOnly = true;
            txtEstado.ReadOnly = true;
            txtFecha.ReadOnly = true;
            txtNeto.ReadOnly = true;
            txtExento.ReadOnly = true;
            txtBaseImponible.ReadOnly = true;
            txtIva.ReadOnly = true;

            txtNumRetenc.ReadOnly = true;
            txtNumRetenc.BackColor = SystemColors.Window; 

            cmbPorcRet.Enabled = false;
            cmbPorcRet.BackColor = SystemColors.Window; 

            txtMontoRet.ReadOnly = true;
            txtMontoRet.BackColor = SystemColors.Window; 

            txtCuenta.ReadOnly = true;
            txtCuenta.BackColor = SystemColors.Window;

            btnAceptar.Enabled = false;
        }

        private bool validarCuentaContable() {

            if (groupBox4.Enabled == true && txtCuenta.Text == "")
            {
                MessageBox.Show("Debe seleccionar una cuenta contable", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCuenta.Focus();
                return false;
            }

            return true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            desbloquear();
            txtCodigo.Focus();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (retExiste != 1)
            {

                ret.IngresarRti(contenidoDataTable());
                ret.actualizarSaldo(nuevoSaldo, status, txtNumDoc.Text);
                ret.actualizarDoccli(txtMontoRet.Text.Replace(",", "."), txtTipoDoc.Text, txtNumDoc.Text);

                asignandoValores();

                movConta.ingresarMovContab(contenidoMovContab());
                movConta.cabeceraSyscontab(contenidoCabecera(), compania.BaseDatosActual);
                movConta.detallesSyscontab(contenidoMovContab(), compania.BaseDatosActual);
                
                
                panel1.Visible = false;

                cliente.habilitarCliente(txtCodigo.Text);
                cliente.suspenderCLientes(txtCodigo.Text);

                MessageBox.Show("Operacion realizada con exito", " Exito ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                imprimirReporteContablidad();
                desbloquear();
            }

            if (retExiste == 1)
            {
                //ret.actualizarFechas(Convert.ToDateTime(fecha.Value).ToString("yyyy-MM-dd"), Convert.ToDateTime(fechaRecibido.Value).ToString("yyyy-MM-dd"));
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
        }

        private bool validarFecha() {

            if (Convert.ToDateTime(fecha.Text) < Convert.ToDateTime(txtFecha.Text))
            {
                MessageBox.Show("La fecha de retencion no puede ser menor a la fecha de emision de la factura", " Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                fecha.Focus();
                return false;
            }

            if (Convert.ToDateTime(fecha.Text) > Convert.ToDateTime(DateTime.Now.ToShortDateString()))
            {
                MessageBox.Show("La fecha de retencion no puede ser mayor a la fecha actual", " Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                fecha.Focus();
                return false;
            }

            return true;
        }

        private bool validarFechaRecibido()
        {

            if (Convert.ToDateTime(fechaRecibido.Text) < Convert.ToDateTime(fecha.Text))
            {
                MessageBox.Show("La fecha de Recibido no puede menor a la fecha de la emision de la retencion", " Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                fechaRecibido.Focus();
                return false;
            }

            if (Convert.ToDateTime(fechaRecibido.Text) > Convert.ToDateTime(DateTime.Now.ToShortDateString()))
            {
                MessageBox.Show("La fecha de Recibido no puede mayor a la fecha actual", " Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                fechaRecibido.Focus();
                return false;
            }

            if (Convert.ToDateTime(fechaRecibido.Text).Year.ToString() == anio && Convert.ToDateTime(fechaRecibido.Text).Month.ToString() == mes)
            {
                MessageBox.Show("No puede cargar retenciones con esta fecha, libro de ventas cerrado", " Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                fechaRecibido.Focus();
                return false;
            }

            return true;
        }

        private void txtNumRetenc_KeyPress(object sender, KeyPressEventArgs e)
        {
            textBox.onlyNumberWithTab(sender, e);
        }

        private void txtMontoRet_KeyPress(object sender, KeyPressEventArgs e)
        {
            textBox.conDecimalesDefinidos(sender, e, txtMontoRet, 2);
        }

        private void fecha_KeyPress(object sender, KeyPressEventArgs e)
        {
            textBox.onlyNumberWithTab(sender, e);
        }

        private void fechaRecibido_KeyPress(object sender, KeyPressEventArgs e)
        {
            //textBox.onlyNumberWithTab(sender, e);
            if (e.KeyChar == 13 && groupBox4.Enabled == true)
            {
                txtCuenta.Focus();
            }
            if (e.KeyChar == 13 && groupBox4.Enabled == false)
            {
                btnAceptar.Focus();
            }

        }

        private void txtCuenta_Leave(object sender, EventArgs e)
        {
            //verificarCuentaContable();
            buscarVerificarPlanC();
        }

        private bool verificarCuentaContable() {

            if (txtCuenta.Text != "")
            {
                lblDescripcionCuenta.Text = planCuenta.getDescripcionPlanC(txtCuenta.Text, "01");
                
                if (lblDescripcionCuenta.Text == "")
                {

                    MessageBox.Show("Este numero de cuenta no existe", " Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtAuxiliar.Text = "";
                    txtAuxiliar.Enabled = false;
                    btnAuxiliar.Enabled = false;
                    lblDescripcionCuenta.Text = "...";
                    txtCuenta.Text = "";
                    txtCuenta.Focus();
                    return false;
                }
            }

            return true;
        }


        public DataTable contenidoMovContab()
        {
            tabla.Clear();
            tabla.Reset();

            tabla = movConta.armarDataTableMovContab();
            int items = 0;


            if (Convert.ToDecimal(txtMontoRet.Text) != 0)
            {


                if (txtTipoDoc.Text == "FAV")
                {
                    tipoDoc2 = "FACT";
                }
                else if (txtTipoDoc.Text == "DEV")
                {
                    tipoDoc2 = "N/C";
                }
                else
                {
                    tipoDoc2 = txtTipoDoc.Text;
                }
                    
                    

                if (statusSalcli == "1" || statusSalcli == "")
                {
                    DataRow fila = tabla.NewRow();
                    items = items + 1;

                    fila[0] = items;
                    fila[1] = txtCodigo.Text;
                    fila[2] = txtNumDoc.Text;
                    fila[3] = "0";
                    fila[4] = tipoDoc;
                    fila[5] = comprobante;
                    fila[6] = "RET IVA S/ " + lblNombreCliente.Text + " " + tipoDoc2 + " " + txtControlFiscal.Text;
                    fila[7] = txtCuenta.Text;

                    if (txtTipoDoc.Text == "FAV")
                    {
                        fila[8] = "-1";
                        fila[19] = "C";
                        montoC2 = montoC2 + Convert.ToDecimal(txtMontoRet.Text);

                    }
                    else
                    {
                        fila[8] = "1";
                        fila[19] = "D";
                        montoD2 = montoD2 + Convert.ToDecimal(txtMontoRet.Text);
                    }

                    fila[9] = "";
                    fila[10] = "" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString().PadLeft(2, '0') + "-" + DateTime.Now.Day.ToString();
                    fila[11] = "" + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString();
                    fila[12] = Convert.ToString(Math.Abs(Convert.ToDecimal(txtMontoRet.Text))).Replace(",", ".");
                    fila[13] = "0";
                    fila[14] = Principal.usuarioActual.Id;
                    fila[15] = "RTI";
                    fila[16] = txtAuxiliar.Text; //auxiliar
                    fila[17] = txtCodigo.Text;
                    fila[18] = lblNombreCliente.Text;
                    fila[20] = "";
                    fila[21] = txtNumDoc.Text;
                    fila[22] = txtNumRetenc.Text;

                    tabla.Rows.Add(fila);
                }
                else
                {

                    DataRow fila = tabla.NewRow();
                    items = items + 1;

                    fila[0] = items;
                    fila[1] = txtCodigo.Text;
                    fila[2] = txtNumDoc.Text;
                    fila[3] = "0";
                    fila[4] = tipoDoc;
                    fila[5] = comprobante;
                    fila[6] = "RET IVA S/ " + lblNombreCliente.Text + " " + tipoDoc2 + " " + txtControlFiscal.Text;
                    fila[7] = mayorCliente;

                    if (txtTipoDoc.Text == "FAV")
                    {
                        fila[8] = "-1";
                        fila[19] = "C";
                        montoC2 = montoC2 + Convert.ToDecimal(txtMontoRet.Text);

                    }
                    else
                    {
                        fila[8] = "1";
                        fila[19] = "D";
                        montoD2 = montoD2 + Convert.ToDecimal(txtMontoRet.Text);
                    }

                    fila[9] = "";
                    fila[10] = "" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString().PadLeft(2, '0') + "-" + DateTime.Now.Day.ToString();
                    fila[11] = "" + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString();
                    fila[12] = Convert.ToString(Math.Abs(Convert.ToDecimal(txtMontoRet.Text))).Replace(",", ".");
                    fila[13] = "0";
                    fila[14] = Principal.usuarioActual.Id;
                    fila[15] = "RTI";
                    fila[16] = auxiliarClientes;
                    fila[17] = txtCodigo.Text;
                    fila[18] = lblNombreCliente.Text;
                    fila[20] = "";
                    fila[21] = txtNumDoc.Text;
                    fila[22] = txtNumRetenc.Text;

                    tabla.Rows.Add(fila);
                }

                
            }


            if (Convert.ToDecimal(txtMontoRet.Text) != 0)
            {
                items = items + 1;
                DataRow fila = tabla.NewRow();

                fila[0] = items;
                fila[1] = txtCodigo.Text;
                fila[2] = txtNumDoc.Text;
                fila[3] = "0";
                fila[4] = tipoDoc;
                fila[5] = comprobante;
                fila[6] = "RET IVA S/ " + lblNombreCliente.Text + " " + tipoDoc2 + " " + txtControlFiscal.Text;
                fila[7] = cuentaDoc;                            

                if (txtTipoDoc.Text == "FAV")
                {
                    fila[8] = "1";
                    fila[19] = "D";
                    montoC2 = montoC2 + Convert.ToDecimal(txtMontoRet.Text);
                }
                else
                {
                    fila[8] = "-1";
                    fila[19] = "C";
                    montoD2 = montoD2 + Convert.ToDecimal(txtMontoRet.Text);
                }

                fila[9] = "";                                   
                fila[10] = "" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString().PadLeft(2, '0') + "-" + DateTime.Now.Day.ToString();
                fila[11] = "" + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString();
                fila[12] = Convert.ToString(Math.Abs(Convert.ToDecimal(txtMontoRet.Text))).Replace(",", ".");
                fila[13] = "0";
                fila[14] = Principal.usuarioActual.Id;
                fila[15] = "RTI";
                fila[16] = "";                                                            
                fila[17] = txtCodigo.Text;                 
                fila[18] = lblNombreCliente.Text;                               
                fila[20] = "";
                fila[21] = txtNumDoc.Text;
                fila[22] = txtNumRetenc.Text;

                tabla.Rows.Add(fila);
            }

            return tabla;
        }



        private DataTable contenidoCabecera()
        {
            tabla.Clear();
            tabla.Reset();
            tabla = movConta.armarDataTableCabeceraMovContab();

            if (txtTipoDoc.Text == "FAV")
            {
                tipoDoc2 = "FACT";
            }
            else if (txtTipoDoc.Text == "DEV")
            {
                tipoDoc2 = "N/C";
            }
            else
            {
                tipoDoc2 = txtTipoDoc.Text;
            }

            DataRow fila = tabla.NewRow();

            fila[0] = comprobante;
            fila[1] = "" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString().PadLeft(2, '0') + "-" + DateTime.Now.Day.ToString();
            fila[2] = compania.BaseDatosActual;
            fila[3] = Convert.ToString(Math.Abs(Convert.ToDecimal(txtMontoRet.Text))).Replace(",", ".");
            fila[4] = Convert.ToString(Math.Abs(Convert.ToDecimal(txtMontoRet.Text))).Replace(",", ".");
            fila[5] = "2";                          
            fila[6] = Principal.usuarioActual.Id;
            fila[7] = Principal.usuarioActual.Id;
            fila[8] = compania.BaseDatosActual;
            fila[9] = "RIV";                                              
            fila[10] = "" + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString();                                  
            fila[11] = "" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString().PadLeft(2, '0') + "-" + DateTime.Now.Day.ToString();
            fila[12] = "RET IVA S/ " + lblNombreCliente.Text + " " + tipoDoc2 + " " + txtControlFiscal.Text;


            tabla.Rows.Add(fila);

            return tabla;

        }

        private void buscarCuentasContables()
        {

            tabla = movConta.cuentaDoc("RIV");

            if (tabla.Rows.Count > 0)
            {
                cuentaDoc = tabla.Rows[0][0].ToString();
            }

            tabla = movConta.cuentaCliente(txtCodigo.Text);

            if (tabla.Rows.Count > 0)
            {
                mayorCliente = tabla.Rows[0][0].ToString();
                auxiliarClientes = tabla.Rows[0][1].ToString();
            }
        }

        private void AsignarTipoDoc()
        {
            if (txtTipoDoc.Text == "FAV") { tipoDoc = "37"; } else { tipoDoc = "43"; }
        }

        private void ObtenerComprobante()
        {
            compania.obtenerCodigoCompaniaActual();

            tabla = movConta.obtenerNumeroComprobante(compania.BaseDatosActual, "03");

            if (tabla.Rows.Count > 0)
            {
                comprobante = tabla.Rows[0][1].ToString();
            }
        }

        private void asignandoValores()
        {
            buscarCuentasContables();
            AsignarTipoDoc();
            ObtenerComprobante();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            refClientes = lbxClientes.DefInstance;
            refClientes.destino(this, 5);
            refClientes.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            compania.obtenerCodigoCompaniaActual();
            planCuentaLbx = lbxPlanCuentas.DefInstance;
            planCuentaLbx.soloVista(this, this.Name, compania.BaseDatosActual);
            planCuentaLbx.Show();
        }

        private void button6_Click(object sender, EventArgs e)
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



        private void txtAuxiliar_Leave(object sender, EventArgs e)
        {
            compania.obtenerCodigoCompaniaActual();

            if (txtAuxiliar.Text != "")
            {
                if (!au.permisoAuxiliar(txtCuenta.Text, txtAuxiliar.Text, compania.BaseDatosActual))
                {
                    MessageBox.Show("El auxiliar seleccionado no existe", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtAuxiliar.Text = "";
                }
            }
        }

        private void buscarVerificarPlanC()
        {
            if (txtCuenta.Text != "")
            {
                txtAuxiliar.Text = "";
                au.CodigoPlanCuentas = txtCuenta.Text;

                if (au.validarPlanCuenta(10, "01"))
                {
                    lblDescripcionCuenta.Text = au.getDescripcionPlanC(au.CodigoPlanCuentas, "01");
                    if (au.poseeAuxilar("01", au.CodigoPlanCuentas))
                    {
                        btnAuxiliar.Enabled = true;
                        txtAuxiliar.Enabled = true;
                        txtAuxiliar.Focus();

                    }
                    else
                    {
                        btnAuxiliar.Enabled = false;
                        txtAuxiliar.Enabled = false;
                       
                    }
                   
                }
                else
                {

                    MessageBox.Show("El numero de cuenta ingresado no existe", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lblDescripcionCuenta.Text = "";
                    txtCuenta.Text = "";
                    txtAuxiliar.Text = "";

                    //guard2 = false;
                    txtCuenta.Focus();
                   
                }
            }        
        }

        private void txtCuenta_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13 && txtAuxiliar.Enabled == true)
            {
                txtAuxiliar.Focus();
            }
            if (e.KeyChar == 13 && txtAuxiliar.Enabled == false)
            {
                btnAceptar.Focus();
            }
        }

        private void txtAuxiliar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnAceptar.Focus();
            }
        }

        private void txtCodigo_Leave(object sender, EventArgs e)
        {
            if (txtCodigo.Text != "")
            {
                tabla.Clear();
                tabla.Reset();

                txtCodigo.Text = txtCodigo.Text.ToUpper();
                tabla = cliente.buscarClienteUnico(txtCodigo.Text);

                if (tabla.Rows.Count > 0)
                {
                    lblNombreCliente.Text = tabla.Rows[0][1].ToString();
                }
                else
                {
                    MessageBox.Show("El cliente seleccionado no existe", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCodigo.Text = "";
                    lblNombreCliente.Text = "...";
                    txtCodigo.Focus();
                }
            }
            else
            {
                lblNombreCliente.Text = "...";
            }
        }

        private bool validarBusqueda() {

            if (txtCodigo.Text == "") {

                MessageBox.Show("El campo codigo cliente no debe quedar vacio", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCodigo.Focus();
                return false;
            }

            if (txtControlFiscal.Text == "") {
                MessageBox.Show("El campo # control fiscal no debe quedar vacio", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtControlFiscal.Focus();
                return false;
            }

            return true;
        }

        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) {
                txtControlFiscal.Focus();
                
            }
        }

        private void txtControlFiscal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) {
                btnImportar.Focus();
            }
        }

        private void imprimirReporteContablidad()
        {
            rptMovContab = new frmConteMovContab("MovContaRecibo", comprobante, txtCodigo.Text);
            rptMovContab.Show();
        }

        private void txtCodigo_TextChanged(object sender, EventArgs e)
        {
            txtControlFiscal.Text = "";
        }

        private void fechaLibroCerrado()
        {
            tabla = ret.fechaUltimoCierreLibro();

            if (tabla.Rows.Count > 0)
            {
                mes = tabla.Rows[0][1].ToString();
                anio = tabla.Rows[0][2].ToString();
            }
        }

        private void reimprimirComprobante()
        {
            if (txtComprobante.Text != "")
            {
                rptMovContab = new frmConteMovContab("MovContaRecibo", txtComprobante.Text);
                rptMovContab.Show();
            }
        }

        private void btnReimprimir_Click(object sender, EventArgs e)
        {
            reimprimirComprobante();
        }

        private void cmbPorcRet_ValueMemberChanged(object sender, EventArgs e)
        {
            if (txtIva.Text != "")
            {
                txtMontoRet.Text = "" + (Convert.ToDecimal(txtIva.Text) * Convert.ToDecimal(cmbPorcRet.Text)) / 100;
                txtMontoRet.Text = string.Format("{0:0.00}", Convert.ToDecimal(txtMontoRet.Text));
                calcularSaldoDoc();
                alicuota = ret.obtenerAlicuota(txtCodigo.Text, txtNumDoc.Text, txtTipoDoc.Text);
            }
        }









        
        






 
    }
}
