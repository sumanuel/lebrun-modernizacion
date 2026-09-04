using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using lebrun.clases.complementos;
using lebrun.formularios.importaciones;
using lebrun.formularios.reportes;
using lebrun.formularios.compras;
using lebrun.formularios.productos;

namespace lebrun.formularios.complementos
{
    public partial class lbxProveedores : Form
    {

        //declaraciones
        Principal SistemaUsuario;
        frmProveedores proveedores;
        private frmEmisionARC refEmisionArc;
        private proveedor proveedor;
        private static lbxProveedores m_FormDefInstance;
        private frmOrdenCompra refOrdenCompra;
        private frmPagare refPagare;
        private frmFacturaNacionalImportacion refImportaciones;
        private string origen;
        private frmProcesoPagoManual refPagoManual;
        private frmCruceDeSaldo refCruce;
        private frmServicios refServicios;
        private frmAsignarProveedor refAsignarProvee;
        public string modulo;
        public string Proviene;
        public string Eleccion="0";
        public string frmProduct;
        public string Servicios;
        public string ipMaq;
        public string IdMaq;

        public string IPMAQ
        {
            get { return ipMaq; ;}
            set { ipMaq = value; }
        }
        public string IDMAQ
        {
            get { return IdMaq; ;}
            set { IdMaq = value; }
        }

        //cons
        public lbxProveedores()
        {
            InitializeComponent();
            proveedor = new proveedor();
            ttmensajes.SetToolTip(button1, "Buscar");
            ttmensajes.SetToolTip(btnAgregar, "Agregar");
            ttmensajes.SetToolTip(btnModificar, "Modificar");
            ttmensajes.SetToolTip(btnImprimir, "Imprimir");
            ttmensajes.SetToolTip(btnLimpiar, "Limpiar");
            ttmensajes.SetToolTip(button2, "Selecionar");
            ttmensajes.SetToolTip(btnSalir, "Salir");
        }

        public static lbxProveedores DefInstance
        {
            get
            {
                if (m_FormDefInstance == null || m_FormDefInstance.IsDisposed)
                    m_FormDefInstance = new lbxProveedores();
                return m_FormDefInstance;
            }
            set
            {
                m_FormDefInstance = value;
            }
        }


        //public void destino(frmOrdenCompra referencia) {
        //    refOrdenCompra = referencia;
        //}


        public void destino(Form referencia, string opcion)
        {

            if (opcion.Equals("frmPagare")) {
                origen = opcion;
                refPagare = (frmPagare)referencia;
                Eleccion = "1";
            }

            if (opcion.Equals("frmOrdenCompra")){
                origen = opcion;
                refOrdenCompra = (frmOrdenCompra)referencia;
                Eleccion = "1";
            }
            if (opcion.Equals("frmFacturaNacionalImportacion"))
            {
                origen = opcion;
                refImportaciones = (frmFacturaNacionalImportacion)referencia;
                Eleccion = "1";
            }

            if (opcion.Equals("frmEmisionARC"))
            {
                origen = opcion;
                refEmisionArc = (frmEmisionARC)referencia;
                Eleccion = "1";
            }

            if (opcion.Equals("Compras Otras Transacciones Procesar Pagos Manuales"))
            {
                origen = opcion;
                refPagoManual = (frmProcesoPagoManual)referencia;
                Eleccion = "1";
            }

            if (opcion.Equals("Compras Otras Transacciones Cruce de Saldo"))
            {
                origen = opcion;
                refCruce = (frmCruceDeSaldo)referencia;
                Eleccion = "1";
            }

            if (opcion.Equals("frmServicios"))
            {
                origen = opcion;
                refServicios = (frmServicios)referencia;
                Eleccion = "1";
            }
            if (opcion.Equals("frmAsignarProveedor")) 
            {
                origen = opcion;
                refAsignarProvee = (frmAsignarProveedor)referencia;
                Eleccion = "1";
            }

        }

