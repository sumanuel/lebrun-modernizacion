using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using lebrun.clases.facturacion;

namespace lebrun.formularios.facturacion
{
    public partial class frmSerieFiscal : Form
    {
        public frmSerieFiscal()
        {
            InitializeComponent();
        }

        private SerieFiscal serieIngresar;
        private static frmSerieFiscal m_FormDefInstance;
        public string operacion;
        Principal referenciaPrincipal;
        private LbxSerieFiscal referenciaLbx;

        private void frmSerieFiscal_Load(object sender, EventArgs e)
        {
            serieIngresar = new SerieFiscal();
            txtSerie.Focus();
            referenciaPrincipal = (Principal)this.MdiParent;
            txtIp.Text = referenciaPrincipal.usuarioActual.IpPc;
            cboPuntoActivo.SelectedIndex = 0;
        }

        public static frmSerieFiscal DefInstance
        {
            get
            {
                if (m_FormDefInstance == null || m_FormDefInstance.IsDisposed)
                    m_FormDefInstance = new frmSerieFiscal();
                return m_FormDefInstance;
            }
            set
            {
                m_FormDefInstance = value;
            }
        }


        private void txtSerie_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)(Keys.Enter)) && txtSerie.Text != "") {
                txtSerial.Focus();
            }
        }

        private void txtSerial_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)(Keys.Enter)) && txtSerial.Text != "")
            {
                cboPuntoActivo.Focus();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (referenciaLbx != null) {
                referenciaLbx.actualizarLbx();
            }
            this.Close();
        }

        private void cmdAgregar_Click(object sender, EventArgs e)
        {
            if (validarCampos()) {
                if (operacion.Equals("guardar")) {
                    if (!(this.serieIngresar.existeSerieFiscal(txtIp.Text, txtSerie.Text)))
                    {
                        this.serieIngresar.Seri_serie = txtSerie.Text;
                        this.serieIngresar.Serial = txtSerial.Text;
                        this.serieIngresar.IpMaquina = txtIp.Text;
                        this.serieIngresar.SerieDocumento = txtDocumento.Text;
                        this.serieIngresar.SerieAct = cboPuntoActivo.Text;
                        this.serieIngresar.SerieCaja = txtcaja.Text;
                        this.serieIngresar.guardarSerieFiscal();
                        MessageBox.Show("Serie Fiscal Guardada Exitosamente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        limpiarCampos();
                    }
                    else {
                        MessageBox.Show("Esta Serie Fiscal Ya existe!!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }

                if (operacion.Equals("modificar")) {
                    this.serieIngresar.Serial = txtSerial.Text;
                    this.serieIngresar.SerieAct = cboPuntoActivo.Text;
                    this.serieIngresar.SerieCaja = txtcaja.Text;
                    this.serieIngresar.updateSerieFiscal();
                    MessageBox.Show("Serie Fiscal Modificada Exitosamente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    limpiarCampos();
                    referenciaLbx.actualizarLbx();
                    this.Close();
                }
            }
        }

        private bool validarCampos() { 
            bool centinela = true;
            if (txtSerie.Text == "") {
                centinela = false;
                MessageBox.Show("La serie es un campo Obligatorio!!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtSerie.Focus();
                return centinela;
            }

            if (cboPuntoActivo.Text == "") {
                centinela = false;
                MessageBox.Show("Punto Activo es un campo Obligatorio!!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                cboPuntoActivo.Focus();
                return centinela;
            }

            if (txtcaja.Text == "")
            {
                centinela = false;
                MessageBox.Show("La Caja es un campo Obligatorio!!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtcaja.Focus();
                return centinela;
            }

            return centinela;
        }

        private void cboPuntoActivo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)(Keys.Enter)) && cboPuntoActivo.Text != "")
            {
                txtcaja.Focus();
            }
        }

        private void limpiarCampos() {
            txtSerie.Clear();
            txtSerial.Clear();
            cboPuntoActivo.SelectedIndex = 0;
            txtcaja.Clear();
            txtDocumento.Text = "FAV";
        }

        public void actualizarReferencia(string tipoOperacion, LbxSerieFiscal lbx) {
            this.operacion = tipoOperacion;
            referenciaLbx = lbx;
        }


        public void cargarSerieFis(string ip, string serie) {
            this.serieIngresar.cargarSerieFiscal(ip, serie);
            txtSerie.Text = this.serieIngresar.Seri_serie;
            txtSerial.Text = this.serieIngresar.Serial;
            txtIp.Text = this.serieIngresar.IpMaquina;
            txtDocumento.Text = this.serieIngresar.SerieDocumento;
            cboPuntoActivo.SelectedIndex = cboPuntoActivo.FindString(this.serieIngresar.SerieAct);
            txtcaja.Text = this.serieIngresar.SerieCaja;
        }
    }
}
