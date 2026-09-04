using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using lebrun.clases.facturacion;
using lebrun.formularios.clientes;


using lebrun.formularios.vendedores;
using lebrun.clases.clientes;
using lebrun.clases.vendedores;
using lebrun.formularios.complementos;
using lebrun.clases.complementos;
using lebrun.clasesData;
using lebrun.clases.bancos;
using lebrun.formularios.bancos;
using lebrun.clases.contabilidad;
using lebrun.formularios.facturacion;
using lebrun.Reportes.pedido;
using lebrun.clases.pedidos;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Reflection;


namespace lebrun.formularios.prefactura
{
    public partial class frmPrefactura : Form
    {
        public frmPrefactura()
        {
            InitializeComponent();
        }

        internal Factura fac1;
        private lebrun.formularios.facturacion.lbxClientes refClientes;
        private lbxVendedores refVendedores;
        internal Clientes client1;
        internal Vendedor vende1;
        private frmConsultaArticulos refProductos;
        private Producto productoFactura;
        private FuncionesTexbox funtxt;
        private Banco bancoFac;
        private lbxBancos lbxBanCancelar;
        private Caja cajaCobrar;
        private bool centinelaConfirmacion;
        private lebrun.formularios.facturacion.frmClaveConfirmacion2 claveSuper;
        private Principal referenciaPrincipal;
        private ImpresoraFiscal epsonLX300;
        internal bool exportDev;
        private bool cetinela1VezCantidad;
        private string temporalCambioPrecio;
        private AuxiliarContable cuentaContable;
        private frmContePedido frmReporte;
        private frmReportePedido frmReporteP;
        private AprobacionGerencial aprob;
        internal string operacion;
        private Transporte transporte1;
        private CondicionPago condPag;
        public lbxPrefactura referenciaLbxPrefactura;
        public lbxprefacturaS12 referencia12;
        private bool modificarArticulo = false;
        private int indiceFilaModificar = -1;
        internal FacturaFerle factFerle;

        public bool CentinelaConfirmacion
        {
            get { return centinelaConfirmacion; }
            set { centinelaConfirmacion = value; }
        }

