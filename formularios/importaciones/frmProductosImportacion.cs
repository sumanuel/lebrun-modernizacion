using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using lebrun.clases.importaciones;
using lebrun.clasesData;
using lebrun.formularios.complementos;
using lebrun.clases.complementos;

namespace lebrun.formularios.importaciones
{
    public partial class frmProductosImportacion : Form
    {
        private DetallesOrdenDeCompra detallesImportaciones;
        public string varDocumento;
        public string varSufijo;
        public string moneda;
        public DataTable tablaInvMov;
        public DataTable arancel;
        private decimal total;
        private FuncionesTexbox textBox;
        private TextBox a;
        private int centinelaFila = -1;
        private frmConsultaArticulos productos;
        private string ValorArancel;
        private int items;
        private decimal totalExtrajera;


        public frmProductosImportacion()
        {
            InitializeComponent();
            detallesImportaciones = new DetallesOrdenDeCompra();
            tablaInvMov = new DataTable();
            textBox = new FuncionesTexbox();
            
            //llenarGrid();
        }


        public void llenarGrid(string documento) {

            DataTable tablaDetalles = new DataTable();
            if (gridProductos.Rows.Count>0)
            {
            gridProductos.Rows.Clear();
            }
            tablaDetalles = detallesImportaciones.detalleOrdenCompraArancel(documento);

            if (tablaDetalles.Rows.Count > 0)
            {
                txtCambio.Text = tablaDetalles.Rows[0]["ord_valcamb"].ToString();
                txtProveedor.Text = tablaDetalles.Rows[0]["ord_codigo"].ToString();
                txtNombrePro.Text = tablaDetalles.Rows[0]["pro_nombre"].ToString();
                txtRifPro.Text = tablaDetalles.Rows[0]["pro_rif"].ToString();
                varDocumento = tablaDetalles.Rows[0]["ord_documento"].ToString();
                varSufijo = tablaDetalles.Rows[0]["ord_sufijo"].ToString();
                moneda = tablaDetalles.Rows[0]["ord_moneda"].ToString();


            }
            else 
            { 
                MessageBox.Show("No existe ninguna orden de compra con ese numero","Algo no esta Bien",MessageBoxButtons.OK, MessageBoxIcon.Information); 
            }

            for (int i = 0; i < tablaDetalles.Rows.Count; i++)
            {
                gridProductos.Rows.Add();
                gridProductos.Rows[i].Cells[0].Value = tablaDetalles.Rows[i]["mov_codigo"].ToString();
                gridProductos.Rows[i].Cells[1].Value = tablaDetalles.Rows[i]["mov_memo"].ToString();
                gridProductos.Rows[i].Cells[2].Value = tablaDetalles.Rows[i]["mov_undmed"].ToString();
                gridProductos.Rows[i].Cells[3].Value = tablaDetalles.Rows[i]["mov_cant"].ToString();
                gridProductos.Rows[i].Cells[4].Value = tablaDetalles.Rows[i]["mov_costoorig"].ToString();//mov_precio
                if (tablaDetalles.Rows[i]["ara_porcen"].ToString() != "" || isNull(tablaDetalles.Rows[i]["ara_porcen"].ToString()))
                {
                    gridProductos.Rows[i].Cells[6].Value = tablaDetalles.Rows[i]["ara_porcen"].ToString();
                }
                else 
                {
                    gridProductos.Rows[i].Cells[6].Value = "No tiene asignado";
                }
                gridProductos.Rows[i].Cells[15].Value = tablaDetalles.Rows[i]["inv_arancel"].ToString();//mov_precio
            }
        }

