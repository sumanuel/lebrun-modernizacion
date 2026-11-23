using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using lebrun.clasesData;
using static lebrun.formularios.gerencial.EstiloGerencial;

namespace lebrun.formularios.facturacion
{
    // Estado de cuentas de un cliente, como el del sistema anterior (ModelSoft): se elige el cliente (por codigo, nombre o RIF), se muestran sus datos y dos pestanas:
    //   - Estado de cuenta: los documentos pendientes de cuentas por cobrar (admsalcli) con lo cobrado, el saldo, el estado de cobro (Pagado, Parcial o
    //     Pendiente, de admdoccli.dcli_estatus) y lo que esta en mora (vencido y con saldo).
    //   - Historico de movimientos: las facturas, notas de debito y devoluciones (admdoccli), los recibos de ingreso (admreciboingre, uno por documento
    //     cancelado) y las retenciones de IVA (admdocclirti), aunque ya no esten pendientes.
    // Sin cliente, la pestana Estado de cuenta lista los documentos por cobrar de todos los clientes. Es solo de consulta: no cambia nada en la base de datos.
    public class lbxEstadoCuentas : Form
    {
        private const int MaximoFilas = 2000;
        private const int MaximoClientes = 100;

        private readonly ConexionBD baseDatos = new ConexionBD();
        private readonly DataGridView grillaPendientes = new DataGridView();
        private readonly DataGridView grillaMovimientos = new DataGridView();
        private readonly TabControl pestanas = new TabControl();
        private readonly TabPage paginaPendientes = new TabPage("Estado de cuenta");
        private readonly TabPage paginaMovimientos = new TabPage("Histórico de movimientos");
        private readonly TextBox txtCliente = new TextBox();
        private readonly Button cmdLupa = new Button();
        private readonly Label lblNombreCliente = new Label();
        private const string TextoAyuda = "Escriba un código y presione Enter (vacío = todos)";
        private readonly DateTimePicker dtpDesde = new DateTimePicker();
        private readonly DateTimePicker dtpHasta = new DateTimePicker();
        private readonly CheckBox chkConSaldo = new CheckBox();
        private readonly Button cmdBuscar = new Button();
        private readonly Button cmdLimpiar = new Button();
        private readonly Button cmdCerrar = new Button();
        private readonly Label lblTotales = new Label();
        private readonly Label lblMora = new Label();
        private readonly Label lblAviso = new Label();

        // datos del cliente elegido
        private Control tarjetaCliente;
        private readonly Label vRif = new Label();
        private readonly Label vTelefonos = new Label();
        private readonly Label vDireccion = new Label();
        private readonly Label vSituacion = new Label();

        private string clienteActual;              // codigo del cliente elegido; null = todos los clientes
        private DataTable pendientes;
        private DataTable movimientos;

        protected override void OnLoad(EventArgs e)
        {
            Armar();
            base.OnLoad(e);
            txtCliente.Focus();
        }

        // ---------------------------------------------------------------- diseno

