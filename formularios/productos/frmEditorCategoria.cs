using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using lebrun.clases.producto;

namespace lebrun.formularios.productos
{
    public partial class frmEditorCategoria : Form
    {

        //declacion de variable
        public string Eleccion;
        public int ultimo;
        Productos Editor;
        public string idPc;
        public string ipPc;
        public string modulo;
        public string accion;

        public frmEditorCategoria()
        {
            InitializeComponent();
            ttMesajes.SetToolTip(btnActulizar, "Actulizar");
            ttMesajes.SetToolTip(BtnRegistra, "Registrar");
            ttMesajes.SetToolTip(btnCancelar, "Cancelar");
        }
        private void frmEditorCategoria_Load(object sender, EventArgs e)
        {
             Editor = new Productos();
        }
        private void btnActulizar_Click(object sender, EventArgs e)
        {

            accion = "Modi";
            Editor.codigoCategoria = txtCodigoCategoria.Text;
            Editor.descripCategoria = txtDescricionCategoria.Text;

            if (txtComVenta.Text == "")
            {
                Editor.ventaCategoria = Convert.ToString(0.00);
            }
            else 
            {
                Editor.ventaCategoria = txtComVenta.Text;
            }
            if (txtComCompra.Text == "")
            {
                Editor.compraCategoria = Convert.ToString(0.00);
            }
            else
            {
                Editor.compraCategoria = txtComCompra.Text;
            }
            
            if (MessageBox.Show("Desea Actulizar Esta Categoria", "Agregar Nueva Informacion Data", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (Editor.exesteCategoria() == false)
                {
                    MessageBox.Show("La Categoria  No Existe!!", "Invalido", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (Editor.actulziarCategoria())
                {
                    Editor.trazUsuario(idPc, ipPc, modulo, accion, txtCodigoCategoria.Text);
                    MessageBox.Show("La Categoria  Actulizado Exitosamente!!", "Exito!! ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
        }
        
        private void frmEditorCategoria_Shown(object sender, EventArgs e)
        {     
            if (Eleccion == "Agregar")
            {
                BtnRegistra.Visible = true;
                btnActulizar.Visible=false;
                txtCodigoCategoria.Focus();
              
            }
            else if (Eleccion == "Modificar")
            {
                txtCodigoCategoria.Enabled= false;
                BtnRegistra.Visible = false;
                txtDescricionCategoria.Focus();
            }
            else if (Eleccion == "Visor") 
            {
                btnActulizar.Visible = false;
                BtnRegistra.Visible = false;
            }
        }
        // registra
        private void BtnRegistra_Click(object sender, EventArgs e)
        {
            accion = "Agre";
            Editor.codigoCategoria = txtCodigoCategoria.Text;
            Editor.descripCategoria = txtDescricionCategoria.Text;
            if (txtComVenta.Text == "")
            {
                Editor.ventaCategoria = Convert.ToString(0.00);
            }
            else
            {
                Editor.ventaCategoria = txtComVenta.Text;
            }
            if (txtComCompra.Text == "")
            {
                Editor.compraCategoria = Convert.ToString(0.00);
            }
            else
            {
                Editor.compraCategoria = txtComCompra.Text;
            }

            if (MessageBox.Show("Desea Ingresar Un Nueva Categoria Al Sistema", "Agregar Nueva Informacion Data", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (validacionCampoCategoria())
                {
                    if (Editor.exesteCategoria())
                    {
                        MessageBox.Show("La Categoria Ya Existe!!", "Invalido", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        limpiarCaja();
                    }
                    else if (Editor.ingresarCategoria())
                    {
                        Editor.trazUsuario(idPc, ipPc, modulo, accion, txtCodigoCategoria.Text);
                        MessageBox.Show("La Categoria Se Guardado Exitosamente!!", "Exito!! ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                }
            }
        }
        //validaciond e campo
        public bool validacionCampoCategoria()
        {
            bool Ciclope = true;
            if(txtCodigoCategoria.Text=="")
            {
                MessageBox.Show("El CODIGO Es Un Campo Obligatorio!!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCodigoCategoria.Focus();
                Ciclope = false;
                return Ciclope; 
            }
            //if (txtDescricionCategoria.Text == "")
            //{
            //    MessageBox.Show("La DESCRIPCION es un campo obligatorio!!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    txtDescricionCategoria.Focus();
            //    Ciclope = false;
            //    return Ciclope;
            //}
            return Ciclope;
        }

        private void frmEditorCategoria_FormClosed(object sender, FormClosedEventArgs e)
        {
            lbxCategoria regresos = new lbxCategoria();
            regresos.frmProduct = "Producto";
            regresos.IdMaq = idPc;
            regresos.ipMaq = ipPc;
            regresos.modulo = modulo;
            regresos.Show();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void limpiarCaja()
        {
            txtCodigoCategoria.Clear();
            txtDescricionCategoria.Clear();
            txtComCompra.Clear();
            txtComVenta.Clear();
        }

        private void txtCodigoCategoria_KeyPress(object sender, KeyPressEventArgs e)
        {
                e.Handled = char.IsNumber(e.KeyChar) || e.KeyChar == 8 ? false : true;
                txtCodigoCategoria.MaxLength = 6;
                
           if (e.KeyChar == (char)(Keys.Enter))
                {
                   if (validacionCampoCategoria())
                 {
                     txtCodigoCategoria.Text = string.Format("{0:000000}", Convert.ToDouble(txtCodigoCategoria.Text));
                       txtDescricionCategoria.Focus();
                  }
                }
        }

        private void txtDescricionCategoria_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                txtComVenta.Focus();
            }
        }
        private void txtComVenta_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsNumber(e.KeyChar) || e.KeyChar == 46 || e.KeyChar == 44 || e.KeyChar == 8 ? false : true;
            if (e.KeyChar == (char)(Keys.Enter))
            {
                txtComCompra.Focus();
            }
        }

        private void txtComCompra_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsNumber(e.KeyChar) || e.KeyChar == 46 || e.KeyChar == 44 || e.KeyChar == 8 ? false : true;
            if (Eleccion == "Agregar")
            {
                if (e.KeyChar == (char)(Keys.Enter))
                {
                    BtnRegistra.Focus();
                }
            }
            else if (Eleccion == "Modificar")
            {
                if (e.KeyChar == (char)(Keys.Enter))
                {
                    btnActulizar.Focus();
                }
            }
        }
    }
}
