using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using lebrun.clases.contabilidad;

namespace lebrun.formularios.contabilidad
{
    public partial class lbxComprobanteContable : Form
    {
        frmComprobanteContable comprobante;
        ComprobanteContable compro1;
        private Principal referenciaPrincipal;
        DataTable tablaComprobantes;
        private DataView dv;
        private static lbxComprobanteContable m_FormDefInstance;

        public lbxComprobanteContable()
        {
            InitializeComponent();
        }
        private void lbxComprobanteContable_Load(object sender, EventArgs e)
        {
            referenciaPrincipal = (Principal)this.MdiParent;
            compro1 = new ComprobanteContable();
            //compro1.lbxComprobante(referenciaPrincipal.empresaActual.BaseDatosActual);
            cargarComprobantes();
            dv = new DataView(tablaComprobantes);
        }

        private void cmdNuevo_Click(object sender, EventArgs e)
        {
            comprobante = frmComprobanteContable.DefInstance;
            comprobante.MdiParent = referenciaPrincipal;
            comprobante.cargarCorrelativo();
            //comprobante.cargarCorrelativo();
            comprobante.Show();
            comprobante.referenciaLbxC(this);
            referenciaPrincipal.toolStripComboBox1.Enabled = false;
            this.Hide();
            referenciaPrincipal.toolStripComboBox1.Enabled = false;
            
        }

        public static lbxComprobanteContable DefInstance
        {
            get
            {
                if (m_FormDefInstance == null || m_FormDefInstance.IsDisposed)
                    m_FormDefInstance = new lbxComprobanteContable();
                return m_FormDefInstance;
            }
            set
            {
                m_FormDefInstance = value;
            }
        }


        public void cargarComprobantes() {
            tablaComprobantes = compro1.lbxComprobante(referenciaPrincipal.empresaActual.BaseDatosActual);
            this.dataGridView1.DataSource = tablaComprobantes;
            this.dataGridView1.Columns[0].HeaderText = "Nro Comprobante";
            this.dataGridView1.Columns[1].HeaderText = "Fecha";
            this.dataGridView1.Columns[2].HeaderText = "Descripción";
            this.dataGridView1.Columns[3].HeaderText = "Monto Debitos";
            this.dataGridView1.Columns[4].HeaderText = "Monto Creditos";
            this.dataGridView1.Columns[5].HeaderText = "Status";
            this.dataGridView1.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
            this.dataGridView1.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;

        }

