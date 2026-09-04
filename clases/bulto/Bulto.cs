using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using lebrun.clasesData;
using System.Windows.Forms;
using System.Data;
using lebrun.clases.facturacion;
using lebrun.clases.clientes;
using lebrun.clases.vendedores;

namespace lebrun.clases.bulto
{
    public class Bulto
    {
        string sentenciaSql = null;
        private ConexionBD conexionSisadm;
        public string cantBultos { get; set; }
        public string pesoTotal { get; set; }


        public Bulto()
        {
            conexionSisadm = new ConexionBD();
        }

        public int actualizarCantidadPeso(string bultos, string peso, string numeroPre)
        {
            int respuesta = 0;
            sentenciaSql = null;
            sentenciaSql = "UPDATE admdocclipre SET dcli_girnum='$dcli_girnum', dcli_cbtnum='$dcli_cbtnum' , dcli_estado='2' WHERE " +
                "dcli_numero='$dcli_numero';";
            
            sentenciaSql = sentenciaSql.Replace("$dcli_girnum",bultos);
            sentenciaSql = sentenciaSql.Replace("$dcli_cbtnum",peso);
            sentenciaSql = sentenciaSql.Replace("$dcli_numero",numeroPre);
            respuesta = conexionSisadm.ejecutarInsert2(sentenciaSql);
            conexionSisadm.cerrarConexion();

            return respuesta;
        }

        public void guardarDetalleBulto(DataGridView data,string numeroPre) 
        {   
            sentenciaSql = null;

            for (int i = 0; i < data.Rows.Count; i++)
            {
                sentenciaSql = "INSERT INTO admBulto (" +
                            "item, dcli_num, bulto, codigoProd,peso) " +
                            "VALUES (" +
                            "'@item', '@dcli_num', '$bulto','@dlcli_codigo', '@peso');";

                sentenciaSql = sentenciaSql.Replace("@item", data.Rows[i].Cells["item"].Value.ToString());
                sentenciaSql = sentenciaSql.Replace("@dcli_num", numeroPre);
                sentenciaSql = sentenciaSql.Replace("$bulto", data.Rows[i].Cells["bulto"].Value.ToString());
                sentenciaSql = sentenciaSql.Replace("@dlcli_codigo", data.Rows[i].Cells["codigoProd"].Value.ToString());
                sentenciaSql = sentenciaSql.Replace("@peso", data.Rows[i].Cells["peso"].Value.ToString());
                conexionSisadm.ejecutarInsert2(sentenciaSql);
            }
            conexionSisadm.cerrarConexion();
        }

        public void borrarDetalleBulto(string numeroPre)
        {
            sentenciaSql = null;
            sentenciaSql = "DELETE FROM admBulto WHERE dcli_num='$dcli_num';";
            sentenciaSql = sentenciaSql.Replace("$dcli_num", numeroPre);
            conexionSisadm.ejecutarInsert2(sentenciaSql);
            conexionSisadm.cerrarConexion();
        }

        public bool existeBulto(string numeroPre)
        {
            bool centinela = false;
            DataTable dt = null;
            sentenciaSql = null;
            sentenciaSql = "SELECT item FROM admbulto WHERE dcli_num='$dcli_num' LIMIT 1;";
            sentenciaSql = sentenciaSql.Replace("$dcli_num", numeroPre);
            dt = conexionSisadm.fDataTable(sentenciaSql);
            if (dt.Rows.Count > 0)
            {
                centinela = true;
            }
            conexionSisadm.cerrarConexion();
            return centinela;
        }

        public DataTable detalleBulto(string numeroPre)
        {
            DataTable dt = null;
            sentenciaSql = null;
            sentenciaSql = "SELECT item, dcli_num,bulto, codigoProd, peso, inv_descri FROM admbulto " +
                "LEFT JOIN  adminv ON adminv.inv_codigo = admbulto.codigoProd " +
                "WHERE dcli_num='$dcli_num';";
            sentenciaSql = sentenciaSql.Replace("$dcli_num", numeroPre);
            dt = conexionSisadm.fDataTable(sentenciaSql);

            conexionSisadm.cerrarConexion();
            return dt;
        }

