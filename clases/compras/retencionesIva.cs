using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using lebrun.clasesData;
using lebrun.clases.contabilidad;

namespace lebrun.clases.compras
{
    class retencionesIva
    {
        private ConexionBD database;
        private movimientoContable movCon;
        private Compania compania;
        private string StrSql;
        private DataTable tabla;
        private string correlativo;
        private string tdc_reporte;
        private string usuario;
        private DataTable tablaMovContab;
        private string nombre;
        private string rif;
        private DataTable tablaReporte;


        private string nroDoc;
        private string sufDoc;
        private string tipoDoc;
        private string proveedor;

        private string ivaRd;
        private string ivaGn;
        private string porcIva;
        private string porcIva2;
        private string varNroRetenc;
        private string periodoFiscal;
        private string montoRetenc = "0";
        private string Reten2 = "0";
        private string statusRetenc;
        private string porcRetenc;
        private string comprobante;
        private string descripcion;
        private string correlativoCompro;
        private int items;
        private string cuentaPro;
        private string auxiliarPro;
        private int numero;
        private string letra;
        private string totalRetencion = "0";
        


        public string NroDoc
        {
            get { return nroDoc; }
            set { nroDoc = value; }
        }

        public string SufDoc
        {
            get { return sufDoc; }
            set { sufDoc = value; }
        }

        public string TipoDoc
        {
            get { return tipoDoc; }
            set { tipoDoc = value; }
        }

        public string Proveedor
        {
            get { return proveedor; }
            set { proveedor = value; }
        }

        public string Usuario
        {
            get { return usuario; }
            set { usuario = value; }
        }

        public retencionesIva()
        {
            database = new ConexionBD();
            tabla = new DataTable();
            tablaMovContab = new DataTable();
            movCon = new movimientoContable();
            compania = new Compania();
            compania.obtenerCodigoCompaniaActual();
            tablaReporte = new DataTable();
        }

        public bool verificarRetencion()
        {
            StrSql = "select *  from admlibrocompras " +
                "where (doc_iva_gn> 0 or doc_iva_rd> 0) and PorcRetenc>0 and StatusRetenc=0 " +
                "and IdProveedor='$1' " +
                "and NroDocum='$2' " +
                "and SufiDocum='$3' " +
                "and IdTipoDoc='$4';";

            StrSql = StrSql.Replace("$1", this.Proveedor);
            StrSql = StrSql.Replace("$2", this.NroDoc);
            StrSql = StrSql.Replace("$3", this.SufDoc);
            StrSql = StrSql.Replace("$4", this.TipoDoc);

            tabla = database.fDataTable(StrSql);
            database.cerrarConexion();

            if (tabla.Rows.Count > 0)
            {
                this.ivaGn = tabla.Rows[0]["doc_iva_gn"].ToString();
                this.ivaRd = tabla.Rows[0]["doc_iva_rd"].ToString();
                this.porcIva = tabla.Rows[0]["PorcIva"].ToString();
                this.porcIva2 = tabla.Rows[0]["PorcIva2"].ToString();
                this.porcRetenc = tabla.Rows[0]["PorcRetenc"].ToString();

                return true;
            }

            return false;
        }


        public void actulizarRetLibroCompras()
        {
            //consultarCuenta();
            //calculoIva();
            //datosComprobante();
            //consultarCuentaPro();

            StrSql = "UPDATE admlibrocompras SET " +
                "FechaRetenc='$varFechaRetenc', " +
                "MontoRetenc='$varMontoRetenc', " +
                "MontoRetenc2='$MontoRetenc2'," +
                "NroRetenc='$varNroRetenc', " +
                "periodoFiscal='$varPeriodoFiscal', " +
                "StatusRetenc='$varStatusRetenc', " +
                "FechaRetencEnt='$varFechaRetenc'" +
                "WHERE " +
                "IdProveedor='$proveedor' " +
                "AND NroDocum='$documento' " +
                "AND SufiDocum='$sufijo'" +
                "AND IdTipoDoc='$tipoDoc';";


            StrSql = StrSql.Replace("$varFechaRetenc", "" + DateTime.Now.Year + "-" + DateTime.Now.Month.ToString().PadLeft(2, '0') + "-" + DateTime.Now.Day);
            StrSql = StrSql.Replace("$varMontoRetenc", montoRetenc.Replace(",", "."));
            StrSql = StrSql.Replace("$MontoRetenc2", Reten2.Replace(",", "."));
            StrSql = StrSql.Replace("$varNroRetenc", "" + DateTime.Now.Year + "" + DateTime.Now.Month.ToString().PadLeft(2, '0') + correlativo);
            StrSql = StrSql.Replace("$varPeriodoFiscal", "" + DateTime.Now.Year + "" + DateTime.Now.Month.ToString().PadLeft(2, '0'));
            StrSql = StrSql.Replace("$varStatusRetenc", "2");

            StrSql = StrSql.Replace("$proveedor", proveedor);
            StrSql = StrSql.Replace("$documento", nroDoc);
            StrSql = StrSql.Replace("$sufijo", sufDoc);
            StrSql = StrSql.Replace("$tipoDoc", tipoDoc);

            database.ejecutarInsert(StrSql);
            database.cerrarConexion();
            
        }

