using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using lebrun.clasesData;

namespace lebrun
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            RegistroErrores.Instalar();
            ControladorMain c1 = new ControladorMain();
        }
    }
}
