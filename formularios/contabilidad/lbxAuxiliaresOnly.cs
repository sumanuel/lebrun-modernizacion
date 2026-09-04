using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using lebrun.clasesData;
using lebrun.clases.contabilidad;
using lebrun.formularios.complementos;
using lebrun.formularios.tesoreria;
using lebrun.formularios.bancos;
using lebrun.formularios.compras;
using lebrun.formularios.clientesMayor;

namespace lebrun.formularios.contabilidad
{
    public partial class lbxAuxiliaresOnly : Form
    {
        private static lbxAuxiliaresOnly m_FormDefInstance;
        private DataTable dtAuxilares;
        private Principal referenciaPrincipal;
        private AuxiliarContable aC;
        private frmComprobanteContable comprobante;
        private frmServicios comprobante1;
        private frmPagare refPagare;
        private frmServicios refServios;
        private string origen;
        private DataView dv;
        private frmRecibodeIngreso refRecibo;
        private frmRetIvaCli refIvaCli;
        private frmCuentasBanc refCuentasBan;
        private string compania;
        private frmProcesoPagoManual refPagoManual;
        private frmCruceDeSaldo refCruce;
        public string compañiaActual;
        private frmClientesMayor referenciaClientesMayor;

        public string CompañiaActual
        {
            get { return compañiaActual; }
            set { compañiaActual = value; }
        }

        public lbxAuxiliaresOnly()
        {
            InitializeComponent();
            aC = new AuxiliarContable();
            dtAuxilares = new DataTable();
        }

        public static lbxAuxiliaresOnly DefInstance
        {
            get
            {
                if (m_FormDefInstance == null || m_FormDefInstance.IsDisposed)
                    m_FormDefInstance = new lbxAuxiliaresOnly();
                return m_FormDefInstance;
            }
            set
            {
                m_FormDefInstance = value;
            }
        }