        public void consultarCuenta()
        {

            StrSql = "select tdc_correlativo,tdc_reporte from admtipdocpro where tdc_codigo='41';";

            tabla = database.fDataTable(StrSql);

            if (tabla.Rows.Count > 0)
            {
                this.correlativo = tabla.Rows[0]["tdc_correlativo"].ToString();
                this.tdc_reporte = tabla.Rows[0]["tdc_reporte"].ToString();

                database.ejecutarInsert("UPDATE admtipdocpro SET tdc_correlativo=LPAD((tdc_correlativo+1),'8','0') WHERE tdc_codigo='41'");
            }

            database.cerrarConexion();
        }

        public void calculoIva()
        {

            if (Convert.ToDecimal(ivaGn) > 0)
            {
                montoRetenc = "" + ((Convert.ToDecimal(ivaGn) * Convert.ToDecimal(porcRetenc)) / 100);
            }

            if (Convert.ToDecimal(ivaRd) > 0)
            {
                Reten2 = "" + ((Convert.ToDecimal(ivaRd) * Convert.ToDecimal(porcRetenc)) / 100);
            }

            totalRetencion = "" + (Convert.ToDecimal(Reten2) + Convert.ToDecimal(montoRetenc));
        }

        public void datosComprobante()
        {
            StrSql = " SELECT movcon_descrip,movcon_numcomp,tdc_correlativo,movcon_nombre,movcon_rif,movcon_monto FROM admmovcontab " +
            "LEFT JOIN admtipdocpro ON movcon_tipdoc=tdc_codigo " +
            "WHERE movcon_tipdoc='$tipoDoc' AND movcon_sufdoc ='$sufijo' " +
            " AND movcon_proveedor='$proveedor' AND movcon_numdoc='$documento' ORDER BY movcon_item DESC";

            StrSql = StrSql.Replace("$proveedor", proveedor);
            StrSql = StrSql.Replace("$documento", nroDoc);
            StrSql = StrSql.Replace("$sufijo", sufDoc);
            StrSql = StrSql.Replace("$tipoDoc", tipoDoc);

            tabla.Clear();
            tabla = database.fDataTable(StrSql);
            database.cerrarConexion();

            if (tabla.Rows.Count > 0)
            {
                this.correlativoCompro = tabla.Rows[0]["tdc_correlativo"].ToString();
                this.descripcion = tabla.Rows[0]["movcon_descrip"].ToString();
                this.comprobante = tabla.Rows[0]["movcon_numcomp"].ToString();
                this.nombre = tabla.Rows[0]["movcon_nombre"].ToString();
                this.rif = tabla.Rows[0]["movcon_rif"].ToString();
                this.items = Convert.ToInt16(tabla.Rows.Count);

                var credito = tabla.AsEnumerable().Select(r => Convert.ToDecimal(r.Field<decimal>("movcon_monto"))).Sum();
                var debito = tabla.AsEnumerable().Select(r => Convert.ToDecimal(r.Field<decimal>("movcon_monto"))).Sum();
            }
        }

