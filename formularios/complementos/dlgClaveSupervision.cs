using System;
using System.Drawing;
using System.Windows.Forms;
using lebrun.clasesData;
using static lebrun.formularios.gerencial.EstiloGerencial;

namespace lebrun.formularios.complementos
{
    // Pide usuario y contrasena de un usuario con clave de supervision (la misma que se usa para cambiar un precio en la factura).
    // Se usa asi: if (dlgClaveSupervision.Pedir(this, "Cambiar la condicion de pago")) { ... }
    internal class dlgClaveSupervision : Form
    {
        private readonly TextBox txtUsuario = new TextBox();
        private readonly TextBox txtClave = new TextBox();

        public static bool Pedir(IWin32Window padre, string motivo)
        {
            using (dlgClaveSupervision dialogo = new dlgClaveSupervision(motivo))
            {
                return dialogo.ShowDialog(padre) == DialogResult.OK;
            }
        }

        private dlgClaveSupervision(string motivo)
        {
            AutoScaleMode = AutoScaleMode.None;
            Font = new Font(Fuente, 9F);
            BackColor = Fondo;
            Text = "Clave de supervisión";
            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition = FormStartPosition.CenterParent;
            MinimizeBox = false;
            MaximizeBox = false;
            ShowInTaskbar = false;
            ClientSize = new Size(360, 214);

            Label titulo = new Label();
            titulo.Text = motivo;
            titulo.Font = new Font(Fuente, 10F, FontStyle.Bold);
            titulo.ForeColor = Azul;
            titulo.SetBounds(16, 14, 328, 36);

            Label aviso = new Label();
            aviso.Text = "Se necesita la clave de un supervisor.";
            aviso.ForeColor = TextoSecundario;
            aviso.SetBounds(16, 52, 328, 18);

            Label lblUsuario = new Label();
            lblUsuario.Text = "Usuario";
            lblUsuario.SetBounds(16, 82, 90, 18);
            txtUsuario.SetBounds(112, 79, 232, 23);

            Label lblClave = new Label();
            lblClave.Text = "Contraseña";
            lblClave.SetBounds(16, 114, 90, 18);
            txtClave.SetBounds(112, 111, 232, 23);
            txtClave.UseSystemPasswordChar = true;

            Button aceptar = new Button();
            aceptar.Text = "Aceptar";
            aceptar.SetBounds(150, 164, 94, 30);
            aceptar.FlatStyle = FlatStyle.Flat;
            aceptar.BackColor = Azul;
            aceptar.ForeColor = Color.White;
            aceptar.FlatAppearance.BorderSize = 0;
            aceptar.Font = new Font(Fuente, 9F, FontStyle.Bold);
            aceptar.Click += delegate { Validar(); };

            Button cancelar = new Button();
            cancelar.Text = "Cancelar";
            cancelar.SetBounds(250, 164, 94, 30);
            cancelar.FlatStyle = FlatStyle.Flat;
            cancelar.BackColor = Color.White;
            cancelar.ForeColor = Azul;
            cancelar.FlatAppearance.BorderColor = Borde;
            cancelar.DialogResult = DialogResult.Cancel;

            AcceptButton = aceptar;
            CancelButton = cancelar;
            Controls.AddRange(new Control[] { titulo, aviso, lblUsuario, txtUsuario, lblClave, txtClave, aceptar, cancelar });
            Shown += delegate { txtUsuario.Focus(); };
        }

        private void Validar()
        {
            string usuario = txtUsuario.Text.Trim();
            string clave = txtClave.Text;
            if (usuario.Length == 0 || clave.Length == 0)
            {
                Avisar("Escriba el usuario y la contraseña.");
                return;
            }
            // las consultas de validacion arman el texto con el usuario y la clave: no se aceptan comillas ni barras
            if (usuario.IndexOfAny(new[] { '\'', '"', '\\' }) >= 0 || clave.IndexOfAny(new[] { '\'', '"', '\\' }) >= 0)
            {
                Avisar("Usuario o contraseña erróneos.");
                return;
            }
            try
            {
                UsuarioSistema validar = new UsuarioSistema(2);
                if (!validar.existeUsuario(usuario))
                {
                    Avisar("El usuario no existe.");
                    return;
                }
                validar.Login = usuario;
                validar.Contrasena = clave;
                if (!validar.validarUsuario())
                {
                    Avisar("Usuario o contraseña erróneos.");
                    return;
                }
                if (!validar.usuarioActivo(validar.Id))
                {
                    Avisar("El usuario está inactivo.");
                    return;
                }
                if (!validar.isClaveSupervicion(usuario, clave))
                {
                    Avisar("El usuario no tiene permiso de supervisión.");
                    return;
                }
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                RegistroErrores.Registrar(ex, "Clave de supervisión");
                Avisar("No se pudo validar la clave.\n\n" + ex.Message);
            }
        }

        private void Avisar(string mensaje)
        {
            MessageBox.Show(this, mensaje, "Clave de supervisión", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            txtClave.Clear();
            txtClave.Focus();
        }
    }
}
