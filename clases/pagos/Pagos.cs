using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using lebrun.clasesData;
using System.Windows.Forms;

namespace lebrun.clases.compras
{
    class Pagos
    {

        private ConexionBD database;
        private string StrSql;
        private DataTable tabla;
        private bool centinela = false;


        public Pagos()
        {
            database = new ConexionBD();
            tabla = new DataTable();
        }

        public DataTable armarDocumentosPendientes()
        {
            StrSql = "SELECT IdTipoDoc,Descripcion,NroDocum,SufiDocum,pro_nombre,NroDocumAfect," +
                "NroControlSeniat,fechaEmision,  FechaVencimiento,NroCompPago,"+
                "IdProveedor,MontoDocum,SaldoDocum,statusPago,"+
                "idBanco,correlativo  FROM admsalpro "+
                "LEFT OUTER JOIN admproveedor ON admsalpro.IdProveedor=admproveedor.pro_codigo "+
                "WHERE admsalpro.Status=0 AND admsalpro.procesado=0 order by admsalpro.IdProveedor, "+
                "admsalpro.fechaVencimiento ASC,admsalpro.NroDocum,NroDocum,SufiDocum ";

            tabla = database.fDataTable(StrSql);
            database.cerrarConexion();
            return tabla;
        }

        public DataTable buscarDocumPendiente(string campo, string parametro)
        {
                StrSql = "SELECT IdTipoDoc,Descripcion,NroDocum,SufiDocum,pro_nombre,NroDocumAfect," +
                    "NroControlSeniat,fechaEmision,  FechaVencimiento,NroCompPago," +
                    "IdProveedor,MontoDocum,SaldoDocum,statusPago," +
                    "idBanco,correlativo  FROM admsalpro " +
                    "LEFT OUTER JOIN admproveedor ON admsalpro.IdProveedor=admproveedor.pro_codigo " +
                    "WHERE admsalpro.Status=0 AND admsalpro.procesado=0 AND $Field='$Parametro' ";

                StrSql = StrSql.Replace("$Field", campo);
                StrSql = StrSql.Replace("$Parametro", parametro);
            
            tabla = database.fDataTable(StrSql);
            database.cerrarConexion();
            return tabla;
        }

        public DataTable buscarDocumPendiente(string campo, string desde, string hasta)
        {
            StrSql = "SELECT IdTipoDoc,Descripcion,NroDocum,SufiDocum,pro_nombre,NroDocumAfect," +
                "NroControlSeniat,fechaEmision,  FechaVencimiento,NroCompPago," +
                "IdProveedor,MontoDocum,SaldoDocum,statusPago," +
                "idBanco,correlativo  FROM admsalpro " +
                "LEFT OUTER JOIN admproveedor ON admsalpro.IdProveedor=admproveedor.pro_codigo " +
                "WHERE admsalpro.Status=0 AND admsalpro.procesado=0 AND $Field BETWEEN '$desde' AND '$hasta' ";

            StrSql = StrSql.Replace("$Field", campo);
            StrSql = StrSql.Replace("$desde", desde);
            StrSql = StrSql.Replace("$hasta", hasta);

            tabla = database.fDataTable(StrSql);
            database.cerrarConexion();
            return tabla;


        }

        public void actualizarPagosTemp(DataTable tabla, int posicion)
        {
            StrSql = "";
            StrSql = "UPDATE admpagos_temp SET pag_portador='$portador',pag_fechaCarga='$fechaPago', pag_loginUpd='$login',pag_idBanco='$idBanco' " +
            " WHERE pag_idProveedor='$proveedor' AND pag_nroDocum='$numeDocum' AND pag_sufDocum='$sufijo' AND pag_tipoDoc='$tipoDoc' ;";

            StrSql = StrSql.Replace("$portador", tabla.Rows[posicion][0].ToString());
            StrSql = StrSql.Replace("$fechaPago", tabla.Rows[posicion][0].ToString());
            StrSql = StrSql.Replace("$idBanco", tabla.Rows[posicion][0].ToString());
            StrSql = StrSql.Replace("$proveedor", tabla.Rows[posicion][0].ToString());
            StrSql = StrSql.Replace("$numeDocum", tabla.Rows[posicion][0].ToString());
            StrSql = StrSql.Replace("$sufijo", tabla.Rows[posicion][0].ToString());
            StrSql = StrSql.Replace("$tipoDoc", tabla.Rows[posicion][0].ToString());

            database.ejecutarInsert(StrSql);
            database.cerrarConexion();

        }

