using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using lebrun.clases.importaciones;
using lebrun.formularios.complementos;
using lebrun.clases.complementos;
using lebrun.clasesData;
using System.Collections;

namespace lebrun.formularios.importaciones
{
    public partial class frmOrdenCompra : Form
    {

        //declaraciones

        private ConexionBD database;
        private OrdenDeCompra ordenCompra;
        private DetallesOrdenDeCompra detalleOrdenCompra;
        private lbxProveedores proveedores;
        private proveedor cproveedores;
        private Producto cproducto;
        private frmConsultaArticulos productos;
        private FuncionesTexbox funcionesTextBox;
        private static frmOrdenCompra m_FormDefInstance;
        private UsuarioSistema usuarioNuevo;
        //private string StrSql;
        private int centinelaFila = -1;
        //private decimal costoDes;
        private decimal desc;
        private decimal subtotalLineas;              // suma de las lineas ya con sus descuentos
        private decimal totalOrden;                  // despues de los descuentos del proveedor
        // proveedor cuyos descuentos, moneda y tasa estan en pantalla: mientras no cambie no se vuelven a leer de su ficha
        private string proveedorCargado;
        private bool cargandoMoneda;
        private string fechaSistema;
        //private Principal Principal;
        private string usuarioOrigen;
        private string usuaLogin;
        private string ordenPorCargar;               // numero de la orden que hay que mostrar cuando la ventana termine de abrirse
        private bool lista;                          // la ventana ya cargo sus datos
        public bool Grabada { get; private set; }    // se grabo la orden: la lista debe actualizarse

        // La ventana de la orden que esta abierta, o null. Solo se trabaja una a la vez: abrir otra sobre la misma ventana mezclaba sus lineas.
        public static frmOrdenCompra VentanaAbierta
        {
            get { return m_FormDefInstance != null && !m_FormDefInstance.IsDisposed && m_FormDefInstance.Visible ? m_FormDefInstance : null; }
        }

       


        public frmOrdenCompra()
        {
            InitializeComponent();
            usuarioNuevo = new UsuarioSistema();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            //if (txtOrdenNum.Text == "")
            //{
            //    ordenCompra.asignarCorrelativo();
            //}
            //else 
            //{
            //    ordenCompra.Correlativo = txtOrdenNum.Text ;
            //    detalleOrdenCompra.Documento = txtOrdenNum.Text;
            //    ordenCompra.eliminarCabeceraOrdenCompra();
            //    detalleOrdenCompra.eliminarDetallesOrdenCompra();
            //}
            //    asignandoValores();
            //    ordenCompra.guardarOrdenCompra();
            //    reemplazar();
            //    detalleOrdenCompra.guardarDetallesOrdenDeCompra(gridProductos);
            
            //    this.Close();
        }

        private void asignandoValores() {

            calcularTotales();

            ordenCompra.Documento = txtDocumento.Text;
            ordenCompra.Sufijo = txtSufijo.Text;
            ordenCompra.Cbtnum = null;
            ordenCompra.Cbtpre = null;
            ordenCompra.Cencost = "00001";
            ordenCompra.Codigo = txtCodProvee.Text;
            ordenCompra.Codtra = null;
            ordenCompra.Concep = txtConcepto.Text;
            ordenCompra.Condic = cmbCond.Text;
            //ordenCompra.Correlativo = "00001";
            ordenCompra.Cxp = null;
            ordenCompra.Ditem = txtItems.Text;
            ordenCompra.Estadodoc = "0";
            ordenCompra.Moneda = cmbMoneda.Text;
            //ordenCompra.Ordencompra = txtOrdenNum.Text;
            ordenCompra.Origen = "ORC";
            ordenCompra.Pagnum = null;
            ordenCompra.Plazo = cmbCond.Text;
            ordenCompra.Prod = txtUnidades.Text.Replace(",",".");
            ordenCompra.Status = "0";
            ordenCompra.Sucus = "00001";
            ordenCompra.Tipdoc = "30";
            ordenCompra.Tiptra = null;
            ordenCompra.Totaldivisa = null;
            ordenCompra.Usuario = usuarioNuevo.Id;
            ordenCompra.Valcamb = txtValCamb.Text.Replace(",",".");
            ordenCompra.Totaldivisa = txtTotalCambio.Text.Replace(",",".");
            ordenCompra.Ord = txtOrd.Text.Replace(",",".");
            ordenCompra.FechaCarga = ""+dateTimePicker1.Value.Year + "-" + dateTimePicker1.Value.Month + "-" + dateTimePicker1.Value.Day;
            ordenCompra.FechaRecep = ""+dateTimePicker2.Value.Year + "-" + dateTimePicker2.Value.Month + "-" + dateTimePicker2.Value.Day;
            ordenCompra.FechaSistema = fechaSistema;
            ordenCompra.UpdUsuario = usuaLogin;
            ordenCompra.FechaUltMod = "" + DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day;

            if (usuarioOrigen == "" || usuarioOrigen == null)
            {

                ordenCompra.Usuario = usuaLogin;

            }
            else {

                ordenCompra.Usuario = usuarioOrigen;
            }


            ordenCompra.SubTotal = subtotalLineas.ToString(CultureInfo.InvariantCulture);
            ordenCompra.Total = totalOrden.ToString(CultureInfo.InvariantCulture);
            ordenCompra.Descuento1 = numero(txtDescuento1.Text);
            ordenCompra.Descuento2 = numero(txtDescuento2.Text);
            ordenCompra.Descuento3 = numero(txtDescuento3.Text);

            for (int i = 0; i < gridProductos.Rows.Count; i++)
            {

                gridProductos.Rows[i].Cells[10].Value = txtCodProvee.Text;
                //gridProductos.Rows[i].Cells[11].Value = txtDocumento.Text;
                gridProductos.Rows[i].Cells[11].Value = ordenCompra.Correlativo;
                gridProductos.Rows[i].Cells[12].Value = txtSufijo.Text;
                gridProductos.Rows[i].Cells[13].Value = dateTimePicker1.Value.Hour + ":" + dateTimePicker1.Value.Minute + ":" + dateTimePicker1.Value.Second;
                gridProductos.Rows[i].Cells[14].Value = dateTimePicker1.Value.Year + "-" + dateTimePicker1.Value.Month.ToString().PadLeft(2, '0')+ "-" + dateTimePicker1.Value.Day;
                gridProductos.Rows[i].Cells[15].Value = dateTimePicker2.Value.Year+"-"+dateTimePicker2.Value.Month.ToString().PadLeft(2,'0')+"-"+dateTimePicker2.Value.Day;
                gridProductos.Rows[i].Cells[16].Value = "30";
                
            }
        }