        public void consultarCuentaPro()
        {

            tabla = movCon.cuentaTipDocPro2(this.tipoDoc, this.proveedor);

            if (tabla.Rows.Count > 0)
            {
                this.cuentaPro = tabla.Rows[0]["n_cuenta"].ToString();
                this.auxiliarPro = tabla.Rows[0]["n_auxiliar"].ToString();
            }

        }

        public DataTable armarDetallesMovContab()
        {

            tablaMovContab = movCon.armarDataTableMovContab();

            if (Convert.ToDecimal(montoRetenc) > 0)
            {

                this.items = this.items + 1;
                DataRow fila = tablaMovContab.NewRow();

                if (correlativoCompro == "43")
                {
                    numero = -1;
                    letra = "C";
                }

                if (correlativoCompro == "37" || correlativoCompro == "40")
                {
                    numero = 1;
                    letra = "D";
                }

                fila[0] = items;
                fila[1] = proveedor;
                fila[2] = nroDoc; //recibo
                fila[3] = sufDoc;
                fila[4] = tipoDoc;
                fila[5] = "" + this.comprobante;          //COMPROBANTE
                fila[6] = this.descripcion;  //descripcion
                fila[7] = this.cuentaPro; //mayor proveedor                              //cc.CuentaProveedor;//CODIGO CUENTA
                fila[8] = numero; //-1 o 1
                fila[9] = "";                                   //AQUI NO VA UN COÑO
                fila[10] = "" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString().PadLeft(2, '0') + "-" + DateTime.Now.Day.ToString().PadLeft(2, '0');
                fila[11] = "" + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString().PadLeft(2, '0');//me da lala esta parte copy and paste                                          //hora
                fila[12] = montoRetenc.Replace(",", "."); ;//monto
                fila[13] = "1";
                fila[14] = usuario;
                fila[15] = "RTI";
                fila[16] = "" + auxiliarPro;     //auxiliar                         //AUXILIAR  PROVEEDOR                                 
                fila[17] = this.proveedor;                  //CODIGO
                fila[18] = this.nombre;               //NOMBRE                      
                fila[19] = letra;//letra D o C
                fila[20] = "";
                fila[21] = ""; // interno
                fila[22] = ""; // interno

                tablaMovContab.Rows.Add(fila);

                this.items = this.items + 1;
                DataRow fila2 = tablaMovContab.NewRow();

                if (correlativoCompro == "43")
                {
                    numero = 1;
                    letra = "D";
                }

                if (correlativoCompro == "37" || correlativoCompro == "40")
                {
                    numero = -1;
                    letra = "C";
                }

                fila2[0] = items;
                fila2[1] = proveedor;
                fila2[2] = nroDoc; //recibo
                fila2[3] = sufDoc;
                fila2[4] = tipoDoc;
                fila2[5] = "" + this.comprobante;          //COMPROBANTE
                fila2[6] = this.descripcion;  //descripcion
                fila2[7] = tdc_reporte; //mayor proveedor                              //cc.CuentaProveedor;//CODIGO CUENTA
                fila2[8] = numero; //-1 o 1
                fila2[9] = "";                                   //AQUI NO VA UN COÑO
                fila2[10] = "" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString().PadLeft(2, '0') + "-" + DateTime.Now.Day.ToString().PadLeft(2, '0');
                fila2[11] = "" + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString().PadLeft(2, '0');//me da lala esta parte copy and paste                                          //hora
                fila2[12] = montoRetenc.Replace(",", "."); ;//monto
                fila2[13] = "1";
                fila2[14] = usuario;
                fila2[15] = "RTI";
                fila2[16] = "" + auxiliarPro;     //auxiliar                         //AUXILIAR  PROVEEDOR                                 
                fila2[17] = this.proveedor;                  //CODIGO
                fila2[18] = this.nombre;               //NOMBRE                      
                fila2[19] = letra;//letra D o C
                fila2[20] = "";
                fila2[21] = ""; // interno
                fila2[22] = ""; // interno

                tablaMovContab.Rows.Add(fila2);
            }

            //EN CASO DE MONTO 2 MAYOR A 0

            if (Convert.ToDecimal(Reten2) > 0)
            {

                this.items = this.items + 1;
                DataRow fila = tablaMovContab.NewRow();

                if (correlativoCompro == "43")
                {
                    numero = -1;
                    letra = "C";
                }

                if (correlativoCompro == "37" || correlativoCompro == "40")
                {
                    numero = 1;
                    letra = "D";
                }

                fila[0] = items;
                fila[1] = proveedor;
                fila[2] = nroDoc; //recibo
                fila[3] = sufDoc;
                fila[4] = tipoDoc;
                fila[5] = "" + this.comprobante;          //COMPROBANTE
                fila[6] = this.descripcion;  //descripcion
                fila[7] = this.cuentaPro; //mayor proveedor                              //cc.CuentaProveedor;//CODIGO CUENTA
                fila[8] = numero; //-1 o 1
                fila[9] = "";                                   //AQUI NO VA UN COÑO
                fila[10] = "" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString().PadLeft(2, '0') + "-" + DateTime.Now.Day.ToString().PadLeft(2, '0');
                fila[11] = "" + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString().PadLeft(2, '0');//me da lala esta parte copy and paste                                          //hora
                fila[12] = Reten2.Replace(",", "."); ;//monto
                fila[13] = "1";
                fila[14] = usuario;
                fila[15] = "RTI";
                fila[16] = "" + auxiliarPro;     //auxiliar                         //AUXILIAR  PROVEEDOR                                 
                fila[17] = this.proveedor;                  //CODIGO
                fila[18] = this.nombre;                //NOMBRE                      
                fila[19] = letra;//letra D o C
                fila[20] = "";
                fila[21] = ""; // interno
                fila[22] = ""; // interno

                tablaMovContab.Rows.Add(fila);

                this.items = this.items + 1;
                DataRow fila2 = tablaMovContab.NewRow();

                if (correlativoCompro == "43")
                {
                    numero = 1;
                    letra = "D";
                }

                if (correlativoCompro == "37" || correlativoCompro == "40")
                {
                    numero = -1;
                    letra = "C";
                }

                fila2[0] = items;
                fila2[1] = proveedor;
                fila2[2] = nroDoc; //recibo
                fila2[3] = sufDoc;
                fila2[4] = tipoDoc;
                fila2[5] = "" + this.comprobante;          //COMPROBANTE
                fila2[6] = this.descripcion;  //descripcion
                fila2[7] = tdc_reporte; //mayor proveedor                              //cc.CuentaProveedor;//CODIGO CUENTA
                fila2[8] = numero; //-1 o 1
                fila2[9] = "";                                   //AQUI NO VA UN COÑO
                fila2[10] = "" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString().PadLeft(2, '0') + "-" + DateTime.Now.Day.ToString().PadLeft(2, '0');
                fila2[11] = "" + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString().PadLeft(2, '0');//me da lala esta parte copy and paste                                          //hora
                fila2[12] = Reten2.Replace(",", "."); ;//monto
                fila2[13] = "1";
                fila2[14] = usuario;
                fila2[15] = "RTI";
                fila2[16] = "" + auxiliarPro;     //auxiliar                         //AUXILIAR  PROVEEDOR                                 
                fila2[17] = this.proveedor;                  //CODIGO
                fila2[18] = this.nombre;               //NOMBRE                      
                fila2[19] = letra;//letra D o C
                fila2[20] = "";
                fila2[21] = ""; // interno
                fila2[22] = ""; // interno

                tablaMovContab.Rows.Add(fila2);
            }

            return tablaMovContab;

        }