        public DataTable docExistePagoTemp(DataTable tabla, int posicion)
        {

            StrSql = "";
            StrSql = "SELECT pag_nroDocum as n FROM admpagos_temp WHERE pag_idProveedor='$1' AND  pag_nroDocum='$2' AND pag_sufDocum='$3' AND pag_tipoDoc='$4'";

            StrSql = StrSql.Replace("$1", tabla.Rows[posicion][10].ToString());
            StrSql = StrSql.Replace("$2", tabla.Rows[posicion][2].ToString());
            StrSql = StrSql.Replace("$3", tabla.Rows[posicion][3].ToString());
            StrSql = StrSql.Replace("$4", tabla.Rows[posicion][0].ToString());

            tabla = database.fDataTable(StrSql);
            database.cerrarConexion();
            return tabla;
        }


        public void guardarDocuApagar(DataTable tabla, int posicion)
        {
            StrSql = "";
            StrSql = "INSERT INTO admpagos_temp (pag_idProveedor,pag_nroDocum,pag_sufDocum," +
                "pag_tipoDoc,pag_portador,pag_fechaCarga,pag_montoDocum,pag_idBanco," +
                "pag_login,pag_loginupd,pag_descripcion,pag_fechaVen,pag_correlativo) " +
                " VALUES('$1','$2','$3','$4','$5','$6','$7','$8','$9','$q','$w','$e','$r')";

            StrSql = StrSql.Replace("$1", tabla.Rows[posicion][10].ToString());
            StrSql = StrSql.Replace("$2", tabla.Rows[posicion][2].ToString());
            StrSql = StrSql.Replace("$3", tabla.Rows[posicion][3].ToString());
            StrSql = StrSql.Replace("$4", tabla.Rows[posicion][0].ToString());
            StrSql = StrSql.Replace("$5", tabla.Rows[posicion][4].ToString());
            StrSql = StrSql.Replace("$6", tabla.Rows[posicion][18].ToString());
            StrSql = StrSql.Replace("$7", tabla.Rows[posicion][12].ToString().Replace(",", "."));
            StrSql = StrSql.Replace("$8", tabla.Rows[posicion][15].ToString());
            StrSql = StrSql.Replace("$9", tabla.Rows[posicion][19].ToString());
            StrSql = StrSql.Replace("$q", tabla.Rows[posicion][19].ToString());
            StrSql = StrSql.Replace("$w", tabla.Rows[posicion][1].ToString());
            StrSql = StrSql.Replace("$e", tabla.Rows[posicion][18].ToString());
            StrSql = StrSql.Replace("$r", tabla.Rows[posicion][16].ToString());

            database.ejecutarInsert(StrSql);
            database.cerrarConexion();
        }

        public void actualizarProcesadoSalpro(DataTable tabla, int posicion)
        {
            StrSql = "";
            StrSql = "UPDATE admsalpro SET procesado='1' WHERE " +
            "IdProveedor='$1' AND NroDocum='$2' " +
            "AND SufiDocum='$3' AND IdTipoDoc='$4';";

            StrSql = StrSql.Replace("$1", tabla.Rows[posicion][10].ToString());
            StrSql = StrSql.Replace("$2", tabla.Rows[posicion][2].ToString());
            StrSql = StrSql.Replace("$3", tabla.Rows[posicion][3].ToString());
            StrSql = StrSql.Replace("$4", tabla.Rows[posicion][0].ToString());

            database.ejecutarInsert(StrSql);
            database.cerrarConexion();
        }

        public void actualizarStatusSalpro(DataTable tabla, int posicion)
        {
            StrSql = "";
            StrSql = "UPDATE admsalpro SET status='1' WHERE " +
            "IdProveedor='$1' AND NroDocum='$2' " +
            "AND SufiDocum='$3' AND IdTipoDoc='$4';";

            StrSql = StrSql.Replace("$1", tabla.Rows[posicion][0].ToString());
            StrSql = StrSql.Replace("$2", tabla.Rows[posicion][1].ToString());
            StrSql = StrSql.Replace("$3", tabla.Rows[posicion][2].ToString());
            StrSql = StrSql.Replace("$4", tabla.Rows[posicion][3].ToString());

            database.ejecutarInsert(StrSql);
            database.cerrarConexion();
        }

