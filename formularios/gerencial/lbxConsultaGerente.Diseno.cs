using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using static lebrun.formularios.gerencial.EstiloGerencial;

namespace lebrun.formularios.gerencial
{
    // Diseno visual de la pantalla (mismo estilo que Consulta General Art.). Se aplica al crear el formulario: reubica y da estilo
    // a los controles que declara lbxConsultaGerente.Designer.cs, sin cambiar sus nombres ni la logica que los llena.
    // El Designer.cs trae controles repetidos escondidos detras de otros (txtCategoria, txtExitecia, txtPreci, ...): el codigo solo
    // llena los que se usan aqui; los demas se descartan junto con los contenedores originales.
    public partial class lbxConsultaGerente
    {
        private readonly HashSet<string> camposTexto = new HashSet<string>
        {
            "txtCateg", "txtSubCat", "txtUniM", "txtOferta", "txtEstado", "txtProve", "txtUltCompra", "txtDocComp",
            "txtClient", "txtUltVent", "txtDocumVent", "txtUltAjus", "txtDocum"
        };

        private void AplicarDiseno()
        {
            CamposTexto = camposTexto;
            SuspendLayout();
            AutoScaleMode = AutoScaleMode.None;
            Font = new Font(Fuente, 9F);
            BackColor = Fondo;
            Text = "Consulta Gerentes";
            Rectangle area = Screen.PrimaryScreen.WorkingArea;
            ClientSize = new Size(Math.Min(1000, area.Width - 60), Math.Min(600, area.Height - 150));
            MinimumSize = new Size(820, 520);
            DoubleBuffered = true;

            Control encabezado = EncabezadoProducto(label21, txtCodigo, button3, label22);
            ConstruirGeneral();
            EstiloPestanas(tabControl1);
            ArmarPantalla(this, encabezado, tabControl1);

            // los contenedores originales ya quedaron vacios: sus controles se movieron a las tarjetas nuevas
            Control[] viejos = { groupBox1, groupBox2, groupBox3, groupBox4, groupBox5, groupBox6, groupBox7 };
            foreach (Control viejo in viejos)
            {
                viejo.Dispose();
            }
            ResumeLayout(true);
        }

        private void ConstruirGeneral()
        {
            TableLayoutPanel g = new TableLayoutPanel();
            g.Dock = DockStyle.Fill;
            g.BackColor = Fondo;
            g.ColumnCount = 1;
            g.RowCount = 4;
            g.RowStyles.Add(new RowStyle(SizeType.Absolute, 80));
            g.RowStyles.Add(new RowStyle(SizeType.Absolute, 170));
            g.RowStyles.Add(new RowStyle(SizeType.Absolute, 170));
            g.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            TableLayoutPanel cifras = Columnas(new float[] { 25, 25, 25, 25 });
            cifras.Controls.Add(Indicador(label31, "Existencia", txtExist), 0, 0);
            cifras.Controls.Add(Indicador(label34, "Disponible", txtDispon), 1, 0);
            cifras.Controls.Add(Indicador(label28, "Precio", txtPrecio), 2, 0);
            cifras.Controls.Add(Indicador(label35, "Último costo", txtUCost), 3, 0);
            g.Controls.Add(cifras, 0, 0);

            TableLayoutPanel fila1 = Columnas(new float[] { 62, 38 });
            fila1.Controls.Add(TarjetaInformacion(), 0, 0);
            fila1.Controls.Add(TarjetaAjuste(), 1, 0);
            g.Controls.Add(fila1, 0, 1);

            TableLayoutPanel fila2 = Columnas(new float[] { 50, 50 });
            fila2.Controls.Add(TarjetaCompra(), 0, 0);
            fila2.Controls.Add(TarjetaVenta(), 1, 0);
            g.Controls.Add(fila2, 0, 2);

            tabPage1.Controls.Clear();
            tabPage1.Padding = new Padding(10, 10, 0, 0);
            tabPage1.BackColor = Fondo;
            tabPage1.UseVisualStyleBackColor = false;
            tabPage1.AutoScroll = true;
            g.MinimumSize = new Size(800, 460);
            tabPage1.Controls.Add(g);
        }

        private Control TarjetaInformacion()
        {
            TableLayoutPanel t = Rejilla(2, 4);
            Par(t, 0, 0, label30, "Categoría", txtCateg);
            Par(t, 0, 1, label27, "Sub-categoría", txtSubCat);
            Par(t, 1, 0, label29, "Unidad de medida", txtUniM);
            Par(t, 1, 1, label24, "Oferta", txtOferta);
            Par(t, 2, 0, label26, "Estado", txtEstado);
            Par(t, 2, 1, label32, "Prefac.", txtPrefa);
            Par(t, 3, 0, label33, "Pedidas", txtPedid);
            return Tarjeta("Información del producto", t);
        }

        private Control TarjetaAjuste()
        {
            TableLayoutPanel t = Rejilla(1, 3);
            Par(t, 0, 0, label16, "Fecha", txtUltAjus);
            Par(t, 1, 0, label17, "Cantidad", txtCantidad);
            Par(t, 2, 0, label18, "Documento", txtDocum);
            return Tarjeta("Último ajuste", t);
        }

        private Control TarjetaCompra()
        {
            TableLayoutPanel t = Rejilla(1, 4);
            Par(t, 0, 0, label10, "Proveedor", txtProve);
            Par(t, 1, 0, label8, "Fecha", txtUltCompra);
            Par(t, 2, 0, label9, "Cantidad", txtCantComp);
            Par(t, 3, 0, label11, "Documento", txtDocComp);
            return Tarjeta("Última compra", t);
        }

        private Control TarjetaVenta()
        {
            TableLayoutPanel t = Rejilla(1, 4);
            Par(t, 0, 0, label14, "Cliente", txtClient);
            Par(t, 1, 0, label12, "Fecha", txtUltVent);
            Par(t, 2, 0, label13, "Cantidad", txtCantVent);
            Par(t, 3, 0, label15, "Documento", txtDocumVent);
            return Tarjeta("Última venta", t);
        }
    }
}
