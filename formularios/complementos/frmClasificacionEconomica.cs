using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using lebrun.clases.complementos;

namespace lebrun.formularios.complementos
{
    public partial class frmClasificacionEconomica : Form
    {
        proveedor proveedor;
        lbxClasificacionEconomica economica;
        public string Eleccion;
        public string ipPc;
        public string idPc;
        public string modulo = "Clasificacion Economica";
        public frmClasificacionEconomica()
        {
            InitializeComponent();
        }

        private void frmClasificacionEconomica_Load(object sender, EventArgs e)
        {
            proveedor = new proveedor();
            economica = new lbxClasificacionEconomica();

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnRegistra_Click(object sender, EventArgs e)
        {

        }
    }
}