        public void limpiarPagosTemp() {

            StrSql = "";
            StrSql = "UPDATE admsalpro SET statusPago='' , procesado='', IdBanco='0' WHERE status=0";
            database.ejecutarInsert(StrSql);

            StrSql = "";
            StrSql = "DELETE FROM admpagos_temp";
            database.ejecutarInsert(StrSql);
            database.cerrarConexion();
        }

        public DataTable cargarLbxPago() {

            StrSql = "";
            StrSql = "SELECT pag_idProveedor 'Codigo Proveedor',pag_portador 'Nombre Proveedor' FROM admpagos_temp WHERE pag_status=0 GROUP BY pag_idProveedor ORDER BY pag_idProveedor  ";

            tabla = database.fDataTable(StrSql);
            database.cerrarConexion();
            return tabla;
            
        }

        public DataTable armarDocumentosProcesoPago(string codigoProveedor)
        {

            StrSql = "";
            StrSql = "SELECT pag_fechaVen,pag_descripcion,pag_nroDocum 'Numero Documento',pag_sufDocum 'Sufijo',pag_tipoDoc 'Tipo',pag_montoDocum 'Monto Documento',pag_fechaCarga,pag_correlativo FROM admpagos_temp  WHERE pag_idProveedor='$1' AND pag_status=0 ORDER BY pag_fechaVen ASC,pag_nroDocum,pag_sufDocum ";
            StrSql = StrSql.Replace("$1", codigoProveedor);

            tabla = database.fDataTable(StrSql);
            database.cerrarConexion();
            return tabla;

        }

        public void actualizarProcesoPagoTemp(DataTable tabla)
        {

            StrSql = "";

            for (int i = 0; i < tabla.Rows.Count; i++)
            {

                StrSql = "UPDATE admpagos_temp SET pag_numCheque='$cheque' , pag_status='4', pag_fechaPago='$fechaPago', pag_idBanco='$idBanco', pag_status='4',pag_montoCheque='$montoCheque'," +
                        " pag_numComprobante='$comprobante', pag_cuentaBan='$cuentaBancaria' WHERE pag_idProveedor='$idProveedor' AND pag_nroDocum='$documento' AND pag_sufDocum='$sufijo' AND pag_tipoDoc='$tipoDoc' AND pag_status='0'";


                StrSql = StrSql.Replace("$cheque", "" + tabla.Rows[i][10].ToString());
                StrSql = StrSql.Replace("$fechaPago", "" + tabla.Rows[i][12].ToString());
                StrSql = StrSql.Replace("$idBanco", "" + tabla.Rows[i][8].ToString());
                StrSql = StrSql.Replace("$montoCheque", "" + tabla.Rows[i][7].ToString());
                StrSql = StrSql.Replace("$comprobante", "" + tabla.Rows[i][13].ToString());
                StrSql = StrSql.Replace("$cuentaBancaria", "" + tabla.Rows[i][14].ToString());
                StrSql = StrSql.Replace("$idProveedor", "" + tabla.Rows[i][0].ToString());
                StrSql = StrSql.Replace("$documento", "" + tabla.Rows[i][1].ToString());
                StrSql = StrSql.Replace("$sufijo", "" + tabla.Rows[i][2].ToString());
                StrSql = StrSql.Replace("$tipoDoc", "" + tabla.Rows[i][3].ToString());

                database.ejecutarInsert(StrSql);
            }

            database.cerrarConexion();

        }

