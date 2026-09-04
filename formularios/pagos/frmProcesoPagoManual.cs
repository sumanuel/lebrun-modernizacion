using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using lebrun.formularios.complementos;
using lebrun.formularios.bancos;
using lebrun.clases.complementos;
using lebrun.clases.bancos;
using lebrun.clasesData;
using lebrun.clases.contabilidad;
using lebrun.clases.compras;
using lebrun.clases.proveedores;
using lebrun.formularios.contabilidad;
using lebrun.Reportes.compras;
using lebrun.Reportes.MovContab;

namespace lebrun.formularios.compras
{
   
    public partial class frmProcesoPagoManual : Form
    {
        private frmConteMovContab frmMovContab;
        private frmContendorRptCompras reporte;
        private lbxAuxiliaresOnly auxiliar;
        private lbxAuxiliarContable planCuenta;
        private AuxiliarContable au; 
        private FuncionesTexbox ftextbox;
        //private proveedor proveedor;
        private Pagos pago;
        private movimientoContable movContab;
        private Compania compania;
        private TipoDocumentoProveedor tipoDocumento; 
        private proveedor cproveedores;
        private Proveedores clasePro;
        private lbxProveedores proveedores;
        private DataTable tabla;
        private DataTable tablaB;
        private DataTable tablaPago;
        private DataTable tablaChequera;
        private DataTable tablaMovcontab;
        private DataTable tablaCabacera;
        private Banco banco;
        private lbxBancos lbxBanco;
        private lbxtipoDocPro lbxTipDocpro;
        private Principal Principal;
        public bool guard2;
        private string mayorProveedor;
        private string auxiliarProveedor;
        private string mayorBanco;
        private string auxiliarBanco;
        private string comprobante;
        private string rifProvee;
        private int chequeraActiva;
        private int desde1;
        private int hasta1;
        private int ultimo1;
        private int desde2;
        private int hasta2;
        private int ultimo2;
        private int desde3;
        private int hasta3;
        private int ultimo3;
        private int centinelaFila = -1;
        private decimal montoD;
        private decimal montoC;
        private decimal montoD2;
        private decimal montoC2;
        private decimal montoDC;
        private bool identificador = false;
        private string compañiaActual;
        private string idb;
        private decimal desdeIDB;
        private decimal hastaIDB;
        private decimal PorcIDB;
        private string cuentaIDB;