        private void lbxProveedores_Load(object sender, EventArgs e)
        {
            SistemaUsuario = new Principal();
            proveedores = new frmProveedores();
            dataGridView1.DataSource = proveedor.cargarProveedores();
            dataGridView1.Columns[0].Width = 100;
            dataGridView1.Columns[1].Width = 350;
            dataGridView1.Columns[2].Width = 100;
            dataGridView1.Columns[3].Visible = false;
            dataGridView1.Columns[4].Visible = false;
            dataGridView1.Columns[5].Visible = false;
            dataGridView1.Columns[6].Visible = false;
            dataGridView1.Columns[7].Visible = false;
            if (frmProduct != "Producto")
            {
                SistemaUsuario = (Principal)this.MdiParent;
                IdMaq = Convert.ToString(SistemaUsuario.usuarioActual.Id);
                ipMaq = Convert.ToString(SistemaUsuario.usuarioActual.IpPc);
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {

            dataGridView1.DataSource = null;
            //dataGridView1.DataSource=proveedor.consultarProveedor(1,txtBusqueda.Text);
            dataGridView1.DataSource = proveedor.consultarProveedor(txtBusqueda.Text);
            dataGridView1.Update();
            dataGridView1.Columns[0].Width = 100;
            dataGridView1.Columns[1].Width = 350;
            dataGridView1.Columns[2].Width = 100;

            dataGridView1.Columns[3].Visible = false;
            dataGridView1.Columns[4].Visible = false;
            dataGridView1.Columns[5].Visible = false;
            dataGridView1.Columns[6].Visible = false;
            dataGridView1.Columns[7].Visible = false;
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            if (origen.Equals("frmOrdenCompra"))
            {
                refOrdenCompra.txtCodProvee.Text = this.dataGridView1.CurrentRow.Cells[0].Value.ToString();
                refOrdenCompra.lblNombreProvee.Text = this.dataGridView1.CurrentRow.Cells[1].Value.ToString();
                refOrdenCompra.lblRif.Text = this.dataGridView1.CurrentRow.Cells[2].Value.ToString();
                refOrdenCompra.cmbMoneda.Text = this.dataGridView1.CurrentRow.Cells[3].Value.ToString();
                refOrdenCompra.txtDescuento1.Text = this.dataGridView1.CurrentRow.Cells[5].Value.ToString();
                refOrdenCompra.txtDescuento2.Text = this.dataGridView1.CurrentRow.Cells[6].Value.ToString();
                refOrdenCompra.txtDescuento3.Text = this.dataGridView1.CurrentRow.Cells[7].Value.ToString();
                refOrdenCompra.txtCodProvee.Focus();
                this.Close();
            }

            if (origen.Equals("frmPagare"))
            {
                refPagare.txtProveedor.Text=this.dataGridView1.CurrentRow.Cells[0].Value.ToString();
                refPagare.lblNombreProveedor.Text = this.dataGridView1.CurrentRow.Cells[1].Value.ToString();
                refPagare.lblRif.Text = this.dataGridView1.CurrentRow.Cells[2].Value.ToString();
                refPagare.txtProveedor.Focus();
                this.Close();
            }

            if (origen.Equals("frmFacturaNacionalImportacion"))
            {
                refImportaciones.txtCodigoProveedor.Text = this.dataGridView1.CurrentRow.Cells[0].Value.ToString();
                refImportaciones.lblNomProveNac.Text = this.dataGridView1.CurrentRow.Cells[1].Value.ToString();
                refImportaciones.lblRifProvNac.Text = this.dataGridView1.CurrentRow.Cells[2].Value.ToString();
                refImportaciones.txtCodigoProveedor.Focus();
                this.Close();
            }

            if (origen.Equals("frmEmisionARC"))
            {
                refEmisionArc.txtCodigo.Text = this.dataGridView1.CurrentRow.Cells[0].Value.ToString();
                refEmisionArc.txtCodigo.Focus();
                this.Close();
            }

            if (origen.Equals("Compras Otras Transacciones Procesar Pagos Manuales"))
            {
                refPagoManual.txtCodigoProvee.Text = this.dataGridView1.CurrentRow.Cells[0].Value.ToString();
                refPagoManual.lblNombreProveedor3.Text = this.dataGridView1.CurrentRow.Cells[1].Value.ToString();
                refPagoManual.txtCodigoProvee.Focus();
                this.Close();
            }

            if (origen.Equals("Compras Otras Transacciones Cruce de Saldo"))
            {
                refCruce.txtCodigoProveedor.Text = this.dataGridView1.CurrentRow.Cells[0].Value.ToString();
                refCruce.lblNombrePro.Text = this.dataGridView1.CurrentRow.Cells[1].Value.ToString();
                refCruce.txtCodigoProveedor.Focus();
                this.Close();
            }

            if (origen.Equals("frmServicios"))
            {
                refServicios.txtProveedor.Text = this.dataGridView1.CurrentRow.Cells[0].Value.ToString();
                refServicios.lblNombrePro.Text = this.dataGridView1.CurrentRow.Cells[1].Value.ToString();
                refServicios.txtProveedor.Focus();
                this.Close();
            }
            if (origen.Equals("frmAsignarProveedor")) 
            {
                refAsignarProvee.txtCodProv.Text = this.dataGridView1.CurrentRow.Cells[0].Value.ToString();
                refAsignarProvee.txtDesProv.Text = this.dataGridView1.CurrentRow.Cells[1].Value.ToString();
                refAsignarProvee.txtCodProv.Focus();
                this.Close();
            }

        }

        private void txtBusqueda_KeyPress(object sender, KeyPressEventArgs e)
        {
            //aqui se llamara la funcion buscar
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
          this.Close();
        }

        private void lbxProveedores_Shown(object sender, EventArgs e)
        {
            if (Eleccion == "1")
            {
                btnAgregar.Visible = false;
                btnModificar.Visible = false;
                btnImprimir.Visible = false;
                btnLimpiar.Visible = true;
                button2.Visible = true;
                btnVisor.Visible = false;
            }
            else
            {
                btnAgregar.Visible = true;
                btnModificar.Visible = true;
                btnImprimir.Visible = true;
                btnLimpiar.Visible = true;
                button2.Visible = false;
                btnVisor.Visible = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (origen.Equals("frmOrdenCompra"))
            {
                refOrdenCompra.txtCodProvee.Text = this.dataGridView1.CurrentRow.Cells[0].Value.ToString();
                refOrdenCompra.lblNombreProvee.Text = this.dataGridView1.CurrentRow.Cells[1].Value.ToString();
                refOrdenCompra.lblRif.Text = this.dataGridView1.CurrentRow.Cells[2].Value.ToString();
                refOrdenCompra.cmbMoneda.Text = this.dataGridView1.CurrentRow.Cells[3].Value.ToString();
                refOrdenCompra.txtDescuento1.Text = this.dataGridView1.CurrentRow.Cells[5].Value.ToString();
                refOrdenCompra.txtDescuento2.Text = this.dataGridView1.CurrentRow.Cells[6].Value.ToString();
                refOrdenCompra.txtDescuento3.Text = this.dataGridView1.CurrentRow.Cells[7].Value.ToString();
                refOrdenCompra.txtCodProvee.Focus();
                this.Close();
            }

            if (origen.Equals("frmPagare"))
            {
                refPagare.txtProveedor.Text = this.dataGridView1.CurrentRow.Cells[0].Value.ToString();
                refPagare.lblNombreProveedor.Text = this.dataGridView1.CurrentRow.Cells[1].Value.ToString();
                refPagare.lblRif.Text = this.dataGridView1.CurrentRow.Cells[2].Value.ToString();
                refPagare.txtProveedor.Focus();
                this.Close();
            }

            if (origen.Equals("frmFacturaNacionalImportacion"))
            {
                refImportaciones.txtCodigoProveedor.Text = this.dataGridView1.CurrentRow.Cells[0].Value.ToString();
                refImportaciones.lblNomProveNac.Text = this.dataGridView1.CurrentRow.Cells[1].Value.ToString();
                refImportaciones.lblRifProvNac.Text = this.dataGridView1.CurrentRow.Cells[2].Value.ToString();
                refImportaciones.txtCodigoProveedor.Focus();
                this.Close();
            }

            if (origen.Equals("frmEmisionARC"))
            {
                refEmisionArc.txtCodigo.Text = this.dataGridView1.CurrentRow.Cells[0].Value.ToString();
                refEmisionArc.txtCodigo.Focus();
                this.Close();
            }

            if (origen.Equals("frmProcesoPagoManual"))
            {
                refPagoManual.txtCodigoProvee.Text = this.dataGridView1.CurrentRow.Cells[0].Value.ToString();
                refPagoManual.lblNombreProveedor3.Text = this.dataGridView1.CurrentRow.Cells[1].Value.ToString();
                refPagoManual.txtCodigoProvee.Focus();
                this.Close();
            }

            if (origen.Equals("Compras Otras Transacciones Cruce de Saldo"))
            {
                refCruce.txtCodigoProveedor.Text = this.dataGridView1.CurrentRow.Cells[0].Value.ToString();
                refCruce.lblNombrePro.Text = this.dataGridView1.CurrentRow.Cells[1].Value.ToString();
                refCruce.txtCodigoProveedor.Focus();
                this.Close();
            }

            if (origen.Equals("frmServicios"))
            {
                refServicios.txtProveedor.Text = this.dataGridView1.CurrentRow.Cells[0].Value.ToString();
                refServicios.lblNombrePro.Text = this.dataGridView1.CurrentRow.Cells[1].Value.ToString();
                refServicios.txtProveedor.Focus();
                this.Close();
            }
            if (origen.Equals("frmAsignarProveedor"))
            {
                refAsignarProvee.txtCodProv.Text = this.dataGridView1.CurrentRow.Cells[0].Value.ToString();
                refAsignarProvee.txtDesProv.Text = this.dataGridView1.CurrentRow.Cells[1].Value.ToString();
                refAsignarProvee.txtCodProv.Focus();
                this.Close();
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            proveedores.Show();
            proveedores.Eleccion = "Agregar";
            proveedores.idPc = IDMAQ;
            proveedores.ipPc = IPMAQ;
            this.Close();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            string codigoProvee;
            proveedores.Eleccion = "Modificar";
            proveedores.idPc = IDMAQ;
            proveedores.ipPc = IPMAQ;
            codigoProvee=this.dataGridView1.CurrentRow.Cells[0].Value.ToString();
            proveedor.modificProveedor(codigoProvee);
            proveedores.txtCodigoProvee.Text=proveedor.ProvCod;
            proveedores.txtDesProvee.Text=proveedor.ProvNom;
            proveedores.txtRifProv.Text=proveedor.ProvRif;
            proveedores.txtDivProv.Text=proveedor.ProvDiv;
            proveedores.cmbActEcono.Text =proveedor.ProvActDes;
            proveedores.cmbSituacion.Text =proveedor.ProvSitua;
            proveedores.cmdResidente.Text =proveedor.ProvResid;
            proveedores.txtReIva.Text=proveedor.ProvRet;
            proveedores.txtCodPais.Text = proveedor.ProvPais;
            //proveedores.cmbPais.Text =proveedor;
            proveedores.txtCodRegion.Text =proveedor.ProvRegion;
            //proveedores.cmbRegion.Text =proveedor;
            proveedores.txtCodEstado.Text = proveedor.ProvEstado;
            //proveedores.cmbEstado.Text=proveedor.;
            proveedores.cmbMun.Text =proveedor.ProvMun;
            proveedores.cmbParro.Text =proveedor.ProvParr;
            proveedores.txtD1.Text=proveedor.ProvDirr3;
            proveedores.txtD2.Text=proveedor.ProvDirr2;
            proveedores.txtD3.Text=proveedor.ProvDir3;
            proveedores.txtCodArea.Text=proveedor.ProvCodArea;
            proveedores.txtTlMaster.Text=proveedor.ProvTelf1;
            proveedores.txtTlf.Text=proveedor.ProvTelf2;
            proveedores.txtFax.Text=proveedor.ProvTelf3;
            proveedores.txtCelu.Text=proveedor.ProvCel1;
            proveedores.txtOtrCelu.Text=proveedor.ProvCel2;
            proveedores.txtCorrElec.Text=proveedor.ProvCorreo;
            proveedores.txtPagWeb.Text=proveedor.ProvPagina;
            proveedores.cmbTipPer.Items.Add(proveedor.ProvTippER);
            proveedores.cmbTipPer.SelectedIndex = 0;
            proveedores.cmbConDIva.Text =proveedor.ProvContri;
            proveedores.txtBanc.Text=proveedor.Provbanco;
            proveedores.cmbTipCuenta.Text=proveedor.ProvTipoCuenta;
            proveedores.txtCodCueProvee.Text=proveedor.ProvCuentEfec;
            proveedores.txtLimCre.Text =proveedor.ProvLimCre;
            proveedores.txtDiaDPlazo.Text =proveedor.ProvDiasPlaz;
            proveedores.cmbConDPag.Text =proveedor.ProvCondPago;
            proveedores.cmbCxP.Text =proveedor.ProvCxpom;
            proveedores.cmbZL.Text =proveedor.ProvZonaLibre;
            proveedores.cmbClasi.Text =proveedor.ProvClase;
            proveedores.cmbODCDME.Text =proveedor.ProvSoliCotZom;
            proveedores.txtDes1.Text =proveedor.ProvDesp;
            proveedores.txtDes2.Text =proveedor.ProvDcto2;
            proveedores.txtDes3.Text =proveedor.ProvDcto3;
            proveedores.txtObser1.Text=proveedor.ProvComent;
            proveedores.txtCodCueProvee.Text = proveedor.ProvCuentEfec;
            proveedores.Show();

            this.Close();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = proveedor.cargarProveedores();
            dataGridView1.Columns[0].Width = 100;
            dataGridView1.Columns[1].Width = 350;
            dataGridView1.Columns[2].Width = 100;
            dataGridView1.Columns[3].Visible = false;
            dataGridView1.Columns[4].Visible = false;
            dataGridView1.Columns[5].Visible = false;
            dataGridView1.Columns[6].Visible = false;
            dataGridView1.Columns[7].Visible = false;
        }

        private void btnVisor_Click(object sender, EventArgs e)
        {
            string codigoProvee;
            proveedores.Eleccion = "Modificar";
            codigoProvee = this.dataGridView1.CurrentRow.Cells[0].Value.ToString();
            proveedor.modificProveedor(codigoProvee);
            proveedores.txtCodigoProvee.Text = proveedor.ProvCod;
            proveedores.txtDesProvee.Text = proveedor.ProvNom;
            proveedores.txtRifProv.Text = proveedor.ProvRif;
            proveedores.txtDivProv.Text = proveedor.ProvDiv;
            proveedores.cmbActEcono.Text = proveedor.ProvActDes;
            proveedores.cmbSituacion.Text = proveedor.ProvSitua;
            proveedores.cmdResidente.Text = proveedor.ProvResid;
            proveedores.txtReIva.Text = proveedor.ProvRet;
            proveedores.txtCodPais.Text = proveedor.ProvPais;
            //proveedores.cmbPais.Text =proveedor;
            proveedores.txtCodRegion.Text = proveedor.ProvRegion;
            //proveedores.cmbRegion.Text =proveedor;
            proveedores.txtCodEstado.Text = proveedor.ProvEstado;
            //proveedores.cmbEstado.Text=proveedor.;
            proveedores.cmbMun.Text = proveedor.ProvMun;
            proveedores.cmbParro.Text = proveedor.ProvParr;
            proveedores.txtD1.Text = proveedor.ProvDirr3;
            proveedores.txtD2.Text = proveedor.ProvDirr2;
            proveedores.txtD3.Text = proveedor.ProvDir3;
            proveedores.txtCodArea.Text = proveedor.ProvCodArea;
            proveedores.txtTlMaster.Text = proveedor.ProvTelf1;
            proveedores.txtTlf.Text = proveedor.ProvTelf2;
            proveedores.txtFax.Text = proveedor.ProvTelf3;
            proveedores.txtCelu.Text = proveedor.ProvCel1;
            proveedores.txtOtrCelu.Text = proveedor.ProvCel2;
            proveedores.txtCorrElec.Text = proveedor.ProvCorreo;
            proveedores.txtPagWeb.Text = proveedor.ProvPagina;
            proveedores.cmbTipPer.Items.Add(proveedor.ProvTippER);
            proveedores.cmbTipPer.SelectedIndex = 0;
            proveedores.cmbConDIva.Text = proveedor.ProvContri;
            proveedores.txtBanc.Text = proveedor.Provbanco;
            proveedores.cmbTipCuenta.Text = proveedor.ProvTipoCuenta;
            proveedores.txtCodCueProvee.Text = proveedor.ProvCuentEfec;
            proveedores.txtLimCre.Text = proveedor.ProvLimCre;
            proveedores.txtDiaDPlazo.Text = proveedor.ProvDiasPlaz;
            proveedores.cmbConDPag.Text = proveedor.ProvCondPago;
            proveedores.cmbCxP.Text = proveedor.ProvCxpom;
            proveedores.cmbZL.Text = proveedor.ProvZonaLibre;
            proveedores.cmbClasi.Text = proveedor.ProvClase;
            proveedores.cmbODCDME.Text = proveedor.ProvSoliCotZom;
            proveedores.txtDes1.Text = proveedor.ProvDesp;
            proveedores.txtDes2.Text = proveedor.ProvDcto2;
            proveedores.txtDes3.Text = proveedor.ProvDcto3;
            proveedores.txtObser1.Text = proveedor.ProvComent;
            proveedores.txtCodCueProvee.Text = proveedor.ProvCuentEfec;
            proveedores.Show();
            proveedores.Eleccion = "Visor";
            this.Close();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {

        }

    }
}
