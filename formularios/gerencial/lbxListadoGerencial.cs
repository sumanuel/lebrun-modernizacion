using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows.Forms;
using lebrun.clases.gerencial;
using lebrun.clasesData;
using static lebrun.formularios.gerencial.EstiloGerencial;

namespace lebrun.formularios.gerencial
{
    // Base de los listados del modulo gerencial: titulo y descripcion arriba, filtros con el boton Consultar, la grilla con el
    // contador de registros, un resumen al pie y el boton Exportar (archivo CSV que abre Excel). Cada listado indica sus filtros,
    // su consulta y el formato de sus columnas; el estilo es el mismo de Consulta General Art. (EstiloGerencial).
    public abstract class lbxListadoGerencial : Form
    {
        protected readonly ListadosGerenciales datos = new ListadosGerenciales();
        protected readonly DataGridView grilla = new DataGridView();
        protected readonly FlowLayoutPanel panelFiltros = new FlowLayoutPanel();

        private readonly Label lblResumen = new Label();
        private readonly Button cmdConsultar = new Button();
        private readonly Button cmdExportar = new Button();
        private Label cabeceraGrilla;
        private Label cabeceraDetalle;
        protected readonly DataGridView grillaDetalle = new DataGridView();

        protected abstract string TituloPantalla { get; }
        protected abstract string DescripcionPantalla { get; }
        protected virtual string TituloGrilla { get { return "Resultados"; } }

        // Crea los controles de filtro (use Filtro(...) para ponerlos en el panel).
        protected abstract void CrearFiltros();

        // Ejecuta la consulta con los filtros actuales. Para avisar que un filtro no es valido, lance ArgumentException con el mensaje.
        protected abstract DataTable Consultar();

        // Ajusta las columnas ya cargadas (formato, ancho, alineacion).
        protected virtual void DarFormato() { }

        // Texto de resumen al pie (totales); null = ninguno.
        protected virtual string ResumenDe(DataTable tabla) { return null; }

        // Detalle opcional: una segunda grilla bajo la principal que muestra los datos de la fila seleccionada.
        protected virtual bool TieneDetalle { get { return false; } }
        protected virtual string TituloDetalle { get { return "Detalle"; } }
        protected virtual DataTable ConsultarDetalle(DataRow fila) { return null; }
        protected virtual void DarFormatoDetalle() { }

        // true cuando la consulta tenia mas filas que el limite y solo se muestran las primeras.
        protected bool ResultadoRecortado { get; private set; }

        protected override void OnLoad(EventArgs e)
        {
            Armar();
            base.OnLoad(e);
        }

        private void Armar()
        {
            SuspendLayout();
            AutoScaleMode = AutoScaleMode.None;
            Font = new Font(Fuente, 9F);
            BackColor = Fondo;
            Text = TituloPantalla;
            Rectangle area = Screen.PrimaryScreen.WorkingArea;
            ClientSize = new Size(Math.Min(1150, area.Width - 60), Math.Min(680, area.Height - 150));
            MinimumSize = new Size(820, 480);
            DoubleBuffered = true;
            StartPosition = FormStartPosition.CenterParent;

            // encabezado
            TableLayoutPanel encabezado = new TableLayoutPanel();
            encabezado.Dock = DockStyle.Fill;
            encabezado.BackColor = Color.White;
            encabezado.Margin = new Padding(0, 0, 0, 10);
            encabezado.Padding = new Padding(14, 8, 14, 6);
            encabezado.ColumnCount = 1;
            encabezado.RowCount = 2;
            encabezado.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));
            encabezado.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            ConBorde(encabezado);
            Label titulo = new Label();
            titulo.Text = TituloPantalla;
            titulo.AutoSize = false;
            titulo.Dock = DockStyle.Fill;
            titulo.Font = new Font(Fuente, 15F, FontStyle.Bold);
            titulo.ForeColor = Azul;
            titulo.TextAlign = ContentAlignment.MiddleLeft;
            Label descripcion = new Label();
            descripcion.Text = DescripcionPantalla;
            descripcion.AutoSize = false;
            descripcion.Dock = DockStyle.Fill;
            descripcion.ForeColor = TextoSecundario;
            descripcion.TextAlign = ContentAlignment.TopLeft;
            encabezado.Controls.Add(titulo, 0, 0);
            encabezado.Controls.Add(descripcion, 0, 1);