        private void Armar()
        {
            SuspendLayout();
            AutoScaleMode = AutoScaleMode.None;
            Font = new Font(Fuente, 9F);
            BackColor = Fondo;
            Text = "Estado de cuentas";
            Rectangle area = Screen.PrimaryScreen.WorkingArea;
            ClientSize = new Size(Math.Min(1180, area.Width - 60), Math.Min(700, area.Height - 100));
            MinimumSize = new Size(960, 560);
            StartPosition = FormStartPosition.CenterParent;

            // El campo del cliente se maneja igual que el del producto en la Consulta Gerencial: se escribe el codigo y, al dar Enter, si el cliente existe
            // se completa el codigo y aparece su nombre en verde; la lupa abre la lista de clientes para elegirlo. Sin cliente se ven los de todos.
            txtCliente.MaxLength = 60;
            txtCliente.KeyDown += delegate (object s, KeyEventArgs ev)
            {
                if (ev.KeyCode == Keys.Enter)
                {
                    ev.SuppressKeyPress = true;
                    Buscar();
                }
            };
            cmdLupa.Image = global::lebrun.Properties.Resources.Search_16x16;
            cmdLupa.Click += delegate { AbrirListaClientes(); };
            Label etiquetaCliente = new Label();
            Control encabezado = EncabezadoProducto(etiquetaCliente, txtCliente, cmdLupa, lblNombreCliente);
            etiquetaCliente.Text = "Código del cliente";
            lblNombreCliente.Text = TextoAyuda;

            foreach (DataGridView g in new[] { grillaPendientes, grillaMovimientos })
            {
                g.AllowUserToAddRows = false;
                g.AllowUserToDeleteRows = false;
                g.ReadOnly = true;
                g.MultiSelect = false;
                g.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                g.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                g.Margin = new Padding(0);
                g.Dock = DockStyle.Fill;
                EstiloGrilla(g);
                g.CellFormatting += FormatearCelda;
            }

            EstiloPestanas(pestanas);
            pestanas.ItemSize = new Size(260, 32);
            pestanas.Dock = DockStyle.Fill;
            pestanas.Margin = new Padding(0, 0, 0, 6);
            foreach (TabPage p in new[] { paginaPendientes, paginaMovimientos })
            {
                p.BackColor = Color.White;
                p.Padding = new Padding(0);
            }
            paginaPendientes.Controls.Add(grillaPendientes);
            paginaMovimientos.Controls.Add(grillaMovimientos);
            pestanas.TabPages.Add(paginaPendientes);
            pestanas.TabPages.Add(paginaMovimientos);
            pestanas.SelectedIndexChanged += delegate { MostrarTotales(); };

            lblTotales.Dock = DockStyle.Fill;
            lblTotales.TextAlign = ContentAlignment.MiddleRight;
            lblTotales.Font = new Font(Fuente, 10F, FontStyle.Bold);
            lblTotales.ForeColor = Azul;
            lblMora.AutoSize = true;
            lblMora.Font = new Font(Fuente, 10F, FontStyle.Bold);
            lblMora.ForeColor = Negativo;
            lblMora.Margin = new Padding(0, 6, 0, 0);

            TableLayoutPanel pie = new TableLayoutPanel();
            pie.Dock = DockStyle.Fill;
            pie.ColumnCount = 2;
            pie.RowCount = 1;
            pie.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 300));
            pie.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            pie.Controls.Add(lblMora, 0, 0);
            pie.Controls.Add(lblTotales, 1, 0);

            tarjetaCliente = ArmarTarjetaCliente();
            tarjetaCliente.Visible = false;

