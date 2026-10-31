using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using lebrun.clases.gerencial;
using static lebrun.formularios.gerencial.EstiloGerencial;

namespace lebrun.formularios.gerencial
{
    // Base de Ventas por Vendedor y Compras por Proveedor: el monto neto de cada vendedor o proveedor en el dia elegido, la semana, el mes,
    // el anio y los 3, 6, 9 y 12 meses completos anteriores, separado en dos grupos (clientes y filial, o nacionales e importados) con
    // el total de cada grupo y el total general al final de la lista.
    public abstract class lbxVentasCompras : lbxListadoGerencial
    {
        private static readonly string[] Periodos = { "Dia", "Semana", "Mes", "Anio", "M3", "M6", "M9", "M12" };

        protected readonly MovimientosGerenciales movimientos = new MovimientosGerenciales();
        private readonly DateTimePicker dtpDia = NuevaFecha(DateTime.Today);
        private DateTime diaConsultado;

        protected abstract string EncabezadoNombre { get; }
        protected abstract string[] NombresGrupo { get; }
        protected abstract DataTable ConsultarPeriodos(DateTime dia);
        protected virtual bool ConCosto { get { return false; } }

        protected override int FilasDeTotales { get { return 3; } }

        protected override void CrearFiltros()
        {
            Filtro("Al día", dtpDia);
        }

        protected override DataTable Consultar()
        {
            diaConsultado = dtpDia.Value.Date;
            DataTable tabla = ConsultarPeriodos(diaConsultado);

            // se quitan los que no tuvieron movimiento en ningun periodo
            for (int i = tabla.Rows.Count - 1; i >= 0; i--)
            {
                bool movimiento = false;
                foreach (string p in Periodos)
                {
                    if (tabla.Rows[i][p] != DBNull.Value && Convert.ToDecimal(tabla.Rows[i][p]) != 0m)
                    {
                        movimiento = true;
                        break;
                    }
                }
                if (!movimiento)
                {
                    tabla.Rows.RemoveAt(i);
                }
            }

            if (ConCosto)
            {
                tabla.Columns.Add("PctCostoMes", typeof(decimal));
                tabla.Columns.Add("PctCostoAnio", typeof(decimal));
                foreach (DataRow r in tabla.Rows)
                {
                    PonerPorcentajes(r);
                }
            }
            tabla.Columns.Add("EsTotal", typeof(int));
            foreach (DataRow r in tabla.Rows)
            {
                r["EsTotal"] = 0;
            }
            AgregarTotal(tabla, "Total " + NombresGrupo[0].ToLower(), 0);
            AgregarTotal(tabla, "Total " + NombresGrupo[1].ToLower(), 1);
            AgregarTotal(tabla, "TOTAL GENERAL", -1);
            return tabla;
        }

        private static void PonerPorcentajes(DataRow r)
        {
            r["PctCostoMes"] = Porcentaje(r["CostoMes"], r["Mes"]);
            r["PctCostoAnio"] = Porcentaje(r["CostoAnio"], r["Anio"]);
        }

        private static decimal Porcentaje(object costo, object venta)
        {
            decimal v = venta == DBNull.Value ? 0m : Convert.ToDecimal(venta);
            decimal c = costo == DBNull.Value ? 0m : Convert.ToDecimal(costo);
            return v == 0m ? 0m : Math.Round(c * 100m / v, 2);
        }

        // Agrega una fila con la suma del grupo (0 o 1) o de todos (-1).
        private void AgregarTotal(DataTable tabla, string nombre, int grupo)
        {
            DataRow total = tabla.NewRow();
            total["Nombre"] = nombre;
            total["EsTotal"] = grupo == -1 ? 2 : 1;
            foreach (string p in Periodos)
            {
                total[p] = 0m;
            }
            if (ConCosto)
            {
                total["CostoMes"] = 0m;
                total["CostoAnio"] = 0m;
            }
            foreach (DataRow r in tabla.Rows)
            {
                if (Convert.ToInt32(r["EsTotal"]) != 0 || (grupo != -1 && Convert.ToInt32(r["Grupo"]) != grupo))
                {
                    continue;
                }
                foreach (string p in Periodos)
                {
                    total[p] = Convert.ToDecimal(total[p]) + (r[p] == DBNull.Value ? 0m : Convert.ToDecimal(r[p]));
                }
                if (ConCosto)
                {
                    total["CostoMes"] = Convert.ToDecimal(total["CostoMes"]) + (r["CostoMes"] == DBNull.Value ? 0m : Convert.ToDecimal(r["CostoMes"]));
                    total["CostoAnio"] = Convert.ToDecimal(total["CostoAnio"]) + (r["CostoAnio"] == DBNull.Value ? 0m : Convert.ToDecimal(r["CostoAnio"]));
                }
            }
            if (ConCosto)
            {
                PonerPorcentajes(total);
            }
            tabla.Rows.Add(total);
        }

