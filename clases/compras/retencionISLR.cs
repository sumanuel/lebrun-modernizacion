using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using lebrun.clasesData;
using System.Data;
using lebrun.clases.compras;
using lebrun.clases.contabilidad;

namespace lebrun.clases.compras
{
    class retencionISLR
    {
        private ConexionBD database;
        private DataTable tabla;
        private string StrSql;

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
        private string correlativoISLR;
        private string cuentaC;
        private string totalRetenc = "0";
        private string login;
        private DataTable tablaMovContab;
        private movimientoContable movCon;
        private DataTable tablaISLR;
        private Compania compania;

        private string correlativoCompro;
        private string comprobante;
        private string nombre;
        private string rif;
        private int items;
        private int numero;
        private string letra;
        private string cuentaPro;
        private string auxiliarPro;


        public string IdProveedor
        {
            get { return idProveedor; }
            set { idProveedor = value; }
        }

        public string NroDocum
        {
            get { return nroDocum; }
            set { nroDocum = value; }
        }
        
        public string SufiDocum
        {
            get { return sufiDocum; }
            set { sufiDocum = value; }
        }
        

        public string NroControlSeniat
        {
            get { return nroControlSeniat; }
            set { nroControlSeniat = value; }
        }
        
        public string NroFiscal
        {
            get { return nroFiscal; }
            set { nroFiscal = value; }
        }
        
        public string IdTipoDoc
        {
            get { return idTipoDoc; }
            set { idTipoDoc = value; }
        }
        
        public string IdTipoDocAfec
        {
            get { return idTipoDocAfec; }
            set { idTipoDocAfec = value; }
        }
        
        public string NroDocumAfec
        {
            get { return nroDocumAfec; }
            set { nroDocumAfec = value; }
        }
        
        public string SufiDocumAfec
        {
            get { return sufiDocumAfec; }
            set { sufiDocumAfec = value; }
        }
        
        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }

        public string Login
        {
            get { return login; }
            set { login = value; }
        }



        public retencionISLR()
        {
            database = new ConexionBD();
            tabla = new DataTable();
            tablaMovContab = new DataTable();
            movCon = new movimientoContable();
            tablaISLR = new DataTable();
            compania = new Compania();
            compania.obtenerCodigoCompaniaActual();
        }


        public void insertarLibroISLR(DataTable tabla, Servicios clase, string montoReten, string unidTributaria, string baseRetener)
        {

            StrSql = "";

            StrSql = "INSERT INTO admlibroislr (lislr_proveedor,lislr_fechaEmisionFac,lislr_numFac,lislr_sufFac ,lislr_numCtrlFac," +
            "lislr_facAfec ,lislr_ctrlFacAfec,lislr_tipoDocum,lislr_descripcion,lislr_totalFac,lislr_totalBase," +
            "lislr_baseReten,lislr_porRetenc,lislr_sustraendo,lislr_retenido,lislr_factor,lislr_fechaEmision," +
            "lislr_usuario,lislr_periodo,lislr_codConcep,lislr_conceXml,lislr_ut,lislr_comproContab, " +
            "lislr_fechEnterado,lislr_tarifa,lislr_rifsujRete,lislr_dirSujRete,lislr_nombSujRet) VALUES('$idPro','$emision','$docum','$sufi','$ctrl', " +
            "'$afec','$fiscal','$tip','$pcion','$monto', " +
            "'$bRetener','$bRetener','$porcReten','$sustraendo','$RetenMonto','0.00','$emision'," +
            "'$login','$periodo','$codConcepto','$XML','$UniTributaria','', " +
            "'$emision', '$tarifa','','','')";


            StrSql = StrSql.Replace("$idPro", clase.IdProveedor);
            StrSql = StrSql.Replace("$docum", clase.NroDocum);
            StrSql = StrSql.Replace("$sufi", clase.SufiDocum);
            StrSql = StrSql.Replace("$ctrl", clase.NroControlSeniat);
            StrSql = StrSql.Replace("$fiscal", clase.NroFiscal);
            StrSql = StrSql.Replace("$tip", clase.IdTipoDoc);
            StrSql = StrSql.Replace("$afec", clase.NroDocumAfec);
            StrSql = StrSql.Replace("$descrip", clase.Descripcion);
            StrSql = StrSql.Replace("$emision", clase.FechaEmision);
            StrSql = StrSql.Replace("$vencimiento", clase.FechaVenc);
            StrSql = StrSql.Replace("$libro", clase.FechaLibCompras);
            StrSql = StrSql.Replace("$retenc", clase.FechaRetenc);
            StrSql = StrSql.Replace("$base", clase.MontoBase.Replace(",", "."));
            StrSql = StrSql.Replace("$monto", clase.MontoDocum.Replace(",", "."));
            StrSql = StrSql.Replace("$login", clase.Login);
            StrSql = StrSql.Replace("$updLogin", clase.LoginUpd);

            StrSql = StrSql.Replace("$codConcepto", tabla.Rows[0][0].ToString());
            StrSql = StrSql.Replace("$pcion", tabla.Rows[0][1].ToString());
            StrSql = StrSql.Replace("$XML", tabla.Rows[0][2].ToString());
            StrSql = StrSql.Replace("$basePorc", tabla.Rows[0][7].ToString().Replace(",", "."));
            StrSql = StrSql.Replace("$porcReten", tabla.Rows[0][8].ToString().Replace(",", "."));
            StrSql = StrSql.Replace("$sustraendo", tabla.Rows[0][12].ToString().Replace(",", "."));
            StrSql = StrSql.Replace("$tarifa", tabla.Rows[0][13].ToString());

            StrSql = StrSql.Replace("$RetenMonto", montoReten.Replace(",", "."));
            StrSql = StrSql.Replace("$bRetener", baseRetener.Replace(",", "."));
            StrSql = StrSql.Replace("$UniTributaria", unidTributaria.Replace(",", "."));
            StrSql = StrSql.Replace("$periodo","" + DateTime.Now.Year.ToString() + "" + DateTime.Now.Month.ToString().PadLeft(2, '0'));

            database.ejecutarInsert(StrSql);
            database.cerrarConexion();

        }

