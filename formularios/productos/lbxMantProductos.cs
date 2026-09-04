using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using lebrun.clases.producto;
using lebrun.clasesData;
using lebrun.formularios.gerencial;

namespace lebrun.formularios.productos
{
    public partial class lbxMantProductos : Form
    {
        Principal SistemaUsuario;
        private Productos mantProduct;
        private DataView dv;
        private DataTable dtMantProduct;
        private FuncionesTexbox funcionesTextBox;
        private lbxConsultaGerencial gerenAdmin;
        private lbxConsultaGerente gerenTiend;
        private string seleccion;
        private bool palbContetiva;
        public string frmProduct;
        public string ipMaq;
        public delegate void gerenteAdmin(string datoProduct);
        public event gerenteAdmin productoPasado;
        public string proCual;


        public string IdMaq;
        string modulo = "Mantenimiento De Producto";
        string accion;

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

        public lbxMantProductos()
        {
            InitializeComponent();
            ttmensajes.SetToolTip(btnBuscar, "Buscar");
            ttmensajes.SetToolTip(btnAgregar, "Agregar");
            ttmensajes.SetToolTip(btnVisor, "Visor");
            ttmensajes.SetToolTip(btnAgregar, "Agregar");
            ttmensajes.SetToolTip(button2, "Eliminar Precios");
            ttmensajes.SetToolTip(button3, "Eliminar Promociones");
            ttmensajes.SetToolTip(button1, "Unidad De Medida");
            ttmensajes.SetToolTip(btnModificar, "Modificar");
            ttmensajes.SetToolTip(btnEliminar, "Eliminar");
            ttmensajes.SetToolTip(btnImprimir, "Imprimir");
            ttmensajes.SetToolTip(btnLimpiar, "Actulizar");
            ttmensajes.SetToolTip(btnSalir, "Salir");
            ttmensajes.SetToolTip(btnUnidMed, "Opciones Adicionales");
        }


        private void lbxMantProductos_Load(object sender, EventArgs e)
        {
            gerenAdmin=new lbxConsultaGerencial();
            gerenTiend=new lbxConsultaGerente();
            SistemaUsuario = new Principal();
            funcionesTextBox = new FuncionesTexbox();
            mantProduct = new Productos();
            dtMantProduct = mantProduct.lbxMantProducto();
            dv = new DataView(dtMantProduct);
            this.dataGridView1.DataSource = dtMantProduct;
            dataGridView1.AutoResizeColumns();
            if (frmProduct !="Producto")
            {
                SistemaUsuario = (Principal)this.MdiParent;
                IdMaq = Convert.ToString(SistemaUsuario.usuarioActual.Id);
                ipMaq = Convert.ToString(SistemaUsuario.usuarioActual.IpPc);
            }
        }

