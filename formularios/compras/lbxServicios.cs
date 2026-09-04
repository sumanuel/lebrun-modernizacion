using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using lebrun.clases.compras;
using lebrun.formularios.compras;

namespace lebrun.formularios.compras
{
    public partial class lbxServicios : Form
    {
        private Servicios serv;
        private frmServicios formServicio;
        private frmOpcionesServicios opcionesServicios;
        private Principal Principal2;
        private string StrSql = "";
        private int opcion;
        private retencionISLR retISLR;

        public lbxServicios()
        {
            InitializeComponent();
            serv = new Servicios();
            retISLR = new retencionISLR();
        }

        private void lbxServicios_Load(object sender, EventArgs e)
        {
            //dataGridView1.DataSource = serv.armarLbxServicios("");
            filtrarPorCheck();
            Principal2 = (Principal)this.MdiParent;
            retISLR = new retencionISLR();
        }

        private void filtrarPorCheck()
        {
            StrSql = "";

            if (checkCarga.Checked)
            {
                StrSql = StrSql + " and (doc_status='1' ";
            }
            if (checkTransito.Checked)
            {
                if (checkCarga.Checked)
                {
                    StrSql = StrSql + "or ";
                }
                else
                {
                    StrSql = StrSql + "and(  ";//and
                }
                StrSql = StrSql + "doc_status='2' ";
            }

            if (checkLiquidada.Checked)
            {
                if (checkCarga.Checked || checkTransito.Checked)
                {
                    StrSql = StrSql + "or ";
                }
                else
                {
                    StrSql = StrSql + "and ( ";
                }
                StrSql = StrSql + "doc_status='4' ";
            }
            if (checkAnuladas.Checked)
            {
                if (checkCarga.Checked || checkTransito.Checked || checkLiquidada.Checked)
                {
                    StrSql = StrSql + "or ";
                }
                else
                {
                    StrSql = StrSql + "and  (";
                }
                StrSql = StrSql + "doc_status='0' ";
            }

            if (checkCarga.Checked || checkTransito.Checked || checkLiquidada.Checked || checkAnuladas.Checked)
            {
                StrSql = StrSql + ")";
            }

            dataGridView1.DataSource = serv.armarLbxServicios(StrSql);

            dataGridView1.Columns[2].Visible = false;
            dataGridView1.Columns[9].Visible = false;

            dataGridView1.Columns[0].Width = 100;
            dataGridView1.Columns[1].Width = 50;
            dataGridView1.Columns[2].Width = 50;
            dataGridView1.Columns[3].Width = 80;
            dataGridView1.Columns[4].Width = 210;
            dataGridView1.Columns[5].Width = 80;
            dataGridView1.Columns[6].Width = 80;
            dataGridView1.Columns[7].Width = 70;
            dataGridView1.Columns[8].Width = 100;
            dataGridView1.Columns[9].Width = 50;
            dataGridView1.Columns[10].Width = 50;

            dataGridView1.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dataGridView1.AllowUserToResizeColumns = false;
            dataGridView1.AllowUserToResizeRows = false;

            cambiarStatus();
        }

        private void checkCarga_CheckedChanged(object sender, EventArgs e)
        {
            filtrarPorCheck();
        }

        private void checkTransito_CheckedChanged(object sender, EventArgs e)
        {
            filtrarPorCheck();
        }

        private void checkLiquidada_CheckedChanged(object sender, EventArgs e)
        {
            filtrarPorCheck();
        }

        private void checkAnuladas_CheckedChanged(object sender, EventArgs e)
        {
            filtrarPorCheck();
        }

        private void btnCargar_Click(object sender, EventArgs e)
        {
            formServicio = new frmServicios();
            formServicio.obtenerUsuario(Principal2.usuarioActual.Id);
            formServicio.refrescarLbx(this);
            formServicio.Show();

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            busqueda();
        }

