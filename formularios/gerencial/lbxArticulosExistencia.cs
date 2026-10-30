using System;
using System.Data;
using System.Windows.Forms;

namespace lebrun.formularios.gerencial
{
    // Articulos Agotados (existencia 0) y Articulos Negativos (existencia menor que 0): productos filtrados por la fecha de su ultima venta.
    // Sustituyen a FRM_PRODUCTOS_AGOTADOS y FRM_PRODUCTOS_NEGATIVOS del sistema anterior.
    public abstract class lbxArticulosExistencia : lbxListadoGerencial
    {
        private readonly DateTimePicker dtpDesde = NuevaFecha(DateTime.Today.AddDays(-30));
        private readonly DateTimePicker dtpHasta = NuevaFecha(DateTime.Today);
        private readonly CheckBox chkActivos = new CheckBox();

        protected abstract bool Negativos { get; }

        protected override string DescripcionPantalla
        {
            get
            {
                return Negativos
                    ? "Productos con existencia menor que cero cuya última venta cae en el rango de fechas."
                    : "Productos sin existencia cuya última venta cae en el rango de fechas.";
            }
        }

        protected override string TituloGrilla { get { return Negativos ? "Productos con existencia negativa" : "Productos agotados"; } }

        protected override void CrearFiltros()
        {
            Filtro("Última venta desde", dtpDesde);
            Filtro("hasta", dtpHasta);
            chkActivos.Text = "Solo productos activos";
            chkActivos.Checked = true;
            chkActivos.AutoSize = true;
            Filtro(null, chkActivos);
        }

        protected override DataTable Consultar()
        {
            if (dtpDesde.Value.Date > dtpHasta.Value.Date)
            {
                throw new ArgumentException("La fecha «desde» no puede ser mayor que la fecha «hasta».");
            }
            return datos.ArticulosSinExistencia(Negativos, dtpDesde.Value.Date, dtpHasta.Value.Date, chkActivos.Checked);
        }

        protected override void DarFormato()
        {
            ColumnaTexto("Codigo", "Código", 11);
            ColumnaTexto("Descripcion", "Descripción", 34);
            ColumnaTexto("Estado", null, 8);
            ColumnaFecha("UltimaVenta", 10);
            ColumnaFecha("UltimaCompra", 12);
            ColumnaTexto("Vendedor", null, 14);
            ColumnaTexto("Proveedor", null, 22);
            ColumnaNumerica("Existencia", "n2", 10);
            grilla.Columns["UltimaVenta"].HeaderText = "Última venta";
            grilla.Columns["UltimaCompra"].HeaderText = "Última compra";
        }

        protected override string ResumenDe(DataTable tabla)
        {
            return tabla.Rows.Count + (tabla.Rows.Count == 1 ? " producto" : " productos");
        }
    }

    public class lbxArticulosAgotados : lbxArticulosExistencia
    {
        protected override string TituloPantalla { get { return "Artículos agotados"; } }
        protected override bool Negativos { get { return false; } }
    }

    public class lbxArticulosNegativos : lbxArticulosExistencia
    {
        protected override string TituloPantalla { get { return "Artículos negativos"; } }
        protected override bool Negativos { get { return true; } }
    }
}
