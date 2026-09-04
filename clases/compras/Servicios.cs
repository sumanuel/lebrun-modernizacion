using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using lebrun.clasesData;
using System.Data;


namespace lebrun.clases.compras
{
    class Servicios
    {
        private ConexionBD database;
        private string StrSql;
        private DataTable tabla;

        private string tdc_tipo;
        private string tdc_codigo;
        private string tdc_codcta;
        private string tdc_descri;
        private string tdc_fisici;
        private string tdc_logico;
        private string tdc_contable;
        private string tdc_almacen;
        private string tdc_produc;
        private string tdc_invact;
        private string tdc_iva;
        private string tdc_pagos;
        private string tdc_itemcta;
        private string tdc_conaut;
        private string tdc_edtdoc;
        private string tdc_autonum;
        private string tdc_regfis;
        private string tdc_reqapr;
        private string tdc_cxp;
        private string tdc_reporte;
        private string tdc_reqgasto;
        private string tdc_estatus;
        private string tdc_libcom;
        private string tdc_rti;
        private string tdc_auxiliar;
        private string tdc_correlativo;


        private string idProveedor;
        private string nroDocum;
        private string sufiDocum;
        private string nroControlSeniat;
        private string nroFiscal;
        private string idTipoDoc;
        private string idTipoDocAfec;
        private string nroDocumAfec;
        private string sufiDocumAfec;
        private string descripcion;
        private string fechaEmision;
        private string fechaVenc;
        private string fechaLibCompras;
        private string fechaRetenc;
        private string fechaRecepcion;
        private string montoBase;
        private string montoDocum;
        private string doc_iva_gn;
        private string doc_iva_rd;
        private string doc_base_gn;
        private string doc_base_rd;
        private string doc_base_ex;
        private string porcIva;
        private string porcIva2;
        private string montoIva;
        private string montoRetenc;
        private string montoRetenc2;
        private string porcRetenc;
        private string nroRetenc;
        private string periodoFiscal;
        private string idMoneda;
        private string montoEnDivisa;
        private string aduana;
        private int statusLibro;
        private int statusRetenc;
        private int loteEmision;
        private string fechaCarga;
        private string login;
        private string loginUpd;
        private string origen;
        private string cantImpresiones;
        private string baseImport;
        private string ivaImport;
        private string transporte;
        private string otrosGastos;
        private string dif_Inv;
        private string correlativo;
        private string fechaRetencEnt;
        private int fechaEnt;
        private string user_Ent;
        private string pro_nombreProve;
        private string pro_rifProve;
        private string estadoFila;
        private string condicion;
        private string baseImponible;
        private string codCondicionPago;
        private string diasCondicionPago;
        private string recibidor;
        private string tipTra;
        private string sstatus;




        private string tablaBD = "";

        //@database @conectionns

        public string IdProveedor
        {
            get{return idProveedor;}
            set{idProveedor = value;}
        }

        public string NroDocum
        {
            get{return nroDocum;}
            set{nroDocum = value;}
        }

        public string SufiDocum
        {
            get{return sufiDocum;}
            set{ sufiDocum = value;}
        }

        public string NroControlSeniat
        {
            get{return nroControlSeniat;}
            set{ nroControlSeniat = value;}
        }

        public string NroFiscal
        {
            get{return nroFiscal;}
            set{nroFiscal = value;}
        }

        public string IdTipoDoc
        {
            get{return idTipoDoc;}
            set{idTipoDoc = value;}
        }

        public string IdTipoDocAfec
        {
            get{return idTipoDocAfec;}
            set{idTipoDocAfec = value;}
        }

        public string NroDocumAfec
        {
            get{return nroDocumAfec;}
            set{nroDocumAfec = value;}
        }

        public string SufiDocumAfec
        {
            get{return sufiDocumAfec;}
            set{ sufiDocumAfec = value;}
        }

        public string Descripcion
        {
            get{ return descripcion;}
            set{descripcion = value;}
        }

        public string FechaEmision
        {
            get{return fechaEmision;}
            set{fechaEmision = value;}
        }

        public string FechaVenc
        {
            get{return fechaVenc;}
            set{fechaVenc = value;}
        }

        public string FechaLibCompras
        {
            get{return fechaLibCompras;}
            set{fechaLibCompras = value;}
        }

        public string FechaRetenc
        {
            get{return fechaRetenc;}
            set{ fechaRetenc = value;}
        }

        public string FechaRecepcion
        {
            get { return fechaRecepcion; }
            set { fechaRecepcion = value; }
        }

        public string MontoBase
        {
            get{return montoBase;}
            set{montoBase = value;}
        }

        public string MontoDocum
        {
            get{return montoDocum;}
            set{montoDocum = value;}
        }

        public string Doc_iva_gn
        {
            get{return doc_iva_gn;}
            set{doc_iva_gn = value;}
        }

        public string Doc_iva_rd
        {
            get{return doc_iva_rd;}
            set{ doc_iva_rd = value;}
        }

        public string Doc_base_gn
        {
            get{return doc_base_gn;}
            set{doc_base_gn = value;}
        }

        public string Doc_base_rd
        {
            get{return doc_base_rd;}
            set{doc_base_rd = value;}
        }

        public string Doc_base_ex
        {
            get{return doc_base_ex;}
            set{doc_base_ex = value;}
        }

        public string PorcIva
        {
            get{return porcIva;}
            set{porcIva = value;}
        }

        public string PorcIva2
        {
            get{return porcIva2;}
            set{porcIva2 = value;}
        }

        public string MontoIva
        {
            get{return montoIva;}
            set{montoIva = value;}
        }

        public string MontoRetenc
        {
            get{return montoRetenc;}
            set{montoRetenc = value;}
        }

        public string MontoRetenc2
        {
            get{return montoRetenc2;}
            set{montoRetenc2 = value;}
        }

        public string PorcRetenc
        {
            get{return porcRetenc;}
            set{porcRetenc = value;}
        }

        public string NroRetenc
        {
            get{return nroRetenc;}
            set{nroRetenc = value;}
        }