        private void busqueda()
        {

            StrSql = "";

            if (radioButton1.Checked)
            {
                StrSql = StrSql + " And doc_numerodoc like '%" + txtBusqueda.Text + "%' ";
            }

            if (radioButton2.Checked)
            {
                StrSql = StrSql + " And doc_codigo like '" + txtBusqueda.Text + "%' ";
            }

            if (radioButton3.Checked)
            {
                StrSql = StrSql + " And doc_fechEmis between " + Convert.ToDateTime(dateTimeDesde.Text).ToString("yyyy-MM-dd") + " AND  " + Convert.ToDateTime(dateTimeHasta.Text).ToString("yyyy-MM-dd") + " ";
            }


            if (checkCarga.Checked)
            {
                StrSql = StrSql + " and (doc_status='1' ";
            }
            if (checkTransito.Checked)
            {
                if (checkCarga.Checked)
                {
                    StrSql = StrSql + "or ";
                }
                else
                {
                    StrSql = StrSql + "and ( ";
                }
                StrSql = StrSql + "doc_status='2' ";
            }

            if (checkLiquidada.Checked)
            {
                if (checkCarga.Checked || checkTransito.Checked)
                {
                    StrSql = StrSql + "or ";
                }
                else
                {
                    StrSql = StrSql + "and ( ";
                }
                StrSql = StrSql + "doc_status='4' ";
            }
            if (checkAnuladas.Checked)
            {
                if (checkCarga.Checked || checkTransito.Checked || checkLiquidada.Checked)
                {
                    StrSql = StrSql + "or ";
                }
                else
                {
                    StrSql = StrSql + "and ( ";
                }
                StrSql = StrSql + "doc_status='0' ";
            }

            if (checkCarga.Checked || checkTransito.Checked || checkLiquidada.Checked || checkAnuladas.Checked)
            {
                StrSql = StrSql + ")";
            }

            //StrSql = StrSql + " order by doc_fechEmis desc,doc_hora desc limit 200";

            dataGridView1.DataSource = serv.armarLbxServicios(StrSql);

            dataGridView1.Columns[2].Visible = false;
            dataGridView1.Columns[9].Visible = false;

            dataGridView1.Columns[0].Width = 100;
            dataGridView1.Columns[1].Width = 50;
            dataGridView1.Columns[2].Width = 50;
            dataGridView1.Columns[3].Width = 80;
            dataGridView1.Columns[4].Width = 210;
            dataGridView1.Columns[5].Width = 80;
            dataGridView1.Columns[6].Width = 80;
            dataGridView1.Columns[7].Width = 70;
            dataGridView1.Columns[8].Width = 100;
            dataGridView1.Columns[9].Width = 50;
            dataGridView1.Columns[10].Width = 50;

            dataGridView1.AllowUserToResizeColumns = false;
            dataGridView1.AllowUserToResizeRows = false;

            cambiarStatus();

        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
            {
                lblDesde.Visible = true;
                lblHasta.Visible = true;
                dateTimeDesde.Visible = true;
                dateTimeHasta.Visible = true;
                txtBusqueda.Visible = false;
            }
            else
            {
                lblDesde.Visible = false;
                lblHasta.Visible = false;
                dateTimeDesde.Visible = false;
                dateTimeHasta.Visible = false;
                txtBusqueda.Visible = true;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
            {
                lblDesde.Visible = true;
                lblHasta.Visible = true;
                dateTimeDesde.Visible = true;
                dateTimeHasta.Visible = true;
                txtBusqueda.Visible = false;
            }
            else
            {
                lblDesde.Visible = false;
                lblHasta.Visible = false;
                dateTimeDesde.Visible = false;
                dateTimeHasta.Visible = false;
                txtBusqueda.Visible = true;
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
            {
                lblDesde.Visible = true;
                lblHasta.Visible = true;
                dateTimeDesde.Visible = true;
                dateTimeHasta.Visible = true;
                txtBusqueda.Visible = false;
            }
            else
            {
                lblDesde.Visible = false;
                lblHasta.Visible = false;
                dateTimeDesde.Visible = false;
                dateTimeHasta.Visible = false;
                txtBusqueda.Visible = true;
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow.Cells[7].Value.ToString().Equals("En Carga"))
            {
                formServicio = new frmServicios(dataGridView1.CurrentRow.Cells[0].Value.ToString(), dataGridView1.CurrentRow.Cells[1].Value.ToString(), dataGridView1.CurrentRow.Cells[3].Value.ToString(), dataGridView1.CurrentRow.Cells[10].Value.ToString(), 2, 2);
                formServicio.obtenerUsuario(Principal2.usuarioActual.Id);
                formServicio.refrescarLbx(this);
                formServicio.Show();
                
            }
            else
            {
                MessageBox.Show("Solo se pueden modificar documentos en carga ", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cambiarStatus()
        {
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                if (dataGridView1.Rows[i].Cells[7].Value.ToString().Equals("1"))
                {
                    dataGridView1.Rows[i].Cells[7].Value = "En Carga";
                }
                if (dataGridView1.Rows[i].Cells[7].Value.ToString().Equals("2"))
                {
                    dataGridView1.Rows[i].Cells[7].Value = "En Transito";
                }
                if (dataGridView1.Rows[i].Cells[7].Value.ToString().Equals("3"))
                {
                    dataGridView1.Rows[i].Cells[7].Value = "Ent. parcial";
                }
                if (dataGridView1.Rows[i].Cells[7].Value.ToString().Equals("4"))
                {
                    dataGridView1.Rows[i].Cells[7].Value = "Liquidada";
                }
                if (dataGridView1.Rows[i].Cells[7].Value.ToString().Equals("0"))
                {
                    dataGridView1.Rows[i].Cells[7].Value = "Anulada";
                }
            }

        }

        private void btnVer_Click(object sender, EventArgs e)
        {

            if (Convert.ToDateTime(dataGridView1.CurrentRow.Cells[5].Value.ToString()).Year == DateTime.Now.Year)
            {

                if (dataGridView1.CurrentRow.Cells[7].Value.ToString().Equals("Liquidada"))
                {

                    if (!retISLR.verificarExistenciaRet(dataGridView1.CurrentRow.Cells[0].Value.ToString(), dataGridView1.CurrentRow.Cells[1].Value.ToString(), dataGridView1.CurrentRow.Cells[10].Value.ToString(), dataGridView1.CurrentRow.Cells[3].Value.ToString()))
                    {

                        if (dataGridView1.CurrentRow.Cells[7].Value.ToString().Equals("En Carga"))
                        {
                            opcion = 2;
                        }
                        if (dataGridView1.CurrentRow.Cells[7].Value.ToString().Equals("Liquidada"))
                        {
                            opcion = 1;
                        }

                        formServicio = new frmServicios(
                            dataGridView1.CurrentRow.Cells[0].Value.ToString(),
                            dataGridView1.CurrentRow.Cells[1].Value.ToString(),
                            dataGridView1.CurrentRow.Cells[3].Value.ToString(),
                            dataGridView1.CurrentRow.Cells[10].Value.ToString(), 3, opcion);
                        formServicio.obtenerUsuario(Principal2.usuarioActual.Id);
                        formServicio.refrescarLbx(this);
                        formServicio.Show();


                    }
                    else
                    {
                        MessageBox.Show("Este documento ya tiene una retencion asignada", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
                else
                {
                    MessageBox.Show("Solo se pueden utilizar documentos liquidados ", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("No se puede realizar una retencion de periodos fiscal diferente", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void refrescar()
        {
            filtrarPorCheck();
        }

        private void btnOpciones_Click(object sender, EventArgs e)
        {
            opcionesServicios = new frmOpcionesServicios(
                dataGridView1.CurrentRow.Cells[3].Value.ToString(), 
                dataGridView1.CurrentRow.Cells[0].Value.ToString(), 
                dataGridView1.CurrentRow.Cells[1].Value.ToString(), 
                dataGridView1.CurrentRow.Cells[10].Value.ToString(), 
                dataGridView1.CurrentRow.Cells[4].Value.ToString(), 
                Principal2.usuarioActual.Id);
            opcionesServicios.manejandoLbx(this);
            opcionesServicios.Show();
            this.Visible = false;
        }















    }



}