        public void ingresarMovContab()
        {
            movCon.ingresarMovContab(tablaMovContab);
            movCon.detallesSyscontab(tablaMovContab, compania.BaseDatosActual);
            actualizarCabeceraComprobante();

        }

        public void actualizarCabeceraComprobante()
        {
            database.conectionStringSysconta(compania.BaseDatosActual);

            StrSql = "UPDATE admmovimientoscontablesc SET mc_MontoDebitos=mc_MontoDebitos +'$totalReten', " +
            "mc_MontoCreditos=mc_MontoCreditos + '$totalReten'   WHERE mc_nroComprobante='$comprobante'";

            StrSql = StrSql.Replace("$totalReten", this.totalRetencion.Replace(",", "."));
            StrSql = StrSql.Replace("$comprobante", this.comprobante);

            database.ejecutarInsert(StrSql);
            database.modificarConexionString(1);
            database.cerrarConexion();
        }

        public bool habilitarBotonRetencion()
        {

            StrSql = "SELECT * From admlibrocompras " +
                "LEFT OUTER JOIN admtipdocpro on admlibrocompras.IdTipoDoc=admtipdocpro.tdc_codigo " +
                "LEFT OUTER JOIN " +
                "admproveedor ON admproveedor.pro_codigo = admlibrocompras.IdProveedor " +
                "Where " +
                "IdTipoDoc='$tipoDoc' and " +
                "IdProveedor='$proveedor' and " +
                "NroDocum='$documento' and " +
                "SufiDocum='$sufijo';";

            StrSql = StrSql.Replace("$proveedor", this.Proveedor);
            StrSql = StrSql.Replace("$documento", this.NroDoc);
            StrSql = StrSql.Replace("$sufijo", this.SufDoc);
            StrSql = StrSql.Replace("$tipoDoc", this.TipoDoc);

            tabla = database.fDataTable(StrSql);

            if (tabla.Rows.Count > 0)
            {
                if (tabla.Rows[0]["statusRetenc"].ToString() == "2" || tabla.Rows[0]["statusRetenc"].ToString() == "4")
                {
                    return false;
                }
                if (tabla.Rows[0]["tdc_ret"].ToString() == "0")
                {
                    return false;
                }

                if (tabla.Rows[0]["PorcRetenc"].ToString() == "0")
                {
                    return false;
                }
                if (tabla.Rows[0]["pro_contribuyen"].ToString() == "No")
                {
                    return false;
                }
                if (Convert.ToDecimal(tabla.Rows[0]["doc_base_gn"]) == 0 && Convert.ToDecimal(tabla.Rows[0]["doc_base_rd"]) == 0)
                {
                    return false;
                }

            }
            else
            {
                return false;
            }

            return true;
            
        }

