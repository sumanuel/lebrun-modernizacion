using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using lebrun.clases.complementos;
using lebrun.clases.contabilidad;
using lebrun.clasesData;
using lebrun.Reportes;


using lebrun.formularios.contabilidad;

namespace lebrun.formularios.complementos
{
    public partial class frmPagare : Form
    {

        private proveedor cproveedores; 
        private TipoDocumentoProveedor tipoDocumento; 
        private lbxtipoDocPro lbxTipDocpro;
        private FuncionesTexbox funcionesTextBox;
        private AuxiliarContable au;      
        private lbxProveedores proveedores;
        private lbxAuxiliarContable cuentas;
        private lbxAuxiliaresOnly auxiliar;
        private DataTable datosGuardar;
        private DataTable datosGuardarSalpro;
        private DataTable cuenta;
        private pagare pagare;
        private UsuarioSistema usuarioActual;
        private Principal Principal;
        private lbxPlanCuentas planCuenta;

        private rptPagare reportePagare;
        private frmReporte frmReportePagare;

        private Compania empresaActual;
        private string cuentaContable;
        private string auxiliarContable;
        private decimal totalDebito=0;
        private decimal totalCredito=0;
        private decimal cuadre;
        private int centinelaFila = -1;
        public bool guard2;
        private int items;
        

        public frmPagare()
        {
            InitializeComponent();
            cproveedores = new proveedor();
            tipoDocumento = new TipoDocumentoProveedor();
            funcionesTextBox = new FuncionesTexbox();
            pagare = new pagare();
            datosGuardar = new DataTable();
            datosGuardarSalpro = new DataTable();
            //cuentas = new lbxAuxiliarContable();
            au = new AuxiliarContable();
            empresaActual = new Compania();
            empresaActual.obtenerCodigoCompaniaActual();
            usuarioActual = new UsuarioSistema();
            
        }

        private void btnBuscarProve_Click(object sender, EventArgs e)
        {
            proveedores = lbxProveedores.DefInstance;
            proveedores.destino(this, this.Name);
            proveedores.Show();
        }

        public void agregarFilas(int fila,DataGridView grid)
        {
           
            if (grid.Name.Equals("gridDocumentos")){
                if (fila < 0)
                {
                    grid.Rows.Add();
                    fila = grid.Rows.Count - 1;
                }

                grid.Rows[fila].Cells[0].Value = fila + 1;
                grid.Rows[fila].Cells[1].Value = txtProveedor.Text;
                grid.Rows[fila].Cells[2].Value = txtTipoDoc.Text;
                grid.Rows[fila].Cells[3].Value = txtDocumento.Text;
                grid.Rows[fila].Cells[4].Value = txtSufijo.Text;
                grid.Rows[fila].Cells[5].Value = txtMonto.Text;
                grid.Rows[fila].Cells[6].Value = PickerEmision.Value.Day + "-" + PickerEmision.Value.Month.ToString().PadLeft(2, '0') + "-" + PickerEmision.Value.Year;
                grid.Rows[fila].Cells[7].Value = PickerVencimiento.Value.Day + "-" + PickerVencimiento.Value.Month.ToString().PadLeft(2, '0') + "-" + PickerVencimiento.Value.Year;
                grid.Rows[fila].Cells[8].Value = PickerEmision.Value.Year + "-" + PickerEmision.Value.Month.ToString().PadLeft(2, '0') + "-" + PickerEmision.Value.Day;
                grid.Rows[fila].Cells[9].Value = PickerVencimiento.Value.Year + "-" + PickerVencimiento.Value.Month.ToString().PadLeft(2, '0') + "-" + PickerVencimiento.Value.Day;

                
                totalMovContab();
                totalDocumentos();

            }

            if (grid.Name.Equals("gridContabilidad"))
            {
                if (fila < 0)
                {
                    grid.Rows.Add();
                    fila = grid.Rows.Count - 1;
                }

                grid.Rows[fila].Cells[0].Value = fila + 1;
                grid.Rows[fila].Cells[1].Value = txtCuenta.Text;
                grid.Rows[fila].Cells[2].Value = lblDescripcionCuenta.Text;
                grid.Rows[fila].Cells[3].Value = txtAuxiliar.Text;
                

                if (radioDebito.Checked)
                {
                    grid.Rows[fila].Cells[4].Value = txtMontoConta.Text;
                    grid.Rows[fila].Cells[7].Value = "1";
                }
                if (radioCredito.Checked)
                {
                    grid.Rows[fila].Cells[5].Value = txtMontoConta.Text;
                    grid.Rows[fila].Cells[7].Value = "-1";
                }
                //grid.Rows[fila].Cells[6].Value = txtReferencia.Text;
                grid.Rows[fila].Cells[6].Value = gridDocumentos.Rows[0].Cells[3].Value;
                grid.Rows[fila].Cells[8].Value = txtDescripcion.Text;

            }
        }

