using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AxTM_FISCAL;
using AxMSCommLib;
using lebrun.clases.facturacion;

namespace lebrun.formularios.facturacion
{
    public partial class ReportesZ_X : Form
    {

        ImpresoraFiscal epsonLX300;

        public ReportesZ_X()
        {
            InitializeComponent();
        }

        private void ReportesZ_X_Load(object sender, EventArgs e)
        {   
            epsonLX300 = new ImpresoraFiscal(this.FISCAL, this.IPuerto);
            this.Text = this.Text + " " + this.Name;
            label1.Text = label1.Text + " " + this.Name;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            string respuesta = null;
            respuesta = epsonLX300.estadoImpresora();
            if (this.Name.Equals("Reporte X"))
            {

                if ((respuesta.Equals("Error de Comunicaion")))
                {
                    MessageBox.Show("Error de Comunicaion!! Verifique que la impresora este conectada y encendida!!", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (respuesta.Equals("01"))
                {
                    epsonLX300.cerrarDocumentoFiscal();
                    epsonLX300.reporteX();
                    this.Close();
                }
                if (respuesta.Equals("04"))
                {
                    epsonLX300.reporteX();
                    this.Close();
                }
                if (respuesta.Equals("08"))
                {
                    epsonLX300.reporteX();
                    this.Close();
                }
                if (respuesta.Equals("00"))
                {
                    epsonLX300.reporteX();
                    this.Close();
                }
                if (respuesta.Equals("02"))
                {
                    MessageBox.Show("Comprobante NoFiscal Abierto!!", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if ((Convert.ToInt32(respuesta) >= 9) && (Convert.ToInt32(respuesta) <= 14))
                {
                    MessageBox.Show("Controlador Bloqueado!", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if ((Convert.ToInt32(respuesta)) >= 15)
                {
                    MessageBox.Show("Error Desconocido", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                this.Close();
            }

            if (this.Name.Equals("Reporte Z"))
            {
                if ((respuesta.Equals("Error de Comunicaion")))
                {
                    MessageBox.Show("Error de Comunicaion!! Verifique que la impresora este conectada y encendida!!", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (respuesta.Equals("01"))
                {
                    epsonLX300.cerrarDocumentoFiscal();
                    epsonLX300.reporteZ();
                    this.Close();
                }
                if (respuesta.Equals("04"))
                {
                    epsonLX300.reporteZ();
                    this.Close();
                }
                if (respuesta.Equals("08"))
                {
                    epsonLX300.reporteZ();
                    this.Close();
                }
                if (respuesta.Equals("00"))
                {
                    epsonLX300.reporteZ();
                    this.Close();
                }
                if (respuesta.Equals("02"))
                {
                    MessageBox.Show("Comprobante NoFiscal Abierto!!", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if ((Convert.ToInt32(respuesta) >= 9) && (Convert.ToInt32(respuesta) <= 14))
                {
                    MessageBox.Show("Controlador Bloqueado!", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if ((Convert.ToInt32(respuesta)) >= 15)
                {
                    MessageBox.Show("Error Desconocido", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                this.Close();
            }
        }


    }
}