        public string PeriodoFiscal
        {
            get{return periodoFiscal;}
            set{ periodoFiscal = value;}
        }

        public string IdMoneda
        {
            get{return idMoneda;}
            set{idMoneda = value;}
        }

        public string MontoEnDivisa
        {
            get{return montoEnDivisa;}
            set{montoEnDivisa = value;}
        }

        public string Aduana
        {
            get{return aduana;}
            set{aduana = value;}
        }

        public int StatusLibro
        {
            get{return statusLibro;}
            set{statusLibro = value;}
        }

        public int StatusRetenc
        {
            get{return statusRetenc;}
            set{statusRetenc = value;}
        }

        public int LoteEmision
        {
            get{return loteEmision;}
            set{loteEmision = value;}
        }

        public string FechaCarga
        {
            get{return fechaCarga;}
            set{fechaCarga = value;}
        }

        public string Login
        {
            get{ return login;}
            set{login = value;}
        }

        public string LoginUpd
        {
            get{return loginUpd;}
            set{loginUpd = value;}
        }

        public string Origen
        {
            get{return origen;}
            set{origen = value;}
        }

        public string CantImpresiones
        {
            get{return cantImpresiones;}
            set{cantImpresiones = value;}
        }

        public string BaseImport
        {
            get{return baseImport;}
            set{baseImport = value;}
        }

        public string IvaImport
        {
            get{return ivaImport;}
            set{ivaImport = value;}
        }

        public string Transporte
        {
            get{return transporte;}
            set{transporte = value;}
        }

        public string OtrosGastos
        {
            get{return otrosGastos;}
            set{otrosGastos = value;}
        }

        public string Dif_Inv
        {
            get{return dif_Inv;}
            set{dif_Inv = value;}
        }

        public string Correlativo
        {
            get{return correlativo;}
            set{correlativo = value;}
        }

        public string FechaRetencEnt
        {
            get{return fechaRetencEnt;}
            set{fechaRetencEnt = value;}
        }

        public int FechaEnt
        {
            get{return fechaEnt;}
            set{fechaEnt = value;}
        }

        public string User_Ent
        {
            get{return user_Ent;}
            set{user_Ent = value;}
        }

        public string Pro_nombreProve
        {
            get{return pro_nombreProve;}
            set{pro_nombreProve = value;}
        }

        public string Pro_rifProve
        {
            get{return pro_rifProve;}
            set{pro_rifProve = value;}
        }
       
        public string EstadoFila
        {
            get { return estadoFila; }
            set { estadoFila = value; }
        }

        public string BaseImponible
        {
            get { return baseImponible; }
            set { baseImponible = value; }
        }

        public string CodCondicionPago
        {
            get { return codCondicionPago; }
            set { codCondicionPago = value; }
        }

        public string DiasCondicionPago
        {
            get { return diasCondicionPago; }
            set { diasCondicionPago = value; }
        }

        public string Recibidor
        {
            get { return recibidor; }
            set { recibidor = value; }
        }

        public string TipTra
        {
            get { return tipTra; }
            set { tipTra = value; }
        }

        public string Sstatus
        {
            get { return sstatus; }
            set { sstatus = value; }
        }

        //normal de la clase

        public string Tdc_codigo
        {
            get { return tdc_codigo; }
            set { tdc_codigo = value; }
        }

        public string Tdc_descri
        {
            get { return tdc_descri; }
            set { tdc_descri = value; }
        }

        public string Tdc_codcta
        {
            get { return tdc_codcta; }
            set { tdc_codcta = value; }
        }

        public string Tdc_descri1
        {
            get { return tdc_descri; }
            set { tdc_descri = value; }
        }
        
        public string Tdc_fisici
        {
            get { return tdc_fisici; }
            set { tdc_fisici = value; }
        }
        
        public string Tdc_logico
        {
            get { return tdc_logico; }
            set { tdc_logico = value; }
        }

        public string Tdc_contable
        {
            get { return tdc_contable; }
            set { tdc_contable = value; }
        }
        
        public string Tdc_almacen
        {
            get { return tdc_almacen; }
            set { tdc_almacen = value; }
        }
        
        public string Tdc_produc
        {
            get { return tdc_produc; }
            set { tdc_produc = value; }
        }
        
        public string Tdc_invact
        {
            get { return tdc_invact; }
            set { tdc_invact = value; }
        }

        public string Tdc_iva
        {
            get { return tdc_iva; }
            set { tdc_iva = value; }
        }
        
        public string Tdc_pagos
        {
            get { return tdc_pagos; }
            set { tdc_pagos = value; }
        }
        
        public string Tdc_itemcta
        {
            get { return tdc_itemcta; }
            set { tdc_itemcta = value; }
        }
        
        public string Tdc_conaut
        {
            get { return tdc_conaut; }
            set { tdc_conaut = value; }
        }
        
        public string Tdc_edtdoc
        {
            get { return tdc_edtdoc; }
            set { tdc_edtdoc = value; }
        }
        
        public string Tdc_autonum
        {
            get { return tdc_autonum; }
            set { tdc_autonum = value; }
        }
        
        public string Tdc_regfis
        {
            get { return tdc_regfis; }
            set { tdc_regfis = value; }
        }
        
        public string Tdc_reqapr
        {
            get { return tdc_reqapr; }
            set { tdc_reqapr = value; }
        }
        
        public string Tdc_cxp
        {
            get { return tdc_cxp; }
            set { tdc_cxp = value; }
        }
        
        public string Tdc_reporte
        {
            get { return tdc_reporte; }
            set { tdc_reporte = value; }
        }
        
        public string Tdc_reqgasto
        {
            get { return tdc_reqgasto; }
            set { tdc_reqgasto = value; }
        }
        
        public string Tdc_estatus
        {
            get { return tdc_estatus; }
            set { tdc_estatus = value; }
        }
        
        public string Tdc_libcom
        {
            get { return tdc_libcom; }
            set { tdc_libcom = value; }
        }
        