            // filtros y botones
            panelFiltros.Dock = DockStyle.Fill;
            panelFiltros.AutoSize = true;
            panelFiltros.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            panelFiltros.BackColor = Color.White;
            panelFiltros.Margin = new Padding(0, 0, 0, 10);
            panelFiltros.Padding = new Padding(12, 10, 12, 6);
            panelFiltros.WrapContents = true;
            ConBorde(panelFiltros);
            CrearFiltros();
            EstiloBoton(cmdConsultar, "Consultar", true);
            EstiloBoton(cmdExportar, "Exportar a Excel", false);
            cmdConsultar.Click += delegate { Ejecutar(); };
            cmdExportar.Click += delegate { Exportar(); };
            cmdConsultar.Margin = new Padding(16, 2, 6, 4);
            cmdExportar.Margin = new Padding(0, 2, 0, 4);
            panelFiltros.Controls.Add(cmdConsultar);
            panelFiltros.Controls.Add(cmdExportar);
            AcceptButton = cmdConsultar;

            // grilla
            grilla.AllowUserToAddRows = false;
            grilla.AllowUserToDeleteRows = false;
            grilla.ReadOnly = true;
            grilla.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grilla.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grilla.Margin = new Padding(0);
            EstiloGrilla(grilla);
            grilla.CellFormatting += FormatearCelda;
            Control tarjeta = Tarjeta(TituloGrilla, grilla, out cabeceraGrilla);
            tarjeta.Margin = new Padding(0, 0, 0, TieneDetalle ? 10 : 6);

            Control tarjetaDetalle = null;
            if (TieneDetalle)
            {
                grillaDetalle.AllowUserToAddRows = false;
                grillaDetalle.AllowUserToDeleteRows = false;
                grillaDetalle.ReadOnly = true;
                grillaDetalle.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                grillaDetalle.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                grillaDetalle.Margin = new Padding(0);
                EstiloGrilla(grillaDetalle);
                grillaDetalle.CellFormatting += FormatearCelda;
                tarjetaDetalle = Tarjeta(TituloDetalle, grillaDetalle, out cabeceraDetalle);
                tarjetaDetalle.Margin = new Padding(0, 0, 0, 6);
                grilla.SelectionChanged += delegate { CargarDetalle(); };
            }

            // resumen
            lblResumen.AutoSize = false;
            lblResumen.Dock = DockStyle.Fill;
            lblResumen.ForeColor = Azul;
            lblResumen.Font = new Font(Fuente, 9.5F, FontStyle.Bold);
            lblResumen.TextAlign = ContentAlignment.MiddleLeft;

