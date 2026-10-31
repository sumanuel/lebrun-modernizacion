using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using lebrun.clases.gerencial;
using lebrun.clasesData;

namespace lebrun.formularios.gerencial
{
    // Base de Movimiento Facturacion y Movimiento Producto: el movimiento de un anio mes por mes y, al elegir un mes, dia por dia.
    // Con doble clic en un dia se abren sus documentos. Separa lo vendido por vendedores de clientes (codigo menor a 80) y de la filial
    // (80 o mayor) y se puede ver comparando los tres grupos o el detalle completo de uno solo.
    public abstract class lbxMovimientoPeriodo : lbxListadoGerencial
    {
        protected sealed class Medida
        {
            public readonly string Clave;
            public readonly string Encabezado;
            public readonly string Formato;
            public readonly bool Comparativa;
            public readonly string Corto;

            public Medida(string clave, string encabezado, string formato, bool comparativa, string corto = null)
            {
                Clave = clave;
                Encabezado = encabezado;
                Formato = formato;
                Comparativa = comparativa;
                Corto = corto ?? encabezado.ToLower();
            }
        }

        private static readonly string[] CodigosGrupo = { "CLI", "FIL", "GEN" };
        private static readonly string[] NombresGrupo = { "Clientes", "Filial", "General" };
        private static readonly string[] Vistas = { "Comparar clientes y filial", "Solo clientes", "Solo filial", "General (todos)" };

        protected readonly MovimientosGerenciales movimientos = new MovimientosGerenciales();
        private readonly NumericUpDown nudAnio = new NumericUpDown();
        private readonly ComboBox cboVista = new ComboBox();
        private int anioConsultado;
        private int mesDelDetalle;

        protected abstract Medida[] Medidas { get; }
        protected abstract DataTable PorMes(int anio);
        protected abstract DataTable PorDia(int anio, int mes);
        protected abstract DataTable DelDia(DateTime dia);
        protected abstract void FormatoDelDia(DataGridView g);
        protected abstract string NombreDocumentos { get; }

        protected override string TituloGrilla { get { return "Movimiento por mes"; } }
        protected override bool TieneDetalle { get { return true; } }
        protected override string TituloDetalle { get { return "Movimiento por día del mes elegido (doble clic en un día para ver sus " + NombreDocumentos + ")"; } }

        protected override void CrearFiltros()
        {
            nudAnio.Minimum = 2000;
            nudAnio.Maximum = 2100;
            nudAnio.Value = DateTime.Today.Year;
            nudAnio.Width = 70;
            Filtro("Año", nudAnio);

            cboVista.DropDownStyle = ComboBoxStyle.DropDownList;
            cboVista.Items.AddRange(Vistas);
            cboVista.SelectedIndex = 0;
            cboVista.Width = 190;
            cboVista.SelectedIndexChanged += delegate
            {
                AplicarVista(grilla, "Mes");
                AplicarVista(grillaDetalle, "Dia");
            };
            Filtro("Ver", cboVista);
        }

        protected override DataTable Consultar()
        {
            anioConsultado = (int)nudAnio.Value;
            return PorMes(anioConsultado);
        }

        protected override void DarFormato()
        {
            AplicarVista(grilla, "Mes");
            grilla.CellFormatting -= NombreDelMes;
            grilla.CellFormatting += NombreDelMes;
        }

        protected override DataTable ConsultarDetalle(DataRow fila)
        {
            mesDelDetalle = Convert.ToInt32(fila["Mes"]);
            return PorDia(anioConsultado, mesDelDetalle);
        }

        protected override void DarFormatoDetalle()
        {
            AplicarVista(grillaDetalle, "Dia");
        }