        public string Tdc_rti
        {
            get { return tdc_rti; }
            set { tdc_rti = value; }
        }

        public string Tdc_auxiliar
        {
            get { return tdc_auxiliar; }
            set { tdc_auxiliar = value; }
        }

        public string Tdc_correlativo
        {
            get { return tdc_correlativo; }
            set { tdc_correlativo = value; }
        }

        public string Tdc_tipo
        {
            get { return tdc_tipo; }
            set { tdc_tipo = value; }
        }

        public string Condicion
        {
            get { return condicion; }
            set { condicion = value; }
        }

        public Servicios()
        {
            database = new ConexionBD();
            tabla = new DataTable();                    
        }

        public DataTable consultarAlicuotas()
        {
            StrSql = "select * from admtipiva " +
                        "left join admiva on tip_codigo=alict_tipodiva " +
                        "order by alict_compra desc";

            tabla = database.fDataTable(StrSql);
            database.cerrarConexion();
            return tabla;
        }

        public void insertarSalpro(DataTable tabla)
        {
            StrSql = "insert into admsalpro " +
                        "(IdProveedor, NroDocum, SufiDocum,NroDocumAfect, NroControlSeniat, Descripcion, " +
                        "NroComprobante,FechaEmision, FechaVencimiento, FechaCarga, " +
                        "FechaLibCompras, FechaPago,NroCompPago,IdTipoDoc,IdBanco,IdMoneda, " +
                        "MontoEnDivisa, MontoDocum, SaldoDocum, MontoIva, " +
                        "PorcIva, PorcIva2,MontoRetenc, NroCompRetenc,Status," +
                        "Login, Loginact,correlativo)";
        }

        public void insertarDocPro(DataTable tabla)
        {
            StrSql = "INSERT INTO " + tabla + " " +
           "(doc_correlativo, doc_numerodoc, doc_sufijo, doc_sufiOrden, " +
           "doc_numfis, doc_tipdoc, doc_facafe, doc_sufijofacafe,doc_numcontrol, doc_ordencompra, " +
           "doc_docasociado, doc_codigo, doc_concep, doc_cencost, " +
           "doc_sucus, doc_moneda, doc_codtra, doc_condic, " +
           "doc_usuario, doc_tiptra, doc_origen, doc_estadodoc, " +
           "doc_status,doc_expimp, doc_fechCarg, doc_fechEmis, " +
           "doc_fechRecep,doc_fechven, doc_fecReten, doc_fechLCompra, " +
           "doc_hora, doc_cbtnum, doc_recibidopor, doc_docorg, " +
           "doc_cbtpre, doc_pagnum, doc_plaimp, doc_aprob1, " +
           "doc_aprob2, doc_aprob3, doc_anufis, doc_crepago, " +
           "doc_cxp, doc_plazo, doc_basnet, doc_dcto, " +
           "doc_otrimp, doc_mtoiva, doc_neto, doc_otros, " +
           "doc_recargo, doc_valcamb, doc_dctobs, doc_totaldivisa, " +
           "doc_prod, doc_subtot, doc_ditem, doc_descdoc, " +
           "doc_descdoc2, doc_descdoc3, doc_subbase, doc_imponible, " +
           "doc_iva_gn, doc_iva_rd, doc_base_gn, doc_base_rd, " +
           "doc_base_ex,doc_existe) ";
        }

        public DataTable datosTipoDoc2(string tipoDoc)
        {
            StrSql = "select * from admtipdocpro where tdc_codigo='$1' and tdc_estatus='1'";
            StrSql = StrSql.Replace("$1", tipoDoc);

            tabla = database.fDataTable(StrSql);
            database.cerrarConexion();
            return tabla;
        }


        public bool datosTipoDoc(string tipoDoc)
        {
            bool bandera = false;

            StrSql = "select * from admtipdocpro where tdc_codigo='$1' and tdc_estatus='1'";
            StrSql = StrSql.Replace("$1", tipoDoc);

            tabla = database.fDataTable(StrSql);

            if (tabla.Rows.Count > 0)
            {
                this.tdc_descri = tabla.Rows[0]["tdc_descri"].ToString();
                this.tdc_codcta = tabla.Rows[0]["tdc_codcta"].ToString();
                this.tdc_descri = tabla.Rows[0]["tdc_descri"].ToString();
                this.tdc_fisici = tabla.Rows[0]["tdc_fisici"].ToString();
                this.tdc_logico = tabla.Rows[0]["tdc_logico"].ToString();
                this.tdc_contable = tabla.Rows[0]["tdc_contable"].ToString();
                this.tdc_almacen = tabla.Rows[0]["tdc_almacen"].ToString();
                this.tdc_produc = tabla.Rows[0]["tdc_produc"].ToString();
                this.tdc_invact = tabla.Rows[0]["tdc_invact"].ToString();
                this.tdc_iva = tabla.Rows[0]["tdc_iva"].ToString();
                this.tdc_pagos = tabla.Rows[0]["tdc_pagos"].ToString();
                this.tdc_itemcta = tabla.Rows[0]["tdc_itemcta"].ToString();
                this.tdc_conaut = tabla.Rows[0]["tdc_conaut"].ToString();
                this.tdc_edtdoc = tabla.Rows[0]["tdc_edtdoc"].ToString();
                this.tdc_autonum = tabla.Rows[0]["tdc_autonum"].ToString();
                this.tdc_regfis = tabla.Rows[0]["tdc_regfis"].ToString();
                this.tdc_reqapr = tabla.Rows[0]["tdc_reqapr"].ToString();
                this.tdc_cxp = tabla.Rows[0]["tdc_cxp"].ToString();
                this.tdc_reporte = tabla.Rows[0]["tdc_reporte"].ToString();
                this.tdc_reqgasto = tabla.Rows[0]["tdc_reqgasto"].ToString();
                this.tdc_estatus = tabla.Rows[0]["tdc_estatus"].ToString();
                this.tdc_libcom = tabla.Rows[0]["tdc_libcom"].ToString();
                this.tdc_rti = tabla.Rows[0]["tdc_rti"].ToString();
                this.tdc_auxiliar = tabla.Rows[0]["tdc_auxiliar"].ToString();
                this.tdc_correlativo = tabla.Rows[0]["tdc_correlativo"].ToString();
                this.tdc_codigo = tabla.Rows[0]["tdc_codigo"].ToString();
                this.tdc_tipo = tabla.Rows[0]["tdc_tipo"].ToString();

                bandera = true;
            }
            else
            {
                bandera = false;
            }
            
            database.cerrarConexion();
            return bandera;
        }

