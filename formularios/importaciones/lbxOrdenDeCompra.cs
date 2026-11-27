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
            abrirOrden(null, false);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string orden = ordenSeleccionada("modificar");
            if (orden != null)
            {
                abrirOrden(orden, false);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string orden = ordenSeleccionada("ver");
            if (orden != null)
            {
                abrirOrden(orden, true);
            }
        }

        // El numero de la orden elegida en la lista; null (con un aviso) si no hay ninguna, por ejemplo en la fila vacia del final.
        private string ordenSeleccionada(string paraQue)
        {
            DataGridViewRow fila = dataGridView1.CurrentRow;
            string orden = fila == null || fila.IsNewRow ? "" : Convert.ToString(fila.Cells[0].Value);
            if (orden.Length == 0)
            {
                MessageBox.Show("Seleccione en la lista la orden que quiere " + paraQue + ".", "Ordenes de compra", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return null;
            }
            return orden;
        }

        // Abre la ventana de la orden: nueva (numero null), para modificar o solo para verla.
        private void abrirOrden(string numero, bool soloVer)
        {
            frmOrdenCompra abierta = frmOrdenCompra.VentanaAbierta;
            if (abierta != null)
            {
                MessageBox.Show("Ya hay una orden de compra abierta. Ciérrela antes de abrir otra.", "Ordenes de compra", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (abierta.WindowState == FormWindowState.Minimized)
                {
                    abierta.WindowState = FormWindowState.Normal;
                }
                abierta.BringToFront();
                abierta.Activate();
                return;
            }
            frmOrdenCompar = frmOrdenCompra.DefInstance;
            if (soloVer)
            {
                frmOrdenCompar.MdiParent = this.MdiParent;
            }
            if (numero != null)
            {
                frmOrdenCompar.cargarModificar(numero);
            }
            if (soloVer)
            {
                frmOrdenCompar.button5.Enabled = false;
                frmOrdenCompar.button3.Enabled = false;
                frmOrdenCompar.btnAddProduct.Enabled = false;
            }
            else
            {
                frmOrdenCompar.modificarUsu(Principal.usuarioActual.Id);
            }
            frmOrdenCompar.FormClosed -= ordenCerrada;
            frmOrdenCompar.FormClosed += ordenCerrada;
            frmOrdenCompar.Show();
        }

        // Al cerrar una orden que se grabo, la lista se actualiza.
        private void ordenCerrada(object sender, FormClosedEventArgs e)
        {
            if (((frmOrdenCompra)sender).Grabada && !IsDisposed)
            {
                cargarOrdenesCompra();
            }
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
            if (ordenSeleccionada("imprimir") == null)
            {
                return;
            }
            contenedorOrden = new frmContenedorOrden(
                dataGridView1.CurrentRow.Cells[0].Value.ToString(),
                dataGridView1.CurrentRow.Cells[4].Value.ToString(),
                dataGridView1.CurrentRow.Cells[1].Value.ToString(),
                dataGridView1.CurrentRow.Cells[6].Value.ToString(), "Nacional");

            contenedorOrden.Show();
        }
        












 










    }
}
