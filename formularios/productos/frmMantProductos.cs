using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using lebrun.formularios.productos;
using lebrun.clases.producto;
using MySql.Data.MySqlClient;
using lebrun.clasesData;
using lebrun.formularios.complementos;

namespace lebrun.formularios.productos
{
    public partial class frmMantProductos : Form
    {
        private Productos productos;
        frmAsignarProveedor proveedor;
        DataSet ds=new DataSet();
        AutoCompleteStringCollection source = new AutoCompleteStringCollection();
        AutoCompleteStringCollection source1 = new AutoCompleteStringCollection();
        AutoCompleteStringCollection source2 = new AutoCompleteStringCollection();
        public string codCategoria;
        public string eleccion;
        public string idPc;
        public string ipPc;
        public string modulo;
        string[] campoDatos = new string[21];

        public frmMantProductos()
        {
            InitializeComponent();
            cmdDescEdit.DropDownStyle = ComboBoxStyle.DropDownList;
            cmdEstado.DropDownStyle = ComboBoxStyle.DropDownList;
            cmdProced.DropDownStyle = ComboBoxStyle.DropDownList;
            cmdIva.DropDownStyle = ComboBoxStyle.DropDownList;
            this.ttmensajes.SetToolTip(this.BtnRegistra, "Registra");
            this.ttmensajes.SetToolTip(this.btnActulizar, "Actualizar");
            this.ttmensajes.SetToolTip(this.btnCancelar, "Cancelar");
            this.ttmensajes.SetToolTip(this.btnLimpiar, "Limpiar");
            this.ttmensajes.SetToolTip(this.button1, "Categoria");
            this.ttmensajes.SetToolTip(this.button2, "buscar Sub-Categoria");
            this.ttmensajes.SetToolTip(this.button3, "Buscar Arancel");
            this.ttmensajes.SetToolTip(this.button4, "Buscar Iva");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            lbxArancel arancel = new lbxArancel();
            arancel.eleccion = "Selecionar";
            arancel.frmProduct = "Producto";
            arancel.aracelPasado+=new lbxArancel.aracelPD(ejecutarAracelSelec);
            arancel.arancelDPasado += new lbxArancel.aracelPDD(ejecutarAracelSelecDescrip);
            arancel.ShowDialog();
            txtArancel.Focus();
        }
        public void ejecutarAracelSelec(string datoArancel) 
        {
            txtArancel.Text = datoArancel;
        }
        public void ejecutarAracelSelecDescrip(string datoDesArancel) 
        {
            txtArancelD.Text = datoDesArancel;
        }

        private void frmMantProductos_Load(object sender, EventArgs e)
        {
            productos = new Productos();
            proveedor = new frmAsignarProveedor();
                //Source();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            lbxALICU_IVA iva = new lbxALICU_IVA();
            iva.frmProduct = "Producto";
            iva.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }





        private void txtFabricate_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsNumber(e.KeyChar) || e.KeyChar == 8 ? false : true;
            cmdDescEdit.Focus();
        }