        protected override void AlAbrirDetalle(DataRow fila)
        {
            DateTime dia = new DateTime(anioConsultado, mesDelDetalle, Convert.ToInt32(fila["Dia"]));
            try
            {
                lbxTablaEmergente.Mostrar(this, NombreDocumentos.Substring(0, 1).ToUpper() + NombreDocumentos.Substring(1) + " del " + dia.ToString("dd/MM/yyyy"), DelDia(dia), FormatoDelDia);
            }
            catch (Exception ex)
            {
                RegistroErrores.Registrar(ex, "Documentos del dia en " + TituloPantalla);
                MessageBox.Show("No se pudo hacer la consulta.\n\n" + ex.Message, TituloPantalla, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Muestra solo las columnas de la vista elegida: la comparacion de grupos (medidas principales de clientes, filial y general)
        // o todas las medidas de un grupo.
        private void AplicarVista(DataGridView g, string columnaClave)
        {
            if (g.DataSource == null || !g.Columns.Contains(columnaClave))
            {
                return;
            }
            int vista = cboVista.SelectedIndex;
            foreach (DataGridViewColumn c in g.Columns)
            {
                c.Visible = false;
            }
            int orden = 0;
            DataGridViewColumn clave = g.Columns[columnaClave];
            clave.Visible = true;
            clave.DisplayIndex = orden++;
            clave.HeaderText = columnaClave == "Mes" ? "Mes" : "Día";
            clave.FillWeight = columnaClave == "Mes" ? 12 : 8;
            clave.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            clave.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;

            string[] grupos = vista == 0 ? new[] { "CLI", "FIL", "GEN" } : new[] { CodigosGrupo[vista == 1 ? 0 : vista == 2 ? 1 : 2] };
            foreach (string grupo in grupos)
            {
                foreach (Medida m in Medidas)
                {
                    if (vista == 0 && !m.Comparativa)
                    {
                        continue;
                    }
                    string nombre = m.Clave + "_" + grupo;
                    if (!g.Columns.Contains(nombre))
                    {
                        continue;
                    }
                    DataGridViewColumn c = g.Columns[nombre];
                    c.Visible = true;
                    c.DisplayIndex = orden++;
                    c.HeaderText = vista == 0 ? NombresGrupo[Array.IndexOf(CodigosGrupo, grupo)] + " · " + m.Encabezado : m.Encabezado;
                    c.DefaultCellStyle.Format = m.Formato;
                    c.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    c.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    c.FillWeight = m.Formato == "n0" ? 9 : 13;
                }
            }
        }

        private void NombreDelMes(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0 || e.Value == null || e.Value == DBNull.Value || grilla.Columns[e.ColumnIndex].Name != "Mes")
            {
                return;
            }
            int mes = Convert.ToInt32(e.Value);
            if (mes >= 1 && mes <= 12)
            {
                string nombre = CultureInfo.GetCultureInfo("es-ES").DateTimeFormat.GetMonthName(mes);
                e.Value = char.ToUpper(nombre[0]) + nombre.Substring(1);
                e.FormattingApplied = true;
            }
        }

        // Totales del anio por grupo con las medidas principales.
        protected override string ResumenDe(DataTable tabla)
        {
            List<string> partes = new List<string>();
            for (int i = 0; i < CodigosGrupo.Length; i++)
            {
                List<string> medidas = new List<string>();
                foreach (Medida m in Medidas)
                {
                    if (!m.Comparativa || !tabla.Columns.Contains(m.Clave + "_" + CodigosGrupo[i]))
                    {
                        continue;
                    }
                    decimal total = 0m;
                    foreach (DataRow r in tabla.Rows)
                    {
                        total += r[m.Clave + "_" + CodigosGrupo[i]] == DBNull.Value ? 0m : Convert.ToDecimal(r[m.Clave + "_" + CodigosGrupo[i]]);
                    }
                    medidas.Add(total.ToString(m.Formato) + " " + m.Corto);
                }
                partes.Add(NombresGrupo[i] + ": " + string.Join(" · ", medidas.ToArray()));
            }
            return "Año " + anioConsultado + "   —   " + string.Join("   |   ", partes.ToArray());
        }
    }

    // Movimiento Facturacion: facturas y devoluciones de cada mes del anio. Sustituye a FRMMOVFACTU del sistema anterior.
    public class lbxMovimientoFacturacion : lbxMovimientoPeriodo
    {
        private static readonly Medida[] medidas =
        {
            new Medida("CANT_FAV", "Facturas", "n0", true, "facturas"),
            new Medida("CANT_DEV", "Devoluciones", "n0", false),
            new Medida("MONTO_FAV", "Monto facturado", "n2", false),
            new Medida("MONTO_DEV", "Monto devuelto", "n2", false),
            new Medida("MONTO_NETO", "Venta neta", "n2", true, "venta neta")
        };

        protected override string TituloPantalla { get { return "Movimiento de facturación"; } }
        protected override string DescripcionPantalla { get { return "Facturas y devoluciones del año mes por mes (base neta), separando clientes y filial. Seleccione un mes para ver sus días."; } }
        protected override Medida[] Medidas { get { return medidas; } }
        protected override string NombreDocumentos { get { return "documentos"; } }

        protected override DataTable PorMes(int anio) { return movimientos.FacturacionPorMes(anio); }
        protected override DataTable PorDia(int anio, int mes) { return movimientos.FacturacionPorDia(anio, mes); }
        protected override DataTable DelDia(DateTime dia) { return movimientos.FacturasDelDia(dia); }

        protected override void FormatoDelDia(DataGridView g)
        {
            ColumnaTexto(g, "Tipo", null, 6);
            ColumnaTexto(g, "Documento", null, 14);
            ColumnaTexto(g, "Cliente", "Código cliente", 11);
            ColumnaTexto(g, "Nombre", "Cliente", 30);
            ColumnaTexto(g, "Caja", null, 8);
            ColumnaTexto(g, "CodVendedor", "Cód. vend.", 9);
            ColumnaTexto(g, "Vendedor", null, 22);
            ColumnaNumerica(g, "Monto", "n2", 14);
        }
    }

    // Movimiento Producto: lineas de producto vendidas y devueltas de cada mes del anio. Sustituye a FRMMOVPROD del sistema anterior.
    public class lbxMovimientoProducto : lbxMovimientoPeriodo
    {
        private static readonly Medida[] medidas =
        {
            new Medida("LINEAS_FAV", "Líneas facturadas", "n0", true, "líneas"),
            new Medida("LINEAS_DEV", "Líneas devueltas", "n0", false),
            new Medida("UND_FAV", "Unidades facturadas", "n2", false),
            new Medida("UND_DEV", "Unidades devueltas", "n2", false),
            new Medida("UND_NETO", "Unidades netas", "n2", true, "und."),
            new Medida("MONTO_FAV", "Monto facturado", "n2", false),
            new Medida("MONTO_DEV", "Monto devuelto", "n2", false),
            new Medida("MONTO_NETO", "Monto neto", "n2", true, "neto")
        };

        protected override string TituloPantalla { get { return "Movimiento de productos"; } }
        protected override string DescripcionPantalla { get { return "Líneas, unidades y montos de productos vendidos y devueltos del año mes por mes, separando clientes y filial. Seleccione un mes para ver sus días."; } }
        protected override Medida[] Medidas { get { return medidas; } }
        protected override string NombreDocumentos { get { return "líneas"; } }

        protected override DataTable PorMes(int anio) { return movimientos.ProductosPorMes(anio); }
        protected override DataTable PorDia(int anio, int mes) { return movimientos.ProductosPorDia(anio, mes); }
        protected override DataTable DelDia(DateTime dia) { return movimientos.LineasDelDia(dia); }

        protected override void FormatoDelDia(DataGridView g)
        {
            ColumnaTexto(g, "Tipo", null, 5);
            ColumnaTexto(g, "Documento", null, 11);
            ColumnaTexto(g, "Codigo", "Código", 11);
            ColumnaTexto(g, "Descripcion", "Descripción", 28);
            ColumnaTexto(g, "CodVendedor", "Cód. vend.", 8);
            ColumnaTexto(g, "Vendedor", null, 16);
            ColumnaTexto(g, "Unidad", null, 6);
            ColumnaNumerica(g, "Cantidad", "n2", 8);
            ColumnaNumerica(g, "Costo", "n2", 9);
            ColumnaNumerica(g, "Precio", "n2", 9);
            ColumnaNumerica(g, "Total", "n2", 10);
        }
    }
}
