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
    public partial class lbxCategoria : Form
    {
        //llamdo de clases
        Principal SistemaUsuario;
        private Productos Categoria1;
        private DataView dv;
        private DataTable dCategoria;
        public string ordenReslt;
        public string Eleccion;
        public delegate void categoriaPD(string datoCategoria);
        public delegate void categoriaPDD(string datoCategoriaD);
        public event categoriaPD categoriaPasado;
        public event categoriaPDD categoriaPDescrip;
        public string frmProduct;
        public string ipMaq;
        public string IdMaq;
        public string modulo = "Categoria";
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


        public lbxCategoria()
        {
            InitializeComponent();
            ttmensajes.SetToolTip(btnBuscar, "Buscar");
            ttmensajes.SetToolTip(btnAgregar, "Agregar");
            ttmensajes.SetToolTip(btnModificar, "Modificar");
            ttmensajes.SetToolTip(btnEliminar, "Eliminar");
            ttmensajes.SetToolTip(btnImprimir, "Imprimir");
            ttmensajes.SetToolTip(btnLimpiar, "Actualizar");
            ttmensajes.SetToolTip(btnSubCategoria, "Seleccionar");
            ttmensajes.SetToolTip(btnVisor, "Visor");
            ttmensajes.SetToolTip(btnSalir, "Salir");
        }

        private void lbxCategoria_Load(object sender, EventArgs e)
        {
            Categoria1 = new Productos();
            dCategoria = Categoria1.lbxCategoria();
            dv = new DataView(dCategoria);
            this.dataGridView1.DataSource = dCategoria;
            dataGridView1.Columns[0].HeaderText = "Codigo";
            dataGridView1.Columns[1].HeaderText = "Descripcion";
            dataGridView1.Columns[2].HeaderText = "Comision Ventas";
            dataGridView1.Columns[3].HeaderText = "Comision Combranza";
            if (frmProduct != "Producto")
            {
                SistemaUsuario = (Principal)this.MdiParent;
                IdMaq = Convert.ToString(SistemaUsuario.usuarioActual.Id);
                ipMaq = Convert.ToString(SistemaUsuario.usuarioActual.IpPc);
            }
        }



        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (rdbCodigo.Checked==true) 
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
            else if(rdbDescripcion.Checked==true)
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

        }
        //filtro de buscar
        private void filtra(string filtro)
        {
            DataTable Temp;
            if (chbPalContentiva.Checked == true)
            {
                if (rdbCodigo.Checked == true)
                {
                    txtBuscador.SelectionStart = 0;
                    txtBuscador.SelectionLength = txtBuscador.Text.Length;
                    Temp = Categoria1.codigoCategoriaBuscados(txtBuscador.Text);
                    if (Temp.Rows.Count == 0)
                    {
                        MessageBox.Show("No Se Consiguieron Resultados!!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        dataGridView1.DataSource = null;
                        dataGridView1.DataSource = Temp;
                        dataGridView1.Columns[0].HeaderText = "Codigo";
                        dataGridView1.Columns[1].HeaderText = "Descripcion";
                        dataGridView1.Columns[2].HeaderText = "Comision Ventas";
                        dataGridView1.Columns[3].HeaderText = "Comision Combranza";
                        dataGridView1.Update();
                    }
                }
                else if (rdbDescripcion.Checked == true)
                {
                    txtBuscador.SelectionStart = 0;
                    txtBuscador.SelectionLength = txtBuscador.Text.Length;
                    Temp = Categoria1.descripCategoriaBuscados(txtBuscador.Text);
                    if (Temp.Rows.Count == 0)
                    {
                        MessageBox.Show("No Se Consiguieron Resultados!!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        dataGridView1.DataSource = null;
                        dataGridView1.DataSource = Temp;
                        dataGridView1.Columns[0].HeaderText = "Codigo";
                        dataGridView1.Columns[1].HeaderText = "Descripcion";
                        dataGridView1.Columns[2].HeaderText = "Comision Ventas";
                        dataGridView1.Columns[3].HeaderText = "Comision Combranza";
                        dataGridView1.Update();
                    }
                }
            }
            else 
            {
                if (rdbCodigo.Checked == true)
                {
                    txtBuscador.Text = string.Format("{0:000000}", Convert.ToDouble(txtBuscador.Text));
                    txtBuscador.SelectionStart = 0;
                    txtBuscador.SelectionLength = txtBuscador.Text.Length;
                    Temp = Categoria1.codigoCategoriaBuscado(txtBuscador.Text);
                    if (Temp.Rows.Count == 0)
                    {
                        MessageBox.Show("No Se Consiguieron Resultados!!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        dataGridView1.DataSource = null;
                        dataGridView1.DataSource = Temp;
                        dataGridView1.Columns[0].HeaderText = "Codigo";
                        dataGridView1.Columns[1].HeaderText = "Descripcion";
                        dataGridView1.Columns[2].HeaderText = "Comision Ventas";
                        dataGridView1.Columns[3].HeaderText = "Comision Combranza";
                        dataGridView1.Update();
                    }
                }
                else if (rdbDescripcion.Checked == true)
                {
                    txtBuscador.SelectionStart = 0;
                    txtBuscador.SelectionLength = txtBuscador.Text.Length;
                    Temp = Categoria1.descripCategoriaBuscado(txtBuscador.Text);
                    if (Temp.Rows.Count == 0)
                    {
                        MessageBox.Show("No Se Consiguieron Resultados!!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        dataGridView1.DataSource = null;
                        dataGridView1.DataSource = Temp;
                        dataGridView1.Columns[0].HeaderText = "Codigo";
                        dataGridView1.Columns[1].HeaderText = "Descripcion";
                        dataGridView1.Columns[2].HeaderText = "Comision Ventas";
                        dataGridView1.Columns[3].HeaderText = "Comision Combranza";
                        dataGridView1.Update();
                    }
                }
            
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {

            frmEditorCategoria Agregarlbx = new frmEditorCategoria();
           //Categoria1.contadorCategira();
           // Agregarlbx.txtCodigoCategoria.Text = string.Format("{0:000000}",Categoria1.total);
            Agregarlbx.Eleccion = "Agregar";
            Agregarlbx.modulo = modulo;
            Agregarlbx.ipPc = IPMAQ;
            Agregarlbx.idPc = IDMAQ;
            Agregarlbx.Show();
            Close();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                frmEditorCategoria Modificarlbx = new frmEditorCategoria();
                Modificarlbx.txtCodigoCategoria.Text = dataGridView1.CurrentRow.Cells["gru_codigo"].Value.ToString();
                Modificarlbx.txtDescricionCategoria.Text = dataGridView1.CurrentRow.Cells["gru_descri"].Value.ToString();
                Modificarlbx.txtComCompra.Text = dataGridView1.CurrentRow.Cells["gru_comvta"].Value.ToString();
                Modificarlbx.txtComVenta.Text = dataGridView1.CurrentRow.Cells["gru_comcob"].Value.ToString();
                Modificarlbx.Eleccion = "Modificar";
                Modificarlbx.idPc = IDMAQ;
                Modificarlbx.ipPc = IPMAQ;
                Modificarlbx.modulo = modulo;
                Modificarlbx.Show();
                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            string codigocat=dataGridView1.CurrentRow.Cells["gru_codigo"].Value.ToString();
            accion = "Elim";
            if (MessageBox.Show("Desea Eliminar Esta Categoria Del Sistema", "Agregar Nueva Informacion Data", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Categoria1.codigoCategoria = dataGridView1.CurrentRow.Cells["gru_codigo"].Value.ToString();
                if (!Categoria1.categoriaEnUso())
                {
                    Categoria1.codigoCategoria = dataGridView1.CurrentRow.Cells["gru_codigo"].Value.ToString();
                    Categoria1.eliminarOactulizarCategoria();
                    Categoria1.trazUsuario(IDMAQ,IPMAQ,modulo,accion,codigocat);
                    MessageBox.Show("La Categoria Se A Eliminado", "Imformacion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    btnLimpiar.PerformClick();
                }
                else
                {
                    MessageBox.Show("No se puede eliminar la categoria porque tiene productos o sub-categorias asociados", "Imformacion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            dCategoria = null;
            dCategoria = Categoria1.lbxCategoria();
            dv.Dispose();
            dv = new DataView(dCategoria);
            this.dataGridView1.DataSource = dCategoria;
            dataGridView1.Columns[0].HeaderText = "Codigo";
            dataGridView1.Columns[1].HeaderText = "Descripcion";
            dataGridView1.Columns[2].HeaderText = "Comision Ventas";
            dataGridView1.Columns[3].HeaderText = "Comision Combranza";
            txtBuscador.Clear();
        }

        private void btnBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void btnSubCategoria_Click(object sender, EventArgs e)
        {
            if (Eleccion == "Seleccion Producto")
            {
                categoriaPasado(dataGridView1.CurrentRow.Cells["gru_codigo"].Value.ToString());
                categoriaPDescrip(dataGridView1.CurrentRow.Cells["gru_descri"].Value.ToString());
            }
            else if (Eleccion == "Agregar")
            {
                frmEditorSubCategoria Agregarlbx = new frmEditorSubCategoria();
                Agregarlbx.txtCodigoCategoria.Text = dataGridView1.CurrentRow.Cells["gru_codigo"].Value.ToString();
                Agregarlbx.txtDescripCategoria.Text = dataGridView1.CurrentRow.Cells["gru_descri"].Value.ToString();
                Categoria1.codigoSubCodig = dataGridView1.CurrentRow.Cells["gru_codigo"].Value.ToString();
                Agregarlbx.Eleccion = "Agregar";
                Agregarlbx.idPc = IDMAQ;
                Agregarlbx.ipPc = IPMAQ;
                Categoria1.contadorSubCategoria();
                Agregarlbx.txtCodigoSubCategoria.Text = string.Format("{0:0000}", Categoria1.total);
                Agregarlbx.Show();
            }
            this.Close();
        }
        private void lbxCategoria_Shown(object sender, EventArgs e)
        {
            btnAgregar.Visible = true;
            btnModificar.Visible = true;
            btnImprimir.Visible = true;
            btnEliminar.Visible = true;
            btnSubCategoria.Visible = false;
            if (Eleccion == "Agregar") 
            {
                btnAgregar.Visible = false;
                btnModificar.Visible = false;
                btnImprimir.Visible = false;
                btnEliminar.Visible = false;
                btnSubCategoria.Visible = true;
            }
            if (Eleccion == "Seleccion Producto") 
            {
                btnAgregar.Visible = false;
                btnModificar.Visible = false;
                btnImprimir.Visible = false;
                btnEliminar.Visible = false;
                btnSubCategoria.Visible = true;
            }
        }

        private void txtBuscador_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                btnBuscar.PerformClick();
            }
            if (rdbCodigo.Checked == true)
            {
                e.Handled = char.IsNumber(e.KeyChar) || e.KeyChar == 8 ? false : true;
                txtBuscador.MaxLength = 6;
            }
            if (rdbDescripcion.Checked == true)
            {
                e.Handled = char.IsLetter(e.KeyChar) || char.IsNumber(e.KeyChar) || e.KeyChar == 32 || e.KeyChar == 46 || e.KeyChar == 44 || e.KeyChar == 8 ? false : true;
            }
        }


        private void btnVisor_Click(object sender, EventArgs e)
        {

            if (dataGridView1.CurrentRow != null)
            {
                frmEditorCategoria Visor = new frmEditorCategoria();
                Visor.txtCodigoCategoria.Text = dataGridView1.CurrentRow.Cells["gru_codigo"].Value.ToString();
                Visor.txtDescricionCategoria.Text = dataGridView1.CurrentRow.Cells["gru_descri"].Value.ToString();
                Visor.txtComCompra.Text = dataGridView1.CurrentRow.Cells["gru_comvta"].Value.ToString();
                Visor.txtComVenta.Text = dataGridView1.CurrentRow.Cells["gru_comcob"].Value.ToString();
                Visor.Eleccion = "Visor";
                Visor.Show();
                this.Close();
            }
        }
        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            categoriaPasado(dataGridView1.CurrentRow.Cells["gru_codigo"].Value.ToString());
            categoriaPDescrip(dataGridView1.CurrentRow.Cells["gru_descri"].Value.ToString());
            this.Close();
        }
        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                categoriaPasado(dataGridView1.CurrentRow.Cells["gru_codigo"].Value.ToString());
                categoriaPDescrip(dataGridView1.CurrentRow.Cells["gru_descri"].Value.ToString());
                this.Close();
            }
        }
    }
}