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
    public partial class lbxElimiPrecios : Form
    {
        lbxMantProductos manPro;
        Productos productos;
        private DataView dv;
        private DataTable dt;
        string codigo;
        public string idPc;
        public string ipPc;
        public string modulo ="Eliminacion De Precios" ;
        public lbxElimiPrecios()
        {
            InitializeComponent();
            this.ttmensajes.SetToolTip(this.btnSalir, "Salir");
            this.ttmensajes.SetToolTip(this.btnCancelar, "Cancelar");
            this.ttmensajes.SetToolTip(this.btnEliminar, "Eliminar");
            this.ttmensajes.SetToolTip(this.btnAgreUnidad, "Aceptar");
        }

        private void lbxElimiPrecios_Load(object sender, EventArgs e)
        {
            manPro = new lbxMantProductos();
            productos = new Productos();
            dt = productos.eliminarPrecios(txtCodigo.Text);
            dv = new DataView(dt);
            this.dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].HeaderText = "Lista";
            dataGridView1.Columns[1].HeaderText = "Precios";
            dataGridView1.Columns[2].HeaderText = "Descuentos";
            dataGridView1.Columns[3].HeaderText = "Utilidad";
            dataGridView1.Columns[4].HeaderText = "Vencimiento";
            dataGridView1.Columns[5].HeaderText = "Usuario";
            colorActivo();
            groupBox2.Visible = false;
        }
        public void colorActivo() 
        {
         for(int i=0;i<dataGridView1.Rows.Count;i++)
         {
             string val = dataGridView1.Rows[i].Cells[6].Value.ToString();
             if (val == "Activo")
             {
                 dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Green;
             }
         }
         
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            string Statud = dataGridView1.CurrentRow.Cells["Estado"].Value.ToString();
            codigo = txtCodigo.Text;
            if (Statud == "Activo")
            {
                groupBox2.Visible = true;
                groupBox1.Visible = false;
                label3.Text = "Desea Eliminar El Precio [" + dataGridView1.CurrentRow.Cells["pre_lista"].Value.ToString() + "] " + dataGridView1.CurrentRow.Cells["pre_precio"].Value.ToString();
            }
            else 
            {
                MessageBox.Show("Solo Se Puede Desactivar Precios Activo !!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            groupBox2.Visible = false;
            groupBox1.Visible = true;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAgreUnidad_Click(object sender, EventArgs e)
        {
            string accion="Elim";
            string tipoPrecio = dataGridView1.CurrentRow.Cells["pre_lista"].Value.ToString();
            string codigo = txtCodigo.Text;
            productos.desactivarPrecios(codigo,tipoPrecio);
            groupBox2.Visible = false;
            groupBox1.Visible = true;
            dt = productos.eliminarPrecios(txtCodigo.Text);
            dv = new DataView(dt);
            this.dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].HeaderText = "Lista";
            dataGridView1.Columns[1].HeaderText = "Precios";
            dataGridView1.Columns[2].HeaderText = "Descuentos";
            dataGridView1.Columns[3].HeaderText = "Utilidad";
            dataGridView1.Columns[4].HeaderText = "Vencimiento";
            dataGridView1.Columns[5].HeaderText = "Usuario";
            colorActivo();
            productos.trazUsuario(idPc, ipPc, modulo, accion, codigo);
            MessageBox.Show("Se Desactivar Con Exito !!", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

      

    }
}