        public DataTable consultarCuentasContab(string tdc_nivel5, string tdc_reporte, string db)
        {
            database.conectionStringSysconta(db);

            StrSql = "SELECT * FROM admcuentascontables where cc_codigoCuenta='$1'  OR cc_codigoCuenta='$2'";
            StrSql = StrSql.Replace("$1", tdc_nivel5);
            StrSql = StrSql.Replace("$2", tdc_reporte);

            tabla = database.fDataTable(StrSql);
            database.modificarConexionString(1);
            database.cerrarConexion();
            return tabla;
        }

        public void ingresarLibroCompras()
        {

                StrSql = "";
                StrSql = "insert into admlibrocompras (" +
                "IdProveedor, NroDocum, SufiDocum, NroControlSeniat,NroFiscal, " +
                "IdTipoDoc, NroDocumAfec,Descripcion, FechaEmision, FechaVenc, " +
                "FechaLibCompras, FechaRetenc, MontoBase, " +
                "MontoDocum, doc_iva_gn, doc_iva_rd, doc_base_gn, " +
                "doc_base_rd, doc_base_ex, PorcIva, PorcIva2, " +
                "MontoIva, MontoRetenc, PorcRetenc, NroRetenc, " +
                "periodoFiscal, IdMoneda, MontoEnDivisa, " +
                "Aduana, StatusLibro, StatusRetenc, LoteEmision, " +
                "FechaCarga, Login, LoginUpd,origen,correlativo,FechaRetencEnt)" +
                "VALUES('$idPro','$docum','$sufi','$ctrl','$fiscal'," +
                "'$tip','$afec','$descrip','$emision'," +
                "'$vencimiento','$libro','$retenc','$base','$monto'," +
                "'$ivGn','$ivaRd','$Gn','$Rd','$ex'," +
                "'$porcIva','$porIva2','$monIv','$mretencion','$porRetenc'," +
                "'$nroRetenc','$periodoFiscal','$idMoneda','$divisa','$aduana'," +
                "'$statusLibro','$staRetenc','$lote','$cargaFecha','$login','$updLogin','$origen','$correlativo','$fEntregado')";

                StrSql = StrSql.Replace("$idPro", this.IdProveedor);
                StrSql = StrSql.Replace("$docum", this.NroDocum);
                StrSql = StrSql.Replace("$sufi", this.SufiDocum);
                StrSql = StrSql.Replace("$ctrl", this.NroControlSeniat);
                StrSql = StrSql.Replace("$fiscal", this.NroFiscal);
                StrSql = StrSql.Replace("$tip", this.IdTipoDoc);
                StrSql = StrSql.Replace("$afec", this.NroDocumAfec);
                StrSql = StrSql.Replace("$descrip", this.Descripcion);
                StrSql = StrSql.Replace("$emision", this.FechaEmision);
                StrSql = StrSql.Replace("$vencimiento", this.FechaVenc);
                StrSql = StrSql.Replace("$libro", this.FechaLibCompras);
                StrSql = StrSql.Replace("$retenc", this.FechaRetenc);
                StrSql = StrSql.Replace("$base", this.MontoBase);
                StrSql = StrSql.Replace("$monto", this.MontoDocum);
                StrSql = StrSql.Replace("$ivGn", this.Doc_iva_gn);
                StrSql = StrSql.Replace("$ivaRd", this.Doc_iva_rd);
                StrSql = StrSql.Replace("$Gn", this.Doc_base_gn);
                StrSql = StrSql.Replace("$Rd", this.Doc_base_rd);
                StrSql = StrSql.Replace("$ex", this.Doc_base_ex);
                StrSql = StrSql.Replace("$porcIva", this.PorcIva);
                StrSql = StrSql.Replace("$porIva2", this.PorcIva2);
                StrSql = StrSql.Replace("$monIv", this.MontoIva);
                StrSql = StrSql.Replace("$mretencion", this.MontoRetenc);
                StrSql = StrSql.Replace("$porRetenc", this.PorcRetenc);
                StrSql = StrSql.Replace("$nroRetenc", this.NroRetenc);
                StrSql = StrSql.Replace("$periodoFiscal", "");
                StrSql = StrSql.Replace("$idMoneda", "Bs");
                StrSql = StrSql.Replace("$divisa", "0.00");
                StrSql = StrSql.Replace("$aduana", "0");
                StrSql = StrSql.Replace("$statusLibro", "0");
                StrSql = StrSql.Replace("$staRetenc", "0");
                StrSql = StrSql.Replace("$lote", "0");
                StrSql = StrSql.Replace("$cargaFecha", this.FechaCarga);
                StrSql = StrSql.Replace("$login", this.Login);
                StrSql = StrSql.Replace("$updLogin", this.LoginUpd);
                StrSql = StrSql.Replace("$origen", this.Origen);
                StrSql = StrSql.Replace("$correlativo", this.Correlativo);
                StrSql = StrSql.Replace("$fEntregado", this.FechaCarga);

                database.ejecutarInsert(StrSql);
                database.cerrarConexion();

        }