        public void insertarProcesoPagoTemp(DataTable tabla, int posicion)
        {

            StrSql = "";
            StrSql = "INSERT INTO admpagos_temp (pag_idProveedor,pag_nroDocum,pag_sufDocum,"+
                "pag_tipoDoc,pag_portador,pag_fechaCarga,pag_montoDocum,pag_idBanco,"+
                "pag_login,pag_loginupd,pag_numCheque,pag_status,pag_fechaPago,"+
                "pag_descripcion,pag_fechaVen,pag_correlativo)VALUES " +
                "('$proveedor','$documento','$sufijo','$tipoDoc','$nombreProveedor','$fechaCarga','$restante','', " +
                " '$login','$login','','$status','$fechaPago','$descripcion','$vencimiento','$correlativo')";

            StrSql = StrSql.Replace("$proveedor", "" + tabla.Rows[posicion][0].ToString());
            StrSql = StrSql.Replace("$documento", "" + tabla.Rows[posicion][1].ToString());
            StrSql = StrSql.Replace("$sufijo", "" + tabla.Rows[posicion][2].ToString());
            StrSql = StrSql.Replace("$tipoDoc", "" + tabla.Rows[posicion][3].ToString());
            StrSql = StrSql.Replace("$nombreProveedor", "" + tabla.Rows[posicion][4].ToString());
            StrSql = StrSql.Replace("$fechaCarga", "" + tabla.Rows[posicion][5].ToString());
            StrSql = StrSql.Replace("$montoDocum", "" + tabla.Rows[posicion][18].ToString().Replace(",","."));
            StrSql = StrSql.Replace("$banco", "" + tabla.Rows[posicion][8].ToString());
            StrSql = StrSql.Replace("$login", "" + tabla.Rows[posicion][9].ToString());
            StrSql = StrSql.Replace("$cheque", "" + tabla.Rows[posicion][10].ToString());
            StrSql = StrSql.Replace("$status", "" + tabla.Rows[posicion][11].ToString());
            StrSql = StrSql.Replace("$fechaPago", "" + tabla.Rows[posicion][12].ToString());
            StrSql = StrSql.Replace("$descripcion", "" + tabla.Rows[posicion][15].ToString());
            StrSql = StrSql.Replace("$vencimiento", "" + tabla.Rows[posicion][16].ToString());
            StrSql = StrSql.Replace("$correlativo", "" + tabla.Rows[posicion][17].ToString());
            StrSql = StrSql.Replace("$restante", "" + tabla.Rows[posicion][18].ToString().Replace(",", "."));

            database.ejecutarInsert(StrSql);
            database.cerrarConexion();
        }

        public void insertarProcesoPago(DataTable tabla)
        {

            string[] parametros = new string[19];

            //StrSql = "";
            //StrSql = "INSERT INTO admpagos (pag_idProveedor,pag_nroDocum,pag_sufDocum,pag_tipoDoc,pag_portador,pag_fechaCarga,pag_montoDocum,pag_idBanco,pag_login,pag_loginupd,pag_numCheque,pag_status,pag_fechaPago,pag_descripcion,pag_fechaVen,pag_correlativo)VALUES " +
            //"(@codigoProveedor,@numeroDocum,@sufijoDoc,@tipoDoc,@NombreProveedor,@fechaCarga,@MontoDocum,@idBanco, " +
            //" @Login,@Login,@numeroCheque,@status,@fechaPago,@descripcion,@vencimiento,@correlativo)";

            StrSql = "";
            StrSql = "INSERT INTO admpagos (pag_idProveedor,pag_nroDocum,pag_sufDocum,pag_tipoDoc,pag_portador,pag_fechaCarga,pag_montoDocum,pag_montoCheque,pag_idBanco,pag_login,pag_loginupd,pag_numCheque,pag_status,pag_fechaPago,pag_descripcion,pag_numComprobante,pag_cuentaBan)VALUES " +
            "(@codigoProveedor,@numeroDocum,@sufijoDoc,@tipoDoc,@NombreProveedor,@fechaCarga,@MontoDocum,@MontoCheque,@idBanco, " +
            " @Login,@Login,@numeroCheque,'4',@fechaPago,@descripcion,@numeroComprobante,@cuentaBancaria)";

            parametros[0] = "@codigoProveedor";
            parametros[1] = "@numeroDocum";
            parametros[2] = "@sufijoDoc";
            parametros[3] = "@tipoDoc";
            parametros[4] = "@NombreProveedor";
            parametros[5] = "@fechaCarga";
            parametros[6] = "@MontoDocum";
            parametros[7] = "@MontoCheque";
            parametros[8] = "@idBanco";
            parametros[9] = "@Login";
            parametros[10] = "@numeroCheque";
            parametros[11] = "@status";
            parametros[12] = "@fechaPago";
            parametros[13] = "@numeroComprobante";
            parametros[14] = "@cuentaBancaria";
            parametros[15] = "@descripcion";
            parametros[16] = "@vencimiento";
            parametros[17] = "@correlativo";
            parametros[18] = "@restante";

            database.insertDataTable(tabla, parametros, StrSql);
            database.cerrarConexion();

        }

