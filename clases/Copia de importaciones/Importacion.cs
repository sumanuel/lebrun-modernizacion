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
    class Importacion
    {

        private string StrSql;
        private ConexionBD database;
        private DataSet ds;
        private MySqlDataReader dr;
        
        //declarar todas las variables a usar en la cabecera
        private string concaEstatus;
        private string pro_nombre;
        private string pro_rif;
        private string codProveedor;
        private string factorCif;
        private string factorgastos;
        private string factorX;
        private string documento;
        private string sufijo;
        private string montoFob;
        private string fleteFob;
        private string seguroFob;
        private string costoMaterialFob;
        private string otros1Fob;
        private string otros2Fob;
        private string montoBs;
        private string fleteBs;
        private string seguroBs;
        private string costoMaterialBs;
        private string otros1Bs;
        private string otros2Bs;
        private string cambioOc;
        private string cambioLiqui;
        private string cambioProducto;
        private string fechaimport;
        private string comprobante;
        private string IdSistemasInt;
        private string tablaBD;
        private string tablaBD2;
        private string totalCif;
        private string costoCif;
        private string verificaMercan;
        private string honorarios;
        private string gastoCadivi;
        private string segurosMercancia;
        private string otrosGastos;
        private string gastAduanaOtros;
        private string gastosGenerales;
        private string valorImportacion;
        private string factorGastImpor;
        private string moneda;
        private string ivaNacional;
        private string ivaImportacion;
        private string derechosImportacion;
        private string fechaSistema;

        private string controlImportacion;
        private string montoBaseImport;
        private string montoExenImport;
        private string montoTotalImpor;
        private string factNacional;
        private string sufiNacional;
        private string controlNacional;
        private string fechEmisionNacio;
        private string fechVencNacio;
        private string baseNacional;
        private string exentoNacional;
        private string totalNacional;
        private string proveNacional;

        //nuevos campos
        private string codicionPago;
        private string ExpNumMilco;
        private string fechaSolicMilco;
        private string fechaAutoMilco;
        private string fechaVencMilco;
        private string numImpCadivi;
        private string fechSoliCadivi;
        private string fechAutoCadivi;
        private string fechAAD;
        private string fechVENAAD;
        private string montoAutoCadivi;
        private string monedaCadivi;
        private string fechVencCadivi;
        private string fechEmbarque;
        private string ciudad;
        private string Vapor;
        private string BLNum;
        private string fechaVeri;
        private string transportista;
        private string agente;
        private string formaEmbalaje;
        private string cantidadBultos;
        private string fechLlegada;
        private string consignatario;
        private string numExpediente;
        private string numFactura;
        private string fechFactura;
        private string tasaCambio;
        private string fechReciboActa;
        private string fechDocuALD;
        private string fechOtorgaALD;
        private string fechTransfPago;
        private string cambioPermuta;




        // get & set

        public string Pro_nombre
        {
            get { return pro_nombre; }
            set { pro_nombre = value; }
        }

        public string Pro_rif
        {
            get { return pro_rif; }
            set { pro_rif = value; }
        }

        public string CodProveedor
        {
            get { return codProveedor; }
            set { codProveedor = value; }
        }

        public string FactorCif
        {
            get { return factorCif; }
            set { factorCif = value; }
        }
        
        public string Factorgastos
        {
            get { return factorgastos; }
            set { factorgastos = value; }
        }

        public string FactorX
        {
            get { return factorX; }
            set { factorX = value; }
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

        public string MontoFob
        {
            get { return montoFob; }
            set { montoFob = value; }
        }

        public string FleteFob
        {
            get { return fleteFob; }
            set { fleteFob = value; }
        }

        public string SeguroFob
        {
            get { return seguroFob; }
            set { seguroFob = value; }
        }

        public string CostoMaterialFob
        {
            get { return costoMaterialFob; }
            set { costoMaterialFob = value; }
        }

        public string Otros1Fob
        {
            get { return otros1Fob; }
            set { otros1Fob = value; }
        }

        public string Otros2Fob
        {
            get { return otros2Fob; }
            set { otros2Fob = value; }
        }

        public string MontoBs
        {
            get { return montoBs; }
            set { montoBs = value; }
        }

        public string FleteBs
        {
            get { return fleteBs; }
            set { fleteBs = value; }
        }

        public string SeguroBs
        {
            get { return seguroBs; }
            set { seguroBs = value; }
        }

        public string CostoMaterialBs
        {
            get { return costoMaterialBs; }
            set { costoMaterialBs = value; }
        }

        public string Otros1Bs
        {
            get { return otros1Bs; }
            set { otros1Bs = value; }
        }

        public string Otros2Bs
        {
            get { return otros2Bs; }
            set { otros2Bs = value; }
        }

        public string CambioOc
        {
            get { return cambioOc; }
            set { cambioOc = value; }
        }

        public string CambioLiqui
        {
            get { return cambioLiqui; }
            set { cambioLiqui = value; }
        }

        public string CambioProducto
        {
            get { return cambioProducto; }
            set { cambioProducto = value; }
        }

        public string Fechaimport
        {
            get { return fechaimport; }
            set { fechaimport = value; }
        }

        public string Comprobante
        {
            get { return comprobante; }
            set { comprobante = value; }
        }

        public string IdSistemasInt1
        {
            get { return IdSistemasInt; }
            set { IdSistemasInt = value; }
        }

        public string TotalCif
        {
            get { return totalCif; }
            set { totalCif = value; }
        }

        public string CostoCif
        {
            get { return costoCif; }
            set { costoCif = value; }
        }

        public string VerificaMercan
        {
            get { return verificaMercan; }
            set { verificaMercan = value; }
        }

        public string Honorarios
        {
            get { return honorarios; }
            set { honorarios = value; }
        }

        public string GastoCadivi
        {
            get { return gastoCadivi; }
            set { gastoCadivi = value; }
        }

        public string SegurosMercancia
        {
            get { return segurosMercancia; }
            set { segurosMercancia = value; }
        }

        public string OtrosGastos
        {
            get { return otrosGastos; }
            set { otrosGastos = value; }
        }

        public string GastAduanaOtros
        {
            get { return gastAduanaOtros; }
            set { gastAduanaOtros = value; }
        }

        public string GastosGenerales
        {
            get { return gastosGenerales; }
            set { gastosGenerales = value; }
        }

        public string ValorImportacion
        {
            get { return valorImportacion; }
            set { valorImportacion = value; }
        }

        public string FactorGastImpor
        {
            get { return factorGastImpor; }
            set { factorGastImpor = value; }
        }

        public string Moneda
        {
            get { return moneda; }
            set { moneda = value; }
        }

        public string IvaNacional
        {
            get { return ivaNacional; }
            set { ivaNacional = value; }
        }

        public string IvaImportacion
        {
            get { return ivaImportacion; }
            set { ivaImportacion = value; }
        }

        public string DerechosImportacion
        {
            get { return derechosImportacion; }
            set { derechosImportacion = value; }
        }

        public string FechaSistema
        {
            get { return fechaSistema; }
            set { fechaSistema = value; }
        }

        public string ControlImportacion
        {
            get { return controlImportacion; }
            set { controlImportacion = value; }
        }

        public string MontoBaseImport
        {
            get { return montoBaseImport; }
            set { montoBaseImport = value; }
        }

        public string MontoExenImport
        {
            get { return montoExenImport; }
            set { montoExenImport = value; }
        }

        public string MontoTotalImpor
        {
            get { return montoTotalImpor; }
            set { montoTotalImpor = value; }
        }

        public string FactNacional
        {
            get { return factNacional; }
            set { factNacional = value; }
        }

        public string SufiNacional
        {
            get { return sufiNacional; }
            set { sufiNacional = value; }
        }

        public string ControlNacional
        {
            get { return controlNacional; }
            set { controlNacional = value; }
        }

        public string FechEmisionNacio
        {
            get { return fechEmisionNacio; }
            set { fechEmisionNacio = value; }
        }

        public string FechVencNacio
        {
            get { return fechVencNacio; }
            set { fechVencNacio = value; }
        }

        public string BaseNacional
        {
            get { return baseNacional; }
            set { baseNacional = value; }
        }

        public string ExentoNacional
        {
            get { return exentoNacional; }
            set { exentoNacional = value; }
        }

        public string TotalNacional
        {
            get { return totalNacional; }
            set { totalNacional = value; }
        }

        public string ProveNacional
        {
            get { return proveNacional; }
            set { proveNacional = value; }
        }

        //nuevos get & set

        public string CodicionPago
        {
            get { return codicionPago; }
            set { codicionPago = value; }
        }

        public string ExpNumMilco1
        {
            get { return ExpNumMilco; }
            set { ExpNumMilco = value; }
        }

        public string FechaSolicMilco
        {
            get { return fechaSolicMilco; }
            set { fechaSolicMilco = value; }
        }

        public string FechaAutoMilco
        {
            get { return fechaAutoMilco; }
            set { fechaAutoMilco = value; }
        }

        public string FechaVencMilco
        {
            get { return fechaVencMilco; }
            set { fechaVencMilco = value; }
        }

        public string NumImpCadivi
        {
            get { return numImpCadivi; }
            set { numImpCadivi = value; }
        }

        public string FechSoliCadivi
        {
            get { return fechSoliCadivi; }
            set { fechSoliCadivi = value; }
        }

        public string FechAutoCadivi
        {
            get { return fechAutoCadivi; }
            set { fechAutoCadivi = value; }
        }

        public string FechAAD
        {
            get { return fechAAD; }
            set { fechAAD = value; }
        }

        public string FechVENAAD
        {
            get { return fechVENAAD; }
            set { fechVENAAD = value; }
        }

        public string MontoAutoCadivi
        {
            get { return montoAutoCadivi; }
            set { montoAutoCadivi = value; }
        }

        public string MonedaCadivi
        {
            get { return monedaCadivi; }
            set { monedaCadivi = value; }
        }

        public string FechVencCadivi
        {
            get { return fechVencCadivi; }
            set { fechVencCadivi = value; }
        }

        public string FechEmbarque
        {
            get { return fechEmbarque; }
            set { fechEmbarque = value; }
        }

        public string Ciudad
        {
            get { return ciudad; }
            set { ciudad = value; }
        }

        public string Vapor1
        {
            get { return Vapor; }
            set { Vapor = value; }
        }

        public string BLNum1
        {
            get { return BLNum; }
            set { BLNum = value; }
        }

        public string FechaVeri
        {
            get { return fechaVeri; }
            set { fechaVeri = value; }
        }
        public string Transportista
        {
            get { return transportista; }
            set { transportista = value; }
        }

        public string Agente
        {
            get { return agente; }
            set { agente = value; }
        }

        public string FormaEmbalaje
        {
            get { return formaEmbalaje; }
            set { formaEmbalaje = value; }
        }

        public string CantidadBultos
        {
            get { return cantidadBultos; }
            set { cantidadBultos = value; }
        }

        public string FechLlegada
        {
            get { return fechLlegada; }
            set { fechLlegada = value; }
        }

        public string Consignatario
        {
            get { return consignatario; }
            set { consignatario = value; }
        }

        public string NumExpediente
        {
            get { return numExpediente; }
            set { numExpediente = value; }
        }

        public string NumFactura
        {
            get { return numFactura; }
            set { numFactura = value; }
        }

        public string FechFactura
        {
            get { return fechFactura; }
            set { fechFactura = value; }
        }

        public string TasaCambio
        {
            get { return tasaCambio; }
            set { tasaCambio = value; }
        }

        public string FechReciboActa
        {
            get { return fechReciboActa; }
            set { fechReciboActa = value; }
        }

        public string FechDocuALD
        {
            get { return fechDocuALD; }
            set { fechDocuALD = value; }
        }

        public string FechOtorgaALD
        {
            get { return fechOtorgaALD; }
            set { fechOtorgaALD = value; }
        }

        public string FechTransfPago
        {
            get { return fechTransfPago; }
            set { fechTransfPago = value; }
        }

        public string CambioPermuta
        {
            get { return cambioPermuta; }
            set { cambioPermuta = value; }
        }

        // fin nuevos get & set

        // continue

        public Importacion() {
            database = new ConexionBD();
           
        }

        public DataTable CargarImportaciones()
        {
            StrSql = "SELECT admDocumen 'Documento',admSufijo 'Sufijo',admCodProveedor 'Codigo Proveedor',pro_nombre 'Nombre Proveedor',admMontoFOB 'Monto FOB',admfechaImportacion 'Fecha', 'En Carga' Estatus  FROM admcabeceradocumento_temp LEFT OUTER JOIN admproveedor ON admCodProveedor=pro_codigo";
            return database.fDataTable(StrSql); //asignar a otro datatable para poder cerrar la conexion
            //database.cerrarConexion(); 

        }

        public DataTable Cargarimportaciones2() {

            StrSql = "SELECT admDocumen 'Documento',admSufijo 'Sufijo',admCodProveedor 'Codigo Proveedor',pro_nombre 'Nombre Proveedor',admMontoFOB 'Monto FOB',admfechaImportacion 'Fecha', 'Liquidada' Estatus  FROM admcabeceradocumento LEFT OUTER JOIN admproveedor ON admCodProveedor=pro_codigo";
            return database.fDataTable(StrSql);
        }


        //aqui funcion para armar la cabecera
        public void cargarCabeceraImportacion(string doc, string suf, int opc,string prove) {

            //database.modificarConexionString2(1);

            if (opc == 1) { tablaBD = "admcabeceradocumento"; }
            if (opc == 2) { tablaBD = "admcabeceradocumento_temp"; }

            StrSql = "SELECT * FROM " + tablaBD + " LEFT OUTER JOIN admproveedor ON admCodProveedor=pro_codigo WHERE admDocumen='$1' AND admSufijo='$2' AND admCodProveedor='$3'";
            StrSql = StrSql.Replace("$1", doc);
            StrSql = StrSql.Replace("$2", suf);
            StrSql = StrSql.Replace("$3", prove);


            dr = database.ejecutarQueryDr(StrSql);

            if (dr.HasRows)
            {
                dr.Read();

                this.documento = dr.GetString("admDocumen");
                this.sufijo = dr.GetString("admSufijo");
                this.factorCif = dr.GetString("admFactorCif");
                this.factorgastos = dr.GetString("admFactorGasto");
                this.factorX = dr.GetString("admFactorX");
                this.montoFob = dr.GetString("admMontoFOB");
                this.fleteFob = dr.GetString("admFleteMaritFOB");
                this.seguroFob = dr.GetString("admSegTransFOB");
                this.costoMaterialFob = dr.GetString("admCostoMateFOB");
                this.otros1Fob = dr.GetString("admOtros1FOB");
                this.otros2Fob = dr.GetString("admOtros2FOB");
                this.montoBs = dr.GetString("admMontoBs");
                this.fleteBs = dr.GetString("admFleteMaritBs");
                this.seguroBs = dr.GetString("admSegTransBs");
                this.costoMaterialBs = dr.GetString("admCostoMateBs");
                this.otros1Bs = dr.GetString("admOtros1Bs");
                this.otros2Bs = dr.GetString("admOtros2Bs");
                this.cambioOc = dr.GetString("admCambioOC");
                this.cambioLiqui = dr.GetString("admCambioLiqui");
                this.cambioProducto = dr.GetString("admCambioProductos");
                this.fechaimport = dr.GetString("admfechaImportacion");
                this.CostoCif = dr.GetString("admCostoCif");
                this.totalCif = dr.GetString("admTotalCif");
                this.verificaMercan = dr.GetString("otrVerifiMercancia");
                this.honorarios = dr.GetString("otrHonorarios");
                this.gastoCadivi = dr.GetString("otrGastCadivi");
                this.segurosMercancia = dr.GetString("otrSeguroMerca");
                this.otrosGastos = dr.GetString("otrOtros");
                this.GastAduanaOtros = dr.GetString("GastAduanaOtros");
                this.gastosGenerales = dr.GetString("gastosGenerales");
                this.valorImportacion = dr.GetString("valorImportacion");
                this.factorGastImpor = dr.GetString("factorGastImpor");
                this.codProveedor = dr.GetString("admCodProveedor");
                this.pro_nombre = dr.GetString("pro_nombre");
                this.pro_rif = dr.GetString("pro_rif");
                this.moneda = dr.GetString("moneda");
                this.ivaImportacion = dr.GetString("iva_importacion");
                this.ivaNacional = dr.GetString("iva_nacional");
                this.derechosImportacion = dr.GetString("derechosImportacion");
                this.fechaSistema = dr.GetString("fechaSistema");
                this.controlImportacion = dr.GetString("controlImportacion");
                this.montoBaseImport = dr.GetString("montoBaseImport");
                this.montoExenImport = dr.GetString("montoExenImport");
                this.montoTotalImpor = dr.GetString("montoTotalImpor");
                this.factNacional = dr.GetString("factNacional");
                this.sufiNacional = dr.GetString("sufiNacional");
                this.controlNacional = dr.GetString("controlNacional");
                this.fechEmisionNacio = dr.GetString("fechEmisionNacio");
                this.fechVencNacio = dr.GetString("fechVencNacio");
                this.baseNacional = dr.GetString("baseNacional");
                this.exentoNacional = dr.GetString("exentoNacional");
                this.totalNacional = dr.GetString("totalNacional");
                this.proveNacional = dr.GetString("proveNacional");


                this.ExpNumMilco= dr.GetString("ExpNumMilco");
                this.fechaSolicMilco= dr.GetString("fechaSolicMilco");

                this.fechaAutoMilco= dr.GetString("fechaAutoMilco");
                this.fechaVencMilco= dr.GetString("fechaVencMilco");
                this.numImpCadivi= dr.GetString("numImpCadivi");
                this.fechSoliCadivi= dr.GetString("fechSoliCadivi");
                this.fechAutoCadivi= dr.GetString("fechAutoCadivi");
                this.fechAAD= dr.GetString("fechAAD");
                this.fechVENAAD= dr.GetString("fechVENAAD");
                this.montoAutoCadivi= dr.GetString("montoAutoCadivi");
                this.monedaCadivi= dr.GetString("monedaCadivi");
                this.fechVencCadivi= dr.GetString("fechVencCadivi");
                this.fechEmbarque= dr.GetString("fechEmbarque");
                this.ciudad= dr.GetString("ciudad");
                this.Vapor= dr.GetString("Vapor");
                this.BLNum= dr.GetString("BLNum");
                this.fechaVeri= dr.GetString("fechaVeri");
                this.transportista= dr.GetString("transportista");
                this.agente= dr.GetString("agente");
                this.formaEmbalaje= dr.GetString("formaEmbalaje");
                this.cantidadBultos= dr.GetString("cantidadBultos");
                this.fechLlegada= dr.GetString("fechLlegada");
                this.consignatario= dr.GetString("consignatario");
                this.numExpediente= dr.GetString("numExpediente");
                this.numFactura= dr.GetString("numFactura");
                this.fechFactura = dr.GetString("fechFactura");
                this.tasaCambio= dr.GetString("tasaCambio");
                this.fechReciboActa= dr.GetString("fechReciboActa");
                this.fechDocuALD= dr.GetString("fechDocuALD");
                this.fechOtorgaALD= dr.GetString("fechOtorgaALD");
                this.fechTransfPago= dr.GetString("fechTransfPago");
                this.cambioPermuta = dr.GetString("cambioPermuta");


    //controlImportacion,montoBaseImport,montoExenImport,montoTotalImpor,factNacional,sufiNacional` 
    //controlNacional,fechEmisionNacio,fechVencNaciobaseNacional,exentoNacional,totalNacional` 


            }

            database.cerrarConexion();
            dr.Close();
            database.modificarConexionString(1);
        }



        //aqui funcion cargar productos

        public DataTable cargarProductosImportacion(string doc, string suf, int opc) {

            if (opc == 1) { tablaBD = "admcalcprodimpor"; }
            if (opc == 2) { tablaBD = "admcalcprodimpor_temp"; }

            StrSql = "SELECT codigo,descripcion,unidad,cantidad,precioFOB,c,d,e,f,g,h,i,j,k,l FROM " + tablaBD + " WHERE documento='$1' AND sufijo='$2' ";
            StrSql = StrSql.Replace("$1",doc);
            StrSql = StrSql.Replace("$2",suf);
            return database.fDataTable(StrSql);        
        }


        //aqui funcion con datatable para armar gastos

        public DataTable cargarGastosImportaciones(string doc, string suf, int opc) {

            if (opc == 1) { tablaBD = "admgasfacimpor"; }
            if (opc == 2) { tablaBD = "admgasfacimpor_temp"; }

            StrSql = "SELECT * FROM " + tablaBD + " WHERE gfi_documen='$1' AND gfi_sufijo='$2'";
            StrSql = StrSql.Replace("$1",doc);
            StrSql = StrSql.Replace("$2",suf);

            return database.fDataTable(StrSql);
        }


        public void modificarImportaciones(string doc, string suf, string prove)
        {
            //BORRAR TODO TODO TODO TODO

            //StrSql = "DELETE FROM admmovcontab_temp WHERE movcon_numdoc='$1' AND movcon_sufdoc='$2' ";
            //StrSql = "DELETE FROM admgasfacimpor_temp WHERE gfi_documen='$1' AND gfi_sufijo='$2' ";
            //StrSql = "DELETE FROM adminvmov_temp WHERE mov_docume='$1' AND mov_sufijo='$2' ";
            //StrSql = "DELETE FROM admcabeceradocumento_temp WHERE admDocumen='$1' AND admSufijo='$2' ";
            //StrSql = "DELETE FROM admcalcprodimpor_temp WHERE documento='$1' AND sufijo='$2'";

            StrSql = "DELETE FROM admmovcontab_temp WHERE movcon_numdoc='$1' AND movcon_sufdoc='$2' AND movcon_proveedor='$3';" +
                "DELETE FROM admgasfacimpor_temp WHERE gfi_documen='$1' AND gfi_sufijo='$2' AND gfi_proveedor='$3';" +
                "DELETE FROM adminvmov_temp WHERE mov_docume='$1' AND mov_sufijo='$2' AND mov_codcta='$3';" +
                "DELETE FROM admcabeceradocumento_temp WHERE admDocumen='$1' AND admSufijo='$2' AND admCodProveedor='$3';" +
                "DELETE FROM admcalcprodimpor_temp WHERE documento='$1' AND sufijo='$2' AND codigoProveedor='$3';";

            StrSql = StrSql.Replace("$1", doc);
            StrSql = StrSql.Replace("$2", suf);
            StrSql = StrSql.Replace("$3", prove);

            database.ejecutarInsert(StrSql);

        }

        public void guardarMovContab(DataTable tabla, int opc) 
        {
            if (opc == 1) { tablaBD = "admmovcontab"; }
            if (opc == 2) { tablaBD = "admmovcontab_temp"; };

            StrSql = null;
            string[] parametros = new string[20];
            StrSql = "INSERT INTO " + tablaBD + " (movcon_item,movcon_proveedor,movcon_numdoc,movcon_sufdoc," +
                        "movcon_tipdoc,movcon_numcomp,movcon_descrip,movcon_cuenta,movcon_tipo,movcon_basetip,movcon_fecaha," +
                        "movcon_hora,movcon_monto,movcon_status,movcon_login,idSistemas,movcon_auxiliar,movcon_rif,movcon_nombre,codigoIslr)" +
                        "VALUES(@item,@codprove,@numeroDoc,@sufiDoc,@tipoDoc,@numComprobante,@descripcion,@cuenta,@tipo,@baseTipo, " +
                        "@fecha,@hora,@monto,'1',@login,@idSistemas,@auxiliar,@rif,@nombre,@islr)";

            parametros[0] = "@item";
            parametros[1] = "@codprove";
            parametros[2] = "@numeroDoc";
            parametros[3] = "@sufiDoc";
            parametros[4] = "@tipoDoc";
            parametros[5] = "@numComprobante";
            parametros[6] = "@descripcion";
            parametros[7] = "@cuenta";
            parametros[8] = "@tipo";
            parametros[9] = "@baseTipo";
            parametros[10] = "@fecha";
            parametros[11] = "@hora";
            parametros[12] = "@monto";
            parametros[13] = "@status";
            parametros[14] = "@login";
            parametros[15] = "@idSistemas";
            parametros[16] = "@auxiliar";
            parametros[17] = "@rif";
            parametros[18] = "@nombre";
            parametros[19] = "@islr";

            database.insertDataTable(tabla, parametros, StrSql);
            database.cerrarConexion();
        
        }

        public void guardarGastosImportaciones(DataTable tabla, int opc)
        {
            if (opc == 1) { tablaBD = "admgasfacimpor"; }
            if (opc == 2) { tablaBD = "admgasfacimpor_temp"; };

            StrSql = null;
            string[] parametros = new string[7];

            StrSql = "INSERT INTO " + tablaBD + " (gfi_documen,gfi_sufijo,gfi_gasto, " +
            "gfi_monto,gfi_cuenta,gfi_islr,gfi_fecha,gfi_proveedor)VALUES(@documento,@sufijo,@gasto,@monto,@cuenta,@islr,@fecha,'$1')";

            StrSql = StrSql.Replace("$1", this.codProveedor);

            parametros[0] = "@documento";
            parametros[1] = "@sufijo";
            parametros[2] = "@gasto";
            parametros[3] = "@monto";
            parametros[4] = "@cuenta";
            parametros[5] = "@islr";
            parametros[6] = "@fecha";

            database.insertDataTable(tabla, parametros, StrSql);
            database.cerrarConexion();

        }

        public void guardarProveedor2(string doc, string codProve, string fecha, string facCif, string facGas, string facOtros) {

            StrSql = null;

            StrSql = "UPDATE admproveedor2 SET fec_ult_compra='$4',ult_refer_compra='$1', " +
                     "Factor_CIF='$5',Factor_GAS='$6', "+
                     "Factor_Otros='$7' WHERE pro2_codigo='$3';";

            StrSql = StrSql.Replace("$1", doc);
            StrSql = StrSql.Replace("$3", codProve);
            StrSql = StrSql.Replace("$4", fecha);
            StrSql = StrSql.Replace("$5", facCif);
            StrSql = StrSql.Replace("$6", facGas);
            StrSql = StrSql.Replace("$7", facOtros);

            database.ejecutarInsert(StrSql);
            database.cerrarConexion();

            //se deben actualizar los factores, si es 0 se colocara 1 en la base de datos 08-02-2013
        
        }

        public void guardarDocPro(string valcamb,string totalDivisa, string cantArticulos, string cantItems,string nombreProveedor) {

            StrSql = null;
            //string[] parametros = new string[55];

            StrSql ="INSERT INTO admdocpro "+
                    "(doc_numerodoc, doc_sufijo,doc_tipdoc, doc_ordencompra, doc_codigo, "+
                    "doc_cencost,doc_sucus, doc_moneda, doc_usuario, doc_tiptra, doc_origen, "+
                    "doc_estadodoc,doc_status, doc_fechCarg, doc_fechEmis, " +
                    "doc_fechRecep,doc_fechven, doc_fecReten, doc_fechLCompra, " +
                    "doc_cxp, doc_basnet, " +
                    "doc_neto,doc_valcamb, doc_totaldivisa,doc_subtot,doc_subbase,doc_prod,doc_concep" +
                    ")values "+
                    "('$1','$2','$3','','$4','$doc_cencost','$doc_sucus'," +
                    "'$5','$usuario','$doc_tiptra','$doc_origen','$doc_estadodoc',"+
                    "'$doc_status','$7','$7','$7','$7','$7','$7','$doc_cxp'," +
                    "'$totalDivisa','$totalDivisa','$valcamb','$totalDivisa','$totalDivisa','$totalDivisa','$cantItems','$concepto')";


            StrSql = StrSql.Replace("$1", this.documento);
            StrSql = StrSql.Replace("$2", this.sufijo);
            StrSql = StrSql.Replace("$3", "35");
            StrSql = StrSql.Replace("$4", this.codProveedor);
            StrSql = StrSql.Replace("$5", this.moneda);
            StrSql = StrSql.Replace("$6", "IMP");
            StrSql = StrSql.Replace("$7", this.fechaimport);
            StrSql = StrSql.Replace("$8", this.montoBs);
            StrSql = StrSql.Replace("$usuario", "21");
            StrSql = StrSql.Replace("$doc_cencost", "00000001");
            StrSql = StrSql.Replace("$doc_sucus", "0001");
            StrSql = StrSql.Replace("$doc_tiptra", "C");
            StrSql = StrSql.Replace("$doc_origen", "Importado");
            StrSql = StrSql.Replace("$doc_status", "4");
            StrSql = StrSql.Replace("$doc_estadodoc", "Activo");
            StrSql = StrSql.Replace("$doc_cxp", "1");
            StrSql = StrSql.Replace("$valcamb",valcamb);
            StrSql = StrSql.Replace("$totalDivisa",totalDivisa);
            StrSql = StrSql.Replace("$cantItems", cantItems);
            StrSql = StrSql.Replace("$concepto", nombreProveedor);



            database.ejecutarInsert(StrSql);
        }


        public void guardarInvMov(DataTable tabla,string doc, string suf, int opc) {


            StrSql = null;
            string[] parametros = new string[31];

            if (opc == 1) { 

                tablaBD = "adminvmov2";

                StrSql = "INSERT INTO " + tablaBD + " (mov_cencos,mov_codalm,mov_codcta,mov_codigo,mov_undmed," +
                    "mov_codsuc,mov_codtra,mov_docume,mov_item,mov_tipdoc,mov_ivatip,mov_tipo,mov_invact,mov_fecha," +
                    "mov_fechven,mov_hora,mov_contab,mov_fisico,mov_logico,mov_memo,mov_costo," +
                    "mov_cxund,mov_precio,mov_total,mov_arancel,mov_cosmon,mov_totalmon)VALUES(@cencos,@codalm,@codcta,@codigo,@undmed," +
                    "@codsuc,@codtra,@docum,@item,@tipodoc,@ivatipo,@tipo," +
                    "@invact,@fecha,@fechaven,@hora,@contab,@fisico,@logico,@memo," +
                    "@cant,@costo,@cxund,@precio,@arancel,@cosmon,@totalmon);";


                StrSql = StrSql.Replace("$1", doc);
                StrSql = StrSql.Replace("$2", suf);

                parametros[0] = "@cencos";
                parametros[1] = "@codalm";
                parametros[2] = "@codcta";
                parametros[3] = "@codigo";
                parametros[4] = "@undmed";
                parametros[5] = "@codsuc";
                parametros[6] = "@codtra";
                parametros[7] = "@docum";
                parametros[8] = "@sufijo";
                parametros[9] = "@item";
                parametros[10] = "@tipodoc";
                parametros[11] = "@ivatipo";
                parametros[12] = "@tipo";
                parametros[13] = "@invact";
                parametros[14] = "@fecha";
                parametros[15] = "@fechaven";
                parametros[16] = "@hora";
                parametros[17] = "@contab";
                parametros[18] = "@fisico";
                parametros[19] = "@logico";
                parametros[20] = "@cdcom";
                parametros[21] = "@memo";
                parametros[22] = "@cant";
                parametros[23] = "@costo";
                parametros[24] = "@costoorigen";
                parametros[25] = "@cxund";
                parametros[26] = "@precio";
                parametros[27] = "@total";
                parametros[28] = "@arancel";
                parametros[29] = "@cosmon";
                parametros[30] = "@totalmon";
            }

            if (opc == 2) { 

                tablaBD = "adminvmov_temp";

            StrSql = "INSERT INTO " + tablaBD + " (mov_cencos,mov_codalm,mov_codcta,mov_codigo,mov_undmed," +
            "mov_codsuc,mov_codtra,mov_docume,mov_sufijo,mov_item,mov_tipdoc,mov_ivatip,mov_tipo,mov_invact,mov_fecha," +
            "mov_fecven,mov_hora,mov_contab,mov_fisico,mov_logico,mov_cdcon,mov_memo,mov_costo,mov_costoorig," +
            "mov_cxund,mov_precio,mov_total,mov_arancel,mov_cosmon,mov_totalmon)VALUES(@cencos,@codalm,@codcta,@codigo,@undmed," +
            "@codsuc,@codtra,@docum,@sufijo,@item,@tipodoc,@ivatipo,@tipo," +
            "@invact,@fecha,@fechaven,@hora,@contab,@fisico,@logico,@cdcom,@memo," +
            "@cant,@costo,@costoorigen,@cxund,@precio,@arancel,@cosmon,@totalmon);";

            StrSql = StrSql.Replace("$1", doc);
            StrSql = StrSql.Replace("$2", suf);

            parametros[0] = "@cencos";
            parametros[1] = "@codalm";
            parametros[2] = "@codcta";
            parametros[3] = "@codigo";
            parametros[4] = "@undmed";
            parametros[5] = "@codsuc";
            parametros[6] = "@codtra";
            parametros[7] = "@docum";
            parametros[8] = "@sufijo";
            parametros[9] = "@item";
            parametros[10] = "@tipodoc";
            parametros[11] = "@ivatipo";
            parametros[12] = "@tipo";
            parametros[13] = "@invact";
            parametros[14] = "@fecha";
            parametros[15] = "@fechaven";
            parametros[16] = "@hora";
            parametros[17] = "@contab";
            parametros[18] = "@fisico";
            parametros[19] = "@logico";
            parametros[20] = "@cdcom";
            parametros[21] = "@memo";
            parametros[22] = "@cant";
            parametros[23] = "@costo";
            parametros[24] = "@costoorigen";
            parametros[25] = "@cxund";
            parametros[26] = "@precio";
            parametros[27] = "@total";
            parametros[28] = "@arancel";
            parametros[29] = "@cosmon";
            parametros[30] = "@totalmon";
            
            };



            //StrSql = "INSERT INTO " + tablaBD + " (mov_cencos,mov_codalm,mov_codcta,mov_codigo,mov_undmed," +
            //"mov_codsuc,mov_codtra,mov_docume,mov_sufijo,mov_item,mov_tipdoc,mov_ivatip,mov_tipo,mov_invact,mov_fecha," +
            //"mov_fecven,mov_hora,mov_contab,mov_fisico,mov_logico,mov_cdcon,mov_memo,mov_costo,mov_costoorig,"+
            //"mov_cxund,mov_precio,mov_total,mov_arancel,mov_cosmon,mov_totalmon)VALUES(@cencos,@codalm,@codcta,@codigo,@undmed," +
            //"@codsuc,@codtra,@docum,@sufijo,@item,@tipodoc,@ivatipo,@tipo," +
            //"@invact,@fecha,@fechaven,@hora,@contab,@fisico,@logico,@cdcom,@memo," +
            //"@cant,@costo,@costoorigen,@cxund,@precio,@arancel,@cosmon,@totalmon);";

          

            //StrSql = StrSql.Replace("$1", doc);
            //StrSql = StrSql.Replace("$2", suf);

            //parametros[0] = "@cencos";
            //parametros[1] = "@codalm";
            //parametros[2] = "@codcta";
            //parametros[3] = "@codigo";
            //parametros[4] = "@undmed";
            //parametros[5] = "@codsuc";
            //parametros[6] = "@codtra";
            //parametros[7] = "@docum";
            //parametros[8] = "@sufijo";
            //parametros[9] = "@item";
            //parametros[10] = "@tipodoc";
            //parametros[11] = "@ivatipo";
            //parametros[12] = "@tipo";
            //parametros[13] = "@invact";
            //parametros[14] = "@fecha";
            //parametros[15] = "@fechaven";
            //parametros[16] = "@hora";
            //parametros[17] = "@contab";
            //parametros[18] = "@fisico";
            //parametros[19] = "@logico";
            //parametros[20] = "@cdcom";
            //parametros[21] = "@memo";
            //parametros[22] = "@cant";
            //parametros[23] = "@costo";
            //parametros[24] = "@costoorigen";
            //parametros[25] = "@cxund";
            //parametros[26] = "@precio";
            //parametros[27] = "@total";
            //parametros[28] = "@arancel";
            //parametros[29] = "@cosmon";
            //parametros[30] = "@totalmon";

            database.insertDataTable(tabla, parametros, StrSql);
            database.cerrarConexion();
        
        }

        public void guardarArticulosImportacion(DataGridView tabla, string doc, string suf, int opc)
        {
            if (opc == 1) { tablaBD = "admcalcprodimpor"; }
            if (opc == 2) { tablaBD = "admcalcprodimpor_temp"; };

            StrSql = null;
            string[] parametros = new string[15];

            StrSql = "INSERT INTO " + tablaBD + " (documento,sufijo,codigo,descripcion,unidad," +
            "cantidad,precioFOB,C,D,E,F,G,H,I,J,K,L,codigoProveedor)VALUES('$1','$2',@codigo,@descripcion," +
            "@unidad,@cantidad,@precioFOB,@C,@D,@E,@F,@G,@H,@I,@J,@K,@L,'$3') ";

            StrSql = StrSql.Replace("$1", doc);
            StrSql = StrSql.Replace("$2", suf);
            StrSql = StrSql.Replace("$3", this.codProveedor);

            parametros[0] = "@codigo";
            parametros[1] = "@descripcion";
            parametros[2] = "@unidad";
            parametros[3] = "@cantidad";
            parametros[4] = "@precioFOB";
            parametros[5] = "@C";
            parametros[6] = "@D";
            parametros[7] = "@E";
            parametros[8] = "@F";
            parametros[9] = "@G";
            parametros[10] = "@H";
            parametros[11] = "@I";
            parametros[12] = "@J";
            parametros[13] = "@K";
            parametros[14] = "@L";

            database.insertdataGridView(tabla, parametros, StrSql);
            database.cerrarConexion();

        }

        public void modificarExistencia(DataGridView tabla)
        {

            string[] parametros = new string[4];

            StrSql = "UPDATE adminv2 SET existencia=existencia+@cantidad WHERE inv2_codigo=@codigo";

            parametros[0] = "@Codigo";
            parametros[1] = "@Descripcion";
            parametros[2] = "@Unidad";
            parametros[3] = "@Cantidad";

            database.insertdataGridView(tabla, parametros, StrSql);
            database.cerrarConexion();

        }

        public void guardarCabeceraImportacion(string factorGasto, int opc) {

            if (opc == 1) { tablaBD = "admcabeceradocumento"; }
            if (opc == 2) { tablaBD = "admcabeceradocumento_temp"; };


            StrSql = null;
            StrSql = "INSERT INTO " + tablaBD + " (admFactorCif,admFactorGasto,admFactorX,admDocumen," +
                       " admSufijo,admMontoFOB,admFleteMaritFOB,admSegTransFOB,admCostoMateFOB,admOtros1FOB," +
                       " admOtros2FOB,admMontoBs,admFleteMaritBs,admSegTransBs,admCostoMateBs,admOtros1Bs,admOtros2Bs,"+
                       " admCambioOC,admCambioLiqui,admCambioProductos,admfechaImportacion,admCostoCif,admTotalCif,otrVerifiMercancia,otrHonorarios," +
                       " otrGastCadivi,otrSeguroMerca,otrOtros,gastAduanaOtros,gastosGenerales,valorImportacion,factorGastImpor,admCodProveedor,iva_importacion,iva_nacional,derechosImportacion,moneda,fechaSistema, " +
                       " ControlImportacion,MontoBaseImport,MontoTotalImpor,ExentoNacional,FactNacional, " +
                       " SufiNacional,ControlNacional,FechEmisionNacio,fechVencNacio,BaseNacional,TotalNacional,proveNacional,"+
                       " codicionPago,ExpNumMilco,fechaSolicMilco,fechaAutoMilco,fechaVencMilco,"+
                       " numImpCadivi,fechSoliCadivi,fechAutoCadivi,fechAAD,fechVENAAD,montoAutoCadivi,"+
                       " monedaCadivi,fechVencCadivi,fechEmbarque,ciudad,Vapor,BLNum,fechaVeri,"+
                       " transportista,agente,formaEmbalaje,cantidadBultos,fechLlegada,consignatario,"+
                       " numExpediente,numFactura,fechFactura,tasaCambio,fechReciboActa,fechDocuALD,"+
                       " fechOtorgaALD,fechTransfPago,cambioPermuta) " +
                       " VALUES ('$1','$2','0.00','$4','$5','$6','$7','$8','$9','$otros1Fob','$otros2Fob','$montoBs', " +
                       "'$fleteBs','$seguroBs','$costoMaterialBs','$otros1Bs','$otros2Bs','$cambioOc','$cambioLiqui','$cambioProducto','$fecha'," +
                       "'$costoCif','$totalCif','$verifiMercancia','$honorarios','$gastCadivi','$seguroMerca','$otrosGastos',"+
                       "'$gastAduanaOtros','$gastosGenerales','$valorImportacion','$factorGastImpor','$codProveedor','$ivaImportacion','$ivaNacional','$derechosImportacion','$moneda','$fechSystem', "+
                       "'$controlImportacion','$montoBaseImport','$montoTotalImpor','$exentoNacional','$factNacional','$sufiNacional'," +
                       "'$controlNacional','$fechEmisionNacio','$fechVencNacio','$baseNacional','$totalNacional','$proveNacional', "+
                       "'','$expNumMilco','$solicMilcoFecha','$autoMilcoFecha','$vencMilcoFecha','$numImpCadivi','$CadiviFech',"+
                       "'$fechAutoriCadivi','$AADFech','$fechVENAAD','$montoAutoCadivi','$moneCadivi','$feVencCadivi',"+
                       "'$fecEmbarque','$ciudad','$Vapor','$BLNum','$fecVerif','$transportista','$agente','$formaEmbalaje',"+
                       "'$cantidadBultos','$fechLlegada','$consignatario','$numExpediente','$numFactura','$FacturaFech',"+
                       "'$tasaCambio','$ReciboActaFech','$DocuALDfech','$OtorgaALDfech','$TransfPagoFech','$cambioPermuta')";



            StrSql = StrSql.Replace("$1",this.FactorCif);
            StrSql = StrSql.Replace("$2", factorGasto);
            StrSql = StrSql.Replace("$3",this.factorX);
            StrSql = StrSql.Replace("$4",this.documento);
            StrSql = StrSql.Replace("$5",this.sufijo);
            StrSql = StrSql.Replace("$6",this.montoFob);
            StrSql = StrSql.Replace("$7",this.fleteFob);
            StrSql = StrSql.Replace("$8",this.seguroFob);
            StrSql = StrSql.Replace("$9",this.costoMaterialFob);
            StrSql = StrSql.Replace("$otros1Fob", this.otros1Fob);
            StrSql = StrSql.Replace("$otros2Fob", this.otros2Fob);
            StrSql = StrSql.Replace("$montoBs", this.montoBs);
            StrSql = StrSql.Replace("$fleteBs", this.fleteBs);
            StrSql = StrSql.Replace("$seguroBs", this.seguroBs);
            StrSql = StrSql.Replace("$costoMaterialBs", this.costoMaterialBs);
            StrSql = StrSql.Replace("$otros1Bs", this.otros1Bs);
            StrSql = StrSql.Replace("$otros2Bs", this.otros2Bs);
            StrSql = StrSql.Replace("$cambioOc", this.cambioOc);
            StrSql = StrSql.Replace("$cambioLiqui", this.cambioLiqui);
            StrSql = StrSql.Replace("$cambioProducto", this.cambioProducto);
            StrSql = StrSql.Replace("$fecha",this.Fechaimport);
            StrSql = StrSql.Replace("$totalCif",this.TotalCif);
            StrSql = StrSql.Replace("$costoCif", this.CostoCif);
            StrSql = StrSql.Replace("$verifiMercancia", this.verificaMercan);
            StrSql = StrSql.Replace("$honorarios", this.honorarios);
            StrSql = StrSql.Replace("$gastCadivi", this.gastoCadivi);
            StrSql = StrSql.Replace("$seguroMerca", this.segurosMercancia);
            StrSql = StrSql.Replace("$otrosGastos", this.otrosGastos);
            StrSql = StrSql.Replace("$gastAduanaOtros",this.gastAduanaOtros);
            StrSql = StrSql.Replace("$gastosGenerales",this.gastosGenerales);
            StrSql = StrSql.Replace("$valorImportacion", this.valorImportacion);
            StrSql = StrSql.Replace("$factorGastImpor",this.factorGastImpor);
            StrSql = StrSql.Replace("$codProveedor", this.codProveedor);
            StrSql = StrSql.Replace("$ivaImportacion",this.ivaImportacion);
            StrSql = StrSql.Replace("$ivaNacional",this.ivaNacional);
            StrSql = StrSql.Replace("$derechosImportacion", this.derechosImportacion);
            StrSql = StrSql.Replace("$moneda", this.moneda);
            StrSql = StrSql.Replace("$fechSystem", this.fechaSistema);

            StrSql = StrSql.Replace("$controlImportacion", this.controlImportacion);
            StrSql = StrSql.Replace("$montoBaseImport", this.montoBaseImport);
            StrSql = StrSql.Replace("$montoTotalImpor", this.montoTotalImpor);
            StrSql = StrSql.Replace("$exentoNacional", this.exentoNacional);
            StrSql = StrSql.Replace("$factNacional", this.factNacional);
            StrSql = StrSql.Replace("$sufiNacional", this.sufiNacional);
            StrSql = StrSql.Replace("$controlNacional", this.controlNacional);
            StrSql = StrSql.Replace("$fechEmisionNacio", this.fechEmisionNacio);
            StrSql = StrSql.Replace("$fechVencNacio", this.fechVencNacio);
            StrSql = StrSql.Replace("$baseNacional", this.baseNacional);
            StrSql = StrSql.Replace("$totalNacional", this.totalNacional);
            StrSql = StrSql.Replace("$proveNacional", this.proveNacional);

            StrSql = StrSql.Replace("$expNumMilco",this.ExpNumMilco);
            StrSql = StrSql.Replace("$solicMilcoFecha", this.fechaSolicMilco);
            StrSql = StrSql.Replace("$autoMilcoFecha", this.fechaAutoMilco);
            StrSql = StrSql.Replace("$vencMilcoFecha", this.fechaVencMilco);
            StrSql = StrSql.Replace("$numImpCadivi",this.numImpCadivi);
            StrSql = StrSql.Replace("$CadiviFech", this.fechSoliCadivi);
            StrSql = StrSql.Replace("$fechAutoriCadivi",this.fechAutoCadivi);
            StrSql = StrSql.Replace("$AADFech", this.fechAAD);
            StrSql = StrSql.Replace("$fechVENAAD",this.fechVENAAD);
            StrSql = StrSql.Replace("$montoAutoCadivi",this.montoAutoCadivi);
            StrSql = StrSql.Replace("$moneCadivi",this.monedaCadivi);
            StrSql = StrSql.Replace("$feVencCadivi",this.fechVencCadivi);
            StrSql = StrSql.Replace("$fecEmbarque",this.fechEmbarque);
            StrSql = StrSql.Replace("$ciudad",this.ciudad);
            StrSql = StrSql.Replace("$Vapor",this.Vapor);
            StrSql = StrSql.Replace("$BLNum",this.BLNum);
            StrSql = StrSql.Replace("$fecVerif",this.fechaVeri);
            StrSql = StrSql.Replace("$transportista",this.transportista);
            StrSql = StrSql.Replace("$agente",this.agente);
            StrSql = StrSql.Replace("$formaEmbalaje",this.formaEmbalaje);
            StrSql = StrSql.Replace("$cantidadBultos",this.cantidadBultos);
            StrSql = StrSql.Replace("$fechLlegada",this.fechLlegada);
            StrSql = StrSql.Replace("$consignatario",this.consignatario);
            StrSql = StrSql.Replace("$numExpediente",this.numExpediente);
            StrSql = StrSql.Replace("$numFactura",this.numFactura);
            StrSql = StrSql.Replace("$FacturaFech", this.fechFactura);
            StrSql = StrSql.Replace("$tasaCambio",this.tasaCambio);
            StrSql = StrSql.Replace("$ReciboActaFech", this.fechReciboActa);
            StrSql = StrSql.Replace("$DocuALDfech", this.fechDocuALD);
            StrSql = StrSql.Replace("$OtorgaALDfech", this.fechOtorgaALD);
            StrSql = StrSql.Replace("$TransfPagoFech", this.fechTransfPago);
            StrSql = StrSql.Replace("$cambioPermuta",this.cambioPermuta);

//codicionPago,

            database.ejecutarInsert(StrSql);

        }

        public bool verificarDocumento(string doc, string suf, int opc) {
            bool centicela= false;

            if (opc == 1) { tablaBD = "admcabeceradocumento"; }
            if (opc == 2) { tablaBD = "admcabeceradocumento_temp"; };

            StrSql = "SELECT * FROM " + tablaBD + " WHERE admDocumen='$1' AND admSufijo='$2'";
            StrSql = StrSql.Replace("$1", doc);
            StrSql = StrSql.Replace("$2", suf);

            ds = database.ejecutarQueryDs(StrSql);

            if (ds.Tables[0].Rows.Count != 0) {
                centicela = true;
            }

            return centicela;
            
        }


        public DataTable buscarImportaciones(string doc)
        {
            DataTable tabla1;
            DataTable tabla2;
            tabla1 = new DataTable();
            tabla2 = new DataTable();

            //StrSql = "SELECT admDocumen 'Documento',admSufijo 'Sufijo',admMontoFOB 'Monto FOB',admfechaImportacion 'Fecha', 'En Carga' Estatus  FROM admcabeceradocumento_temp WHERE admDocumen LIKE '$1%' ";
            StrSql = "SELECT admDocumen 'Documento',admSufijo 'Sufijo',admCodProveedor 'Codigo Proveedor',pro_nombre 'Nombre Proveedor',admMontoFOB 'Monto FOB',admfechaImportacion 'Fecha', 'En Carga' Estatus  FROM admcabeceradocumento_temp LEFT OUTER JOIN admproveedor ON admCodProveedor=pro_codigo WHERE admDocumen LIKE '$1%'";

            StrSql = StrSql.Replace("$1",doc);
            tabla1 = database.fDataTable(StrSql);

            //StrSql = "SELECT admDocumen 'Documento',admSufijo 'Sufijo',admMontoFOB 'Monto FOB',admfechaImportacion 'Fecha', 'Liquidada' Estatus  FROM admcabeceradocumento WHERE admDocumen LIKE '$1%' ";
            StrSql = "SELECT admDocumen 'Documento',admSufijo 'Sufijo',admCodProveedor 'Codigo Proveedor',pro_nombre 'Nombre Proveedor',admMontoFOB 'Monto FOB',admfechaImportacion 'Fecha', 'Liquidada' Estatus  FROM admcabeceradocumento LEFT OUTER JOIN admproveedor ON admCodProveedor=pro_codigo WHERE admDocumen LIKE '$1%'";
            StrSql = StrSql.Replace("$1", doc);
            tabla2 = database.fDataTable(StrSql);

            tabla1.Merge(tabla2);

            return tabla1;

        }
        
        public void asignarNumeroComprobante(string bd)
        {

            database.conectionStringSysconta(bd);

            ds = database.ejecutarQueryDs("SELECT pc_idSistema,pc_CorrCompAut FROM admparametroscontables WHERE pc_idSistema='04'");

            if (ds.Tables[0].Rows.Count != 0)
            {
                this.Comprobante = String.Format("{0:0000000000}", Convert.ToDouble(ds.Tables[0].Rows[0][1].ToString()) + 1);
                this.IdSistemasInt = ds.Tables[0].Rows[0][0].ToString();
                database.ejecutarInsert("UPDATE admparametroscontables SET pc_CorrCompAut='" + this.Comprobante + "' WHERE pc_idSistema='04'");

            }
            database.modificarConexionString(1);
            database.cerrarConexion();

        }

        public DataTable imprimirReporte(string doc, string suf, string provee, int opc)
        {

            if (opc == 1) { 
                tablaBD = "admcabeceradocumento";
                tablaBD2 = "admcalcprodimpor";
                concaEstatus = ",'Liquidada' Estatus";
            }
            if (opc == 2) { 
                tablaBD = "admcabeceradocumento_temp";
                tablaBD2 = "admcalcprodimpor_temp";
                concaEstatus = ",'En Carga' Estatus";
            }

            if (opc == 3)
            {
                tablaBD = "admcabeceradocumento_temp";
                tablaBD2 = "admcalcprodimpor_temp";
                concaEstatus = ",'Proyeccion' Estatus";
            }
       
            StrSql = "SELECT " +
                    "`pro_codigo`," +
                    "`pro_nombre`," +
                    "`pro_rif`," +
                    "`admDocumen`," +
                    "`admCodProveedor`," +
                    "`admfechaImportacion`," +
                    "`admFactorCif`," +
                    "`admFactorGasto`," +
                    "`admFactorX`," +
                    "`admMontoFOB`," +
                    "`admMontoBs`," +
                    "`admCambioLiqui`," +
                    "`admCambioProductos`," +
                    "`admTotalCif`," +
                    "`gastosGenerales`," +
                    "`moneda`, " +
                    "`codigo`," +
                    "`descripcion`," +
                    "`unidad`," +
                    "`cantidad`," +
                    "`precioFOB`," +
                    "`admCambioOC`," +
                    "`d`," +
                    "`l`," +
                    "`k`," +
                    "`derechosImportacion`," +
                    "`cost_prom`, "+
                    "`existencia`, "+
                    "`pre_precio`, "+
                    "`valorImportacion`, " +
                    "`fechaSistema`  " + concaEstatus + "  " +
                    "FROM " + tablaBD + "  " +
                    "LEFT OUTER JOIN " + tablaBD2 + "  ON `documento`=`admDocumen` AND `admCodProveedor`=`codigoProveedor` " +
                    "LEFT OUTER JOIN admproveedor  ON admCodProveedor=pro_codigo "+
                    "LEFT OUTER JOIN adminv2 ON  codigo=inv2_codigo "+
                    "LEFT OUTER JOIN admprecios ON codigo=pre_codigo AND pre_act='1'  AND pre_act='1' AND pre_lista='A' " +
                    "WHERE `admDocumen`='$1' AND`admSufijo`='$3' AND `admCodProveedor`='$2'";

            //valorImportacion
            
                StrSql = StrSql.Replace("$1", doc);
                StrSql = StrSql.Replace("$2", provee);
                StrSql = StrSql.Replace("$3", suf);

            return database.fDataTable(StrSql);

        }
        //
        public void guardarLibroCompraFN() {

            //falta el monto total

            StrSql = "INSERT INTO admlibrocompras (IdProveedor, NroDocum, SufiDocum, NroControlSeniat," +
                "NroFiscal,IdTipoDoc,NroDocumAfec, Descripcion, FechaEmision, FechaVenc,FechaLibCompras," +
                "FechaRetenc, MontoBase, MontoDocum, doc_iva_gn, doc_iva_rd, doc_base_gn, doc_base_rd, " +
                "doc_base_ex, PorcIva, PorcIva2,MontoIva, MontoRetenc, PorcRetenc, NroRetenc," +
                "periodoFiscal, IdMoneda, MontoEnDivisa, Aduana, StatusLibro, StatusRetenc, " +
                "FechaCarga, Login, LoginUpd,origen,transporte,otrosGastos,dif_inv," +
                "correlativo) VALUES('$codProveedor','$factNacional','$sufiNacional',"+
                "'$controlNacional','','37','','','$fechEmisionNacio','$fechVencNacio','$fechEmisionNacio',"+
                "'$fechEmisionNacio','$baseNacional','$baseNacional','0.00','0.00','0.00','0.00','0.00','0','0',"+
                "'$ivaNacional','0.00','0','0','0','$moneda','0.00','0.00','1','0','$fechaCarga',"+
                "'029','029','IMP','0.00','0.00','0.00','37');";


            StrSql = StrSql.Replace("$codProveedor", this.codProveedor);
            StrSql = StrSql.Replace("$factNacional", this.factNacional);
            StrSql = StrSql.Replace("$sufiNacional", this.sufiNacional);
            StrSql = StrSql.Replace("$controlNacional", this.controlNacional);
            StrSql = StrSql.Replace("$fechEmisionNacio", this.fechEmisionNacio);
            StrSql = StrSql.Replace("$fechVencNacio", this.fechVencNacio);
            StrSql = StrSql.Replace("$baseNacional", this.baseNacional);
            StrSql = StrSql.Replace("$ivaNacional", this.ivaNacional);
            StrSql = StrSql.Replace("$moneda", "Bs");
            StrSql = StrSql.Replace("$fechaCarga",""+DateTime.Today.Year+"-"+DateTime.Today.Month+"-"+DateTime.Today.Day);

            database.ejecutarInsert(StrSql);
        }

        public void guardarLibroCompraFI()
        {
            //falta el monto total

            StrSql = "INSERT INTO admlibrocompras (IdProveedor, NroDocum, SufiDocum, NroControlSeniat," +
                "NroFiscal,IdTipoDoc,NroDocumAfec, Descripcion, FechaEmision, FechaVenc,FechaLibCompras," +
                "FechaRetenc, MontoBase, MontoDocum, doc_iva_gn, doc_iva_rd, doc_base_gn, doc_base_rd, " +
                "doc_base_ex, PorcIva, PorcIva2,MontoIva, MontoRetenc, PorcRetenc, NroRetenc," +
                "periodoFiscal, IdMoneda, MontoEnDivisa, Aduana, StatusLibro, StatusRetenc, " +
                "FechaCarga, Login, LoginUpd,origen,transporte,otrosGastos,dif_inv," +
                "correlativo) VALUES('$codProveedor','$factImportada','$sufiImport'," +
                "'$controlImport','','35','','','$fechEmisionImport','$fechVencImport','$fechEmisionImport'," +
                "'$fechEmisionImport','$baseImportado','$baseImportado','0.00','0.00','0.00','0.00','0.00','0','0'," +
                "'$ivaImport','0.00','0','0','0','$moneda','0.00','0.00','1','0','$fechaCarga'," +
                "'029','029','IMP','0.00','0.00','0.00','37');";
            

            StrSql = StrSql.Replace("$codProveedor", this.codProveedor);
            StrSql = StrSql.Replace("$factImportada", this.documento);
            StrSql = StrSql.Replace("$sufiImport", this.sufijo);
            StrSql = StrSql.Replace("$controlImport", this.controlImportacion);
            StrSql = StrSql.Replace("$fechEmisionImport", this.Fechaimport);
            StrSql = StrSql.Replace("$fechVencImport", this.Fechaimport);
            StrSql = StrSql.Replace("$baseImportado", this.totalCif);
            StrSql = StrSql.Replace("$montoExenImport", this.montoExenImport);
            StrSql = StrSql.Replace("$ivaImport", this.ivaImportacion);
            StrSql = StrSql.Replace("$moneda", this.moneda);
            StrSql = StrSql.Replace("$fechaCarga", "" + DateTime.Today.Year + "-" + DateTime.Today.Month + "-" + DateTime.Today.Day);
            
            database.ejecutarInsert(StrSql);
        }

        public void guardarSalpro() {


            StrSql = "INSERT INTO admsalpro (IdProveedor, NroDocum, SufiDocum, " +
                       "NroComprobante,FechaEmision, FechaVencimiento, FechaCarga, " +
                       "FechaLibCompras,IdTipoDoc,IdMoneda, " +
                       "MontoEnDivisa, MontoDocum, SaldoDocum, MontoIva, " +
                       "PorcIva, PorcIva2,MontoRetenc, NroCompRetenc,Status," +
                       "Login, Loginact,dif_inv,correlativo)VALUES('$1','$2','$3','$4',"+
                       "'$5','$6','$7','$7','$8','$9','$MontoEnDivisa','$MontoDocum',"+
                       "'$SaldoDocum','$MontoIva','$PorcIva','$PorIva2','$MontoRetenc',"+
                       "'$NroCompRetenc','$Status','$Login','$Login','$dif_inv','$correlativo')";

             StrSql = StrSql.Replace("$1",this.codProveedor);
             StrSql = StrSql.Replace("$2",this.factNacional);
             StrSql = StrSql.Replace("$3",this.sufiNacional);
             StrSql = StrSql.Replace("$4",this.comprobante);
             StrSql = StrSql.Replace("$5",this.fechEmisionNacio);
             StrSql = StrSql.Replace("$6",this.fechVencNacio);
             StrSql = StrSql.Replace("$7",this.fechaSistema);
             StrSql = StrSql.Replace("$8","37");
             StrSql = StrSql.Replace("$9",this.moneda);
             StrSql = StrSql.Replace("$MontoEnDivisa","0.00");
             StrSql = StrSql.Replace("$MontoDocum",this.totalNacional);
             StrSql = StrSql.Replace("$SaldoDocum",this.totalCif);
             StrSql = StrSql.Replace("$MontoIva",this.ivaNacional);
             StrSql = StrSql.Replace("$PorcIva","12.00");
             StrSql = StrSql.Replace("$PorIva2","8.00");
             StrSql = StrSql.Replace("$MontoRetenc","0.00");
             StrSql = StrSql.Replace("$NroCompRetenc","0");
             StrSql = StrSql.Replace("$Status","0");
             StrSql = StrSql.Replace("$Login","029");
             StrSql = StrSql.Replace("$dif_inv","0.00");
             StrSql = StrSql.Replace("$correlativo","37");

             database.ejecutarInsert(StrSql);
             database.cerrarConexion();
        }

    }
}