        public void actualizarSalpro()
        {

            if (correlativoCompro == "43")
            {
                totalRetencion = "" + (Convert.ToDecimal(totalRetencion) * -1);
            }

            StrSql = "UPDATE admsalpro SET " +
                "SaldoDocum=SaldoDocum-($totalRetenc) ," +
                "FechaRetenc='$varFechaRetenc', " +
                "NroRetenc='$varNroRetenc', " +
                "MontoRetenc='$varMontoRetenc', " +
                "MontoRetenc2='$MontoRetenc2' " +
                "WHERE " +
                "IdProveedor='$proveedor' " +
                "AND NroDocum='$documento' " +
                "AND SufiDocum='$sufijo'" +
                "AND IdTipoDoc='$tipoDoc';";


            StrSql = StrSql.Replace("$totalRetenc", totalRetencion.Replace(",", "."));
            StrSql = StrSql.Replace("$proveedor", this.Proveedor);
            StrSql = StrSql.Replace("$documento", this.NroDoc);
            StrSql = StrSql.Replace("$sufijo", this.SufDoc);
            StrSql = StrSql.Replace("$tipoDoc", this.TipoDoc);

            StrSql = StrSql.Replace("$varFechaRetenc", "" + DateTime.Now.Year + "-" + DateTime.Now.Month.ToString().PadLeft(2, '0') + "-" + DateTime.Now.Day);
            StrSql = StrSql.Replace("$varMontoRetenc", montoRetenc.Replace(",", "."));
            StrSql = StrSql.Replace("$MontoRetenc2", Reten2.Replace(",", "."));
            StrSql = StrSql.Replace("$varNroRetenc", "" + DateTime.Now.Year + "" + DateTime.Now.Month.ToString().PadLeft(2, '0') + correlativo);

            database.ejecutarInsert(StrSql);
            database.cerrarConexion();
        }

