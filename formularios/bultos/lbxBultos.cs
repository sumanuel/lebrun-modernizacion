using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using lebrun.clases.facturacion;
using lebrun.clases.clientes;
using lebrun.clases.vendedores;
using lebrun.clasesData;
using lebrun.clases.bulto;

namespace lebrun.formularios.bultos
{
    public partial class lbxBultos : Form
    {
        private Factura prefactura;
        private Vendedor vende;
        private Clientes client;
        private Principal referenciaPrincipal;
        private FuncionesTexbox funtext;
        private DataGridViewRow filaEscojida;
        public lbxBulto2 referencialbxBultos;
        private Bulto bult;

        public lbxBultos()
        {
            InitializeComponent();
        }

        private void lbxBultos_Load(object sender, EventArgs e)
        {
            referenciaPrincipal = (Principal)this.MdiParent;
            prefactura = new Factura(200, "PRE", referenciaPrincipal.usuarioActual.IpPc);
            vende = new Vendedor();
            client = new Clientes();
            funtext = new FuncionesTexbox();
            filaEscojida = null;
            bult = new Bulto();
        }

        public void cargarPrefactura(string numeroPrefact)
        {
            prefactura.cargarPrefactura(numeroPrefact, client, vende);
            
            client.cargarDatosCliente();
        }