        public void actualizarSaldo(DataTable tabla)
        {
            StrSql = "";
            for (int i = 0; i < tabla.Rows.Count; i++)
            {

                StrSql = "UPDATE admsalpro SET SaldoDocum='$montoDocum', status='$status' WHERE " +
                "IdProveedor='$proveedor' AND NroDocum='$documento' " +
                "AND SufiDocum='$sufijo' AND IdTipoDoc='$tipoDoc';";

                StrSql = StrSql.Replace("$proveedor", "" + tabla.Rows[i][0].ToString());
                StrSql = StrSql.Replace("$documento", "" + tabla.Rows[i][1].ToString());
                StrSql = StrSql.Replace("$sufijo", "" + tabla.Rows[i][2].ToString());
                StrSql = StrSql.Replace("$tipoDoc", "" + tabla.Rows[i][3].ToString());
                StrSql = StrSql.Replace("$montoDocum", "" + tabla.Rows[i][18].ToString().Replace(",", "."));
                StrSql = StrSql.Replace("$status", "" + tabla.Rows[i][11].ToString());

                database.ejecutarInsert(StrSql);
            }

            database.cerrarConexion();
        }

        public void actualizarSaldoUnico(DataTable tabla)
        {
            //aqui de debe pasar la tablaPago
            StrSql = "";

            for (int i = 0; i < tabla.Rows.Count; i++)
            {

                StrSql = "UPDATE admsalpro SET SaldoDocum=SaldoDocum-'$montoRebajar' WHERE " +
                "IdProveedor='$proveedor' AND NroDocum='$documento' " +
                "AND SufiDocum='$sufijo' AND IdTipoDoc='$tipoDoc';";

                StrSql = StrSql.Replace("$proveedor", tabla.Rows[i][0].ToString());
                StrSql = StrSql.Replace("$documento", "" + tabla.Rows[i][1].ToString());
                StrSql = StrSql.Replace("$sufijo", "" + tabla.Rows[i][2].ToString());
                StrSql = StrSql.Replace("$tipoDoc", "" + tabla.Rows[i][3].ToString());
                StrSql = StrSql.Replace("$montoRebajar", "" + tabla.Rows[i][6].ToString().Replace(",", "."));

                database.ejecutarInsert(StrSql);
            }

            database.cerrarConexion();
        }

        public DataTable imprimirVoucherPago(string numeroCheque)
        {
            StrSql = "";

            StrSql = "SELECT admcuentasbanc.ctas_contable3,admcuentasbanc.ctas_pagCheque,admbancos.ban_nombre,pag_nroDocum,pag_sufDocum,pag_fechaPago,pag_portador,pag_montoDocum,pag_montoCheque,pag_numComprobante,pag_idBanco,pag_numCheque,pag_cuentaBan,n_cuenta as tdc_codcta,n_auxiliar as tdc_auxiliar " +
            " FROM admpagos_temp " +
            " LEFT JOIN admtipdocpro2 ON admpagos_temp.pag_tipoDoc = admtipdocpro2.n_tipo and  admpagos_temp.pag_idProveedor = admtipdocpro2.n_proveedor " +
            " LEFT JOIN admbancos ON admpagos_temp.pag_idBanco = admbancos.ban_codigo " +
            " LEFT JOIN admcuentasbanc ON pag_cuentaBan=ctas_numero WHERE pag_numCheque='$1'";

            StrSql = StrSql.Replace("$1", numeroCheque);

            tabla = database.fDataTable(StrSql);
            database.cerrarConexion();
            return tabla;
        }

