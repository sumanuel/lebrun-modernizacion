using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using static lebrun.formularios.gerencial.EstiloGerencial;

namespace lebrun.formularios.complementos
{
    // Lista para elegir cuando lo escrito en un campo de codigo (cliente, proveedor) coincide con varios registros: codigo, nombre y RIF.
    internal sealed class SelectorRegistro : Form
    {
        public string Codigo;
        private readonly DataGridView lista = new DataGridView();
        private readonly string columnaCodigo;

        // registro y registros: "cliente" / "clientes", "proveedor" / "proveedores". maximo: cuantos se muestran si recortado es verdadero.
        public SelectorRegistro(DataTable datos, string registro, string registros, string columnaCodigo, string columnaNombre, string columnaRif, bool recortado, int maximo)
        {
            this.columnaCodigo = columnaCodigo;
            AutoScaleMode = AutoScaleMode.None;
            Font = new Font(Fuente, 9F);
            BackColor = Fondo;
            Text = "Elegir " + registro;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition = FormStartPosition.CenterParent;
            MinimizeBox = false;
            MaximizeBox = false;
            ShowInTaskbar = false;
            ClientSize = new Size(640, 420);

            Label aviso = new Label();
            aviso.Text = recortado ? "Hay más de " + maximo + " " + registros + " con ese texto: se muestran los primeros. Escriba más del nombre para acotar."
                                   : "Hay varios " + registros + " con ese texto. Elija uno (doble clic o Enter).";
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
                ConfigurarColumna(columnaCodigo, "Código", 22);
                ConfigurarColumna(columnaNombre, "Nombre", 56);
                ConfigurarColumna(columnaRif, "RIF", 22);
            };
            lista.DataSource = datos;
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
            Codigo = Convert.ToString(lista.CurrentRow.Cells[columnaCodigo].Value);
            DialogResult = DialogResult.OK;
        }
    }
}