        public void asignarDatos()
        {
            txtCorrelativo.Text = prefactura.CorrelativoInterno;
            txtCodCliente.Text = client.Codigo;
            txtNombreCli.Text = client.Nombre;
            this.dataGridView1.DataSource = prefactura.itemsPre();
            foreach(DataGridViewColumn y in this.dataGridView1.Columns )
            {
                y.Visible = false;
            }
            
            foreach (DataGridViewColumn x in this.dataGridView1.Columns)
            {
                if ((x.Name.Equals("@mov_codigo")) || (x.Name.Equals("colProducto")) ||
                    (x.Name.Equals("@mov_undmed")) || (x.Name.Equals("@mov_cant")))
                {
                    x.Visible = true;
                }
            }

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (String.IsNullOrEmpty(textBox1.Text)) 
            {
                MessageBox.Show("Debe ingresar primero la cantidad de bultos", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                label5.Text = dataGridView1.CurrentRow.Cells["@mov_codigo"].Value.ToString();
                filaEscojida = dataGridView1.CurrentRow;
                textBox2.Focus();
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            funtext.OnlyNumbers(sender, e);

            if ((e.KeyChar == (char)(Keys.Enter)) && (!(String.IsNullOrEmpty(textBox1.Text))))
            {
                e.Handled = true; SendKeys.Send("{TAB}");
            }
            
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            funtext.conDecimalesDefinidos(sender, e, this.textBox4, 3);
            if ((e.KeyChar == (char)(Keys.Enter)) && (!(String.IsNullOrEmpty(textBox4.Text))))
            {
                e.Handled = true; button1_Click(new object(), new EventArgs());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!(String.IsNullOrEmpty(textBox1.Text)))
            {
                this.dataGridView1.Enabled = true;
                MessageBox.Show("Proceda a cargar los bultos", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                label9.Text = "";
                label9.Text = "de ";
                label9.Text = label9.Text + " " + textBox1.Text;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            funtext.OnlyNumbers(sender, e);

            if ((e.KeyChar == (char)(Keys.Enter)) && (!(String.IsNullOrEmpty(textBox2.Text))))
            {
                e.Handled = true; SendKeys.Send("{TAB}");
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            funtext.conDecimalesDefinidos(sender, e, this.textBox3, 3);
            if ((e.KeyChar == (char)(Keys.Enter)) )
            {
                e.Handled = true; button2_Click(new object(), new EventArgs());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (label5.Text.Equals("."))
            {
                MessageBox.Show("Debe escoger un item de la prefactura", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (String.IsNullOrEmpty(textBox2.Text))
                {
                    MessageBox.Show("Debe ingresar el número de bulto", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox2.Focus();
                }
                else
                {
                    if (validarNumBulto())
                    {
                        dataGridView2.Rows.Add();
                        DataGridViewRow filaNueva = dataGridView2.Rows[dataGridView2.Rows.Count - 1];
                        filaNueva.Cells["codigo"].Value = filaEscojida.Cells["@mov_codigo"].Value.ToString();
                        filaNueva.Cells["producto"].Value = filaEscojida.Cells["colProducto"].Value.ToString();
                        filaNueva.Cells["bulto"].Value = Convert.ToInt32(textBox2.Text).ToString();
                        filaNueva.Cells["peso"].Value = (string.IsNullOrEmpty(textBox3.Text)||textBox3.Text=="")?"0":Convert.ToDecimal(textBox3.Text).ToString();
                        filaNueva.Cells["item"].Value = (dataGridView2.Rows.Count).ToString();
                        filaNueva.Cells["codigoProd"].Value = filaEscojida.Cells["@mov_codigo"].Value.ToString();
                        limpiarBultoPeso();
                    }
                    else
                    {
                        textBox2.Focus();
                    }
                }
            }
        }

        public void limpiarBultoPeso()
        {
            label5.Text = ".";
            textBox2.Clear();
            textBox3.Clear();
        }

        public bool validarNumBulto()
        {
            bool centinela = true;
            if (Convert.ToInt32(textBox2.Text) > (Convert.ToInt32(textBox1.Text)))
            {
                MessageBox.Show("El numero de bulto Indicado no Existe", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                centinela = false;
            }

            return centinela;
        }

        private void cmdSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdConfirmar_Click(object sender, EventArgs e)
        {
            int respuesta = 0;
            if (dataGridView2.Rows.Count > 0)
            {

                respuesta = bult.actualizarCantidadPeso(textBox1.Text, String.IsNullOrEmpty(textBox4.Text) ? "" : textBox4.Text, txtCorrelativo.Text);
                if (respuesta == -1)
                {
                    bult.borrarDetalleBulto(txtCorrelativo.Text);
                    bult.guardarDetalleBulto(this.dataGridView2, txtCorrelativo.Text);
                    MessageBox.Show("Bulto guardado exitosamente", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    referencialbxBultos.cmdLimpiar_Click(new object(), new EventArgs());
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Error al guardar bulto. Intente Nuevamente", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Debe agregar minimo 1 bulto", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox2.Focus();
            }
        }

        public void pesoBulto()
        {
            textBox1.Text = prefactura.bultos;
            textBox4.Text = prefactura.peso;
            label9.Text = label9.Text +" "+ prefactura.bultos;
        }

        public void cargarDetalle(string numeroPre)
        {
            DataTable datos = bult.detalleBulto(numeroPre);
            int indice = 0;
            foreach (DataRow fila in datos.Rows)
            {
                dataGridView2.Rows.Add();
                indice = dataGridView2.Rows.Count - 1;
                dataGridView2.Rows[indice].Cells["codigo"].Value = fila["codigoProd"].ToString();
                dataGridView2.Rows[indice].Cells["producto"].Value = fila["inv_descri"].ToString();
                dataGridView2.Rows[indice].Cells["bulto"].Value = fila["bulto"].ToString();
                dataGridView2.Rows[indice].Cells["peso"].Value = fila["peso"].ToString();
                dataGridView2.Rows[indice].Cells["item"].Value = fila["item"].ToString();
                dataGridView2.Rows[indice].Cells["codigoProd"].Value = fila["codigoProd"].ToString(); ;
            }
        }

        private void cmdCierreDefinitivo_Click(object sender, EventArgs e)
        {
            int respuesta = 0;
            if (dataGridView2.Rows.Count > 0)
            {
                
                respuesta = bult.actualizarCantidadPeso(textBox1.Text, String.IsNullOrEmpty(textBox4.Text) ? "" : textBox4.Text, txtCorrelativo.Text);
                if (respuesta == -1)
                {
                    bult.borrarDetalleBulto(txtCorrelativo.Text);
                    bult.guardarDetalleBulto(this.dataGridView2, txtCorrelativo.Text);
                    if (bultoListo())
                    {
                        respuesta = bult.updateStatus("2", txtCorrelativo.Text);
                        if (respuesta == -1)
                        {
                            MessageBox.Show("Bulto Cerrado exitosamente", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            referencialbxBultos.cmdLimpiar_Click(new object(), new EventArgs());
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Error al cerrar bulto el programa se cerrara", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            this.Close();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Debe cargar todos los bultos", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    
                    referencialbxBultos.cmdLimpiar_Click(new object(), new EventArgs());
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Error al guardar bulto. Intente Nuevamente", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Debe agregar minimo 1 bulto", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox2.Focus();
            }
        }


        public bool bultoListo()
        {
            bool centinela = false;
            bool retorno = true;
            List<string> listBultos = new List<string>();
            int catidadBultos = Convert.ToInt32(textBox1.Text.ToString());
            DataTable dt = new DataTable();
            DataColumn[] dcs = new DataColumn[] { };

            for (int i =1; i<=catidadBultos; i++)
            {
                listBultos.Add(i.ToString());
            }
            foreach (DataGridViewColumn c in dataGridView2.Columns)
            {
                DataColumn dc = new DataColumn();
                dc.ColumnName = c.Name;
                dc.DataType = System.Type.GetType("System.String");
                dt.Columns.Add(dc);
            }
            foreach (DataGridViewRow r in dataGridView2.Rows)
            {
                DataRow drow = dt.NewRow();

                foreach (DataGridViewCell cell in r.Cells)
                {
                    drow[cell.OwningColumn.Name] = cell.Value;
                }

                dt.Rows.Add(drow);
            }

            var distinto = (from a in dt.AsEnumerable()
                            select a.Field<string>("bulto")).Distinct().OrderBy(name=> name).ToList();

            foreach (DataGridViewRow fila1 in dataGridView1.Rows)
            {
                centinela = false;
                foreach (DataGridViewRow fila2 in dataGridView2.Rows)
                {

                    if (fila1.Cells["@mov_codigo"].Value.ToString().Equals(fila2.Cells["codigo"].Value.ToString()))
                    {
                        centinela = true;
                    }
                }

                if (centinela == false)
                {
                    retorno = false;
                    return retorno;
                }
            }

            if (Enumerable.SequenceEqual(listBultos.OrderBy(flist => flist),distinto.OrderBy(slist => slist)))
            {
                centinela = true;
            }
            else
            {
                centinela = false;
            }

            return centinela;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView2.Rows.Count > 0)
            {
                dataGridView2.Rows.RemoveAt(dataGridView2.CurrentRow.Index);
            }
            else
            {
                MessageBox.Show("No existen bultos para eliminar", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

     
    }
}
