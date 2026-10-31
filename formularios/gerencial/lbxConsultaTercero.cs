using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using lebrun.clases.gerencial;
using lebrun.clasesData;
using static lebrun.formularios.gerencial.EstiloGerencial;

namespace lebrun.formularios.gerencial
{
    // Base de las consultas de cliente y de proveedor: a la izquierda la busqueda (codigo, nombre, RIF) y sus resultados; a la derecha la
    // ficha de la persona seleccionada, sus cifras (saldo, vencido, documentos) y dos pestanas: documentos pendientes e historial.
    // Cada consulta indica sus busquedas y sus consultas; el estilo es el de Consulta General Art. (EstiloGerencial).
    public abstract class lbxConsultaTercero : Form
    {
        protected readonly ConsultasTerceros datos = new ConsultasTerceros();

        private readonly TextBox txtCodigo = new TextBox();
        private readonly TextBox txtNombre = new TextBox();
        private readonly TextBox txtRif = new TextBox();
        private readonly DataGridView grillaBusqueda = new DataGridView();
        private readonly DataGridView grillaPendientes = new DataGridView();
        private readonly DataGridView grillaHistorial = new DataGridView();
        private readonly Panel contenedorFicha = new Panel();
        private readonly TextBox cifraSaldo = new TextBox();
        private readonly TextBox cifraVencido = new TextBox();
        private readonly TextBox cifraDocumentos = new TextBox();
        private readonly TextBox cifraPorVencer = new TextBox();
        private readonly TabControl pestanas = new TabControl();
        private readonly Label lblAviso = new Label();
        private Label cabeceraBusqueda, cabeceraPendientes, cabeceraHistorial;
        private TableLayoutPanel cuerpoDetalle;
        private string codigoActual;
        private bool cargandoBusqueda;

        protected abstract string TituloPantalla { get; }
        protected abstract string DescripcionPantalla { get; }
        protected abstract string Persona { get; }          // "cliente" o "proveedor"
        protected abstract DataTable Buscar(string codigo, string nombre, string rif);
        protected abstract List<KeyValuePair<string, string>> Ficha(string codigo);
        protected abstract DataTable Pendientes(string codigo);
        protected abstract DataTable Historial(string codigo);

        // Ajustes de columnas de cada grilla (los nombres de columna los define la consulta).
        protected abstract void FormatoPendientes(DataGridView g);
        protected abstract void FormatoHistorial(DataGridView g);

        // Ayudas para armar la ficha: un dato con su etiqueta y la union de varios textos sin vacios.
        protected static KeyValuePair<string, string> Dato(string etiqueta, object valor)
        {
            return new KeyValuePair<string, string>(etiqueta, valor == null || valor == DBNull.Value ? "" : Convert.ToString(valor).Trim());
        }

        protected static string Unir(params object[] partes)
        {
            List<string> textos = new List<string>();
            foreach (object parte in partes)
            {
                string texto = parte == null || parte == DBNull.Value ? "" : Convert.ToString(parte).Trim();
                if (texto.Length > 0)
                {
                    textos.Add(texto);
                }
            }
            return string.Join(" ", textos.ToArray());
        }

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
            ClientSize = new Size(Math.Min(1250, area.Width - 60), Math.Min(740, area.Height - 130));
            MinimumSize = new Size(960, 560);
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
            encabezado.Controls.Add(titulo, 0, 0);
            encabezado.Controls.Add(descripcion, 0, 1);

            TableLayoutPanel cuerpo = Columnas(new float[] { 34, 66 });
            cuerpo.Controls.Add(ConstruirBusqueda(), 0, 0);
            cuerpo.Controls.Add(ConstruirDetalle(), 1, 0);

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
            MostrarAviso("Busque " + ArticuloPersona() + " y selecciónelo en la lista.");
            ResumeLayout(true);
        }

        private string ArticuloPersona()
        {
            return Persona == "cliente" ? "un cliente" : "un proveedor";
        }

        // ---------- busqueda ----------