        public DataTable imprimirVoucherPagoManual(string numeroCheque, string comprobante)
        {

            StrSql = "";
            StrSql = "SELECT *,admcuentasbanc.ctas_contable3,admcuentasbanc.ctas_pagCheque,admbancos.ban_nombre,pag_nroDocum,pag_sufDocum,pag_fechaPago,pag_portador,pag_montoDocum,pag_numComprobante,pag_idBanco,pag_numCheque,pag_cuentaBan,n_cuenta as tdc_codcta,sysconta.admcuentascontables.cc_Descripcion  " +
            " FROM admpagos " +
            " LEFT JOIN admtipdocpro2 ON admpagos.pag_tipoDoc = admtipdocpro2.n_tipo and  admpagos.pag_idProveedor = admtipdocpro2.n_proveedor " +
            " LEFT OUTER JOIN admmovcontab ON admpagos.pag_numComprobante = admmovcontab.movcon_numcomp " +
            " LEFT OUTER JOIN sysconta.admcuentascontables ON admmovcontab.movcon_cuenta= sysconta.admcuentascontables.cc_CodigoCuenta " +
            " LEFT JOIN admbancos ON admpagos.pag_idBanco = admbancos.ban_codigo " +
            " LEFT JOIN admcuentasbanc ON pag_cuentaBan=ctas_numero WHERE pag_numCheque='$1' AND movcon_numcomp ='$2' AND movcon_item='1' ORDER BY length(movcon_item), movcon_item; ";

            StrSql = StrSql.Replace("$1", numeroCheque);
            StrSql = StrSql.Replace("$2", comprobante);

            tabla = database.fDataTable(StrSql);
            database.cerrarConexion();
            return tabla;
        }



        public bool consultarExistenciaDoc(string codigoProveedor, string documento, string sufijo, string tipo)
        {

                StrSql = "";
                StrSql = "SELECT NroDocum FROM admsalpro WHERE IdProveedor='$codigo' " +
                " AND NroDocum='$documento' AND SufiDocum='$sufijo' AND IdTipoDoc='$tipo'";

                StrSql = StrSql.Replace("$codigo", codigoProveedor);
                StrSql = StrSql.Replace("$documento", documento);
                StrSql = StrSql.Replace("$sufijo", sufijo);
                StrSql = StrSql.Replace("$tipo", tipo);

                tabla = database.fDataTable(StrSql);

                if (tabla.Rows.Count > 0)
                {
                    centinela = true;
                }
                database.cerrarConexion();

                return centinela;
    
        }

