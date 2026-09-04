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

namespace lebrun.formularios.productos
{
    public partial class lbxSubCategoria : Form
     {
        //Llamada de clases
        Principal SistemaUsuario;
        private Productos SubCategoria;
        private DataView dv;
        private DataTable dtSubCategoria;
        public string descripCodigoCategori;
        public string eleccion;
        public string codeCategoria;
        public delegate void subCategoriaPD(string datoSubCategoria);
        public delegate void subCategoriaD(string datoDescripSubC);
        public event subCategoriaPD subCagoriaPasado;
        public event subCategoriaD subCategoriaDescrip;
        public string frmProduct;
        public string ipMaq;
        public string IdMaq;
        public string modulo = "SubCategoria";
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
    
        public lbxSubCategoria()
        {
            InitializeComponent();
            ttMensajes.SetToolTip(btnAgregar,"Agregar");
            ttMensajes.SetToolTip(btnBuscar, "Buscar");
            ttMensajes.SetToolTip(btnModificar, "Modificar");
            ttMensajes.SetToolTip(btnEliminar, "Eliminar");
            ttMensajes.SetToolTip(btnLimpiar, "Actualizar");
            ttMensajes.SetToolTip(btnSalir, "Salir");
            ttMensajes.SetToolTip(btnImprimir, "Imprimir");
            ttMensajes.SetToolTip(button1, "Seleccionar");
            ttMensajes.SetToolTip(btnVisor, "Visor");
        }

