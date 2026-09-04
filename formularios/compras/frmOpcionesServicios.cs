using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using lebrun.clases.compras;
using lebrun.Reportes.retenciones;
using lebrun.Reportes.MovContab;
using lebrun.clases.contabilidad;

namespace lebrun.formularios.compras
{

    public partial class frmOpcionesServicios : Form
    {

        private lbxServicios reflbxServicios;
        private retencionesIva retIva;
        private retencionISLR retISLR;
        private string proveedor;
        private string documento;
        private string sufijo;
        private string tipoDoc;
        private string nombrePro;
        private string usuario;
        private frmContenedorRetIva conteIva;
        private frmContenedorRetISLR conteISLR;
        private frmConteMovContab conteMovContab;
        private movimientoContable movConta;
         
        private DataTable tabla;


        public frmOpcionesServicios()
        {
            InitializeComponent();
        }

        public frmOpcionesServicios(string pro, string docu, string suf, string tipo, string nomb, string usu)
        {
            InitializeComponent();

            retIva = new retencionesIva();
            retISLR = new retencionISLR();
            txtCodigo.Text = pro;
            txtDocumento.Text = docu;
            txtSufijo.Text = suf;
            txtTipoDoc.Text = tipo;
            txtNombre.Text = nomb;
            usuario = usu;
 
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            if (btnISLR.Visible == true || btnIva.Visible == true)
            {
                if (MessageBox.Show("¿Esta seguro que desea salir y anular la(s) retencion(es)? ", "Alerta ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (btnIva.Visible == true)
                    {
                        retIva.anularRetencionIva(txtDocumento.Text, txtSufijo.Text, txtTipoDoc.Text, txtCodigo.Text);
                    }

                    if (btnISLR.Visible == true)
                    {
                        retISLR.anularRetencionISLR(txtDocumento.Text, txtSufijo.Text, txtTipoDoc.Text, txtCodigo.Text);
                    }

                    reflbxServicios.Visible = true;
                    this.Close();
                }
            }
            else
            {

                reflbxServicios.Visible = true;
                this.Close();
            }
        }

        public void manejandoLbx(Form lbxServicio)
        {
            reflbxServicios = (lbxServicios)lbxServicio;
        }

        private void frmOpcionesServicios_Load(object sender, EventArgs e)
        {
            asignandoValores();
            if (!retIva.habilitarBotonRetencion())
            {
                btnIva.Visible = false;
            }

            asignandoValoresISLR();
            if (!retISLR.habilitarBoton())
            {
                btnISLR.Visible = false;
            }
            
        }

        private void btnIva_Click(object sender, EventArgs e)
        {            
            if (retIva.verificarRetencion())
            {
                retIva.consultarCuenta();
                retIva.calculoIva();
                retIva.datosComprobante();
                retIva.consultarCuentaPro();
                retIva.armarDetallesMovContab();
                retIva.ingresarMovContab();
                retIva.actulizarRetLibroCompras();
                retIva.actualizarSalpro();
                conteIva = new frmContenedorRetIva(retIva.imprimirRetencionIva(txtCodigo.Text, txtDocumento.Text, txtSufijo.Text, txtTipoDoc.Text));
                conteIva.Show();
                //this.Close();
                if (!retIva.habilitarBotonRetencion())
                {
                    btnIva.Visible = false;
                }
            }
        }

        private void asignandoValores()
        {
            retIva.NroDoc = txtDocumento.Text;
            retIva.Proveedor = txtCodigo.Text;
            retIva.SufDoc = txtSufijo.Text;
            retIva.TipoDoc = txtTipoDoc.Text;
            retIva.Usuario = usuario;
        }

        private void asignandoValoresISLR()
        {
            retISLR.NroDocum = txtDocumento.Text;
            retISLR.SufiDocum = txtSufijo.Text;
            retISLR.IdProveedor = txtCodigo.Text;
            retISLR.IdTipoDoc = txtTipoDoc.Text;
            retISLR.Login = usuario;
        }

        private void btnISLR_Click(object sender, EventArgs e)
        {
            asignandoValoresISLR();
            retISLR.cuentaCorrelativo();
            retISLR.actualizarLibroISLR();
            retISLR.actualizarSaldoISLR();
            retISLR.armarDetallesMovContab();
            retISLR.ingresarMovContab();
            conteISLR = new frmContenedorRetISLR(retISLR.ImprimirISLR(txtCodigo.Text, txtDocumento.Text, txtSufijo.Text, txtTipoDoc.Text));
            conteISLR.Show();
            //this.Close();
            if (!retISLR.habilitarBoton())
            {
                btnISLR.Visible = false;
            }
        }

        private void btnComprobante_Click(object sender, EventArgs e)
        {
            movConta = new movimientoContable();
            conteMovContab = new frmConteMovContab(movConta.comprobaContableCompras(txtTipoDoc.Text, txtCodigo.Text, txtDocumento.Text, txtSufijo.Text), "Servicios");
            conteMovContab.Show();
        }
        


    }
}
