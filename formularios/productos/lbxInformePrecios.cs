using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows.Forms;
using lebrun.clasesData;
using static lebrun.formularios.gerencial.EstiloGerencial;

namespace lebrun.formularios.productos
{
    // Informe de Precios (Inventario > Informes > Informe de Precios): el listado de precios de los productos, en dolares o en bolivares por separado
    // (cuando se cierra temprano la facturacion se entregan los precios en dolares y los bolivares quedan para la actualizacion masiva).
    //   - Dolares: el precio en dolares de cada producto (adminv2.pre_ext).
    //   - Bolivares: los precios vigentes (admprecios con pre_act = 1) por lista de precio y unidad, con la fecha, hora y usuario del ultimo cambio.
    // Se puede filtrar por proveedor (adminvparam), mostrar solo los productos con precio o sin precio, incluir los inactivos y, en bolivares,
    // elegir la lista de precio y ver solo lo cambiado desde una fecha y hora. Se puede imprimir y exportar a un archivo que abre Excel.
    // Es solo de consulta: no cambia nada en la base de datos.
    public class lbxInformePrecios : Form
    {
        private const int MaximoFilas = 60000;

        private readonly ConexionBD baseDatos = new ConexionBD();
        private readonly DataGridView grilla = new DataGridView();
        private Label cabeceraGrilla;
        private readonly ComboBox cboMoneda = new ComboBox();
        private readonly ComboBox cboLista = new ComboBox();
        private readonly TextBox txtProveedor = new TextBox();
        private readonly ComboBox cboMostrar = new ComboBox();
        private readonly CheckBox chkInactivos = new CheckBox();
        private readonly DateTimePicker dtpDesde = new DateTimePicker();
        private readonly Button cmdBuscar = new Button();
        private readonly Button cmdLimpiar = new Button();
        private readonly Button cmdExportar = new Button();
        private readonly Button cmdImprimir = new Button();
        private readonly Button cmdCerrar = new Button();
        private readonly Label lblResumen = new Label();
        private readonly Label lblAviso = new Label();

        private bool desdeMarcado;
        private DataTable resultado;
        private bool resultadoEnDolares;
        private string subtituloInforme = "";
        private int filaImpresion;
        private int paginaImpresion;

        protected override void OnLoad(EventArgs e)
        {
            Armar();
            CargarListas();
            base.OnLoad(e);
            cboMoneda.Focus();
        }

        // ---------------------------------------------------------------- diseno