            TableLayoutPanel raiz = new TableLayoutPanel();
            raiz.Dock = DockStyle.Fill;
            raiz.ColumnCount = 1;
            raiz.RowCount = TieneDetalle ? 5 : 4;
            raiz.Padding = new Padding(12, 10, 12, 8);
            raiz.RowStyles.Add(new RowStyle(SizeType.Absolute, 74));
            raiz.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            raiz.RowStyles.Add(new RowStyle(SizeType.Percent, TieneDetalle ? 58 : 100));
            if (TieneDetalle)
            {
                raiz.RowStyles.Add(new RowStyle(SizeType.Percent, 42));
            }
            raiz.RowStyles.Add(new RowStyle(SizeType.Absolute, 26));
            raiz.Controls.Add(encabezado, 0, 0);
            raiz.Controls.Add(panelFiltros, 0, 1);
            raiz.Controls.Add(tarjeta, 0, 2);
            if (TieneDetalle)
            {
                raiz.Controls.Add(tarjetaDetalle, 0, 3);
            }
            raiz.Controls.Add(lblResumen, 0, TieneDetalle ? 4 : 3);
            Controls.Clear();
            Controls.Add(raiz);
            ResumeLayout(true);
        }

        private static void EstiloBoton(Button b, string texto, bool principal)
        {
            b.Text = texto;
            b.AutoSize = true;
            b.MinimumSize = new Size(110, 30);
            b.Padding = new Padding(10, 0, 10, 0);
            b.FlatStyle = FlatStyle.Flat;
            b.Cursor = Cursors.Hand;
            b.Font = new Font(Fuente, 9F, FontStyle.Bold);
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

        // Pone un filtro en el panel: etiqueta y su control, con la separacion de los demas.
        protected void Filtro(string etiqueta, Control control)
        {
            if (etiqueta != null)
            {
                Label l = EtiquetaNueva(etiqueta);
                l.Margin = new Padding(0, 8, 6, 0);
                panelFiltros.Controls.Add(l);
            }
            control.Margin = new Padding(0, 4, 16, 4);
            panelFiltros.Controls.Add(control);
        }

        protected static DateTimePicker NuevaFecha(DateTime valor)
        {
            DateTimePicker f = new DateTimePicker();
            f.Format = DateTimePickerFormat.Custom;
            f.CustomFormat = "dd/MM/yyyy";
            f.Width = 110;
            f.Value = valor;
            return f;
        }

        private void Ejecutar()
        {
            Cursor = Cursors.WaitCursor;
            try
            {
                DataTable tabla = Consultar();
                bool recortado = tabla.Rows.Count > ListadosGerenciales.LimiteFilas;
                ResultadoRecortado = recortado;
                if (recortado)
                {
                    while (tabla.Rows.Count > ListadosGerenciales.LimiteFilas)
                    {
                        tabla.Rows.RemoveAt(tabla.Rows.Count - 1);
                    }
                }
                grilla.DataSource = null;
                grilla.DataSource = tabla;
                DarFormato();
                ActualizarContador(grilla, cabeceraGrilla, TituloGrilla);
                string resumen = ResumenDe(tabla);
                if (recortado)
                {
                    resumen = (resumen == null ? "" : resumen + "   ·   ") + "Se muestran los primeros " + ListadosGerenciales.LimiteFilas.ToString("n0") +
                              " registros: acote los filtros para ver el resto.";
                }
                else if (tabla.Rows.Count == 0)
                {
                    resumen = "No hay registros con esos filtros.";
                }
                lblResumen.Text = resumen ?? "";
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, TituloPantalla, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                RegistroErrores.Registrar(ex, "Consulta de " + TituloPantalla);
                MessageBox.Show("No se pudo hacer la consulta.\n\n" + ex.Message + "\n\nQuedo registrado en el archivo de registro de errores.",
                    TituloPantalla, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void CargarDetalle()
        {
            if (!TieneDetalle)
            {
                return;
            }
            DataRowView fila = grilla.CurrentRow == null ? null : grilla.CurrentRow.DataBoundItem as DataRowView;
            if (fila == null)
            {
                grillaDetalle.DataSource = null;
                ActualizarContador(grillaDetalle, cabeceraDetalle, TituloDetalle);
                return;
            }
            try
            {
                DataTable detalle = ConsultarDetalle(fila.Row);
                while (detalle != null && detalle.Rows.Count > ListadosGerenciales.LimiteFilas)
                {
                    detalle.Rows.RemoveAt(detalle.Rows.Count - 1);
                }
                grillaDetalle.DataSource = detalle;
                DarFormatoDetalle();
                ActualizarContador(grillaDetalle, cabeceraDetalle, TituloDetalle);
            }
            catch (Exception ex)
            {
                RegistroErrores.Registrar(ex, "Detalle de " + TituloPantalla);
                grillaDetalle.DataSource = null;
            }
        }

        // Numeros negativos en rojo en las columnas con formato numerico.
        private void FormatearCelda(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0 || e.Value == null)
            {
                return;
            }
            if (e.Value is decimal && (decimal)e.Value < 0m)
            {
                e.CellStyle.ForeColor = Negativo;
            }
        }

        protected void ColumnaNumerica(string nombre, string formato, float peso)
        {
            ColumnaNumerica(grilla, nombre, formato, peso);
        }

        protected static void ColumnaNumerica(DataGridView grilla, string nombre, string formato, float peso)
        {
            if (!grilla.Columns.Contains(nombre))
            {
                return;
            }
            DataGridViewColumn c = grilla.Columns[nombre];
            c.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            c.DefaultCellStyle.Format = formato;
            c.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            c.FillWeight = peso;
        }

        protected void ColumnaFecha(string nombre, float peso)
        {
            ColumnaFecha(grilla, nombre, peso);
        }

        protected static void ColumnaFecha(DataGridView grilla, string nombre, float peso)
        {
            if (!grilla.Columns.Contains(nombre))
            {
                return;
            }
            DataGridViewColumn c = grilla.Columns[nombre];
            c.DefaultCellStyle.Format = "dd/MM/yyyy";
            c.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            c.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            c.FillWeight = peso;
        }

        protected void ColumnaTexto(string nombre, string encabezado, float peso)
        {
            ColumnaTexto(grilla, nombre, encabezado, peso);
        }

        protected static void ColumnaTexto(DataGridView grilla, string nombre, string encabezado, float peso)
        {
            if (!grilla.Columns.Contains(nombre))
            {
                return;
            }
            DataGridViewColumn c = grilla.Columns[nombre];
            if (encabezado != null)
            {
                c.HeaderText = encabezado;
            }
            c.FillWeight = peso;
        }

        // Exporta lo que se ve en la grilla a un archivo CSV (separador de la configuracion regional, para que Excel lo abra en columnas).
        private void Exportar()
        {
            if (grilla.Rows.Count == 0)
            {
                MessageBox.Show("Primero haga la consulta: no hay datos para exportar.", TituloPantalla, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            using (SaveFileDialog guardar = new SaveFileDialog())
            {
                guardar.Filter = "Archivo para Excel (*.csv)|*.csv";
                guardar.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                guardar.FileName = TituloPantalla.Replace(" ", "_") + "_" + DateTime.Now.ToString("yyyyMMdd_HHmm") + ".csv";
                if (guardar.ShowDialog(this) != DialogResult.OK)
                {
                    return;
                }
                try
                {
                    string separador = CultureInfo.CurrentCulture.TextInfo.ListSeparator;
                    using (StreamWriter escritor = new StreamWriter(guardar.FileName, false, new UTF8Encoding(true)))
                    {
                        List<string> campos = new List<string>();
                        foreach (DataGridViewColumn c in grilla.Columns)
                        {
                            if (c.Visible)
                            {
                                campos.Add(Celda(c.HeaderText, separador));
                            }
                        }
                        escritor.WriteLine(string.Join(separador, campos.ToArray()));
                        foreach (DataGridViewRow fila in grilla.Rows)
                        {
                            campos.Clear();
                            foreach (DataGridViewColumn c in grilla.Columns)
                            {
                                if (c.Visible)
                                {
                                    campos.Add(Celda(Convert.ToString(fila.Cells[c.Index].FormattedValue), separador));
                                }
                            }
                            escritor.WriteLine(string.Join(separador, campos.ToArray()));
                        }
                    }
                    if (MessageBox.Show("Archivo guardado.\n\n¿Desea abrirlo ahora?", TituloPantalla, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start(guardar.FileName);
                    }
                }
                catch (Exception ex)
                {
                    RegistroErrores.Registrar(ex, "Exportar " + TituloPantalla);
                    MessageBox.Show("No se pudo guardar el archivo.\n\n" + ex.Message, TituloPantalla, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private static string Celda(string valor, string separador)
        {
            valor = valor ?? "";
            if (valor.Contains(separador) || valor.Contains("\"") || valor.Contains("\n"))
            {
                return "\"" + valor.Replace("\"", "\"\"") + "\"";
            }
            return valor;
        }
    }
}
