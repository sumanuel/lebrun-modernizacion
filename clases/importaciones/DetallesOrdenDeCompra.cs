using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using lebrun.clasesData;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace lebrun.clases.importaciones
{
    class DetallesOrdenDeCompra
    {
        private string codcta;
        private string documento;
        private string sufijo;
        private string tipo;
        private int item;
        private string codigo;
        private string producto;
        private string unidad;
        private int cantidad;
        private decimal costo;
        private decimal desc1;
        private decimal desc2;
        private decimal desc3;
        //private decimal total;
        //private string memo;
        private string StrSql;


        public string Codcta
        {
            get { return codcta; }
            set { codcta = value; }
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

        public string Tipo
        {
            get { return tipo; }
            set { tipo = value; }
        }

        public int Item
        {
            get { return item; }
            set { item = value; }
        }

        public string Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        public string Producto
        {
            get { return producto; }
            set { producto = value; }
        }

        public string Unidad
        {
            get { return unidad; }
            set { unidad = value; }
        }

        public int Cantidad
        {
            get { return cantidad; }
            set { cantidad = value; }
        }

        public decimal Costo
        {
            get { return costo; }
            set { costo = value; }
        }

        public decimal Desc1
        {
            get { return desc1; }
            set { desc1 = value; }
        }

        public decimal Desc2
        {
            get { return desc2; }
            set { desc2 = value; }
        }

        public decimal Desc3
        {
            get { return desc3; }
            set { desc3 = value; }
        }


        //base de datos
        
        private ConexionBD database;

        public DetallesOrdenDeCompra() {
            database = new ConexionBD();
        }


        // columnas de adminvmov_temp y la columna de la grilla de la orden de donde sale cada una
        private static readonly string[] Columnas = { "mov_item", "mov_codigo", "mov_memo", "mov_undmed", "mov_cant", "mov_costo", "mov_desc", "mov_desc2",
            "mov_desc3", "mov_total", "mov_codcta", "mov_docume", "mov_sufijo", "mov_hora", "mov_fecha", "mov_fechven", "mov_tipdoc", "mov_costoorig",
            "mov_precio", "mov_totalmon", "mov_arancel" };
        private static readonly int[] Celdas = { 0, 1, 2, 3, 4, 17, 6, 7, 8, 18, 10, 11, 12, 13, 14, 15, 16, 5, 17, 18, 19 };
        private static readonly List<int> CeldasNumericas = new List<int> { 4, 5, 6, 7, 8, 17, 18, 19 };

        // Graba las lineas dentro de la transaccion de la orden. Los numeros se pasan con punto decimal sin cambiar lo que se ve en la grilla.
        internal static void insertar(MySqlConnection conexion, MySqlTransaction transaccion, DataGridView grilla)
        {
            List<string> parametros = new List<string>();
            for (int i = 0; i < Columnas.Length; i++)
            {
                parametros.Add("@p" + i);
            }
            string sql = "INSERT INTO adminvmov_temp (" + string.Join(",", Columnas) + ") VALUES (" + string.Join(",", parametros.ToArray()) + ")";
            foreach (DataGridViewRow fila in grilla.Rows)
            {
                if (fila.IsNewRow)
                {
                    continue;
                }
                MySqlCommand c = new MySqlCommand(sql, conexion, transaccion);
                for (int i = 0; i < Columnas.Length; i++)
                {
                    object valor = fila.Cells[Celdas[i]].Value;
                    if (CeldasNumericas.Contains(Celdas[i]))
                    {
                        string numero = Convert.ToString(valor).Trim().Replace(",", ".");
                        valor = numero.Length > 0 ? numero : "0";
                    }
                    c.Parameters.AddWithValue("@p" + i, valor);
                }
                c.ExecuteNonQuery();
            }
        }

        public DataTable detalleOrdenCompra() {

            StrSql = "SELECT * FROM adminvmov_temp WHERE mov_docume='$1' AND mov_tipdoc='30' ORDER BY length(mov_item), mov_item";

            StrSql = StrSql.Replace("$1",this.documento);
            return database.fDataTable(StrSql);
            database.cerrarConexion();
        }

        public DataTable detalleOrdenCompra(string documento)
        {

            StrSql = "SELECT * FROM adminvmov_temp WHERE mov_docume='$1' ORDER BY length(mov_item), mov_item";

            StrSql = StrSql.Replace("$1", documento);
            return database.fDataTable(StrSql);
            //database.cerrarConexion();  //Chain Driver
        }

        public DataTable detalleOrdenCompraArancel(string documento) 
        {

            StrSql = "SELECT mov_codigo,mov_memo,mov_undmed,mov_cant,mov_precio,mov_costoorig,inv_arancel,ara_porcen,ord_valcamb,ord_codigo,pro_nombre,pro_rif,ord_documento,ord_sufijo,ord_moneda FROM adminvmov_temp " +
                "LEFT OUTER JOIN adminv ON mov_codigo=inv_codigo "+
                "LEFT OUTER JOIN admordencompra ON ord_ordencompra=mov_docume "+
                "LEFT OUTER JOIN admproveedor ON ord_codigo=pro_codigo "+
                "LEFT OUTER JOIN admarancel ON inv_arancel=ara_codigo WHERE mov_docume='$1'";

            StrSql = StrSql.Replace("$1", documento);

            return database.fDataTable(StrSql);
            //database.cerrarConexion();
        }

        public DataTable arancelArticuloEspecifico(string articulo){

         StrSql = "SELECT ara_porcen, adminv.inv_descri, ime_undmed FROM adminv LEFT OUTER JOIN admarancel ON inv_arancel = ara_codigo "+
                    "LEFT OUTER JOIN adminvmed ON  inv_codigo= ime_codigo WHERE inv_codigo='$1'";
         
            StrSql = StrSql.Replace("$1", articulo);

         return database.fDataTable(StrSql);

        }

    }
}
