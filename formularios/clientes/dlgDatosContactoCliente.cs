using System;
using System.Drawing;
using System.Windows.Forms;
using lebrun.clases.clientes;
using lebrun.clasesData;
using static lebrun.formularios.gerencial.EstiloGerencial;

namespace lebrun.formularios.clientes
{
    // Aviso a la cajera cuando el cliente elegido en la factura no tiene correo o fecha de nacimiento: se piden en caja para poder
    // enviarle promociones y felicitarlo en su cumpleanos. Ninguno de los dos es obligatorio ("Omitir" deja la factura seguir).
    // Se usa asi: dlgDatosContactoCliente.Solicitar(this, client1);
    internal class dlgDatosContactoCliente : Form
    {
        private readonly Clientes cliente;
        private readonly TextBox txtCorreo = new TextBox();
        private readonly DateTimePicker dtpNacimiento = new DateTimePicker();

        // Muestra el aviso solo si al cliente le falta algun dato. Nunca impide facturar: cualquier error se registra y se sigue.
        public static void Solicitar(IWin32Window padre, Clientes cliente)
        {
            try
            {
                if (cliente == null || !cliente.FaltanDatosContacto())
                {
                    return;
                }
                using (dlgDatosContactoCliente dialogo = new dlgDatosContactoCliente(cliente))
                {
                    dialogo.ShowDialog(padre);
                }
            }
            catch (Exception ex)
            {
                RegistroErrores.Registrar(ex, "Datos de contacto del cliente");
            }
        }

        private dlgDatosContactoCliente(Clientes cliente)
        {
            this.cliente = cliente;
            AutoScaleMode = AutoScaleMode.None;
            Font = new Font(Fuente, 9F);
            BackColor = Fondo;
            Text = "Datos de contacto del cliente";
            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition = FormStartPosition.CenterParent;
            MinimizeBox = false;
            MaximizeBox = false;
            ShowInTaskbar = false;
            ClientSize = new Size(420, 258);

            bool faltaCorreo = string.IsNullOrWhiteSpace(cliente.Email);
            bool faltaFecha = !cliente.FechaNacimiento.HasValue;
            string falta = faltaCorreo && faltaFecha ? "el correo y la fecha de nacimiento"
                         : faltaCorreo ? "el correo" : "la fecha de nacimiento";

            Label titulo = new Label();
            titulo.Text = "Actualizar datos del cliente";
            titulo.Font = new Font(Fuente, 10F, FontStyle.Bold);
            titulo.ForeColor = Azul;
            titulo.SetBounds(16, 14, 388, 20);

            Label aviso = new Label();
            aviso.Text = (cliente.Nombre ?? "") + "\nLe falta " + falta + ". Pídaselo al cliente: sirve para enviarle promociones y felicitarlo " +
                         "en su cumpleaños. No es obligatorio.";
            aviso.ForeColor = TextoSecundario;
            aviso.SetBounds(16, 40, 388, 64);

            Label lblCorreo = new Label();
            lblCorreo.Text = "Correo";
            lblCorreo.SetBounds(16, 118, 120, 18);
            txtCorreo.SetBounds(142, 115, 262, 23);
            txtCorreo.MaxLength = 60;
            txtCorreo.Text = cliente.Email ?? "";

            Label lblNac = new Label();
            lblNac.Text = "Fecha de nacimiento";
            lblNac.SetBounds(16, 154, 124, 18);
            dtpNacimiento.Format = DateTimePickerFormat.Short;
            dtpNacimiento.ShowCheckBox = true;
            dtpNacimiento.SetBounds(142, 151, 150, 23);
            dtpNacimiento.MaxDate = DateTime.Today;
            if (cliente.FechaNacimiento.HasValue)
            {
                dtpNacimiento.Value = cliente.FechaNacimiento.Value;
                dtpNacimiento.Checked = true;
            }
            else
            {
                // sin fecha: el cajero marca la casilla y elige la fecha
                dtpNacimiento.Value = DateTime.Today.AddYears(-30);
                dtpNacimiento.Checked = false;
            }

            Button guardar = new Button();
            guardar.Text = "Guardar";
            guardar.SetBounds(206, 206, 94, 32);
            guardar.FlatStyle = FlatStyle.Flat;
            guardar.BackColor = Azul;
            guardar.ForeColor = Color.White;
            guardar.FlatAppearance.BorderSize = 0;
            guardar.Font = new Font(Fuente, 9F, FontStyle.Bold);
            guardar.Click += delegate { Guardar(); };

            Button omitir = new Button();
            omitir.Text = "Omitir";
            omitir.SetBounds(310, 206, 94, 32);
            omitir.FlatStyle = FlatStyle.Flat;
            omitir.BackColor = Color.White;
            omitir.ForeColor = Azul;
            omitir.FlatAppearance.BorderColor = Borde;
            omitir.DialogResult = DialogResult.Cancel;

            AcceptButton = guardar;
            CancelButton = omitir;
            Controls.AddRange(new Control[] { titulo, aviso, lblCorreo, txtCorreo, lblNac, dtpNacimiento, guardar, omitir });
            Shown += delegate { (faltaCorreo ? (Control)txtCorreo : dtpNacimiento).Focus(); };
        }

        private void Guardar()
        {
            string correo = txtCorreo.Text.Trim();
            DateTime? fecha = dtpNacimiento.Checked ? (DateTime?)dtpNacimiento.Value.Date : null;

            if (correo == "" && !fecha.HasValue)
            {
                Avisar("Escriba el correo o marque la fecha de nacimiento, o pulse Omitir.");
                return;
            }
            if (!Clientes.CorreoValido(correo))
            {
                Avisar("El correo no tiene un formato válido (ejemplo: nombre@dominio.com).");
                txtCorreo.Focus();
                return;
            }
            if (fecha.HasValue && !Clientes.FechaNacimientoValida(fecha.Value))
            {
                Avisar("La fecha de nacimiento no es válida.");
                dtpNacimiento.Focus();
                return;
            }
            if (!cliente.GuardarDatosContacto(correo, fecha))
            {
                Avisar("No se pudieron guardar los datos. Puede omitirlos y facturar.");
                return;
            }
            DialogResult = DialogResult.OK;
        }

        private void Avisar(string mensaje)
        {
            MessageBox.Show(this, mensaje, "Datos de contacto", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