        private void cmdSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmPreFac_Load(object sender, EventArgs e)
        {
            referenciaPrincipal = (Principal)this.MdiParent;
            if (referenciaPrincipal.empresaActual.Codigo.Equals("08"))
            {
                txtDescuentoGeneral.Enabled = true;
                txtplazoDias.Enabled = true;
                cboCondicionPago.Enabled = true;
                cboTransporte.Enabled = true;
            }
            fac1 = new Factura("PRE", referenciaPrincipal.usuarioActual.IpPc);
            client1 = new Clientes();
            vende1 = new Vendedor();
            productoFactura = new Producto();
            fac1.obtenerMaximaCantidadItemsFAV();
            funtxt = new FuncionesTexbox();
            llenarModoPago();
            bancoFac = new Banco();
            activarCancelarFactura(false);
            cajaCobrar = new Caja();
            CentinelaConfirmacion = false;
            //instancia del objeto ImpresoraFiscal
            //epsonLX300 = new ImpresoraFiscal(this.FISCAL, this.IPuerto);
            exportDev = false;
            cetinela1VezCantidad = false;
            cuentaContable = new AuxiliarContable();
            txtnumCont.Focus();
            transporte1 = new Transporte();
            comboTransporte();
            this.label35.Text = "PREFACTURA";
            this.label35.ForeColor = Color.FromArgb(0, 128, 0);
            this.label35.Visible = true;
            condPag = new CondicionPago();
            comboCondigPag();

            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView1.Columns["colProducto"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridView1.Columns["@mov_codigo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridView1.Columns["colMed"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            //dataGridView1.Columns["@mov_cant"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridView1.Columns["@mov_precio"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridView1.Columns["@mov_desc"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridView1.Columns["@mov_total"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

        }

        public void asignarCorrelativo(string tipoDoc)
        {
            txtCorrelativo.Text = fac1.obtenerCorrelativo(tipoDoc);
            fac1.TipoDocumento = tipoDoc;
        }

        public void frmPreFac_Load(object sender, EventArgs e, bool v)
        {
            referenciaPrincipal = (Principal)this.MdiParent;
            if (referenciaPrincipal.empresaActual.Codigo.Equals("08"))
            {
                txtDescuentoGeneral.Enabled = true;
                txtplazoDias.Enabled = true;
                cboCondicionPago.Enabled = true;
                cboTransporte.Enabled = true;
            }
            fac1 = new Factura("PRE", referenciaPrincipal.usuarioActual.IpPc);
            fac1.obtenerMaximaCantidadItemsFAV();
            client1 = new Clientes();
            vende1 = new Vendedor();
            productoFactura = new Producto();
            funtxt = new FuncionesTexbox();
            llenarModoPago();
            bancoFac = new Banco();
            activarCancelarFactura(false);
            cajaCobrar = new Caja();
            CentinelaConfirmacion = false;
            transporte1 = new Transporte();
            comboTransporte();
            condPag = new CondicionPago();
            comboCondigPag();
        }

        private void cmdBuscarCliente_Click(object sender, EventArgs e)
        {
            refClientes = lebrun.formularios.facturacion.lbxClientes.DefInstance;
            refClientes.MdiParent = this.MdiParent;
            refClientes.Show();
            if (fac1.TipoDocumento.Equals("PED"))
            {
                //refClientes.refFacturacion(this, 8);
            }
            frmPrefactura.SetParent(refClientes.Handle, this.MdiParent.Handle);
        }


        private void cmdBuscarVendedor_Click(object sender, EventArgs e)
        {
            refVendedores = lbxVendedores.DefInstance;
            refVendedores.MdiParent = this.MdiParent;
            refVendedores.Show();
            refVendedores.busVenFac(this, 9);
            frmPrefactura.SetParent(refVendedores.Handle, this.MdiParent.Handle);
        }

        internal void txtCodCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == '.') || (e.KeyChar == ','))
            {
                e.Handled = true;
                return;
            }
            else if (Char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
                return;
            }
            else if (Char.IsSymbol(e.KeyChar))
            {
                e.Handled = true;
                return;
            }
            else if (Char.IsPunctuation(e.KeyChar))
            {
                e.Handled = true;
                return;
            }
            else if ((e.KeyChar == (char)(Keys.Enter)) && txtCodCliente.Text != "")
            {
                client1.Codigo = txtCodCliente.Text;

                if (client1.existeCliente())
                {
                    if (client1.estaActivo(client1.Codigo))
                    {
                        client1.cargarDatosCliente();

                        txtNombreCli.Text = client1.Nombre;
                        txtRif.Text = client1.Rif;
                        //se asigna divisa
                        //cboTransporte.Items.Add(client1.DivisaCliente);
                        cboTransporte.SelectedIndex = 0;
                        txtCodVendedor.Text = client1.Vendedor;
                        txtNombreVendedor.Text = vende1.getNombreVen(client1.Vendedor);
                        txtCodVendedor.Focus();
                        txtplazoDias.Text = client1.CondicionPago;
                        txtDescuentoGeneral.Text = client1.DescuentoEnventas.ToString();
                        if ((String.IsNullOrEmpty(client1.Transporte)))
                        {
                            cboTransporte.SelectedIndex = 0;
                        }
                        else
                        {
                            for (int i = 0; i <= cboTransporte.Items.Count; i++)
                            {
                                cboTransporte.SelectedItem = i;
                                if (cboTransporte.SelectedValue.ToString().Equals(client1.Transporte))
                                {
                                    cboTransporte.SelectedIndex = i;
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Solo puede seleccionar clientes Activos!!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                else
                {
                    MessageBox.Show("El Cliente no Existe!!", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public void asignarCondicionPago(string cond)
        {
            DataTable dt = null;
            dt = (DataTable)cboCondicionPago.DataSource;
            cboCondicionPago.SelectedIndex = (dt.AsEnumerable().ToList().FindIndex(
                x => x.Field<string>("conp_codigo") == cond)) == -1 ? 6 : (dt.AsEnumerable().ToList().FindIndex(
                x => x.Field<string>("conp_codigo") == cond));
        }

        public void txtCodVendedor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == '.') || (e.KeyChar == ','))
            {
                e.Handled = true;
                return;
            }
            else if (Char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
                return;
            }
            else if (Char.IsSymbol(e.KeyChar))
            {
                e.Handled = true;
                return;
            }
            else if (Char.IsPunctuation(e.KeyChar))
            {
                e.Handled = true;
                return;
            }
            else if ((e.KeyChar == (char)(Keys.Enter)) && txtCodVendedor.Text != "")
            {
                txtCodVendedor.Text = (String.Format("{0:0000000000}", Convert.ToDouble(txtCodVendedor.Text)));
                txtCodVendedor.Select(txtCodVendedor.Text.Length, 0);

                if (vende1.existeVendedor(txtCodVendedor.Text))
                {
                    if (vende1.estaActivo(txtCodVendedor.Text))
                    {
                        vende1.CodigoV = txtCodVendedor.Text;
                        vende1.cargarDatosVendedor();
                        txtCodVendedor.Text = vende1.CodigoV;
                        txtNombreVendedor.Text = vende1.Nombre;
                        client1.Vendedor = vende1.CodigoV;
                        txtDirEntrega.Focus();
                    }
                    else
                    {
                        MessageBox.Show("Debe Seleccionar un vendedor Activo!!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtCodVendedor.Text = vende1.CodigoV;
                    }
                }
                else
                {
                    MessageBox.Show("El Vendedor no Existe!!", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public void cargarCliVen(string codClie, string codVend)
        {
            client1.Codigo = codClie;
            client1.cargarDatosCliente();
            cboTransporte.Items.Add(client1.DivisaCliente);
            cboTransporte.SelectedIndex = 0;
            vende1.CodigoV = codVend;
            vende1.cargarDatosVendedor();
        }

        public void actuCondiPag()
        {
            //cboCondicionPago.Text = ;
            cboCondicionPago.DataSource = client1.condigPag();
            cboCondicionPago.DisplayMember = "conp_descripcion";
            cboCondicionPago.ValueMember = "conp_codigo";
            //cboCondicionPago.Items.Add(client1.CondicionPago);
            cboCondicionPago.SelectedIndex = 0;
        }

        private void cmdBuscarProducto_Click(object sender, EventArgs e)
        {
            refProductos = frmConsultaArticulos.DefInstance;
            refProductos.MdiParent = this.MdiParent;
            refProductos.Show();
            refProductos.ordernarGrid();
            refProductos.actulizarRefFactura(this);
            frmPrefactura.SetParent(refProductos.Handle, this.MdiParent.Handle);
        }

        public void cargarProducto(string codigoProd)
        {
            int inicioCadena = 0;
            string cadenaTemporal = null;

            if (codigoProd.Length > 11)
            {
                inicioCadena = codigoProd.Length - 11;
                cadenaTemporal = codigoProd.Substring((inicioCadena));
                //cadenaTemporal = codigoProd.Substring((inicioCadena), (codigoProd.Length - 1));
            }
            else
            {
                cadenaTemporal = codigoProd;
            }

            if (productoFactura.existeProdAdmEquiv(cadenaTemporal))
            {
                productoFactura.cargarDatosAdmEquiv(cadenaTemporal);
                if (productoFactura.Estado.ToLower().Equals("inactivo"))
                {
                    MessageBox.Show("El Producto esta Inactivo!!", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    limpiarCamposProducto();
                    txtCodProducto.Focus();
                    return;
                }
                //productoFactura.cargarDatosProd(productoFactura.CodigoProd);
                asignarCamposProd();
                txtCantidad.Text = "" + productoFactura.ValorUnidadPrincipal;
                //if (txtCantidad.Text == "")
                //{
                //    txtCantidad.Text = "1";
                //}
                txtCantidad_KeyPress(new object(), new KeyPressEventArgs((char)(Keys.Enter)));
                //txtCantidad.Focus();
                //if (isEditable())
                //{
                //    soloLectura(false);
                //}
            }
            else
            {
                // formato para el codigo de producto
                codigoProd = String.Format("{0:000000000000000}", Convert.ToDouble(codigoProd));
                if (productoFactura.existeProdAdminv(codigoProd))
                {
                    productoFactura.CodigoProd = codigoProd;
                    productoFactura.cargarDatosProd(productoFactura.CodigoProd);
                    if (productoFactura.Estado.ToLower().Equals("inactivo"))
                    {
                        MessageBox.Show("El Producto esta Inactivo!!", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        limpiarCamposProducto();
                        txtCodProducto.Focus();
                        return;
                    }
                    asignarCamposProd();
                    //if (txtCantidad.Text == "")
                    //{
                    //    txtCantidad.Text = "1";
                    //}
                    //txtCantidad_KeyPress(new object(), new KeyPressEventArgs((char)(Keys.Enter)));
                    txtCantidad.Focus();
                    if (isEditable())
                    {
                        soloLectura(false);
                    }
                }
                else
                {
                    MessageBox.Show("El Producto no Existe!!", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    limpiarCamposProducto();
                    txtCodProducto.Clear();
                    txtCodProducto.Focus();
                }

            }
        }

        private void txtCodProducto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)(Keys.Enter)) && txtCodProducto.Text != "")
            {
                //txtCantidad.Focus();
                productoFactura.limpiarProducto();
                cargarProducto(txtCodProducto.Text);
                if (isEditable())
                {
                    soloLectura(false);
                }
            }
            else
            {
                if (e.KeyChar == (char)(Keys.Space))
                {
                    e.Handled = true;
                }
                else
                {
                    funtxt.OnlyNumbers(sender, e);
                }

            }
        }

        private void asignarCamposProd()
        {
            txtCodProducto.Text = productoFactura.CodigoProd;
            txtNombreProducto.Text = productoFactura.Descripcion;
            cboUnidad.Items.Clear();
            for (int z = 0; z < productoFactura.UnidadMedida.Length; z++)
            {
                cboUnidad.Items.Add(productoFactura.UnidadMedida[z]);
            }
            cboUnidad.SelectedIndex = 0;
            productoFactura.precioProductoClienteUn(cboUnidad.Text, client1.TipoLista, "Bs", productoFactura.CodigoProd, productoFactura.Iva);
            //txtPrecio.Text = productoFactura.Precio.ToString("0.##");
            txtPrecio.Text = productoFactura.Precio.ToString();
        }

        private void asignarCamposProd2()
        {
            txtCodProducto.Text = productoFactura.CodigoProd;
            txtNombreProducto.Text = productoFactura.Descripcion;
            txtPrecio.Text = productoFactura.Precio.ToString();
        }

        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            int tempC;
            if ((e.KeyChar == '.') || (e.KeyChar == ','))
            {
                e.Handled = true;
                return;
            }
            if (Char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
                return;
            }

            if (Char.IsSymbol(e.KeyChar))
            {
                e.Handled = true;
                return;
            }

            if (Char.IsPunctuation(e.KeyChar))
            {
                e.Handled = true;
                return;
            }

            if (e.KeyChar == (char)(Keys.Enter))
            {
                if ((txtPrecio.Text == "") || (txtPrecio.Text == null))
                {
                    MessageBox.Show("El precio tiene que ser Mayor que 0", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (Convert.ToDouble(txtPrecio.Text) <= 0)
                {
                    MessageBox.Show("El precio tiene que ser Mayor que 0", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (modificarArticulo) 
                {   
                    modificarFila(indiceFilaModificar);
                    
                    modificarArticulo = false;
                    indiceFilaModificar = -1;
                    productoFactura.limpiarProducto();
                    limpiarCamposProducto();
                    txtCodProducto.Enabled = true;
                    cmdBuscarProducto.Enabled = true;
                    txtNombreProducto.Enabled = true;
                    cboUnidad.Enabled = true;
                    txtCodProducto.Focus();
                }
                else
                {
                    if (txtCantidad.Text != "" && txtCodProducto.Text != "")
                    {
                        double calculosMultiplo;
                        if (validarCampos())
                        {
                            tempC = (productoFactura.CenCodigoBarras ? (productoFactura.ValorUnidadPrincipal) : (Convert.ToInt32(txtCantidad.Text)));

                            if (!(productoFactura.CostoPromedio == 0) && (tempC > 0))
                            {
                                if ((labelZ.Visible) && (!(productoFactura.isCleanProducto())))
                                {
                                    if (findRowDataGrid(txtCodProducto.Text, (txtPrecio.Text.Replace(",", ".")), cboUnidad.Text))
                                    {
                                        sumarFila(txtCodProducto.Text, txtPrecio.Text.Replace(",", "."), "" + tempC, cboUnidad.Text);
                                    }
                                    else
                                    {
                                        agregarArticulo(-1);
                                    }
                                    limpiarCamposProducto();
                                    txtCodProducto.Focus();
                                }
                                else if ((cetinela1VezCantidad) && (!(productoFactura.isCleanProducto())))
                                {
                                    if (findRowDataGrid(txtCodProducto.Text, (txtPrecio.Text.Replace(",", ".")), cboUnidad.Text))
                                    {
                                        sumarFila(txtCodProducto.Text, txtPrecio.Text.Replace(",", "."), "" + tempC, cboUnidad.Text);
                                    }
                                    else
                                    {
                                        agregarArticulo(-1);
                                    }
                                    limpiarCamposProducto();
                                    txtCodProducto.Focus();
                                }
                                else
                                {
                                    if (centinelaConfirmacion)
                                    {
                                        productoFactura.Precio = Convert.ToDecimal(txtPrecio.Text);
                                        productoFactura.ValorPrecio = Convert.ToDecimal(txtPrecio.Text);
                                        if (findRowDataGrid(txtCodProducto.Text, (txtPrecio.Text.Replace(",", ".")), cboUnidad.Text))
                                        {
                                            sumarFila(txtCodProducto.Text, txtPrecio.Text.Replace(",", "."), "" + tempC, cboUnidad.Text);

                                        }
                                        else
                                        {
                                            agregarArticulo(-1);
                                        }
                                        chkCambioPrecio.Checked = false;
                                        centinelaConfirmacion = false;
                                        txtPrecio.ReadOnly = true;
                                        limpiarCamposProducto();
                                        txtCodProducto.Focus();

                                    }
                                    else if (productoFactura.CenCodigoBarras)
                                    {
                                        if (productoFactura.cargarOfertas(productoFactura.CodigoProd, "" + dateTimePicker1.Value.Year + "-" + dateTimePicker1.Value.Month + "-" + dateTimePicker1.Value.Day + "",
                                        cboUnidad.Text, productoFactura.Iva, client1.TipoLista, tempC))
                                        {
                                            productoFactura.CenPromocion = true;
                                            productoFactura.precioRealProd(cboUnidad.Text, client1.TipoLista, cboTransporte.Text, productoFactura.CodigoProd, productoFactura.Iva);
                                            asignarCamposProd2();
                                            lblPrecio.Text = "" + productoFactura.mostrarTotalProd();
                                            lblPrecio.Visible = true;
                                            labelZ.Visible = true;
                                        }
                                        if (findRowDataGrid(txtCodProducto.Text, (txtPrecio.Text.Replace(",", ".")), cboUnidad.Text))
                                        {
                                            sumarFila(txtCodProducto.Text, txtPrecio.Text.Replace(",", "."), "" + tempC, cboUnidad.Text);
                                        }
                                        else
                                        {
                                            agregarArticulo(-1);
                                        }
                                        limpiarCamposProducto();
                                        txtCodProducto.Focus();
                                    }
                                    else
                                    {
                                        if (productoFactura.cargarOfertas(productoFactura.CodigoProd, "" + dateTimePicker1.Value.Year + "-" + dateTimePicker1.Value.Month + "-" + dateTimePicker1.Value.Day + "",
                                        cboUnidad.Text, productoFactura.Iva, client1.TipoLista, tempC))
                                        {
                                            productoFactura.CenPromocion = true;
                                            productoFactura.precioRealProd(cboUnidad.Text, client1.TipoLista, cboTransporte.Text, productoFactura.CodigoProd, productoFactura.Iva);
                                            asignarCamposProd2();
                                            lblPrecio.Text = "" + productoFactura.mostrarTotalProd();
                                            lblPrecio.Visible = true;
                                            labelZ.Visible = true;
                                            cmdAgregarItem.Focus();

                                            //if (findRowDataGrid(txtCodProducto.Text, (txtPrecio.Text.Replace(",", ".")), cboUnidad.Text))
                                            //{
                                            //sumarFila(txtCodProducto.Text, txtPrecio.Text.Replace(",", "."), "" + tempC, cboUnidad.Text);

                                            //}
                                            //else
                                            //{
                                            //    agregarArticulo(-1);
                                            //}
                                            //limpiarCamposProducto();
                                            //txtCodProducto.Focus();
                                        }
                                        else
                                        {
                                            calculosMultiplo = productoFactura.validarMultipos(productoFactura.CodigoProd, "" + dateTimePicker1.Value.Year + "-" + dateTimePicker1.Value.Month + "-" + dateTimePicker1.Value.Day + "",
                                                cboUnidad.Text, productoFactura.Iva, client1.TipoLista, tempC);
                                            //multipo exacto
                                            if (calculosMultiplo == -1)
                                            {
                                                productoFactura.cargarMutiplos(productoFactura.CodigoProd, "" + dateTimePicker1.Value.Year + "-" + dateTimePicker1.Value.Month + "-" + dateTimePicker1.Value.Day + "",
                                                cboUnidad.Text, productoFactura.Iva, client1.TipoLista, tempC);
                                                productoFactura.precioRealProd(cboUnidad.Text, client1.TipoLista, cboTransporte.Text, productoFactura.CodigoProd, productoFactura.Iva);
                                                productoFactura.CenPromocion = true;
                                                asignarCamposProd2();
                                                //agregarArticulo(-1);
                                                //limpiarCamposProducto();
                                                //txtCodProducto.Focus();
                                                lblPrecio.Text = "" + productoFactura.mostrarTotalProd();
                                                lblPrecio.Visible = true;
                                                cetinela1VezCantidad = true;
                                                cmdAgregarItem.Focus();
                                            }
                                            else
                                            {
                                                //no hay multipo
                                                if (calculosMultiplo == -2)
                                                {
                                                    //if (!(findRowDataGrid(txtCodProducto.Text, txtPrecio.Text, cboUnidad.Text)))
                                                    //{
                                                    //    agregarArticulo(-1);
                                                    //}
                                                    //else
                                                    //{
                                                    //    sumarFila(txtCodProducto.Text, txtPrecio.Text.Replace(",", "."), "" + tempC, cboUnidad.Text);
                                                    //}
                                                    //limpiarCamposProducto();
                                                    //txtCodProducto.Focus();
                                                    lblPrecio.Text = "" + productoFactura.mostrarTotalProd();
                                                    lblPrecio.Visible = true;
                                                    cetinela1VezCantidad = true;
                                                    cmdAgregarItem.Focus();
                                                }
                                                else
                                                {
                                                    //si entra aqui es porque hay multiplo pero existe sobrante
                                                    MessageBox.Show("Solo se pueden Facturar Con Off = " + (tempC - Math.Truncate(calculosMultiplo)) + " Items");
                                                }
                                            }
                                            /*if((productoFactura.validarMultipos(productoFactura.CodigoProd,"" + dateTimePicker1.Value.Year + "-" + dateTimePicker1.Value.Month + "-" + dateTimePicker1.Value.Day + "",
                                                cboUnidad.Text, productoFactura.Iva, client1.TipoLista, Convert.ToInt32(txtCantidad.Text))) != -1 ){
                                    
                                            }else{
                                                agregarArticulo(-1);
                                                limpiarCamposProducto();
                                                txtCodProducto.Focus();
                                            }*/
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (productoFactura.CostoPromedio == 0)
                                {
                                    MessageBox.Show("El Articulo No posee Costo Promedio!!", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    limpiarCamposProducto();
                                    txtCodProducto.Focus();
                                    return;
                                }
                                if (tempC > productoFactura.Existencia)
                                {
                                    MessageBox.Show("No posee existencia para cubrir la Salida!! \n Existencia= " + productoFactura.Existencia, "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    //limpiarCamposProducto();
                                    txtCantidad.Clear();
                                    txtCodProducto.Focus();
                                    return;
                                }

                                if (tempC <= 0)
                                {
                                    MessageBox.Show("La cantidad Debe Ser Mayor que 0 ", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    txtCantidad.Focus();
                                    return;
                                }

                                if (!(totalExistencia(productoFactura.CodigoProd, productoFactura.Existencia, Convert.ToDecimal(tempC))))
                                {
                                    MessageBox.Show("Cantidad Insuficiente para cubrir la Existencia ", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    txtCantidad.Focus();
                                    return;
                                }
                            }
                        }
                    }
                    else
                    {
                        validarCampos();
                    }
                }
            }
        }

        private void agregarArticulo(int fila)
        {
            //decimal precioCliente = 0;
            decimal temp;

            if ((dataGridView1.Rows.Count + 1) > fac1.MaximaCantidadDetalles)
            {
                MessageBox.Show("Este Documento ya contiene la Máxima Cantidad de Items", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (fila < 0)
            {
                dataGridView1.Rows.Add();
                fila = dataGridView1.Rows.Count - 1;
            }

            int tempC = (productoFactura.CenCodigoBarras ? (productoFactura.ValorUnidadPrincipal) : (Convert.ToInt32(txtCantidad.Text)));
            if (isEditable())
            {
                dataGridView1.Rows[fila].Cells["colProducto"].Value = txtNombreProducto.Text;
            }
            else
            {
                dataGridView1.Rows[fila].Cells["colProducto"].Value = productoFactura.Descripcion;
            }

            //precioCliente = productoFactura.Precio;

            //if (!(string.IsNullOrEmpty(txtDescuentoGeneral.Text)) && (txtDescuentoGeneral.Text != " "))
            //{
            //    if (Convert.ToDecimal(txtDescuentoGeneral.Text) > 0)
            //    {

            //        precioCliente = productoFactura.Precio - funtxt.Truncate(((productoFactura.Precio * Convert.ToDecimal(txtDescuentoGeneral.Text)) / 100), 2);
            //    }
            //    else
            //    {
            //        precioCliente = productoFactura.Precio;
            //    }
            //}


            dataGridView1.Rows[fila].Cells["@mov_precio"].Value = txtPrecio.Text.Replace(",", ".");
            //dataGridView1.Rows[fila].Cells["@mov_precio"].Value = precioCliente.ToString("G29").Replace(",",".");
            dataGridView1.Rows[fila].Cells["@mov_cant"].Value = tempC;
            temp = Convert.ToDecimal(dataGridView1.Rows[fila].Cells["@mov_precio"].Value.ToString().Replace(".", ",")) *
                    Convert.ToDecimal(dataGridView1.Rows[fila].Cells["@mov_cant"].Value.ToString());
            //temp = precioCliente * Convert.ToDecimal(dataGridView1.Rows[fila].Cells["@mov_cant"].Value.ToString());
            dataGridView1.Rows[fila].Cells["@mov_precio"].Value = txtPrecio.Text.Replace(",", ".");
            //dataGridView1.Rows[fila].Cells["@mov_total"].Value = temp.ToString("0.##").Replace(",", ".");
            dataGridView1.Rows[fila].Cells["@mov_total"].Value = temp.ToString("0.##").Replace(",", ".");
            dataGridView1.Rows[fila].Cells["@mov_ivatip"].Value = productoFactura.Iva;
            dataGridView1.Rows[fila].Cells["procedencia"].Value = productoFactura.Procedencia;
            if (fac1.TipoDocumento.Equals("DEV"))
            {
                dataGridView1.Rows[fila].Cells["@mov_docaso"].Value = "FAV";
            }
            else
            {
                dataGridView1.Rows[fila].Cells["@mov_docaso"].Value = fac1.TipoDocumento;
            }
            dataGridView1.Rows[fila].Cells["@mov_tipoaso"].Value = "";
            dataGridView1.Rows[fila].Cells["@mov_cencos"].Value = "0000000001";
            dataGridView1.Rows[fila].Cells["@mov_codalm"].Value = "000001";
            dataGridView1.Rows[fila].Cells["@mov_cdcomp"].Value = " ";
            dataGridView1.Rows[fila].Cells["@mov_codcta"].Value = client1.Codigo;
            dataGridView1.Rows[fila].Cells["@mov_codigo"].Value = productoFactura.CodigoProd;
            dataGridView1.Rows[fila].Cells["@mov_codsuc"].Value = "0000" + "01";//temporal
            if (fac1.TipoDocumento.Equals("FAV"))
            {
                dataGridView1.Rows[fila].Cells["@mov_codtra"].Value = "S000";
            }
            else
            {
                dataGridView1.Rows[fila].Cells["@mov_codtra"].Value = "E000";
            }
            dataGridView1.Rows[fila].Cells["@mov_vendedor"].Value = vende1.CodigoV;
            dataGridView1.Rows[fila].Cells["@mov_docume"].Value = fac1.CorrelativoInterno;
            dataGridView1.Rows[fila].Cells["@mov_hora"].Value = DateTime.Now.ToString("hh:mm:ss tt");
            dataGridView1.Rows[fila].Cells["@mov_item"].Value = String.Format("{0:000}", (fila + 1));
            dataGridView1.Rows[fila].Cells["@mov_itemaso"].Value = " ";
            dataGridView1.Rows[fila].Cells["@mov_itemcomp"].Value = " ";
            if (productoFactura.CenPromocion)
            {
                dataGridView1.Rows[fila].Cells["@mov_lista"].Value = "Z";
            }
            else if (centinelaConfirmacion)
            {
                dataGridView1.Rows[fila].Cells["@mov_lista"].Value = "X";
            }
            else
            {
                dataGridView1.Rows[fila].Cells["@mov_lista"].Value = client1.TipoLista;
            }
            dataGridView1.Rows[fila].Cells["colMed"].Value = cboUnidad.Text;
            dataGridView1.Rows[fila].Cells["@mov_lote"].Value = " ";
            dataGridView1.Rows[fila].Cells["@mov_tipdoc"].Value = fac1.TipoDocumento;
            dataGridView1.Rows[fila].Cells["@mov_tipo"].Value = "V";
            dataGridView1.Rows[fila].Cells["@mov_undmed"].Value = cboUnidad.Text;
            dataGridView1.Rows[fila].Cells["@mov_usuario"].Value = referenciaPrincipal.usuarioActual.Id;
            dataGridView1.Rows[fila].Cells["@mov_fechven"].Value = "" + DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day;
            dataGridView1.Rows[fila].Cells["@mov_fecha"].Value = "" + DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day;
            dataGridView1.Rows[fila].Cells["@mov_bandas"].Value = "0";
            if (fac1.TipoDocumento.Equals("DEV"))
            {
                dataGridView1.Rows[fila].Cells["@mov_contab"].Value = 1;
            }
            else
            {
                dataGridView1.Rows[fila].Cells["@mov_contab"].Value = -1;
            }
            dataGridView1.Rows[fila].Cells["@mov_costo"].Value = productoFactura.CostoPromedio;
            dataGridView1.Rows[fila].Cells["@mov_cxund"].Value = productoFactura.ValorUnidadPrincipal;
            //dataGridView1.Rows[fila].Cells["@mov_desc"].Value = txtDescuentoGeneral.Text.Replace(",", ".");//temporal
            dataGridView1.Rows[fila].Cells["@mov_desc"].Value = "0";
            dataGridView1.Rows[fila].Cells["@mov_expendio"].Value = "0";
            dataGridView1.Rows[fila].Cells["@mov_export"].Value = "0";
            if (fac1.TipoDocumento.Equals("DEV"))
            {
                dataGridView1.Rows[fila].Cells["@mov_fisico"].Value = 1;
            }
            else
            {
                dataGridView1.Rows[fila].Cells["@mov_fisico"].Value = -1;
            }



            //correccion mov_import siempre va a ser igual a la existencia del sistema
            dataGridView1.Rows[fila].Cells["@mov_import"].Value = productoFactura.Existencia;

            dataGridView1.Rows[fila].Cells["@mov_otimp"].Value = tempC;
            dataGridView1.Rows[fila].Cells["@mov_impprodu"].Value = "0";
            dataGridView1.Rows[fila].Cells["@mov_invact"].Value = "1";
            dataGridView1.Rows[fila].Cells["@mov_iva"].Value = funtxt.Truncate(((Convert.ToDecimal(dataGridView1.Rows[fila].Cells["@mov_total"].Value.ToString().Replace(".", ",")) * productoFactura.ValorIva) / 100), 6);
            if (fac1.TipoDocumento.Equals("DEV"))
            {
                dataGridView1.Rows[fila].Cells["@mov_logico"].Value = 1;
            }
            else
            {
                dataGridView1.Rows[fila].Cells["@mov_logico"].Value = -1;
            }
            dataGridView1.Rows[fila].Cells["@mov_mtocom"].Value = "0";
            dataGridView1.Rows[fila].Cells["@mov_memo"].Value = txtNombreProducto.Text;
            //colProducto
            dataGridView1.Rows[fila].Cells["@mov_talla"].Value = "0";
            dataGridView1.Rows[fila].Cells["@mov_color"].Value = "0";
            dataGridView1.Rows[fila].Cells["@mov_arancel"].Value = "0";
            dataGridView1.Rows[fila].Cells["@mov_kilos"].Value = "0";
            dataGridView1.Rows[fila].Cells["@mov_impuesto"].Value = "0";
            dataGridView1.Rows[fila].Cells["@mov_cosmon"].Value = productoFactura.Precio;
            dataGridView1.Rows[fila].Cells["@mov_totalmon"].Value = temp;
            //poner propiedades clase producto si se cambia el precio poner el de la base de datos
            if (dataGridView1.Rows[fila].Cells["@mov_lista"].Value.ToString().Equals("Z"))
            {
                dataGridView1.Rows[fila].Cells["@mov_precio_ini"].Value = productoFactura.ValorPrecio;
            }
            else if (centinelaConfirmacion)
            {
                //dataGridView1.Rows[fila].Cells["@mov_precio_ini"].Value = Convert.ToString(productoFactura.precioRealProd2(cboUnidad.Text, client1.TipoLista, cboDivisa.Text, productoFactura.CodigoProd, productoFactura.Iva)).Replace(",", ".");
                dataGridView1.Rows[fila].Cells["@mov_itemcomp"].Value = temporalCambioPrecio;
                dataGridView1.Rows[fila].Cells["@mov_precio_ini"].Value = "0.00";
                temporalCambioPrecio = "";
            }
            else
            {
                dataGridView1.Rows[fila].Cells["@mov_precio_ini"].Value = "0.00";
            }
            dataGridView1.Rows[fila].Cells["@mov_porciva"].Value = productoFactura.ValorIva;

            //se llama a la funcion para acumular bases e iva
            fac1.acumularBases(temp, productoFactura.Iva);
            switch (productoFactura.Iva)
            {
                case "GN":
                    fac1.acumularIvas(fac1.BaseGN, productoFactura.Iva);
                    break;
                case "RD":
                    fac1.acumularIvas(fac1.BaseRD, productoFactura.Iva);
                    break;
            };

            fac1.acumularCostos(productoFactura.CostoPromedio, tempC, productoFactura.Procedencia);
            fac1.acumularBases2(tempC, Convert.ToDecimal(dataGridView1.Rows[fila].Cells["@mov_precio"].Value.ToString().Replace(".", ",")), productoFactura.Procedencia);
            //fac1.acumularIvas(temp, productoFactura.Iva);
            actualizarTextPagos(fila);
            acumularItems(tempC);

            //agregado por boton regresar
            if (dataGridView2.Rows.Count > 0)
            {
                txtContado.Text = txtNeto.Text;
                cambio();
            }
            labelZ.Visible = false;
            cetinela1VezCantidad = false;
            lblPrecio.Text = "";
            lblPrecio.Visible = false;
        }

        private void cmdAgregarItem_Click(object sender, EventArgs e)
        {
            txtCantidad_KeyPress(new object(), new KeyPressEventArgs((char)(Keys.Enter)));
        }

        private bool validarCampos()
        {
            bool centinela = true;

            if (txtCodCliente.Text == "")
            {
                MessageBox.Show("El cliente es un campo Obligatorio!!", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCodCliente.Focus();
                centinela = false;
                return centinela;
            }

            if (txtCodVendedor.Text == "")
            {
                MessageBox.Show("El vendedor es un campo Obligatorio!!", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCodVendedor.Focus();
                centinela = false;
                return centinela;
            }

            if (txtCodProducto.Text == "")
            {
                MessageBox.Show("El producto es un campo Obligatorio!!", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCodProducto.Focus();
                centinela = false;
                return centinela;
            }

            if (txtCantidad.Text == "")
            {
                MessageBox.Show("La cantidad es un campo Obligatorio!!", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCantidad.Focus();
                centinela = false;
                return centinela;
            }
            return centinela;
        }

        private void limpiarCamposProducto()
        {
            txtCodProducto.Clear();
            txtNombreProducto.Clear();
            cboUnidad.Items.Clear();
            txtCantidad.Clear();
            txtPrecio.Clear();
            productoFactura.limpiarProducto();
            soloLectura(true);
        }

        internal void actualizarTextPagos(int fila)
        {

            txtSubTotal.Text = Convert.ToString(Convert.ToDecimal(txtSubTotal.Text) + Convert.ToDecimal(dataGridView1.Rows[fila].Cells["@mov_total"].Value.ToString().Replace(".", ",")));
            txtBase.Text = Convert.ToString(Convert.ToDecimal(txtSubTotal.Text) - Convert.ToDecimal(txtDesItems.Text));
            txtTotalProduc.Text = Convert.ToString(Convert.ToDecimal(txtTotalProduc.Text) + Convert.ToDecimal(dataGridView1.Rows[fila].Cells["@mov_cant"].Value.ToString()));
            txtIva.Text = Convert.ToString(fac1.IvaGN + fac1.IvaRD);
            txtNeto.Text = Convert.ToString((Convert.ToDecimal(txtSubTotal.Text) - Convert.ToDecimal(txtDesItems.Text)) + (fac1.IvaGN + fac1.IvaRD));
            //por los momentos
            fac1.TotalBase = Convert.ToDecimal(txtBase.Text);
            fac1.TotalNeto = Convert.ToDecimal(txtNeto.Text);

        }

        private void restarTextPagos(int fila)
        {
            txtSubTotal.Text = Convert.ToString(Convert.ToDecimal(txtSubTotal.Text) - Convert.ToDecimal(dataGridView1.Rows[fila].Cells["@mov_total"].Value.ToString().Replace(".", ",")));
            txtBase.Text = Convert.ToString(Convert.ToDecimal(txtSubTotal.Text) - Convert.ToDecimal(txtDesItems.Text));
            txtTotalProduc.Text = Convert.ToString(Convert.ToDecimal(txtTotalProduc.Text) - Convert.ToDecimal(dataGridView1.Rows[fila].Cells["@mov_cant"].Value.ToString()));
            txtIva.Text = Convert.ToString(fac1.IvaGN + fac1.IvaRD);
            txtNeto.Text = Convert.ToString((Convert.ToDecimal(txtSubTotal.Text) - Convert.ToDecimal(txtDesItems.Text)) + (fac1.IvaGN + fac1.IvaRD));
            //por los momentos
            fac1.TotalBase = Convert.ToDecimal(txtBase.Text);
            fac1.TotalNeto = Convert.ToDecimal(txtNeto.Text);
        }

        private void cmdEliminarItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {

                switch ((dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["@mov_ivatip"].Value.ToString()))
                {
                    case "GN":
                        fac1.restarIvaElemento(Convert.ToDecimal(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["@mov_iva"].Value.ToString().Replace('.', ',')), "GN");
                        break;
                    case "RD":
                        fac1.restarIvaElemento(Convert.ToDecimal(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["@mov_iva"].Value.ToString().Replace('.', ',')), "RD");
                        break;
                };


                fac1.restarBases((Convert.ToDecimal(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["@mov_total"].Value.ToString().Replace(".", ","))),
                    dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["@mov_ivatip"].Value.ToString());


                /*fac1.restarIvas((Convert.ToDecimal(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["@mov_total"].Value.ToString().Replace(".", ","))),
                    dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["@mov_ivatip"].Value.ToString());*/

                //fac1.acumularBases(temp, productoFactura.Iva);
                //modificado para redondear segun 3 decimal 08:56 a.m. 16/07/2013


                fac1.restarCosto((Convert.ToDecimal((dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["@mov_costo"].Value.ToString().Replace(".", ",")))),
                    (Convert.ToInt32((dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["@mov_cant"].Value.ToString()))),
                    (dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["procedencia"].Value.ToString()));

                fac1.restarBases2((Convert.ToInt32((dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["@mov_cant"].Value.ToString()))),
                    (Convert.ToDecimal(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["@mov_precio"].Value.ToString().Replace(".", ","))),
                    (dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["procedencia"].Value.ToString()));
                restarTextPagos(dataGridView1.CurrentRow.Index);

                dataGridView1.Rows.RemoveAt(dataGridView1.CurrentRow.Index);
                if ((dataGridView1.Rows.Count == 0) && (dataGridView2.Rows.Count > 0))
                {
                    limpiarTotales();
                    int fila = -1;
                    do
                    {
                        fila = (dataGridView2.Rows.Count - 1);
                        restarMontos(fila);
                        dataGridView2.Rows.RemoveAt(fila);
                    } while (fila != 0);
                    limpiarTotales2();
                }
                else if ((dataGridView1.Rows.Count > 0) && (dataGridView2.Rows.Count > 0))
                {
                    limpiarTotales();
                    txtTotalProduc.Text = "0.00";
                    for (int z = 0; z < dataGridView1.Rows.Count; z++)
                    {
                        actualizarTextPagos(z);
                    }
                    txtContado.Text = txtNeto.Text;
                    cambio();

                }

                /*for (int z = 0; z < dataGridView1.Rows.Count; z++) {
                    restarTextPagos(z);
                }*/
                recalcularItems();
                txtCodProducto.Focus();
            }
            else
            {
                MessageBox.Show("No hay Items para Eliminar!!", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void selectText(object sender, MouseEventArgs e)
        {
            TextBox a = (TextBox)sender;
            //if (a.Text == "")
            //{
            //    a.Text = "0,00000000";
            //}
            a.SelectAll();
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Space))
            {
                e.Handled = true;
            }
            else
            {
                if ((e.KeyChar == (char)(Keys.Enter)) && txtDescuentoGeneral.Text != null)
                {
                    SendKeys.Send("{TAB}");
                }
                else
                {
                    funtxt.OnlyNumbers(sender, e);
                }
            }
        }

        private void txtplazoDias_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Space))
            {
                e.Handled = true;
            }
            else
            {
                if ((e.KeyChar == (char)(Keys.Enter)) && txtDescuentoGeneral.Text != null)
                {
                    SendKeys.Send("{TAB}");
                }
                else
                {
                    funtxt.OnlyNumbers(sender, e);
                }
            }
        }

        private bool totalExistencia(string codProducto, decimal existencia, decimal pedido)
        {
            bool centinela = true;
            decimal cantidadAcumulada = 0;

            cantidadAcumulada = pedido;

            for (int x = 0; x < dataGridView1.Rows.Count; x++)
            {
                if (dataGridView1.Rows.Count == 0)
                {
                    centinela = true;
                    return centinela;
                }
                else
                {
                    if ((dataGridView1.Rows[x].Cells["@mov_codigo"].Value.ToString()).Equals(codProducto))
                    {
                        cantidadAcumulada = cantidadAcumulada + Convert.ToDecimal(dataGridView1.Rows[x].Cells["@mov_cant"].Value.ToString());
                        if (cantidadAcumulada > existencia)
                        {
                            centinela = false;
                            return centinela;
                        }
                        else
                        {
                            centinela = true;
                        }
                    }
                }
            }
            return centinela;
        }

        private bool findRowDataGrid(string codProd, string precio, string unidad)
        {
            bool centinela = false;
            
            
            precio = precioDesCliente(Convert.ToDecimal(precio.Replace(".", ","))).ToString();

            foreach (DataGridViewRow fila in dataGridView1.Rows)
            {
                if ((fila.Cells["@mov_codigo"].Value.ToString().Equals(codProd)) && (fila.Cells["@mov_precio"]).Value.ToString().Equals(precio)
                     && (fila.Cells["@mov_undmed"].Value.ToString().Equals(unidad)))
                {
                    centinela = true;
                }
            }

            return centinela;
        }

        private void sumarFila(string codProd, string precio, string cantidad, string unidad)
        {
            int indiceF = -1;
            decimal temp;
            decimal temp2;
            decimal precioCliente = 0;

            precio = precioDesCliente(Convert.ToDecimal(precio.Replace(".", ","))).ToString();

            foreach (DataGridViewRow fila in dataGridView1.Rows)
            {
                if ((fila.Cells["@mov_codigo"].Value.ToString().Equals(codProd)) && (fila.Cells["@mov_precio"]).Value.ToString().Equals(precio)
                     && (fila.Cells["@mov_undmed"].Value.ToString().Equals(unidad)))
                {
                    indiceF = fila.Index;
                    if (centinelaConfirmacion)
                    {
                        dataGridView1.Rows[indiceF].Cells["@mov_itemcomp"].Value = temporalCambioPrecio;
                        temporalCambioPrecio = "";
                    }
                    /* se calcula el precio de lo que va a restar*/
                    temp2 = funtxt.Truncate((Convert.ToDecimal(dataGridView1.Rows[indiceF].Cells["@mov_precio"].Value.ToString().Replace(".", ",")) *
                     Convert.ToDecimal(dataGridView1.Rows[indiceF].Cells["@mov_cant"].Value.ToString())), 2);

                    /* para calculos de factura se restan las bases y los ivas*/
                    fac1.restarBases(temp2, dataGridView1.Rows[indiceF].Cells["@mov_ivatip"].Value.ToString());

                    //fac1.restarIvas(temp2, dataGridView1.Rows[indiceF].Cells["@mov_ivatip"].Value.ToString());

                    fac1.restarCosto((Convert.ToDecimal((dataGridView1.Rows[indiceF].Cells["@mov_costo"].Value.ToString()))),
                    (Convert.ToInt32((dataGridView1.Rows[indiceF].Cells["@mov_cant"].Value.ToString()))),
                    (dataGridView1.Rows[indiceF].Cells["procedencia"].Value.ToString()));

                    fac1.restarBases2((Convert.ToInt32((dataGridView1.Rows[indiceF].Cells["@mov_cant"].Value.ToString()))),
                    (Convert.ToDecimal(dataGridView1.Rows[indiceF].Cells["@mov_precio"].Value.ToString().Replace(".", ","))),
                    (dataGridView1.Rows[indiceF].Cells["procedencia"].Value.ToString()));


                    dataGridView1.Rows[indiceF].Cells["@mov_cant"].Value = (Convert.ToInt32(fila.Cells["@mov_cant"].Value.ToString()) + Convert.ToInt32(cantidad));
                    dataGridView1.Rows[indiceF].Cells["@mov_otimp"].Value = (Convert.ToInt32(fila.Cells["@mov_otimp"].Value.ToString()) + Convert.ToInt32(cantidad));

                    //if (!(string.IsNullOrEmpty(txtDescuentoGeneral.Text)) && (txtDescuentoGeneral.Text != " "))
                    //{
                    //    if (Convert.ToDecimal(txtDescuentoGeneral.Text) > 0)
                    //    {

                    //        precioCliente = productoFactura.Precio - funtxt.Truncate(((productoFactura.Precio * Convert.ToDecimal(txtDescuentoGeneral.Text)) / 100), 2);
                    //    }
                    //    else
                    //    {
                    //        precioCliente = productoFactura.Precio;
                    //    }
                    //}

                    precioCliente = productoFactura.Precio;

                    temp = precioCliente * Convert.ToDecimal(dataGridView1.Rows[indiceF].Cells["@mov_cant"].Value.ToString());
                    Convert.ToDecimal(dataGridView1.Rows[indiceF].Cells["@mov_cant"].Value.ToString());
                    dataGridView1.Rows[indiceF].Cells["@mov_total"].Value = temp.ToString("0.##").Replace(",", ".");
                    dataGridView1.Rows[indiceF].Cells["@mov_iva"].Value = funtxt.Truncate(((Convert.ToDecimal(dataGridView1.Rows[indiceF].Cells["@mov_total"].Value.ToString().Replace(".", ",")) * productoFactura.ValorIva) / 100), 6);

                    ////se llama a la funcion para acumular bases e iva
                    fac1.acumularBases(temp, productoFactura.Iva);
                    //fac1.acumularIvas(temp, productoFactura.Iva);

                    //modificado para redondear segun 3 decimal 08:56 a.m. 16/07/2013
                    switch ((dataGridView1.Rows[indiceF].Cells["@mov_ivatip"].Value.ToString()))
                    {
                        case "GN":
                            fac1.acumularIvas(fac1.BaseGN, "GN");
                            break;
                        case "RD":
                            fac1.acumularIvas(fac1.BaseRD, "RD");
                            break;
                    };



                    fac1.acumularCostos((Convert.ToDecimal((dataGridView1.Rows[indiceF].Cells["@mov_costo"].Value.ToString()))),
                    (Convert.ToInt32((dataGridView1.Rows[indiceF].Cells["@mov_cant"].Value.ToString()))),
                    (dataGridView1.Rows[indiceF].Cells["procedencia"].Value.ToString()));
                    fac1.acumularBases2((Convert.ToInt32(dataGridView1.Rows[indiceF].Cells["@mov_cant"].Value)),
                    Convert.ToDecimal(dataGridView1.Rows[indiceF].Cells["@mov_precio"].Value.ToString().Replace(".", ",")), (dataGridView1.Rows[indiceF].Cells["procedencia"].Value.ToString()));

                    limpiarTotales();

                    for (int z = 0; z < dataGridView1.Rows.Count; z++)
                    {
                        actualizarTextPagos(z);
                    }
                    recalcularItems();
                    //agregado por boton regresar
                    if (dataGridView2.Rows.Count > 0)
                    {
                        txtContado.Text = txtNeto.Text;
                        cambio();
                    }
                }
            }
            labelZ.Visible = false;
            cetinela1VezCantidad = false;
            lblPrecio.Text = "";
            lblPrecio.Visible = false;
        }

        private void limpiarTotales()
        {
            txtSubTotal.Text = "0.00";
            txtDesItems.Text = "0.00";
            txtBase.Text = "0.00";
            txtTotalProduc.Text = "0.00";
            txtIva.Text = "0.00";
            txtNeto.Text = "0.00";
        }


        private bool isEditable()
        {
            bool centinela = false;
            if (productoFactura.Editable != null)
            {
                if (productoFactura.Editable.Equals("Si"))
                {
                    centinela = true;
                }
            }
            return centinela;
        }

        private void soloLectura(bool valor)
        {
            txtNombreProducto.ReadOnly = valor;
        }

        private void cboCondicionPago_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
                SendKeys.Send("{TAB}");
        }

        private void cboDivisa_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtCodProducto.Focus();
        }

        private void cmdConfirmar_Click(object sender, EventArgs e)
        {
            /*if (operacion != "editar")
            {
                actualizarCliVen();
                guardarFactura();
                fac1.DgvItems = this.dataGridView1;
                fac1.ClienteFacturar = this.client1;
                fac1.VendedorFactura = this.vende1;
                frmReporteP = frmReportePedido.DefInstance;
                frmReporteP.armarReporte((fac1.rptPedidoM(this.client1, referenciaPrincipal.usuarioActual.Codigo, this.fac1, cboTransporte.Text,true)), referenciaPrincipal.empresaActual.Nombre, referenciaPrincipal.empresaActual.Rif,true);
                frmReporteP.MdiParent = this.MdiParent;
                frmReporteP.Show();

                /*frmReporte = frmContePedido.DefInstance;
                frmReporte.armarReporte(fac1.rtpPedido(cboCondicionPago.Text, txtplazoDias.Text, referenciaPrincipal.usuarioActual.NombreUsuario),referenciaPrincipal.empresaActual.Nombre, referenciaPrincipal.empresaActual.Rif);
                frmReporte.Show();*/
                /*limpiarCompleto();
                activarCancelarFactura(false);
                activarItemsFactura(true);
                fac1.limpiarFactura();
                this.Close();
                return;
            }*/
            if (operacion.Equals("editar"))
            {
                actualizarCliVen();
                fac1.ActualizarSentenciaCabecerpre(client1, vende1, referenciaPrincipal.empresaActual.BaseDatosActual, referenciaPrincipal.usuarioActual.Id, referenciaPrincipal.usuarioActual.NumeroCaja, txtCambio.Text,
                    txtPagado.Text, txtContado.Text);
                fac1.guardarProntoPagos(fac1);
                int valor = fac1.actualizarCabeceraPed();
                if (valor == -1)
                {
                    fac1.borrarDetallePre(this.dataGridView1);
                    fac1.guardarDetallePre(dataGridView1);
                    MessageBox.Show("Documento Modificado Existosamente", "Exito ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    limpiarCompleto();
                    fac1.limpiarFactura();
                    this.Close();
                }
                return;
            }
        }

        private void acumularItems(int cantidad)
        {
            fac1.TotalItems = fac1.TotalItems + cantidad;
        }

        private void recalcularItems()
        {
            fac1.TotalItems = 0;
            if (dataGridView1.Rows.Count > 0)
            {
                for (int x = 0; x < dataGridView1.Rows.Count; x++)
                {
                    fac1.TotalItems = fac1.TotalItems + Convert.ToInt32(dataGridView1.Rows[x].Cells["@mov_cant"].Value.ToString());
                }
            }
        }

        public void limpiarCompleto()
        {
            limpiarCamposProducto();
            limpiarTotales();
            limpiarTotales2();
            limpiarDatos();
            client1.limpiarCliente();

            if (dataGridView1.RowCount > 0)
            {
                if (dataGridView1.DataSource != null)
                {
                    //dataGridView1.DataSource = new DataTable();
                    //filasDGV1 = dataGridView1.Rows.Count;
                    //for (int x = 0; x < filasDGV1; x++) {
                    //    if (x > 0)
                    //    {
                    //        dataGridView1.Rows.RemoveAt(x - 1);
                    //    }
                    //    else {
                    //        dataGridView1.Rows.RemoveAt(x);
                    //    }
                    //}
                    DataTable dtTemp2 = (DataTable)dataGridView1.DataSource;
                    DataTable estruc1 = dtTemp2.Clone();
                    dataGridView1.DataSource = (estruc1);
                }
                else
                {
                    dataGridView1.Rows.Clear();
                    //dataGridView1.Columns[0].HeaderText
                    //dataGridView2.RowCount = 0;
                    //MessageBox.Show("" + dataGridView1.Columns["@mov_precio_ini"]);
                    //dataGridView1.Refresh();
                }

            }

            if (dataGridView2.RowCount > 0)
            {
                if (dataGridView2.DataSource != null)
                {
                    //filasDGV2 = dataGridView2.Rows.Count;
                    //for (int y = 0; y < filasDGV2; y++)
                    //{
                    //    if (y > 0)
                    //    {
                    //        dataGridView2.Rows.RemoveAt(y - 1);
                    //    }
                    //    else {
                    //        dataGridView2.Rows.RemoveAt(y);
                    //    }
                    //}
                    DataTable dtTemp = (DataTable)dataGridView2.DataSource;
                    DataTable estruc2 = dtTemp.Clone();
                    dataGridView2.DataSource = (estruc2);
                }
                else
                {
                    dataGridView2.RowCount = 0;
                }

            }
        }

        private void limpiarTotales2()
        {
            txtMontoCheques.Clear();
            txtEfectivo.Clear();
            //txtMontoEfect2.Clear();
            txtPagado.Clear();
            txtMontoTarjetas.Clear();
            txtContado.Clear();

            txtMontoCheques.Text = "0.00";
            txtMontoTarjetas.Text = "0.00";
            txtEfectivo.Text = "0.00";
            txtContado.Text = "0.00";
            txtCambio.Text = "0.00";
            txtPagado.Text = "0.00";
        }

        private void limpiarDatos()
        {
            txtnumCont.Clear();
            txtCodCliente.Clear();
            txtNombreCli.Clear();
            txtCodVendedor.Clear();
            txtNombreVendedor.Clear();
            txtDescuentoGeneral.Text = "0";
            txtplazoDias.Text = "0";
            cboCondicionPago.Text = "";
            cboTransporte.Text = "";
            txtRif.Text = "";
            txtCorrelativo.Text = "";
            txtDirEntrega.Text = "";
            cboCondicionPago.SelectedIndex = 6;
        }

        private void cmdCancelarF_Click(object sender, EventArgs e)
        {
            DialogResult respuesta;
            respuesta = MessageBox.Show("¿Desea Salir del Modulo y Cancelar el Documento Actual?", "Cancelar Documento", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (respuesta == System.Windows.Forms.DialogResult.Yes)
            {
                limpiarCompleto();
                this.Close();
            }
            else
            {
                return;
            }
        }

        internal void actualizarCorrelativoGrid()
        {
            foreach (DataGridViewRow fila in dataGridView1.Rows)
            {
                fila.Cells["@mov_docume"].Value = fac1.CorrelativoInterno;
            }
        }

        private void llenarModoPago()
        {
            cboModoPago.Items.Add("Efectivo");
            cboModoPago.Items.Add("Tarjeta de Debito");
            cboModoPago.Items.Add("Tarjeta de Credito");
            cboModoPago.Items.Add("Cheque");
            cboModoPago.SelectedIndex = 0;
        }

        private void cmdAgregarPago_Click(object sender, EventArgs e)
        {
            string fechaMes = null;
            if ((txtBanco.Text != "") && (txtNumeroT.Text != "") && (txtMontoAbonar.Text != "") && (!cboModoPago.Text.Equals("Efectivo")))
            {
                dataGridView2.Rows.Add();
                int fila = dataGridView2.Rows.Count - 1;
                dataGridView2.Rows[fila].Cells["codTipo"].Value = bancoFac.CodBanco;
                dataGridView2.Rows[fila].Cells["detalleTipo"].Value = txtBanco.Text;
                dataGridView2.Rows[fila].Cells["codNum"].Value = txtNumeroT.Text;
                dataGridView2.Rows[fila].Cells["@movc_monto"].Value = txtMontoAbonar.Text;


                if (cboModoPago.Text.Equals("Tarjeta de Debito"))
                {
                    dataGridView2.Rows[fila].Cells["@mocv_forpag"].Value = "TARJETA-D";
                    dataGridView2.Rows[fila].Cells["@movc_tipoctaban"].Value = "DEBITO";
                }
                if (cboModoPago.Text.Equals("Tarjeta de Credito"))
                {
                    dataGridView2.Rows[fila].Cells["@mocv_forpag"].Value = "TARJETA-C";
                    dataGridView2.Rows[fila].Cells["@movc_tipoctaban"].Value = "CREDITO";
                }
                if (cboModoPago.Text.Equals("Cheque"))
                {
                    dataGridView2.Rows[fila].Cells["@mocv_forpag"].Value = cboModoPago.Text.ToUpper();
                    dataGridView2.Rows[fila].Cells["@movc_tipoctaban"].Value = "N/A";
                }

                dataGridView2.Rows[fila].Cells["@movc_cuentacheq"].Value = txtNumeroT.Text;
                dataGridView2.Rows[fila].Cells["@movc_numero"].Value = txtNumeroT.Text;

                dataGridView2.Rows[fila].Cells["@movc_codmaestr"].Value = client1.Codigo;
                dataGridView2.Rows[fila].Cells["@movc_numdoc"].Value = fac1.CorrelativoInterno;
                dataGridView2.Rows[fila].Cells["@movc_descrioper"].Value = "Mov Caja en Ventas";
                dataGridView2.Rows[fila].Cells["@movc_tipomovc"].Value = "MOVCAJAV";
                if (fac1.TipoDocumento.Equals("FAV"))
                {
                    dataGridView2.Rows[fila].Cells["@movc_operacion"].Value = "D";
                }
                if (fac1.TipoDocumento.Equals("DEV"))
                {
                    dataGridView2.Rows[fila].Cells["@movc_operacion"].Value = "C";
                }

                dataGridView2.Rows[fila].Cells["@movc_divisa"].Value = cboTransporte.Text;
                if (DateTime.Now.Month < 10)
                {
                    fechaMes = "0" + DateTime.Now.Month;
                }
                else
                {
                    fechaMes = "" + DateTime.Now.Month;
                }
                dataGridView2.Rows[fila].Cells["@movc_fchemision"].Value = "" + (DateTime.Now.Day).ToString().PadLeft(2, '0') + "/" + fechaMes + "" + "/" + DateTime.Now.Year;
                dataGridView2.Rows[fila].Cells["@movc_hora"].Value = DateTime.Now.ToString("hh:mm:ss tt");
                dataGridView2.Rows[fila].Cells["@movc_vendedor"].Value = client1.Vendedor;
                dataGridView2.Rows[fila].Cells["@movc_codcaja"].Value = referenciaPrincipal.usuarioActual.NumeroCaja;
                dataGridView2.Rows[fila].Cells["@movc_estatus"].Value = "Activo";
                dataGridView2.Rows[fila].Cells["@movc_codtipopag"].Value = bancoFac.CodBanco;

                if (Convert.ToDecimal(txtCambio.Text.Replace(".", ",")) > 0)
                {
                    dataGridView2.Rows[fila].Cells["@movc_valcam"].Value = txtCambio.Text;
                }
                else
                {
                    dataGridView2.Rows[fila].Cells["@movc_valcam"].Value = "0.00";
                }

                dataGridView2.Rows[fila].Cells["@movc_memo"].Value = " ";

                calcularPagar(cboModoPago.Text);
                limpiarCancelarBasico();
                return;
            }
            else
            {
                if (cboModoPago.Text.Equals("Efectivo"))
                {
                    if (txtMontoAbonar.Text != "")
                    {
                        dataGridView2.Rows.Add();
                        int fila = dataGridView2.Rows.Count - 1;
                        dataGridView2.Rows[fila].Cells["codTipo"].Value = "N/A";
                        dataGridView2.Rows[fila].Cells["detalleTipo"].Value = "N/A";
                        dataGridView2.Rows[fila].Cells["codNum"].Value = "N/A";
                        dataGridView2.Rows[fila].Cells["@mocv_forpag"].Value = cboModoPago.Text;
                        dataGridView2.Rows[fila].Cells["@movc_monto"].Value = txtMontoAbonar.Text;
                        dataGridView2.Rows[fila].Cells["@mocv_forpag"].Value = "EFECTIVO";
                        dataGridView2.Rows[fila].Cells["@movc_tipoctaban"].Value = "N/A";
                        dataGridView2.Rows[fila].Cells["@movc_codmaestr"].Value = client1.Codigo;
                        dataGridView2.Rows[fila].Cells["@movc_numdoc"].Value = fac1.CorrelativoInterno;
                        dataGridView2.Rows[fila].Cells["@movc_descrioper"].Value = "Mov Caja en Ventas";
                        dataGridView2.Rows[fila].Cells["@movc_tipomovc"].Value = "MOVCAJAV";
                        dataGridView2.Rows[fila].Cells["@movc_divisa"].Value = cboTransporte.Text;
                        if (DateTime.Now.Month < 10)
                        {
                            fechaMes = "0" + DateTime.Now.Month;
                        }
                        else
                        {
                            fechaMes = "" + DateTime.Now.Month;
                        }
                        dataGridView2.Rows[fila].Cells["@movc_fchemision"].Value = "" + DateTime.Now.Day + "/" + fechaMes + "" + "/" + DateTime.Now.Year;
                        dataGridView2.Rows[fila].Cells["@movc_hora"].Value = DateTime.Now.ToString("hh:mm:ss tt");
                        dataGridView2.Rows[fila].Cells["@movc_vendedor"].Value = client1.Vendedor;
                        dataGridView2.Rows[fila].Cells["@movc_codcaja"].Value = "0002";
                        dataGridView2.Rows[fila].Cells["@movc_estatus"].Value = "Activo";

                        if (fac1.TipoDocumento.Equals("FAV"))
                        {
                            dataGridView2.Rows[fila].Cells["@movc_operacion"].Value = "D";
                        }
                        if (fac1.TipoDocumento.Equals("DEV"))
                        {
                            dataGridView2.Rows[fila].Cells["@movc_operacion"].Value = "C";
                        }
                        dataGridView2.Rows[fila].Cells["@movc_codtipopag"].Value = "N/A";
                        dataGridView2.Rows[fila].Cells["@movc_cuentacheq"].Value = " ";
                        dataGridView2.Rows[fila].Cells["@movc_numero"].Value = " ";
                        dataGridView2.Rows[fila].Cells["@movc_tipomovc"].Value = "MOVCAJAV";

                        if (Convert.ToDecimal(txtCambio.Text.Replace(".", ",")) > 0)
                        {
                            dataGridView2.Rows[fila].Cells["@movc_valcam"].Value = txtCambio.Text;
                        }
                        else
                        {
                            dataGridView2.Rows[fila].Cells["@movc_valcam"].Value = "0.00";
                        }

                        dataGridView2.Rows[fila].Cells["@movc_memo"].Value = " ";

                        calcularPagar(cboModoPago.Text);
                        limpiarCancelarBasico();
                        return;
                    }
                    else
                    {
                        MessageBox.Show("El Monto Abonar puede quedar Vacio!!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtMontoAbonar.Focus();
                        return;
                    }

                }

                if ((txtBanco.Text == "") && (cboModoPago.Text.Equals("Tarjeta de Debito") || cboModoPago.Text.Equals("Tarjeta de Credito") || cboModoPago.Text.Equals("Cheque")))
                {
                    MessageBox.Show("Para el Modo de pago " + cboModoPago.Text + " el campo Banco no puede quedar Vacio!!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtBanco.Focus();
                    return;
                }

                if ((txtNumeroT.Text == "") && (cboModoPago.Text.Equals("Tarjeta de Debito") || cboModoPago.Text.Equals("Tarjeta de Credito") || cboModoPago.Text.Equals("Cheque")))
                {
                    MessageBox.Show("El Nº para el  Modo de pago " + cboModoPago.Text + " no puede quedar Vacio!!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtNumeroT.Focus();
                    return;
                }

                if (txtMontoAbonar.Text == "")
                {
                    MessageBox.Show("El Monto Abonar puede quedar Vacio!!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtMontoAbonar.Focus();
                    return;
                }
            }
        }

        public void activarBancos(bool valor)
        {
            txtBanco.Enabled = valor;
            cmdlbxBancos.Enabled = valor;
            txtNumeroT.Enabled = valor;
        }

        private void cboModoPago_SelectedIndexChanged(object sender, EventArgs e)
        {
            limpiarCancelarBasico2();
            if ((cboModoPago.Text.Equals("Efectivo")))
            {
                activarBancos(false);
            }
            else
            {
                activarBancos(true);
            }
        }

        public void actualizarCodBanco(string codBanco)
        {
            bancoFac.CodBanco = codBanco;
        }

        private void txtNumeroT_KeyPress(object sender, KeyPressEventArgs e)
        {
            funtxt.onlyNumberWithTab(sender, e);
        }

        public void limpiarCancelarBasico()
        {
            cboModoPago.SelectedIndex = 0;
            txtBanco.Clear();
            txtNumeroT.Clear();
            txtMontoAbonar.Clear();
            bancoFac.CodBanco = null;
        }

        public void limpiarCancelarBasico2()
        {
            try
            {
                txtBanco.Clear();
                txtNumeroT.Clear();
                txtMontoAbonar.Clear();
                bancoFac.CodBanco = null;
            }
            catch (NullReferenceException e)
            {
            }
        }

        private void cmdlbxBancos_Click(object sender, EventArgs e)
        {
            //lbxBanCancelar = lbxBancos.DefInstance;
            //lbxBanCancelar.actualizarRefFact(this, "preFC");
            //lbxBanCancelar.MdiParent = this.MdiParent;
            //lbxBanCancelar.Show();
            //frmPedido.SetParent(lbxBanCancelar.Handle, this.MdiParent.Handle);
        }

        [DllImport("user32.DLL")]
        public static extern void SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        private void txtBanco_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)(Keys.Enter)) && txtBanco.Text != "")
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void txtMontoAbonar_KeyPress(object sender, KeyPressEventArgs e)
        {
            funtxt.txtOnlyDecimal(sender, e, this.txtMontoAbonar);
        }

        private void calcularPagar(string metodoPago)
        {
            switch (metodoPago)
            {
                case ("Efectivo"):
                    txtEfectivo.Text = Convert.ToString(Convert.ToDecimal(txtEfectivo.Text.Replace(".", ",")) + (Convert.ToDecimal(txtMontoAbonar.Text.Replace(".", ","))));
                    break;
                case ("Tarjeta de Debito"):
                    txtMontoTarjetas.Text = Convert.ToString(Convert.ToDecimal(txtMontoTarjetas.Text.Replace(".", ",")) + (Convert.ToDecimal(txtMontoAbonar.Text.Replace(".", ","))));
                    break;
                case ("Tarjeta de Credito"):
                    txtMontoTarjetas.Text = Convert.ToString(Convert.ToDecimal(txtMontoTarjetas.Text.Replace(".", ",")) + (Convert.ToDecimal(txtMontoAbonar.Text.Replace(".", ","))));
                    break;
                case ("Cheque"):
                    txtMontoCheques.Text = Convert.ToString(Convert.ToDecimal(txtMontoCheques.Text.Replace(".", ",")) + (Convert.ToDecimal(txtMontoAbonar.Text.Replace(".", ","))));
                    break;
            };
            sumarCancelado();
            cambio();
        }


        private void calcularPagar()
        {
            foreach (DataGridViewRow fila in dataGridView2.Rows)
            {
                switch (fila.Cells["@mocv_forpag"].Value.ToString())
                {
                    case ("EFECTIVO"):
                        txtEfectivo.Text = Convert.ToString(Convert.ToDecimal(txtEfectivo.Text.Replace(".", ",")) + (Convert.ToDecimal(Convert.ToDecimal(fila.Cells["@movc_monto"].Value.ToString().Replace(".", ",")))));
                        break;
                    case ("TARJETA-D"):
                        txtMontoTarjetas.Text = Convert.ToString(Convert.ToDecimal(txtMontoTarjetas.Text.Replace(".", ",")) + (Convert.ToDecimal(Convert.ToDecimal(fila.Cells["@movc_monto"].Value.ToString().Replace(".", ",")))));
                        break;
                    case ("TARJETA-C"):
                        txtMontoTarjetas.Text = Convert.ToString(Convert.ToDecimal(txtMontoTarjetas.Text.Replace(".", ",")) + (Convert.ToDecimal(Convert.ToDecimal(fila.Cells["@movc_monto"].Value.ToString().Replace(".", ",")))));
                        break;
                    case ("CHEQUE"):
                        txtMontoCheques.Text = Convert.ToString(Convert.ToDecimal(txtMontoCheques.Text.Replace(".", ",")) + (Convert.ToDecimal(Convert.ToDecimal(fila.Cells["@movc_monto"].Value.ToString().Replace(".", ",")))));
                        break;
                }
                sumarCancelado();
            }
            cambio();
        }

        private void sumarCancelado()
        {
            txtPagado.Text = Convert.ToString(((Convert.ToDecimal(txtMontoCheques.Text.Replace(".", ","))) + (Convert.ToDecimal(txtMontoTarjetas.Text.Replace(".", ","))) + (Convert.ToDecimal(txtEfectivo.Text.Replace(".", ",")))));
        }

        private void cambio()
        {
            decimal temporal = 0;
            decimal montoPagar = 0;
            decimal montoPagado = 0;

            montoPagar = Convert.ToDecimal(txtContado.Text.Replace(".", ","));
            montoPagado = Convert.ToDecimal(txtPagado.Text.Replace(".", ","));

            temporal = funtxt.Truncate((montoPagar - montoPagado), 2);
            if (temporal > 0)
            {
                txtCambio.Text = "0.00";
            }
            else
            {
                txtCambio.Text = Convert.ToString((temporal * (-1))).Replace(",", ".");
            }
        }

        public void activarCancelarFactura(bool valor)
        {
            groupBox7.Enabled = false;
            dataGridView2.Enabled = valor;
            groupBox6.Enabled = valor;
            cmdConfirmar.Enabled = valor;
            cmdCancelarF.Enabled = valor;
            cmdRegresar.Enabled = valor;
        }

        public void activarItemsFactura(bool valor)
        {
            if (this.dataGridView1.Rows.Count > 0)
            {
                this.dataGridView1.ClearSelection();
            }
            groupBox2.Enabled = valor;
        }

        private void cmdConfirmarDocumento_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                if (vende1.Status.Equals("Activo"))
                {
                    activarCancelarFactura(true);
                    txtContado.Text = txtNeto.Text;
                    //datos que faltan en la factura
                    fac1.Divisa = cboTransporte.Text;
                    activarItemsFactura(false);
                    fac1.DireccionEnvio = txtDirEntrega.Text;
                    fac1.NumeroPedido = txtnumCont.Text;
                }
                else
                {
                    MessageBox.Show("Debe Seleccionar un vendedor Activo!!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtCodVendedor.Text = vende1.CodigoV;
                }
            }
            else
            {
                MessageBox.Show("El documento Debe Tener algun Articulo!!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void cboModoPago_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
                SendKeys.Send("{TAB}");
        }

        private void cmdEliminarPago_Click(object sender, EventArgs e)
        {
            if (dataGridView2.Rows.Count > 0)
            {
                restarMontos(dataGridView2.CurrentRow.Index);
                dataGridView2.Rows.RemoveAt(dataGridView2.CurrentRow.Index);
            }
        }

        private void restarMontos(int fila)
        {
            string tipoPago = null;
            tipoPago = dataGridView2.Rows[fila].Cells["@mocv_forpag"].Value.ToString();
            switch (tipoPago)
            {

                case ("EFECTIVO"):
                    txtEfectivo.Text = Convert.ToString(Convert.ToDecimal(txtEfectivo.Text.Replace(".", ",")) - (Convert.ToDecimal(dataGridView2.Rows[fila].Cells["@movc_monto"].Value.ToString().Replace(".", ","))));
                    break;

                case ("TARJETA-D"):
                    txtMontoTarjetas.Text = Convert.ToString(Convert.ToDecimal(txtMontoTarjetas.Text.Replace(".", ",")) - (Convert.ToDecimal(dataGridView2.Rows[fila].Cells["@movc_monto"].Value.ToString().Replace(".", ","))));
                    break;

                case ("TARJETA-C"):
                    txtMontoTarjetas.Text = Convert.ToString(Convert.ToDecimal(txtMontoTarjetas.Text.Replace(".", ",")) - (Convert.ToDecimal(dataGridView2.Rows[fila].Cells["@movc_monto"].Value.ToString().Replace(".", ","))));
                    break;

                case ("CHEQUE"):
                    txtMontoCheques.Text = Convert.ToString(Convert.ToDecimal(txtMontoCheques.Text.Replace(".", ",")) - (Convert.ToDecimal(dataGridView2.Rows[fila].Cells["@movc_monto"].Value.ToString().Replace(".", ","))));
                    break;
            };
            sumarCancelado();
            cambio();
        }

        private void agregarCambio()
        {
            dataGridView2.Rows.Add();
            int fila = dataGridView2.Rows.Count - 1;
            dataGridView2.Rows[fila].Cells["codTipo"].Value = "N/A";
            dataGridView2.Rows[fila].Cells["detalleTipo"].Value = "N/A";
            dataGridView2.Rows[fila].Cells["codNum"].Value = "N/A";
            dataGridView2.Rows[fila].Cells["@mocv_forpag"].Value = "CAMBIO";
            dataGridView2.Rows[fila].Cells["@movc_monto"].Value = txtCambio.Text;
            dataGridView2.Rows[fila].Cells["@movc_tipoctaban"].Value = "N/A";
            dataGridView2.Rows[fila].Cells["@movc_codmaestr"].Value = client1.Codigo;
            dataGridView2.Rows[fila].Cells["@movc_numdoc"].Value = fac1.CorrelativoInterno;
            dataGridView2.Rows[fila].Cells["@movc_descrioper"].Value = "Mov Caja en Ventas";
            dataGridView2.Rows[fila].Cells["@movc_tipomovc"].Value = "MOVCAJAV";
            dataGridView2.Rows[fila].Cells["@movc_divisa"].Value = cboTransporte.Text;
            dataGridView2.Rows[fila].Cells["@movc_fchemision"].Value = "" + DateTime.Now.Day + "/" + DateTime.Now.Month + "" + "/" + DateTime.Now.Year;
            dataGridView2.Rows[fila].Cells["@movc_hora"].Value = DateTime.Now.ToString("hh:mm:ss tt");
            dataGridView2.Rows[fila].Cells["@movc_vendedor"].Value = client1.Vendedor;
            dataGridView2.Rows[fila].Cells["@movc_codcaja"].Value = "0002";
            dataGridView2.Rows[fila].Cells["@movc_estatus"].Value = "Activo";

            dataGridView2.Rows[fila].Cells["@movc_operacion"].Value = "D";
            dataGridView2.Rows[fila].Cells["@movc_codtipopag"].Value = " ";
            dataGridView2.Rows[fila].Cells["@movc_cuentacheq"].Value = " ";
            dataGridView2.Rows[fila].Cells["@movc_numero"].Value = " ";

            if (Convert.ToDecimal(txtCambio.Text.Replace(".", ",")) > 0)
            {
                dataGridView2.Rows[fila].Cells["@movc_valcam"].Value = txtCambio.Text;
            }
            else
            {
                dataGridView2.Rows[fila].Cells["@movc_valcam"].Value = "0.00";
            }

            dataGridView2.Rows[fila].Cells["@movc_memo"].Value = " ";
        }

        private bool requiereConfirmacion()
        {
            bool centinela = false;
            if (!((Convert.ToDecimal(txtPagado.Text.Replace(".", ","))) >= (Convert.ToDecimal(txtContado.Text.Replace(".", ",")))))
            {
                centinela = true;
            }
            return centinela;
        }

        private void guardarFactura()
        {
            int estadoGuardar = -9;

            if ((Convert.ToDecimal(txtContado.Text.Replace(".", ","))) <= (Convert.ToDecimal(txtPagado.Text.Replace(".", ","))))
            {
                fac1.Dcli_estado = "Pagado";
            }
            else
            {
                fac1.Dcli_estado = "Activo";
            }
            asignarCorrelativo(fac1.TipoDocumento);
            //agregado
            fac1.aumentarCorrelativo(fac1.TipoDocumento, fac1.CorrelativoInterno);
            actualizarCorrelativoGrid();

            fac1.crearSentenciaCabeceraped(client1, vende1, referenciaPrincipal.empresaActual.BaseDatosActual, referenciaPrincipal.usuarioActual.Id, referenciaPrincipal.usuarioActual.NumeroCaja, txtCambio.Text,
                txtPagado.Text, txtContado.Text);
            do
            {
                estadoGuardar = fac1.guardarCabecera();
                if ((estadoGuardar == 1062))
                {
                    fac1.aumentarCorrelativo(fac1.TipoDocumento, fac1.CorrelativoInterno);
                    fac1.obtenerCorrelativo(fac1.TipoDocumento);
                    asignarCorrelativo(fac1.TipoDocumento);
                    //agregado
                    fac1.aumentarCorrelativo(fac1.TipoDocumento, fac1.CorrelativoInterno);
                    fac1.crearSentenciaCabeceraped(client1, vende1, referenciaPrincipal.empresaActual.BaseDatosActual, referenciaPrincipal.usuarioActual.Id, referenciaPrincipal.usuarioActual.NumeroCaja, txtCambio.Text,
                        txtPagado.Text, txtContado.Text);
                    actualizarCorrelativoGrid();
                }

                if (estadoGuardar == -666)
                {
                    fac1.obtenerCorrelativo(fac1.TipoDocumento);
                    asignarCorrelativo(fac1.TipoDocumento);
                    //agregado
                    fac1.aumentarCorrelativo(fac1.TipoDocumento, fac1.CorrelativoInterno);
                    fac1.crearSentenciaCabeceraped(client1, vende1, referenciaPrincipal.empresaActual.BaseDatosActual, referenciaPrincipal.usuarioActual.Id, referenciaPrincipal.usuarioActual.NumeroCaja, txtCambio.Text,
                        txtPagado.Text, txtContado.Text);
                    actualizarCorrelativoGrid();
                }
            } while ((estadoGuardar == 1062) || (estadoGuardar == -666));

            ////agregado
            //fac1.aumentarCorrelativo(fac1.TipoDocumento, fac1.CorrelativoInterno);

            /*if (fac1.TipoDocumento.Equals("CTZ"))
            {*/
            fac1.guardarDetallePed(this.dataGridView1);
            /*}
            else if (fac1.TipoDocumento.Equals("DEV"))
            {
                productoFactura.actualizarItemDev(this.dataGridView1);
                //poner los exportados en admdocli
                string[,] valores = new string[dataGridView1.Rows.Count, 4];

                for (int x = 0; x < dataGridView1.Rows.Count; x++)
                {
                    valores[x, 0] = dataGridView1.Rows[x].Cells["@mov_cant"].Value.ToString();
                    valores[x, 1] = fac1.FacturaAfectada;
                    valores[x, 2] = dataGridView1.Rows[x].Cells["@mov_codigo"].Value.ToString();
                    valores[x, 3] = dataGridView1.Rows[x].Cells["@mov_item"].Value.ToString();
                }
                fac1.actulizarItemsDev(valores);
                fac1.guardarDetallePed(this.dataGridView1);
            }*/
            //guarda en caja
            /*if (fac1.TipoDocumento != "CTZ")
            {
                if (Convert.ToDecimal(txtCambio.Text) > 0)
                {
                    agregarCambio();
                }
                if ((this.dataGridView2.Rows.Count > 0) && ((this.centinelaConfirmacion) || !(this.centinelaConfirmacion)))
                {
                    cajaCobrar.insertMovCajaFacturacion(this.dataGridView2, fac1);
                }
            }*/
            MessageBox.Show("Documento Grabado Existosamente", "Exito ", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //limpiarCompleto();
            //activarCancelarFactura(false);
            //activarItemsFactura(true);
            //fac1.limpiarFactura();
        }

        public void continuarClave(string usuario)
        {
            this.Enabled = true;
            despuesConfirmacionConfirmarDoc(usuario);
        }

        public void despuesConfirmacionConfirmarDoc(string usuario)
        {

            if (this.centinelaConfirmacion)
            {
                if (fac1.TipoDocumento.Equals("FAV"))
                {
                    fac1.Dcli_aprob1 = usuario;
                    if (((client1.CuentaManor == null) || (client1.CuentaManor == "")) && ((client1.AuxiliarC == null) || (client1.AuxiliarC == "")))
                    {
                        if (fac1.Ctd_codcta.Substring(fac1.Ctd_codcta.Length - 1, 1).Equals("0"))
                        {
                            cuentaContable.ingresarMCuentaFAV("01", client1, fac1.Ctd_codcta,
                                                                                referenciaPrincipal.usuarioActual.Id);
                        }

                        if (fac1.Ctd_codcta.Substring(fac1.Ctd_codcta.Length - 1, 1).Equals("1"))
                        {
                            cuentaContable.ingresarACuentaFAV("01", client1, fac1.Ctd_codcta,
                                                                                referenciaPrincipal.usuarioActual.Id);
                        }
                    }
                }
                if (fac1.TipoDocumento.Equals("DEV"))
                {
                    fac1.Dcli_aprob2 = usuario;
                }
                guardarFactura();
                fac1.guardarSalcli(client1, vende1, referenciaPrincipal.empresaActual.BaseDatosActual, referenciaPrincipal.usuarioActual.Id, referenciaPrincipal.usuarioActual.NumeroCaja, this.txtPagado, this.txtContado);
                fac1.DgvItems = this.dataGridView1;
                fac1.ClienteFacturar = this.client1;
                fac1.VendedorFactura = this.vende1;
                frmReporteP = frmReportePedido.DefInstance;
                frmReporteP.armarReporte((fac1.rptPedidoM(this.client1, referenciaPrincipal.usuarioActual.Codigo, this.fac1, cboTransporte.Text,true)), referenciaPrincipal.empresaActual.Nombre, referenciaPrincipal.empresaActual.Rif,true);
                frmReporteP.MdiParent = this.MdiParent;
                frmReporteP.Show();

                /*frmReporte = frmContePedido.DefInstance;
                frmReporte.armarReporte(fac1.rtpPedido(cboCondicionPago.Text, txtplazoDias.Text,referenciaPrincipal.usuarioActual.NombreUsuario), referenciaPrincipal.empresaActual.Nombre, referenciaPrincipal.empresaActual.Rif);
                frmReporte.Show();*/

                limpiarCompleto();
                activarCancelarFactura(false);
                //asignarCorrelativo(fac1.TipoDocumento);
                //t2412 = fac1.CorrelativoInterno;
                fac1.limpiarFactura();
                //fac1.CorrelativoInterno = t2412;
                //txtCorrelativo.Text = t2412;
                activarItemsFactura(true);
            }
            else
            {
                MessageBox.Show("Necesita la Clave de Supervision para Guardar el Documento!!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void frmPreFac_FormClosing(object sender, FormClosingEventArgs e)
        {
           for (int x = 0; x < Application.OpenForms.Count; x++)
           {
                Form forX = Application.OpenForms[x];
                if (forX.GetType().ToString().Equals("lebrun.formularios.prefactura.lbxprefacturaS12"))
                {
                    referencia12.limpiarForm();
                }

                if (forX.GetType().ToString().Equals("lebrun.formularios.prefactura.lbxPrefactura"))
                {
                    referenciaLbxPrefactura.limpiarForm();
                }
           }
        }

        private void espaciosBlanco()
        {
            try
            {
                //client1.Nombre = Regex.Replace(client1.Nombre, @"[^\u0030-\u007F]", " ");
                client1.Nombre = Regex.Replace(client1.Nombre, @"[^\u0030-\u0039|u0041-\u005A|u0061-\u007A]", " ");
                client1.Nombre = client1.Nombre.Trim();
                client1.Rif = client1.Rif.Trim();
            }
            catch (Exception e)
            {
            }
        }

        private void caracteresProductos()
        {
            foreach (DataGridViewRow fila in dataGridView1.Rows)
            {
                //fila.Cells["@mov_codigo"].Value = Regex.Replace((fila.Cells["@mov_codigo"].Value.ToString()), @"[^\u0030-\u007F]", " ", RegexOptions.None);
                fila.Cells["@mov_codigo"].Value = Regex.Replace((fila.Cells["@mov_codigo"].Value.ToString()), @"[^\u0030-\u0039|u0041-\u005A|u0061-\u007A]", " ", RegexOptions.None);
                fila.Cells["@mov_codigo"].ToString().Trim();
                //fila.Cells["colProducto"].Value = Regex.Replace((fila.Cells["colProducto"].Value.ToString()), @"[^\u0030-\u007F]", " ", RegexOptions.None);
                fila.Cells["colProducto"].Value = Regex.Replace((fila.Cells["colProducto"].Value.ToString()), @"[^\u0030-\u0039|u0041-\u005A|u0061-\u007A]", " ", RegexOptions.None);
                fila.Cells["colProducto"].ToString().Trim();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            epsonLX300.cerrarDocumentoFiscal();
        }

        private void cmdConfirmarDocumento_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(this.cmdConfirmarDocumento, "Terminar la Carga de Articulos y Procesar el Pago");

        }
        private void cmdSalir_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(this.cmdSalir, "Cancela la carga de Articulos");

        }
        private void cmdBuscarCliente_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(this.cmdBuscarCliente, "Realiza busqueda de clientes");
        }
        private void cmdBuscarVendedor_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(this.cmdBuscarVendedor, "Realiza busqueda de vendedores");
        }
        private void cmdBuscarProducto_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(this.cmdBuscarProducto, "Realiza busqueda de productos");
        }
        private void cmdAgregarItem_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(this.cmdAgregarItem, "Agrega articulos al documento");
        }
        private void cmdEliminarItem_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(this.cmdEliminarItem, "Elimina articulos al documento");
        }
        private void cmdlbxBancos_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(this.cmdlbxBancos, "Realiza busqueda de bancos");
        }
        private void cmdAgregarPago_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(this.cmdAgregarPago, "Agrega un pago al documento");
        }
        private void cmdEliminarPago_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(this.cmdEliminarPago, "Elimina un pago al documento");
        }
        private void cmdConfirmar_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(this.cmdConfirmar, "Confirma el documento y genera Factura");
        }
        private void cmdCancelarF_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(this.cmdCancelarF, "Aborta el documento y cierra el programa");
        }

        private void chkCambioPrecio_CheckedChanged(object sender, EventArgs e)
        {
            if ((chkCambioPrecio.Checked) && (txtCodProducto.Text != ""))
            {
                claveSuper = frmClaveConfirmacion2.DefInstance;
                claveSuper.Show();
                claveSuper.cambiarReferencia(this, "1");
                this.Enabled = false;
                frmPrefactura.SetParent(claveSuper.Handle, this.MdiParent.Handle);
                claveSuper.txtClave.Focus();
            }
            else
            {
                if ((txtCodProducto.Text == "") && (chkCambioPrecio.Checked))
                {
                    MessageBox.Show("Necesita un articulo para cambiar de precio!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    chkCambioPrecio.Checked = false;
                    txtCodProducto.Focus();
                }
            }
        }

        public void despuesdeConfirmacionPrecio(string idUsuario)
        {
            this.Enabled = true;
            if (centinelaConfirmacion)
            {
                txtPrecio.ReadOnly = false;
                txtPrecio.Focus();
                temporalCambioPrecio = idUsuario;
            }
            chkCambioPrecio.Checked = false;
        }

        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtPrecio.ReadOnly == false)
            {
                funtxt.txtOnlyDecimal(sender, e, this.txtPrecio);
            }
        }

        #region implementacion Interfaz

        public frmPrefactura cargarModificar(string numeroInterno, string tipoDoc)
        {
            mostrarFactura(numeroInterno, tipoDoc);
            txtCorrelativo.Enabled = false;
            dateTimePicker1.Enabled = false;
            cboCondicionPago.Enabled = true;
            txtCodCliente.Enabled = false;
            txtRif.Enabled = false;
            txtNombreCli.Enabled = false;
            cmdBuscarCliente.Enabled = false;
            txtCodVendedor.Enabled = true;
            cmdBuscarVendedor.Enabled = true;
            txtNombreVendedor.Enabled = true;
            return this;
        }

        public frmPrefactura soloVista(string cod)
        {
            cmdConfirmarDocumento.Enabled = false;
            groupBox1.Enabled = false;
            dateTimePicker1.Enabled = false;
            groupBox5.Enabled = false;
            groupBox7.Enabled = false;
            cmdConfirmar.Enabled = false;
            cmdCancelarF.Enabled = false;
            mostrarFactura(cod);
            return this;
        }

        public frmPrefactura soloVista2(string cod, string tipoDoc, string numeroInterno)
        {
            cmdConfirmarDocumento.Enabled = false;
            groupBox1.Enabled = false;
            dateTimePicker1.Enabled = false;
            groupBox5.Enabled = false;
            groupBox7.Enabled = false;
            cmdConfirmar.Enabled = false;
            cmdCancelarF.Enabled = false;
            mostrarFactura(cod);
            this.fac1.CorrelativoInterno = numeroInterno;
            this.fac1.TipoDocumento = tipoDoc;
            return this;
        }

        public void cargadoSoloVista() { }
        public string respuestaImpresora()
        {
            return epsonLX300.estadoImpresora();
        }

        public bool reimprimirFAV(bool centinela)
        {
            /*frmReporte = frmContePedido.DefInstance;
            frmReporte.armarReporte(fac1.rtpPedido(cboCondicionPago.Text, txtplazoDias.Text,referenciaPrincipal.usuarioActual.NombreUsuario), referenciaPrincipal.empresaActual.Nombre, referenciaPrincipal.empresaActual.Rif);
            frmReporte.Show();*/
            frmReporteP = frmReportePedido.DefInstance;
            frmReporteP.armarReporte((fac1.rptPedidoM(this.client1, referenciaPrincipal.usuarioActual.Codigo, this.fac1, cboTransporte.Text,true)), referenciaPrincipal.empresaActual.Nombre, referenciaPrincipal.empresaActual.Rif,true);
            //frmReporteP.MdiParent = this.MdiParent;
            frmReporteP.Show();
            return true;
        }

        public bool reimprimirDEV(string numAfec)
        {
            bool centinela = false;
            espaciosBlanco();
            caracteresProductos();
            fac1.obtenerDatosFacturaAfectada(numAfec);
            epsonLX300.imprimirDEV(fac1, txtPagado);
            if (this.fac1.NumeroFiscal != null)
            {
                centinela = true;
            }
            return centinela;
        }

        public bool realizarZ()
        {
            return epsonLX300.reporteZExterno();
        }

        #endregion

        private void mostrarFactura(string numeroInterno, string tipoDoc)
        {
            string codigoConPagTemp = null;
            //cargo Factura, Cliente, vendedor
            fac1.cargarPrefactura(numeroInterno, this.client1, this.vende1);
            codigoConPagTemp = fac1.ClienteFacturar.CondicionPago;
            client1.cargarDatosCliente();
            client1.CondicionPago = codigoConPagTemp;
            vende1.cargarDatosVendedor();

            //asigno en el formulario
            fac1.ClienteFacturar = client1;
            fac1.VendedorFactura = vende1;
            txtCorrelativo.Text = fac1.CorrelativoInterno;
            txtCodCliente.Text = client1.Codigo;
            txtNombreCli.Text = client1.Nombre;
            txtRif.Text = client1.Rif;
            txtCodVendedor.Text = vende1.CodigoV;
            txtNombreVendedor.Text = vende1.Nombre;
            txtnumCont.Text = fac1.NumeroPedido;
            txtDirEntrega.Text = fac1.DireccionEnvio;
            this.dateTimePicker1.Value = fac1.FechaFactura;

            //items factura
            DataTable itemsCargados = fac1.itemsPre();
            int indice = 0;

            foreach (DataRow filac in itemsCargados.Rows)
            {
                dataGridView1.Rows.Add();
                indice = dataGridView1.Rows.Count - 1;
                dataGridView1.Rows[indice].Cells["@mov_docaso"].Value = filac["@mov_docaso"].ToString();
                dataGridView1.Rows[indice].Cells["@mov_tipoaso"].Value = filac["@mov_tipoaso"].ToString();
                dataGridView1.Rows[indice].Cells["@mov_cencos"].Value = filac["@mov_cencos"].ToString();
                dataGridView1.Rows[indice].Cells["@mov_codalm"].Value = filac["@mov_codalm"].ToString();
                dataGridView1.Rows[indice].Cells["@mov_cdcomp"].Value = filac["@mov_cdcomp"].ToString();
                dataGridView1.Rows[indice].Cells["@mov_codcta"].Value = filac["@mov_codcta"].ToString();
                dataGridView1.Rows[indice].Cells["@mov_codsuc"].Value = filac["@mov_codsuc"].ToString();
                dataGridView1.Rows[indice].Cells["@mov_codtra"].Value = filac["@mov_codtra"].ToString();
                dataGridView1.Rows[indice].Cells["@mov_vendedor"].Value = filac["@mov_vendedor"].ToString();
                dataGridView1.Rows[indice].Cells["@mov_docume"].Value = filac["@mov_docume"].ToString();
                dataGridView1.Rows[indice].Cells["@mov_hora"].Value = filac["@mov_hora"].ToString();
                dataGridView1.Rows[indice].Cells["@mov_item"].Value = filac["@mov_item"].ToString();
                dataGridView1.Rows[indice].Cells["@mov_itemcomp"].Value = filac["@mov_itemcomp"].ToString();
                dataGridView1.Rows[indice].Cells["@mov_lista"].Value = filac["@mov_lista"].ToString();
                dataGridView1.Rows[indice].Cells["@mov_lote"].Value = filac["@mov_lote"].ToString();
                dataGridView1.Rows[indice].Cells["@mov_tipdoc"].Value = filac["@mov_tipdoc"].ToString();
                dataGridView1.Rows[indice].Cells["@mov_ivatip"].Value = filac["@mov_ivatip"].ToString();
                dataGridView1.Rows[indice].Cells["@mov_tipo"].Value = filac["@mov_tipo"].ToString();
                dataGridView1.Rows[indice].Cells["colMed"].Value = filac["@mov_undmed"].ToString();
                dataGridView1.Rows[indice].Cells["@mov_undmed"].Value = filac["@mov_undmed"].ToString();
                dataGridView1.Rows[indice].Cells["@mov_usuario"].Value = filac["@mov_usuario"].ToString();
                dataGridView1.Rows[indice].Cells["@mov_fechven"].Value = "" + (Convert.ToDateTime(filac["@mov_fechven"].ToString())).Year + "-" + (Convert.ToDateTime(filac["@mov_fechven"].ToString())).Month.ToString().PadLeft(2, '0') + "-" + (Convert.ToDateTime(filac["@mov_fechven"].ToString())).Day.ToString().PadLeft(2, '0');
                dataGridView1.Rows[indice].Cells["@mov_fecha"].Value = "" + (Convert.ToDateTime(filac["@mov_fecha"].ToString())).Year + "-" + (Convert.ToDateTime(filac["@mov_fecha"].ToString())).Month.ToString().PadLeft(2, '0') + "-" + (Convert.ToDateTime(filac["@mov_fecha"].ToString())).Day.ToString().PadLeft(2, '0');
                dataGridView1.Rows[indice].Cells["@mov_bandas"].Value = filac["@mov_bandas"].ToString().Replace(',', '.');
                dataGridView1.Rows[indice].Cells["@mov_contab"].Value = filac["@mov_contab"].ToString();
                dataGridView1.Rows[indice].Cells["@mov_cxund"].Value = filac["@mov_cxund"].ToString().Replace(',', '.');
                dataGridView1.Rows[indice].Cells["@mov_expendio"].Value = filac["@mov_expendio"].ToString().Replace(',', '.');
                dataGridView1.Rows[indice].Cells["@mov_export"].Value = filac["@mov_export"].ToString().Replace(',', '.');
                dataGridView1.Rows[indice].Cells["@mov_fisico"].Value = filac["@mov_fisico"].ToString();
                dataGridView1.Rows[indice].Cells["@mov_import"].Value = filac["@mov_import"].ToString().Replace(',', '.');
                dataGridView1.Rows[indice].Cells["@mov_otimp"].Value = filac["@mov_otimp"].ToString().Replace(',', '.');
                dataGridView1.Rows[indice].Cells["@mov_impprodu"].Value = filac["@mov_impprodu"].ToString().Replace(',', '.');
                dataGridView1.Rows[indice].Cells["@mov_invact"].Value = filac["@mov_invact"].ToString();
                dataGridView1.Rows[indice].Cells["@mov_iva"].Value = filac["@mov_iva"].ToString().Replace(',', '.');
                dataGridView1.Rows[indice].Cells["@mov_logico"].Value = filac["@mov_logico"].ToString();
                dataGridView1.Rows[indice].Cells["@mov_mtocom"].Value = filac["@mov_mtocom"].ToString().Replace(',', '.');
                dataGridView1.Rows[indice].Cells["@mov_memo"].Value = filac["@mov_memo"].ToString();
                dataGridView1.Rows[indice].Cells["@mov_talla"].Value = filac["@mov_talla"].ToString();
                dataGridView1.Rows[indice].Cells["@mov_color"].Value = filac["@mov_color"].ToString();
                dataGridView1.Rows[indice].Cells["@mov_arancel"].Value = filac["@mov_arancel"].ToString().Replace(',', '.');
                dataGridView1.Rows[indice].Cells["@mov_kilos"].Value = filac["@mov_kilos"].ToString().Replace(',', '.');
                dataGridView1.Rows[indice].Cells["@mov_impuesto"].Value = filac["@mov_impuesto"].ToString().Replace(',', '.');
                dataGridView1.Rows[indice].Cells["@mov_cosmon"].Value = filac["@mov_cosmon"].ToString().Replace(',', '.');
                dataGridView1.Rows[indice].Cells["@mov_totalmon"].Value = filac["@mov_totalmon"].ToString().Replace(',', '.');
                dataGridView1.Rows[indice].Cells["@mov_precio_ini"].Value = filac["@mov_precio_ini"].ToString().Replace(',', '.');
                dataGridView1.Rows[indice].Cells["@mov_porciva"].Value = filac["@mov_porciva"].ToString().Replace(',', '.');
                dataGridView1.Rows[indice].Cells["@mov_codigo"].Value = filac["@mov_codigo"].ToString();
                dataGridView1.Rows[indice].Cells["@mov_precio"].Value = filac["@mov_precio"].ToString().Replace(",", ".");
                dataGridView1.Rows[indice].Cells["colProducto"].Value = filac["colProducto"].ToString();
                dataGridView1.Rows[indice].Cells["@mov_total"].Value = filac["@mov_total"].ToString().Replace(",", ".");
                dataGridView1.Rows[indice].Cells["@mov_cant"].Value = filac["@mov_cant"].ToString();
                dataGridView1.Rows[indice].Cells["@mov_desc"].Value = filac["@mov_desc"].ToString().Replace(',', '.');
                dataGridView1.Rows[indice].Cells["@mov_costo"].Value = filac["@mov_costo"].ToString().Replace(',', '.');
                if ((filac["procedencia"].ToString() == "") || (filac["procedencia"].ToString() == null))
                {
                    dataGridView1.Rows[indice].Cells["procedencia"].Value = "Nacional";
                }
                else
                {
                    if ((Convert.ToDecimal(filac["procedencia"].ToString())) < 8000000)
                    {
                        dataGridView1.Rows[indice].Cells["procedencia"].Value = "Importado";
                    }
                    else
                    {
                        dataGridView1.Rows[indice].Cells["procedencia"].Value = "Nacional";
                    }
                }

            }
            //this.dataGridView1.DataSource = fac1.itemsPre();
            fac1.DgvItems = dataGridView1;

            foreach (DataGridViewRow fila in dataGridView1.Rows)
            {
                txtSubTotal.Text = Convert.ToString(Convert.ToDecimal(txtSubTotal.Text) + Convert.ToDecimal(fila.Cells["@mov_total"].Value.ToString().Replace(".", ",")));
                txtBase.Text = Convert.ToString(Convert.ToDecimal(txtSubTotal.Text) - Convert.ToDecimal(txtDesItems.Text));
                txtTotalProduc.Text = Convert.ToString(Convert.ToDecimal(txtTotalProduc.Text) + Convert.ToDecimal(fila.Cells["@mov_cant"].Value.ToString()));
                txtNeto.Text = Convert.ToString((Convert.ToDecimal(txtSubTotal.Text) - Convert.ToDecimal(txtDesItems.Text)) + (fac1.IvaGN + fac1.IvaRD));
            }
            txtIva.Text = Convert.ToString(fac1.IvaGN + fac1.IvaRD);
            fac1.TotalNeto = fac1.TotalBase + (fac1.IvaGN + fac1.IvaRD);
            txtDiaspp1.Text = string.IsNullOrEmpty(fac1.diasPP1) ? string.Empty : fac1.diasPP1;
            txtPorcenPP1.Text = string.IsNullOrEmpty(fac1.porcentajePP1) ? string.Empty : fac1.porcentajePP1;
            txtDiaspp2.Text = string.IsNullOrEmpty(fac1.diasPP2) ? string.Empty : fac1.diasPP2;
            txtPorcenPP2.Text = string.IsNullOrEmpty(fac1.porcentajePP2) ? string.Empty : fac1.porcentajePP2;
            return;
        }

        private void mostrarFactura(string cod)
        {
            string codigoConPagTemp = null;
            //cabecera de Factura
            fac1.CorrelativoInterno = cod;
            txtCorrelativo.Text = fac1.CorrelativoInterno;
            fac1.cargarPrefactura(cod, this.client1, this.vende1);
            codigoConPagTemp =  fac1.ClienteFacturar.CondicionPago;
            txtCodCliente.Text = client1.Codigo;
            client1.cargarDatosCliente();
            client1.CondicionPago = codigoConPagTemp;
            txtNombreCli.Text = client1.Nombre;
            txtRif.Text = client1.Rif;
            txtnumCont.Text = fac1.NumeroPedido;
            txtDirEntrega.Text = fac1.DireccionEnvio;
            vende1.cargarDatosVendedor();
            txtCodVendedor.Text = vende1.CodigoV;
            fac1.ClienteFacturar = client1;
            fac1.VendedorFactura = vende1;
            this.dateTimePicker1.Value = fac1.FechaFactura;

            txtNombreVendedor.Text = vende1.Nombre;

            //items factura
            this.dataGridView1.DataSource = fac1.itemsPre();
            this.dataGridView1.Columns["procedencia"].Visible = false;
            this.dataGridView1.Enabled = false;
            fac1.DgvItems = dataGridView1;
            //detalles del pago
            /*dataGridView2.DataSource = cajaCobrar.dtCancelado(cod);
            txtContado.Text = Convert.ToString(fac1.TotalNeto).Replace(",", ".");*/
            calcularPagar();

            //Resumen del pago
            foreach (DataGridViewRow fila in dataGridView1.Rows)
            {
                txtSubTotal.Text = Convert.ToString(Convert.ToDecimal(txtSubTotal.Text) + Convert.ToDecimal(fila.Cells["@mov_total"].Value.ToString().Replace(".", ",")));
                txtBase.Text = Convert.ToString(Convert.ToDecimal(txtSubTotal.Text) - Convert.ToDecimal(txtDesItems.Text));
                txtTotalProduc.Text = Convert.ToString(Convert.ToDecimal(txtTotalProduc.Text) + Convert.ToDecimal(fila.Cells["@mov_cant"].Value.ToString()));
                txtNeto.Text = Convert.ToString((Convert.ToDecimal(txtSubTotal.Text) - Convert.ToDecimal(txtDesItems.Text)) + (fac1.IvaGN + fac1.IvaRD));
            }
            txtIva.Text = Convert.ToString(fac1.IvaGN + fac1.IvaRD);
            fac1.TotalNeto = fac1.TotalBase + (fac1.IvaGN + fac1.IvaRD);

            if ((String.IsNullOrEmpty(client1.Transporte)))
            {
                cboTransporte.SelectedIndex = 0;
            }
            else
            {
                //cboTransporte.DisplayMember = "trans_des";
                //cboTransporte.ValueMember = "trans_cod";
                DataTable dt = (DataTable)cboTransporte.DataSource;
                int indice = dt.AsEnumerable().ToList().FindIndex(x => x.Field<string>("trans_cod").Equals(client1.Transporte));
                if (indice > -1)
                {
                    cboTransporte.SelectedItem = indice;
                }
                else
                {
                    cboTransporte.SelectedItem = 0;
                }

                //for (int i = 0; i < cboTransporte.Items.Count; i++)
                //{
                   
                //    cboTransporte.SelectedItem = i;
                    
                //    if (cboTransporte.SelectedValue.ToString().Equals(client1.Transporte))
                //    {
                //        cboTransporte.SelectedIndex = i;
                //        break;
                //    }
                //}
            }

            return;
        }

        private void cmdRegresar_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Desea Cerrar la Prefactura Definitivamente?", "Pregunta", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                actualizarCliVen();
                //Preguntarle a la Sra Haydee
                fac1.Dcli_estado = "3";
                fac1.ActualizarSentenciaCabecerpre(client1, vende1, referenciaPrincipal.empresaActual.BaseDatosActual, referenciaPrincipal.usuarioActual.Id, referenciaPrincipal.usuarioActual.NumeroCaja, txtCambio.Text,
                    txtPagado.Text, txtContado.Text);
                int valor = fac1.actualizarCabeceraPed();
                if (valor == -1)
                {
                    fac1.borrarDetallePre(this.dataGridView1);
                    fac1.guardarDetallePre(dataGridView1);
                }
                fac1.guardarProntoPagos(fac1);
                string numeroTemp = guardarFacturaDoccli();
                fac1.guardarSalcli(client1,vende1,referenciaPrincipal.empresaActual.BaseDatosActual,referenciaPrincipal.usuarioActual.Id,
                    referenciaPrincipal.usuarioActual.NumeroCaja,this.txtPagado, this.txtContado);
                MessageBox.Show("Prefactura Cerrada Existosamente", "Exito ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MessageBox.Show("Factura número="+numeroTemp+"", "Exito ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                factFerle = new FacturaFerle();
                factFerle.imprimirFav(fac1);
                fac1.dcli_impreso(fac1,"1");
                //this.Dispose();
                this.Close();
            }
        }

        public void cantidadItemDev()
        {
            fac1.TotalItems = Convert.ToInt32(txtTotalProduc.Text);
        }

        public void dev_valVendedor()
        {
            if (!(vende1.Status.Equals("Activo")))
            {
                MessageBox.Show("Debe Seleccionar un vendedor Activo!!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCodVendedor.Text = vende1.CodigoV;
            }
        }

        private void txtnumCont_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)(Keys.Enter)))
            {
                if ((txtnumCont.Text != null) && (txtnumCont.Text != ""))
                {
                    fac1.NumeroPedido = txtnumCont.Text;
                }
                if (txtCodCliente.Enabled)
                {
                    txtCodCliente.Focus();
                }
                else
                {
                    SendKeys.Send("{TAB}");
                }

            }
        }

        private void txtDirEntrega_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)(Keys.Enter)))
            {
                if ((txtDirEntrega.Text != null) && (txtDirEntrega.Text != ""))
                {
                    fac1.DireccionEnvio = txtDirEntrega.Text;
                }
                txtCodProducto.Focus();
            }
        }

        public void comboTransporte()
        {
            cboTransporte.DisplayMember = "trans_des";
            cboTransporte.ValueMember = "trans_cod";
            cboTransporte.DataSource = transporte1.lbxTransp();

        }

        private void cboTransporte_SelectedIndexChanged(object sender, EventArgs e)
        {
            client1.Transporte = cboTransporte.SelectedValue.ToString();
        }

        private void guardarPrefactura(string status)
        {
            int estadoGuardar = -9;
            asignarCorrelativo("PRE");
            fac1.aumentarCorrelativo(fac1.TipoDocumento, fac1.CorrelativoInterno);
            actualizarCorrelativoGrid();
            fac1.crearSentenciaCabecerapre(client1, vende1, referenciaPrincipal.empresaActual.BaseDatosActual, referenciaPrincipal.usuarioActual.Id, referenciaPrincipal.usuarioActual.NumeroCaja, txtCambio.Text,
                txtPagado.Text, txtContado.Text, status);
            do
            {
                estadoGuardar = fac1.guardarCabecera();
                if ((estadoGuardar == 1062))
                {
                    fac1.aumentarCorrelativo(fac1.TipoDocumento, fac1.CorrelativoInterno);
                    fac1.obtenerCorrelativo(fac1.TipoDocumento);
                    asignarCorrelativo(fac1.TipoDocumento);
                    //agregado
                    fac1.aumentarCorrelativo(fac1.TipoDocumento, fac1.CorrelativoInterno);
                    fac1.crearSentenciaCabecerapre(client1, vende1, referenciaPrincipal.empresaActual.BaseDatosActual, referenciaPrincipal.usuarioActual.Id, referenciaPrincipal.usuarioActual.NumeroCaja, txtCambio.Text,
                        txtPagado.Text, txtContado.Text, status);
                    actualizarCorrelativoGrid();
                }
                if (estadoGuardar == -666)
                {
                    fac1.obtenerCorrelativo(fac1.TipoDocumento);
                    asignarCorrelativo(fac1.TipoDocumento);
                    //agregado
                    fac1.aumentarCorrelativo(fac1.TipoDocumento, fac1.CorrelativoInterno);
                    fac1.crearSentenciaCabecerapre(client1, vende1, referenciaPrincipal.empresaActual.BaseDatosActual, referenciaPrincipal.usuarioActual.Id, referenciaPrincipal.usuarioActual.NumeroCaja, txtCambio.Text,
                        txtPagado.Text, txtContado.Text, status);
                    actualizarCorrelativoGrid();
                }
            } while ((estadoGuardar == 1062) || (estadoGuardar == -666));
            fac1.guardarDetallePre(this.dataGridView1);
            MessageBox.Show("Prefactura Grabada Existosamente", "Exito ", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public Factura soloDocumento(string cod)
        {  
            mostrarFactura(cod);
            fac1.ClienteFacturar.Transporte = cboTransporte.Text;
            return fac1;
        }

        public void comboCondigPag()
        {
            cboCondicionPago.DisplayMember = "conp_descripcion";
            cboCondicionPago.ValueMember = "conp_codigo";
            cboCondicionPago.DataSource = condPag.lbxCondPag();
            cboCondicionPago.SelectedIndex = 6;
        }

        private void cmdModificar_Click(object sender, EventArgs e)
        {
            
            if (dataGridView1.Rows.Count > 0)
            {
                productoFactura.limpiarProducto();

                productoFactura.Descripcion = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["colProducto"].Value.ToString();
                productoFactura.Iva = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["@mov_ivatip"].Value.ToString();
                productoFactura.Procedencia = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["procedencia"].Value.ToString();
                productoFactura.CodigoProd = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["@mov_codigo"].Value.ToString();
                productoFactura.CostoPromedio = Convert.ToDecimal(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["@mov_costo"].Value.ToString().Replace(".",","));
                productoFactura.ValorUnidadPrincipal = Convert.ToInt32(Convert.ToDecimal(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["@mov_cxund"].Value.ToString().Replace(".",",")));
                productoFactura.Precio = Convert.ToDecimal(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["@mov_cosmon"].Value.ToString().Replace(".",","));
                productoFactura.ValorIva = Convert.ToDecimal(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["@mov_porciva"].Value.ToString().Replace(".",","));


                //dataGridView1.Rows[fila].Cells["@mov_porciva"].Value = productoFactura.ValorIva;
                //dataGridView1.Rows[fila].Cells["@mov_precio"].Value = txtPrecio.Text.Replace(",", ".");

                txtCodProducto.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["@mov_codigo"].Value.ToString();
                txtNombreProducto.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["colProducto"].Value.ToString();
                cboUnidad.Items.Clear();
                cboUnidad.Items.Add(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["@mov_undmed"].Value.ToString());
                cboUnidad.SelectedIndex = 0;
                txtCantidad.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["@mov_cant"].Value.ToString();
                txtPrecio.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["@mov_precio"].Value.ToString();
                txtCantidad.Focus();
                modificarArticulo = true;
                indiceFilaModificar = dataGridView1.CurrentRow.Index;

                txtCodProducto.Enabled = false;
                cmdBuscarProducto.Enabled = false;
                txtNombreProducto.Enabled = false;
                cboUnidad.Enabled = false;
            }
            else
            {
                MessageBox.Show("No existen articulos para modificar!!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void modificarFila(int indiceFila)
        {
            decimal temp;
            decimal temp2;
            if (Convert.ToInt32(txtCantidad.Text)>0)
            {
                /* se calcula el precio de lo que va a restar*/
                temp2 = funtxt.Truncate((Convert.ToDecimal(dataGridView1.Rows[indiceFila].Cells["@mov_precio"].Value.ToString().Replace(".", ",")) *
                     Convert.ToDecimal(dataGridView1.Rows[indiceFila].Cells["@mov_cant"].Value.ToString())), 2);

                /* para calculos de factura se restan las bases y los ivas*/
                fac1.restarBases(temp2, dataGridView1.Rows[indiceFila].Cells["@mov_ivatip"].Value.ToString());

                fac1.restarCosto((Convert.ToDecimal((dataGridView1.Rows[indiceFila].Cells["@mov_costo"].Value.ToString()))),
                    (Convert.ToInt32((dataGridView1.Rows[indiceFila].Cells["@mov_cant"].Value.ToString()))),
                    (dataGridView1.Rows[indiceFila].Cells["procedencia"].Value.ToString()));

                fac1.restarBases2((Convert.ToInt32((dataGridView1.Rows[indiceFila].Cells["@mov_cant"].Value.ToString()))),
                    (Convert.ToDecimal(dataGridView1.Rows[indiceFila].Cells["@mov_precio"].Value.ToString().Replace(".", ","))),
                    (dataGridView1.Rows[indiceFila].Cells["procedencia"].Value.ToString()));

                dataGridView1.Rows[indiceFila].Cells["@mov_cant"].Value = (Convert.ToInt32(txtCantidad.Text));
                dataGridView1.Rows[indiceFila].Cells["@mov_otimp"].Value = (Convert.ToInt32(dataGridView1.Rows[indiceFila].Cells["@mov_cant"].Value));

                dataGridView1.Rows[indiceFila].Cells["@mov_precio"].Value = txtPrecio.Text.Replace(",", ".");
                
                temp = Convert.ToDecimal(dataGridView1.Rows[indiceFila].Cells["@mov_precio"].Value.ToString().Replace(".", ",")) *
                    Convert.ToDecimal(dataGridView1.Rows[indiceFila].Cells["@mov_cant"].Value.ToString());

                dataGridView1.Rows[indiceFila].Cells["@mov_total"].Value = temp.ToString("0.##").Replace(",", ".");
                dataGridView1.Rows[indiceFila].Cells["@mov_iva"].Value = funtxt.Truncate(((Convert.ToDecimal(dataGridView1.Rows[indiceFila].Cells["@mov_total"].Value.ToString().Replace(".", ",")) * productoFactura.ValorIva) / 100), 6);

                fac1.acumularBases(temp, productoFactura.Iva);

                //modificado para redondear segun 3 decimal 08:56 a.m. 16/07/2013
                switch ((dataGridView1.Rows[indiceFila].Cells["@mov_ivatip"].Value.ToString()))
                {
                    case "GN":
                        fac1.acumularIvas(fac1.BaseGN, "GN");
                        break;
                    case "RD":
                        fac1.acumularIvas(fac1.BaseRD, "RD");
                        break;
                };


                fac1.acumularCostos((Convert.ToDecimal((dataGridView1.Rows[indiceFila].Cells["@mov_costo"].Value.ToString()))),
                    (Convert.ToInt32((dataGridView1.Rows[indiceFila].Cells["@mov_cant"].Value.ToString()))),
                    (dataGridView1.Rows[indiceFila].Cells["procedencia"].Value.ToString()));
                fac1.acumularBases2((Convert.ToInt32(dataGridView1.Rows[indiceFila].Cells["@mov_cant"].Value)),
                Convert.ToDecimal(dataGridView1.Rows[indiceFila].Cells["@mov_precio"].Value.ToString().Replace(".", ",")), (dataGridView1.Rows[indiceFila].Cells["procedencia"].Value.ToString()));

                limpiarTotales();

                for (int z = 0; z < dataGridView1.Rows.Count; z++)
                {
                    actualizarTextPagos(z);
                }
                recalcularItems();
                //agregado por boton regresar
                if (dataGridView2.Rows.Count > 0)
                {
                    txtContado.Text = txtNeto.Text;
                    cambio();
                }
            }
            else
            {
                MessageBox.Show("La cantidad debe ser mayor que cero", "Información", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void cargarComboTransporte(Clientes cli)
        {
            if ((String.IsNullOrEmpty(cli.Transporte)))
            {
                cboTransporte.SelectedIndex = 0;
            }
            else
            {
                //cboTransporte.DisplayMember = "trans_des";
                //cboTransporte.ValueMember = "trans_cod";
                DataTable dt = (DataTable)cboTransporte.DataSource;
                int indice = dt.AsEnumerable().ToList().FindIndex(x => x.Field<string>("trans_cod").Equals(cli.Transporte));
                if (indice > -1)
                {
                    cboTransporte.SelectedIndex = indice;
                }
                else
                {
                    cboTransporte.SelectedIndex = 0;
                }

                //for (int i = 0; i < cboTransporte.Items.Count; i++)
                //{

                //    cboTransporte.SelectedItem = i;

                //    if (cboTransporte.SelectedValue.ToString().Equals(client1.Transporte))
                //    {
                //        cboTransporte.SelectedIndex = i;
                //        break;
                //    }
                //}
            }
        }

        private void cboCondicionPago_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)cboCondicionPago.DataSource;
            txtplazoDias.Text = dt.AsEnumerable().ToList().Find(x => x.Field<string>("conp_codigo").Equals(cboCondicionPago.SelectedValue.ToString())).Field<decimal>("conp_cant_dias").ToString();
            client1.CodigoCondicionPago = cboCondicionPago.SelectedValue.ToString();
        }


        public decimal precioDesCliente(decimal monto)
        {   
            decimal precioCliente = 0;
            if (!(string.IsNullOrEmpty(txtDescuentoGeneral.Text)) && (txtDescuentoGeneral.Text != " "))
            {
                if (Convert.ToDecimal(txtDescuentoGeneral.Text) > 0)
                {

                    precioCliente = monto - funtxt.Truncate(((monto * Convert.ToDecimal(txtDescuentoGeneral.Text)) / 100), 2);
                }
                else
                {
                    precioCliente = monto;
                }
            }

            return precioCliente;
        }

        public void actualizarCliVen()
        {
            foreach (DataGridViewRow fila in dataGridView1.Rows)
            {
                fila.Cells["@mov_vendedor"].Value = vende1.CodigoV;
                fila.Cells["@mov_codcta"].Value = client1.Codigo;
            }
        }

        private string guardarFacturaDoccli()
        {
            string correlativoNuevo= null;
            int estadoGuardar = -9;
            Factura factDoccli = fac1;
            factDoccli.TipoDocumento = "FAV";
            factDoccli.Dcli_estado = "Activo";
            asignarCorrelativo(factDoccli.TipoDocumento);
            //agregado
            factDoccli.aumentarCorrelativo(factDoccli.TipoDocumento, factDoccli.CorrelativoInterno);
            actualizarCorrelativoGrid();

            factDoccli.crearSentenciaCabecera(client1, vende1, referenciaPrincipal.empresaActual.BaseDatosActual, referenciaPrincipal.usuarioActual.Id, referenciaPrincipal.usuarioActual.NumeroCaja, txtCambio.Text,
                txtPagado.Text, txtContado.Text);
            do
            {
                estadoGuardar = factDoccli.guardarCabecera();
                if ((estadoGuardar == 1062))
                {
                    //factDoccli.aumentarCorrelativo(factDoccli.TipoDocumento, factDoccli.CorrelativoInterno);
                    factDoccli.obtenerCorrelativo(factDoccli.TipoDocumento);
                    asignarCorrelativo(factDoccli.TipoDocumento);
                    //agregado
                    factDoccli.aumentarCorrelativo(factDoccli.TipoDocumento, factDoccli.CorrelativoInterno);
                    factDoccli.crearSentenciaCabecera(client1, vende1, referenciaPrincipal.empresaActual.BaseDatosActual, referenciaPrincipal.usuarioActual.Id, referenciaPrincipal.usuarioActual.NumeroCaja, txtCambio.Text,
                        txtPagado.Text, txtContado.Text);
                    actualizarCorrelativoGrid();
                }

                if (estadoGuardar == -666)
                {
                    factDoccli.obtenerCorrelativo(factDoccli.TipoDocumento);
                    asignarCorrelativo(factDoccli.TipoDocumento);
                    //agregado
                    factDoccli.aumentarCorrelativo(factDoccli.TipoDocumento, factDoccli.CorrelativoInterno);
                    factDoccli.crearSentenciaCabecera(client1, vende1, referenciaPrincipal.empresaActual.BaseDatosActual, referenciaPrincipal.usuarioActual.Id, referenciaPrincipal.usuarioActual.NumeroCaja, txtCambio.Text,
                        txtPagado.Text, txtContado.Text);
                    actualizarCorrelativoGrid();
                }
            } while ((estadoGuardar == 1062) || (estadoGuardar == -666));

            ////agregado
            correlativoNuevo = factDoccli.CorrelativoInterno;
            actualizarCliVen(correlativoNuevo);
            //if (factDoccli.TipoDocumento.Equals("FAV"))
            //{
                factDoccli.guardarDetalleFac(this.dataGridView1);
                //se actualiza Inventario
                foreach (DataGridViewRow fila in dataGridView1.Rows)
                {
                    productoFactura.ajustarCanInventario((Convert.ToDecimal(fila.Cells["@mov_cant"].Value)), (fila.Cells["@mov_codigo"].Value.ToString()), factDoccli.TipoDocumento,
                        factDoccli.CorrelativoInterno, client1.Codigo);
                }
            //}
            //else if (factDoccli.TipoDocumento.Equals("DEV"))
            //{
            //    productoFactura.actualizarItemDev(this.dataGridView1);
            //    //poner los exportados en admdocli
            //    string[,] valores = new string[dataGridView1.Rows.Count, 4];

            //    for (int x = 0; x < dataGridView1.Rows.Count; x++)
            //    {
            //        valores[x, 0] = dataGridView1.Rows[x].Cells["@mov_cant"].Value.ToString();
            //        valores[x, 1] = factDoccli.FacturaAfectada;
            //        valores[x, 2] = dataGridView1.Rows[x].Cells["@mov_codigo"].Value.ToString();
            //        valores[x, 3] = dataGridView1.Rows[x].Cells["@mov_item"].Value.ToString();
            //    }
            //    factDoccli.actulizarItemsDev(valores);
            //    factDoccli.guardarDetalleFac(this.dataGridView1);
            //}
            //guarda en caja
            /*if (Convert.ToDecimal(txtCambio.Text) > 0)
            {
                agregarCambio();
            }
            if ((this.dataGridView2.Rows.Count > 0) && ((this.centinelaConfirmacion) || !(this.centinelaConfirmacion)))
            {
                cajaCobrar.insertMovCajaFacturacion(this.dataGridView2, factDoccli);
            }*/
            //limpiarCompleto();
            //activarCancelarFactura(false);
            //activarItemsFactura(true);
            //factDoccli.limpiarFactura();
            return correlativoNuevo;
        }

        public void actualizarCliVen(string numeroInterno)
        {
            foreach (DataGridViewRow fila in dataGridView1.Rows)
            {
                fila.Cells["@mov_docaso"].Value = "FAV";
                fila.Cells["@mov_docume"].Value = numeroInterno;
                fila.Cells["@mov_hora"].Value = DateTime.Now.ToString("hh:mm:ss tt");
                fila.Cells["@mov_tipdoc"].Value = "FAV";
                fila.Cells["@mov_fechven"].Value = "" + DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day;
                fila.Cells["@mov_fecha"].Value = "" + DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day;

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            txtDiaspp1.Focus();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
        }

        private void txtDiaspp1_KeyPress(object sender, KeyPressEventArgs e)
        {
            funtxt.onlyNumberWithTab(sender, e);
        }

        private void txtPorcenPP1_KeyPress(object sender, KeyPressEventArgs e)
        {
            funtxt.txtOnlyDecimal(sender, e, this.txtPorcenPP1);
        }

        private void txtDiaspp2_KeyPress(object sender, KeyPressEventArgs e)
        {
            funtxt.onlyNumberWithTab(sender, e);
        }

        private void txtPorcenPP2_KeyPress(object sender, KeyPressEventArgs e)
        {
            funtxt.txtOnlyDecimal(sender, e, this.txtPorcenPP2);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (validarDiasPP())
            {
                if (validarPorcentajePP())
                {
                    fac1.diasPP1 = txtDiaspp1.Text;
                    fac1.porcentajePP1 = txtPorcenPP1.Text.Replace(",", ".");
                    fac1.diasPP2 = txtDiaspp2.Text;
                    fac1.porcentajePP2 = txtPorcenPP2.Text.Replace(",", ".");
                    this.panel1.Visible = false;
                }
            }
        }

        private bool validarDiasPP()
        {
            bool centinela = true;
            int totalDias = 0;
            decimal plazoDias = Convert.ToDecimal(txtplazoDias.Text);
            if (!(string.IsNullOrEmpty(txtDiaspp1.Text)))
            {
                if (!(string.IsNullOrEmpty(txtDiaspp2.Text)))
                {
                    totalDias = Convert.ToInt32(txtDiaspp1.Text) + Convert.ToInt32(txtDiaspp2.Text);
                }
                else
                {
                    totalDias = Convert.ToInt32(txtDiaspp1.Text);
                }
                if (totalDias > plazoDias)
                {
                    MessageBox.Show("El Pronto Pago No puede ser mayor que la condicion de pago!!", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    centinela = false;
                    return centinela;
                }
            }
            return centinela;
        }


        private bool validarPorcentajePP()
        {
            bool centinela = true;
            if (!(string.IsNullOrEmpty(txtDiaspp1.Text)))
            {
                if (string.IsNullOrEmpty(txtPorcenPP1.Text))
                {
                    MessageBox.Show("Tiene que agregar Porcentaje Pronto Pago 1", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    centinela = false;
                    txtPorcenPP1.Focus();
                    return centinela;
                }
            }

            if ((!(string.IsNullOrEmpty(txtDiaspp1.Text))) && (!(string.IsNullOrEmpty(txtDiaspp2.Text))))
            {
                if (string.IsNullOrEmpty(txtPorcenPP2.Text))
                {
                    MessageBox.Show("Tiene que agregar Porcentaje Pronto Pago 2", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    centinela = false;
                    txtPorcenPP2.Focus();
                    return centinela;
                }
            }
            else
            {
                if ((!(string.IsNullOrEmpty(txtDiaspp2.Text))) && (string.IsNullOrEmpty(txtDiaspp1.Text)))
                {
                    MessageBox.Show("No puede Asignar Pronto Pago 2, sin el Pronto Pago 1", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    centinela = false;
                    txtDiaspp1.Focus();
                    return centinela;
                }
            }

            return centinela;
        }
    }
}