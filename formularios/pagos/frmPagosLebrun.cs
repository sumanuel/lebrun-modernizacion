using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using lebrun.clases.compras;
//using Microsoft.VisualBasic;


namespace lebrun.formularios.compras
{
    public partial class frmPagosLebrun : Form
    {
        private Pagos pago;
        private DataTable tabla;
        private int opc = 0;
        private DataTable tablaPagos;
        private Principal Principal;

        public frmPagosLebrun()
        {
            InitializeComponent();
            pago = new Pagos();
            tabla = new DataTable();
            tablaPagos = new DataTable();
        }

        private void frmPagosLebrun_Load(object sender, EventArgs e)
        {
            cargarDatos();
            Principal = (Principal)this.MdiParent;
        }

        private void cargarDatos() {

            dataGridView1.Rows.Clear();
            tabla.Clear();
            tabla.Reset();
            tabla = pago.armarDocumentosPendientes();

            for (int i = 0; i < tabla.Rows.Count; i++)
            {
                dataGridView1.Rows.Add();
                dataGridView1.Rows[i].Cells[0].Value = tabla.Rows[i][0].ToString();
                dataGridView1.Rows[i].Cells[1].Value = tabla.Rows[i][1].ToString();
                dataGridView1.Rows[i].Cells[2].Value = tabla.Rows[i][2].ToString();
                dataGridView1.Rows[i].Cells[3].Value = tabla.Rows[i][3].ToString();
                dataGridView1.Rows[i].Cells[4].Value = tabla.Rows[i][4].ToString();
                dataGridView1.Rows[i].Cells[5].Value = tabla.Rows[i][5].ToString();
                dataGridView1.Rows[i].Cells[6].Value = tabla.Rows[i][6].ToString();
                dataGridView1.Rows[i].Cells[7].Value = tabla.Rows[i][7].ToString();
                dataGridView1.Rows[i].Cells[8].Value = tabla.Rows[i][8].ToString();
                dataGridView1.Rows[i].Cells[9].Value = tabla.Rows[i][9].ToString();
                dataGridView1.Rows[i].Cells[10].Value = tabla.Rows[i][10].ToString();
                dataGridView1.Rows[i].Cells[11].Value = tabla.Rows[i][11].ToString();
                dataGridView1.Rows[i].Cells[12].Value = tabla.Rows[i][12].ToString();
                dataGridView1.Rows[i].Cells[13].Value = tabla.Rows[i][13].ToString();
                dataGridView1.Rows[i].Cells[14].Value = tabla.Rows[i][14].ToString();
                dataGridView1.Rows[i].Cells[15].Value = tabla.Rows[i][15].ToString();
                dataGridView1.Rows[i].Cells[16].Value = "";

            }

            dataGridView1.AllowUserToResizeColumns = false;
            dataGridView1.AllowUserToResizeRows = false;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            buscarDocumento();
        }

        private void radioButton1_MouseClick(object sender, MouseEventArgs e)
        {
            opc = 1;
            label1.Visible = false;
            label2.Visible = false;
            fechaDesde.Visible = false;
            fechaHasta.Visible = false;
            txtBuscar.Visible = true;
        }

        private void radioButton2_MouseClick(object sender, MouseEventArgs e)
        {
            opc = 2;
            label1.Visible = false;
            label2.Visible = false;
            fechaDesde.Visible = false;
            fechaHasta.Visible = false;
            txtBuscar.Visible = true;
        }

        private void radioButton3_MouseClick(object sender, MouseEventArgs e)
        {
            opc = 3;
            label1.Visible = false;
            label2.Visible = false;
            fechaDesde.Visible = false;
            fechaHasta.Visible = false;
            txtBuscar.Visible = true;
        }


        private void radioButton5_MouseClick(object sender, MouseEventArgs e)
        {
            opc = 5;
            label1.Visible = true;
            label2.Visible = true;
            fechaDesde.Visible = true;
            fechaHasta.Visible = true;
            txtBuscar.Visible = false;
        }

        private void radioButton6_MouseClick(object sender, MouseEventArgs e)
        {
            opc = 6;

            label1.Visible = true;
            label2.Visible = true;
            fechaDesde.Visible = true;
            fechaHasta.Visible = true;
            txtBuscar.Visible = false;
        }

        private void buscarDocumento() {
            
            RadioButton radio = (groupBox1.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked));