        public void calcularC()
        {
            reemplazar2();

            total = 0;
            totalExtrajera = 0;
            for (int i = 0; i < gridProductos.Rows.Count; i++)
            {
                /*C*/

                
                gridProductos.Rows[i].Cells[5].Value = (Convert.ToDecimal(gridProductos.Rows[i].Cells[4].Value)) * (Convert.ToDecimal(gridProductos.Rows[i].Cells[3].Value));
                /*E*/
                gridProductos.Rows[i].Cells[7].Value = (Convert.ToDecimal(gridProductos.Rows[i].Cells[5].Value)) * (Convert.ToDecimal(txtCambio.Text));
                /*F*/
                gridProductos.Rows[i].Cells[8].Value = (Convert.ToDecimal(gridProductos.Rows[i].Cells[7].Value)) * (Convert.ToDecimal(factorCif.Text));//
                /*G*/
                gridProductos.Rows[i].Cells[9].Value = (((Convert.ToDecimal(gridProductos.Rows[i].Cells[8].Value)) * ((Convert.ToDecimal(factorX.Text))) + (Convert.ToDecimal(gridProductos.Rows[i].Cells[8].Value))));
                /*H*/
                gridProductos.Rows[i].Cells[10].Value = (Convert.ToDecimal(gridProductos.Rows[i].Cells[8].Value)) * (Convert.ToDecimal(factorGasto.Text));
                /*I*/
                gridProductos.Rows[i].Cells[11].Value = (Convert.ToDecimal(gridProductos.Rows[i].Cells[8].Value)) + (Convert.ToDecimal(gridProductos.Rows[i].Cells[10].Value));
                /*J*/
                gridProductos.Rows[i].Cells[12].Value = (((Convert.ToDecimal(gridProductos.Rows[i].Cells[6].Value)) * (Convert.ToDecimal(gridProductos.Rows[i].Cells[9].Value))) / 100);
                /*K*/
                gridProductos.Rows[i].Cells[13].Value = (Convert.ToDecimal(gridProductos.Rows[i].Cells[11].Value)) + (Convert.ToDecimal(gridProductos.Rows[i].Cells[12].Value));
                /*L*/
                gridProductos.Rows[i].Cells[14].Value = (Convert.ToDecimal(gridProductos.Rows[i].Cells[13].Value)) / (Convert.ToDecimal(gridProductos.Rows[i].Cells[3].Value));


                total= total + Convert.ToDecimal(gridProductos.Rows[i].Cells[13].Value);
                totalExtrajera = totalExtrajera + Convert.ToDecimal(gridProductos.Rows[i].Cells[5].Value);
            }
            totalArticulos();
            txtTotalProductos.Text = "" + total;
            txtMonedaExtr.Text = "" + totalExtrajera;
        }

        private bool isNull(string evaluar) {
            
            if (evaluar == null)
            {
                return true;
            }
            else { return false; }
        }

        public bool validarArancel() {

            bool vArancel=true;
            for (int i = 0; i < gridProductos.Rows.Count; i++) {

                if (gridProductos.Rows[i].Cells[6].Value.Equals("No tiene asignado"))
                {
                    vArancel =false;
                    return vArancel;
                }
                else {
                    vArancel= true;
                }
            }
            return vArancel;
        }

        public void armarDataTableInvMov() {

            

            tablaInvMov.Reset();
            tablaInvMov.Clear();

            tablaInvMov.Columns.Add("cencos");
            tablaInvMov.Columns.Add("codalm");
            tablaInvMov.Columns.Add("codcta");
            tablaInvMov.Columns.Add("codigo");
            tablaInvMov.Columns.Add("undmed");
            tablaInvMov.Columns.Add("codsuc");
            tablaInvMov.Columns.Add("codtra");
            tablaInvMov.Columns.Add("docum");
            tablaInvMov.Columns.Add("sufijo");
            tablaInvMov.Columns.Add("item");
            tablaInvMov.Columns.Add("tipodoc");
            tablaInvMov.Columns.Add("ivatipo");
            tablaInvMov.Columns.Add("tipo");
            tablaInvMov.Columns.Add("invact");
            tablaInvMov.Columns.Add("fecha");
            tablaInvMov.Columns.Add("fechaven");
            tablaInvMov.Columns.Add("hora");
            tablaInvMov.Columns.Add("contab");
            tablaInvMov.Columns.Add("fisico");
            tablaInvMov.Columns.Add("logico");
            tablaInvMov.Columns.Add("cdcom");
            tablaInvMov.Columns.Add("memo");
            tablaInvMov.Columns.Add("cant");
            tablaInvMov.Columns.Add("costo");
            tablaInvMov.Columns.Add("costoorigen");
            tablaInvMov.Columns.Add("cxund");
            tablaInvMov.Columns.Add("precio");
            tablaInvMov.Columns.Add("total");
            tablaInvMov.Columns.Add("arancel");
            tablaInvMov.Columns.Add("cosmon");
            tablaInvMov.Columns.Add("totalmon");

        }

