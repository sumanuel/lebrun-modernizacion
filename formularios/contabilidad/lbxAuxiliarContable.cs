using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using lebrun.clases.contabilidad;
using lebrun.formularios.complementos;
using lebrun.formularios.importaciones;
using lebrun.formularios.compras;
using lebrun.formularios.clientesMayor;


namespace lebrun.formularios.contabilidad{

    public partial class lbxAuxiliarContable : Form
    {
        private PlanCuentas pcBuscado;
        private AuxiliarContable auxiliarPlanC;
        private frmServicios refServicio;
        private string compania;
        private string retorno;
        private DataTable dtAxuiliar;
        private Principal referenciaPrincipal;
        public frmAuxiliarContable fmAu;
        private DataView dv;
        private static lbxAuxiliarContable m_FormDefInstance;
        private frmComprobanteContable refComprobanteContable;
        private frmPagare refPagare;
        private string boton1;
        private frmFacturaNacionalImportacion refFacturaNacional;
        private frmCruceDeSaldo refCruceDeSaldo;
        private frmProcesoPagoManual refProcesoPagoManual;
        private frmClientesMayor referenciaClientesMayor;

        /*fuente es un variable centinela que me sirve para saber que formulario lo llamo
         * por los momentos 0 es por defecto 
         * y 1 si lo llama frmcomprobanteContable
         * */
        public int fuente = 0;

        public lbxAuxiliarContable()
        {
            InitializeComponent();
        }
        public void cargarLbx() 
        {
            if (fuente == 0)
            {

                referenciaPrincipal = (Principal)this.MdiParent;
                auxiliarPlanC = new AuxiliarContable();
                dtAxuiliar = new DataTable();
                dtAxuiliar = auxiliarPlanC.lbxAuxiliar(referenciaPrincipal.empresaActual.BaseDatosActual);
                for (int i = 0; i < dtAxuiliar.Rows.Count; i++)
                {
                    dtAxuiliar.Rows[i]["codigo"] = auxiliarPlanC.cargarCodigo(dtAxuiliar.Rows[i]["codigo"].ToString());
                }
                dataGridView1.DataSource = dtAxuiliar;
                dv = new DataView(dtAxuiliar);
                dataGridView1.Columns[2].Visible = false;
                dataGridView1.Columns[3].Visible = false;
                dataGridView2.DataSource = null;
            }


            if (fuente == 1)
            {

                referenciaPrincipal = (Principal)this.MdiParent;
                auxiliarPlanC = new AuxiliarContable();
                
                dtAxuiliar = auxiliarPlanC.lbxAuxiliar(referenciaPrincipal.empresaActual.BaseDatosActual);
                for (int i = 0; i < dtAxuiliar.Rows.Count; i++)
                {
                    dtAxuiliar.Rows[i]["codigo"] = auxiliarPlanC.cargarCodigo(dtAxuiliar.Rows[i]["codigo"].ToString());
                }
                dataGridView1.DataSource = dtAxuiliar;
                dv = new DataView(dtAxuiliar);
                dataGridView1.Columns[2].Visible = false;
                dataGridView1.Columns[3].Visible = false;
                dataGridView2.DataSource = null;
            }
            if (fuente == 2)
            {
                auxiliarPlanC = new AuxiliarContable();
                dtAxuiliar = new DataTable();
                dtAxuiliar = auxiliarPlanC.lbxAuxiliar("01");
                for (int i = 0; i < dtAxuiliar.Rows.Count; i++)
                {
                    dtAxuiliar.Rows[i]["codigo"] = auxiliarPlanC.cargarCodigo(dtAxuiliar.Rows[i]["codigo"].ToString());
                }
                dataGridView1.DataSource = dtAxuiliar;
                dv = new DataView(dtAxuiliar);
                dataGridView1.Columns[2].Visible = false;
                dataGridView1.Columns[3].Visible = false;
                dataGridView2.DataSource = null;
            }
            if (fuente == 3)
            {

                auxiliarPlanC = new AuxiliarContable();
                dtAxuiliar = new DataTable();
                dtAxuiliar = auxiliarPlanC.lbxAuxiliar(compania);
                for (int i = 0; i < dtAxuiliar.Rows.Count; i++)
                {
                    dtAxuiliar.Rows[i]["codigo"] = auxiliarPlanC.cargarCodigo(dtAxuiliar.Rows[i]["codigo"].ToString());
                }
                dataGridView1.DataSource = dtAxuiliar;
                dv = new DataView(dtAxuiliar);
                dataGridView1.Columns[2].Visible = false;
                dataGridView1.Columns[3].Visible = false;
                dataGridView2.DataSource = null;
            }
            if (fuente == 4)
            {

                auxiliarPlanC = new AuxiliarContable();
                dtAxuiliar = new DataTable();
                dtAxuiliar = auxiliarPlanC.lbxAuxiliar(compania);
                for (int i = 0; i < dtAxuiliar.Rows.Count; i++)
                {
                    dtAxuiliar.Rows[i]["codigo"] = auxiliarPlanC.cargarCodigo(dtAxuiliar.Rows[i]["codigo"].ToString());
                }
                dataGridView1.DataSource = dtAxuiliar;
                dv = new DataView(dtAxuiliar);
                dataGridView1.Columns[2].Visible = false;
                dataGridView1.Columns[3].Visible = false;
                dataGridView2.DataSource = null;
            }
        }
        private void lbxAuxiliarContable_Load(object sender, EventArgs e)
        {
            pcBuscado = new PlanCuentas();
            referenciaPrincipal = (Principal)this.MdiParent;
            cargarLbx();
            /*
            * 01-05-2012
            * Importante: Debido a la sugerencia del Lic Orochena, de colocar 
            * las compañias que NO se deben ver, en la base de datos se esta guardando
            * en formato 01-02-03 donde 01,02,03 es el codigo de la compañia definido
            * en sysconta en la tabla confdatos empresa, las compañias que SI van.
             * 
             * Para el auxiliar contable tener en cuenta las compañias que SI puede ver
             * el padre y no mostrar las que NO puede ver el padre
          */

            /*
             * 08:25 a.m. 18/12/2012 
             * agregando caso fuente = 0
             * Roberto Fernández V.
             */
           

        }

