using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using lebrun.clases.complementos;
using lebrun.formularios.bancos;

namespace lebrun.formularios.complementos
{
    public partial class frmProveedores : Form
    {
        proveedor proveedor;
        public string Eleccion;
        string actEco;
        public string idPc;
        public string ipPc;
        public string modulo="Proveedores";
        public frmProveedores()
        {
            InitializeComponent();
            cmbActEcono.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbSituacion.DropDownStyle = ComboBoxStyle.DropDownList;
            cmdResidente.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPais.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbRegion.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbEstado.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMun.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbParro.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbConDIva.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTipCuenta.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbConDPag.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCxP.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbZL.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbClasi.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbODCDME.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTipPer.DropDownStyle = ComboBoxStyle.DropDownList;
            ttmensajes.SetToolTip(BtnRegistra,"Registra");
            ttmensajes.SetToolTip(btnActulizar,"Actulizar");
            ttmensajes.SetToolTip(btnLimpiar, "Limpiar");
            ttmensajes.SetToolTip(btnCancelar, "Cancelar");
        }

        private void btnDivisa_Click(object sender, EventArgs e)
        {
            lbxMonedad moneda = new lbxMonedad();
            moneda.signoMoney+=new lbxMonedad.lbxmoneda(ejecutarMoneda);
            moneda.frmProduct = "Producto";
            moneda.ShowDialog();
            txtDivProv.Focus();
        }
        public void ejecutarMoneda(string dataMoneda)
        {
            txtDivProv.Text = dataMoneda;
        }
        public void ejecutarBanco(string dataBanco) 
        {
            txtBanc.Text = dataBanco;
        }

        private void btnActEconomica_Click(object sender, EventArgs e)
        {
            lbxClasificacionEconomica economica = new lbxClasificacionEconomica();
            economica.ShowDialog();
        }

        private void frmProveedores_Load(object sender, EventArgs e)
        {

            proveedor = new proveedor();
            proveedor.llenarPais(cmbPais);
        }

        private void txtCodigoProvee_KeyPress(object sender, KeyPressEventArgs e)
        {
            string codigoExiste;
            e.Handled = char.IsNumber(e.KeyChar) || e.KeyChar == 8 ? false : true;
            if ((txtCodigoProvee.Text.Length > 0 && e.KeyChar == 13))
            {
                txtCodigoProvee.Text = string.Format("{0:00000000}", Convert.ToDouble(txtCodigoProvee.Text));
                codigoExiste = proveedor.validarProveedor(txtCodigoProvee.Text);
                if (codigoExiste == txtCodigoProvee.Text)
                {
                    MessageBox.Show("El Codigo de ese Proveedor Ya Existe!!", "Invalido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCodigoProvee.Clear();
                    txtCodigoProvee.Focus();
                }
                else
                {
                    txtDesProvee.Focus();
                }
            }
        }

        private void txtDesProvee_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((txtDesProvee.Text.Length > 0 && e.KeyChar == 13))
            {
                txtRifProv.Text = txtCodigoProvee.Text;
                txtRifProv.Focus();
            }
        }

