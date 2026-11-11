using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using lebrun.clasesData;
using static lebrun.formularios.gerencial.EstiloGerencial;

namespace lebrun.formularios.facturacion
{
    // Estado de cuentas de los clientes: los documentos de cuentas por cobrar (admsalcli) con lo cobrado, el saldo y el estado de cobro
    // (Pagado, Parcial o Pendiente, de admdoccli.dcli_estatus). Se puede filtrar por cliente (codigo o parte del nombre), por fecha de emision y
    // por "solo con saldo". Es solo de consulta: no cambia nada en la base de datos.
    public class lbxEstadoCuentas : Form
    {
        private const int MaximoFilas = 2000;

        private readonly ConexionBD baseDatos = new ConexionBD();
        private readonly DataGridView grilla = new DataGridView();
        private Label cabeceraGrilla;
        private readonly TextBox txtCliente = new TextBox();
        private readonly DateTimePicker dtpDesde = new DateTimePicker();
        private readonly DateTimePicker dtpHasta = new DateTimePicker();
        private readonly CheckBox chkConSaldo = new CheckBox();
        private readonly Button cmdBuscar = new Button();
        private readonly Button cmdLimpiar = new Button();
        private readonly Button cmdCerrar = new Button();
        private readonly Label lblTotales = new Label();
        private readonly Label lblAviso = new Label();

        protected override void OnLoad(EventArgs e)
        {
            Armar();
            base.OnLoad(e);
            txtCliente.Focus();
        }