        public void crearDocPro(DataTable tablaPago)
        {
            //aqui pasar tablapago
            for (int i = 0; i < tablaPago.Rows.Count; i++)
            {

                StrSql = "INSERT INTO admdocpro(doc_correlativo, doc_numerodoc,doc_sufijo, doc_sufiOrden, doc_numfis, doc_numcontrol, doc_tipdoc, doc_facafe, doc_sufijofacafe, doc_ordencompra, doc_docasociado, doc_codigo, doc_concep, doc_cencost, doc_sucus, doc_moneda, doc_codtra, doc_condic, doc_usuario, doc_tiptra, doc_origen, doc_estadodoc, doc_status, doc_expimp, doc_fechCarg, doc_fechEmis, doc_fechRecep, doc_fechven, doc_fecReten, doc_fechLCompra, doc_hora, doc_cbtnum, doc_recibidopor, doc_docorg, doc_cbtpre, doc_pagnum, doc_plaimp, doc_aprob1, doc_aprob2, doc_aprob3, doc_anufis, doc_crepago, doc_cxp, doc_plazo, doc_basnet, doc_dcto, doc_otrimp, doc_mtoiva, doc_neto, doc_otros, doc_recargo, doc_valcamb, doc_dctobs, doc_totaldivisa, doc_prod, doc_subtot, doc_ditem, doc_descdoc, doc_descdoc2, doc_descdoc3, doc_subbase, doc_imponible, doc_iva_gn, doc_iva_rd, doc_base_gn, " +
                "doc_base_rd, doc_base_ex,transporte, otrosGastos, dif_Inv) VALUES ('', '$documento', '$sufijo', '', '', '$documento', '$tipoDoc', '', '', '', '', '$proveedor', '$descripcion', '0000000001', '000001', 'Bs', '', '07', '0002', 'D', 'Nacional', 'Activo', '4', '', '$fechaCarga', '$fechaCarga', '$fechaCarga', '$fechaCarga', '$fechaCarga', '$fechaCarga', '$hora', 'CHEM', '', '', '', '', '', '', '', '', '', '', '1', '0', '$montoDocum', 0.00, 0.00, 0.00, '$montoDocum', 0.00, 0.00, 1.00, 0.00, '$montoDocum', 0.00, '$montoDocum', 0.00,  " +
                " 0.00,0.00, 0.00,'$montoDocum', 0.00, 0.00, 0.00, 0.00, 0.00, '$montoDocum', 0.00, 0.00, 0.00);";

                StrSql = StrSql.Replace("$proveedor", tablaPago.Rows[i][0].ToString());
                StrSql = StrSql.Replace("$documento", "" + tablaPago.Rows[i][1].ToString());
                StrSql = StrSql.Replace("$sufijo", "" + tablaPago.Rows[i][2].ToString());
                StrSql = StrSql.Replace("$tipoDoc", "" + tablaPago.Rows[i][3].ToString());
                StrSql = StrSql.Replace("$banco", "" + tablaPago.Rows[i][8].ToString());
                StrSql = StrSql.Replace("$descripcion", "" + tablaPago.Rows[i][4].ToString());
                StrSql = StrSql.Replace("$montoDocum", "" + tablaPago.Rows[i][6].ToString().Replace(",", "."));
                StrSql = StrSql.Replace("$comprobante", "" + tablaPago.Rows[i][13].ToString());
                StrSql = StrSql.Replace("$usuario", "" + tablaPago.Rows[i][9].ToString());
                StrSql = StrSql.Replace("$fechaCarga", "" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString());
                StrSql = StrSql.Replace("$hora", DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second);

                database.ejecutarInsert(StrSql);
            }

            database.cerrarConexion();

        }

        public void insertarSalproPagoManual(DataTable tablaPago)
        {

            //aqui pasar tablapago

            for (int i = 0; i < tablaPago.Rows.Count; i++)
            {
                StrSql = "";
                StrSql = "insert into admsalpro (" +
                "IdProveedor, NroDocum, SufiDocum, NroDocumAfect,NroControlSeniat, Descripcion, " +
                "NroComprobante,FechaEmision, FechaVencimiento, FechaCarga, " +
                "FechaLibCompras, FechaPago,NroCompPago,IdTipoDoc,IdBanco,IdMoneda, " +
                "MontoEnDivisa, MontoDocum, SaldoDocum, MontoIva, " +
                "PorcIva, PorcIva2,MontoRetenc, NroCompRetenc,Status," +
                "Login, Loginact,dif_inv,correlativo)" +
                "VALUES('$proveedor','$documento','$sufijo','0','0'," +
                "'$descripcion','$comprobante','$fecha','$fecha','$fecha    $hora'," +
                "'$fecha','$fecha','$comprobante','$tipoDoc','$banco','Bs','0.00','$montoDocum','$montoDocum','0.00','12','8', " +
                "'0.00','0','0'," +
                "'$usuario','$usuario','0',(SELECT tdc_correlativo FROM admtipdocpro WHERE tdc_codigo='$tipoDoc'));";

                StrSql = StrSql.Replace("$proveedor", tablaPago.Rows[i][0].ToString());
                StrSql = StrSql.Replace("$documento", "" + tablaPago.Rows[i][1].ToString());
                StrSql = StrSql.Replace("$sufijo", "" + tablaPago.Rows[i][2].ToString());
                StrSql = StrSql.Replace("$tipoDoc", "" + tablaPago.Rows[i][3].ToString());
                StrSql = StrSql.Replace("$banco", "" + tablaPago.Rows[i][8].ToString());
                StrSql = StrSql.Replace("$descripcion", "" + tablaPago.Rows[i][4].ToString());
                StrSql = StrSql.Replace("$montoDocum", "" + tablaPago.Rows[i][6].ToString().Replace(",", "."));
                StrSql = StrSql.Replace("$comprobante", "" + tablaPago.Rows[i][13].ToString());
                StrSql = StrSql.Replace("$usuario", "" + tablaPago.Rows[i][9].ToString());
                StrSql = StrSql.Replace("$fecha", "" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString());
                StrSql = StrSql.Replace("$hora", DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second);

                database.ejecutarInsert(StrSql);
            }

            database.cerrarConexion();
        
        }

