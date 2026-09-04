using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using lebrun.clases.importaciones;
using lebrun.formularios.importaciones;
using lebrun.Reportes;
using lebrun.clasesData;


namespace lebrun.formularios.importaciones
{
    public partial class frmImportaciones : Form
    {
        private frmFacturaNacionalImportacion facturaNacionalPanel1;
        private frmProductosImportacion productosPanel;
        private frmFacturaImportacion facturaOrigenPanel1;
        private frmOtrosDatos otrosDatosPanel;
        private lbxImportaciones refLbxImportaciones;  //para refrescar lbxImportaciones
        private Compania compania;

        //private frmFacturaNacionalImportacion facturaNacionalPanel1;
        private frmOtrosGastos otrosGastosPanel;
        private lbxOrdenDeCompra lbxOrdenImportar;
        private static frmImportaciones m_FormDefInstance;
        private factores ultimosFactores;
        private decimal diferencia;
        private frmReporte reporteImpor;
        private Principal Principal;
        private string usuaLogin;
        private string usuarioOrigen;

        //private OrdenDeCompra importarOrden;
        //private DetallesOrdenDeCompra detallesImportarOrden;
        private decimal totalGastos;
        private decimal gastoSinDereImportacion;
        public string numeroOrden;
        private Importacion importacion;
        private DataTable gastosImportaciones;
        private CrystalReport1 rptImportacion;

        private string factorGasto;
        private string factorCif;
        private string factorFin;
        private string factorOtros;
        private string fecha;
        private int proyeccion=0;
        private string totalImport;
        private string baseImport;
        private string ultDoc, ultSuf, ultProve;

        //0       4       2      6    9       9       4   5     5     0     8     B        a   n      a     v     i   h 
        
        public frmImportaciones()
        {
            InitializeComponent();
            facturaNacionalPanel1 = new frmFacturaNacionalImportacion();
            productosPanel = new frmProductosImportacion();
            facturaOrigenPanel1 = new frmFacturaImportacion();
            otrosGastosPanel = new frmOtrosGastos();
            otrosDatosPanel = new frmOtrosDatos();
            importacion = new Importacion();
            compania = new Compania();
            //refLbxImportaciones = new lbxImportaciones();
            
        }