            if (radio.Name != "FechaEmision" && radio.Name != "FechaVencimiento")
            {
                tabla = pago.buscarDocumPendiente(radio.Name, txtBuscar.Text);
            }
            else
            {
                tabla = pago.buscarDocumPendiente(radio.Name, Convert.ToDateTime(fechaDesde.Value).ToString("yyyy-MM-dd"), Convert.ToDateTime(fechaHasta.Value).ToString("yyyy-MM-dd"));
            }


            if (tabla.Rows.Count > 0)
            {
                dataGridView1.DataSource = null;
                dataGridView1.Rows.Clear();
                for (int i = 0; i < tabla.Rows.Count; i++)
                {
                    dataGridView1.Rows.Add();

                    dataGridView1.Rows[i].Cells[0].Value = tabla.Rows[i][0].ToString();
                    dataGridView1.Rows[i].Cells[1].Value = tabla.Rows[i][1].ToString();
                    dataGridView1.Rows[i].Cells[2].Value = tabla.Rows[i][2].ToString();
                    dataGridView1.Rows[i].Cells[3].Value = tabla.Rows[i][3].ToString();
                    dataGridView1.Rows[i].Cells[4].Value = tabla.Rows[i][4].ToString();
                    dataGridView1.Rows[i].Cells[5].Value = tabla.Rows[i][5].ToString();
                    dataGridView1.Rows[i].Cells[6].Value = tabla.Rows[i][6].ToString();
                    dataGridView1.Rows[i].Cells[7].Value = tabla.Rows[i][7].ToString();
                    dataGridView1.Rows[i].Cells[8].Value = tabla.Rows[i][8].ToString();
                    dataGridView1.Rows[i].Cells[9].Value = tabla.Rows[i][9].ToString();
                    dataGridView1.Rows[i].Cells[10].Value = tabla.Rows[i][10].ToString();
                    dataGridView1.Rows[i].Cells[11].Value = tabla.Rows[i][11].ToString();
                    dataGridView1.Rows[i].Cells[12].Value = tabla.Rows[i][12].ToString();
                    dataGridView1.Rows[i].Cells[13].Value = tabla.Rows[i][13].ToString();
                    dataGridView1.Rows[i].Cells[14].Value = tabla.Rows[i][14].ToString();
                    dataGridView1.Rows[i].Cells[15].Value = tabla.Rows[i][15].ToString();
                    dataGridView1.Rows[i].Cells[16].Value = "";

                }
            }
            else
            {
                MessageBox.Show("No hay resultados para esta busqueda", " No info ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            dataGridView1.AllowUserToResizeColumns = false;
            dataGridView1.AllowUserToResizeRows = false;
         
        }

        private void btnProcesar_Click(object sender, EventArgs e)
        {
            contenidoDataTable();

            if (tablaPagos.Rows.Count > 0)
            {
                registrarPago();
                MessageBox.Show("Informacion enviada exitosamente al modulo de pagos", " Exito ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Debe seleccionar al menos un documento a pagar", " Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private bool verificarPagosTemp() {

            return true;
        }
        private void armarDataTable()
        {
            tablaPagos.Clear();
            tablaPagos.Reset();
            
            tablaPagos.Columns.Add("tipo");              //0
            tablaPagos.Columns.Add("descripcion");       //1
            tablaPagos.Columns.Add("documento");         //2
            tablaPagos.Columns.Add("sufijo");            //3
            tablaPagos.Columns.Add("nombreProveedor");   //4
            tablaPagos.Columns.Add("docAfectado");       //5
            tablaPagos.Columns.Add("controlSeniat");     //6
            tablaPagos.Columns.Add("fechaEmision");      //7
            tablaPagos.Columns.Add("fechaVenc");         //8
            tablaPagos.Columns.Add("comprobante");       //9
            tablaPagos.Columns.Add("codigo");            //10
            tablaPagos.Columns.Add("monto");             //11
            tablaPagos.Columns.Add("saldo");             //12
            tablaPagos.Columns.Add("status");            //13
            tablaPagos.Columns.Add("fechaPago");         //14
            tablaPagos.Columns.Add("idBanco");           //15
            tablaPagos.Columns.Add("correlativo");       //16
            tablaPagos.Columns.Add("pago");              //17
            tablaPagos.Columns.Add("fechaPago2");        //18
            tablaPagos.Columns.Add("usuario");           //19

        }

        private DataTable contenidoDataTable()
        {
            armarDataTable();

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {

                if (Convert.ToBoolean(dataGridView1.Rows[i].Cells[17].Value) == true)
                {
                    DataRow fila = tablaPagos.NewRow();

                    fila[0] = dataGridView1.Rows[i].Cells[0].Value.ToString();
                    fila[1] = dataGridView1.Rows[i].Cells[1].Value.ToString();
                    fila[2] = dataGridView1.Rows[i].Cells[2].Value.ToString();
                    fila[3] = dataGridView1.Rows[i].Cells[3].Value.ToString();
                    fila[4] = dataGridView1.Rows[i].Cells[4].Value.ToString();
                    fila[5] = dataGridView1.Rows[i].Cells[5].Value.ToString();
                    fila[6] = dataGridView1.Rows[i].Cells[6].Value.ToString();
                    fila[7] = dataGridView1.Rows[i].Cells[7].Value.ToString();
                    fila[8] = dataGridView1.Rows[i].Cells[8].Value.ToString();
                    fila[9] = dataGridView1.Rows[i].Cells[9].Value.ToString();
                    fila[10] = dataGridView1.Rows[i].Cells[10].Value.ToString();
                    fila[11] = dataGridView1.Rows[i].Cells[11].Value.ToString().Replace(",", ".");
                    fila[12] = dataGridView1.Rows[i].Cells[12].Value.ToString();
                    fila[13] = dataGridView1.Rows[i].Cells[13].Value.ToString();
                    fila[14] = dataGridView1.Rows[i].Cells[14].Value.ToString();
                    fila[15] = dataGridView1.Rows[i].Cells[15].Value.ToString();
                    fila[16] = dataGridView1.Rows[i].Cells[16].Value.ToString();
                    fila[17] = dataGridView1.Rows[i].Cells[17].Value.ToString();
                    fila[18] = "" + DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day;
                    fila[19] = Principal.usuarioActual.Id;

                    tablaPagos.Rows.Add(fila);
                }
            }
            return tablaPagos;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void registrarPago() {

            for (int i = 0; i < tablaPagos.Rows.Count; i++)
            {
                tabla.Clear();
                tabla.Reset();

                tabla = pago.docExistePagoTemp(tablaPagos, i);

                if (tabla.Rows.Count > 0)
                {
                    pago.actualizarPagosTemp(tablaPagos, i);
                    //pago.actualizarSaldo(tablaPagos);
                }
                else
                {
                    pago.guardarDocuApagar(tablaPagos, i);
                }
                pago.actualizarProcesadoSalpro(tablaPagos, i);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            pago.limpiarPagosTemp();
            cargarDatos();
        }

        private void button1_Click(object sender, EventArgs e)
        {


            //FUNCIONA LINQ
            //var results = from h in tabla.AsEnumerable()
            //              where h.Field<string>("IdProveedor")=="08908012"
            //              select h.Field<decimal>("MontoDocum")
                        
                          
            //              ;

            //var results = from h in tabla.AsEnumerable()
            //              where h.Field<string>("IdProveedor") == "08908012" && h.Field<decimal>("MontoDocum")==3000
            //              group h by h.Field<string>("IdProveedor") into pro
            //              select new
            //              {
            //                  sumita = pro.Sum(r => r.Field<decimal>("MontoDocum"))
            //              };
            //MessageBox.Show("" + results);

            //FUNCIONA LINQ
            double resul = dataGridView1.Rows.Cast<DataGridViewRow>().Sum(x => x.Cells[10].Value.ToString().Equals("08908012") && 
                Convert.ToDecimal(x.Cells[11].Value) == 3000 ? Convert.ToDouble(x.Cells[11].Value) : 0); 

            MessageBox.Show("" + Convert.ToString(resul));
            // 02-10-2013 124.006.144,97

        }

        private void dataGridView1_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (this.dataGridView1.IsCurrentCellDirty)
            {
                this.dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Convert.ToBoolean(dataGridView1.CurrentRow.Cells[17].Value) == true)
            {
                dataGridView1.CurrentRow.Cells[18].Value = "" + DateTime.Now.Day.ToString().PadLeft(2, '0') + "-" + DateTime.Now.Month.ToString().PadLeft(2, '0') + "-" + DateTime.Now.Year;
            }
            else
            {
                dataGridView1.CurrentRow.Cells[18].Value = "";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //System.Diagnostics.Process.Start("calc");
        }






















    }
}