        private void txtBusqueda_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (rbtCodigo.Checked) { funcionesTextBox.OnlyNumbers(sender, e); }
            if (e.KeyChar == (char)(Keys.Enter))
            {
                btnBuscar.PerformClick();
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {

        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            dtMantProduct = null;
            mantProduct = new Productos();
            dtMantProduct = mantProduct.lbxMantProducto();
            dv.Dispose();
            dv = new DataView(dtMantProduct);
            this.dataGridView1.DataSource = dtMantProduct;
            dataGridView1.AutoResizeColumns();
        }

        private void btnBuscar_Click_1(object sender, EventArgs e)
        {
            if (chbPalContentiva.Checked == true)
            { palbContetiva = true; }
            else { palbContetiva = false; }
            if ((txtBusqueda.Text == "") || (txtBusqueda.Text == null)) 
            {
                txtBusqueda.SelectionStart = 0;
                txtBusqueda.SelectionLength = txtBusqueda.Text.Length;
                MessageBox.Show("Debe Poner un Dato Para buscar!!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtBusqueda.Focus();
            }
            else
            {
                if (rbtCodigo.Checked)
                {
                    seleccion = rbtCodigo.AccessibleName;
                }
                if (rbtDescripcion.Checked)
                {
                    seleccion = rbtDescripcion.AccessibleName;
                }
                if (rbtCategoria.Checked)
                {
                    seleccion = rbtCategoria.AccessibleName;
                }
                if (rbtSubCategoria.Checked)
                {
                    seleccion = rbtSubCategoria.AccessibleName;
                }
                if (rbtSubCategoria.Checked)
                {
                    seleccion = rbtSubCategoria.AccessibleName;
                }
                if (rbtAlicuota.Checked)
                {
                    seleccion = rbtAlicuota.AccessibleName;
                }
                if (rbtFabricante.Checked)
                {
                    seleccion = rbtFabricante.AccessibleName;
                }
                if (rbtProcedincia.Checked)
                {
                    seleccion = rbtProcedincia.AccessibleName;
                }
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = mantProduct.buscarProductoFrm(seleccion, txtBusqueda.Text, palbContetiva);
                dataGridView1.AutoResizeColumns();
                txtBusqueda.SelectionStart = 0;
                txtBusqueda.SelectionLength = txtBusqueda.Text.Length;
            }
        }

        private void lbxMantProductos_Shown(object sender, EventArgs e)
        {
            txtBusqueda.Focus();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmMantProductos insertProduc = new frmMantProductos();
            insertProduc.Show();
            insertProduc.txtCodigo.Focus();
            insertProduc.eleccion = "Agregar";
            insertProduc.idPc = IDMAQ;
            insertProduc.ipPc = IPMAQ;
            insertProduc.modulo = modulo;
            this.Close();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            string codigoProductos;
            frmMantProductos modifProduc = new frmMantProductos();
            codigoProductos = dataGridView1.CurrentRow.Cells["Codigo"].Value.ToString();
            mantProduct.elimiProducto(codigoProductos);
            if (mantProduct.EstadoProducto == "Activo")
            {
                mantProduct.modifProducto(codigoProductos);
                modifProduc.idPc = IDMAQ;
                modifProduc.ipPc = IPMAQ;
                modifProduc.modulo = modulo;
                modifProduc.txtCodigo.Text = dataGridView1.CurrentRow.Cells["Codigo"].Value.ToString();
                modifProduc.txtDescripcion.Text = dataGridView1.CurrentRow.Cells["Descripcion"].Value.ToString();
                modifProduc.txtFabricate.Text = mantProduct.FabriProduc;
                modifProduc.cmdDescEdit.Text = mantProduct.DesEdit;
                modifProduc.cmdEstado.Text = mantProduct.Estado;
                modifProduc.cmdProced.Text = mantProduct.Procede;
                modifProduc.txtArancel.Text = mantProduct.AracelProduc;
                modifProduc.txtArancelD.Text = mantProduct.desAracProduc;
                modifProduc.txtPDD.Text = mantProduct.PDDProd;
                modifProduc.txtComCobra.Text = mantProduct.ComCobProd;
                modifProduc.txtComVenta.Text = mantProduct.comVenProduc;
                modifProduc.txtExMax.Text = mantProduct.ExMaxProd;
                modifProduc.txtExMin.Text = mantProduct.ExMinProd;
                modifProduc.txtGPD.Text = mantProduct.GPMProd;
                modifProduc.txtAlto.Text = mantProduct.AltaProd;
                modifProduc.txtAncho.Text = mantProduct.AnchoProd;
                modifProduc.txtVolume.Text = mantProduct.VoluProd;
                modifProduc.txtCapacidad.Text = mantProduct.CapaProd;
                modifProduc.cmdIva.Text = dataGridView1.CurrentRow.Cells["AlicuotaIva"].Value.ToString();
                modifProduc.txtDescripCategoria.Text = dataGridView1.CurrentRow.Cells["Categoria"].Value.ToString();
                modifProduc.txtCategoria.Text = mantProduct.CategProduc;
                modifProduc.txtSubCategoria.Text = mantProduct.SubCatProduc;
                modifProduc.txtDescripSubCategoria.Text = dataGridView1.CurrentRow.Cells["SubCategoria"].Value.ToString();
                modifProduc.txtObser1.Text = mantProduct.Obser1;
                modifProduc.txtObser2.Text = mantProduct.Obser2;
                modifProduc.txtObser3.Text = mantProduct.Obser3;
                modifProduc.txtContenido.Text = mantProduct.Contenido;
                modifProduc.Show();
                modifProduc.txtDescripcion.Focus();
                modifProduc.eleccion = "Modificar";
                this.Close();
            }
            else 
            {
                MessageBox.Show("No Se Puede Modificar Productos Inactivos!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }  

        private void btnVisor_Click(object sender, EventArgs e)
        {
            string codigoProductos;
            frmMantProductos visorProduc = new frmMantProductos();
            codigoProductos = dataGridView1.CurrentRow.Cells["Codigo"].Value.ToString();
            mantProduct.modifProducto(codigoProductos);
            visorProduc.txtCodigo.Text = dataGridView1.CurrentRow.Cells["Codigo"].Value.ToString();
            visorProduc.txtDescripcion.Text = dataGridView1.CurrentRow.Cells["Descripcion"].Value.ToString();
            visorProduc.txtFabricate.Text = mantProduct.FabriProduc;
            visorProduc.cmdDescEdit.Text = mantProduct.DesEdit;
            visorProduc.cmdEstado.Text = mantProduct.Estado;
            visorProduc.cmdProced.Text = mantProduct.Procede;
            visorProduc.txtArancel.Text = mantProduct.AracelProduc;
            visorProduc.txtArancelD.Text = mantProduct.desAracProduc;
            visorProduc.txtPDD.Text = mantProduct.PDDProd;
            visorProduc.txtComCobra.Text = mantProduct.ComCobProd;
            visorProduc.txtComVenta.Text = mantProduct.comVenProduc;
            visorProduc.txtExMax.Text = mantProduct.ExMaxProd;
            visorProduc.txtExMin.Text = mantProduct.ExMinProd;
            visorProduc.txtGPD.Text = mantProduct.GPMProd;
            visorProduc.txtAlto.Text = mantProduct.AltaProd;
            visorProduc.txtAncho.Text = mantProduct.AnchoProd;
            visorProduc.txtVolume.Text = mantProduct.VoluProd;
            visorProduc.txtCapacidad.Text = mantProduct.CapaProd;
            visorProduc.cmdIva.Text = dataGridView1.CurrentRow.Cells["AlicuotaIva"].Value.ToString();
            visorProduc.txtDescripCategoria.Text = dataGridView1.CurrentRow.Cells["Categoria"].Value.ToString();
            visorProduc.txtCategoria.Text = mantProduct.CategProduc;
            visorProduc.txtSubCategoria.Text = mantProduct.SubCatProduc;
            visorProduc.txtDescripSubCategoria.Text = dataGridView1.CurrentRow.Cells["SubCategoria"].Value.ToString();
            visorProduc.txtObser1.Text = mantProduct.Obser1;
            visorProduc.txtObser2.Text = mantProduct.Obser2;
            visorProduc.txtObser3.Text = mantProduct.Obser3;
            visorProduc.txtContenido.Text = mantProduct.Contenido;
            visorProduc.Show();
            visorProduc.eleccion = "Visor";
            Close();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Desea Eliminar Este Producto Del Sistema", "Eliminar Informacion Data", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                 accion = "Elim";
                string codigProd;
                codigProd = dataGridView1.CurrentRow.Cells["Codigo"].Value.ToString();
                mantProduct.elimiProducto(codigProd);
                if (mantProduct.EstadoProducto == "Activo")
                {
                    mantProduct.actulizarEstado(codigProd);
                    mantProduct.trazUsuario(IDMAQ, IPMAQ, modulo, accion, codigProd);
                    MessageBox.Show("Se Eliminado El Producto Correctamente", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnLimpiar.PerformClick();
                }
                else 
                {
                    MessageBox.Show("No se consiguieron Resultados!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void btnUnidMed_Click(object sender, EventArgs e)
        {
            lbxOpcionesAdicionales opciones = new lbxOpcionesAdicionales();
            opciones.txtCodProd.Text = dataGridView1.CurrentRow.Cells["Codigo"].Value.ToString();
            opciones.txtDescProduc.Text = dataGridView1.CurrentRow.Cells["Descripcion"].Value.ToString();
            opciones.ipMaq= IPMAQ;
            opciones.IdMaq = IDMAQ;
            opciones.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmUnidaXProducto unidad = new frmUnidaXProducto();
            unidad.idPc = IDMAQ;
            unidad.ipPc = IPMAQ;
            unidad.txtCodProd.Text = dataGridView1.CurrentRow.Cells["Codigo"].Value.ToString();
            unidad.txtDesProd.Text = dataGridView1.CurrentRow.Cells["Descripcion"].Value.ToString();
            unidad.codigoProd = dataGridView1.CurrentRow.Cells["Codigo"].Value.ToString();
            unidad.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            lbxElimiPrecios eliminacion = new lbxElimiPrecios();
            eliminacion.txtCodigo.Text = dataGridView1.CurrentRow.Cells["Codigo"].Value.ToString();
            eliminacion.txtProducto.Text = dataGridView1.CurrentRow.Cells["Descripcion"].Value.ToString();
            eliminacion.idPc = IDMAQ;
            eliminacion.ipPc = IPMAQ;
            eliminacion.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            lbxElimiPromo promo = new lbxElimiPromo();
            promo.txtCodigo.Text = dataGridView1.CurrentRow.Cells["Codigo"].Value.ToString();
            promo.txtProducto.Text=dataGridView1.CurrentRow.Cells["Descripcion"].Value.ToString();
            promo.idPc = IDMAQ;
            promo.ipPc = IPMAQ;
            promo.Show();
        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
           if(proCual=="Gerente")
           {
             productoPasado(dataGridView1.CurrentRow.Cells["Codigo"].Value.ToString());
             this.Close();
           }
           else if (proCual=="Admin")
           {
           productoPasado(dataGridView1.CurrentRow.Cells["Codigo"].Value.ToString());
           this.Close();
          }
        }

    }
}
