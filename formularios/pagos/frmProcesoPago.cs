using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using lebrun.clases.compras;
using lebrun.clases.bancos;
using lebrun.formularios.bancos;
using lebrun.clasesData;
using lebrun.clases.contabilidad;
using lebrun.clases.proveedores;
using lebrun.Reportes.compras;
using lebrun.Reportes.MovContab;

namespace lebrun.formularios.compras
{
    public partial class frmProcesoPago : Form
    {
        private DataTable tabla;
        private DataTable tablaB;
        private DataTable tablaChequera;
        private DataTable tablaPago;
        private DataTable tablaMovcontab;
        private DataTable tablaCabacera;
        private Pagos pago;
        private Banco banco;
        private movimientoContable movContab;
        private Compania compania;
        private FuncionesTexbox ftextbox;
        private lbxBancos lbxBanco;
        private Proveedores proveedor;
        private Principal Principal;
        private frmContendorRptCompras reporte;
        private string comprobante = null;
        private string usuario;
        private string codigoProveedor;
        private string nombreProveedor;
        private string mayorProveedor;
        private string auxiliarProveedor;
        private string mayorBanco;
        private string auxiliarBanco;
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
        private decimal montoDocumPagar;
        private decimal montoC2 = 0;
        private decimal montoD2 = 0;
        private frmConteMovContab frmMovContab;
        private string idb;
        private decimal desdeIDB;
        private decimal hastaIDB;
        private decimal PorcIDB;
        private string cuentaIDB;



        public frmProcesoPago()
        {
            InitializeComponent();
        }

        public frmProcesoPago(string codProveedor, string nomProveedor, string usua)
        {
            InitializeComponent();
            codigoProveedor = codProveedor;
            nombreProveedor = nomProveedor;
            usuario = usua;

            pago = new Pagos();
            tabla = new DataTable();
            tablaB = new DataTable();
            banco = new Banco();
            tablaChequera = new DataTable();
            ftextbox = new FuncionesTexbox();
            tablaPago = new DataTable();
            movContab = new movimientoContable();
            compania = new Compania();
            proveedor = new Proveedores();
            tablaCabacera = new DataTable();
            compania = new Compania();

        }

        private void frmProcesoPago_Load(object sender, EventArgs e)
        {
            cargarDocumentos();
            cmbTipoCuenta.SelectedIndex = 0;
            Principal = (Principal)this.MdiParent;
        }
        private void cargarDocumentos()
        {
            //dataGridView1.DataSource = null;
            dataGridView1.Columns.Clear();
            tabla = pago.armarDocumentosProcesoPago(codigoProveedor);

            if (tabla.Rows.Count > 0)
            {
                lblCodigoProveedor.Text = codigoProveedor;
                lblNombreProveedor.Text = nombreProveedor;
                dataGridView1.DataSource = tabla;

                dataGridView1.Columns[2].Width = 200;
                dataGridView1.Columns[3].Width = 110;
                dataGridView1.Columns[4].Width = 110;
                dataGridView1.Columns[5].Width = 200;
                dataGridView1.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].Visible = false;
                dataGridView1.Columns[6].Visible = false;
                dataGridView1.Columns[7].Visible = false;

                dataGridView1.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
                dataGridView1.Columns[3].SortMode = DataGridViewColumnSortMode.NotSortable;
                dataGridView1.Columns[4].SortMode = DataGridViewColumnSortMode.NotSortable;
                dataGridView1.Columns[5].SortMode = DataGridViewColumnSortMode.NotSortable;

                dataGridView1.AllowUserToResizeColumns = false;
                dataGridView1.AllowUserToResizeRows = false;
            }
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentRow.DefaultCellStyle.ForeColor != Color.Red)
            {
                panel1.Visible = true;
                txtMontoCanDocP.Focus();
                txtMontoDocP.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                txtMontoCanDocP.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
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
            sumaDocumAPagar();
            
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

                grid.Rows[fila].Cells[0].Value = "" + Convert.ToDateTime(dataGridView1.CurrentRow.Cells[0].Value.ToString()).Day + "-" + Convert.ToDateTime(dataGridView1.CurrentRow.Cells[0].Value.ToString()).Month.ToString().PadLeft(2, '0') + "-" + Convert.ToDateTime(dataGridView1.CurrentRow.Cells[0].Value.ToString()).Year;
                grid.Rows[fila].Cells[1].Value = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                grid.Rows[fila].Cells[2].Value = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                grid.Rows[fila].Cells[3].Value = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                grid.Rows[fila].Cells[4].Value = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                grid.Rows[fila].Cells[5].Value = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                grid.Rows[fila].Cells[6].Value = "" + Convert.ToDateTime(dataGridView1.CurrentRow.Cells[6].Value.ToString()).Day + "-" + Convert.ToDateTime(dataGridView1.CurrentRow.Cells[6].Value.ToString()).Month.ToString().PadLeft(2, '0') + "-" + Convert.ToDateTime(dataGridView1.CurrentRow.Cells[6].Value.ToString()).Year;
                grid.Rows[fila].Cells[7].Value = dataGridView1.CurrentRow.Cells[7].Value.ToString();
                grid.Rows[fila].Cells[8].Value = txtMontoCanDocP.Text;
                grid.Rows[fila].Cells[9].Value = "" + dataGridView1.CurrentRow.Index;
                grid.Rows[fila].Cells[10].Value = "" + (Convert.ToDecimal(dataGridView1.CurrentRow.Cells[5].Value) - Convert.ToDecimal(txtMontoCanDocP.Text));
                
                if (Convert.ToDecimal(grid.Rows[fila].Cells[10].Value) == 0)
                {
                    grid.Rows[fila].Cells[11].Value = "1";
                }
                if (Convert.ToDecimal(grid.Rows[fila].Cells[10].Value) != 0)
                {
                    grid.Rows[fila].Cells[11].Value = "0";
                }
                grid.Rows[fila].Cells[12].Value = lblCodigoProveedor.Text;
                grid.Rows[fila].Cells[13].Value = lblNombreProveedor.Text;
            }

