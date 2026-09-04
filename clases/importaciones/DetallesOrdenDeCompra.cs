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


        public void guardarDetallesOrdenDeCompra(DataGridView grid) {
           
            StrSql = null;
            string[] parametros = new string[20];

            StrSql = "INSERT INTO adminvmov_temp (mov_item,mov_codigo,mov_memo,mov_undmed,mov_cant,mov_costo, " +
                    "mov_desc,mov_desc2,mov_desc3,mov_total,mov_codcta,mov_docume,mov_sufijo, " +
                    "mov_hora,mov_fecha,mov_fechven,mov_tipdoc," +
                    "mov_costoorig,mov_precio,mov_totalmon,mov_arancel) " +
                    "VALUES " +
                    "(@Items,@Codigo,@Producto,@Unidad,@Cant,@CostoDes,@Des1, " +
                    "@Des2,@Des3,@TotalDes,@Proveedor,@Documento,@Sufijo, " +
                    "@Hora,@FechaCarga,@FechaRecep,@TipoDoc,@Costo,@CostoDes,@TotalDes,@PorcArancel);";


            parametros[0] = "@Items";
            parametros[1] = "@Codigo";
            parametros[2] = "@Producto";
            parametros[3] = "@Unidad";
            parametros[4] = "@Cant";
            parametros[5] = "@Costo";
            parametros[6] = "@Des1";
            parametros[7] = "@Des2";
            parametros[8] = "@Des3";
            parametros[9] = "@Total";
            parametros[10] = "@Proveedor";
            parametros[11] = "@Documento";
            parametros[12] = "@Sufijo";
            parametros[13] = "@hora";
            parametros[14] = "@FechaCarga";
            parametros[15] = "@FechaRecep";
            parametros[16] = "@TipoDoc";
            parametros[17] = "@CostoDes";
            parametros[18] = "@TotalDes";
            parametros[19] = "@PorcArancel";
            database.insertdataGridView(grid, parametros, StrSql);
            database.cerrarConexion();

        }

        public DataTable detalleOrdenCompra() {

            StrSql = "SELECT * FROM adminvmov_temp WHERE mov_docume='$1' ORDER BY length(mov_item), mov_item";

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

        public void eliminarDetallesOrdenCompra()
        {
            StrSql = "DELETE FROM adminvmov_temp WHERE mov_docume='$1';";
            StrSql = StrSql.Replace("$1", this.documento);
            database.ejecutarInsert(StrSql);
            database.cerrarConexion();

        }

        public DataTable arancelArticuloEspecifico(string articulo){

         StrSql = "SELECT ara_porcen, adminv.inv_descri, ime_undmed FROM adminv LEFT OUTER JOIN admarancel ON inv_arancel = ara_codigo "+
                    "LEFT OUTER JOIN adminvmed ON  inv_codigo= ime_codigo WHERE inv_codigo='$1'";
         
            StrSql = StrSql.Replace("$1", articulo);

         return database.fDataTable(StrSql);

        }

    }
}