        public DataTable imprimirRetencionIva(string pro, string doc, string suf, string tip)
        {

            StrSql = "SELECT NroFiscal,FechaRetenc, PorcIva,PorcIva2, doc_base_rd, doc_base_ex, doc_base_gn, " +
                    "FechaRetenc, doc_iva_rd, MontoRetenc2, NroDocumAfec, FechaEmision,NroControlSeniat, " +
                    "IF(correlativo='37',NroDocum,'') AS factura, " +
                    "IF(correlativo='37',NroControlSeniat,'') AS controlFactura, " +
                    "IF(correlativo='43',NroDocum,'') AS notadebito, " +
                    "IF(correlativo='43',NroControlSeniat,'') AS controlNotadebito, " +
                    "IF(correlativo='40',NroDocum,'') AS ndebito, " +
                    "MontoDocum, MontoBase, doc_iva_gn, MONTOIVA, MontoRetenc, " +
                    "NroRetenc, pro_nombre, pro_rif, IdProveedor,pro_direcc1,periodoFiscal " +
                    "FROM admlibrocompras " +
                    "LEFT OUTER " +
                    "JOIN admProveedor ON admlibrocompras.IdProveedor = admproveedor.pro_codigo " +
                    "WHERE IdTipoDoc='$tipoDoc' AND IdProveedor='$proveedor' AND " +
                    "NroDocum='$documento' AND SufiDocum='$sufijo' AND StatusRetenc ='$Status';";

            StrSql = StrSql.Replace("$proveedor", pro);
            StrSql = StrSql.Replace("$documento", doc);
            StrSql = StrSql.Replace("$sufijo", suf);
            StrSql = StrSql.Replace("$tipoDoc", tip);
            StrSql = StrSql.Replace("$Status", "2");

            tablaReporte = database.fDataTable(StrSql);
            database.cerrarConexion();
            return tablaReporte;



        }

