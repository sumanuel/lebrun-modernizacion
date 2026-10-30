using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using static lebrun.formularios.gerencial.EstiloGerencial;

namespace lebrun.formularios.gerencial
{
    // Diseno visual de la pantalla. Se aplica al crear el formulario (AplicarDiseno): reubica y da estilo a los controles que
    // declara lbxConsultaGerencial.Designer.cs, sin cambiar sus nombres ni la logica que los llena (lbxConsultaGerencial.cs).
    // Las piezas comunes (colores, tarjetas, campos, grillas) estan en EstiloGerencial.cs.
    public partial class lbxConsultaGerencial
    {
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
            CamposTexto = camposTexto;
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
            EstiloPestanas(txtRes1);

            ArmarPantalla(this, encabezado, txtRes1);

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
            return EncabezadoProducto(label21, txtCodigo, button3, label22);
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
            PaginaConContenido(tabPage2, t, new Size(900, 420));
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
            PaginaConContenido(tabPage3, t, new Size(900, 420));
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
