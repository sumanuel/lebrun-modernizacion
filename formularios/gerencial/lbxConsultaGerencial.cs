using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using lebrun.clases.gerencial;
using lebrun.formularios.productos;
using lebrun.clasesData;

namespace lebrun.formularios.gerencial
{
    public partial class lbxConsultaGerencial : Form
    {
        lbxMantProductos productos;
        ConsultaGeneral consulGeneral;


        public lbxConsultaGerencial()
        {
            InitializeComponent();
            AplicarDiseno();
        }

        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {

            
            e.Handled = char.IsNumber(e.KeyChar) || e.KeyChar == 8 ? false : true;
            if (e.KeyChar == (char)(Keys.Enter))
            {

                txtCodigo.Text = string.Format("{0:000000000000000}", Convert.ToDouble(txtCodigo.Text));
                txtCodigo.SelectionStart = 0;
                txtCodigo.SelectionLength = txtCodigo.Text.Length;
                //validar si exite o no el producto
                


                consulGeneral.consultaGereneralProducto(txtCodigo.Text);
                if (consulGeneral.Vacio != "Vacio")
                {
                    cmbPrecios.Items.Clear();
                    cmbPrecios.ResetText();
                    label22.Text = "Escriba un código y presione Enter";
                    txtCateg.Clear();
                    txtSubCat.Clear();
                    txtUniM.Clear();
                    txtExist.Clear();
                    txtPrefa.Clear();
                    txtPedid.Clear();
                    txtDispon.Clear();
                    txtUCost.Clear();
                    txtOferta.Clear();
                    txtEstado.Clear();
                    txtUltCompra.Clear();
                    txtCantComp.Clear();
                    txtProve.Clear();
                    txtDocComp.Clear();
                    txtUltVent.Clear();
                    txtClient.Clear();
                    txtCantVent.Clear();
                    txtDocumVent.Clear();
                    txtUltAjus.Clear();
                    txtCantidad.Clear();
                    txtDocum.Clear();
                    txtCosProm.Clear();
                    txtPreList.Clear();
                    txtDes1.Clear();
                    txtDes2.Clear();
                    txtDes3.Clear();
                    txtOrigen.Clear();
                    //calculo
                    txtCosRes1.Clear();
                    txtCosRes2.Clear();
                    txtCosRes3.Clear();
                    txtCosRes4.Clear();
                    //Principal Proveedor
                    txtProvee.Clear();
                    txtPais.Clear();
                    txtCodAran.Clear();
                    txtDesMon.Clear();
                    txtSigMon.Clear();
                    txttipCam.Clear();
                    txtEnt.Clear();
                    txtCif.Clear();
                    txtFin.Clear();
                    txtGas.Clear();
                    txtOtGas.Clear();
                    txtPXT.Clear();
                    txtD1.Clear();
                    txtD2.Clear();
                    txtD3.Clear();
                    txtMontoVenAnoAC.Clear();
                    txtCanVenAnoAC.Clear();
                    txtVenFilAnoAC.Clear(); ;
                    txtCostVentAnoAC.Clear();
                    txtPorCostVentaAñoAC.Clear();
                    txtCostoCompra.Clear();
                    txtCantCompra.Clear();
                    txt3Meses.Clear();
                    txt6Meses.Clear();
                    txt12Meses.Clear();
                    txtUniVen3.Clear();
                    txtUniVen6.Clear();
                    txtUniVen12.Clear();
                    txtProU3.Clear();
                    txtProU6.Clear();
                    txtProU12.Clear();
                    limpiarExistenciasYCostos();
                    dataGridView2.DataSource = null;
                    dataGridView3.DataSource = null;

                    consulGeneral.consultaGeneralProveedor(txtCodigo.Text);
                    label22.Text = consulGeneral.DescProduc;
                    txtCateg.Text = consulGeneral.DescGrupProduc;
                    txtSubCat.Text = consulGeneral.DescSubGrupProduc;
                    txtUniM.Text = consulGeneral.UndMedProduc;
                    txtExist.Text = consulGeneral.ExisProduc;
                    txtPrefa.Text = consulGeneral.PrefacProduc;
                    txtPedid.Text = consulGeneral.PedidoProduc;
                    txtDispon.Text = "";
                    txtUCost.Text = consulGeneral.UltCostProduc;
                    txtOferta.Text = consulGeneral.PromProduc;
                    txtEstado.Text = consulGeneral.EstadoProduc;
                    txtUltCompra.Text = consulGeneral.FecUltCompProduc;
                    txtCantComp.Text = consulGeneral.UltCantCompProduc;
                    txtProve.Text = consulGeneral.UltProveProduc;
                    txtDocComp.Text = consulGeneral.UltRefCompProduc;
                    txtUltVent.Text = consulGeneral.FecUltVentProduc;
                    txtClient.Text = consulGeneral.UltClienProduc;
                    txtCantVent.Text = consulGeneral.UultCantVent;
                    txtDocumVent.Text = consulGeneral.UltRefVentProduc;
                    txtUltAjus.Text = consulGeneral.FecUltAjusProduc;
                    txtCantidad.Text = consulGeneral.UltCantAjustProduc;
                    txtDocum.Text = consulGeneral.UltRefAjusProduc;

                    txtCosProm.Text = consulGeneral.ContProm;
                    txtPreList.Text = Convert.ToString(consulGeneral.PreLista2);
                    txtDes1.Text = Convert.ToString(consulGeneral.ProvDes1.ToString("n2"));
                    txtDes2.Text = Convert.ToString(consulGeneral.ProvDes2.ToString("n2"));
                    txtDes3.Text = Convert.ToString(consulGeneral.ProvDesc3.ToString("n2"));
                    txtOrigen.Text = consulGeneral.Procecia;
                    //calculo
                    txtCosRes1.Text = Convert.ToString(consulGeneral.CostoRep1.ToString("n2"));
                    txtCosRes2.Text = Convert.ToString(consulGeneral.CostoRep2.ToString("n2"));
                    txtCosRes3.Text = Convert.ToString(consulGeneral.CostoRep3.ToString("n2"));
                    txtCosRes4.Text = Convert.ToString(consulGeneral.CostoRep4.ToString("n2"));
                    //Principal Proveedor

                    txtProvee.Text = consulGeneral.ProveeNombr;
                    txtPais.Text = consulGeneral.DescPais;
                    txtCodAran.Text = consulGeneral.AranDescProduc;
                    txtDesMon.Text = consulGeneral.MonDescrp;
                    txtSigMon.Text = consulGeneral.PinMoned;
                    txttipCam.Text = consulGeneral.HistMond;
                    txtEnt.Text = Convert.ToString(consulGeneral.FacEnt);
                    txtCif.Text = Convert.ToString(consulGeneral.FacCif.ToString("n2"));
                    txtFin.Text = Convert.ToString(consulGeneral.FacFin.ToString("n2"));
                    txtGas.Text = Convert.ToString(consulGeneral.FacGas.ToString("n2"));
                    txtOtGas.Text = Convert.ToString(consulGeneral.FacOtro.ToString("n2"));
                    txtPXT.Text = Convert.ToString(consulGeneral.AranPorsProduc.ToString("n2"));
                    txtD1.Text = consulGeneral.DesProv1;
                    txtD2.Text = consulGeneral.DesProv2;
                    txtD3.Text = consulGeneral.DesProv3;
                    consulGeneral.llenarPrecio(cmbPrecios, txtCodigo.Text);
                    //Datagrip
                    dataGridView2.DataSource = null;
                    dataGridView2.DataSource = consulGeneral.consultaCosto(txtCodigo.Text);
                    dataGridView2.AutoResizeColumns();
                    dataGridView2.Columns[0].DefaultCellStyle.ForeColor = Color.Black;
                    dataGridView2.Columns[0].DefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
                    dataGridView2.Columns[0].DefaultCellStyle.BackColor = Color.FromArgb(232, 240, 250);
                    dataGridView2.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopCenter;
                    dataGridView2.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopCenter;
                    dataGridView2.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopCenter;
                    dataGridView2.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
                    dataGridView2.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
                    dataGridView2.Columns[5].DefaultCellStyle.Format = "n2";
                    dataGridView2.Columns[6].DefaultCellStyle.Format = "n2";
                    consulGeneral.MovientoAñoActual(txtCodigo.Text);
                    txtMontoVenAnoAC.Text = consulGeneral.MontoVenta.ToString("n2");
                    txtCanVenAnoAC.Text = consulGeneral.CantVendida.ToString("n2");
                    txtVenFilAnoAC.Text = consulGeneral.CantVendidaFil.ToString("n2");
                    txtCostVentAnoAC.Text = consulGeneral.CostoVenta.ToString("n2");
                    txtPorCostVentaAñoAC.Text = consulGeneral.PorCostVent.ToString("n2");
                    txtCostoCompra.Text = consulGeneral.CostoCompras.ToString("n2");
                    txtCantCompra.Text = consulGeneral.CantidadComprada.ToString("n2");
                    consulGeneral.PromedioVentas(txtCodigo.Text);
                    txt3Meses.Text = consulGeneral.Vet3Meses.ToString("n2");
                    txt6Meses.Text = consulGeneral.Vet6Meses.ToString("n2");
                    txt12Meses.Text = consulGeneral.Vet12Meses.ToString("n2");
                    txtUniVen3.Text = consulGeneral.CanVen3Meses.ToString("n2");
                    txtUniVen6.Text = consulGeneral.CanVen6Meses.ToString("n2");
                    txtUniVen12.Text = consulGeneral.CanVen12Meses.ToString("n2");
                    txtProU3.Text = consulGeneral.Uti3Meses.ToString("n2");
                    txtProU6.Text = consulGeneral.Uti6Meses.ToString("n2");
                    txtProU12.Text = consulGeneral.Uti12Meses.ToString("n2");
                    dataGridView3.DataSource = null;
                    //historial de costo
                    dataGridView3.DataSource = consulGeneral.datosHistoriaVentas(txtCodigo.Text);
                    dataGridView3.Columns[1].DefaultCellStyle.Format = "n2";
                    dataGridView3.Columns[2].DefaultCellStyle.Format = "n2";
                    dataGridView3.Columns[3].DefaultCellStyle.Format = "n2";
                    dataGridView3.Columns[4].DefaultCellStyle.Format = "n2";
                    dataGridView3.Columns[5].DefaultCellStyle.Format = "n2";
                    dataGridView3.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
                    dataGridView3.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
                    dataGridView3.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
                    dataGridView3.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
                    dataGridView3.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
                    dataGridView3.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
                    cargarExistenciasYCostos(txtCodigo.Text);
                    costoMontVend();
                }
                else 
                {
                   txtCodigo.Focus();
                   cmbPrecios.Items.Clear();
                   cmbPrecios.ResetText();
                   label22.Text = "Escriba un código y presione Enter";
                   txtCateg.Clear();
                   txtSubCat.Clear();
                   txtUniM.Clear();
                   txtExist.Clear();
                   txtPrefa.Clear();
                   txtPedid.Clear();
                   txtDispon.Clear();
                   txtUCost.Clear();
                   txtOferta.Clear();
                   txtEstado.Clear();
                   txtUltCompra.Clear();
                   txtCantComp.Clear();
                   txtProve.Clear();
                   txtDocComp.Clear();
                   txtUltVent.Clear();
                   txtClient.Clear();
                   txtCantVent.Clear();
                   txtDocumVent.Clear();
                   txtUltAjus.Clear();
                   txtCantidad.Clear();
                   txtDocum.Clear();
                   txtCosProm.Clear();
                   txtPreList.Clear();
                   txtDes1.Clear();
                   txtDes2.Clear();
                   txtDes3.Clear();
                   txtOrigen.Clear();
                   //calculo
                   txtCosRes1.Clear();
                   txtCosRes2.Clear();
                   txtCosRes3.Clear();
                   txtCosRes4.Clear();
                   //Principal Proveedor
                   txtProvee.Clear();
                   txtPais.Clear();
                   txtCodAran.Clear();
                   txtDesMon.Clear();
                   txtSigMon.Clear();
                   txttipCam.Clear();
                   txtEnt.Clear();
                   txtCif.Clear();
                   txtFin.Clear();
                   txtGas.Clear();
                   txtOtGas.Clear();
                   txtPXT.Clear();
                   txtD1.Clear();
                   txtD2.Clear();
                   txtD3.Clear();
                   txtMontoVenAnoAC.Clear();
                   txtCanVenAnoAC.Clear();
                   txtVenFilAnoAC.Clear(); ;
                   txtCostVentAnoAC.Clear();
                   txtPorCostVentaAñoAC.Clear();
                   txtCostoCompra.Clear();
                   txtCantCompra.Clear();
                   txt3Meses.Clear();
                   txt6Meses.Clear();
                   txt12Meses.Clear();
                   txtUniVen3.Clear();
                   txtUniVen6.Clear();
                   txtUniVen12.Clear();
                   txtProU3.Clear();
                   txtProU6.Clear();
                   txtProU12.Clear();
                   limpiarExistenciasYCostos();
                   dataGridView2.DataSource = null;
                   dataGridView3.DataSource = null;
                }
               
            }
        }

