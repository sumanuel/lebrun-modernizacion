using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using lebrun.clases.clientes;
using lebrun.clasesData;
using static lebrun.formularios.gerencial.EstiloGerencial;

namespace lebrun.formularios.tesoreria
{
    // Mantenimiento de las condiciones de pago (Contado, 30 dias, ...) y de las formas de pago (Efectivo, Tarjeta, ...): la tabla admcondpago.
    // Nada se borra: una condicion o forma de pago que ya no se use se desactiva, porque los documentos antiguos la tienen guardada.
    public class lbxCondicionesPago : Form
    {
        private readonly CondicionPago datos = new CondicionPago();
        private readonly DataGridView grilla = new DataGridView();
        private Label cabeceraGrilla;
        private readonly TextBox txtCodigo = new TextBox();
        private readonly TextBox txtDescripcion = new TextBox();
        private readonly TextBox txtSiglas = new TextBox();
        private readonly ComboBox cboTipo = new ComboBox();
        private readonly NumericUpDown nudDias = new NumericUpDown();
        private readonly NumericUpDown nudMora = new NumericUpDown();
        private readonly TextBox txtObservacion = new TextBox();
        private readonly CheckBox chkActiva = new CheckBox();
        private readonly CheckBox chkPideBanco = new CheckBox();
        private readonly ComboBox cboGrupo = new ComboBox();
        private readonly ComboBox cboMoneda = new ComboBox();
        private readonly Button cmdNuevo = new Button();
        private readonly Button cmdGuardar = new Button();
        private readonly Button cmdCerrar = new Button();
        private readonly Label lblAviso = new Label();
        private bool esNueva;
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
            Text = "Condiciones y formas de pago";
            Rectangle area = Screen.PrimaryScreen.WorkingArea;
            ClientSize = new Size(Math.Min(1080, area.Width - 60), Math.Min(600, area.Height - 150));
            MinimumSize = new Size(900, 520);
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
            titulo.Text = "Condiciones y formas de pago";
            titulo.Dock = DockStyle.Fill;
            titulo.Font = new Font(Fuente, 15F, FontStyle.Bold);
            titulo.ForeColor = Azul;
            Label descripcion = new Label();
            descripcion.Text = "Las condiciones de crédito (Contado, 30 días, ...) se eligen en el cliente y en la factura; las formas de pago (Efectivo, Tarjeta, ...) al cancelar la factura. Se desactivan, no se borran.";
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
            Control tarjetaGrilla = Tarjeta("Condiciones y formas de pago", grilla, out cabeceraGrilla);
            tarjetaGrilla.Margin = new Padding(0, 0, 10, 6);

            Control tarjetaEditor = Tarjeta("Datos", ArmarEditor());
            tarjetaEditor.Margin = new Padding(0, 0, 0, 6);

