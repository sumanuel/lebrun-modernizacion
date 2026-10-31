using System;
using System.Data;
using System.Windows.Forms;
using lebrun.clases.gerencial;

namespace lebrun.formularios.gerencial
{
    // Base de Movimientos Clientes, Movimiento Items y Productos por Vendedor: un rango de fechas, un filtro de producto y una lista
    // de clientes, productos o vendedores con lo facturado, devuelto y neto en unidades, lineas y monto; al elegir uno se ven sus detalles.
    public abstract class lbxMovimientoTerceros : lbxListadoGerencial
    {
        protected readonly MovimientosGerenciales movimientos = new MovimientosGerenciales();
        protected readonly DateTimePicker dtpDesde = NuevaFecha(new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1));
        protected readonly DateTimePicker dtpHasta = NuevaFecha(DateTime.Today);
        protected readonly TextBox txtProducto = new TextBox();

        // Rango y producto de la ultima consulta (el detalle usa estos, no lo que se haya cambiado despues en los filtros).
        protected DateTime consultadoDesde;
        protected DateTime consultadoHasta;
        protected string consultadoProducto;

        protected abstract string EncabezadoNombre { get; }
        protected virtual string EtiquetaProducto { get { return "Producto (opcional)"; } }
        protected virtual bool ProductoObligatorio { get { return false; } }
        protected virtual bool ConCosto { get { return false; } }
        protected override bool TieneDetalle { get { return true; } }

        protected override void CrearFiltros()
        {
            Filtro("Desde", dtpDesde);
            Filtro("Hasta", dtpHasta);
            txtProducto.Width = 140;
            txtProducto.CharacterCasing = CharacterCasing.Upper;
            Filtro(EtiquetaProducto, txtProducto);
        }

        // Valida los filtros y los guarda para el detalle.
        protected void LeerFiltros()
        {
            if (dtpDesde.Value.Date > dtpHasta.Value.Date)
            {
                throw new ArgumentException("La fecha 'Desde' no puede ser posterior a la fecha 'Hasta'.");
            }
            consultadoProducto = txtProducto.Text.Trim();
            if (ProductoObligatorio && consultadoProducto.Length == 0)
            {
                throw new ArgumentException("Indique el código del producto.");
            }
            consultadoDesde = dtpDesde.Value.Date;
            consultadoHasta = dtpHasta.Value.Date;
        }

        protected override void DarFormato()
        {
            FormatoMedidas(grilla, EncabezadoNombre, ConCosto);
        }

        // Columnas comunes: codigo, nombre y las medidas de unidades, lineas y monto.
        protected static void FormatoMedidas(DataGridView g, string encabezadoNombre, bool conCosto)
        {
            ColumnaTexto(g, "Codigo", "Código", 17);
            ColumnaTexto(g, "Nombre", encabezadoNombre, 28);
            ColumnaNumerica(g, "UndFav", "n2", 9);
            ColumnaNumerica(g, "UndDev", "n2", 9);
            ColumnaNumerica(g, "UndNeto", "n2", 9);
            ColumnaNumerica(g, "LineasFav", "n0", 7);
            ColumnaNumerica(g, "LineasDev", "n0", 7);
            ColumnaNumerica(g, "LineasNeto", "n0", 7);
            ColumnaNumerica(g, "MontoFav", "n2", 11);
            ColumnaNumerica(g, "MontoDev", "n2", 11);
            ColumnaNumerica(g, "MontoNeto", "n2", 12);
            ColumnaNumerica(g, "Costo", "n2", 11);
            ColumnaNumerica(g, "Margen", "n2", 7);
            Encabezados(g, "UndFav", "Und. facturadas", "UndDev", "Und. devueltas", "UndNeto", "Und. netas",
                "LineasFav", "Líneas fact.", "LineasDev", "Líneas dev.", "LineasNeto", "Líneas netas",
                "MontoFav", "Monto facturado", "MontoDev", "Monto devuelto", "MontoNeto", "Monto neto", "Margen", "% Margen");
        }