        public DataTable armarContenidoArrayInv(string codProve, string doc, string suf)
        {

            armarDataTableInvMov();

            for (int i = 0; i < gridProductos.Rows.Count; i++)
            {
                items = items + 1;
                DataRow fila = tablaInvMov.NewRow();
                

                fila[0] = "0000000001";
                fila[1] = "000001";
                fila[2] = codProve;
                fila[3] = gridProductos.Rows[i].Cells[0].Value;//codigo
                fila[4] = gridProductos.Rows[i].Cells[2].Value;//unidad medida
                fila[5] = "000001";
                fila[6] = "E000";
                fila[7] = doc;
                fila[8] = suf;
                fila[9] = items;//items
                fila[10] = "35";//tipo doc
                fila[11] = "RD";
                fila[12] = "C";
                fila[13] = "1";
                fila[14] = "" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString();
                fila[15] = "" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString();
                fila[16] = "" + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString();//me da lala esta parte copy and paste                                          //hora
                fila[17] = "1";
                fila[18] = "1";
                fila[19] = "1";
                fila[20] = "1";
                fila[21] = gridProductos.Rows[i].Cells[1].Value;//descripcion articulo
                fila[22] = gridProductos.Rows[i].Cells[3].Value;//cantidad
                fila[23] = gridProductos.Rows[i].Cells[4].Value;//costo
                fila[24] = gridProductos.Rows[i].Cells[4].Value;//costo origen
                fila[25] = "1";
                fila[26] = gridProductos.Rows[i].Cells[4].Value;//costo
                fila[27] = "" + (Convert.ToDecimal(gridProductos.Rows[i].Cells[0].Value) * Convert.ToDecimal(gridProductos.Rows[i].Cells[0].Value));// cantidad x costo
                fila[28] = gridProductos.Rows[i].Cells[6].Value;//arancel
                fila[29] = gridProductos.Rows[i].Cells[4].Value;//costo
                fila[30] = "" + (Convert.ToDecimal(gridProductos.Rows[i].Cells[0].Value) * Convert.ToDecimal(gridProductos.Rows[i].Cells[0].Value));// cantidad x costo

                tablaInvMov.Rows.Add(fila);

            }

            return tablaInvMov;

        }


        public void reemplezar() {

            for (int i = 0; i < gridProductos.Rows.Count; i++) {

                gridProductos.Rows[i].Cells[3].Value = gridProductos.Rows[i].Cells[3].Value.ToString().Replace(",", ".");
                gridProductos.Rows[i].Cells[4].Value = gridProductos.Rows[i].Cells[4].Value.ToString().Replace(",", ".");
                gridProductos.Rows[i].Cells[5].Value = gridProductos.Rows[i].Cells[5].Value.ToString().Replace(",", ".");
                gridProductos.Rows[i].Cells[6].Value = gridProductos.Rows[i].Cells[6].Value.ToString().Replace(",", ".");
                gridProductos.Rows[i].Cells[7].Value = gridProductos.Rows[i].Cells[7].Value.ToString().Replace(",", ".");
                gridProductos.Rows[i].Cells[8].Value = gridProductos.Rows[i].Cells[8].Value.ToString().Replace(",", ".");
                gridProductos.Rows[i].Cells[9].Value = gridProductos.Rows[i].Cells[9].Value.ToString().Replace(",", ".");
                gridProductos.Rows[i].Cells[10].Value = gridProductos.Rows[i].Cells[10].Value.ToString().Replace(",", ".");
                gridProductos.Rows[i].Cells[11].Value = gridProductos.Rows[i].Cells[11].Value.ToString().Replace(",", ".");
                gridProductos.Rows[i].Cells[12].Value = gridProductos.Rows[i].Cells[12].Value.ToString().Replace(",", ".");
                gridProductos.Rows[i].Cells[13].Value = gridProductos.Rows[i].Cells[13].Value.ToString().Replace(",", ".");
                gridProductos.Rows[i].Cells[14].Value = gridProductos.Rows[i].Cells[14].Value.ToString().Replace(",", ".");
            }

        }