            TableLayoutPanel cuerpo = new TableLayoutPanel();
            cuerpo.Dock = DockStyle.Fill;
            cuerpo.ColumnCount = 2;
            cuerpo.RowCount = 1;
            cuerpo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 58));
            cuerpo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 42));
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
            txtCodigo.MaxLength = 7;
            txtCodigo.CharacterCasing = CharacterCasing.Upper;
            txtDescripcion.MaxLength = 128;
            txtSiglas.MaxLength = 4;
            txtSiglas.CharacterCasing = CharacterCasing.Upper;
            cboTipo.DropDownStyle = ComboBoxStyle.DropDownList;
            cboTipo.Items.Add("Condición de crédito");
            cboTipo.Items.Add("Forma de pago");
            cboTipo.SelectedIndex = 0;
            cboTipo.SelectedIndexChanged += delegate { AjustarSegunTipo(); };
            nudDias.Minimum = 0;
            nudDias.Maximum = 999;
            nudMora.Minimum = 0;
            nudMora.Maximum = 999;
            txtObservacion.MaxLength = 255;
            txtObservacion.Multiline = true;
            txtObservacion.Height = 48;
            chkActiva.Text = "Activa (se puede elegir)";
            chkActiva.AutoSize = true;
            chkPideBanco.Text = "Pide banco y número al cobrar";
            chkPideBanco.AutoSize = true;
            cboGrupo.DropDownStyle = ComboBoxStyle.DropDownList;
            cboGrupo.Items.Add("Efectivo");
            cboGrupo.Items.Add("Tarjetas");
            cboGrupo.Items.Add("Cheques");
            cboGrupo.SelectedIndex = 0;
            // moneda en que se cobra la forma de pago: en Divisa $ y Divisa Euro el cajero escribe lo recibido en dolares o euros y se guarda el monto original
            cboMoneda.DropDownStyle = ComboBoxStyle.DropDownList;
            cboMoneda.Items.Add("Bolívares");
            cboMoneda.Items.Add("Dólares ($)");
            cboMoneda.Items.Add("Euros (€)");
            cboMoneda.SelectedIndex = 0;

            TableLayoutPanel t = new TableLayoutPanel();
            t.Dock = DockStyle.Fill;
            t.ColumnCount = 2;
            t.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 112));
            t.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            string[] etiquetas = { "Código", "Descripción", "Siglas (máx. 4)", "Tipo", "Días de crédito", "Días de mora", "Se suma en", "Moneda", "", "Observación", "" };
            Control[] campos = { txtCodigo, txtDescripcion, txtSiglas, cboTipo, nudDias, nudMora, cboGrupo, cboMoneda, chkPideBanco, txtObservacion, chkActiva };
            int[] altos = { 30, 30, 30, 30, 30, 30, 30, 30, 28, 58, 30 };
            t.RowCount = etiquetas.Length + 2;
            for (int i = 0; i < etiquetas.Length; i++)
            {
                t.RowStyles.Add(new RowStyle(SizeType.Absolute, altos[i]));
                Label l = EtiquetaNueva(etiquetas[i]);
                l.Margin = new Padding(0, 7, 6, 0);
                t.Controls.Add(l, 0, i);
                campos[i].Margin = new Padding(0, 3, 0, 3);
                if (!(campos[i] is CheckBox))
                {
                    campos[i].Width = 230;
                }
                if (campos[i] == txtDescripcion || campos[i] == txtObservacion)
                {
                    campos[i].Dock = DockStyle.Fill;
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
                grilla.DataSource = datos.Todas();
                ColTexto(grilla, "conp_codigo", "Código", 11);
                ColTexto(grilla, "conp_descripcion", "Descripción", 34);
                ColTexto(grilla, "conp_siglas", "Siglas", 9);
                ColTexto(grilla, "conp_tipo", "Tipo", 16);
                grilla.Columns["conp_req_banco"].Visible = false;
                grilla.Columns["conp_grupo"].Visible = false;
                grilla.Columns["conp_moneda"].Visible = false;
                ColNumerica(grilla, "conp_cant_dias", "n0", 8);
                ColNumerica(grilla, "mora_fact1", "n0", 7);
                ColTexto(grilla, "conp_status", "Estado", 10);
                grilla.Columns["conp_cant_dias"].HeaderText = "Días";
                grilla.Columns["mora_fact1"].HeaderText = "Mora";
                grilla.Columns["conp_observ"].Visible = false;
                ActualizarContador(grilla, cabeceraGrilla, "Condiciones y formas de pago");
                grilla.ClearSelection();
                if (codigoAElegir != null)
                {
                    foreach (DataGridViewRow fila in grilla.Rows)
                    {
                        if (string.Equals(Convert.ToString(fila.Cells["conp_codigo"].Value), codigoAElegir, StringComparison.OrdinalIgnoreCase))
                        {
                            fila.Selected = true;
                            grilla.CurrentCell = fila.Cells["conp_codigo"];
                            break;
                        }
                    }
                }
                else if (grilla.Rows.Count > 0)
                {
                    grilla.Rows[0].Selected = true;
                    grilla.CurrentCell = grilla.Rows[0].Cells["conp_codigo"];
                }
            }
            catch (Exception ex)
            {
                RegistroErrores.Registrar(ex, "Condiciones y formas de pago");
                MessageBox.Show("No se pudo leer la tabla de condiciones de pago.\n\n" + ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                cargando = false;
            }
            MostrarFilaActual();
        }

        private static void ColTexto(DataGridView g, string nombre, string encabezado, float peso)
        {
            g.Columns[nombre].HeaderText = encabezado;
            g.Columns[nombre].FillWeight = peso;
        }

        private static void ColNumerica(DataGridView g, string nombre, string formato, float peso)
        {
            DataGridViewColumn c = g.Columns[nombre];
            c.DefaultCellStyle.Format = formato;
            c.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            c.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            c.FillWeight = peso;
        }

        private void FormatearCelda(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0 || e.Value == null || e.Value == DBNull.Value)
            {
                return;
            }
            string columna = grilla.Columns[e.ColumnIndex].Name;
            if (columna == "conp_tipo")
            {
                e.Value = Convert.ToString(e.Value) == CondicionPago.TipoFormaPago ? "Forma de pago" : "Condición";
                e.FormattingApplied = true;
            }
            else if (columna == "conp_status")
            {
                bool activa = Convert.ToString(e.Value) == "1";
                e.Value = activa ? "Activa" : "Inactiva";
                e.FormattingApplied = true;
                if (!activa)
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
            esNueva = false;
            txtCodigo.Text = Convert.ToString(r["conp_codigo"]);
            txtCodigo.Enabled = false;
            txtDescripcion.Text = Convert.ToString(r["conp_descripcion"]);
            txtSiglas.Text = r["conp_siglas"] == DBNull.Value ? "" : Convert.ToString(r["conp_siglas"]);
            cboTipo.SelectedIndex = Convert.ToString(r["conp_tipo"]) == CondicionPago.TipoFormaPago ? 1 : 0;
            cboTipo.Enabled = false;  // el tipo no se cambia: los documentos antiguos ya usan ese codigo
            nudDias.Value = r["conp_cant_dias"] == DBNull.Value ? 0 : Math.Min(999, Convert.ToDecimal(r["conp_cant_dias"]));
            nudMora.Value = r["mora_fact1"] == DBNull.Value ? 0 : Math.Min(999, Convert.ToInt32(r["mora_fact1"]));
            txtObservacion.Text = r["conp_observ"] == DBNull.Value ? "" : Convert.ToString(r["conp_observ"]);
            chkActiva.Checked = Convert.ToString(r["conp_status"]) == "1";
            chkPideBanco.Checked = Convert.ToString(r["conp_req_banco"]) == "1";
            string grupo = Convert.ToString(r["conp_grupo"]);
            cboGrupo.SelectedIndex = grupo == "T" ? 1 : grupo == "C" ? 2 : 0;
            string moneda = r["conp_moneda"] == DBNull.Value ? "" : Convert.ToString(r["conp_moneda"]).Trim().ToUpper();
            cboMoneda.SelectedIndex = moneda == "USD" ? 1 : moneda == "EUR" ? 2 : 0;
            lblAviso.Text = "";
            AjustarSegunTipo();
        }

        private void NuevaFila()
        {
            esNueva = true;
            grilla.ClearSelection();
            txtCodigo.Clear();
            txtCodigo.Enabled = true;
            txtDescripcion.Clear();
            txtSiglas.Clear();
            cboTipo.Enabled = true;
            cboTipo.SelectedIndex = 0;
            nudDias.Value = 0;
            nudMora.Value = 0;
            txtObservacion.Clear();
            chkActiva.Checked = true;
            chkPideBanco.Checked = false;
            cboGrupo.SelectedIndex = 0;
            cboMoneda.SelectedIndex = 0;
            lblAviso.Text = "";
            AjustarSegunTipo();
            txtCodigo.Focus();
        }

        private void Mensaje(string texto, bool error)
        {
            lblAviso.ForeColor = error ? Negativo : Azul;
            lblAviso.Text = texto;
        }

        // Las formas de pago no tienen dias de credito ni de mora; las condiciones de credito no piden banco ni se suman en una casilla.
        private void AjustarSegunTipo()
        {
            bool condicion = cboTipo.SelectedIndex == 0;
            nudDias.Enabled = condicion;
            nudMora.Enabled = condicion;
            cboGrupo.Enabled = !condicion;
            chkPideBanco.Enabled = !condicion;
            cboMoneda.Enabled = !condicion;
            if (condicion)
            {
                cboMoneda.SelectedIndex = 0;
            }
            if (!condicion)
            {
                nudDias.Value = 0;
                nudMora.Value = 0;
            }
            else
            {
                chkPideBanco.Checked = false;
                cboGrupo.SelectedIndex = 0;
            }
        }

        private void Guardar()
        {
            string codigo = txtCodigo.Text.Trim();
            string descripcion = txtDescripcion.Text.Trim();
            string siglas = txtSiglas.Text.Trim();
            string error = null;
            if (codigo.Length == 0 || codigo.IndexOfAny(new[] { ' ', '\'', '"', '\\' }) >= 0)
            {
                error = "El código es obligatorio y no puede tener espacios ni comillas.";
            }
            else if (descripcion.Length == 0)
            {
                error = "La descripción es obligatoria.";
            }
            else if (siglas.Length == 0 || siglas.Length > 4)
            {
                error = "Las siglas son obligatorias y tienen como máximo 4 letras.";
            }
            else if (!chkActiva.Checked && codigo == CondicionPago.CodigoContado)
            {
                error = "La condición Contado (07) no se puede desactivar: es la que se usa por omisión.";
            }
            else if (!chkActiva.Checked && codigo == lebrun.clases.complementos.FormasPagoVenta.Efectivo)
            {
                error = "La forma de pago Efectivo (F01) no se puede desactivar: es la que se usa por omisión y para el vuelto.";
            }
            if (error != null)
            {
                Mensaje(error, true);
                return;
            }
            try
            {
                if (esNueva && datos.ExisteCodigo(codigo))
                {
                    Mensaje("Ya existe una fila con ese código.", true);
                    return;
                }
                if (datos.SiglasEnUso(siglas, codigo))
                {
                    Mensaje("Esas siglas ya las usa otra fila.", true);
                    return;
                }
                string tipo = cboTipo.SelectedIndex == 1 ? CondicionPago.TipoFormaPago : CondicionPago.TipoCondicion;
                if (datos.Guardar(esNueva, codigo, descripcion, siglas, tipo, nudDias.Value, (int)nudMora.Value, txtObservacion.Text.Trim(), chkActiva.Checked,
                    chkPideBanco.Checked, cboGrupo.SelectedIndex == 1 ? "T" : cboGrupo.SelectedIndex == 2 ? "C" : "E",
                    cboMoneda.SelectedIndex == 1 ? "USD" : cboMoneda.SelectedIndex == 2 ? "EUR" : ""))
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
                RegistroErrores.Registrar(ex, "Guardar condicion de pago");
                Mensaje("No se pudo guardar: " + ex.Message, true);
            }
        }
    }
}
