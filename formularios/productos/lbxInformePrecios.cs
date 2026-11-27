using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows.Forms;
using lebrun.clasesData;
using lebrun.formularios.complementos;
using static lebrun.formularios.gerencial.EstiloGerencial;

namespace lebrun.formularios.productos
{
    // Informe de Precios (Inventario > Informes > Informe de Precios): el listado de precios de los productos, en dolares o en bolivares por separado
    // (cuando se cierra temprano la facturacion se entregan los precios en dolares y los bolivares quedan para la actualizacion masiva).
    //   - Dolares: el precio en dolares de cada producto (adminv2.pre_ext).
    //   - Bolivares: los precios vigentes (admprecios con pre_act = 1) por lista de precio y unidad, con la fecha, hora y usuario del ultimo cambio.
    // Se puede filtrar por proveedor (adminvparam), mostrar solo los productos con precio o sin precio, incluir los inactivos y, en bolivares,
    // elegir la lista de precio y ver solo lo cambiado desde una fecha y hora. Se puede imprimir y exportar a un archivo que abre Excel.
    // El proveedor no se repite en una columna: si se elige uno, su codigo y nombre van en el titulo; si se listan todos, los productos salen
    // agrupados bajo un renglon con el codigo y el nombre de cada proveedor.
    // Es solo de consulta: no cambia nada en la base de datos.
    public class lbxInformePrecios : Form
    {
        private const int MaximoFilas = 60000;
        private const int MaximoProveedores = 100;
        private const string TextoAyudaProveedor = "Escriba un código y presione Enter (vacío = todos)";
        private static readonly Color FondoGrupo = Color.FromArgb(206, 221, 244);

        private readonly ConexionBD baseDatos = new ConexionBD();
        private readonly DataGridView grilla = new DataGridView();
        private Label cabeceraGrilla;
        private readonly ComboBox cboMoneda = new ComboBox();
        private readonly ComboBox cboLista = new ComboBox();
        private readonly TextBox txtProveedor = new TextBox();
        private readonly Button cmdLupaProveedor = new Button();
        private readonly Label lblProveedor = new Label();
        private string proveedorCodigo;              // codigo del proveedor elegido; null = todos
        private string proveedorNombre = "";
        private string tituloProveedor = "";         // "Proveedor: codigo - nombre" de lo que se ve en la grilla
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
            grilla.RowPrePaint += PintarGrupo;
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
            raiz.RowStyles.Add(new RowStyle(SizeType.Absolute, 144));
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