        private void Armar()
        {
            SuspendLayout();
            AutoScaleMode = AutoScaleMode.None;
            Font = new Font(Fuente, 9F);
            BackColor = Fondo;
            Text = "Informe de Precios";
            Rectangle area = Screen.PrimaryScreen.WorkingArea;
            ClientSize = new Size(Math.Min(1180, area.Width - 60), Math.Min(700, area.Height - 100));
            MinimumSize = new Size(1000, 540);
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
            titulo.Text = "Informe de Precios";
            titulo.Dock = DockStyle.Fill;
            titulo.Font = new Font(Fuente, 15F, FontStyle.Bold);
            titulo.ForeColor = Azul;
            Label descripcion = new Label();
            descripcion.Text = "Precios de los productos en dólares o en bolívares, por separado, con filtro por proveedor y por con precio / sin precio.";
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
            grilla.CellFormatting += FormatearCelda;
            Control tarjetaGrilla = Tarjeta("Precios", grilla, out cabeceraGrilla);
            tarjetaGrilla.Dock = DockStyle.Fill;
            tarjetaGrilla.Margin = new Padding(0, 0, 0, 6);

            lblResumen.Dock = DockStyle.Fill;
            lblResumen.TextAlign = ContentAlignment.MiddleRight;
            lblResumen.Font = new Font(Fuente, 10F, FontStyle.Bold);
            lblResumen.ForeColor = Azul;
            lblAviso.Dock = DockStyle.Fill;
            lblAviso.TextAlign = ContentAlignment.MiddleLeft;
            lblAviso.ForeColor = Negativo;

            TableLayoutPanel pie = new TableLayoutPanel();
            pie.Dock = DockStyle.Fill;
            pie.ColumnCount = 2;
            pie.RowCount = 1;
            pie.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 55));
            pie.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45));
            pie.Controls.Add(lblAviso, 0, 0);
            pie.Controls.Add(lblResumen, 1, 0);

            TableLayoutPanel raiz = new TableLayoutPanel();
            raiz.Dock = DockStyle.Fill;
            raiz.ColumnCount = 1;
            raiz.RowCount = 4;
            raiz.Padding = new Padding(12, 10, 12, 8);
            raiz.RowStyles.Add(new RowStyle(SizeType.Absolute, 74));
            raiz.RowStyles.Add(new RowStyle(SizeType.Absolute, 104));
            raiz.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            raiz.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));
            raiz.Controls.Add(encabezado, 0, 0);
            raiz.Controls.Add(ArmarFiltros(), 0, 1);
            raiz.Controls.Add(tarjetaGrilla, 0, 2);
            raiz.Controls.Add(pie, 0, 3);
            Controls.Clear();
            Controls.Add(raiz);
            ResumeLayout(true);
        }

        private Control ArmarFiltros()
        {
            cboMoneda.DropDownStyle = ComboBoxStyle.DropDownList;
            cboMoneda.Items.AddRange(new object[] { "Dólares ($)", "Bolívares (Bs)" });
            cboMoneda.SelectedIndex = 0;
            cboMoneda.Width = 130;
            cboMoneda.SelectedIndexChanged += delegate { ActualizarFiltros(); };

            cboLista.DropDownStyle = ComboBoxStyle.DropDownList;
            cboLista.Width = 90;

            txtProveedor.Width = 190;
            txtProveedor.MaxLength = 60;
            txtProveedor.KeyDown += delegate (object s, KeyEventArgs ev)
            {
                if (ev.KeyCode == Keys.Enter)
                {
                    ev.SuppressKeyPress = true;
                    Buscar();
                }
            };

            cboMostrar.DropDownStyle = ComboBoxStyle.DropDownList;
            cboMostrar.Items.AddRange(new object[] { "Todos", "Con precio", "Sin precio" });
            cboMostrar.SelectedIndex = 0;
            cboMostrar.Width = 110;

            chkInactivos.Text = "Incluir inactivos";
            chkInactivos.AutoSize = true;

            dtpDesde.Format = DateTimePickerFormat.Custom;
            dtpDesde.CustomFormat = "dd/MM/yyyy hh:mm tt";
            dtpDesde.ShowCheckBox = true;
            dtpDesde.Checked = false;
            dtpDesde.Width = 190;
            // al marcar la casilla se propone desde el comienzo del dia: con la hora actual no saldria nada de lo cambiado hasta ese momento
            dtpDesde.ValueChanged += delegate
            {
                bool recienMarcada = dtpDesde.Checked && !desdeMarcado;
                desdeMarcado = dtpDesde.Checked;
                if (recienMarcada)
                {
                    dtpDesde.Value = dtpDesde.Value.Date;
                }
            };

            EstiloBoton(cmdBuscar, "Buscar", true);
            EstiloBoton(cmdLimpiar, "Limpiar", false);
            EstiloBoton(cmdImprimir, "Imprimir", false);
            EstiloBoton(cmdExportar, "Exportar", false);
            EstiloBoton(cmdCerrar, "Cerrar", false);
            cmdBuscar.Click += delegate { Buscar(); };
            cmdLimpiar.Click += delegate { Limpiar(); };
            cmdImprimir.Click += delegate { Imprimir(); };
            cmdExportar.Click += delegate { Exportar(); };
            cmdCerrar.Click += delegate { Close(); };
            cmdImprimir.Enabled = false;
            cmdExportar.Enabled = false;

            FlowLayoutPanel linea1 = Linea();
            linea1.Controls.Add(Alinear(EtiquetaNueva("Precios en")));
            linea1.Controls.Add(cboMoneda);
            linea1.Controls.Add(Alinear(EtiquetaNueva("Lista de precio")));
            linea1.Controls.Add(cboLista);
            linea1.Controls.Add(Alinear(EtiquetaNueva("Proveedor (código o nombre)")));
            linea1.Controls.Add(txtProveedor);
            linea1.Controls.Add(Alinear(EtiquetaNueva("Mostrar")));
            linea1.Controls.Add(cboMostrar);
            chkInactivos.Margin = new Padding(14, 6, 0, 0);
            linea1.Controls.Add(chkInactivos);

            FlowLayoutPanel linea2 = Linea();
            linea2.Controls.Add(Alinear(EtiquetaNueva("Cambiados desde (solo bolívares)")));
            linea2.Controls.Add(dtpDesde);
            cmdBuscar.Margin = new Padding(24, 0, 0, 0);
            linea2.Controls.Add(cmdBuscar);
            linea2.Controls.Add(cmdLimpiar);
            linea2.Controls.Add(cmdImprimir);
            linea2.Controls.Add(cmdExportar);
            linea2.Controls.Add(cmdCerrar);

            TableLayoutPanel f = new TableLayoutPanel();
            f.Dock = DockStyle.Fill;
            f.BackColor = Color.White;
            f.Padding = new Padding(6, 6, 6, 2);
            f.Margin = new Padding(0, 0, 0, 10);
            f.ColumnCount = 1;
            f.RowCount = 2;
            f.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
            f.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
            ConBorde(f);
            f.Controls.Add(linea1, 0, 0);
            f.Controls.Add(linea2, 0, 1);
            return f;
        }

        private static FlowLayoutPanel Linea()
        {
            FlowLayoutPanel l = new FlowLayoutPanel();
            l.Dock = DockStyle.Fill;
            l.WrapContents = false;
            l.BackColor = Color.White;
            l.Margin = new Padding(0);
            return l;
        }

        private static Label Alinear(Label l)
        {
            l.Margin = new Padding(8, 8, 4, 0);
            return l;
        }

        private static void EstiloBoton(Button b, string texto, bool principal)
        {
            b.Text = texto;
            b.AutoSize = true;
            b.MinimumSize = new Size(90, 28);
            b.FlatStyle = FlatStyle.Flat;
            b.Cursor = Cursors.Hand;
            b.Font = new Font(Fuente, 9F, FontStyle.Bold);
            b.Margin = new Padding(6, 0, 0, 0);
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

        // ---------------------------------------------------------------- filtros

        private bool EnDolares
        {
            get { return cboMoneda.SelectedIndex == 0; }
        }

        // Las listas de precio y la fecha de cambio solo existen en bolivares (el precio en dolares es uno por producto).
        private void ActualizarFiltros()
        {
            cboLista.Enabled = !EnDolares;
            dtpDesde.Enabled = !EnDolares;
            if (EnDolares)
            {
                dtpDesde.Checked = false;
                desdeMarcado = false;
            }
        }

        private void CargarListas()
        {
            cboLista.Items.Clear();
            cboLista.Items.Add("Todas");
            try
            {
                DataTable dt = baseDatos.fDataTable("SELECT DISTINCT pre_lista FROM admprecios WHERE pre_act = '1' AND pre_codmon = 'Bs' ORDER BY pre_lista");
                baseDatos.cerrarConexion();
                foreach (DataRow r in dt.Rows)
                {
                    string lista = Convert.ToString(r[0]).Trim();
                    if (lista.Length > 0)
                    {
                        cboLista.Items.Add(lista);
                    }
                }
            }
            catch (Exception ex)
            {
                RegistroErrores.Registrar(ex, "Informe de precios: listas de precio");
            }
            cboLista.SelectedIndex = 0;
            ActualizarFiltros();
        }

        private void Limpiar()
        {
            cboMoneda.SelectedIndex = 0;
            cboLista.SelectedIndex = 0;
            txtProveedor.Clear();
            cboMostrar.SelectedIndex = 0;
            chkInactivos.Checked = false;
            dtpDesde.Checked = false;
            desdeMarcado = false;
            grilla.DataSource = null;
            resultado = null;
            lblResumen.Text = "";
            lblAviso.Text = "";
            cmdImprimir.Enabled = false;
            cmdExportar.Enabled = false;
            ActualizarContador(grilla, cabeceraGrilla, "Precios");
            cboMoneda.Focus();
        }

        // ---------------------------------------------------------------- consulta

        private static string Texto(string valor)
        {
            return (valor ?? "").Trim().Replace("\\", "\\\\").Replace("'", "''");
        }

        // para LIKE: los comodines % y _ escritos por el usuario se toman como texto
        private static string ParaLike(string valorEscapado)
        {
            return valorEscapado.Replace("%", "\\%").Replace("_", "\\_");
        }

        private string FiltroComun()
        {
            string filtro = "";
            if (!chkInactivos.Checked)
            {
                filtro += " AND i.inv_estado = 'Activo'";
            }
            string prov = Texto(txtProveedor.Text);
            if (prov.Length > 0)
            {
                filtro += " AND (pa.pin_proveedor = '" + prov + "' OR p.pro_nombre LIKE '%" + ParaLike(prov) + "%')";
            }
            return filtro;
        }

        private string SqlDolares()
        {
            string filtro = FiltroComun();
            if (cboMostrar.SelectedIndex == 1)
            {
                filtro += " AND IFNULL(i2.pre_ext, 0) > 0";
            }
            else if (cboMostrar.SelectedIndex == 2)
            {
                filtro += " AND IFNULL(i2.pre_ext, 0) = 0";
            }
            return "SELECT i.inv_codigo AS codigo, i.inv_descri AS descripcion, IFNULL(p.pro_nombre, '') AS proveedor, IFNULL(i2.pre_ext, 0) AS precio " +
                   "FROM adminv i LEFT JOIN adminv2 i2 ON i2.inv2_codigo = i.inv_codigo " +
                   "LEFT JOIN adminvparam pa ON pa.pin_codigo = i.inv_codigo LEFT JOIN admproveedor p ON p.pro_codigo = pa.pin_proveedor " +
                   "WHERE 1 = 1" + filtro + " ORDER BY proveedor, i.inv_descri LIMIT " + (MaximoFilas + 1);
        }

        private string SqlBolivares()
        {
            string filtro = FiltroComun();
            string lista = cboLista.SelectedIndex > 0 ? " AND a.pre_lista = '" + Texto(Convert.ToString(cboLista.SelectedItem)) + "'" : "";
            if (cboMostrar.SelectedIndex == 1)
            {
                filtro += " AND a.pre_precio > 0";
            }
            else if (cboMostrar.SelectedIndex == 2)
            {
                filtro += " AND (a.pre_codigo IS NULL OR a.pre_precio = 0)";
            }
            if (dtpDesde.Checked)
            {
                // fecha y hora del cambio
                filtro += " AND TIMESTAMP(a.pre_fecha, " + lebrun.clases.complementos.HoraGuardada.Sql("a.pre_hora") + ") >= '" +
                          dtpDesde.Value.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture) + "'";
            }
            return "SELECT i.inv_codigo AS codigo, i.inv_descri AS descripcion, a.pre_lista AS lista, a.pre_undmed AS unidad, IFNULL(p.pro_nombre, '') AS proveedor, " +
                   "a.pre_precio AS precio, IF(a.pre_fecha > '1900-01-01', a.pre_fecha, NULL) AS fecha, NULLIF(a.pre_hora, '') AS hora, NULLIF(a.pre_usuario, '') AS usuario " +
                   "FROM adminv i LEFT JOIN admprecios a ON a.pre_codigo = i.inv_codigo AND a.pre_act = '1' AND a.pre_codmon = 'Bs'" + lista + " " +
                   "LEFT JOIN adminvparam pa ON pa.pin_codigo = i.inv_codigo LEFT JOIN admproveedor p ON p.pro_codigo = pa.pin_proveedor " +
                   "WHERE 1 = 1" + filtro + " ORDER BY proveedor, i.inv_descri, a.pre_lista, a.pre_undmed LIMIT " + (MaximoFilas + 1);
        }

        private void Buscar()
        {
            lblAviso.Text = "";
            try
            {
                Cursor = Cursors.WaitCursor;
                bool dolares = EnDolares;
                DataTable dt = baseDatos.fDataTable(dolares ? SqlDolares() : SqlBolivares());
                baseDatos.cerrarConexion();
                bool recortado = dt.Rows.Count > MaximoFilas;
                if (recortado)
                {
                    dt.Rows.RemoveAt(dt.Rows.Count - 1);
                    lblAviso.Text = "Se muestran los primeros " + MaximoFilas + " registros: use los filtros para acotar.";
                }
                resultado = dt;
                resultadoEnDolares = dolares;
                subtituloInforme = Subtitulo();
                grilla.DataSource = dt;
                Encabezados(dolares);
                ActualizarContador(grilla, cabeceraGrilla, "Precios en " + (dolares ? "dólares" : "bolívares"));
                MostrarResumen(dt);
                cmdImprimir.Enabled = dt.Rows.Count > 0;
                cmdExportar.Enabled = dt.Rows.Count > 0;
                if (dt.Rows.Count == 0)
                {
                    lblAviso.Text = "No hay productos con ese filtro.";
                }
            }
            catch (Exception ex)
            {
                RegistroErrores.Registrar(ex, "Informe de precios");
                MessageBox.Show("No se pudo consultar el informe de precios.\n\n" + ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        // Resumen de los filtros, para el encabezado del informe impreso o exportado.
        private string Subtitulo()
        {
            List<string> partes = new List<string>();
            partes.Add(EnDolares ? "Precios en dólares" : "Precios en bolívares");
            if (!EnDolares && cboLista.SelectedIndex > 0)
            {
                partes.Add("lista " + cboLista.SelectedItem);
            }
            if (txtProveedor.Text.Trim().Length > 0)
            {
                partes.Add("proveedor: " + txtProveedor.Text.Trim());
            }
            if (cboMostrar.SelectedIndex > 0)
            {
                partes.Add(cboMostrar.SelectedItem.ToString().ToLower());
            }
            if (!EnDolares && dtpDesde.Checked)
            {
                partes.Add("cambiados desde " + dtpDesde.Value.ToString("dd/MM/yyyy hh:mm tt", CultureInfo.CurrentCulture));
            }
            if (chkInactivos.Checked)
            {
                partes.Add("incluye inactivos");
            }
            return string.Join(" · ", partes.ToArray());
        }

        private void MostrarResumen(DataTable dt)
        {
            int con = 0;
            foreach (DataRow r in dt.Rows)
            {
                if (r["precio"] != DBNull.Value && Convert.ToDecimal(r["precio"]) > 0m)
                {
                    con++;
                }
            }
            lblResumen.Text = dt.Rows.Count + " registros     Con precio " + con + "     Sin precio " + (dt.Rows.Count - con);
        }

        // ---------------------------------------------------------------- presentacion

        private void Encabezados(bool dolares)
        {
            Col("codigo", "Código", dolares ? 14 : 12);
            Col("descripcion", "Descripción", dolares ? 46 : 32);
            Col("proveedor", "Proveedor", dolares ? 26 : 20);
            Col("precio", dolares ? "Precio $" : "Precio Bs", 12, "n2", true);
            if (!dolares)
            {
                Col("lista", "Lista", 5);
                Col("unidad", "Unidad", 7);
                Col("fecha", "Fecha cambio", 9, "dd/MM/yyyy");
                Col("hora", "Hora", 9);
                Col("usuario", "Usuario", 8);
                grilla.Columns["lista"].DisplayIndex = 2;
                grilla.Columns["unidad"].DisplayIndex = 3;
                grilla.Columns["proveedor"].DisplayIndex = 4;
            }
        }

        private void Col(string nombre, string encabezado, float peso, string formato = null, bool derecha = false)
        {
            DataGridViewColumn c = grilla.Columns[nombre];
            c.HeaderText = encabezado;
            c.FillWeight = peso;
            if (formato != null)
            {
                c.DefaultCellStyle.Format = formato;
            }
            if (derecha)
            {
                c.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                c.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
        }

        private void FormatearCelda(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0 || grilla.Columns[e.ColumnIndex].Name != "precio")
            {
                return;
            }
            decimal valor;
            if (e.Value == null || e.Value == DBNull.Value || !decimal.TryParse(Convert.ToString(e.Value, CultureInfo.InvariantCulture), NumberStyles.Any, CultureInfo.InvariantCulture, out valor) || valor == 0m)
            {
                e.Value = "Sin precio";
                e.CellStyle.ForeColor = Negativo;
                e.FormattingApplied = true;
            }
        }

        // ---------------------------------------------------------------- exportar

        // Archivo de texto con las columnas separadas por punto y coma y codificacion UTF-8: Excel lo abre con doble clic.
        private void Exportar()
        {
            if (resultado == null || resultado.Rows.Count == 0)
            {
                return;
            }
            using (SaveFileDialog guardar = new SaveFileDialog())
            {
                guardar.Filter = "Archivo para Excel (*.csv)|*.csv";
                guardar.FileName = "Precios_" + (resultadoEnDolares ? "Dolares" : "Bolivares") + "_" + DateTime.Now.ToString("yyyyMMdd_HHmm");
                if (guardar.ShowDialog(this) != DialogResult.OK)
                {
                    return;
                }
                try
                {
                    List<DataGridViewColumn> columnas = ColumnasVisibles();
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine(Csv("Informe de Precios") + ";" + Csv(subtituloInforme) + ";" + Csv(DateTime.Now.ToString("dd/MM/yyyy hh:mm tt", CultureInfo.CurrentCulture)));
                    List<string> cab = new List<string>();
                    foreach (DataGridViewColumn c in columnas)
                    {
                        cab.Add(Csv(c.HeaderText));
                    }
                    sb.AppendLine(string.Join(";", cab.ToArray()));
                    foreach (DataGridViewRow fila in grilla.Rows)
                    {
                        List<string> celdas = new List<string>();
                        foreach (DataGridViewColumn c in columnas)
                        {
                            string valor = ValorExportado(fila, c);
                            // el codigo del producto empieza con ceros: ="000..." hace que Excel lo deje como texto y no lo convierta en numero
                            celdas.Add(Csv(c.Name == "codigo" ? "=\"" + valor + "\"" : valor));
                        }
                        sb.AppendLine(string.Join(";", celdas.ToArray()));
                    }
                    File.WriteAllText(guardar.FileName, sb.ToString(), new UTF8Encoding(true));
                    MessageBox.Show(this, "Archivo guardado:\n" + guardar.FileName, Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    RegistroErrores.Registrar(ex, "Informe de precios: exportar");
                    MessageBox.Show(this, "No se pudo guardar el archivo.\n\n" + ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private List<DataGridViewColumn> ColumnasVisibles()
        {
            List<DataGridViewColumn> lista = new List<DataGridViewColumn>();
            DataGridViewColumn c = grilla.Columns.GetFirstColumn(DataGridViewElementStates.Visible);
            while (c != null)
            {
                lista.Add(c);
                c = grilla.Columns.GetNextColumn(c, DataGridViewElementStates.Visible, DataGridViewElementStates.None);
            }
            return lista;
        }

        // Valor tal como se ve en pantalla (Sin precio, fechas, 2 decimales).
        private static string ValorExportado(DataGridViewRow fila, DataGridViewColumn c)
        {
            object v = fila.Cells[c.Index].FormattedValue;
            return v == null ? "" : Convert.ToString(v);
        }

        private static string Csv(string valor)
        {
            valor = valor ?? "";
            if (valor.IndexOfAny(new[] { ';', '"', '\r', '\n' }) >= 0)
            {
                valor = "\"" + valor.Replace("\"", "\"\"") + "\"";
            }
            return valor;
        }

        // ---------------------------------------------------------------- imprimir

        private void Imprimir()
        {
            if (resultado == null || resultado.Rows.Count == 0)
            {
                return;
            }
            try
            {
                filaImpresion = 0;
                paginaImpresion = 0;
                PrintDocument documento = new PrintDocument();
                documento.DocumentName = "Informe de Precios";
                documento.DefaultPageSettings.Landscape = !resultadoEnDolares;
                documento.DefaultPageSettings.Margins = new Margins(40, 40, 40, 40);
                documento.PrintPage += ImprimirPagina;
                using (PrintPreviewDialog vista = new PrintPreviewDialog())
                {
                    vista.Document = documento;
                    vista.Width = 1000;
                    vista.Height = 720;
                    vista.StartPosition = FormStartPosition.CenterParent;
                    vista.ShowDialog(this);
                }
            }
            catch (Exception ex)
            {
                RegistroErrores.Registrar(ex, "Informe de precios: imprimir");
                MessageBox.Show(this, "No se pudo preparar la impresión.\n\n" + ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ImprimirPagina(object sender, PrintPageEventArgs e)
        {
            if (paginaImpresion == 0)
            {
                filaImpresion = 0;
            }
            paginaImpresion++;
            List<DataGridViewColumn> columnas = ColumnasVisibles();
            Rectangle area = e.MarginBounds;
            float suma = 0f;
            foreach (DataGridViewColumn c in columnas)
            {
                suma += c.FillWeight;
            }
            using (Font fTitulo = new Font("Arial", 13F, FontStyle.Bold))
            using (Font fSub = new Font("Arial", 8.5F))
            using (Font fCab = new Font("Arial", 8.5F, FontStyle.Bold))
            using (Font fTxt = new Font("Arial", 8F))
            using (Pen lapiz = new Pen(Color.Gray))
            using (StringFormat izquierda = new StringFormat { Trimming = StringTrimming.EllipsisCharacter, FormatFlags = StringFormatFlags.NoWrap })
            using (StringFormat derecha = new StringFormat { Alignment = StringAlignment.Far, Trimming = StringTrimming.EllipsisCharacter, FormatFlags = StringFormatFlags.NoWrap })
            {
                float y = area.Top;
                e.Graphics.DrawString("Informe de Precios", fTitulo, Brushes.Black, area.Left, y);
                y += fTitulo.GetHeight(e.Graphics) + 2;
                e.Graphics.DrawString(subtituloInforme, fSub, Brushes.Black, area.Left, y);
                string fecha = DateTime.Now.ToString("dd/MM/yyyy hh:mm tt", CultureInfo.CurrentCulture) + "   Página " + paginaImpresion;
                e.Graphics.DrawString(fecha, fSub, Brushes.Black, new RectangleF(area.Left, y, area.Width, fSub.GetHeight(e.Graphics)), derecha);
                y += fSub.GetHeight(e.Graphics) + 8;

                float alto = fTxt.GetHeight(e.Graphics) + 4;
                // encabezados de columna
                float x = area.Left;
                foreach (DataGridViewColumn c in columnas)
                {
                    float ancho = area.Width * c.FillWeight / suma;
                    bool der = c.DefaultCellStyle.Alignment == DataGridViewContentAlignment.MiddleRight;
                    e.Graphics.DrawString(c.HeaderText, fCab, Brushes.Black, new RectangleF(x + 2, y, ancho - 4, alto), der ? derecha : izquierda);
                    x += ancho;
                }
                y += alto;
                e.Graphics.DrawLine(lapiz, area.Left, y, area.Right, y);
                y += 2;

                while (filaImpresion < grilla.Rows.Count && y + alto <= area.Bottom)
                {
                    x = area.Left;
                    DataGridViewRow fila = grilla.Rows[filaImpresion];
                    foreach (DataGridViewColumn c in columnas)
                    {
                        float ancho = area.Width * c.FillWeight / suma;
                        bool der = c.DefaultCellStyle.Alignment == DataGridViewContentAlignment.MiddleRight;
                        e.Graphics.DrawString(ValorExportado(fila, c), fTxt, Brushes.Black, new RectangleF(x + 2, y, ancho - 4, alto), der ? derecha : izquierda);
                        x += ancho;
                    }
                    y += alto;
                    filaImpresion++;
                }
            }
            e.HasMorePages = filaImpresion < grilla.Rows.Count;
            if (!e.HasMorePages)
            {
                paginaImpresion = 0;
            }
        }
    }
}