        private void lbxSubCategoria_Load(object sender, EventArgs e)
        {
            SubCategoria = new Productos();
            dtSubCategoria = SubCategoria.lbxSubCategoria();
            dv = new DataView(dtSubCategoria);
            this.dataGridView1.DataSource = dtSubCategoria;
            dataGridView1.Columns[0].HeaderText = "Codigo Categoria";
            dataGridView1.Columns[1].HeaderText = "Codigo Sub-Categoria";
            dataGridView1.Columns[2].HeaderText = "Descripcion";
            dataGridView1.Columns[3].HeaderText = "Comision Venta";
            dataGridView1.Columns[4].HeaderText = "Comision Compra";
            dataGridView1.Columns[5].HeaderText = "Statud";
            if (frmProduct != "Producto")
            {
                SistemaUsuario = (Principal)this.MdiParent;
                IdMaq = Convert.ToString(SistemaUsuario.usuarioActual.Id);
                ipMaq = Convert.ToString(SistemaUsuario.usuarioActual.IpPc);
            }
        }
        //filtro
        private void filtra(string filtro)
        {
            DataTable Temp;

            if (chbPalContentiva.Checked == true)
            {
                if (rdbCodigo.Checked == true)
                {
                    if (eleccion == "Selecionar")
                    {
                        Temp = SubCategoria.codigoSubCategoriaBuscados(codeCategoria);
                        if (Temp.Rows.Count == 0)
                        {
                            MessageBox.Show("No se consiguieron Resultados!!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        else
                        {
                            dataGridView1.DataSource = null;
                            dataGridView1.DataSource = Temp;
                            dataGridView1.Columns[0].HeaderText = "Codigo Categoria";
                            dataGridView1.Columns[1].HeaderText = "Codigo Sub-Categoria";
                            dataGridView1.Columns[2].HeaderText = "Descripcion";
                            dataGridView1.Columns[3].HeaderText = "Comision Venta";
                            dataGridView1.Columns[4].HeaderText = "Comision Compra";
                            dataGridView1.Columns[5].HeaderText = "Statud";
                            dataGridView1.Update();
                        }
                    }
                    else
                    {
                        Temp = SubCategoria.codigoSubCategoriaBuscados(txtBuscador.Text);
                        if (Temp.Rows.Count == 0)
                        {
                            MessageBox.Show("No se consiguieron Resultados!!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        else
                        {
                            dataGridView1.DataSource = null;
                            dataGridView1.DataSource = Temp;
                            dataGridView1.Columns[0].HeaderText = "Codigo Categoria";
                            dataGridView1.Columns[1].HeaderText = "Codigo Sub-Categoria";
                            dataGridView1.Columns[2].HeaderText = "Descripcion";
                            dataGridView1.Columns[3].HeaderText = "Comision Venta";
                            dataGridView1.Columns[4].HeaderText = "Comision Compra";
                            dataGridView1.Columns[5].HeaderText = "Statud";
                            dataGridView1.Update();
                        }
                    }
                }
                else if (rdbDescripcion.Checked == true)
                {
                    
                    Temp = SubCategoria.descripSubCategoriaBuscados(txtBuscador.Text);
                    if (Temp.Rows.Count == 0)
                    {
                        MessageBox.Show("No se consiguieron Resultados!!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        dataGridView1.DataSource = null;
                        dataGridView1.DataSource = Temp;
                        dataGridView1.Columns[0].HeaderText = "Codigo Categoria";
                        dataGridView1.Columns[1].HeaderText = "Codigo Sub-Categoria"; ;
                        dataGridView1.Columns[2].HeaderText = "Descripcion";
                        dataGridView1.Columns[3].HeaderText = "Comision Venta";
                        dataGridView1.Columns[4].HeaderText = "Comision Compra";
                        dataGridView1.Columns[5].HeaderText = "Statud";
                        dataGridView1.Update();
                   }
                  
                }
                else if (rdbSubCodigo.Checked==true)
                {
                    Temp = SubCategoria.subCodigoSubCategoriaBuscados(txtBuscador.Text);
                    if (Temp.Rows.Count == 0)
                    {
                        MessageBox.Show("No se consiguieron Resultados!!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        dataGridView1.DataSource = null;
                        dataGridView1.DataSource = Temp;
                        dataGridView1.Columns[0].HeaderText = "Codigo Categoria";
                        dataGridView1.Columns[1].HeaderText = "Codigo Sub-Categoria";
                        dataGridView1.Columns[2].HeaderText = "Descripcion";
                        dataGridView1.Columns[3].HeaderText = "Comision Venta";
                        dataGridView1.Columns[4].HeaderText = "Comision Compra";
                        dataGridView1.Columns[5].HeaderText = "Statud";
                        dataGridView1.Update();
                   }
                }
            }
            else
            {
                if (rdbCodigo.Checked == true)
                {
                    Temp = SubCategoria.codigoSubCategoriaBuscado(txtBuscador.Text);
                    if (Temp.Rows.Count == 0)
                    {
                        MessageBox.Show("No se consiguieron Resultados!!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        dataGridView1.DataSource = null;
                        dataGridView1.DataSource = Temp;
                        dataGridView1.Columns[0].HeaderText = "Codigo Categoria";
                        dataGridView1.Columns[1].HeaderText = "Codigo Sub-Categoria";
                        dataGridView1.Columns[2].HeaderText = "Descripcion";
                        dataGridView1.Columns[3].HeaderText = "Comision Venta";
                        dataGridView1.Columns[4].HeaderText = "Comision Compra";
                        dataGridView1.Columns[5].HeaderText = "Statud";
                        dataGridView1.Update();
                    }
                }
                else if (rdbDescripcion.Checked == true)
                {

                    Temp = SubCategoria.descripSubCategoriaBuscado(txtBuscador.Text);
                    if (Temp.Rows.Count == 0)
                    {
                        MessageBox.Show("No se consiguieron Resultados!!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        dataGridView1.DataSource = null;
                        dataGridView1.DataSource = Temp;
                        dataGridView1.Columns[0].HeaderText = "Codigo Categoria";
                        dataGridView1.Columns[1].HeaderText = "Codigo Sub-Categoria";
                        dataGridView1.Columns[2].HeaderText = "Descripcion";
                        dataGridView1.Columns[3].HeaderText = "Comision Venta";
                        dataGridView1.Columns[4].HeaderText = "Comision Compra";
                        dataGridView1.Columns[5].HeaderText = "Statud";
                        dataGridView1.Update();
                    }

                }
                else if (rdbSubCodigo.Checked == true)
                {
                    Temp = SubCategoria.subCodigoSubCategoriaBuscado(txtBuscador.Text);
                    if (Temp.Rows.Count == 0)
                    {
                        MessageBox.Show("No se consiguieron Resultados!!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        dataGridView1.DataSource = null;
                        dataGridView1.DataSource = Temp;
                        dataGridView1.Columns[0].HeaderText = "Codigo Categoria";
                        dataGridView1.Columns[1].HeaderText = "Codigo Sub-Categoria";
                        dataGridView1.Columns[2].HeaderText = "Descripcion";
                        dataGridView1.Columns[3].HeaderText = "Comision Venta";
                        dataGridView1.Columns[4].HeaderText = "Comision Compra";
                        dataGridView1.Columns[5].HeaderText = "Statud";
                        dataGridView1.Update();
                    }
                }
            }
        
        }
        //buscar
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            
            if (rdbCodigo.Checked == true)
            {
               txtBuscador.Text = string.Format("{0:000000}",Convert.ToDouble(txtBuscador.Text));
               txtBuscador.SelectionStart = 0;
               txtBuscador.SelectionLength = txtBuscador.Text.Length;
                if (txtBuscador.Text != "")
                {
                    filtra(txtBuscador.Text);
                }
                else
                {
                    MessageBox.Show("Debe Poner un Dato Para buscar!!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtBuscador.Focus();
                }
            }
            else if (rdbDescripcion.Checked == true)
            {
                txtBuscador.SelectionStart = 0;
                txtBuscador.SelectionLength = txtBuscador.Text.Length;
                if (txtBuscador.Text != "")
                {
                    filtra(txtBuscador.Text);
                }
                else
                {
                    MessageBox.Show("Debe Poner un Dato Para buscar!!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtBuscador.Focus();
                }
            }
            else if(rdbSubCodigo.Checked==true)
            {
                txtBuscador.Text = string.Format("{0:0000}", Convert.ToDouble(txtBuscador.Text));
                txtBuscador.SelectionStart = 0;
                txtBuscador.SelectionLength = txtBuscador.Text.Length;
                if (txtBuscador.Text != "")
                {
                    filtra(txtBuscador.Text);
                }
                else
                {
                    MessageBox.Show("Debe Poner un Dato Para buscar!!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtBuscador.Focus();
                }       
            }
        }
        //Validaciones de campo
        private void txtBuscador_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                btnBuscar.PerformClick();
            }
            if(rdbCodigo.Checked==true)
            {
                e.Handled = char.IsNumber(e.KeyChar) || e.KeyChar == 8 ? false : true;
                txtBuscador.MaxLength = 6; 
            }
            if (rdbDescripcion.Checked == true)
            {
                e.Handled = char.IsLetter(e.KeyChar) || char.IsNumber(e.KeyChar) || e.KeyChar == 32 || e.KeyChar == 46 || e.KeyChar == 44 || e.KeyChar == 8 ? false : true;
            
            }
            if (rdbSubCodigo.Checked == true)
            {
                e.Handled = char.IsNumber(e.KeyChar) || e.KeyChar == 8 ? false : true;
                txtBuscador.MaxLength = 4; 
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            lbxCategoria AgregarlbxC = new lbxCategoria();
            AgregarlbxC.Eleccion = "Agregar";
            AgregarlbxC.frmProduct="Producto";
            AgregarlbxC.ipMaq = IPMAQ;
            AgregarlbxC.IdMaq = IDMAQ;
            AgregarlbxC.Show();
            Close();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            frmEditorSubCategoria Modificarlbx = new frmEditorSubCategoria();
            Modificarlbx.txtCodigoCategoria.Text = dataGridView1.CurrentRow.Cells["sgr_grupo"].Value.ToString();
            Modificarlbx.txtCodigoSubCategoria.Text = dataGridView1.CurrentRow.Cells["sgr_codigo"].Value.ToString();
            Modificarlbx.txtDescripSubCategoria.Text = dataGridView1.CurrentRow.Cells["sgr_descri"].Value.ToString();
            Modificarlbx.txtComVentasSubCategoria.Text = dataGridView1.CurrentRow.Cells["sgr_comvta"].Value.ToString();
            Modificarlbx.txtComCompraSubCategoria.Text = dataGridView1.CurrentRow.Cells["sgr_comcob"].Value.ToString();
            Modificarlbx.ipPc = IPMAQ;
            Modificarlbx.idPc = IDMAQ;
            Modificarlbx.Eleccion = "Modificar";
            Modificarlbx.Show();
            Close();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            string codigoSub=dataGridView1.CurrentRow.Cells["sgr_codigo"].Value.ToString();
            accion = "Elim";
            if (MessageBox.Show("Desea Eliminar Esta Sub-Categoria Del Sistema", "Agregar Nueva Informacion Data", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (dataGridView1.CurrentRow.Cells["sgr_statud"].Value.ToString().Equals("Activo"))
                {
                    SubCategoria.codigoSubCat = dataGridView1.CurrentRow.Cells["sgr_codigo"].Value.ToString();
                    SubCategoria.codigoCategoria = dataGridView1.CurrentRow.Cells["sgr_grupo"].Value.ToString();
                    SubCategoria.eliminarOactulizarSubCateg();
                    SubCategoria.trazUsuario(IDMAQ, IPMAQ, modulo, accion, codigoSub);
                    MessageBox.Show("La Sub-Categoria Se A Eliminado", "Imformacion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    btnLimpiar.PerformClick();
                }
                else
                {
                    MessageBox.Show("La Sub-Categoria Que a Elejido Se Encuetra Eliminado", "Imformacion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            SubCategoria = new Productos();
            dtSubCategoria = SubCategoria.lbxSubCategoria();
            dv = new DataView(dtSubCategoria);
            this.dataGridView1.DataSource = dtSubCategoria;
            dataGridView1.Columns[0].HeaderText = "Codigo Categoria";
            dataGridView1.Columns[1].HeaderText = "Codigo Sub-Categoria";
            dataGridView1.Columns[2].HeaderText = "Descripcion";
            dataGridView1.Columns[3].HeaderText = "Comision Venta";
            dataGridView1.Columns[4].HeaderText = "Comision Compra";
            dataGridView1.Columns[5].HeaderText = "Statud";  
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtBuscador_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnVisor_Click(object sender, EventArgs e)
        {
            frmEditorSubCategoria Visor = new frmEditorSubCategoria();
            Visor.txtCodigoCategoria.Text = dataGridView1.CurrentRow.Cells["sgr_grupo"].Value.ToString();
            Visor.txtCodigoSubCategoria.Text = dataGridView1.CurrentRow.Cells["sgr_codigo"].Value.ToString();
            Visor.txtDescripSubCategoria.Text = dataGridView1.CurrentRow.Cells["sgr_descri"].Value.ToString();
            Visor.txtComVentasSubCategoria.Text = dataGridView1.CurrentRow.Cells["sgr_comvta"].Value.ToString();
            Visor.txtComCompraSubCategoria.Text = dataGridView1.CurrentRow.Cells["sgr_comcob"].Value.ToString();
            Visor.Eleccion = "Visor";
            Visor.Show();
            this.Close();
        }

        private void lbxSubCategoria_Shown(object sender, EventArgs e)
        {
            button1.Visible = false;
            if (eleccion == "Selecionar")
            {
                btnAgregar.Visible = false;
                btnModificar.Visible = false;
                btnEliminar.Visible = false;
                btnImprimir.Visible = false;
                btnVisor.Visible = false;
                button1.Visible = true;
                filtra(codeCategoria);
            }
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            subCagoriaPasado(dataGridView1.CurrentRow.Cells["sgr_codigo"].Value.ToString());
            subCategoriaDescrip(dataGridView1.CurrentRow.Cells["sgr_descri"].Value.ToString());
            this.Close();
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                subCagoriaPasado(dataGridView1.CurrentRow.Cells["sgr_codigo"].Value.ToString());
                subCategoriaDescrip(dataGridView1.CurrentRow.Cells["sgr_descri"].Value.ToString());
                this.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            subCagoriaPasado(dataGridView1.CurrentRow.Cells["sgr_codigo"].Value.ToString());
            subCategoriaDescrip(dataGridView1.CurrentRow.Cells["sgr_descri"].Value.ToString());
            this.Close();
        }
    }
}