        public void reemplazar2() {

            for (int i = 0; i < gridProductos.Rows.Count; i++)
            {
                if (gridProductos.Rows[i].Cells[3].Value !=null)
                {
                    gridProductos.Rows[i].Cells[3].Value = gridProductos.Rows[i].Cells[3].Value.ToString().Replace(".", ",");
                }
                if (gridProductos.Rows[i].Cells[4].Value != null)
                {
                    gridProductos.Rows[i].Cells[4].Value = gridProductos.Rows[i].Cells[4].Value.ToString().Replace(".", ",");
                }
                if (gridProductos.Rows[i].Cells[5].Value!=null)
                {
                    gridProductos.Rows[i].Cells[5].Value = gridProductos.Rows[i].Cells[5].Value.ToString().Replace(".", ",");
                }
                if (gridProductos.Rows[i].Cells[6].Value != null)
                {
                    gridProductos.Rows[i].Cells[6].Value = gridProductos.Rows[i].Cells[6].Value.ToString().Replace(".", ",");
                }
                if (gridProductos.Rows[i].Cells[7].Value != null)
                {
                    gridProductos.Rows[i].Cells[7].Value = gridProductos.Rows[i].Cells[7].Value.ToString().Replace(".", ",");
                }
                if (gridProductos.Rows[i].Cells[8].Value != null)
                {
                    gridProductos.Rows[i].Cells[8].Value = gridProductos.Rows[i].Cells[8].Value.ToString().Replace(".", ",");
                }
                if (gridProductos.Rows[i].Cells[9].Value != null)
                {
                    gridProductos.Rows[i].Cells[9].Value = gridProductos.Rows[i].Cells[9].Value.ToString().Replace(".", ",");
                }
                if (gridProductos.Rows[i].Cells[10].Value != null)
                {
                    gridProductos.Rows[i].Cells[10].Value = gridProductos.Rows[i].Cells[10].Value.ToString().Replace(".", ",");
                }
                if (gridProductos.Rows[i].Cells[11].Value != null)
                {
                    gridProductos.Rows[i].Cells[11].Value = gridProductos.Rows[i].Cells[11].Value.ToString().Replace(".", ",");
                }
                if (gridProductos.Rows[i].Cells[12].Value != null)
                {
                    gridProductos.Rows[i].Cells[12].Value = gridProductos.Rows[i].Cells[12].Value.ToString().Replace(".", ",");
                }
                if (gridProductos.Rows[i].Cells[13].Value != null)
                {
                    gridProductos.Rows[i].Cells[13].Value = gridProductos.Rows[i].Cells[13].Value.ToString().Replace(".", ",");
                }
                if (gridProductos.Rows[i].Cells[14].Value != null)
                {
                    gridProductos.Rows[i].Cells[14].Value = gridProductos.Rows[i].Cells[14].Value.ToString().Replace(".", ",");
                }
            }
        }

        private void gridProductos_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            
            if (e.Control is TextBox) {
                
                a = e.Control as TextBox;
                a.Text = ""+gridProductos.CurrentCell.Value;
                a.KeyPress += OnlyNumbers_KeyPress;
                
            }
        }

