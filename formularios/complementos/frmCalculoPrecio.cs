using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using lebrun.clasesData;

namespace lebrun.formularios.complementos
{
    public partial class frmCalculoPrecio : Form
    {

        private string codigo;
        private string descripcion;
        private ConexionBD database;
        private DataTable tabla;
        private DataTable tabla2;
        private string StrSql = "";
        private string costo;
        //private char cAlgoritmo;
        private decimal porcentaje = 0;
        private decimal costoReal = 0;
        //private decimal costoCero;
        private decimal porcAlicuota;
        private string incluyeIva;
        private decimal porcIva;

        public frmCalculoPrecio()
        {
            InitializeComponent();
            tabla = new DataTable();
            tabla2 = new DataTable();
            database = new ConexionBD();
        }

        public frmCalculoPrecio(string cod, string des)
        {
            InitializeComponent();
            codigo = cod;
            descripcion = des;
            tabla = new DataTable();
            tabla2 = new DataTable();
            database = new ConexionBD();
        }

        private void factores()
        {
            StrSql = "";
            StrSql = "SELECT factor1,factor2,factor3,factor4,factor5,factor6," +
                "factor7,factor8,factor9,factor10,costo_algoritmo FROM sysconf.confdatosempresa WHERE empre_actual='1'";
            
            tabla = database.fDataTable(StrSql);
            database.cerrarConexion();
            dataGridView1.DataSource = tabla;

            dataGridView1.Columns[10].Visible = false;

        }

        private void consultarCosto()
        {
            StrSql = "";
            StrSql = "SELECT ult_costo,cost_prom FROM adminv2 WHERE inv2_codigo='$1'";
            StrSql = StrSql.Replace("$1", codigo);

            tabla2 = database.fDataTable(StrSql);
            database.cerrarConexion();

            if (tabla2.Rows.Count > 0)
            {
                if(tabla.Rows[0][10].ToString().Equals("1"))
                {
                    costo = tabla2.Rows[0][0].ToString();
                    lblCosto.Text = tabla2.Rows[0][0].ToString();
                    label3.Text = "U.C:";
                }
                if(tabla.Rows[0][10].ToString().Equals("2"))
                {
                    costo = tabla2.Rows[0][1].ToString();
                    lblCosto.Text = tabla2.Rows[0][1].ToString();
                    label3.Text = "C.P:";
                }

            }
        }
        
        private void calculoPorcentaje()
        {
            for (int i = 0; i < dataGridView1.ColumnCount - 1; i++)
            {
                porcentaje = porcentaje + (Convert.ToDecimal(dataGridView1.Rows[0].Cells[i].Value.ToString()));
            }

            if (Convert.ToDecimal(porcentaje) == 0)
            {
                lblPrecio.Text = costo;
            }
            else
            {
                lblPrecio.Text = "" + ((1 + (Convert.ToDecimal(porcentaje)) / 100) * (Convert.ToDecimal(costo)));
            }

            lblPrecio.Text = string.Format("{0:0.00}", Convert.ToDecimal(lblPrecio.Text));
        }


        private void calculoConFactores()
        {
            if (Convert.ToDecimal(costo) == 0)
            {
                costoReal = Convert.ToDecimal(0.01);
            }
            else
            {
                costoReal = Convert.ToDecimal(costo);
            }

            for (int i = 0; i < dataGridView1.ColumnCount - 1; i++)
            {
                if (Convert.ToDecimal(dataGridView1.Rows[0].Cells[i].Value.ToString()) != 0)
                {
                    costoReal = costoReal * (1 + ((Convert.ToDecimal(dataGridView1.Rows[0].Cells[i].Value.ToString())) / 100));
                }
                
            }
            //
            lblPrecio.Text = string.Format("{0:0.00}", Convert.ToDecimal(costoReal));

        }

        private void frmCalculoPrecio_Load(object sender, EventArgs e)
        {
            lblCodigo.Text = codigo;
            lblDescripcion.Text = descripcion;
            factores();
            consultarCosto();
            calculoConFactores();
            buscarIncluyeIva();
            buscarPorcAlicuota();
            calcularPrecioFinal();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buscarIncluyeIva()
        {
            StrSql = "SELECT ttp_inciva FROM admpreciotip WHERE ttp_codigo='A'";
            tabla2 = database.fDataTable(StrSql);

            if (tabla2.Rows.Count > 0)
            {
                incluyeIva = tabla2.Rows[0]["ttp_inciva"].ToString();
            }
        }
        
        private void buscarPorcAlicuota()
        {
            StrSql = "SELECT adminv.inv_codigo, adminv.inv_descri,admiva.alict_venta,admiva.alict_tipodiva " +
                        "FROM adminv LEFT OUTER JOIN admiva ON adminv.inv_iva= admiva.alict_tipodiva WHERE adminv.inv_codigo='000000000013434';";

            tabla2 = database.fDataTable(StrSql);

            if (tabla2.Rows.Count > 0)
            {
                porcAlicuota = Convert.ToDecimal(tabla2.Rows[0]["alict_venta"].ToString());
                lblIva.Text = "" + porcAlicuota;
            }
        }
        
        private void calcularPrecioFinal()
        {
            if (incluyeIva.Equals("1"))
            {
                porcIva = (1 + ((Convert.ToDecimal(porcAlicuota)) / 100));
                costoReal = costoReal * porcIva;

                lblPrecio.Text = string.Format("{0:0.00}", Convert.ToDecimal(costoReal));
            }
        }

    }
}
