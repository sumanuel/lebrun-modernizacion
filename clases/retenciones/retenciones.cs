using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using lebrun.clasesData;

namespace lebrun.clases.retenciones
{
    class retenciones
    {
        private string StrSql;
        private DataTable tabla;
        private ConexionBD database;
        private string alicuota;
        private string status;
        private DataSet ds;


        public retenciones() {

            database = new ConexionBD();
        
        }

        public DataTable cargarDocumentoSalcli(string codCliente, string numFiscal) {


            //StrSql = "SELECT dcli_tipdoc,dcli_numero,dcli_estado,dcli_fecha,dcli_neto,dcli_subtotal,dcli_mtoiva,admdoccli.dcli_numfis,dcli_saldo,admsalcli.Status FROM admdoccli " +
            //    " LEFT OUTER JOIN admsalcli ON admsalcli.NroDocum= admdoccli.dcli_numero WHERE " +
            //"dcli_codigo='$1'  AND dcli_numero='$2' AND dcli_estado='Activo'";


            StrSql = "SELECT tipoDoc as dcli_tipdoc,nroDocum as dcli_numero,'Activo' as dcli_estado,FechaEmision as dcli_fecha,montoTotal as dcli_neto, (montoTotal-MontoIva)as dcli_subtotal  ,MontoIva as dcli_mtoiva,dcli_numfis,sal_actual as dcli_saldo,Status FROM admsalcli  " +
                        " WHERE cli_codigo='$1' AND dcli_numfis='$2' ";

            StrSql = StrSql.Replace("$1",codCliente);
            StrSql = StrSql.Replace("$2", numFiscal);

            tabla = database.fDataTable(StrSql);
            database.cerrarConexion();
            return tabla;

        }

        public DataTable cargarDocumentoDocCli(string codCliente, string numFiscal)
        {
            StrSql = "SELECT dcli_tipdoc,dcli_numero,dcli_estado,dcli_fecha,dcli_neto,dcli_subtotal,dcli_mtoiva,admdoccli.dcli_numfis,dcli_saldo,admsalcli.Status FROM admdoccli " +
                     " LEFT OUTER JOIN admsalcli ON admsalcli.NroDocum= admdoccli.dcli_numero WHERE " +
                     " admdoccli.dcli_codigo='$1'  AND admdoccli.dcli_numfis='$2'";

            StrSql = StrSql.Replace("$1", codCliente);
            StrSql = StrSql.Replace("$2", numFiscal);

            tabla = database.fDataTable(StrSql);
            database.cerrarConexion();
            return tabla;
        }

        public DataTable buscarSalcli(string codCliente, string numFiscal)
        {
            StrSql = "SELECT rti_clinumero,rti_climonreten,rti_cliporcen FROM admdocclirti WHERE rti_clicodigo='$1' AND rti_clidocnum='$2' AND rti_statusRet!='4'";
            StrSql = StrSql.Replace("$1", codCliente);
            StrSql = StrSql.Replace("$2", numFiscal);

            tabla = database.fDataTable(StrSql);
            database.cerrarConexion();
            return tabla;
        }

        public void actualizarFechas(string fechaRet, string fechaRecibido)
        {
            StrSql = "UPDATE admdocclirti SET rti_clifecha='$1', rti_fechaRecibido='$2' ";
            StrSql = StrSql.Replace("$1", fechaRet);
            StrSql = StrSql.Replace("$2", fechaRecibido);

            database.ejecutarInsert(StrSql);
            database.cerrarConexion();
        }

        public void actualizarSaldo(string saldo, string estatus, string numDocum)
        {
            StrSql = "UPDATE admsalcli SET sal_actual= $1, Status= '$3' WHERE NroDocum = '$2';";
            StrSql = StrSql.Replace("$1", saldo.Replace(",","."));
            StrSql = StrSql.Replace("$2", numDocum);
            StrSql = StrSql.Replace("$3", estatus);

            database.ejecutarInsert(StrSql);
        }

