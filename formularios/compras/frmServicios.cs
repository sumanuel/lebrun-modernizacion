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
using lebrun.clases.proveedores;
using lebrun.clasesData;
using lebrun.formularios.complementos;
using lebrun.formularios.contabilidad;
using lebrun.clases.complementos;
using lebrun.Reportes.retenciones;
using lebrun.Reportes.MovContab;

namespace lebrun.formularios.compras
{
    public partial class frmServicios : Form
    {
        private string porcReten;
        private Pagos pago;
        private int item;
        private int centinelaFila = -1;
        private movimientoContable movContab;
        private Servicios serv;
        private string ivaGn;
        private string ivaRd;
        private DataTable tabla;
        private Proveedores proveedor;
        private string mayorProveedor;
        private string auxiliarProveedor;
        private AuxiliarContable au;
        private Compania compania;
        public bool guard2;
        private FuncionesTexbox ftextbox;
        private lbxProveedores proveedores;
        private lbxtipoDocPro lbxTipDocpro;
        private lbxPlanCuentas planCuenta;
        private lbxAuxiliarContable auxContable;
        private lbxAuxiliaresOnly auxiliar;
        private proveedor cproveedores;
        private string descCuentaIva;
        private string descCuentaPro;
        private PlanCuentas planCuentas;
        private DataTable tablaCabacera;
        private movimientoContable movConta;
        private DataTable tablaMovcontab;
        private DataTable saldoLibro;
        private string ivaTotal;
        private string usuario;
        private Proveedores clasePro;
        private string docu;
        private string sufi;
        private string provee;
        private string tipo;
        private string opcion = "1";
        private int caso = 2;
        private DataTable tablaB;
        private string comprobante;
        private string docOrigen;
        private string sufOrigen;
        private string tipoOrigen;
        private string control;
        private lbxServicios refLblxServ;
        private retencionISLR retISLR;
        private string unidadTributaria;
        private string sustraendo;
        private string tarifaReten;
        private string montoMinimo;
        private string baseRetener;
        private string porRetenISLR;
        private lbxTarifaRetenc lbxTarifaReten;
        private frmOpcionesServicios opcionesServicios;
        private frmContenedorRetISLR conteISLR;
        private frmConteMovContab conteMovContab;
        private frmRif msjRif;
        private Principal referenciaPrincipal;
        private string compañiaActual;

        private decimal islr1 = 0;

        public frmServicios()
        {
            InitializeComponent();
            pago = new Pagos();
            serv = new Servicios();
            tabla = new DataTable();
            proveedor = new Proveedores();
            au = new AuxiliarContable();
            compania = new Compania();
            ftextbox = new FuncionesTexbox();
            cproveedores = new proveedor();
            planCuentas = new PlanCuentas();
            auxContable = new lbxAuxiliarContable();
            movConta = new movimientoContable();
            tablaCabacera = new DataTable();
            tablaMovcontab = new DataTable();
            clasePro = new Proveedores();
            tablaB = new DataTable();
            movContab = new movimientoContable();
            retISLR = new retencionISLR();
            compania.obtenerCodigoCompaniaActual();
            
        }

        public frmServicios(string doc, string suf, string pro, string tip, int opc)
        {
            InitializeComponent();
            pago = new Pagos();
            serv = new Servicios();
            tabla = new DataTable();
            proveedor = new Proveedores();
            au = new AuxiliarContable();
            compania = new Compania();
            ftextbox = new FuncionesTexbox();
            cproveedores = new proveedor();
            planCuentas = new PlanCuentas();
            movConta = new movimientoContable();
            tablaCabacera = new DataTable();
            tablaMovcontab = new DataTable();
            clasePro = new Proveedores();
            tablaB = new DataTable();
            movContab = new movimientoContable();
            retISLR = new retencionISLR();
            compania.obtenerCodigoCompaniaActual();

            docu = doc;
            sufi = suf;
            provee = pro;
            tipo = tip;
            opcion = Convert.ToString(opc);
        }

        public frmServicios(string doc, string suf, string pro, string tip, int opc, int tab)
        {
            InitializeComponent();
            pago = new Pagos();
            serv = new Servicios();
            tabla = new DataTable();
            proveedor = new Proveedores();
            au = new AuxiliarContable();
            compania = new Compania();
            ftextbox = new FuncionesTexbox();
            cproveedores = new proveedor();
            planCuentas = new PlanCuentas();
            movConta = new movimientoContable();
            tablaCabacera = new DataTable();
            tablaMovcontab = new DataTable();
            clasePro = new Proveedores();
            tablaB = new DataTable();
            movContab = new movimientoContable();
            retISLR = new retencionISLR();
            compania.obtenerCodigoCompaniaActual();

            docu = doc;
            sufi = suf;
            provee = pro;
            tipo = tip;
            opcion = Convert.ToString(opc);
            caso = tab;
        }

        
        private void button5_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Seguro que desea cerrar el documento sin guardarlo?(¡Perdera todos los cambios!)", "Alerta ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void frmServicios_Load(object sender, EventArgs e)
        {
            referenciaPrincipal = (Principal)this.MdiParent;
            auxiliar = new lbxAuxiliaresOnly();
            if (opcion.Equals("1"))
            {

                cmbCodicionPago.DisplayMember = "conp_descripcion";
                cmbCodicionPago.ValueMember = "conp_codigo";
                cmbCodicionPago.DataSource = pago.cargarCondicionPago();
                diasCondicionPago();
                consultarAlicuotasIva();
                compania.obtenerCodigoCompaniaActual(); 
            }

            if (opcion.Equals("2"))
            {
                if (txtDCOM.Visible == false)
                {
                    docOrigen = txtFCOM.Text;
                    sufOrigen = txtSufFCOM.Text;
                }
                else
                {
                    docOrigen = txtDCOM.Text;
                    sufOrigen = txtSufDCOM.Text;
                }

                tipoOrigen = txtTipoDoc.Text;
                serv.datosTipoDoc(tipo);
                cmbCodicionPago.DisplayMember = "conp_descripcion";
                cmbCodicionPago.ValueMember = "conp_codigo";
                cmbCodicionPago.DataSource = pago.cargarCondicionPago();
                diasCondicionPago();
                consultarAlicuotasIva();
                compania.obtenerCodigoCompaniaActual();
                cargarModificar();

                txtDebito.Text = "" + Convert.ToString(dataGridView1.Rows.Cast<DataGridViewRow>().Sum(x => Convert.ToDouble(x.Cells[4].Value)));
                txtCredito.Text = "" + Convert.ToString(dataGridView1.Rows.Cast<DataGridViewRow>().Sum(x => Convert.ToDouble(x.Cells[5].Value)));
                txtDebito.Text = string.Format("{0:0.00}", Convert.ToDecimal(txtDebito.Text));
                txtCredito.Text = string.Format("{0:0.00}", Convert.ToDecimal(txtCredito.Text));
            }

            if (opcion.Equals("3"))
            {
                serv.datosTipoDoc(tipo);
                cmbCodicionPago.DisplayMember = "conp_descripcion";
                cmbCodicionPago.ValueMember = "conp_codigo";
                cmbCodicionPago.DataSource = pago.cargarCondicionPago();
                diasCondicionPago();
                consultarAlicuotasIva();
                compania.obtenerCodigoCompaniaActual();
                cargarModificar();
                bloquearControles();

                txtDebito.Text = "" + Convert.ToString(dataGridView1.Rows.Cast<DataGridViewRow>().Sum(x => Convert.ToDouble(x.Cells[4].Value)));
                txtCredito.Text = "" + Convert.ToString(dataGridView1.Rows.Cast<DataGridViewRow>().Sum(x => Convert.ToDouble(x.Cells[5].Value)));
                txtDebito.Text = string.Format("{0:0.00}", Convert.ToDecimal(txtDebito.Text));
                txtCredito.Text = string.Format("{0:0.00}", Convert.ToDecimal(txtCredito.Text));
                
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (validarCierre())
            {
                panel1.Visible = true;
            }
        }
        private void button9_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            //asignandoValores(2);
            //obtenerComprobanteContable();
            //contenidoMovContab();
        }

        private void totalMonto()
        {
            txtMontoTotal.Text = "" + Convert.ToDecimal(txtGN.Text) + Convert.ToDecimal(txtRD.Text) + Convert.ToDecimal(txtEX.Text);
        }

        private void calculosIvas()
        {

            txtIvaGN.Text = "" + (Convert.ToDecimal(txtGN.Text) * Convert.ToDecimal(ivaGn)) / 100;
            txtIvaGN.Text = "" + (Convert.ToDecimal(string.Format("{0:0.00}", Convert.ToDecimal(txtIvaGN.Text))));

            txtIvaRD.Text = "" + (Convert.ToDecimal(txtRD.Text) * Convert.ToDecimal(ivaRd)) / 100;
            txtIvaRD.Text = "" + (Convert.ToDecimal(string.Format("{0:0.00}", Convert.ToDecimal(txtIvaRD.Text))));

            txtIvaTotal.Text = "" + (Convert.ToDecimal(txtIvaGN.Text) + Convert.ToDecimal(txtIvaRD.Text));
            
        }