        private void btnAgregarDoc_Click(object sender, EventArgs e)
        {

            if (validarCamposGrid())
            {
                if (documentoAgregado())
                {
                    totalMovContab();
                    totalDocumentos();
                }
                else
                {
                    if (centinelaFila >= 0)
                    {
                        agregarFilas(centinelaFila,gridDocumentos);
                        centinelaFila = -1;
                    }
                    else
                    {
                        agregarFilas(-1, gridDocumentos);
                    }
                    totalMovContab();
                    totalDocumentos();
                    clearDocumen();
                }
            }
        }

        private bool validarCamposGrid() {

            bool flag;
            flag = false;

            if (txtProveedor.Text != "")
            {
                flag = true;
            }
            else { flag = false; MessageBox.Show("Debe seleccionar un proveedor", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information); txtProveedor.Focus(); return false; }

            if (txtTipoDoc.Text != "")
            {
                flag = true;
            }
            else { flag = false; MessageBox.Show("Debe selecionar algun tipo de documento", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information); txtTipoDoc.Focus(); return false; }


            if (txtConcepto.Text != "")
            {
                flag = true;
            }
            else { flag = false; MessageBox.Show("Debe agregar un concepto al movimiento", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information); txtTipoDoc.Focus(); return false; }

            if (txtDocumento.Text != "")
            {
                flag = true;
            }
            else { flag = false; MessageBox.Show("Debe ingresar un numero de documento", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information); txtDocumento.Focus(); return false; }


            if (txtSufijo.Text != "")
            {
                flag = true;
            }
            else { flag = false; MessageBox.Show("Debe ingresar un sufijo para este documento", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information); txtSufijo.Focus(); return false; }

            if (txtMonto.Text != "")
            {
                flag = true;
            }
            else { flag = false; MessageBox.Show("Debe ingresar el monto del documento", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information); txtMonto.Focus(); return false; }

            return flag;

        }

        private bool validarCamposGridConta()
        {

            bool flag;
            flag = false;

            if (txtProveedor.Text != "")
            {
                flag = true;
            }
            else { flag = false; MessageBox.Show("Debe seleccionar un proveedor", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information); txtProveedor.Focus(); return false; }

            if (txtCuenta.Text!= "")
            {
                flag = true;
            }
            else { flag = false; MessageBox.Show("Debe seleccionar un numero de cuenta contable", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information); txtCuenta.Focus(); return false; }

            if (btnAuxiliar.Enabled == true)  // guard2
            {
                if (txtAuxiliar.Text == "")
                {

                    MessageBox.Show("Debe selecionar algun auxiliar", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information); return false;
                }
                //flag = true;
            }
            else { return true; }
            
            //else { flag = false; MessageBox.Show("Debe selecionar algun auxiliar", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information); return false; }

            //if (txtDescripcion.Text != "")
            //{
            //    flag = true;
            //}
            //else { flag = false; MessageBox.Show("Debe ingresar alguna descripcion para este movimiento", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information); txtDescripcion.Focus(); return false; }


            //if (txtReferencia.Text != "")
            //{
            //    flag = true;
            //}
            //else { flag = false; MessageBox.Show("Debe ingresar alguna referencia adicional para este movimiento", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information); txtReferencia.Focus(); return false; }

            if (txtMontoConta.Text != "")
            {
                flag = true;
            }
            else { flag = false; MessageBox.Show("Debe ingresar el monto para este movimiento", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information); txtMontoConta.Focus(); return false; }

            return flag;

        }