        private void frmImportaciones_Load(object sender, EventArgs e)
        {
      
                productosPanel.TopLevel = false;
                productosPanel.FormBorderStyle = FormBorderStyle.None;
                productosPanel.Dock = DockStyle.Fill;
                this.panel1.Controls.Add(productosPanel);
                this.panel1.Tag = productosPanel;
                productosPanel.Show();

                facturaNacionalPanel1.TopLevel = false;
                facturaNacionalPanel1.FormBorderStyle = FormBorderStyle.None;
                facturaNacionalPanel1.Dock = DockStyle.Fill;
                this.panel1.Controls.Add(facturaNacionalPanel1);
                this.panel1.Tag = facturaNacionalPanel1;

                facturaOrigenPanel1.TopLevel = false;
                facturaOrigenPanel1.FormBorderStyle = FormBorderStyle.None;
                facturaOrigenPanel1.Dock = DockStyle.Fill;
                this.panel1.Controls.Add(facturaOrigenPanel1);
                this.panel1.Tag = facturaOrigenPanel1;

                otrosGastosPanel.TopLevel = false;
                otrosGastosPanel.FormBorderStyle = FormBorderStyle.None;
                otrosGastosPanel.Dock = DockStyle.Fill;
                this.panel1.Controls.Add(otrosGastosPanel);
                this.panel1.Tag = otrosGastosPanel;

            //nuevo frm

                otrosDatosPanel.TopLevel = false;
                otrosDatosPanel.FormBorderStyle = FormBorderStyle.None;
                otrosDatosPanel.Dock = DockStyle.Fill;
                this.panel1.Controls.Add(otrosDatosPanel);
                this.panel1.Tag = otrosDatosPanel;

                Principal = (Principal)this.MdiParent;

                if (proyeccion == 1)
                {
                    btnFacOrigen_Click(sender, e);
                    bloquearProyeccion();
                }
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            if (txtNumeroOrden.Text != "")
            {
                //cambiar y pasar un datatable
                
                productosPanel.llenarGrid(txtNumeroOrden.Text);
                facturaOrigenPanel1.txtCambioOrden.Text = productosPanel.txtCambio.Text;
                facturaOrigenPanel1.lblNombrePro.Text = productosPanel.txtNombrePro.Text;
                facturaOrigenPanel1.lblRifPro.Text = productosPanel.txtRifPro.Text;
                facturaOrigenPanel1.txtDocumento.Text = productosPanel.varDocumento;
                facturaOrigenPanel1.txtSufijoDoc.Text = productosPanel.varSufijo;
                facturaOrigenPanel1.lblmoneda.Text = productosPanel.moneda;

                productosPanel.Visible = true;
                productosPanel.txtTotalProductos.Text = "";
                productosPanel.totalBs();
                productosPanel.totalArticulos();
                //crear funcion para pasar datos como el rif, documento, sufijo, nombre proveedor   
            }
            else {
                MessageBox.Show("Debe ingresar una orden de compra","Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);  
            }
        }

        private void btnImportar_Click(object sender, EventArgs e)
        {       
            lbxOrdenImportar = lbxOrdenDeCompra.DefInstance;
            lbxOrdenImportar.destino(this, this.Name);
            //lbxOrdenImportar.groupBox1.Enabled = true;
            lbxOrdenImportar.button1.Enabled = false;
            lbxOrdenImportar.button2.Enabled = false;
            lbxOrdenImportar.button3.Enabled = false;
            lbxOrdenImportar.ShowDialog();
            //lbxOrdenImportar.Show();
        }

        private void btnSalir_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnProductos_Click_1(object sender, EventArgs e)
        {
            if (facturaNacionalPanel1.Visible == true)
            {
                facturaNacionalPanel1.Hide();
            }
            if (facturaOrigenPanel1.Visible == true)
            {
                facturaOrigenPanel1.Hide();
            }
            productosPanel.Show();
        }

        private void btnGastos_Click_1(object sender, EventArgs e)
        {
            if (productosPanel.Visible == true)
            {
                productosPanel.Hide();
            }
            if (facturaOrigenPanel1.Visible == true)
            {
                facturaOrigenPanel1.Hide();
            }
            if (otrosDatosPanel.Visible == true)
            {
                otrosDatosPanel.Hide();
            }
            facturaNacionalPanel1.Show();
        }

        private void btnFacOrigen_Click(object sender, EventArgs e)
        {
            if (facturaNacionalPanel1.Visible == true)
            {
                facturaNacionalPanel1.Hide();
            }
            if (productosPanel.Visible == true)
            {
                productosPanel.Hide();
            }
            if (otrosDatosPanel.Visible == true)
            {
                otrosDatosPanel.Hide();
            }
            facturaOrigenPanel1.Show();
        }

        private void btnOtros_Click(object sender, EventArgs e)
        {
            if (facturaNacionalPanel1.Visible == true)
            {
                facturaNacionalPanel1.Hide();
            }
            if (productosPanel.Visible == true)
            {
                productosPanel.Hide();
            }
            if (facturaOrigenPanel1.Visible == true)
            {
                facturaOrigenPanel1.Hide();
            }
            if (otrosDatosPanel.Visible == true)
            {
                otrosDatosPanel.Hide();
            }
            otrosGastosPanel.Show();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (facturaNacionalPanel1.Visible == true)
            {
                facturaNacionalPanel1.Hide();
            }
            if (productosPanel.Visible == true)
            {
                productosPanel.Hide();
            }
            if (facturaOrigenPanel1.Visible == true)
            {
                facturaOrigenPanel1.Hide();
            }
            if (otrosGastosPanel.Visible == true)
            {
                otrosGastosPanel.Hide();
            }
            otrosDatosPanel.Show();
        }

        private void btnMsj_Click(object sender, EventArgs e)
        {

            if (verificarValores())
            {

                if (!productosPanel.validarArancel())
                {
                    MessageBox.Show("Uno o mas articulos no tiene el arancel asignado", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    //productosPanel.calcularC(); 
                    facturaOrigenPanel1.calcularFOB();
                    //otrosGastosPanel.CalcularTotalGastos();
                    facturaNacionalPanel1.calcularSinDerechoImportacion();

                    gastoSinDereImportacion = facturaNacionalPanel1.calcularSinDerechoImportacion();
                    totalGastos = otrosGastosPanel.CalcularTotalGastos();

                    otrosGastosPanel.txtFactorGastos.Text = "" + ((totalGastos + gastoSinDereImportacion) / facturaOrigenPanel1.totalBolivares);
                    otrosGastosPanel.txtFactorGastosImpor.Text = "" + (((totalGastos + gastoSinDereImportacion) / facturaOrigenPanel1.totalBolivares) + 1);

                    otrosGastosPanel.txtMasOtros.Text = "" + (totalGastos + gastoSinDereImportacion);
                    otrosGastosPanel.txtTotalGastos.Text = "" + (totalGastos + gastoSinDereImportacion + Convert.ToDecimal(facturaNacionalPanel1.textBox1.Text));
                    otrosGastosPanel.txtValorImportacion.Text = "" + (Convert.ToDecimal(otrosGastosPanel.txtTotalGastos.Text) + (facturaOrigenPanel1.totalBolivares));

                    productosPanel.factorCif.Text = facturaOrigenPanel1.txtFactorCIF.Text;
                    //productosPanel.txtCambio.Text = facturaOrigenPanel1.txtCambioLiquidar.Text;
                    productosPanel.txtCambio.Text = facturaOrigenPanel1.txtCambioProducto.Text;
                    productosPanel.factorGasto.Text = otrosGastosPanel.txtFactorGastos.Text;
                    productosPanel.calcularC();

                    //facturaNacionalPanel1.contenidoArrayMovContab(); //comentado por pruebas
                    facturaNacionalPanel1.armarArray(); //se armara una sola vez el arreglo del textbox
                    facturaNacionalPanel1.contenidoArrayGastos(facturaOrigenPanel1.txtDocumento.Text, facturaOrigenPanel1.txtSufijoDoc.Text);
                    //facturaNacionalPanel1.asignandoValores();
                    //facturaNacionalPanel1.armarDataTable();
                }
            }
        }

        private void cargarCabeceraImportacion(string doc, string suf, int opc, string prove) {

            importacion.cargarCabeceraImportacion(doc, suf, opc,prove);

            facturaOrigenPanel1.txtCambioLiquidar.Text = importacion.CambioLiqui;
            facturaOrigenPanel1.txtCambioOrden.Text = importacion.CambioOc;
            facturaOrigenPanel1.txtCambioProducto.Text = importacion.CambioProducto;
            facturaOrigenPanel1.txtDocumento.Text = importacion.Documento;
            facturaOrigenPanel1.txtSufijoDoc.Text = importacion.Sufijo;
            facturaOrigenPanel1.txtFactorCIF.Text = importacion.FactorCif;
            facturaOrigenPanel1.txtNetoFOB.Text = importacion.MontoFob;
            facturaOrigenPanel1.txtNetoBs.Text = importacion.MontoBs;
            facturaOrigenPanel1.txtSeguro.Text = importacion.SeguroFob;
            facturaOrigenPanel1.txtSeguroBs.Text = importacion.SeguroBs;
            facturaOrigenPanel1.txtFlete.Text = importacion.FleteFob;
            facturaOrigenPanel1.txtCostoMaterial.Text = importacion.CostoMaterialFob;
            facturaOrigenPanel1.txtCostoMaterialBs.Text = importacion.CostoMaterialBs;
            facturaOrigenPanel1.txtFleteBs.Text = importacion.FleteBs;
            facturaOrigenPanel1.txtExtra1.Text = importacion.Otros1Fob;
            facturaOrigenPanel1.txtExtra1Bs.Text = importacion.Otros1Bs;
            facturaOrigenPanel1.txtExtra2.Text = importacion.Otros2Fob;
            facturaOrigenPanel1.txtExtra2Bs.Text = importacion.Otros2Bs;
            facturaOrigenPanel1.txtFactorCIF.Text = importacion.FactorCif;
            facturaOrigenPanel1.txtTotalCIF.Text = importacion.TotalCif;
            facturaOrigenPanel1.txtCostoCIF.Text = importacion.CostoCif;
            facturaOrigenPanel1.lblNombrePro.Text = importacion.Pro_nombre;
            facturaOrigenPanel1.lblRifPro.Text = importacion.Pro_rif;
            facturaOrigenPanel1.lblmoneda.Text = importacion.Moneda;
            facturaOrigenPanel1.txtIvaImportacion.Text = importacion.IvaImportacion;
            facturaOrigenPanel1.txtControlImpor.Text = importacion.ControlImportacion;
            facturaOrigenPanel1.txtExentoImport.Text = importacion.MontoExenImport;
            facturaOrigenPanel1.lblCodigoProveeImpor.Text = importacion.CodProveedor;
            facturaOrigenPanel1.fechEmiFacImport.Value = Convert.ToDateTime(importacion.Fechaimport);

            facturaNacionalPanel1.txtIvaNacional.Text = importacion.IvaNacional;
            facturaNacionalPanel1.txtNumeroFactura.Text = importacion.FactNacional;
            facturaNacionalPanel1.txtNumeroControl.Text = importacion.ControlNacional;
            facturaNacionalPanel1.txtSufijoNacional.Text = importacion.SufiNacional;
            facturaNacionalPanel1.txtMontoBase.Text = importacion.BaseNacional;
            facturaNacionalPanel1.txtMontoExento.Text = importacion.ExentoNacional;
            facturaNacionalPanel1.txtTotalNacional.Text = importacion.TotalNacional;
            facturaNacionalPanel1.txtCodigoProveedor.Text = importacion.ProveNacional;

            facturaNacionalPanel1.txtIvaNacional.Text = string.Format("{0:0.00}", Convert.ToDecimal(facturaNacionalPanel1.txtIvaNacional.Text));
            facturaNacionalPanel1.txtMontoBase.Text = string.Format("{0:0.00}", Convert.ToDecimal(facturaNacionalPanel1.txtMontoBase.Text));
            facturaNacionalPanel1.txtMontoExento.Text = string.Format("{0:0.00}", Convert.ToDecimal(facturaNacionalPanel1.txtMontoExento.Text));
            facturaNacionalPanel1.txtTotalNacional.Text = string.Format("{0:0.00}", Convert.ToDecimal(facturaNacionalPanel1.txtTotalNacional.Text));

            facturaNacionalPanel1.fechaEmisionNac.Value = Convert.ToDateTime(importacion.FechEmisionNacio);
            facturaNacionalPanel1.fechavencNacio.Value = Convert.ToDateTime(importacion.FechVencNacio);

            productosPanel.txtProveedor.Text = importacion.CodProveedor;

            otrosGastosPanel.txtVerificarMercancia.Text = importacion.VerificaMercan;
            otrosGastosPanel.txtHononarios.Text = importacion.Honorarios;
            otrosGastosPanel.txtTramiteCadivi.Text = importacion.GastoCadivi;
            otrosGastosPanel.txtSeguroMercancia.Text = importacion.SegurosMercancia;
            otrosGastosPanel.txtOtrosGastos.Text = importacion.OtrosGastos;
            otrosGastosPanel.txtMasOtros.Text = importacion.GastAduanaOtros;
            otrosGastosPanel.txtTotalGastos.Text = importacion.GastosGenerales;
            otrosGastosPanel.txtValorImportacion.Text = importacion.ValorImportacion;
            otrosGastosPanel.txtFactorGastos.Text = importacion.Factorgastos;
            otrosGastosPanel.txtFactorGastosImpor.Text = importacion.FactorGastImpor;
            otrosGastosPanel.txtMasOtros.Text = string.Format("{0:0.00000000}", Convert.ToDecimal(otrosGastosPanel.txtMasOtros.Text));
            otrosGastosPanel.txtTotalGastos.Text = string.Format("{0:0.00000000}", Convert.ToDecimal(otrosGastosPanel.txtTotalGastos.Text));
            otrosGastosPanel.txtValorImportacion.Text = string.Format("{0:0.00000000}", Convert.ToDecimal(otrosGastosPanel.txtValorImportacion.Text));
            otrosGastosPanel.txtFactorGastosImpor.Text = string.Format("{0:0.00000000}", Convert.ToDecimal(otrosGastosPanel.txtFactorGastosImpor.Text));
            otrosGastosPanel.txtFactorGastos.Text = string.Format("{0:0.00000000}", Convert.ToDecimal(otrosGastosPanel.txtFactorGastos.Text));


            otrosDatosPanel.txtNumExpediente.Text = importacion.ExpNumMilco1;
            otrosDatosPanel.fechSoliMilco.Value = Convert.ToDateTime(importacion.FechaSolicMilco);

            //if (importacion.FechaSolicMilco != DateTime.MinValue) 
            //{
                //en caso de haber fecha NULL o formato 0000-00-00 validar de esta manera y asignarle la minima fecha establecida
                //otrosDatosPanel.fechSoliMilco.Value = importacion.FechaSolicMilco;
            //}

            otrosDatosPanel.fechAutoMilco.Value = Convert.ToDateTime(importacion.FechaAutoMilco);
            otrosDatosPanel.fechVencMilco.Value = Convert.ToDateTime(importacion.FechaVencMilco);

            otrosDatosPanel.txtNumIMP.Text = importacion.NumImpCadivi;

            otrosDatosPanel.fechSoliCadivi.Value = Convert.ToDateTime(importacion.FechSoliCadivi);
            otrosDatosPanel.fechAutoCadivi.Value = Convert.ToDateTime(importacion.FechAutoCadivi);
            otrosDatosPanel.fechaVencCadivi.Value = Convert.ToDateTime(importacion.FechVencCadivi);
            otrosDatosPanel.fechAAD.Value = Convert.ToDateTime(importacion.FechAAD);
            otrosDatosPanel.fechVencAAD.Value = Convert.ToDateTime(importacion.FechVENAAD);

            otrosDatosPanel.txtMontoAutori.Text = importacion.MontoAutoCadivi;
            otrosDatosPanel.ttxMoneda.Text = importacion.MonedaCadivi;

            otrosDatosPanel.fechEmbarque.Value = Convert.ToDateTime(importacion.FechEmbarque);
            otrosDatosPanel.fechVeri.Value = Convert.ToDateTime(importacion.FechaVeri);

            otrosDatosPanel.txtCiudad.Text = importacion.Ciudad;
            otrosDatosPanel.txtVapor.Text = importacion.Vapor1;
            otrosDatosPanel.txtBLnum.Text = importacion.BLNum1;
            otrosDatosPanel.txtTransportista.Text = importacion.Transportista;
            otrosDatosPanel.txtAgente.Text = importacion.Agente;
            otrosDatosPanel.txtEmbalaje.Text = importacion.FormaEmbalaje;
            otrosDatosPanel.txtBultos.Text = importacion.CantidadBultos;

            otrosDatosPanel.fechLlegada.Value = Convert.ToDateTime(importacion.FechLlegada);

            otrosDatosPanel.txtConsignatario.Text = importacion.Consignatario;
            otrosDatosPanel.txtExpe.Text = importacion.NumExpediente;
            otrosDatosPanel.txtNumFac.Text = importacion.NumFactura;

            otrosDatosPanel.fechFactura.Value = Convert.ToDateTime(importacion.FechFactura);

            otrosDatosPanel.txtTasaCambio.Text = importacion.TasaCambio;

            otrosDatosPanel.fechActaCadivi.Value = Convert.ToDateTime(importacion.FechReciboActa);
            otrosDatosPanel.fechEntregaADL.Value = Convert.ToDateTime(importacion.FechDocuALD);
            otrosDatosPanel.fechOtorgaADL.Value = Convert.ToDateTime(importacion.FechOtorgaALD);
            otrosDatosPanel.fechPagoBancario.Value = Convert.ToDateTime(importacion.FechTransfPago);

            otrosDatosPanel.txtTasaCambio.Text = importacion.CambioPermuta;
            usuarioOrigen = importacion.Login;
            formatoTextCabecera();

        }

        private void cargarProductosImportacion(string doc, string suf, int opc) {
            productosPanel.gridProductos.DataSource = null;

            DataTable temporal = new DataTable();
            int fila = 0;
            temporal = importacion.cargarProductosImportacion(doc, suf, opc);
            for (int i = 0; i < temporal.Rows.Count; i++)
            {

                productosPanel.gridProductos.Rows.Add();
                fila = productosPanel.gridProductos.Rows.Count - 1;

                productosPanel.gridProductos.Rows[fila].Cells[0].Value = temporal.Rows[i]["codigo"].ToString();
                productosPanel.gridProductos.Rows[fila].Cells[1].Value = temporal.Rows[i]["descripcion"].ToString();
                productosPanel.gridProductos.Rows[fila].Cells[2].Value = temporal.Rows[i]["unidad"].ToString().Replace(".", ",");
                productosPanel.gridProductos.Rows[fila].Cells[3].Value = temporal.Rows[i]["cantidad"].ToString().Replace(".", ",");
                productosPanel.gridProductos.Rows[fila].Cells[4].Value = temporal.Rows[i]["precioFOB"].ToString().Replace(".", ",");
                productosPanel.gridProductos.Rows[fila].Cells[5].Value = temporal.Rows[i]["c"].ToString().Replace(".", ",");
                productosPanel.gridProductos.Rows[fila].Cells[6].Value = temporal.Rows[i]["d"].ToString().Replace(".", ",");
                productosPanel.gridProductos.Rows[fila].Cells[7].Value = temporal.Rows[i]["e"].ToString().Replace(".", ",");
                productosPanel.gridProductos.Rows[fila].Cells[8].Value = temporal.Rows[i]["f"].ToString().Replace(".", ","); 
                productosPanel.gridProductos.Rows[fila].Cells[9].Value = temporal.Rows[i]["g"].ToString().Replace(".", ","); 
                productosPanel.gridProductos.Rows[fila].Cells[10].Value = temporal.Rows[i]["h"].ToString().Replace(".", ","); 
                productosPanel.gridProductos.Rows[fila].Cells[11].Value = temporal.Rows[i]["i"].ToString().Replace(".", ","); 
                productosPanel.gridProductos.Rows[fila].Cells[12].Value = temporal.Rows[i]["j"].ToString().Replace(".", ","); 
                productosPanel.gridProductos.Rows[fila].Cells[13].Value = temporal.Rows[i]["k"].ToString().Replace(".", ","); 
                productosPanel.gridProductos.Rows[fila].Cells[14].Value = temporal.Rows[i]["l"].ToString().Replace(".", ",");
                productosPanel.gridProductos.Rows[fila].Cells[15].Value = temporal.Rows[i]["codigoArancel"].ToString().Replace(".", ",");

            }
        }

        private void cargarGastosImportacion(string doc, string suf, int opc) {

            gastosImportaciones= importacion.cargarGastosImportaciones(doc, suf, opc);


            //MONTOS
            if (gastosImportaciones.Rows.Count > 0)
            {

                facturaNacionalPanel1.textBox1.Text = gastosImportaciones.Rows[0]["gfi_monto"].ToString();
                facturaNacionalPanel1.textBox2.Text = gastosImportaciones.Rows[1]["gfi_monto"].ToString();
                facturaNacionalPanel1.textBox3.Text = gastosImportaciones.Rows[2]["gfi_monto"].ToString();
                facturaNacionalPanel1.textBox4.Text = gastosImportaciones.Rows[3]["gfi_monto"].ToString();
                facturaNacionalPanel1.textBox5.Text = gastosImportaciones.Rows[4]["gfi_monto"].ToString();
                facturaNacionalPanel1.textBox6.Text = gastosImportaciones.Rows[5]["gfi_monto"].ToString();
                facturaNacionalPanel1.textBox7.Text = gastosImportaciones.Rows[6]["gfi_monto"].ToString();
                facturaNacionalPanel1.textBox8.Text = gastosImportaciones.Rows[7]["gfi_monto"].ToString();
                facturaNacionalPanel1.textBox9.Text = gastosImportaciones.Rows[8]["gfi_monto"].ToString();
                facturaNacionalPanel1.textBox10.Text = gastosImportaciones.Rows[9]["gfi_monto"].ToString();
                facturaNacionalPanel1.textBox11.Text = gastosImportaciones.Rows[10]["gfi_monto"].ToString();
                facturaNacionalPanel1.textBox12.Text = gastosImportaciones.Rows[11]["gfi_monto"].ToString();
                facturaNacionalPanel1.textBox13.Text = gastosImportaciones.Rows[12]["gfi_monto"].ToString();
                facturaNacionalPanel1.textBox14.Text = gastosImportaciones.Rows[13]["gfi_monto"].ToString();
                facturaNacionalPanel1.textBox15.Text = gastosImportaciones.Rows[14]["gfi_monto"].ToString();
                facturaNacionalPanel1.textBox16.Text = gastosImportaciones.Rows[15]["gfi_monto"].ToString();
                facturaNacionalPanel1.textBox17.Text = gastosImportaciones.Rows[16]["gfi_monto"].ToString();
                facturaNacionalPanel1.textBox18.Text = gastosImportaciones.Rows[17]["gfi_monto"].ToString();
                facturaNacionalPanel1.textBox19.Text = gastosImportaciones.Rows[18]["gfi_monto"].ToString();


                //CUENTAS
                facturaNacionalPanel1.button1txt.Text = gastosImportaciones.Rows[0]["gfi_cuenta"].ToString();
                facturaNacionalPanel1.button2txt.Text = gastosImportaciones.Rows[1]["gfi_cuenta"].ToString();
                facturaNacionalPanel1.button3txt.Text = gastosImportaciones.Rows[2]["gfi_cuenta"].ToString();
                facturaNacionalPanel1.button4txt.Text = gastosImportaciones.Rows[3]["gfi_cuenta"].ToString();
                facturaNacionalPanel1.button5txt.Text = gastosImportaciones.Rows[4]["gfi_cuenta"].ToString();
                facturaNacionalPanel1.button6txt.Text = gastosImportaciones.Rows[5]["gfi_cuenta"].ToString();
                facturaNacionalPanel1.button7txt.Text = gastosImportaciones.Rows[6]["gfi_cuenta"].ToString();
                facturaNacionalPanel1.button8txt.Text = gastosImportaciones.Rows[7]["gfi_cuenta"].ToString();
                facturaNacionalPanel1.button9txt.Text = gastosImportaciones.Rows[8]["gfi_cuenta"].ToString();
                facturaNacionalPanel1.button10txt.Text = gastosImportaciones.Rows[9]["gfi_cuenta"].ToString();
                facturaNacionalPanel1.button11txt.Text = gastosImportaciones.Rows[10]["gfi_cuenta"].ToString();
                facturaNacionalPanel1.button12txt.Text = gastosImportaciones.Rows[11]["gfi_cuenta"].ToString();
                facturaNacionalPanel1.button13txt.Text = gastosImportaciones.Rows[12]["gfi_cuenta"].ToString();
                facturaNacionalPanel1.button14txt.Text = gastosImportaciones.Rows[13]["gfi_cuenta"].ToString();
                facturaNacionalPanel1.button15txt.Text = gastosImportaciones.Rows[14]["gfi_cuenta"].ToString();
                facturaNacionalPanel1.button16txt.Text = gastosImportaciones.Rows[15]["gfi_cuenta"].ToString();
                facturaNacionalPanel1.button17txt.Text = gastosImportaciones.Rows[16]["gfi_cuenta"].ToString();
                facturaNacionalPanel1.button18txt.Text = gastosImportaciones.Rows[17]["gfi_cuenta"].ToString();
                facturaNacionalPanel1.button19txt.Text = gastosImportaciones.Rows[18]["gfi_cuenta"].ToString();


                //ISLR
                facturaNacionalPanel1.button20txt.Text = gastosImportaciones.Rows[0]["gfi_islr"].ToString();
                facturaNacionalPanel1.button21txt.Text = gastosImportaciones.Rows[1]["gfi_islr"].ToString();
                facturaNacionalPanel1.button22txt.Text = gastosImportaciones.Rows[2]["gfi_islr"].ToString();
                facturaNacionalPanel1.button23txt.Text = gastosImportaciones.Rows[3]["gfi_islr"].ToString();
                facturaNacionalPanel1.button24txt.Text = gastosImportaciones.Rows[4]["gfi_islr"].ToString();
                facturaNacionalPanel1.button25txt.Text = gastosImportaciones.Rows[5]["gfi_islr"].ToString();
                facturaNacionalPanel1.button26txt.Text = gastosImportaciones.Rows[6]["gfi_islr"].ToString();
                facturaNacionalPanel1.button27txt.Text = gastosImportaciones.Rows[7]["gfi_islr"].ToString();
                facturaNacionalPanel1.button28txt.Text = gastosImportaciones.Rows[8]["gfi_islr"].ToString();
                facturaNacionalPanel1.button29txt.Text = gastosImportaciones.Rows[9]["gfi_islr"].ToString();
                facturaNacionalPanel1.button30txt.Text = gastosImportaciones.Rows[10]["gfi_islr"].ToString();
                facturaNacionalPanel1.button31txt.Text = gastosImportaciones.Rows[11]["gfi_islr"].ToString();
                facturaNacionalPanel1.button32txt.Text = gastosImportaciones.Rows[12]["gfi_islr"].ToString();
                facturaNacionalPanel1.button33txt.Text = gastosImportaciones.Rows[13]["gfi_islr"].ToString();
                facturaNacionalPanel1.button34txt.Text = gastosImportaciones.Rows[14]["gfi_islr"].ToString();
                facturaNacionalPanel1.button35txt.Text = gastosImportaciones.Rows[15]["gfi_islr"].ToString();
                facturaNacionalPanel1.button36txt.Text = gastosImportaciones.Rows[16]["gfi_islr"].ToString();
                facturaNacionalPanel1.button37txt.Text = gastosImportaciones.Rows[17]["gfi_islr"].ToString();
                facturaNacionalPanel1.button38txt.Text = gastosImportaciones.Rows[18]["gfi_islr"].ToString();
            }

            formatoTextGastos();
        }

        private void cargarOtrosgastos(string doc, string suf, int opc) {

            //los otros gastos se cargaron en la cabecera del documento de importacion
            //eliminar esta funcion
        }


        public void armarTodo(string doc, string suf, int opc, string prove) {

            ultDoc = doc;
            ultSuf = suf;
            ultProve = prove;

            cargarCabeceraImportacion(doc, suf, opc,prove);
            cargarGastosImportacion(doc, suf, opc);
            cargarOtrosgastos(doc, suf, opc);
            cargarProductosImportacion(doc, suf, opc);
        }

        private void btnAceptar_Click_1(object sender, EventArgs e)
        {
            if (verificarDatosFactura(sender, e))
            {

                //diferencia = Convert.ToDecimal(productosPanel.txtTotalProductos.Text) - Convert.ToDecimal(facturaOrigenPanel1.txtTotalCIF.Text);
                diferencia = Convert.ToDecimal(productosPanel.txtTotalProductos.Text)-Convert.ToDecimal(otrosGastosPanel.txtValorImportacion.Text);

                if (MessageBox.Show("Esta seguro que desea realizar el cierre definitivo?", "Alerta ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    if (diferencia != 0)
                    {
                        if (MessageBox.Show("Tiene una diferencia, ¿Desea liquidar el documento de todas formas?", "Alerta ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            btnMsj_Click(sender, e);
                            aceptandoInformacion();
                        }
                    }
                    else
                    {
                        btnMsj_Click(sender, e);
                        aceptandoInformacion();
                    }
                }
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // enviar 2 para el cierre temporal

            if (Convert.ToDecimal(productosPanel.txtCantProduc.Text) != 0)
            {
                if (otrosGastosPanel.txtTotalGastos.Text.Equals(""))
                {

                    MessageBox.Show("Debe hacer los calculos", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    //if (importacion.verificarDocumento(facturaOrigenPanel1.txtDocumento.Text, facturaOrigenPanel1.txtSufijoDoc.Text,2) == false)
                    //{

                    //al modificar el documento se eliminara todo y se volvera a cargar
                    btnMsj_Click(sender, e);
                    asignadoValoresCabecera();
                    //importacion.modificarImportaciones(facturaOrigenPanel1.txtDocumento.Text, facturaOrigenPanel1.txtSufijoDoc.Text, productosPanel.txtProveedor.Text);
                    importacion.modificarImportaciones(ultDoc, ultSuf, ultProve);
                    guardarCabecera(otrosGastosPanel.txtFactorGastos.Text.Replace(",", "."), 2);
                    importacion.guardarGastosImportaciones(facturaNacionalPanel1.contenidoArrayGastos(facturaOrigenPanel1.txtDocumento.Text, facturaOrigenPanel1.txtSufijoDoc.Text), 2);
                    //22-01-2013   
                    //importacion.guardarMovContab(facturaNacionalPanel1.contenidoArrayMovContab(importacion.Comprobante, facturaOrigenPanel1.txtDocumento.Text, facturaOrigenPanel1.txtSufijoDoc.Text, productosPanel.txtProveedor.Text, facturaOrigenPanel1.txtIvaImportacion.Text),1);
                    productosPanel.reemplezar();
                    importacion.guardarArticulosImportacion(productosPanel.gridProductos, facturaOrigenPanel1.txtDocumento.Text, facturaOrigenPanel1.txtSufijoDoc.Text, 2);
                    
                    //refLbxImportaciones.Activate(); //probar en la maquina de la sra haydee
                    refLbxImportaciones.refrescar(new object(), new EventArgs());

                    this.Close();
                    //}
                    //else
                    //{

                    //    MessageBox.Show("El documento ya existe, agregue un sufijo", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //}
                }
            }
            else {
                MessageBox.Show("Debe importar una Orden de Compra", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private bool verificarValores() {
            bool cent = false;

            //valores indispensable para el calculo

            if (Convert.ToDecimal(facturaOrigenPanel1.txtNetoFOB.Text) == 0)
            {
                MessageBox.Show("El neto FOB no debe ser igual a 0", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cent = false;
                return cent;

            }
            else { cent = true; }

            if (Convert.ToDecimal(facturaOrigenPanel1.txtCambioLiquidar.Text) == 0)
            {
                MessageBox.Show("El monto del cambio a liquidar no debe ser igual a 0", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cent = false;
                return cent;
            }
            else { cent = true; }

            if (Convert.ToDecimal(facturaOrigenPanel1.txtCambioProducto.Text) == 0)
            {
                MessageBox.Show("El monto del cambio aplicado a los productos no debe ser igual a 0", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cent = false;
                return cent;
            }
            else { cent = true; }

            return cent;
        }


        public void asignadoValoresCabecera()
        {
            //asignando valores a la cabecera del documento(incluyendo otros gastos)

            importacion.FactorCif = facturaOrigenPanel1.txtFactorCIF.Text.Replace(",", ".");
            importacion.Documento = facturaOrigenPanel1.txtDocumento.Text;
            importacion.Sufijo = facturaOrigenPanel1.txtSufijoDoc.Text;
            importacion.MontoFob = facturaOrigenPanel1.txtNetoFOB.Text.Replace(",", ".");
            importacion.SeguroFob = facturaOrigenPanel1.txtSeguro.Text.Replace(",", ".");
            importacion.FleteFob = facturaOrigenPanel1.txtFlete.Text.Replace(",", ".");
            importacion.CostoMaterialFob = facturaOrigenPanel1.txtCostoMaterial.Text.Replace(",", ".");
            importacion.Otros1Fob = facturaOrigenPanel1.txtExtra1.Text.Replace(",", ".");
            importacion.Otros2Fob = facturaOrigenPanel1.txtExtra2.Text.Replace(",", ".");
            importacion.MontoBs = facturaOrigenPanel1.txtNetoBs.Text.Replace(",", ".");
            importacion.SeguroBs = facturaOrigenPanel1.txtSeguroBs.Text.Replace(",", ".");
            importacion.FleteBs = facturaOrigenPanel1.txtFleteBs.Text.Replace(",", ".");
            importacion.CostoMaterialBs = facturaOrigenPanel1.txtCostoMaterialBs.Text.Replace(",", ".");
            importacion.Otros1Bs = facturaOrigenPanel1.txtExtra1Bs.Text.Replace(",", ".");
            importacion.Otros2Bs = facturaOrigenPanel1.txtExtra2Bs.Text.Replace(",", ".");
            importacion.TotalCif = facturaOrigenPanel1.txtTotalCIF.Text.Replace(",", ".");
            importacion.CostoCif = facturaOrigenPanel1.txtCostoCIF.Text.Replace(",", ".");
            importacion.CambioLiqui = facturaOrigenPanel1.txtCambioLiquidar.Text.Replace(",", ".");
            importacion.CambioOc = facturaOrigenPanel1.txtCambioOrden.Text.Replace(",", ".");
            importacion.CambioProducto = facturaOrigenPanel1.txtCambioProducto.Text.Replace(",", ".");
            importacion.Fechaimport = facturaOrigenPanel1.fechEmiFacImport.Value.Year.ToString() + "-" + facturaOrigenPanel1.fechEmiFacImport.Value.Month.ToString() + "-" + facturaOrigenPanel1.fechEmiFacImport.Value.Day.ToString();
            importacion.VerificaMercan = otrosGastosPanel.txtVerificarMercancia.Text.Replace(",", ".");
            importacion.Honorarios = otrosGastosPanel.txtHononarios.Text.Replace(",", ".");
            importacion.GastoCadivi = otrosGastosPanel.txtTramiteCadivi.Text.Replace(",", ".");
            importacion.SegurosMercancia = otrosGastosPanel.txtSeguroMercancia.Text.Replace(",", ".");
            importacion.OtrosGastos = otrosGastosPanel.txtOtrosGastos.Text.Replace(",", ".");
            importacion.CodProveedor = productosPanel.txtProveedor.Text;
            importacion.Moneda = facturaOrigenPanel1.lblmoneda.Text;
            importacion.IvaImportacion = facturaOrigenPanel1.txtIvaImportacion.Text.Replace(",", ".");
            importacion.IvaNacional = facturaNacionalPanel1.txtIvaNacional.Text.Replace(",", ".");
            importacion.DerechosImportacion = facturaNacionalPanel1.textBox1.Text.Replace(",", ".");
            importacion.FechaSistema = ""+ DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString();
            importacion.ProveNacional = facturaNacionalPanel1.txtCodigoProveedor.Text;
            importacion.Fechaimport = "" + facturaOrigenPanel1.fechEmiFacImport.Value.Year + "-" + facturaOrigenPanel1.fechEmiFacImport.Value.Month + "-" + facturaOrigenPanel1.fechEmiFacImport.Value.Day;

            importacion.GastAduanaOtros = otrosGastosPanel.txtMasOtros.Text.Replace(",", ".");
            importacion.GastosGenerales = otrosGastosPanel.txtTotalGastos.Text.Replace(",", ".");
            importacion.ValorImportacion = otrosGastosPanel.txtValorImportacion.Text.Replace(",", ".");
            importacion.FactorGastImpor = otrosGastosPanel.txtFactorGastosImpor.Text.Replace(",", ".");

            importacion.ControlImportacion = facturaOrigenPanel1.txtControlImpor.Text;

            totalImport = "" + (Convert.ToDecimal(facturaOrigenPanel1.txtIvaImportacion.Text) + Convert.ToDecimal(facturaNacionalPanel1.textBox1.Text) + Convert.ToDecimal(facturaNacionalPanel1.textBox2.Text) + Convert.ToDecimal(facturaNacionalPanel1.textBox3.Text) + Convert.ToDecimal(facturaNacionalPanel1.textBox4.Text)+ Convert.ToDecimal(facturaOrigenPanel1.txtTotalCIF.Text));
            baseImport = "" + (Convert.ToDecimal(facturaNacionalPanel1.textBox1.Text) + Convert.ToDecimal(facturaNacionalPanel1.textBox2.Text) + Convert.ToDecimal(facturaNacionalPanel1.textBox3.Text) + Convert.ToDecimal(facturaNacionalPanel1.textBox4.Text) + Convert.ToDecimal(facturaOrigenPanel1.txtTotalCIF.Text));
            
            importacion.MontoTotalImpor = totalImport.Replace(",", ".");
            importacion.MontoBaseImport = baseImport.Replace(",", ".");
       
            importacion.ExentoNacional = facturaNacionalPanel1.txtMontoExento.Text.Replace(",", ".");
            importacion.FactNacional = facturaNacionalPanel1.txtNumeroFactura.Text;
            importacion.SufiNacional = facturaNacionalPanel1.txtSufijoNacional.Text;
            importacion.ControlNacional = facturaNacionalPanel1.txtNumeroControl.Text;
            importacion.FechEmisionNacio = "" + facturaNacionalPanel1.fechaEmisionNac.Value.Year + "-" + facturaNacionalPanel1.fechaEmisionNac.Value.Month + "-" + facturaNacionalPanel1.fechaEmisionNac.Value.Day;
            //importacion.FechEmisionNacio = facturaNacionalPanel1.fechaEmisionNac.Value.Date;
            importacion.FechVencNacio = "" + facturaNacionalPanel1.fechavencNacio.Value.Year + "-" + facturaNacionalPanel1.fechavencNacio.Value.Month + "-" + facturaNacionalPanel1.fechavencNacio.Value.Day;
            importacion.BaseNacional = facturaNacionalPanel1.txtMontoBase.Text.Replace(",", "."); 
            importacion.ExentoNacional = facturaNacionalPanel1.txtMontoExento.Text.Replace(",", "."); 
            importacion.TotalNacional = facturaNacionalPanel1.txtTotalNacional.Text.Replace(",", ".");
            importacion.MontoExenImport = facturaOrigenPanel1.txtExentoImport.Text.Replace(",", ".");

            //nuevos datos 20-03-2013

            importacion.ExpNumMilco1 = otrosDatosPanel.txtNumExpediente.Text;
            importacion.FechaSolicMilco = "" + otrosDatosPanel.fechSoliMilco.Value.Year + "-" + otrosDatosPanel.fechSoliMilco.Value.Month + "-" + otrosDatosPanel.fechSoliMilco.Value.Day;
            importacion.FechaAutoMilco = "" + otrosDatosPanel.fechAutoMilco.Value.Year + "-" + otrosDatosPanel.fechAutoMilco.Value.Month + "-" + otrosDatosPanel.fechAutoMilco.Value.Day;
            importacion.FechaVencMilco = "" + otrosDatosPanel.fechVencMilco.Value.Year + "-" + otrosDatosPanel.fechVencMilco.Value.Month + "-" + otrosDatosPanel.fechVencMilco.Value.Day;
            importacion.NumImpCadivi = otrosDatosPanel.txtNumIMP.Text;
            importacion.FechSoliCadivi = "" + otrosDatosPanel.fechSoliCadivi.Value.Year + "-" + otrosDatosPanel.fechSoliCadivi.Value.Month + "-" + otrosDatosPanel.fechSoliCadivi.Value.Day;
            importacion.FechAutoCadivi = "" + otrosDatosPanel.fechAutoCadivi.Value.Year + "-" + otrosDatosPanel.fechAutoCadivi.Value.Month + "-" + otrosDatosPanel.fechAutoCadivi.Value.Day;
            importacion.FechVencCadivi = "" + otrosDatosPanel.fechaVencCadivi.Value.Year + "-" + otrosDatosPanel.fechaVencCadivi.Value.Month + "-" + otrosDatosPanel.fechaVencCadivi.Value.Day;
            importacion.FechAAD = "" + otrosDatosPanel.fechAAD.Value.Year + "-" + otrosDatosPanel.fechAAD.Value.Month + "-" + otrosDatosPanel.fechAAD.Value.Day;
            importacion.FechVENAAD = "" + otrosDatosPanel.fechVencAAD.Value.Year + "-" + otrosDatosPanel.fechVencAAD.Value.Month + "-" + otrosDatosPanel.fechVencAAD.Value.Day;
            importacion.MontoAutoCadivi = otrosDatosPanel.txtMontoAutori.Text.Replace(",",".");
            importacion.MonedaCadivi = otrosDatosPanel.ttxMoneda.Text;
            importacion.FechEmbarque = "" + otrosDatosPanel.fechEmbarque.Value.Year + "-" + otrosDatosPanel.fechEmbarque.Value.Month + "-" + otrosDatosPanel.fechEmbarque.Value.Day;
            importacion.FechaVeri = "" + otrosDatosPanel.fechVeri.Value.Year + "-" + otrosDatosPanel.fechVeri.Value.Month + "-" + otrosDatosPanel.fechVeri.Value.Day;
            importacion.Ciudad = otrosDatosPanel.txtCiudad.Text;
            importacion.Vapor1 = otrosDatosPanel.txtVapor.Text;
            importacion.BLNum1 = otrosDatosPanel.txtBLnum.Text;
            importacion.Transportista = otrosDatosPanel.txtTransportista.Text;
            importacion.Agente = otrosDatosPanel.txtAgente.Text;
            importacion.FormaEmbalaje = otrosDatosPanel.txtEmbalaje.Text;
            importacion.CantidadBultos = otrosDatosPanel.txtBultos.Text;
            importacion.FechLlegada = "" + otrosDatosPanel.fechLlegada.Value.Year + "-" + otrosDatosPanel.fechLlegada.Value.Month + "-" + otrosDatosPanel.fechLlegada.Value.Day;
            importacion.Consignatario = otrosDatosPanel.txtConsignatario.Text;
            importacion.NumExpediente = otrosDatosPanel.txtExpe.Text;
            importacion.NumFactura = otrosDatosPanel.txtNumFac.Text;
            importacion.FechFactura = "" + otrosDatosPanel.fechFactura.Value.Year + "-" + otrosDatosPanel.fechFactura.Value.Month + "-" + otrosDatosPanel.fechFactura.Value.Day;
            importacion.TasaCambio = otrosDatosPanel.txtTasaCambio.Text.Replace(",",".");
            importacion.FechReciboActa = "" + otrosDatosPanel.fechActaCadivi.Value.Year + "-" + otrosDatosPanel.fechActaCadivi.Value.Month + "-" + otrosDatosPanel.fechActaCadivi.Value.Day;
            importacion.FechDocuALD = "" + otrosDatosPanel.fechEntregaADL.Value.Year + "-" + otrosDatosPanel.fechEntregaADL.Value.Month + "-" + otrosDatosPanel.fechEntregaADL.Value.Day;
            importacion.FechOtorgaALD = "" + otrosDatosPanel.fechOtorgaADL.Value.Year + "-" + otrosDatosPanel.fechOtorgaADL.Value.Month + "-" + otrosDatosPanel.fechOtorgaADL.Value.Day;
            importacion.FechTransfPago = "" + otrosDatosPanel.fechPagoBancario.Value.Year + "-" + otrosDatosPanel.fechPagoBancario.Value.Month + "-" + otrosDatosPanel.fechPagoBancario.Value.Day;
            importacion.CambioPermuta = otrosDatosPanel.txtTasaCambio.Text.Replace(",",".");
            importacion.UpdLogin = usuaLogin;
            importacion.FechaUltimaMod = "" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString();

            if (usuarioOrigen == "" || usuarioOrigen == null)
            {
                importacion.Login = usuaLogin;
            }
            else
            {
                importacion.Login = usuarioOrigen;
            }
            //"" + otrosDatosPanel.fechPagoBancario.Value.Year + "-" + otrosDatosPanel.fechPagoBancario.Value.Month + "-" + otrosDatosPanel.fechPagoBancario.Value.Day;
            //csm
        }

        public void guardarCabecera(string factorGasto, int opc)
        {
            importacion.guardarCabeceraImportacion(otrosGastosPanel.txtFactorGastos.Text.Replace(",","."), opc);
        }

        public void formatoTextCabecera()
        {

            facturaOrigenPanel1.txtCambioLiquidar.Text = string.Format("{0:0.00000000}", Convert.ToDecimal(facturaOrigenPanel1.txtCambioLiquidar.Text));
            facturaOrigenPanel1.txtCambioOrden.Text = string.Format("{0:0.00000000}", Convert.ToDecimal(facturaOrigenPanel1.txtCambioOrden.Text));
            facturaOrigenPanel1.txtCambioProducto.Text = string.Format("{0:0.00000000}", Convert.ToDecimal(facturaOrigenPanel1.txtCambioProducto.Text));
            facturaOrigenPanel1.txtFactorCIF.Text = string.Format("{0:0.00000000}", Convert.ToDecimal(facturaOrigenPanel1.txtFactorCIF.Text));
            facturaOrigenPanel1.txtNetoFOB.Text = string.Format("{0:0.00000000}", Convert.ToDecimal(facturaOrigenPanel1.txtNetoFOB.Text));
            facturaOrigenPanel1.txtNetoBs.Text = string.Format("{0:0.00000000}", Convert.ToDecimal(facturaOrigenPanel1.txtNetoBs.Text));
            facturaOrigenPanel1.txtSeguro.Text = string.Format("{0:0.00000000}", Convert.ToDecimal(facturaOrigenPanel1.txtSeguro.Text));
            facturaOrigenPanel1.txtSeguroBs.Text = string.Format("{0:0.00000000}", Convert.ToDecimal(facturaOrigenPanel1.txtSeguroBs.Text));
            facturaOrigenPanel1.txtFlete.Text = string.Format("{0:0.00000000}", Convert.ToDecimal(facturaOrigenPanel1.txtFlete.Text));
            facturaOrigenPanel1.txtCostoMaterial.Text = string.Format("{0:0.00000000}", Convert.ToDecimal(facturaOrigenPanel1.txtCostoMaterial.Text));
            facturaOrigenPanel1.txtCostoMaterialBs.Text = string.Format("{0:0.00000000}", Convert.ToDecimal(facturaOrigenPanel1.txtCostoMaterialBs.Text));
            facturaOrigenPanel1.txtFleteBs.Text = string.Format("{0:0.00000000}", Convert.ToDecimal(facturaOrigenPanel1.txtFleteBs.Text));
            facturaOrigenPanel1.txtExtra1.Text = string.Format("{0:0.00000000}", Convert.ToDecimal(facturaOrigenPanel1.txtExtra1.Text));
            facturaOrigenPanel1.txtExtra1Bs.Text = string.Format("{0:0.00000000}", Convert.ToDecimal(facturaOrigenPanel1.txtExtra1Bs.Text));
            facturaOrigenPanel1.txtExtra2.Text = string.Format("{0:0.00000000}", Convert.ToDecimal(facturaOrigenPanel1.txtExtra2.Text));
            facturaOrigenPanel1.txtExtra2Bs.Text = string.Format("{0:0.00000000}", Convert.ToDecimal(facturaOrigenPanel1.txtExtra2Bs.Text));
            facturaOrigenPanel1.txtFactorCIF.Text = string.Format("{0:0.00000000}", Convert.ToDecimal(facturaOrigenPanel1.txtFactorCIF.Text));
            facturaOrigenPanel1.txtTotalCIF.Text = string.Format("{0:0.00000000}", Convert.ToDecimal(facturaOrigenPanel1.txtTotalCIF.Text));
            facturaOrigenPanel1.txtCostoCIF.Text = string.Format("{0:0.00000000}", Convert.ToDecimal(facturaOrigenPanel1.txtCostoCIF.Text));
            facturaOrigenPanel1.txtIvaImportacion.Text = string.Format("{0:0.00000000}", Convert.ToDecimal(facturaOrigenPanel1.txtIvaImportacion.Text));
            
            otrosGastosPanel.txtVerificarMercancia.Text = string.Format("{0:0.00000000}", Convert.ToDecimal(otrosGastosPanel.txtVerificarMercancia.Text));
            otrosGastosPanel.txtHononarios.Text = string.Format("{0:0.00000000}", Convert.ToDecimal(otrosGastosPanel.txtHononarios.Text));
            otrosGastosPanel.txtTramiteCadivi.Text = string.Format("{0:0.00000000}", Convert.ToDecimal(otrosGastosPanel.txtTramiteCadivi.Text));
            otrosGastosPanel.txtSeguroMercancia.Text = string.Format("{0:0.00000000}", Convert.ToDecimal(otrosGastosPanel.txtSeguroMercancia.Text));
            otrosGastosPanel.txtOtrosGastos.Text = string.Format("{0:0.00000000}", Convert.ToDecimal(otrosGastosPanel.txtOtrosGastos.Text));

            //otrosGastosPanel.txtBaseDifAraEntreValCif

            //otrosGastosPanel.txtFactorGastos = string.Format("{0:0.00000000}", Convert.ToDecimal(otrosGastosPanel.txtFactorGastos.Text));
            //otrosGastosPanel.txtFactorGastosImpor = string.Format("{0:0.00000000}", Convert.ToDecimal(otrosGastosPanel.txtFactorGastosImpor.Text));

        }

        private void formatoTextGastos()
        {

            facturaNacionalPanel1.textBox1.Text = string.Format("{0:0.00000000}", Convert.ToDecimal(facturaNacionalPanel1.textBox1.Text));
            facturaNacionalPanel1.textBox2.Text = string.Format("{0:0.00000000}", Convert.ToDecimal(facturaNacionalPanel1.textBox2.Text));
            facturaNacionalPanel1.textBox3.Text = string.Format("{0:0.00000000}", Convert.ToDecimal(facturaNacionalPanel1.textBox3.Text));
            facturaNacionalPanel1.textBox4.Text = string.Format("{0:0.00000000}", Convert.ToDecimal(facturaNacionalPanel1.textBox4.Text));
            facturaNacionalPanel1.textBox5.Text = string.Format("{0:0.00000000}", Convert.ToDecimal(facturaNacionalPanel1.textBox5.Text));
            facturaNacionalPanel1.textBox6.Text = string.Format("{0:0.00000000}", Convert.ToDecimal(facturaNacionalPanel1.textBox6.Text));
            facturaNacionalPanel1.textBox7.Text = string.Format("{0:0.00000000}", Convert.ToDecimal(facturaNacionalPanel1.textBox7.Text));
            facturaNacionalPanel1.textBox8.Text = string.Format("{0:0.00000000}", Convert.ToDecimal(facturaNacionalPanel1.textBox8.Text));
            facturaNacionalPanel1.textBox9.Text = string.Format("{0:0.00000000}", Convert.ToDecimal(facturaNacionalPanel1.textBox9.Text));
            facturaNacionalPanel1.textBox10.Text = string.Format("{0:0.00000000}", Convert.ToDecimal(facturaNacionalPanel1.textBox10.Text));
            facturaNacionalPanel1.textBox11.Text = string.Format("{0:0.00000000}", Convert.ToDecimal(facturaNacionalPanel1.textBox11.Text));
            facturaNacionalPanel1.textBox12.Text = string.Format("{0:0.00000000}", Convert.ToDecimal(facturaNacionalPanel1.textBox12.Text));
            facturaNacionalPanel1.textBox13.Text = string.Format("{0:0.00000000}", Convert.ToDecimal(facturaNacionalPanel1.textBox13.Text));
            facturaNacionalPanel1.textBox14.Text = string.Format("{0:0.00000000}", Convert.ToDecimal(facturaNacionalPanel1.textBox14.Text));
            facturaNacionalPanel1.textBox15.Text = string.Format("{0:0.00000000}", Convert.ToDecimal(facturaNacionalPanel1.textBox15.Text));
            facturaNacionalPanel1.textBox16.Text = string.Format("{0:0.00000000}", Convert.ToDecimal(facturaNacionalPanel1.textBox16.Text));
            facturaNacionalPanel1.textBox17.Text = string.Format("{0:0.00000000}", Convert.ToDecimal(facturaNacionalPanel1.textBox17.Text));
            facturaNacionalPanel1.textBox18.Text = string.Format("{0:0.00000000}", Convert.ToDecimal(facturaNacionalPanel1.textBox18.Text));
            facturaNacionalPanel1.textBox19.Text = string.Format("{0:0.00000000}", Convert.ToDecimal(facturaNacionalPanel1.textBox19.Text));
            facturaNacionalPanel1.txtIvaNacional.Text = string.Format("{0:0.00000000}", Convert.ToDecimal(facturaNacionalPanel1.txtIvaNacional.Text));
        }

        public static frmImportaciones DefInstance
        {
            get
            {
                if (m_FormDefInstance == null || m_FormDefInstance.IsDisposed)
                    m_FormDefInstance = new frmImportaciones();

                return m_FormDefInstance;
            }
            set
            {
                m_FormDefInstance = value;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //imprimirReporte();
            calcularProyeccion();
            aceptarImprimirProyeccion();
        }
        private void imprimirReporte()
        {
            //rptImportacion = new CrystalReport1();
            //DataSet dsReporteImportacion;
            //dsReporteImportacion = new DataSet();
            //dsReporteImportacion.Tables.Add(importacion.imprimirReporte(facturaOrigenPanel1.txtDocumento.Text, facturaOrigenPanel1.txtSufijoDoc.Text,productosPanel.txtProveedor.Text));
            //rptImportacion.SetDataSource(dsReporteImportacion);
            //rptImportacion.SetDataSource(importacion.imprimirReporte(facturaOrigenPanel1.txtDocumento.Text, facturaOrigenPanel1.txtSufijoDoc.Text, productosPanel.txtProveedor.Text));
            //this.crystalReportViewer1.ReportSource= rptImportacion;
            //crystalReportViewer1.Visible = true;

        }
        private void bloquearProyeccion() {

            facturaOrigenPanel1.txtCostoMaterial.ReadOnly = true;
            facturaOrigenPanel1.txtDocumento.ReadOnly = true;
            facturaOrigenPanel1.txtExtra1.ReadOnly = true;
            facturaOrigenPanel1.txtExtra2.ReadOnly = true;
            facturaOrigenPanel1.txtIvaImportacion.ReadOnly = true;
            facturaOrigenPanel1.txtNetoFOB.ReadOnly = true;
            facturaOrigenPanel1.txtSufijoDoc.ReadOnly = true;
            facturaOrigenPanel1.txtSeguro.ReadOnly = true;
            facturaOrigenPanel1.txtCostoMaterial.ReadOnly = true;
            facturaOrigenPanel1.txtFlete.ReadOnly = true;
            facturaOrigenPanel1.txtDifArancelaria.ReadOnly = true;
            facturaOrigenPanel1.fechEmiFacImport.Enabled = false;

            facturaOrigenPanel1.txtCostoMaterial.BackColor = Color.White;
            facturaOrigenPanel1.txtDocumento.BackColor = Color.White;
            facturaOrigenPanel1.txtExtra1.BackColor = Color.White;
            facturaOrigenPanel1.txtExtra2.BackColor = Color.White;
            facturaOrigenPanel1.txtIvaImportacion.BackColor = Color.White;
            facturaOrigenPanel1.txtNetoFOB.BackColor = Color.White;
            facturaOrigenPanel1.txtSufijoDoc.BackColor = Color.White;
            facturaOrigenPanel1.txtSeguro.BackColor = Color.White;
            facturaOrigenPanel1.txtCostoMaterial.BackColor = Color.White;
            facturaOrigenPanel1.txtDifArancelaria.BackColor = Color.White;
            facturaOrigenPanel1.txtFlete.BackColor = Color.White;
            
        }
        public void paraProyeccion(){

            proyeccion = 1;
        }

        public void calcularProyeccion() {

            ultimosFactores = new factores();
            ultimosFactores.buscarUltimosFactores(productosPanel.txtProveedor.Text);

            facturaOrigenPanel1.calcularFOB();
            //otrosGastosPanel.CalcularTotalGastos();
            facturaNacionalPanel1.calcularSinDerechoImportacion();

            gastoSinDereImportacion = facturaNacionalPanel1.calcularSinDerechoImportacion();
            totalGastos = otrosGastosPanel.CalcularTotalGastos();

            otrosGastosPanel.txtFactorGastos.Text = "" + ((totalGastos + gastoSinDereImportacion) / facturaOrigenPanel1.totalBolivares);
            otrosGastosPanel.txtFactorGastosImpor.Text = "" + (((totalGastos + gastoSinDereImportacion) / facturaOrigenPanel1.totalBolivares) + 1);

            otrosGastosPanel.txtMasOtros.Text = "" + (totalGastos + gastoSinDereImportacion);
            otrosGastosPanel.txtTotalGastos.Text = "" + (totalGastos + gastoSinDereImportacion + Convert.ToDecimal(facturaNacionalPanel1.textBox1.Text));
            otrosGastosPanel.txtValorImportacion.Text = "" + (Convert.ToDecimal(otrosGastosPanel.txtTotalGastos.Text) + (facturaOrigenPanel1.totalBolivares));
            productosPanel.factorCif.Text = facturaOrigenPanel1.txtFactorCIF.Text;
            //productosPanel.txtCambio.Text = facturaOrigenPanel1.txtCambioLiquidar.Text;
            productosPanel.txtCambio.Text = facturaOrigenPanel1.txtCambioProducto.Text;
            productosPanel.factorGasto.Text = otrosGastosPanel.txtFactorGastos.Text;
            facturaOrigenPanel1.txtFactorCIF.Text = ultimosFactores.UltimoFactorCif;
            otrosGastosPanel.txtFactorGastos.Text = ultimosFactores.UltimoFactorGasto;
            productosPanel.factorCif.Text = ultimosFactores.UltimoFactorCif;
            productosPanel.factorGasto.Text = "" + (Convert.ToDecimal(ultimosFactores.UltimoFactorGasto) - 1);

            productosPanel.calcularC();

            //facturaNacionalPanel1.contenidoArrayMovContab(); //comentado por pruebas
            facturaNacionalPanel1.armarArray(); //se armara una sola vez el arreglo del textbox
            facturaNacionalPanel1.contenidoArrayGastos(facturaOrigenPanel1.txtDocumento.Text, facturaOrigenPanel1.txtSufijoDoc.Text);
        }


        private void aceptandoInformacion() {

            if (otrosGastosPanel.txtTotalGastos.Text.Equals(""))
            {
                MessageBox.Show("Debe hacer los calculos", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                //if (Convert.ToDecimal(productosPanel.txtTotalProductos.Text) - Convert.ToDecimal(facturaOrigenPanel1.txtFactorCIF.Text) == 0)
                //{
                if (importacion.verificarDocumento(facturaOrigenPanel1.txtDocumento.Text, facturaOrigenPanel1.txtSufijoDoc.Text, facturaOrigenPanel1.lblCodigoProveeImpor.Text, 1) == false)
                {

                    if (importacion.verificarDocumentoNacional(facturaNacionalPanel1.txtNumeroFactura.Text, facturaNacionalPanel1.txtSufijoNacional.Text, facturaNacionalPanel1.txtCodigoProveedor.Text, 1) == false)
                    {
                        importacion.Comprobante = null;
                        importacion.asignarNumeroComprobante("01");
                        asignadoValoresCabecera();
                        guardarCabecera(otrosGastosPanel.txtFactorGastos.Text.Replace(",", "."), 1);
                        importacion.guardarGastosImportaciones(facturaNacionalPanel1.contenidoArrayGastos(facturaOrigenPanel1.txtDocumento.Text, facturaOrigenPanel1.txtSufijoDoc.Text), 1);
                        //para contabilidad
                        importacion.guardarMovContab(facturaNacionalPanel1.contenidoArrayMovContab(importacion.Comprobante, facturaOrigenPanel1.txtDocumento.Text, facturaOrigenPanel1.txtSufijoDoc.Text, productosPanel.txtProveedor.Text, facturaNacionalPanel1.txtCodigoProveedor.Text, facturaNacionalPanel1.txtIvaNacional.Text, totalImport, facturaOrigenPanel1.lblNombrePro.Text, facturaOrigenPanel1.txtIvaImportacion.Text, facturaNacionalPanel1.txtNumeroFactura.Text, facturaNacionalPanel1.lblNomProveNac.Text), 1);
                        //^contabilidad
                        productosPanel.reemplezar();
                        importacion.guardarArticulosImportacion(productosPanel.gridProductos, facturaOrigenPanel1.txtDocumento.Text, facturaOrigenPanel1.txtSufijoDoc.Text, 1);
                        //importacion.modificarImportaciones(facturaOrigenPanel1.txtDocumento.Text, facturaOrigenPanel1.txtSufijoDoc.Text,productosPanel.txtProveedor.Text);
                        importacion.modificarImportaciones(ultDoc, ultSuf, ultProve);
                        importacion.guardarInvMov(productosPanel.armarContenidoArrayInv(productosPanel.txtProveedor.Text, facturaOrigenPanel1.txtDocumento.Text, facturaOrigenPanel1.txtSufijoDoc.Text), facturaOrigenPanel1.txtDocumento.Text, facturaOrigenPanel1.txtSufijoDoc.Text, 1);

                        if (Convert.ToDecimal(otrosGastosPanel.txtFactorGastos.Text) == 0)
                        {
                            factorGasto = "1";
                        }
                        else
                        {
                            factorGasto = otrosGastosPanel.txtFactorGastos.Text;
                        }
                        if (Convert.ToDecimal(facturaOrigenPanel1.txtFactorCIF.Text) == 0)
                        {
                            factorCif = "1";
                        }
                        else
                        {
                            factorCif = facturaOrigenPanel1.txtFactorCIF.Text;
                        }
                        if (Convert.ToDecimal(otrosGastosPanel.txtFactorGastosImpor.Text) == 0)
                        {
                            factorFin = "1";   //ESTE FACTOR NO SE CUAL ES
                        }
                        else
                        {

                        }
                        fecha = facturaOrigenPanel1.fechEmiFacImport.Value.Year + "-" + facturaOrigenPanel1.fechEmiFacImport.Value.Month + "-" + facturaOrigenPanel1.fechEmiFacImport.Value.Day;
                        importacion.guardarProveedor2(facturaOrigenPanel1.txtDocumento.Text, productosPanel.txtProveedor.Text, fecha, factorCif.Replace(",", "."), factorGasto.Replace(",", "."), "1");
                        //importacion.modificarExistencia2(productosPanel.gridProductos);
                        importacion.modificarExistencia2(productosPanel.gridProductos, facturaOrigenPanel1.lblmoneda.Text);
                        importacion.guardarDocPro(facturaOrigenPanel1.txtCambioLiquidar.Text.Replace(",", "."), productosPanel.txtTotalProductos.Text.Replace(",", "."), productosPanel.txtCantProduc.Text.Replace(",", "."), "" + productosPanel.gridProductos.Rows.Count, facturaOrigenPanel1.lblNombrePro.Text);
                        importacion.guardarLibroCompraFN();
                        importacion.guardarLibroCompraFI();
                        importacion.guardarSalpro();
                        importacion.salproProveedorImp();
                        compania.obtenerCodigoCompaniaActual();//ver compañia activa
                        //importacion.syscontaDetalles(facturaNacionalPanel1.contenidoArrayMovContab(importacion.Comprobante, facturaOrigenPanel1.txtDocumento.Text, facturaOrigenPanel1.txtSufijoDoc.Text, productosPanel.txtProveedor.Text, facturaNacionalPanel1.txtCodigoProveedor.Text, facturaNacionalPanel1.txtIvaNacional.Text, totalImport, facturaOrigenPanel1.lblNombrePro.Text, facturaOrigenPanel1.txtIvaImportacion.Text, facturaNacionalPanel1.txtNumeroFactura.Text, facturaNacionalPanel1.lblNomProveNac.Text), compania.BaseDatosCompania);
                        importacion.syscontaDetalles(facturaNacionalPanel1.movContab, compania.BaseDatosCompania);
                        importacion.syscontabCabecera(facturaNacionalPanel1.montoD.ToString().Replace(",", "."), facturaNacionalPanel1.montoC.ToString().Replace(",", "."), compania.BaseDatosCompania, facturaOrigenPanel1.lblNombrePro.Text + " " + facturaOrigenPanel1.txtDocumento.Text);
                        //refLbxImportaciones.Activate(); //para refrescar el lbxImportaciones probar en la pc de la sra Haydee
                        
                        refLbxImportaciones.refrescar(new object(), new EventArgs());
                        this.Close();
                    }
                    else {
                        MessageBox.Show("El documento ya existe, agregue un sufijo", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        facturaNacionalPanel1.txtSufijoNacional.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("El documento ya existe, agregue un sufijo", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    facturaOrigenPanel1.txtSufijoDoc.Focus();
                }
                //}
            }
        
        }

        private void aceptarImprimirProyeccion() {

            if (otrosGastosPanel.txtTotalGastos.Text.Equals(""))
            {

                MessageBox.Show("Debe hacer los calculos", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                //if (importacion.verificarDocumento(facturaOrigenPanel1.txtDocumento.Text, facturaOrigenPanel1.txtSufijoDoc.Text,2) == false)
                //{

                //al modificar el documento se eliminara todo y se volvera a cargar
                asignadoValoresCabecera();
                importacion.modificarImportaciones(facturaOrigenPanel1.txtDocumento.Text, facturaOrigenPanel1.txtSufijoDoc.Text, productosPanel.txtProveedor.Text);
                guardarCabecera(otrosGastosPanel.txtFactorGastos.Text.Replace(",", "."), 2);
                importacion.guardarGastosImportaciones(facturaNacionalPanel1.contenidoArrayGastos(facturaOrigenPanel1.txtDocumento.Text, facturaOrigenPanel1.txtSufijoDoc.Text), 2);
                //22-01-2013   //importacion.guardarMovContab(facturaNacionalPanel1.contenidoArrayMovContab(importacion.Comprobante,facturaOrigenPanel1.txtDocumento.Text,facturaOrigenPanel1.txtSufijoDoc.Text,productosPanel.txtProveedor.Text));
                productosPanel.reemplezar();
                importacion.guardarArticulosImportacion(productosPanel.gridProductos, facturaOrigenPanel1.txtDocumento.Text, facturaOrigenPanel1.txtSufijoDoc.Text, 2);

                reporteImpor = new frmReporte(facturaOrigenPanel1.txtDocumento.Text,facturaOrigenPanel1.txtSufijoDoc.Text,productosPanel.txtProveedor.Text, 3, "importacion");
                //importacion.modificarImportaciones(facturaOrigenPanel1.txtDocumento.Text, facturaOrigenPanel1.txtSufijoDoc.Text);
                reporteImpor.Show();
                importacion.modificarImportaciones(facturaOrigenPanel1.txtDocumento.Text, facturaOrigenPanel1.txtSufijoDoc.Text, productosPanel.txtProveedor.Text);
                //this.Close();
            }
        }

        private void btnConfirmar_MouseEnter(object sender, EventArgs e)
        {
            //Button a = (Button)sender;
            //a.BackColor = Color.FromArgb(49, 106, 197);
            //a.ForeColor = Color.White;
        }
        private void btnConfirmar_MouseLeave(object sender, EventArgs e)
        {
            //Button a = (Button)sender;
            //a.BackColor = SystemColors.ButtonFace;
            //a.BackColor = Color.FromName("Control");
            //a.ForeColor = Color.Black;
        }
        private bool verificarDatosFactura(object sender, EventArgs e)
        {
            if (proyeccion == 1)
            {
                return true;
            }
                if (facturaNacionalPanel1.txtNumeroFactura.Text == "")
                {
                    MessageBox.Show("El campo #Factura no debe quedar vacio", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnGastos_Click_1(sender, e);
                    facturaNacionalPanel1.txtNumeroFactura.Focus();
                    
                    return false;
                }
                else if (facturaNacionalPanel1.txtNumeroControl.Text == "")
                {
                    MessageBox.Show("El campo #Control de la Factura Nacional no debe quedar vacio ", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnGastos_Click_1(sender, e);
                    facturaNacionalPanel1.txtNumeroControl.Focus();

                    return false;
                }
                else if (facturaNacionalPanel1.txtSufijoNacional.Text == "")
                {
                    MessageBox.Show("El campo sufijo no debe quedar vacio", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnGastos_Click_1(sender, e);
                    facturaNacionalPanel1.txtSufijoNacional.Focus();
                    return false;
                }
                else if (Convert.ToDecimal(facturaNacionalPanel1.txtMontoBase.Text) == 0)
                {
                    MessageBox.Show("El campo Monto Base no puede ser cero(0)", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnGastos_Click_1(sender, e);
                    facturaNacionalPanel1.txtMontoBase.Focus();
                    return false;
                }
                else if (facturaOrigenPanel1.txtSufijoDoc.Text == "")
                {
                    MessageBox.Show("El campo sufijo no debe quedar vacio", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnFacOrigen_Click(sender, e);
                    facturaOrigenPanel1.txtSufijoDoc.Focus();
                    return false;
                }
                else if (facturaOrigenPanel1.txtDocumento.Text == "")
                {
                    MessageBox.Show("El campo Documento de debe quedar vacio", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnFacOrigen_Click(sender, e);
                    facturaOrigenPanel1.txtDocumento.Focus();
                    return false;
                }
                else if (facturaOrigenPanel1.txtControlImpor.Text == "")
                {
                    MessageBox.Show("El campo Control no debe quedar vacio", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnFacOrigen_Click(sender, e);
                    facturaOrigenPanel1.txtControlImpor.Focus();
                    return false;
                }
                else if(facturaNacionalPanel1.txtCodigoProveedor.Text=="")
                {
                    MessageBox.Show("El campo proveedor Nacional no debe quedar vacio", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnGastos_Click_1(sender, e);
                    facturaNacionalPanel1.txtCodigoProveedor.Focus();
                    return false;
                }
                else if ((Convert.ToDecimal(facturaOrigenPanel1.txtCambioOrden.Text) == 0) || (Convert.ToDecimal(productosPanel.txtCantProduc.Text))==0) 
                {
                    MessageBox.Show("Debe importar una orden de compra para realizar esta operacion", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                else
                {
                    return true;
                }
            
        }

        private void valildarMontoTotal(){

            if (Convert.ToDecimal(facturaNacionalPanel1.txtTotalNacional.Text) == 0) {
                MessageBox.Show("Los montos totales estan descuadrados", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void referenciaRefrescar(Form referencia)
        {
            //para refrescar lbxImportaciones 25-03-2013
            refLbxImportaciones = (lbxImportaciones)referencia;  
        }
        public void modificarUsu(string idUsuario)
        {
            usuaLogin = idUsuario;
        }




    }
}
