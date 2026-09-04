using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using lebrun.clases.importaciones;
using lebrun.formularios.importaciones;
using lebrun.Reportes;

namespace lebrun.formularios.importaciones
{
    public partial class lbxImportaciones : Form
    {

        private Importacion importacion;
        private DataTable tablaImportaciones;
        private DataTable tablaImportaciones2;
        private frmImportaciones principalImportaciones;
        private CrystalReport1 rptImportacion;
        private frmReporte reporteImpor;
        private int opc;
        private Principal Principal;

        public lbxImportaciones()
        {
            InitializeComponent();
            importacion = new Importacion();

        }

        private void lbxImportaciones_Load(object sender, EventArgs e)
        {
            Principal = (Principal)this.MdiParent;
            dataGridView1.DataSource=null;
            tablaImportaciones = importacion.CargarImportaciones();
            tablaImportaciones2 = importacion.Cargarimportaciones2();

            tablaImportaciones.Merge(tablaImportaciones2);

            if (tablaImportaciones.Rows.Count >= 1)
            {
                dataGridView1.DataSource = tablaImportaciones;

                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
               //dataGridView1.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //principalImportaciones = new frmImportaciones();
            //principalImportaciones.armarTodo(dataGridView1.CurrentRow.Cells[0].ToString(),dataGridView1.CurrentRow.Cells[1].ToString());
            //principalImportaciones.Show();

        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            //principalImportaciones = new frmImportaciones();

            if (dataGridView1.CurrentRow.Cells[6].Value.Equals("En Carga"))
            {
                //aqui verificar cargar una sola vez 

                principalImportaciones = frmImportaciones.DefInstance;

                if (principalImportaciones.Visible == false)
                {
                    principalImportaciones.armarTodo(dataGridView1.CurrentRow.Cells[0].Value.ToString(), dataGridView1.CurrentRow.Cells[1].Value.ToString(), 2, dataGridView1.CurrentRow.Cells[2].Value.ToString());
                    principalImportaciones.btnAceptar.Enabled = false;
                    principalImportaciones.btnGuardar.Enabled = false;
                    principalImportaciones.btnImportar.Enabled = false;
                    principalImportaciones.btnConfirmar.Enabled = false;
                    principalImportaciones.Show();
                }
            }

            if (dataGridView1.CurrentRow.Cells[6].Value.Equals("Liquidada"))
            {
                //aqui verificar cargar una sola vez 

                principalImportaciones = frmImportaciones.DefInstance;

                if (principalImportaciones.Visible == false)
                {
                    principalImportaciones.armarTodo(dataGridView1.CurrentRow.Cells[0].Value.ToString(), dataGridView1.CurrentRow.Cells[1].Value.ToString(), 1, dataGridView1.CurrentRow.Cells[2].Value.ToString());
                    principalImportaciones.btnAceptar.Enabled = false;
                    principalImportaciones.btnGuardar.Enabled = false;
                    principalImportaciones.btnImportar.Enabled = false;
                    principalImportaciones.btnConfirmar.Enabled = false;

                    principalImportaciones.Show();
                }
            }
                
           // this.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            principalImportaciones = frmImportaciones.DefInstance;
            if (principalImportaciones.Visible == false)
            {
                principalImportaciones.modificarUsu(Principal.usuarioActual.Id);
                principalImportaciones.referenciaRefrescar(this);
                principalImportaciones.Show();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {

                if (dataGridView1.CurrentRow.Cells[6].Value.ToString().Equals("En Carga"))
                {
                    if (MessageBox.Show("¿Seguro que desea eliminar este registro?", "Alerta ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        importacion.modificarImportaciones(dataGridView1.CurrentRow.Cells[0].Value.ToString(), dataGridView1.CurrentRow.Cells[1].Value.ToString(), dataGridView1.CurrentRow.Cells[2].Value.ToString());
                       
                        //dataGridView1.DataSource = null;
                        //tablaImportaciones = importacion.CargarImportaciones();
                        //tablaImportaciones2 = importacion.Cargarimportaciones2();

                        //tablaImportaciones.Merge(tablaImportaciones2);
                        //dataGridView1.DataSource = tablaImportaciones;

                        lbxImportaciones_Activated(sender, e);
                    }
                }
                else
                {
                    MessageBox.Show("Solo se pueden eliminar importaciones en carga", "Atención ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else { MessageBox.Show("No existe registro para eliminar", "Atención ", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        }

        private void lbxImportaciones_Activated(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            tablaImportaciones = importacion.CargarImportaciones();
            tablaImportaciones2 = importacion.Cargarimportaciones2();

            tablaImportaciones.Merge(tablaImportaciones2);
            dataGridView1.DataSource = tablaImportaciones;
        }

        public void btnBuscar_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = importacion.buscarImportaciones(textBox1.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow.Cells[6].Value.ToString().Equals("En Carga"))
            {
                opc = 2;
            }
            else
            {
                opc = 1;
            }

            reporteImpor = new frmReporte(dataGridView1.CurrentRow.Cells[0].Value.ToString(), dataGridView1.CurrentRow.Cells[1].Value.ToString(), dataGridView1.CurrentRow.Cells[2].Value.ToString(), opc, "importacion");
            reporteImpor.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow.Cells[6].Value.Equals("En Carga"))
            {
                //aqui verificar cargar una sola vez 

                principalImportaciones = frmImportaciones.DefInstance;

                if (principalImportaciones.Visible == false)
                {
                    principalImportaciones.modificarUsu(Principal.usuarioActual.Id);
                    principalImportaciones.armarTodo(dataGridView1.CurrentRow.Cells[0].Value.ToString(), dataGridView1.CurrentRow.Cells[1].Value.ToString(), 2, dataGridView1.CurrentRow.Cells[2].Value.ToString());
                    principalImportaciones.referenciaRefrescar(this);
                    principalImportaciones.Show();
                }
            }

            if (dataGridView1.CurrentRow.Cells[6].Value.Equals("Liquidada"))
            {
                //aqui verificar cargar una sola vez 
                MessageBox.Show("Solo se pueden modificar importaciones en carga", "Atención ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //principalImportaciones = frmImportaciones.DefInstance;

                //if (principalImportaciones.Visible == false)
                //{
                //    principalImportaciones.armarTodo(dataGridView1.CurrentRow.Cells[0].Value.ToString(), dataGridView1.CurrentRow.Cells[1].Value.ToString(), 1, dataGridView1.CurrentRow.Cells[2].Value.ToString());
                //    principalImportaciones.btnAceptar.Enabled = false;
                //    principalImportaciones.btnGuardar.Enabled = false;
                //    principalImportaciones.btnImportar.Enabled = false;
                //    principalImportaciones.btnConfirmar.Enabled = false;

                //    principalImportaciones.Show();
                //}
            }
        }

        private void imprimirReporte()
        {
            //rptImportacion = new CrystalReport1();
            //DataSet dsReporteImportacion;
            //dsReporteImportacion = new DataSet();
            //dsReporteImportacion.Tables.Add(importacion.imprimirReporte(facturaOrigenPanel1.txtDocumento.Text, facturaOrigenPanel1.txtSufijoDoc.Text,productosPanel.txtProveedor.Text));
            //rptImportacion.SetDataSource(dsReporteImportacion);
            //rptImportacion.SetDataSource(importacion.imprimirReporte(dataGridView1.CurrentRow.Cells[0].Value.ToString(), dataGridView1.CurrentRow.Cells[1].Value.ToString(), dataGridView1.CurrentRow.Cells[2].Value.ToString(),opc));
            //this.crystalReportViewer1.ReportSource = rptImportacion;
            //crystalReportViewer1.Visible = true;

        }

        private void btnProyecciones_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow.Cells[6].Value.Equals("Liquidada"))
            {
                //aqui verificar cargar una sola vez 

                principalImportaciones = frmImportaciones.DefInstance;

                if (principalImportaciones.Visible == false)
                {
                    principalImportaciones.armarTodo(dataGridView1.CurrentRow.Cells[0].Value.ToString(), dataGridView1.CurrentRow.Cells[1].Value.ToString(), 1, dataGridView1.CurrentRow.Cells[2].Value.ToString());
                    principalImportaciones.btnAceptar.Enabled = false;
                    principalImportaciones.btnGuardar.Enabled = false;
                    principalImportaciones.btnImportar.Enabled = false;
                    principalImportaciones.btnConfirmar.Enabled = false;
                    principalImportaciones.btnFacOrigen.Enabled = false;
                    principalImportaciones.btnOtros.Enabled = false;
                    principalImportaciones.btnMsj.Enabled = false;
                    principalImportaciones.btnProductos.Enabled = false;
                    principalImportaciones.btnFacNacional.Enabled = false;
                    principalImportaciones.btnOtrosDatos.Enabled = false;
                    principalImportaciones.button1.Visible = true;
                    principalImportaciones.paraProyeccion();
                    principalImportaciones.Show();

                }
            }
            else {

                MessageBox.Show("Debe seleccionar solo documentos liquidados", "Atención ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void refrescar(object sender, EventArgs e)
        {
            btnBuscar_Click(sender, e);
        
        }





    }
}