        private void OnlyNumbers_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ',') { e.KeyChar='.';}

            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != 8 && e.KeyChar != 46) // Si no es numerico y si no es espacio
            {
                // Invalidar la accion
                e.Handled = true;

            }
            else
            {
                textBox.conDecimalesDefinidos(sender, e, a, 10);
            }
        }

        private void gridProductos_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (validarCamposGrid())
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

                    limpiarTextArticulos();
                //calcularTotales();

            }
        }

        public bool validarCamposGrid() 
        {
            if (txtPrecio.Text == "") { txtPrecio.Text = "0"; }
            if (txtCantidad.Text == "") { txtCantidad.Text = "0"; }

            txtCantidad.Text = "" + Convert.ToInt32(txtCantidad.Text);
            txtPrecio.Text = "" + Convert.ToDecimal(txtPrecio.Text);

            bool flag;
            flag = false;

            if (txtCodigo.Text != "")
            {
                flag = true;
            }
            else { flag = false; MessageBox.Show("Debe ingresar un articulo", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information); txtCodigo.Focus(); return false; }

            if (Convert.ToDecimal(txtCantidad.Text) != 0) 
            {
                flag = true;
            }
            else { flag = false; MessageBox.Show("La cantidad no debe ser igual cero (0)", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information); txtCantidad.Focus(); return false; }

            if (Convert.ToDecimal(txtPrecio.Text) != 0)
            {
                flag = true;
            }
            else { flag = false; MessageBox.Show("El costo no debe ser igual a cero (0)", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information); txtPrecio.Focus(); return false; }


           

            return flag;
        
        }

        private void agregarFila(int fila)
        {

            if (fila < 0)
            {
                
                gridProductos.Rows.Add();
                fila = gridProductos.Rows.Count - 1;
            }

            gridProductos.Rows[fila].Cells[0].Value = txtCodigo.Text;
            gridProductos.Rows[fila].Cells[1].Value = lblDescripcionPro.Text;
            gridProductos.Rows[fila].Cells[2].Value = lblunidadpro.Text;
            gridProductos.Rows[fila].Cells[3].Value = txtCantidad.Text;
            gridProductos.Rows[fila].Cells[4].Value = txtPrecio.Text;
            gridProductos.Rows[fila].Cells[6].Value = ValorArancel;


        
        }

        private void button2_Click(object sender, EventArgs e)
        {
            productos = frmConsultaArticulos.DefInstance;
            productos.destino(this,this.Name);
            productos.MdiParent = this.MdiParent;
            productos.ShowDialog();
            //productos.Show();
        }



        private void txtCodigo_Leave(object sender, EventArgs e)
        {
            if (txtCodigo.Text != "")
            {

                txtCodigo.Text = txtCodigo.Text.PadLeft(15, '0');

                arancel = detallesImportaciones.arancelArticuloEspecifico(txtCodigo.Text);

                if (arancel.Rows.Count > 0)
                {
                    lblDescripcionPro.Text = arancel.Rows[0]["inv_descri"].ToString();
                    lblunidadpro.Text = arancel.Rows[0]["ime_undmed"].ToString();
                    ValorArancel = arancel.Rows[0]["ara_porcen"].ToString();

                }
                else
                {
                    MessageBox.Show("El articulo no existe", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCodigo.Text = "";
                    lblDescripcionPro.Text = "...";
                    lblunidadpro.Text = "...";
                    ValorArancel = null;
                    txtCodigo.Focus();

                }
            }
        }

        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter)) {

                txtCantidad.Focus();
            }
        }

        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            textBox.OnlyNumbers(sender, e);

            if (e.KeyChar == (char)(Keys.Enter))
            {
                txtPrecio.Focus();
            }
        }

        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {

            TextBox a = (TextBox)sender;

            textBox.conDecimalesDefinidos(sender, e, a, 10);

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (gridProductos.Rows.Count > 0)
            {
                gridProductos.Rows.RemoveAt(gridProductos.CurrentRow.Index);
                totalArticulos();
                totalBs();
                totalMonedaExtrajera();
            }
        }

        private void limpiarTextArticulos() {

            txtCodigo.Text = "";
            lblDescripcionPro.Text = "...";
            lblunidadpro.Text = "...";
            txtCantidad.Text = "";
            txtPrecio.Text = "";

            txtCodigo.Focus();
        }

        public void totalArticulos() {
            txtCantProduc.Text = "0";

            for (int i = 0; i < gridProductos.Rows.Count; i++) {

                txtCantProduc.Text = "" + ((Convert.ToDecimal(txtCantProduc.Text.Replace(".", ","))) + (Convert.ToDecimal(gridProductos.Rows[i].Cells[3].Value.ToString().Replace(".", ","))));
            
            }
        }

        public void totalBs()
        {
            total = 0;
            for (int i = 0; i < gridProductos.Rows.Count; i++)
            {
                total = total + Convert.ToDecimal(gridProductos.Rows[i].Cells[13].Value);
            }
            txtTotalProductos.Text = "" + total;
        }

        public void totalMonedaExtrajera()
        {
            totalExtrajera = 0;
            for (int i = 0; i < gridProductos.Rows.Count; i++)
            {
                totalExtrajera = totalExtrajera + Convert.ToDecimal(gridProductos.Rows[i].Cells[5].Value);
            }
            txtMonedaExtr.Text = "" + totalExtrajera;
        }

        private void frmProductosImportacion_Load(object sender, EventArgs e)
        {
            totalArticulos();
            totalBs();
            totalMonedaExtrajera();
        }

        private void txtTotalProductos_TextChanged(object sender, EventArgs e)
        {
            TextBox a = (TextBox)sender;
            if (a.Text == "")
            {
                a.Text = "0,00000000";
            }

            a.Text = string.Format("{0:0.00000000}", Convert.ToDecimal(a.Text));
        }














    }
}
