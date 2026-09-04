using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using lebrun.clases.complementos;
using lebrun.formularios.compras;

namespace lebrun.formularios.complementos
{
    public partial class lbxtipoDocPro : Form
    {
        private TipoDocumentoProveedor tipoDocuProveedor;
        private static lbxtipoDocPro m_FormDefInstance;
        private frmPagare refPagare;
        private frmProcesoPagoManual refPagoManual;
        private frmServicios refServicios;
        
        private string origen;

        public lbxtipoDocPro()
        {
            InitializeComponent();
            tipoDocuProveedor = new TipoDocumentoProveedor();
        }


        public static lbxtipoDocPro DefInstance
        {
            get
            {
                if (m_FormDefInstance == null || m_FormDefInstance.IsDisposed)
                    m_FormDefInstance = new lbxtipoDocPro();
                return m_FormDefInstance;
            }
            set
            {
                m_FormDefInstance = value;
            }
        }

        public void destino(Form referencia, string opcion)
        {

            if (opcion.Equals("frmPagare"))
            {
                origen = opcion;
                refPagare = (frmPagare)referencia;
            }

            if (opcion.Equals("Compras Otras Transacciones Procesar Pagos Manuales"))
            {
                origen = opcion;
                refPagoManual = (frmProcesoPagoManual)referencia;
            }

            if (opcion.Equals("frmServicios"))
            {
                origen = opcion;
                refServicios = (frmServicios)referencia;
            }
        }



        private void lbxtipoDocPro_Load(object sender, EventArgs e)
        {
            if (origen.Equals("frmPagare"))
            {

                gridTipoDocumentos.DataSource = tipoDocuProveedor.cargarDocumentos();
                gridTipoDocumentos.Columns[0].Width = 100;
                gridTipoDocumentos.Columns[1].Width = 100;
                gridTipoDocumentos.Columns[2].Width = 600;
                gridTipoDocumentos.Columns[3].Visible = false;
                gridTipoDocumentos.Columns[4].Visible = false;
                gridTipoDocumentos.Columns[5].Visible = false;
            }
            else if (origen.Equals("frmServicios"))
            {
                gridTipoDocumentos.DataSource = tipoDocuProveedor.cargarTodosDocumentosServicios();
                gridTipoDocumentos.Columns[0].Width = 100;
                gridTipoDocumentos.Columns[1].Width = 100;
                gridTipoDocumentos.Columns[2].Width = 600;
                gridTipoDocumentos.Columns[3].Visible = false;
                gridTipoDocumentos.Columns[4].Visible = false;
                gridTipoDocumentos.Columns[5].Visible = false;
            }
            else
            {
                gridTipoDocumentos.DataSource = tipoDocuProveedor.cargarTodosDocumentos();
                gridTipoDocumentos.Columns[0].Width = 100;
                gridTipoDocumentos.Columns[1].Width = 100;
                gridTipoDocumentos.Columns[2].Width = 600;
                gridTipoDocumentos.Columns[3].Visible = false;
                gridTipoDocumentos.Columns[4].Visible = false;
                gridTipoDocumentos.Columns[5].Visible = false;

            }
        }

        private void gridTipoDocumentos_DoubleClick(object sender, EventArgs e)
        {
            if (origen.Equals("frmPagare"))
            {
                refPagare.txtTipoDoc.Text = this.gridTipoDocumentos.CurrentRow.Cells[0].Value.ToString();
                refPagare.lblDescripDoc.Text = this.gridTipoDocumentos.CurrentRow.Cells[1].Value.ToString();
                refPagare.txtTipoDoc.Focus();
                this.Close();
            }

            if (origen.Equals("Compras Otras Transacciones Procesar Pagos Manuales"))
            {
                refPagoManual.txtTipoDoc.Text = this.gridTipoDocumentos.CurrentRow.Cells[0].Value.ToString();
                refPagoManual.lblTipoDoc.Text = this.gridTipoDocumentos.CurrentRow.Cells[1].Value.ToString();
                refPagoManual.txtTipoDoc.Focus();
                this.Close();
            }

            if (origen.Equals("frmServicios"))
            {

                refServicios.txtTipoDoc.Text = this.gridTipoDocumentos.CurrentRow.Cells[0].Value.ToString();
                refServicios.lblTipoDoc.Text = this.gridTipoDocumentos.CurrentRow.Cells[2].Value.ToString();
                refServicios.txtTipoDoc.Focus();
                this.Close();
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (rbtnCodigo.Checked)
            {
                gridTipoDocumentos.DataSource = tipoDocuProveedor.buscarDocumentos(1, txtBusqueda.Text);
                gridTipoDocumentos.Columns[0].Width = 100;
                gridTipoDocumentos.Columns[1].Width = 100;
                gridTipoDocumentos.Columns[2].Width = 600;
                gridTipoDocumentos.Columns[3].Visible = false;
                gridTipoDocumentos.Columns[4].Visible = false;
                gridTipoDocumentos.Columns[5].Visible = false;
            }
            if (rbtnDescripcion.Checked)
            {
                gridTipoDocumentos.DataSource = tipoDocuProveedor.buscarDocumentos(2, txtBusqueda.Text);
                gridTipoDocumentos.Columns[0].Width = 100;
                gridTipoDocumentos.Columns[1].Width = 100;
                gridTipoDocumentos.Columns[2].Width = 500;
                gridTipoDocumentos.Columns[3].Visible = false;
                gridTipoDocumentos.Columns[4].Visible = false;
                gridTipoDocumentos.Columns[5].Visible = false;
                
            }
        }




    }
}