        public DataTable consultarUTSustraendo()
        {

            StrSql = "SELECT uni_monto,uni_facsus FROM sysconf.confundtri where uni_fchgac <='$fecha' order by uni_fchgac desc limit 1";
            StrSql = StrSql.Replace("$fecha", DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day);

            tabla = database.fDataTable(StrSql);
            database.cerrarConexion();
            return tabla;
            
        }

        public DataTable consultartarifasRet(string codConcepto)
        {
            StrSql = "SELECT * FROM admtarifasreten WHERE tar_codconcep='$codConcep'";
            StrSql = StrSql.Replace("$codConcep", codConcepto);

            tabla = database.fDataTable(StrSql);
            database.cerrarConexion();
            return tabla;

        }

        public void cuentaCorrelativo()
        {
            StrSql = "SELECT tdc_correlativo,tdc_reporte FROM admtipdocpro WHERE tdc_codigo='51';";
            tabla = database.fDataTable(StrSql);
            
            if (tabla.Rows.Count > 0)
            {
                correlativoISLR = tabla.Rows[0][0].ToString();
                cuentaC = tabla.Rows[0][1].ToString();

                database.ejecutarInsert("UPDATE admtipdocpro SET tdc_correlativo=LPAD((tdc_correlativo+1),'8','0') WHERE tdc_codigo='51'");
            }

            database.cerrarConexion();
        }