        private void cmdDescEdit_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsNumber(e.KeyChar) || e.KeyChar == 8 ? false : true;
            cmdProced.Focus();
        }

        private void cmdProced_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsNumber(e.KeyChar) || e.KeyChar == 8 ? false : true;
            cmdEstado.Focus();
        }

        private void cmdIva_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsNumber(e.KeyChar) || e.KeyChar == 8 ? false : true;
            txtPDD.Focus();
        }

        private void cmdEstado_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsNumber(e.KeyChar) || e.KeyChar == 8 ? false : true;
            cmdIva.Focus();
        }

        private void txtArancel_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtComVenta.Focus();
        }

        private void txtPDD_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsNumber(e.KeyChar) || e.KeyChar == 8 ? false : true;
            if ((txtPDD.Text.Length > 0 && e.KeyChar == 13))
            {
                txtComCobra.Focus();
            }
            
        }
        public void Source() 
        {

            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            dt = productos.imformaAracel();
            foreach (DataRow row in dt.Rows) 
            {
                source.Add(Convert.ToString(row["ara_descri"]));
            }
        
        }

        private void txtArancel_TextChanged(object sender, EventArgs e)
        {
            txtArancel.AutoCompleteCustomSource = source;
            txtArancel.AutoCompleteMode = AutoCompleteMode.Suggest;
            txtArancel.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }

        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            string codigoExiste;
            e.Handled = char.IsNumber(e.KeyChar) || e.KeyChar == 8 ? false : true;

            txtCodigo.MaxLength = 15;
            if ((txtCodigo.Text.Length > 0 && e.KeyChar == 13))
            {
                txtCodigo.Text = string.Format("{0:000000000000000}", Convert.ToDouble(txtCodigo.Text));
                codigoExiste = productos.validaCodProducto(txtCodigo.Text);
                if (codigoExiste == txtCodigo.Text)
                {
                    MessageBox.Show("El Codigo Ya Existe!!", "Invalido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCodigo.Clear();
                    txtCodigo.Focus();
                }
                else
                {
                    txtDescripcion.Focus();
                }
            }
            
        }

        private void txtDescripcion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((txtDescripcion.Text.Length > 0 && e.KeyChar == 13))
            {
                txtFabricate.Focus();
                txtFabricate.Text = txtCodigo.Text;
                txtFabricate.SelectionStart = 0;
                txtFabricate.SelectionLength = txtFabricate.Text.Length;

            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            lbxCategoria categoria = new lbxCategoria();
            categoria.categoriaPasado+=new lbxCategoria.categoriaPD(ejecutarCategoriaSelec);
            categoria.categoriaPDescrip += new lbxCategoria.categoriaPDD(ejecutarCategoriaSelecDescrip);
            categoria.Eleccion = "Seleccion Producto";
            categoria.frmProduct = "Producto";
            categoria.ShowDialog();
            txtCategoria.Focus();
        }
        public void ejecutarCategoriaSelec(string datoCategoria) 
        {
            txtCategoria.Text = datoCategoria;
        }
        public void ejecutarCategoriaSelecDescrip(string dataCategoriaDescrip) 
        {
            txtDescripCategoria.Text = dataCategoriaDescrip;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (txtCategoria.Text != "")
            {
                lbxSubCategoria subCategoria = new lbxSubCategoria();
                subCategoria.eleccion = "Selecionar";
                subCategoria.codeCategoria = txtCategoria.Text;
                subCategoria.subCagoriaPasado += new lbxSubCategoria.subCategoriaPD(ejecutarSubCategoriaSelect);
                subCategoria.subCategoriaDescrip += new lbxSubCategoria.subCategoriaD(ejecutarSubCategoriaSelectD);
                subCategoria.frmProduct = "Producto";
                subCategoria.ShowDialog();
                txtSubCategoria.Focus();
            }
            else
            {
                MessageBox.Show("Debe Seleccionar Una Sub-Categoria", "Invalido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                button1.Focus();
            }
        }
        public void ejecutarSubCategoriaSelect(string datoSubcategoria) 
        {
            txtSubCategoria.Text = datoSubcategoria;
        }
        public void ejecutarSubCategoriaSelectD(string datoSubCategoria) 
        {
            txtDescripSubCategoria.Text = datoSubCategoria;
        }

        private void txtFabricate_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsNumber(e.KeyChar) || e.KeyChar == 8 ? false : true;
            if ((cmdDescEdit.Text.Length > 0 && e.KeyChar == 13))
            {
                cmdDescEdit.Focus();
            }
        }

        private void cmdProced_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if ((cmdEstado.Text.Length > 0 && e.KeyChar == 13))
            {
                cmdEstado.Focus();
            }
            
        }

        private void cmdDescEdit_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if ((cmdProced.Text.Length > 0 && e.KeyChar == 13))
            {
                cmdProced.Focus();
            }
        }

        private void cmdEstado_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (eleccion == "Modificar")
            {
                productos.exitProducto(txtCodigo.Text);
                if (productos.ExiteProdc != "0,00")
                {
                    MessageBox.Show("No puede Inactivar Producto, Comunicarse con Departamento Sistemas", "Invalido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else 
                {
                    if ((cmdIva.Text.Length > 0 && e.KeyChar == 13))
                    {
                        cmdIva.Focus();
                    }
                }
            }
            else
            {
               if ((cmdIva.Text.Length > 0 && e.KeyChar == 13))
               {
                cmdIva.Focus();
                }
            }
        }
        private void cmdIva_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if ((cmdIva.Text.Length > 0 && e.KeyChar == 13))
            {
                txtContenido.Focus();
                //if (cmdIva.Text != "GN")
                //{
                //    if (MessageBox.Show("¿Esta Seguro Que Desea Cambiar La Tasa Inpuesto?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                //    {
                //        cmdIva.Focus();
                //    }
                //}
            }
        }



        private void txtArancel_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            e.Handled = e.KeyChar == 8 ? false : true;
            if ((txtArancel.Text.Length > 0 && e.KeyChar == 13))
            {
                button1.Focus();
            }
        }

        public void txtCategoria_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((txtCategoria.Text.Length > 0 && e.KeyChar == 13))
            {
                button2.Focus();
            }
        }

        private void txtSubCategoria_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((txtSubCategoria.Text.Length > 0 && e.KeyChar == 13))
            {
                txtObser1.Focus();
            }
        }

        private void txtObser1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == 13))
            {
                txtObser2.Focus();
            }
        }

        private void txtObser2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == 13))
            {
                txtObser3.Focus();
            }
        }

        private void txtObser3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == 13))
            {
                if (eleccion == "Agregar")
                {
                    BtnRegistra.Focus();
                }
                if (eleccion == "Modificar")
                {
                    btnActulizar.Focus();
                }

            }
        }

        private void txtArancelD_KeyPress(object sender, KeyPressEventArgs e)
        {
         
        }

        private void txtContenido_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsNumber(e.KeyChar) || e.KeyChar == 8 ? false : true;
            if ((txtContenido.Text.Length > 0 && e.KeyChar == 13))
            {
                button3.Focus();
            }
        }

        private void txtComCobra_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsNumber(e.KeyChar) || e.KeyChar == 8 ? false : true;
            if ((txtComCobra.Text.Length > 0 && e.KeyChar == 13))
            {
                txtComVenta.Focus();
            }
        }

        private void txtComVenta_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsNumber(e.KeyChar) || e.KeyChar == 8 ? false : true;
            if ((txtComVenta.Text.Length > 0 && e.KeyChar == 13))
            {
                txtExMax.Focus();
            }

        }

        private void txtExMax_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsNumber(e.KeyChar) || e.KeyChar == 8 ? false : true;
            if ((txtExMax.Text.Length > 0 && e.KeyChar == 13))
            {
                txtExMin.Focus();
            }

        }

        private void txtExMin_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsNumber(e.KeyChar) || e.KeyChar == 8 ? false : true;
            if ((txtExMin.Text.Length > 0 && e.KeyChar == 13))
            {
                txtGPD.Focus();
            }
        }

        private void txtGPD_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsNumber(e.KeyChar) || e.KeyChar == 8 ? false : true;
            if ((txtGPD.Text.Length > 0 && e.KeyChar == 13))
            {
                txtAlto.Focus();
            }
        }

        private void txtAlto_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsNumber(e.KeyChar) || e.KeyChar == 8 ? false : true;
            if ((txtAlto.Text.Length > 0 && e.KeyChar == 13))
            {
                txtAncho.Focus();
            }
        }

        private void txtAncho_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsNumber(e.KeyChar) || e.KeyChar == 8 ? false : true;
            if ((txtAncho.Text.Length > 0 && e.KeyChar == 13))
            {
                txtVolume.Focus();
            }
        }

        private void txtVolume_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsNumber(e.KeyChar) || e.KeyChar == 8 ? false : true;
            if ((txtVolume.Text.Length > 0 && e.KeyChar == 13))
            {
                txtCapacidad.Focus();
            }
        }

        private void BtnRegistra_Click(object sender, EventArgs e)
        {
            string accion ="Agre";
            decimal comVenta = Convert.ToDecimal(txtComVenta.Text);
            decimal comCombra = Convert.ToDecimal(txtComCobra.Text);
            decimal capacidad = Convert.ToDecimal(txtCapacidad.Text.Replace(",","."));
            txtPDD.Text = txtPDD.Text.Replace(",", ".");
            if (validarCampos()==false)
            {
                if (cmdIva.Text != "GN")
                {
                    if (MessageBox.Show("¿Esta Seguro Que Desea Cambiar La Tasa Inpuesto?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        cmdIva.Focus();
                    }
                }
                
                productos.agregarProducto(txtCodigo.Text, txtDescripcion.Text, txtCategoria.Text, txtSubCategoria.Text, cmdDescEdit.Text,
                cmdEstado.Text, cmdIva.Text,txtContenido.Text, txtObser1.Text, txtObser2.Text, txtObser3.Text, cmdProced.Text, capacidad,
                comVenta, txtExMax.Text, txtExMin.Text, txtGPD.Text, txtVolume.Text, txtAlto.Text, txtAncho.Text,
                comCombra, txtArancel.Text, txtFabricate.Text,txtPDD.Text);
                productos.trazUsuario(idPc, ipPc, modulo, accion, txtCodigo.Text);
                proveedor.txtCodProd.Text = txtCodigo.Text;
                proveedor.txtDesProd.Text = txtDescripcion.Text;
                proveedor.txtCodProv.Focus();
                proveedor.ShowDialog();
                this.Close();
            }
       }

        private bool validarCampos() 
        {
            bool ciclipe = false;
            if(txtCodigo.Text=="")
            {
                MessageBox.Show("El Codigo Es Un Campo Obligatorio!!","ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                ciclipe = true;
            }
            if(txtDescripcion.Text=="")
            {
                MessageBox.Show("La DESCRIPCION Es Un Campo Obligatorio!!","ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                ciclipe = true;
            }
            if (txtCategoria.Text == "" && txtDescripCategoria.Text == "")
            {
                MessageBox.Show("La CATEGORIA Es Un Campo Obligatorio!!","ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                ciclipe = true;
            }
            if (txtArancel.Text == "" && txtArancelD.Text == "")
            {
                MessageBox.Show("La ARANCEL Es Un Campo Obligatorio!!", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                ciclipe = true;
            }
            if (txtDescripSubCategoria.Text == "" && txtSubCategoria.Text == "")
            {
                MessageBox.Show("La SUB-CATEGORIA Es Un Campo Obligatorio!!", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                ciclipe = true;
            }
            return ciclipe;
        }

        private void frmMantProductos_Shown(object sender, EventArgs e)
        {
            if(eleccion=="Agregar")
            {
                btnActulizar.Visible = false;

            }
            if (eleccion == "Modificar") 
            {
                BtnRegistra.Visible = false;
                txtCodigo.ReadOnly = true;
                btnLimpiar.Visible = false;
            }
            if (eleccion == "Visor")
            {
                btnLimpiar.Visible = false;
                BtnRegistra.Visible = false;
                btnActulizar.Visible = false;
                button1.Visible = false;
                button2.Visible = false;
                button3.Visible = false;
                button4.Visible = false;
                txtCodigo.ReadOnly = true;
                txtDescripcion.ReadOnly = true;
                txtFabricate.ReadOnly = true;
                cmdDescEdit.Enabled = false;
                cmdEstado.Enabled = false;
                cmdProced.Enabled = false;
                cmdIva.Enabled = false;
                txtArancel.ReadOnly = true;
                txtArancelD.ReadOnly = true;
                txtCategoria.ReadOnly = true;
                txtDescripCategoria.ReadOnly = true;
                txtSubCategoria.ReadOnly = true;
                txtDescripSubCategoria.ReadOnly = true;
                txtObser1.ReadOnly = true;
                txtObser2.ReadOnly = true;
                txtObser3.ReadOnly = true;
                txtPDD.ReadOnly = true;
                txtComCobra.ReadOnly = true;
                txtComVenta.ReadOnly = true;
                txtExMax.ReadOnly = true;
                txtExMin.ReadOnly = true;
                txtGPD.ReadOnly = true;
                txtAlto.ReadOnly = true;
                txtAncho.ReadOnly = true;
                txtVolume.ReadOnly = true;
                txtCapacidad.ReadOnly = true;
                txtContenido.ReadOnly = true;

            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtCodigo.Clear();
            txtDescripcion.Clear();
            txtFabricate.Clear();
            txtContenido.Text = "0.00";
            txtArancel.Clear();
            txtCategoria.Clear();
            txtSubCategoria.Clear();
            txtArancelD.Clear();
            txtDescripCategoria.Clear();
            txtSubCategoria.Clear();
            txtDescripSubCategoria.Clear();
            txtPDD.Text = "0.00";
            txtComCobra.Text = "0.00";
            txtComVenta.Text = "0.00";
            txtExMax.Text = "0";
            txtExMin.Text = "0";
            txtGPD.Text = "0";
            txtAlto.Text = "0";
            txtAncho.Text = "0";
            txtVolume.Text = "0";
            txtCapacidad.Text = "0.00";
            txtObser1.Clear();
            txtObser2.Clear();
            txtObser3.Clear();
            txtContenido.Text = "0.00";
        }
        private void btnActulizar_Click(object sender, EventArgs e)
        {
            if (validarCampos()== false) 
            {
                string accion="Modi";
                productos.actualizarProducto(txtDescripcion.Text, txtCategoria.Text, txtSubCategoria.Text, cmdDescEdit.Text, txtPDD.Text.Replace(",", ".")
                , cmdEstado.Text, cmdIva.Text, txtContenido.Text.Replace(",", "."), txtObser1.Text, txtObser2.Text, txtObser3.Text, cmdProced.Text, txtCapacidad.Text.Replace(",", ".")
                ,txtComVenta.Text.Replace(",", "."), txtComCobra.Text.Replace(",", "."), txtExMax.Text, txtExMin.Text, txtVolume.Text, txtAlto.Text, txtAncho.Text
                ,txtArancel.Text, txtFabricate.Text, txtCodigo.Text,txtGPD.Text);
                productos.trazUsuario(idPc, ipPc, modulo, accion, txtCodigo.Text);
                MessageBox.Show("Se a Actualizado Correctamente!!", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }

        private void frmMantProductos_FormClosed(object sender, FormClosedEventArgs e)
        {
            lbxMantProductos mantProduct = new lbxMantProductos();
            mantProduct.frmProduct = "Producto";
            mantProduct.ipMaq = ipPc;
            mantProduct.IdMaq = idPc;
            mantProduct.Show();
            
        }

        private void txtCapacidad_TextChanged(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsNumber(e.KeyChar) || e.KeyChar == 8 ? false : true;
            if ((txtVolume.Text.Length > 0 && e.KeyChar == 13))
            {
                if (eleccion == "Agregar")
                {
                    BtnRegistra.Focus();
                }
                if (eleccion == "Modificar") 
                {
                    btnActulizar.Focus();
                }
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            lbxProveedores proveedor = new lbxProveedores();
            proveedor.ShowDialog();
        }

    }
}