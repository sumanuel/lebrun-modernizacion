using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using lebrun.clases.complementos;
using lebrun.clasesData;
using static lebrun.formularios.gerencial.EstiloGerencial;

namespace lebrun.formularios.facturacion
{
    // Mantenimiento de los origenes de la venta (Tienda fisica, Instagram, Facebook, WhatsApp, ...): la tabla admorigenventa. Cada factura guarda
    // el origen elegido por la cajera en admdoccli.dcli_origenventa. Nada se borra: un origen que ya no se use se desactiva, porque las facturas
    // antiguas lo tienen guardado.
    public class lbxOrigenesVenta : Form
    {
        private readonly OrigenVenta datos = new OrigenVenta();
        private readonly DataGridView grilla = new DataGridView();
        private Label cabeceraGrilla;
        private readonly TextBox txtCodigo = new TextBox();
        private readonly TextBox txtDescripcion = new TextBox();
        private readonly CheckBox chkActivo = new CheckBox();
        private readonly Button cmdNuevo = new Button();
        private readonly Button cmdGuardar = new Button();
        private readonly Button cmdCerrar = new Button();
        private readonly Label lblAviso = new Label();
        private bool esNuevo;
        private bool cargando;

        protected override void OnLoad(EventArgs e)
        {
            Armar();
            base.OnLoad(e);
            Recargar(null);
        }

        private void Armar()
        {
            SuspendLayout();
            AutoScaleMode = AutoScaleMode.None;
            Font = new Font(Fuente, 9F);
            BackColor = Fondo;
            Text = "Origen de la venta";
            Rectangle area = Screen.PrimaryScreen.WorkingArea;
            ClientSize = new Size(Math.Min(900, area.Width - 60), Math.Min(460, area.Height - 150));
            MinimumSize = new Size(760, 400);
            StartPosition = FormStartPosition.CenterParent;

            TableLayoutPanel encabezado = new TableLayoutPanel();
            encabezado.Dock = DockStyle.Fill;
            encabezado.BackColor = Color.White;
            encabezado.Margin = new Padding(0, 0, 0, 10);
            encabezado.Padding = new Padding(14, 8, 14, 6);
            encabezado.RowCount = 2;
            encabezado.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));
            encabezado.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            ConBorde(encabezado);
            Label titulo = new Label();
            titulo.Text = "Origen de la venta";
            titulo.Dock = DockStyle.Fill;
            titulo.Font = new Font(Fuente, 15F, FontStyle.Bold);
            titulo.ForeColor = Azul;
            Label descripcion = new Label();
            descripcion.Text = "Por dónde llegó la venta (tienda, Instagram, WhatsApp, ...). Se elige al facturar y queda en la factura. Se desactivan, no se borran.";
            descripcion.Dock = DockStyle.Fill;
            descripcion.ForeColor = TextoSecundario;
            encabezado.Controls.Add(titulo, 0, 0);
            encabezado.Controls.Add(descripcion, 0, 1);

            grilla.AllowUserToAddRows = false;
            grilla.AllowUserToDeleteRows = false;
            grilla.ReadOnly = true;
            grilla.MultiSelect = false;
            grilla.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grilla.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grilla.Margin = new Padding(0);
            EstiloGrilla(grilla);
            grilla.SelectionChanged += delegate { if (!cargando) { MostrarFilaActual(); } };
            grilla.CellFormatting += FormatearCelda;
            Control tarjetaGrilla = Tarjeta("Orígenes de la venta", grilla, out cabeceraGrilla);
            tarjetaGrilla.Margin = new Padding(0, 0, 10, 6);

            Control tarjetaEditor = Tarjeta("Datos", ArmarEditor());
            tarjetaEditor.Margin = new Padding(0, 0, 0, 6);