        public void ingresarSalpro()
        {
            StrSql = "";
            StrSql = "insert into admsalpro (" +
            "IdProveedor,NroDocum,SufiDocum,NroDocumAfect,NroControlSeniat,Descripcion, " +
            "NroComprobante,FechaEmision,FechaVencimiento,FechaCarga, " +
            "FechaLibCompras,FechaPago,NroCompPago,IdTipoDoc,IdBanco,IdMoneda, " +
            "MontoEnDivisa,MontoDocum,SaldoDocum,MontoIva, " +
            "PorcIva,PorcIva2,MontoRetenc,NroCompRetenc,Status,Login,Loginact,correlativo)" +
            "VALUES('$idPro','$docum','$sufi','$afec','$ctrl','$descrip',"+
            "'','$emision','$vencimiento','$cargaFecha',"+
            "'$libro','$cargaFecha','','$tip','','$idMoneda'," +
            "'$divisa','$monto','$monto','$monIv'," +
            "'$porcIva','$porIva2','$mretencion','','$staRetenc','$login','$updLogin','$correlativo')";

            StrSql = StrSql.Replace("$idPro", this.IdProveedor);
            StrSql = StrSql.Replace("$docum", this.NroDocum);
            StrSql = StrSql.Replace("$sufi", this.SufiDocum);
            StrSql = StrSql.Replace("$ctrl", this.NroControlSeniat);
            StrSql = StrSql.Replace("$fiscal", this.NroFiscal);
            StrSql = StrSql.Replace("$tip", this.IdTipoDoc);
            StrSql = StrSql.Replace("$afec", this.NroDocumAfec);
            StrSql = StrSql.Replace("$descrip", this.Descripcion);
            StrSql = StrSql.Replace("$emision", this.FechaEmision);
            StrSql = StrSql.Replace("$vencimiento", this.FechaVenc);
            StrSql = StrSql.Replace("$libro", this.FechaLibCompras);
            StrSql = StrSql.Replace("$retenc", this.FechaRetenc);
            StrSql = StrSql.Replace("$base", this.MontoBase);
            StrSql = StrSql.Replace("$monto", this.MontoDocum);
            StrSql = StrSql.Replace("$ivGn", this.Doc_iva_gn);
            StrSql = StrSql.Replace("$ivaRd", this.Doc_iva_rd);
            StrSql = StrSql.Replace("$Gn", this.Doc_base_gn);
            StrSql = StrSql.Replace("$Rd", this.Doc_base_rd);
            StrSql = StrSql.Replace("$ex", this.Doc_base_ex);
            StrSql = StrSql.Replace("$porcIva", this.PorcIva);
            StrSql = StrSql.Replace("$porIva2", this.PorcIva2);
            StrSql = StrSql.Replace("$monIv", this.MontoIva);
            StrSql = StrSql.Replace("$mretencion", this.MontoRetenc);
            StrSql = StrSql.Replace("$porRetenc", this.PorcRetenc);
            StrSql = StrSql.Replace("$nroRetenc", this.NroRetenc);
            StrSql = StrSql.Replace("$periodoFiscal", "");
            StrSql = StrSql.Replace("$idMoneda", "Bs");
            StrSql = StrSql.Replace("$divisa", "0.00");
            StrSql = StrSql.Replace("$aduana", "0");
            StrSql = StrSql.Replace("$statusLibro", "0");
            StrSql = StrSql.Replace("$staRetenc", "0");
            StrSql = StrSql.Replace("$lote", "0");
            StrSql = StrSql.Replace("$cargaFecha", this.FechaCarga);
            StrSql = StrSql.Replace("$login", this.Login);
            StrSql = StrSql.Replace("$updLogin", this.LoginUpd);
            StrSql = StrSql.Replace("$origen", this.Origen);
            StrSql = StrSql.Replace("$correlativo", this.Correlativo);
            StrSql = StrSql.Replace("$fEntregado", this.FechaCarga);

            database.ejecutarInsert(StrSql);
            database.cerrarConexion();

        }

        public void ingresarISLR(DataTable tabla)
        {



        }