        protected override void DarFormato()
        {
            ColumnaTexto("Codigo", "Código", 11);
            ColumnaTexto("Nombre", EncabezadoNombre, 26);
            ColumnaTexto("Grupo", null, 12);
            foreach (string p in Periodos)
            {
                ColumnaNumerica(p, "n2", 14);
            }
            ColumnaNumerica("PctCostoMes", "n2", 7);
            ColumnaNumerica("PctCostoAnio", "n2", 7);
            Ocultar("CostoMes");
            Ocultar("CostoAnio");
            Ocultar("EsTotal");
            Cabecera("Dia", "Día " + diaConsultado.ToString("dd/MM"));
            Cabecera("Anio", "Año " + diaConsultado.Year);
            Cabecera("M3", "3 meses");
            Cabecera("M6", "6 meses");
            Cabecera("M9", "9 meses");
            Cabecera("M12", "12 meses");
            Cabecera("PctCostoMes", "% costo mes");
            Cabecera("PctCostoAnio", "% costo año");
            foreach (DataGridViewColumn c in grilla.Columns)
            {
                c.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            grilla.CellFormatting -= EstiloFila;
            grilla.CellFormatting += EstiloFila;
        }

        private void Ocultar(string columna)
        {
            if (grilla.Columns.Contains(columna))
            {
                grilla.Columns[columna].Visible = false;
            }
        }

        private void Cabecera(string columna, string texto)
        {
            if (grilla.Columns.Contains(columna))
            {
                grilla.Columns[columna].HeaderText = texto;
            }
        }

        // Los totales van en negrita sobre fondo azul claro; la columna del grupo muestra su nombre.
        private void EstiloFila(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0 || !grilla.Columns.Contains("EsTotal"))
            {
                return;
            }
            DataGridViewRow fila = grilla.Rows[e.RowIndex];
            object esTotal = fila.Cells["EsTotal"].Value;
            if (esTotal != null && esTotal != DBNull.Value && Convert.ToInt32(esTotal) > 0)
            {
                e.CellStyle.Font = new Font(Fuente, 9F, FontStyle.Bold);
                e.CellStyle.BackColor = Convert.ToInt32(esTotal) == 2 ? Color.FromArgb(205, 222, 245) : AzulClaro;
            }
            else if (grilla.Columns[e.ColumnIndex].Name == "Grupo" && e.Value != null && e.Value != DBNull.Value)
            {
                int grupo = Convert.ToInt32(e.Value);
                if (grupo >= 0 && grupo < NombresGrupo.Length)
                {
                    e.Value = NombresGrupo[grupo];
                    e.FormattingApplied = true;
                }
            }
        }

        protected override string ResumenDe(DataTable tabla)
        {
            int registros = tabla.Rows.Count - FilasDeTotales;
            if (registros <= 0)
            {
                return "No hay movimiento en los períodos hasta el " + diaConsultado.ToString("dd/MM/yyyy") + ".";
            }
            DataRow total = tabla.Rows[tabla.Rows.Count - 1];
            return registros + (registros == 1 ? " registro" : " registros") + "   ·   Total día " + Convert.ToDecimal(total["Dia"]).ToString("n2") +
                   "   ·   mes " + Convert.ToDecimal(total["Mes"]).ToString("n2") + "   ·   año " + Convert.ToDecimal(total["Anio"]).ToString("n2") +
                   "   ·   Los 3, 6, 9 y 12 meses son los meses completos anteriores al mes del día elegido.";
        }
    }

    // Ventas por Vendedor: ventas netas (facturas menos devoluciones, base neta) de cada vendedor por periodo, con el porcentaje que
    // representa el costo. Clientes son los vendedores de codigo menor a 80 y filial los de 80 o mayor. Sustituye a FRMVENTAXVENDEDORLEBRUN2.
    // A diferencia del sistema anterior, el mes y el anio se miden por la fecha de la factura y no por su fecha de vencimiento.
    public class lbxVentasPorVendedor : lbxVentasCompras
    {
        protected override string TituloPantalla { get { return "Ventas por vendedor"; } }
        protected override string DescripcionPantalla { get { return "Ventas netas de cada vendedor por día, semana (de lunes al día), mes, año y meses anteriores, con su porcentaje de costo. Separa clientes y filial."; } }
        protected override string TituloGrilla { get { return "Ventas por vendedor"; } }
        protected override string EncabezadoNombre { get { return "Vendedor"; } }
        protected override string[] NombresGrupo { get { return new[] { "Clientes", "Filial" }; } }
        protected override bool ConCosto { get { return true; } }

        protected override DataTable ConsultarPeriodos(DateTime dia)
        {
            return movimientos.VentasPorVendedor(dia);
        }
    }

    // Compras por Proveedor: compras netas (comprobantes de compra tipo 37 menos notas de credito tipo 43, base neta) de cada proveedor
    // por periodo. Importados son los proveedores de otro pais (en el sistema anterior eran los de codigo 8.900.000 o mayor).
    // Sustituye a FRMCOMPRASXPROVEEDOR del sistema anterior.
    public class lbxComprasPorProveedor : lbxVentasCompras
    {
        protected override string TituloPantalla { get { return "Compras por proveedor"; } }
        protected override string DescripcionPantalla { get { return "Compras netas a cada proveedor por día, semana (de lunes al día), mes, año y meses anteriores. Separa nacionales e importados."; } }
        protected override string TituloGrilla { get { return "Compras por proveedor"; } }
        protected override string EncabezadoNombre { get { return "Proveedor"; } }
        protected override string[] NombresGrupo { get { return new[] { "Nacionales", "Importados" }; } }

        protected override DataTable ConsultarPeriodos(DateTime dia)
        {
            return movimientos.ComprasPorProveedor(dia);
        }
    }
}
