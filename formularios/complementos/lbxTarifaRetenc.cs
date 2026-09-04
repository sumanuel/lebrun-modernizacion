using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using lebrun.clasesData;
using lebrun.clases.complementos;
using lebrun.formularios.importaciones;
using lebrun.formularios.compras;

namespace lebrun.formularios.complementos
{
    public partial class lbxTarifaRetenc : Form
    {

        private RetencionISLR tarifaRetenc;
        //private frmFacturaNacionalImportacion frmFacturaNacional;
        private static lbxTarifaRetenc m_FormDefInstance;
        private frmFacturaNacionalImportacion refFacturaNacional;
        private frmServicios refServicios;
        private string txtImpuesto;
        private string rifProveedor;
        private string codigoProveedor;
        private string origen = "Normal";



        public lbxTarifaRetenc()
        {
            InitializeComponent();
            tarifaRetenc = new RetencionISLR();
        }

        public lbxTarifaRetenc(string rifProve, string codigoProve, string ori)
        {
            InitializeComponent();
            tarifaRetenc = new RetencionISLR();
            rifProveedor = rifProve;
            codigoProveedor = codigoProve;
            origen = ori;
        }

        private void llenarGrid() {

            if (origen == "normal")
            {
                gridTarifaRetenc.DataSource = null;
                gridTarifaRetenc.DataSource = tarifaRetenc.armarRegistros();
            }

            if (origen == "Servicios")
            {
                gridTarifaRetenc.DataSource = null;
                gridTarifaRetenc.DataSource = tarifaRetenc.armarRegistrosOpcion(rifProveedor, codigoProveedor, origen);
            }

            if (origen == "Compras")
            {
                gridTarifaRetenc.DataSource = null;
                gridTarifaRetenc.DataSource = tarifaRetenc.armarRegistrosOpcion(rifProveedor, codigoProveedor, origen);
            }
            
        }

        

        private void llenarGrid(string tipoPer, string residente)
        {
            gridTarifaRetenc.DataSource = null;
            gridTarifaRetenc.DataSource = tarifaRetenc.armarRegistros(tipoPer, residente);

        }

        private void lbxTarifaRetenc_Load(object sender, EventArgs e)
        {
            llenarGrid();
        }

        public void destino(Form referencia, string opcion, string boton )
        {

            if (opcion.Equals("frmFacturaNacionalImportacion"))
            {
                origen = opcion;
                txtImpuesto = boton;
                refFacturaNacional = (frmFacturaNacionalImportacion)referencia;
            }
        }

        public void destino(Form referencia, string opcion)
        {

            if (opcion.Equals("Servicios"))
            {
                origen = opcion;
                refServicios = (frmServicios)referencia;
            }
        }

        public static lbxTarifaRetenc DefInstance
        {
            get
            {
                if (m_FormDefInstance == null || m_FormDefInstance.IsDisposed)
                    m_FormDefInstance = new lbxTarifaRetenc();
                return m_FormDefInstance;
            }
            set
            {
                m_FormDefInstance = value;
            }
        }

        private void gridTarifaRetenc_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (origen.Equals("Servicios"))
            {
                refServicios.dataGridView1.CurrentRow.Cells[10].Value = gridTarifaRetenc.CurrentRow.Cells[0].Value.ToString();
                this.Close();
            }
            


            if (origen.Equals("frmFacturaNacionalImportacion"))
            {
                foreach (Control c in  refFacturaNacional.groupBox2.Controls)
                {
                    if (c.Name == ""+txtImpuesto+"txt")
                    {
                        c.Text = this.gridTarifaRetenc.CurrentRow.Cells[0].Value.ToString();
                        c.AccessibleDescription = this.gridTarifaRetenc.CurrentRow.Cells[1].Value.ToString();
                        c.AccessibleName = this.gridTarifaRetenc.CurrentRow.Cells[2].Value.ToString();
                        c.Tag = this.gridTarifaRetenc.CurrentRow.Cells[3].Value.ToString();
                    }
                }

                foreach (Control c in refFacturaNacional.groupBox3.Controls)
                {
                    if (c.Name == "" + txtImpuesto + "txt")
                    {
                        c.Text = this.gridTarifaRetenc.CurrentRow.Cells[0].Value.ToString();
                        c.AccessibleDescription = this.gridTarifaRetenc.CurrentRow.Cells[1].Value.ToString();
                        c.AccessibleName = this.gridTarifaRetenc.CurrentRow.Cells[2].Value.ToString();
                        c.Tag = this.gridTarifaRetenc.CurrentRow.Cells[3].Value.ToString();
                    }
                }
                this.Close();
            }
        }





        












    }
}
