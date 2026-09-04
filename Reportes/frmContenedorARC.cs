using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using lebrun.clasesData;
using lebrun.clases.Reportes;
        
namespace lebrun.Reportes
{
    public partial class frmContenedorARC : Form
    {
        private Compania compania;
        private reporteARC rptARC;
        private arc ARC;
        private string fechaDesde;
        private string fechaHasta;
        private string valor;
        private int opcion;
        private DataTable tabla;
        private ConexionBD database;
        private DataTable tabla2;


        public frmContenedorARC()
        {
            InitializeComponent();

        }

        public frmContenedorARC(string fdesde,string fhasta, string textBox, int opc)
        {
            InitializeComponent();
  
            fechaDesde = fdesde;
            fechaHasta = fhasta;
            valor = textBox;
            opcion = opc;
            ARC = new arc();
            database = new ConexionBD();
   
        }
        
        private void imprimirReporte() {

            rptARC = new reporteARC();

            if (opcion == 1)
            {
                //TRABAJADOR
                tabla = ARC.armarTablaTrabajadores(fechaDesde, fechaHasta, valor);
                rptARC.SetDataSource(tabla);
           
            }
            if (opcion == 2)
            {
                //PROVEEDOR
                tabla = ARC.armarTablaProveedor(fechaDesde, fechaHasta, valor);
                rptARC.SetDataSource(tabla);
                
            }
            if (opcion == 3)
            {
                //TODOS LOS TRABAJADORES
                tabla = ARC.armarTablaTrabajadores(fechaDesde, fechaHasta);
                rptARC.SetDataSource(tabla);
              
            }
            if (opcion == 4)
            {
                //TODOS LOS PROVEEDORES
                tabla = ARC.armarTablaProveedor(fechaDesde, fechaHasta);
                rptARC.SetDataSource(tabla);
               
            }
            if (opcion == 5)
            {
                //RAQUEL Y TODO AQUEL
                tabla = ARC.armarTablaProveedor(fechaDesde, fechaHasta);
                tabla.Merge(ARC.armarTablaTrabajadores(fechaDesde, fechaHasta));
                rptARC.SetDataSource(tabla);
                
            }

            if (tabla.Rows.Count != 0)
            {
                tabla2 = database.fDataTable("SELECT confi_direcc FROM admconfisisten");

                rptARC.SetParameterValue("direccion",tabla2.Rows[0]["confi_direcc"].ToString().ToUpper());
                rptARC.SetParameterValue("NombreCompania", compania.Nombre.ToUpper());
                rptARC.SetParameterValue("rifCompania", compania.Rif);
                rptARC.SetParameterValue("fechaDesde", Convert.ToDateTime(fechaDesde));
                rptARC.SetParameterValue("fechaHasta", Convert.ToDateTime(fechaHasta));
                this.crystalReportViewer1.ReportSource = rptARC;
                crystalReportViewer1.Visible = true;
            }
            else
            {
                MessageBox.Show("No existe informacion para generar este reporte", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        
        }

        private void frmContenedorARC_Load(object sender, EventArgs e)
        {
            compania = new Compania();
            compania.obtenerCodigoCompaniaActual();
            imprimirReporte();
        }

    }
}