        protected static void Encabezados(DataGridView g, params string[] paresNombreTexto)
        {
            for (int i = 0; i + 1 < paresNombreTexto.Length; i += 2)
            {
                if (g.Columns.Contains(paresNombreTexto[i]))
                {
                    g.Columns[paresNombreTexto[i]].HeaderText = paresNombreTexto[i + 1];
                }
            }
        }

        protected static decimal Suma(DataTable tabla, string columna)
        {
            decimal total = 0m;
            if (!tabla.Columns.Contains(columna))
            {
                return total;
            }
            foreach (DataRow r in tabla.Rows)
            {
                if (r[columna] != DBNull.Value)
                {
                    total += Convert.ToDecimal(r[columna]);
                }
            }
            return total;
        }

        protected override string ResumenDe(DataTable tabla)
        {
            string texto = tabla.Rows.Count + " " + (tabla.Rows.Count == 1 ? EncabezadoNombre.ToLower() : EncabezadoNombre.ToLower() + "s") +
                           "   ·   Unidades netas " + Suma(tabla, "UndNeto").ToString("n2") + "   ·   Monto neto " + Suma(tabla, "MontoNeto").ToString("n2");
            if (ConCosto)
            {
                decimal neto = Suma(tabla, "MontoNeto");
                decimal costo = Suma(tabla, "Costo");
                texto += "   ·   Costo " + costo.ToString("n2") + "   ·   Margen " + (neto == 0m ? 0m : (neto - costo) * 100m / neto).ToString("n2") + "%";
            }
            return texto;
        }