        public DataTable imprimirRetencionIva2(string pro, string doc, string suf, string tip)
        {

            //StrSql = " SELECT NroFiscal,FechaRetenc 'FechaRetenc', PorcIva, doc_base_gn 'doc_base', doc_base_ex 'doc_base_ex', " +
            //             "NroDocumAfec, FechaEmision 'FechaEmision',NroControlSeniat 'NroControlSeniat',  " +
            //             "IF(correlativo='37',NroDocum,'') AS factura,  " +
            //             "IF(correlativo='37',NroControlSeniat,'') AS controlFactura,  " +
            //             "IF(correlativo='43',NroDocum,'') AS notadebito,  " +
            //             "IF(correlativo='43',NroControlSeniat,'') AS controlNotadebito,  " +
            //             "IF(correlativo='40',NroDocum,'') AS ndebito,  " +
            //             "MontoDocum, MontoBase, doc_iva_gn 'doc_iva', MONTOIVA, MontoRetenc,  " +
            //             "NroRetenc, pro_nombre, pro_rif, IdProveedor,pro_direcc1  " +
            //             "FROM admlibrocompras  " +
            //             "LEFT OUTER  " +
            //             "JOIN admProveedor ON admlibrocompras.IdProveedor = admproveedor.pro_codigo  " +
            //             "WHERE NroRetenc = '$varNroRetenc' AND StatusRetenc ='2' AND doc_base_gn>0 UNION ALL (  " +
            //             "SELECT NroFiscal,ifnull(FechaRetenc=null,'')'FechaRetenc', PorcIva2 'PorcIva', doc_base_rd 'doc_base', ifnull(doc_base_ex=null,'')'doc_base_ex',   " +
            //             "NroDocumAfec, ifnull(FechaEmision=null,'')'FechaEmision',ifnull(NroControlSeniat=null,'') 'NroControlSeniat',  " +
            //             "IF(correlativo='37',NroDocum,'') AS factura,  " +
            //             "IF(correlativo='37',NroControlSeniat,'') AS controlFactura,  " +
            //             "IF(correlativo='43',NroDocum,'') AS notadebito,  " +
            //             "IF(correlativo='43',NroControlSeniat,'') AS controlNotadebito,  " +
            //             "IF(correlativo='40',NroDocum,'') AS ndebito,  " +
            //             "MontoDocum, MontoBase, doc_iva_rd 'doc_iva' , MONTOIVA, MontoRetenc2 'MontoRetenc' ,  " +
            //             "NroRetenc, pro_nombre, pro_rif, IdProveedor,pro_direcc1  " +
            //             "FROM admlibrocompras  " +
            //             "LEFT OUTER  " +
            //             "JOIN admProveedor ON admlibrocompras.IdProveedor = admproveedor.pro_codigo  " +
            //             "WHERE NroRetenc = '$varNroRetenc' AND StatusRetenc ='2' AND doc_base_rd>0)";


            StrSql = " SELECT NroFiscal,FechaRetenc 'FechaRetenc', PorcIva, doc_base_gn 'doc_base', doc_base_ex 'doc_base_ex', " +
                         "NroDocumAfec, FechaEmision 'FechaEmision',NroControlSeniat 'NroControlSeniat',  " +
                         "IF(correlativo='37',NroDocum,'') AS factura,  " +
                         "IF(correlativo='37',NroControlSeniat,'') AS controlFactura,  " +
                         "IF(correlativo='43',NroDocum,'') AS notadebito,  " +
                         "IF(correlativo='43',NroControlSeniat,'') AS controlNotadebito,  " +
                         "IF(correlativo='40',NroDocum,'') AS ndebito,  " +
                         "MontoDocum, MontoBase, doc_iva_gn 'doc_iva', MONTOIVA, MontoRetenc,  " +
                         "NroRetenc, pro_nombre, pro_rif, IdProveedor,pro_direcc1  " +
                         "FROM admlibrocompras  " +
                         "LEFT OUTER  " +
                         "JOIN admProveedor ON admlibrocompras.IdProveedor = admproveedor.pro_codigo  " +
                         "WHERE IdTipoDoc='$tipoDoc' AND IdProveedor='$proveedor' AND " +
                         "NroDocum='$documento' AND SufiDocum='$sufijo' AND StatusRetenc ='2' AND doc_base_gn>0 UNION ALL (  " +
                         "SELECT NroFiscal,ifnull(FechaRetenc=null,'')'FechaRetenc', PorcIva2 'PorcIva', doc_base_rd 'doc_base', ifnull(doc_base_ex=null,'')'doc_base_ex',   " +
                         "NroDocumAfec, ifnull(FechaEmision=null,'')'FechaEmision',ifnull(NroControlSeniat=null,'') 'NroControlSeniat',  " +
                         "IF(correlativo='37',NroDocum,'') AS factura,  " +
                         "IF(correlativo='37',NroControlSeniat,'') AS controlFactura,  " +
                         "IF(correlativo='43',NroDocum,'') AS notadebito,  " +
                         "IF(correlativo='43',NroControlSeniat,'') AS controlNotadebito,  " +
                         "IF(correlativo='40',NroDocum,'') AS ndebito,  " +
                         "MontoDocum, MontoBase, doc_iva_rd 'doc_iva' , MONTOIVA, MontoRetenc2 'MontoRetenc' ,  " +
                         "NroRetenc, pro_nombre, pro_rif, IdProveedor,pro_direcc1  " +
                         "FROM admlibrocompras  " +
                         "LEFT OUTER  " +
                         "JOIN admProveedor ON admlibrocompras.IdProveedor = admproveedor.pro_codigo  " +
                         "WHERE IdTipoDoc='$tipoDoc' AND IdProveedor='$proveedor' AND " +
                         "NroDocum='$documento' AND SufiDocum='$sufijo' AND StatusRetenc ='2' AND doc_base_rd>0)";

            
            StrSql = StrSql.Replace("$proveedor", pro);
            StrSql = StrSql.Replace("$documento", doc);
            StrSql = StrSql.Replace("$sufijo", suf);
            StrSql = StrSql.Replace("$tipoDoc", tip);

            tablaReporte = database.fDataTable(StrSql);
            database.cerrarConexion();
            return tablaReporte;

        }

        public void anularRetencionIva(string docu, string suf, string tipo, string pro)
        {
            StrSql = "UPDATE admlibrocompras SET admlibrocompras.StatusRetenc='4' " +
                    " WHERE admlibrocompras.NroDocum='$1' " +
                    " AND admlibrocompras.SufiDocum='$2' " +
                    " AND admlibrocompras.IdTipoDoc='$3' " +
                    " AND admlibrocompras.IdProveedor='$4'";

            StrSql = StrSql.Replace("$1", docu);
            StrSql = StrSql.Replace("$2", suf);
            StrSql = StrSql.Replace("$3", tipo);
            StrSql = StrSql.Replace("$4", pro);

            database.ejecutarInsert(StrSql);
            database.cerrarConexion();
        }









    }
}