        public void ingresarDocPro(int opcion)
        {

            if (opcion == 1)
            {
                tablaBD = "admdocpro";
            }

            if (opcion == 2)
            {
                tablaBD = "admdocpro_temp";
            }

            StrSql = "INSERT INTO " + tablaBD + " " +
           "(doc_correlativo, doc_numerodoc, doc_sufijo, doc_sufiOrden, " +
           "doc_numfis, doc_tipdoc, doc_facafe, doc_sufijofacafe,doc_numcontrol, doc_ordencompra, " +
           "doc_docasociado, doc_codigo, doc_concep, doc_cencost, " +
           "doc_sucus, doc_moneda, doc_codtra, doc_condic, " +
           "doc_usuario, doc_tiptra, doc_origen, doc_estadodoc, " +
           "doc_status,doc_expimp, doc_fechCarg, doc_fechEmis, " +
           "doc_fechRecep,doc_fechven, doc_fecReten, doc_fechLCompra, " +
           "doc_hora, doc_cbtnum, doc_recibidopor, doc_docorg, " +
           "doc_cbtpre, doc_pagnum, doc_plaimp, doc_aprob1, " +
           "doc_aprob2, doc_aprob3, doc_anufis, doc_crepago, " +
           "doc_cxp, doc_plazo, doc_basnet, doc_dcto, " +
           "doc_otrimp, doc_mtoiva, doc_neto, doc_otros, " +
           "doc_recargo, doc_valcamb, doc_dctobs, doc_totaldivisa, " +
           "doc_prod, doc_subtot, doc_ditem, doc_descdoc, " +
           "doc_descdoc2, doc_descdoc3, doc_subbase, doc_imponible, " +
           "doc_iva_gn, doc_iva_rd, doc_base_gn, doc_base_rd, " +
           "doc_base_ex,doc_existe)VALUES( " +
           "'','$docum','$sufi',''," +
           "'$fiscal','$tip','$afec','$sAfec','$ctrl',''," +
           "'','$idPro','$descrip','$cCosto'," +
           "'$sucursal','$idMoneda','','$condicion'," +
           "'$login','$Tra','Nacional','Activo', " +
           "'$sst','','$cargaFecha','$emision'," +
           "'$recep','$vencimiento','$retenc','$libro'," +
           "'$hora','SERV','$recibidor',''," +
           "'','','',''," +
           "'','','',''," +
           "'$cxp','$dias','$subTotal','0.00'," +
           "'0.00','$monIv','$monto','0.00', " +
           "'0.00','$valCamb','0.00','$divisa'," +
           "'0.00','$base','0.00','0.00'," +
           "'0.00','0.00','$base','$imponible'," +
           "'$ivGn','$ivaRd','$Gn','$Rd'," +
           "'$ex','');";

            StrSql = StrSql.Replace("$idPro", this.IdProveedor);
            StrSql = StrSql.Replace("$docum", this.NroDocum);
            StrSql = StrSql.Replace("$sufi", this.SufiDocum);
            StrSql = StrSql.Replace("$ctrl", this.NroControlSeniat);
            StrSql = StrSql.Replace("$fiscal", this.NroFiscal);
            StrSql = StrSql.Replace("$tip", this.IdTipoDoc);
            StrSql = StrSql.Replace("$afec", this.NroDocumAfec);
            StrSql = StrSql.Replace("$sAfec", this.SufiDocumAfec);
            StrSql = StrSql.Replace("$descrip", this.Descripcion);
            StrSql = StrSql.Replace("$emision", this.FechaEmision);
            StrSql = StrSql.Replace("$vencimiento", this.FechaVenc);
            StrSql = StrSql.Replace("$libro", this.FechaLibCompras);
            StrSql = StrSql.Replace("$retenc", this.FechaRetenc);
            StrSql = StrSql.Replace("$recep", this.FechaRecepcion);
            StrSql = StrSql.Replace("$base", this.MontoBase);
            StrSql = StrSql.Replace("$monto", this.MontoDocum);
            StrSql = StrSql.Replace("$ivGn", this.Doc_iva_gn);
            StrSql = StrSql.Replace("$ivaRd", this.Doc_iva_rd);
            StrSql = StrSql.Replace("$Gn", this.Doc_base_gn);
            StrSql = StrSql.Replace("$Rd", this.Doc_base_rd);
            StrSql = StrSql.Replace("$ex", this.Doc_base_ex);
            StrSql = StrSql.Replace("$porcIva", this.PorcIva);
            StrSql = StrSql.Replace("$porIva2", this.PorcIva2);
            StrSql = StrSql.Replace("$monIv", this.MontoIva);
            StrSql = StrSql.Replace("$mretencion", this.MontoRetenc);
            StrSql = StrSql.Replace("$porRetenc", this.PorcRetenc);
            StrSql = StrSql.Replace("$nroRetenc", this.NroRetenc);
            StrSql = StrSql.Replace("$periodoFiscal", "");
            StrSql = StrSql.Replace("$idMoneda", "Bs");
            StrSql = StrSql.Replace("$divisa", this.MontoDocum);
            StrSql = StrSql.Replace("$aduana", "0");
            StrSql = StrSql.Replace("$statusLibro", "0");
            StrSql = StrSql.Replace("$staRetenc", "0");
            StrSql = StrSql.Replace("$lote", "0");
            StrSql = StrSql.Replace("$cargaFecha", this.FechaCarga);
            StrSql = StrSql.Replace("$login", this.Login);
            StrSql = StrSql.Replace("$updLogin", this.LoginUpd);
            StrSql = StrSql.Replace("$origen", this.Origen);
            StrSql = StrSql.Replace("$correlativo", this.Correlativo);
            StrSql = StrSql.Replace("$fEntregado", this.FechaCarga);
            StrSql = StrSql.Replace("$cCosto", "0000000001");
            StrSql = StrSql.Replace("$sucursal", "000001");
            StrSql = StrSql.Replace("$condicion", this.Condicion);
            StrSql = StrSql.Replace("$cxp", this.Tdc_cxp);
            StrSql = StrSql.Replace("$subTotal", this.MontoBase);
            StrSql = StrSql.Replace("$imponible", this.BaseImponible);
            StrSql = StrSql.Replace("$dias",this.DiasCondicionPago);
            StrSql = StrSql.Replace("$recibidor", this.Recibidor);
            StrSql = StrSql.Replace("$hora", "" + DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second);
            StrSql = StrSql.Replace("$valCamb", "1");
            StrSql = StrSql.Replace("$sst", this.Sstatus);
            StrSql = StrSql.Replace("$Tra", this.TipTra);

            database.ejecutarInsert(StrSql);
            database.cerrarConexion();
           
        }

        public void actualizarDocPro2()
        {
            
            ////

            StrSql = "update admproveedor2 set " +
                "fec_ult_compra='$1',ult_refer_compra='$2', " +
                "ult_cant_compra='0.00'" +
                "where " +
                "pro2_codigo='$3'";

            StrSql = StrSql.Replace("$1", this.FechaEmision);
            StrSql = StrSql.Replace("$2", this.NroDocum);
            StrSql = StrSql.Replace("$3", this.IdProveedor);

        }

        public DataTable armarLbxServicios(string cadena)
        {
            StrSql = "(SELECT " +
                    "doc_numerodoc 'Numero Doc', doc_sufijo 'Sufijo',doc_sufiOrden,doc_codigo 'Codigo',pro_nombre 'Proveedor',doc_fechEmis 'Fecha', doc_hora 'Hora', doc_status 'Status', " +
                    "doc_neto 'Monto',pro_rif 'Rif',doc_tipdoc 'Tipo'" +
                    "FROM " +
                    "admdocpro " +
                    "left join admproveedor on pro_codigo=doc_codigo " +
                    "WHERE " +
                    "doc_cbtnum='SERV' " + cadena + " " +
                    ") UNION ALL (SELECT " +
                    "doc_numerodoc 'Numero Doc', doc_sufijo 'Sufijo',doc_sufiOrden,doc_codigo 'Codigo',pro_nombre 'Proveedor',doc_fechEmis 'Fecha', doc_hora 'Hora', doc_status 'Status', " +
                    "doc_neto 'Monto',pro_rif 'Rif',doc_tipdoc 'Tipo'" +
                    "FROM " +
                    "admdocpro_temp " +
                    "left join admproveedor on pro_codigo=doc_codigo " +
                    "WHERE " +
                    "doc_cbtnum='SERV' " + cadena + ")  order by Fecha desc,Hora desc limit 200";

            tabla = database.fDataTable(StrSql);
            database.cerrarConexion();
            return tabla;
        }