        private void calculosIvaGN()
        {

            txtIvaGN.Text = "" + (Convert.ToDecimal(txtGN.Text) * Convert.ToDecimal(ivaGn)) / 100;
            txtIvaGN.Text = "" + (Convert.ToDecimal(string.Format("{0:0.00}", Convert.ToDecimal(txtIvaGN.Text))));

            txtIvaTotal.Text = "" + (Convert.ToDecimal(txtIvaGN.Text) + Convert.ToDecimal(txtIvaRD.Text));

        }


        private void calculosIvaRD()
        {
            txtIvaRD.Text = "" + (Convert.ToDecimal(txtRD.Text) * Convert.ToDecimal(ivaRd)) / 100;
            txtIvaRD.Text = "" + (Convert.ToDecimal(string.Format("{0:0.00}", Convert.ToDecimal(txtIvaRD.Text))));

            txtIvaTotal.Text = "" + (Convert.ToDecimal(txtIvaGN.Text) + Convert.ToDecimal(txtIvaRD.Text));
        }

        private void calcularTotalGN()
        {

            txtGN.Text = "" + (Convert.ToDecimal(txtMontoTotal.Text)) / Convert.ToDecimal(1 + (Convert.ToDecimal(ivaGn) / 100));
            txtGN.Text = string.Format("{0:0.00}", Convert.ToDecimal(txtGN.Text));
        }

        private void txtMontoTotal_Leave(object sender, EventArgs e)
        {
            if (txtMontoTotal.Text == "")
            {
                txtMontoTotal.Text = "0";
            }

            calcularTotalGN();
            txtMontoTotal.Text = string.Format("{0:0.00}", Convert.ToDecimal(txtMontoTotal.Text));
        }

        private void txtGN_Leave(object sender, EventArgs e)
        {
            //calculosIvas();

            if (txtGN.Text == "")
            {
                txtGN.Text = "0";
            }
            txtGN.Text = string.Format("{0:0.00}", Convert.ToDecimal(txtGN.Text));

            calculosIvaGN();
            calcularSubTotal();
        }

        private void txtRD_Leave(object sender, EventArgs e)
        {
            if (txtRD.Text == "")
            {
                txtRD.Text = "0";
            }

            txtRD.Text = string.Format("{0:0.00}", Convert.ToDecimal(txtRD.Text));
            //calculosIvas();
            calculosIvaRD();
            calcularSubTotal();
        }

        private void txtIvaRD_Leave(object sender, EventArgs e)
        {
            if (txtIvaRD.Text == "")
            {
                txtIvaRD.Text = "0";
            }

            txtIvaTotal.Text = "" + (Convert.ToDecimal(txtIvaGN.Text) + Convert.ToDecimal(txtIvaRD.Text));
        }

        private bool validarMontos()
        {
            if (Convert.ToDecimal(txtMontoTotal.Text) != (Convert.ToDecimal(txtGN.Text) + Convert.ToDecimal(txtRD.Text) + Convert.ToDecimal(txtEX.Text) + Convert.ToDecimal(txtIvaGN.Text) + Convert.ToDecimal(txtIvaRD.Text)))
            {
                //MessageBox.Show("Monto total descuadrado", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lblDescuadreBase.Text = "Descuadrado";
                lblDescuadreBase.ForeColor = Color.Red;
                txtMontoTotal.Focus();
                return false;
            }
            else
            {
                lblDescuadreBase.Text = "Cuadrado";
                lblDescuadreBase.ForeColor = Color.Black;
            }

            if (Convert.ToDecimal(txtIvaTotal.Text) != (Convert.ToDecimal(txtIvaGN.Text) + Convert.ToDecimal(txtIvaRD.Text)))
            {
                //MessageBox.Show("Monto total IVA descuadrado", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lblDescuadreIva.Text = "Descuadrado";
                lblDescuadreIva.ForeColor = Color.Red;
                txtMontoTotal.Focus();
                return false;
            }
            else
            {
                lblDescuadreIva.Text = "Cuadrado";
                lblDescuadreIva.ForeColor = Color.Black;
            }

            return true;
   
        }

        private void txtEX_Leave(object sender, EventArgs e)
        {
            if (txtEX.Text == "")
            {
                txtEX.Text = "0";
            }

            if (validarDatosAddgrid())
            {
                if (validarMontos())
                {
                    consultarCuentaProveedor();
                    porRetenProveedor();
                    txtEX.Text = string.Format("{0:0.00}", Convert.ToDecimal(txtEX.Text));
                    calcularSubTotal();
                    verificarCuentasContab();
                    agregarFilaPrimaria();
                    consultarCuentaProveedor();
                    calcularMontos();

                }
            }
        }

        private void agregarFilaPrimaria()
        {
            dataGridView1.Rows.Clear();

            int indice = 0;
            item = 1;

            dataGridView1.Rows.Add();
            dataGridView1.Rows[indice].Cells[0].Value = item;
            dataGridView1.Rows[indice].Cells[1].Value = "Monto Total";
            dataGridView1.Rows[indice].Cells[2].Value = "" + descCuentaPro;


            if (serv.Tdc_correlativo == "37" || serv.Tdc_correlativo == "40")
            {
                dataGridView1.Rows[indice].Cells[3].Value = "CREDITO"; //si es 37 CREDITO Sino DEBITO
                dataGridView1.Rows[indice].Cells[5].Value = string.Format("{0:0.00}", Convert.ToDecimal(txtMontoTotal.Text));
                dataGridView1.Rows[indice].Cells[7].Value = "-1"; //Si es Credito -1 Sino 1
            }
            if (serv.Tdc_correlativo == "43")
            {
                dataGridView1.Rows[indice].Cells[3].Value = "DEBITO"; //si es 37 CREDITO Sino DEBITO
                dataGridView1.Rows[indice].Cells[4].Value = string.Format("{0:0.00}", Convert.ToDecimal(txtMontoTotal.Text));
                dataGridView1.Rows[indice].Cells[7].Value = "1"; //Si es Credito -1 Sino 1
            }

            dataGridView1.Rows[indice].Cells[6].Value = string.Format("{0:0.00}", Convert.ToDecimal(txtMontoTotal.Text));
            dataGridView1.Rows[indice].Cells[8].Value = "0";
            dataGridView1.Rows[indice].Cells[9].Value = "0";
            dataGridView1.Rows[indice].Cells[10].Value = "";
            dataGridView1.Rows[indice].Cells[11].Value = "" + auxiliarProveedor;

            if (Convert.ToDecimal(txtIvaRD.Text) != 0)
            {
                item = item + 1;

                dataGridView1.Rows.Add();
                dataGridView1.Rows[indice + 1].Cells[0].Value = item;
                dataGridView1.Rows[indice + 1].Cells[1].Value = "Iva Reducido";
                dataGridView1.Rows[indice + 1].Cells[2].Value = "" + descCuentaIva;

                if (serv.Tdc_correlativo == "37" || serv.Tdc_correlativo == "40")
                {
                    dataGridView1.Rows[indice + 1].Cells[3].Value = "DEBITO"; //si es 37 CREDITO Sino DEBITO
                    dataGridView1.Rows[indice + 1].Cells[4].Value = string.Format("{0:0.00}", Convert.ToDecimal(txtIvaRD.Text));
                    dataGridView1.Rows[indice + 1].Cells[7].Value = "1"; //Si es Credito -1 Sino 1
                }

                if (serv.Tdc_correlativo == "43")
                {
                    dataGridView1.Rows[indice + 1].Cells[3].Value = "CREDITO"; //si es 37 CREDITO Sino DEBITO
                    dataGridView1.Rows[indice + 1].Cells[5].Value = string.Format("{0:0.00}", Convert.ToDecimal(txtIvaRD.Text));
                    dataGridView1.Rows[indice + 1].Cells[7].Value = "-1"; //Si es Credito -1 Sino 1
                }

                dataGridView1.Rows[indice + 1].Cells[6].Value = string.Format("{0:0.00}", Convert.ToDecimal(txtIvaRD.Text));
                dataGridView1.Rows[indice + 1].Cells[8].Value = "0";
                dataGridView1.Rows[indice + 1].Cells[9].Value = "0";
                dataGridView1.Rows[indice + 1].Cells[10].Value = "";
                dataGridView1.Rows[indice + 1].Cells[11].Value = "";

                indice = indice + 1;
            }

            if (Convert.ToDecimal(txtIvaGN.Text) != 0)
            {
                item = item + 1;

                dataGridView1.Rows.Add();
                dataGridView1.Rows[indice + 1].Cells[0].Value = item;
                dataGridView1.Rows[indice + 1].Cells[1].Value = "Iva General";
                dataGridView1.Rows[indice + 1].Cells[2].Value = "" + descCuentaIva;

                if (serv.Tdc_correlativo == "37" || serv.Tdc_correlativo == "40")
                {
                    dataGridView1.Rows[indice + 1].Cells[3].Value = "DEBITO"; //si es 37 CREDITO Sino DEBITO
                    dataGridView1.Rows[indice + 1].Cells[4].Value = string.Format("{0:0.00}", Convert.ToDecimal(txtIvaGN.Text));
                    dataGridView1.Rows[indice + 1].Cells[7].Value = "1"; //Si es Credito -1 Sino 1
                }

                if (serv.Tdc_correlativo == "43")
                {
                    dataGridView1.Rows[indice + 1].Cells[3].Value = "CREDITO"; //si es 37 CREDITO Sino DEBITO
                    dataGridView1.Rows[indice + 1].Cells[5].Value = string.Format("{0:0.00}", Convert.ToDecimal(txtIvaGN.Text));
                    dataGridView1.Rows[indice + 1].Cells[7].Value = "-1"; //Si es Credito -1 Sino 1
                }

                dataGridView1.Rows[indice + 1].Cells[6].Value = string.Format("{0:0.00}", Convert.ToDecimal(txtIvaGN.Text));
                dataGridView1.Rows[indice + 1].Cells[8].Value = "0";
                dataGridView1.Rows[indice + 1].Cells[9].Value = "0";
                dataGridView1.Rows[indice + 1].Cells[10].Value = "";
                dataGridView1.Rows[indice + 1].Cells[11].Value = "";
            }

        }


