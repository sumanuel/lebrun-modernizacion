using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Globalization;
using lebrun.clases.complementos;
using lebrun.clasesData;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace lebrun.clases.importaciones
{
    public class OrdenDeCompra
    {

        //Para la BD

        private string StrSql;
        private ConexionBD database;
        private MySqlDataReader dr;
        private DataSet ds;

        //variables

        private string correlativo;
        private string tipdoc;
        private string ordencompra;
        private string codigo;
        private string concep;
        private string cencost;
        private string sucus;
        private string moneda;
        private string codtra;
        private string condic;
        private string usuario;
        private string updUsuario;
        private string tiptra;
        private string origen;
        private string estadodoc;
        private string status;
        private string cbtnum;
        private string docorg;
        private string cbtpre;
        private string pagnum;
        private string cxp;
        private string plazo;
        private string valcamb;
        private string totaldivisa;
        private string prod;
        private string ditem;
        private string ord;
        private string fechaCarga;
        private string fechaRecep;
        private string documento;
        private string sufijo;
        private string proNombre;
        private string proRif;
        private string subTotal;
        private string total;
        private string fechaSistema;
        private string login;
        private string updLogin;
        private string fechaUltMod;
        // porcentajes de descuento de la orden; al leer una orden anterior a estas columnas quedan en null
        public decimal? Descuento1 { get; set; }
        public decimal? Descuento2 { get; set; }
        public decimal? Descuento3 { get; set; }

        // El total de la orden: los descuentos se aplican en cascada (cada uno sobre lo que queda del anterior), como en el sistema anterior
        // (ModelSoft) y en la Consulta Gerencial. 1.295,80 con 10 % y 5 % = 1.107,91.
        public static decimal TotalConDescuentos(decimal subtotal, params decimal[] porcentajes)
        {
            decimal total = subtotal;
            foreach (decimal porcentaje in porcentajes)
            {
                total -= total * porcentaje / 100m;
            }
            return Math.Round(total, 2, MidpointRounding.AwayFromZero);
        }

        // Bolivares por unidad de la moneda a la fecha de la orden. Los proveedores tienen la moneda "$" o el euro, cuyo historial propio dejo de
        // cargarse en 2022: se usa la tasa del BCV (dolar o euro), la misma de las ventas. null = no hay tasa cargada.
        public static decimal? TasaDeMoneda(string moneda, DateTime fecha)
        {
            string m = (moneda ?? "").Trim();
            if (m.Length == 0 || m.StartsWith("Bs", StringComparison.OrdinalIgnoreCase))
            {
                return 1m;
            }
            if (m == "$" || m == "$8")
            {
                return TasaCambio.Vigente(fecha, TasaCambio.Dolar);
            }
            // el euro puede llegar como el signo o como el byte 0x80 sin convertir
            bool euro = m[0] == '€' || m[0] == '';
            if (euro && (m.Length == 1 || m.Substring(1) == "8"))
            {
                return TasaCambio.Vigente(fecha, TasaCambio.Euro);
            }
            // otras monedas (por ejemplo $2 o la de aduana): la ultima de su propio historial hasta esa fecha
            DataTable dt = new ConexionBD().fDataTable("SELECT histo_valor FROM admhistodivi WHERE histo_moneda = '" + m.Replace("\\", "\\\\").Replace("'", "''") +
                                                       "' AND histo_fecha <= '" + fecha.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture) +
                                                       "' ORDER BY histo_fecha DESC, " + HoraGuardada.Sql("histo_hora") + " DESC LIMIT 1");
            if (dt.Rows.Count == 0 || dt.Rows[0][0] == DBNull.Value || Convert.ToDecimal(dt.Rows[0][0]) <= 0m)
            {
                return null;
            }
            return Convert.ToDecimal(dt.Rows[0][0]);
        }








        //get & set

    public string Correlativo
    {
        get { return correlativo; }
        set { correlativo = value; }
    }

    public string Tipdoc
    {
        get { return tipdoc; }
        set { tipdoc = value; }
    }

    public string Ordencompra
    {
        get { return ordencompra; }
        set { ordencompra = value; }
    }

    public string Codigo
    {
        get { return codigo; }
        set { codigo = value; }
    }

    public string Concep
    {
        get { return concep; }
        set { concep = value; }
    }

    public string Cencost
    {
        get { return cencost; }
        set { cencost = value; }
    }

    public string Sucus
    {
        get { return sucus; }
        set { sucus = value; }
    }

    public string Moneda
    {
        get { return moneda; }
        set { moneda = value; }
    }

    public string Codtra
    {
        get { return codtra; }
        set { codtra = value; }
    }

    public string Condic
    {
        get { return condic; }
        set { condic = value; }
    }

    public string Usuario
    {
        get { return usuario; }
        set { usuario = value; }
    }

    public string UpdUsuario
    {
        get { return updUsuario; }
        set { updUsuario = value; }
    }

    public string Tiptra
    {
        get { return tiptra; }
        set { tiptra = value; }
    }

    public string Origen
    {
        get { return origen; }
        set { origen = value; }
    }

    public string Estadodoc
    {
        get { return estadodoc; }
        set { estadodoc = value; }
    }

    public string Status
    {
        get { return status; }
        set { status = value; }
    }

    public string Cbtnum
    {
        get { return cbtnum; }
        set { cbtnum = value; }
    }

    public string Docorg
    {
        get { return docorg; }
        set { docorg = value; }
    }

    public string Cbtpre
    {
        get { return cbtpre; }
        set { cbtpre = value; }
    }

    public string Pagnum
    {
        get { return pagnum; }
        set { pagnum = value; }
    }

    public string Cxp
    {
        get { return cxp; }
        set { cxp = value; }
    }

    public string Plazo
    {
        get { return plazo; }
        set { plazo = value; }
    }

    public string Valcamb
    {
        get { return valcamb; }
        set { valcamb = value; }
    }

    public string Totaldivisa
    {
        get { return totaldivisa; }
        set { totaldivisa = value; }
    }

    public string Prod
    {
        get { return prod; }
        set { prod = value; }
    }

    public string Ditem
    {
        get { return ditem; }
        set { ditem = value; }
    }

    public string Ord
    {
        get { return ord; }
        set { ord = value; }
    }

    public string FechaCarga
    {
        get { return fechaCarga; }
        set { fechaCarga = value; }
    }

    public string FechaRecep
    {
        get { return fechaRecep; }
        set { fechaRecep = value; }
    }

    public string Documento
    {
        get { return documento; }
        set { documento = value; }
    }

    public string Sufijo
    {
        get { return sufijo; }
        set { sufijo = value; }
    }

    public string ProNombre
    {
        get { return proNombre; }
        set { proNombre = value; }
    }

    public string ProRif
    {
        get { return proRif; }
        set { proRif = value; }
    }

    public string SubTotal
    {
        get { return subTotal; }
        set { subTotal = value; }
    }

    public string Total
    {
        get { return total; }
        set { total = value; }
    }

    public string FechaSistema
    {
        get { return fechaSistema; }
        set { fechaSistema = value; }
    }

    public string FechaUltMod
    {
        get { return fechaUltMod; }
        set { fechaUltMod = value; }
    }





    //conexion

    public OrdenDeCompra() {
        database = new ConexionBD(); //sisadm
    }

    // Graba la orden completa (numero, cabecera y lineas) en una sola transaccion: si algo falla no cambia nada. Antes, al modificar, se borraba la
    // orden y despues se grababa la nueva; si esa grabacion fallaba (bastaba un apostrofo en las observaciones) la orden se perdia.
    // numeroExistente es null para una orden nueva. prepararDatos llena esta cabecera y las lineas, y se llama cuando Correlativo ya tiene su numero.
    public void grabar(string numeroExistente, Action prepararDatos, DataGridView lineas)
    {
        string numeroAnterior = this.correlativo;
        try
        {
            database.ejecutarTransaccion(delegate (MySqlConnection conexion, MySqlTransaction transaccion)
            {
                if (string.IsNullOrEmpty(numeroExistente))
                {
                    this.correlativo = siguienteCorrelativo(conexion, transaccion);
                }
                else
                {
                    this.correlativo = numeroExistente;
                    borrar(conexion, transaccion, numeroExistente);
                }
                prepararDatos();
                insertarCabecera(conexion, transaccion);
                DetallesOrdenDeCompra.insertar(conexion, transaccion, lineas);
            });
        }
        catch
        {
            this.correlativo = numeroAnterior;
            throw;
        }
    }

    private static MySqlCommand comando(MySqlConnection conexion, MySqlTransaction transaccion, string sql)
    {
        return new MySqlCommand(sql, conexion, transaccion);
    }

    // El numero que sigue al ultimo usado (admtipdocpro, tipo 30). Se saltan los que ya tengan cabecera o lineas, para no chocar con ellos.
    private static string siguienteCorrelativo(MySqlConnection conexion, MySqlTransaction transaccion)
    {
        object ultimo = comando(conexion, transaccion, "SELECT tdc_correlativo FROM admtipdocpro WHERE tdc_codigo='30' FOR UPDATE").ExecuteScalar();
        if (ultimo == null || ultimo == DBNull.Value)
        {
            throw new InvalidOperationException("No está configurado el correlativo de las órdenes de compra (admtipdocpro, tipo 30).");
        }
        double numero = Convert.ToDouble(ultimo.ToString());
        string candidato;
        bool ocupado;
        do
        {
            numero++;
            candidato = String.Format("{0:0000000000}", numero);
            MySqlCommand buscar = comando(conexion, transaccion, "SELECT (SELECT COUNT(*) FROM admordencompra WHERE ord_correlativo=@numero) + " +
                                                                 "(SELECT COUNT(*) FROM adminvmov_temp WHERE mov_docume=@numero AND mov_tipdoc='30')");
            buscar.Parameters.AddWithValue("@numero", candidato);
            ocupado = Convert.ToInt64(buscar.ExecuteScalar()) > 0;
        } while (ocupado);
        MySqlCommand actualizar = comando(conexion, transaccion, "UPDATE admtipdocpro SET tdc_correlativo=@numero WHERE tdc_codigo='30'");
        actualizar.Parameters.AddWithValue("@numero", candidato);
        actualizar.ExecuteNonQuery();
        return candidato;
    }

    private static void borrar(MySqlConnection conexion, MySqlTransaction transaccion, string numero)
    {
        // en las lineas se filtra por el tipo: la tabla tambien guarda documentos de otros tipos, que pueden repetir el numero
        foreach (string sql in new[] { "DELETE FROM admordencompra WHERE ord_correlativo=@numero",
                                       "DELETE FROM adminvmov_temp WHERE mov_docume=@numero AND mov_tipdoc='30'" })
        {
            MySqlCommand c = comando(conexion, transaccion, sql);
            c.Parameters.AddWithValue("@numero", numero);
            c.ExecuteNonQuery();
        }
    }

    private static string porcentajeSql(decimal? porcentaje)
    {
        return (porcentaje ?? 0m).ToString(CultureInfo.InvariantCulture);
    }

    private static decimal? leerPorcentaje(MySqlDataReader lector, string columna)
    {
        int i = lector.GetOrdinal(columna);
        return lector.IsDBNull(i) ? (decimal?)null : lector.GetDecimal(i);
    }

    private void insertarCabecera(MySqlConnection conexion, MySqlTransaction transaccion)
    {
        string hoy = "" + DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day;
        string sistema = hoy;
        if (fechaSistema != null)
        {
            DateTime f = Convert.ToDateTime(this.fechaSistema);
            sistema = "" + f.Year + "-" + f.Month + "-" + f.Day;
        }
        // columna y valor; los datos van como parametros, asi un apostrofo o cualquier otro signo se guarda tal cual
        string[,] datos =
        {
            { "ord_correlativo", correlativo }, { "ord_tipdoc", tipdoc }, { "ord_ordencompra", correlativo }, { "ord_concep", concep },
            { "ord_cencost", cencost }, { "ord_sucus", sucus }, { "ord_moneda", moneda }, { "ord_codtra", codtra }, { "ord_condic", condic },
            { "ord_usuario", usuario }, { "ord_tiptra", tiptra }, { "ord_codigo", codigo }, { "ord_origen", origen }, { "ord_estadodoc", estadodoc },
            { "ord_status", status }, { "ord_fechCarg", fechaCarga }, { "ord_hora", "" + DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second },
            { "ord_cbtnum", cbtnum }, { "ord_docorg", docorg }, { "ord_cbtpre", cbtpre }, { "ord_pagnum", pagnum }, { "ord_cxp", cxp },
            { "ord_plazo", plazo }, { "ord_valcamb", valcamb }, { "ord_totaldivisa", totaldivisa }, { "ord_prod", prod }, { "ord_ditem", ditem },
            { "ord_documento", documento }, { "ord_sufijo", sufijo }, { "ord_subtot", subTotal }, { "ord_total", total },
            { "ord_usuarioUdp", updUsuario }, { "ord_fechaRecep", fechaRecep }, { "ord_fechaSistema", sistema }, { "ord_fechaUltMod", fechaUltMod },
            { "ord_desc1", porcentajeSql(Descuento1) }, { "ord_desc2", porcentajeSql(Descuento2) }, { "ord_desc3", porcentajeSql(Descuento3) }
        };
        StringBuilder columnas = new StringBuilder();
        StringBuilder valores = new StringBuilder();
        MySqlCommand c = comando(conexion, transaccion, "");
        for (int i = 0; i < datos.GetLength(0); i++)
        {
            columnas.Append((i > 0 ? "," : "") + datos[i, 0]);
            valores.Append((i > 0 ? "," : "") + "@p" + i);
            c.Parameters.AddWithValue("@p" + i, datos[i, 1] ?? "");
        }
        c.CommandText = "INSERT INTO admordencompra (" + columnas + ") VALUES (" + valores + ")";
        c.ExecuteNonQuery();
    }

    public void buscarOrdenCompra() {

        StrSql = "SELECT * FROM admordencompra LEFT OUTER JOIN admproveedor ON pro_codigo = ord_codigo WHERE ord_correlativo='$1';";
        StrSql = StrSql.Replace("$1", this.correlativo);

        dr = database.ejecutarQueryDr(StrSql);

        if (dr.HasRows)
        {
            dr.Read();

            this.correlativo = dr.GetString("ord_correlativo");
            this.codigo = dr.GetString("ord_codigo");
            this.concep = dr.GetString("ord_concep");
            this.condic = dr.GetString("ord_condic");
            this.moneda = dr.GetString("ord_moneda");
            this.valcamb = Convert.ToString(dr.GetDecimal("ord_valcamb"));
            this.documento = dr.GetString("ord_documento");
            this.sufijo = dr.GetString("ord_sufijo");
            this.proNombre = dr.GetString("pro_nombre");
            this.proRif = dr.GetString("pro_rif");
            this.subTotal = Convert.ToString(dr.GetDecimal("ord_subtot"));
            this.total = Convert.ToString(dr.GetDecimal("ord_total"));
            this.prod = Convert.ToString(dr.GetDecimal("ord_prod"));
            this.ditem = Convert.ToString(dr.GetInt32("ord_ditem"));
            this.totaldivisa = Convert.ToString(dr.GetDecimal("ord_totaldivisa"));
            this.fechaCarga = Convert.ToString(dr.GetString("ord_fechCarg"));
            this.fechaRecep = Convert.ToString(dr.GetString("ord_fechaRecep"));
            this.fechaSistema = Convert.ToString(dr.GetString("ord_fechaSistema"));
            this.usuario = dr.GetString("ord_usuario");
            this.updUsuario = dr.GetString("ord_usuarioUdp");
            this.Descuento1 = leerPorcentaje(dr, "ord_desc1");
            this.Descuento2 = leerPorcentaje(dr, "ord_desc2");
            this.Descuento3 = leerPorcentaje(dr, "ord_desc3");
        }

        database.cerrarConexion();
        dr.Close();
    }
        //

    public DataTable armarLbxOrdenCompra() {

        StrSql = "SELECT ord_correlativo 'Correlativo', ord_tipdoc 'Tipo Doc', ord_ordencompra 'Orden Compra', ord_documento 'Documento'," +
                    "ord_sufijo 'Sufijo',ord_concep 'Concepto', ord_codigo 'Cod Proveedor', pro_nombre 'Nombre Proveedor'" +
                    " FROM admordencompra LEFT OUTER JOIN admproveedor ON ord_codigo=pro_codigo";
        
        return database.fDataTable(StrSql);
        //database.cerrarConexion();

    }

    // Busca por codigo de proveedor (ese codigo, escrito con o sin los ceros de la izquierda), por parte del nombre o por el comienzo del documento.
    // Sin texto devuelve todas las ordenes.
    public DataTable buscarOrdenCompra(int option, string value)
    {
        string texto = (value ?? "").Trim().Replace("\\", "\\\\").Replace("'", "''");
        if (texto.Length == 0)
        {
            return armarLbxOrdenCompra();
        }
        string parecido = texto.Replace("%", "\\%").Replace("_", "\\_");
        string filtro;
        if (option == 0)
        {
            filtro = "ord_codigo = '" + texto + "' OR TRIM(LEADING '0' FROM ord_codigo) = TRIM(LEADING '0' FROM '" + texto + "')";
        }
        else if (option == 1)
        {
            filtro = "pro_nombre LIKE '%" + parecido + "%'";
        }
        else
        {
            filtro = "ord_documento LIKE '" + parecido + "%'";
        }
        StrSql = "SELECT ord_correlativo 'Correlativo', ord_tipdoc 'Tipo Doc', ord_ordencompra 'Orden Compra', ord_documento 'Documento'," +
                 "ord_sufijo 'Sufijo',ord_concep 'Concepto', ord_codigo 'Cod Proveedor', pro_nombre 'Nombre Proveedor' " +
                 "FROM admordencompra LEFT OUTER JOIN admproveedor ON ord_codigo=pro_codigo WHERE " + filtro;
        return database.fDataTable(StrSql);
    }

    }//fin clase
}