        public void cambiarBaseDatos(string id) {
            
            //this.referenciaPrincipal.empresaActual.BaseDatosActual = id;
        }


        public static lbxAuxiliarContable DefInstance
        {
            get
            {
                if (m_FormDefInstance == null || m_FormDefInstance.IsDisposed)
                    m_FormDefInstance = new lbxAuxiliarContable();
                return m_FormDefInstance;
            }
            set
            {
                m_FormDefInstance = value;
            }
        }

        public void lbxComprobante1(bool val, string referenciaPrincipal1)
        {
            this.dataGridView1.DataSource = null;
            this.dataGridView2.DataSource = null;
            auxiliarPlanC = new AuxiliarContable();
            dtAxuiliar = new DataTable();
            dtAxuiliar.Clear();
            dtAxuiliar = auxiliarPlanC.lbxAuxiliarMayor(referenciaPrincipal1);
            for (int i = 0; i < dtAxuiliar.Rows.Count; i++)
            {
                dtAxuiliar.Rows[i]["codigo"] = auxiliarPlanC.cargarCodigo(dtAxuiliar.Rows[i]["codigo"].ToString());
            }
            dataGridView1.DataSource = dtAxuiliar;
            dv = new DataView(dtAxuiliar);
            dataGridView1.Columns[2].Visible = false;
            dataGridView2.DataSource = null;
        }
        public void lbxComprobante(bool val, Principal referenciaPrincipal1)
        {
            this.dataGridView1.DataSource = null;
            this.dataGridView2.DataSource = null;
            auxiliarPlanC = new AuxiliarContable();
            dtAxuiliar = new DataTable();
            dtAxuiliar.Clear();
            referenciaPrincipal = referenciaPrincipal1;
            dtAxuiliar = auxiliarPlanC.lbxAuxiliarMayor(referenciaPrincipal.empresaActual.BaseDatosActual);
            for (int i = 0; i < dtAxuiliar.Rows.Count; i++)
            {
                dtAxuiliar.Rows[i]["codigo"] = auxiliarPlanC.cargarCodigo(dtAxuiliar.Rows[i]["codigo"].ToString());
            }
            dataGridView1.DataSource = dtAxuiliar;
            dv = new DataView(dtAxuiliar);
            dataGridView1.Columns[2].Visible = false;
            dataGridView2.DataSource = null;
        }
        