            TableLayoutPanel raiz = new TableLayoutPanel();
            raiz.Dock = DockStyle.Fill;
            raiz.ColumnCount = 1;
            raiz.RowCount = 6;
            raiz.Padding = new Padding(12, 10, 12, 8);
            raiz.RowStyles.Add(new RowStyle(SizeType.Absolute, 62));
            raiz.RowStyles.Add(new RowStyle(SizeType.Absolute, 54));
            raiz.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            raiz.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            raiz.RowStyles.Add(new RowStyle(SizeType.Absolute, 22));
            raiz.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));
            lblAviso.AutoSize = false;
            lblAviso.Dock = DockStyle.Fill;
            lblAviso.TextAlign = ContentAlignment.MiddleLeft;
            lblAviso.ForeColor = Negativo;
            raiz.Controls.Add(encabezado, 0, 0);
            raiz.Controls.Add(ArmarFiltros(), 0, 1);
            raiz.Controls.Add(tarjetaCliente, 0, 2);
            raiz.Controls.Add(pestanas, 0, 3);
            raiz.Controls.Add(lblAviso, 0, 4);
            raiz.Controls.Add(pie, 0, 5);
            Controls.Clear();
            Controls.Add(raiz);
            ResumeLayout(true);
        }

        private Control ArmarFiltros()
        {
            foreach (DateTimePicker dtp in new[] { dtpDesde, dtpHasta })
            {
                dtp.Format = DateTimePickerFormat.Short;
                dtp.ShowCheckBox = true;
                dtp.Checked = false;
                dtp.Width = 120;
            }
            chkConSaldo.Text = "Solo con saldo";
            chkConSaldo.Checked = true;
            chkConSaldo.AutoSize = true;
            EstiloBoton(cmdBuscar, "Buscar", true);
            EstiloBoton(cmdLimpiar, "Limpiar", false);
            EstiloBoton(cmdCerrar, "Cerrar", false);
            cmdBuscar.Click += delegate { Buscar(); };
            cmdLimpiar.Click += delegate { Limpiar(); };
            cmdCerrar.Click += delegate { Close(); };
            FlowLayoutPanel f = new FlowLayoutPanel();
            f.Dock = DockStyle.Fill;
            f.BackColor = Color.White;
            f.Padding = new Padding(10, 8, 10, 4);
            f.Margin = new Padding(0, 0, 0, 10);
            ConBorde(f);
            f.WrapContents = false;
            f.Controls.Add(Alinear(EtiquetaNueva("Desde")));
            f.Controls.Add(dtpDesde);
            f.Controls.Add(Alinear(EtiquetaNueva("hasta")));
            f.Controls.Add(dtpHasta);
            chkConSaldo.Margin = new Padding(14, 6, 10, 0);
            f.Controls.Add(chkConSaldo);
            f.Controls.Add(cmdBuscar);
            f.Controls.Add(cmdLimpiar);
            f.Controls.Add(cmdCerrar);
            return f;
        }

        // Datos del cliente elegido: codigo, nombre, RIF, telefonos, direccion y situacion.
        private Control ArmarTarjetaCliente()
        {
            TableLayoutPanel t = new TableLayoutPanel();
            t.Dock = DockStyle.Fill;
            t.AutoSize = true;
            t.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            t.BackColor = Color.White;
            t.Margin = new Padding(0, 0, 0, 8);
            t.Padding = new Padding(14, 10, 14, 6);
            t.ColumnCount = 4;
            t.RowCount = 2;
            t.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80));
            t.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            t.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80));
            t.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            for (int i = 0; i < 2; i++)
            {
                t.RowStyles.Add(new RowStyle(SizeType.Absolute, 24));
            }
            ConBorde(t);
            // el codigo y el nombre ya se ven arriba, en el campo del cliente
            AgregarDato(t, 0, 0, "RIF", vRif);
            AgregarDato(t, 0, 2, "Teléfonos", vTelefonos);
            AgregarDato(t, 1, 0, "Situación", vSituacion);
            AgregarDato(t, 1, 2, "Dirección", vDireccion);
            return t;
        }

        private static void AgregarDato(TableLayoutPanel t, int fila, int columna, string etiqueta, Label valor)
        {
            Label l = EtiquetaNueva(etiqueta);
            l.Dock = DockStyle.Fill;
            l.TextAlign = ContentAlignment.MiddleLeft;
            l.ForeColor = TextoSecundario;
            valor.Dock = DockStyle.Fill;
            valor.AutoEllipsis = true;
            valor.TextAlign = ContentAlignment.MiddleLeft;
            valor.Font = new Font(Fuente, 9.5F, FontStyle.Bold);
            t.Controls.Add(l, columna, fila);
            t.Controls.Add(valor, columna + 1, fila);
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

        // ---------------------------------------------------------------- utilidades

        private static string Texto(string valor)
        {
            return (valor ?? "").Trim().Replace("\\", "\\\\").Replace("'", "''");
        }

        // para LIKE: los comodines % y _ escritos por el usuario se toman como texto
        private static string ParaLike(string valorEscapado)
        {
            return valorEscapado.Replace("%", "\\%").Replace("_", "\\_");
        }

        private static string Fecha(DateTime f)
        {
            return f.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
        }

        private void Limpiar()
        {
            txtCliente.Clear();
            dtpDesde.Checked = false;
            dtpHasta.Checked = false;
            chkConSaldo.Checked = true;
            clienteActual = null;
            tarjetaCliente.Visible = false;
            NombreCliente(TextoAyuda, false);
            pendientes = null;
            movimientos = null;
            grillaPendientes.DataSource = null;
            grillaMovimientos.DataSource = null;
            lblAviso.Text = "";
            lblMora.Text = "";
            lblTotales.Text = "";
            TituloPestanas();
            txtCliente.Focus();
        }

        // Texto junto al codigo: el nombre del cliente en verde, o un aviso en rojo si no se encontro.
        private void NombreCliente(string texto, bool error)
        {
            lblNombreCliente.Text = texto;
            lblNombreCliente.ForeColor = error ? Negativo : Color.DarkGreen;
        }

        // La lupa abre la misma lista de clientes que usan las facturas y los recibos; al elegir uno vuelve aqui por SeleccionarCliente.
        private void AbrirListaClientes()
        {
            lbxClientes lista = lbxClientes.DefInstance;
            if (MdiParent != null)
            {
                lista.MdiParent = MdiParent;
            }
            lista.destino(this, 9);
            lista.Show();
            lista.BringToFront();
        }

        // Lo llama la lista de clientes al elegir uno: se pone el codigo y se consulta como si se hubiera escrito y dado Enter.
        public void SeleccionarCliente(string codigo)
        {
            txtCliente.Text = codigo;
            Buscar();
            Activate();
            txtCliente.Focus();
            txtCliente.SelectionStart = 0;
            txtCliente.SelectionLength = txtCliente.Text.Length;
        }

        // ---------------------------------------------------------------- busqueda

        private void Buscar()
        {
            lblAviso.Text = "";
            try
            {
                string texto = txtCliente.Text.Trim();
                if (texto.Length == 0)
                {
                    clienteActual = null;
                    tarjetaCliente.Visible = false;
                    NombreCliente("Todos los clientes", false);
                    CargarPendientes();
                    movimientos = null;
                    grillaMovimientos.DataSource = null;
                    lblAviso.Text = "Mostrando los documentos por cobrar de todos los clientes. Elija un cliente para ver sus movimientos.";
                    TituloPestanas();
                    pestanas.SelectedTab = paginaPendientes;
                    MostrarTotales();
                    return;
                }

                DataRow cliente = ElegirCliente(texto);
                if (cliente == null)
                {
                    return;
                }
                clienteActual = Convert.ToString(cliente["cli_codigo"]);
                // se completa el codigo y se deja seleccionado, como en el campo del producto
                txtCliente.Text = clienteActual;
                txtCliente.SelectionStart = 0;
                txtCliente.SelectionLength = txtCliente.Text.Length;
                MostrarCliente(clienteActual);
                CargarPendientes();
                CargarMovimientos();
                TituloPestanas();
                MostrarTotales();
            }
            catch (Exception ex)
            {
                RegistroErrores.Registrar(ex, "Estado de cuentas");
                MessageBox.Show("No se pudo consultar el estado de cuentas.\n\n" + ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Busca el cliente por codigo, nombre o RIF. Si hay uno solo (o el codigo es exacto) lo toma; si hay varios muestra la lista para elegir.
        private DataRow ElegirCliente(string texto)
        {
            string t = Texto(texto);
            string sql = "SELECT cli_codigo, cli_nombre, cli_rif FROM admclientes WHERE cli_codigo = '" + t + "' OR cli_rif = '" + t + "' " +
                         "OR cli_codigo LIKE '%" + ParaLike(t) + "%' OR cli_rif LIKE '%" + ParaLike(t) + "%' OR cli_nombre LIKE '%" + ParaLike(t) + "%' " +
                         "ORDER BY (cli_codigo = '" + t + "') DESC, cli_nombre LIMIT " + (MaximoClientes + 1);
            DataTable dt = baseDatos.fDataTable(sql);
            baseDatos.cerrarConexion();
            if (dt.Rows.Count == 0)
            {
                clienteActual = null;
                tarjetaCliente.Visible = false;
                grillaPendientes.DataSource = null;
                grillaMovimientos.DataSource = null;
                pendientes = null;
                movimientos = null;
                TituloPestanas();
                lblTotales.Text = "";
                lblMora.Text = "";
                NombreCliente("El cliente no existe", true);
                lblAviso.Text = "No hay clientes con ese código, nombre o RIF. Use la lupa para buscarlo en la lista.";
                return null;
            }
            foreach (DataRow r in dt.Rows)
            {
                if (string.Equals(Convert.ToString(r["cli_codigo"]).Trim(), texto, StringComparison.OrdinalIgnoreCase))
                {
                    return r;
                }
            }
            if (dt.Rows.Count == 1)
            {
                return dt.Rows[0];
            }
            bool recortado = dt.Rows.Count > MaximoClientes;
            if (recortado)
            {
                dt.Rows.RemoveAt(dt.Rows.Count - 1);
            }
            using (SelectorCliente selector = new SelectorCliente(dt, recortado))
            {
                if (selector.ShowDialog(this) == DialogResult.OK)
                {
                    foreach (DataRow r in dt.Rows)
                    {
                        if (Convert.ToString(r["cli_codigo"]) == selector.Codigo)
                        {
                            return r;
                        }
                    }
                }
            }
            return null;
        }

        private void MostrarCliente(string codigo)
        {
            DataTable dt = baseDatos.fDataTable("SELECT c.cli_codigo, c.cli_nombre, c.cli_rif, c.cli_telefono, c.cli_telmaster, c.cli_celular, c.cli_direc1, c.cli_situacion " +
                                                "FROM admclientes c WHERE c.cli_codigo = '" + Texto(codigo) + "'");
            baseDatos.cerrarConexion();
            if (dt.Rows.Count == 0)
            {
                tarjetaCliente.Visible = false;
                return;
            }
            DataRow r = dt.Rows[0];
            NombreCliente(Convert.ToString(r["cli_nombre"]), false);
            vRif.Text = Convert.ToString(r["cli_rif"]);
            string tel = Convert.ToString(r["cli_telefono"]).Trim();
            foreach (string otro in new[] { Convert.ToString(r["cli_telmaster"]).Trim(), Convert.ToString(r["cli_celular"]).Trim() })
            {
                if (otro.Length > 0 && !tel.Contains(otro))
                {
                    tel += (tel.Length > 0 ? " / " : "") + otro;
                }
            }
            vTelefonos.Text = tel;
            vDireccion.Text = Convert.ToString(r["cli_direc1"]);
            vSituacion.Text = Convert.ToString(r["cli_situacion"]);
            tarjetaCliente.Visible = true;
        }

        // ---------------------------------------------------------------- pestana Estado de cuenta

        private void CargarPendientes()
        {
            string filtro = "";
            if (clienteActual != null)
            {
                filtro += " AND s.cli_codigo = '" + Texto(clienteActual) + "'";
            }
            if (dtpDesde.Checked)
            {
                filtro += " AND s.FechaEmision >= '" + Fecha(dtpDesde.Value) + "'";
            }
            if (dtpHasta.Checked)
            {
                filtro += " AND s.FechaEmision <= '" + Fecha(dtpHasta.Value) + "'";
            }
            if (chkConSaldo.Checked)
            {
                filtro += " AND s.sal_actual <> 0 AND s.Status <> 1";
            }
            // el estado de cobro sale de admdoccli.dcli_estatus; si el documento no esta en admdoccli (un anticipo, por ejemplo) se calcula con el saldo
            string sql = "SELECT s.cli_codigo, c.cli_nombre, s.TipoDoc, s.NroDocum, s.FechaEmision, s.FechaVenc, s.MontoTotal, s.MontoCob, s.sal_actual, s.Status, " +
                         "IFNULL(NULLIF(d.dcli_estatus, ''), CASE WHEN s.Status = 1 OR s.sal_actual = 0 THEN 'Pagado' " +
                         "WHEN ABS(IFNULL(s.MontoTotal, 0)) - ABS(s.sal_actual) > 0.005 THEN 'Parcial' ELSE 'Pendiente' END) AS cobro " +
                         "FROM admsalcli s LEFT JOIN admclientes c ON c.cli_codigo = s.cli_codigo " +
                         "LEFT JOIN admdoccli d ON d.dcli_numero = s.NroDocum AND d.dcli_codigo = s.cli_codigo " +
                         "WHERE 1 = 1" + filtro + " ORDER BY c.cli_nombre, s.FechaEmision, s.NroDocum LIMIT " + (MaximoFilas + 1);
            DataTable dt = baseDatos.fDataTable(sql);
            baseDatos.cerrarConexion();
            bool recortado = dt.Rows.Count > MaximoFilas;
            if (recortado)
            {
                dt.Rows.RemoveAt(dt.Rows.Count - 1);
            }
            pendientes = dt;
            grillaPendientes.DataSource = dt;
            EncabezadosPendientes();
            if (recortado)
            {
                lblAviso.Text = "Se muestran los primeros " + MaximoFilas + " documentos: use un filtro para acotar.";
            }
            else if (dt.Rows.Count == 0 && clienteActual != null)
            {
                lblAviso.Text = chkConSaldo.Checked ? "El cliente no tiene documentos pendientes." : "El cliente no tiene documentos por cobrar con ese filtro.";
            }
            else if (dt.Rows.Count == 0)
            {
                lblAviso.Text = "No hay documentos con ese filtro.";
            }
        }

        private void EncabezadosPendientes()
        {
            bool todos = clienteActual == null;
            Col(grillaPendientes, "cli_codigo", "Cliente", 9, null, false, todos);
            Col(grillaPendientes, "cli_nombre", "Nombre", 22, null, false, todos);
            Col(grillaPendientes, "TipoDoc", "Tipo", 5);
            Col(grillaPendientes, "NroDocum", "Número", 11);
            Col(grillaPendientes, "FechaEmision", "Emisión", 9, "dd/MM/yyyy");
            Col(grillaPendientes, "FechaVenc", "Vence", 9, "dd/MM/yyyy");
            Col(grillaPendientes, "MontoTotal", "Monto", 10, "n2", true);
            Col(grillaPendientes, "MontoCob", "Cobrado", 10, "n2", true);
            Col(grillaPendientes, "sal_actual", "Saldo", 10, "n2", true);
            Col(grillaPendientes, "Status", "Status", 5, null, false, false);
            Col(grillaPendientes, "cobro", "Cobro", 9);
        }

        // ---------------------------------------------------------------- pestana Historico de movimientos

        private void CargarMovimientos()
        {
            string c = Texto(clienteActual);
            // fechas validas solamente (la base tiene fechas cero en algunos registros)
            string sql =
                "SELECT m.fecha, m.tipo, m.numero, m.monto, m.aplica, m.fiscal, m.detalle FROM (" +
                // facturas, notas de debito y devoluciones
                " SELECT IF(d.dcli_fecha > '1900-01-01', d.dcli_fecha, NULL) AS fecha, d.dcli_tipdoc AS tipo, d.dcli_numero AS numero, d.dcli_neto AS monto, " +
                "        IF(d.dcli_facafe = d.dcli_numero, NULL, NULLIF(d.dcli_facafe, '')) AS aplica, NULLIF(d.dcli_numfis, '') AS fiscal, " +
                "        CASE WHEN d.dcli_tipdoc = 'DEV' THEN NULL ELSE NULLIF(d.dcli_estatus, '') END AS detalle, 1 AS orden " +
                " FROM admdoccli d WHERE d.dcli_codigo = '" + c + "'" +
                // recibos de ingreso: una fila por documento cancelado (las devoluciones que se cruzan en el recibo ya salen arriba como devolucion)
                " UNION ALL " +
                " SELECT IF(r.rec_fecha > '1900-01-01', r.rec_fecha, NULL), 'REI', r.rec_numcomprob, x.ingreDoc_MontoCancelado, NULLIF(x.ingreDoc_NumDoc, ''), NULL, " +
                "        CONCAT(r.rec_tippago, IF(r.rec_estado = 'Activo', '', CONCAT(' (', r.rec_estado, ')'))), 2 " +
                " FROM admreciboingre r JOIN admreciboingre_doc x ON x.ingreDoc_numcomprob = r.rec_numcomprob " +
                " WHERE r.rec_cliente = '" + c + "' AND x.ingreDoc_tipo <> 'DEV'" +
                // recibos sin documentos (un anticipo, por ejemplo): una sola fila con el monto del recibo
                " UNION ALL " +
                " SELECT IF(r.rec_fecha > '1900-01-01', r.rec_fecha, NULL), 'REI', r.rec_numcomprob, r.rec_monto, NULL, NULL, " +
                "        CONCAT(r.rec_tippago, IF(r.rec_estado = 'Activo', '', CONCAT(' (', r.rec_estado, ')'))), 2 " +
                " FROM admreciboingre r WHERE r.rec_cliente = '" + c + "' AND NOT EXISTS (SELECT 1 FROM admreciboingre_doc x " +
                "        WHERE x.ingreDoc_numcomprob = r.rec_numcomprob AND x.ingreDoc_tipo <> 'DEV')" +
                // retenciones de IVA (las anuladas, con rti_statusRet = 4, no salen)
                " UNION ALL " +
                " SELECT IF(t.rti_clifecha > '1900-01-01', t.rti_clifecha, NULL), 'RIVA', t.rti_clinumero, t.rti_climonreten, NULLIF(t.rti_clidocnum, ''), NULL, " +
                "        'Retencion IVA', 3 " +
                " FROM admdocclirti t WHERE t.rti_clicodigo = '" + c + "' AND t.rti_statusRet <> 4" +
                ") m WHERE 1 = 1";
            if (dtpDesde.Checked)
            {
                sql += " AND m.fecha >= '" + Fecha(dtpDesde.Value) + "'";
            }
            if (dtpHasta.Checked)
            {
                sql += " AND m.fecha <= '" + Fecha(dtpHasta.Value) + "'";
            }
            // los mas recientes primero: si el cliente tiene mas movimientos que el maximo, lo que se corta es lo mas antiguo
            sql += " ORDER BY m.fecha DESC, m.orden, m.numero LIMIT " + (MaximoFilas + 1);

            DataTable dt = baseDatos.fDataTable(sql);
            baseDatos.cerrarConexion();
            bool recortado = dt.Rows.Count > MaximoFilas;
            if (recortado)
            {
                dt.Rows.RemoveAt(dt.Rows.Count - 1);
                lblAviso.Text = "Se muestran los " + MaximoFilas + " movimientos más recientes: use las fechas Desde y hasta para ver otros.";
            }
            foreach (DataRow r in dt.Rows)
            {
                if (Convert.ToString(r["tipo"]) == "RIVA")
                {
                    r["detalle"] = "Retención IVA";   // con tilde aqui y no en la sentencia, para no depender del juego de caracteres de la conexion
                }
            }
            movimientos = dt;
            grillaMovimientos.DataSource = dt;
            Col(grillaMovimientos, "fecha", "Fecha", 10, "dd/MM/yyyy");
            Col(grillaMovimientos, "tipo", "Tipo", 6);
            Col(grillaMovimientos, "numero", "Número", 16);
            Col(grillaMovimientos, "monto", "Monto", 13, "n2", true);
            Col(grillaMovimientos, "aplica", "Aplica a", 16);
            Col(grillaMovimientos, "fiscal", "Nº fiscal", 12);
            Col(grillaMovimientos, "detalle", "Detalle", 17);
        }

        // ---------------------------------------------------------------- presentacion

        private static void Col(DataGridView g, string nombre, string encabezado, float peso, string formato = null, bool derecha = false, bool visible = true)
        {
            DataGridViewColumn c = g.Columns[nombre];
            c.HeaderText = encabezado;
            c.FillWeight = peso;
            c.Visible = visible;
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
            DataGridView g = (DataGridView)sender;
            if (e.RowIndex < 0 || e.ColumnIndex < 0 || e.Value == null || e.Value == DBNull.Value)
            {
                return;
            }
            string columna = g.Columns[e.ColumnIndex].Name;
            if (columna != "cobro" && columna != "detalle")
            {
                return;
            }
            string valor = Convert.ToString(e.Value);
            if (valor == "Pendiente")
            {
                e.CellStyle.ForeColor = Negativo;
            }
            else if (valor == "Parcial")
            {
                e.CellStyle.ForeColor = Color.FromArgb(176, 96, 0);
            }
            else if (valor.IndexOf("Anulado", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                e.CellStyle.ForeColor = TextoSecundario;
            }
        }

        private void TituloPestanas()
        {
            paginaPendientes.Text = "Estado de cuenta" + (pendientes != null ? " (" + pendientes.Rows.Count + ")" : "");
            paginaMovimientos.Text = "Histórico de movimientos" + (movimientos != null ? " (" + movimientos.Rows.Count + ")" : "");
            pestanas.Invalidate();
        }

        private void MostrarTotales()
        {
            // en mora: lo vencido que todavia tiene saldo
            lblMora.Text = "";
            if (clienteActual != null && pendientes != null && pestanas.SelectedTab == paginaPendientes)
            {
                decimal mora = 0m;
                foreach (DataRow r in pendientes.Rows)
                {
                    if (r["FechaVenc"] != DBNull.Value && r["sal_actual"] != DBNull.Value && Convert.ToDateTime(r["FechaVenc"]).Date < DateTime.Today &&
                        Convert.ToDecimal(r["sal_actual"]) != 0m && (r["Status"] == DBNull.Value || Convert.ToInt32(r["Status"]) != 1))
                    {
                        mora += Convert.ToDecimal(r["sal_actual"]);
                    }
                }
                lblMora.Text = "En mora: " + mora.ToString("n2");
            }

            if (pestanas.SelectedTab == paginaMovimientos)
            {
                lblTotales.Text = movimientos == null ? "" :
                    "Ventas " + SumaTipos(movimientos, "FAV", "NDE") + "     Devoluciones " + SumaTipos(movimientos, "DEV") +
                    "     Recibos " + SumaTipos(movimientos, "REI") + "     Retenciones " + SumaTipos(movimientos, "RIVA");
            }
            else
            {
                lblTotales.Text = pendientes == null ? "" :
                    "Monto " + Suma(pendientes, "MontoTotal") + "     Cobrado " + Suma(pendientes, "MontoCob") + "     Saldo " + Suma(pendientes, "sal_actual");
            }
        }

        private static string Suma(DataTable dt, string columna)
        {
            decimal total = 0m;
            foreach (DataRow r in dt.Rows)
            {
                if (r[columna] != DBNull.Value)
                {
                    total += Convert.ToDecimal(r[columna]);
                }
            }
            return total.ToString("n2");
        }

        private static string SumaTipos(DataTable dt, params string[] tipos)
        {
            decimal total = 0m;
            foreach (DataRow r in dt.Rows)
            {
                string tipo = Convert.ToString(r["tipo"]).Trim();
                if (Array.IndexOf(tipos, tipo) >= 0 && r["monto"] != DBNull.Value)
                {
                    total += Convert.ToDecimal(r["monto"]);
                }
            }
            return total.ToString("n2");
        }

        // ---------------------------------------------------------------- lista para elegir cuando hay varios clientes

        private sealed class SelectorCliente : Form
        {
            public string Codigo;
            private readonly DataGridView lista = new DataGridView();

            public SelectorCliente(DataTable clientes, bool recortado)
            {
                AutoScaleMode = AutoScaleMode.None;
                Font = new Font(Fuente, 9F);
                BackColor = Fondo;
                Text = "Elegir cliente";
                FormBorderStyle = FormBorderStyle.FixedDialog;
                StartPosition = FormStartPosition.CenterParent;
                MinimizeBox = false;
                MaximizeBox = false;
                ShowInTaskbar = false;
                ClientSize = new Size(640, 420);

                Label aviso = new Label();
                aviso.Text = recortado ? "Hay más de " + MaximoClientes + " clientes con ese texto: se muestran los primeros. Escriba más del nombre para acotar."
                                       : "Hay varios clientes con ese texto. Elija uno (doble clic o Enter).";
                aviso.ForeColor = recortado ? Negativo : TextoSecundario;
                aviso.SetBounds(14, 10, 612, 34);

                lista.SetBounds(14, 48, 612, 320);
                lista.AllowUserToAddRows = false;
                lista.AllowUserToDeleteRows = false;
                lista.ReadOnly = true;
                lista.MultiSelect = false;
                lista.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                lista.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                EstiloGrilla(lista);
                // las columnas existen cuando la grilla ya enlazo los datos: se configuran en ese momento
                lista.DataBindingComplete += delegate
                {
                    ConfigurarColumna("cli_codigo", "Código", 22);
                    ConfigurarColumna("cli_nombre", "Nombre", 56);
                    ConfigurarColumna("cli_rif", "RIF", 22);
                };
                lista.DataSource = clientes;
                lista.CellDoubleClick += delegate (object s, DataGridViewCellEventArgs ev) { if (ev.RowIndex >= 0) { Elegir(); } };
                lista.KeyDown += delegate (object s, KeyEventArgs ev)
                {
                    if (ev.KeyCode == Keys.Enter)
                    {
                        ev.SuppressKeyPress = true;
                        Elegir();
                    }
                };

                Button elegir = new Button();
                elegir.Text = "Elegir";
                elegir.SetBounds(432, 378, 94, 30);
                elegir.FlatStyle = FlatStyle.Flat;
                elegir.BackColor = Azul;
                elegir.ForeColor = Color.White;
                elegir.FlatAppearance.BorderSize = 0;
                elegir.Font = new Font(Fuente, 9F, FontStyle.Bold);
                elegir.Click += delegate { Elegir(); };

                Button cancelar = new Button();
                cancelar.Text = "Cancelar";
                cancelar.SetBounds(532, 378, 94, 30);
                cancelar.FlatStyle = FlatStyle.Flat;
                cancelar.BackColor = Color.White;
                cancelar.ForeColor = Azul;
                cancelar.FlatAppearance.BorderColor = Borde;
                cancelar.DialogResult = DialogResult.Cancel;

                CancelButton = cancelar;
                Controls.AddRange(new Control[] { aviso, lista, elegir, cancelar });
                Shown += delegate { lista.Focus(); };
            }

            private void ConfigurarColumna(string nombre, string encabezado, float peso)
            {
                DataGridViewColumn c = lista.Columns[nombre];
                if (c != null)
                {
                    c.HeaderText = encabezado;
                    c.FillWeight = peso;
                }
            }

            private void Elegir()
            {
                if (lista.CurrentRow == null)
                {
                    return;
                }
                Codigo = Convert.ToString(lista.CurrentRow.Cells["cli_codigo"].Value);
                DialogResult = DialogResult.OK;
            }
        }
    }
}
