using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using lebrun.clases.gerencial;
using static lebrun.formularios.gerencial.EstiloGerencial;

namespace lebrun.formularios.gerencial
{
    // Ventana para ver una lista de registros (por ejemplo, los documentos de un dia) sin salir de la pantalla de origen.
    internal class lbxTablaEmergente : Form
    {
        private readonly DataGridView grilla = new DataGridView();
        private readonly string titulo;
        private readonly DataTable tabla;
        private readonly Action<DataGridView> formato;
        private Label cabecera;

        public static void Mostrar(IWin32Window padre, string titulo, DataTable tabla, Action<DataGridView> formato)
        {
            using (lbxTablaEmergente ventana = new lbxTablaEmergente(titulo, tabla, formato))
            {
                ventana.ShowDialog(padre);
            }
        }

        private lbxTablaEmergente(string titulo, DataTable tabla, Action<DataGridView> formato)
        {
            this.titulo = titulo;
            this.tabla = tabla;
            this.formato = formato;
            AutoScaleMode = AutoScaleMode.None;
            Font = new Font(Fuente, 9F);
            BackColor = Fondo;
            Text = titulo;
            StartPosition = FormStartPosition.CenterParent;
            ShowInTaskbar = false;
            MinimizeBox = false;
            MaximizeBox = true;
            Rectangle area = Screen.PrimaryScreen.WorkingArea;
            ClientSize = new Size(Math.Min(1000, area.Width - 80), Math.Min(560, area.Height - 120));
            MinimumSize = new Size(640, 360);

            grilla.AllowUserToAddRows = false;
            grilla.AllowUserToDeleteRows = false;
            grilla.ReadOnly = true;
            grilla.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grilla.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grilla.Margin = new Padding(0);
            EstiloGrilla(grilla);
            grilla.CellFormatting += delegate (object remitente, DataGridViewCellFormattingEventArgs e)
            {
                if (e.RowIndex >= 0 && e.Value is decimal && (decimal)e.Value < 0m)
                {
                    e.CellStyle.ForeColor = Negativo;
                }
            };

            Control tarjeta = Tarjeta(titulo, grilla, out cabecera);
            tarjeta.Margin = new Padding(0, 0, 0, 8);

            Button cerrar = new Button();
            cerrar.Text = "Cerrar";
            cerrar.Size = new Size(110, 30);
            cerrar.FlatStyle = FlatStyle.Flat;
            cerrar.BackColor = Azul;
            cerrar.ForeColor = Color.White;
            cerrar.Font = new Font(Fuente, 9F, FontStyle.Bold);
            cerrar.FlatAppearance.BorderSize = 0;
            cerrar.Cursor = Cursors.Hand;
            cerrar.Anchor = AnchorStyles.Right;
            cerrar.DialogResult = DialogResult.Cancel;
            CancelButton = cerrar;

            TableLayoutPanel raiz = new TableLayoutPanel();
            raiz.Dock = DockStyle.Fill;
            raiz.ColumnCount = 1;
            raiz.RowCount = 2;
            raiz.Padding = new Padding(12, 10, 12, 8);
            raiz.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            raiz.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));
            raiz.Controls.Add(tarjeta, 0, 0);
            raiz.Controls.Add(cerrar, 0, 1);
            Controls.Add(raiz);
        }

        // Los datos se cargan al abrir la ventana: con la grilla ya creada se generan sus columnas y se puede darles formato.
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            bool recortado = tabla.Rows.Count > MovimientosGerenciales.LimiteFilas;
            while (tabla.Rows.Count > MovimientosGerenciales.LimiteFilas)
            {
                tabla.Rows.RemoveAt(tabla.Rows.Count - 1);
            }
            grilla.DataSource = tabla;
            if (formato != null)
            {
                formato(grilla);
            }
            ActualizarContador(grilla, cabecera, titulo);
            if (recortado)
            {
                cabecera.Text = titulo + "  ·  primeros " + MovimientosGerenciales.LimiteFilas.ToString("n0") + " registros";
            }
        }
    }
}