        private Control ConstruirBusqueda()
        {
            TableLayoutPanel filtros = new TableLayoutPanel();
            filtros.Dock = DockStyle.Top;
            filtros.AutoSize = true;
            filtros.BackColor = Color.White;
            filtros.ColumnCount = 2;
            filtros.RowCount = 4;
            filtros.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            filtros.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            for (int i = 0; i < 4; i++)
            {
                filtros.RowStyles.Add(new RowStyle(SizeType.Absolute, 32));
            }
            AgregarCampoBusqueda(filtros, 0, "Código", txtCodigo);
            AgregarCampoBusqueda(filtros, 1, "Nombre", txtNombre);
            AgregarCampoBusqueda(filtros, 2, "RIF", txtRif);
            Button buscar = new Button();
            buscar.Text = "Buscar";
            buscar.AutoSize = true;
            buscar.MinimumSize = new Size(100, 30);
            buscar.FlatStyle = FlatStyle.Flat;
            buscar.Cursor = Cursors.Hand;
            buscar.Font = new Font(Fuente, 9F, FontStyle.Bold);
            buscar.BackColor = Azul;
            buscar.ForeColor = Color.White;
            buscar.FlatAppearance.BorderSize = 0;
            buscar.Margin = new Padding(0, 2, 0, 0);
            buscar.Click += delegate { EjecutarBusqueda(); };
            filtros.Controls.Add(buscar, 1, 3);
            AcceptButton = buscar;

            grillaBusqueda.AllowUserToAddRows = false;
            grillaBusqueda.AllowUserToDeleteRows = false;
            grillaBusqueda.ReadOnly = true;
            grillaBusqueda.MultiSelect = false;
            grillaBusqueda.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grillaBusqueda.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grillaBusqueda.Margin = new Padding(0);
            EstiloGrilla(grillaBusqueda);
            grillaBusqueda.SelectionChanged += delegate { SeleccionCambio(); };

            TableLayoutPanel interior = new TableLayoutPanel();
            interior.Dock = DockStyle.Fill;
            interior.BackColor = Color.White;
            interior.ColumnCount = 1;
            interior.RowCount = 2;
            interior.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            interior.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            interior.Controls.Add(filtros, 0, 0);
            Control resultados = Tarjeta("Resultados", grillaBusqueda, out cabeceraBusqueda);
            resultados.Margin = new Padding(0, 8, 0, 0);
            resultados.Padding = new Padding(0, 4, 0, 0);
            interior.Controls.Add(resultados, 0, 1);
            return Tarjeta("Buscar " + Persona, interior);
        }

        private void AgregarCampoBusqueda(TableLayoutPanel t, int fila, string etiqueta, TextBox caja)
        {
            Label l = EtiquetaNueva(etiqueta);
            l.Margin = new Padding(0, 0, 10, 0);
            caja.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            caja.Margin = new Padding(0, 4, 0, 4);
            t.Controls.Add(l, 0, fila);
            t.Controls.Add(caja, 1, fila);
        }

