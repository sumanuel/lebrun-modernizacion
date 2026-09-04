using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using lebrun.formularios.facturacion;

namespace lebrun.formularios.facturacion
{
    public interface IFormFacturacion {

        frmFactura soloVista(string cod);
        frmFactura soloVista2(string cod, string tipoDoc, string numeroInterno);

        void cargadoSoloVista();

        string respuestaImpresora();

        bool reimprimirFAV();

        bool realizarZ();

        bool reimprimirDEV(string a);
    }
}