        // Agrega el porcentaje de margen sobre el monto neto (monto neto menos costo, entre monto neto).
        protected static void AgregarMargen(DataTable tabla)
        {
            tabla.Columns.Add("Margen", typeof(decimal));
            foreach (DataRow r in tabla.Rows)
            {
                decimal neto = Convert.ToDecimal(r["MontoNeto"]);
                decimal costo = r["Costo"] == DBNull.Value ? 0m : Convert.ToDecimal(r["Costo"]);
                r["Margen"] = neto == 0m ? 0m : Math.Round((neto - costo) * 100m / neto, 2);
            }
        }
    }

    // Movimientos Clientes: lo facturado y devuelto a cada cliente en un rango de fechas; al elegir un cliente se ven los productos que compro.
    // Sustituye a FRMMOVCLIENTES del sistema anterior.
    public class lbxMovimientosClientes : lbxMovimientoTerceros
    {
        protected override string TituloPantalla { get { return "Movimientos de clientes"; } }
        protected override string DescripcionPantalla { get { return "Lo facturado y devuelto a cada cliente en el rango de fechas. Seleccione un cliente para ver los productos que compró."; } }
        protected override string TituloGrilla { get { return "Clientes"; } }
        protected override string TituloDetalle { get { return "Productos del cliente seleccionado"; } }
        protected override string EncabezadoNombre { get { return "Cliente"; } }

        protected override DataTable Consultar()
        {
            LeerFiltros();
            return movimientos.MovimientosPorCliente(consultadoDesde, consultadoHasta, consultadoProducto);
        }

        protected override DataTable ConsultarDetalle(DataRow fila)
        {
            return movimientos.ProductosDeCliente(Convert.ToString(fila["Codigo"]), consultadoDesde, consultadoHasta, consultadoProducto);
        }

        protected override void DarFormatoDetalle()
        {
            FormatoMedidas(grillaDetalle, "Producto", false);
        }
    }

    // Movimiento Items: lo vendido y devuelto de cada producto en un rango de fechas, con su costo y margen; al elegir un producto se ven sus lineas.
    // Sustituye a FRMMOVPRODUCTOS del sistema anterior.
    public class lbxMovimientoItems : lbxMovimientoTerceros
    {
        protected override string TituloPantalla { get { return "Movimiento de items"; } }
        protected override string DescripcionPantalla { get { return "Lo vendido y devuelto de cada producto en el rango de fechas, con su costo y margen. Seleccione un producto para ver sus facturas y devoluciones."; } }
        protected override string TituloGrilla { get { return "Productos"; } }
        protected override string TituloDetalle { get { return "Facturas y devoluciones del producto seleccionado"; } }
        protected override string EncabezadoNombre { get { return "Producto"; } }
        protected override string EtiquetaProducto { get { return "Código de producto (opcional)"; } }
        protected override bool ConCosto { get { return true; } }

        protected override DataTable Consultar()
        {
            LeerFiltros();
            DataTable tabla = movimientos.MovimientosPorProducto(consultadoDesde, consultadoHasta, consultadoProducto);
            AgregarMargen(tabla);
            return tabla;
        }

        protected override DataTable ConsultarDetalle(DataRow fila)
        {
            return movimientos.LineasDeProducto(Convert.ToString(fila["Codigo"]), consultadoDesde, consultadoHasta);
        }

        protected override void DarFormatoDetalle()
        {
            ColumnaFecha(grillaDetalle, "Fecha", 10);
            ColumnaTexto(grillaDetalle, "Tipo", null, 6);
            ColumnaTexto(grillaDetalle, "Documento", null, 14);
            ColumnaTexto(grillaDetalle, "Cliente", null, 32);
            ColumnaTexto(grillaDetalle, "Vendedor", null, 22);
            ColumnaNumerica(grillaDetalle, "Cantidad", "n2", 10);
            ColumnaNumerica(grillaDetalle, "Precio", "n2", 12);
            ColumnaNumerica(grillaDetalle, "Monto", "n2", 13);
        }
    }

    // Productos por Vendedor: que vendedores vendieron un producto en un rango de fechas y cuanto; al elegir uno se ven sus documentos.
    // Sustituye a FRMPROXVENDE del sistema anterior.
    public class lbxProductosPorVendedor : lbxMovimientoTerceros
    {
        private string descripcionProducto;

        protected override string TituloPantalla { get { return "Productos por vendedor"; } }
        protected override string DescripcionPantalla { get { return "Indique un producto y el rango de fechas para ver qué vendedores lo vendieron. Seleccione un vendedor para ver sus facturas y devoluciones."; } }
        protected override string TituloGrilla { get { return "Vendedores"; } }
        protected override string TituloDetalle { get { return "Facturas y devoluciones del vendedor seleccionado"; } }
        protected override string EncabezadoNombre { get { return "Vendedor"; } }
        protected override string EtiquetaProducto { get { return "Código de producto"; } }
        protected override bool ProductoObligatorio { get { return true; } }

        protected override DataTable Consultar()
        {
            LeerFiltros();
            descripcionProducto = movimientos.DescripcionProducto(consultadoProducto);
            if (descripcionProducto == null)
            {
                throw new ArgumentException("No existe un producto con el código '" + consultadoProducto + "'.");
            }
            return movimientos.ProductoPorVendedor(consultadoProducto, consultadoDesde, consultadoHasta);
        }

        protected override DataTable ConsultarDetalle(DataRow fila)
        {
            return movimientos.LineasDeProductoPorVendedor(consultadoProducto, Convert.ToString(fila["Codigo"]), consultadoDesde, consultadoHasta);
        }

        protected override void DarFormatoDetalle()
        {
            ColumnaFecha(grillaDetalle, "Fecha", 10);
            ColumnaTexto(grillaDetalle, "Tipo", null, 6);
            ColumnaTexto(grillaDetalle, "Documento", null, 14);
            ColumnaTexto(grillaDetalle, "Cliente", null, 34);
            ColumnaNumerica(grillaDetalle, "Cantidad", "n2", 10);
            ColumnaNumerica(grillaDetalle, "Precio", "n2", 12);
            ColumnaNumerica(grillaDetalle, "Monto", "n2", 13);
        }

        protected override string ResumenDe(DataTable tabla)
        {
            return consultadoProducto + " · " + descripcionProducto + "   —   " + base.ResumenDe(tabla);
        }
    }
}
