using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lebrun.formularios.cotizacion
{
    public interface IFormPreFac
    {
        frmPreFac soloVista(string cod);
        frmPreFac soloVista2(string cod, string tipoDoc, string numeroInterno);

        void cargadoSoloVista();

        string respuestaImpresora();

        bool reimprimirFAV();

        bool realizarZ();

        bool reimprimirDEV(string a);
    }
}
