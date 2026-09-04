using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using lebrun.clases.contabilidad;
using lebrun.clasesData;

namespace lebrun.formularios.contabilidad
{
    public partial class frmComprobanteContable : Form
    {
        private ComprobanteContable comprobante;
        private AuxiliarContable au;        
        private DataTable dtAuxiliares;
        private lbxAuxiliarContable lbxAxuliar;
        private lbxComprobanteContable reflbxComprobanteContable;
        private lbxAuxiliaresOnly lbxSoloAuxiliar;
        private Principal referenciaPrincipal;
        private DetalleComprobanteContable detalleComprobante;
        private int centinelaFila = -1;
        private static frmComprobanteContable m_FormDefInstance;
        private DataTable temporal;
        
        //para manejarse en las diferentes sucursales y tablas
        private int idBaseDatos = 0;
        private int tabla = 0;

        public int Tabla
        {
            get { return tabla; }
            set { tabla = value; }
        }
        public int IdBaseDatos
        {
            get { return idBaseDatos; }
            set { idBaseDatos = value; }
        }
        
        public frmComprobanteContable()
        {
            InitializeComponent();
            comprobante = new ComprobanteContable();
            detalleComprobante = new DetalleComprobanteContable();
            au = new AuxiliarContable();
            rbtDebito.TabStop = true;
            rbtCredito.TabStop = true;
        }

        private void ComprobanteContable_Load(object sender, EventArgs e)
        {   
            referenciaPrincipal = (Principal)this.MdiParent;
        }


        public void cargarCorrelativo() {
            referenciaPrincipal = (Principal)this.MdiParent;
            comprobante.asignarCorrelativo(referenciaPrincipal.empresaActual.BaseDatosActual);
            textBox5.Text = comprobante.NroComprobanteCorrelativo;
        }

        public static frmComprobanteContable DefInstance
        {
            get
            {
                if (m_FormDefInstance == null || m_FormDefInstance.IsDisposed)
                    m_FormDefInstance = new frmComprobanteContable();
                return m_FormDefInstance;
            }
            set
            {
                m_FormDefInstance = value;
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            if (lbxAxuliar != null)
            {
                if (!lbxAxuliar.IsDisposed)
                {
                    lbxAxuliar.Dispose();
                }
            }

            referenciaPrincipal.toolStripComboBox1.Enabled = true;
            this.Close();
            
            
            //referenciaPrincipal.lbxComprobante.Show();
        }

   

        private void txtNroDocumento_KeyPress(object sender, KeyPressEventArgs e)
            {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                cmdAgregarItem.Focus();
                return;
            }

            if (e.KeyChar == 8)
            {
                e.Handled = false;
                return;
            }
            if ((e.KeyChar == '.') || (e.KeyChar == ','))
            {
                if (existeCaracter())
                {
                    e.Handled = true;
                    return;
                }
            }

            if (Char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
                return;
            }


            bool IsDec = false;
            int nroDec = 0;

            for (int i = 0; i < txtMontoAsiento.Text.Length; i++)
            {
                if ((txtMontoAsiento.Text[i] == '.') || (txtMontoAsiento.Text[i] == ','))
                    IsDec = true;


                if (IsDec && nroDec++ >= 2)
                {
                    e.Handled = true;
                    return;
                }
            }
        }
                
        private Boolean existeCaracter()
        {
            int i;
            int contador = 1;
            Boolean centinela = false;
            for (i = 0; i < txtMontoAsiento.Text.Length; i++)
            {
                if ((txtMontoAsiento.Text[i] == ',') || (txtMontoAsiento.Text[i] == '.'))
                {
                    contador = contador + 1;
                    if (contador > 1)
                    {
                        centinela = true;
                        break;
                    }

                }
            }
            return centinela;
        }

        private void cmdAgregarItem_Click(object sender, EventArgs e)
        {
            if (validarCampos()) {
                if (centinelaFila >= 0){
                    
                    agregarFilas(centinelaFila);
                    centinelaFila = -1;
                }
                else
                {
                    agregarFilas(-1);
                }
            }
        }
        
