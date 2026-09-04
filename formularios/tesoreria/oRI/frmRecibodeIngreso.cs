using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//using lebrun.formularios.clientes;
using lebrun.clases.tesoreria;
using lebrun.clasesData;
using lebrun.formularios.contabilidad;
using lebrun.clases.contabilidad;
using lebrun.formularios.facturacion;



namespace lebrun.formularios.tesoreria
{
    public partial class frmRecibodeIngreso : Form
    {
        
        private reciboDeingreso recibo;
        private int centinelaFila = -1;
        private DataTable tabla;
        private ConexionBD database;
        private string codigoVendedor;
        private decimal montoFacturasPagar;
        private decimal montoD;
        private decimal montoC;
        private decimal montoFormaPago;
        public bool guard2;
        private lbxAuxiliaresOnly auxiliar;
        private AuxiliarContable au; 
        private lbxAuxiliarContable cuentas;
        private lbxClientes refClientes;
        private string validaBanco;
        private FuncionesTexbox text;
        private DataTable tablaMovcontab;
        private Compania compania;
        private static frmRecibodeIngreso m_FormDefInstance;
        private string usuario;
        private lbxReciboIngreso refLbxRecibo;
        private lbxPlanCuentas planCuenta;
        private string descripcionAuxiliar;
        private string tipoDoc = "";
        

        private decimal montoD2 = 0;
        private decimal montoC2 = 0;
        private decimal hola = 0;
        private decimal hola2 = 0;
        

        public frmRecibodeIngreso()
        {
            InitializeComponent();
            
            recibo = new reciboDeingreso();
            tabla = new DataTable();
            database = new ConexionBD();
            au = new AuxiliarContable();
            text = new FuncionesTexbox();
            tablaMovcontab = new DataTable();
            compania = new Compania();
            
        }


        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmRecibodeIngreso_Load(object sender, EventArgs e)
        { 
            cargarComboBanco();
            cargarComboCobrador();
            cargarComboCaja();
            compania.obtenerCodigoCompaniaActual();
        }

        private void btnCancMonto_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;     
        }