        private void txtRifProv_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtRifProv.Text.Length == 0)
            {
                if (char.IsLetter(e.KeyChar))
                {
                    if (!((e.KeyChar.ToString().ToUpper().Equals("J")) || (e.KeyChar.ToString().ToUpper().Equals("G")) || (e.KeyChar.ToString().ToUpper().Equals("V")) || (e.KeyChar.ToString().ToUpper().Equals("E")) || (e.KeyChar.ToString().ToUpper().Equals("P"))))
                    {
                        MessageBox.Show("El RIF debe empezar con J,G,V,E,P!!", "Advertencia ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtRifProv.Focus();
                        e.Handled = true;
                    }
                }
                else
                {
                    MessageBox.Show("El RIF debe empezar con J,G,V,E,P!!", "Advertencia ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtRifProv.Focus();
                    e.Handled = true;
                }
            }
            else
            {
                if ((txtRifProv.Text[0].ToString().ToUpper().Equals("J")) || (txtRifProv.Text[0].ToString().ToUpper().Equals("G")) || (txtRifProv.Text[0].ToString().ToUpper().Equals("V")) || (txtRifProv.Text[0].ToString().ToUpper().Equals("E")) || (txtRifProv.Text[0].ToString().ToUpper().Equals("P")))
                {
                    if ((char.IsLetter(e.KeyChar)))
                    {
                        e.Handled = true;
                    }
                    if (char.IsSymbol(e.KeyChar))
                    {
                        e.Handled = true;
                    }
                    if (char.IsPunctuation(e.KeyChar)) { e.Handled = true; }
                    btnDivisa.Focus();
                }
                else
                {
                    if ((!((e.KeyChar.ToString().ToUpper().Equals("J")) || (e.KeyChar.ToString().ToUpper().Equals("G")) || (e.KeyChar.ToString().ToUpper().Equals("V")) || (e.KeyChar.ToString().ToUpper().Equals("E")) || (e.KeyChar.ToString().ToUpper().Equals("P")))))
                    {
                        MessageBox.Show("El RIF debe empezar con J,G,V,E,P!!", "Advertencia ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtRifProv.Focus();
                        e.Handled = true;
                    }
                }
                
            }
        }

        private void txtDivProv_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((txtDivProv.Text.Length > 0 && e.KeyChar == 13))
            {
                cmbActEcono.Focus();
            }
        }

        private void cmbActEcono_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((cmbActEcono.Text.Length > 0 && e.KeyChar == 13))
            {
                cmbSituacion.Focus();
            }
        }