        private bool documentoAgregado() {


            for (int i = 0; i < gridDocumentos.Rows.Count; i++)
            {
                if ((gridDocumentos.Rows[i].Cells[3].Value.ToString() == txtDocumento.Text) && (gridDocumentos.Rows[i].Cells[4].Value.ToString() == txtSufijo.Text))
                {
                    MessageBox.Show("El numero de documento ya fue agregado, use un sufijo", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtSufijo.Focus();
                    return true;
                }
            }
            return false;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            eliminarItems(gridDocumentos);
        }

        private void actualizarNumeroItems(DataGridView grid)
        {
           
            for (int i = 0; i <grid.Rows.Count; i++)
            {
                grid.Rows[i].Cells[0].Value = i + 1;
            }
        }

        private void eliminarItems(DataGridView grid)
        {

            if (grid.Rows.Count > 0)
            {
                grid.Rows.RemoveAt(grid.CurrentRow.Index);
                actualizarNumeroItems(grid);
                totalMovContab();
                totalDocumentos();
            }
            totalMovContab();
            totalDocumentos();
        }

        private void btnAgregarConta_Click(object sender, EventArgs e)
        {
            if (validarCamposGridConta())
            {
                    if (centinelaFila >= 0)
                    {

                        agregarFilas(centinelaFila, gridContabilidad);
                        centinelaFila = -1;
                    }
                    else
                    {
                        agregarFilas(-1, gridContabilidad);
                    }
                    totalMovContab();
                    totalDocumentos();
                    clearMovConta();
                
            }

            totalMovContab();
            totalDocumentos();
        }

        private void btnEliminarConta_Click(object sender, EventArgs e)
        {
            eliminarItems(gridContabilidad);
            
           
        }

        private bool consultaRapidaTipoDoc() //TipoDoc
        {
            bool val2 = false;

            DataTable dtipoDoc = new DataTable();
            dtipoDoc = tipoDocumento.consultarDocumento(txtTipoDoc.Text);

            if (dtipoDoc.Rows.Count != 0)
            {
                txtTipoDoc.Text = dtipoDoc.Rows[0][0].ToString();
                lblDescripDoc.Text = dtipoDoc.Rows[0][1].ToString();
                consultarCuentaProveedor();
                txtConcepto.Focus();
                val2 = true;
            }
            else
            {
                MessageBox.Show("No existe documento con este codigo", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                val2 = false;
                txtTipoDoc.Text = "";
                lblDescripDoc.Text = "...";

            }
            return val2;

        }

        private bool consultaRapidaProveedor() //PROVEEDOR
        {
            bool val2 = false;

            DataTable dproveedor = new DataTable();
            dproveedor = cproveedores.consultarProveedorUnico(txtProveedor.Text);

            if (dproveedor.Rows.Count != 0)
            {

                lblNombreProveedor.Text = dproveedor.Rows[0][1].ToString();
                lblRif.Text = dproveedor.Rows[0][2].ToString();
                consultarCuentaProveedor();
                txtTipoDoc.Focus();
                val2 = true;
            }
            else
            {
                MessageBox.Show("No existe proveedor con este codigo", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                val2 = false;
                txtProveedor.Text = "";
                lblNombreProveedor.Text = "...";
                lblRif.Text = "...";

            }
            return val2;

        }

        private void txtProveedor_KeyPress(object sender, KeyPressEventArgs e)
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

                if (txtProveedor.Text != "")
                {
                    consultaRapidaProveedor();
                }
                else
                {
                    lblNombreProveedor.Text = "...";
                    lblRif.Text = "...";
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

        private void txtProveedor_Leave(object sender, EventArgs e)
        {
            if (txtProveedor.Text != "")
            {
                if (!consultaRapidaProveedor())
                {
                    txtProveedor.Focus();
                }
            }
            else 
            { 
                lblNombreProveedor.Text = "...";
                lblRif.Text = "...";
            }
        }

        private void btnCuenta_Click(object sender, EventArgs e)
        {
            //guard2 = false;
            //cuentas = lbxAuxiliarContable.DefInstance;
            //cuentas.soloVista(this, this.Name);
            //cuentas.Show();
            planCuenta = lbxPlanCuentas.DefInstance;
            planCuenta.soloVista(this, this.Name, empresaActual.BaseDatosActual);
            planCuenta.Show();
        }

        private void ingresandoDatos() {

        items = 0;

        datosGuardar.Columns.Add("item");           //0
        datosGuardar.Columns.Add("codprove");       //1
        datosGuardar.Columns.Add("numeroDoc");      //2
        datosGuardar.Columns.Add("sufiDoc");        //3
        datosGuardar.Columns.Add("tipoDoc");        //4
        datosGuardar.Columns.Add("numComprobante");//5
        datosGuardar.Columns.Add("descripcion");//6
        datosGuardar.Columns.Add("cuenta");     //7
        datosGuardar.Columns.Add("tipo");       //8
        datosGuardar.Columns.Add("baseTipo");   //9
        datosGuardar.Columns.Add("fecha");      //10
        datosGuardar.Columns.Add("hora");       //11
        datosGuardar.Columns.Add("monto");      //12
        datosGuardar.Columns.Add("status");     //13
        datosGuardar.Columns.Add("login");      //14
        datosGuardar.Columns.Add("idSistemas"); //15
        datosGuardar.Columns.Add("auxiliar");   //16
        datosGuardar.Columns.Add("rif");        //17
        datosGuardar.Columns.Add("nombre");     //18
        datosGuardar.Columns.Add("tipoLetra");     //19
        //datosGuardar.Columns.Add("fechaVenc");     //20


        datosGuardarSalpro.Columns.Add("item");           //0
        datosGuardarSalpro.Columns.Add("codprove");       //1
        datosGuardarSalpro.Columns.Add("numeroDoc");      //2
        datosGuardarSalpro.Columns.Add("sufiDoc");        //3
        datosGuardarSalpro.Columns.Add("tipoDoc");        //4
        datosGuardarSalpro.Columns.Add("numComprobante");//5
        datosGuardarSalpro.Columns.Add("descripcion");//6
        datosGuardarSalpro.Columns.Add("cuenta");     //7
        datosGuardarSalpro.Columns.Add("tipo");       //8
        datosGuardarSalpro.Columns.Add("baseTipo");   //9
        datosGuardarSalpro.Columns.Add("fecha");      //10
        datosGuardarSalpro.Columns.Add("hora");       //11
        datosGuardarSalpro.Columns.Add("monto");      //12
        datosGuardarSalpro.Columns.Add("status");     //13
        datosGuardarSalpro.Columns.Add("login");      //14
        datosGuardarSalpro.Columns.Add("idSistemas"); //15
        datosGuardarSalpro.Columns.Add("auxiliar");   //16
        datosGuardarSalpro.Columns.Add("rif");        //17
        datosGuardarSalpro.Columns.Add("nombre");     //18
        datosGuardarSalpro.Columns.Add("tipoLetra");     //19
        datosGuardarSalpro.Columns.Add("fechaVenc");     //20


   /////BLOQUE 1// barba vs bigotes

            for (int i = 0; i < gridDocumentos.Rows.Count; i++)
            {
                items = items + 1;
                DataRow fila = datosGuardar.NewRow();
                DataRow fila2 = datosGuardarSalpro.NewRow();


                fila[0] = items;
                fila[1] = txtProveedor.Text;
                fila[2] = gridDocumentos.Rows[i].Cells[3].Value.ToString();
                fila[3] = gridDocumentos.Rows[i].Cells[4].Value.ToString();
                fila[4] = gridDocumentos.Rows[i].Cells[2].Value.ToString();
                fila[5] = pagare.Comprobante;         //Buscar en parametros contables
                fila[6] = txtDescripcion.Text;            //crear variable
                fila[7] = cuentaContable;                //buscar en admtipdocpro2 /listo falta asignar

                if (Convert.ToDecimal(gridDocumentos.Rows[i].Cells[5].Value) > 0)
                {
                    fila[8] = "-1";                 //depende del monto
                    fila[19] = "C";
                }
                if (Convert.ToDecimal(gridDocumentos.Rows[i].Cells[5].Value) < 0)
                {
                    fila[8] = "1";                 //depende del monto
                    fila[19] = "D";
                }
                fila[9] = "";
                fila[10] = gridDocumentos.Rows[i].Cells[8].Value.ToString(); //fecha //aqui es 8
                fila[11] = "" + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString();//me da lala esta parte copy and paste                                          //hora
                fila[12] = gridDocumentos.Rows[i].Cells[5].Value.ToString().Replace(",",".");
                fila[13] = "0";
                fila[14] = Principal.usuarioActual.Id;               //se pasa por parametros
                fila[15] = "PAG";
                fila[16] = auxiliarContable;     //buscar en admtipdocpro2 'listo falta asignar
                fila[17] = lblRif.Text;
                fila[18] = lblNombreProveedor.Text;
                //fila[19] = gridDocumentos.Rows[i].Cells[9].Value.ToString();

                datosGuardar.Rows.Add(fila);

                fila2[0] = items;
                fila2[1] = txtProveedor.Text;
                fila2[2] = gridDocumentos.Rows[i].Cells[3].Value.ToString();
                fila2[3] = gridDocumentos.Rows[i].Cells[4].Value.ToString();
                fila2[4] = gridDocumentos.Rows[i].Cells[2].Value.ToString();
                fila2[5] = pagare.Comprobante;            //Buscar en parametros contables
                fila2[6] = txtDescripcion.Text;            //crear variable
                fila2[7] = cuentaContable;                //buscar en admtipdocpro2 /listo falta asignar

                if (Convert.ToDecimal(gridDocumentos.Rows[i].Cells[5].Value) > 0)
                {
                    fila2[8] = "-1";                 //depende del monto
                    fila[19] = "C";
                }
                if (Convert.ToDecimal(gridDocumentos.Rows[i].Cells[5].Value) < 0)
                {
                    fila2[8] = "1";                 //depende del monto
                    fila[19] = "D";
                }
                fila2[9] = "";
                //fila2[10] = gridDocumentos.Rows[i].Cells[6].Value.ToString(); //fecha
                fila2[10] = gridDocumentos.Rows[i].Cells[8].Value.ToString();
                fila2[11] = "" + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString();//me da lala esta parte copy and paste                                          //hora
                fila2[12] = gridDocumentos.Rows[i].Cells[5].Value.ToString().Replace(",", ".");
                fila2[13] = "0";
                fila2[14] = Principal.usuarioActual.Id;               //se pasa por parametros
                fila2[15] = "PAG";
                fila2[16] = auxiliarContable;     //buscar en admtipdocpro2 'listo falta asignar
                fila2[17] = lblRif.Text;
                fila2[18] = lblNombreProveedor.Text;
                fila2[20] = gridDocumentos.Rows[i].Cells[9].Value.ToString();

                datosGuardarSalpro.Rows.Add(fila2);

            }

            ///BLOQUE 2
            for (int i = 0 ; i < gridContabilidad.Rows.Count;i++)
            {
                items = items + 1;
                DataRow fila = datosGuardar.NewRow();

                fila[0] = items;
                fila[1] = txtProveedor.Text;
                fila[2] = gridContabilidad.Rows[i].Cells[6].Value.ToString();
                fila[3] = "0";
                fila[4] = gridDocumentos.Rows[0].Cells[2].Value.ToString();
                fila[5] = pagare.Comprobante;              //Buscar en parametros contables
                fila[6] = gridContabilidad.Rows[i].Cells[8].Value.ToString(); //txtDescripcion.Text;
                fila[7] = gridContabilidad.Rows[i].Cells[1].Value.ToString();
                fila[8] = gridContabilidad.Rows[i].Cells[7].Value.ToString();
                fila[9] = "";
                fila[10] = ""+DateTime.Now.Year.ToString()+"-"+DateTime.Now.Month.ToString()+"-"+DateTime.Now.Day.ToString();//me da lala esta parte
                fila[11] = ""+DateTime.Now.Hour.ToString()+":"+DateTime.Now.Minute.ToString()+":"+DateTime.Now.Second.ToString();//me da lala esta parte copy and paste
                if (gridContabilidad.Rows[i].Cells[7].Value.ToString().Equals("1"))
                {
                    fila[12] = gridContabilidad.Rows[i].Cells[4].Value.ToString().Replace(",", ".");
                    fila[19] = "D";
                }
                if (gridContabilidad.Rows[i].Cells[7].Value.ToString().Equals("-1"))
                {
                    fila[12] = gridContabilidad.Rows[i].Cells[5].Value.ToString().Replace(",", ".");
                    fila[19] = "C";
                }

                fila[13] = "0";
                fila[14] = Principal.usuarioActual.Id;     //se pasa por parametros
                fila[15] = "PAG";
                fila[16] = gridContabilidad.Rows[i].Cells[3].Value.ToString();
                fila[17] = "";
                fila[18] = lblNombreProveedor.Text;
                //fila[19] = gridDocumentos.Rows[i].Cells[9].Value.ToString();

                datosGuardar.Rows.Add(fila);
            }

        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            //if (pagare.verificarTipDocPro2(txtProveedor.Text, txtTipoDoc.Text))
            //{
                if (Convert.ToDecimal(txtTotalDoc.Text) > 0)
                {
                    if (cuadre == 0)
                    {
                        if (txtDescripcion.Text != "")
                        {

                            pagare.asignarNumeroComprobante(empresaActual.BaseDatosActual);
                            ingresandoDatos();
                            pagare.insertarMovContab(datosGuardar);
                            pagare.insertarSalpro(datosGuardarSalpro);
                            pagare.insertMoviContableD(datosGuardar, empresaActual.BaseDatosActual);
                            asignandoValores();
                            pagare.insertMoviContableC(empresaActual.BaseDatosActual, Principal.usuarioActual.Id);
                            imprimirReporte();

                            this.Close();

                        }
                        else
                        {
                            MessageBox.Show("Agregue alguna descripcion", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtDescripcion.Focus();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Valores descuadrados", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Ingrese algun movimiento", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            //}
            //else
            //{
            //    MessageBox.Show("Este tipo de documento no tiene cuenta en admtipdocpro2 ", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
        }

        private void button1_Click(object sender, EventArgs e)
        {
            lbxTipDocpro = lbxtipoDocPro.DefInstance;
            lbxTipDocpro.destino(this, this.Name);
            lbxTipDocpro.Show();
        }

        private void btnAuxiliar_Click(object sender, EventArgs e)
        {
            if (txtCuenta.Text != "")
            {
                auxiliar = lbxAuxiliaresOnly.DefInstance;
                auxiliar.destino(this, this.Name);
                auxiliar.codCuenta(txtCuenta.Text);
                auxiliar.Show();
                auxiliar.guardarCompania(empresaActual.BaseDatosActual);
                auxiliar.llenarAuxiliares();
            }
            else
            {
                MessageBox.Show("Debe seleccionar una cuenta antes de elegir un auxiliar", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void txtTipoDoc_KeyPress(object sender, KeyPressEventArgs e)
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

                if (txtTipoDoc.Text != "")
                {
                    consultaRapidaTipoDoc();
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

        private void consultarCuentaProveedor() {

            cuenta = cproveedores.consultarCuentaContable(txtProveedor.Text, txtTipoDoc.Text);
            if (cuenta.Rows.Count > 0)
            {

                cuentaContable = cuenta.Rows[0][0].ToString();
                auxiliarContable = cuenta.Rows[0][1].ToString();
            }
        }

        private void txtTipoDoc_Leave(object sender, EventArgs e)
        {
            if (txtTipoDoc.Text != "")
            {
                if (!consultaRapidaTipoDoc()) { 
                    txtTipoDoc.Focus();
                }
                else
                {
                    if (!pagare.verificarTipDocPro2(txtProveedor.Text, txtTipoDoc.Text))
                    {
                        MessageBox.Show("Este tipo de documento no tiene cuenta en admtipdocpro2 ", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtTipoDoc.Text = "";
                        lblDescripDoc.Text = "...";
                    }
                }
            }
            else
            {
                lblDescripDoc.Text = "...";
            }
        }

        private void txtDocumento_KeyPress(object sender, KeyPressEventArgs e)
        {
            funcionesTextBox.tab(sender, e);
        }

        private void txtSufijo_KeyPress(object sender, KeyPressEventArgs e)
        {
            funcionesTextBox.tab(sender, e);
        }

        private void txtMonto_KeyPress(object sender, KeyPressEventArgs e)
        {
            funcionesTextBox.only2Decimal(sender, e, txtMonto);
        }

        private void PickerEmision_KeyPress(object sender, KeyPressEventArgs e)
        {
            funcionesTextBox.tab(sender, e);
        }

        private void PickerVencimiento_KeyPress(object sender, KeyPressEventArgs e)
        {
            funcionesTextBox.tab(sender, e);
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

        private void txtAuxiliar_KeyPress(object sender, KeyPressEventArgs e)
        {
            funcionesTextBox.onlyNumberWithTab(sender, e);
        }

        private void txtDescripcion_KeyPress(object sender, KeyPressEventArgs e)
        {
            funcionesTextBox.tab(sender, e);
        }

        private void txtReferencia_KeyPress(object sender, KeyPressEventArgs e)
        {
            funcionesTextBox.tab(sender, e);
        }

        private void txtMontoConta_KeyPress(object sender, KeyPressEventArgs e)
        {
            funcionesTextBox.only2Decimal(sender, e, txtMontoConta);
        }


        private void buscarVerificarPlanC()
        {
            if (txtCuenta.Text != "")
            {
                au.CodigoPlanCuentas = txtCuenta.Text;

                if (au.validarPlanCuenta(10, empresaActual.BaseDatosActual))
                {
                    //txtNombreCuenta.Text = au.getDescripcionPlanC(au.CodigoPlanCuentas, "01");
                    if (au.poseeAuxilar(empresaActual.BaseDatosActual, au.CodigoPlanCuentas))
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

        private void totalDocumentos() {
            txtTotalDoc.Text="0,00";
            for (int i = 0; i < gridDocumentos.Rows.Count; i++)
            {
                txtTotalDoc.Text = "" + ((Convert.ToDecimal(txtTotalDoc.Text.Replace(".", ","))) + (Convert.ToDecimal(gridDocumentos.Rows[i].Cells[5].Value.ToString().Replace(".", ","))));
            }
            habilitarCampos();
        }


        private void totalMovContab() {

            totalDebito = 0;
            totalDebito = 0;
            for (int i = 0; i < gridContabilidad.Rows.Count; i++)
            {
                if (gridContabilidad.Rows[i].Cells[7].Value.ToString().Equals("1"))
                {
                    //txtTotalMov.Text = "" + ((Convert.ToDecimal(txtTotalMov.Text.Replace(".", ","))) + (Convert.ToDecimal(gridContabilidad.Rows[i].Cells[4].Value.ToString().Replace(".", ","))));
                    totalDebito = (totalDebito) + (Convert.ToDecimal(gridContabilidad.Rows[i].Cells[4].Value.ToString().Replace(".", ",")));
                }
                if (gridContabilidad.Rows[i].Cells[7].Value.ToString().Equals("-1"))
                {
                    //txtTotalMov.Text = "" + ((Convert.ToDecimal(txtTotalMov.Text.Replace(".", ","))) - (Convert.ToDecimal(gridContabilidad.Rows[i].Cells[5].Value.ToString().Replace(".", ","))));
                    totalCredito = (totalCredito) + (Convert.ToDecimal(gridContabilidad.Rows[i].Cells[5].Value.ToString().Replace(".", ",")));
                }
                
            }
            txtTotalMov.Text = "" + ((totalCredito) - (totalDebito));
            cuadre = (Convert.ToDecimal(txtTotalDoc.Text)) + (Convert.ToDecimal(txtTotalMov.Text));
            txtTotalMov.Text = "" + string.Format("{0:0.00}", Convert.ToDecimal(txtTotalMov.Text));
        }

        private void habilitarCampos() {

            if (Convert.ToDecimal(txtTotalDoc.Text) > 0)
            {
                txtDescripcion.Text = txtConcepto.Text;
                txtProveedor.Enabled = false;
                btnBuscarProve.Enabled = false;
                txtTipoDoc.Enabled = false;
                btnBuscarTipoDoc.Enabled = false;
                txtConcepto.Enabled = false;
            }
            else
            {

                txtProveedor.Enabled = true;
                btnBuscarProve.Enabled = true;
                txtTipoDoc.Enabled = true;
                btnBuscarTipoDoc.Enabled = true;
                txtConcepto.Enabled = true;
            }
        }

        private void clearMovConta() {

            txtCuenta.Text = "";
            txtMontoConta.Text = "";
            txtAuxiliar.Text = "";
            //txtDescripcion.Text= "";
            txtReferencia.Text = "";
            btnAuxiliar.Enabled = false;
            txtAuxiliar.ReadOnly = true;
            txtCuenta.Focus();
            
        }

        private void clearDocumen() {

            txtDocumento.Text = "";
            txtSufijo.Text = "0";
            txtMonto.Text = "";
            txtDocumento.Focus();
        }

        private void asignandoValores() {
            
            pagare.FechaEmision = ""+DateTime.Now.Year+"-"+DateTime.Now.Month+"-"+DateTime.Now.Day;
            pagare.IdSistemas = "PAG";
            pagare.Descripcion = txtDescripcion.Text;
            pagare.MontoDebito = txtTotalDoc.Text.Replace(",", ".");
            //pagare.MontoCredito =txtTotalMov.Text.Replace(",", ".");
            pagare.MontoCredito = Convert.ToString(Math.Abs(Convert.ToDecimal(txtTotalMov.Text))).Replace(",",".");
            pagare.Status = "1";
            pagare.Login = Principal.usuarioActual.Id;
            pagare.Hora = ""+DateTime.Now.Hour+":"+DateTime.Now.Minute+":"+DateTime.Now.Second;
        
        }

        private void ClearAllControls() {

            //foreach (Control t in this.Controls)
            //{
            //    if (t is TextBox)
            //        ((TextBox)t).Clear();
            //}

            foreach (Control t in this.groupBox1.Controls)
            {
                if (t is TextBox)
                    ((TextBox)t).Clear();
            }

            foreach (Control t in this.groupBox3.Controls)
            {
                if (t is TextBox)
                    ((TextBox)t).Clear();
            }
            txtProveedor.Enabled = true;
            txtTipoDoc.Enabled = true;
            btnBuscarProve.Enabled = true;
            btnBuscarTipoDoc.Enabled = true;
            txtTotalDoc.Text = "0,00";
            txtTotalMov.Text = "0,00";
            gridContabilidad.Rows.Clear();
            gridDocumentos.Rows.Clear();
            lblDescripcionCuenta.Text = "";
            lblRif.Text = "";
            lblNombreProveedor.Text = "";
            lblDescripDoc.Text = "";
            txtProveedor.Focus();
            
        }

        private void imprimirReporte()
        {

            frmReportePagare = new frmReporte("Pagare",pagare.Comprobante);
            frmReportePagare.Show();
            //reportePagare = new rptPagare();
            //reportePagare.SetDataSource(pagare.datosComprobante());
            ////reportePagare.SetParameterValue("NombreCompania", compania.Nombre);
            ////reportePagare.SetParameterValue("rifCompania", compania.Rif);
            //this.crystalReportViewer1.ReportSource = reportePagare;
            //crystalReportViewer1.Visible = true;

        }

        private void frmPagare_Load(object sender, EventArgs e)
        {
            string[] parametros;
            parametros = Environment.GetCommandLineArgs();
            if (parametros.Length > 1)
            {
                usuarioActual.Id = parametros[1];
                pagare.Login = usuarioActual.Id;
            }

            Principal = (Principal)this.MdiParent;
            //MessageBox.Show(""+Principal.usuarioActual.Id );
            //this.MdiParent = 
            //lebrun.Principal temp;
            //temp = (lebrun.Principal)this.MdiParent;
            //temp.usuarioActual.Id;
            
            //MessageBox.Show("" + Principal.usuarioActual.Id);
        }

        private void txtConcepto_KeyPress(object sender, KeyPressEventArgs e)
        {
            funcionesTextBox.tab(sender, e);
        }

    }//FRM
}