        public void actualizarLibroISLR()
        {
            StrSql = "select * from admlibroislr " +
                    "where lislr_proveedor='$Codigo' " +
                    "and lislr_numFac='$NumDoc' " +
                    "and lislr_sufFac='$Sufijo' " +
                    "and lislr_tipoDocum='$TipDoc';";


            StrSql = StrSql.Replace("$Codigo", IdProveedor);
            StrSql = StrSql.Replace("$NumDoc", NroDocum);
            StrSql = StrSql.Replace("$Sufijo", SufiDocum);
            StrSql = StrSql.Replace("$TipDoc", IdTipoDoc);

            tablaISLR = database.fDataTable(StrSql);
            database.cerrarConexion();

            for (int i = 0; i < tablaISLR.Rows.Count; i++)
            {

                StrSql = "UPDATE admlibroislr SET " +
                    " lislr_fechaEmision='$1' , " +
                    " lislr_retenido=REPLACE('$2',',','.') , " +
                    " lislr_periodo='$3' ,  " +
                    " lislr_estatus='2' ,  " +
                    " lislr_correlativo='$4' , " +
                    " lislr_cantImpresiones='1' " +
                    " WHERE " +
                    " lislr_proveedor='$Codigo' " +
                    " AND lislr_numFac='$NumDoc' " +
                    " AND lislr_sufFac='$Sufijo'" +
                    " AND lislr_tipoDocum='$TipDoc'" +
                    " AND lislr_baseReten='$bRetener'" +
                    " AND lislr_porRetenc='$porcReten'" +
                    " AND lislr_estatus ='0'";

                StrSql = StrSql.Replace("$Codigo", IdProveedor);
                StrSql = StrSql.Replace("$NumDoc", NroDocum);
                StrSql = StrSql.Replace("$Sufijo", SufiDocum);
                StrSql = StrSql.Replace("$TipDoc", IdTipoDoc);
                StrSql = StrSql.Replace("$basePorc", tablaISLR.Rows[i][7].ToString().Replace(",", "."));
                StrSql = StrSql.Replace("$porcReten", tablaISLR.Rows[i][12].ToString().Replace(",", "."));
                StrSql = StrSql.Replace("$2", "" + (Convert.ToDecimal(tablaISLR.Rows[i][11].ToString()) * Convert.ToDecimal(tablaISLR.Rows[i][12].ToString()) / 100 - Convert.ToDecimal(tablaISLR.Rows[i][13].ToString())));
                StrSql = StrSql.Replace("$bRetener", tablaISLR.Rows[i][11].ToString().Replace(",", "."));
                StrSql = StrSql.Replace("$3", "" + DateTime.Now.Year.ToString() + "" + DateTime.Now.Month.ToString().PadLeft(2, '0'));
                StrSql = StrSql.Replace("$1", DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day);
                StrSql = StrSql.Replace("$4", "" + DateTime.Now.Year + "" + DateTime.Now.Month.ToString().PadLeft(2, '0') + correlativoISLR);

                database.ejecutarInsert(StrSql);
                totalRetenc = "" + (Convert.ToDecimal(totalRetenc) + Convert.ToDecimal(tablaISLR.Rows[i][11].ToString()) * Convert.ToDecimal(tablaISLR.Rows[i][12].ToString()) / 100 - Convert.ToDecimal(tablaISLR.Rows[i][13].ToString()));
            }
            database.cerrarConexion();
        }


        public void actualizarLibroISLR2()
        {

            //se creo esta para validar que solo actualice
            //el correlativo en el nuevo registro y no alteres los anteriores

            StrSql = "select * from admlibroislr " +
                    "where lislr_proveedor='$Codigo' " +
                    "and lislr_numFac='$NumDoc' " +
                    "and lislr_sufFac='$Sufijo' " +
                    "and lislr_tipoDocum='$TipDoc';";


            StrSql = StrSql.Replace("$Codigo", IdProveedor);
            StrSql = StrSql.Replace("$NumDoc", NroDocum);
            StrSql = StrSql.Replace("$Sufijo", SufiDocum);
            StrSql = StrSql.Replace("$TipDoc", IdTipoDoc);

            tablaISLR = database.fDataTable(StrSql);
            database.cerrarConexion();

            for (int i = 0; i < tablaISLR.Rows.Count; i++)
            {

                StrSql = "UPDATE admlibroislr SET " +
                    " lislr_fechaEmision='$1' , " +
                    " lislr_retenido=REPLACE('$2',',','.') , " +
                    " lislr_periodo='$3' ,  " +
                    " lislr_estatus='2' ,  " +
                    " lislr_correlativo='$4' , " +
                    " lislr_cantImpresiones='1' " +
                    " WHERE " +
                    " lislr_proveedor='$Codigo' " +
                    " AND lislr_numFac='$NumDoc' " +
                    " AND lislr_sufFac='$Sufijo'" +
                    " AND lislr_tipoDocum='$TipDoc'" +
                    " AND lislr_baseReten='$bRetener'" +
                    " AND lislr_porRetenc='$porcReten'" +
                    " AND lislr_estatus ='0'" +
                    " AND lislr_correlativo =''";

                StrSql = StrSql.Replace("$Codigo", IdProveedor);
                StrSql = StrSql.Replace("$NumDoc", NroDocum);
                StrSql = StrSql.Replace("$Sufijo", SufiDocum);
                StrSql = StrSql.Replace("$TipDoc", IdTipoDoc);
                StrSql = StrSql.Replace("$basePorc", tablaISLR.Rows[i][7].ToString().Replace(",", "."));
                StrSql = StrSql.Replace("$porcReten", tablaISLR.Rows[i][12].ToString().Replace(",", "."));
                StrSql = StrSql.Replace("$2", "" + (Convert.ToDecimal(tablaISLR.Rows[i][11].ToString()) * Convert.ToDecimal(tablaISLR.Rows[i][12].ToString()) / 100 - Convert.ToDecimal(tablaISLR.Rows[i][13].ToString())));
                StrSql = StrSql.Replace("$bRetener", tablaISLR.Rows[i][11].ToString().Replace(",", "."));
                StrSql = StrSql.Replace("$3", "" + DateTime.Now.Year.ToString() + "" + DateTime.Now.Month.ToString().PadLeft(2, '0'));
                StrSql = StrSql.Replace("$1", DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day);
                StrSql = StrSql.Replace("$4", "" + DateTime.Now.Year + "" + DateTime.Now.Month.ToString().PadLeft(2, '0') + correlativoISLR);

                database.ejecutarInsert(StrSql);
                totalRetenc = "" + (Convert.ToDecimal(totalRetenc) + Convert.ToDecimal(tablaISLR.Rows[i][11].ToString()) * Convert.ToDecimal(tablaISLR.Rows[i][12].ToString()) / 100 - Convert.ToDecimal(tablaISLR.Rows[i][13].ToString()));
            }
            database.cerrarConexion();
        }

