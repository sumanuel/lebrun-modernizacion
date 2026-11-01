using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using lebrun.clasesData;

namespace lebrun.clases.gerencial
{
    // Compras sugeridas: para un proveedor, lo que conviene comprar de cada uno de sus productos activos segun lo que se ha vendido y lo que hay
    // en existencia. Sustituye a FRMCOMPSUGE del sistema anterior.
    //
    // Se mide el promedio mensual de unidades vendidas (facturas menos devoluciones) en el periodo elegido como base (15 dias, 1, 3, 6 o 12 meses
    // anteriores a hoy). Los meses de existencia son la existencia entre ese promedio, y lo que se sugiere comprar es lo que falta para cubrir
    // los meses de cobertura deseados (por omision, los meses del periodo base).
    // A diferencia del sistema anterior, todo se calcula con dos consultas por proveedor (no una por producto y periodo), los meses de
    // existencia siempre estan en meses (en el anterior, con la base de 15 dias estaban en periodos de 15 dias), no se guarda nada en la tabla
    // compsugcontenedor y se puede valorar con el ultimo costo (por omision) o con el costo de reposicion, que cae al ultimo costo cuando el
    // producto no tiene precio de lista.
    public class ComprasSugeridas
    {
        public const int LimiteFilas = 5000;

        public static readonly string[] Bases = { "15 días", "1 mes", "3 meses", "6 meses", "12 meses" };
        public static readonly decimal[] MesesDeBase = { 0.5m, 1m, 3m, 6m, 12m };

        private readonly ConexionBD baseDatos = new ConexionBD();

        private DataTable Ejecutar(string sql)
        {
            DataTable tabla = baseDatos.fDataTable(sql);
            baseDatos.cerrarConexion();
            return tabla;
        }

        private static string Texto(string valor)
        {
            return (valor ?? "").Trim().Replace("\\", "\\\\").Replace("'", "''");
        }

        private static string F(DateTime fecha)
        {
            return "'" + fecha.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture) + "'";
        }

        private static decimal D(object valor)
        {
            return valor == null || valor == DBNull.Value ? 0m : Convert.ToDecimal(valor);
        }

        // Proveedores que tienen productos asignados (los unicos de los que se puede sugerir una compra).
        public DataTable Proveedores()
        {
            return Ejecutar("SELECT pr.pro_codigo AS Codigo, pr.pro_nombre AS Nombre FROM admproveedor pr " +
                            "WHERE pr.pro_codigo IN (SELECT pin_proveedor FROM adminvparam) ORDER BY pr.pro_nombre, pr.pro_codigo");
        }

        // Tasa de cambio mas reciente de la moneda: se busca entre la moneda y sus variantes (el sistema guarda varias tasas por moneda con el
        // sufijo 2, 3, 4, 5 u 8; la 8 es la del BCV y es la que hoy se actualiza). El bolivar no tiene tasa.
        private static string TasaReciente(string columnaMoneda)
        {
            return "(SELECT h.histo_valor FROM admhistodivi h WHERE " + columnaMoneda + " NOT IN ('Bs', '') AND h.histo_moneda IN (" + columnaMoneda + ", " +
                   "CONCAT(" + columnaMoneda + ", '2'), CONCAT(" + columnaMoneda + ", '3'), CONCAT(" + columnaMoneda + ", '4'), CONCAT(" + columnaMoneda + ", '5'), CONCAT(" + columnaMoneda + ", '8')) " +
                   "ORDER BY h.histo_fecha DESC, h.histo_hora DESC LIMIT 1)";
        }

        // Costo de reposicion: precio de lista del proveedor con el arancel, los factores del proveedor (CIF, otros, gastos, financiamiento),
        // la tasa de cambio y los descuentos del producto y del proveedor, uno tras otro. Es 0 si el producto no tiene precio de lista.
        private static decimal CostoReposicion(decimal lista, decimal arancel, decimal tasa, decimal cif, decimal otros, decimal gastos, decimal financiamiento, decimal[] descuentos)
        {
            if (lista <= 0m)
            {
                return 0m;
            }
            decimal costo = arancel > 0m ? lista * (1m + arancel / 100m) : lista;
            foreach (decimal factor in new[] { cif, otros, gastos, tasa, financiamiento })
            {
                if (factor > 0m)
                {
                    costo *= factor;
                }
            }
            foreach (decimal descuento in descuentos)
            {
                if (descuento > 0m)
                {
                    costo -= costo * descuento / 100m;
                }
            }
            return costo;
        }