            TableLayoutPanel cuerpo = new TableLayoutPanel();
            cuerpo.Dock = DockStyle.Fill;
            cuerpo.ColumnCount = 2;
            cuerpo.RowCount = 1;
            cuerpo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 55));
            cuerpo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45));
            cuerpo.Controls.Add(tarjetaGrilla, 0, 0);
            cuerpo.Controls.Add(tarjetaEditor, 1, 0);

            TableLayoutPanel raiz = new TableLayoutPanel();
            raiz.Dock = DockStyle.Fill;
            raiz.ColumnCount = 1;
            raiz.RowCount = 2;
            raiz.Padding = new Padding(12, 10, 12, 8);
            raiz.RowStyles.Add(new RowStyle(SizeType.Absolute, 74));
            raiz.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            raiz.Controls.Add(encabezado, 0, 0);
            raiz.Controls.Add(cuerpo, 0, 1);
            Controls.Clear();
            Controls.Add(raiz);
            ResumeLayout(true);
        }

        private Control ArmarEditor()
        {
            txtCodigo.MaxLength = 4;
            txtCodigo.CharacterCasing = CharacterCasing.Upper;
            txtDescripcion.MaxLength = 40;
            chkActivo.Text = "Activo (se puede elegir)";
            chkActivo.AutoSize = true;

            TableLayoutPanel t = new TableLayoutPanel();
            t.Dock = DockStyle.Fill;
            t.ColumnCount = 2;
            t.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100));
            t.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            string[] etiquetas = { "Código", "Descripción", "" };
            Control[] campos = { txtCodigo, txtDescripcion, chkActivo };
            t.RowCount = etiquetas.Length + 2;
            for (int i = 0; i < etiquetas.Length; i++)
            {
                t.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));
                Label l = EtiquetaNueva(etiquetas[i]);
                l.Margin = new Padding(0, 7, 6, 0);
                t.Controls.Add(l, 0, i);
                campos[i].Margin = new Padding(0, 3, 0, 3);
                if (campos[i] == txtDescripcion)
                {
                    campos[i].Dock = DockStyle.Fill;
                }
                else if (!(campos[i] is CheckBox))
                {
                    campos[i].Width = 120;
                }
                t.Controls.Add(campos[i], 1, i);
            }

            lblAviso.AutoSize = false;
            lblAviso.ForeColor = Negativo;
            lblAviso.Dock = DockStyle.Fill;
            t.RowStyles.Add(new RowStyle(SizeType.Absolute, 36));
            t.Controls.Add(lblAviso, 0, etiquetas.Length);
            t.SetColumnSpan(lblAviso, 2);

            EstiloBoton(cmdNuevo, "Nuevo", false);
            EstiloBoton(cmdGuardar, "Guardar", true);
            EstiloBoton(cmdCerrar, "Cerrar", false);
            cmdNuevo.Click += delegate { NuevaFila(); };
            cmdGuardar.Click += delegate { Guardar(); };
            cmdCerrar.Click += delegate { Close(); };
            FlowLayoutPanel botones = new FlowLayoutPanel();
            botones.Dock = DockStyle.Fill;
            botones.Controls.Add(cmdGuardar);
            botones.Controls.Add(cmdNuevo);
            botones.Controls.Add(cmdCerrar);
            t.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            t.Controls.Add(botones, 0, etiquetas.Length + 1);
            t.SetColumnSpan(botones, 2);
            return t;
        }

        private static void EstiloBoton(Button b, string texto, bool principal)
        {
            b.Text = texto;
            b.AutoSize = true;
            b.MinimumSize = new Size(100, 30);
            b.FlatStyle = FlatStyle.Flat;
            b.Cursor = Cursors.Hand;
            b.Font = new Font(Fuente, 9F, FontStyle.Bold);
            b.Margin = new Padding(0, 4, 8, 0);
            if (principal)
            {
                b.BackColor = Azul;
                b.ForeColor = Color.White;
                b.FlatAppearance.BorderSize = 0;
            }
            else
            {
                b.BackColor = Color.White;
                b.ForeColor = Azul;
                b.FlatAppearance.BorderColor = Borde;
            }
        }

        // Vuelve a leer la tabla y deja seleccionada la fila indicada (o la primera).
        private void Recargar(string codigoAElegir)
        {
            cargando = true;
            try
            {
                grilla.DataSource = datos.Todos();
                grilla.Columns["orig_codigo"].HeaderText = "Código";
                grilla.Columns["orig_codigo"].FillWeight = 15;
                grilla.Columns["orig_descripcion"].HeaderText = "Descripción";
                grilla.Columns["orig_descripcion"].FillWeight = 60;
                grilla.Columns["orig_status"].HeaderText = "Estado";
                grilla.Columns["orig_status"].FillWeight = 25;
                ActualizarContador(grilla, cabeceraGrilla, "Orígenes de la venta");
                grilla.ClearSelection();
                if (codigoAElegir != null)
                {
                    foreach (DataGridViewRow fila in grilla.Rows)
                    {
                        if (string.Equals(Convert.ToString(fila.Cells["orig_codigo"].Value), codigoAElegir, StringComparison.OrdinalIgnoreCase))
                        {
                            fila.Selected = true;
                            grilla.CurrentCell = fila.Cells["orig_codigo"];
                            break;
                        }
                    }
                }
                else if (grilla.Rows.Count > 0)
                {
                    grilla.Rows[0].Selected = true;
                    grilla.CurrentCell = grilla.Rows[0].Cells["orig_codigo"];
                }
            }
            catch (Exception ex)
            {
                RegistroErrores.Registrar(ex, "Origen de la venta");
                MessageBox.Show("No se pudo leer la tabla de orígenes de la venta. ¿Se aplicó el script 06_origen_venta.sql?\n\n" + ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                cargando = false;
            }
            MostrarFilaActual();
        }

        private void FormatearCelda(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0 || e.Value == null || e.Value == DBNull.Value)
            {
                return;
            }
            if (grilla.Columns[e.ColumnIndex].Name == "orig_status")
            {
                bool activo = Convert.ToString(e.Value) == "1";
                e.Value = activo ? "Activo" : "Inactivo";
                e.FormattingApplied = true;
                if (!activo)
                {
                    e.CellStyle.ForeColor = Negativo;
                }
            }
        }

        private void MostrarFilaActual()
        {
            if (grilla.CurrentRow == null)
            {
                return;
            }
            DataRowView fila = grilla.CurrentRow.DataBoundItem as DataRowView;
            if (fila == null)
            {
                return;
            }
            DataRow r = fila.Row;
            esNuevo = false;
            txtCodigo.Text = Convert.ToString(r["orig_codigo"]);
            txtCodigo.Enabled = false;   // el codigo no se cambia: las facturas antiguas ya lo tienen guardado
            txtDescripcion.Text = Convert.ToString(r["orig_descripcion"]);
            chkActivo.Checked = Convert.ToString(r["orig_status"]) == "1";
            lblAviso.Text = "";
        }

        private void NuevaFila()
        {
            esNuevo = true;
            grilla.ClearSelection();
            txtCodigo.Clear();
            txtCodigo.Enabled = true;
            txtDescripcion.Clear();
            chkActivo.Checked = true;
            lblAviso.Text = "";
            txtCodigo.Focus();
        }

        private void Mensaje(string texto, bool error)
        {
            lblAviso.ForeColor = error ? Negativo : Azul;
            lblAviso.Text = texto;
        }

        private void Guardar()
        {
            string codigo = txtCodigo.Text.Trim();
            string descripcion = txtDescripcion.Text.Trim();
            string error = null;
            if (codigo.Length == 0 || codigo.IndexOfAny(new[] { ' ', '\'', '"', '\\' }) >= 0)
            {
                error = "El código es obligatorio y no puede tener espacios ni comillas.";
            }
            else if (descripcion.Length == 0)
            {
                error = "La descripción es obligatoria.";
            }
            else if (!chkActivo.Checked && codigo == OrigenVenta.CodigoPredeterminado)
            {
                error = "El origen " + OrigenVenta.CodigoPredeterminado + " no se puede desactivar: es el que aparece elegido al facturar.";
            }
            if (error != null)
            {
                Mensaje(error, true);
                return;
            }
            try
            {
                if (esNuevo && datos.ExisteCodigo(codigo))
                {
                    Mensaje("Ya existe un origen con ese código.", true);
                    return;
                }
                if (datos.DescripcionEnUso(descripcion, codigo))
                {
                    Mensaje("Ya existe un origen con esa descripción.", true);
                    return;
                }
                if (datos.Guardar(esNuevo, codigo, descripcion, chkActivo.Checked))
                {
                    Recargar(codigo);
                    Mensaje("Guardado.", false);
                }
                else
                {
                    Mensaje("No se pudo guardar. Revise el registro de errores.", true);
                }
            }
            catch (Exception ex)
            {
                RegistroErrores.Registrar(ex, "Guardar origen de la venta");
                Mensaje("No se pudo guardar: " + ex.Message, true);
            }
        }
    }
}
