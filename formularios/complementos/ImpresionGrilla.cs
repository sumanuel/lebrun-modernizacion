using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.Windows.Forms;
using lebrun.clasesData;

namespace lebrun.formularios.complementos
{
    // Imprime, con vista previa, lo que se ve en una grilla: las columnas visibles, en su orden y con el ancho en proporcion a su FillWeight.
    // Cada pagina repite el titulo y los nombres de las columnas. Si la grilla tiene filas que son titulo de un grupo (por ejemplo, el proveedor)
    // se imprimen a todo lo ancho y el titulo se repite cuando el grupo sigue en otra pagina. Los totales van despues de la ultima fila.
    internal sealed class ImpresionGrilla
    {
        public DataGridView Grilla;
        public string Compania = "";
        public string Titulo = "";
        public readonly List<string> Destacadas = new List<string>();   // en negrita bajo el titulo: el proveedor o el cliente
        public readonly List<string> Detalle = new List<string>();      // datos y filtros usados
        public readonly List<string> Totales = new List<string>();
        public bool Horizontal;
        public Func<DataGridViewRow, string> TituloDeGrupo;            // texto si la fila es titulo de un grupo; null si es una fila normal

        private int fila;
        private int pagina;
        private string grupoActual;

        public void VistaPrevia(IWin32Window duenio)
        {
            try
            {
                using (PrintDocument documento = Documento())
                using (PrintPreviewDialog vista = new PrintPreviewDialog())
                {
                    vista.Document = documento;
                    vista.Width = 1000;
                    vista.Height = 720;
                    vista.StartPosition = FormStartPosition.CenterParent;
                    vista.ShowDialog(duenio);
                }
            }
            catch (Exception ex)
            {
                RegistroErrores.Registrar(ex, Titulo + ": imprimir");
                MessageBox.Show(duenio, "No se pudo preparar la impresión.\n\n" + ex.Message, Titulo, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public PrintDocument Documento()
        {
            PrintDocument documento = new PrintDocument();
            documento.DocumentName = Titulo;
            documento.DefaultPageSettings.Landscape = Horizontal;
            documento.DefaultPageSettings.Margins = new Margins(40, 40, 40, 40);
            // la vista previa y la impresion recorren las paginas cada una desde el principio
            documento.BeginPrint += delegate
            {
                fila = 0;
                pagina = 0;
                grupoActual = null;
            };
            documento.PrintPage += ImprimirPagina;
            return documento;
        }

        private List<DataGridViewColumn> ColumnasVisibles()
        {
            List<DataGridViewColumn> lista = new List<DataGridViewColumn>();
            DataGridViewColumn c = Grilla.Columns.GetFirstColumn(DataGridViewElementStates.Visible);
            while (c != null)
            {
                lista.Add(c);
                c = Grilla.Columns.GetNextColumn(c, DataGridViewElementStates.Visible, DataGridViewElementStates.None);
            }
            return lista;
        }

        private string Grupo(DataGridViewRow r)
        {
            return TituloDeGrupo == null ? null : TituloDeGrupo(r);
        }

        private void ImprimirPagina(object sender, PrintPageEventArgs e)
        {
            pagina++;
            List<DataGridViewColumn> columnas = ColumnasVisibles();
            Rectangle area = e.MarginBounds;
            float suma = 0f;
            foreach (DataGridViewColumn c in columnas)
            {
                suma += c.FillWeight;
            }
            Graphics g = e.Graphics;
            bool faltanTotales = false;
            using (Font fCompania = new Font("Arial", 10F, FontStyle.Bold))
            using (Font fTitulo = new Font("Arial", 13F, FontStyle.Bold))
            using (Font fDestacada = new Font("Arial", 10F, FontStyle.Bold))
            using (Font fSub = new Font("Arial", 8.5F))
            using (Font fCab = new Font("Arial", 8.5F, FontStyle.Bold))
            using (Font fTxt = new Font("Arial", 8F))
            using (Pen lapiz = new Pen(Color.Gray))
            using (StringFormat izquierda = new StringFormat { Trimming = StringTrimming.EllipsisCharacter, FormatFlags = StringFormatFlags.NoWrap })
            using (StringFormat derecha = new StringFormat { Alignment = StringAlignment.Far, Trimming = StringTrimming.EllipsisCharacter, FormatFlags = StringFormatFlags.NoWrap })
            {
                float y = area.Top;
                string fecha = DateTime.Now.ToString("dd/MM/yyyy hh:mm tt", CultureInfo.CurrentCulture) + "   Página " + pagina;
                g.DrawString(Compania, fCompania, Brushes.Black, area.Left, y);
                g.DrawString(fecha, fSub, Brushes.Black, new RectangleF(area.Left, y, area.Width, fCompania.GetHeight(g)), derecha);
                y += fCompania.GetHeight(g) + 2;
                g.DrawString(Titulo, fTitulo, Brushes.Black, area.Left, y);
                y += fTitulo.GetHeight(g) + 2;
                // el proveedor o el cliente se escriben completos: si no caben en un renglon siguen en el de abajo
                foreach (string linea in Destacadas)
                {
                    float altoLinea = g.MeasureString(linea, fDestacada, area.Width).Height;
                    g.DrawString(linea, fDestacada, Brushes.Black, new RectangleF(area.Left, y, area.Width, altoLinea));
                    y += altoLinea + 1;
                }
                foreach (string linea in Detalle)
                {
                    float altoLinea = g.MeasureString(linea, fSub, area.Width).Height;
                    g.DrawString(linea, fSub, Brushes.Black, new RectangleF(area.Left, y, area.Width, altoLinea));
                    y += altoLinea;
                }
                y += 6;

                float alto = fTxt.GetHeight(g) + 4;
                float x = area.Left;
                foreach (DataGridViewColumn c in columnas)
                {
                    float ancho = area.Width * c.FillWeight / suma;
                    bool der = c.DefaultCellStyle.Alignment == DataGridViewContentAlignment.MiddleRight;
                    g.DrawString(c.HeaderText, fCab, Brushes.Black, new RectangleF(x + 2, y, ancho - 4, alto), der ? derecha : izquierda);
                    x += ancho;
                }
                y += alto;
                g.DrawLine(lapiz, area.Left, y, area.Right, y);
                y += 2;

                // el grupo que viene de la pagina anterior repite su titulo
                if (grupoActual != null && fila < Grilla.Rows.Count && Grupo(Grilla.Rows[fila]) == null)
                {
                    g.DrawString(grupoActual + "  (continuación)", fCab, Brushes.Black, new RectangleF(area.Left, y, area.Width, alto), izquierda);
                    y += alto;
                }

                while (fila < Grilla.Rows.Count)
                {
                    DataGridViewRow r = Grilla.Rows[fila];
                    string grupo = Grupo(r);
                    // un titulo de grupo no se deja solo al final de la pagina
                    float necesario = grupo != null ? alto * 2 + 4 : alto;
                    if (y + necesario > area.Bottom)
                    {
                        break;
                    }
                    if (grupo != null)
                    {
                        grupoActual = grupo;
                        y += 4;
                        g.DrawString(grupo, fCab, Brushes.Black, new RectangleF(area.Left, y, area.Width, alto), izquierda);
                    }
                    else
                    {
                        x = area.Left;
                        foreach (DataGridViewColumn c in columnas)
                        {
                            float ancho = area.Width * c.FillWeight / suma;
                            bool der = c.DefaultCellStyle.Alignment == DataGridViewContentAlignment.MiddleRight;
                            g.DrawString(Convert.ToString(r.Cells[c.Index].FormattedValue), fTxt, Brushes.Black, new RectangleF(x + 2, y, ancho - 4, alto), der ? derecha : izquierda);
                            x += ancho;
                        }
                    }
                    y += alto;
                    fila++;
                }

                if (fila >= Grilla.Rows.Count && Totales.Count > 0)
                {
                    float altoTotal = fCab.GetHeight(g) + 4;
                    if (y + 6 + altoTotal * Totales.Count > area.Bottom)
                    {
                        faltanTotales = true;   // van en una pagina mas
                    }
                    else
                    {
                        y += 3;
                        g.DrawLine(lapiz, area.Left, y, area.Right, y);
                        y += 3;
                        foreach (string linea in Totales)
                        {
                            g.DrawString(linea, fCab, Brushes.Black, new RectangleF(area.Left, y, area.Width, altoTotal), derecha);
                            y += altoTotal;
                        }
                    }
                }
            }
            e.HasMorePages = fila < Grilla.Rows.Count || faltanTotales;
        }
    }
}