        private void frmOrdenCompra_Load(object sender, EventArgs e)
        {
                database = new ConexionBD();
                funcionesTextBox = new FuncionesTexbox();
                this.cproveedores = new proveedor();
                this.cproducto = new Producto();
                this.cargarComboPago();
                this.ordenCompra = new OrdenDeCompra();
                this.detalleOrdenCompra = new DetallesOrdenDeCompra();
                // si cambian los descuentos en pantalla (al elegir un proveedor en la lista) se recalculan los totales
                foreach (TextBox campo in new[] { txtDescuento1, txtDescuento2, txtDescuento3 })
                {
                    campo.TextChanged += delegate { calcularTotales(); };
                }
                // en una orden nueva la tasa es la de la fecha de la orden; una orden grabada conserva la suya
                dateTimePicker1.ValueChanged += delegate
                {
                    if (txtOrdenNum.Text == "" && proveedorCargado != null)
                    {
                        mostrarTasa(cmbMoneda.Text);
                    }
                };
                lista = true;
                // la orden se carga despues de llenar las listas: antes se cargaba primero y al abrir la ventana la condicion volvia a la primera
                if (ordenPorCargar != null)
                {
                    cargarOrden(ordenPorCargar);
                }





        }

        private void button4_Click(object sender, EventArgs e)
        {
            //this.Close();
        }