        public DataTable reimpresionVoucherPago(string comprobante)
        {
            StrSql = "SELECT admcuentasbanc.ctas_contable3,admcuentasbanc.ctas_pagCheque," +
            "admbancos.ban_nombre,pag_nroDocum,pag_sufDocum,pag_fechaPago," +
            "pag_portador,pag_montoDocum,pag_montoCheque,pag_numComprobante," +
            "pag_idBanco,pag_numCheque,pag_cuentaBan,n_cuenta as tdc_codcta," +
            "n_auxiliar as tdc_auxiliar " +
            " FROM admpagos " +
            " LEFT JOIN admtipdocpro2 ON admpagos.pag_tipoDoc = admtipdocpro2.n_tipo and  admpagos.pag_idProveedor = admtipdocpro2.n_proveedor " +
            " LEFT JOIN admbancos ON admpagos.pag_idBanco = admbancos.ban_codigo " +
            " LEFT JOIN admcuentasbanc ON pag_cuentaBan=ctas_numero WHERE pag_numComprobante='$1'";


            StrSql = StrSql.Replace("$1", comprobante);
            tabla = database.fDataTable(StrSql);
            database.cerrarConexion();
            return tabla;
        }

        public DataTable cargarDocCruce(string codProveedor)
        {
            StrSql = "";
            StrSql = "SELECT pro_nombre,Descripcion,NroDocum,SufiDocum,IdTipoDoc," +
                "saldoDocum,FechaCarga,correlativo,FechaVencimiento FROM admsalpro " +
                " INNER JOIN admproveedor ON IdProveedor=pro_codigo WHERE IdProveedor='$1' " +
                " AND status='0'";

            StrSql = StrSql.Replace("$1", codProveedor);
            tabla = database.fDataTable(StrSql);
            database.cerrarConexion();
            return tabla;
        }

        public DataTable reimprimirComprobanteCruce(string numeroComprobante)
        {
            StrSql = "SELECT " +
            "IdProveedor as doc_codigo,NroDocum as doc_numerodoc,sufiDocum as doc_sufijo, NroDocumAfect as doc_facafe,nroControlSeniat as doc_numControl," +
            "descripcion as doc_concep, fechaEmision as doc_fechEmis, fechaVencimiento as doc_fechVen,Status as doc_status,tdc_tipo, " +
            "movcon_item, movcon_cuenta,movcon_auxiliar,movcon_descrip,movcon_numcomp,movcon_tipo,movcon_monto,admproveedor.pro_nombre,admproveedor.pro_rif,cc_Descripcion as Descripcion, movcon_login as doc_usuario " +
            "From " +
            "admsalpro " +
            "left join admtipdocpro on IdTipoDoc=tdc_codigo " +
            "left join  admmovcontab  on IdTipoDoc=movcon_tipdoc and IdProveedor=movcon_proveedor and NroDocum=movcon_numdoc and SufiDocum=movcon_sufdoc " +
            "left join sysconta.admcuentascontables on movcon_cuenta=cc_CodigoCuenta left join admproveedor on IdProveedor=pro_codigo " +
            "Where movcon_numcomp='" + numeroComprobante + "' " +
            "order By " +
            "length(movcon_item), movcon_item; ";

            tabla = database.fDataTable(StrSql);
            database.cerrarConexion();
            return tabla;
        }

        public DataTable cargarCondicionPago()
        {
            StrSql = "";
            StrSql = "select conp_codigo, conp_descripcion, conp_cant_dias, conp_status from admcondpago";
            tabla = database.fDataTable(StrSql);
            database.cerrarConexion();
            return tabla;
        }

        public DataTable codCondicionPago(string codigo)
        {
            StrSql = "";
            StrSql = "select conp_cant_dias from admcondpago WHERE conp_codigo='$1'";
            StrSql = StrSql.Replace("$1", codigo);
            tabla = database.fDataTable(StrSql);
            database.cerrarConexion();
            return tabla;
        }





    }
}
