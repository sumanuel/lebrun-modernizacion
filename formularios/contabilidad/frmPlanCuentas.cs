using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using lebrun.clases.contabilidad;
using lebrun.clasesData;



namespace lebrun.formularios.contabilidad
{
    public partial class frmPlanCuentas : Form
    {
        private PlanCuentas nuevoPlanC;
        private bool actualizarCombos = true;
        private Principal referenciaPrincipal;
        private static frmPlanCuentas m_FormDefInstance;
        private lbxPlanCuentas referenciaLbxPlanCuentas;
        private FuncionesTexbox textBox;
      

        public frmPlanCuentas()
        {
            InitializeComponent();
            llenarCboGrupo();
        }

        private void frmPlanCuentas_Load(object sender, EventArgs e)
        {

            cboSubGrupo.Enabled = false;
            cboCuenta.Enabled = false;
            cboSubCuenta.Enabled = false;
            txtAuxiliar.Enabled = false;
            label27.Visible = false;
            btnModificar.Visible = false;
            chkAuxiliar.Enabled = false;

            nuevoPlanC = new PlanCuentas();
            textBox = new FuncionesTexbox();
            referenciaPrincipal = (Principal)this.MdiParent;
            companiasGrid();

            //Accefer a elementos del padre
            ////Form1 Nombre_Formulario_1 = (Form1)this.MdiParent;
            //        Nombre_Formulario_1.lblNombreVentana.Visible = true;
            //        Nombre_Formulario_1.btnAcceder.Enabled = false;
        }

        public static frmPlanCuentas DefInstance
        {
            get
            {
                if (m_FormDefInstance == null || m_FormDefInstance.IsDisposed)
                    m_FormDefInstance = new frmPlanCuentas();
                return m_FormDefInstance;
            }
            set
            {
                m_FormDefInstance = value;
            }
        }

        private void llenarCboGrupo()
        {
            cboGrupo.Items.Clear();
            cboGrupo.Items.Add("1");
            cboGrupo.Items.Add("2");
            cboGrupo.Items.Add("3");
            cboGrupo.Items.Add("4");
            cboGrupo.Items.Add("5");
            cboGrupo.Items.Add("6");
        }