        public void agregarFila(int fila) {
             if (fila<0){
                 gridProductos.Rows.Add();
                 fila = gridProductos.Rows.Count - 1;
            }

            gridProductos.Rows[fila].Cells[0].Value = fila + 1;
            gridProductos.Rows[fila].Cells[1].Value = txtProducto.Text;
            gridProductos.Rows[fila].Cells[2].Value = lblDesProducto.Text;
            gridProductos.Rows[fila].Cells[3].Value = txtUnidad.Text;
            gridProductos.Rows[fila].Cells[4].Value = txtCant.Text;
            gridProductos.Rows[fila].Cells[5].Value = txtCosto.Text;
            gridProductos.Rows[fila].Cells[6].Value = txtDes1.Text;
            gridProductos.Rows[fila].Cells[7].Value = txtDes2.Text;
            gridProductos.Rows[fila].Cells[8].Value = txtDes3.Text;
            gridProductos.Rows[fila].Cells[9].Value = "" + Convert.ToString(Convert.ToDecimal(txtCosto.Text.Replace(".",",")) * Convert.ToDecimal(txtCant.Text.Replace(".",","))).Replace(",",".");
            gridProductos.Rows[fila].Cells[10].Value = txtCodProvee.Text ;
            //gridProductos.Rows[fila].Cells[11].Value = txtDocumento.Text;
            gridProductos.Rows[fila].Cells[11].Value = txtOrdenNum.Text ;
            gridProductos.Rows[fila].Cells[12].Value = txtSufijo.Text;
            //gridProductos.Rows[fila].Cells[13].Value = txtDocumento.Text;

            //if (Convert.ToDecimal(txtDes1.Text) != 0)
            if (txtDes1.Text != "0,00")
            {
                desc = Convert.ToDecimal(txtCosto.Text.Replace(".", ",")) * Convert.ToDecimal(txtDes1.Text.Replace(".", ",")) / 100;
                gridProductos.Rows[fila].Cells[17].Value = (Convert.ToDecimal(gridProductos.Rows[fila].Cells[5].Value.ToString().Replace(".", ","))) - desc;
                gridProductos.Rows[fila].Cells[18].Value = (Convert.ToDecimal(gridProductos.Rows[fila].Cells[17].Value.ToString().Replace(".", ","))) * (Convert.ToDecimal(gridProductos.Rows[fila].Cells[4].Value.ToString().Replace(".", ",")));
            }
            else 
            { 
                gridProductos.Rows[fila].Cells[17].Value = (Convert.ToDecimal(gridProductos.Rows[fila].Cells[5].Value.ToString().Replace(".", ",")));
                gridProductos.Rows[fila].Cells[18].Value = (Convert.ToDecimal(gridProductos.Rows[fila].Cells[17].Value.ToString().Replace(".", ","))) * (Convert.ToDecimal(gridProductos.Rows[fila].Cells[4].Value.ToString().Replace(".", ",")));
            }

            //if (Convert.ToDecimal(txtDes2.Text) != 0)
            if (txtDes2.Text != "0,00")
            {
                desc = Convert.ToDecimal(gridProductos.Rows[fila].Cells[17].Value.ToString().Replace(".", ",")) * Convert.ToDecimal(txtDes2.Text.Replace(".", ",")) / 100;
                gridProductos.Rows[fila].Cells[17].Value = (Convert.ToDecimal(gridProductos.Rows[fila].Cells[17].Value.ToString().Replace(".", ","))) - desc;
                gridProductos.Rows[fila].Cells[18].Value = (Convert.ToDecimal(gridProductos.Rows[fila].Cells[17].Value.ToString().Replace(".", ","))) * (Convert.ToDecimal(gridProductos.Rows[fila].Cells[4].Value.ToString().Replace(".", ",")));
            }
            //else
            //{
            //    gridProductos.Rows[fila].Cells[17].Value = (Convert.ToDecimal(gridProductos.Rows[fila].Cells[5].Value.ToString().Replace(".", ",")));
            //    gridProductos.Rows[fila].Cells[18].Value = (Convert.ToDecimal(gridProductos.Rows[fila].Cells[17].Value.ToString().Replace(".", ","))) * (Convert.ToDecimal(gridProductos.Rows[fila].Cells[4].Value.ToString().Replace(".", ",")));
            //}

            //if (Convert.ToDecimal(txtDes3.Text) != 0)
            if (txtDes3.Text != "0,00")
            {
                desc = Convert.ToDecimal(gridProductos.Rows[fila].Cells[17].Value.ToString().Replace(".", ",")) * Convert.ToDecimal(txtDes3.Text.Replace(".", ",")) / 100;
                gridProductos.Rows[fila].Cells[17].Value = (Convert.ToDecimal(gridProductos.Rows[fila].Cells[17].Value.ToString().Replace(".", ","))) - desc;
                gridProductos.Rows[fila].Cells[18].Value = (Convert.ToDecimal(gridProductos.Rows[fila].Cells[17].Value.ToString().Replace(".", ","))) * (Convert.ToDecimal(gridProductos.Rows[fila].Cells[4].Value.ToString().Replace(".", ",")));
            }
            //else
            //{
            //    gridProductos.Rows[fila].Cells[17].Value = (Convert.ToDecimal(gridProductos.Rows[fila].Cells[5].Value.ToString().Replace(".", ",")));
            //    gridProductos.Rows[fila].Cells[18].Value = (Convert.ToDecimal(gridProductos.Rows[fila].Cells[17].Value.ToString().Replace(".", ","))) * (Convert.ToDecimal(gridProductos.Rows[fila].Cells[4].Value.ToString().Replace(".", ",")));
            //}
            gridProductos.Rows[fila].Cells[19].Value = txtPorcArancel.Text.Replace(",", ".");
            calcularTotales();
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            if (validarCamposGrid()){
                validarOrigenArticulo();
                if (mismoArticulo())
                {
                    calcularTotales();
                }
                else
                    
                {
                    if (centinelaFila >= 0)
                    {
                        agregarFila(centinelaFila);
                        centinelaFila = -1;
                    }
                    else
                    {
                        agregarFila(-1);
                    }
                }
                    limpiarToGrid();
                    //calcularTotales();
                    
            }
      
        }
        private void button1_Click(object sender, EventArgs e)
        {
            proveedores = lbxProveedores.DefInstance;
            proveedores.destino(this,this.Name);
            //proveedores.MdiParent = this.MdiParent;
            proveedores.Show();
  
        }

        public void cargarComboPago() {
            
            DataTable condicion = new DataTable();
            condicion = database.fDataTable("SELECT * FROM admcondpago WHERE conp_tipo = 'C' ORDER BY conp_codigo ASC");
            cmbCond.DisplayMember = "conp_descripcion";
            cmbCond.ValueMember = "conp_cant_dias";
            cmbCond.DataSource = condicion;
        }

        // Deja elegida la moneda. A los proveedores de importacion (codigo menor que 8900000) se les puede cambiar: se les listan todas.
        private void llenarMonedas(string moneda, bool importado)
        {
            cargandoMoneda = true;
            try
            {
                cmbMoneda.DataSource = null;
                cmbMoneda.ValueMember = "";
                cmbMoneda.Items.Clear();
                if (importado)
                {
                    DataTable monedas = database.fDataTable("SELECT DISTINCT histo_moneda FROM admhistodivi ORDER BY histo_moneda");
                    foreach (DataRow r in monedas.Rows)
                    {
                        cmbMoneda.Items.Add(Convert.ToString(r[0]));
                    }
                }
                if (!cmbMoneda.Items.Contains(moneda))
                {
                    cmbMoneda.Items.Insert(0, moneda);
                }
                cmbMoneda.SelectedIndex = cmbMoneda.Items.IndexOf(moneda);
            }
            finally
            {
                cargandoMoneda = false;
            }
        }