        // Periodos de venta respecto a hoy: los 15 dias anteriores, el mes anterior y los 3, 6 y 12 meses completos anteriores, mas el mes en curso.
        private static DateTime[][] Periodos(DateTime hoy)
        {
            DateTime primero = new DateTime(hoy.Year, hoy.Month, 1);
            DateTime finAnterior = primero.AddDays(-1);
            return new[]
            {
                new[] { hoy.AddDays(-15), hoy.AddDays(-1) },
                new[] { primero.AddMonths(-1), finAnterior },
                new[] { primero.AddMonths(-3), finAnterior },
                new[] { primero.AddMonths(-6), finAnterior },
                new[] { primero.AddMonths(-12), finAnterior },
                new[] { primero, hoy }
            };
        }

        // Calcula la sugerencia de compra del proveedor. baseIndice: posicion en Bases del periodo que se usa como promedio.
        // Devuelve una fila por producto activo del proveedor, ordenadas por el monto a comprar; resumen describe el proveedor y los totales.
        public DataTable Calcular(string proveedor, int baseIndice, decimal mesesCobertura, bool usarReposicion, bool soloComprar, DateTime hoy, out string resumen)
        {
            string codigo = Texto(proveedor);
            DataTable ficha = Ejecutar("SELECT pr.pro_nombre, pr.pro_divisa, pr.pro_descuento, pr.pro_dcto2, pr.pro_dcto3, f.Factor_CIF, f.Factor_FIN, f.Factor_GAS, " +
                                       "f.Factor_Otros, f.Factor_Ent, " + TasaReciente("pr.pro_divisa") + " AS Tasa " +
                                       "FROM admproveedor pr LEFT JOIN admproveedor2 f ON f.pro2_codigo = pr.pro_codigo WHERE pr.pro_codigo = '" + codigo + "'");
            if (ficha.Rows.Count == 0)
            {
                throw new ArgumentException("No existe el proveedor '" + proveedor + "'.");
            }
            DataRow p = ficha.Rows[0];
            decimal cif = D(p["Factor_CIF"]), fin = D(p["Factor_FIN"]), gas = D(p["Factor_GAS"]), otros = D(p["Factor_Otros"]);
            decimal[] descuentosProveedor = { D(p["pro_descuento"]), D(p["pro_dcto2"]), D(p["pro_dcto3"]) };

            DataTable productos = Ejecutar("SELECT a.inv_codigo AS Codigo, a.inv_descri AS Descripcion, m.ime_undmed AS Unidad, b.existencia AS Existencia, " +
                                           "b.pre_lista2 AS PrecioLista, b.ult_costo AS UltimoCosto, b.dcto_prov1 AS K1, b.dcto_prov2 AS K2, b.dcto_prov3 AS K3, " +
                                           "ar.ara_porcen AS Arancel, " + TasaReciente("p.pin_moneda") + " AS Tasa " +
                                           "FROM adminvparam p INNER JOIN adminv a ON a.inv_codigo = p.pin_codigo INNER JOIN adminv2 b ON b.inv2_codigo = a.inv_codigo " +
                                           "LEFT JOIN adminvmed m ON m.ime_codigo = a.inv_codigo AND m.ime_principal = '1' " +
                                           "LEFT JOIN admarancel ar ON ar.ara_codigo = a.inv_arancel " +
                                           "WHERE p.pin_proveedor = '" + codigo + "' AND a.inv_estado = 'Activo' ORDER BY a.inv_codigo LIMIT " + (LimiteFilas + 1));

            DateTime[][] periodos = Periodos(hoy.Date);
            DateTime primeraFecha = periodos[0][0];
            foreach (DateTime[] periodo in periodos)
            {
                if (periodo[0] < primeraFecha)
                {
                    primeraFecha = periodo[0];
                }
            }
            string[] nombres = { "V15D", "V1M", "V3M", "V6M", "V12M", "VMes" };
            List<string> sumas = new List<string>();
            for (int i = 0; i < periodos.Length; i++)
            {
                sumas.Add("SUM(IF(v.mov_fecha BETWEEN " + F(periodos[i][0]) + " AND " + F(periodos[i][1]) + ", IF(v.mov_tipdoc = 'FAV', v.mov_cant, -v.mov_cant), 0)) AS " + nombres[i]);
            }
            DataTable ventas = Ejecutar("SELECT v.mov_codigo AS Codigo, " + string.Join(", ", sumas.ToArray()) + " FROM adminvmov v " +
                                        "WHERE v.mov_tipdoc IN ('FAV', 'DEV') AND v.mov_fecha BETWEEN " + F(primeraFecha) + " AND " + F(hoy.Date) +
                                        " AND v.mov_codigo IN (SELECT pin_codigo FROM adminvparam WHERE pin_proveedor = '" + codigo + "') GROUP BY v.mov_codigo");
            Dictionary<string, DataRow> ventasPorProducto = new Dictionary<string, DataRow>();
            foreach (DataRow r in ventas.Rows)
            {
                ventasPorProducto[Convert.ToString(r["Codigo"]).Trim()] = r;
            }

            DataTable resultado = new DataTable();
            resultado.Columns.Add("Codigo", typeof(string));
            resultado.Columns.Add("Descripcion", typeof(string));
            resultado.Columns.Add("Unidad", typeof(string));
            resultado.Columns.Add("Existencia", typeof(decimal));
            resultado.Columns.Add("Prom15D", typeof(decimal));
            resultado.Columns.Add("Prom1M", typeof(decimal));
            resultado.Columns.Add("Prom3M", typeof(decimal));
            resultado.Columns.Add("Prom6M", typeof(decimal));
            resultado.Columns.Add("Prom12M", typeof(decimal));
            resultado.Columns.Add("VendidoMes", typeof(decimal));
            resultado.Columns.Add("MesesExistencia", typeof(decimal));
            resultado.Columns.Add("AComprar", typeof(decimal));
            resultado.Columns.Add("Costo", typeof(decimal));
            resultado.Columns.Add("BaseCosto", typeof(int));  // 1 = reposicion, 2 = ultimo costo, 0 = sin costo
            resultado.Columns.Add("PrecioLista", typeof(decimal));
            resultado.Columns.Add("TotalAComprar", typeof(decimal));

            int porComprar = 0;
            decimal totalComprar = 0m;
            foreach (DataRow r in productos.Rows)
            {
                string cod = Convert.ToString(r["Codigo"]).Trim();
                DataRow v;
                ventasPorProducto.TryGetValue(cod, out v);
                decimal[] promedios = new decimal[5];
                for (int i = 0; i < 5; i++)
                {
                    promedios[i] = v == null ? 0m : D(v[nombres[i]]) / MesesDeBase[i];
                }
                decimal existencia = D(r["Existencia"]);
                decimal promedioBase = promedios[baseIndice];
                decimal? mesesExistencia = promedioBase > 0m ? Math.Round(existencia / promedioBase, 2) : (decimal?)null;
                decimal necesario = promedioBase * mesesCobertura;
                decimal aComprar = promedioBase > 0m && existencia < necesario ? Math.Round(necesario - existencia, 2) : 0m;

                decimal tasa = D(r["Tasa"]);
                decimal costo = usarReposicion ? CostoReposicion(D(r["PrecioLista"]), D(r["Arancel"]), tasa, cif, otros, gas, fin,
                    new[] { D(r["K1"]), D(r["K2"]), D(r["K3"]), descuentosProveedor[0], descuentosProveedor[1], descuentosProveedor[2] }) : 0m;
                int baseCosto = 1;
                if (costo <= 0m)
                {
                    costo = D(r["UltimoCosto"]);
                    baseCosto = costo > 0m ? 2 : 0;
                }
                costo = Math.Round(costo, 2);

                if (soloComprar && aComprar <= 0m)
                {
                    continue;
                }
                if (aComprar > 0m)
                {
                    porComprar++;
                    totalComprar += Math.Round(aComprar * costo, 2);
                }
                DataRow fila = resultado.NewRow();
                fila["Codigo"] = cod;
                fila["Descripcion"] = r["Descripcion"];
                fila["Unidad"] = r["Unidad"];
                fila["Existencia"] = existencia;
                fila["Prom15D"] = Math.Round(promedios[0], 2);
                fila["Prom1M"] = Math.Round(promedios[1], 2);
                fila["Prom3M"] = Math.Round(promedios[2], 2);
                fila["Prom6M"] = Math.Round(promedios[3], 2);
                fila["Prom12M"] = Math.Round(promedios[4], 2);
                fila["VendidoMes"] = v == null ? 0m : D(v["VMes"]);
                fila["MesesExistencia"] = mesesExistencia.HasValue ? (object)mesesExistencia.Value : DBNull.Value;
                fila["AComprar"] = aComprar;
                fila["Costo"] = costo;
                fila["BaseCosto"] = baseCosto;
                fila["PrecioLista"] = D(r["PrecioLista"]);
                fila["TotalAComprar"] = Math.Round(aComprar * costo, 2);
                resultado.Rows.Add(fila);
            }

            DataView orden = resultado.DefaultView;
            orden.Sort = "TotalAComprar DESC, Codigo ASC";
            DataTable ordenado = orden.ToTable();

            string moneda = p["pro_divisa"] == DBNull.Value ? "" : Convert.ToString(p["pro_divisa"]).Trim();
            decimal tasaProveedor = D(p["Tasa"]);
            resumen = Convert.ToString(p["pro_nombre"]).Trim() + "   ·   " + productos.Rows.Count + " productos activos   ·   " + porComprar + " por comprar   ·   Total a comprar " +
                      totalComprar.ToString("n2") + (D(p["Factor_Ent"]) > 0m ? "   ·   Entrega " + D(p["Factor_Ent"]).ToString("n0") + " días" : "") +
                      (moneda.Length > 0 && tasaProveedor > 0m ? "   ·   Tasa " + tasaProveedor.ToString("n2") : "");
            return ordenado;
        }
    }
}
