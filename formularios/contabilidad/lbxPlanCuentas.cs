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
using lebrun.formularios.tesoreria;
using lebrun.formularios.importaciones;
using lebrun.formularios.bancos;
using lebrun.formularios.compras;

namespace lebrun.formularios.contabilidad
{
    public partial class lbxPlanCuentas : Form
    {
        private PlanCuentas pcBuscado;
        private DataTable dtOrdenado;
        private Principal referenciaPrincipal;
        private DataView dv;
        private static lbxPlanCuentas m_FormDefInstance;
        private frmPlanCuentas refPlanCuentas;
        private frmRetIvaCli refIvaCli;
        private frmCuentasBanc refCuentasBan;
        private frmProcesoPagoManual refPagoManual;
        private frmCruceDeSaldo refCruce;
        //jesus
        private int fuente = 0;
        private frmPagare refPagare;
        private frmRecibodeIngreso refRecibo;
        private frmServicios refServicio;
        private string compania;
        private string retorno;

        private frmFacturaNacionalImportacion refFacturaNacional;
        private string txtCuenta;

        public lbxPlanCuentas()
        {
            InitializeComponent();
        }

        private void lbxPlanCuentas_Load(object sender, EventArgs e)
        {
            pcBuscado = new PlanCuentas();
            referenciaPrincipal = (Principal)this.MdiParent;
            cargarLbx();
            dv = new DataView(dtOrdenado);
            
        }

        public static lbxPlanCuentas DefInstance
        {
            get
            {
                if (m_FormDefInstance == null || m_FormDefInstance.IsDisposed)
                    m_FormDefInstance = new lbxPlanCuentas();
                return m_FormDefInstance;
            }
            set
            {
                m_FormDefInstance = value;
            }
        }

        public void cargarLbx(){
           /*
             * 01-05-2012
             * Importante: Debido a la sugerencia del Lic Orochena, de colocar 
             * las compañias que NO se deben ver, en la base de datos se esta guardando
             * en formato 01-02-03 donde 01,02,03 es el codigo de la compañia definido
             * en sysconta en la tabla confdatos empresa, las compañias que SI van
           */

            if (fuente == 0)
            {

                DataColumn columna = new DataColumn("codigo", typeof(string));
                dtOrdenado = pcBuscado.lbxPCCargado(referenciaPrincipal.empresaActual.BaseDatosActual);
                dtOrdenado.Columns.Add(columna);
                for (int i = 0; i < dtOrdenado.Rows.Count; i++)
                {
                    dtOrdenado.Rows[i]["codigo"] = dtOrdenado.Rows[i]["cc_CodigoCuenta"].ToString();
                    dtOrdenado.Rows[i]["cc_CodigoCuenta"] = pcBuscado.cargarCodigo(dtOrdenado.Rows[i]["cc_CodigoCuenta"].ToString());
                }
                //se muestra solo los datos necesarios para el usuario
                dataGridView1.DataSource = dtOrdenado;
                dataGridView1.Columns[0].HeaderText = "Código";
                dataGridView1.Columns[1].HeaderText = "Descripción";
                dataGridView1.Columns[2].HeaderText = "Nivel de Total";
                //los demas se ocultan
                dataGridView1.Columns[3].Visible = false;
                dataGridView1.Columns[4].Visible = false;
                dataGridView1.Columns[5].Visible = false;
                dataGridView1.Columns[6].Visible = false;
                dataGridView1.Columns[7].Visible = false;
                dataGridView1.Columns[8].Visible = false;
                dataGridView1.Columns[9].Visible = false;
            }

            if (fuente == 2)
            {

                DataColumn columna = new DataColumn("codigo", typeof(string));
                dtOrdenado = pcBuscado.lbxPCCargado(compania);
                dtOrdenado.Columns.Add(columna);
                for (int i = 0; i < dtOrdenado.Rows.Count; i++)
                {
                    dtOrdenado.Rows[i]["codigo"] = dtOrdenado.Rows[i]["cc_CodigoCuenta"].ToString();
                    dtOrdenado.Rows[i]["cc_CodigoCuenta"] = pcBuscado.cargarCodigo(dtOrdenado.Rows[i]["cc_CodigoCuenta"].ToString());
                }
                //se muestra solo los datos necesarios para el usuario
                dataGridView1.DataSource = dtOrdenado;
                dataGridView1.Columns[0].HeaderText = "Código";
                dataGridView1.Columns[1].HeaderText = "Descripción";
                dataGridView1.Columns[2].HeaderText = "Nivel de Total";
                //los demas se ocultan
                dataGridView1.Columns[3].Visible = false;
                dataGridView1.Columns[4].Visible = false;
                dataGridView1.Columns[5].Visible = false;
                dataGridView1.Columns[6].Visible = false;
                dataGridView1.Columns[7].Visible = false;
                dataGridView1.Columns[8].Visible = false;
                dataGridView1.Columns[9].Visible = false;
            }

        }