        private void cmdBuscar_Click(object sender, EventArgs e)
        {
            string sentencia;
            sentencia = "admAC_codigoAuxiliarContable LIKE '%" + txtBuscar.Text + "%' OR admAC_descripcion LIKE '%" + txtBuscar.Text + "%'";
            dv.RowFilter = sentencia;
            if (dv.Count != 0) {
                dataGridView1.DataSource = dv;
                dataGridView1.Update();
            }
            else {
                MessageBox.Show("No se consiguieron Resultados!!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void cmdSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void codCuenta(string codigo) {
            aC.CodigoPlanCuentas = codigo;
        }

        //public void llenarAuxiliares() {
        //    // cambiado Roberto Fernandez 08:56 a.m. 18/12/2012
        //    // origen!=null 
        //    if ((origen!=null)  &&  (origen.Equals("frmPagare")))
        //    {
        //        dtAuxilares = aC.detalleAuxiliar(aC.CodigoPlanCuentas, "01");
        //    }
        //    else
        //    {
                
        //        referenciaPrincipal = (Principal)this.MdiParent;
        //        dtAuxilares = aC.detalleAuxiliar(aC.CodigoPlanCuentas, referenciaPrincipal.empresaActual.BaseDatosActual); 
        //    }
            
        //    dataGridView1.DataSource = dtAuxilares;
        //    dv = new DataView(dtAuxilares);
        //    dataGridView1.Update();
        //    dataGridView1.Columns[0].HeaderText = "Código";
        //    dataGridView1.Columns[1].HeaderText = "Nombre";
        //    dataGridView1.Columns[2].Visible = false;
        //    dataGridView1.Columns[3].HeaderText = "Saldo Apertura";
        //    dataGridView1.Columns[4].Visible = false;
        //}

        public void llenarAuxiliares()
        {
            
            // cambiado Roberto Fernandez 08:56 a.m. 18/12/2012
            // origen!=null 
            if ((origen != null) && ((origen.Equals("frmPagare")) || (origen.Equals("frmRecibodeIngreso")) || (origen.Equals("frmRetIvaCli")) || (origen.Equals("frmCuentasBanc")) ||
            (origen.Equals("Compras Otras Transacciones Procesar Pagos Manuales")) || (origen.Equals("Compras Otras Transacciones Cruce de Saldo"))
            || (origen.Equals("frmServicios")) || (origen.Equals("frmClientesMayor"))))
            {
                dtAuxilares = aC.detalleAuxiliar(aC.CodigoPlanCuentas, compania);
            }
            else
            {
                referenciaPrincipal = (Principal)this.MdiParent;
                dtAuxilares = aC.detalleAuxiliar(aC.CodigoPlanCuentas, referenciaPrincipal.empresaActual.BaseDatosActual);
            }

            dataGridView1.DataSource = dtAuxilares;
            dv = new DataView(dtAuxilares);
            dataGridView1.Update();
            dataGridView1.Columns[0].HeaderText = "Código";
            dataGridView1.Columns[1].HeaderText = "Nombre";
            dataGridView1.Columns[2].Visible = false;
            dataGridView1.Columns[3].HeaderText = "Saldo Apertura";
            dataGridView1.Columns[4].Visible = false;
        }

        public void comprobantePadre(frmComprobanteContable frmcompro)
        {
            comprobante = frmcompro;
        }
   

        //public void destino(Form referencia, string opcion)
        //{
        //    // cambiado Roberto Fernandez 08:56 a.m. 18/12/2012
        //    // origen!=null 
        //    origen = opcion;
        //    if ((origen != null) && (opcion.Equals("frmPagare")))
        //    {
        //        origen = opcion;
        //        refPagare = (frmPagare)referencia;
        //    }
        //}
        public void guardarCompania(string comp)
        {
            compania = comp;
        }

        public void destino(Form referencia, string opcion)
        {
            // cambiado Roberto Fernandez 08:56 a.m. 18/12/2012
            // origen!=null 
            origen = opcion;
            if ((origen != null) && (opcion.Equals("frmPagare")))
            {
                origen = opcion;
                refPagare = (frmPagare)referencia;
            }

            if ((origen != null) && (opcion.Equals("frmServicios")))
            {
                origen = opcion;
                refServios = (frmServicios)referencia;
            }
            if ((origen != null) && (opcion.Equals("frmRecibodeIngreso")))
            {
                origen = opcion;
                refRecibo = (frmRecibodeIngreso)referencia;
            }
            if ((origen != null) && (opcion.Equals("frmRetIvaCli")))
            {
                origen = opcion;
                refIvaCli = (frmRetIvaCli)referencia;
            }
            if ((origen != null) && (opcion.Equals("frmCuentasBanc")))
            {
                origen = opcion;
                refCuentasBan = (frmCuentasBanc)referencia;
            }

            if ((origen != null) && (opcion.Equals("Compras Otras Transacciones Procesar Pagos Manuales")))
            {
                origen = opcion;
                refPagoManual = (frmProcesoPagoManual)referencia;
            }

            if ((origen != null) && (opcion.Equals("Compras Otras Transacciones Cruce de Saldo")))
            {
                origen = opcion;
                refCruce = (frmCruceDeSaldo)referencia;
            }
            if (origen.Equals("frmClientesMayor"))
            {
                origen = opcion;
                referenciaClientesMayor = (frmClientesMayor)referencia;
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // cambiado Roberto Fernandez 08:56 a.m. 18/12/2012
            // origen!=null 

            //if ((origen != null) && (origen.Equals("frmPagare")))
            //{
            //    refPagare.txtAuxiliar.Text = dataGridView1.CurrentRow.Cells["admAC_codigoAuxiliarContable"].Value.ToString();
            //    refPagare.txtAuxiliar.Focus();
            //    refPagare.guard2 = true;
            //    this.Close();
            //}
            //else
            //{
            //    comprobante.txtAuxiliar.Text = dataGridView1.CurrentRow.Cells["admAC_codigoAuxiliarContable"].Value.ToString();
            //    comprobante.txtNombreAuxiliar.Text = dataGridView1.CurrentRow.Cells["admAC_descripcion"].Value.ToString();
            //    comprobante.txtAuxiliar.Focus();
            //    this.Close();
            //}

            if ((origen != null) && ((origen.Equals("frmPagare")) || (origen.Equals("frmRecibodeIngreso")) ||
                (origen.Equals("frmRetIvaCli")) || (origen.Equals("frmCuentasBanc")) || (origen.Equals("Compras Otras Transacciones Procesar Pagos Manuales")) ||
                (origen.Equals("Compras Otras Transacciones Cruce de Saldo")) || (origen.Equals("frmServicios"))
                 || (origen.Equals("frmClientesMayor"))))

            {

                if (origen.Equals("frmPagare"))
                {
                    refPagare.txtAuxiliar.Text = dataGridView1.CurrentRow.Cells["admAC_codigoAuxiliarContable"].Value.ToString();
                    refPagare.txtAuxiliar.Focus();
                    refPagare.guard2 = true;
                    this.Close();
                }
                if (origen.Equals("frmServicios"))
                {
                    refServios.txtAuxiliar.Text = dataGridView1.CurrentRow.Cells["admAC_codigoAuxiliarContable"].Value.ToString();
                    refServios.txtAuxiliar.Focus();
                    refServios.guard2 = true;
                    this.Close();
                }
                if (origen.Equals("frmRecibodeIngreso"))
                {
                    refRecibo.txtAuxiliar.Text = dataGridView1.CurrentRow.Cells["admAC_codigoAuxiliarContable"].Value.ToString();
                    refRecibo.txtAuxiliar.Focus();
                    refRecibo.guard2 = true;
                    this.Close();
                }


                if (origen.Equals("frmRetIvaCli"))
                {

                    refIvaCli.txtAuxiliar.Text = dataGridView1.CurrentRow.Cells["admAC_codigoAuxiliarContable"].Value.ToString();
                    refIvaCli.txtAuxiliar.Focus();
                    this.Close();
                }


                if (origen.Equals("frmCuentasBanc"))
                {

                    refCuentasBan.txtAuxiliar.Text = dataGridView1.CurrentRow.Cells["admAC_codigoAuxiliarContable"].Value.ToString();
                    refCuentasBan.txtAuxiliar.Focus();
                    this.Close();
                }


                if (origen.Equals("Compras Otras Transacciones Procesar Pagos Manuales"))
                {

                    refPagoManual.txtAuxiliar.Text = dataGridView1.CurrentRow.Cells["admAC_codigoAuxiliarContable"].Value.ToString();
                    refPagoManual.txtAuxiliar.Focus();
                    this.Close();
                }

                if (origen.Equals("Compras Otras Transacciones Cruce de Saldo"))
                {

                    refCruce.txtAuxiliar.Text = dataGridView1.CurrentRow.Cells["admAC_codigoAuxiliarContable"].Value.ToString();
                    refCruce.txtAuxiliar.Focus();
                    this.Close();
                }

                if (origen.Equals("frmClientesMayor"))
                {
                    referenciaClientesMayor.txtAuxiliar.Text = dataGridView1.CurrentRow.Cells["admAC_codigoAuxiliarContable"].Value.ToString();
                    referenciaClientesMayor.txtAuxiliar_KeyPress(new object(), new KeyPressEventArgs((char)(Keys.Enter)));
                    this.Close();
                }
            }
            else
            {
                comprobante.txtAuxiliar.Text = dataGridView1.CurrentRow.Cells["admAC_codigoAuxiliarContable"].Value.ToString();
                comprobante.txtNombreAuxiliar.Text = dataGridView1.CurrentRow.Cells["admAC_descripcion"].Value.ToString();
                comprobante.txtAuxiliar.Focus();
                this.Close();
            }
        }

        private void cmdLimpiar_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = dtAuxilares;
            dataGridView1.Update();
            dataGridView1.Columns[0].HeaderText = "Código";
            dataGridView1.Columns[1].HeaderText = "Nombre";
            dataGridView1.Columns[2].Visible = false;
            dataGridView1.Columns[3].HeaderText = "Saldo Apertura";
            dataGridView1.Columns[4].Visible = false;
        }

        private void txtBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmdBuscar.Focus();
            }
        }

        private void lbxAuxiliaresOnly_Load(object sender, EventArgs e)
        {

        }
        
    }
}
