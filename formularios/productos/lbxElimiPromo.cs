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
    public partial class lbxElimiPromo : Form
    {
        Productos productos;
        private DataView dv;
        private DataTable dt;
        public string idPc;
        public string ipPc;
        public string modulo = "Eliminacion De Promo";
        public lbxElimiPromo()
        {
            InitializeComponent();
            this.ttmensajes.SetToolTip(this.btnSalir, "Salir");
            this.ttmensajes.SetToolTip(this.btnCancelar, "Cancelar");
            this.ttmensajes.SetToolTip(this.btnEliminar, "Eliminar");
            this.ttmensajes.SetToolTip(this.btnAgreUnidad, "Aceptar");
        }
        public void colorActivo()
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                string val = dataGridView1.Rows[i].Cells[7].Value.ToString();
                if (val == "Activo")
                {
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Green;
                }
            }

        }

        private void lbxElimiPromo_Load(object sender, EventArgs e)
        {
            productos = new Productos();
            dt = productos.eliminarPromo(txtCodigo.Text);
            dv = new DataView(dt);
            this.dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].HeaderText = "Medida";
            dataGridView1.Columns[1].HeaderText = "Medida promo";
            dataGridView1.Columns[2].HeaderText = "Fecha De Fin";
            dataGridView1.Columns[3].HeaderText = "Fecha De Desde";
            dataGridView1.Columns[4].HeaderText = "Precios";
            dataGridView1.Columns[5].HeaderText = "Observacion";
            dataGridView1.Columns[6].HeaderText = "Multiplo";
            colorActivo();
            groupBox2.Visible = false;
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            string Statud = dataGridView1.CurrentRow.Cells["Status"].Value.ToString();
            if (Statud == "Activo")
            {
                groupBox2.Visible = true;
                groupBox1.Visible = false;
                label3.Text = "¿Desea Eliminar Esta Promocion?";
            }
            else
            {
                MessageBox.Show("Solo Se Puede Desactivar Producto Activo !!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            groupBox2.Visible = false;
            groupBox1.Visible = true;
        }

        private void btnAgreUnidad_Click(object sender, EventArgs e)
        {
            string accion = "Elim";
            string codigo = txtCodigo.Text;
            string fechaInicio = dataGridView1.CurrentRow.Cells["prom_fchedes"].Value.ToString(); ;
            string frchaFin = dataGridView1.CurrentRow.Cells["prom_fchfin"].Value.ToString(); ;
            productos.desactivarPromo(codigo,frchaFin,fechaInicio);
            groupBox2.Visible = false;
            groupBox1.Visible = true;
            dt = productos.eliminarPromo(txtCodigo.Text);
            dv = new DataView(dt);
            this.dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].HeaderText = "Medida";
            dataGridView1.Columns[1].HeaderText = "Fecha De Fin";
            dataGridView1.Columns[2].HeaderText = "Fecha De Desde";
            dataGridView1.Columns[3].HeaderText = "Precios";
            dataGridView1.Columns[4].HeaderText = "Observacion";
            dataGridView1.Columns[5].HeaderText = "Multiplo";
            colorActivo();
            productos.trazUsuario(idPc, ipPc, modulo, accion, codigo);
            MessageBox.Show("Se Desactivar Con Exito !!", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            dataGridView1.Update();
        }
    }
}