        public void actualizarSaldoISLR()
        {


            StrSql = "UPDATE admsalpro SET " +
                    "SaldoDocum=SaldoDocum-('$totalRetenc') ," +
                    "FechaRetenc='$fecha', " +
                    "NroRetenc='$correlativo' " +
                    "WHERE " +
                    "IdProveedor='$Codigo' " +
                    "AND NroDocum='$NumDoc' " +
                    "AND SufiDocum='$Sufijo' " +
                    "AND IdTipoDoc='$TipDoc';";


            StrSql = StrSql.Replace("$Codigo", IdProveedor);
            StrSql = StrSql.Replace("$NumDoc", NroDocum);
            StrSql = StrSql.Replace("$Sufijo", SufiDocum);
            StrSql = StrSql.Replace("$TipDoc", IdTipoDoc);
            StrSql = StrSql.Replace("$totalRetenc", Convert.ToString(Math.Round(Convert.ToDecimal(totalRetenc),2)).Replace(",", "."));
            StrSql = StrSql.Replace("$fecha", DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day);
            StrSql = StrSql.Replace("$correlativo", "" + DateTime.Now.Year + "" + DateTime.Now.Month.ToString().PadLeft(2, '0') + correlativoISLR);

            database.ejecutarInsert(StrSql);
            database.cerrarConexion();
        }


        public void actualizarSaldoISLR2()
        {
            //se creo esta para que actualice el status a 1

            StrSql = "UPDATE admsalpro SET " +
                    "SaldoDocum=SaldoDocum-('$totalRetenc') ," +
                    "FechaRetenc='$fecha', " +
                    "NroRetenc='$correlativo', " +
                    "Status='1' " +
                    " WHERE " +
                    "IdProveedor='$Codigo' " +
                    "AND NroDocum='$NumDoc' " +
                    "AND SufiDocum='$Sufijo' " +
                    "AND IdTipoDoc='$TipDoc';";


            StrSql = StrSql.Replace("$Codigo", IdProveedor);
            StrSql = StrSql.Replace("$NumDoc", NroDocum);
            StrSql = StrSql.Replace("$Sufijo", SufiDocum);
            StrSql = StrSql.Replace("$TipDoc", IdTipoDoc);
            StrSql = StrSql.Replace("$totalRetenc", Convert.ToString(Math.Round(Convert.ToDecimal(totalRetenc), 2)).Replace(",", "."));
            StrSql = StrSql.Replace("$fecha", DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day);
            StrSql = StrSql.Replace("$correlativo", "" + DateTime.Now.Year + "" + DateTime.Now.Month.ToString().PadLeft(2, '0') + correlativoISLR);

            database.ejecutarInsert(StrSql);
            database.cerrarConexion();
        }