        public bool validarCampos()
        {
            bool centinela = true;

            if (txtDescripcion.Text == "")
            {
                centinela = false;
                MessageBox.Show("La Descripción es un campo Obligatorio!!", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtDescripcion.Focus();
                return centinela;
            }

            if (txtPlanCuentas.Text == "")
            {
                centinela = false;
                MessageBox.Show("El plan de cuentas es un campo Obligatorio!!", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPlanCuentas.Focus();
                return centinela;
            }

            // se deja habilitado porque la unica forma que
            //tenga auxiliares es cuando este habilitado
            if (txtAuxiliar.Enabled)
            {
                if (txtAuxiliar.Text == "")
                {
                    centinela = false;
                    MessageBox.Show("El auxiliar para este plan de cuentas es Obligatiorio!!", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtAuxiliar.Focus();
                    return centinela;
                }
            }

            if (txtNroDocumento.Enabled == true)
            {
                if (txtNroDocumento.Text == "")
                {
                    if (txtAuxiliar.Enabled)
                    {
                        if (au.valManejaDocumentos(txtAuxiliar.Text, txtPlanCuentas.Text, referenciaPrincipal.empresaActual.BaseDatosActual))
                        {
                            MessageBox.Show("El Nùmero es un campo Obligatiorio!!", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            centinela = false;
                            txtNroDocumento.Focus();
                            return centinela;
                        }
                    }
                }
            }

            //if (txtReferencia.Text == "")
            //{
            //    centinela = false;
            //    MessageBox.Show("La referencia  es un campo Obligatiorio!!", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    txtReferencia.Focus();
            //    return centinela;
            //}

            

            if (txtConcepto.Text == "")
            {
                centinela = false;
                MessageBox.Show("El concepto  es un campo Obligatiorio!!", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtConcepto.Focus();
                return centinela;
            }

            if (rbtCredito.Checked == false && rbtDebito.Checked == false)
            {
                centinela = false;
                MessageBox.Show("El Tipo de Movimiento es un campo Obligatiorio!!", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                rbtCredito.Focus();
                return centinela;
            }

            if (txtMontoAsiento.Text == "")
            {
                centinela = false;
                MessageBox.Show("El Monto del Asiento es un campo Obligatiorio!!", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMontoAsiento.Focus();
                return centinela;
            }

            /* segunda parte valido tanto que la cuenta exista, y si tiene auxilar que exista tambien*/
            if (txtPlanCuentas.Text != "") {
                au.CodigoPlanCuentas = txtPlanCuentas.Text;
                if (au.validarPlanCuenta(10, referenciaPrincipal.empresaActual.BaseDatosActual))
                {
                    txtNombreCuenta.Text = au.getDescripcionPlanC(au.CodigoPlanCuentas, referenciaPrincipal.empresaActual.BaseDatosActual);

                    if (au.poseeAuxilar(referenciaPrincipal.empresaActual.BaseDatosActual, au.CodigoPlanCuentas))
                    {
                        if (txtAuxiliar.Enabled == false)
                        {
                            MessageBox.Show("Debe Especificar el auxiliar del Plan de cuentas!!", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtAuxiliar.Enabled = true;
                            btAuxiliar.Enabled = true;
                            txtAuxiliar.Focus();
                            centinela = false;
                            return centinela;
                        }
                        else
                        {
                            au.CodigoAuxiliar = txtAuxiliar.Text;
                            if (!au.existeAxulilar())
                            {
                                MessageBox.Show("El auxiliar No existe!!", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                txtAuxiliar.Clear();
                                txtAuxiliar.Focus();
                                centinela = false;
                                return centinela;
                            }
                        }
                    }
                }
                else {
                    MessageBox.Show("El plan de Cuenta no Existe!!", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPlanCuentas.Clear();
                    txtPlanCuentas.Focus();
                    centinela = false;
                    return centinela;
                }
            }

            return centinela;
        }


        public void agregarFilas(int fila) {
            DateTime x = DateTime.Now;
            if (fila<0){
                dataGridView1.Rows.Add();
                fila = dataGridView1.Rows.Count-1;
            }

            dataGridView1.Rows[fila].Cells[0].Value = fila + 1;
            dataGridView1.Rows[fila].Cells[3].Value = txtNroDocumento.Text;
            au.reversarCodigo(txtPlanCuentas.Text);
            dataGridView1.Rows[fila].Cells[1].Value = txtPlanCuentas.Text;
            if (txtAuxiliar.Enabled)
            {
                dataGridView1.Rows[fila].Cells[2].Value = txtAuxiliar.Text;
            }
            else {
                dataGridView1.Rows[fila].Cells[2].Value = "";
            }

            dataGridView1.Rows[fila].Cells[4].Value = txtReferencia.Text;
            dataGridView1.Rows[fila].Cells[5].Value = txtDescripcion.Text;
            dataGridView1.Rows[fila].Cells[6].Value = txtConcepto.Text;
            if (rbtCredito.Checked)
            {
                dataGridView1.Rows[fila].Cells[8].Value = txtMontoAsiento.Text;
                dataGridView1.Rows[fila].Cells[7].Value = "";
            }
            else {
                dataGridView1.Rows[fila].Cells[7].Value = txtMontoAsiento.Text;
                dataGridView1.Rows[fila].Cells[8].Value = "";
            }
            dataGridView1.Rows[fila].Cells[9].Value = au.CodigoPlanCuentas;
            dataGridView1.Rows[fila].Cells[10].Value = ""+x.Year + "-" + x.Month + "-" + x.Day;
            dataGridView1.Rows[fila].Cells[11].Value = "" + x.Year + "-" + x.Month + "-" + x.Day;
            dataGridView1.Rows[fila].Cells[12].Value = referenciaPrincipal.usuarioActual.Id;

            centinelaFila = -1;
            limpiarFormulario();
            sumarTotal();
            txtPlanCuentas.Focus();
        }

        public void limpiarFormulario() {
            txtPlanCuentas.Text = "";
            txtReferencia.Text = "";
            txtNroDocumento.Text = "";
            txtConcepto.Text = txtDescripcion.Text;
            rbtCredito.Checked = false;
            rbtDebito.Checked = false;
            txtMontoAsiento.Text = "";
            txtNombreCuenta.Text = "Nombre Plan Cuenta";
            txtNombreAuxiliar.Text = "Nombre Auxiliar";

            if (txtAuxiliar.Enabled) {
                txtAuxiliar.Clear();
                txtAuxiliar.Enabled = false;
                btAuxiliar.Enabled = false;
                txtNroDocumento.Enabled = false;
            }
        }

        private void sumarTotal() {
            double credito = 0;
            double debito = 0;
            string temp = null;
            for (int i = 0; i < dataGridView1.Rows.Count; i++) {
                if (dataGridView1.Rows[i].Cells[7].Value.ToString() != "")
                {
                    temp = dataGridView1.Rows[i].Cells[7].Value.ToString();
                    temp = temp.Replace(".", ",");
                    debito = debito + Convert.ToDouble(temp); 
                }
                else {
                    temp = dataGridView1.Rows[i].Cells[8].Value.ToString();
                    temp = temp.Replace(".", ",");
                    credito = credito + Convert.ToDouble(temp);
                }
                //se limpia el temp para que no quede embasurado
                temp = null;
            }
            txtCredito.Text = ""+credito;
            txtDebito.Text = ""+debito;
        }

        public void modificarFilas(int numFila) { 

            txtReferencia.Text = dataGridView1.Rows[numFila].Cells[4].Value.ToString();
            
            if (dataGridView1.Rows[numFila].Cells[3].Value.ToString() != "")
            {
                txtNroDocumento.Enabled = true;                
                txtNroDocumento.Text = dataGridView1.Rows[numFila].Cells[3].Value.ToString();
            }
            txtConcepto.Text = dataGridView1.Rows[numFila].Cells[6].Value.ToString();            
            txtPlanCuentas.Text = dataGridView1.Rows[numFila].Cells[9].Value.ToString();
            au.CodigoPlanCuentas = dataGridView1.Rows[numFila].Cells[9].Value.ToString();
            txtNombreCuenta.Text = au.getDescripcionPlanC(au.CodigoPlanCuentas, referenciaPrincipal.empresaActual.BaseDatosActual);

            if(dataGridView1.Rows[numFila].Cells[2].Value.ToString()!=""){
                txtAuxiliar.Enabled = true;
                btAuxiliar.Enabled = true;
                txtAuxiliar.Text = dataGridView1.Rows[numFila].Cells[2].Value.ToString();
                txtNombreAuxiliar.Text = au.getDescripcionAuxiliar(txtPlanCuentas.Text, txtAuxiliar.Text, referenciaPrincipal.empresaActual.BaseDatosActual);
            }
            
            if (dataGridView1.Rows[numFila].Cells[8].Value.ToString() != "")
            {
                txtMontoAsiento.Text = dataGridView1.Rows[numFila].Cells[8].Value.ToString();
                rbtCredito.Checked = true;
            }
            else {
                txtMontoAsiento.Text = dataGridView1.Rows[numFila].Cells[7].Value.ToString();
                rbtDebito.Checked = true;
            }
            
        }

        private void cmdModificarItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                limpiarFormulario();
                modificarFilas(dataGridView1.CurrentRow.Index);
                centinelaFila = dataGridView1.CurrentRow.Index;
            }
            else {
                MessageBox.Show("No se puede Modificar si no existe ningun Registro!!", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
      

        private void cmdBuscarPlanC_Click(object sender, EventArgs e)
        {
            if (txtPlanCuentas.Text == "")
            {
                lbxAxuliar = lbxAuxiliarContable.DefInstance;
                lbxAxuliar.MdiParent = referenciaPrincipal;
                lbxAxuliar.soloVista(this);
                lbxAxuliar.Show();
                lbxAxuliar.lbxComprobante(false, referenciaPrincipal);
                lbxAxuliar.cmdAgregar.Visible = false;
                lbxAxuliar.cmdBotonModificar.Visible = false;
            }
            else {
                buscarVerificarPlanC();
            }
        }

        private void cmdEliminarItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("No Posee Items para eliminar!!", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                dataGridView1.Rows.RemoveAt(dataGridView1.CurrentRow.Index);
                actualizarNumeroFilas();
                limpiarFormulario();
                sumarTotal();
            }
        }

        //pasar tab con enter en todos los text designados
        private void MyTextBox_KeyPress(Object sender, KeyPressEventArgs e)
        {   
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true; SendKeys.Send("{TAB}");
            }
        }

        private void cmdGuardar_Click(object sender, EventArgs e)
        {
            DateTime miFecha =  dateTimePicker4.Value.Date;
            int resultado =comprobante.validarFechaCierre(miFecha,referenciaPrincipal.empresaActual.BaseDatosActual);
            if (resultado == 0)
            {
                if ((txtDescripcion.Text != "") && (dataGridView1.Rows.Count > 0))
                {
                    comprobante.Descripcion = txtDescripcion.Text;
                    comprobante.NroComprobanteCorrelativo = textBox5.Text;
                    comprobante.FechaComprobante = dateTimePicker4.Value.Year + "-" + dateTimePicker4.Value.Month + "-" + dateTimePicker4.Value.Day;
                    comprobante.FechaCarga = dateTimePicker3.Value.Year + "-" + dateTimePicker3.Value.Month + "-" + dateTimePicker3.Value.Day;
                    comprobante.IdOrigen = 8; //porque viene de contabilidad
                    comprobante.MontoCredito = txtCredito.Text.Replace(",", ".");
                    comprobante.MontoDebito = txtDebito.Text.Replace(",", ".");
                    comprobante.Status = 0; //en carga
                    comprobante.UltimoLogin = referenciaPrincipal.usuarioActual.Id;
                    comprobante.UltimaFecha = dateTimePicker3.Value.Year + "-" + dateTimePicker3.Value.Month + "-" + dateTimePicker3.Value.Day;
                    comprobante.agregarCabeceraTemp(referenciaPrincipal.usuarioActual.Id, referenciaPrincipal.empresaActual.BaseDatosActual,referenciaPrincipal.empresaActual.BaseDatosActual);

                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        datosDetalle(i, 0);
                    }
                    MessageBox.Show("Comprobante Contable Guardado Exitosamente!!", "Exito!! ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                    referenciaPrincipal.toolStripComboBox1.Enabled = true;
                    if (reflbxComprobanteContable != null)
                    {
                        reflbxComprobanteContable.cargarComprobantes();
                        reflbxComprobanteContable.Show();
                    }
                    else
                    {
                        referenciaPrincipal.lbxComprobante.cargarComprobantes();
                        referenciaPrincipal.lbxComprobante.Show();
                    }
                }
                else
                {
                    if (txtDescripcion.Text == "")
                    {
                        MessageBox.Show("La Cabecera del Comprobante Contable no puede Estar Vacio!!", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    if (dataGridView1.Rows.Count > 0)
                    {
                        MessageBox.Show("El detalle del Comprobante Contable debe al menos tener un Item!!", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else { 
                switch (resultado){
                    case 1:
                        MessageBox.Show("La Fecha del Comprobante no puede ser menor que la Fecha mensual del Cierre!!", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case 2:
                        MessageBox.Show("La Fecha del Comprobante no puede ser menor que la Fecha Anual del Cierre!!", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
            }
        }

        //primera version de esta funcion
        private void datosDetalle(int fila,int tabla1) { 
            /*tabla = 0 para temporal
             * tabla = 1 para definitivo*/
            if (tabla1 == 0)
            {
                detalleComprobante.Registro = textBox5.Text;
                detalleComprobante.NroComprobante = textBox5.Text;
            }
            else {
                detalleComprobante.Registro = comprobante.NroComprobanteCorrelativo;
                detalleComprobante.NroComprobante = comprobante.NroComprobanteCorrelativo;
            }
            
            detalleComprobante.Item = Convert.ToInt32(dataGridView1.Rows[fila].Cells[0].Value);
            detalleComprobante.FechaCarga1 = dateTimePicker3.Value.Year + "-" + dateTimePicker3.Value.Month + "-" + dateTimePicker3.Value.Day;
            detalleComprobante.CodigoMayor =dataGridView1.Rows[fila].Cells[9].Value.ToString();
            detalleComprobante.CodigoAuxiliar = dataGridView1.Rows[fila].Cells[2].Value.ToString();
            detalleComprobante.NroDocumento = dataGridView1.Rows[fila].Cells[3].Value.ToString();
            detalleComprobante.NroReferencia = dataGridView1.Rows[fila].Cells[4].Value.ToString();
            detalleComprobante.Descripcion1 = dataGridView1.Rows[fila].Cells[6].Value.ToString();
            detalleComprobante.FechaComprobanteItem = dateTimePicker4.Value.Year + "-" + dateTimePicker4.Value.Month + "-" + dateTimePicker4.Value.Day;
            if (tabla1 == 0)
            {
                detalleComprobante.Status = 0;
            }
            else {
                detalleComprobante.Status = 2;
            }
            detalleComprobante.Login = referenciaPrincipal.usuarioActual.Id;
            detalleComprobante.UltLogin = dataGridView1.Rows[fila].Cells[12].Value.ToString();
            detalleComprobante.FechaMod = dataGridView1.Rows[fila].Cells[11].Value.ToString();
            detalleComprobante.Compania = referenciaPrincipal.usuarioActual.CodigoCompania;
            if (dataGridView1.Rows[fila].Cells[7].Value.ToString() != "")
            {
                detalleComprobante.TipoTransaccion = "D";
                detalleComprobante.Monto = dataGridView1.Rows[fila].Cells[7].Value.ToString().Replace(",", ".");
            }
            else {
                detalleComprobante.TipoTransaccion = "C";
                detalleComprobante.Monto = dataGridView1.Rows[fila].Cells[8].Value.ToString().Replace(",", ".");
            }
            if (tabla1 == 0) {
                detalleComprobante.agregarDetalleTemp(referenciaPrincipal.empresaActual.BaseDatosActual);
                detalleComprobante.limpiar();                
            }
            if (tabla1 == 1) {
                detalleComprobante.agregarDetalle(referenciaPrincipal.empresaActual.BaseDatosActual);
                detalleComprobante.limpiar();
            }
        }

        private void datosDetalle(int fila, int tabla1,string dbActual)
        {
            /*tabla = 0 para temporal
             * tabla = 1 para definitivo*/
            if (tabla1 == 0)
            {
                detalleComprobante.Registro = textBox5.Text;
                detalleComprobante.NroComprobante = textBox5.Text;
            }
            else
            {
                detalleComprobante.Registro = comprobante.NroComprobanteCorrelativo;
                detalleComprobante.NroComprobante = comprobante.NroComprobanteCorrelativo;
            }

            detalleComprobante.Item = Convert.ToInt32(dataGridView1.Rows[fila].Cells[0].Value);
            detalleComprobante.FechaCarga1 = dateTimePicker3.Value.Year + "-" + dateTimePicker3.Value.Month + "-" + dateTimePicker3.Value.Day;
            detalleComprobante.CodigoMayor = dataGridView1.Rows[fila].Cells[9].Value.ToString();
            detalleComprobante.CodigoAuxiliar = dataGridView1.Rows[fila].Cells[2].Value.ToString();
            detalleComprobante.NroDocumento = dataGridView1.Rows[fila].Cells[3].Value.ToString();
            detalleComprobante.NroReferencia = dataGridView1.Rows[fila].Cells[4].Value.ToString();
            detalleComprobante.Descripcion1 = dataGridView1.Rows[fila].Cells[6].Value.ToString();
            detalleComprobante.FechaComprobanteItem = dateTimePicker4.Value.Year + "-" + dateTimePicker4.Value.Month + "-" + dateTimePicker4.Value.Day;
            if (tabla1 == 0)
            {
                detalleComprobante.Status = 0;
            }
            else
            {
                detalleComprobante.Status = 2;
            }
            detalleComprobante.Login = referenciaPrincipal.usuarioActual.Id;
            detalleComprobante.UltLogin = dataGridView1.Rows[fila].Cells[12].Value.ToString();
            detalleComprobante.FechaMod = dataGridView1.Rows[fila].Cells[11].Value.ToString();
            detalleComprobante.Compania = referenciaPrincipal.usuarioActual.CodigoCompania;
            if (dataGridView1.Rows[fila].Cells[7].Value.ToString() != "")
            {
                detalleComprobante.TipoTransaccion = "D";
                detalleComprobante.Monto = dataGridView1.Rows[fila].Cells[7].Value.ToString().Replace(",", ".");
            }
            else
            {
                detalleComprobante.TipoTransaccion = "C";
                detalleComprobante.Monto = dataGridView1.Rows[fila].Cells[8].Value.ToString().Replace(",", ".");
            }
            if (tabla1 == 0)
            {
                detalleComprobante.agregarDetalleTemp(dbActual);
                detalleComprobante.limpiar();
            }
            if (tabla1 == 1)
            {
                detalleComprobante.agregarDetalle(dbActual);
                detalleComprobante.limpiar();
            }
        }



        public void cargarModificar(string idComprobante) {
            comprobante.NroComprobanteCorrelativo = idComprobante;
            referenciaPrincipal = (Principal)this.MdiParent;
            comprobante.cargarCabecera(referenciaPrincipal.empresaActual.BaseDatosActual);
            mostrarDatosCabecera();
            detalleComprobante.NroComprobante = idComprobante;
            mostarDatosGrid();
            mostrarBotones(true);
        }

        /*se le pasa idbasedatos mas tabla, para la diferentes opciones 
         * que se manejaran en este modulo tanto bases de datos como tablas
         * */
        public void cargarModificar(string idComprobante,int tabla)
        {
            referenciaPrincipal = (Principal)this.MdiParent;
            comprobante.NroComprobanteCorrelativo = idComprobante;
            comprobante.cargarCabecera(referenciaPrincipal.empresaActual.BaseDatosActual, tabla);
            mostrarDatosCabecera();
            detalleComprobante.NroComprobante = idComprobante;
            mostarDatosGrid(referenciaPrincipal.empresaActual.BaseDatosActual,tabla);
        }



        public void mostrarDatosCabecera() {
            DateTime fechaTemporal;
            textBox5.Text = comprobante.NroComprobanteCorrelativo;
            txtDescripcion.Text = comprobante.Descripcion;
            fechaTemporal = Convert.ToDateTime(comprobante.FechaComprobante);
            dateTimePicker4.Value = fechaTemporal;
            fechaTemporal = Convert.ToDateTime(comprobante.FechaCarga);
            dateTimePicker3.Value = fechaTemporal;
        }

        public void mostarDatosGrid(string idBaseDatos, int tabla)
        {
            int fila = 0;
            temporal = detalleComprobante.itemsDetalle(referenciaPrincipal.empresaActual.BaseDatosActual, tabla);
            for (int i = 0; i < temporal.Rows.Count; i++)
            {

                dataGridView1.Rows.Add();
                fila = dataGridView1.Rows.Count - 1;
                dataGridView1.Rows[fila].Cells[0].Value = Convert.ToInt32(temporal.Rows[i][0].ToString());                
                dataGridView1.Rows[fila].Cells[3].Value = temporal.Rows[i][1].ToString();
                dataGridView1.Rows[fila].Cells[1].Value = au.cargarCodigo(temporal.Rows[i][2].ToString());
                dataGridView1.Rows[fila].Cells[2].Value = temporal.Rows[i][3].ToString();
                dataGridView1.Rows[fila].Cells[4].Value = temporal.Rows[i][4].ToString();
                dataGridView1.Rows[fila].Cells[5].Value = temporal.Rows[i][5].ToString();
                dataGridView1.Rows[fila].Cells[6].Value = temporal.Rows[i][5].ToString();

                if (temporal.Rows[i][6].ToString() != "")
                {
                    dataGridView1.Rows[fila].Cells[8].Value = temporal.Rows[i][6].ToString();
                }
                else
                {
                    dataGridView1.Rows[fila].Cells[8].Value = "";
                }

                if (temporal.Rows[i][7].ToString() != "")
                {
                    dataGridView1.Rows[fila].Cells[7].Value = temporal.Rows[i][7].ToString();
                }
                else
                {
                    dataGridView1.Rows[fila].Cells[7].Value = "";
                }

                dataGridView1.Rows[fila].Cells[9].Value = temporal.Rows[i][8].ToString();
                dataGridView1.Rows[fila].Cells[10].Value = temporal.Rows[i][9].ToString();
                dataGridView1.Rows[fila].Cells[11].Value = temporal.Rows[i][11].ToString();
                dataGridView1.Rows[fila].Cells[12].Value = temporal.Rows[i][10].ToString();
            }
            sumarTotal();
        }



        //primera version
        public void mostarDatosGrid() {
            int fila = 0;            
            temporal = detalleComprobante.itemsDetalle(referenciaPrincipal.empresaActual.BaseDatosActual);            
            for (int i = 0; i < temporal.Rows.Count; i++) {
                
                dataGridView1.Rows.Add();
                fila = dataGridView1.Rows.Count - 1;
                dataGridView1.Rows[fila].Cells[0].Value = Convert.ToInt32(temporal.Rows[i][0].ToString());
                dataGridView1.Rows[fila].Cells[3].Value = temporal.Rows[i][1].ToString();
                dataGridView1.Rows[fila].Cells[1].Value = au.cargarCodigo(temporal.Rows[i][2].ToString());
                dataGridView1.Rows[fila].Cells[2].Value = temporal.Rows[i][3].ToString();
                dataGridView1.Rows[fila].Cells[4].Value = temporal.Rows[i][4].ToString();
                dataGridView1.Rows[fila].Cells[5].Value = temporal.Rows[i][5].ToString();
                dataGridView1.Rows[fila].Cells[6].Value = temporal.Rows[i][5].ToString();

                if (temporal.Rows[i][6].ToString() != "")
                {
                    dataGridView1.Rows[fila].Cells[8].Value = temporal.Rows[i][6].ToString();
                }
                else {
                    dataGridView1.Rows[fila].Cells[8].Value = "";
                }

                if (temporal.Rows[i][7].ToString() != "")
                {
                    dataGridView1.Rows[fila].Cells[7].Value = temporal.Rows[i][7].ToString();
                }
                else {
                    dataGridView1.Rows[fila].Cells[7].Value = "";
                }

                dataGridView1.Rows[fila].Cells[9].Value = temporal.Rows[i][8].ToString();
                dataGridView1.Rows[fila].Cells[10].Value = temporal.Rows[i][9].ToString();
                dataGridView1.Rows[fila].Cells[11].Value = temporal.Rows[i][11].ToString();
                dataGridView1.Rows[fila].Cells[12].Value = temporal.Rows[i][10].ToString();
            }
            sumarTotal();
        }

        public void mostrarBotones(bool valor) {
            btGuardarModificar.Visible = valor;
            cmdGuardar.Visible = !valor;
            //por lo general como va relacionado con modificar
            dateTimePicker3.Enabled = !valor;

        }

        private void btGuardarModificar_Click(object sender, EventArgs e)
        {   
            DateTime miFecha =  dateTimePicker4.Value.Date;
            int resultado =comprobante.validarFechaCierre(miFecha,referenciaPrincipal.empresaActual.BaseDatosActual);
            if (resultado == 0)
            {
                if ((txtDescripcion.Text != "") && (dataGridView1.Rows.Count > 0))
                {
                    if ((idBaseDatos != 0) && (tabla != 0))
                    {
                        if (montosDescuadrados())
                        {//error
                            MessageBox.Show("No se permiten Guardar Comprobantes Descuadrados!!", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            //se llama a la funcion que tiene la base de datos
                            camposDefinitivo(referenciaPrincipal.empresaActual.BaseDatosActual);
                        }
                    }
                    else {
                        
                        //la funcion normal
                        camposTemporal();
                    }
                }
                else
                {
                    if (txtDescripcion.Text == "")
                    {
                        MessageBox.Show("La Cabecera del Comprobante Contable no puede Estar Vacio!!", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    if (dataGridView1.Rows.Count <= 0)
                    {
                        MessageBox.Show("El detalle del Comprobante Contable debe al menos tener un Item!!", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else {
                switch (resultado)
                {
                    case 1:
                        MessageBox.Show("La Fecha del Comprobante no puede ser menor que la Fecha mensual del Cierre!!", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case 2:
                        MessageBox.Show("La Fecha del Comprobante no puede ser menor que la Fecha Anual del Cierre!!", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
            }
        }

        //primera Version
        public void camposTemporal() {
            comprobante.Descripcion = txtDescripcion.Text;
            comprobante.NroComprobanteCorrelativo = textBox5.Text;
            comprobante.FechaComprobante = dateTimePicker4.Value.Year + "-" + dateTimePicker4.Value.Month + "-" + dateTimePicker4.Value.Day;
            comprobante.FechaCarga = dateTimePicker3.Value.Year + "-" + dateTimePicker3.Value.Month + "-" + dateTimePicker3.Value.Day;
            comprobante.IdOrigen = 8; //porque viene de contabilidad
            comprobante.MontoCredito = txtCredito.Text.Replace(",", ".");
            comprobante.MontoDebito = txtDebito.Text.Replace(",", ".");
            comprobante.Status = 0; //en carga
            comprobante.UltimoLogin = referenciaPrincipal.usuarioActual.Id;
            comprobante.UltimaFecha = dateTimePicker3.Value.Year + "-" + dateTimePicker3.Value.Month + "-" + dateTimePicker3.Value.Day;

            if (comprobante.actualizarCabeceraTemporal(referenciaPrincipal.empresaActual.BaseDatosActual))
            {
                if (detalleComprobante.borrarItemsTemporal(textBox5.Text,referenciaPrincipal.empresaActual.BaseDatosActual))
                {
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        datosDetalle(i, 0);
                    }
                    MessageBox.Show("Comprobante Contable Guardado Exitosamente!!", "Exito!! ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                    referenciaPrincipal.toolStripComboBox1.Enabled = true;
                    if (reflbxComprobanteContable != null)
                    {
                        reflbxComprobanteContable.cargarComprobantes();
                        reflbxComprobanteContable.Show();
                    }
                    else {
                        referenciaPrincipal.lbxComprobante.cargarComprobantes();
                        referenciaPrincipal.lbxComprobante.Show();
                    }
                }
            }
        }


        public void camposDefinitivo(string dbActual)
        {
            comprobante.Descripcion = txtDescripcion.Text;
            comprobante.NroComprobanteCorrelativo = textBox5.Text;
            comprobante.FechaComprobante = dateTimePicker4.Value.Year + "-" + dateTimePicker4.Value.Month + "-" + dateTimePicker4.Value.Day;
            comprobante.FechaCarga = dateTimePicker3.Value.Year + "-" + dateTimePicker3.Value.Month + "-" + dateTimePicker3.Value.Day;
            comprobante.IdOrigen = 8; //porque viene de contabilidad
            comprobante.MontoCredito = txtCredito.Text.Replace(",", ".");
            comprobante.MontoDebito = txtDebito.Text.Replace(",", ".");
            comprobante.Status = 1; //procesado
            comprobante.UltimoLogin = referenciaPrincipal.usuarioActual.Id;
            comprobante.UltimaFecha = dateTimePicker3.Value.Year + "-" + dateTimePicker3.Value.Month + "-" + dateTimePicker3.Value.Day;
            comprobante.FechaComprobanteItem = dateTimePicker4.Value.Year + "-" + dateTimePicker4.Value.Month + "-" + dateTimePicker4.Value.Day;
            foreach (DataGridViewRow p in dataGridView1.Rows)
            {
                //comprobante.FechaComprobante
                p.Cells[10].Value = comprobante.FechaComprobante;
            }

            if (comprobante.actualizarCabecera(dbActual))
            {
                if (detalleComprobante.borrarItems(textBox5.Text,dbActual))
                {
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        datosDetalle(i, 1, dbActual);
                    }
                    MessageBox.Show("Comprobante Contable Guardado Exitosamente!!", "Exito!! ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    referenciaPrincipal.toolStripComboBox1.Enabled = true;
                    this.Close();
                    if (reflbxComprobanteContable != null)
                    {
                        reflbxComprobanteContable.cargarComprobantes();
                        reflbxComprobanteContable.Show();
                    }
                    else
                    {
                        referenciaPrincipal.lbxComprobante.cargarComprobantes();
                        referenciaPrincipal.lbxComprobante.Show();
                    }
                }
            }
        }





        private void actualizarNumeroFilas() {
            for (int i = 0; i < dataGridView1.Rows.Count;i++ )
                dataGridView1.Rows[i].Cells[0].Value = i+1;
        }
        

        private void cmdCierreDefinitivo_Click(object sender, EventArgs e)
        {
            if ((txtDescripcion.Text != "")&& (dataGridView1.Rows.Count>0)){
                DateTime miFecha =  dateTimePicker4.Value.Date;
            int resultado =comprobante.validarFechaCierre(miFecha,referenciaPrincipal.empresaActual.BaseDatosActual);
            if (resultado == 0)
            {

                //se comprueba si esta grabado en tabla temporal 
                if (comprobante.existeTemporal(textBox5.Text, referenciaPrincipal.empresaActual.BaseDatosActual))
                {
                    //lo que se hace en caso que exista
                    //se valida si se puede cerrar descuadrado o no
                    if ((comprobante.comprobanteDescuadre(referenciaPrincipal.empresaActual.BaseDatosActual)))
                    {
                        //primero borramos en temporal
                        comprobante.borrarCabeceraTemp(textBox5.Text, referenciaPrincipal.empresaActual.BaseDatosActual);
                        detalleComprobante.borrarItemsTemporal(textBox5.Text, referenciaPrincipal.empresaActual.BaseDatosActual);
                        camposDefinitivo();
                    }
                    else
                    {
                        //se valida contra el grid porque el usuario pude poner algo de mas que no este en la base de datos
                        if (montosDescuadrados())
                        {
                            //error
                            MessageBox.Show("No se permiten Guardar Comprobantes Descuadrados!!", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            //cierro Normal
                            //primero borramos en temporal
                            comprobante.borrarCabeceraTemp(textBox5.Text, referenciaPrincipal.empresaActual.BaseDatosActual);
                            detalleComprobante.borrarItemsTemporal(textBox5.Text, referenciaPrincipal.empresaActual.BaseDatosActual);
                            camposDefinitivo();
                        }
                    }
                }
                else
                {
                    //y en el caso que trabaje directo
                    //se valida si se puede cerrar descuadrado o no
                    if ((comprobante.comprobanteDescuadre(referenciaPrincipal.empresaActual.BaseDatosActual)))
                    {
                        camposDefinitivo();
                    }
                    else
                    {
                        if (montosDescuadrados())
                        {
                            //error
                            MessageBox.Show("No se permiten Guardar Comprobantes Descuadrados!!", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            //cierro Normal
                            camposDefinitivo();
                        }
                    }

                }
            }
            else {
                switch (resultado)
                {
                    case 1:
                        MessageBox.Show("La Fecha del Comprobante no puede ser menor que la Fecha mensual del Cierre!!", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case 2:
                        MessageBox.Show("La Fecha del Comprobante no puede ser menor que la Fecha Anual del Cierre!!", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }

            }

            }
            else
            {
                if (txtDescripcion.Text == "")
                {
                    MessageBox.Show("La Cabecera del Comprobante Contable no puede Estar Vacio!!", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                if (dataGridView1.Rows.Count <= 0)
                {
                    MessageBox.Show("El detalle del Comprobante Contable debe al menos tener un Item!!", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void camposDefinitivo() {

            //luego asignamos y guardamos
            comprobante.asignarCorrelativoFinal(referenciaPrincipal.empresaActual.BaseDatosActual);
            comprobante.Descripcion = txtDescripcion.Text;
            //comprobante.NroComprobanteCorrelativo = textBox5.Text;
            comprobante.FechaComprobante = dateTimePicker4.Value.Year + "-" + dateTimePicker4.Value.Month + "-" + dateTimePicker4.Value.Day;
            comprobante.FechaCarga = dateTimePicker3.Value.Year + "-" + dateTimePicker3.Value.Month + "-" + dateTimePicker3.Value.Day;
            comprobante.IdOrigen = 8; //porque viene de contabilidad
            comprobante.MontoCredito = txtCredito.Text.Replace(",", ".");
            comprobante.MontoDebito = txtDebito.Text.Replace(",", ".");
            comprobante.Status = 1; //procesado
            comprobante.UltimoLogin = referenciaPrincipal.usuarioActual.Id;
            comprobante.UltimaFecha = dateTimePicker3.Value.Year + "-" + dateTimePicker3.Value.Month + "-" + dateTimePicker3.Value.Day;
            comprobante.agregarCabecera(referenciaPrincipal.usuarioActual.Id, referenciaPrincipal.usuarioActual.CodigoCompania,referenciaPrincipal.empresaActual.BaseDatosActual);
            comprobante.FechaComprobanteItem = dateTimePicker4.Value.Year + "-" + dateTimePicker4.Value.Month + "-" + dateTimePicker4.Value.Day;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                datosDetalle(i, 1);
            }
            MessageBox.Show("Comprobante Contable Guardado Exitosamente!!", "Exito!! ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
            referenciaPrincipal.toolStripComboBox1.Enabled = true;
            if (reflbxComprobanteContable != null)
            {
                reflbxComprobanteContable.cargarComprobantes();
                reflbxComprobanteContable.Show();
            }
            else
            {
                referenciaPrincipal.lbxComprobante.cargarComprobantes();
                referenciaPrincipal.lbxComprobante.Show();
            }
        }

        private void dateTimePicker4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true; SendKeys.Send("{TAB}");
            }
        }

        private bool montosDescuadrados() {
            bool centinela = true;
            double montoDebito = 0;
            double montoCredito =0;

            montoDebito = Convert.ToDouble(txtDebito.Text);
            montoCredito = Convert.ToDouble(txtCredito.Text);
            if (montoDebito == montoCredito) {
                centinela = false;
            }
            return centinela;
        }

        public void formularioVer() {
            groupBox1.Enabled = false;
            groupBox2.Enabled = false;
            cmdAgregarItem.Enabled = false;
            cmdModificarItem.Enabled = false;
            cmdEliminarItem.Enabled = false;
            btGuardarModificar.Enabled = false;
            cmdGuardar.Enabled = false;
            cmdCierreDefinitivo.Enabled = false;
        }

        public void guardarModificacionDefinitivo() {
            cmdCierreDefinitivo.Visible = false;
            cmdGuardar.Visible = false;
            btGuardarModificar.Visible = true;
        }

        private void txtDescripcion_Leave(object sender, EventArgs e)
        {
            txtConcepto.Text = txtDescripcion.Text;
        }

        private void rbtDebito_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true; SendKeys.Send("{TAB}");
            }
        }

        private void rbtCredito_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                rbtCredito.TabStop = true;
                rbtDebito.TabStop = true;
                e.Handled = true; SendKeys.Send("{TAB}");
            }
        }

        private void rbtDebito_CheckedChanged(object sender, EventArgs e)
        {
            rbtDebito.TabStop = true;
            txtMontoAsiento.Focus();
        }

        private void rbtCredito_CheckedChanged(object sender, EventArgs e)
        {
            rbtCredito.TabStop = true;
            txtMontoAsiento.Focus();
        }

        private void rbtDebito_Enter(object sender, EventArgs e)
        {
            rbtCredito.TabStop = true;
            rbtDebito.TabStop = true;
            rbtDebito.Checked = true;
        }

        private void btAuxiliar_Click(object sender, EventArgs e)
        {
            lbxSoloAuxiliar = lbxAuxiliaresOnly.DefInstance;
            lbxSoloAuxiliar.comprobantePadre(this);
            lbxSoloAuxiliar.MdiParent = referenciaPrincipal;
            lbxSoloAuxiliar.codCuenta(txtPlanCuentas.Text);
            lbxSoloAuxiliar.llenarAuxiliares();
            lbxSoloAuxiliar.Show();
        }

        public void banderaAuxiliar(bool valor) {
            au.TieneAuxiliar = valor;
        }

        private void txtAuxiliar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
                return;
            }
            if (e.KeyChar.Equals(' ')) {
                e.Handled = true;
                return;
            }

            if (e.KeyChar == (char)(Keys.Enter) &&  txtAuxiliar.Text!="")
            {
                //e.Handled = true; SendKeys.Send("{TAB}");
                string caracterFaltante = null;
                if (txtAuxiliar.Text.Length < 5)
                {
                    for (int i = 0; i < (5 - txtAuxiliar.Text.Length); i++)
                    {
                        caracterFaltante = caracterFaltante + "0";
                    }
                    txtAuxiliar.Text = caracterFaltante + txtAuxiliar.Text;

                }
                au.CodigoAuxiliar = txtAuxiliar.Text;

                if (au.existeAxulilar())
                {
                    if (au.permisoAuxiliar(txtPlanCuentas.Text, au.CodigoAuxiliar, referenciaPrincipal.empresaActual.BaseDatosActual))
                    {
                        txtNombreAuxiliar.Text = au.getDescripcionAuxiliar(txtPlanCuentas.Text, au.CodigoAuxiliar, referenciaPrincipal.empresaActual.BaseDatosActual);
                        if (au.valManejaDocumentos(au.CodigoAuxiliar, txtPlanCuentas.Text, referenciaPrincipal.empresaActual.BaseDatosActual))
                        {
                            txtNroDocumento.Enabled = true;
                            txtNroDocumento.Focus();
                        }
                        else
                        {
                            txtNroDocumento.Enabled = false;
                            txtReferencia.Focus();
                        }
                    }
                    else {
                        MessageBox.Show("El Auxiliar no Posee Permisos en Cia.!!", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtAuxiliar.Text = "";
                        txtAuxiliar.Focus();
                        au.CodigoAuxiliar = "";
                    }
                }
                else
                {
                    MessageBox.Show("El Auxiliar no Existe!!", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtAuxiliar.Text = "";
                    txtAuxiliar.Focus();
                    au.CodigoAuxiliar = "";
                }

            }
        }

        //private void txtAuxiliar_Leave(object sender, EventArgs e)
        //{
        //    //string caracterFaltante = null;
        //    //if (txtAuxiliar.Text != "")
        //    //{
        //    //    if (txtAuxiliar.Text.Length< 5)
        //    //    { 
        //    //        for(int i=0;i<(5- txtAuxiliar.Text.Length);i++){
        //    //            caracterFaltante = caracterFaltante+"0";
        //    //        }
        //    //        txtAuxiliar.Text = caracterFaltante + txtAuxiliar.Text;
                    
        //    //    }
        //    //    au.CodigoAuxiliar = txtAuxiliar.Text;

        //    //    if (au.existeAxulilar())
        //    //    {
        //    //        txtNombreAuxiliar.Text = au.getDescripcionAuxiliar(txtPlanCuentas.Text, au.CodigoAuxiliar, referenciaPrincipal.empresaActual.BaseDatosActual);
        //    //        if (au.valManejaDocumentos(au.CodigoAuxiliar, txtPlanCuentas.Text, referenciaPrincipal.empresaActual.BaseDatosActual))
        //    //        {
        //    //            txtNroDocumento.Enabled = true;
        //    //            txtNroDocumento.Focus();
        //    //        }
        //    //        else {
        //    //            txtNroDocumento.Enabled = false;
        //    //            txtReferencia.Focus();
        //    //        }
        //    //    }
        //    //    else {
        //    //        MessageBox.Show("El Auxiliar no Existe!!", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    //        txtAuxiliar.Text = "";
        //    //        txtAuxiliar.Focus();
        //    //        au.CodigoAuxiliar = "";
        //    //    }
                
        //    //}
        //    //else { 

        //    //}
        //}

        private void txtPlanCuentas_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)(Keys.Enter)) && txtPlanCuentas.Text != "")
            {
                buscarVerificarPlanC();
            }
            else { 
                if (e.KeyChar == (char)(Keys.Enter)){
                    e.Handled = true; SendKeys.Send("{TAB}");
                }
            }
        }

        private void buscarVerificarPlanC()
        {
            if (txtPlanCuentas.Text != "")
            {
                au.CodigoPlanCuentas = txtPlanCuentas.Text;

                if (au.validarPlanCuenta(10, referenciaPrincipal.empresaActual.BaseDatosActual))
                {
                    if (au.isNivel5(referenciaPrincipal.empresaActual.BaseDatosActual))
                    {
                        string cadenaPermisos = null;
                        cadenaPermisos = au.getControl(au.CodigoPlanCuentas, referenciaPrincipal.empresaActual.BaseDatosActual);
                        if ((cadenaPermisos.Equals("0")) || (cadenaPermisos.Contains(referenciaPrincipal.empresaActual.BaseDatosActual)))
                        {
                            txtNombreCuenta.Text = au.getDescripcionPlanC(au.CodigoPlanCuentas, referenciaPrincipal.empresaActual.BaseDatosActual);
                            if (au.poseeAuxilar(referenciaPrincipal.empresaActual.BaseDatosActual, au.CodigoPlanCuentas))
                            {
                                /*Principal x = (Principal)this.MdiParent;
                                x.toolStripComboBox1.ComboBox.SelectedValue;*/
                                txtAuxiliar.Enabled = true;
                                txtAuxiliar.Focus();
                                btAuxiliar.Enabled = true;
                            }
                            else
                            {
                                txtAuxiliar.Enabled = false;
                                btAuxiliar.Enabled = false;
                                txtReferencia.Focus();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Esta compañia no posee permisos para manejar la cuenta !!", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("El plan de Cuenta No maneja Movimiento!!", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtPlanCuentas.Text = "";
                        txtPlanCuentas.Focus();
                    }

                    //cargarComboAuxiliar(au.CodigoPlanCuentas);
                    //if (cboAuxiliar.SelectedValue != null)
                    //{
                    //    if (au.valManejaDocumentos(cboAuxiliar.SelectedValue.ToString(), txtPlanCuentas.Text,referenciaPrincipal.empresaActual.BaseDatosActual))
                    //    {
                    //        txtNroDocumento.Enabled = true;
                    //        txtNroDocumento.Focus();
                    //    }
                    //    else {
                    //        txtNroDocumento.Enabled = false;
                    //    }
                    //} else{
                    //    if (txtNroDocumento.Enabled)
                    //    {
                    //        txtNroDocumento.Enabled = false;
                    //    }
                    //}

                }
                else
                {
                    MessageBox.Show("El plan de Cuenta no Existe!!", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPlanCuentas.Text = "";
                    txtPlanCuentas.Focus();
                }
            }
            //else {
            //        MessageBox.Show("El plan de Cuenta No puede Quedar Vacio!!", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        public void referenciaLbxC(lbxComprobanteContable reflbx) {
            reflbxComprobanteContable = reflbx;
        }
    }
}
    