        public DataTable cargarCabeceraModificar(string doc, string suf, string proveedor, string tipo, int opcion)
        {
            if (opcion == 1)
            {
                tablaBD = "admdocpro";
            }

            if (opcion == 2)
            {
                tablaBD = "admdocpro_temp";
            }


            StrSql = "SELECT " + tablaBD + ".*,pro_nombre,pro_rif " +
                    "FROM " + tablaBD + " " +
                    "LEFT JOIN admproveedor on doc_codigo= pro_codigo " +
                    "WHERE doc_numerodoc='$1' AND " +
                    "doc_sufijo='$2' " +
                    "AND doc_codigo='$3' AND doc_tipdoc='$4';";

            StrSql = StrSql.Replace("$1", doc);
            StrSql = StrSql.Replace("$2", suf);
            StrSql = StrSql.Replace("$3", proveedor);
            StrSql = StrSql.Replace("$4", tipo);
    
            tabla = database.fDataTable(StrSql);
            database.cerrarConexion();
            return tabla;
        }

        public DataTable armarParteContable(string doc, string suf, string proveedor, string tipo, int opcion, string compania, int caso)
        {

            string bd;

            if (compania.Equals("01"))
            {
                bd = "sysconta";
            }
            else
            {
                bd = "sysconta_" + compania;
            }

            if (caso == 1)
            {
                tablaBD = "admmovcontab";
            }
            if (caso == 2)
            {
                tablaBD = "admmovcontab_temp";
            }

            StrSql = "SELECT " +
                  "* " +
                "FROM " +
                "" + tablaBD + " left join " + bd + ".admcuentascontables on cc_CodigoCuenta = movcon_cuenta " +
                "WHERE " +
                "movcon_proveedor='$1' AND movcon_numdoc='$2' " +
                "AND movcon_sufdoc='$3' AND movcon_tipdoc='$4' AND IdSistemas<>'ISLR'";

            //se le agrego idSistemas 2014-08-05
            //

            StrSql = StrSql.Replace("$1", proveedor);
            StrSql = StrSql.Replace("$2", doc);
            StrSql = StrSql.Replace("$3", suf);
            StrSql = StrSql.Replace("$4", tipo);

            tabla = database.fDataTable(StrSql);
            database.cerrarConexion();
            return tabla;
        }
 
        //usar crear cuenta contable que hizo roberto 
        //usar clase movimientos contables para generar MOVCONTAB Y SYSCONTAB