        public bool habilitarBoton()
        {
            StrSql = "SELECT *  FROM admlibroislr JOIN admproveedor " +
                    " ON admproveedor.pro_codigo = admlibroislr.lislr_proveedor " +
                    " WHERE lislr_tipoDocum='$TipDoc' " +
                    " AND lislr_numFac='$NumDoc' AND lislr_sufFac='$Sufijo' AND  lislr_proveedor='$Codigo' ";

            StrSql = StrSql.Replace("$Codigo", IdProveedor);
            StrSql = StrSql.Replace("$NumDoc", NroDocum);
            StrSql = StrSql.Replace("$Sufijo", SufiDocum);
            StrSql = StrSql.Replace("$TipDoc", IdTipoDoc);

            tabla = database.fDataTable(StrSql);
            database.cerrarConexion();

            if (tabla.Rows.Count > 0)
            {
                if (tabla.Rows[0][28].ToString() == "2" || tabla.Rows[0][28].ToString() == "4")
                {
                    return false;
                }

                return true;
            }

            return false;
        }

        public DataTable armarDetallesMovContab()
        {
            consultarCuentaPro();
            datosComprobante();
            tablaMovContab = movCon.armarDataTableMovContab();

            for (int i = 0; i < tablaISLR.Rows.Count; i++)
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
                fila[1] = idProveedor;
                fila[2] = nroDocum; //recibo
                fila[3] = sufiDocum;
                fila[4] = IdTipoDoc;
                fila[5] = "" + this.comprobante;          //COMPROBANTE
                fila[6] = this.descripcion;  //descripcion
                fila[7] = this.cuentaPro; //mayor proveedor                              //cc.CuentaProveedor;//CODIGO CUENTA
                fila[8] = numero; //-1 o 1
                fila[9] = "";                                   //AQUI NO VA UN COÑO
                fila[10] = "" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString().PadLeft(2, '0') + "-" + DateTime.Now.Day.ToString().PadLeft(2, '0');
                fila[11] = "" + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString().PadLeft(2, '0');//me da lala esta parte copy and paste                                          //hora
                fila[12] = "" + tablaISLR.Rows[i][14].ToString().Replace(",", "."); //monto
                fila[13] = "1";
                fila[14] = login;
                fila[15] = "RTI";
                fila[16] = "" + auxiliarPro;     //auxiliar                         //AUXILIAR  PROVEEDOR                                 
                fila[17] = this.idProveedor;                  //CODIGO
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
                fila2[1] = idProveedor;
                fila2[2] = nroDocum; //recibo
                fila2[3] = sufiDocum;
                fila2[4] = IdTipoDoc;
                fila2[5] = "" + this.comprobante;          //COMPROBANTE
                fila2[6] = this.descripcion;  //descripcion
                fila2[7] = cuentaC; //mayor proveedor                              //cc.CuentaProveedor;//CODIGO CUENTA
                fila2[8] = numero; //-1 o 1
                fila2[9] = "";                                   //AQUI NO VA UN COÑO
                fila2[10] = "" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString().PadLeft(2, '0') + "-" + DateTime.Now.Day.ToString().PadLeft(2, '0');
                fila2[11] = "" + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString().PadLeft(2, '0');//me da lala esta parte copy and paste                                          //hora
                fila2[12] = "" + tablaISLR.Rows[i][14].ToString().Replace(",", ".");
                fila2[13] = "1";
                fila2[14] = login;
                fila2[15] = "RTI";
                fila2[16] = "" + auxiliarPro;     //auxiliar                         //AUXILIAR  PROVEEDOR                                 
                fila2[17] = this.idProveedor;                  //CODIGO
                fila2[18] = this.nombre;               //NOMBRE                      
                fila2[19] = letra;//letra D o C
                fila2[20] = "";
                fila2[21] = ""; // interno
                fila2[22] = ""; // interno

                tablaMovContab.Rows.Add(fila2);
            }