        private void lbxConsultaGerencial_Load(object sender, EventArgs e)
        {
            productos = new lbxMantProductos();
            consulGeneral = new ConsultaGeneral();
      
        }

        private void button3_Click(object sender, EventArgs e)
        {
            productos.frmProduct = "Producto";
            productos.proCual = "Admin";
            productos.productoPasado += new lbxMantProductos.gerenteAdmin(ejecutarProductSelec);
            productos.ShowDialog();
            txtCodigo.Focus();
            txtCodigo.SelectionStart = 0;
            txtCodigo.SelectionLength = txtCodigo.Text.Length;
        }
        // Kardex de la pestana Existencias y costo en movimientos de la pestana Costos
        private void cargarExistenciasYCostos(string codigo)
        {
            decimal saldoPrevio;
            dataGridView1.DataSource = consulGeneral.consultaMovimientosInventario(codigo, out saldoPrevio);
            MostrarNotaExistencias(saldoPrevio, dataGridView1.Rows.Count);
            dataGridView4.DataSource = consulGeneral.consultaCostoEnMovimientos(codigo);
        }

        private void limpiarExistenciasYCostos()
        {
            dataGridView1.DataSource = null;
            dataGridView4.DataSource = null;
            MostrarNotaExistencias(0m, 0);
        }

        public void costoMontVend() 
        {
            for (int i = 0; i < dataGridView3.Rows.Count; i++)
            {
                string val = dataGridView3.Rows[i].Cells[5].Value.ToString();
                if (val == "")
                {

                    dataGridView3.Rows[i].Cells[5].Value = "0,00";
                }
            }
        }

        private void lbxConsultaGerencial_Shown(object sender, EventArgs e)
        {
            txtCodigo.Focus();
        }

        public void ejecutarProductSelec(string datoproducto)
        {
            txtCodigo.Text = datoproducto;
        }
    }
}