        private void cmdAgregar_Click(object sender, EventArgs e)
        {
            refPlanCuentas = frmPlanCuentas.DefInstance;
            refPlanCuentas.MdiParent = referenciaPrincipal;
            refPlanCuentas.Show();
            refPlanCuentas.referenciaLbx(this);

            //frmPlanCuentas.DefInstance.MdiParent = referenciaPrincipal  ;
            //frmPlanCuentas.DefInstance.Show();
            //referenciaPrincipal.toolStripComboBox1.Enabled = false;
        }

        private void cmdBotonModificar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                pcBuscado.NombreCuenta = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["cc_Descripcion"].Value.ToString();
                pcBuscado.CodigoPlanCuentas = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["codigo"].Value.ToString();
                pcBuscado.reversarAuxilar(pcBuscado.CodigoPlanCuentas);
                pcBuscado.reversarSubCuenta(pcBuscado.CodigoPlanCuentas);
                pcBuscado.reversarCuenta(pcBuscado.CodigoPlanCuentas);
                pcBuscado.reversarSubGrupo(pcBuscado.CodigoPlanCuentas);
                pcBuscado.reversarGrupo(pcBuscado.CodigoPlanCuentas);
                pcBuscado.ManejaAuxiliar = Convert.ToBoolean(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["cc_FlagAux"].Value);
                pcBuscado.ImprimeBalance = Convert.ToBoolean(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["cc_FlagImpBal"].Value);
                pcBuscado.ImprimeDiarios = Convert.ToBoolean(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["cc_FlagImpDia"].Value);
                pcBuscado.SaldoInicialEjercicio = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["cc_SaldoIniEjerc"].Value.ToString();
                pcBuscado.Siglas = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["cc_Siglas"].Value.ToString();
                pcBuscado.Control = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["cc_control"].Value.ToString();


                frmPlanCuentas.DefInstance.MdiParent = this.MdiParent;
                frmPlanCuentas.DefInstance.Show();
                frmPlanCuentas.DefInstance.cargarPlan(pcBuscado, this);
                frmPlanCuentas.DefInstance.loadCompaniasHabilitadas();
                frmPlanCuentas.DefInstance.habilitarModificar(false);

                if (pcBuscado.tieneMovimientos(referenciaPrincipal.empresaActual.BaseDatosActual, pcBuscado.CodigoPlanCuentas))
                {
                    frmPlanCuentas.DefInstance.chkAuxiliar.Enabled = false;
                }
                else
                {
                    if (pcBuscado.poseeAuxilar(referenciaPrincipal.empresaActual.BaseDatosActual, pcBuscado.CodigoPlanCuentas))
                    {
                        frmPlanCuentas.DefInstance.chkAuxiliar.Enabled = false;
                    }
                    /*else
                    {
                        frmPlanCuentas.DefInstance.chkAuxiliar.Enabled = true;
                    }*/
                }