            return tablaMovContab;

        }


        public DataTable armarDetallesMovContabISLR(string comprobanteISLR)
        {
            consultarCuentaPro();
            datosComprobante();
            tablaMovContab = movCon.armarDataTableMovContab();

            for (int i = 0; i < tablaISLR.Rows.Count; i++)
            {

                this.items = i + 1;
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
                fila[1] = idProveedor;
                fila[2] = nroDocum; //recibo
                fila[3] = sufiDocum;
                fila[4] = IdTipoDoc;
                fila[5] = "" + comprobanteISLR;          //COMPROBANTE
                fila[6] = this.descripcion;  //descripcion
                fila[7] = this.cuentaPro; //mayor proveedor                              //cc.CuentaProveedor;//CODIGO CUENTA
                fila[8] = numero; //-1 o 1
                fila[9] = "";                                   //AQUI NO VA UN COÑO
                fila[10] = "" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString().PadLeft(2, '0') + "-" + DateTime.Now.Day.ToString().PadLeft(2, '0');
                fila[11] = "" + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString().PadLeft(2, '0');//me da lala esta parte copy and paste                                          //hora
                fila[12] = "" + tablaISLR.Rows[i][14].ToString().Replace(",", "."); //monto
                fila[13] = "1";
                fila[14] = login;
                fila[15] = "ISLR";
                fila[16] = "" + auxiliarPro;     //auxiliar                         //AUXILIAR  PROVEEDOR                                 
                fila[17] = this.idProveedor;                  //CODIGO
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
                fila2[1] = idProveedor;
                fila2[2] = nroDocum; //recibo
                fila2[3] = sufiDocum;
                fila2[4] = IdTipoDoc;
                fila2[5] = "" + comprobanteISLR;          //COMPROBANTE
                fila2[6] = this.descripcion;  //descripcion
                fila2[7] = cuentaC; //mayor proveedor                              //cc.CuentaProveedor;//CODIGO CUENTA
                fila2[8] = numero; //-1 o 1
                fila2[9] = "";                                   //AQUI NO VA UN COÑO
                fila2[10] = "" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString().PadLeft(2, '0') + "-" + DateTime.Now.Day.ToString().PadLeft(2, '0');
                fila2[11] = "" + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString().PadLeft(2, '0');//me da lala esta parte copy and paste                                          //hora
                fila2[12] = "" + tablaISLR.Rows[i][14].ToString().Replace(",", ".");
                fila2[13] = "1";
                fila2[14] = login;
                fila2[15] = "ISLR";
                fila2[16] = "" + auxiliarPro;     //auxiliar                         //AUXILIAR  PROVEEDOR                                 
                fila2[17] = this.idProveedor;                  //CODIGO
                fila2[18] = this.nombre;               //NOMBRE                      
                fila2[19] = letra;//letra D o C
                fila2[20] = "";
                fila2[21] = ""; // interno
                fila2[22] = ""; // interno

                tablaMovContab.Rows.Add(fila2);
            }

            return tablaMovContab;

        }

        public void datosComprobante()
        {
            StrSql = " SELECT movcon_descrip,movcon_numcomp,tdc_correlativo,movcon_nombre,movcon_rif,movcon_monto FROM admmovcontab " +
            "LEFT JOIN admtipdocpro ON movcon_tipdoc=tdc_codigo " +
            "WHERE movcon_tipdoc='$tipoDoc' AND movcon_sufdoc ='$sufijo' " +
            " AND movcon_proveedor='$proveedor' AND movcon_numdoc='$documento' ORDER BY movcon_item DESC";

            StrSql = StrSql.Replace("$proveedor", IdProveedor);
            StrSql = StrSql.Replace("$documento", NroDocum);
            StrSql = StrSql.Replace("$sufijo", SufiDocum);
            StrSql = StrSql.Replace("$tipoDoc", IdTipoDoc);

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

            tabla = movCon.cuentaTipDocPro2(this.idTipoDoc, this.idProveedor);

            if (tabla.Rows.Count > 0)
            {
                this.cuentaPro = tabla.Rows[0]["n_cuenta"].ToString();
                this.auxiliarPro = tabla.Rows[0]["n_auxiliar"].ToString();
            }

        }
        public void actualizarCabeceraComprobante()
        {
            database.conectionStringSysconta(compania.BaseDatosActual);

            StrSql = "UPDATE admmovimientoscontablesc SET mc_MontoDebitos=mc_MontoDebitos +'$totalReten', " +
            "mc_MontoCreditos=mc_MontoCreditos + '$totalReten'   WHERE mc_nroComprobante='$comprobante'";

            StrSql = StrSql.Replace("$totalReten", this.totalRetenc.Replace(",", "."));
            StrSql = StrSql.Replace("$comprobante", this.comprobante);

            database.ejecutarInsert(StrSql);
            database.modificarConexionString(1);
            database.cerrarConexion();
        }

        public void ingresarMovContab()
        {
            movCon.ingresarMovContab(tablaMovContab);
            movCon.detallesSyscontab(tablaMovContab, compania.BaseDatosActual);
            actualizarCabeceraComprobante();

        }

        public void ingresarMovContabISLR()
        {
            movCon.ingresarMovContab(tablaMovContab);
            movCon.detallesSyscontab(tablaMovContab, compania.BaseDatosActual);
            
        }

        public void ingresarCabeceraISLR(DataTable tablaCabecera)
        {
            movCon.cabeceraSyscontab(tablaCabecera, compania.BaseDatosActual);
        }

        public DataTable ImprimirISLR(string codigo, string nroDoc, string sufijo, string tipoDoc)
        {

            StrSql = "SELECT lislr_proveedor,lislr_fechaEmisionFac,	lislr_numFac,lislr_sufFac,lislr_numCtrlFac," +
                        "lislr_facAfec,lislr_ctrlFacAfec,lislr_tipoDocum,lislr_descripcion,lislr_totalFac,lislr_totalBase " +
                        ",lislr_baseReten,lislr_porRetenc,lislr_sustraendo,lislr_retenido,lislr_factor,lislr_fechaEmision " +
                        ",lislr_usuario,lislr_periodo,lislr_codConcep,lislr_conceXml,lislr_ut ,lislr_comproContab,lislr_fechEnterado " +
                        ",lislr_tarifa,lislr_rifsujRete,lislr_dirSujRete,lislr_nombSujRet,lislr_estatus,lislr_correlativo " +
                        ",lislr_cantImpresiones,pro_direcc1,pro_nombre,pro_rif " +
                        "FROM " +
                        "admlibroislr " +
                        "LEFT OUTER JOIN " +
                        "admproveedor ON admproveedor.pro_codigo = admlibroislr.lislr_proveedor " +
                        "WHERE " +
                        "lislr_proveedor='$proveedor' " +
                        "AND lislr_numFac='$documento' " +
                        "AND lislr_sufFac='$sufijo' " +
                        "AND lislr_tipoDocum='$tipoDoc';";


            StrSql = StrSql.Replace("$proveedor", codigo);
            StrSql = StrSql.Replace("$documento", nroDoc);
            StrSql = StrSql.Replace("$sufijo", sufijo);
            StrSql = StrSql.Replace("$tipoDoc", tipoDoc);

            tabla = database.fDataTable(StrSql);
            database.cerrarConexion();
            return tabla;


        }

        public void anularRetencionISLR(string docu, string suf, string tipo, string pro)
        {
            StrSql = "UPDATE admlibroislr SET admlibroislr.lislr_estatus='4' " +
                    " WHERE admlibroislr.lislr_proveedor='$4' " +
                    " AND admlibroislr.lislr_numFac='$1' " +
                    " AND admlibroislr.lislr_sufFac='$2' " +
                    " AND admlibroislr.lislr_tipoDocum='$3' ";

            StrSql = StrSql.Replace("$1", docu);
            StrSql = StrSql.Replace("$2", suf);
            StrSql = StrSql.Replace("$3", tipo);
            StrSql = StrSql.Replace("$4", pro);

            database.ejecutarInsert(StrSql);
            database.cerrarConexion();
        }

        public bool verificarExistenciaRet(string docu, string suf, string tipo, string pro)
        {
            StrSql = "SELECT lislr_estatus FROM admlibroislr " +
                    "WHERE admlibroislr.lislr_numFac='$1' " +
                    "AND admlibroislr.lislr_sufFac='$2' " +
                    "AND admlibroislr.lislr_tipoDocum='$3' " +
                    "AND admlibroislr.lislr_proveedor='$4' ";

            StrSql = StrSql.Replace("$1", docu);
            StrSql = StrSql.Replace("$2", suf);
            StrSql = StrSql.Replace("$3", tipo);
            StrSql = StrSql.Replace("$4", pro);

            tabla = database.fDataTable(StrSql);

            if (tabla.Rows.Count > 0)
            {
                //if (tabla.Rows[0][0].ToString() == "2")
                //{
                //    return true;
                //}
                return true;
            }

            return false;
        }

        





    }
}