        private void EjecutarBusqueda()
        {
            if (string.IsNullOrWhiteSpace(txtCodigo.Text) && string.IsNullOrWhiteSpace(txtNombre.Text) && string.IsNullOrWhiteSpace(txtRif.Text))
            {
                MessageBox.Show("Escriba el código, parte del nombre o el RIF para buscar.", TituloPantalla, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            Cursor = Cursors.WaitCursor;
            try
            {
                DataTable tabla = Buscar(txtCodigo.Text, txtNombre.Text, txtRif.Text);
                bool recortado = tabla.Rows.Count > ConsultasTerceros.LimiteFilas;
                while (tabla.Rows.Count > ConsultasTerceros.LimiteFilas)
                {
                    tabla.Rows.RemoveAt(tabla.Rows.Count - 1);
                }
                cargandoBusqueda = true;
                grillaBusqueda.DataSource = null;
                grillaBusqueda.DataSource = tabla;
                ColumnaTextoBusqueda("Codigo", "Código", 30);
                ColumnaTextoBusqueda("Nombre", null, 52);
                ColumnaTextoBusqueda("Rif", "RIF", 18);
                cargandoBusqueda = false;
                ActualizarContador(grillaBusqueda, cabeceraBusqueda, "Resultados");
                if (recortado)
                {
                    cabeceraBusqueda.Text = "Resultados  ·  primeros " + ConsultasTerceros.LimiteFilas + " (afine la búsqueda)";
                }
                if (tabla.Rows.Count == 0)
                {
                    LimpiarDetalle();
                    MostrarAviso("No se encontró ningún " + Persona + " con esos datos.");
                }
                else
                {
                    SeleccionCambio();
                }
            }
            catch (Exception ex)
            {
                cargandoBusqueda = false;
                RegistroErrores.Registrar(ex, "Busqueda en " + TituloPantalla);
                MessageBox.Show("No se pudo hacer la búsqueda.\n\n" + ex.Message, TituloPantalla, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void ColumnaTextoBusqueda(string nombre, string encabezado, float peso)
        {
            if (grillaBusqueda.Columns.Contains(nombre))
            {
                if (encabezado != null)
                {
                    grillaBusqueda.Columns[nombre].HeaderText = encabezado;
                }
                grillaBusqueda.Columns[nombre].FillWeight = peso;
            }
        }

        // ---------- detalle ----------

        private Control ConstruirDetalle()
        {
            lblAviso.AutoSize = false;
            lblAviso.Dock = DockStyle.Fill;
            lblAviso.TextAlign = ContentAlignment.MiddleCenter;
            lblAviso.ForeColor = TextoSecundario;
            lblAviso.Font = new Font(Fuente, 11F);
            lblAviso.BackColor = Color.White;

            // ficha
            contenedorFicha.Dock = DockStyle.Fill;
            contenedorFicha.BackColor = Color.White;
            Control tarjetaFicha = Tarjeta("Datos del " + Persona, contenedorFicha);
            tarjetaFicha.Margin = new Padding(0, 0, 0, 10);

            // cifras
            TableLayoutPanel cifras = Columnas(new float[] { 25, 25, 25, 25 });
            cifras.Controls.Add(Indicador(new Label(), "Saldo pendiente", cifraSaldo), 0, 0);
            cifras.Controls.Add(Indicador(new Label(), "Vencido", cifraVencido), 1, 0);
            cifras.Controls.Add(Indicador(new Label(), "Por vencer", cifraPorVencer), 2, 0);
            cifras.Controls.Add(Indicador(new Label(), "Documentos pendientes", cifraDocumentos), 3, 0);

            // pestanas
            ConfigurarGrilla(grillaPendientes);
            ConfigurarGrilla(grillaHistorial);
            grillaPendientes.CellFormatting += ResaltarVencidos;
            TabPage pagPendientes = new TabPage("Pendientes");
            TabPage pagHistorial = new TabPage("Historial");
            PaginaConContenido(pagPendientes, Tarjeta("Documentos con saldo pendiente", grillaPendientes, out cabeceraPendientes), new Size(400, 200));
            PaginaConContenido(pagHistorial, Tarjeta("Historial de documentos", grillaHistorial, out cabeceraHistorial), new Size(400, 200));
            pestanas.TabPages.Add(pagPendientes);
            pestanas.TabPages.Add(pagHistorial);
            EstiloPestanas(pestanas);
            pestanas.ItemSize = new Size(130, 30);

            TableLayoutPanel t = new TableLayoutPanel();
            cuerpoDetalle = t;
            t.Dock = DockStyle.Fill;
            t.BackColor = Fondo;
            t.Margin = new Padding(0);
            t.Padding = new Padding(10, 0, 0, 0);
            t.ColumnCount = 1;
            t.RowCount = 4;
            t.RowStyles.Add(new RowStyle(SizeType.Absolute, 168));
            t.RowStyles.Add(new RowStyle(SizeType.Absolute, 80));
            t.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            t.Controls.Add(tarjetaFicha, 0, 0);
            t.Controls.Add(cifras, 0, 1);
            pestanas.Dock = DockStyle.Fill;
            pestanas.Margin = new Padding(0);
            t.Controls.Add(pestanas, 0, 2);

            // el aviso se muestra encima del detalle mientras no haya una persona seleccionada
            Panel contenedor = new Panel();
            contenedor.Dock = DockStyle.Fill;
            contenedor.Padding = new Padding(0);
            contenedor.Controls.Add(lblAviso);
            contenedor.Controls.Add(t);
            lblAviso.BringToFront();
            return contenedor;
        }

        private static void ConfigurarGrilla(DataGridView g)
        {
            g.AllowUserToAddRows = false;
            g.AllowUserToDeleteRows = false;
            g.ReadOnly = true;
            g.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            g.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            g.Margin = new Padding(0);
            EstiloGrilla(g);
        }

        private void MostrarAviso(string texto)
        {
            lblAviso.Text = texto;
            lblAviso.Visible = true;
            lblAviso.BringToFront();
        }

        private void LimpiarDetalle()
        {
            codigoActual = null;
            grillaPendientes.DataSource = null;
            grillaHistorial.DataSource = null;
            contenedorFicha.Controls.Clear();
        }

        private void SeleccionCambio()
        {
            if (cargandoBusqueda)
            {
                return;
            }
            DataGridViewRow fila = grillaBusqueda.CurrentRow;
            if (fila == null || grillaBusqueda.Rows.Count == 0)
            {
                return;
            }
            string codigo = Convert.ToString(fila.Cells["Codigo"].Value);
            if (codigo == codigoActual)
            {
                return;
            }
            CargarDetalle(codigo);
        }

        private void CargarDetalle(string codigo)
        {
            Cursor = Cursors.WaitCursor;
            try
            {
                codigoActual = codigo;
                MostrarFicha(Ficha(codigo));

                DataTable pendientes = Pendientes(codigo);
                CalcularVencimientos(pendientes);
                grillaPendientes.DataSource = pendientes;
                FormatoPendientes(grillaPendientes);
                ActualizarContador(grillaPendientes, cabeceraPendientes, "Documentos con saldo pendiente");
                MostrarCifras(pendientes);

                DataTable historial = Historial(codigo);
                bool recortado = historial.Rows.Count > ConsultasTerceros.LimiteFilas * 4;
                while (historial.Rows.Count > ConsultasTerceros.LimiteFilas * 4)
                {
                    historial.Rows.RemoveAt(historial.Rows.Count - 1);
                }
                grillaHistorial.DataSource = historial;
                FormatoHistorial(grillaHistorial);
                ActualizarContador(grillaHistorial, cabeceraHistorial, "Historial de documentos");
                if (recortado)
                {
                    cabeceraHistorial.Text += "  ·  los más recientes";
                }
                lblAviso.Visible = false;
            }
            catch (Exception ex)
            {
                RegistroErrores.Registrar(ex, "Detalle en " + TituloPantalla);
                MessageBox.Show("No se pudo cargar la información.\n\n" + ex.Message, TituloPantalla, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        // Agrega los dias de atraso de cada documento pendiente (0 si no esta vencido o no tiene fecha de vencimiento).
        private static void CalcularVencimientos(DataTable pendientes)
        {
            pendientes.Columns.Add("DiasVencido", typeof(int));
            foreach (DataRow fila in pendientes.Rows)
            {
                int dias = 0;
                if (fila["Vencimiento"] != DBNull.Value)
                {
                    dias = (int)(DateTime.Today - Convert.ToDateTime(fila["Vencimiento"]).Date).TotalDays;
                    if (dias < 0)
                    {
                        dias = 0;
                    }
                }
                fila["DiasVencido"] = dias;
            }
        }

        private void MostrarCifras(DataTable pendientes)
        {
            decimal total = 0m, vencido = 0m;
            foreach (DataRow fila in pendientes.Rows)
            {
                decimal saldo = fila["Saldo"] == DBNull.Value ? 0m : Convert.ToDecimal(fila["Saldo"]);
                total += saldo;
                if (Convert.ToInt32(fila["DiasVencido"]) > 0)
                {
                    vencido += saldo;
                }
            }
            cifraSaldo.Text = total.ToString("n2");
            cifraVencido.Text = vencido.ToString("n2");
            cifraVencido.ForeColor = vencido > 0m ? Negativo : Azul;
            cifraPorVencer.Text = (total - vencido).ToString("n2");
            cifraDocumentos.Text = pendientes.Rows.Count.ToString("n0");
        }

        // Los dias vencidos y el saldo de los documentos vencidos se resaltan en rojo.
        private void ResaltarVencidos(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0 || !grillaPendientes.Columns.Contains("DiasVencido"))
            {
                return;
            }
            string columna = grillaPendientes.Columns[e.ColumnIndex].Name;
            if (columna == "DiasVencido" || columna == "Vencimiento")
            {
                object dias = grillaPendientes.Rows[e.RowIndex].Cells["DiasVencido"].Value;
                if (dias != null && dias != DBNull.Value && Convert.ToInt32(dias) > 0)
                {
                    e.CellStyle.ForeColor = Negativo;
                }
            }
            if (columna == "DiasVencido" && e.Value != null && e.Value != DBNull.Value && Convert.ToInt32(e.Value) == 0)
            {
                e.Value = "";
                e.FormattingApplied = true;
            }
        }

        // Ficha: pares etiqueta/valor en dos columnas de pares.
        private void MostrarFicha(List<KeyValuePair<string, string>> datosFicha)
        {
            contenedorFicha.Controls.Clear();
            int filas = (datosFicha.Count + 1) / 2;
            TableLayoutPanel t = Rejilla(2, filas);
            for (int i = 0; i < datosFicha.Count; i++)
            {
                int par = i / filas;
                int fila = i % filas;
                TextBox valor = new TextBox();
                valor.Name = "txtFicha" + i;
                CamposTexto.Add(valor.Name);
                valor.Text = datosFicha[i].Value ?? "";
                Par(t, fila, par, EtiquetaNueva(datosFicha[i].Key), null, valor);
            }
            contenedorFicha.Controls.Add(t);
            // la tarjeta de la ficha crece segun las filas que tenga
            cuerpoDetalle.RowStyles[0].Height = Math.Max(2, filas) * AltoFila + 70;
        }
    }
}
