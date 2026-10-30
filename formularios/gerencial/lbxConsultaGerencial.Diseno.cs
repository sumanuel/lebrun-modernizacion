using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace lebrun.formularios.gerencial
{
    // Diseno visual de la pantalla. Se aplica al crear el formulario (AplicarDiseno): reubica y da estilo a los controles que
    // declara lbxConsultaGerencial.Designer.cs, sin cambiar sus nombres ni la logica que los llena (lbxConsultaGerencial.cs).
    // Los colores salen de los que ya usa el sistema: azul (49,106,197), celeste de las grillas (232,240,250), verde oscuro del
    // nombre del producto y rojo (192,0,0) de los avisos.
    public partial class lbxConsultaGerencial
    {
        private static readonly Color Azul = Color.FromArgb(49, 106, 197);
        private static readonly Color AzulClaro = Color.FromArgb(232, 240, 250);
        private static readonly Color Fondo = Color.FromArgb(244, 246, 250);
        private static readonly Color Borde = Color.FromArgb(205, 214, 228);
        private static readonly Color CampoFondo = Color.FromArgb(238, 242, 248);
        private static readonly Color TextoSecundario = Color.FromArgb(84, 94, 110);
        private static readonly Color Negativo = Color.FromArgb(192, 0, 0);
        private const string Fuente = "Segoe UI";
        private const int AltoFila = 26;

        // Controles que crea el diseno (no estan en el Designer.cs)
        private DataGridView dataGridView4;
        private Label lblNotaExistencias;
        private Label cabeceraMovimientos, cabeceraCompras, cabeceraCostoMov, cabeceraHistorial;

        // Campos de texto (no numericos): van alineados a la izquierda; el resto se alinea a la derecha.
        private readonly HashSet<string> camposTexto = new HashSet<string>
        {
            "txtCateg", "txtSubCat", "txtOrigen", "txtUniM", "txtOferta", "txtEstado", "txtProve", "txtUltCompra", "txtDocComp",
            "txtClient", "txtUltVent", "txtDocumVent", "txtUltAjus", "txtDocum", "txtProvee", "txtPais", "txtCodAran", "txtDesMon",
            "txtSigMon"
        };

        private void AplicarDiseno()
        {
            SuspendLayout();
            AutoScaleMode = AutoScaleMode.None;
            Font = new Font(Fuente, 9F);
            BackColor = Fondo;
            Text = "Consulta Gerencial";
            Rectangle area = Screen.PrimaryScreen.WorkingArea;
            ClientSize = new Size(Math.Min(1200, area.Width - 60), Math.Min(830, area.Height - 150));
            MinimumSize = new Size(900, 520);
            DoubleBuffered = true;

            Control encabezado = ConstruirEncabezado();
            ConstruirGeneral();
            dataGridView4 = new DataGridView();
            ConstruirExistencias();
            ConstruirCostos();
            foreach (DataGridView g in new DataGridView[] { dataGridView1, dataGridView2, dataGridView3, dataGridView4 })
            {
                EstiloGrilla(g);
            }
            dataGridView3.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView3.DataBindingComplete += dataGridView3_DataBindingComplete;
            dataGridView2.DataBindingComplete += dataGridView2_DataBindingComplete;
            dataGridView1.DataBindingComplete += dataGridView1_DataBindingComplete;
            dataGridView4.DataBindingComplete += dataGridView4_DataBindingComplete;
            dataGridView1.CellFormatting += FormatearMovimiento;
            dataGridView4.CellFormatting += FormatearMovimiento;
            dataGridView3.CellFormatting += dataGridView3_CellFormatting;
            EstiloPestanas();

            TableLayoutPanel raiz = new TableLayoutPanel();
            raiz.Dock = DockStyle.Fill;
            raiz.ColumnCount = 1;
            raiz.RowCount = 2;
            raiz.Padding = new Padding(12, 10, 12, 10);
            raiz.RowStyles.Add(new RowStyle(SizeType.Absolute, 70));
            raiz.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            txtRes1.Dock = DockStyle.Fill;
            txtRes1.Margin = new Padding(0);
            Controls.Clear();
            raiz.Controls.Add(encabezado, 0, 0);
            raiz.Controls.Add(txtRes1, 0, 1);
            Controls.Add(raiz);

            // los contenedores originales ya quedaron vacios: sus controles se movieron a las tarjetas nuevas
            Control[] viejos = { groupBox1, groupBox2, groupBox3, groupBox4, groupBox5, groupBox6, groupBox7, groupBox8, groupBox9,
                                 groupBox10, groupBox11, groupBox12, groupBox13 };
            foreach (Control viejo in viejos)
            {
                viejo.Dispose();
            }
            ResumeLayout(true);
        }

        private Control ConstruirEncabezado()
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

            Etiqueta(label21, "Código del producto");
            label21.Font = new Font(Fuente, 9.5F, FontStyle.Bold);
            label21.Margin = new Padding(0, 0, 10, 0);

            txtCodigo.BorderStyle = BorderStyle.FixedSingle;
            txtCodigo.BackColor = Color.White;
            txtCodigo.Font = new Font(Fuente, 12F, FontStyle.Bold);
            txtCodigo.Dock = DockStyle.None;
            txtCodigo.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtCodigo.Margin = new Padding(0, 0, 8, 0);

            button3.FlatStyle = FlatStyle.Flat;
            button3.FlatAppearance.BorderColor = Borde;
            button3.BackColor = Color.White;
            button3.Cursor = Cursors.Hand;
            button3.Size = new Size(40, 32);
            button3.Anchor = AnchorStyles.Left;
            button3.Margin = new Padding(0);

            label22.AutoSize = false;
            label22.Dock = DockStyle.Fill;
            label22.AutoEllipsis = true;
            label22.TextAlign = ContentAlignment.MiddleLeft;
            label22.Font = new Font(Fuente, 15F, FontStyle.Bold);
            label22.ForeColor = Color.DarkGreen;
            label22.Margin = new Padding(8, 0, 0, 0);
            if (label22.Text == "Esperado Codigo")
            {
                label22.Text = "Escriba un código y presione Enter";
            }

            t.Controls.Add(label21, 0, 0);
            t.Controls.Add(txtCodigo, 1, 0);
            t.Controls.Add(button3, 2, 0);
            t.Controls.Add(label22, 3, 0);
            return t;
        }

        private void ConstruirGeneral()
        {
            TableLayoutPanel g = new TableLayoutPanel();
            g.Dock = DockStyle.Fill;
            g.BackColor = Fondo;
            g.Padding = new Padding(10, 10, 0, 0);
            g.MinimumSize = new Size(1000, 600);
            g.ColumnCount = 1;
            g.RowCount = 4;
            g.RowStyles.Add(new RowStyle(SizeType.Absolute, 80));
            g.RowStyles.Add(new RowStyle(SizeType.Absolute, 182));
            g.RowStyles.Add(new RowStyle(SizeType.Absolute, 206));
            g.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            g.Controls.Add(FilaIndicadores(), 0, 0);
            g.Controls.Add(FilaTarjetas(new float[] { 30, 20, 50 }, TarjetaInformacion(), TarjetaCostos(), TarjetaMovimientos()), 0, 1);
            g.Controls.Add(FilaTarjetas(new float[] { 43, 28, 29 }, TarjetaProveedor(), TarjetaPromedio(), TarjetaAnioActual()), 0, 2);
            dataGridView3.Margin = new Padding(0);
            g.Controls.Add(Tarjeta("Historial de ventas", dataGridView3, out cabeceraHistorial), 0, 3);

            tabPage1.Controls.Clear();
            tabPage1.AutoScroll = true;
            tabPage1.BackColor = Fondo;
            tabPage1.UseVisualStyleBackColor = false;
            tabPage1.Controls.Add(g);
        }

        // Pestana Existencias: cifras de existencia arriba (las mismas de la pestana General) y el kardex del producto.
        private void ConstruirExistencias()
        {
            TableLayoutPanel t = new TableLayoutPanel();
            t.Dock = DockStyle.Fill;
            t.BackColor = Fondo;
            t.ColumnCount = 1;
            t.RowCount = 3;
            t.RowStyles.Add(new RowStyle(SizeType.Absolute, 80));
            t.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            t.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            TableLayoutPanel cifras = Columnas(new float[] { 25, 25, 25, 25 });
            cifras.Controls.Add(IndicadorEspejo("Existencia", txtExist), 0, 0);
            cifras.Controls.Add(IndicadorEspejo("Prefacturado", txtPrefa), 1, 0);
            cifras.Controls.Add(IndicadorEspejo("Pedidas", txtPedid), 2, 0);
            cifras.Controls.Add(IndicadorEspejo("Disponible", txtDispon), 3, 0);
            t.Controls.Add(cifras, 0, 0);

            lblNotaExistencias = new Label();
            lblNotaExistencias.AutoSize = true;
            lblNotaExistencias.MaximumSize = new Size(1100, 0);
            lblNotaExistencias.BackColor = AzulClaro;
            lblNotaExistencias.ForeColor = Azul;
            lblNotaExistencias.Font = new Font(Fuente, 9F);
            lblNotaExistencias.Padding = new Padding(10, 6, 10, 6);
            lblNotaExistencias.Margin = new Padding(0, 0, 10, 10);
            lblNotaExistencias.Visible = false;
            t.Controls.Add(lblNotaExistencias, 0, 1);

            dataGridView1.Margin = new Padding(0);
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            t.Controls.Add(Tarjeta("Movimientos de inventario", dataGridView1, out cabeceraMovimientos), 0, 2);
            PaginaConContenido(tabPage2, t);
        }

        // Pestana Costos: cifras de costo arriba, las compras del producto y el costo registrado en ventas y ajustes.
        private void ConstruirCostos()
        {
            TableLayoutPanel t = new TableLayoutPanel();
            t.Dock = DockStyle.Fill;
            t.BackColor = Fondo;
            t.ColumnCount = 1;
            t.RowCount = 3;
            t.RowStyles.Add(new RowStyle(SizeType.Absolute, 80));
            t.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
            t.RowStyles.Add(new RowStyle(SizeType.Percent, 50));

            TableLayoutPanel cifras = Columnas(new float[] { 25, 25, 25, 25 });
            cifras.Controls.Add(IndicadorEspejo("Último costo", txtUCost), 0, 0);
            cifras.Controls.Add(IndicadorEspejo("Costo promedio", txtCosProm), 1, 0);
            cifras.Controls.Add(IndicadorEspejo("Costo de reposición 4", txtCosRes4), 2, 0);
            cifras.Controls.Add(IndicadorEspejo("Precio de lista", txtPreList), 3, 0);
            t.Controls.Add(cifras, 0, 0);

            dataGridView2.Margin = new Padding(0);
            t.Controls.Add(Tarjeta("Compras del producto", dataGridView2, out cabeceraCompras), 0, 1);

            dataGridView4.Margin = new Padding(0);
            dataGridView4.AllowUserToAddRows = false;
            dataGridView4.AllowUserToDeleteRows = false;
            dataGridView4.ReadOnly = true;
            dataGridView4.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView4.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            t.Controls.Add(Tarjeta("Costo registrado en ventas y ajustes", dataGridView4, out cabeceraCostoMov), 0, 2);
            PaginaConContenido(tabPage3, t);
        }

        private void PaginaConContenido(TabPage pagina, Control contenido)
        {
            pagina.Controls.Clear();
            pagina.Padding = new Padding(10, 10, 0, 0);
            pagina.BackColor = Fondo;
            pagina.UseVisualStyleBackColor = false;
            pagina.AutoScroll = true;
            contenido.MinimumSize = new Size(900, 420);
            pagina.Controls.Add(contenido);
        }

        private Control FilaIndicadores()
        {
            TableLayoutPanel t = Columnas(new float[] { 20, 20, 20, 20, 20 });
            t.Controls.Add(Indicador(label31, "Existencia", txtExist), 0, 0);
            t.Controls.Add(Indicador(label34, "Disponible", txtDispon), 1, 0);
            t.Controls.Add(Indicador(label40, "Precio de lista", txtPreList), 2, 0);
            t.Controls.Add(Indicador(label35, "Último costo", txtUCost), 3, 0);
            t.Controls.Add(Indicador(label45, "Costo promedio", txtCosProm), 4, 0);
            return t;
        }

        private Control FilaTarjetas(float[] porcentajes, params Control[] tarjetas)
        {
            TableLayoutPanel t = Columnas(porcentajes);
            for (int i = 0; i < tarjetas.Length; i++)
            {
                t.Controls.Add(tarjetas[i], i, 0);
            }
            return t;
        }

        private Control TarjetaInformacion()
        {
            TableLayoutPanel t = Rejilla(2, 5);
            Par(t, 0, 0, label30, "Categoría", txtCateg);
            Par(t, 0, 1, label27, "Sub-categoría", txtSubCat);
            Par(t, 1, 0, label36, "Origen", txtOrigen);
            Par(t, 1, 1, label29, "U. medida", txtUniM);
            Par(t, 2, 0, label28, "Precios", cmbPrecios);
            Par(t, 2, 1, label24, "Oferta", txtOferta);
            Par(t, 3, 0, label26, "Estado", txtEstado);
            Par(t, 3, 1, label32, "Prefac.", txtPrefa);
            Par(t, 4, 0, label33, "Pedidas", txtPedid);
            return Tarjeta("Información del producto", t);
        }

        private Control TarjetaCostos()
        {
            TableLayoutPanel t = Rejilla(1, 5);
            Par(t, 0, 0, label41, "C. reposición 1", txtCosRes1);
            Par(t, 1, 0, label42, "C. reposición 2", txtCosRes2);
            Par(t, 2, 0, label43, "C. reposición 3", txtCosRes3);
            Par(t, 3, 0, label44, "C. reposición 4", txtCosRes4);
            Par(t, 4, 0, label47, "% Desc.", Varios(txtDes1, txtDes2, txtDes3));
            return Tarjeta("Costos de reposición", t);
        }

        private Control TarjetaMovimientos()
        {
            TableLayoutPanel t = Rejilla(3, 5);
            t.Controls.Add(Subtitulo("Última compra"), 0, 0);
            t.SetColumnSpan(t.GetControlFromPosition(0, 0), 2);
            t.Controls.Add(Subtitulo("Última venta"), 2, 0);
            t.SetColumnSpan(t.GetControlFromPosition(2, 0), 2);
            t.Controls.Add(Subtitulo("Último ajuste"), 4, 0);
            t.SetColumnSpan(t.GetControlFromPosition(4, 0), 2);

            Par(t, 1, 0, label10, "Proveedor", txtProve);
            Par(t, 2, 0, label8, "Fecha", txtUltCompra);
            Par(t, 3, 0, label11, "Documento", txtDocComp);
            Par(t, 4, 0, label9, "Cantidad", txtCantComp);

            Par(t, 1, 1, label14, "Cliente", txtClient);
            Par(t, 2, 1, label12, "Fecha", txtUltVent);
            Par(t, 3, 1, label15, "Documento", txtDocumVent);
            Par(t, 4, 1, label13, "Cantidad", txtCantVent);

            Par(t, 1, 2, label16, "Fecha", txtUltAjus);
            Par(t, 2, 2, label18, "Documento", txtDocum);
            Par(t, 3, 2, label17, "Cantidad", txtCantidad);
            return Tarjeta("Últimos movimientos", t);
        }

        private Control TarjetaProveedor()
        {
            TableLayoutPanel t = Rejilla(2, 6);
            Par(t, 0, 0, label1, "Proveedor", txtProvee);
            t.SetColumnSpan(txtProvee.Parent, 3);
            Par(t, 1, 0, label2, "País", txtPais);
            Par(t, 1, 1, label3, "Cód. arancel", txtCodAran);
            Par(t, 2, 0, label4, "Moneda", Varios(txtDesMon, txtSigMon));
            Par(t, 2, 1, label5, "Tipo de cambio", txttipCam);
            Par(t, 3, 0, label6, "ENT", EntregaPorcentaje());
            Par(t, 3, 1, label25, "CIF", txtCif);
            Par(t, 4, 0, label19, "FIN", txtFin);
            Par(t, 4, 1, label20, "GAS", txtGas);
            Par(t, 5, 0, label23, "Otro gas", txtOtGas);
            Par(t, 5, 1, EtiquetaNueva("% Desc."), null, Varios(txtD1, txtD2, txtD3));
            return Tarjeta("Proveedor principal", t);
        }

        private Control TarjetaPromedio()
        {
            TableLayoutPanel t = new TableLayoutPanel();
            t.Dock = DockStyle.Fill;
            t.BackColor = Color.White;
            t.Margin = new Padding(0);
            t.ColumnCount = 4;
            t.RowCount = 5;
            t.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            for (int i = 0; i < 3; i++)
            {
                t.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3f));
            }
            for (int i = 0; i < 4; i++)
            {
                t.RowStyles.Add(new RowStyle(SizeType.Absolute, AltoFila));
            }
            t.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            Label[] meses = { label37, label38, label39 };
            string[] textos = { "3 meses", "6 meses", "12 meses" };
            for (int i = 0; i < 3; i++)
            {
                Etiqueta(meses[i], textos[i]);
                meses[i].Anchor = AnchorStyles.Right;
                meses[i].Margin = new Padding(0, 0, 10, 0);
                meses[i].Font = new Font(Fuente, 9F, FontStyle.Bold);
                t.Controls.Add(meses[i], i + 1, 0);
            }
            // las etiquetas repetidas de cada columna ya no hacen falta: ahora hay una por fila
            label46.Visible = label48.Visible = label50.Visible = label51.Visible = false;

            t.Controls.Add(Etiqueta(EtiquetaNueva(null), "Ventas Bs"), 0, 1);
            t.Controls.Add(Etiqueta(label49, "Unidades"), 0, 2);
            t.Controls.Add(Etiqueta(label52, "% Utilidad"), 0, 3);
            TextBox[][] valores =
            {
                new TextBox[] { txt3Meses, txt6Meses, txt12Meses },
                new TextBox[] { txtUniVen3, txtUniVen6, txtUniVen12 },
                new TextBox[] { txtProU3, txtProU6, txtProU12 }
            };
            for (int fila = 0; fila < 3; fila++)
            {
                for (int col = 0; col < 3; col++)
                {
                    t.Controls.Add(Campo(valores[fila][col]), col + 1, fila + 1);
                }
            }
            return Tarjeta("Promedio de ventas", t);
        }

        private Control TarjetaAnioActual()
        {
            TableLayoutPanel t = Rejilla(2, 4);
            Par(t, 0, 0, label54, "Venta Bs", txtMontoVenAnoAC);
            Par(t, 0, 1, label53, "% UT", txtPorCostVentaAñoAC);
            Par(t, 1, 0, label55, "Unidades", txtCanVenAnoAC);
            Par(t, 1, 1, label59, "Costo ventas", txtCostVentAnoAC);
            Par(t, 2, 0, label56, "Filial", txtVenFilAnoAC);
            Par(t, 2, 1, label58, "Unid. compra", txtCantCompra);
            Par(t, 3, 0, label57, "Compra Bs", txtCostoCompra);
            return Tarjeta("Movimientos del año actual", t);
        }

        // ---------- piezas de construccion ----------

        private TableLayoutPanel Columnas(float[] porcentajes)
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
        private TableLayoutPanel Rejilla(int pares, int filas)
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

        private void Par(TableLayoutPanel t, int fila, int par, Label etiqueta, string texto, Control valor)
        {
            Par(t, fila, par, etiqueta, valor, texto);
        }

        private void Par(TableLayoutPanel t, int fila, int par, Label etiqueta, Control valor, string texto)
        {
            t.Controls.Add(Etiqueta(etiqueta, texto), par * 2, fila);
            if (valor != null)
            {
                t.Controls.Add(Campo(valor), par * 2 + 1, fila);
            }
        }

        private Label Etiqueta(Label l, string texto)
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

        private Label EtiquetaNueva(string texto)
        {
            return Etiqueta(new Label(), texto);
        }

        private Label Subtitulo(string texto)
        {
            Label l = Etiqueta(new Label(), texto);
            l.Font = new Font(Fuente, 8.5F, FontStyle.Bold);
            l.ForeColor = Azul;
            return l;
        }

        // Devuelve el control que debe ir en la celda: los cuadros de texto se envuelven en un panel con fondo para que se vean
        // como campos; la lista de precios se estiliza directamente.
        private Control Campo(Control c)
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
            texto.TextAlign = camposTexto.Contains(texto.Name) ? HorizontalAlignment.Left : HorizontalAlignment.Right;
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
        private Control Varios(params TextBox[] campos)
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

        // ENT y su porcentaje comparten celda: [valor] % [valor]
        private Control EntregaPorcentaje()
        {
            TableLayoutPanel t = new TableLayoutPanel();
            t.Dock = DockStyle.Fill;
            t.BackColor = Color.White;
            t.Margin = new Padding(0);
            t.ColumnCount = 3;
            t.RowCount = 1;
            t.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            t.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            t.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            t.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            Control entrega = Campo(txtEnt);
            entrega.Margin = new Padding(0, 2, 6, 2);
            Label signo = Etiqueta(label7, "%");
            signo.Margin = new Padding(0, 0, 6, 0);
            t.Controls.Add(entrega, 0, 0);
            t.Controls.Add(signo, 1, 0);
            t.Controls.Add(Campo(txtPXT), 2, 0);
            return t;
        }

        private Control Tarjeta(string titulo, Control cuerpo)
        {
            Label cabecera;
            return Tarjeta(titulo, cuerpo, out cabecera);
        }

        private Control Tarjeta(string titulo, Control cuerpo, out Label cabeceraCreada)
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
        private Control Indicador(Label etiqueta, string texto, TextBox valor)
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

        // Cifra destacada que repite el valor de un campo de la pestana General: se actualiza sola cuando ese campo cambia.
        private Control IndicadorEspejo(string titulo, TextBox origen)
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

        private void ConBorde(Control c)
        {
            c.Paint += PintarTarjeta;
            c.Resize += delegate { c.Invalidate(); };
        }

        private void PintarTarjeta(object sender, PaintEventArgs e)
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

        private void EstiloGrilla(DataGridView g)
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

        private void EstiloPestanas()
        {
            txtRes1.DrawMode = TabDrawMode.OwnerDrawFixed;
            txtRes1.SizeMode = TabSizeMode.Fixed;
            txtRes1.ItemSize = new Size(130, 32);
            txtRes1.Font = new Font(Fuente, 9.5F, FontStyle.Bold);
            txtRes1.DrawItem += txtRes1_DrawItem;
        }

        private void txtRes1_DrawItem(object sender, DrawItemEventArgs e)
        {
            Rectangle zona = txtRes1.GetTabRect(e.Index);
            bool activa = e.Index == txtRes1.SelectedIndex;
            using (SolidBrush fondo = new SolidBrush(Fondo))
            {
                e.Graphics.FillRectangle(fondo, zona);
            }
            TextRenderer.DrawText(e.Graphics, txtRes1.TabPages[e.Index].Text, txtRes1.Font, zona, activa ? Azul : TextoSecundario,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            if (activa)
            {
                using (SolidBrush linea = new SolidBrush(Azul))
                {
                    e.Graphics.FillRectangle(linea, zona.Left + 8, zona.Bottom - 3, zona.Width - 16, 3);
                }
            }
        }

        // La consulta del historial trae los encabezados tal como los escribio el SQL (CANT_Vendida, Utilida...): se muestran legibles.
        private void dataGridView3_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            string[] encabezados = { "Fecha", "Cant. vendida", "Cant. filial", "Monto vendido", "Costo", "Utilidad" };
            for (int i = 0; i < encabezados.Length && i < dataGridView3.Columns.Count; i++)
            {
                dataGridView3.Columns[i].HeaderText = encabezados[i];
            }
            ActualizarContador(dataGridView3, cabeceraHistorial, "Historial de ventas");
        }

        // Cuenta de registros junto al titulo de cada tarjeta con grilla.
        private void ActualizarContador(DataGridView g, Label cabecera, string titulo)
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

        private void AlinearNumericas(DataGridView g, params string[] columnas)
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

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            AlinearNumericas(dataGridView1, "Entrada", "Salida", "Saldo", "Costo");
            ActualizarContador(dataGridView1, cabeceraMovimientos, "Movimientos de inventario");
        }

        private void dataGridView4_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            AlinearNumericas(dataGridView4, "Cantidad", "Costo");
            ActualizarContador(dataGridView4, cabeceraCostoMov, "Costo registrado en ventas y ajustes");
        }

        // Entradas en verde, salidas y saldos negativos en rojo; el cero de entrada/salida se deja en blanco.
        private void FormatearMovimiento(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridView g = (DataGridView)sender;
            if (e.RowIndex < 0 || e.ColumnIndex < 0 || e.Value == null || !(e.Value is decimal))
            {
                return;
            }
            string columna = g.Columns[e.ColumnIndex].Name;
            decimal valor = (decimal)e.Value;
            if ((columna == "Entrada" || columna == "Salida") && valor == 0m)
            {
                e.Value = "";
                e.FormattingApplied = true;
            }
            else if (columna == "Entrada")
            {
                e.CellStyle.ForeColor = Color.DarkGreen;
            }
            else if (columna == "Salida" || (columna == "Saldo" && valor < 0m))
            {
                e.CellStyle.ForeColor = Negativo;
            }
        }

        // Aviso bajo las cifras de existencia cuando el kardex no explica toda la existencia o esta recortado.
        private void MostrarNotaExistencias(decimal saldoPrevio, int movimientos)
        {
            string texto = null;
            if (movimientos >= 500)
            {
                texto = "Se muestran los 500 movimientos más recientes.";
            }
            else if (Math.Abs(saldoPrevio) >= 0.005m)
            {
                texto = "La existencia incluye " + saldoPrevio.ToString("n2") + " que no proviene de ningún movimiento registrado " +
                        "(por ejemplo, un saldo inicial cargado sin documento).";
            }
            lblNotaExistencias.Text = texto ?? "";
            lblNotaExistencias.Visible = texto != null;
        }

        // Los encabezados de la grilla de costos tambien vienen del SQL (Tipo_Documento): se muestran legibles.
        private void dataGridView2_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewColumn columna in dataGridView2.Columns)
            {
                columna.HeaderText = columna.HeaderText.Replace("_", " ").Replace("Codigo", "Código");
            }
            ActualizarContador(dataGridView2, cabeceraCompras, "Compras del producto");
        }

        // La utilidad negativa se resalta en rojo.
        private void dataGridView3_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex != 5 || e.RowIndex < 0 || e.Value == null)
            {
                return;
            }
            string valor = Convert.ToString(e.Value).Trim();
            if (valor.StartsWith("-"))
            {
                e.CellStyle.ForeColor = Negativo;
                e.CellStyle.Font = new Font(Fuente, 9F, FontStyle.Bold);
            }
        }
    }
}
