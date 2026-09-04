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
    public partial class frmAuxiliarContable : Form
    {
        
        private static frmAuxiliarContable m_FormDefInstance;
        private Principal referenciaPrincipal;
        public AuxiliarContable nuevoAuxiliar;
        public lbxAuxiliarContable referenciLbx;
        private DataTable tableCom;
        
        public frmAuxiliarContable()
        {
            InitializeComponent();
            nuevoAuxiliar = new AuxiliarContable();
        }

        //definir una sola Instancia
        public static frmAuxiliarContable DefInstance
        {
            get
            {
                if (m_FormDefInstance == null || m_FormDefInstance.IsDisposed)
                    m_FormDefInstance = new frmAuxiliarContable();
                return m_FormDefInstance;
            }
            set
            {
                m_FormDefInstance = value;
            }
        }

        private void frmAuxiliarContable_Load(object sender, EventArgs e)
        {
            referenciaPrincipal = (Principal)this.MdiParent;
            this.label9.Text = nuevoAuxiliar.Auxiliar1;
            label27.Visible = false;
            cmdModificar.Visible = false;
            //se carga la dt una sola vez
            tableCom = referenciaPrincipal.empresaActual.obtenerCompanias();
            companiasGrid();
        }

        private void cmdSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool validarCampos() {
            bool centinela = true;

            if (txtCodigoAuxiliar.Text == "")
            {
                label1.BackColor = Color.FromArgb(255, 0, 0);
                MessageBox.Show("El Código del Auxilar es Obligatorio", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);               
                centinela = false;
            }

            if (txtNombreAuxiliar.Text == "")
            {
                label6.BackColor = Color.FromArgb(255, 0, 0);
                MessageBox.Show("El Nombre del Auxiliar es Obligatorio", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                centinela = false;
            }
            validarValidated();
            return centinela;

        }

        private void txtCodigoAuxiliar_TextChanged(object sender, EventArgs e)
        {
            label1.BackColor = TransparencyKey;
            validarValidated();
        }

        private void validarValidated()
        {
            if ((label1.BackColor.Name != "Control") || (label6.BackColor.Name != "Control"))
            {
                label27.Visible = true;
            }
            else
            {
                label27.Visible = false;
            }
        }

        private void txtCodigoAuxiliar_Validated(object sender, EventArgs e)
        {
            if (txtCodigoAuxiliar.Text == "") {
                label1.BackColor = Color.FromArgb(255, 0, 0);
                MessageBox.Show("El Código del Auxilar es Obligatorio", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                validarValidated();
            }
            
        }

        private void txtNombreAuxiliar_TextChanged(object sender, EventArgs e)
        {
            label6.BackColor = TransparencyKey;
            validarValidated();
        }

        private void txtNombreAuxiliar_Validated(object sender, EventArgs e)
        {
            if (txtNombreAuxiliar.Text == "")
            {
                label6.BackColor = Color.FromArgb(255, 0, 0);
                MessageBox.Show("El Nombre del Auxiliar es Obligatorio", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                validarValidated();
            }
        }

        private void cmdAgregar_Click(object sender, EventArgs e)
        {
            if (validarCampos()) {
                this.nuevoAuxiliar.CodigoAuxiliar = nuevoAuxiliar.convertirAuxiliar(txtCodigoAuxiliar.Text.Trim());
                this.nuevoAuxiliar.NombreAuxiliar = txtNombreAuxiliar.Text.Trim();
                this.nuevoAuxiliar.ManejaDocumentos = chkManejaDocumentos.Checked;
                companiasHabilitadas();

                if (txtMontoApertura.Text.Length > 0)
                {
                    this.nuevoAuxiliar.MontoApertura = txtMontoApertura.Text.Replace(",", ".");
                }
                else {
                    this.nuevoAuxiliar.MontoApertura = "0";
                }
                if (this.nuevoAuxiliar.agregarAuxiliar(referenciaPrincipal.usuarioActual.Id,referenciaPrincipal.empresaActual.BaseDatosActual))
                {
                    MessageBox.Show("Auxiliar Agregado Exitosamente!!", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    limpiar();
                    limpiarObjeto();
                    this.Close();
                }
            }
        }



        private void limpiar() {
            txtCodigoAuxiliar.Text = "";
            txtNombreAuxiliar.Text = "";
            chkManejaDocumentos.Checked = false;
            label9.Text = "zaaaa";
        }

        private void limpiarObjeto() {
            this.nuevoAuxiliar.Auxiliar1 = null;
            this.nuevoAuxiliar.CodigoAuxiliar = null;
            this.nuevoAuxiliar.NombreAuxiliar = null;
            this.nuevoAuxiliar.ManejaDocumentos = false;
            this.nuevoAuxiliar.CodigoPlanCuentas = null;
        }

        public void cargarDatos(AuxiliarContable auxiliar, lbxAuxiliarContable lbx) {
            label9.Text = auxiliar.Auxiliar1;
            txtCodigoAuxiliar.Text = auxiliar.CodigoAuxiliar;
            txtNombreAuxiliar.Text = auxiliar.NombreAuxiliar;
            chkManejaDocumentos.Checked = auxiliar.ManejaDocumentos;
            referenciLbx = lbx;
            nuevoAuxiliar = auxiliar;
            txtMontoApertura.Text = auxiliar.MontoApertura;
            camposModificar(false);
        }

        public void mostrarDatos(AuxiliarContable auxiliar) {
            label9.Text = auxiliar.Auxiliar1;
            txtCodigoAuxiliar.Text = auxiliar.CodigoAuxiliar;
            txtNombreAuxiliar.Text = auxiliar.NombreAuxiliar;
            chkManejaDocumentos.Checked = auxiliar.ManejaDocumentos;
            txtMontoApertura.Text = auxiliar.MontoApertura;
            nuevoAuxiliar = auxiliar;
            camposVer(false);
        }


        private void camposModificar(bool valor) {
            txtCodigoAuxiliar.Enabled = valor;
            chkManejaDocumentos.Enabled = valor;
            
            if (valor)
            {
                cmdAgregar.Visible = true;
                cmdModificar.Visible = false;
            }
            else {
                cmdAgregar.Visible = false;
                cmdModificar.Visible = true;
            }
        }

        private void camposVer(bool valor) {
            camposModificar(valor);
            cmdAgregar.Visible = valor;
            cmdModificar.Visible = valor;
            txtNombreAuxiliar.Enabled = valor;
            txtMontoApertura.Enabled = valor;
            //dataGridView1.Enabled = valor;
            dataGridView1.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void cmdModificar_Click(object sender, EventArgs e)
        {   
            string controlAuxTemp;
            controlAuxTemp = null;

            this.nuevoAuxiliar.NombreAuxiliar = txtNombreAuxiliar.Text.Trim();
            if (txtMontoApertura.Text.Length > 0)
            {
                this.nuevoAuxiliar.MontoApertura = txtMontoApertura.Text.Replace(",", ".").Trim();
            }
            else {
                this.nuevoAuxiliar.MontoApertura = "0";
            }
            controlAuxTemp = this.nuevoAuxiliar.ControAux;
            companiasHabilitadas();
            if (controlAuxTemp.Equals(this.nuevoAuxiliar.ControAux))
            {

                if (this.nuevoAuxiliar.modificarAuxiliar(referenciaPrincipal.empresaActual.BaseDatosActual))
                {
                    MessageBox.Show("Auxiliar Modificado Exitosamente!!!!", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    limpiar();
                    limpiarObjeto();
                    referenciLbx.mostrarDetalle();
                    this.Close();

                }
            }
            else {
                MessageBox.Show("Ingrese clave de supervision para cambiar las compañias", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                panel1.Visible = true;
                txtClave.Focus();
            }            
        }

        private void frmAuxiliarContable_FormClosed(object sender, FormClosedEventArgs e)
        {
            referenciaPrincipal.toolStripComboBox1.Enabled = true;
        }

        private void txtMontoApertura_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true; SendKeys.Send("{TAB}");
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

            for (int i = 0; i < txtMontoApertura.Text.Length; i++)
            {
                if ((txtMontoApertura.Text[i] == '.') || (txtMontoApertura.Text[i] == ','))
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
            for (i = 0; i < txtMontoApertura.Text.Length; i++)
            {
                if ((txtMontoApertura.Text[i] == ',') || (txtMontoApertura.Text[i] == '.'))
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

        private void txtCodigoAuxiliar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true; SendKeys.Send("{TAB}");
                return;
            }

            if ((e.KeyChar == '.') || (e.KeyChar == ',') || (e.KeyChar == '-'))
            {
                e.Handled = true;
                return;
            }

        }

        private void txtNombreAuxiliar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true; SendKeys.Send("{TAB}");
                return;
            }
        }

        private void chkManejaDocumentos_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true; SendKeys.Send("{TAB}");
                return;
            }
        }
 
        private void cmboAuxiliarCom_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true; SendKeys.Send("{TAB}");
                return;
            }
        }

        private void companiasGrid()
        {   
            DataTable dtTemp;
            DataRow[] fila;            
            string[] arrayComp;

            dtTemp = new DataTable();
            dtTemp.Columns.Add(new DataColumn("empre_nombre",typeof(string)));
            dtTemp.Columns.Add(new DataColumn("empre_codigo",typeof(string)));
            dtTemp.Columns.Add(new DataColumn("empre_actual",typeof(bool)));
            
            arrayComp = this.nuevoAuxiliar.Control.Split('-');
            
                for (int i = 0; i < arrayComp.Length; i++)
                {
                    if ((arrayComp[i] != "") && (arrayComp[i] != "0"))
                    {
                        fila = tableCom.Select("empre_codigo= '" + arrayComp[i] + "'");
                        dtTemp.Rows.Add(fila[0][0], fila[0][1], fila[0][2]);
                    }
                    else {
                        if (arrayComp[i] == "") {
                            break;
                        }
                        if (arrayComp[i] == "0")
                        {
                        dtTemp.Clear();
                        dtTemp = tableCom;
                        break;}
                    }
                }
            dataGridView1.DataSource = dtTemp;
            dataGridView1.Columns[0].Width = 100;
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[2].Visible = false;
            dataGridView1.Columns[3].Visible = false;
        }


        private void companiasHabilitadas()
        {
            int empresas = 0;
            this.nuevoAuxiliar.ControAux = null;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if ((Convert.ToBoolean(dataGridView1.Rows[i].Cells[0].Value)) == false)
                {
                    empresas = empresas + 1;
                    this.nuevoAuxiliar.ControAux = this.nuevoAuxiliar.ControAux + dataGridView1.Rows[i].Cells[2].Value.ToString() + "-";
                }
            }
            
            if ((empresas == 6) && (dataGridView1.Rows.Count == 6))
            {
                this.nuevoAuxiliar.ControAux = "";
                this.nuevoAuxiliar.ControAux = "0";
            }
            if (empresas == 0)
            {
                this.nuevoAuxiliar.ControAux = "";
                this.nuevoAuxiliar.ControAux = "-1";
            }
        }


        public void loadCompaniasHabilitadas()
        {

            string[] arrayComp;
            arrayComp = this.nuevoAuxiliar.ControAux.Split('-');



            if (this.nuevoAuxiliar.ControAux != "0")
            {
                for (int j = 0; j < dataGridView1.Rows.Count; j++)
                {
                    dataGridView1.Rows[j].Cells[0].Value = true;
                }

                for (int j = 0; j < dataGridView1.Rows.Count; j++)
                {
                    for (int i = 0; i < arrayComp.Length; i++)
                    {
                        if (arrayComp[i] != "")
                        {

                            if ((dataGridView1.Rows[j].Cells[2].Value.ToString().Equals(arrayComp[i])))
                            {
                                dataGridView1.Rows[j].Cells[0].Value = false;
                                i = arrayComp.Length;
                            }
                        }
                    }
                }
            }
        }

        private void cmdAceptarSupervicion_Click(object sender, EventArgs e)
        {
            if (txtClave.Text != "")
            {
                if (referenciaPrincipal.usuarioActual.validarContrasenaParametrosC(txtClave.Text, referenciaPrincipal.empresaActual.BaseDatosActual))
                {
                    txtClave.Clear();
                    this.panel1.Visible = false;
                    modificarAuxClave();
                }
                else {
                    MessageBox.Show("Contraseña Erronea", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else { MessageBox.Show("La clave No puede estar Vacia", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void modificarAuxClave(){
            if (this.nuevoAuxiliar.modificarAuxiliar(referenciaPrincipal.empresaActual.BaseDatosActual))
            {
                MessageBox.Show("Auxiliar Modificado Exitosamente!!!!", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                limpiar();
                limpiarObjeto();
                referenciLbx.mostrarDetalle();
                this.Close();

            }
        }

        private void cmdCancelarSupervicion_Click(object sender, EventArgs e)
        {
            txtClave.Clear();
            this.panel1.Visible = false;
        }

        private void frmAuxiliarContable_FormClosing(object sender, FormClosingEventArgs e)
        {
            limpiarObjeto();
            dataGridView1.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2;
        }

    }
}