            // El campo del proveedor se maneja como el del producto en la Consulta Gerencial: se escribe el codigo y, al dar Enter, si existe se completa
            // y aparece su nombre en verde; la lupa abre la lista de proveedores para elegirlo.
            txtProveedor.Width = 150;
            txtProveedor.MaxLength = 60;
            txtProveedor.Font = new Font(Fuente, 11F, FontStyle.Bold);
            txtProveedor.BorderStyle = BorderStyle.FixedSingle;
            txtProveedor.KeyDown += delegate (object s, KeyEventArgs ev)
            {
                if (ev.KeyCode == Keys.Enter)
                {
                    ev.SuppressKeyPress = true;
                    Buscar();
                }
            };
            txtProveedor.TextChanged += delegate
            {
                // al cambiar lo escrito deja de valer el proveedor que se habia elegido
                if (proveedorCodigo == null || txtProveedor.Text.Trim() != proveedorCodigo)
                {
                    proveedorCodigo = null;
                    proveedorNombre = "";
                    NombreProveedor(TextoAyudaProveedor, false);
                }
            };
            cmdLupaProveedor.Image = global::lebrun.Properties.Resources.Search_16x16;
            cmdLupaProveedor.FlatStyle = FlatStyle.Flat;
            cmdLupaProveedor.FlatAppearance.BorderColor = Borde;
            cmdLupaProveedor.BackColor = Color.White;
            cmdLupaProveedor.Cursor = Cursors.Hand;
            cmdLupaProveedor.Size = new Size(40, 28);
            cmdLupaProveedor.Margin = new Padding(6, 0, 0, 0);
            cmdLupaProveedor.Click += delegate { AbrirListaProveedores(); };
            lblProveedor.AutoSize = true;
            lblProveedor.Font = new Font(Fuente, 12F, FontStyle.Bold);
            lblProveedor.Margin = new Padding(10, 3, 0, 0);
            NombreProveedor(TextoAyudaProveedor, false);

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
            f.RowCount = 3;
            f.RowStyles.Add(new RowStyle(SizeType.Percent, 33.4f));
            f.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3f));
            f.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3f));
            ConBorde(f);
            FlowLayoutPanel lineaProveedor = Linea();
            Label etiquetaProveedor = Alinear(EtiquetaNueva("Código del proveedor"));
            etiquetaProveedor.Font = new Font(Fuente, 9.5F, FontStyle.Bold);
            lineaProveedor.Controls.Add(etiquetaProveedor);
            lineaProveedor.Controls.Add(txtProveedor);
            lineaProveedor.Controls.Add(cmdLupaProveedor);
            lineaProveedor.Controls.Add(lblProveedor);
            f.Controls.Add(lineaProveedor, 0, 0);
            f.Controls.Add(linea1, 0, 1);
            f.Controls.Add(linea2, 0, 2);
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
            tituloProveedor = "";
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

        // ---------------------------------------------------------------- proveedor

        // Texto junto al codigo: el nombre del proveedor en verde, la ayuda en gris o un aviso en rojo si no se encontro.
        private void NombreProveedor(string texto, bool error)
        {
            lblProveedor.Text = texto;
            lblProveedor.ForeColor = error ? Negativo : (texto == TextoAyudaProveedor ? TextoSecundario : Color.DarkGreen);
        }

        // La lupa abre la lista de proveedores del sistema; al elegir uno vuelve aqui por SeleccionarProveedor.
        private void AbrirListaProveedores()
        {
            lbxProveedores lista = lbxProveedores.DefInstance;
            if (MdiParent != null)
            {
                lista.MdiParent = MdiParent;
            }
            lista.destino(this, "lbxInformePrecios");
            lista.Show();
            lista.BringToFront();
        }

        // Lo llama la lista de proveedores al elegir uno: se pone el codigo y se consulta como si se hubiera escrito y dado Enter.
        public void SeleccionarProveedor(string codigo)
        {
            txtProveedor.Text = (codigo ?? "").Trim();
            Buscar();
            Activate();
            txtProveedor.Focus();
            txtProveedor.SelectAll();
        }

        // Convierte lo escrito en un proveedor: el codigo exacto (o sin los ceros de la izquierda), o lo que coincida por codigo, nombre o RIF; si coinciden
        // varios se elige en una lista. Devuelve false si no existe o no se eligio ninguno. Vacio = todos los proveedores.
        private bool ResolverProveedor()
        {
            string escrito = txtProveedor.Text.Trim();
            if (escrito.Length == 0)
            {
                proveedorCodigo = null;
                proveedorNombre = "";
                NombreProveedor("Todos los proveedores", false);
                return true;
            }
            if (proveedorCodigo != null && escrito == proveedorCodigo)
            {
                return true;
            }
            string t = Texto(escrito);
            string parecido = "'%" + ParaLike(t) + "%'";
            string sinCeros = "TRIM(LEADING '0' FROM pro_codigo) = TRIM(LEADING '0' FROM '" + t + "')";
            DataTable dt = baseDatos.fDataTable("SELECT pro_codigo, pro_nombre, pro_rif FROM admproveedor WHERE pro_codigo = '" + t + "' OR pro_rif = '" + t + "' OR " + sinCeros +
                                                " OR pro_codigo LIKE " + parecido + " OR pro_rif LIKE " + parecido + " OR pro_nombre LIKE " + parecido +
                                                " ORDER BY (pro_codigo = '" + t + "') DESC, (" + sinCeros + ") DESC, pro_nombre LIMIT " + (MaximoProveedores + 1));
            baseDatos.cerrarConexion();

            DataRow elegido = null;
            List<DataRow> mismoNumero = new List<DataRow>();
            foreach (DataRow r in dt.Rows)
            {
                string codigo = Convert.ToString(r["pro_codigo"]).Trim();
                if (string.Equals(codigo, escrito, StringComparison.OrdinalIgnoreCase))
                {
                    elegido = r;
                    break;
                }
                if (string.Equals(codigo.TrimStart('0'), escrito.TrimStart('0'), StringComparison.OrdinalIgnoreCase))
                {
                    mismoNumero.Add(r);
                }
            }
            if (elegido == null && mismoNumero.Count == 1)
            {
                elegido = mismoNumero[0];
            }
            if (elegido == null && dt.Rows.Count == 1)
            {
                elegido = dt.Rows[0];
            }
            if (elegido == null && dt.Rows.Count > 1)
            {
                bool recortada = dt.Rows.Count > MaximoProveedores;
                if (recortada)
                {
                    dt.Rows.RemoveAt(dt.Rows.Count - 1);
                }
                using (SelectorRegistro selector = new SelectorRegistro(dt, "proveedor", "proveedores", "pro_codigo", "pro_nombre", "pro_rif", recortada, MaximoProveedores))
                {
                    if (selector.ShowDialog(this) != DialogResult.OK)
                    {
                        return false;
                    }
                    foreach (DataRow r in dt.Rows)
                    {
                        if (Convert.ToString(r["pro_codigo"]) == selector.Codigo)
                        {
                            elegido = r;
                        }
                    }
                }
            }
            if (elegido == null)
            {
                NombreProveedor("El proveedor no existe", true);
                lblAviso.Text = "No hay proveedores con ese código, nombre o RIF. Use la lupa para buscarlo en la lista.";
                return false;
            }
            // se completa el codigo y se deja seleccionado, como en el campo del producto
            proveedorCodigo = Convert.ToString(elegido["pro_codigo"]).Trim();
            proveedorNombre = Convert.ToString(elegido["pro_nombre"]).Trim();
            txtProveedor.Text = proveedorCodigo;
            txtProveedor.SelectAll();
            NombreProveedor(proveedorNombre, false);
            return true;
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
            if (proveedorCodigo != null)
            {
                filtro += " AND pa.pin_proveedor = '" + Texto(proveedorCodigo) + "'";
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
            return "SELECT i.inv_codigo AS codigo, i.inv_descri AS descripcion, TRIM(IFNULL(pa.pin_proveedor, '')) AS provcod, IFNULL(p.pro_nombre, '') AS proveedor, " +
                   "IFNULL(i2.pre_ext, 0) AS precio " +
                   "FROM adminv i LEFT JOIN adminv2 i2 ON i2.inv2_codigo = i.inv_codigo " +
                   "LEFT JOIN adminvparam pa ON pa.pin_codigo = i.inv_codigo LEFT JOIN admproveedor p ON p.pro_codigo = pa.pin_proveedor " +
                   "WHERE 1 = 1" + filtro + " ORDER BY proveedor, provcod, i.inv_descri LIMIT " + (MaximoFilas + 1);
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
            return "SELECT i.inv_codigo AS codigo, i.inv_descri AS descripcion, a.pre_lista AS lista, a.pre_undmed AS unidad, TRIM(IFNULL(pa.pin_proveedor, '')) AS provcod, IFNULL(p.pro_nombre, '') AS proveedor, " +
                   "a.pre_precio AS precio, IF(a.pre_fecha > '1900-01-01', a.pre_fecha, NULL) AS fecha, NULLIF(a.pre_hora, '') AS hora, NULLIF(a.pre_usuario, '') AS usuario " +
                   "FROM adminv i LEFT JOIN admprecios a ON a.pre_codigo = i.inv_codigo AND a.pre_act = '1' AND a.pre_codmon = 'Bs'" + lista + " " +
                   "LEFT JOIN adminvparam pa ON pa.pin_codigo = i.inv_codigo LEFT JOIN admproveedor p ON p.pro_codigo = pa.pin_proveedor " +
                   "WHERE 1 = 1" + filtro + " ORDER BY proveedor, provcod, i.inv_descri, a.pre_lista, a.pre_undmed LIMIT " + (MaximoFilas + 1);
        }

        private void Buscar()
        {
            lblAviso.Text = "";
            try
            {
                if (!ResolverProveedor())
                {
                    // no se deja en pantalla el resultado de otro proveedor
                    grilla.DataSource = null;
                    resultado = null;
                    lblResumen.Text = "";
                    cmdImprimir.Enabled = false;
                    cmdExportar.Enabled = false;
                    cabeceraGrilla.Text = "Precios";
                    return;
                }
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
                int productos = dt.Rows.Count;
                tituloProveedor = proveedorCodigo == null ? "" : "Proveedor: " + proveedorCodigo + " - " + proveedorNombre;
                int proveedores = 0;
                dt.Columns.Add("grupo", typeof(string));
                if (proveedorCodigo == null)
                {
                    dt = AgruparPorProveedor(dt, out proveedores);
                }
                resultado = dt;
                resultadoEnDolares = dolares;
                subtituloInforme = Subtitulo();
                grilla.DataSource = dt;
                Encabezados(dolares, proveedorCodigo == null);
                cabeceraGrilla.Text = "Precios en " + (dolares ? "dólares" : "bolívares") + (tituloProveedor.Length > 0 ? "  ·  " + tituloProveedor : "") +
                                      "  ·  " + (productos == 0 ? "sin registros" : productos + (productos == 1 ? " registro" : " registros"));
                MostrarResumen(dt, proveedores);
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

        // Antes de los productos de cada proveedor va un renglon con su codigo y nombre (la columna grupo lo marca).
        private static DataTable AgruparPorProveedor(DataTable datos, out int proveedores)
        {
            proveedores = 0;
            DataTable agrupada = datos.Clone();
            foreach (DataColumn c in agrupada.Columns)
            {
                c.AllowDBNull = true;
                c.ReadOnly = false;
                if (c.DataType == typeof(string))
                {
                    c.MaxLength = -1;   // el titulo del grupo va en la columna de la descripcion y puede ser mas largo que ella
                }
            }
            string anterior = null;
            foreach (DataRow r in datos.Rows)
            {
                string codigo = Convert.ToString(r["provcod"]);
                if (codigo != anterior)
                {
                    anterior = codigo;
                    proveedores++;
                    string nombre = Convert.ToString(r["proveedor"]).Trim();
                    DataRow titulo = agrupada.NewRow();
                    titulo["grupo"] = codigo.Length == 0 ? "Sin proveedor asignado" : "Proveedor: " + codigo + " - " + (nombre.Length > 0 ? nombre : "(sin ficha de proveedor)");
                    titulo["descripcion"] = titulo["grupo"];
                    agrupada.Rows.Add(titulo);
                }
                agrupada.ImportRow(r);
            }
            return agrupada;
        }

        private static string TituloDeGrupo(DataGridViewRow fila)
        {
            DataRowView vista = fila.DataBoundItem as DataRowView;
            if (vista == null || vista["grupo"] == DBNull.Value)
            {
                return null;
            }
            return Convert.ToString(vista["grupo"]);
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
            if (proveedorCodigo == null)
            {
                partes.Add("todos los proveedores");
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

        private void MostrarResumen(DataTable dt, int proveedores)
        {
            int productos = 0;
            int con = 0;
            foreach (DataRow r in dt.Rows)
            {
                if (r["grupo"] != DBNull.Value)
                {
                    continue;
                }
                productos++;
                if (r["precio"] != DBNull.Value && Convert.ToDecimal(r["precio"]) > 0m)
                {
                    con++;
                }
            }
            lblResumen.Text = productos + " registros     Con precio " + con + "     Sin precio " + (productos - con) + (proveedores > 0 ? "     Proveedores " + proveedores : "");
        }

        // ---------------------------------------------------------------- presentacion

        // El proveedor no lleva columna (va en el titulo o en el renglon de cada grupo). Agrupado no se puede reordenar: se desarmarian los grupos.
        private void Encabezados(bool dolares, bool agrupado)
        {
            Col("codigo", "Código", dolares ? 15 : 13);
            Col("descripcion", "Descripción", dolares ? 70 : 42);
            Col("precio", dolares ? "Precio $" : "Precio Bs", 12, "n2", true);
            if (!dolares)
            {
                Col("lista", "Lista", 5);
                Col("unidad", "Unidad", 7);
                Col("fecha", "Fecha cambio", 10, "dd/MM/yyyy");
                Col("hora", "Hora", 10);
                Col("usuario", "Usuario", 7);
            }
            foreach (string oculta in new[] { "provcod", "proveedor", "grupo" })
            {
                grilla.Columns[oculta].Visible = false;
            }
            foreach (DataGridViewColumn c in grilla.Columns)
            {
                c.SortMode = agrupado ? DataGridViewColumnSortMode.NotSortable : DataGridViewColumnSortMode.Automatic;
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
            if (e.RowIndex < 0 || e.ColumnIndex < 0 || grilla.Columns[e.ColumnIndex].Name != "precio" || TituloDeGrupo(grilla.Rows[e.RowIndex]) != null)
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

        // El renglon de cada proveedor se distingue de los productos.
        private void PintarGrupo(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            DataGridViewRow fila = grilla.Rows[e.RowIndex];
            if (TituloDeGrupo(fila) != null && fila.DefaultCellStyle.BackColor != FondoGrupo)
            {
                fila.DefaultCellStyle.BackColor = FondoGrupo;
                fila.DefaultCellStyle.Font = new Font(Fuente, 9F, FontStyle.Bold);
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
                    if (tituloProveedor.Length > 0)
                    {
                        sb.AppendLine(Csv(tituloProveedor));
                    }
                    List<string> cab = new List<string>();
                    foreach (DataGridViewColumn c in columnas)
                    {
                        cab.Add(Csv(c.HeaderText));
                    }
                    sb.AppendLine(string.Join(";", cab.ToArray()));
                    foreach (DataGridViewRow fila in grilla.Rows)
                    {
                        string grupo = TituloDeGrupo(fila);
                        if (grupo != null)
                        {
                            sb.AppendLine(Csv(grupo));
                            continue;
                        }
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
            Principal principal = MdiParent as Principal;
            ImpresionGrilla impresion = new ImpresionGrilla();
            impresion.Grilla = grilla;
            impresion.Compania = principal != null ? principal.empresaActual.Nombre : "";
            impresion.Titulo = "Informe de Precios";
            if (tituloProveedor.Length > 0)
            {
                impresion.Destacadas.Add(tituloProveedor);
            }
            impresion.Detalle.Add(subtituloInforme);
            impresion.Totales.Add(lblResumen.Text);
            impresion.Horizontal = !resultadoEnDolares;
            impresion.TituloDeGrupo = TituloDeGrupo;
            impresion.VistaPrevia(this);
        }
    }
}
