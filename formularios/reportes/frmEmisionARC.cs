using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using lebrun.clases.Reportes;
using lebrun.Reportes;
using lebrun.formularios.complementos;




namespace lebrun.formularios.reportes
{
    public partial class frmEmisionARC : Form
    {
        private frmContenedorARC contenedor;
        private int opc;
        private lbxProveedores lbxProveedores;
        private lbxTrabjadores lbxTrabajadores;


        public frmEmisionARC()
        {
            InitializeComponent();
            lbxProveedores = new lbxProveedores();
            lbxTrabajadores = new lbxTrabjadores();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked) { opc = 1; }
            if (radioButton2.Checked) { opc = 2; }
            if (radioButton3.Checked) { opc = 3; }
            if (radioButton4.Checked) { opc = 4; }
            if (radioButton5.Checked) { opc = 5; }

            if ((opc == 1) && (txtCodigo.Text == "") || (opc == 2) && (txtCodigo.Text == ""))
            {
                MessageBox.Show("Debe ingresar el codigo del trabajador o proveedor", "Algo no esta bien ", MessageBoxButtons.OK, MessageBoxIcon.Information);  
            }
            else
            {
                contenedor = new frmContenedorARC("" + dateTimePicker1.Value.Year + "-" + dateTimePicker1.Value.Month + "-" + dateTimePicker1.Value.Day, "" + dateTimePicker2.Value.Year + "-" + dateTimePicker2.Value.Month + "-" + dateTimePicker2.Value.Day, txtCodigo.Text, opc);
                contenedor.Show();

                //proyecciones estimadas para el calculo del islr
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked) { txtCodigo.Visible = true; button3.Visible = true; } else { txtCodigo.Visible = false; button3.Visible = false; }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked) { txtCodigo.Visible = true; button3.Visible = true; } else { txtCodigo.Visible = false; button3.Visible = false; }
        }



        private void txtCodigo_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(this.txtCodigo, "Ingresar codigo de proveedor o trabajador según la opcion seleccionada");
            toolTip1.IsBalloon = true;
         
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked) {

                lbxTrabajadores = lbxTrabjadores.DefInstance;
                lbxTrabajadores.destino(this, this.Name);
                lbxTrabajadores.Show();
                
            }
            if (radioButton2.Checked) {

                lbxProveedores = lbxProveedores.DefInstance;
                lbxProveedores.destino(this, this.Name);
                lbxProveedores.Show(); 
            }
        }












  




    }
}
