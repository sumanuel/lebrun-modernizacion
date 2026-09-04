using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using lebrun.clases.compras;
using lebrun.Reportes.compras;


namespace lebrun.formularios.compras
{

    public partial class lbxProveedoresPago : Form
    {
        private DataTable tabla;
        private DataTable tabla2;
        private Pagos pago;
        private frmProcesoPago procesoPago;
        private Principal Principal2;
        private frmContendorRptCompras reporte;

        public lbxProveedoresPago()
        {
            InitializeComponent();
            pago = new Pagos();
            tabla = new DataTable();
            tabla2 = new DataTable();
        }

        private void armarLbx()
        {
            dataGridView1.DataSource = null;

            tabla = pago.cargarLbxPago();

            if (tabla.Rows.Count > 0)
            {
                dataGridView1.DataSource = tabla;
                dataGridView1.Columns[0].Width = 180;
                dataGridView1.Columns[1].Width = 440;


                dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView1.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dataGridView1.AllowUserToResizeColumns = false;
                dataGridView1.AllowUserToResizeRows = false;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void lbxProveedoresPago_Load(object sender, EventArgs e)
        {
            Principal2 = (Principal)this.MdiParent;
            armarLbx();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            procesoPago = new frmProcesoPago(dataGridView1.CurrentRow.Cells[0].Value.ToString(), dataGridView1.CurrentRow.Cells[1].Value.ToString(), Principal2.usuarioActual.Id);
            procesoPago.Show();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("En construccion", "    Advertencia    ", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtNumeroComprobante.Text != "")
            {
                if (txtNumeroComprobante.Text.Substring(0, 1) == "5" && txtNumeroComprobante.Text.Length == 10)
                {
                    tabla2 = pago.reimpresionVoucherPago(txtNumeroComprobante.Text);

                    if (tabla2.Rows.Count > 0)
                    {
                        reporte = new frmContendorRptCompras(tabla2, "voucher");
                        reporte.Show();
                    }
                    else
                    {
                        MessageBox.Show("No hay datos para reimprimir este voucher, verifique el numero", "    Advertencia    ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("No es un voucher", "    Advertencia    ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }


    }
}