        public void agregarFila(int fila, DataGridView grid)
        {

            if (grid.Name.Equals("dataGridView1"))
            {
                if (fila < 0)
                {
                    grid.Rows.Add();
                    fila = grid.Rows.Count - 1;
                }

                grid.Rows[fila].Cells[0].Value = fila + 1;
                grid.Rows[fila].Cells[1].Value = txtDescripcionMov.Text;
                grid.Rows[fila].Cells[2].Value = txtCuenta.Text + " - " + planCuentas.getDescripcionPlanC(txtCuenta.Text, "01");


                if (rDebito.Checked)
                {
                    grid.Rows[fila].Cells[3].Value = "DEBITO";
                    grid.Rows[fila].Cells[7].Value = "1";
                }

                if (rCredito.Checked)
                {
                    grid.Rows[fila].Cells[3].Value = "CREDITO";
                    grid.Rows[fila].Cells[7].Value = "-1";
                }

                if (rDebito.Checked)
                {
                    grid.Rows[fila].Cells[4].Value = string.Format("{0:0.00}", Convert.ToDecimal(txtMontoMov.Text));
                }

                if (rCredito.Checked)
                {
                    grid.Rows[fila].Cells[5].Value = string.Format("{0:0.00}", Convert.ToDecimal(txtMontoMov.Text));
                }


                grid.Rows[fila].Cells[6].Value = string.Format("{0:0.00}", Convert.ToDecimal(txtMontoMov.Text));
                grid.Rows[fila].Cells[8].Value = "";
                grid.Rows[fila].Cells[9].Value = "";
                grid.Rows[fila].Cells[10].Value = "";
                grid.Rows[fila].Cells[11].Value = txtAuxiliar.Text;

                //grid.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                //grid.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                //grid.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                //grid.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                //grid.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                //grid.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                limpiarMovContab();
                calcularMontos();
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {

            if (validarDatosMovContab())
            {

                if (centinelaFila >= 0)
                {
                    agregarFila(centinelaFila, dataGridView1);
                    centinelaFila = -1;
                }
                else
                {
                    agregarFila(-1, dataGridView1);
                }
            }
        }

        private bool validarDatosMovContab()
        {
            if (txtCuenta.Text == "")
            {
                MessageBox.Show("Debe seleccionar alguna cuenta contable", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCuenta.Focus();
                return false;
            }

            if (btnAuxiliar.Enabled == true && txtAuxiliar.Text == "")
            {
                MessageBox.Show("Esta cuenta maneja auxilar, seleccione alguno", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtAuxiliar.Focus();
                return false;
            }

            if (txtDescripcionMov.Text == "")
            {
                MessageBox.Show("La descripción no debe quedar vacia", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDescripcionMov.Focus();
                return false;
            }

            if (Convert.ToDecimal(txtMontoMov.Text) == 0)
            {
                MessageBox.Show("El monto no debe ser igual a cero (0)", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMontoMov.Focus();
                return false;
            }

            return true;
            
        }
        
        private void limpiarMovContab()
        {
            txtCuenta.Text = "";
            txtAuxiliar.Text = "";
            //txtDescripcionMov.Text = "";
            txtMontoMov.Text = "0";
        }

        private void consultarAlicuotasIva()
        {
            tabla = serv.consultarAlicuotas();

            if (tabla.Rows.Count > 0)
            {
                ivaGn = tabla.Rows[0][5].ToString();
                ivaRd = tabla.Rows[1][5].ToString();
            }
        }

        private void txtTipoDoc_Leave(object sender, EventArgs e)
        {
            if (txtTipoDoc.Text != "")
            {
                if (serv.datosTipoDoc(txtTipoDoc.Text))
                {
                    cambioEtiquetas();
                    lblTipoDoc.Text = serv.Tdc_descri;

                    if (serv.Tdc_codigo.Equals("41") || serv.Tdc_codigo.Equals("51") || serv.Tdc_codigo.Equals("42") || serv.Tdc_codigo.Equals("99") || serv.Tdc_cxp.Equals("0"))
                    {
                        MessageBox.Show("Documento invalido para este tipo de transsacion", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtTipoDoc.Text = "";
                        lblTipoDoc.Text = "...";
                        txtTipoDoc.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("El tipo de documento no existe", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtTipoDoc.Focus();
                    txtTipoDoc.Text = "";
                    txtTipoDoc.Focus();
                }
            }
        }

        private void txtProveedor_Leave(object sender, EventArgs e)
        {
            if (txtProveedor.Text != "")
            {
                if (consultaRapidaProveedor())
                {
                    consultarCuentaProveedor();
                    porRetenProveedor();
                }
            }
        }

        private void consultarCuentaProveedor()
        {
            mayorProveedor = "";
            auxiliarProveedor = "";

            tabla = proveedor.obtenerCuentasproveedor(txtProveedor.Text, txtTipoDoc.Text);
            if (tabla.Rows.Count > 0)
            {
                mayorProveedor = tabla.Rows[0][0].ToString();
                auxiliarProveedor = tabla.Rows[0][1].ToString();
            }
        }

        private void calcularMontos()
        {
           
            txtDebito.Text = "" + Convert.ToString(dataGridView1.Rows.Cast<DataGridViewRow>().Sum(x => Convert.ToDouble(x.Cells[4].Value)));
            txtCredito.Text = "" + Convert.ToString(dataGridView1.Rows.Cast<DataGridViewRow>().Sum(x => Convert.ToDouble(x.Cells[5].Value)));

            txtDebito.Text = string.Format("{0:0.00}", Convert.ToDecimal(txtDebito.Text));
            txtCredito.Text = string.Format("{0:0.00}", Convert.ToDecimal(txtCredito.Text));

            txtDiferencia.Text = "" + (Convert.ToDecimal(txtCredito.Text) - Convert.ToDecimal(txtDebito.Text));
        }

        private void calcularSubTotal()
        {
            txtSubTotal.Text = "" + (Convert.ToDecimal(txtGN.Text) + Convert.ToDecimal(txtRD.Text) + Convert.ToDecimal(txtEX.Text));
            //txtDiferencia.Text = txtSubTotal.Text;
            //txtDiferencia.Text = "" + (Convert.ToDecimal(txtCredito.Text) - Convert.ToDecimal(txtDebito.Text));
        }

        private void buscarVerificarPlanC()
        {
            if (txtCuenta.Text != "")
            {
                au.CodigoPlanCuentas = txtCuenta.Text;

                if (au.validarPlanCuenta(10, compania.BaseDatosActual))
                {
                    control = planCuentas.getControl(txtCuenta.Text, compania.BaseDatosActual);
                    //txtNombreCuenta.Text = au.getDescripcionPlanC(au.CodigoPlanCuentas, "01");

                    if (control.Equals("-1"))
                    {
                        MessageBox.Show("El numero de cuenta ingresado esta bloqueado", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtCuenta.Text = "";
                        txtCuenta.Focus();
                    }
                    else
                    {

                        
                        if (control.Contains(compania.BaseDatosActual) || control.Equals("0"))
                        {

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
                                txtDescripcionMov.Focus();
                            }
                        }
                        else
                        {
                            MessageBox.Show("La compañia no posee permiso para manejar esta cuenta", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtCuenta.Text = "";
                            txtCuenta.Focus();
                        }
                        
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

        private void button2_Click(object sender, EventArgs e)
        {
            proveedores = lbxProveedores.DefInstance;
            proveedores.destino(this, this.Name);
            proveedores.frmProduct = "Producto";//para invocar cuando lo invoca  otro modulo
            proveedores.Eleccion = "1";
            proveedores.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            lbxTipDocpro = lbxtipoDocPro.DefInstance;
            lbxTipDocpro.destino(this, this.Name);
            lbxTipDocpro.Show();
        }

        private void btnBuscarCuenta_Click(object sender, EventArgs e)
        {
            
            //fabian 27/04/2016
            //compania.obtenerCodigoCompaniaActual();
            //auxContable = lbxAuxiliarContable.DefInstance;
            //auxContable.soloVista(this, this.Name, compania.BaseDatosActual);
            //auxContable.Show();
            if (txtCuenta.Text == "")
            {
                compañiaActual = null;
                auxiliar.CompañiaActual = null;
                compania.obtenerCodigoCompaniaActual();
                compañiaActual = compania.BaseDatosActual;
                auxContable = lbxAuxiliarContable.DefInstance;
                auxContable.soloVista(this, this.Name, compañiaActual); ;
                auxContable.Show();
                auxContable.lbxComprobante1(false, compañiaActual);
                auxContable.cmdAgregar.Visible = false;
                auxContable.cmdBotonModificar.Visible = false;
            }
            else
            {
                buscarVerificarPlanC();
            }



        //    compania.obtenerCodigoCompaniaActual();
        //    planCuenta = lbxPlanCuentas.DefInstance;
         //  planCuenta.soloVista(this, this.Name, compania.BaseDatosActual);
        //    planCuenta.Show();
        }

        private void btnAuxiliar_Click(object sender, EventArgs e)
        {
            if(txtCuenta.Text != "")
            {
                
                //compania.obtenerCodigoCompaniaActual();
                //auxiliar = lbxAuxiliaresOnly.DefInstance;
                //auxiliar.guardarCompania(compania.BaseDatosActual);
                //auxiliar.destino(this, this.Name);
                //auxiliar.codCuenta(txtCuenta.Text);
                ////auxiliar.llenarAuxiliares(); 
                //auxiliar.Show();
                //auxiliar.llenarAuxiliares();
                compañiaActual = null;
                auxiliar = lbxAuxiliaresOnly.DefInstance;
                compania.obtenerCodigoCompaniaActual();
                compañiaActual = compania.BaseDatosActual;
                auxiliar.compañiaActual=compañiaActual;
                auxiliar.destino(this, this.Name);
                auxiliar.guardarCompania(compania.BaseDatosActual);
                auxiliar.codCuenta(txtCuenta.Text);
                auxiliar.Show();
                auxiliar.llenarAuxiliares();
            }
            else
            {
                MessageBox.Show("Debe seleccionar una cuenta antes de elegir un auxiliar", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private bool consultaRapidaProveedor() //PROVEEDOR
        {
            bool val2 = false;

            DataTable dproveedor = new DataTable();
            dproveedor = cproveedores.consultarProveedorUnico(txtProveedor.Text);

            if (dproveedor.Rows.Count != 0)
            {
                lblNombrePro.Text = dproveedor.Rows[0][1].ToString();
                lblRifProveedor.Text = dproveedor.Rows[0][2].ToString();
                val2 = true;

                msjRif = new frmRif();
                msjRif.lblRifPro.Text = lblRifProveedor.Text;
                msjRif.Text = lblRifProveedor.Text;
                msjRif.ShowDialog();
            }
            else
            {
                MessageBox.Show("No existe proveedor con este codigo", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                val2 = false;
                txtProveedor.Focus();
                txtProveedor.Text = "";
                lblNombrePro.Text = "...";
                lblRifProveedor.Text = "...";

            }
            return val2;

        }

        private void verificarCuentasContab()
        {
            tabla = serv.consultarCuentasContab(mayorProveedor, serv.Tdc_reporte,"01");

            if (tabla.Rows.Count > 0)
            {
                if (mayorProveedor == tabla.Rows[1][0].ToString())
                {
                    descCuentaPro = mayorProveedor + " - " + tabla.Rows[1][1].ToString();
                }

                if (serv.Tdc_reporte == tabla.Rows[0][0].ToString())
                {
                    descCuentaIva = serv.Tdc_reporte + " - " + tabla.Rows[0][1].ToString();
                }
            }
        }

        private void txtTipoDoc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtProveedor.Focus();
            }
        }

        private void txtProveedor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                ftextbox.onlyNumberWithTab(sender, e);
            }
        }

        private void cambioEtiquetas()
        {
            if (serv.Tdc_correlativo == "37")
            {
                txtDCOM.Visible = false;
                txtControlDCOM.Visible = false;
                txtSufDCOM.Visible = false;

                txtDCOM.Text = "";
                txtControlDCOM.Text = "";
                txtSufDCOM.Text = "0";

                txtFCOM.Text = "";
                txtControlFCOM.Text = "";
                txtSufFCOM.Text = "0";

                label3.Visible = false;
                label6.Visible = false;

                label7.Text = "Factura";
                label8.Text = "Control F";
                   
            }

            if (serv.Tdc_correlativo == "43")
            {

                txtDCOM.Visible = true;
                txtControlDCOM.Visible = true;
                txtSufDCOM.Visible = true;

                txtDCOM.Text = "";
                txtControlDCOM.Text = "";
                txtSufDCOM.Text = "0";

                txtFCOM.Text = "";
                txtControlFCOM.Text = "";
                txtSufFCOM.Text = "0";


                label3.Visible = true;
                label6.Visible = true;

                label7.Text = "Fac Aplica";
                label8.Text = "Control A";

            }

            if (serv.Tdc_correlativo == "40")
            {
                txtDCOM.Visible = true;
                txtControlDCOM.Visible = true;
                txtSufDCOM.Visible = true;

                txtDCOM.Text = "";
                txtControlDCOM.Text = "";
                txtSufDCOM.Text = "0";

                txtFCOM.Text = "";
                txtControlFCOM.Text = "";
                txtSufFCOM.Text = "0";

                label3.Visible = true;
                label6.Visible = true;

                label7.Text = "Fac Aplica";
                label8.Text = "Control A";
            }

        }

        private void txtDCOM_KeyPress(object sender, KeyPressEventArgs e)
        {
            ftextbox.tab(sender, e);
        }

        private void txtSufDCOM_KeyPress(object sender, KeyPressEventArgs e)
        {

            ftextbox.onlyNumberWithTab(sender, e);
        }

        private void txtControlDCOM_KeyPress(object sender, KeyPressEventArgs e)
        {
            ftextbox.tab(sender, e);
        }

        private void txtFCOM_KeyPress(object sender, KeyPressEventArgs e)
        {
            ftextbox.tab(sender, e);
        }

        private void txtSufFCOM_KeyPress(object sender, KeyPressEventArgs e)
        {
            //ftextbox.tab(sender, e);
            ftextbox.onlyNumberWithTab(sender, e);
        }

        private void txtControlFCOM_KeyPress(object sender, KeyPressEventArgs e)
        {
            ftextbox.tab(sender, e);
        }

        private void cmbCodicionPago_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                ftextbox.tab(sender, e);
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtDescripcion_KeyPress(object sender, KeyPressEventArgs e)
        {
            ftextbox.tab(sender, e);
        }

        private void fechaEmision_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            ftextbox.tab(sender, e);

        }

        private void txtMontoTotal_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox a = (TextBox)sender;
            ftextbox.conDecimalesDefinidosNegativo(sender, e, a, 2);
        }

        private void txtDescripcionMov_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtMontoMov.Focus();
            }
        }

        private void txtMontoMov_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox a = (TextBox)sender;
            ftextbox.conDecimalesDefinidosNegativo(sender, e, a, 2);
        }

        private bool validarDatosAddgrid()
        {
            if (txtTipoDoc.Text == "") {

                MessageBox.Show("Debe seleccionar un tipo de documento", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTipoDoc.Focus();
                return false;
            }

            if (txtProveedor.Text == "")
            {
                MessageBox.Show("Debe seleccionar algún proveedor", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtProveedor.Focus();
                return false;
            }

            if (Convert.ToDecimal(txtMontoTotal.Text) == 0)
            {
                MessageBox.Show("El monto total no debe ser igual a cero (0)", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMontoTotal.Focus();
                return false;
            }

            return true;
        }

        private bool validarCierre()
        {
                if (txtTipoDoc.Text == "")
                {
                    MessageBox.Show("Debe seleccionar un tipo de documento", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtTipoDoc.Focus();
                    return false;
                }

                if (txtProveedor.Text == "")
                {
                    MessageBox.Show("Debe seleccionar algun proveedor", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtProveedor.Focus();
                    return false;
                }

                if (txtDCOM.Visible == true && txtDCOM.Text == "")
                {
                    MessageBox.Show("El campo # Devolucion no debe quedar vacio", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtDCOM.Focus();
                    return false;
                }

                if (txtSufDCOM.Visible == true && txtSufDCOM.Text == "")
                {
                    MessageBox.Show("El campo  Sufijo Devolucion no debe quedar vacio", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtSufDCOM.Focus();
                    return false;
                }

                if (txtControlDCOM.Visible == true && txtControlDCOM.Text == "")
                {
                    MessageBox.Show("El campo # Control Devolucion no debe quedar vacio", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtControlDCOM.Focus();
                    return false;
                }

                if (txtFCOM.Text == "")
                {
                    MessageBox.Show("El campo # Factura no debe quedar vacio", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtFCOM.Focus();
                    return false;
                }

                if (txtSufFCOM.Text == "")
                {
                    MessageBox.Show("El campo Sufijo Factura no debe quedar vacio", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtSufFCOM.Focus();
                    return false;
                }

                if (txtControlFCOM.Text == "")
                {
                    MessageBox.Show("El campo # Control Factura no debe quedar vacio", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtControlFCOM.Focus();
                    return false;
                }

                if (txtDescripcion.Text == "")
                {
                    MessageBox.Show("El campo descripcion no debe quedar vacio", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtDescripcion.Focus();
                    return false;
                }
                if (Convert.ToDecimal(txtDiferencia.Text) != 0)
                {
                    MessageBox.Show("Valores descuadrados verifique los montos", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtDiferencia.ForeColor = Color.Red;
                    return false;
                }

                return true;
        }

        private void btnDefinitivo_Click(object sender, EventArgs e)
        {
            if (opcion.Equals("3"))
            {
                if (!retISLR.verificarExistenciaRet(docOrigen, sufOrigen, tipoOrigen, txtProveedor.Text))
                {
                    porRetenProveedor();
                    asignandoValores(4);
                    guardarISLR();

                    contenidoMovContabISLR2();
                    obtenerComprobanteContable();
                    asignandoValoresISLR();
                    retISLR.cuentaCorrelativo();
                    retISLR.actualizarLibroISLR2();
                    retISLR.actualizarSaldoISLR2();
                    retISLR.armarDetallesMovContabISLR(comprobante);
                    retISLR.ingresarMovContabISLR();
                    
                    retISLR.ingresarCabeceraISLR(contenidoCabeceraISLR());

                    conteISLR = new frmContenedorRetISLR(retISLR.ImprimirISLR(txtProveedor.Text, serv.NroDocum, serv.SufiDocum, txtTipoDoc.Text));
                    conteISLR.Show();

                    conteMovContab = new frmConteMovContab(movConta.ReimprimirComprobante(comprobante, compania.BaseDatosActual));
                    conteMovContab.Show();

                    refLblxServ.refrescar();
                    refLblxServ.Visible = true;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Este documento ya tiene una retención asignada", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                if (verificarDoc("1"))
                {
                    porRetenProveedor();
                    asignandoValores(4);

                    if (serv.Tdc_libcom.Equals("1"))
                    {
                        serv.ingresarLibroCompras();
                    }

                    if (serv.Tdc_cxp.Equals("1"))
                    {
                        serv.ingresarSalpro();
                    }

                    serv.ingresarDocPro(1);
                    serv.actualizarDocPro2();
                    obtenerComprobanteContable();
                    contenidoCabecera();
                    movConta.cabeceraSyscontab(tablaCabacera, compania.BaseDatosActual);
                    contenidoMovContab();
                    movConta.ingresarMovContab(tablaMovcontab);
                    movConta.detallesSyscontab(tablaMovcontab, compania.BaseDatosActual);

                    movConta.eliminarMovContab(txtProveedor.Text, docOrigen, sufOrigen, tipoOrigen);
                    movConta.eliminarMovContab(txtProveedor.Text, serv.NroDocum, serv.SufiDocum, serv.IdTipoDoc);

                    serv.eliminarCabeceraDocTemp(docOrigen, sufOrigen, tipoOrigen, txtProveedor.Text);
                    serv.eliminarCabeceraDocTemp(serv.NroDocum, serv.SufiDocum, serv.IdTipoDoc, txtProveedor.Text);

                    if (serv.Tdc_rti.Equals("1"))
                    {
                        guardarISLR();
                    }

                    panel1.Visible = false;

                    opcionesServicios = new frmOpcionesServicios(serv.IdProveedor,
                        serv.NroDocum, serv.SufiDocum, serv.IdTipoDoc, serv.Pro_nombreProve, serv.Login);

                    opcionesServicios.Show();

                    opcionesServicios.manejandoLbx(refLblxServ);
                    refLblxServ.refrescar();
                    refLblxServ.Visible = false;
                    this.Close();
                }
                else
                {
                    panel1.Visible = false;
                }
            }
        }

        private void asignandoValores(int status) {

            serv.IdProveedor = txtProveedor.Text;
            serv.IdTipoDoc = txtTipoDoc.Text;

            if(serv.Tdc_correlativo.Equals("37")){

                serv.NroDocum = txtFCOM.Text;
                serv.SufiDocum = txtSufFCOM.Text;
                serv.NroControlSeniat = txtControlFCOM.Text;

                serv.NroDocumAfec = "";
                serv.SufiDocumAfec = "";
                serv.NroFiscal = "";
                serv.TipTra = "C";
                
            }

            if (serv.Tdc_correlativo.Equals("43") || serv.Tdc_correlativo.Equals("40"))
            {
                serv.NroDocum = txtDCOM.Text;
                serv.SufiDocum = txtSufDCOM.Text;
                serv.NroControlSeniat = txtControlDCOM.Text;

                serv.NroDocumAfec = txtFCOM.Text;
                serv.SufiDocumAfec = txtSufFCOM.Text;
                serv.NroFiscal = txtControlFCOM.Text;
                serv.TipTra = "D";
            }

            serv.Condicion = cmbCodicionPago.SelectedValue.ToString();
            serv.Descripcion = txtDescripcion.Text;
            serv.FechaEmision = Convert.ToDateTime(fechaEmision.Text).ToString("yyyy-MM-dd");
            serv.FechaCarga = DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day;
            serv.FechaLibCompras = Convert.ToDateTime(fechaLibro.Text).ToString("yyyy-MM-dd");
            serv.FechaRetenc = Convert.ToDateTime(fechaRetencion.Text).ToString("yyyy-MM-dd");
            serv.FechaVenc = Convert.ToDateTime(fechaVenc.Text).ToString("yyyy-MM-dd");
            serv.FechaRecepcion = Convert.ToDateTime(fechaRecepcion.Text).ToString("yyyy-MM-dd");
            serv.MontoDocum = txtMontoTotal.Text.Replace(",", ".");
            serv.MontoBase = txtSubTotal.Text.Replace(",", ".");
            serv.Doc_base_gn = txtGN.Text.Replace(",", ".");
            serv.Doc_base_rd = txtRD.Text.Replace(",", ".");
            serv.Doc_base_ex = txtEX.Text.Replace(",", ".");
            serv.Doc_iva_gn = txtIvaGN.Text.Replace(",", ".");
            serv.Doc_iva_rd = txtIvaRD.Text.Replace(",", ".");
            serv.PorcIva = ivaGn.Replace(",", ".");
            serv.PorcIva2 = ivaRd.Replace(",", ".");
            serv.MontoIva = "" + Convert.ToString((Convert.ToDecimal(txtIvaGN.Text) + Convert.ToDecimal(txtIvaRD.Text))).Replace(",", ".");
            serv.PorcRetenc  = "" + porcReten.Replace(",", ".");
            serv.Origen = "SEVDOCPRO";
            serv.Dif_Inv = txtDiferencia.Text.Replace(",", ".");
            serv.Pro_nombreProve = lblNombrePro.Text;
            serv.Pro_rifProve = lblRifProveedor.Text;
            serv.MontoRetenc = "0.00";
            serv.PorcRetenc = porcReten.Replace(",", ".");
            serv.Correlativo = serv.Tdc_correlativo;
            serv.BaseImponible = "" + Convert.ToString((Convert.ToDecimal(txtGN.Text) + Convert.ToDecimal(txtRD.Text))).Replace(",", ".");
            serv.Recibidor = txtRecibidor.Text;
            serv.Login = usuario;
            serv.LoginUpd = usuario;
            serv.Sstatus = Convert.ToString(status);

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
            fila[3] = Convert.ToString(Math.Abs(Convert.ToDecimal(txtDebito.Text))).Replace(",", ".");
            fila[4] = Convert.ToString(Math.Abs(Convert.ToDecimal(txtCredito.Text))).Replace(",", ".");
            fila[5] = "1";
            fila[6] = usuario;
            fila[7] = usuario;
            fila[8] = compania.BaseDatosActual;
            fila[9] = "SERV";
            fila[10] = "" + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString();
            fila[11] = "" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString().PadLeft(2, '0') + "-" + DateTime.Now.Day.ToString();
            fila[12] = "";

            tablaCabacera.Rows.Add(fila);

            return tablaCabacera;
        }

        private DataTable contenidoMovContab()
        {
            tablaMovcontab = movContab.armarDataTableMovContab();
            int items = 0;


            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                
                items = items + 1;
                DataRow fila = tablaMovcontab.NewRow();

                fila[0] = items;
                fila[1] = txtProveedor.Text;
                fila[2] = serv.NroDocum; //recibo
                fila[3] = serv.SufiDocum;
                fila[4] = txtTipoDoc.Text;
                fila[5] = "" + comprobante;            //COMPROBANTE
                fila[6] = dataGridView1.Rows[i].Cells[1].Value;
                fila[7] = dataGridView1.Rows[i].Cells[2].Value.ToString().Substring(0, 9); //mayor proveedor                              //cc.CuentaProveedor;//CODIGO CUENTA
                fila[8] = dataGridView1.Rows[i].Cells[7].Value;
                fila[9] = "";                                   //AQUI NO VA UN COÑO
                fila[10] = "" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString().PadLeft(2, '0') + "-" + DateTime.Now.Day.ToString();
                fila[11] = "" + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString();//me da lala esta parte copy and paste                                          //hora
                fila[12] = dataGridView1.Rows[i].Cells[6].Value.ToString().Replace(",", ".").Replace("-", "");//monto
                fila[13] = "1";
                fila[14] = usuario;
                fila[15] = "SERV";
                fila[16] = "" + dataGridView1.Rows[i].Cells[11].Value.ToString();                              //AUXILIAR  PROVEEDOR                                 
                fila[17] = txtProveedor.Text;                    //CODIGO
                fila[18] = lblNombrePro.Text;               //NOMBRE                      
                fila[19] = dataGridView1.Rows[i].Cells[3].Value.ToString().Substring(0, 1);
                fila[20] = "";
                fila[21] = ""; // interno
                fila[22] = ""; // interno

                tablaMovcontab.Rows.Add(fila);
            }

            return tablaMovcontab;
        }

        private void armarDataTableLibro()
        {
            
            saldoLibro.Reset();

            saldoLibro.Columns.Add("proveedor");        //1
            saldoLibro.Columns.Add("documento");        //2
            saldoLibro.Columns.Add("sufijo");           //3
            saldoLibro.Columns.Add("control");          //4
            saldoLibro.Columns.Add("docAfec");          //5
            saldoLibro.Columns.Add("sufijoAfec");       //6
            saldoLibro.Columns.Add("controlAfec");      //7
            saldoLibro.Columns.Add("tipoDoc");          //8
            saldoLibro.Columns.Add("concepto");         //9
            saldoLibro.Columns.Add("fechaE");           //10
            saldoLibro.Columns.Add("fechaR");           //11
            saldoLibro.Columns.Add("fechaL");           //12
            saldoLibro.Columns.Add("fechaRet");         //13
            saldoLibro.Columns.Add("porcRet1");         //14
            saldoLibro.Columns.Add("porcRet2");         //15
            saldoLibro.Columns.Add("montoBase");        //16
            saldoLibro.Columns.Add("montoDoc");         //17
            saldoLibro.Columns.Add("baseGn");           //18
            saldoLibro.Columns.Add("baseRd");           //19
            saldoLibro.Columns.Add("baseEx");           //20
            saldoLibro.Columns.Add("ivaGn");            //21
            saldoLibro.Columns.Add("ivaRd");            //22
            saldoLibro.Columns.Add("montoIva");         //23
            saldoLibro.Columns.Add("montoReten1");      //24
            saldoLibro.Columns.Add("montoReten2");      //25
            saldoLibro.Columns.Add("porcRet");          //26
            saldoLibro.Columns.Add("NroRetenc");        //27
            saldoLibro.Columns.Add("PeriodoFiscal");    //28
            saldoLibro.Columns.Add("statusLibro");      //29
            saldoLibro.Columns.Add("statusRet");        //30
            saldoLibro.Columns.Add("login");            //31
            saldoLibro.Columns.Add("loginUpd");         //32
            saldoLibro.Columns.Add("origen");           //33
            saldoLibro.Columns.Add("cantImpresiones");  //34
            saldoLibro.Columns.Add("baseImport");       //35
            saldoLibro.Columns.Add("ivaImport");        //36
            saldoLibro.Columns.Add("dif");              //37
            saldoLibro.Columns.Add("correlativo");      //38
            saldoLibro.Columns.Add("fechaRet");         //39
            saldoLibro.Columns.Add("fechaEnt");         //40
            saldoLibro.Columns.Add("userEnt");          //41
            saldoLibro.Columns.Add("proNombre");        //42
            saldoLibro.Columns.Add("proRif");           //43

        }

        private void porRetenProveedor()
        {
            porcReten = cproveedores.porcRetenPro(txtProveedor.Text);
        }

        private void btnTemporal_Click(object sender, EventArgs e)
        {
            porRetenProveedor();
            asignandoValores(1);


            if (opcion.Equals("2"))
            {
                
                movConta.eliminarMovContab(txtProveedor.Text, docOrigen, sufOrigen, tipoOrigen);
                movConta.eliminarMovContab(txtProveedor.Text, serv.NroDocum, serv.SufiDocum, serv.IdTipoDoc);
                contenidoMovContab();
                movConta.ingresarMovContab_temp(tablaMovcontab);
                serv.actualizarDocPro(docu, sufi, tipo);
                panel1.Visible = false;

                refLblxServ.refrescar();
                this.Close();
                
            }
            else
            {
                if (verificarDoc("2"))
                {
                    contenidoMovContab();
                    movConta.ingresarMovContab_temp(tablaMovcontab);
                    serv.ingresarDocPro(2);
                    panel1.Visible = false;
                    this.Close();
                }
                else
                {
                    panel1.Visible = false;
                }
            }
        }

        private void cmbCodicionPago_SelectedIndexChanged(object sender, EventArgs e)
        {
            diasCondicionPago(); 
        }

        private void diasCondicionPago()
        {
            tabla = pago.codCondicionPago(cmbCodicionPago.SelectedValue.ToString());

            if (tabla.Rows.Count > 0)
            {
                serv.DiasCondicionPago = tabla.Rows[0][0].ToString();
            }
        }

        private void txtDescripcion_Leave(object sender, EventArgs e)
        {
            txtDescripcionMov.Text = txtDescripcion.Text;
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            //if (dataGridView1.CurrentRow.Index > 1)
            //{
            //    dataGridView1.Rows.RemoveAt(dataGridView1.CurrentRow.Index);
            //}


            if (dataGridView1.CurrentRow.Cells[1].Value.ToString() != "Iva General" && dataGridView1.CurrentRow.Cells[1].Value.ToString() != "Monto Total")
            {
                dataGridView1.Rows.RemoveAt(dataGridView1.CurrentRow.Index);
            }

            actualizarNumeroItems();
            calcularMontos();
        }

        private void lblNombrePro_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
        }

        private void actualizarNumeroItems()
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                dataGridView1.Rows[i].Cells[0].Value = i + 1;
            }
        }

        public void obtenerUsuario(string login)
        {
            usuario = login;
        }

        private void cargarModificar()
        {
            tabla = serv.cargarCabeceraModificar(docu, sufi, provee, tipo, caso);

            if (tabla.Rows.Count > 0)
            {
                if (serv.Tdc_pagos.Equals("1"))
                {
                    txtDCOM.Text = tabla.Rows[0]["doc_numerodoc"].ToString();
                    txtSufDCOM.Text = tabla.Rows[0]["doc_sufijo"].ToString();
                    txtControlDCOM.Text = tabla.Rows[0]["doc_numcontrol"].ToString();

                    txtFCOM.Text = tabla.Rows[0]["doc_facafe"].ToString();
                    txtSufFCOM.Text = tabla.Rows[0]["doc_sufijofacafe"].ToString();
                    txtControlFCOM.Text = tabla.Rows[0]["doc_numfis"].ToString();

                    txtDCOM.Visible = true;
                    txtSufDCOM.Visible = true;
                    txtControlDCOM.Visible = true;

                    label3.Visible = true;
                    label6.Visible = true;
                }

                if (serv.Tdc_pagos.Equals("-1"))
                {
                    txtFCOM.Text = tabla.Rows[0]["doc_numerodoc"].ToString();
                    txtSufFCOM.Text = tabla.Rows[0]["doc_sufijo"].ToString();
                    txtControlFCOM.Text = tabla.Rows[0]["doc_numcontrol"].ToString();

                    txtDCOM.Visible= false;
                    txtSufDCOM.Visible = false;
                    txtControlDCOM.Visible = false;

                    label3.Visible = false;
                    label6.Visible = false;

                }

                txtTipoDoc.Text = tabla.Rows[0]["doc_tipdoc"].ToString();
                txtProveedor.Text = tabla.Rows[0]["doc_codigo"].ToString();
                lblNombrePro.Text = tabla.Rows[0]["pro_nombre"].ToString();
                lblRifProveedor.Text = tabla.Rows[0]["pro_rif"].ToString();
                txtDescripcion.Text = tabla.Rows[0]["doc_concep"].ToString();
                txtMontoTotal.Text = tabla.Rows[0]["doc_neto"].ToString();
                txtSubTotal.Text = tabla.Rows[0]["doc_subtot"].ToString();
                txtGN.Text = tabla.Rows[0]["doc_base_gn"].ToString();
                txtRD.Text = tabla.Rows[0]["doc_base_rd"].ToString();
                txtEX.Text = tabla.Rows[0]["doc_base_ex"].ToString();
                txtIvaGN.Text = tabla.Rows[0]["doc_iva_gn"].ToString();
                txtIvaRD.Text = tabla.Rows[0]["doc_iva_rd"].ToString();
                fechaEmision.Text = tabla.Rows[0]["doc_fechEmis"].ToString();
                fechaLibro.Text = tabla.Rows[0]["doc_fechLCompra"].ToString();
                fechaRecepcion.Text = tabla.Rows[0]["doc_fechRecep"].ToString();
                fechaRetencion.Text = tabla.Rows[0]["doc_fecReten"].ToString();
                fechaVenc.Text = tabla.Rows[0]["doc_fechven"].ToString();
                txtIvaTotal.Text = tabla.Rows[0]["doc_mtoiva"].ToString(); 

            }

            tabla.Clear();
            tabla.Reset();

            if (txtDCOM.Visible == true)
            {
                tabla = serv.armarParteContable(txtDCOM.Text, txtSufDCOM.Text, txtProveedor.Text, txtTipoDoc.Text, Convert.ToInt16(opcion), compania.BaseDatosActual, caso);

                for (int i = 0; i < tabla.Rows.Count; i++)
                {
                    
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[i].Cells[0].Value = tabla.Rows[i][0].ToString();
                    dataGridView1.Rows[i].Cells[1].Value = tabla.Rows[i][6].ToString();
                    dataGridView1.Rows[i].Cells[2].Value = tabla.Rows[i][7].ToString() + " - " + tabla.Rows[i][22].ToString();

                    if (tabla.Rows[i][8].ToString().Equals("-1"))
                    {
                        dataGridView1.Rows[i].Cells[3].Value = "CREDITO";
                        dataGridView1.Rows[i].Cells[5].Value = tabla.Rows[i][12].ToString();
                    }
                    else
                    {
                        dataGridView1.Rows[i].Cells[3].Value = "DEBITO";
                        dataGridView1.Rows[i].Cells[4].Value = tabla.Rows[i][12].ToString();
                    }

                    dataGridView1.Rows[i].Cells[6].Value = tabla.Rows[i][12].ToString();
                    dataGridView1.Rows[i].Cells[7].Value = tabla.Rows[i][8].ToString();
                    dataGridView1.Rows[i].Cells[10].Value = tabla.Rows[i][16].ToString();
                    dataGridView1.Rows[i].Cells[11].Value = tabla.Rows[i][18].ToString();
                }
            }
            else
            {
                tabla = serv.armarParteContable(txtFCOM.Text, txtSufFCOM.Text, txtProveedor.Text, txtTipoDoc.Text, Convert.ToInt16(opcion), compania.BaseDatosActual, caso);

                for (int i = 0; i < tabla.Rows.Count; i++)
                {
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[i].Cells[0].Value = tabla.Rows[i][0].ToString();
                    dataGridView1.Rows[i].Cells[1].Value = tabla.Rows[i][6].ToString();
                    dataGridView1.Rows[i].Cells[2].Value = tabla.Rows[i][7].ToString() + " - " + tabla.Rows[i][22].ToString();

                    if (tabla.Rows[i][8].ToString().Equals("-1"))
                    {
                        dataGridView1.Rows[i].Cells[3].Value = "CREDITO";
                        dataGridView1.Rows[i].Cells[5].Value = tabla.Rows[i][12].ToString();
                    }
                    else
                    {
                        dataGridView1.Rows[i].Cells[3].Value = "DEBITO";
                        dataGridView1.Rows[i].Cells[4].Value = tabla.Rows[i][12].ToString();
                    }

                    dataGridView1.Rows[i].Cells[6].Value = tabla.Rows[i][12].ToString();
                    dataGridView1.Rows[i].Cells[7].Value = tabla.Rows[i][8].ToString();
                    dataGridView1.Rows[i].Cells[10].Value = tabla.Rows[i][16].ToString();
                    dataGridView1.Rows[i].Cells[11].Value = tabla.Rows[i][18].ToString();
                }
            }
        }

        private void bloquearControles()
        {
            txtTipoDoc.Enabled = false;
            txtProveedor.Enabled = false;
            txtDCOM.Enabled = false;
            txtSufDCOM.Enabled = false;
            txtControlDCOM.Enabled = false;
            txtFCOM.Enabled = false;
            txtSufFCOM.Enabled = false;
            txtControlFCOM.Enabled = false;
            cmbCodicionPago.Enabled = false;
            txtDescripcion.Enabled = false;

            button1.Enabled = false;
            button2.Enabled = false;

            txtTipoDoc.BackColor = Color.White;
            txtProveedor.BackColor = Color.White;
            txtDCOM.BackColor = Color.White;
            txtSufDCOM.BackColor = Color.White;
            txtControlDCOM.BackColor = Color.White;
            txtFCOM.BackColor = Color.White;
            txtSufFCOM.BackColor = Color.White;
            txtControlFCOM.BackColor = Color.White;
            cmbCodicionPago.BackColor = Color.White;
            txtDescripcion.BackColor = Color.White;


            fechaEmision.Enabled = false;
            fechaLibro.Enabled = false;
            fechaRecepcion.Enabled = false;
            fechaRetencion.Enabled = false;
            fechaVenc.Enabled = false;

            fechaEmision.BackColor = Color.White;
            fechaLibro.BackColor = Color.White;
            fechaRecepcion.BackColor = Color.White;
            fechaRetencion.BackColor = Color.White;
            fechaVenc.BackColor = Color.White;

            txtMontoTotal.Enabled = false;
            txtIvaTotal.Enabled = false;
            txtSubTotal.Enabled = false;
            txtGN.Enabled = false;
            txtRD.Enabled = false;
            txtIvaRD.Enabled = false;
            txtEX.Enabled = false;
            txtIvaGN.Enabled = false;
            txtIvaRD.Enabled = false;

            txtMontoTotal.BackColor = Color.White;
            txtIvaTotal.BackColor = Color.White;
            txtSubTotal.BackColor = Color.White;
            txtGN.BackColor = Color.White;
            txtRD.BackColor = Color.White;
            txtIvaRD.BackColor = Color.White;
            txtEX.BackColor = Color.White;
            txtIvaGN.BackColor = Color.White;
            txtIvaRD.BackColor = Color.White;

            txtCuenta.Enabled = false;
            txtAuxiliar.Enabled = false;
            txtDescripcionMov.Enabled = false;
            txtMontoMov.Enabled = false;

            txtCuenta.BackColor = Color.White;
            txtAuxiliar.BackColor = Color.White;
            txtDescripcionMov.BackColor = Color.White;
            txtMontoMov.BackColor = Color.White;

            btnBuscarCuenta.Enabled = false;
            btnAuxiliar.Enabled = false;

            //btnAceptar.Enabled = false;
            btnAceptar.Enabled = true;
            btnTemporal.Enabled = false;

            btnAgregar.Enabled = false;
            btnEliminar.Enabled = false;

            txtCentroCosto.Enabled = false;
            txtTransporte.Enabled = false;
            txtPlanilla.Enabled = false;
            txtRecibidor.Enabled = false;
            txtDivisa.Enabled = false;
            txtCambio.Enabled = false;

            btnCentroCosto.Enabled = false;
            btnTransporte.Enabled = false;
        }

        //private void obtenerCuentasPro(string tipoDoc)
        //{
        //    tablaB.Reset();
        //    tablaB.Clear();
        //    tablaB = clasePro.obtenerCuentasproveedor(txtProveedor.Text, tipoDoc);

        //    if (tablaB.Rows.Count > 0)
        //    {
        //        mayorProveedor = tablaB.Rows[0][0].ToString();
        //        auxiliarProveedor = tablaB.Rows[0][1].ToString();
        //    }
        //}

        private void obtenerComprobanteContable()
        {
            tablaB.Reset();
            tablaB.Clear();
            compania.obtenerCodigoCompaniaActual();
            tablaB = movContab.obtenerNumeroComprobante(compania.BaseDatosActual, "04");

            comprobante = tablaB.Rows[0][1].ToString();
        }

        private bool verificarDoc(string opcion)
        {
            if (txtDCOM.Visible == true)
            {

                if (txtDCOM.Text != "" && txtSufDCOM.Text != "")
                {
                    if (serv.documentoExiste(txtProveedor.Text, txtDCOM.Text, txtSufDCOM.Text,opcion))
                    {
                        MessageBox.Show("El número de documento ya existe, agregue un sufijo", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtSufDCOM.Focus();
                        return false;
                    }
                }

                if (txtFCOM.Text != "" && txtFCOM.Text != "")
                {
                    if (!serv.documentoAplicaExiste(txtProveedor.Text, txtFCOM.Text, txtSufFCOM.Text))
                    {
                        MessageBox.Show("El número de documento no existe o no esta cerrado", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return false;
                    }
                }
                
            }

            if (txtDCOM.Visible == false && txtFCOM.Text != "" && txtSufFCOM.Text != "")
            {
                if (serv.documentoExiste(txtProveedor.Text, txtFCOM.Text, txtSufFCOM.Text, opcion))
                {
                    MessageBox.Show("El numero de documento ya existe, agregue un sufijo", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtSufFCOM.Focus();
                    return false;
                }
            }

            return true;
        }

        private void txtDCOM_Leave(object sender, EventArgs e)
        {
            verificarDoc("1");
        }

        private void txtSufDCOM_Leave(object sender, EventArgs e)
        {
            if (txtSufDCOM.Text == "")
            {
                txtSufDCOM.Text = "0";
                verificarDoc("1");
            }
            verificarDoc("1");
        }

        private void txtFCOM_Leave(object sender, EventArgs e)
        {
            if (txtDCOM.Visible == true)
            {
                verificarDoc("1");
            }

            verificarDoc("1");

        }

        private void txtSufFCOM_Leave(object sender, EventArgs e)
        {
            if (txtSufFCOM.Text == "")
            {
                txtSufFCOM.Text = "0";
                verificarDoc("1");
            }
            
            if (txtDCOM.Visible == true)
            {
                verificarDoc("1");
            }
            verificarDoc("1");
        }

        public void refrescarLbx(Form lbxServicios)
        {
            refLblxServ = (lbxServicios)lbxServicios;
            
        }

        private void txtDiferencia_TextChanged(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(txtDiferencia.Text) != 0)
            {
                txtDiferencia.ForeColor = Color.Red;
            }
            else
            {
                txtDiferencia.ForeColor = Color.Black;
            }
        }

        private void txtEX_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtCuenta.Focus();
                return;
            }

            TextBox a = (TextBox)sender;
            ftextbox.conDecimalesDefinidosNegativo(sender, e, a, 2);

        }

        private void guardarISLR()
        {
            consultarUniTributaria();

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                tarifaReten = "";
                if (dataGridView1.Rows[i].Cells[10].Value.ToString() != "")
                {
                    consultarTarifasReten(dataGridView1.Rows[i].Cells[10].Value.ToString());

                    if (tarifaReten == "T1")
                    {
                        //if (Convert.ToDecimal(txtSubTotal.Text) > Convert.ToDecimal(montoMinimo))
                        //{
                        if (Convert.ToDecimal(dataGridView1.Rows[i].Cells[6].Value.ToString()) > Convert.ToDecimal(montoMinimo))
                        {

                            baseRetener = "" + (Convert.ToDecimal(dataGridView1.Rows[i].Cells[6].Value.ToString()) * Convert.ToDecimal(porRetenISLR) / 100);

                            retISLR.insertarLibroISLR(retISLR.consultartarifasRet(dataGridView1.Rows[i].Cells[10].Value.ToString()),
                                serv, baseRetener, unidadTributaria, dataGridView1.Rows[i].Cells[6].Value.ToString());
                              
                        }
                    }
                }
            }
        }

        private void consultarUniTributaria()
        {
            tabla = retISLR.consultarUTSustraendo();

            if (tabla.Rows.Count > 0)
            {
                unidadTributaria = tabla.Rows[0][0].ToString();
                sustraendo = tabla.Rows[0][1].ToString();
            }
        }

        private void consultarTarifasReten(string codigo)
        {
            tabla = retISLR.consultartarifasRet(codigo);

            if (tabla.Rows.Count > 0)
            {
                montoMinimo = tabla.Rows[0][10].ToString();
                porRetenISLR = tabla.Rows[0][8].ToString();
                tarifaReten = tabla.Rows[0][13].ToString();
            }
            
        }
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentRow.Cells[1].Value.ToString() != "Iva General" && dataGridView1.CurrentRow.Cells[1].Value.ToString() != "Monto Total")
            {
                if (dataGridView1.CurrentRow.Cells[10].Selected)
                {
                    lbxTarifaReten = new lbxTarifaRetenc(lblRifProveedor.Text, txtProveedor.Text, "Servicios");
                    lbxTarifaReten.destino(this, "Servicios");
                    lbxTarifaReten.Show();
                }
            }
        }

        private void txtSubTotal_Leave(object sender, EventArgs e)
        {
            if (txtSubTotal.Text == "")
            {
                txtSubTotal.Text = "0";
            }
        }

        private void txtIvaGN_Leave(object sender, EventArgs e)
        {
            if (txtIvaGN.Text == "")
            {
                txtIvaGN.Text = "0";
            }
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Right)
            {
                dataGridView1.CurrentRow.Cells[10].Value = "";
            }
        }

        //


        private DataTable contenidoMovContabISLR2()
        {

            islr1 = 0;

            tablaMovcontab = movContab.armarDataTableMovContab();
            int items = 0;


            for (int i = 0; i < dataGridView1.RowCount; i++)
            {

                if (dataGridView1.Rows[i].Cells[10].Value.ToString() != "")
                {

                    items = items + 1;
                    DataRow fila = tablaMovcontab.NewRow();

                    fila[0] = items;
                    fila[1] = txtProveedor.Text;
                    fila[2] = serv.NroDocum; //recibo
                    fila[3] = serv.SufiDocum;
                    fila[4] = txtTipoDoc.Text;
                    fila[5] = "" + comprobante;            //COMPROBANTE
                    fila[6] = dataGridView1.Rows[i].Cells[1].Value;
                    fila[7] = dataGridView1.Rows[i].Cells[2].Value.ToString().Substring(0, 9); //mayor proveedor                              //cc.CuentaProveedor;//CODIGO CUENTA
                    fila[8] = dataGridView1.Rows[i].Cells[7].Value;
                    fila[9] = "";                                   //AQUI NO VA UN COÑO
                    fila[10] = "" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString().PadLeft(2, '0') + "-" + DateTime.Now.Day.ToString();
                    fila[11] = "" + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString();//me da lala esta parte copy and paste                                          //hora
                    fila[12] = dataGridView1.Rows[i].Cells[6].Value.ToString().Replace(",", ".").Replace("-", "");//monto
                    fila[13] = "1";
                    fila[14] = usuario;
                    fila[15] = "SERV";
                    fila[16] = "" + dataGridView1.Rows[i].Cells[11].Value.ToString();                              //AUXILIAR  PROVEEDOR                                 
                    fila[17] = txtProveedor.Text;                    //CODIGO
                    fila[18] = lblNombrePro.Text;               //NOMBRE                      
                    fila[19] = dataGridView1.Rows[i].Cells[3].Value.ToString().Substring(0, 1);
                    fila[20] = "";
                    fila[21] = ""; // interno
                    fila[22] = ""; // interno

                    tablaMovcontab.Rows.Add(fila);

                    islr1 = islr1 + Convert.ToDecimal(dataGridView1.Rows[i].Cells[6].Value.ToString());
                }
            }

            return tablaMovcontab;
        }


        private DataTable contenidoCabeceraISLR()
        {

            tablaCabacera.Clear();
            tablaCabacera.Reset();
            tablaCabacera = movContab.armarDataTableCabeceraMovContab();

            DataRow fila = tablaCabacera.NewRow();

            fila[0] = comprobante;
            fila[1] = "" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString().PadLeft(2, '0') + "-" + DateTime.Now.Day.ToString();
            fila[2] = compania.BaseDatosActual;
            fila[3] = Convert.ToString(Math.Abs(Convert.ToDecimal(islr1))).Replace(",", "."); //cambiar 
            fila[4] = Convert.ToString(Math.Abs(Convert.ToDecimal(islr1))).Replace(",", "."); //cambiar 
            fila[5] = "1";
            fila[6] = usuario;
            fila[7] = usuario;
            fila[8] = compania.BaseDatosActual;
            fila[9] = "SERV";
            fila[10] = "" + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString();
            fila[11] = "" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString().PadLeft(2, '0') + "-" + DateTime.Now.Day.ToString();
            fila[12] = "";

            tablaCabacera.Rows.Add(fila);

            return tablaCabacera;
        }

        private void asignandoValoresISLR()
        {
            retISLR.NroDocum = serv.NroDocum;
            retISLR.SufiDocum = serv.SufiDocum;
            retISLR.IdProveedor = txtProveedor.Text;
            retISLR.IdTipoDoc = txtTipoDoc.Text;
            retISLR.Login = usuario;
        }

        private void fechaRecepcion_Validating(object sender, CancelEventArgs e)
        {
            if (fechaRecepcion.Value.CompareTo(this.fechaEmision.Value) == -1) 
            {
                MessageBox.Show("Fecha De Recepcion Es Anterio a la Fecha de Emicion", "Informacion ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                fechaRecepcion.Value = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy"));
                fechaRecepcion.Focus();
            }
        }

        private void fechaVenc_Validating(object sender, CancelEventArgs e)
        {
            if (fechaVenc.Value.CompareTo(this.fechaEmision.Value) == -1)
            {
                MessageBox.Show("Fecha De Vencimiento Es Anterio a la Fecha de Emicion", "Informacion ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                fechaVenc.Value = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy"));
                fechaVenc.Focus();
            }
        }

        private void fechaEmision_Validating(object sender, CancelEventArgs e)
        {
            DateTime Hoy = DateTime.Today;
            if (fechaEmision.Value.CompareTo(Hoy) != 0 && fechaEmision.Value.CompareTo(Hoy) != -1)
            {
                MessageBox.Show("Fecha De Emision Es Mayor a la Fecha de a la fecha actual", "Informacion ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                fechaEmision.Value = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy"));
                fechaEmision.Focus();
            }
        }

       













    }
}
