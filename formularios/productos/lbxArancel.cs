using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using lebrun.clases.producto;
using lebrun.formularios.productos;

namespace lebrun.formularios.productos
{
    public partial class lbxArancel : Form
    {
        //llamado la Clases
        Principal SistemaUsuario;
        private Productos Aracel1;
        private DataView dv;
        private DataTable dtArancel;
        public string eleccion;
        public delegate void aracelPD(string datoAracel);
        public delegate void aracelPDD(string datoDArancel);
        public event aracelPD aracelPasado;
        public event aracelPDD arancelDPasado;
        public string frmProduct;
        public string ipMaq;
        public string IdMaq;
        public string modulo = "Aracel";
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

        public lbxArancel()
        {
            InitializeComponent();
            this.ttmensajes.SetToolTip(this.btnAgregar, "Agregar");
            this.ttmensajes.SetToolTip(this.btnModificar, "Modificar");
            this.ttmensajes.SetToolTip(this.btnEliminar, "Eliminar");
            this.ttmensajes.SetToolTip(this.btnImprimir, "Imprimir");
            this.ttmensajes.SetToolTip(this.btnLimpiar, "Actualizar");
            this.ttmensajes.SetToolTip(this.btnSalir, "Salir");
            this.ttmensajes.SetToolTip(this.btnBuscar, "Buscar");
            this.ttmensajes.SetToolTip(this.btnSelcArancel, "Seleccionar");
            ttmensajes.SetToolTip(btnVisor, "Visor");
        }
        //Load de Arancel
        private void lbxArancel_Load(object sender, EventArgs e)
        {
           
            Aracel1 = new Productos("200");
            dtArancel = Aracel1.lbxArancel();
            dv = new DataView(dtArancel);
            this.dataGridView1.DataSource = dtArancel;
            dataGridView1.Columns[0].HeaderText = "Codigo";
            dataGridView1.Columns[1].HeaderText = "Descripcion";
            dataGridView1.Columns[2].HeaderText = "Porcetaje";
            dataGridView1.Columns[3].HeaderText = "Kilos";
            dataGridView1.Columns[4].HeaderText = "Impuestos";
            dataGridView1.Columns[5].HeaderText = "Status";
            if (frmProduct != "Producto")
            {
                SistemaUsuario = (Principal)this.MdiParent;
                IdMaq = Convert.ToString(SistemaUsuario.usuarioActual.Id);
                ipMaq = Convert.ToString(SistemaUsuario.usuarioActual.IpPc);
            }
        }
        //Agregar
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmEdicionArancel Agregarlbx = new frmEdicionArancel();  
            Agregarlbx.Eleccion = "Agregar";
            Agregarlbx.idPc = IDMAQ;
            Agregarlbx.ipPc = IPMAQ;
            Agregarlbx.modulo = modulo;
            Agregarlbx.Show();
            Close();
        }
        //Modificar
        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow.Cells["ara_act"].Value.ToString().Equals("1"))
            {
                frmEdicionArancel Modificarlbx = new frmEdicionArancel();
                Modificarlbx.txtCodigoArancel.Text = dataGridView1.CurrentRow.Cells["ara_codigo"].Value.ToString();
                Modificarlbx.txtDescricionArancel.Text = dataGridView1.CurrentRow.Cells["ara_descri"].Value.ToString();
                Modificarlbx.txtKg.Text = dataGridView1.CurrentRow.Cells["ara_kilos"].Value.ToString();
                Modificarlbx.txtImpuesto.Text = dataGridView1.CurrentRow.Cells["ara_impuesto"].Value.ToString();
                Modificarlbx.txtProcentajeArancel.Text = dataGridView1.CurrentRow.Cells["ara_porcen"].Value.ToString();
                Modificarlbx.Eleccion = "Modificar";
                Modificarlbx.idPc = IDMAQ;
                Modificarlbx.ipPc = IPMAQ;
                Modificarlbx.modulo = modulo;
                Modificarlbx.Show();
                Close();
            }
            else
            {
                MessageBox.Show("El Arancel Que a Elejido Se Encuetra Desativado", "Imformacion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        //buscar
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (rdbCodigo.Checked == true)
            {
                txtBuscar.SelectionStart = 0;
                txtBuscar.SelectionLength = txtBuscar.Text.Length;
                if (txtBuscar.Text != "")
                {
                    filtra(txtBuscar.Text);
                }
                else
                {
                    MessageBox.Show("Debe Poner un Dato Para buscar!!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtBuscar.Focus();
                }
            }
            else if(rdbDescripcion.Checked==true)
            {
                txtBuscar.SelectionStart = 0;
                txtBuscar.SelectionLength = txtBuscar.Text.Length;
                if (txtBuscar.Text != "")
                {
                    filtra(txtBuscar.Text);
                }
                else
                {
                    MessageBox.Show("Debe Poner un Dato Para buscar!!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtBuscar.Focus();
                }
            }     

        }
        // funcion de filtro de buscador
        private void filtra(string filtro) 
        {
            DataTable Temp;
           
            if (chbPalContentiva.Checked == true)
            {
                if (rdbCodigo.Checked == true)
                {
                    Temp = Aracel1.codigoArancelBuscados(txtBuscar.Text);
                    if (Temp.Rows.Count == 0)
                    {
                        MessageBox.Show("No se consiguieron Resultados!!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        dataGridView1.DataSource = null;
                        dataGridView1.DataSource = Temp;
                        dataGridView1.Columns[0].HeaderText = "Codigo";
                        dataGridView1.Columns[1].HeaderText = "Descripcion";
                        dataGridView1.Columns[2].HeaderText = "Porcetaje";
                        dataGridView1.Columns[3].HeaderText = "Kilos";
                        dataGridView1.Columns[4].HeaderText = "Impuestos";
                        dataGridView1.Columns[5].HeaderText = "Status";
                        dataGridView1.Update();
                    }
                }
                else if (rdbDescripcion.Checked == true)
                {
                    Temp = Aracel1.descripArancelBuscados(txtBuscar.Text);
                    if (Temp.Rows.Count == 0)
                    {
                        MessageBox.Show("No se consiguieron Resultados!!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        dataGridView1.DataSource = null;
                        dataGridView1.DataSource = Temp;
                        dataGridView1.Columns[0].HeaderText = "Codigo";
                        dataGridView1.Columns[1].HeaderText = "Descripcion";
                        dataGridView1.Columns[2].HeaderText = "Porcetaje";
                        dataGridView1.Columns[3].HeaderText = "Kilos";
                        dataGridView1.Columns[4].HeaderText = "Impuestos";
                        dataGridView1.Columns[5].HeaderText = "Statud";
                        dataGridView1.Update();
                    }
                }
            }
            else
            {
                if (rdbCodigo.Checked == true)
                {
                    Temp = Aracel1.codigoAracelBuscado(txtBuscar.Text);
                    if (Temp.Rows.Count == 0)
                    {
                        MessageBox.Show("No se consiguieron Resultados!!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        dataGridView1.DataSource = null;
                        dataGridView1.DataSource = Temp;
                        dataGridView1.Columns[0].HeaderText = "Codigo";
                        dataGridView1.Columns[1].HeaderText = "Descripcion";
                        dataGridView1.Columns[2].HeaderText = "Porcetaje";
                        dataGridView1.Columns[3].HeaderText = "Kilos";
                        dataGridView1.Columns[4].HeaderText = "Impuestos";
                        dataGridView1.Columns[5].HeaderText = "Statud";
                        dataGridView1.Update();
                    }
                }
                else if (rdbDescripcion.Checked == true)
                {
                    Temp = Aracel1.descripArancelBuscado(txtBuscar.Text);
                    if (Temp.Rows.Count == 0)
                    {
                        MessageBox.Show("No se consiguieron Resultados!!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        dataGridView1.DataSource = null;
                        dataGridView1.DataSource = Temp;
                        dataGridView1.Columns[0].HeaderText = "Codigo";
                        dataGridView1.Columns[1].HeaderText = "Descripcion";
                        dataGridView1.Columns[2].HeaderText = "Porcetaje";
                        dataGridView1.Columns[3].HeaderText = "Kilos";
                        dataGridView1.Columns[4].HeaderText = "Impuestos";
                        dataGridView1.Columns[5].HeaderText = "Statud";
                        dataGridView1.Update();
                    }
                }
            }
        
        }
        //eliminar o desativar
        private void button4_Click(object sender, EventArgs e)
        {
            accion = "Elim";
            string codigoara=dataGridView1.CurrentRow.Cells["ara_codigo"].Value.ToString();
            if (MessageBox.Show("Desea Eliminar Esta Alicuota Del Sistema", "Agregar Nueva Informacion Data", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (dataGridView1.CurrentRow.Cells["ara_act"].Value.ToString().Equals("1"))
                {
                    Aracel1.codigoA = dataGridView1.CurrentRow.Cells["ara_codigo"].Value.ToString();
                    Aracel1.actulizarStatudArancel();
                    Aracel1.trazUsuario(IDMAQ, IPMAQ, modulo, accion, codigoara);
                    MessageBox.Show("El Arancel Se A Eliminado", "Imformacion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    btnLimpiar.PerformClick();
                }
                else
                {
                    MessageBox.Show("El Arancel Que a Elejido Se Encuetra Desativado", "Imformacion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                }
            }
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void txtBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                btnBuscar.PerformClick();
            }
            if (rdbCodigo.Checked == true)
            {
                e.Handled = char.IsNumber(e.KeyChar) || e.KeyChar == 8 ? false : true;
                txtBuscar.MaxLength = 15;
            }
            if (rdbDescripcion.Checked == true)
            {
                e.Handled = char.IsLetter(e.KeyChar) || char.IsNumber(e.KeyChar) || e.KeyChar == 32 || e.KeyChar == 46 || e.KeyChar == 44 || e.KeyChar == 8 ? false : true;
                txtBuscar.MaxLength = 50;
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Aracel1 = new Productos("200");
            dtArancel = Aracel1.lbxArancel();
            dv = new DataView(dtArancel);
            this.dataGridView1.DataSource = dtArancel;
            dataGridView1.Columns[0].HeaderText = "Codigo";
            dataGridView1.Columns[1].HeaderText = "Descripcion";
            dataGridView1.Columns[2].HeaderText = "Porcetaje";
            dataGridView1.Columns[3].HeaderText = "Kilos";
            dataGridView1.Columns[4].HeaderText = "Impuestos";
            dataGridView1.Columns[5].HeaderText = "Statud";
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnVisor_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow.Cells["ara_act"].Value.ToString().Equals("1"))
            {
                frmEdicionArancel Visor = new frmEdicionArancel();
                Visor.txtCodigoArancel.Text = dataGridView1.CurrentRow.Cells["ara_codigo"].Value.ToString();
                Visor.txtDescricionArancel.Text = dataGridView1.CurrentRow.Cells["ara_descri"].Value.ToString();
                Visor.txtKg.Text = dataGridView1.CurrentRow.Cells["ara_kilos"].Value.ToString();
                Visor.txtImpuesto.Text = dataGridView1.CurrentRow.Cells["ara_impuesto"].Value.ToString();
                Visor.txtProcentajeArancel.Text = dataGridView1.CurrentRow.Cells["ara_porcen"].Value.ToString();
                Visor.Eleccion = "Visor";
                Visor.Show();
                Close();
            }
            else
            {
                MessageBox.Show("El Arancel Que a Elejido Se Encuetra Desativado", "Imformacion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void lbxArancel_Shown(object sender, EventArgs e)
        {
            btnSelcArancel.Visible = false;
            if (eleccion == "Selecionar") 
            {
                btnAgregar.Visible = false;
                btnModificar.Visible = false;
                btnEliminar.Visible = false;
                btnImprimir.Visible = false;
                btnVisor.Visible = false;
                btnSelcArancel.Visible= true;
            }
        }
      //Selecion Del Arancel
        private void btnSelcArancel_Click(object sender, EventArgs e)
        {
                aracelPasado(dataGridView1.CurrentRow.Cells["ara_codigo"].Value.ToString());
                arancelDPasado(dataGridView1.CurrentRow.Cells["ara_descri"].Value.ToString());
                this.Close();
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {       
                aracelPasado(dataGridView1.CurrentRow.Cells["ara_codigo"].Value.ToString());
                arancelDPasado(dataGridView1.CurrentRow.Cells["ara_descri"].Value.ToString());
                this.Close();
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                aracelPasado(dataGridView1.CurrentRow.Cells["ara_codigo"].Value.ToString());
                arancelDPasado(dataGridView1.CurrentRow.Cells["ara_descri"].Value.ToString());
                this.Close();
            }
        }
    }
}
