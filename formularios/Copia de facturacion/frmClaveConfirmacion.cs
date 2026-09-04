using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using lebrun.clasesData;


namespace lebrun.formularios.facturacion
{
    public partial class frmClaveSupervision : Form
    {

        private static frmClaveSupervision m_FormDefInstance;
        private ConexionBD dataBaseConexion;
        private string sentenciaSql;
        private string tipoDocumento;
        private frmFactura referenciaFactura;
        private bool centinelaClave;
        private string tipoOperacion;

        public frmClaveSupervision()
        {
            InitializeComponent();
        }


        public static frmClaveSupervision DefInstance
        {
            get
            {
                if (m_FormDefInstance == null || m_FormDefInstance.IsDisposed)
                    m_FormDefInstance = new frmClaveSupervision();
                return m_FormDefInstance;
            }
            set
            {
                m_FormDefInstance = value;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {   
            referenciaFactura.Enabled = true;
            referenciaFactura.CentinelaConfirmacion = centinelaClave;
            this.Close();
            //referenciaFactura.despuesConfirmacionConfirmarDoc();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if ((txtClave.Text == "") || (txtClave.Text == null))
            {
                MessageBox.Show("La calve no puede quedar Vacia!!", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else {
                sentenciaSql = null;
                string cla1= null;
                string cla2= null;
                string cla3= null;
                DataTable dt;

                sentenciaSql = "SELECT ctd_clave,ctd_clave2,ctd_clave3 FROM admtipdoccli WHERE ctd_tipo ='$1';";
                sentenciaSql = sentenciaSql.Replace("$1", tipoDocumento);
                dt = dataBaseConexion.fDataTable(sentenciaSql);
                if (dt.Rows.Count > 0) {
                    cla1 = dt.Rows[0][0].ToString();
                    cla2 = dt.Rows[0][1].ToString();
                    cla3 = dt.Rows[0][2].ToString();
                }

                if((txtClave.Text.Equals(cla1)) || (txtClave.Text.Equals(cla2)) || (txtClave.Text.Equals(cla3))){
                    
                    centinelaClave = true;
                    referenciaFactura.CentinelaConfirmacion = centinelaClave;
                    /*referenciaFactura.despuesConfirmacionConfirmarDoc();
                    referenciaFactura.Enabled = true;*/
                    /*referenciaFactura.continuarClave();*/
                    this.Close();

                }else{
                    centinelaClave = false;
                    MessageBox.Show("Clave Incorrecta!!", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtClave.Focus();
                    return;
                }

                dataBaseConexion.cerrarConexion();
            }
        }

        private void frmClaveSupervision_Load(object sender, EventArgs e)
        {
            dataBaseConexion = new ConexionBD();
            sentenciaSql= null;
            tipoDocumento= null;
            centinelaClave= false;
        }

        public void setTipoDoc(string tipo,frmFactura f1) {
            tipoDocumento = tipo;
            referenciaFactura = f1;
        }

        public void setTipoDoc(string tipo, frmFactura f1, string tipoO)
        {
            tipoDocumento = tipo;
            referenciaFactura = f1;
            tipoOperacion = tipoO;
        }

        private void frmClaveSupervision_FormClosing(object sender, FormClosingEventArgs e)
        {}

        private void txtClave_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)(Keys.Enter)) && txtClave.Text != "") {
                button1_Click(new object(), new KeyPressEventArgs((char)(Keys.Enter)));
            }
        }

        private void frmClaveSupervision_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Visible = false;
            if (((tipoDocumento.Equals("FAV")) || (tipoDocumento.Equals("DEV"))) && (tipoOperacion != "1"))
            {
                if (referenciaFactura.CentinelaConfirmacion)
                {
                    //referenciaFactura.continuarClave();
                }
                referenciaFactura.Focus();
            }
            else {
                if (((tipoDocumento.Equals("FAV")) || (tipoDocumento.Equals("DEV"))) && (tipoOperacion == "1")) {
                    //referenciaFactura.despuesdeConfirmacionPrecio();
                }
                referenciaFactura.Focus();
            }
        }

    }
}
