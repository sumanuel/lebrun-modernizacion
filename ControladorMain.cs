using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Configuration;
using lebrun.formularios.facturacion;
using lebrun.formularios.vendedores;
using lebrun.formularios.transporte;
using lebrun.formularios.importaciones;
using lebrun.formularios.complementos;
using lebrun;
using lebrun.clasesData;


namespace lebrun
{
   public class ControladorMain
    {
       public ControladorMain()
        {
            //MessageBox.Show(Application.StartupPath);
           string ipGeneral = File.ReadAllText(Application.StartupPath + "\\ip.txt");
           try
           {
               ConfigurationManager.AppSettings.Set("server", ipGeneral);
           }
           catch (System.Configuration.ConfigurationException e) {
               MessageBox.Show("Error de configuracion");
               MessageBox.Show(e.Message);
           }


            string[] parametros;
            parametros = Environment.GetCommandLineArgs();
            if (parametros.Length > 1)
            {
                if (parametros[2] == "parametrosContables")
                {
                    Application.Run(new Principal());
                }
                if (parametros[2] == "lbxPlanCuentas")
                {
                    Application.Run(new Principal());
                }
                if (parametros[2] == "lbxAuxiliarContable")
                {
                    Application.Run(new Principal());
                }
                if (parametros[2] == "lbxComprobanteContable")
                {
                    Application.Run(new Principal());
                }

                if (parametros[2] == "comprobanteCierre")
                {
                    Application.Run(new Principal());
                }

                if (parametros[2] == "facturacion")
                {
                    Application.Run(new lbxFacturas());
                }

                if (parametros[2] == "lbxOrdenDeCompra")
                {
                    Application.Run(new lbxOrdenDeCompra());
                }
                if (parametros[2] == "frmPagare")
                {
                    Application.Run(new frmPagare());
                }

                if (parametros[2] == "frmEstructuraCostos")
                {
                    Application.Run(new Principal());
                }
            }

            else
            {
                try
                {
                    Application.Run(new frmLogin());
                }
                catch (Exception errorG)
                {
                    MessageBox.Show("Excepcion no controlada:" + errorG.Message, "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    MessageBox.Show("La aplicacion se cerrara", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    RegistroErrores.Registrar(errorG, "Excepcion no controlada al iniciar la aplicacion");
                    Application.Exit();
                }
            }
        }

      
    }
}