                if (referenciaPrincipal.empresaActual.BaseDatosActual != "01")
                {
                    frmPlanCuentas.DefInstance.habilitarModificar2(false);
                }


                pcBuscado.limpiarObjeto();
                referenciaPrincipal.toolStripComboBox1.Enabled = false;
            }
            else {
                MessageBox.Show("Debe Seleccionar un registro con  datos!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmdVer_Click(object sender, EventArgs e)
        {   
            if (dataGridView1.Rows.Count > 0)
            {
                pcBuscado.NombreCuenta = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["cc_Descripcion"].Value.ToString();
                pcBuscado.CodigoPlanCuentas = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["codigo"].Value.ToString();
                pcBuscado.reversarAuxilar(pcBuscado.CodigoPlanCuentas);
                pcBuscado.reversarSubCuenta(pcBuscado.CodigoPlanCuentas);
                pcBuscado.reversarCuenta(pcBuscado.CodigoPlanCuentas);
                pcBuscado.reversarSubGrupo(pcBuscado.CodigoPlanCuentas);
                pcBuscado.reversarGrupo(pcBuscado.CodigoPlanCuentas);
                pcBuscado.ManejaAuxiliar = Convert.ToBoolean(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["cc_FlagAux"].Value);
                pcBuscado.ImprimeBalance = Convert.ToBoolean(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["cc_FlagImpBal"].Value);
                pcBuscado.ImprimeDiarios = Convert.ToBoolean(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["cc_FlagImpDia"].Value);
                pcBuscado.SaldoInicialEjercicio = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["cc_SaldoIniEjerc"].Value.ToString();
                pcBuscado.Siglas = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["cc_Siglas"].Value.ToString();
                pcBuscado.Control = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["cc_control"].Value.ToString();
                frmPlanCuentas.DefInstance.MdiParent = this.MdiParent;
                frmPlanCuentas.DefInstance.Show();
                frmPlanCuentas.DefInstance.cargarPlan(pcBuscado, this,false);
                frmPlanCuentas.DefInstance.habilitarModificar(false);
                frmPlanCuentas.DefInstance.loadCompaniasHabilitadas();
                frmPlanCuentas.DefInstance.chkAuxiliar.Enabled = false;
                pcBuscado.limpiarObjeto();
                referenciaPrincipal.toolStripComboBox1.Enabled = false;
            }
            else
            {
                MessageBox.Show("Debe Seleccionar un registro con  datos!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmdBuscar_Click(object sender, EventArgs e)
        {
            if (txtBuscar.Text != "")
            {
                filtrar(txtBuscar.Text);
            }
            else {
                MessageBox.Show("Debe Poner un Dato Para buscar!!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtBuscar.Focus();
            }
        }

        private void filtrar(string filtro) {
            string sentencia;
            string codigo;
            int i = 0;
            bool centinela = false;

            while (i < filtro.Length) {
                if (IsNumeric(filtro[i]))
                {
                    i++;
                    centinela = true;
                }
                else {
                    if (filtro[i].Equals('.'))
                    {
                        i++;
                    }
                    else {
                        centinela = false;
                        break;
                    }
                }
            }

            if (centinela)
            {
                pcBuscado.reversarCodigo(filtro);
                codigo = pcBuscado.CodigoPlanCuentas;
                pcBuscado.limpiarObjeto();
            }
            else {
                codigo = filtro;
            }
            
            try
            {
                sentencia = "codigo LIKE '" + codigo + "%'";
                dv.RowFilter = sentencia;
                if (dv.Count == 0) {
                    sentencia = "cc_Descripcion LIKE '" + codigo + "%'";
                    dv.RowFilter = sentencia;
                }
                dataGridView1.DataSource = dv;
                dataGridView1.Update();
            }
            catch (Exception e) {
                MessageBox.Show("Error buscar " + e.Message);
            }
        }

        private void cmdLimpiarBusqueda_Click(object sender, EventArgs e)
        {
            txtBuscar.Text = "";
            dataGridView1.DataSource = dtOrdenado;
            dataGridView1.Update();
        }

        private void txtBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmdBuscar.Focus();
            }
        }

        private void lbxPlanCuentas_Activated(object sender, EventArgs e)
        {
            //if (!referenciaPrincipal.empresaActual.BaseDatosActual.Equals("01"))
            //{
            //    cmdAgregar.Visible = false;
            //}
            //else
            //{
            //    cmdAgregar.Visible = true;
            //}

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

            if (fuente == 2)
            {

                if (!compania.Equals("01"))
                {
                    cmdAgregar.Visible = false;
                }
                else
                {
                    cmdAgregar.Visible = true;
                }
            }
        }

        public void recargarlbx() 
        {
            pcBuscado = new PlanCuentas();
            referenciaPrincipal = (Principal)this.MdiParent;
            cargarLbx();
            dv = new DataView(dtOrdenado);

        }

        public bool IsNumeric(object Expression)
        {
            bool isNum;
            double retNum;

            isNum = Double.TryParse(Convert.ToString(Expression), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
            return isNum;
        }

        //jesus 17-07-2013

        public void soloVista(Form referencia, string opcion, string com)
        {

            if (opcion.Equals("frmPagare"))
            {
                fuente = 2;
                compania = com;
                refPagare = (frmPagare)referencia;
                retorno = "frmPagare";
            }

            if (opcion.Equals("frmRecibodeIngreso"))
            {
                fuente = 2;
                compania = com;
                refRecibo = (frmRecibodeIngreso)referencia;
                retorno = "frmRecibodeIngreso";
            }
            if (opcion.Equals("frmRetIvaCli"))
            {
                fuente = 2;
                compania = com;
                refIvaCli = (frmRetIvaCli)referencia;
                retorno = "frmRetIvaCli";
            }
            if (opcion.Equals("frmCuentasBanc"))
            {
                fuente = 2;
                compania = com;
                refCuentasBan = (frmCuentasBanc)referencia;
                retorno = "frmCuentasBanc";
            }

            if (opcion.Equals("frmProcesoPagoManual"))
            {
                fuente = 2;
                compania = com;
                refPagoManual = (frmProcesoPagoManual)referencia;
                retorno = "frmProcesoPagoManual";
            }

            if (opcion.Equals("frmCruceDeSaldo"))
            {
                fuente = 2;
                compania = com;
                refCruce = (frmCruceDeSaldo)referencia;
                retorno = "frmCruceDeSaldo";
            }

            if (opcion.Equals("frmServicios"))
            {
                fuente = 2;
                compania = com;
                refServicio = (frmServicios)referencia;
                retorno = "frmServicios";
            }
        }


        public void soloVista2(Form referencia, string opcion, string com, string cuenta)
        {

            if (opcion.Equals("frmFacturaNacionalImportacion"))
            {
                fuente = 2;
                compania = com;
                txtCuenta = cuenta;
                refFacturaNacional = (frmFacturaNacionalImportacion)referencia;
                retorno = "frmFacturaNacionalImportacion";
            }
        }

        //jesus 17-07-2013
        private void dataGridView1_CellDoubleClick1(object sender, DataGridViewCellEventArgs e)
        {
            //if (retorno == "frmPagare")
            //{

            //    refPagare.txtCuenta.Text = dataGridView1.CurrentRow.Cells["Codigo"].Value.ToString();
            //    refPagare.lblDescripcionCuenta.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            //    refPagare.txtAuxiliar.Text = "";

            //    //refPagare.guard2 = false;
            //    refPagare.txtCuenta.Focus();
            //    this.Close();
            //}

            //if (retorno == "frmRecibodeIngreso")
            //{

            //    refRecibo.txtCuenta.Text = dataGridView1.CurrentRow.Cells["Codigo"].Value.ToString();
            //    //refRecibo.lblDescripcionCuenta.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            //    refRecibo.txtAuxiliar.Text = "";

            //    //refPagare.guard2 = false;
            //    refRecibo.txtCuenta.Focus();
            //    this.Close();
            //}


            if (retorno == "frmPagare")
            {

                refPagare.txtCuenta.Text = dataGridView1.CurrentRow.Cells["Codigo"].Value.ToString();
                refPagare.lblDescripcionCuenta.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                refPagare.txtAuxiliar.Text = "";

                //refPagare.guard2 = false; 
                refPagare.txtCuenta.Focus();
                this.Close();
            }

            if (retorno == "frmRecibodeIngreso")
            {

                refRecibo.txtCuenta.Text = dataGridView1.CurrentRow.Cells["Codigo"].Value.ToString();
                //refRecibo.lblDescripcionCuenta.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                refRecibo.txtAuxiliar.Text = "";

                //refPagare.guard2 = false;
                refRecibo.txtCuenta.Focus();
                this.Close();
            }


            if (retorno == "frmRetIvaCli")
            {

                refIvaCli.txtCuenta.Text = dataGridView1.CurrentRow.Cells["Codigo"].Value.ToString();
                refIvaCli.lblDescripcionCuenta.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                refIvaCli.txtCuenta.Focus();
                this.Close();
            }

            if (retorno == "frmCuentasBanc")
            {

                refCuentasBan.txtCuenta.Text = dataGridView1.CurrentRow.Cells["Codigo"].Value.ToString();
                refCuentasBan.lblDescripcionCuenta.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                refCuentasBan.txtCuenta.Focus();
                this.Close();
            }


            if (retorno == "frmProcesoPagoManual")
            {

                refPagoManual.txtCuenta.Text = dataGridView1.CurrentRow.Cells["Codigo"].Value.ToString();
                //refPagoManual.lblDescripcionCuenta.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                refPagoManual.txtCuenta.Focus();
                this.Close();
            }


            if (retorno == "frmCruceDeSaldo")
            {

                refCruce.txtCuenta.Text = dataGridView1.CurrentRow.Cells["Codigo"].Value.ToString();
                //refPagoManual.lblDescripcionCuenta.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                refCruce.txtCuenta.Focus();
                this.Close();
            }

            if (retorno == "frmServicios")
            {
                refServicio.txtCuenta.Text = dataGridView1.CurrentRow.Cells["Codigo"].Value.ToString();
                //refPagoManual.lblDescripcionCuenta.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                refServicio.txtCuenta.Focus();
                this.Close();
            }


            if (retorno == "frmFacturaNacionalImportacion")
            {
                foreach (Control c in refFacturaNacional.groupBox2.Controls)
                {
                    if (c.Name == "" + txtCuenta + "txt")
                    {
                        c.Text = this.dataGridView1.CurrentRow.Cells[0].Value.ToString().Replace(".", "");
                        //c.AccessibleDescription = this.dataGridView1.CurrentRow.Cells[1].Value.ToString();
                        //c.AccessibleName = this.dataGridView1.CurrentRow.Cells[2].Value.ToString();
                        //c.Tag = this.dataGridView1.CurrentRow.Cells[3].Value.ToString();
                    }
                }

                foreach (Control c in refFacturaNacional.groupBox3.Controls)
                {
                    if (c.Name == "" + txtCuenta + "txt")
                    {
                        c.Text = this.dataGridView1.CurrentRow.Cells[0].Value.ToString().Replace(".", "");
                        //c.AccessibleDescription = this.dataGridView1.CurrentRow.Cells[1].Value.ToString();
                        //c.AccessibleName = this.dataGridView1.CurrentRow.Cells[2].Value.ToString();
                        //c.Tag = this.dataGridView1.CurrentRow.Cells[3].Value.ToString();
                    }
                }
                this.Close();
            }


        }

     

    }
}

 