        public void IngresarRti(DataTable tabla) {

            StrSql = null;
            string[] parametros = new string[14];


            StrSql = "INSERT INTO admdocclirti " +
                    "(rti_clicodigo,rti_clicodsuc,rti_clicontab,rti_clidocnum,rti_clitipdoc," +
                    "rti_clinumero,rti_clinumtra,rti_clidoctip,rti_clitiptra,rti_clifecha," +
                    "rti_cliporcen,rti_cliporiva,rti_climonreten,rti_fechaRecibido,rti_fechaSistema) VALUES" +
                    "(@codigo,@sucursal,@cliConta,@numeroDocum,@tipoDoc,@numReten,@numReten," +
                    "@cliDocCli,@TipoTrans,@fecha,@porceReten,@pIva,@montoRetenc,@Recibido,@sistema)";

            parametros[0] = "@codigo";
            parametros[1] = "@sucursal";
            parametros[2] = "@cliConta";
            parametros[3] = "@numeroDocum";
            parametros[4] = "@tipoDoc";
            parametros[5] = "@numReten";
            parametros[6] = "@cliDocCli";
            parametros[7] = "@TipoTrans";
            parametros[8] = "@fecha";
            parametros[9] = "@porceReten";
            parametros[10] = "@pIva";
            parametros[11] = "@montoRetenc";
            parametros[12] = "@Recibido";
            parametros[13] = "@sistema";


            database.insertDataTable(tabla, parametros, StrSql);
            database.cerrarConexion();
        
        }

        public void actualizarDoccli(string MontoReten, string tipoDoc, string numDocum)
        {

            StrSql = "UPDATE admdoccli set dcli_saldo =(dcli_saldo - " + MontoReten + ") " +
                    " WHERE dcli_tipdoc='$2'  AND dcli_numero='$1'";

            StrSql = StrSql.Replace("$1", numDocum);
            StrSql = StrSql.Replace("$2", tipoDoc);

            database.ejecutarInsert(StrSql);
        
        }

        public string obtenerAlicuota(string codCliente, string numDoc, string tipoDoc)
        {

            //StrSql = "SELECT alict_compra FROM admiva " +
            //    "WHERE alict_tipodiva=(SELECT mov_ivatip FROM adminvmov " +
            //    "WHERE mov_docume='$2'" +
            //    "AND mov_codcta='$1'" +
            //    "AND mov_tipdoc='$3'" +
            //    "GROUP BY mov_codcta,mov_tipdoc,mov_docume)" +
            //    "ORDER BY alict_codigo";

            StrSql = "SELECT alict_compra FROM admiva " +
                "WHERE alict_tipodiva='GN'" +
                "ORDER BY alict_codigo";

            StrSql = StrSql.Replace("$1", codCliente);
            StrSql = StrSql.Replace("$2", numDoc);
            StrSql = StrSql.Replace("$3", tipoDoc);

            ds = database.ejecutarQueryDs(StrSql);

            if (ds.Tables[0].Rows.Count != 0)
            {
                this.alicuota = ds.Tables[0].Rows[0][0].ToString();
            }

            return alicuota;
        }

        public string consultarSalcli(string codCliente, string NroDocum)
        {
            StrSql = "SELECT Status FROM admsalcli WHERE cli_codigo='$1' AND NroDocum='$2'";

            StrSql = StrSql.Replace("$1", codCliente);
            StrSql = StrSql.Replace("$2", NroDocum);

            ds = database.ejecutarQueryDs(StrSql);

            if (ds.Tables[0].Rows.Count != 0)
            {
                this.status = ds.Tables[0].Rows[0][0].ToString();
            }
            return status;
        
        }

        public DataTable fechaUltimoCierreLibro()
        {
            StrSql = "SELECT fechaSistema,mes,anio FROM cierrelibroventa ORDER BY fechasistema DESC LIMIT 1";
            tabla = database.fDataTable(StrSql);
            database.cerrarConexion();
            return tabla;


        }




















    }
}
