using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace lebrun.clasesData
{
    /// <summary>
    /// Captura global de errores: instala los manejadores de excepciones no controladas de toda la
    /// aplicacion y escribe en un unico log la pantalla activa, el usuario, la empresa y la excepcion completa.
    /// </summary>
    public static class RegistroErrores
    {
        private const int MaximoErroresSeguidos = 5;
        private const int VentanaSegundos = 10;

        private static readonly object bloqueo = new object();
        private static readonly Queue<DateTime> erroresRecientes = new Queue<DateTime>();
        private static bool mostrandoAviso = false;
        private static string rutaLog = null;

        /// <summary>Debe llamarse una sola vez, al inicio de Main y antes de crear cualquier formulario.</summary>
        public static void Instalar()
        {
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += new ThreadExceptionEventHandler(ErrorEnHiloInterfaz);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(ErrorNoControlado);
        }

        /// <summary>Ruta del archivo de registro en uso (para mostrarla al usuario).</summary>
        public static string RutaLog
        {
            get { return ObtenerRutaLog(); }
        }

        // Error en un evento de interfaz: se registra, se avisa y la aplicacion sigue abierta.
        private static void ErrorEnHiloInterfaz(object sender, ThreadExceptionEventArgs e)
        {
            Registrar(e.Exception, "Excepcion no controlada en la interfaz");

            if (DemasiadosErroresSeguidos())
            {
                MostrarAviso("Se estan produciendo errores repetidos y la aplicacion se cerrara.\n\nEl detalle quedo registrado en:\n" + RutaLog,
                    "Lebrun");
                Application.Exit();
                return;
            }

            MostrarAviso("Ocurrio un error y quedo registrado. Puede seguir trabajando; si el problema se repite, avise a Sistemas.\n\n" +
                "Pantalla: " + DescribirPantallaActiva() + "\n" +
                "Error: " + Raiz(e.Exception).Message + "\n\n" +
                "Registro: " + RutaLog,
                "Lebrun - Error");
        }

        // Error en otro hilo o fuera del ciclo de mensajes: se registra; el sistema operativo puede cerrar la aplicacion.
        private static void ErrorNoControlado(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = e.ExceptionObject as Exception;
            if (ex != null)
            {
                Registrar(ex, "Excepcion no controlada fuera de la interfaz" + (e.IsTerminating ? " (la aplicacion se cierra)" : ""));
            }
            else
            {
                RegistrarTexto("Excepcion no controlada fuera de la interfaz", "Objeto de error: " + e.ExceptionObject);
            }
            if (e.IsTerminating)
            {
                MostrarAviso("Ocurrio un error grave y la aplicacion se cerrara.\n\nEl detalle quedo registrado en:\n" + RutaLog, "Lebrun - Error");
            }
        }

        /// <summary>Registra una excepcion con el contexto actual. Nunca lanza excepciones.</summary>
        public static void Registrar(Exception ex, string titulo)
        {
            Registrar(ex, titulo, null);
        }

        public static void Registrar(Exception ex, string titulo, string detalle)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                EscribirEncabezado(sb, titulo);
                if (!string.IsNullOrEmpty(detalle))
                {
                    sb.AppendLine(detalle);
                }
                int nivel = 0;
                for (Exception actual = ex; actual != null; actual = actual.InnerException)
                {
                    sb.AppendLine(nivel == 0 ? "Excepcion: " + actual.GetType().FullName : "Excepcion interna (" + nivel + "): " + actual.GetType().FullName);
                    sb.AppendLine("Metodo de error: " + actual.TargetSite);
                    sb.AppendLine("Pila de llamadas: " + actual.StackTrace);
                    sb.AppendLine("Mensaje del Error: " + actual.Message);
                    nivel++;
                }
                sb.AppendLine();
                Escribir(sb.ToString());
            }
            catch (Exception) { }
        }

        /// <summary>Registra un aviso o situacion anormal que no es una excepcion. Nunca lanza excepciones.</summary>
        public static void RegistrarTexto(string titulo, string detalle)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                EscribirEncabezado(sb, titulo);
                sb.AppendLine(detalle);
                sb.AppendLine();
                Escribir(sb.ToString());
            }
            catch (Exception) { }
        }

        private static void EscribirEncabezado(StringBuilder sb, string titulo)
        {
            sb.AppendLine("------------------------------------------------------");
            sb.AppendLine("------------------------------------------------------");
            sb.AppendLine();
            sb.AppendLine("Error Fecha " + DateTime.Now);
            sb.AppendLine("Tipo: " + titulo);
            sb.AppendLine("Pantalla: " + DescribirPantallaActiva());
            sb.AppendLine("Usuario: " + DescribirUsuario());
            sb.AppendLine("Version: " + Assembly.GetExecutingAssembly().GetName().Version + "   Equipo: " + Environment.MachineName);
            sb.AppendLine();
        }

        // Nombre de la opcion de menu (Principal asigna Name con el texto del menu), titulo y clase de la pantalla activa.
        private static string DescribirPantallaActiva()
        {
            try
            {
                Form activa = null;
                foreach (Form f in Application.OpenForms)
                {
                    Principal p = f as Principal;
                    if (p != null)
                    {
                        activa = p.ActiveMdiChild;
                        break;
                    }
                }
                if (activa == null)
                {
                    activa = Form.ActiveForm;
                }
                if (activa == null && Application.OpenForms.Count > 0)
                {
                    activa = Application.OpenForms[Application.OpenForms.Count - 1];
                }
                if (activa == null)
                {
                    return "(ninguna pantalla activa)";
                }
                return "\"" + activa.Text + "\" (opcion de menu: " + activa.Name + ", clase: " + activa.GetType().FullName + ")";
            }
            catch (Exception)
            {
                return "(no se pudo determinar)";
            }
        }

        private static string DescribirUsuario()
        {
            try
            {
                foreach (Form f in Application.OpenForms)
                {
                    Principal p = f as Principal;
                    if (p != null && p.usuarioActual != null)
                    {
                        return p.usuarioActual.NombreUsuario + " (empresa " + p.usuarioActual.Codigo + ")";
                    }
                }
                return "(sin sesion iniciada)";
            }
            catch (Exception)
            {
                return "(no se pudo determinar)";
            }
        }

        private static Exception Raiz(Exception ex)
        {
            while (ex.InnerException != null)
            {
                ex = ex.InnerException;
            }
            return ex;
        }

        private static bool DemasiadosErroresSeguidos()
        {
            lock (bloqueo)
            {
                DateTime ahora = DateTime.Now;
                erroresRecientes.Enqueue(ahora);
                while (erroresRecientes.Count > 0 && (ahora - erroresRecientes.Peek()).TotalSeconds > VentanaSegundos)
                {
                    erroresRecientes.Dequeue();
                }
                return erroresRecientes.Count > MaximoErroresSeguidos;
            }
        }

        private static void MostrarAviso(string mensaje, string titulo)
        {
            // Evita avisos apilados si el error se repite mientras el primero sigue abierto.
            if (mostrandoAviso)
            {
                return;
            }
            try
            {
                mostrandoAviso = true;
                MessageBox.Show(mensaje, titulo, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception) { }
            finally
            {
                mostrandoAviso = false;
            }
        }

        private static void Escribir(string texto)
        {
            lock (bloqueo)
            {
                string ruta = ObtenerRutaLog();
                File.AppendAllText(ruta, texto, Encoding.UTF8);
            }
        }

        // Carpeta comun de datos (C:\ProgramData\Lebrun); si no se puede escribir ahi, la carpeta del ejecutable y por ultimo la temporal.
        private static string ObtenerRutaLog()
        {
            if (rutaLog != null)
            {
                return rutaLog;
            }
            string[] carpetas = new string[]
            {
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Lebrun"),
                Application.StartupPath,
                Path.GetTempPath()
            };
            foreach (string carpeta in carpetas)
            {
                try
                {
                    Directory.CreateDirectory(carpeta);
                    string candidata = Path.Combine(carpeta, "log.txt");
                    using (FileStream fs = new FileStream(candidata, FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
                    {
                    }
                    rutaLog = candidata;
                    return rutaLog;
                }
                catch (Exception) { }
            }
            return Path.Combine(Path.GetTempPath(), "log.txt");
        }
    }
}