        private void gridDocumentos_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (gridDocumentos.CurrentRow.DefaultCellStyle.ForeColor != Color.Red)
            {
                panel1.Visible = true;
                txtMontoDocP.Text = gridDocumentos.CurrentRow.Cells[8].Value.ToString();
                txtMontoCanDocP.Text = gridDocumentos.CurrentRow.Cells[8].Value.ToString();
            }
            else
            {
                MessageBox.Show("El documento seleccionado ya ha sido agregado", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information); 
            }
        }

        private void btnAceptarMonto_Click(object sender, EventArgs e)
        {
            if (centinelaFila >= 0)
            {
                agregarFila(centinelaFila,gridFacturasPagar);
                centinelaFila = -1;
            }
            else
            {
                agregarFila(-1, gridFacturasPagar);
            }

            panel1.Visible = false;
            gridDocumentos.CurrentRow.DefaultCellStyle.ForeColor = Color.Red;
            sumaFacturasPagar();

        }

        public void agregarFila(int fila)
        {
            if (fila < 0)
            {
                gridFacturasPagar.Rows.Add();
                fila = gridFacturasPagar.Rows.Count - 1;
            }

            gridFacturasPagar.Rows[fila].Cells[0].Value = gridDocumentos.CurrentRow.Cells[0].Value.ToString();
            gridFacturasPagar.Rows[fila].Cells[1].Value = gridDocumentos.CurrentRow.Cells[1].Value.ToString();
            gridFacturasPagar.Rows[fila].Cells[2].Value = gridDocumentos.CurrentRow.Cells[2].Value.ToString();
            gridFacturasPagar.Rows[fila].Cells[3].Value = gridDocumentos.CurrentRow.Cells[3].Value.ToString();
            gridFacturasPagar.Rows[fila].Cells[4].Value = gridDocumentos.CurrentRow.Cells[4].Value.ToString();
            gridFacturasPagar.Rows[fila].Cells[5].Value = gridDocumentos.CurrentRow.Cells[5].Value.ToString();
            gridFacturasPagar.Rows[fila].Cells[6].Value = gridDocumentos.CurrentRow.Cells[6].Value.ToString();
            gridFacturasPagar.Rows[fila].Cells[7].Value = gridDocumentos.CurrentRow.Cells[7].Value.ToString();
            gridFacturasPagar.Rows[fila].Cells[8].Value = txtMontoCanDocP.Text;
            gridFacturasPagar.Rows[fila].Cells[9].Value = "" + gridDocumentos.CurrentRow.Index;

        }

        private void txtMontoCanDocP_Enter(object sender, EventArgs e)
        {
            txtMontoCanDocP.SelectAll();
            
        }

        private void btnElimfacPagar_Click(object sender, EventArgs e)
        {
            if (gridFacturasPagar.Rows.Count > 0)
            {
                gridDocumentos.Rows[Convert.ToInt16(gridFacturasPagar.CurrentRow.Cells[10].Value)].DefaultCellStyle.ForeColor = Color.Black;
                gridFacturasPagar.Rows.RemoveAt(gridFacturasPagar.CurrentRow.Index);

            }
            sumaFacturasPagar();
        }

        public void agregarFila(int fila, DataGridView grid)
        {

            if (grid.Name.Equals("gridFacturasPagar"))
            {
                if (fila < 0)
                {
                    grid.Rows.Add();
                    fila = grid.Rows.Count - 1;
                }

                grid.Rows[fila].Cells[0].Value = gridDocumentos.CurrentRow.Cells[0].Value.ToString();
                grid.Rows[fila].Cells[1].Value = gridDocumentos.CurrentRow.Cells[1].Value.ToString();
                grid.Rows[fila].Cells[2].Value = gridDocumentos.CurrentRow.Cells[2].Value.ToString();
                grid.Rows[fila].Cells[3].Value = gridDocumentos.CurrentRow.Cells[3].Value.ToString();
                grid.Rows[fila].Cells[4].Value = "" + Convert.ToDateTime(gridDocumentos.CurrentRow.Cells[4].Value.ToString()).Day + "-" + Convert.ToDateTime(gridDocumentos.CurrentRow.Cells[4].Value.ToString()).Month.ToString().PadLeft(2,'0') + "-" + Convert.ToDateTime(gridDocumentos.CurrentRow.Cells[4].Value.ToString()).Year;
                grid.Rows[fila].Cells[5].Value = "" + Convert.ToDateTime(gridDocumentos.CurrentRow.Cells[5].Value.ToString()).Day + "-" + Convert.ToDateTime(gridDocumentos.CurrentRow.Cells[5].Value.ToString()).Month.ToString().PadLeft(2, '0') + "-" + Convert.ToDateTime(gridDocumentos.CurrentRow.Cells[5].Value.ToString()).Year;
                grid.Rows[fila].Cells[6].Value = gridDocumentos.CurrentRow.Cells[6].Value.ToString();
                grid.Rows[fila].Cells[7].Value = gridDocumentos.CurrentRow.Cells[7].Value.ToString();
                grid.Rows[fila].Cells[8].Value = gridDocumentos.CurrentRow.Cells[8].Value.ToString();
                grid.Rows[fila].Cells[9].Value = txtMontoCanDocP.Text;
                grid.Rows[fila].Cells[10].Value = "" + gridDocumentos.CurrentRow.Index;
                grid.Rows[fila].Cells[11].Value = (Convert.ToDecimal(gridDocumentos.CurrentRow.Cells[8].Value.ToString())) - (Convert.ToDecimal(txtMontoCanDocP.Text));
                grid.Rows[fila].Cells[12].Value = gridDocumentos.CurrentRow.Cells[1].Value.ToString();
                grid.Rows[fila].Cells[13].Value = Convert.ToString(Convert.ToDateTime(gridDocumentos.CurrentRow.Cells[4].Value.ToString()).Year + "-" + Convert.ToString(Convert.ToDateTime(gridDocumentos.CurrentRow.Cells[4].Value.ToString()).Month).PadLeft(2,'0') + "-" + Convert.ToDateTime(gridDocumentos.CurrentRow.Cells[4].Value.ToString()).Day);
                grid.Rows[fila].Cells[14].Value = Convert.ToString(Convert.ToDateTime(gridDocumentos.CurrentRow.Cells[5].Value.ToString()).Year + "-" + Convert.ToString(Convert.ToDateTime(gridDocumentos.CurrentRow.Cells[5].Value.ToString()).Month).PadLeft(2,'0') + "-" + Convert.ToDateTime(gridDocumentos.CurrentRow.Cells[5].Value.ToString()).Day);
                grid.Rows[fila].Cells[15].Value = gridDocumentos.CurrentRow.Cells[6].Value.ToString();
                grid.Rows[fila].Cells[16].Value = gridDocumentos.CurrentRow.Cells[7].Value.ToString();
                grid.Rows[fila].Cells[17].Value = gridDocumentos.CurrentRow.Cells[8].Value.ToString();
                grid.Rows[fila].Cells[18].Value = txtMontoCanDocP.Text;

                if (Convert.ToDecimal(grid.Rows[fila].Cells[11].Value) == 0)
                {
                    grid.Rows[fila].Cells[19].Value = "Pagado";
                    grid.Rows[fila].Cells[20].Value = "1";
                }
                else
                {
                    grid.Rows[fila].Cells[19].Value = "Activo";
                    grid.Rows[fila].Cells[20].Value = "0";
                }


                grid.Columns[10].Visible = false;
                grid.Columns[11].Visible = false;
                grid.Columns[12].Visible = false;
                grid.Columns[13].Visible = false;
                grid.Columns[14].Visible = false;
                grid.Columns[15].Visible = false;
                grid.Columns[16].Visible = false;
                grid.Columns[17].Visible = false;
                grid.Columns[18].Visible = false;
                grid.Columns[19].Visible = false;
                grid.Columns[20].Visible = false;

                grid.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                grid.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                grid.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                grid.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                grid.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                grid.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            }

            if (grid.Name.Equals("gridFormaPago"))
            {
                if (fila < 0)
                {
                    grid.Rows.Add();
                    fila = grid.Rows.Count - 1;
                }

                grid.Rows[fila].Cells[0].Value = cmbFormaPago.Text;
                
                if (cmbFormaPago.Text != "Efectivo")
                {
                    grid.Rows[fila].Cells[1].Value = cmbBanco.Text;
                    grid.Rows[fila].Cells[2].Value = txtNumeroCuenta.Text;
                    grid.Rows[fila].Cells[3].Value = txtNumero.Text;
                    grid.Rows[fila].Cells[6].Value = "" + cmbBanco.SelectedValue.ToString();
                }
                else
                {

                    grid.Rows[fila].Cells[1].Value = "";
                    grid.Rows[fila].Cells[2].Value = "";
                    grid.Rows[fila].Cells[3].Value = "";
                    grid.Rows[fila].Cells[6].Value = "";
                }
                grid.Rows[fila].Cells[4].Value = fechaPago.Value.Year + "-" + fechaPago.Value.Month.ToString().PadLeft(2,'0') + "-" + fechaPago.Value.Day;
                grid.Rows[fila].Cells[5].Value = txtMontoFP.Text;
                //grid.Rows[fila].Cells[6].Value = "" + cmbBanco.SelectedValue.ToString();
                grid.Rows[fila].Cells[7].Value = cmbFormaPago.Text;

                limpiarFormaPago();
            }

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

        private void btnAgregarFP_Click(object sender, EventArgs e)
        {
            if (validacionFormPago())
            {
                if (centinelaFila >= 0)
                {
                    agregarFila(centinelaFila, gridFormaPago);
                    centinelaFila = -1;
                }
                else
                {
                    agregarFila(-1, gridFormaPago);
                }
                sumaFormaPagoFacturas();
            }
        }

        private void btnEliminarFP_Click(object sender, EventArgs e)
        {
            if (gridFormaPago.Rows.Count > 0)
            {

                gridFormaPago.Rows.RemoveAt(gridFormaPago.CurrentRow.Index);

            }
            sumaFormaPagoFacturas();
        }

        private void btnAgregarMC_Click(object sender, EventArgs e)
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

        private void btnEliminarMC_Click(object sender, EventArgs e)
        {
            if (gridMovContab.Rows.Count > 0)
            {
                gridMovContab.Rows.RemoveAt(gridMovContab.CurrentRow.Index);
            }
            sumaMovimientoContable();
        }

        public void cargarComboBanco()
        {
            cmbBanco.DataSource = null;
            tabla = database.fDataTable("SELECT ban_nombre,ban_codigo FROM admbancos WHERE ban_tel3='Activo' ORDER BY ban_nombre ASC");
            cmbBanco.DisplayMember = "ban_nombre";
            cmbBanco.ValueMember = "ban_codigo";
            cmbBanco.DataSource = tabla;
        }

        public void cargarComboCobrador()
        {
            cmbCobrador.DataSource = null;
            tabla = database.fDataTable("SELECT tra_nombre, tra_codigo FROM admtrabajador where tra_cargo='VENDEDOR' AND tra_estatus='Activo'");
            cmbCobrador.DisplayMember = "tra_nombre";
            cmbCobrador.ValueMember = "tra_codigo";
            cmbCobrador.DataSource = tabla;
        }

        public void cargarComboCaja()
        {
            cmbCaja.DataSource = null;
            tabla = database.fDataTable("SELECT ctascaj_codigo FROM admcuentascaja");
            cmbCaja.DisplayMember = "ctascaj_codigo";
            cmbCaja.DataSource = tabla;
        }



        private void cmbTipo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;

        }

        private void cmbFormaPago_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
            if (e.KeyChar == 13 && cmbFormaPago.Text.Equals("Efectivo"))
            {
                txtMontoFP.Focus();
                cmbBanco.Enabled=false;
                txtNumeroCuenta.Enabled=false;
                txtNumero.Enabled = false;
            }
            else if (e.KeyChar == 13 && cmbFormaPago.Text != "Efectivo")
            {
                txtNumeroCuenta.Focus();
                cmbBanco.Enabled = true;
                txtNumeroCuenta.Enabled = true;
                txtNumero.Enabled = true;
            }
        }

        private void cmbBanco_KeyPress(object sender, KeyPressEventArgs e)
        {
            //e.Handled = true;
        }

        private void cmbCobrador_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void cmbCobrador_Leave(object sender, EventArgs e)
        {
            codigoVendedor = "" + cmbCobrador.SelectedValue;
        }

        public void sumaFacturasPagar() {

            montoFacturasPagar = 0;

            for (int i = 0; i < gridFacturasPagar.Rows.Count; i++)
            {
                montoFacturasPagar = montoFacturasPagar + (Convert.ToDecimal(gridFacturasPagar.Rows[i].Cells[9].Value));
            }

            txtMontoDocuT.Text = "" + montoFacturasPagar;
            txtMontoFacturas.Text = "" + montoFacturasPagar;
        }

        public void sumaFormaPagoFacturas()
        {
            montoFormaPago = 0;
            for (int i = 0; i < gridFormaPago.Rows.Count; i++)
            {
                montoFormaPago = montoFormaPago + (Convert.ToDecimal(gridFormaPago.Rows[i].Cells[5].Value));
            }

            txtMontoPagoT.Text = "" + montoFormaPago;

        }

        public void sumaFormaPagoAnticipo()
        {
            montoFormaPago = 0;
            for (int i = 0; i < gridFormaPago.Rows.Count; i++)
            {
                montoFormaPago = montoFormaPago + (Convert.ToDecimal(gridFormaPago.Rows[i].Cells[5].Value));
            }

            txtMontoPagoT.Text = "" + montoFormaPago;

        }

        public void sumaFormaPagoCruce()
        {
            for (int i = 0; i < gridFormaPago.Rows.Count; i++)
            {


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
                    montoD=(montoD+Convert.ToDecimal(gridMovContab.Rows[i].Cells[4].Value));
                }
                if (gridMovContab.Rows[i].Cells[3].Value.ToString() == "C")
                {
                    montoC = (montoC + Convert.ToDecimal(gridMovContab.Rows[i].Cells[5].Value));
                }
                        
            }
            txtDebito.Text = "" + montoD;
            txtCredito.Text = "" + montoC;

        }

        private void cmbTipo_Leave(object sender, EventArgs e)
        {
            if (cmbTipo.Text == "Anticipo")
            {
                if (gridFacturasPagar.Rows.Count > 0)
                {
                    gridFacturasPagar.Rows.Clear();
                    sumaFacturasPagar();

                    for (int i = 0; i < gridDocumentos.Rows.Count; i++)
                    {
                        gridDocumentos.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                    }
                    gridDocumentos.Enabled = false;
                }
            }
            else {

                gridDocumentos.Enabled = true;
            }
        }

        private void btnBuscarCC_Click(object sender, EventArgs e)
        {
            //guard2 = false;
            //cuentas = lbxAuxiliarContable.DefInstance;
            //cuentas.soloVista(this, this.Name);
            //cuentas.Show();

            planCuenta = lbxPlanCuentas.DefInstance;
            planCuenta.soloVista(this, this.Name, compania.BaseDatosActual);
            planCuenta.Show();
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

        private void txtCuenta_Leave(object sender, EventArgs e)
        {
            buscarVerificarPlanC();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {

            refClientes = lbxClientes.DefInstance;
            refClientes.destino(this, 4);
            refClientes.Show();
            
        }

        private void txtCliente_TextChanged(object sender, EventArgs e)
        {
            gridDocumentos.Columns.Clear();
            gridFacturasPagar.Rows.Clear();
            sumaFacturasPagar();

            tabla = recibo.cargarDocumentos(txtCliente.Text);

                gridDocumentos.DataSource = tabla;
                //gridDocumentos.DataSource = recibo.cargarDocumentos(txtCliente.Text);
                //lblNombreCliente.Text = tabla.Rows[0][1].ToString();

                gridDocumentos.Columns[0].Width = 55;
                gridDocumentos.Columns[9].Visible = false;
                gridDocumentos.Columns[10].Visible = false;

                if (gridDocumentos.Rows.Count > 1)
                {
                    gridDocumentos.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    gridDocumentos.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    gridDocumentos.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    gridDocumentos.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    gridDocumentos.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    gridDocumentos.Columns[10].Visible = false;
                    lblNombreCliente.Text = tabla.Rows[0][9].ToString();
                }
                else
                {

                    lblNombreCliente.Text = "...";
                }
            

        }

        private void txtCliente_Leave(object sender, EventArgs e)
        {
            gridDocumentos.Columns.Clear();
            gridDocumentos.DataSource = recibo.cargarDocumentos(txtCliente.Text);
            gridDocumentos.Columns[0].Width = 55;

            gridDocumentos.Columns[9].Visible = false;
            gridDocumentos.Columns[10].Visible = false;

            if (gridDocumentos.Rows.Count > 1)
            {
                gridDocumentos.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                gridDocumentos.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                gridDocumentos.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                gridDocumentos.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                gridDocumentos.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                gridDocumentos.Columns[10].Visible = false;
            }

            if (txtCliente.Text != "") {
                txtCliente.Text = txtCliente.Text.ToUpper();
            }
        }

        private void txtCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (e.KeyChar == 13) {
            //    gridDocumentos.Columns.Clear();
            //    gridDocumentos.DataSource = recibo.cargarDocumentos(txtCliente.Text);
            //    gridDocumentos.Columns[0].Width = 55;
            //}
        }



        private void asignandoValores() {

            recibo.TipoPago = cmbTipo.Text;
            recibo.Caja = cmbCaja.Text;
            recibo.CodCliente = txtCliente.Text;
            recibo.FechaPago = fechaPago.Value.Year + "-" + fechaPago.Value.Month.ToString().PadLeft(2,'0') + "-" + fechaPago.Value.Day;
            recibo.Estado = "Activo";
            recibo.Comprobante = "";
            recibo.Cencos = "";
            recibo.Sucursal = "000001";
            recibo.Hora = DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second;
            recibo.DocAso = "";
            recibo.TipDocAso = "";
            recibo.Cobrador = "" + cmbCobrador.SelectedValue;
            recibo.Monto = txtMontoPagoT.Text.Replace(",",".");
            recibo.MontoIva = "0.00";
            recibo.Divisa = "0";
            recibo.ValCamb = "0";
            recibo.Memo = lblNombreCliente.Text;
            recibo.NombreCliente = lblNombreCliente.Text;
            recibo.Usuario = usuario;
            recibo.NumeroReciboCobro = nReciboCobro.Text;

            recibo.asignarNumeroCorrelativo("REI");
            txtNumeroRecibo.Text = recibo.Correlativo;
            recibo.asignarNumeroComprobante(compania.BaseDatosCompania);
            //txtNumeroComprobante.Text = recibo.Comprobante.PadLeft(12, '0');
            txtNumeroComprobante.Text = recibo.Comprobante;
            recibo.obtenerCuentaCliente();
            recibo.buscarCuentaDoc("ANT");
            
        }

        private void cmbBanco_Leave(object sender, EventArgs e)
        {
            validaBanco = "" + cmbBanco.SelectedValue;

            if (validaBanco == "")
            {
                cmbBanco.DataSource = null;
                cargarComboBanco();
                MessageBox.Show("No existe un banco con ese nombre", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void reemplazando() {

            if (gridFacturasPagar.Rows.Count > 0)
            {

                for (int i = 0; i < gridFacturasPagar.Rows.Count; i++)
                {
                    gridFacturasPagar.Rows[i].Cells[6].Value = gridFacturasPagar.Rows[i].Cells[6].Value.ToString().Replace(",", ".");
                    gridFacturasPagar.Rows[i].Cells[7].Value = gridFacturasPagar.Rows[i].Cells[7].Value.ToString().Replace(",", ".");
                    gridFacturasPagar.Rows[i].Cells[8].Value = gridFacturasPagar.Rows[i].Cells[8].Value.ToString().Replace(",", ".");
                    gridFacturasPagar.Rows[i].Cells[9].Value = gridFacturasPagar.Rows[i].Cells[9].Value.ToString().Replace(",", ".");
                    gridFacturasPagar.Rows[i].Cells[10].Value = gridFacturasPagar.Rows[i].Cells[10].Value.ToString().Replace(",", ".");
                    gridFacturasPagar.Rows[i].Cells[11].Value = gridFacturasPagar.Rows[i].Cells[11].Value.ToString().Replace(",", ".");
                    gridFacturasPagar.Rows[i].Cells[12].Value = gridFacturasPagar.Rows[i].Cells[12].Value.ToString().Replace(",", ".");
                    gridFacturasPagar.Rows[i].Cells[13].Value = gridFacturasPagar.Rows[i].Cells[13].Value.ToString().Replace(",", ".");
                    gridFacturasPagar.Rows[i].Cells[14].Value = gridFacturasPagar.Rows[i].Cells[14].Value.ToString().Replace(",", ".");
                    gridFacturasPagar.Rows[i].Cells[15].Value = gridFacturasPagar.Rows[i].Cells[15].Value.ToString().Replace(",", ".");
                    gridFacturasPagar.Rows[i].Cells[16].Value = gridFacturasPagar.Rows[i].Cells[16].Value.ToString().Replace(",", ".");
                    gridFacturasPagar.Rows[i].Cells[17].Value = gridFacturasPagar.Rows[i].Cells[18].Value.ToString().Replace(",", ".");
                    gridFacturasPagar.Rows[i].Cells[18].Value = gridFacturasPagar.Rows[i].Cells[18].Value.ToString().Replace(",", ".");
                    gridFacturasPagar.Rows[i].Cells[13].Value = Convert.ToString(Convert.ToDateTime(gridFacturasPagar.Rows[i].Cells[13].Value.ToString()).Year + "-" + Convert.ToString(Convert.ToDateTime(gridFacturasPagar.Rows[i].Cells[13].Value.ToString()).Month).PadLeft(2, '0') + "-" + Convert.ToDateTime(gridFacturasPagar.Rows[i].Cells[13].Value.ToString()).Day);
                    gridFacturasPagar.Rows[i].Cells[14].Value = Convert.ToString(Convert.ToDateTime(gridFacturasPagar.Rows[i].Cells[14].Value.ToString()).Year + "-" + Convert.ToString(Convert.ToDateTime(gridFacturasPagar.Rows[i].Cells[14].Value.ToString()).Month).PadLeft(2, '0') + "-" + Convert.ToDateTime(gridFacturasPagar.Rows[i].Cells[14].Value.ToString()).Day);
                    gridFacturasPagar.Rows[i].Cells[4].Value = Convert.ToString(Convert.ToDateTime(gridFacturasPagar.Rows[i].Cells[4].Value.ToString()).Year + "-" + Convert.ToString(Convert.ToDateTime(gridFacturasPagar.Rows[i].Cells[4].Value.ToString()).Month).PadLeft(2, '0') + "-" + Convert.ToDateTime(gridFacturasPagar.Rows[i].Cells[4].Value.ToString()).Day);
                    gridFacturasPagar.Rows[i].Cells[5].Value = Convert.ToString(Convert.ToDateTime(gridFacturasPagar.Rows[i].Cells[5].Value.ToString()).Year + "-" + Convert.ToString(Convert.ToDateTime(gridFacturasPagar.Rows[i].Cells[5].Value.ToString()).Month).PadLeft(2, '0') + "-" + Convert.ToDateTime(gridFacturasPagar.Rows[i].Cells[5].Value.ToString()).Day);
                    
                }

            }

            if (gridFormaPago.Rows.Count > 0)
            {

                for (int i = 0; i < gridFormaPago.Rows.Count; i++)
                {
                    gridFormaPago.Rows[i].Cells[5].Value = gridFormaPago.Rows[i].Cells[5].Value.ToString().Replace(",", ".");

                }

            }

            if (gridMovContab.Rows.Count > 0)
            {

                for (int i = 0; i < gridMovContab.Rows.Count; i++)
                {
                    //gridMovContab.Rows[i].Cells[5].Value=gridMovContab.Rows[i].Cells[5].Value.ToString().Replace(",", ".");
                    //gridMovContab.Rows[i].Cells[6].Value=gridMovContab.Rows[i].Cells[6].Value.ToString().Replace(",", ".");

                }

            }
        }

        private bool validacionMovContab() {

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
                MessageBox.Show("Agregue una descripcion a este movimiento", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDescripcion.Focus();
                return false;
            }
            else if (txtMontoMC.Text == "")
            {
                MessageBox.Show("Debe ingresar un monto para este movimiento", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDescripcion.Focus();
                return false;
            }
            else
            {
                return true;
            }
        
        }

        private bool validacionFormPago() {


            if (cmbFormaPago.Text == "Efectivo")
            {
                if (txtMontoFP.Text == "")
                {

                    MessageBox.Show("Debe ingresar un monto a pagar", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtMontoFP.Focus();
                    return false;
                }
                return true;

            }
            else if (cmbFormaPago.Text == "Transferencia Bancaria")
            {

                if (txtMontoFP.Text == "")
                {

                    MessageBox.Show("Debe ingresar un monto a pagar", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtMontoFP.Focus();
                    return false;
                }

                if (txtNumeroCuenta.Text == "") 
                {
                    MessageBox.Show("Debe ingresar un numero de cuenta", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtNumeroCuenta.Focus();
                    return false;
                }

                //if (txtNumeroCuenta.Text.Length < 20)
                //{
                //    MessageBox.Show("El numero de cuenta no debe ser menor de 20 digitos", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    txtNumeroCuenta.Focus();
                //    return false;
                //}
                return true;

            }
            else if (cmbFormaPago.Text == "Cheque")
            {
                if (txtMontoFP.Text == "")
                {

                    MessageBox.Show("Debe ingresar un monto a pagar", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtMontoFP.Focus();
                    return false;
                }
                if (txtNumeroCuenta.Text == "")
                {
                    MessageBox.Show("Debe ingresar un numero de cuenta", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtNumeroCuenta.Focus();
                    return false;
                }
                

                //if (txtNumeroCuenta.Text.Length < 20)
                //{
                //    MessageBox.Show("El numero de cuenta no debe ser menor de 20 digitos", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    txtNumeroCuenta.Focus();
                //    return false;
                //}

                if (txtNumero.Text == "")
                {
                    MessageBox.Show("Debe ingresar el numero de cheque", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtNumero.Focus();
                    return false;
                }

                //if (txtNumero.Text.Length < 8)
                //{
                //    MessageBox.Show("El numero del cheque no debe ser menor de 8 digitos", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    txtNumero.Focus();
                //    return false;
                //}

                return true;
            }
            else
            {
                MessageBox.Show("Debe Seleccionar una forma de pago", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            //un pago puede cerrar con montos en cero   09:08 a.m. 15/08/2013
            

            //if (Convert.ToDecimal(txtMontoPagoT.Text) != 0 || cmbTipo.Text.Equals("Cruce"))
            //{
                
                if (cmbTipo.Text.Equals("Anticipo"))
                {

                    if ((Convert.ToDecimal(txtMontoPagoT.Text)) != (Convert.ToDecimal(txtDebito.Text) - Convert.ToDecimal(txtCredito.Text)))
                    {
                        MessageBox.Show("El monto de pago no es igual al monto del movimiento contable", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    else
                    {

                        asignandoValores();
                        reemplazando();
                        recibo.reciboIngreso();                             //cabecera
                        recibo.insert_recibo_ingreso_det(gridFormaPago);    //forma de pago
                        recibo.alimentarReciboDocAnt(gridFormaPago);        //Documentos afectados
                        recibo.ingresarAnticipoSalcli(txtMontoPagoT.Text.Replace(",", "."));       //si es ANT se crea nuevo en salcli

                        
                        armarDataTableMovContab();
                        contenidoMovContab();
                        recibo.guardarMovContab(tablaMovcontab);

                        var D = tablaMovcontab.AsEnumerable().Sum(c => c.Field<string>("islr").Equals("D") ? Convert.ToDecimal(c.Field<string>("monto").Replace(".", ",")) : 0);
                        var C = tablaMovcontab.AsEnumerable().Sum(c => c.Field<string>("islr").Equals("C") ? Convert.ToDecimal(c.Field<string>("monto").Replace(".", ",")) : 0);

                        recibo.syscontabCabecera(D.ToString().Replace(",", "."), C.ToString().Replace(",", "."), compania.BaseDatosCompania);
                        recibo.syscontaDetalles(tablaMovcontab, compania.BaseDatosCompania);

                        MessageBox.Show("Recibo de ingreso numero " + txtNumeroRecibo.Text + " registrado  ", "Hecho ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        refLbxRecibo.refrescar(new object(), new EventArgs());
                        this.Close();

                    }

                }
                else if (cmbTipo.Text.Equals("Pago"))
                {

                    //if (Convert.ToDecimal(txtMontoDocuT.Text) != 0)
                    //{
                    if (gridFacturasPagar.Rows.Count > 0)
                    {

                        if ((Convert.ToDecimal(txtMontoPagoT.Text)) != (Convert.ToDecimal(txtDebito.Text) - Convert.ToDecimal(txtCredito.Text)))
                        {
                            MessageBox.Show("El monto de pago no es igual al monto del movimiento contable", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }
                        else if (Convert.ToDecimal(txtMontoDocuT.Text) != Convert.ToDecimal(txtMontoPagoT.Text))
                        {

                            MessageBox.Show("El monto total de los documentos y el monto total de pago no coinciden", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }
                        else if (gridFormaPago.Rows.Count == 0)
                        {

                            MessageBox.Show("Debe Ingresar una forma de pago", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }
                        else
                        {


                            asignandoValores();
                            reemplazando();
                            recibo.reciboIngreso();                             //cabecera
                            recibo.insert_recibo_ingreso_det(gridFormaPago);    //forma de pago
                            recibo.alimentarReciboDoc2(gridFacturasPagar);      //Documentos afectados
                            recibo.actualizarReciboSalcli(gridFacturasPagar);   //actualizo el saldo

                            armarDataTableMovContab();
                            contenidoMovContab();

                            var D = tablaMovcontab.AsEnumerable().Sum(c => c.Field<string>("islr").Equals("D") ? Convert.ToDecimal(c.Field<string>("monto").Replace(".", ",")) : 0);
                            var C = tablaMovcontab.AsEnumerable().Sum(c => c.Field<string>("islr").Equals("C") ? Convert.ToDecimal(c.Field<string>("monto").Replace(".", ",")) : 0);

                            recibo.guardarMovContab(tablaMovcontab);
                            recibo.syscontabCabecera(D.ToString().Replace(",", "."), C.ToString().Replace(",", "."), compania.BaseDatosCompania);
                            recibo.syscontaDetalles(tablaMovcontab, compania.BaseDatosCompania);
                            recibo.habilitarCliente(txtCliente.Text);
                            recibo.suspenderCLientes(txtCliente.Text);

                            MessageBox.Show("Recibo de ingreso numero " + txtNumeroRecibo.Text + " registrado  ", "Hecho ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            refLbxRecibo.refrescar(new object(), new EventArgs());
                            this.Close();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Debe Alegir al menos un documento a procesar", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else if (cmbTipo.Text.Equals("Cruce"))
                {


                    //gridFacturasPagar
                    //if (Convert.ToDecimal(txtMontoDocuT.Text) != 0)
                    //{
                    if (gridFacturasPagar.Rows.Count> 0)
                    {

                        if ((Convert.ToDecimal(txtMontoPagoT.Text)) != (Convert.ToDecimal(txtDebito.Text) - Convert.ToDecimal(txtCredito.Text)))
                        {
                            MessageBox.Show("El monto de pago no es igual al monto del movimiento contable", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }
                        else if (Convert.ToDecimal(txtMontoDocuT.Text) != Convert.ToDecimal(txtMontoPagoT.Text))
                        {

                            MessageBox.Show("El monto total de los documentos y el monto total de pago no coinciden", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }
                        else if (gridFormaPago.Rows.Count == 0)
                        {

                            MessageBox.Show("Debe Ingresar una forma de pago", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }
                        else
                        {

                            asignandoValores();
                            reemplazando();
                            recibo.reciboIngreso();                             //cabecera
                            recibo.insert_recibo_ingreso_det(gridFormaPago);  //forma de pago
                            recibo.alimentarReciboDoc2(gridFacturasPagar);      //documentos afectados
                            recibo.actualizarReciboSalcli(gridFacturasPagar);   //actualizo el saldo

                            armarDataTableMovContab();
                            contenidoMovContab();

                            var D = tablaMovcontab.AsEnumerable().Sum(c => c.Field<string>("islr").Equals("D") ? Convert.ToDecimal(c.Field<string>("monto").Replace(".", ",")) : 0);
                            var C = tablaMovcontab.AsEnumerable().Sum(c => c.Field<string>("islr").Equals("C") ? Convert.ToDecimal(c.Field<string>("monto").Replace(".", ",")) : 0);

                            recibo.guardarMovContab(tablaMovcontab);
                            recibo.syscontabCabecera(D.ToString().Replace(",", "."), C.ToString().Replace(",", "."), compania.BaseDatosCompania);
                            recibo.syscontaDetalles(tablaMovcontab, compania.BaseDatosCompania);
                            
                            recibo.habilitarCliente(txtCliente.Text);
                            recibo.suspenderCLientes(txtCliente.Text);

                            MessageBox.Show("Recibo de ingreso numero " + txtNumeroRecibo.Text + " registrado  ", "Hecho ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            refLbxRecibo.refrescar(new object(), new EventArgs());
                            this.Close();
                        }

                    }
                    else
                    {
                        MessageBox.Show("Debe Alegir al menos un documento a procesar", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }


                }
                else
                {
                    MessageBox.Show("Debe seleccionar algun tipo de transaccion", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }


            //}
            //else
            //{
            //    MessageBox.Show("Debe Existir algun monto a pagar", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}

        }

        private void cmbTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTipo.Text == "Anticipo")
            {
                if (gridFacturasPagar.Rows.Count > 0)
                {
                    gridFacturasPagar.Rows.Clear();
                    sumaFacturasPagar();

                    for (int i = 0; i < gridDocumentos.Rows.Count; i++)
                    {
                        gridDocumentos.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                    }
                    
                }
                gridDocumentos.Enabled = false;
            }
            else
            {

                gridDocumentos.Enabled = true;
            }
        }

        private void txtMontoCanDocP_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox a = (TextBox)sender;
            text.conDecimalesDefinidosNegativo(sender, e, a, 2);

        }

        private void txtMontoCanDocP_MouseUp(object sender, MouseEventArgs e)
        {
            txtMontoCanDocP.SelectAll();
        }

        private void armarDataTableMovContab()
        {
            tablaMovcontab.Reset();
            tablaMovcontab.Clear();

            tablaMovcontab.Columns.Add("item");              //0
            tablaMovcontab.Columns.Add("codprove");          //1
            tablaMovcontab.Columns.Add("numeroDoc");         //2
            tablaMovcontab.Columns.Add("sufiDoc");           //3
            tablaMovcontab.Columns.Add("tipoDoc");           //4
            tablaMovcontab.Columns.Add("numComprobante");    //5
            tablaMovcontab.Columns.Add("descripcion");       //6
            tablaMovcontab.Columns.Add("cuenta");            //7
            tablaMovcontab.Columns.Add("tipo");              //8
            tablaMovcontab.Columns.Add("baseTipo");          //9
            tablaMovcontab.Columns.Add("fecha");             //10
            tablaMovcontab.Columns.Add("hora");              //11
            tablaMovcontab.Columns.Add("monto");             //12
            tablaMovcontab.Columns.Add("status");            //13
            tablaMovcontab.Columns.Add("login");             //14
            tablaMovcontab.Columns.Add("idSistemas");        //15
            tablaMovcontab.Columns.Add("auxiliar");          //16
            tablaMovcontab.Columns.Add("rif");               //17
            tablaMovcontab.Columns.Add("nombre");            //18
            tablaMovcontab.Columns.Add("tipoLetra");         //19
            tablaMovcontab.Columns.Add("islr");              //20
            tablaMovcontab.Columns.Add("interno");           //21
            tablaMovcontab.Columns.Add("referencia");        //22
        }

            

        public DataTable contenidoMovContab()
        {

            int items = 0;
            
            //parte documentos

            for (int i = 0; i < gridFacturasPagar.RowCount;i++ )
            {

                if (gridFacturasPagar.Rows[i].Cells[0].Value.ToString().Equals("FAV"))
                {
                    tipoDoc = "FACT";
                }
                else if (gridFacturasPagar.Rows[i].Cells[0].Value.ToString().Equals("DEV"))
                {
                    tipoDoc = "N/C";
                }
                else
                {
                    tipoDoc = gridFacturasPagar.Rows[i].Cells[0].Value.ToString();
                }

                items = items + 1;
                DataRow fila = tablaMovcontab.NewRow();


                fila[0] = items;
                fila[1] = txtCliente.Text;
                fila[2] = txtNumeroRecibo.Text;
                fila[3] = "0";
                fila[4] = "12";
                fila[5] = txtNumeroComprobante.Text;               //COMPROBANTE
                fila[6] = lblNombreCliente.Text + " " + tipoDoc + " " + gridFacturasPagar.Rows[i].Cells[2].Value.ToString();
                fila[7] = recibo.CuentaCliente;                               //cc.CuentaProveedor;//CODIGO CUENTA

                if (gridFacturasPagar.Rows[i].Cells[0].Value.ToString().Equals("FAV"))
                {

                    if (Convert.ToDecimal(gridFacturasPagar.Rows[i].Cells[9].Value) > 0)
                    {
                        fila[8] = "-1";
                        fila[20] = "C";
                        montoC2 = montoC2 + (Convert.ToDecimal(gridFacturasPagar.Rows[i].Cells[9].Value));
                        hola = hola + (Convert.ToDecimal(gridFacturasPagar.Rows[i].Cells[9].Value));
            
                    }
                    else
                    {
                        fila[8] = "1";
                        fila[20] = "D";
                        montoD2 = montoD2 + Convert.ToDecimal(gridFacturasPagar.Rows[i].Cells[9].Value);

                    }
                }
                else
                {
                    //fila[8] = "1"; //modificado a raiz del error en cruce de yolanda 10-10-2013
                    //fila[20] = "D";
                    //montoD2 = montoD2 + Convert.ToDecimal(gridFacturasPagar.Rows[i].Cells[9].Value);

                    if (Convert.ToDecimal(gridFacturasPagar.Rows[i].Cells[9].Value) > 0)
                    {
                        fila[8] = "-1";
                        fila[20] = "C";
                        montoC2 = montoC2 + Convert.ToDecimal(gridFacturasPagar.Rows[i].Cells[9].Value);
                    }
                    else
                    {
                        fila[8] = "1";
                        fila[20] = "D";
                        montoD2 = montoD2 + Convert.ToDecimal(gridFacturasPagar.Rows[i].Cells[9].Value);

                    }
                }

                fila[9] = "";                                   //AQUI NO VA UN COÑO
                fila[10] = "" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString().PadLeft(2, '0') + "-" + DateTime.Now.Day.ToString();
                fila[11] = "" + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString();//me da lala esta parte copy and paste                                          //hora
                fila[12] = gridFacturasPagar.Rows[i].Cells[9].Value.ToString().Replace(",", ".").Replace("-","");  
                fila[13] = "0";
                fila[14] = usuario;                           
                fila[15] = "REI";
                fila[16] = recibo.AuxiliarCliente;                                 //AUXILIAR                                   
                fila[17] = txtCliente.Text;                     //CODIGO
                fila[18] = lblNombreCliente.Text;               //NOMBRE CLIENTE                     
                fila[19] = "";
                fila[21] = gridFacturasPagar.Rows[i].Cells[1].Value.ToString();
                fila[22] = txtNumeroComprobante.Text;

                tablaMovcontab.Rows.Add(fila);
            }

            //parte movconta 

            //En caso de ser anticipo agregar primera parte de los asientos 

            if (cmbTipo.Text.Equals("Anticipo"))
            {
                items = items + 1;
                DataRow fila = tablaMovcontab.NewRow();


                fila[0] = items;
                fila[1] = txtCliente.Text;
                fila[2] = txtNumeroRecibo.Text;
                fila[3] = "0";
                fila[4] = "12";
                fila[5] = txtNumeroComprobante.Text;               //COMPROBANTE
                fila[6] = txtDescripcion.Text + " " + txtCliente.Text + " ";
                fila[7] = recibo.CuentaDoc;                             //CODIGO CUENTA DE ANT

                fila[8] = "-1";
                fila[20] = "C";
                montoC2 = montoC2 + Convert.ToDecimal(txtMontoPagoT.Text);

                fila[9] = "";                                   //AQUI NO VA UN COÑO
                fila[10] = "" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString().PadLeft(2, '0') + "-" + DateTime.Now.Day.ToString();
                fila[11] = "" + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString();//me da lala esta parte copy and paste                                          //hora
                fila[12] = txtMontoPagoT.Text.Replace(",", ".");
                fila[13] = "0";
                fila[14] = usuario; 
                fila[15] = "REI";
                fila[16] = recibo.AuxiliarCliente;                                 //AUXILIAR                                   
                fila[17] = txtCliente.Text;                     //CODIGO
                fila[18] = lblNombreCliente.Text;               //NOMBRE CLIENTE                     
                fila[19] = "";
                fila[22] = txtNumeroComprobante.Text;

                tablaMovcontab.Rows.Add(fila);
            }


            for (int i = 0; i < gridMovContab.RowCount; i++)
            {
                items = items + 1;
                DataRow fila = tablaMovcontab.NewRow();

                fila[0] = items;
                fila[1] = txtCliente.Text;
                fila[2] = txtNumeroRecibo.Text;
                fila[3] = "0";
                fila[4] = "12";
                fila[5] = txtNumeroComprobante.Text;                            //COMPROBANTE
                fila[6] = lblNombreCliente.Text;
                fila[7] = gridMovContab.Rows[i].Cells[2].Value.ToString();  ////CODIGO CUENTA
                fila[8] = gridMovContab.Rows[i].Cells[6].Value.ToString();  //numero (1 o -1)
                fila[9] = "";                                               //AQUI NO VA UN COÑO
                fila[10] = "" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString().PadLeft(2, '0') + "-" + DateTime.Now.Day.ToString();
                fila[11] = "" + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString();//me da lala esta parte copy and paste                                          //hora
                fila[12] = gridMovContab.Rows[i].Cells[8].Value.ToString().Replace(",", ".");
                fila[13] = "0";
                fila[14] = usuario;
                fila[15] = "REI";
                fila[16] = gridMovContab.Rows[i].Cells[7].Value.ToString();     //AUXILIAR                                   
                fila[17] = txtCliente.Text;                                     //CODIGO
                fila[18] = lblNombreCliente.Text;                               //NOMBRE CLIENTE                     
                fila[19] = "";
                fila[20] = gridMovContab.Rows[i].Cells[3].Value.ToString();     //letra (D o C)
                fila[21] = "";
                fila[22] = txtNumeroComprobante.Text;

                tablaMovcontab.Rows.Add(fila);

            }

            montoD2 = montoD2 + Convert.ToDecimal(txtDebito.Text);
            montoC2 = montoC2 + Convert.ToDecimal(txtCredito.Text);

            return tablaMovcontab;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            //if ((Convert.ToDecimal(txtMontoPagoT.Text))!= (Convert.ToDecimal(txtDebito.Text) - Convert.ToDecimal(txtCredito.Text)))
            //{
            //    MessageBox.Show("El monto de pago no es igual al monto del movimiento contable", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //}else if(Convert.ToDecimal(txtMontoDocuT.Text)!= Convert.ToDecimal(txtMontoPagoT.Text)){

            //    MessageBox.Show("El monto total de los documentos y el monto total de pago no coinciden", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //}else{

            //    armarDataTableMovContab();
            //    contenidoMovContab();
            //    recibo.guardarMovContab(tablaMovcontab);
            //}

            //lebrun.formularios.tesoreria.frmRecibodeIngreso
        }

        public static frmRecibodeIngreso DefInstance
        {
            get
            {
                if (m_FormDefInstance == null || m_FormDefInstance.IsDisposed)
                    m_FormDefInstance = new frmRecibodeIngreso();

                return m_FormDefInstance;
            }
            set
            {
                m_FormDefInstance = value;
            }
        }

        private void limpiarFormaPago() {

            txtNumero.Text = "";
            txtNumeroCuenta.Text = "";
            txtMontoFP.Text = "";
            cargarComboBanco();

        }

        private void limpiarMovcontab() {

            txtCuenta.Text = "";
            txtAuxiliar.Text = "";
            txtMontoMC.Text = "";
            btnAuxiliar.Enabled = false;
        
        }

        private void txtNumeroCuenta_KeyPress(object sender, KeyPressEventArgs e)
        {
            text.onlyNumberWithTab(sender, e);
        }

        public void login(string idUsuario)
        {
            usuario = idUsuario;
        }

        public void referenciaRefrescar(Form referencia)
        {
            //para refrescar lbxImportaciones 25-03-2013
            refLbxRecibo = (lbxReciboIngreso)referencia;
        }

        private void txtMontoFP_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox a = (TextBox)sender;
            text.conDecimalesDefinidosNegativo(sender, e, a, 2);
        }

        private void txtAuxiliar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r') {

                text.tab(sender, e);
            }
        }

        private void txtDescripcion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                txtMontoMC.Focus();
            }

        }

        private void fechaPago_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (cmbFormaPago.Text == "Efectivo")
                {
                    txtMontoFP.Focus();
                }
                else
                {
                    txtNumeroCuenta.Focus();
                }
            }
        }

        private void cmbFormaPago_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbFormaPago.Text.Equals("Efectivo"))
            {
                cmbBanco.Enabled = false;
                txtNumeroCuenta.Enabled = false;
                txtNumero.Enabled = false;
            }
            else if (cmbFormaPago.Text != "Efectivo")
            {
                cmbBanco.Enabled = true;
                txtNumeroCuenta.Enabled = true;
                txtNumero.Enabled = true;
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





        




    }
}