        private void cmbSituacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (( e.KeyChar == 13))
            {
                cmdResidente.Focus();
            }
        }

        private void cmdResidente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((cmdResidente.Text.Length > 0 && e.KeyChar == 13))
            {
                txtReIva.Focus();
            }
        }

        private void txtReIva_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsNumber(e.KeyChar) || e.KeyChar == 8 ? false : true;
            if ((e.KeyChar == 13))
            {
                cmbPais.Focus();
            }
        }

        private void cmbPais_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == 13))
            {
                cmbRegion.Focus();
            }
        }

        private void cmbConDIva_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == 13))
            {
                BtnRegistra.Focus();
            }
        }

        private void cmbRegion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == 13))
            {
                cmbEstado.Focus();
            }
        }

        private void cmbEstado_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (( e.KeyChar == 13))
            {
                cmbMun.Focus();
            }
        }

        private void cmbMun_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == 13))
            {
                cmbParro.Focus();
            }
        }

        private void cmbParro_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == 13))
            {
                txtD1.Focus();
            }
        }

        private void txtD1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == 13))
            {
                txtD2.Focus();
            }
        }

        private void txtD2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == 13))
            {
                txtD3.Focus();
            }
        }

        private void txtD3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == 13))
            {
                txtCodArea.Focus();
            }
        }

        private void txtCodArea_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsNumber(e.KeyChar) || e.KeyChar == 8 ? false : true;
            if ((e.KeyChar == 13))
            {
                txtTlMaster.Focus();
            }
        }


        private void txtFax_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsNumber(e.KeyChar) || e.KeyChar == 8 ? false : true;
            if ((e.KeyChar == 13))
            {
                txtCelu.Focus();
            }
        }

        private void txtCelu_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsNumber(e.KeyChar) || e.KeyChar == 8 ? false : true;
            if ((e.KeyChar == 13))
            {
                txtOtrCelu.Focus();
            }
        }

        private void txtOtrCelu_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsNumber(e.KeyChar) || e.KeyChar == 8 ? false : true;
            if ((e.KeyChar == 13))
            {
                txtCorrElec.Focus();
            }
        }

        private void txtCorrElec_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == 13))
            {
                txtPagWeb.Focus();
            }
        }

        private void txtPagWeb_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == 13))
            {
                dateTimePicker2.Focus();
            }
        }

        private void dateTimePicker2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == 13))
            {
                cmbTipPer.Focus();
            }
        }

        private void cmbTipPer_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == 13))
            {
                cmbConDIva.Focus();
            }
        }

        private void cmbPais_SelectedIndexChanged(object sender, EventArgs e)
        {
            proveedor.llenarCodPais(cmbPais.Text);
            txtCodPais.Text = proveedor.CodPais;
        }

        private void txtRifProv_Leave(object sender, EventArgs e)
        {
            cmbTipPer.Items.Clear();
            if (txtRifProv.Text != "")
            {
                txtRifProv.Text = txtRifProv.Text.ToUpper();
                if ((txtRifProv.Text[0].ToString().ToUpper().Equals("J")) || (txtRifProv.Text[0].ToString().ToUpper().Equals("G")))
                {
                    cmbTipPer.Items.Add("Juridica");
                    cmbTipPer.SelectedIndex = 0;
                }
                if ((txtRifProv.Text[0].ToString().ToUpper().Equals("V")) || (txtRifProv.Text[0].ToString().ToUpper().Equals("E")) || (txtRifProv.Text[0].ToString().ToUpper().Equals("P")))
                {
                    cmbTipPer.Items.Add("Natural");
                    cmbTipPer.SelectedIndex = 0;
                }
            }
            
        }

        private void cmdResidente_Leave(object sender, EventArgs e)
        {
            if (cmdResidente.Text == "Si")
            {
                txtReIva.Text = "75";
                txtReIva.ReadOnly = false;
            }
            else
            {
                txtReIva.ReadOnly = true;
                txtReIva.Text = "0";
            }
        }

        private void cmbPais_Leave(object sender, EventArgs e)
        {
            if (txtCodPais.Text == "0089")
            {
                txtCodRegion.Text = "0001";
                cmbRegion.Items.Add("Cental");
                cmbRegion.SelectedIndex = 0;
                txtCodEstado.Text = "0001";
                cmbEstado.Items.Add("Distrito Capital");
                cmbEstado.SelectedIndex = 0;
                cmbMun.Items.Add("0001");
                cmbMun.SelectedIndex = 0;
                cmbParro.Items.Add("0001");
                cmbParro.SelectedIndex = 0;
            }
            else 
            {
                txtCodRegion.Clear();
                cmbRegion.Items.Clear();
                txtCodEstado.Clear();
                cmbEstado.Items.Clear();
                cmbMun.Items.Clear();
                cmbParro.Items.Clear();
            
            
            
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            lbxBancos bancosS = new lbxBancos();
            bancosS.bancoSelec += new lbxBancos.lbxBanco(ejecutarBanco);
            bancosS.Show();
        }



        private void cmbTipCuenta_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == 13))
            {
                txtCodCueProvee.Focus();
            }
        }

        private void txtCodCueProvee_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == 13))
            {
                txtLimCre.Focus();
            }
        }

        private void txtLimCre_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsNumber(e.KeyChar) || e.KeyChar == 8 || e.KeyChar== 46 ? false : true;
            if ((e.KeyChar == 13))
            {
                txtDiaDPlazo.Focus();
            }
        }

        private void txtDiaDPlazo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsNumber(e.KeyChar) || e.KeyChar == 8 ? false : true;
            if ((e.KeyChar == 13))
            {
                cmbConDPag.Focus();
            }
        }

        private void cmbConDPag_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == 13))
            {
                cmbCxP.Focus();
            }
        }

        private void cmbCxP_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == 13))
            {
                cmbZL.Focus();
            }
        }

        private void cmbZL_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == 13))
            {
                cmbClasi.Focus();
            }
        }

        private void cmbClasi_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == 13))
            {
                cmbODCDME.Focus();
            }
        }

        private void cmbODCDME_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == 13))
            {
                txtDes1.Focus();
            }
        }

     

        private void txtDes1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsNumber(e.KeyChar) || e.KeyChar == 8 || e.KeyChar == 46 ? false : true;
            if ((e.KeyChar == 13))
            {
                txtDes2.Focus();
            }
        }

        private void txtDes2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsNumber(e.KeyChar) || e.KeyChar == 8 || e.KeyChar == 46 ? false : true;
            if ((e.KeyChar == 13))
            {
                txtDes3.Focus();
            }
        }

        private void txtDes3_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsNumber(e.KeyChar) || e.KeyChar == 8 || e.KeyChar == 46 ? false : true;
            if ((e.KeyChar == 13))
            {
                txtObser1.Focus();
            }
        }

        private void txtObser1_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }

        private void txtObser2_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void txtObser3_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }

        private void txtBanc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == 13))
            {
                cmbTipCuenta.Focus();
            }
        }

        private void cmbClasi_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cmbClasi.Text=="BUENA")
            {
                textBox1.Text="0001";
            }
            else if(cmbClasi.Text=="REGULAR")
            {
               textBox1.Text="0002";
            }
               else if(cmbClasi.Text=="EXCELENTE")
            {
               textBox1.Text="0003";
            }
               else if(cmbClasi.Text=="PESIMA")
            {
               textBox1.Text="0004";
            }
               else if(cmbClasi.Text=="MALA")
            {
               textBox1.Text="0005";
            }
        }

        private void BtnRegistra_Click(object sender, EventArgs e)
        {
            string accion = "Agre";
            if(cmbActEcono.Text=="INDEFINIDA")
            {
                actEco = "000001";
            }
            else if(cmbActEcono.Text=="ECONOMICA")
            {
                actEco = "000002";
            }
            else if(cmbActEcono.Text=="ADMINITRATIVA")
            {
                actEco = "000003";
            }
            else  if(cmbActEcono.Text=="CONTABLE")
            {
                actEco = "000004";
            } 
          
            if (validarcampos() == false)
            {
                proveedor.insertProveedor(txtCodigoProvee.Text, txtDesProvee.Text, txtRifProv.Text, txtCodPais.Text, txtCodEstado.Text
                    , cmbMun.Text, cmbParro.Text, txtD1.Text, txtD2.Text, txtD3.Text, txtCodArea.Text, txtTlMaster.Text, txtTlf.Text, txtFax.Text,
                    txtCelu.Text, txtOtrCelu.Text, txtCorrElec.Text, txtPagWeb.Text, dateTimePicker2.Text, cmdResidente.Text, cmbTipPer.Text,
                  cmbSituacion.Text, textBox1.Text, cmbClasi.Text, cmbConDIva.Text, "", txtDivProv.Text, txtDes1.Text.Replace(",", "."), txtLimCre.Text.Replace(",", "."), txtDiaDPlazo.Text,
                  cmbConDPag.Text, "0.00", "0", actEco, "", txtCodCueProvee.Text, "", "", cmbCxP.Text, cmbZL.Text, cmbODCDME.Text, txtBanc.Text, cmbTipCuenta.Text, "", "", txtObser1.Text,
                  txtCodRegion.Text, txtReIva.Text, txtDes2.Text.Replace(",", "."), txtDes3.Text.Replace(",", "."));
                proveedor.trazUsuario(idPc, ipPc, modulo, accion, txtCodigoProvee.Text);
                MessageBox.Show("Se a Registrado Correctamente!!", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }
        private bool validarcampos()
        {
            bool ciclipe = false;
            if (txtCodigoProvee.Text == "")
            {
                MessageBox.Show("El Codigo Proveedor  Es Un Campo Obligatorio!!", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                ciclipe = true;
                txtCodigoProvee.Focus();
            }
            if (txtDesProvee.Text== "")
            {
                MessageBox.Show("La Descripcion de Proveedor Es Un Campo Obligatorio!!", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                ciclipe = true;
                txtDesProvee.Focus();
            }
            if (txtRifProv.Text == "")
            {
                MessageBox.Show("El Rif De proveedor Es Un Campo Obligatorio!!", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                ciclipe = true;
                txtRifProv.Focus();
            }
            if (txtDivProv.Text == "")
            {
                MessageBox.Show("El Divisa Es Un Campo Obligatorio!!", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                ciclipe = true;
                txtDivProv.Focus();
            }
            if (cmbActEcono.Text == "")
            {
                MessageBox.Show("La Activida Economica Es Un Campo Obligatorio!!", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                ciclipe = true;
                cmbActEcono.Focus();
            }
            return ciclipe;
        }

        private void txtTlMaster_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTlMaster_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsNumber(e.KeyChar) || e.KeyChar == 8 ? false : true;
            if ((e.KeyChar == 13))
            {
                txtTlf.Focus();
            }
        }

        private void txtTlf_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsNumber(e.KeyChar) || e.KeyChar == 8 ? false : true;
            if ((e.KeyChar == 13))
            {
                txtFax.Focus();
            }
        }

        private void cmbConDIva_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            
            if ((e.KeyChar == 13))
            {
                BtnRegistra.Focus();
            }
        }

        private void frmProveedores_Shown(object sender, EventArgs e)
        {
            if (Eleccion == "Agregar") 
            {
                BtnRegistra.Visible = true;
                btnActulizar.Visible = false;
                txtCodPais.Text = "0089";
                if (txtCodPais.Text == "0089")
                {
                    cmbPais.Text = "VENEZUELA";
                    txtCodRegion.Text = "0001";
                    cmbRegion.Items.Add("Cental");
                    cmbRegion.SelectedIndex = 0;
                    txtCodEstado.Text = "0001";
                    cmbEstado.Items.Add("Distrito Capital");
                    cmbEstado.SelectedIndex = 0;
                    cmbMun.Items.Add("0001");
                    cmbMun.SelectedIndex = 0;
                    cmbParro.Items.Add("0001");
                    cmbParro.SelectedIndex = 0;
                }
            }
            else if (Eleccion == "Modificar")
            {
                dateTimePicker2.Visible = false;
                btnActulizar.Visible = true;
                btnLimpiar.Visible = false;
                txtCodigoProvee.ReadOnly = true;
                BtnRegistra.Visible = false;

                if (textBox1.Text == "0001")
                {
                    cmbClasi.Text = "BUENA";
                }
                else if (textBox1.Text == "0002")
                {
                    cmbClasi.Text = "REGULAR";
                }
                else if (textBox1.Text == "0003")
                {
                    cmbClasi.Text = "EXCELENTE";
                }
                else if (textBox1.Text == "0004")
                {
                    cmbClasi.Text = "PESIMA";
                }
                else if (textBox1.Text == "0005")
                {
                    cmbClasi.Text = "MALA";
                }
                else 
                {
                }
                if (txtCodPais.Text == "0089")
                {
                    cmbPais.Text = "VENEZUELA";
                    txtCodRegion.Text = "0001";
                    cmbRegion.Items.Add("Cental");
                    cmbRegion.SelectedIndex = 0;
                    txtCodEstado.Text = "0001";
                    cmbEstado.Items.Add("Distrito Capital");
                    cmbEstado.SelectedIndex = 0;
                    cmbMun.Items.Add("0001");
                    cmbMun.SelectedIndex = 0;
                    cmbParro.Items.Add("0001");
                    cmbParro.SelectedIndex = 0;
                }
                else
                {
                    proveedor.descriPais(txtCodPais.Text);
                    cmbPais.Text = proveedor.ProvDescPais;
                    txtCodRegion.Clear();
                    cmbRegion.Items.Clear();
                    txtCodEstado.Clear();
                    cmbEstado.Items.Clear();
                    cmbMun.Items.Clear();
                    cmbParro.Items.Clear();
                }
            }
            else if (Eleccion == "Visor") 
            {
                btnDivisa.Visible = false;
                btnBancos.Visible = false;
                dateTimePicker2.Visible = false;
                BtnRegistra.Visible = false;
                btnActulizar.Visible = false;
                btnLimpiar.Visible = false;
                txtCodigoProvee.ReadOnly = true;
                txtDesProvee.ReadOnly = true;
                txtRifProv.ReadOnly = true;
                txtDivProv.ReadOnly = true;
                cmbActEcono.Enabled = false;
                cmbSituacion.Enabled = false;
                cmdResidente.Enabled = false;
                txtReIva.ReadOnly = true;
                txtCodPais.ReadOnly = true;
                cmbPais.Enabled = false;
                txtCodRegion.Enabled = true;
                cmbRegion.Enabled = false;
                txtCodEstado.ReadOnly = true;
                cmbEstado.Enabled = false;
                cmbMun.Enabled = false;
                cmbParro.Enabled = false;
                txtD1.ReadOnly = true;
                txtD2.ReadOnly = true;
                txtD3.ReadOnly = true;
                txtCodArea.ReadOnly = true;
                txtTlMaster.ReadOnly = true;
                txtTlf.ReadOnly = true;
                txtFax.ReadOnly = true;
                txtCelu.ReadOnly = true;
                txtOtrCelu.ReadOnly = true;
                txtCorrElec.ReadOnly = true;
                txtPagWeb.ReadOnly = true;
                dateTimePicker2.Enabled = false;
                cmbTipPer.Enabled = false;
                cmbConDIva.Enabled = false;
                txtBanc.ReadOnly = true;
                cmbTipCuenta.Enabled = false;
                txtCodCueProvee.ReadOnly = true;
                txtLimCre.ReadOnly = true;
                txtDiaDPlazo.ReadOnly = true;
                cmbConDPag.Enabled = false;
                cmbCxP.Enabled = false;
                cmbZL.Enabled = false;
                cmbClasi.Enabled = false;
                cmbODCDME.Enabled = false;
                txtDes1.ReadOnly = true;
                txtDes2.ReadOnly = true;
                txtDes3.ReadOnly = true;
                txtObser1.ReadOnly = true;
                dateTimePicker2.Visible = false;
                txtCodigoProvee.ReadOnly = true;

                if (textBox1.Text == "0001")
                {
                    cmbClasi.Text = "BUENA";
                }
                else if (textBox1.Text == "0002")
                {
                    cmbClasi.Text = "REGULAR";
                }
                else if (textBox1.Text == "0003")
                {
                    cmbClasi.Text = "EXCELENTE";
                }
                else if (textBox1.Text == "0004")
                {
                    cmbClasi.Text = "PESIMA";
                }
                else if (textBox1.Text == "0005")
                {
                    cmbClasi.Text = "MALA";
                }
                else
                {
                }
                if (txtCodPais.Text == "0089")
                {
                    cmbPais.Text = "VENEZUELA";
                    txtCodRegion.Text = "0001";
                    cmbRegion.Items.Add("Cental");
                    cmbRegion.SelectedIndex = 0;
                    txtCodEstado.Text = "0001";
                    cmbEstado.Items.Add("Distrito Capital");
                    cmbEstado.SelectedIndex = 0;
                    cmbMun.Items.Add("0001");
                    cmbMun.SelectedIndex = 0;
                    cmbParro.Items.Add("0001");
                    cmbParro.SelectedIndex = 0;
                }
                else
                {
                    proveedor.descriPais(txtCodPais.Text);
                    cmbPais.Text = proveedor.ProvDescPais;
                    txtCodRegion.Clear();
                    cmbRegion.Items.Clear();
                    txtCodEstado.Clear();
                    cmbEstado.Items.Clear();
                    cmbMun.Items.Clear();
                    cmbParro.Items.Clear();
                }
            }
        }

        private void frmProveedores_FormClosed(object sender, FormClosedEventArgs e)
        {
            lbxProveedores lbxprovee = new lbxProveedores();
            lbxprovee.frmProduct = "Producto";
            lbxprovee.ipMaq = ipPc;
            lbxprovee.IdMaq = idPc;
            lbxprovee.Show();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtCodigoProvee.Clear();
            txtDesProvee.Clear();
            txtRifProv.Clear();
            txtDivProv.Clear();
            cmbActEcono.Text = "INDEFINIDA";
            cmbSituacion.Text = "Activo";
            cmdResidente.Text = "Si";
            txtReIva.Clear();
            txtCodPais.Clear();
            cmbPais.Text = "";
            txtCodRegion.Text = "";
            cmbRegion.Text = "";
            txtCodEstado.Clear();
            cmbEstado.Text = "";
            cmbMun.Text = "";
            cmbParro.Text = "";
            txtD1.Clear();
            txtD2.Clear();
            txtD3.Clear();
            txtCodArea.Clear();
            txtTlMaster.Clear();
            txtTlf.Clear();
            txtFax.Clear();
            txtCelu.Clear();
            txtOtrCelu.Clear();
            txtCorrElec.Clear();
            txtPagWeb.Clear();
            dateTimePicker2.Text = "";
            cmbTipPer.Text = "";
            cmbConDIva.Text = "Si";
            txtBanc.Clear();
            cmbTipCuenta.Text = "";
            txtCodCueProvee.Clear();
            txtLimCre.Text = "0.00";
            txtDiaDPlazo.Text = "0";
            cmbConDPag.Text = "Credito";
            cmbCxP.Text = "No";
            cmbZL.Text = "No";
            cmbClasi.Text = "BUENA";
            cmbODCDME.Text = "No";
            txtDes1.Text = "0.00";
            txtDes2.Text = "0.00";
            txtDes3.Text = "0.00";
            txtObser1.Clear();
        }

        private void btnActulizar_Click(object sender, EventArgs e)
        {
            string accion = "Modi";
            if (cmbActEcono.Text == "INDEFINIDA")
            {
                actEco = "000001";
            }
            else if (cmbActEcono.Text == "ECONOMICA")
            {
                actEco = "000002";
            }
            else if (cmbActEcono.Text == "ADMINITRATIVA")
            {
                actEco = "000003";
            }
            else if (cmbActEcono.Text == "CONTABLE")
            {
                actEco = "000004";
            } 
            if (validarcampos() == false) 
            {
                proveedor.updateProveedor(txtCodigoProvee.Text, txtDesProvee.Text, txtRifProv.Text, txtCodPais.Text, txtCodEstado.Text
                    ,cmbMun.Text, cmbParro.Text, txtD1.Text, txtD2.Text, txtD3.Text, txtCodArea.Text, txtTlMaster.Text, txtTlf.Text, txtFax.Text,
                    txtCelu.Text, txtOtrCelu.Text, txtCorrElec.Text, txtPagWeb.Text, dateTimePicker2.Text, cmdResidente.Text, cmbTipPer.Text,
                    cmbSituacion.Text, textBox1.Text, cmbClasi.Text, cmbConDIva.Text, "", txtDivProv.Text, txtDes1.Text.Replace(",", "."), txtLimCre.Text.Replace(",", "."), txtDiaDPlazo.Text,
                    cmbConDPag.Text, "0.00", "0", actEco, "", txtCodCueProvee.Text, "", "", cmbCxP.Text, cmbZL.Text, cmbODCDME.Text, txtBanc.Text, cmbTipCuenta.Text, "", "", txtObser1.Text,
                    txtCodRegion.Text, txtReIva.Text.Replace(",", "."), txtDes2.Text.Replace(",", "."), txtDes3.Text.Replace(",", "."));
                proveedor.trazUsuario(idPc, ipPc, modulo, accion, txtCodigoProvee.Text);
                MessageBox.Show("Se a Actualizado Correctamente!!", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }

    }
}