        // La tasa de la moneda a la fecha de la orden.
        private void mostrarTasa(string moneda)
        {
            decimal? tasa = OrdenDeCompra.TasaDeMoneda(moneda, dateTimePicker1.Value.Date);
            if (!tasa.HasValue)
            {
                MessageBox.Show("No hay tasa de cambio cargada para la moneda " + moneda + " a la fecha de la orden. Cárguela antes de grabar.", "Orden de compra",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            txtValCamb.Text = textoTasa(tasa ?? 0m);
            calcularTotales();
        }

        private static string textoTasa(decimal tasa)
        {
            return tasa.ToString("0.00######");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            productos = frmConsultaArticulos.DefInstance;
            productos.destino(this, this.Name);
            productos.MdiParent = this.MdiParent;
            productos.Show();
        }

        private void txtCodProvee_KeyPress(object sender, KeyPressEventArgs e)
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

                if (txtCodProvee.Text != "")
                {
                    if (consultaRapidaProveedor()) { cmbCond.Focus(); }
                }
                else
                {

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

        private void txtProducto_KeyPress(object sender, KeyPressEventArgs e)
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

                if (txtProducto.Text != "")
                {
                    if (consultaRapidaProducto())
                    {
                        SendKeys.Send("{TAB}");
                        e.Handled = false;
                    } 
                }
                else
                {
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

        private void cmbCond_KeyPress(object sender, KeyPressEventArgs e)
        {
            funcionesTextBox.tab(sender, e);
        }

        private void txtDocumento_KeyPress(object sender, KeyPressEventArgs e)
        {
            funcionesTextBox.tab(sender, e);
        }

        private void txtSufijo_KeyPress(object sender, KeyPressEventArgs e)
        {
            funcionesTextBox.tab(sender, e); 
        }

        private void cmbMoneda_KeyPress(object sender, KeyPressEventArgs e)
        {
            funcionesTextBox.tab(sender, e);
        }

        private void txtConcepto_KeyPress(object sender, KeyPressEventArgs e)
        {
            funcionesTextBox.tab(sender, e);
        }


        private void txtCosto_KeyPress(object sender, KeyPressEventArgs e)
        {
            //funcionesTextBox.only2Decimal(sender, e, txtCosto);

            TextBox a = (TextBox)sender;
            funcionesTextBox.conDecimalesDefinidos(sender, e, a, 6);
            //funcionesTextBox.txtOnlyDecimal(sender, e, txtCosto);
        }

        private void txtDes1_KeyPress(object sender, KeyPressEventArgs e)
        {
            //funcionesTextBox.txtOnlyDecimal(sender, e, txtDes1);
            TextBox a = (TextBox)sender;
            funcionesTextBox.conDecimalesDefinidos(sender, e, a, 2);
        }

        private void txtDes2_KeyPress(object sender, KeyPressEventArgs e)
        {
            //funcionesTextBox.txtOnlyDecimal(sender, e, txtDes2);
            TextBox a = (TextBox)sender;
            funcionesTextBox.conDecimalesDefinidos(sender, e, a, 2);
        }

        private void txtDes3_KeyPress(object sender, KeyPressEventArgs e)
        {
            //funcionesTextBox.txtOnlyDecimal(sender, e, txtDes3);
            TextBox a = (TextBox)sender;
            funcionesTextBox.conDecimalesDefinidos(sender, e, a, 2);
        }



        private void txtCant_KeyPress(object sender, KeyPressEventArgs e)
        {
            funcionesTextBox.onlyNumberWithTab(sender, e);
            
        }

        private bool consultaRapidaProveedor() //PROVEEDOR
        { 
            bool val2 =false;

            if (proveedorCargado != null && txtCodProvee.Text == proveedorCargado)
            {
                return true;
            }

            DataTable dproveedor = new DataTable();
            dproveedor = cproveedores.consultarProveedorUnico(txtCodProvee.Text);

            if (dproveedor.Rows.Count != 0)
             {
               lblNombreProvee.Text = dproveedor.Rows[0][1].ToString();
               lblRif.Text = dproveedor.Rows[0][2].ToString();
               proveedorCargado = txtCodProvee.Text;
               string moneda = dproveedor.Rows[0][3].ToString();
               llenarMonedas(moneda, Convert.ToInt32(txtCodProvee.Text) < 8900000);
               txtDescuento1.Text = numero(dproveedor.Rows[0]["pro_descuento"]).ToString("0.00####");
               txtDescuento2.Text = numero(dproveedor.Rows[0]["pro_dcto2"]).ToString("0.00####");
               txtDescuento3.Text = numero(dproveedor.Rows[0]["pro_dcto3"]).ToString("0.00####");
               mostrarTasa(moneda);

               val2 = true;

                 }else {

                  MessageBox.Show("No existe ningun proveedor registrado con este codigo", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                  txtCodProvee.Text = "";
                  lblNombreProvee.Text = "...";
                  lblRif.Text = "...";
                  txtDescuento1.Text = "0,00";
                  txtDescuento2.Text = "0,00";
                  txtDescuento3.Text = "0,00";
                  proveedorCargado = null;
                  val2 = false;
                  
                 }
            return val2;

           }

        private bool consultaRapidaProducto() //PRODUCTO
        {
            bool val = false;

            DataTable dproducto = new DataTable();
            dproducto = cproducto.buscarProductoUnico(txtProducto.Text);

            if (dproducto.Rows.Count != 0)
            {
                txtProducto.Text = dproducto.Rows[0]["Codigo"].ToString();
                lblDesProducto.Text = dproducto.Rows[0]["Descripcion"].ToString();
                txtUnidad.Text = dproducto.Rows[0]["Unidad"].ToString();
                lblDescriUni.Text = dproducto.Rows[0]["Presentacion"].ToString();
                lblOrigen.Text = dproducto.Rows[0]["Procedencia"].ToString();
                lblpro_principal.Text = dproducto.Rows[0]["pro_principal"].ToString();
                txtCodArancel.Text = dproducto.Rows[0]["ara_codigo"].ToString();
                txtPorcArancel.Text = dproducto.Rows[0]["ara_porcen"].ToString();


                if (Convert.ToDecimal(dproducto.Rows[0]["Pre lista"].ToString()) != 0)
                {

                    txtCosto.Text = dproducto.Rows[0]["Pre lista"].ToString();
                }
                else {

                    txtCosto.Text = dproducto.Rows[0]["Ultimo Costo"].ToString();
                }

                val = true;

            }
            else
            {
                MessageBox.Show("No existe articulos registrado con este codigo, o esta inactivo", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtProducto.Text = "";
                lblDesProducto.Text = "...";
                txtUnidad.Text = "";
                lblDescriUni.Text = "...";

                val = false;

            }
            return val;
        }

        private bool validarCamposGrid()  //VALIDAR CAMPOS GRID
        { 
            bool flag;
            flag = false;

            if (txtProducto.Text != "")
            {
                flag = true;
            }
            else { flag = false; MessageBox.Show("Debe ingresar un articulo", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information); return false; }

            if (txtCant.Text != "0,00") 
            {
                flag = true;
            }
            else { flag = false; MessageBox.Show("El cantidad no debe ser igual cero (0)", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information); txtCant.Focus(); return false; }

            if (Convert.ToDecimal(txtCosto.Text) != 0)
            {
                flag = true;
            }
            else { flag = false; MessageBox.Show("La costo no debe ser igual a cero (0)", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information); txtCosto.Focus(); return false; }


            if (txtCodProvee.Text != "")
            {
                flag = true;
            }
            else { flag = false; MessageBox.Show("Debe seleccionar un proveedor", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information); return false; }

            return flag;
        }

        private void txtProducto_Leave(object sender, EventArgs e)
        {
            if (txtProducto.Text != "")
            {
                if (!consultaRapidaProducto()) { txtProducto.Focus(); } 
            }
        }

        private void txtCodProvee_Leave(object sender, EventArgs e)
        {
            if (txtCodProvee.Text != "")
            {
                if (!consultaRapidaProveedor()) { txtCodProvee.Focus(); }
            }
            else
            {
                proveedorCargado = null;
                lblRif.Text = "...";
                lblNombreProvee.Text = "...";
                txtDescuento1.Text = "0,00";
                txtDescuento2.Text = "0,00";
                txtDescuento3.Text = "0,00";



            }
        }

        private void txtCant_Leave(object sender, EventArgs e)
        {
            if (txtCant.Text == "")
            {
                txtCant.Text = "0";
                txtCant.Text = string.Format("{0:0.00}", Convert.ToDecimal(txtCant.Text));
            }
            else {
                txtCant.Text = string.Format("{0:0.00}", Convert.ToDecimal(txtCant.Text));
            }
        }

        private void txtCosto_Leave(object sender, EventArgs e)
        {
            if (txtCosto.Text == "")
            {
                txtCosto.Text = string.Format("{0:0.0000000000}", Convert.ToDecimal(txtCant.Text));
            }
            else {
                txtCosto.Text = string.Format("{0:0.0000000000}", Convert.ToDecimal(txtCosto.Text));
            }
        }

        private void txtDes1_Leave(object sender, EventArgs e)
        {
            if (txtDes1.Text == "")
            {
                txtDes1.Text = string.Format("{0:0.00}", Convert.ToDecimal(txtDes1.Text));
            }
            else {
                txtDes1.Text = string.Format("{0:0.00}", Convert.ToDecimal(txtDes1.Text));
            }
        }

        private void txtDes2_Leave(object sender, EventArgs e)
        {
            if (txtDes2.Text == "")
            {
                txtDes2.Text = string.Format("{0:0.00}", Convert.ToDecimal(txtDes2.Text));
            }
            else 
            {
                validarDesc(txtDes2);
                txtDes2.Text = string.Format("{0:0.00}", Convert.ToDecimal(txtDes2.Text));
            }
        }

        private void txtDes3_Leave(object sender, EventArgs e)
        {
            if (txtDes3.Text == "")
            {
                txtDes3.Text = string.Format("{0:0.00}", Convert.ToDecimal(txtDes3.Text));
            }
            else 
            {
                validarDesc(txtDes3);
                txtDes3.Text = string.Format("{0:0.00}", Convert.ToDecimal(txtDes3.Text));
            }
        }

        private void limpiarToGrid() 
        {
            txtProducto.Text = "";
            txtCosto.Text = "0,00";
            txtCant.Text = "0,00";
            txtUnidad.Text = "";
            txtDes1.Text = "0,00";
            txtDes2.Text = "0,00";
            txtDes3.Text = "0,00";
            lblDesProducto.Text = "...";
            lblDescriUni.Text = "...";
            txtPorcArancel.Text = "0,00";
            txtCodArancel.Text = "";
            this.txtProducto.Focus();
        }

        private void validarDesc(TextBox textBox)
        {
            if (textBox.Name=="txtDes2")
            {
                if (txtDes1.Text == "0,00" && textBox.Text!="0,00"){
                    textBox.Text = "0,00";
                    MessageBox.Show("No puede asignar un segundo descuento si no existe un primer descuento","Algo no esta bien",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
            }
            if (textBox.Name == "txtDes3") 
            {
                if (txtDes2.Text == "0,00" && textBox.Text != "0,00")
                {
                    textBox.Text = "0,00";
                    MessageBox.Show("No puede asignar un tercer descuento si no existe un secundo descuento","Algo no esta bien",MessageBoxButtons.OK,MessageBoxIcon.Information); 
                }
            }
        }

        private void cmbMoneda_SelectedValueChanged(object sender, EventArgs e)
        {
            // solo cuando la elige el usuario: al cargar el proveedor o una orden la tasa se pone aparte
            if (!cargandoMoneda && lista && cmbMoneda.SelectedIndex >= 0)
            {
                mostrarTasa(cmbMoneda.Text);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //if (gridProductos.Rows.Count > 0)
            //{
            //    gridProductos.Rows.RemoveAt(gridProductos.CurrentRow.Index);
            //    actualizarNumeroItems();
            //    calcularTotales();
            //}
        }

        private void actualizarNumeroItems() {

            for (int i = 0; i < gridProductos.Rows.Count; i++) {
                gridProductos.Rows[i].Cells[0].Value = i+1;
            }
        }

        // Los totales de la pantalla: Total (lineas sin ningun descuento), Des Item (descuentos de las lineas), Des Docu (descuentos del proveedor,
        // en cascada), Total Orden (lo que queda) y Total en Bs (Total Orden por la tasa).
        private void calcularTotales()
        {
            decimal bruto = 0m, neto = 0m, unidades = 0m;
            foreach (DataGridViewRow fila in gridProductos.Rows)
            {
                if (fila.IsNewRow || fila.Cells[18].Value == null)
                {
                    continue;
                }
                bruto += numero(fila.Cells[4].Value) * numero(fila.Cells[5].Value);
                neto += numero(fila.Cells[18].Value);
                unidades += numero(fila.Cells[4].Value);
            }
            subtotalLineas = Math.Round(neto, 2, MidpointRounding.AwayFromZero);
            totalOrden = OrdenDeCompra.TotalConDescuentos(subtotalLineas, numero(txtDescuento1.Text), numero(txtDescuento2.Text), numero(txtDescuento3.Text));
            decimal brutoRedondeado = Math.Round(bruto, 2, MidpointRounding.AwayFromZero);

            txtItems.Text = gridProductos.Rows.Count.ToString();
            txtUnidades.Text = unidades.ToString("0.00");
            txtTotal1.Text = brutoRedondeado.ToString("0.00");
            txtDesitem.Text = (brutoRedondeado - subtotalLineas).ToString("0.00");
            txtDesDocum.Text = (subtotalLineas - totalOrden).ToString("0.00");
            txtTotal.Text = totalOrden.ToString("0.00");
            txtTotalCambio.Text = Math.Round(totalOrden * numero(txtValCamb.Text), 2, MidpointRounding.AwayFromZero).ToString("0.00");
        }

        // Convierte el texto de un campo o una celda en numero, con coma o punto decimal (los numeros no llevan separador de miles).
        private static decimal numero(object valor)
        {
            decimal n;
            return decimal.TryParse(Convert.ToString(valor).Trim().Replace(",", "."), NumberStyles.Number, CultureInfo.InvariantCulture, out n) ? n : 0m;
        }

        // Si el producto ya esta en la orden con la misma unidad, costo y descuentos, se suma la cantidad en esa linea. Se comparan los numeros y no
        // los textos: antes se comparaban formatos distintos del mismo costo y siempre se agregaba otra linea.
        private bool mismoArticulo() {

            for (int i = 0; i < gridProductos.Rows.Count; i++)
            {
                DataGridViewCellCollection celdas = gridProductos.Rows[i].Cells;
                if (Convert.ToString(celdas[1].Value) == txtProducto.Text && Convert.ToString(celdas[3].Value) == txtUnidad.Text &&
                    numero(celdas[5].Value) == numero(txtCosto.Text) && numero(celdas[6].Value) == numero(txtDes1.Text) &&
                    numero(celdas[7].Value) == numero(txtDes2.Text) && numero(celdas[8].Value) == numero(txtDes3.Text))
                {
                    decimal cantidad = numero(celdas[4].Value) + numero(txtCant.Text);
                    celdas[4].Value = string.Format("{0:0.00}", cantidad);
                    celdas[9].Value = (numero(celdas[5].Value) * cantidad).ToString(CultureInfo.InvariantCulture);
                    celdas[18].Value = numero(celdas[17].Value) * cantidad;
                    return true;
                }
            }
            return false;
        }

        private void txtCodProvee_MouseUp(object sender, MouseEventArgs e)
        {
            txtCodProvee.SelectAll();
        }

        public static frmOrdenCompra DefInstance
        {
            get
            {
                if (m_FormDefInstance == null || m_FormDefInstance.IsDisposed)
                    m_FormDefInstance = new frmOrdenCompra();
                return m_FormDefInstance;
            }
            set
            {
                m_FormDefInstance = value;
            }
        }

        public void cargarModificar(string correlativo)
        {
            ordenPorCargar = correlativo;
            if (lista)
            {
                cargarOrden(correlativo);
            }
        }

        private void cargarOrden(string correlativo)
        {
            gridProductos.Rows.Clear();
            ordenCompra = new OrdenDeCompra();
            ordenCompra.Correlativo = correlativo;
            ordenCompra.buscarOrdenCompra();
            mostrarDatosCabecera();
            detalleOrdenCompra = new DetallesOrdenDeCompra();
            detalleOrdenCompra.Documento = correlativo;
            mostrarDetalleOrden();
            calcularTotales();
        }

        // Los descuentos con que se grabo la orden. Las anteriores no tienen los porcentajes: sin descuento van en cero; si el total es menor que la
        // suma de las lineas, la diferencia se muestra como un solo descuento.
        private void mostrarDescuentos()
        {
            decimal d1 = ordenCompra.Descuento1 ?? 0m;
            if (!ordenCompra.Descuento1.HasValue)
            {
                decimal subtotal = numero(ordenCompra.SubTotal), total = numero(ordenCompra.Total);
                if (subtotal > 0m && subtotal - total >= 0.005m)
                {
                    d1 = Math.Round((subtotal - total) * 100m / subtotal, 6);
                }
            }
            txtDescuento1.Text = d1.ToString("0.00####");
            txtDescuento2.Text = (ordenCompra.Descuento2 ?? 0m).ToString("0.00####");
            txtDescuento3.Text = (ordenCompra.Descuento3 ?? 0m).ToString("0.00####");
        }

        public void mostrarDatosCabecera()
        {

            txtOrdenNum.Text = ordenCompra.Correlativo;
            txtDocumento.Text = ordenCompra.Documento;
            txtSufijo.Text = ordenCompra.Sufijo;
            // el proveedor de la orden ya esta cargado: al salir del campo no se reemplazan sus descuentos ni su tasa por los de hoy
            proveedorCargado = ordenCompra.Codigo;
            int codigoNumerico;
            llenarMonedas(ordenCompra.Moneda, int.TryParse(ordenCompra.Codigo, out codigoNumerico) && codigoNumerico < 8900000);
            txtValCamb.Text = textoTasa(numero(ordenCompra.Valcamb));
            mostrarDescuentos();
            txtConcepto.Text = ordenCompra.Concep;
            seleccionarCondicion(ordenCompra.Condic);
            txtCodProvee.Text = ordenCompra.Codigo;
            lblNombreProvee.Text = ordenCompra.ProNombre;
            lblRif.Text = ordenCompra.ProRif;
            txtTotal.Text = ""+ordenCompra.SubTotal;
            txtTotal1.Text = "" + ordenCompra.Total;
            txtUnidades.Text = "" + ordenCompra.Prod;
            txtItems.Text = "" + ordenCompra.Ditem;
            txtTotalCambio.Text = "" + ordenCompra.Totaldivisa;
            dateTimePicker1.Value = Convert.ToDateTime(ordenCompra.FechaCarga);
            dateTimePicker2.Value = Convert.ToDateTime(ordenCompra.FechaRecep);
            fechaSistema = ordenCompra.FechaSistema;
            usuarioOrigen = ordenCompra.Usuario;
            

        }

        // Deja elegida la condicion de pago con que se grabo la orden. Si ya no esta en el maestro (un texto antiguo) se agrega a la lista, para que
        // al aceptar no se cambie por otra.
        private void seleccionarCondicion(string condicion)
        {
            if (string.IsNullOrWhiteSpace(condicion))
            {
                return;
            }
            int posicion = cmbCond.FindStringExact(condicion.Trim());
            if (posicion < 0)
            {
                DataTable condiciones = (DataTable)cmbCond.DataSource;
                DataRow fila = condiciones.NewRow();
                fila["conp_descripcion"] = condicion.Trim();
                condiciones.Rows.Add(fila);
                posicion = cmbCond.FindStringExact(condicion.Trim());
            }
            if (posicion >= 0)
            {
                cmbCond.SelectedIndex = posicion;
            }
        }

        public void mostrarDetalleOrden()
        {
            DataTable temporal = new DataTable();
            int fila = 0;
            temporal = detalleOrdenCompra.detalleOrdenCompra();
            for (int i = 0; i < temporal.Rows.Count; i++)
            {
                
                gridProductos.Rows.Add();
                fila = gridProductos.Rows.Count - 1;
                gridProductos.Rows[fila].Cells[0].Value = temporal.Rows[i]["mov_item"].ToString();
                gridProductos.Rows[fila].Cells[1].Value = temporal.Rows[i]["mov_codigo"].ToString();
                gridProductos.Rows[fila].Cells[2].Value = temporal.Rows[i]["mov_memo"].ToString();
                gridProductos.Rows[fila].Cells[3].Value = temporal.Rows[i]["mov_undmed"].ToString();
                gridProductos.Rows[fila].Cells[4].Value = temporal.Rows[i]["mov_cant"].ToString().Replace(".", ","); 
                gridProductos.Rows[fila].Cells[5].Value = temporal.Rows[i]["mov_costoorig"].ToString().Replace(".", ","); 
                gridProductos.Rows[fila].Cells[6].Value = temporal.Rows[i]["mov_desc"].ToString().Replace(".", ","); 
                gridProductos.Rows[fila].Cells[7].Value = temporal.Rows[i]["mov_desc2"].ToString().Replace(".", ","); 
                gridProductos.Rows[fila].Cells[8].Value = temporal.Rows[i]["mov_desc3"].ToString().Replace(".", ","); 
                gridProductos.Rows[fila].Cells[9].Value = temporal.Rows[i]["mov_totalmon"].ToString().Replace(".", ","); 
                gridProductos.Rows[fila].Cells[10].Value = temporal.Rows[i]["mov_codcta"].ToString();
                gridProductos.Rows[fila].Cells[11].Value = temporal.Rows[i]["mov_docume"].ToString();
                gridProductos.Rows[fila].Cells[12].Value = temporal.Rows[i]["mov_sufijo"].ToString();
                gridProductos.Rows[fila].Cells[13].Value = temporal.Rows[i]["mov_hora"].ToString();
                gridProductos.Rows[fila].Cells[14].Value = temporal.Rows[i]["mov_fecha"].ToString();
                gridProductos.Rows[fila].Cells[15].Value = temporal.Rows[i]["mov_fechven"].ToString();
                gridProductos.Rows[fila].Cells[16].Value = temporal.Rows[i]["mov_tipdoc"].ToString();
                gridProductos.Rows[fila].Cells[17].Value = temporal.Rows[i]["mov_precio"].ToString().Replace(".", ",");
                gridProductos.Rows[fila].Cells[18].Value = temporal.Rows[i]["mov_total"].ToString().Replace(".", ",");
                gridProductos.Rows[fila].Cells[19].Value = temporal.Rows[i]["mov_arancel"].ToString().Replace(",", ".");
                
                //
            }

                gridProductos.Columns[10].Visible = false;
                gridProductos.Columns[11].Visible = false;
                gridProductos.Columns[12].Visible = false;
                gridProductos.Columns[13].Visible = false;
                gridProductos.Columns[14].Visible = false;
                gridProductos.Columns[15].Visible = false;
                gridProductos.Columns[16].Visible = false;
        }


        private void validarOrigenArticulo() 
        {
            if ((lblpro_principal.Text != ""))
            {
                if ((Convert.ToInt32(txtCodProvee.Text) < 08900000) && lblOrigen.Text.Equals("Nacional"))
                {
                    MessageBox.Show("El producto es de origen Nacional", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            if ((lblpro_principal.Text != "") )
            {
                if ((Convert.ToInt32(txtCodProvee.Text) > 08900000) && lblOrigen.Text.Equals("Importado"))
                {
                    MessageBox.Show("El producto es de origen Importado", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            if ((lblpro_principal.Text != "") )
            {
                if ((Convert.ToInt32(txtCodProvee.Text) != (Convert.ToInt32(lblpro_principal.Text))))
                {
                    MessageBox.Show("El proveedor seleccionado no es el proveedor principal de esta producto", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        public void modificarUsu(string idUsuario) {
            //this.usuarioNuevo.Id = idUsuario;
            usuaLogin = idUsuario;
        }

        private void dateTimePicker1_KeyPress(object sender, KeyPressEventArgs e)
        {
            funcionesTextBox.tab(sender, e);   
        }

        private void dateTimePicker2_KeyPress(object sender, KeyPressEventArgs e)
        {
            //funcionesTextBox.tab(sender, e); 
            if (e.KeyChar == '\r')
            {
                txtProducto.Focus();
            }
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            if (gridProductos.Rows.Count > 0)
            {
                // sin proveedor no hay descuentos ni tasa que aplicar
                if (proveedorCargado == null || numero(txtValCamb.Text) <= 0m)
                {
                    MessageBox.Show("Elija el proveedor y verifique que su moneda tenga tasa de cambio antes de grabar.", "Orden de compra",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCodProvee.Focus();
                    return;
                }
                try
                {
                    ordenCompra.grabar(txtOrdenNum.Text == "" ? null : txtOrdenNum.Text, asignandoValores, gridProductos);
                }
                catch (Exception ex)
                {
                    RegistroErrores.Registrar(ex, "Orden de compra: grabar");
                    calcularTotales();
                    MessageBox.Show("No se pudo grabar la orden de compra y no se cambió nada. Los datos siguen en pantalla para intentarlo de nuevo.\n\n" + ex.Message,
                                    "Orden de compra", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                Grabada = true;
                this.Close();
            }
            else {
                MessageBox.Show("No se puede guardar la orden de compra sin movimiento de articulos", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
            }

        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click_2(object sender, EventArgs e)
        {
            if (gridProductos.Rows.Count > 0)
            {
                gridProductos.Rows.RemoveAt(gridProductos.CurrentRow.Index);
                actualizarNumeroItems();
                calcularTotales();
            }
        }

        private void txtSufijo_Leave(object sender, EventArgs e)
        {
            if (txtSufijo.Text == "") {
                txtSufijo.Text = "0";
            }
        }























    }//fin 
}