        public void actualizarDocPro(string documento, string sufijo, string tipo)
        {

             StrSql = "update admdocpro_temp set  " +
                    "doc_correlativo='', doc_numerodoc='$docum', doc_numcontrol='$ctrl', doc_sufijo='$sufi',doc_sufiOrden='', doc_numfis='$fiscal', " +
                    "doc_tipdoc='$tip', doc_facafe='$afec', doc_sufijofacafe='$sAfec', doc_ordencompra='', doc_docasociado='', " +
                    "doc_codigo='$idPro', doc_cencost='$cCosto',doc_concep='$descrip', doc_sucus='$sucursal', doc_moneda='$idMoneda', " +
                    "doc_codtra='', doc_condic='$dias', doc_usuario='$login', doc_tiptra='$Tra', " +
                    "doc_origen='Nacional', doc_estadodoc='Activo', doc_status='$sst', doc_expimp='', " +
                    "doc_fechCarg='$cargaFecha', doc_fechEmis='$emision', doc_fechRecep='$recep', doc_fechven='$vencimiento', " +
                    "doc_fecReten='$retenc', doc_fechLCompra='$libro'," +
                    "doc_hora='$hora', doc_cbtnum='SERV', doc_recibidopor='$recibidor', doc_docorg='', " +
                    "doc_cbtpre='', doc_pagnum='', doc_plaimp='', doc_aprob1='', " +
                    "doc_aprob2='', doc_aprob3='', doc_anufis='', doc_crepago='', " +
                    "doc_basnet='$subTotal', doc_cxp='$cxp', doc_dcto='0.00', doc_otrimp=0, " +
                    "doc_descdoc2='0.00', doc_descdoc3='0.00'," +
                    "doc_mtoiva='$monIv', doc_neto='$monto', doc_otros='0.00', doc_plazo='$dias', " +
                    "doc_recargo=0, doc_valcamb='$valCamb', doc_dctobs=0, doc_totaldivisa='$divisa', " +
                    "doc_prod='0', doc_subtot='$subTotal', doc_ditem='0', doc_descdoc='0.00', " +
                    "doc_subbase='$subTotal', doc_imponible='$imponible', doc_iva_gn='$ivGn', doc_iva_rd='$ivaRd', " +
                    "doc_base_gn='$Gn', doc_base_rd='$Rd', doc_base_ex='$ex' " +
                    "where " +
                    "doc_numerodoc='$1' AND doc_tipdoc='$3' and doc_sufijo='$2'";

             StrSql = StrSql.Replace("$idPro", this.IdProveedor);
             StrSql = StrSql.Replace("$docum", this.NroDocum);
             StrSql = StrSql.Replace("$sufi", this.SufiDocum);
             StrSql = StrSql.Replace("$ctrl", this.NroControlSeniat);
             StrSql = StrSql.Replace("$fiscal", this.NroFiscal);
             StrSql = StrSql.Replace("$tip", this.IdTipoDoc);
             StrSql = StrSql.Replace("$afec", this.NroDocumAfec);
             StrSql = StrSql.Replace("$sAfec", this.SufiDocumAfec);
             StrSql = StrSql.Replace("$descrip", this.Descripcion);
             StrSql = StrSql.Replace("$emision", this.FechaEmision);
             StrSql = StrSql.Replace("$vencimiento", this.FechaVenc);
             StrSql = StrSql.Replace("$libro", this.FechaLibCompras);
             StrSql = StrSql.Replace("$retenc", this.FechaRetenc);
             StrSql = StrSql.Replace("$recep", this.FechaRecepcion);
             StrSql = StrSql.Replace("$base", this.MontoBase);
             StrSql = StrSql.Replace("$monto", this.MontoDocum);
             StrSql = StrSql.Replace("$ivGn", this.Doc_iva_gn);
             StrSql = StrSql.Replace("$ivaRd", this.Doc_iva_rd);
             StrSql = StrSql.Replace("$Gn", this.Doc_base_gn);
             StrSql = StrSql.Replace("$Rd", this.Doc_base_rd);
             StrSql = StrSql.Replace("$ex", this.Doc_base_ex);
             StrSql = StrSql.Replace("$porcIva", this.PorcIva);
             StrSql = StrSql.Replace("$porIva2", this.PorcIva2);
             StrSql = StrSql.Replace("$monIv", this.MontoIva);
             StrSql = StrSql.Replace("$mretencion", this.MontoRetenc);
             StrSql = StrSql.Replace("$porRetenc", this.PorcRetenc);
             StrSql = StrSql.Replace("$nroRetenc", this.NroRetenc);
             StrSql = StrSql.Replace("$periodoFiscal", "");
             StrSql = StrSql.Replace("$idMoneda", "Bs");
             StrSql = StrSql.Replace("$divisa", this.MontoDocum);
             StrSql = StrSql.Replace("$aduana", "0");
             StrSql = StrSql.Replace("$statusLibro", "0");
             StrSql = StrSql.Replace("$staRetenc", "0");
             StrSql = StrSql.Replace("$lote", "0");
             StrSql = StrSql.Replace("$cargaFecha", this.FechaCarga);
             StrSql = StrSql.Replace("$login", this.Login);
             StrSql = StrSql.Replace("$updLogin", this.LoginUpd);
             StrSql = StrSql.Replace("$origen", this.Origen);
             StrSql = StrSql.Replace("$correlativo", this.Correlativo);
             StrSql = StrSql.Replace("$fEntregado", this.FechaCarga);
             StrSql = StrSql.Replace("$cCosto", "0000000001");
             StrSql = StrSql.Replace("$sucursal", "000001");
             StrSql = StrSql.Replace("$condicion", this.Condicion);
             StrSql = StrSql.Replace("$cxp", this.Tdc_cxp);
             StrSql = StrSql.Replace("$subTotal", this.MontoBase);
             StrSql = StrSql.Replace("$imponible", this.BaseImponible);
             StrSql = StrSql.Replace("$dias", this.DiasCondicionPago);
             StrSql = StrSql.Replace("$recibidor", this.Recibidor);
             StrSql = StrSql.Replace("$hora", "" + DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second);
             StrSql = StrSql.Replace("$valCamb", "1");
             StrSql = StrSql.Replace("$sst", this.Sstatus);
             StrSql = StrSql.Replace("$Tra", this.TipTra);

             StrSql = StrSql.Replace("$1", documento);
             StrSql = StrSql.Replace("$2", sufijo);
             StrSql = StrSql.Replace("$3", tipo);

             database.ejecutarInsert(StrSql);
             database.cerrarConexion();
        }

        public bool documentoExiste(string provee, string docu, string suf, string opcion)
        {
            if (opcion == "1")
            {
                tablaBD = "";
                tablaBD = "admdocpro";
            }

            if (opcion == "2")
            {
                tablaBD = "";
                tablaBD = "admdocpro_temp";
            }

            StrSql = "SELECT * FROM " + tablaBD + " WHERE doc_codigo='$1' and doc_numerodoc='$2' and doc_sufijo='$3'";
            StrSql = StrSql.Replace("$1", provee);
            StrSql = StrSql.Replace("$2", docu);
            StrSql = StrSql.Replace("$3", suf);
            
            tabla = database.fDataTable(StrSql);
            database.cerrarConexion();

            if (tabla.Rows.Count > 0)
            {
                return true;
            }

            //StrSql = "SELECT * FROM admdocpro_temp WHERE doc_codigo='$1' and doc_numerodoc='$2' and doc_sufijo='$3'";
            //StrSql = StrSql.Replace("$1", provee);
            //StrSql = StrSql.Replace("$2", docu);
            //StrSql = StrSql.Replace("$3", suf);

            //tabla = database.fDataTable(StrSql);
            //database.cerrarConexion();

            //if (tabla.Rows.Count > 0)
            //{
            //    return true;
            //}

            return false;
        }

        public bool documentoAplicaExiste(string provee, string docu, string suf)
        {
            StrSql = "SELECT * FROM admdocpro WHERE doc_codigo='$1' and doc_numerodoc='$2' and doc_sufijo='$3'";
            StrSql = StrSql.Replace("$1", provee);
            StrSql = StrSql.Replace("$2", docu);
            StrSql = StrSql.Replace("$3", suf);
            
            tabla = database.fDataTable(StrSql);
            database.cerrarConexion();

            if (tabla.Rows.Count > 0)
            {
                return true;
            }
            return false;
        }

        public void eliminarCabeceraDocTemp(string docu, string suf, string tipoDoc, string proveedor)
        {

            StrSql = "DELETE  FROM admdocpro_temp " +
                        "WHERE admdocpro_temp.doc_numerodoc='$1' " +
                        "AND admdocpro_temp.doc_sufijo='$2' " +
                        "AND admdocpro_temp.doc_tipdoc='$3' " +
                        "AND admdocpro_temp.doc_codigo='$4' ";

            StrSql = StrSql.Replace("$1", docu);
            StrSql = StrSql.Replace("$2", suf);
            StrSql = StrSql.Replace("$3", tipoDoc);
            StrSql = StrSql.Replace("$4", proveedor);

            database.ejecutarInsert(StrSql);
            database.cerrarConexion();
        }




    }
}