        private void cmdModificar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                if (dataGridView1.CurrentRow.Cells[5].Value.ToString().Equals("Procesado"))
                {
                    this.panel1.Visible = true;
                    rbtModificar.Checked = true;
                }
                else
                {
                    if (dataGridView1.CurrentRow.Cells[5].Value.ToString().Equals("En Carga"))
                    {
                        //se ve a que tabla se refiere
                        comprobante = frmComprobanteContable.DefInstance;
                        comprobante.MdiParent = referenciaPrincipal;
                        comprobante.cargarModificar(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                        referenciaPrincipal.toolStripComboBox1.Enabled = false;
                        this.Hide();
                        comprobante.Show();
                        comprobante.referenciaLbxC(this);
                    }
                    if (dataGridView1.CurrentRow.Cells[5].Value.ToString().Equals("Revision"))
                    {
                        comprobante = frmComprobanteContable.DefInstance;
                        comprobante.MdiParent = referenciaPrincipal;
                        comprobante.cargarModificar(dataGridView1.CurrentRow.Cells[0].Value.ToString(), 2);
                        referenciaPrincipal.toolStripComboBox1.Enabled = false;
                        //comprobante.mostrarBotones(true);
                        comprobante.guardarModificacionDefinitivo();

                        comprobante.IdBaseDatos = 3; //por ahora
                        comprobante.Tabla = 2;
                        this.Hide();
                        comprobante.Show();
                    }

                }
            }
            else {

                MessageBox.Show("Para Modifciar debe existir algun registro!!", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

      

        private void cmdBuscar_Click(object sender, EventArgs e)
        {
            if (textBox5.Text != "")
            {
                if ((textBox5.Text.ToLower().Equals("procesado")) || (textBox5.Text.ToLower().Equals("en carga"))
                    || (textBox5.Text.ToLower().Equals("revision")))
                {
                    if (textBox5.Text.ToLower().Equals("procesado"))
                    {
                        tablaComprobantes = compro1.buscarComprobantesStatus(referenciaPrincipal.empresaActual.BaseDatosActual, 1);
                        if (tablaComprobantes.Rows.Count == 0)
                        {

                            MessageBox.Show("No se consiguieron Resultados!!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            textBox5.Focus();
                        }
                        else
                        {
                            this.dataGridView1.DataSource = null;
                            this.dataGridView1.DataSource = tablaComprobantes;
                            this.dataGridView1.Columns[0].HeaderText = "Nro Comprobante";
                            this.dataGridView1.Columns[1].HeaderText = "Fecha";
                            this.dataGridView1.Columns[2].HeaderText = "Descripción";
                            this.dataGridView1.Columns[3].HeaderText = "Monto Debitos";
                            this.dataGridView1.Columns[4].HeaderText = "Monto Creditos";
                            this.dataGridView1.Columns[5].HeaderText = "Status";
                        }
                    }
                    else { 
                    if (textBox5.Text.ToLower().Equals("en carga"))
                    {
                        tablaComprobantes = compro1.buscarComprobantesStatus(referenciaPrincipal.empresaActual.BaseDatosActual, 0);
                        if (tablaComprobantes.Rows.Count == 0)
                        {

                            MessageBox.Show("No se consiguieron Resultados!!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            textBox5.Focus();
                        }
                        else
                        {
                            this.dataGridView1.DataSource = null;
                            this.dataGridView1.DataSource = tablaComprobantes;
                            this.dataGridView1.Columns[0].HeaderText = "Nro Comprobante";
                            this.dataGridView1.Columns[1].HeaderText = "Fecha";
                            this.dataGridView1.Columns[2].HeaderText = "Descripción";
                            this.dataGridView1.Columns[3].HeaderText = "Monto Debitos";
                            this.dataGridView1.Columns[4].HeaderText = "Monto Creditos";
                            this.dataGridView1.Columns[5].HeaderText = "Status";

                        }
                    }
                    if (textBox5.Text.ToLower().Equals("revision"))
                    {
                        tablaComprobantes = compro1.buscarComprobantesStatus(referenciaPrincipal.empresaActual.BaseDatosActual, 2);
                        if (tablaComprobantes.Rows.Count == 0)
                        {

                            MessageBox.Show("No se consiguieron Resultados!!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            textBox5.Focus();
                        }
                        else
                        {
                            this.dataGridView1.DataSource = null;
                            this.dataGridView1.DataSource = tablaComprobantes;
                            this.dataGridView1.Columns[0].HeaderText = "Nro Comprobante";
                            this.dataGridView1.Columns[1].HeaderText = "Fecha";
                            this.dataGridView1.Columns[2].HeaderText = "Descripción";
                            this.dataGridView1.Columns[3].HeaderText = "Monto Debitos";
                            this.dataGridView1.Columns[4].HeaderText = "Monto Creditos";
                            this.dataGridView1.Columns[5].HeaderText = "Status";
                        }
                    }
                    }
                }
                else
                {

                    tablaComprobantes = compro1.lbxBuscarComprobante(referenciaPrincipal.empresaActual.BaseDatosActual, textBox5.Text);
                    if (tablaComprobantes.Rows.Count == 0)
                    {

                        MessageBox.Show("No se consiguieron Resultados!!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        textBox5.Focus();
                    }
                    else
                    {
                        this.dataGridView1.DataSource = null;
                        this.dataGridView1.DataSource = tablaComprobantes;
                        this.dataGridView1.Columns[0].HeaderText = "Nro Comprobante";
                        this.dataGridView1.Columns[1].HeaderText = "Fecha";
                        this.dataGridView1.Columns[2].HeaderText = "Descripción";
                        this.dataGridView1.Columns[3].HeaderText = "Monto Debitos";
                        this.dataGridView1.Columns[4].HeaderText = "Monto Creditos";
                        this.dataGridView1.Columns[5].HeaderText = "Status";
                    }
                }
            }
            else
            {
                MessageBox.Show("Debe Poner un Dato Para buscar!!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                textBox5.Focus();
            }
        }

        private bool filtrar(string filtro)
        {
            bool centinela = true;
            string sentencia;
            try
            {
                sentencia = "mc_nroComprobante LIKE '%" + filtro + "%'";
                dv.RowFilter = sentencia;
                if (dv.Count == 0)
                {
                    sentencia = "mc_Descripcion LIKE '%" + filtro + "%'";
                    dv.RowFilter = sentencia;
                    if (dv.Count == 0)
                    {
                        centinela = false;
                    }
                    else {
                        dataGridView1.DataSource = dv;
                        dataGridView1.Update();
                    }
                }
                else
                {
                    dataGridView1.DataSource = dv;
                    dataGridView1.Update();
                }

                return centinela;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error buscar " + e.Message);
                return false;
            }
        }

        private void cmdLimpiar_Click(object sender, EventArgs e)
        {
            textBox5.Text = "";
            dataGridView1.DataSource = null;
            cargarComprobantes();
            dataGridView1.Update();
        }

        private void cmdVer_Click(object sender, EventArgs e)
        {
            comprobante = frmComprobanteContable.DefInstance;
            comprobante.MdiParent = referenciaPrincipal;
            if (dataGridView1.Rows.Count>0)
            {
                if (dataGridView1.CurrentRow.Cells[5].Value.ToString().Equals("Procesado") || dataGridView1.CurrentRow.Cells[5].Value.ToString().Equals("Revision"))
                {
                    comprobante.cargarModificar(dataGridView1.CurrentRow.Cells[0].Value.ToString(), 2);
                }
                else
                {
                    comprobante.cargarModificar(dataGridView1.CurrentRow.Cells[0].Value.ToString(), 1);
                }

                this.Hide();
                referenciaPrincipal.toolStripComboBox1.Enabled = false;
                comprobante.formularioVer();
                comprobante.Show();
                comprobante.referenciaLbxC(this);
            }
            else {
                MessageBox.Show("Debe Existir algun regisro para ver!!", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmdSalir_Click(object sender, EventArgs e)
        {
            if (this.MdiParent == null)
            {
                Application.Exit();
            }
            else
            {
                this.Close();
            }
        }

        private void cmdAceptarSupervicion_Click(object sender, EventArgs e)
        {
            if (txtClave.Text != "")
            {

                if (referenciaPrincipal.usuarioActual.validarContrasenaParametrosC(txtClave.Text, referenciaPrincipal.empresaActual.BaseDatosActual))
                {   
                    this.panel1.Visible = false;
                    txtClave.Text = "";

                    if (rbtModificar.Checked)
                    {
                        rbtModificar.Checked = false;
                        
                        comprobante = frmComprobanteContable.DefInstance;
                        comprobante.MdiParent = referenciaPrincipal;
                        comprobante.cargarModificar(dataGridView1.CurrentRow.Cells[0].Value.ToString(),2);
                        referenciaPrincipal.toolStripComboBox1.Enabled = false;
                        //comprobante.mostrarBotones(true);
                        comprobante.guardarModificacionDefinitivo();
                        
                        comprobante.IdBaseDatos = 3; //por ahora
                        comprobante.Tabla = 2;
                        this.Hide();
                        comprobante.Show();
                    }
                    if (rbtEliminar.Checked)
                    {
                        rbtEliminar.Checked = false;

                    }
                }
                else
                {
                    MessageBox.Show("Clave Incorrecta!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else {
                MessageBox.Show("No puede Introduccir una Contraseña Vacia!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmdCancelarSupervicion_Click(object sender, EventArgs e)
        {
            this.panel1.Visible = false;

            if (rbtModificar.Checked) {
                rbtModificar.Checked = false;

            }
            if (rbtEliminar.Checked)
            {
                rbtEliminar.Checked = false;

            }
            txtClave.Clear();
        }
        

        //private void cargarCompanias() {
        //    DataTable tablaComapinas = new DataTable();
        //    tablaComapinas = referenciaPrincipal.empresaActual.obtenerCompanias();

        //    cboCompania.DisplayMember = "empre_nombre";
        //    cboCompania.ValueMember = "empre_codigo";
        //    cboCompania.DataSource = tablaComapinas;

        //    for(int i=0;i<tablaComapinas.Rows.Count;i++){

        //        if (tablaComapinas.Rows[i][2].ToString().Equals("True")) {                
        //            cboCompania.SelectedIndex =cboCompania.FindString(tablaComapinas.Rows[i][0].ToString());                    
        //        }
                
        //    }

        //}

        //private void cboCompania_SelectedIndexChanged(object sender, EventArgs e)
        //{   
        //    referenciaPrincipal.empresaActual.BaseDatosActual = cboCompania.SelectedValue.ToString();
        //    cargarComprobantes();

        //}

        
    }
}