        private void cmdAgregar_Click(object sender, EventArgs e)
        {
            string controlPlanc = null;
            if (dataGridView1.Rows.Count > 0)
            {
                if (auxiliarPlanC.verificarManejaAuxiliar(referenciaPrincipal.empresaActual.BaseDatosActual, dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["cc_CodigoCuenta"].Value.ToString()))
                {   controlPlanc = auxiliarPlanC.getControl(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["cc_CodigoCuenta"].Value.ToString(), referenciaPrincipal.empresaActual.BaseDatosActual);
                    if ( controlPlanc != null)
                    {
                        frmAuxiliarContable.DefInstance.nuevoAuxiliar.limpiarObjetAux();
                        frmAuxiliarContable.DefInstance.nuevoAuxiliar.CodigoPlanCuentas = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["cc_CodigoCuenta"].Value.ToString();
                        frmAuxiliarContable.DefInstance.nuevoAuxiliar.Auxiliar1 = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["codigo"].Value.ToString();
                        //para los permisos de los auxiliares
                        frmAuxiliarContable.DefInstance.nuevoAuxiliar.Control =   dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["cc_control"].Value.ToString();
                        frmAuxiliarContable.DefInstance.MdiParent = referenciaPrincipal;
                        frmAuxiliarContable.DefInstance.Show();
                        auxiliarPlanC.limpiarObjetAux();
                        referenciaPrincipal.toolStripComboBox1.Enabled = true;
                    }
                    else {
                        MessageBox.Show("El Plan de cuenta debe tener asignado la compañia donde se debe usar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("El Plan de cuenta debe Manejar Auxiliar!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else { 
                MessageBox.Show("Debe Crear algun mayor que maneje Auxiliar!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); 
            }
        }

        private void cmdBotonModificar_Click(object sender, EventArgs e)
        {
            if (dataGridView2.Rows.Count>=2)
            {
                auxiliarPlanC.NombreAuxiliar = this.dataGridView2.CurrentRow.Cells["admAC_descripcion"].Value.ToString();
                auxiliarPlanC.CodigoAuxiliar = this.dataGridView2.CurrentRow.Cells["admAC_codigoAuxiliarContable"].Value.ToString();
                auxiliarPlanC.ManejaDocumentos = Convert.ToBoolean(this.dataGridView2.CurrentRow.Cells["admAC_manejaDocumentos"].Value);
                auxiliarPlanC.CodigoPlanCuentas = this.dataGridView1.CurrentRow.Cells["cc_CodigoCuenta"].Value.ToString();             
                auxiliarPlanC.Auxiliar1 = this.dataGridView1.CurrentRow.Cells["codigo"].Value.ToString();
                auxiliarPlanC.MontoApertura = this.dataGridView2.CurrentRow.Cells["admAC_saldoApertura"].Value.ToString().Replace(",", ".");
                //para los permisos de los auxiliares
                auxiliarPlanC.Control = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["cc_control"].Value.ToString();
                auxiliarPlanC.ControAux = this.dataGridView2.CurrentRow.Cells["admAC_control"].Value.ToString();

                fmAu = frmAuxiliarContable.DefInstance;
                fmAu.MdiParent = referenciaPrincipal;
                fmAu.cargarDatos(auxiliarPlanC, this);
                fmAu.Show();
                fmAu.loadCompaniasHabilitadas();
                fmAu.cargarDatos(auxiliarPlanC, this);
                //auxiliarPlanC.limpiarObjetAux();
                referenciaPrincipal.toolStripComboBox1.Enabled = false;
                    
            }
            else {
                 MessageBox.Show("Debe Seleccionar un registro con  datos!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            mostrarDetalle();
            if (fuente == 1) {
                refComprobanteContable.txtPlanCuentas.Text = dataGridView1.CurrentRow.Cells["cc_CodigoCuenta"].Value.ToString();
                refComprobanteContable.txtNombreCuenta.Text = dataGridView1.CurrentRow.Cells["Descripcion"].Value.ToString();
                refComprobanteContable.txtPlanCuentas.Focus();
                //dataGridView2
                if (dataGridView2.Rows.Count > 1)
                {
                    if (dataGridView2.CurrentRow.Cells[1].Value.ToString() != "")
                    {
                        refComprobanteContable.banderaAuxiliar(true);
                    }
                    else
                    {
                        refComprobanteContable.banderaAuxiliar(false);
                    }
                }
                else {
                    refComprobanteContable.banderaAuxiliar(false);
                }
                this.Close();
            }

            if (fuente == 2) {

                refPagare.txtCuenta.Text = dataGridView1.CurrentRow.Cells["cc_CodigoCuenta"].Value.ToString();
                refPagare.lblDescripcionCuenta.Text = dataGridView1.CurrentRow.Cells["Descripcion"].Value.ToString();
                refPagare.txtAuxiliar.Text = "";

                if (dataGridView2.Rows.Count > 1) {
                    refPagare.guard2 = true;
                }
                else{
                    refPagare.guard2 = false;
                }

                //refPagare.guard2 = false;
                refPagare.txtCuenta.Focus();
                this.Close();
            }
            if (fuente == 3)
            {

                refServicio.txtCuenta.Text = dataGridView1.CurrentRow.Cells["cc_CodigoCuenta"].Value.ToString();
                refServicio.txtAuxiliar.Text = "";
                refServicio.txtCuenta.Focus();
                this.Close();
            }
            if (fuente == 4)
            {

                refCruceDeSaldo.txtCuenta.Text = dataGridView1.CurrentRow.Cells["cc_CodigoCuenta"].Value.ToString();
                refCruceDeSaldo.txtAuxiliar.Text = "";
                refCruceDeSaldo.txtCuenta.Focus();
                this.Close();
            }
            if (fuente == 5)
            {
                
                refProcesoPagoManual.txtCuenta.Text = dataGridView1.CurrentRow.Cells["cc_CodigoCuenta"].Value.ToString();
                refProcesoPagoManual.txtAuxiliar.Text = "";
                refProcesoPagoManual.txtCuenta.Focus();
                this.Close();
            }

            //cliente mayor
            if (fuente == 6)
            {
                referenciaClientesMayor.txtPlanCuentas.Text = dataGridView1.CurrentRow.Cells["cc_CodigoCuenta"].Value.ToString();
                referenciaClientesMayor.txtAuxiliar.Text = "";
                referenciaClientesMayor.txtPlanCuentas_KeyPress(new object(), new KeyPressEventArgs((char)(Keys.Enter)));
                this.Close();
            }

        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            mostrarDetalle();
        }
        
        public void mostrarDetalle() {
            // cambiado 08:47 a.m. 18/12/2012 Roberto F.
            if (fuente == 1 || fuente ==0)
            {
                DataTable dtAu;
                dataGridView2.DataSource = null;
                dataGridView2.Update();
                dtAu = auxiliarPlanC.detalleAuxiliar(this.dataGridView1.CurrentRow.Cells["cc_CodigoCuenta"].Value.ToString(), referenciaPrincipal.empresaActual.BaseDatosActual);
                dataGridView2.DataSource = dtAu;
                dataGridView2.Update();
                dataGridView2.Columns[0].HeaderText = "Código";
                dataGridView2.Columns[1].HeaderText = "Nombre";
                dataGridView2.Columns[2].Visible = false;
                dataGridView2.Columns[3].HeaderText = "Saldo Apertura";
                dataGridView2.Columns[4].Visible = false;
            }

            if (fuente == 2) {
                DataTable dtAu;
                dataGridView2.DataSource = null;
                dataGridView2.Update();
                dtAu = auxiliarPlanC.detalleAuxiliar(this.dataGridView1.CurrentRow.Cells["cc_CodigoCuenta"].Value.ToString(),"01");
                dataGridView2.DataSource = dtAu;
                dataGridView2.Update();
                dataGridView2.Columns[0].HeaderText = "Código";
                dataGridView2.Columns[1].HeaderText = "Nombre";
                dataGridView2.Columns[2].Visible = false;
                dataGridView2.Columns[3].HeaderText = "Saldo Apertura";
                dataGridView2.Columns[4].Visible = false;
            }
            if (fuente == 3)
            {
                
                DataTable dtAu;
                dataGridView2.DataSource = null;
                dataGridView2.Update();
                dtAu = auxiliarPlanC.detalleAuxiliar(this.dataGridView1.CurrentRow.Cells["cc_CodigoCuenta"].Value.ToString(), compania);
                dataGridView2.DataSource = dtAu;
                dataGridView2.Update();
                dataGridView2.Columns[0].HeaderText = "Código";
                dataGridView2.Columns[1].HeaderText = "Nombre";
                dataGridView2.Columns[2].Visible = false;
                dataGridView2.Columns[3].HeaderText = "Saldo Apertura";
                dataGridView2.Columns[4].Visible = false;
            }
            if (fuente == 4 || fuente == 5 || fuente == 6)
            {
                DataTable dtAu;
                dataGridView2.DataSource = null;
                dataGridView2.Update();
                dtAu = auxiliarPlanC.detalleAuxiliar(this.dataGridView1.CurrentRow.Cells["cc_CodigoCuenta"].Value.ToString(), compania);
                dataGridView2.DataSource = dtAu;
                dataGridView2.Update();
                dataGridView2.Columns[0].HeaderText = "Código";
                dataGridView2.Columns[1].HeaderText = "Nombre";
                dataGridView2.Columns[2].Visible = false;
                dataGridView2.Columns[3].HeaderText = "Saldo Apertura";
                dataGridView2.Columns[4].Visible = false;
            }
        }

        private void cmdVer_Click(object sender, EventArgs e)
        {
            if (dataGridView2.Rows.Count >= 2)
            {
                auxiliarPlanC.NombreAuxiliar = this.dataGridView2.CurrentRow.Cells["admAC_descripcion"].Value.ToString();
                auxiliarPlanC.CodigoAuxiliar = this.dataGridView2.CurrentRow.Cells["admAC_codigoAuxiliarContable"].Value.ToString();
                auxiliarPlanC.MontoApertura  = this.dataGridView2.CurrentRow.Cells["admAC_saldoApertura"].Value.ToString();
                auxiliarPlanC.ManejaDocumentos = Convert.ToBoolean(this.dataGridView2.CurrentRow.Cells["admAC_manejaDocumentos"].Value);
                auxiliarPlanC.CodigoPlanCuentas = this.dataGridView1.CurrentRow.Cells["cc_CodigoCuenta"].Value.ToString();
                auxiliarPlanC.Auxiliar1 = this.dataGridView1.CurrentRow.Cells["codigo"].Value.ToString();
                //para los permisos de los auxiliares
                auxiliarPlanC.Control = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["cc_control"].Value.ToString();
                auxiliarPlanC.ControAux = this.dataGridView2.CurrentRow.Cells["admAC_control"].Value.ToString();

                fmAu = frmAuxiliarContable.DefInstance;
                fmAu.mostrarDatos(auxiliarPlanC);
                fmAu.MdiParent = referenciaPrincipal;
                fmAu.Show();
                fmAu.mostrarDatos(auxiliarPlanC);
                fmAu.loadCompaniasHabilitadas();
                referenciaPrincipal.toolStripComboBox1.Enabled = false;
            }
            else {
                MessageBox.Show("Debe Seleccionar un registro con  datos!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmdBuscar_Click(object sender, EventArgs e)
        {   
            string sentencia;
            sentencia = "codigo LIKE '%" + txtBuscar.Text + "%' OR Descripcion LIKE '%" + txtBuscar.Text + "%'";
            dv.RowFilter = sentencia;
            if (dv.Count == 0)
            {

                //cambiado agregado fuente 0 08:47 a.m. 18/12/2012 Roberto F.
                if (fuente == 1 || fuente ==0)
                {
                    dtAxuiliar = auxiliarPlanC.lbxAuxiliarBaseDatos(txtBuscar.Text, referenciaPrincipal.empresaActual.BaseDatosActual);
                }

                if (fuente == 2)
                {
                    dtAxuiliar = auxiliarPlanC.lbxAuxiliarBaseDatos(txtBuscar.Text, "01");
                }

                if (dtAxuiliar.Rows.Count == 0)
                {
                    MessageBox.Show("No se consiguieron Resultados!!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else {
                    dataGridView1.DataSource = null;
                    for (int i = 0; i < dtAxuiliar.Rows.Count; i++)
                    {
                        dtAxuiliar.Rows[i]["codigo"] = auxiliarPlanC.cargarCodigo(dtAxuiliar.Rows[i]["codigo"].ToString());
                    }
                    dataGridView1.DataSource = dtAxuiliar;
                    dataGridView1.Update();
                }
            }
            else
            {
                dataGridView1.DataSource = dv;
                dataGridView1.Update();
            }
        }

        private void cmdLimpiarBusqueda_Click(object sender, EventArgs e)
        {
            txtBuscar.Text = "";
            dataGridView1.DataSource = null;
            if (!cmdAgregar.Visible)
            {
                if (fuente == 1)
                {
                    dtAxuiliar = auxiliarPlanC.lbxAuxiliarMayor(referenciaPrincipal.empresaActual.BaseDatosActual);
                }

                if (fuente == 2)
                {
                    dtAxuiliar = auxiliarPlanC.lbxAuxiliarMayor("01");
                }
                for (int i = 0; i < dtAxuiliar.Rows.Count; i++)
                {
                    dtAxuiliar.Rows[i]["codigo"] = auxiliarPlanC.cargarCodigo(dtAxuiliar.Rows[i]["codigo"].ToString());
                }
                dataGridView1.DataSource = dtAxuiliar;
                dataGridView1.Update();
            }
            else
            {
                dtAxuiliar = auxiliarPlanC.lbxAuxiliar(referenciaPrincipal.empresaActual.BaseDatosActual);
                for (int i = 0; i < dtAxuiliar.Rows.Count; i++)
                {
                    dtAxuiliar.Rows[i]["codigo"] = auxiliarPlanC.cargarCodigo(dtAxuiliar.Rows[i]["codigo"].ToString());
                }
                dataGridView1.DataSource = dtAxuiliar;
                dataGridView1.Update();
            }
            dataGridView1.Columns[2].Visible = false;
        }

        private void txtBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmdBuscar.Focus();
            }
        }


        public void soloVista(frmComprobanteContable referencia) { 
            cmdVer.Visible = false;
            cmdAgregar.Visible = false;
            cmdBotonModificar.Visible = false;
            fuente = 1;
            refComprobanteContable = referencia;
        }

        public void soloVista(Form referencia, string opcion)
        {
            cmdVer.Visible = false;
            cmdAgregar.Visible = false;
            cmdBotonModificar.Visible = false;
            if (opcion.Equals("frmPagare"))
            {
                fuente = 2;
                refPagare = (frmPagare)referencia;
            }

            if (opcion.Equals("frmClientesMayor"))
            {
                fuente = 6;
                referenciaClientesMayor = (frmClientesMayor)referencia;
            }
        }

        public void soloVista(Form referencia, string opcion, string boton)
        {
            if (opcion.Equals("frmFacturaNacionalImportacion"))
            {
                fuente = 3;
                boton1 = boton;
                refFacturaNacional = (frmFacturaNacionalImportacion)referencia;
            }
            if (opcion.Equals("frmServicios"))
            {
                fuente = 3;
                compania = boton;
                refServicio = (frmServicios)referencia;
                //retorno = "frmServicios";
            }
            if (opcion.Equals("Compras Otras Transacciones Cruce de Saldo"))
            {
                fuente = 4;
                compania = boton;
                refCruceDeSaldo = (frmCruceDeSaldo)referencia;
                //retorno = "frmServicios";
            }
            if (opcion.Equals("Compras Otras Transacciones Procesar Pagos Manuales"))
            {
                fuente = 5;
                compania = boton;
                refProcesoPagoManual = (frmProcesoPagoManual)referencia;
                //retorno = "frmServicios";
            }
            if (opcion.Equals("frmClientesMayor"))
            {
                fuente = 6;
                compania = boton;
                referenciaClientesMayor = (frmClientesMayor)referencia;
            }
        }
        


        private void lbxAuxiliarContable_Activated(object sender, EventArgs e)
        {
            if (fuente == 1)
            {
                if (!referenciaPrincipal.empresaActual.BaseDatosActual.Equals("01"))
                {
                    cmdAgregar.Visible = false;
                }
                else
                {
                    cmdAgregar.Visible = true;
                }
            }

            if (fuente == 0)
            {
                if (!referenciaPrincipal.empresaActual.BaseDatosActual.Equals("01"))
                {
                    cmdAgregar.Visible = false;
                }
                else
                {
                    cmdAgregar.Visible = true;
                }
            }
        }
    }
}
