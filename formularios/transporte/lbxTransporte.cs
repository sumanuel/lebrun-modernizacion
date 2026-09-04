using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using lebrun.clases.clientes;

namespace lebrun.formularios.transporte
{
    public partial class lbxTransporte : Form
    {
        private DataView dv;
        private DataTable dtTransporte;
        private Transporte trnasp;

        public lbxTransporte()
        {
            InitializeComponent();
        }

        

        private void lbxTransporte_Load(object sender, EventArgs e)
        {
            trnasp = new Transporte();
            dtTransporte = trnasp.lbxTransp();
            dv = new DataView(dtTransporte);
            dataGridView1.DataSource = dtTransporte;
            dataGridView1.Columns[0].HeaderText = "Código";
            dataGridView1.Columns[1].HeaderText = "Descripción Transporte";
            dataGridView1.Columns[2].HeaderText = "Descripción Proveedor";
            dataGridView1.Columns[3].Visible = false;
            tabControl1.TabPages.Remove(this.tabPage2);
            comboBox1.SelectedIndex = 0;
        }

        private void cmdSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdAgregar_Click(object sender, EventArgs e)
        {
            if (tabControl1.TabPages.IndexOf(this.tabPage2) == -1) {
                tabControl1.TabPages.Add(this.tabPage2);
                tabControl1.SelectedIndex = 1;
            }
        }

        private void cmdSalirDatos_Click(object sender, EventArgs e)
        {
            tabControl1.TabPages.Remove(this.tabPage2);
            //tabControl1.SelectedIndex = 0;
        }

        private void cmdBuscar_Click(object sender, EventArgs e)
        {
            if (txtBuscar.Text != "")
            {
                filtrar(txtBuscar.Text);
            }
            else
            {
                MessageBox.Show("Debe Poner un Dato Para buscar!!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtBuscar.Focus();
            }
        }

        private void filtrar(string filtro)
        {
            string sentencia;
            DataTable temp;

            try
            {
                sentencia = "trans_cod LIKE '" + filtro + "%'";
                dv.RowFilter = sentencia;
                if (dv.Count == 0)
                {
                    sentencia = "trans_des LIKE '" + filtro + "%'";
                    dv.RowFilter = sentencia;
                    if (dv.Count == 0)
                    {
                        sentencia = "trans_codpro LIKE '" + filtro + "%'";
                        dv.RowFilter = sentencia;
                    }
                }
                if (dv.Count > 0)
                {
                    dataGridView1.DataSource = dv;
                    dataGridView1.Update();
                }
                else
                {
                    trnasp.CodigoTransporte = filtro;
                    temp = trnasp.transporteBuscado();
                    if (temp.Rows.Count == 0)
                    {
                        MessageBox.Show("No se consiguieron Resultados!!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        dataGridView1.DataSource = null;
                        dataGridView1.DataSource = temp;
                        dataGridView1.Columns[0].HeaderText = "Código";
                        dataGridView1.Columns[1].HeaderText = "Descripción Transporte";
                        dataGridView1.Columns[2].HeaderText = "Descripción Proveedor";
                        dataGridView1.Columns[3].Visible = false;
                        dataGridView1.Update();
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error buscar " + e.Message);
            }
        }

        private void cmdAcepAgre_Click(object sender, EventArgs e)
        {
            if (validarCampos()) {
                asignarCampos();
                if (!trnasp.existeTransporte())
                {
                    if (trnasp.agregarTransporte())
                    {
                        MessageBox.Show("Transporte Agregado Exitosamente!!", "Exito!! ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        limpiarAgregar();
                        tabControl1.TabPages.Remove(this.tabPage2);
                    }
                    else {
                        MessageBox.Show("Problema al Agregar Transporte!!", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else {
                    MessageBox.Show("El Transporte Existe!!", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public bool validarCampos() {
            bool centinela = true;
            if (txtCodigoTrans.Text =="")
            {
                MessageBox.Show("El Código es un campo Obligatorio!!", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCodigoTrans.Focus();
                centinela = false;
                return centinela;
            }

            if (txtDescripcion.Text == "") {
                MessageBox.Show("La Descripción es un campo Obligatorio!!", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtDescripcion.Focus();
                centinela = false;
                return centinela;
            }
            if (txtProveedor.Text == "") {
                MessageBox.Show("El Proveedor es un campo Obligatorio!!", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtProveedor.Focus();
                centinela = false;
                return centinela;
            }
            return centinela;
        }

        public void asignarCampos() {
            trnasp.CodigoTransporte = txtCodigoTrans.Text;
            trnasp.TransporteDescripcion = txtDescripcion.Text;
            trnasp.TransporteCodProveedor = txtProveedor.Text;
            trnasp.TransporteStatus = comboBox1.Text;
            trnasp.TransporteObservaciones = txtObservaciones.Text;
        }

        public void asignarCampos2()
        {
            txtCodigoTrans.Text=trnasp.CodigoTransporte;
            txtDescripcion.Text=trnasp.TransporteDescripcion;
            txtProveedor.Text= trnasp.TransporteCodProveedor;
            comboBox1.SelectedIndex = comboBox1.FindString(trnasp.TransporteStatus);
            txtObservaciones.Text=trnasp.TransporteObservaciones;
        }

        public void limpiarAgregar() {
            txtCodigoTrans.Clear();
            txtDescripcion.Clear();
            txtProveedor.Clear();
            txtObservaciones.Clear();
            comboBox1.SelectedIndex = 0;
        }

        private void cmdLimpiar_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            dtTransporte = trnasp.lbxTransp();
            //dv = new DataView(dtTransporte);
            dataGridView1.DataSource = dtTransporte;
            dataGridView1.Columns[0].HeaderText = "Código";
            dataGridView1.Columns[1].HeaderText = "Descripción Transporte";
            dataGridView1.Columns[2].HeaderText = "Descripción Proveedor";
            dataGridView1.Columns[3].Visible = false;
            tabControl1.TabPages.Remove(this.tabPage2);
            comboBox1.SelectedIndex = 0;
            txtBuscar.Clear();
        }

        private void cmdModificar_Click(object sender, EventArgs e)
        {
            trnasp.CodigoTransporte = null;
            trnasp.CodigoTransporte = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            if (trnasp.existeTransporte()) {
                trnasp.cargarDatosClientes();
                asignarCampos2();
                if (tabControl1.TabPages.IndexOf(this.tabPage2) == -1)
                {
                    tabControl1.TabPages.Add(this.tabPage2);
                    tabControl1.SelectedIndex = 1;
                }
            }
        }

        private void cmdModificarTrans_Click(object sender, EventArgs e)
        {
            if (validarCampos()) {
                if (trnasp.existeTransporte())
                {
                    asignarCampos();
                    if (trnasp.modificarTransporte())
                    {
                        MessageBox.Show("Transporte Modificado Exitosamente!!", "Exito!! ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        limpiarAgregar();
                        tabControl1.TabPages.Remove(this.tabPage2);
                    }
                    else
                    {
                        MessageBox.Show("Problema al modificar!!", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else {
                    MessageBox.Show("El Transporte No Existe!!", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