        private void cboGrupo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Visible == true)
            {
                if (cboSubGrupo.Enabled == true)
                {

                    reiniciarCombos(1);
                    label9.Text = cboGrupo.Text;

                }
                else
                {
                    if (actualizarCombos)
                    {
                        label9.Text = cboGrupo.Text;
                        llenarCboSubGrupo(cboGrupo.Text);
                        cboSubGrupo.Enabled = true;
                    }
                }
                txtInicialEjercicio.Clear();
                chkAuxiliar.Checked = false;
                txtInicialEjercicio.Enabled = false;
                chkAuxiliar.Enabled = false;
            }
        }

        private void llenarCboSubGrupo(string grupo)
        {
            cboSubGrupo.Items.Clear();
            int i;
            for (i = 1; i <= 9; i++)
            {
                cboSubGrupo.Items.Add(grupo + "." + i);
            }
        }

        private void cboSubGrupo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboSubGrupo.Text != "")
            {
                if (cboCuenta.Enabled == true)
                {

                    reiniciarCombos(2);

                }
                else
                {
                    if (actualizarCombos)
                    {
                        llenarCboCuenta(cboSubGrupo.Text);
                        label9.Text = cboSubGrupo.Text;
                        cboCuenta.Enabled = true;
                    }
                }
                txtInicialEjercicio.Clear();
                chkAuxiliar.Checked = false;
                txtInicialEjercicio.Enabled = false;
                chkAuxiliar.Enabled = false;
            }
        }

        private void llenarCboCuenta(string subGrupo)
        {
            int i;
            cboCuenta.Items.Clear();
            for (i = 1; i <= 9; i++)
            {
                cboCuenta.Items.Add(subGrupo + "." + i);
            }
        }

        private void llenarCboSubCuenta(string cuenta)
        {
            int i;
            cboSubCuenta.Items.Clear();
            for (i = 1; i <= 9; i++)
            {
                cboSubCuenta.Items.Add(cuenta + "." + i);
            }

        }

        private void cboSubCuenta_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboSubCuenta.Text != "")
            {
                if (txtAuxiliar.Enabled)
                {
                    reiniciarCombos(4);
                    label9.Text = cboSubCuenta.Text;
                }
                else
                {
                    if (actualizarCombos)
                    {
                        llenarAuxiliar1(cboSubCuenta.Text);
                        label9.Text = cboSubCuenta.Text;
                    }
                }
                txtInicialEjercicio.Clear();
                chkAuxiliar.Checked = false;
                txtInicialEjercicio.Enabled = false;
                chkAuxiliar.Enabled = false;
            }
        }

        private void llenarAuxiliar1(string subCuenta)
        {
            //5 (0) para el auxiliar 26/04/2012
            txtAuxiliar.Text = "";
            txtAuxiliar.Enabled = true;
        }

        private void cboCuenta_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboCuenta.Text != "")
            {
                if (cboSubCuenta.Enabled == true)
                {
                    reiniciarCombos(3);
                }
                else
                {
                    if (actualizarCombos)
                    {
                        llenarCboSubCuenta(cboCuenta.Text);
                        label9.Text = cboCuenta.Text;
                        cboSubCuenta.Enabled = true;
                    }
                }
                txtInicialEjercicio.Clear();
                chkAuxiliar.Checked = false;
                txtInicialEjercicio.Enabled = false;
                chkAuxiliar.Enabled = false;
            }
        }

        private void reiniciarCombos(int combo)
        {
            switch (combo)
            {
                case 1:
                    cboSubGrupo.Items.Clear();
                    label9.Text = cboGrupo.Text;
                    if (cboGrupo.Text != "")
                    {
                        llenarCboSubGrupo(cboGrupo.Text);
                    }
                    else
                    {
                        cboSubGrupo.Enabled = false;
                    }
                    if (cboCuenta.Enabled == true)
                    {
                        cboCuenta.Enabled = false;
                        reiniciarCombos(2);
                    }
                    break;
                case 2:
                    cboCuenta.Items.Clear();
                    llenarCboCuenta(cboSubGrupo.Text);
                    label9.Text = cboSubGrupo.Text;
                    if (cboSubCuenta.Enabled == true)
                    {
                        cboSubCuenta.Enabled = false;
                        reiniciarCombos(3);
                    }
                    break;
                case 3:
                    cboSubCuenta.Items.Clear();
                    llenarCboSubCuenta(cboCuenta.Text);
                    label9.Text = cboCuenta.Text;
                    if (txtAuxiliar.Enabled == true)
                    {
                        txtAuxiliar.Text = "";
                        txtAuxiliar.Enabled = false;
                    }
                    break;
                case 4:
                    txtAuxiliar.Clear();
                    reiniciarCombos(-1);
                    break;
                case -1:
                    break;
            }
        }

        //para validar numeros con decimal
        private void txtInicialEjercicio_KeyPress(object sender, KeyPressEventArgs e)
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

            for (int i = 0; i < txtInicialEjercicio.Text.Length; i++)
            {
                if ((txtInicialEjercicio.Text[i] == '.') || (txtInicialEjercicio.Text[i] == ','))
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
            for (i = 0; i < txtInicialEjercicio.Text.Length; i++)
            {
                if ((txtInicialEjercicio.Text[i] == ',') || (txtInicialEjercicio.Text[i] == '.'))
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

        //pasar tab con enter en todos los text designados
        private void MyTextBox_KeyPress(Object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true; SendKeys.Send("{TAB}");
            }

            //textBox.sinCharEspeciales(sender, e);

        }

        private void cmdAgregar_Click(object sender, EventArgs e)
        {
            if (validarCampos())
            {
                companiasHabilitadas();
                string contenedor = txtInicialEjercicio.Text;
                nuevoPlanC.reversarCodigo(label9.Text);
                nuevoPlanC.NombreCuenta = txtNombreCuenta.Text;
                nuevoPlanC.Siglas = txtSiglas.Text;
                if (txtInicialEjercicio.Text != "")
                {
                    nuevoPlanC.SaldoInicialEjercicio = txtInicialEjercicio.Text.Replace('.', ',');
                }
                else
                {
                    nuevoPlanC.SaldoInicialEjercicio = "0";
                }
                nuevoPlanC.ImprimeDiarios = chckImprimeDiario.Checked;
                nuevoPlanC.ImprimeBalance = chkImprimeBalance.Checked;
                nuevoPlanC.ManejaAuxiliar = chkAuxiliar.Checked;
                nuevoPlanC.nivelDeTotal(label9.Text.Length, label9.Text);
                if (validarGrupos(label9.Text.Length))
                {
                    if (nuevoPlanC.ingresarPlan(referenciaPrincipal.empresaActual.BaseDatosActual, referenciaPrincipal.usuarioActual.Id, referenciaPrincipal.empresaActual.BaseDatosActual))
                    {
                        limpiarCampos();
                    }
                }
            }
        }

        private bool validarCampos()
        {
            bool centinela = true;
            if (cboGrupo.Text == "")
            {
                label1.BackColor = Color.FromArgb(255, 0, 0);
                MessageBox.Show("Debe por lo menos escojer un Grupo para Crear el plan de cuentas", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                centinela = false;
            }
            else { label1.BackColor = TransparencyKey; }
            if (txtNombreCuenta.Text == "")
            {
                label6.BackColor = Color.FromArgb(255, 0, 0);
                MessageBox.Show("El nombre de la cuenta es un campo Obligatorio", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                centinela = false;
            }
            else { label6.BackColor = TransparencyKey; }
            //if (txtSiglas.Text == "")
            //{
            //    label7.BackColor = Color.FromArgb(255, 0, 0);
            //    MessageBox.Show("Las Siglas son un campo Obligatorio", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    centinela = false;
            //}else{ label7.BackColor = TransparencyKey; }
            //por los cambios realizados hay que validar el txtAuxiliar
            if (txtAuxiliar.Text != "")
            {
                if (((Convert.ToDouble(txtAuxiliar.Text)) < 1) && ((Convert.ToDouble(txtAuxiliar.Text)) > 99999))
                {
                    MessageBox.Show("El Auxiliar debe tener un valor desde 1 hasta 99999");
                    centinela = false;
                }
            }

            if (!(centinela))
            {
                label27.Visible = true;
            }
            else
            {
                label27.Visible = false;
            }
            return centinela;
        }

        private void txtNombreCuenta_Validated(object sender, EventArgs e)
        {
            if (txtNombreCuenta.Text == "")
            {
                label6.BackColor = Color.FromArgb(255, 0, 0);
                MessageBox.Show("El Campo Nombre de la cuenta es Obligatorio", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                validarValidated();
            }
        }

        //private void txtSiglas_Validated(object sender, EventArgs e)
        //{
        //    if (txtSiglas.Text == "") 
        //    {
        //        label7.BackColor = Color.FromArgb(255, 0, 0);
        //        MessageBox.Show("El Campo Siglas es Obligatorio", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //        validarValidated();
        //    }
        //}

        private void validarValidated()
        {
            if ((label1.BackColor.Name != "Control") || (label6.BackColor.Name != "Control") || (label7.BackColor.Name != "Control"))
            {
                label27.Visible = true;
            }
            else
            {
                label27.Visible = false;
            }
        }

        private void txtNombreCuenta_TextChanged(object sender, EventArgs e)
        {
            label6.BackColor = TransparencyKey;
            validarValidated();
        }

        //private void txtSiglas_TextChanged(object sender, EventArgs e)
        //{
        //    label7.BackColor = TransparencyKey;
        //    validarValidated();
        //}

        private void limpiarCampos()
        {
            txtNombreCuenta.Text = "";
            txtSiglas.Text = "";
            txtInicialEjercicio.Text = "";
            chckImprimeDiario.Checked = false;
            chkImprimeBalance.Checked = false;
            chkAuxiliar.Checked = false;
            llenarCboGrupo();
            reiniciarCombos(1);
            actualizarCombos = true;
            chkAuxiliar.Checked = false;
            chkAuxiliar.Enabled = false;
            txtInicialEjercicio.Clear();
            txtInicialEjercicio.Enabled = false;

        }

        private bool validarGrupos(int longitudCadena)
        {
            bool centinela = true;
            switch (longitudCadena)
            {
                case 13:
                    centinela = validarGrupos(9);
                    break;
                case 12:
                    centinela = validarGrupos(9);
                    break;
                case 11:
                    centinela = validarGrupos(9);
                    break;
                case 10:
                    centinela = validarGrupos(9);
                    break;
                case 9:
                    if (nuevoPlanC.Auxiliar1 != "00")
                    {
                        if ((nuevoPlanC.validarPlanCuenta(10, referenciaPrincipal.empresaActual.BaseDatosActual)))
                        {
                            MessageBox.Show("El Auxiliar1 para este Plan de Cuentas Existe! Por favor Elija otro ", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            centinela = false;
                            break;
                        }
                        else
                        {
                            if (!nuevoPlanC.validarPlanCuenta(7, referenciaPrincipal.empresaActual.BaseDatosActual))
                            {
                                MessageBox.Show("El Sub Cuenta para este Plan de Cuentas No Existe!Debe Crearlo Primero ", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                txtAuxiliar.Text = "";
                                txtAuxiliar.Enabled = false;
                                label9.Text = cboSubCuenta.Text;
                                txtInicialEjercicio.Clear();
                                txtInicialEjercicio.Enabled = false;
                                chkAuxiliar.Checked = false;
                                chkAuxiliar.Enabled = false;
                                centinela = false;
                                break;
                            }
                        }
                    }
                    else
                    {
                        validarGrupos(7);
                    }

                    break;
                case 7:
                    if ((label9.Text.Length >= 7))
                    {
                        if (nuevoPlanC.SubCuenta != "0")
                        {
                            /* debido al cambios de plan de cuenta esta validacion 
                                quedara condicionada se valida que el text si esta 
                             * vacio quiere decir que esta creando una subcuenta,
                             * si no esta vacio esta creando un auxiliar*/
                            if (txtAuxiliar.Text != "")
                            {
                                //si no existe esta malo
                                if (!(nuevoPlanC.validarPlanCuenta(7, referenciaPrincipal.empresaActual.BaseDatosActual)))
                                {
                                    MessageBox.Show("La Subcuenta para este Plan de Cuentas No Existe! Debe Crearlo Primero ", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    txtAuxiliar.Text = "";
                                    txtAuxiliar.Enabled = false;
                                    label9.Text = cboSubCuenta.Text;
                                    txtInicialEjercicio.Clear();
                                    txtInicialEjercicio.Enabled = false;
                                    centinela = false;
                                    validarGrupos(5);
                                    break;
                                }
                            }
                            else
                            {
                                if (!(nuevoPlanC.validarPlanCuenta(7, referenciaPrincipal.empresaActual.BaseDatosActual)))
                                {   
                                    if (!(nuevoPlanC.validarPlanCuenta(5, referenciaPrincipal.empresaActual.BaseDatosActual)))
                                    {
                                        MessageBox.Show("La Cuenta para este Plan de Cuentas No Existe! ", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        cboSubCuenta.Items.Clear();
                                        cboSubCuenta.Enabled = false;
                                        txtAuxiliar.Text = "";
                                        txtAuxiliar.Enabled = false;
                                        label9.Text = cboCuenta.Text;
                                        centinela = false;
                                        break;
                                    }
                                    else { break; }
                                }
                                else
                                {
                                    MessageBox.Show("La Subcuenta para este Plan de Cuentas Existe! ", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    txtAuxiliar.Text = "";
                                    txtAuxiliar.Enabled = false;
                                    label9.Text = cboSubCuenta.Text;
                                    centinela = false;
                                    validarGrupos(5);
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        /*se pone no Nuevamente porque se esta 
                         * creando un nuevo Nivel 4
                         * se su pone que si existe el Nivel 3 
                         esta bien, si no hay un error*/
                        if (!(nuevoPlanC.validarPlanCuenta(5, referenciaPrincipal.empresaActual.BaseDatosActual)))
                        {
                            MessageBox.Show("La Cuenta para este Plan de Cuentas No Existe! Debe Crearlo Primero", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            cboSubCuenta.Items.Clear();
                            cboSubCuenta.Enabled = false;
                            label9.Text = cboCuenta.Text;
                            centinela = false;
                            break;
                        }
                        else
                        {
                            /* ya aqui para terminar la valiacion en caso de crear
                             * un nivel 4 hay que validar si el mismo existe si es asi
                             error*/
                            if (!(nuevoPlanC.validarPlanCuenta(7, referenciaPrincipal.empresaActual.BaseDatosActual)))
                            {
                                return true;
                            }
                            else
                            {
                                MessageBox.Show("La Subcuenta para este Plan de Cuentas Existe! Por favor Elija otro", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                centinela = false;
                                break;
                            }

                        }
                    }
                    // de todas maneras bueno o Malo debo Validar Todos los Demas!

                    break;
                case 5:
                    if (label9.Text.Length > 5)
                    {
                        if (nuevoPlanC.Cuenta != "0")
                        {
                            if (!(nuevoPlanC.validarPlanCuenta(5, referenciaPrincipal.empresaActual.BaseDatosActual)))
                            {
                                MessageBox.Show("La Cuenta para este Plan de Cuentas No Existe! Debe Crearlo Primero ", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                cboSubCuenta.Items.Clear();
                                cboSubCuenta.Enabled = false;
                                label9.Text = cboCuenta.Text;
                                txtInicialEjercicio.Clear();
                                txtInicialEjercicio.Enabled = false;
                                centinela = false;
                                // de todas maneras bueno o Malo debo Validar Todos los Demas!
                                validarGrupos(3);
                                break;
                            }

                        }
                    }
                    else
                    {
                        if (nuevoPlanC.validarPlanCuenta(5, referenciaPrincipal.empresaActual.BaseDatosActual))
                        {
                            MessageBox.Show("La Cuenta para este Plan de Cuentas Existe!  Por favor Elija otro", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            cboSubCuenta.Items.Clear();
                            cboSubCuenta.Enabled = false;
                            label9.Text = cboCuenta.Text;
                            centinela = false;
                            break;
                        }
                        else
                        {
                            if (!(nuevoPlanC.validarPlanCuenta(3, referenciaPrincipal.empresaActual.BaseDatosActual)))
                            {
                                MessageBox.Show("El sub Grupo para este Plan de Cuentas No Existe! Debe Crearlo Primero", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                cboCuenta.Items.Clear();
                                cboCuenta.Enabled = false;
                                label9.Text = cboSubGrupo.Text;
                                centinela = false;
                                break;
                            }
                            else
                            {
                                return true;
                            }
                        }
                    }

                    break;
                case 3:
                    if (nuevoPlanC.SubGrupo != "0")
                    {
                        if (label9.Text.Length > 3)
                        {
                            if (!(nuevoPlanC.validarPlanCuenta(3, referenciaPrincipal.empresaActual.BaseDatosActual)))
                            {
                                MessageBox.Show("El SubGrupo para este Plan de Cuentas No Existe! Debe Crearlo Primero ", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                cboCuenta.Items.Clear();
                                cboCuenta.Enabled = false;
                                label9.Text = cboSubGrupo.Text;
                                centinela = false;
                                validarGrupos(1);
                                break;
                            }
                        }
                        else
                        {
                            if ((nuevoPlanC.validarPlanCuenta(3, referenciaPrincipal.empresaActual.BaseDatosActual)))
                            {
                                MessageBox.Show("El subGrupo para este Plan de Cuentas Existe!  Por favor Elija otro ", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                cboCuenta.Items.Clear();
                                cboCuenta.Enabled = false;
                                label9.Text = cboGrupo.Text;
                                centinela = false;
                                break;
                            }
                            else
                            {
                                if (!(nuevoPlanC.validarPlanCuenta(1, referenciaPrincipal.empresaActual.BaseDatosActual)))
                                {
                                    MessageBox.Show("El SubGrupo para este Plan de Cuentas No Existe!  Debe Crearlo Primero ", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    cboSubGrupo.Items.Clear();
                                    cboSubGrupo.Enabled = false;
                                    label9.Text = cboGrupo.Text;
                                    centinela = false;
                                    break;
                                }
                            }
                        }
                    }

                    break;
                case 1:
                    if (nuevoPlanC.Grupo != "0")
                    {
                        if ((nuevoPlanC.validarPlanCuenta(1, referenciaPrincipal.empresaActual.BaseDatosActual)))
                        {
                            MessageBox.Show("El Grupo para este Plan de Cuentas Existe! Por favor Elija otro", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            cboSubGrupo.Items.Clear();
                            cboSubGrupo.Enabled = false;
                            label9.Text = cboGrupo.Text;
                            centinela = false;
                            break;
                        }
                    }
                    break;
            }
            actualizarCombos = centinela;
            return centinela;
        }

        public void cargarPlan(PlanCuentas planCargar, lbxPlanCuentas lbxPadre)
        {
            referenciaLbxPlanCuentas = (lbxPlanCuentas)lbxPadre;
            cboGrupo.Text = planCargar.Grupo;
            cboSubGrupo.Text = planCargar.SubGrupo;
            cboCuenta.Text = planCargar.Cuenta;
            cboSubCuenta.Text = planCargar.SubCuenta;
            txtAuxiliar.Text = planCargar.Auxiliar1;
            txtNombreCuenta.Text = planCargar.NombreCuenta;
            txtSiglas.Text = planCargar.Siglas;
            txtInicialEjercicio.Text = planCargar.SaldoInicialEjercicio.Replace(',', '.');
            chckImprimeDiario.Checked = planCargar.ImprimeDiarios;
            chkImprimeBalance.Checked = planCargar.ImprimeBalance;
            if (txtAuxiliar.Text != "")
            {
                chkAuxiliar.Checked = planCargar.ManejaAuxiliar;
            }
            else {
                chkAuxiliar.Enabled = false;
            }
            btnModificar.Visible = true;
            this.nuevoPlanC.CodigoPlanCuentas = planCargar.CodigoPlanCuentas;
            this.nuevoPlanC.Control = planCargar.Control;
        }

        public void cargarPlan(PlanCuentas planCargar, lbxPlanCuentas lbxPadre, bool val)
        {
            cargarPlan(planCargar, lbxPadre);
            btnModificar.Visible = val;
            txtNombreCuenta.Enabled = val;
            txtSiglas.Enabled = val;
            txtInicialEjercicio.Enabled = val;
            chckImprimeDiario.Enabled = val;
            chkImprimeBalance.Enabled = val;
            chkAuxiliar.Enabled = val;
            dataGridView1.EditMode = DataGridViewEditMode.EditProgrammatically;
        }


        public void habilitarModificar(bool val)
        {
            cboGrupo.Enabled = val;
            cboSubGrupo.Enabled = val;
            cboCuenta.Enabled = val;
            cboSubCuenta.Enabled = val;
            txtAuxiliar.Enabled = val;
            cmdAgregar.Visible = val;

        }

        public void habilitarModificar2(bool val)
        {
            txtNombreCuenta.Enabled = val;
            txtSiglas.Enabled = val;
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            string compTemp = null;
            if (validarCampos())
            {
                this.nuevoPlanC.NombreCuenta = txtNombreCuenta.Text;
                this.nuevoPlanC.Siglas = txtSiglas.Text;
                if (txtInicialEjercicio.Text != "")
                {
                    this.nuevoPlanC.SaldoInicialEjercicio = txtInicialEjercicio.Text.Replace('.', ',');
                }
                else
                {
                    this.nuevoPlanC.SaldoInicialEjercicio = "0";
                }
                this.nuevoPlanC.ImprimeDiarios = chckImprimeDiario.Checked;
                this.nuevoPlanC.ImprimeBalance = chkImprimeBalance.Checked;
                this.nuevoPlanC.ManejaAuxiliar = chkAuxiliar.Checked;
                this.nuevoPlanC.Control = compTemp;
                companiasHabilitadas();

                if (this.nuevoPlanC.Control.Equals(compTemp))
                {
                    if (this.nuevoPlanC.modificarPlan(referenciaPrincipal.empresaActual.BaseDatosActual, this.referenciaPrincipal.usuarioActual.Id, referenciaPrincipal.empresaActual.BaseDatosActual))
                    {
                        this.nuevoPlanC.limpiarObjeto();
                        referenciaLbxPlanCuentas.cargarLbx();
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Ingrese clave de supervision para cambiar las compañias", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.panel1.Visible = true;
                    txtClave.Focus();

                    /*if (this.nuevoPlanC.modificarPlan(referenciaPrincipal.empresaActual.BaseDatosActual, this.referenciaPrincipal.usuarioActual.Id, referenciaPrincipal.empresaActual.BaseDatosActual))
                    {
                        this.nuevoPlanC.limpiarObjeto();
                        referenciaLbxPlanCuentas.cargarLbx();
                        this.Close();
                    }*/
                }
            }
        }

        private void cmdSalir_Click(object sender, EventArgs e)
        {
            //referenciaPrincipal.toolStripComboBox1.Enabled = true;
            this.Close();
        }

        private void frmPlanCuentas_FormClosed(object sender, FormClosedEventArgs e)
        {
            referenciaPrincipal.toolStripComboBox1.Enabled = true;
            //referenciaPrincipal.lxPlanC.recargarlbx();
            if (referenciaLbxPlanCuentas != null) {
                referenciaLbxPlanCuentas.recargarlbx();
            }
        }

        private void frmPlanCuentas_Activated(object sender, EventArgs e)
        {
            if (!referenciaPrincipal.empresaActual.BaseDatosActual.Equals("01"))
            {
                chckImprimeDiario.Visible = false;
                chkImprimeBalance.Visible = false;
                //chkAuxiliar.Visible = false;
            }
            else
            {
                chckImprimeDiario.Visible = true;
                chkImprimeBalance.Visible = true;
                //chkAuxiliar.Visible = true;
            }
        }

        private void txtAuxiliar_Leave(object sender, EventArgs e)
        {
            if (txtAuxiliar.Text.Trim() != "")
            {
                label9.Text = cboSubCuenta.Text + "." + txtAuxiliar.Text;
                chkAuxiliar.Enabled = true;
                txtInicialEjercicio.Enabled = true;
            }
            else {
                chkAuxiliar.Enabled = false;
                txtInicialEjercicio.Enabled = false;
            }
        }

        private void txtAuxiliar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true; SendKeys.Send("{TAB}");
                return;
            }


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
        }


        private void cboGrupo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true; SendKeys.Send("{TAB}");
            }
        }

        private void cboSubGrupo_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true; SendKeys.Send("{TAB}");
            }
        }

        private void cboCuenta_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true; SendKeys.Send("{TAB}");
            }
        }

        private void cboSubCuenta_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true; SendKeys.Send("{TAB}");
            }
        }

        private void cmboPlanCompanias_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true; SendKeys.Send("{TAB}");
                return;
            }
        }

        private void companiasGrid()
        {
            DataTable tableCom;
            tableCom = referenciaPrincipal.empresaActual.obtenerCompanias();
            dataGridView1.DataSource = tableCom;
            dataGridView1.Columns[0].Width = 100;
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[2].Visible = false;
            dataGridView1.Columns[3].Visible = false;
        }

        private void companiasHabilitadas()
        {
            int empresas = 0;
            this.nuevoPlanC.Control = "";
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if ((Convert.ToBoolean(dataGridView1.Rows[i].Cells[0].Value)) == false)
                {
                    empresas = empresas + 1;
                    this.nuevoPlanC.Control = this.nuevoPlanC.Control + dataGridView1.Rows[i].Cells[2].Value.ToString() + "-";
                }
            }
            if (empresas == 6)
            {
                this.nuevoPlanC.Control = "";
                this.nuevoPlanC.Control = "0";
            }
            if (empresas == 0)
            {
                this.nuevoPlanC.Control = "";
                this.nuevoPlanC.Control = "-1";
            }
        }

        public void loadCompaniasHabilitadas()
        {

            string[] arrayComp;
            arrayComp = nuevoPlanC.Control.Split('-');



            if (nuevoPlanC.Control != "0")
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
            /*else {
                if (nuevoPlanC.Control == "-1") {
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        dataGridView1.Rows[i].Cells[0].Value = true;
                    }
                }
            }*/
        }

        private void cmdAceptarSupervicion_Click(object sender, EventArgs e)
        {
            if (txtClave.Text == "")
            {
                MessageBox.Show("La clave No puede estar Vacia", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (referenciaPrincipal.usuarioActual.validarContrasenaParametrosC(txtClave.Text, referenciaPrincipal.empresaActual.BaseDatosActual))
                {
                    txtClave.Clear();
                    this.panel1.Visible = false;
                    modificarConClaveSu();
                    
                }
                else
                {
                    MessageBox.Show("Contraseña Erronea", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void modificarConClaveSu()
        {
            if (this.nuevoPlanC.modificarPlan(referenciaPrincipal.empresaActual.BaseDatosActual, this.referenciaPrincipal.usuarioActual.Id, referenciaPrincipal.empresaActual.BaseDatosActual))
            {
                this.nuevoPlanC.limpiarObjeto();
                referenciaLbxPlanCuentas.cargarLbx();
                this.Close();
            }
        }

        private void cmdCancelarSupervicion_Click(object sender, EventArgs e)
        {
            txtClave.Clear();
            this.panel1.Visible = false;
        }

        private void frmPlanCuentas_FormClosing(object sender, FormClosingEventArgs e)
        {
            dataGridView1.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2;
            this.nuevoPlanC.limpiarObjeto();
        }

        public void referenciaLbx(lbxPlanCuentas reft) {
            referenciaLbxPlanCuentas = reft;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    
    }
}