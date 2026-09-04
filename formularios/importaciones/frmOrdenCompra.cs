using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
        private string fechaSistema;
        //private Principal Principal;
        private string usuarioOrigen;
        private string usuaLogin;

       


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
            //ordenCompra.Total = txtTotal1.Text.Replace(",", ".");
            ordenCompra.SubTotal = txtTotal.Text.Replace(",", ".");
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


            if (Convert.ToDecimal(txtDescuento1.Text) != 0 || Convert.ToDecimal(txtDescuento1.Text) != 0 || Convert.ToDecimal(txtDescuento1.Text) != 0)
            {

                if (Convert.ToDecimal(txtDescuento1.Text) != 0)
                {
                    desc = (Convert.ToDecimal(txtTotal.Text.Replace(".", ",")) * Convert.ToDecimal(txtDescuento1.Text.Replace(".", ","))) / 100;
                    txtTotal1.Text = ""+(Convert.ToDecimal(txtTotal.Text) - desc);
                    ordenCompra.Total = txtTotal1.Text.Replace(",", ".");

                }
                if (Convert.ToDecimal(txtDescuento2.Text) != 0)
                {
                    desc = (Convert.ToDecimal(txtTotal.Text.Replace(".", ",")) * Convert.ToDecimal(txtDescuento2.Text.Replace(".", ","))) / 100;
                    txtTotal1.Text = ""+(Convert.ToDecimal(txtTotal.Text) - desc);
                    ordenCompra.Total = txtTotal1.Text.Replace(",", ".");
                }

                if (Convert.ToDecimal(txtDescuento3.Text) != 0)
                {
                    desc = (Convert.ToDecimal(txtTotal.Text.Replace(".", ",")) * Convert.ToDecimal(txtDescuento3.Text.Replace(".", ","))) / 100;
                    txtTotal1.Text = ""+(Convert.ToDecimal(txtTotal.Text) - desc);
                    ordenCompra.Total = txtTotal1.Text.Replace(",", ".");
                }
            }
            else
            {
                ordenCompra.Total = txtTotal.Text.Replace(",", ".");
            }

            


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
            condicion = database.fDataTable("SELECT * FROM admcondpago ORDER BY conp_codigo ASC");
            cmbCond.DisplayMember = "conp_descripcion";
            cmbCond.ValueMember = "conp_cant_dias";
            cmbCond.DataSource = condicion;
        }

        public void cargarComboMoneda(string vmoneda) {

            //DataTable moneda = new DataTable();
            //moneda = database.fDataTable("SELECT MAX(histo_fecha),MAX(histo_hora),histo_moneda,mon_descri,histo_valor,mon_codigo FROM admhistodivi LEFT OUTER JOIN admmoneda ON mon_codigo=histo_moneda  GROUP BY histo_moneda ORDER BY  FIELD(mon_codigo,'"+vmoneda+"')DESC,histo_moneda ;");
            //cmbMoneda.DisplayMember = "mon_codigo";
            //cmbMoneda.ValueMember = "histo_valor";
            //cmbMoneda.DataSource = moneda;


            DataTable moneda = new DataTable();
            moneda = database.fDataTable("SELECT * FROM (SELECT * FROM admhistodivi ORDER BY histo_fecha DESC) valorMoneda GROUP BY histo_moneda ORDER BY FIELD(histo_moneda,'" + vmoneda + "')DESC;");
            cmbMoneda.DisplayMember = "histo_moneda";
            cmbMoneda.ValueMember = "histo_valor";
            cmbMoneda.DataSource = moneda;
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

            DataTable dproveedor = new DataTable();
            dproveedor = cproveedores.consultarProveedorUnico(txtCodProvee.Text);

            if (dproveedor.Rows.Count != 0)
             {
               cmbMoneda.DataSource = null;
               lblNombreProvee.Text = dproveedor.Rows[0][1].ToString();
               lblRif.Text = dproveedor.Rows[0][2].ToString();
               cmbMoneda.Text = dproveedor.Rows[0][3].ToString();
               cmbMoneda.ValueMember = dproveedor.Rows[0]["histo_valor"].ToString();
               txtValCamb.Text = cmbMoneda.ValueMember;
               txtDescuento1.Text = dproveedor.Rows[0]["pro_descuento"].ToString();
               txtDescuento2.Text = dproveedor.Rows[0]["pro_dcto2"].ToString();
               txtDescuento3.Text = dproveedor.Rows[0]["pro_dcto3"].ToString();
                
                
               val2 = true;
               
                 if ((Convert.ToInt32(txtCodProvee.Text))<8900000) {
                    cargarComboMoneda(cmbMoneda.Text);
                 }

                 }else {

                  MessageBox.Show("No existe ningun proveedor registrado con este codigo", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                  txtCodProvee.Text = "";
                  lblNombreProvee.Text = "...";
                  lblRif.Text = "...";
                  txtDescuento1.Text = "0,00";
                  txtDescuento2.Text = "0,00";
                  txtDescuento3.Text = "0,00";
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
            txtValCamb.Text = Convert.ToString(cmbMoneda.SelectedValue);
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

        private void calcularTotales() {
            txtTotal.Text = "0,00";
            txtUnidades.Text = "0,00";
            txtTotal1.Text="0,00";
            txtTotalCambio.Text = "0,00";

            for (int i = 0; i < gridProductos.Rows.Count; i++)
            {
                txtTotal.Text = ""+((Convert.ToDecimal(txtTotal.Text.Replace(".",","))) + (Convert.ToDecimal(gridProductos.Rows[i].Cells[18].Value.ToString().Replace(".",","))));

                txtItems.Text = "" + Convert.ToInt32(i+1);
                txtUnidades.Text = "" + ((Convert.ToDecimal(txtUnidades.Text.Replace(".", ","))) + (Convert.ToDecimal(gridProductos.Rows[i].Cells[4].Value.ToString().Replace(".", ","))));
                txtTotal1.Text = "" + ((Convert.ToDecimal(txtTotal1.Text.Replace(".", ","))) + (Convert.ToDecimal(gridProductos.Rows[i].Cells[9].Value.ToString().Replace(".", ","))));
                txtTotalCambio.Text=""+ (Convert.ToDecimal(txtValCamb.Text.Replace(".",","))*(Convert.ToDecimal(txtTotal.Text.Replace(".",","))));
            }
        }

        private bool mismoArticulo() {

            for (int i = 0; i < gridProductos.Rows.Count; i++)
            {
                if ((gridProductos.Rows[i].Cells[1].Value.ToString() == txtProducto.Text) && (gridProductos.Rows[i].Cells[3].Value.ToString() == txtUnidad.Text) && (gridProductos.Rows[i].Cells[6].Value.ToString()) == string.Format("{0:###,#####0.00##}",Decimal.Parse(txtDes1.Text)) && (gridProductos.Rows[i].Cells[7].Value.ToString()) == string.Format("{0:###,#####0.00##}",Decimal.Parse(txtDes2.Text)) && (gridProductos.Rows[i].Cells[8].Value.ToString()) == string.Format("{0:###,#####0.00##}",Decimal.Parse(txtDes2.Text)) && (gridProductos.Rows[i].Cells[5].Value.ToString()==string.Format("{0:###,#####0.00##}",Decimal.Parse(txtCosto.Text))))
                {

                    //gridProductos.Rows[i].Cells[4].Value = (Convert.ToInt32(gridProductos.Rows[i].Cells[4].Value.ToString()))+(Convert.ToInt32(txtCant.Text));
                    gridProductos.Rows[i].Cells[4].Value = (Convert.ToDecimal(gridProductos.Rows[i].Cells[4].Value.ToString())) + (Convert.ToDecimal(txtCant.Text));
                    gridProductos.Rows[i].Cells[9].Value = "" + Convert.ToString(Convert.ToDecimal(txtCosto.Text.Replace(".", ",")) * Convert.ToDecimal(gridProductos.Rows[i].Cells[4].Value.ToString().Replace(".", ","))).Replace(",", ".");

                    gridProductos.Rows[i].Cells[18].Value = (Convert.ToDecimal(gridProductos.Rows[i].Cells[17].Value.ToString().Replace(".", ","))) * (Convert.ToDecimal(gridProductos.Rows[i].Cells[4].Value.ToString().Replace(".", ",")));

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

        public void cargarModificar(string correlativo){

            ordenCompra = new OrdenDeCompra();
            ordenCompra.Correlativo = correlativo;
            ordenCompra.buscarOrdenCompra();
            mostrarDatosCabecera();
            detalleOrdenCompra = new DetallesOrdenDeCompra();
            detalleOrdenCompra.Documento = correlativo;
            mostrarDetalleOrden();
        }

        public void mostrarDatosCabecera()
        {

            txtOrdenNum.Text = ordenCompra.Correlativo;
            txtDocumento.Text = ordenCompra.Documento;
            txtSufijo.Text = ordenCompra.Sufijo;
            txtValCamb.Text = ordenCompra.Valcamb;
            txtConcepto.Text = ordenCompra.Concep;
            txtCodProvee.Text = ordenCompra.Codigo;
            cmbMoneda.Text = ordenCompra.Moneda;
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

        private void reemplazar() 
        {

            for (int i = 0; i < gridProductos.Rows.Count; i++)
            {
                gridProductos.Rows[i].Cells[4].Value = gridProductos.Rows[i].Cells[4].Value.ToString().Replace(",",".");
                gridProductos.Rows[i].Cells[5].Value = gridProductos.Rows[i].Cells[5].Value.ToString().Replace(",", ".");
                gridProductos.Rows[i].Cells[6].Value = gridProductos.Rows[i].Cells[6].Value.ToString().Replace(",", ".");
                gridProductos.Rows[i].Cells[7].Value = gridProductos.Rows[i].Cells[7].Value.ToString().Replace(",", ".");
                gridProductos.Rows[i].Cells[8].Value = gridProductos.Rows[i].Cells[8].Value.ToString().Replace(",", ".");
                gridProductos.Rows[i].Cells[9].Value = gridProductos.Rows[i].Cells[9].Value.ToString().Replace(",", ".");
                gridProductos.Rows[i].Cells[17].Value = gridProductos.Rows[i].Cells[17].Value.ToString().Replace(",", ".");
                gridProductos.Rows[i].Cells[18].Value = gridProductos.Rows[i].Cells[18].Value.ToString().Replace(",", ".");

                
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

                if (txtOrdenNum.Text == "")
                {
                    ordenCompra.asignarCorrelativo();
                }
                else
                {
                    ordenCompra.Correlativo = txtOrdenNum.Text;
                    detalleOrdenCompra.Documento = txtOrdenNum.Text;
                    ordenCompra.eliminarCabeceraOrdenCompra();
                    detalleOrdenCompra.eliminarDetallesOrdenCompra();
                }
                asignandoValores();
                ordenCompra.guardarOrdenCompra();
                reemplazar();
                detalleOrdenCompra.guardarDetallesOrdenDeCompra(gridProductos);

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