        public frmProcesoPagoManual()
        {
            InitializeComponent();
            pago = new Pagos();
            cproveedores = new proveedor();
            tabla = new DataTable();
            tablaB = new DataTable();
            tablaPago = new DataTable();
            tablaMovcontab = new DataTable();
            tablaCabacera = new DataTable();
            banco = new Banco();
            compania = new Compania();
            tablaChequera = new DataTable();
            tipoDocumento = new TipoDocumentoProveedor();
            movContab = new movimientoContable();
            clasePro = new Proveedores();
            ftextbox = new FuncionesTexbox();
            au = new AuxiliarContable();
            auxiliar = new lbxAuxiliaresOnly();
            cmbTipoCuenta.SelectedIndex = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (txtCodigoProvee.Text != "")
            {
                panel1.Visible = true;
                txtMontoCanDocP.Focus();
            }
            else
            {
                MessageBox.Show("Debe seleccionar algún proveedor", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnAceptarMonto_Click(object sender, EventArgs e)
        {
            agregarMonto();
            panel1.Visible = false;
        }

        private void btnCancMonto_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
        }

        private void cargarPorDefecto() {

            dataGridView2.Rows[0].Cells[0].Value = "000000000";
            dataGridView2.Rows[0].Cells[1].Value = "0";
            dataGridView2.Rows[0].Cells[2].Value = "37";
            dataGridView2.Rows[0].Cells[3].Value = "0,00";

            dataGridView2.AllowUserToResizeColumns = false;
            dataGridView2.AllowUserToResizeRows = false;
        }

        private void modificarDatosDoc()
        {

            dataGridView2.Rows[0].Cells[0].Value = txtNumDoc.Text;
            dataGridView2.Rows[0].Cells[1].Value = txtSufiDoc.Text;
            dataGridView2.Rows[0].Cells[2].Value = txtTipoDoc.Text;
            dataGridView2.Rows[0].Cells[3].Value = "0,00";

            dataGridView2.AllowUserToResizeColumns = false;
            dataGridView2.AllowUserToResizeRows = false;

        }

        private void agregarMonto()
        {

            dataGridView3.Rows[0].Cells[0].Value = "1";
            dataGridView3.Rows[0].Cells[1].Value = dataGridView2.Rows[0].Cells[0].Value;
            dataGridView3.Rows[0].Cells[2].Value = dataGridView2.Rows[0].Cells[1].Value;
            dataGridView3.Rows[0].Cells[3].Value = dataGridView2.Rows[0].Cells[2].Value;
            dataGridView3.Rows[0].Cells[4].Value = txtMontoCanDocP.Text;
            dataGridView3.Rows[0].Cells[5].Value = txtMontoCanDocP.Text;

            dataGridView3.AllowUserToResizeColumns = false;
            dataGridView3.AllowUserToResizeRows = false;

            txtMontoCheque.Text = txtMontoCanDocP.Text;

        }

        private void frmProcesoPagoManual_Load(object sender, EventArgs e)
        {
            Principal = (Principal)this.MdiParent;
            cargarPorDefecto();
            planCuenta = new lbxAuxiliarContable();
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (txtCodigoProvee.Text != "")
            {
                if (!consultaRapidaProveedor())
                {
                    txtCodigoProvee.Focus();
                }
                else
                {
                    cargarPorDefecto();
                    dataGridView3.Rows.Clear();
                    txtMontoCheque.Text = "0";

                    if (txtCodigoProvee.Text != "08900000")
                    {
                        lblNombreProveedor3.ReadOnly = true;
                        panel2.Location = new Point(265, 64);
                        panel2.Visible = true;
                        txtTipoDoc.Focus();
                        identificador = true;
                    }
                    else
                    {
                        rifProvee = "";
                        lblNombreProveedor3.ReadOnly = false;
                        identificador = false;
                    }
                }
            }
            else
            {
                lblNombreProveedor3.Text = "";  
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
            txtCodigoProvee.Text = "";
            lblNombreProveedor3.Text = "";
            txtCodigoProvee.Focus();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (validarCamposPanelDoc())
            {
                if (!pago.consultarExistenciaDoc(txtCodigoProvee.Text, txtNumDoc.Text, txtSufiDoc.Text, txtTipoDoc.Text))
                {
                    modificarDatosDoc();
                    panel2.Visible = false;
                }
                else
                {
                    MessageBox.Show("Este numero de documento ya existe", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            proveedores = lbxProveedores.DefInstance;
            proveedores.destino(this, this.Name);
            proveedores.frmProduct = "Producto";//para invocar cuando lo invoca  otro modulo
            proveedores.Eleccion = "1";
            proveedores.Show();
        }

        private bool consultaRapidaProveedor() //PROVEEDOR
        {
            bool val2 = false;

            DataTable dproveedor = new DataTable();
            dproveedor = cproveedores.consultarProveedorUnico(txtCodigoProvee.Text);

            if (dproveedor.Rows.Count != 0)
            {
                lblNombreProveedor3.Text = dproveedor.Rows[0][1].ToString();
                rifProvee = dproveedor.Rows[0][2].ToString();
                val2 = true;
            }
            else
            {
                MessageBox.Show("No existe proveedor con este codigo", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                val2 = false;
                txtCodigoProvee.Text = "";
                lblNombreProveedor3.Text = "";

            }
            return val2;

        }

        private void txtCodigoBanco_Leave(object sender, EventArgs e)
        {
            verificarBanco();
            cargarComboCuenta();
            obtenerCuentasBanco();
        }

        private void cargarComboCuenta()
        {

            cmbNumeroCuenta.DataSource = null;
            cmbNumeroCuenta.DisplayMember = "ctas_numero";
            cmbNumeroCuenta.ValueMember = "ctas_numero";
            cmbNumeroCuenta.DataSource = banco.obtenerNumerosCuenta(txtCodigoBanco.Text);

        }

        private void verificarBanco()
        {
            if (txtCodigoBanco.Text != "")
            {
                tablaB.Clear();
                tablaB.Reset();

                tablaB = banco.armarDatosBanco(txtCodigoBanco.Text);

                if (tablaB.Rows.Count > 0)
                {
                    if (tablaB.Rows[0][5].ToString() == "Activo")
                    {
                        lblNombreBanco.Text = tablaB.Rows[0][1].ToString();
                        txtNumeroCheque.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("El banco " + tablaB.Rows[0][1].ToString() + " está inactivo", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtCodigoBanco.Text = "";
                        lblNombreBanco.Text = "";
                        txtNumeroCheque.Text = "";
                        txtCodigoBanco.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("El codigo del banco ingresado no existe", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCodigoBanco.Text = "";
                    txtNumeroCheque.Text = "";
                    txtCodigoBanco.Focus();
                }
            }
        }

        private void controlChequeras()
        {

            if (chequeraActiva == 1)
            {
                if (ultimo1 <= hasta1)
                {
                    txtNumeroCheque.Text = "" + ultimo1;
                }
                else
                {
                    chequeraActiva = 2;
                    reactivarChequera();
                }
            }
            if (chequeraActiva == 2)
            {
                if (ultimo2 <= hasta2)
                {
                    txtNumeroCheque.Text = "" + ultimo2;
                }
                else
                {
                    chequeraActiva = 3;
                    reactivarChequera();
                }
            }
            if (chequeraActiva == 3)
            {
                if (ultimo3 <= hasta3)
                {
                    txtNumeroCheque.Text = "" + ultimo3;
                }
                else
                {
                    chequeraActiva = 0;
                    reactivarChequera();
                    MessageBox.Show("No hay chequeras disponibles", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtNumeroCheque.Text = "";
                }
            }
            if (chequeraActiva == 0)
            {
                MessageBox.Show("No hay chequeras activas para este numero de cuenta", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtNumeroCheque.Text = "";
            }

        }

        private void reactivarChequera()
        {
            banco.reactivarChequera(txtCodigoBanco.Text, cmbNumeroCuenta.Text, chequeraActiva);
        }

        private void cmbNumeroCuenta_Leave(object sender, EventArgs e)
        {
            if (txtCodigoBanco.Text != "" && cmbNumeroCuenta.Text != "")
            {
                datosChequera();
                controlChequeras();
            }
        }

        private void datosChequera()
        {

            tablaChequera = banco.obtenerDatosChequera(txtCodigoBanco.Text, cmbNumeroCuenta.Text);

            if (tablaChequera.Rows.Count > 0)
            {

                desde1 = (int)Convert.ToInt64(tablaChequera.Rows[0][0].ToString());
                hasta1 = (int)Convert.ToInt64(tablaChequera.Rows[0][1].ToString());
                ultimo1 = (int)Convert.ToInt64(tablaChequera.Rows[0][9].ToString());

                desde2 = (int)Convert.ToInt64(tablaChequera.Rows[0][3].ToString());
                hasta2 = (int)Convert.ToInt64(tablaChequera.Rows[0][4].ToString());
                ultimo2 = (int)Convert.ToInt64(tablaChequera.Rows[0][10].ToString());

                desde3 = (int)Convert.ToInt64(tablaChequera.Rows[0][6].ToString());
                hasta3 = (int)Convert.ToInt64(tablaChequera.Rows[0][7].ToString());
                ultimo3 = (int)Convert.ToInt64(tablaChequera.Rows[0][11].ToString());

                chequeraActiva = (int)Convert.ToInt64(tablaChequera.Rows[0][12].ToString());
                
                idb = tablaChequera.Rows[0][13].ToString();
                desdeIDB = Convert.ToDecimal(tablaChequera.Rows[0][14].ToString());
                hastaIDB = Convert.ToDecimal(tablaChequera.Rows[0][15].ToString());
                PorcIDB = Convert.ToDecimal(tablaChequera.Rows[0][16].ToString());
            }
        }

        private void btnBuscarbanco_Click(object sender, EventArgs e)
        {
            lbxBanco = lbxBancos.DefInstance;
            lbxBanco.destino(this, "procesoPagoManual");
            lbxBanco.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            lbxTipDocpro = lbxtipoDocPro.DefInstance;
            lbxTipDocpro.destino(this, this.Name);
            lbxTipDocpro.Show();
        }

        private bool consultaRapidaTipoDoc() //TipoDoc
        {
            bool val2 = false;

            DataTable dtipoDoc = new DataTable();
            dtipoDoc = tipoDocumento.consultarDocumento(txtTipoDoc.Text);

            if (dtipoDoc.Rows.Count != 0)
            {
                txtTipoDoc.Text = dtipoDoc.Rows[0][0].ToString();
                lblTipoDoc.Text = dtipoDoc.Rows[0][1].ToString();
                val2 = true;
            }
            else
            {
                MessageBox.Show("No existe documento con este codigo", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                val2 = false;
                txtTipoDoc.Text = "";
                lblTipoDoc.Text = "";

            }
            return val2;

        }

        private void txtTipoDoc_Leave(object sender, EventArgs e)
        {
            if (txtTipoDoc.Text != "")
            {
                //if (!consultaRapidaTipoDoc()) { txtTipoDoc.Focus(); }
                if (tipoDocumento.consultarDocProcePago(txtTipoDoc.Text))
                {
                    if (txtCodigoProvee.Text != "08900000")
                    {
                        if (!tipoDocumento.consultarDocProcePago2(txtTipoDoc.Text, txtCodigoProvee.Text))
                        {
                            MessageBox.Show("Este proveedor no tiene cuenta contable asignada para este tipo de documento", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtTipoDoc.Text = "";
                            lblTipoDoc.Text = "";
                            txtTipoDoc.Focus(); 
                        }
                    }
                }
                else {
                    MessageBox.Show("Tipo de documento no existe o no puede ser usado en este proceso", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtTipoDoc.Text = "";
                    lblTipoDoc.Text = "";
                    txtTipoDoc.Focus(); 
                
                }
            }
            else
            {
                lblTipoDoc.Text = "";
            }
        }

        private void txtCodigoProvee_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (txtCodigoProvee.Text == "08900000") {
                    lblNombreProveedor3.Focus(); 
                } else 
                {
                    if (consultaRapidaProveedor())
                    {
                        panel2.Visible = true;
                        txtTipoDoc.Focus();
                    }
                }
            }
        }

        private void txtMontoCanDocP_MouseUp(object sender, MouseEventArgs e)
        {
            txtMontoCanDocP.SelectAll();
        }

        private void lblNombreProveedor3_TextChanged(object sender, EventArgs e)
        {
            lblNombreProveedor3.Text = lblNombreProveedor3.Text.ToUpper();
            lblNombreProveedor3.Select(lblNombreProveedor3.Text.Length, 0);

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

        public void agregarFila(int fila, DataGridView grid)
        {

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

        private void limpiarMovcontab()
        {

            txtCuenta.Text = "";
            txtAuxiliar.Text = "";
            txtMontoMC.Text = "";
            btnAuxiliar.Enabled = false;

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

        private DataTable contenidoTablaPago()
        {
            armarTablePago();

            for (int i = 0; i < dataGridView3.Rows.Count; i++)
            {
                DataRow fila = tablaPago.NewRow();

                fila[0] = txtCodigoProvee.Text;
                if (dataGridView3.Rows[i].Cells[1].Value.ToString() == "000000000")
                {
                    fila[1] = comprobante;
                }
                else
                {
                    fila[1] = dataGridView3.Rows[i].Cells[1].Value.ToString();
                }
                fila[2] = dataGridView3.Rows[i].Cells[2].Value.ToString();
                fila[3] = dataGridView3.Rows[i].Cells[3].Value.ToString();
                fila[4] = lblNombreProveedor3.Text;
                fila[5] = "" + DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day;
                fila[6] = dataGridView3.Rows[i].Cells[4].Value.ToString().Replace(",", ".");
                fila[7] = txtMontoCheque.Text.Replace(",", ".");
                fila[8] = txtCodigoBanco.Text;
                fila[9] = Principal.usuarioActual.Id;
                fila[10] = txtNumeroCheque.Text;
                fila[11] = "4";
                fila[12] = "" + DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day;
                fila[13] = comprobante;
                fila[14] = cmbNumeroCuenta.Text;
                fila[15] = "CHEQUE  " + lblNombreProveedor3.Text;
                fila[16] = "" + DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day;
                fila[17] = "";
                fila[18] = "0.00";

                tablaPago.Rows.Add(fila);
            }
            return tablaPago;
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

        private void obtenerComprobanteContable()
        {
            tablaB.Reset();
            tablaB.Clear();
            compania.obtenerCodigoCompaniaActual();
            tablaB = movContab.obtenerNumeroComprobante(compania.BaseDatosActual, "05");

            comprobante = tablaB.Rows[0][1].ToString();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (validarCamposProceso())
            {
                obtenerComprobanteContable();
                contenidoTablaPago();
                pago.insertarProcesoPago(tablaPago);
                contenidoMovContab();

                movContab.ingresarMovContab(tablaMovcontab);
                movContab.detallesSyscontab(tablaMovcontab, compania.BaseDatosCompania);

                contenidoCabecera();
                movContab.cabeceraSyscontab(tablaCabacera, compania.BaseDatosCompania);

                MessageBox.Show(" Pago efectuado exitosamente ", " Exito ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                reporte = new frmContendorRptCompras(pago.imprimirVoucherPagoManual(txtNumeroCheque.Text, comprobante), "voucherManual");
                reporte.Show();
                frmMovContab = new frmConteMovContab(movContab.datosComprobante(comprobante, compania.BaseDatosActual, txtCodigoProvee.Text));
                frmMovContab.Show();
                this.Close();
            }
        }

        private DataTable contenidoMovContab()
        {
            tablaMovcontab = movContab.armarDataTableMovContab();
            int items = 0;
            obtenerCuentaIDB();
            for (int i = 0; i < gridMovContab.RowCount; i++)
            {
                items = items + 1;
                DataRow fila = tablaMovcontab.NewRow();

                fila[0] = items;
                fila[1] = txtCodigoProvee.Text;
                //fila[2] = dataGridView3.Rows[0].Cells[1].Value.ToString();

                if (dataGridView3.Rows[0].Cells[1].Value.ToString() == "000000000")
                {
                    fila[2] = comprobante;
                }
                else
                {
                    fila[2] = dataGridView3.Rows[0].Cells[1].Value.ToString();
                }

                fila[3] = "0";
                fila[4] = dataGridView3.Rows[0].Cells[3].Value.ToString();
                fila[5] = comprobante;                         //COMPROBANTE
                //fila[6] = txtDescripcion.Text;
                fila[6] = "CHEQUE  " + lblNombreProveedor3.Text + " ";
                fila[7] = gridMovContab.Rows[i].Cells[2].Value.ToString();  ////CODIGO CUENTA
                fila[8] = gridMovContab.Rows[i].Cells[6].Value.ToString();  //numero (1 o -1)
                fila[9] = "";                                               //AQUI NO VA UN COÑO
                fila[10] = "" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString().PadLeft(2, '0') + "-" + DateTime.Now.Day.ToString();
                fila[11] = "" + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString();//me da lala esta parte copy and paste                                          //hora
                fila[12] = gridMovContab.Rows[i].Cells[8].Value.ToString().Replace(",", ".");
                fila[13] = "0";
                fila[14] = Principal.usuarioActual.Id;
                fila[15] = "CHEM";
                fila[16] = gridMovContab.Rows[i].Cells[7].Value.ToString();     //AUXILIAR                                   
                fila[17] = rifProvee;                                 //CODIGO
                fila[18] = lblNombreProveedor3.Text;                             //NOMBRE CLIENTE                     
                fila[19] = "";
                fila[20] = gridMovContab.Rows[i].Cells[3].Value.ToString();     //letra (D o C)
                fila[21] = "";
                fila[22] = txtNumeroCheque.Text;

                tablaMovcontab.Rows.Add(fila);

            }

            for (int i = 0; i < dataGridView3.RowCount; i++)
            {
                obtenerCuentasPro(dataGridView3.Rows[0].Cells[3].Value.ToString());

                items = items + 1;
                DataRow fila = tablaMovcontab.NewRow();

                fila[0] = items;
                fila[1] = txtCodigoProvee.Text;
                //fila[2] = dataGridView3.Rows[i].Cells[1].Value.ToString(); //recibo

                if (dataGridView3.Rows[0].Cells[1].Value.ToString() == "000000000")
                {
                    fila[2] = comprobante;
                }
                else
                {
                    fila[2] = dataGridView3.Rows[0].Cells[1].Value.ToString();
                }

                fila[3] = "0";
                fila[4] = dataGridView3.Rows[i].Cells[3].Value.ToString();
                fila[5] = comprobante;                              //COMPROBANTE
                fila[6] = "CHEQUE  " + lblNombreProveedor3.Text + " ";
                fila[7] = mayorBanco;                               //mayor proveedor //cc.CuentaProveedor;//CODIGO CUENTA

                fila[8] = "-1";
                fila[20] = "C";
                montoC2 = montoC2 + Convert.ToDecimal(dataGridView3.Rows[i].Cells[4].Value);

                fila[9] = "";                                      //AQUI NO VA UN COÑO
                fila[10] = "" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString().PadLeft(2, '0') + "-" + DateTime.Now.Day.ToString();
                fila[11] = "" + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString();//me da lala esta parte copy and paste                                          //hora
                fila[12] = dataGridView3.Rows[i].Cells[4].Value.ToString().Replace(",", ".").Replace("-", "");
                fila[13] = "0";
                fila[14] = Principal.usuarioActual.Id;
                fila[15] = "CHEM";
                fila[16] = auxiliarBanco;                          //AUXILIAR  PROVEEDOR                                 
                fila[17] = rifProvee;                               //RIF
                fila[18] = lblNombreProveedor3.Text;               //NOMBRE CLIENTE                     
                fila[19] = "";
                fila[21] = "";                                     // interno
                fila[22] = txtNumeroCheque.Text;                   // interno

                tablaMovcontab.Rows.Add(fila);
            }
            


            if (txtCodigoProvee.Text != "08900000")
            {
                items = items + 1;
                DataRow fila = tablaMovcontab.NewRow();

                obtenerCuentasPro(dataGridView3.Rows[0].Cells[3].Value.ToString());

                fila[0] = items;
                fila[1] = txtCodigoProvee.Text;
                fila[2] = dataGridView3.Rows[0].Cells[1].Value.ToString(); //recibo
                fila[3] = "0";
                fila[4] = dataGridView3.Rows[0].Cells[3].Value.ToString();
                fila[5] = comprobante;            //COMPROBANTE
                fila[6] = " " + lblNombreProveedor3.Text + " ";
                fila[7] = mayorProveedor; //mayor proveedor                              //cc.CuentaProveedor;//CODIGO CUENTA

                fila[8] = "1";
                fila[20] = "D";
                montoC2 = montoC2 + Convert.ToDecimal(dataGridView3.Rows[0].Cells[4].Value);

                fila[9] = "";                                   //AQUI NO VA UN COÑO
                fila[10] = "" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString().PadLeft(2, '0') + "-" + DateTime.Now.Day.ToString();
                fila[11] = "" + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString();//me da lala esta parte copy and paste                                          //hora
                fila[12] = dataGridView3.Rows[0].Cells[4].Value.ToString().Replace(",", ".").Replace("-", "");
                fila[13] = "0";
                fila[14] = Principal.usuarioActual.Id;
                fila[15] = "CHEM";
                fila[16] = auxiliarProveedor;                                //AUXILIAR  PROVEEDOR                                 
                fila[17] = rifProvee;                           //CODIGO
                fila[18] = lblNombreProveedor3.Text;               //NOMBRE CLIENTE                     
                fila[19] = "";
                fila[21] = ""; // interno
                fila[22] = txtNumeroCheque.Text; // interno

                tablaMovcontab.Rows.Add(fila);

                //AQUI HAY QUE ACTUALIZAR SALDO PROVEEDOR
                pago.insertarSalproPagoManual(tablaPago);
                //AQUI CREAR DOCUMENTO EN ADMDOCPRO
                pago.crearDocPro(tablaPago);
            }
            if (idb == "1" && Convert.ToDecimal(txtMontoCheque.Text) > desdeIDB && Convert.ToDecimal(txtMontoCheque.Text) < hastaIDB)
            {
                items = items + 1;
                DataRow fila = tablaMovcontab.NewRow();

                fila[0] = items;
                fila[1] = txtCodigoProvee.Text;
                fila[2] = dataGridView2.Rows[0].Cells[2].Value.ToString(); //recibo
                fila[3] = "0";
                fila[4] = "37";
                fila[5] = comprobante;            //COMPROBANTE

                fila[6] = "CHEQUE " + lblNombreProveedor3.Text + " ";
                fila[7] = mayorBanco; //mayor proveedor                         //cc.CuentaProveedor;//CODIGO CUENTA

                //fila[8] = "1";
                //fila[20] = "D";
                fila[8] = "-1";
                fila[20] = "C";
                montoC2 = montoC2 + Convert.ToDecimal(txtMontoCheque.Text);

                fila[9] = "";                                                   //AQUI NO VA UN COÑO
                fila[10] = "" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString().PadLeft(2, '0') + "-" + DateTime.Now.Day.ToString();
                fila[11] = "" + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString();//me da lala esta parte copy and paste                                          //hora
                fila[12] = "" + Convert.ToString((Convert.ToDecimal(txtMontoCheque.Text) * PorcIDB / 100)).Replace(",", ".");
                fila[13] = "1";
                fila[14] = Principal.usuarioActual.Id;
                fila[15] = "CHEP";
                fila[16] = auxiliarBanco;                                       //AUXILIAR  PROVEEDOR                                 
                fila[17] = txtCodigoProvee.Text;                             //CODIGO
                fila[18] = lblNombreProveedor3.Text;                             //NOMBRE CLIENTE                     
                fila[19] = "";
                fila[21] = "";     // interno
                fila[22] = "";

                tablaMovcontab.Rows.Add(fila);
            }

            if (idb == "1" && Convert.ToDecimal(txtMontoCheque.Text) > desdeIDB && Convert.ToDecimal(txtMontoCheque.Text) < hastaIDB)
            {
                obtenerCuentaIDB();
                items = items + 1;
                DataRow fila = tablaMovcontab.NewRow();

                fila[0] = items;
                fila[1] = txtCodigoProvee.Text; 
                fila[2] = dataGridView2.Rows[0].Cells[2].Value.ToString(); //recibo
                fila[3] = "0";
                fila[4] = "37";
                fila[5] = comprobante;            //COMPROBANTE
                fila[6] = "CHEQUE " + lblNombreProveedor3.Text + " ";
                fila[7] = cuentaIDB; //mayor proveedor                         //cc.CuentaProveedor;//CODIGO CUENTA

                //fila[8] = "-1";
                //fila[20] = "C";
                fila[8] = "1";
                fila[20] = "D";
                montoC2 = montoC2 + Convert.ToDecimal(txtMontoCheque.Text);

                fila[9] = "";                                                   //AQUI NO VA UN COÑO
                fila[10] = "" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString().PadLeft(2, '0') + "-" + DateTime.Now.Day.ToString();
                fila[11] = "" + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString();//me da lala esta parte copy and paste                                          //hora
                fila[12] = "" + Convert.ToString((Convert.ToDecimal(txtMontoCheque.Text) * PorcIDB / 100)).Replace(",", ".");
                fila[13] = "1";
                fila[14] = Principal.usuarioActual.Id;
                fila[15] = "CHEP";
                fila[16] = "";                                       //AUXILIAR  PROVEEDOR                                 
                fila[17] = txtCodigoProvee.Text;                             //CODIGO
                fila[18] = lblNombreProveedor3.Text;                             //NOMBRE CLIENTE                     
                fila[19] = "";
                fila[21] = "";     // interno
                fila[22] = "";

                tablaMovcontab.Rows.Add(fila);
            }
            return tablaMovcontab;
        }
        private void obtenerCuentaIDB()
        {

            tablaB.Reset();
            tablaB.Clear();
            tablaB = movContab.cuentaIDB("95");

            if (tablaB.Rows.Count > 0)
            {
                cuentaIDB = tablaB.Rows[0][0].ToString();
            }
        }
        private void obtenerCuentasPro(string tipoDoc)
        {
            tablaB.Reset();
            tablaB.Clear();
            tablaB = clasePro.obtenerCuentasproveedor(txtCodigoProvee.Text, tipoDoc);

            if (tablaB.Rows.Count > 0)
            {
                mayorProveedor = tablaB.Rows[0][0].ToString();
                auxiliarProveedor = tablaB.Rows[0][1].ToString();
            }
            else
            {
                mayorProveedor = "";
                auxiliarProveedor = "";
            }
        }

        private void obtenerCuentasBanco()
        {
            tablaB.Reset();
            tablaB.Clear();
            //tablaB = banco.obtenerCuentaContableBanco(txtCodigoBanco.Text);
            tablaB = banco.obtenerCuentaContableBanco(txtCodigoBanco.Text, cmbNumeroCuenta.Text);
            mayorBanco = "";
            auxiliarBanco = "";

            if (tablaB.Rows.Count > 0)
            {
                mayorBanco = tablaB.Rows[0][0].ToString();
                auxiliarBanco = tablaB.Rows[0][1].ToString();
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (gridMovContab.Rows.Count > 0)
            {
                gridMovContab.Rows.RemoveAt(gridMovContab.CurrentRow.Index);
            }
            
            sumaMovimientoContable();
        }

        private bool validarCamposProceso()
        {
            if (Convert.ToDecimal(txtMontoCheque.Text) == 0)
            {
                MessageBox.Show("Debe ingresar un monto para este movimiento", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            if (txtCodigoBanco.Text == "")
            {
                MessageBox.Show("Debe seleccionar un banco", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            
            if (cmbTipoCuenta.Text == "")
            {
                MessageBox.Show("Debe seleccionar un tipo de cuenta", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            if (cmbNumeroCuenta.Text == "")
            {
                MessageBox.Show("Debe seleccionar un numero de cuenta", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            if (identificador == false && (montoDC != Convert.ToDecimal(txtMontoCheque.Text)))
            {
                MessageBox.Show("Movimiento descuadrado", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            if (identificador == true && (montoD != montoC))
            {
                MessageBox.Show("Movimiento descuadrado", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            if (identificador == false && (gridMovContab.Rows.Count == 0))
            {
                MessageBox.Show("Debe ingresar un movimiento contable", "Algo no está bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            if (txtNumeroCheque.Text == "")
            {
                MessageBox.Show("Seleccione un número de cuenta para asignar un número de cheque ", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            if (lblNombreProveedor3.Text == "")
            {
                MessageBox.Show("El campo nombre del proveedor no debe quedar vacio ", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lblNombreProveedor3.Focus();
                return false;
            }

            if (lblNombreProveedor3.Text == "PROVEEDORES VARIOS")
            {
                MessageBox.Show("Debe asignar un nombre valido para este proceso ", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lblNombreProveedor3.Focus();
                lblNombreProveedor3.SelectAll();
                return false;
            }

            return true;
        }

        private void cmbNumeroCuenta_SelectedValueChanged(object sender, EventArgs e)
        {
            if (txtCodigoBanco.Text != "" && cmbNumeroCuenta.Text != "")
            {
                datosChequera();
                controlChequeras();
                obtenerCuentasBanco();
            }
        }

        private void txtCodigoBanco_KeyPress(object sender, KeyPressEventArgs e)
        {
            ftextbox.onlyNumberWithTab(sender, e);
        }

        private void cmbTipoCuenta_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                cmbNumeroCuenta.Focus();
            }
            else
            {
                e.Handled = true;
            }
        }

        private void cmbNumeroCuenta_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtNumeroCheque.Focus();
            }
            else
            {
                e.Handled = true;
            }
            //ftextbox.tab(sender, e);
        }

        private void txtNumeroCheque_KeyPress(object sender, KeyPressEventArgs e)
        {
            ftextbox.tab(sender, e);
        }

        private void txtMontoCanDocP_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox a = (TextBox)sender;
            ftextbox.conDecimalesDefinidosNegativo(sender, e, a, 2);
        }

        private void limpiarGrids()
        {
            dataGridView2.Rows.Clear();
            dataGridView3.Rows.Clear();
        }

        private void txtTipoDoc_KeyPress(object sender, KeyPressEventArgs e)
        {
            ftextbox.tab(sender, e);
        }

        private void txtNumDoc_KeyPress(object sender, KeyPressEventArgs e)
        {
            ftextbox.tab(sender, e);
        }

        private void txtSufiDoc_KeyPress(object sender, KeyPressEventArgs e)
        {
            //ftextbox.tab(sender, e);
            ftextbox.onlyNumberWithTab(sender, e);
        }

        private void buscarVerificarPlanC()
        {
            if (txtCuenta.Text != "")
            {
                au.CodigoPlanCuentas = txtCuenta.Text;

                if (au.validarPlanCuenta(10, "01"))
                {
                    //txtNombreCuenta.Text = au.getDescripcionPlanC(au.CodigoPlanCuentas, "01");
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

        private void txtAuxiliar_Leave(object sender, EventArgs e)
        {
            if (txtAuxiliar.Text != "")
            {
                if (!au.permisoAuxiliar(txtCuenta.Text, txtAuxiliar.Text, compania.BaseDatosActual))
                {
                    MessageBox.Show("El auxiliar seleccionado no existe", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtAuxiliar.Text = "";
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

        private void txtMontoMC_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox a = (TextBox)sender;
            ftextbox.conDecimalesDefinidosNegativo(sender, e, a, 2);
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
            //if (txtCuenta.Text != "")
            //{
            //    auxiliar = lbxAuxiliaresOnly.DefInstance;
            //    auxiliar.destino(this, this.Name);
            //    auxiliar.codCuenta(txtCuenta.Text);
            //    //auxiliar.llenarAuxiliares(); 
            //    auxiliar.Show();
            //    auxiliar.llenarAuxiliares();
            //}
            //else
            //{
            //    MessageBox.Show("Debe seleccionar una cuenta antes de elegir un auxiliar", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
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

        private void txtDescripcion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtMontoMC.Focus();
            }
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
            fila[3] = Convert.ToString(Math.Abs(Convert.ToDecimal(txtMontoCheque.Text))).Replace(",", ".");
            fila[4] = Convert.ToString(Math.Abs(Convert.ToDecimal(txtMontoCheque.Text))).Replace(",", ".");
            fila[5] = "1";
            fila[6] = Principal.usuarioActual.Id;
            fila[7] = Principal.usuarioActual.Id;
            fila[8] = compania.BaseDatosActual;
            fila[9] = "CHEP";
            fila[10] = "" + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString();
            fila[11] = "" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString().PadLeft(2, '0') + "-" + DateTime.Now.Day.ToString();
            fila[12] = "CHEQUE " + lblNombreProveedor3.Text;

            tablaCabacera.Rows.Add(fila);

            return tablaCabacera;
        }

        private bool validarCamposPanelDoc()
        {
            if (txtTipoDoc.Text == "") 
            {
                MessageBox.Show("Debe seleccionar un tipo de documento", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTipoDoc.Focus();
                return false;
            }

            if (txtNumDoc.Text == "")
            {
                MessageBox.Show("Debe ingresar un número de documento", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtNumDoc.Focus();
                return false;
            }

            if (txtSufiDoc.Text == "")
            {
                MessageBox.Show("Debe ingresar algún sufijo", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSufiDoc.Focus();
                return false;
            }

            return true;
        }


    }
}
