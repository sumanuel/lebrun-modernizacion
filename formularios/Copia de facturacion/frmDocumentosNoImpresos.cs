using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using lebrun.clasesData;
namespace lebrun.formularios.facturacion
{
    public partial class frmDocumentosNoImpresos : Form
    {
        string numeroFact = null;
        private string sentenciaSql = null;
        DataTable dtFac;
        ConexionBD databaseConection;
        Principal referenciaPrincipal;

        public frmDocumentosNoImpresos()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            numeroFact = null;
            numeroFact = txtFactura.Text;
            sentenciaSql = "SELECT dcli_numero,dcli_numfis,dcli_saldo, dcli_impreso, dcli_tipdoc  FROM admdoccli WHERE dcli_numero = '$1'  AND (dcli_tipdoc='FAV' OR dcli_tipdoc='DEV')";
            sentenciaSql = sentenciaSql.Replace("$1", numeroFact);
            dtFac = databaseConection.fDataTable(sentenciaSql);
            if (dtFac.Rows.Count > 0)
            {
                dataGridView1.DataSource = dtFac;
                dataGridView1.Columns[0].HeaderText = "Numero Interno";
                dataGridView1.Columns[1].HeaderText = "Numero Fiscal";
                dataGridView1.Columns[2].HeaderText = "Monto";
                dataGridView1.Columns[3].HeaderText = "Impreso";
            }
            else
            {
                MessageBox.Show("No se consiguieron Resultados!!!!");
                txtFactura.Clear();
                dataGridView1.DataSource = null;
                numeroFact = null;
                sentenciaSql = null;
                dtFac = null;
            }
            databaseConection.cerrarConexion();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           databaseConection = new ConexionBD();
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            referenciaPrincipal = (Principal)this.MdiParent;

            if (this.dataGridView1.Rows.Count > 0)
            {
                sentenciaSql = "UPDATE admdoccli SET dcli_impreso='0' WHERE  dcli_numero='$1'";
                sentenciaSql = sentenciaSql.Replace("$1", this.dataGridView1.CurrentRow.Cells[0].Value.ToString());
                databaseConection.ejecutarInsert(sentenciaSql);
                sentenciaSql = null;
                sentenciaSql = "INSERT INTO admdocunoimp (usuario,numDocumento,fecha,hora,tipoDoc) VALUES('$usuario','$numDocumento','$fecha','$hora','$tipoDoc');";
                sentenciaSql = sentenciaSql.Replace("$usuario", referenciaPrincipal.usuarioActual.Id);
                sentenciaSql = sentenciaSql.Replace("$numDocumento", this.dataGridView1.CurrentRow.Cells[0].Value.ToString());
                sentenciaSql = sentenciaSql.Replace("$fecha", "" + DateTime.Now.Year + "-" + DateTime.Now.Month.ToString().PadLeft(2, '0') + "-" + DateTime.Now.Day.ToString().PadLeft(2, '0'));
                sentenciaSql = sentenciaSql.Replace("$hora", DateTime.Now.ToString("hh:mm:ss"));
                sentenciaSql = sentenciaSql.Replace("$tipoDoc", this.dataGridView1.CurrentRow.Cells[4].Value.ToString());
                databaseConection.ejecutarInsert(sentenciaSql);
                MessageBox.Show("Documento Actualizado");
                databaseConection.cerrarConexion();
                txtFactura.Clear();
                dataGridView1.DataSource = null;
                numeroFact = null;
                sentenciaSql = null;
                dtFac = null;
            }
        }

        private void txtFactura_KeyPress(object sender, KeyPressEventArgs e)
        {
            if((txtFactura.Text!="") && ((e.KeyChar == (char)(Keys.Enter) ))){
                txtFactura.Text = txtFactura.Text.PadLeft(12, '0');
                button4_Click(new object(), new EventArgs());
            }
        }
       
    }
}
