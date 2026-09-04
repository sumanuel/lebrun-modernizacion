using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using lebrun.clases.importaciones;
using lebrun.formularios.complementos;
using lebrun.clases.complementos;
using lebrun.clasesData;
using lebrun.Reportes.compras;

namespace lebrun.formularios.importaciones
{
    public partial class lbxOrdenDeCompra : Form
    {

        private OrdenDeCompra ordenCompra;
        private frmOrdenCompra frmOrdenCompar;
        private frmImportaciones refImportaciones;
        public UsuarioSistema usuarioActual;
        private static lbxOrdenDeCompra m_FormDefInstance;
        public string usuario;
        private string origen;
        private int opc;
        private Principal Principal;
        private frmContenedorCompras contenedorCompras;
        private frmContenedorOrden contenedorOrden;

        public lbxOrdenDeCompra()
        {
            InitializeComponent();
            usuarioActual = new UsuarioSistema();
            refImportaciones = new frmImportaciones();

        }

        private void lbxOrdenDeCompra_Load(object sender, EventArgs e)
        {
            string[] parametros;
            parametros = Environment.GetCommandLineArgs();
            if (parametros.Length > 1)
            {
                usuarioActual.Id = parametros[1];
            }
            Principal = (Principal)this.MdiParent;
            ordenCompra = new OrdenDeCompra();
            


            //dataGridView1.DataSource = null;
            //dataGridView1.DataSource = ordenCompra.armarLbxOrdenCompra();
            cargarOrdenesCompra();

            //usuarioActual.Id = "0003";
            //usuario = "0004";
            
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmOrdenCompar = frmOrdenCompra.DefInstance;
            //frmOrdenCompar.MdiParent = (Principal)this.MdiParent;
            frmOrdenCompar.modificarUsu(Principal.usuarioActual.Id);
            frmOrdenCompar.Show();
            
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount > 1)
            {
                frmOrdenCompar = frmOrdenCompra.DefInstance;
                //frmOrdenCompar.MdiParent = this.MdiParent;
                frmOrdenCompar.cargarModificar(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                frmOrdenCompar.modificarUsu(Principal.usuarioActual.Id);
                frmOrdenCompar.Show();
            }
            else { MessageBox.Show("No existe ningun elemento para modificar", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount > 1)
            {
                frmOrdenCompar = frmOrdenCompra.DefInstance;
                frmOrdenCompar.MdiParent = this.MdiParent;
                frmOrdenCompar.cargarModificar(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                //frmOrdenCompar.groupBox1.Enabled = false;
                //frmOrdenCompar.groupBox2.Enabled = false;
                //frmOrdenCompar.button3.Enabled = false;
                frmOrdenCompar.button5.Enabled = false;
                frmOrdenCompar.button3.Enabled = false;
                frmOrdenCompar.btnAddProduct.Enabled = false;

                frmOrdenCompar.Show();
            }
            else { MessageBox.Show("No existe ningun elemento para ver", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        }

        private void txtBusqueda_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar==(char)(Keys.Enter))
            {
                DataTable dtorden = new DataTable();

                if (radioButton1.Checked)
                {
                    opc = 0;
                }

                if (radioButton2.Checked)
                {
                    opc = 1;
                }
                if (radioButton3.Checked)
                {
                    opc = 2;
                }
                

                dtorden = ordenCompra.buscarOrdenCompra(opc, txtBusqueda.Text);

                if (dtorden.Rows.Count != 0)
                {
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = dtorden;
                }
                else
                {
                    MessageBox.Show("No se ha encontrado ningun resultado", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        public void cargarOrdenesCompra() {

            dataGridView1.DataSource = null;
            dataGridView1.DataSource = ordenCompra.armarLbxOrdenCompra();
        
        }

        private void lbxOrdenDeCompra_Activated(object sender, EventArgs e)
        {
            cargarOrdenesCompra();
        }

        public static lbxOrdenDeCompra DefInstance
        {
            get
            {
                if (m_FormDefInstance == null || m_FormDefInstance.IsDisposed)
                    m_FormDefInstance = new lbxOrdenDeCompra();
                return m_FormDefInstance;
            }
            set
            {
                m_FormDefInstance = value;
            }
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            if (origen != null)
            {
                if (origen.Equals("frmImportaciones"))
                {
                    refImportaciones.txtNumeroOrden.Text = this.dataGridView1.CurrentRow.Cells[0].Value.ToString();
                    this.Close();
                }
            }
        }

        public void destino(Form referencia, string opcion)
        {

            if (opcion.Equals("frmImportaciones"))
            {
                origen = opcion;
                refImportaciones = (frmImportaciones)referencia;
            }
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnImpNacional_Click(object sender, EventArgs e)
        {
            contenedorOrden = new frmContenedorOrden(
                dataGridView1.CurrentRow.Cells[0].Value.ToString(),
                dataGridView1.CurrentRow.Cells[4].Value.ToString(),
                dataGridView1.CurrentRow.Cells[1].Value.ToString(),
                dataGridView1.CurrentRow.Cells[6].Value.ToString(), "Nacional");

            contenedorOrden.Show();
        }
        












 










    }
}