            dataGridView2.Columns[2].Width = 140;
            dataGridView2.Columns[3].Width = 100;
            dataGridView2.Columns[4].Width = 100;
            dataGridView2.Columns[5].Width = 140;
            dataGridView2.Columns[8].Width = 140;

            dataGridView2.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView2.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            
            dataGridView2.AllowUserToResizeColumns = false;
            dataGridView2.AllowUserToResizeRows = false;
        }

        private void sumaDocumAPagar() {

            montoDocumPagar = 0;

            for (int i = 0; i < dataGridView2.Rows.Count; i++)
            {
                montoDocumPagar = montoDocumPagar + (Convert.ToDecimal(dataGridView2.Rows[i].Cells[8].Value));
            }

            txtTotalDocum.Text = "" + montoDocumPagar;
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dataGridView2.Rows.Count > 0)
            {
                dataGridView1.Rows[Convert.ToInt16(dataGridView2.CurrentRow.Cells[9].Value)].DefaultCellStyle.ForeColor = Color.Black;
                dataGridView2.Rows.RemoveAt(dataGridView2.CurrentRow.Index);
            }
            sumaDocumAPagar();
        }

        private void txtCodigoBanco_Leave(object sender, EventArgs e)
        {
            verificarBanco();
            cargarComboCuenta();
            obtenerCuentasBanco();
        }

        private void cargarComboCuenta() {

            cmbNumeroCuenta.DataSource = null;
            cmbNumeroCuenta.DisplayMember = "ctas_numero";
            cmbNumeroCuenta.ValueMember = "ctas_numero";
            cmbNumeroCuenta.DataSource = banco.obtenerNumerosCuenta(txtCodigoBanco.Text);

        }

        private void verificarBanco() {

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

        private void btnBuscarbanco_Click(object sender, EventArgs e)
        {
            lbxBanco = lbxBancos.DefInstance;
            lbxBanco.destino(this, "procesoPago");
            lbxBanco.Show();
        }

        private void datosChequera() {

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

        private void cmbNumeroCuenta_Leave(object sender, EventArgs e)
        {
            if (txtCodigoBanco.Text != "" && cmbNumeroCuenta.Text != "")
            {
                datosChequera();
                controlChequeras();
            }
        }

        private void controlChequeras() {


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
                    MessageBox.Show("No hay chequeras para disponibles", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void txtMontoCheque_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtMontoCheque.Text = string.Format("{0:0.00}", Convert.ToDecimal(txtMontoCheque.Text));
                txtTotalCheque.Text = txtMontoCheque.Text;
                btnAceptar.Focus();
            }
            else
            {
                ftextbox.conDecimalesDefinidos(sender, e, txtMontoCheque, 2);
            }
        }

        private void txtMontoCheque_Leave(object sender, EventArgs e)
        {
            if (txtMontoCheque.Text == "")
            {
                txtMontoCheque.Text = "0";                
            }            
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (validarMovimiento())
            {
                contenidoTablaPago();
                procesarPago();
                movContab.ingresarMovContab(contenidoMovContab());
                movContab.detallesSyscontab(contenidoMovContab(), compania.BaseDatosActual);
                movContab.cabeceraSyscontab(contenidoCabecera(), compania.BaseDatosActual);
                banco.actualizarUltimoCheque(txtCodigoBanco.Text, cmbNumeroCuenta.Text, chequeraActiva, txtNumeroCheque.Text);
                MessageBox.Show(" Pago efectuado exitosamente ", " Exito ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                reporte = new frmContendorRptCompras(pago.imprimirVoucherPago(txtNumeroCheque.Text), "voucher");
                reporte.Show();
                frmMovContab = new frmConteMovContab(movContab.datosComprobante(comprobante,compania.BaseDatosActual,lblCodigoProveedor.Text));
                frmMovContab.Show();
                this.Close();
            }
        }

        private void procesarPago()
        {
            pago.actualizarProcesoPagoTemp(tablaPago);
            pago.actualizarSaldo(tablaPago);

            for (int i = 0; i < tablaPago.Rows.Count; i++)
            {
                if (Convert.ToDecimal(tablaPago.Rows[i][11].ToString()) == 0)
                {
                    pago.insertarProcesoPagoTemp(tablaPago, i);
                }
                else
                {
                    pago.actualizarStatusSalpro(tablaPago, i);
                }
            }
            pago.insertarProcesoPago(tablaPago);
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
            obtenerComprobanteContable();

            for (int i = 0; i < dataGridView2.Rows.Count; i++)
            {
                DataRow fila = tablaPago.NewRow();

                fila[0] = lblCodigoProveedor.Text;
                fila[1] = dataGridView2.Rows[i].Cells[2].Value.ToString();
                fila[2] = dataGridView2.Rows[i].Cells[3].Value.ToString();
                fila[3] = dataGridView2.Rows[i].Cells[4].Value.ToString();
                fila[4] = lblNombreProveedor.Text;
                fila[5] = "" + DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day;
                //fila[6] = dataGridView2.Rows[i].Cells[8].Value.ToString().Replace(",", ".");
                //fila[7] = txtMontoCheque.Text.Replace(",", ".");

                fila[6] = dataGridView2.Rows[i].Cells[5].Value.ToString().Replace(",", ".");
                fila[7] = dataGridView2.Rows[i].Cells[8].Value.ToString().Replace(",", ".");

                fila[8] = txtCodigoBanco.Text;
                fila[9] = usuario;
                fila[10] = txtNumeroCheque.Text;
                fila[11] = dataGridView2.Rows[i].Cells[11].Value.ToString();
                fila[12] = "" + DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day;
                fila[13] = comprobante;
                fila[14] = cmbNumeroCuenta.Text;
                fila[15] = dataGridView2.Rows[i].Cells[1].Value.ToString();
                fila[16] = Convert.ToDateTime(dataGridView2.Rows[i].Cells[0].Value.ToString()).ToString("yyyy-MM-dd");
                fila[17] = dataGridView2.Rows[i].Cells[7].Value.ToString().Replace(",", ".");
                fila[18] = dataGridView2.Rows[i].Cells[10].Value.ToString();

                tablaPago.Rows.Add(fila);
            }
            return tablaPago;
            
        }

        private void obtenerComprobanteContable()
        {
            tablaB.Reset();
            tablaB.Clear();
            compania.obtenerCodigoCompaniaActual();
            tablaB = movContab.obtenerNumeroComprobante(compania.BaseDatosActual, "05");

            comprobante = tablaB.Rows[0][1].ToString();
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
                txtMontoCheque.Focus();
            }
            else
            {
                e.Handled = true;
            }
        }

        private bool validarMovimiento()
        {

            if (dataGridView2.Rows.Count == 0)
            {
                MessageBox.Show("Debe elegir al menos un documento a procesar ", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            if (txtCodigoBanco.Text == "")
            {
                MessageBox.Show("Debe elegir un codigo de banco ", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            if (cmbNumeroCuenta.Text == "")
            {
                MessageBox.Show("Debe elegir un numero de cuenta ", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            if (txtNumeroCheque.Text == "" || txtNumeroCheque.Text == "0")
            {
                MessageBox.Show("verifique el numero de cheque ", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            if (Convert.ToDecimal(txtTotalCheque.Text) != Convert.ToDecimal(txtTotalDocum.Text))
            {
                MessageBox.Show("El monto total de documentos no es igual al monto del cheque ", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            return true;
        }

        private void txtMontoCanDocP_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox a = (TextBox)sender;
            ftextbox.conDecimalesDefinidosNegativo(sender, e, a, 2);
        }

        private void actualizarStatusDoc() { 
        
        
        
        }

        private DataTable contenidoMovContab()
        {
            tablaMovcontab = movContab.armarDataTableMovContab();
            int items = 0;

            for (int i = 0; i < dataGridView2.RowCount; i++)
            {
                obtenerCuentasPro(dataGridView2.Rows[i].Cells[4].Value.ToString());

                items = items + 1;
                DataRow fila = tablaMovcontab.NewRow();

                fila[0] = items;
                fila[1] = lblCodigoProveedor.Text;
                fila[2] = dataGridView2.Rows[i].Cells[2].Value.ToString(); //recibo
                fila[3] = "0";
                fila[4] = dataGridView2.Rows[i].Cells[4].Value.ToString();
                fila[5] = comprobante;            //COMPROBANTE
                fila[6] = " " + lblNombreProveedor.Text + " " + dataGridView2.Rows[i].Cells[2].Value.ToString();
                fila[7] = mayorProveedor; //mayor proveedor                              //cc.CuentaProveedor;//CODIGO CUENTA

                if (Convert.ToDecimal(dataGridView2.Rows[i].Cells[8].Value) < 0)
                {
                    fila[8] = "-1";
                    fila[20] = "C";
                    montoC2 = montoC2 + Convert.ToDecimal(dataGridView2.Rows[i].Cells[8].Value);
                }
                else
                {
                    fila[8] = "1";
                    fila[20] = "D";
                    montoD2 = montoD2 + Convert.ToDecimal(dataGridView2.Rows[i].Cells[8].Value);
                }

                fila[9] = "";                                   //AQUI NO VA UN COÑO
                fila[10] = "" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString().PadLeft(2, '0') + "-" + DateTime.Now.Day.ToString();
                fila[11] = "" + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString();//me da lala esta parte copy and paste                                          //hora
                fila[12] = dataGridView2.Rows[i].Cells[8].Value.ToString().Replace(",", ".").Replace("-", "");
                fila[13] = "0";
                fila[14] = usuario;
                fila[15] = "CHEP";
                fila[16] = auxiliarProveedor;                                //AUXILIAR  PROVEEDOR                                 
                fila[17] = lblCodigoProveedor.Text;                     //CODIGO
                fila[18] = lblNombreProveedor.Text;               //NOMBRE CLIENTE                     
                fila[19] = "";
                fila[21] = dataGridView2.Rows[i].Cells[2].Value.ToString(); // interno
                fila[22] = ""; // interno

                tablaMovcontab.Rows.Add(fila);
            }

            //BANCO

            if (Convert.ToDecimal(txtMontoCheque.Text) > 0)
            {
                items = items + 1;
                DataRow fila = tablaMovcontab.NewRow();

                fila[0] = items;
                fila[1] = lblCodigoProveedor.Text;
                fila[2] = dataGridView2.Rows[0].Cells[2].Value.ToString(); //recibo
                fila[3] = "0";
                fila[4] = "37";
                fila[5] = comprobante;            //COMPROBANTE
                fila[6] = "CHEQUE " + lblNombreProveedor.Text + " ";
                fila[7] = mayorBanco; //mayor proveedor                         //cc.CuentaProveedor;//CODIGO CUENTA

                fila[8] = "-1";
                fila[20] = "C";
                montoC2 = montoC2 + Convert.ToDecimal(txtMontoCheque.Text);

                fila[9] = "";                                                   //AQUI NO VA UN COÑO
                fila[10] = "" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString().PadLeft(2, '0') + "-" + DateTime.Now.Day.ToString();
                fila[11] = "" + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString();//me da lala esta parte copy and paste                                          //hora
                fila[12] = txtMontoCheque.Text.Replace(",", ".").Replace("-", "");
                fila[13] = "1";
                fila[14] = usuario;
                fila[15] = "CHEP";
                fila[16] = auxiliarBanco;                                       //AUXILIAR  PROVEEDOR                                 
                fila[17] = lblCodigoProveedor.Text;                             //CODIGO
                fila[18] = lblNombreProveedor.Text;                             //NOMBRE CLIENTE                     
                fila[19] = "";
                fila[21] = "";     // interno
                fila[22] = "";

                tablaMovcontab.Rows.Add(fila);
            }


            if (idb == "1" && Convert.ToDecimal(txtMontoCheque.Text) > desdeIDB && Convert.ToDecimal(txtMontoCheque.Text) < hastaIDB)
            {
                items = items + 1;
                DataRow fila = tablaMovcontab.NewRow();

                fila[0] = items;
                fila[1] = lblCodigoProveedor.Text;
                fila[2] = dataGridView2.Rows[0].Cells[2].Value.ToString(); //recibo
                fila[3] = "0";
                fila[4] = "37";
                fila[5] = comprobante;            //COMPROBANTE

                fila[6] = "CHEQUE " + lblNombreProveedor.Text + " ";
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
                fila[14] = usuario;
                fila[15] = "CHEP";
                fila[16] = auxiliarBanco;                                       //AUXILIAR  PROVEEDOR                                 
                fila[17] = lblCodigoProveedor.Text;                             //CODIGO
                fila[18] = lblNombreProveedor.Text;                             //NOMBRE CLIENTE                     
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
                fila[1] = lblCodigoProveedor.Text;
                fila[2] = dataGridView2.Rows[0].Cells[2].Value.ToString(); //recibo
                fila[3] = "0";
                fila[4] = "37";
                fila[5] = comprobante;            //COMPROBANTE
                fila[6] = "CHEQUE " + lblNombreProveedor.Text + " ";
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
                fila[14] = usuario;
                fila[15] = "CHEP";
                fila[16] = "";                                       //AUXILIAR  PROVEEDOR                                 
                fila[17] = lblCodigoProveedor.Text;                             //CODIGO
                fila[18] = lblNombreProveedor.Text;                             //NOMBRE CLIENTE                     
                fila[19] = "";
                fila[21] = "";     // interno
                fila[22] = "";

                tablaMovcontab.Rows.Add(fila);
            }

            return tablaMovcontab;
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

        private void obtenerCuentasPro(string tipoDoc)
        {
            tablaB.Reset();
            tablaB.Clear();
            tablaB = proveedor.obtenerCuentasproveedor(lblCodigoProveedor.Text, tipoDoc);

            if (tablaB.Rows.Count > 0)
            {
                mayorProveedor = tablaB.Rows[0][0].ToString();
                auxiliarProveedor = tablaB.Rows[0][1].ToString();
            }
        }

        private void txtTotalDocum_TextChanged(object sender, EventArgs e)
        {
            txtMontoCheque.Text = txtTotalDocum.Text;
            txtTotalCheque.Text = txtTotalDocum.Text;
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
            fila[3] = Convert.ToString(Math.Abs(Convert.ToDecimal(txtTotalDocum.Text))).Replace(",", ".");
            fila[4] = Convert.ToString(Math.Abs(Convert.ToDecimal(txtTotalDocum.Text))).Replace(",", ".");
            fila[5] = "1";
            fila[6] = usuario;
            fila[7] = usuario;
            fila[8] = compania.BaseDatosActual;
            fila[9] = "CHEP";
            fila[10] = "" + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString();
            fila[11] = "" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString().PadLeft(2, '0') + "-" + DateTime.Now.Day.ToString();
            fila[12] = "CHEQUE " + lblNombreProveedor.Text;

            tablaCabacera.Rows.Add(fila);

            return tablaCabacera;
        }

        private void obtenerCuentaIDB() {

            tablaB.Reset();
            tablaB.Clear();
            tablaB = movContab.cuentaIDB("95");

            if (tablaB.Rows.Count > 0)
            {
                cuentaIDB = tablaB.Rows[0][0].ToString();
            }
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

    }
}
