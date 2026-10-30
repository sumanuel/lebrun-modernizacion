using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace lebrun.formularios.gerencial
{
    // Piezas de diseno compartidas por las pantallas de Gerencial (Consulta General Art., Consulta Gerentes...): colores, tarjetas,
    // cifras destacadas, campos de solo lectura, grillas y pestanas. Los colores salen de los que ya usa el sistema: azul
    // (49,106,197), celeste de las grillas (232,240,250), verde oscuro del nombre del producto y rojo (192,0,0) de los avisos.
    internal static class EstiloGerencial
    {
        public static readonly Color Azul = Color.FromArgb(49, 106, 197);
        public static readonly Color AzulClaro = Color.FromArgb(232, 240, 250);
        public static readonly Color Fondo = Color.FromArgb(244, 246, 250);
        public static readonly Color Borde = Color.FromArgb(205, 214, 228);
        public static readonly Color CampoFondo = Color.FromArgb(238, 242, 248);
        public static readonly Color TextoSecundario = Color.FromArgb(84, 94, 110);
        public static readonly Color Negativo = Color.FromArgb(192, 0, 0);
        public const string Fuente = "Segoe UI";
        public const int AltoFila = 26;

        // Nombres de los campos de texto (no numericos) de la pantalla que se esta disenando: van alineados a la izquierda; el
        // resto de los campos se alinea a la derecha. Cada pantalla lo asigna antes de construir su diseno.
        public static HashSet<string> CamposTexto = new HashSet<string>();

        public static TableLayoutPanel Columnas(float[] porcentajes)
        {
            TableLayoutPanel t = new TableLayoutPanel();
            t.Dock = DockStyle.Fill;
            t.BackColor = Fondo;
            t.Margin = new Padding(0);
            t.ColumnCount = porcentajes.Length;
            t.RowCount = 1;
            foreach (float p in porcentajes)
            {
                t.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, p));
            }
            t.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            return t;
        }

        // Rejilla de pares etiqueta/valor: por cada par, una columna de ancho automatico (etiqueta) y otra que se reparte el resto.
        public static TableLayoutPanel Rejilla(int pares, int filas)
        {
            TableLayoutPanel t = new TableLayoutPanel();
            t.Dock = DockStyle.Fill;
            t.BackColor = Color.White;
            t.Margin = new Padding(0);
            t.ColumnCount = pares * 2;
            t.RowCount = filas + 1;
            for (int i = 0; i < pares; i++)
            {
                t.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
                t.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f / pares));
            }
            for (int i = 0; i < filas; i++)
            {
                t.RowStyles.Add(new RowStyle(SizeType.Absolute, AltoFila));
            }
            // fila de relleno: sin ella la ultima fila absorbe el espacio sobrante y se estira
            t.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            return t;
        }

        public static void Par(TableLayoutPanel t, int fila, int par, Label etiqueta, string texto, Control valor)
        {
            t.Controls.Add(Etiqueta(etiqueta, texto), par * 2, fila);
            if (valor != null)
            {
                t.Controls.Add(Campo(valor), par * 2 + 1, fila);
            }
        }

        public static Label Etiqueta(Label l, string texto)
        {
            l.AutoSize = true;
            l.Dock = DockStyle.None;
            l.Anchor = AnchorStyles.Left;
            l.Margin = new Padding(0, 0, 8, 0);
            l.TextAlign = ContentAlignment.MiddleLeft;
            l.Font = new Font(Fuente, 9F);
            l.ForeColor = TextoSecundario;
            l.BackColor = Color.Transparent;
            l.Visible = true;
            if (texto != null)
            {
                l.Text = texto;
            }
            return l;
        }

        public static Label EtiquetaNueva(string texto)
        {
            return Etiqueta(new Label(), texto);
        }

        public static Label Subtitulo(string texto)
        {
            Label l = Etiqueta(new Label(), texto);
            l.Font = new Font(Fuente, 8.5F, FontStyle.Bold);
            l.ForeColor = Azul;
            return l;
        }

        // Devuelve el control que debe ir en la celda: los cuadros de texto se envuelven en un panel con fondo para que se vean
        // como campos; la lista de precios se estiliza directamente.
        public static Control Campo(Control c)
        {
            TextBox texto = c as TextBox;
            if (texto == null)
            {
                c.Anchor = AnchorStyles.Left | AnchorStyles.Right;
                c.Margin = new Padding(0, 2, 10, 2);
                ComboBox lista = c as ComboBox;
                if (lista != null)
                {
                    lista.FlatStyle = FlatStyle.Flat;
                    lista.BackColor = CampoFondo;
                    lista.Font = new Font(Fuente, 9F, FontStyle.Bold);
                }
                return c;
            }
            texto.BorderStyle = BorderStyle.None;
            texto.ReadOnly = true;
            texto.TabStop = false;
            texto.BackColor = CampoFondo;
            texto.ForeColor = Color.Black;
            texto.Font = new Font(Fuente, 9F, FontStyle.Bold);
            texto.TextAlign = CamposTexto.Contains(texto.Name) ? HorizontalAlignment.Left : HorizontalAlignment.Right;
            texto.Dock = DockStyle.Fill;
            texto.Margin = new Padding(0);

            Panel envoltorio = new Panel();
            envoltorio.BackColor = CampoFondo;
            envoltorio.Dock = DockStyle.Fill;
            envoltorio.Margin = new Padding(0, 2, 10, 2);
            envoltorio.Padding = new Padding(6, 3, 4, 0);
            envoltorio.Controls.Add(texto);
            return envoltorio;
        }

        // Varios campos en una misma celda (por ejemplo los tres descuentos).
        public static Control Varios(params TextBox[] campos)
        {
            TableLayoutPanel t = new TableLayoutPanel();
            t.Dock = DockStyle.Fill;
            t.BackColor = Color.White;
            t.Margin = new Padding(0);
            t.ColumnCount = campos.Length;
            t.RowCount = 1;
            t.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            for (int i = 0; i < campos.Length; i++)
            {
                t.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f / campos.Length));
                Control c = Campo(campos[i]);
                c.Margin = new Padding(0, 2, i == campos.Length - 1 ? 10 : 4, 2);
                c.Padding = new Padding(3, 3, 2, 0);
                t.Controls.Add(c, i, 0);
            }
            return t;
        }

        public static Control Tarjeta(string titulo, Control cuerpo)
        {
            Label cabecera;
            return Tarjeta(titulo, cuerpo, out cabecera);
        }

        public static Control Tarjeta(string titulo, Control cuerpo, out Label cabeceraCreada)
        {
            TableLayoutPanel c = new TableLayoutPanel();
            c.Dock = DockStyle.Fill;
            c.BackColor = Color.White;
            c.Margin = new Padding(0, 0, 10, 10);
            c.Padding = new Padding(12, 9, 6, 6);
            c.ColumnCount = 1;
            c.RowCount = 2;
            c.RowStyles.Add(new RowStyle(SizeType.Absolute, 26));
            c.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            ConBorde(c);

            Label cabecera = new Label();
            cabeceraCreada = cabecera;
            cabecera.Text = titulo;
            cabecera.AutoSize = false;
            cabecera.Dock = DockStyle.Fill;
            cabecera.TextAlign = ContentAlignment.MiddleLeft;
            cabecera.Font = new Font(Fuente, 9.5F, FontStyle.Bold);
            cabecera.ForeColor = Azul;
            cabecera.Margin = new Padding(0);
            c.Controls.Add(cabecera, 0, 0);
            cuerpo.Dock = DockStyle.Fill;
            c.Controls.Add(cuerpo, 0, 1);
            return c;
        }

        // Cifra destacada: etiqueta pequena arriba y el valor grande debajo.
        public static Control Indicador(Label etiqueta, string texto, TextBox valor)
        {
            TableLayoutPanel c = new TableLayoutPanel();
            c.Dock = DockStyle.Fill;
            c.BackColor = Color.White;
            c.Margin = new Padding(0, 0, 10, 10);
            c.Padding = new Padding(12, 8, 6, 4);
            c.ColumnCount = 1;
            c.RowCount = 2;
            c.RowStyles.Add(new RowStyle(SizeType.Absolute, 20));
            c.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            ConBorde(c);

            Etiqueta(etiqueta, texto);
            etiqueta.Font = new Font(Fuente, 8.5F);
            etiqueta.Margin = new Padding(0);
            valor.BorderStyle = BorderStyle.None;
            valor.ReadOnly = true;
            valor.TabStop = false;
            valor.BackColor = Color.White;
            valor.ForeColor = Azul;
            valor.Font = new Font(Fuente, 17F, FontStyle.Bold);
            valor.TextAlign = HorizontalAlignment.Left;
            valor.Dock = DockStyle.None;
            valor.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            valor.Margin = new Padding(0);
            c.Controls.Add(etiqueta, 0, 0);
            c.Controls.Add(valor, 0, 1);
            return c;
        }

        // Cifra destacada que repite el valor de un campo de otra pestana: se actualiza sola cuando ese campo cambia.
        public static Control IndicadorEspejo(string titulo, TextBox origen)
        {
            Label valor = new Label();
            valor.AutoSize = false;
            valor.Font = new Font(Fuente, 17F, FontStyle.Bold);
            valor.ForeColor = Azul;
            valor.BackColor = Color.White;
            valor.TextAlign = ContentAlignment.MiddleLeft;
            valor.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            valor.Margin = new Padding(0);
            valor.Height = 36;
            valor.Text = origen.Text;
            origen.TextChanged += delegate { valor.Text = origen.Text; };

            TableLayoutPanel c = new TableLayoutPanel();
            c.Dock = DockStyle.Fill;
            c.BackColor = Color.White;
            c.Margin = new Padding(0, 0, 10, 10);
            c.Padding = new Padding(12, 8, 6, 4);
            c.ColumnCount = 1;
            c.RowCount = 2;
            c.RowStyles.Add(new RowStyle(SizeType.Absolute, 20));
            c.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            ConBorde(c);
            Label etiqueta = EtiquetaNueva(titulo);
            etiqueta.Font = new Font(Fuente, 8.5F);
            etiqueta.Margin = new Padding(0);
            c.Controls.Add(etiqueta, 0, 0);
            c.Controls.Add(valor, 0, 1);
            return c;
        }

        public static void ConBorde(Control c)
        {
            c.Paint += PintarTarjeta;
            c.Resize += delegate { c.Invalidate(); };
        }

        private static void PintarTarjeta(object sender, PaintEventArgs e)
        {
            Control c = (Control)sender;
            using (Pen lapiz = new Pen(Borde))
            {
                e.Graphics.DrawRectangle(lapiz, 0, 0, c.Width - 1, c.Height - 1);
            }
            using (SolidBrush pincel = new SolidBrush(Azul))
            {
                e.Graphics.FillRectangle(pincel, 0, 0, c.Width, 3);
            }
        }

        public static void EstiloGrilla(DataGridView g)
        {
            g.BorderStyle = BorderStyle.None;
            g.BackgroundColor = Color.White;
            g.EnableHeadersVisualStyles = false;
            g.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            g.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            g.ColumnHeadersHeight = 30;
            g.ColumnHeadersDefaultCellStyle.BackColor = Azul;
            g.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            g.ColumnHeadersDefaultCellStyle.SelectionBackColor = Azul;
            g.ColumnHeadersDefaultCellStyle.Font = new Font(Fuente, 9F, FontStyle.Bold);
            g.DefaultCellStyle.Font = new Font(Fuente, 9F);
            g.DefaultCellStyle.SelectionBackColor = Color.FromArgb(190, 212, 240);
            g.DefaultCellStyle.SelectionForeColor = Color.Black;
            g.AlternatingRowsDefaultCellStyle.BackColor = AzulClaro;
            g.GridColor = Borde;
            g.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            g.RowHeadersVisible = false;
            g.RowTemplate.Height = 24;
            g.AllowUserToResizeRows = false;
        }

        public static void EstiloPestanas(TabControl pestanas)
        {
            pestanas.DrawMode = TabDrawMode.OwnerDrawFixed;
            pestanas.SizeMode = TabSizeMode.Fixed;
            pestanas.ItemSize = new Size(130, 32);
            pestanas.Font = new Font(Fuente, 9.5F, FontStyle.Bold);
            pestanas.DrawItem += DibujarPestana;
        }

        private static void DibujarPestana(object sender, DrawItemEventArgs e)
        {
            TabControl pestanas = (TabControl)sender;
            Rectangle zona = pestanas.GetTabRect(e.Index);
            bool activa = e.Index == pestanas.SelectedIndex;
            using (SolidBrush fondo = new SolidBrush(Fondo))
            {
                e.Graphics.FillRectangle(fondo, zona);
            }
            TextRenderer.DrawText(e.Graphics, pestanas.TabPages[e.Index].Text, pestanas.Font, zona, activa ? Azul : TextoSecundario,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            if (activa)
            {
                using (SolidBrush linea = new SolidBrush(Azul))
                {
                    e.Graphics.FillRectangle(linea, zona.Left + 8, zona.Bottom - 3, zona.Width - 16, 3);
                }
            }
        }

        // Cuenta de registros junto al titulo de cada tarjeta con grilla.
        public static void ActualizarContador(DataGridView g, Label cabecera, string titulo)
        {
            if (cabecera == null)
            {
                return;
            }
            int n = g.Rows.Count;
            if (n > 0)
            {
                cabecera.Text = titulo + "  ·  " + n + (n == 1 ? " registro" : " registros");
            }
            else
            {
                cabecera.Text = g.DataSource != null ? titulo + "  ·  sin registros" : titulo;
            }
        }

        public static void AlinearNumericas(DataGridView g, params string[] columnas)
        {
            foreach (string nombre in columnas)
            {
                if (g.Columns.Contains(nombre))
                {
                    g.Columns[nombre].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    g.Columns[nombre].DefaultCellStyle.Format = "n2";
                    g.Columns[nombre].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
            }
        }

        // Encabezado de las pantallas de consulta de producto: codigo con boton de busqueda y, al lado, el nombre del producto.
        public static Control EncabezadoProducto(Label etiquetaCodigo, TextBox codigo, Button buscar, Label nombre)
        {
            TableLayoutPanel t = new TableLayoutPanel();
            t.Dock = DockStyle.Fill;
            t.BackColor = Color.White;
            t.Margin = new Padding(0, 0, 0, 10);
            t.Padding = new Padding(14, 6, 14, 6);
            t.ColumnCount = 4;
            t.RowCount = 1;
            t.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            t.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 210));
            t.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 52));
            t.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            t.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            ConBorde(t);

            Etiqueta(etiquetaCodigo, "Código del producto");
            etiquetaCodigo.Font = new Font(Fuente, 9.5F, FontStyle.Bold);
            etiquetaCodigo.Margin = new Padding(0, 0, 10, 0);

            codigo.BorderStyle = BorderStyle.FixedSingle;
            codigo.BackColor = Color.White;
            codigo.Font = new Font(Fuente, 12F, FontStyle.Bold);
            codigo.Dock = DockStyle.None;
            codigo.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            codigo.Margin = new Padding(0, 0, 8, 0);

            buscar.FlatStyle = FlatStyle.Flat;
            buscar.FlatAppearance.BorderColor = Borde;
            buscar.BackColor = Color.White;
            buscar.Cursor = Cursors.Hand;
            buscar.Size = new Size(40, 32);
            buscar.Anchor = AnchorStyles.Left;
            buscar.Margin = new Padding(0);

            nombre.AutoSize = false;
            nombre.Dock = DockStyle.Fill;
            nombre.AutoEllipsis = true;
            nombre.TextAlign = ContentAlignment.MiddleLeft;
            nombre.Font = new Font(Fuente, 15F, FontStyle.Bold);
            nombre.ForeColor = Color.DarkGreen;
            nombre.Margin = new Padding(8, 0, 0, 0);
            if (nombre.Text == "Esperado Codigo")
            {
                nombre.Text = "Escriba un código y presione Enter";
            }

            t.Controls.Add(etiquetaCodigo, 0, 0);
            t.Controls.Add(codigo, 1, 0);
            t.Controls.Add(buscar, 2, 0);
            t.Controls.Add(nombre, 3, 0);
            return t;
        }

        // Arma la pantalla: encabezado arriba y el cuerpo (pestanas) ocupando el resto. Quita los controles originales del formulario.
        public static void ArmarPantalla(Form pantalla, Control encabezado, Control cuerpo)
        {
            TableLayoutPanel raiz = new TableLayoutPanel();
            raiz.Dock = DockStyle.Fill;
            raiz.ColumnCount = 1;
            raiz.RowCount = 2;
            raiz.Padding = new Padding(12, 10, 12, 10);
            raiz.RowStyles.Add(new RowStyle(SizeType.Absolute, 70));
            raiz.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            cuerpo.Dock = DockStyle.Fill;
            cuerpo.Margin = new Padding(0);
            pantalla.Controls.Clear();
            raiz.Controls.Add(encabezado, 0, 0);
            raiz.Controls.Add(cuerpo, 0, 1);
            pantalla.Controls.Add(raiz);
        }

        // Fondo de pagina de una pestana (las pestanas se dibujan con el fondo de la aplicacion, no con el del sistema).
        public static void PaginaConContenido(TabPage pagina, Control contenido, Size minimo)
        {
            pagina.Controls.Clear();
            pagina.Padding = new Padding(10, 10, 0, 0);
            pagina.BackColor = Fondo;
            pagina.UseVisualStyleBackColor = false;
            pagina.AutoScroll = true;
            contenido.MinimumSize = minimo;
            pagina.Controls.Add(contenido);
        }
    }
}