        private void Armar()
        {
            SuspendLayout();
            AutoScaleMode = AutoScaleMode.None;
            Font = new Font(Fuente, 9F);
            BackColor = Fondo;
            Text = "Estado de cuentas";
            Rectangle area = Screen.PrimaryScreen.WorkingArea;
            ClientSize = new Size(Math.Min(1180, area.Width - 60), Math.Min(640, area.Height - 120));
            MinimumSize = new Size(960, 520);
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
            titulo.Text = "Estado de cuentas";
            titulo.Dock = DockStyle.Fill;
            titulo.Font = new Font(Fuente, 15F, FontStyle.Bold);
            titulo.ForeColor = Azul;
            Label descripcion = new Label();
            descripcion.Text = "Documentos por cobrar de los clientes: monto, cobrado, saldo y estado de cobro (Pagado, Parcial o Pendiente).";
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
            Control tarjetaGrilla = Tarjeta("Documentos", grilla, out cabeceraGrilla);
            tarjetaGrilla.Dock = DockStyle.Fill;
            tarjetaGrilla.Margin = new Padding(0, 0, 0, 6);

            lblTotales.Dock = DockStyle.Fill;
            lblTotales.TextAlign = ContentAlignment.MiddleRight;
            lblTotales.Font = new Font(Fuente, 10F, FontStyle.Bold);
            lblTotales.ForeColor = Azul;

            TableLayoutPanel raiz = new TableLayoutPanel();
            raiz.Dock = DockStyle.Fill;
            raiz.ColumnCount = 1;
            raiz.RowCount = 4;
            raiz.Padding = new Padding(12, 10, 12, 8);
            raiz.RowStyles.Add(new RowStyle(SizeType.Absolute, 74));
            raiz.RowStyles.Add(new RowStyle(SizeType.Absolute, 64));
            raiz.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            raiz.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));
            raiz.Controls.Add(encabezado, 0, 0);
            raiz.Controls.Add(ArmarFiltros(), 0, 1);
            raiz.Controls.Add(tarjetaGrilla, 0, 2);
            raiz.Controls.Add(lblTotales, 0, 3);
            Controls.Clear();
            Controls.Add(raiz);
            ResumeLayout(true);
        }

        private Control ArmarFiltros()
        {
            txtCliente.Width = 170;
            txtCliente.MaxLength = 60;
            txtCliente.KeyDown += delegate (object s, KeyEventArgs ev)
            {
                if (ev.KeyCode == Keys.Enter)
                {
                    ev.SuppressKeyPress = true;
                    Buscar();
                }
            };
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
            lblAviso.AutoSize = true;
            lblAviso.ForeColor = Negativo;

            FlowLayoutPanel f = new FlowLayoutPanel();
            f.Dock = DockStyle.Fill;
            f.BackColor = Color.White;
            f.Padding = new Padding(10, 8, 10, 4);
            f.Margin = new Padding(0, 0, 0, 10);
            ConBorde(f);
            f.WrapContents = false;
            f.Controls.Add(Alinear(EtiquetaNueva("Cliente (código o nombre)")));
            f.Controls.Add(txtCliente);
            f.Controls.Add(Alinear(EtiquetaNueva("Desde")));
            f.Controls.Add(dtpDesde);
            f.Controls.Add(Alinear(EtiquetaNueva("hasta")));
            f.Controls.Add(dtpHasta);
            chkConSaldo.Margin = new Padding(14, 6, 10, 0);
            f.Controls.Add(chkConSaldo);
            f.Controls.Add(cmdBuscar);
            f.Controls.Add(cmdLimpiar);
            f.Controls.Add(cmdCerrar);
            lblAviso.Margin = new Padding(10, 9, 0, 0);
            f.Controls.Add(lblAviso);
            return f;
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

        private static string Texto(string valor)
        {
            return (valor ?? "").Trim().Replace("\\", "\\\\").Replace("'", "''");
        }

        // para LIKE: los comodines % y _ escritos por el usuario se toman como texto
        private static string ParaLike(string valorEscapado)
        {
            return valorEscapado.Replace("%", "\\%").Replace("_", "\\_");
        }

        private void Limpiar()
        {
            txtCliente.Clear();
            dtpDesde.Checked = false;
            dtpHasta.Checked = false;
            chkConSaldo.Checked = true;
            grilla.DataSource = null;
            lblTotales.Text = "";
            lblAviso.Text = "";
            ActualizarContador(grilla, cabeceraGrilla, "Documentos");
            txtCliente.Focus();
        }

        private void Buscar()
        {
            lblAviso.Text = "";
            try
            {
                string filtro = "";
                string cliente = Texto(txtCliente.Text);
                if (cliente.Length > 0)
                {
                    filtro += " AND (s.cli_codigo = '" + cliente + "' OR c.cli_nombre LIKE '%" + ParaLike(cliente) + "%')";
                }
                if (dtpDesde.Checked)
                {
                    filtro += " AND s.FechaEmision >= '" + dtpDesde.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture) + "'";
                }
                if (dtpHasta.Checked)
                {
                    filtro += " AND s.FechaEmision <= '" + dtpHasta.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture) + "'";
                }
                if (chkConSaldo.Checked)
                {
                    filtro += " AND s.sal_actual <> 0 AND s.Status <> 1";
                }
                // el estado de cobro sale de admdoccli.dcli_estatus; si el documento no esta en admdoccli (un anticipo, por ejemplo) se calcula con el saldo
                string sql = "SELECT s.cli_codigo, c.cli_nombre, s.TipoDoc, s.NroDocum, s.FechaEmision, s.FechaVenc, s.MontoTotal, s.MontoCob, s.sal_actual, " +
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
                grilla.DataSource = dt;
                Encabezados();
                ActualizarContador(grilla, cabeceraGrilla, "Documentos");
                MostrarTotales(dt);
                if (recortado)
                {
                    lblAviso.Text = "Se muestran los primeros " + MaximoFilas + " documentos: use un filtro para acotar.";
                }
                else if (dt.Rows.Count == 0)
                {
                    lblAviso.Text = "No hay documentos con ese filtro.";
                }
            }
            catch (Exception ex)
            {
                RegistroErrores.Registrar(ex, "Estado de cuentas");
                MessageBox.Show("No se pudo consultar el estado de cuentas.\n\n" + ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Encabezados()
        {
            Col("cli_codigo", "Cliente", 9);
            Col("cli_nombre", "Nombre", 22);
            Col("TipoDoc", "Tipo", 5);
            Col("NroDocum", "Número", 11);
            Col("FechaEmision", "Emisión", 9, "dd/MM/yyyy");
            Col("FechaVenc", "Vence", 9, "dd/MM/yyyy");
            Col("MontoTotal", "Monto", 10, "n2", true);
            Col("MontoCob", "Cobrado", 10, "n2", true);
            Col("sal_actual", "Saldo", 10, "n2", true);
            Col("cobro", "Cobro", 9);
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
            if (e.RowIndex < 0 || e.ColumnIndex < 0 || e.Value == null || e.Value == DBNull.Value || grilla.Columns[e.ColumnIndex].Name != "cobro")
            {
                return;
            }
            string cobro = Convert.ToString(e.Value);
            if (cobro == "Pendiente")
            {
                e.CellStyle.ForeColor = Negativo;
            }
            else if (cobro == "Parcial")
            {
                e.CellStyle.ForeColor = Color.FromArgb(176, 96, 0);
            }
        }

        private void MostrarTotales(DataTable dt)
        {
            lblTotales.Text = "Monto " + Suma(dt, "MontoTotal") + "     Cobrado " + Suma(dt, "MontoCob") + "     Saldo " + Suma(dt, "sal_actual");
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
    }
}