        public DataSet datasetReporteBulto(Factura preFact, Clientes cli1, 
            string compNombre, string compRif, Bulto bult1)
        {
            DataSet dts = new DataSet();
            DataTable dt1 = new DataTable("DataTable1");
            DataTable dt2 = new DataTable("DataTable2");
            DataTable dt3 = new DataTable("DataTable3");
            DataTable dt4 = new DataTable("DataTable4");
            DataTable detallesB = new DataTable();
            Transporte transp = new Transporte();

            dt1.Columns.Add("cliente.nombre");
            dt1.Columns.Add("cliente.codigo");
            dt1.Columns.Add("cliente.rif");
            dt1.Columns.Add("cliente.telefono");
            dt1.Columns.Add("cliente.zonaPostal");
            dt1.Columns.Add("cliente.DirFiscal");
            dt1.Columns.Add("cliente.DirEnvio");
            dt1.Columns.Add("cliente.Transporte");

            dt2.Columns.Add("pedido.Correlativo");
            dt2.Columns.Add("pedido.totalBultos");

            dt3.Columns.Add("bultoNumero");
            dt3.Columns.Add("bultoPeso");

            dt4.Columns.Add("compan.Nombre");
            dt4.Columns.Add("compan.Rif");

            DataRow filaDt1 = dt1.NewRow();
            filaDt1["cliente.nombre"] = cli1.Nombre;
            filaDt1["cliente.codigo"] = cli1.Codigo;
            filaDt1["cliente.rif"] = cli1.Rif;
            filaDt1["cliente.telefono"] = cli1.Telefono;
            filaDt1["cliente.zonaPostal"] = cli1.ZonaPostal;
            filaDt1["cliente.DirFiscal"] = cli1.Direccion;
            filaDt1["cliente.DirEnvio"] = preFact.DireccionEnvio;
            filaDt1["cliente.Transporte"] = transp.nombreTransporte(cli1.Transporte);
            dt1.Rows.Add(filaDt1);

            DataRow filaDt2 = dt2.NewRow();
            filaDt2["pedido.Correlativo"] = preFact.CorrelativoInterno;
            filaDt2["pedido.totalBultos"] = preFact.bultos;
            dt2.Rows.Add(filaDt2);

            detallesB = bult1.detalleBulto(preFact.CorrelativoInterno);
            foreach (DataRow filaDetalle in detallesB.Rows)
            {
                DataRow filaDt3 = dt3.NewRow();
                filaDt3["bultoNumero"] = filaDetalle["bulto"].ToString();
                filaDt3["bultoPeso"] = filaDetalle["peso"].ToString();
                dt3.Rows.Add(filaDt3);
            }

            DataRow filaDt4 = dt4.NewRow();
            filaDt4["compan.Nombre"] = compNombre;
            filaDt4["compan.Rif"] = compRif;
            dt4.Rows.Add(filaDt4);

            dts.Tables.Add(dt1);
            dts.Tables.Add(dt2);
            dts.Tables.Add(dt3);
            dts.Tables.Add(dt4);
            return dts;
        }

        public int updateStatus(string status, string codigo)
        {
            int respuesta = 0;
            sentenciaSql = null;
            sentenciaSql = "UPDATE admdocclipre SET dcli_estado='$dcli_estado' WHERE dcli_numero='$dcli_numero';";
            sentenciaSql = sentenciaSql.Replace("$dcli_estado", status);
            sentenciaSql = sentenciaSql.Replace("$dcli_numero", codigo);
            respuesta = conexionSisadm.ejecutarInsert2(sentenciaSql);
            conexionSisadm.cerrarConexion();
            return respuesta;
        }

    }
}
