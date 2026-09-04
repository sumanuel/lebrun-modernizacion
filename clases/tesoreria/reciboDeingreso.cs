using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using lebrun.clasesData;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;


namespace lebrun.clases.tesoreria
{

    class reciboDeingreso
    {
        private ConexionBD database;
        private string StrSql;
        private DataTable tabla;
        private MySqlDataReader dr;
        private DataSet ds;

        private string tipoPago;
        private string caja;
        private string codCliente;
        private string fechaSistema;
        private string fechaPago;
        private string estado;
        private string comprobante;
        private string cencos;
        private string sucursal;
        private string hora;
        private string docAso;
        private string tipDocAso;
        private string usuario;
        private string cobrador;
        private string monto;
        private string montoIva;
        private string divisa;
        private string memo;
        private string valCamb;
        private string correlativo;
        private string IdSistemasInt;
        private string login;
        private string nombreCliente;
        private string cuentaCliente;
        private string auxiliarCliente;
        private string cuentaDoc;
        private string numeroReciboCobro;


        public string TipoPago
        {
            get { return tipoPago; }
            set { tipoPago = value; }
        }

        public string Caja
        {
            get { return caja; }
            set { caja = value; }
        }

        public string FechaSistema
        {
            get { return fechaSistema; }
            set { fechaSistema = value; }
        }

        public string FechaPago
        {
            get { return fechaPago; }
            set { fechaPago = value; }
        }
        
        public string Estado
        {
            get { return estado; }
            set { estado = value; }
        }
        
        public string Comprobante
        {
            get { return comprobante; }
            set { comprobante = value; }
        }
        
        public string Cencos
        {
            get { return cencos; }
            set { cencos = value; }
        }
        
        public string Sucursal
        {
            get { return sucursal; }
            set { sucursal = value; }
        }
        
        public string Hora
        {
            get { return hora; }
            set { hora = value; }
        }
        
        public string DocAso
        {
            get { return docAso; }
            set { docAso = value; }
        }
        
        public string TipDocAso
        {
            get { return tipDocAso; }
            set { tipDocAso = value; }
        }
        
        public string Usuario
        {
            get { return usuario; }
            set { usuario = value; }
        }
        
        public string Cobrador
        {
            get { return cobrador; }
            set { cobrador = value; }
        }
        
        public string Monto
        {
            get { return monto; }
            set { monto = value; }
        }
        
        public string MontoIva
        {
            get { return montoIva; }
            set { montoIva = value; }
        }
        
        public string Divisa
        {
            get { return divisa; }
            set { divisa = value; }
        }
        
        public string Memo
        {
            get { return memo; }
            set { memo = value; }
        }
   
        public string CodCliente
        {
            get { return codCliente; }
            set { codCliente = value; }
        }

        public string ValCamb
        {
            get { return valCamb; }
            set { valCamb = value; }
        }

        public string Correlativo
        {
            get { return correlativo; }
            set { correlativo = value; }
        }

        public string IdSistemasInt1
        {
            get { return IdSistemasInt; }
            set { IdSistemasInt = value; }
        }

        public string Login
        {
            get { return login; }
            set { login = value; }
        }

        public string NombreCliente
        {
            get { return nombreCliente; }
            set { nombreCliente = value; }
        }

        public string CuentaCliente
        {
            get { return cuentaCliente; }
            set { cuentaCliente = value; }
        }

        public string AuxiliarCliente
        {
            get { return auxiliarCliente; }
            set { auxiliarCliente = value; }
        }

        public string CuentaDoc
        {
            get { return cuentaDoc; }
            set { cuentaDoc = value; }
        }

        public string NumeroReciboCobro
        {
            get { return numeroReciboCobro; }
            set { numeroReciboCobro = value; }
        }


        /// <summary>
        /// ready
        /// </summary>

        public reciboDeingreso() {

            database = new ConexionBD();
            tabla = new DataTable();
        }

        /// <summary>
        /// ready
        /// </summary>
     


        public DataTable cargarDocumentos(string cliente) {

            StrSql = "SELECT TipoDoc 'Tipo Doc', NroDocum 'Doc Numero', dcli_numfis 'Numero Fiscal', dcli_facafe 'Factura Afectada', FechaEmision 'Fecha Emision' ,FechaEmision 'Fecha Vecimiento',  " +
                " MontoTotal 'Monto Origen' , MontoIva 'Monto Iva', sal_actual 'Saldo Documento',cli_nombre,cli_situacion  " +
                " FROM admsalcli "+
                " LEFT OUTER JOIN admclientes ON admsalcli.cli_codigo=admclientes.cli_codigo  "+
                " WHERE admsalcli.cli_codigo= '$1' AND TipoDoc IN ('FAV','ANT','DEV') " +
                " AND Status = '0' ORDER BY FechaEmision,dcli_facafe";

            StrSql = StrSql.Replace("$1", cliente);
            tabla = database.fDataTable(StrSql);
            database.cerrarConexion();
            return tabla;
            
        
        }
        /// <summary>
        /// ready
        /// </summary>
        /// 

        public void reciboIngreso()
        {
            StrSql = "INSERT INTO admreciboingre (rec_tippago,rec_caja,rec_cliente,rec_fecha," +
                "rec_estado,rec_numcomprob,rec_cencos,rec_sucursal,rec_hora,rec_docaso," +
                "rec_tipdocaso,rec_usuario,rec_cobrador,rec_monto,rec_montoiva,rec_divisa," +
                "rec_valorcam,rec_memo,rec_numinter) VALUES ('$tipoPago','$caja','$codCliente','$fechaPago','$estado'," +
                "'$correlativo','','$sucursal','$hora',' ',' ','$usuario','$cobrador','$monto','$montIva','$divisa','$valCamb','$memo','$interno')";
            
            //dejar el numero comprobante en 999
            
            StrSql = StrSql.Replace("$tipoPago", this.tipoPago);
            StrSql = StrSql.Replace("$caja", this.caja);
            StrSql = StrSql.Replace("$codCliente", this.codCliente);
            StrSql = StrSql.Replace("$fechaPago", this.fechaPago);
            StrSql = StrSql.Replace("$estado",this.estado);
            StrSql = StrSql.Replace("$comprobante", this.comprobante);
            StrSql = StrSql.Replace("$cencos", this.cencos);
            StrSql = StrSql.Replace("$sucursal", this.sucursal);
            StrSql = StrSql.Replace("$hora", this.hora);
            StrSql = StrSql.Replace("$docAso", this.docAso);
            StrSql = StrSql.Replace("$tipDocAso", this.tipDocAso);
            StrSql = StrSql.Replace("$usuario", this.usuario);
            StrSql = StrSql.Replace("$cobrador", this.cobrador);
            StrSql = StrSql.Replace("$monto", this.monto);
            StrSql = StrSql.Replace("$montIva", this.montoIva);
            StrSql = StrSql.Replace("$divisa", this.divisa);
            StrSql = StrSql.Replace("$valCamb", this.valCamb);
            StrSql = StrSql.Replace("$memo", this.memo);
            StrSql = StrSql.Replace("$correlativo",this.correlativo);
            StrSql = StrSql.Replace("$interno",this.numeroReciboCobro);

            database.ejecutarInsert(StrSql);
            database.cerrarConexion();

        }

        /// <summary>
        /// ready
        /// </summary>


        public void insert_recibo_ingreso_det(DataGridView tabla)
        {
            string[] parametros = new string[7];

            StrSql = "INSERT INTO admreciboingre_det (movrec_numtra,movrec_numero,movrec_descri, " +
                " movrec_codclie,movrec_cencos,movrec_sucursal,movrec_codcaj,movrec_forpag,movrec_codban, " +
                " movrec_xbanco,movrec_cuentaban,movrec_nuncheque,movrec_tipcta,movrec_hora,movrec_usuario," +
                " movrec_fecha,movrec_fhccom,movrec_monto,movrec_divisa,movrec_tiptra,movrec_status) VALUES" +
                "('','$correlativo','$tipoPago','$codCliente','','$sucursal','$caja',@FormaPago,@IdBanco," +
                "@Banco,@Cuenta,@Numero,'0','$hora','$usuario','$fechaPago','$fechaPago',@Monto,'BsF','D','Activo')";

            StrSql = StrSql.Replace("$tipoPago", this.tipoPago);
            StrSql = StrSql.Replace("$caja", this.caja);
            StrSql = StrSql.Replace("$codCliente", this.codCliente);
            StrSql = StrSql.Replace("$fechaPago", this.fechaPago);
            StrSql = StrSql.Replace("$estado", this.estado);
            StrSql = StrSql.Replace("$comprobante", this.comprobante);
            StrSql = StrSql.Replace("$cencos", this.cencos);
            StrSql = StrSql.Replace("$sucursal", this.sucursal);
            StrSql = StrSql.Replace("$hora", this.hora);
            StrSql = StrSql.Replace("$docAso", this.docAso);
            StrSql = StrSql.Replace("$tipDocAso", this.tipDocAso);
            StrSql = StrSql.Replace("$usuario", this.usuario);
            StrSql = StrSql.Replace("$cobrador", this.cobrador);
            StrSql = StrSql.Replace("$monto", this.monto);
            StrSql = StrSql.Replace("$montIva", this.montoIva);
            StrSql = StrSql.Replace("$divisa", this.divisa);
            StrSql = StrSql.Replace("$valCamb", this.valCamb);
            StrSql = StrSql.Replace("$memo", this.memo);
            StrSql = StrSql.Replace("$correlativo", this.correlativo);
           

            parametros[0] = "@FormaPago";
            parametros[1] = "@Banco";
            parametros[2] = "@Cuenta";
            parametros[3] = "@Numero";
            parametros[4] = "@Fecha";
            parametros[5] = "@Monto";
            parametros[6] = "@IdBanco";

            database.insertdataGridView(tabla, parametros, StrSql);
            database.cerrarConexion();

        }

        /// <summary>
        /// ready
        /// </summary>


        public void alimentarReciboDoc(DataGridView tabla)
        {

            string[] parametros = new string[8];

            StrSql = "INSERT INTO admreciboingre_doc " +
                    "(ingreDoc_tipo,ingreDoc_Nrecibo, ingreDoc_NumDoc, ingreDoc_MontoCancelado, " +
                    "ingreDoc_MontoOrigen, ingreDoc_fechaFact, ingreDoc_fechaCance,  " +
                    "ingreDoc_Cobrador, ingreDoc_fechaVenc, ingreDoc_iva,ingreDoc_fechaProceso,  " +
                    "ingreDoc_numcomprob,ingreDoc_facafe) VALUES  " +
                    "(@Tipo,'$NreciboCobro',@ND,@MC,@MO,@FE,@FE,'$cobrador',  " +
                    "@FV,@MI,'$Sistema','$correlativo',@ND)";


            
            StrSql = StrSql.Replace("$tipoPago", this.tipoPago);
            StrSql = StrSql.Replace("$caja", this.caja);
            StrSql = StrSql.Replace("$codCliente", this.codCliente);
            StrSql = StrSql.Replace("$fechaPago", this.fechaPago);
            StrSql = StrSql.Replace("$estado", this.estado);
            StrSql = StrSql.Replace("$comprobante", this.comprobante);
            StrSql = StrSql.Replace("$cencos", this.cencos);
            StrSql = StrSql.Replace("$sucursal", this.sucursal);
            StrSql = StrSql.Replace("$hora", this.hora);
            StrSql = StrSql.Replace("$docAso", this.docAso);
            StrSql = StrSql.Replace("$tipDocAso", this.tipDocAso);
            StrSql = StrSql.Replace("$usuario", this.usuario);
            StrSql = StrSql.Replace("$cobrador", this.cobrador);
            StrSql = StrSql.Replace("$monto", this.monto);
            StrSql = StrSql.Replace("$montIva", this.montoIva);
            StrSql = StrSql.Replace("$divisa", this.divisa);
            StrSql = StrSql.Replace("$valCamb", this.valCamb);
            StrSql = StrSql.Replace("$memo", this.memo);
            StrSql = StrSql.Replace("$correlativo", this.correlativo);
            StrSql = StrSql.Replace("$NreciboCobro", this.numeroReciboCobro);
            StrSql = StrSql.Replace("$Sistema", DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day);

            parametros[0] = "@Tipo";
            parametros[1] = "@ND";
            parametros[2] = "@MC";
            parametros[3] = "@MO";
            parametros[4] = "@FE";
            parametros[5] = "@FV";
            parametros[6] = "@MI";
            parametros[7] = "@SD";

            database.insertdataGridView(tabla, parametros, StrSql);
            database.cerrarConexion();


        }

        /// <summary>
        /// ready
        /// </summary>



        public void alimentarReciboDoc2(DataGridView tabla)
        {
            for (int i = 0; i < tabla.Rows.Count; i++)

            {
                StrSql = "INSERT INTO admreciboingre_doc " +
                "(ingreDoc_tipo,ingreDoc_Nrecibo, ingreDoc_NumDoc, ingreDoc_MontoCancelado, " +
                "ingreDoc_MontoOrigen, ingreDoc_fechaFact, ingreDoc_fechaCance,  " +
                "ingreDoc_Cobrador, ingreDoc_fechaVenc, ingreDoc_iva,ingreDoc_fechaProceso,  " +
                "ingreDoc_numcomprob,ingreDoc_facafe) VALUES  " +
                "('$Tipo','$NreciboCobro','$ND','$MC','$MO','$FE','$FE','$cobrador',  " +
                "'$FV','$MI','$Sistema','$correlativo','$ND')";

                StrSql = StrSql.Replace("$tipoPago", this.tipoPago);
                StrSql = StrSql.Replace("$caja", this.caja);
                StrSql = StrSql.Replace("$codCliente", this.codCliente);
                StrSql = StrSql.Replace("$fechaPago", this.fechaPago);
                StrSql = StrSql.Replace("$estado", this.estado);
                StrSql = StrSql.Replace("$comprobante", this.comprobante);//este no se esta usando, se esta usando correlativo
                StrSql = StrSql.Replace("$cencos", this.cencos);
                StrSql = StrSql.Replace("$sucursal", this.sucursal);
                StrSql = StrSql.Replace("$hora", this.hora);
                StrSql = StrSql.Replace("$docAso", this.docAso);
                StrSql = StrSql.Replace("$tipDocAso", this.tipDocAso);
                StrSql = StrSql.Replace("$usuario", this.usuario);
                StrSql = StrSql.Replace("$cobrador", this.cobrador);
                StrSql = StrSql.Replace("$monto", this.monto);
                StrSql = StrSql.Replace("$montIva", this.montoIva);
                StrSql = StrSql.Replace("$divisa", this.divisa);
                StrSql = StrSql.Replace("$valCamb", this.valCamb);
                StrSql = StrSql.Replace("$memo", this.memo);
                StrSql = StrSql.Replace("$correlativo", this.correlativo);
                StrSql = StrSql.Replace("$NreciboCobro", this.numeroReciboCobro);
                StrSql = StrSql.Replace("$Sistema", DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day);

            
                StrSql = StrSql.Replace("$Tipo", "" + tabla.Rows[i].Cells[0].Value);
                StrSql = StrSql.Replace("$ND", "" + tabla.Rows[i].Cells[12].Value);
                StrSql = StrSql.Replace("$FE", "" + tabla.Rows[i].Cells[13].Value);
                StrSql = StrSql.Replace("$FV", "" + tabla.Rows[i].Cells[14].Value);
                StrSql = StrSql.Replace("$MO", "" + tabla.Rows[i].Cells[15].Value);
                StrSql = StrSql.Replace("$MI", "" + tabla.Rows[i].Cells[16].Value);
                StrSql = StrSql.Replace("$SD", "" + tabla.Rows[i].Cells[17].Value);
                StrSql = StrSql.Replace("$MC", "" + tabla.Rows[i].Cells[18].Value);

                database.ejecutarInsert(StrSql);
            }
            database.cerrarConexion();
        }

        /// <summary>
        /// ready
        /// </summary>


        public void alimentarReciboDocAnt(DataGridView tabla)
        {
            for (int i = 0; i < tabla.Rows.Count; i++)
            {
                StrSql = "INSERT INTO admreciboingre_doc " +
                "(ingreDoc_tipo,ingreDoc_Nrecibo, ingreDoc_NumDoc, ingreDoc_MontoCancelado, " +
                "ingreDoc_MontoOrigen, ingreDoc_fechaFact, ingreDoc_fechaCance,  " +
                "ingreDoc_Cobrador, ingreDoc_fechaVenc, ingreDoc_iva,ingreDoc_fechaProceso,  " +
                "ingreDoc_numcomprob,ingreDoc_facafe) VALUES  " +
                "('$Tipo','$NreciboCobro','$Recibo','$Monto','0.00','$FechaSistema','$Fech','$cobrador',  " +
                "'$FechaSistema','$Iva','$FechaSistema','$correlativo','$Recibo')";

                StrSql = StrSql.Replace("$tipoPago", this.tipoPago);
                StrSql = StrSql.Replace("$caja", this.caja);
                StrSql = StrSql.Replace("$codCliente", this.codCliente);
                StrSql = StrSql.Replace("$fechaPago", this.fechaPago);
                StrSql = StrSql.Replace("$estado", this.estado);
                StrSql = StrSql.Replace("$comprobante", this.comprobante);
                StrSql = StrSql.Replace("$cencos", this.cencos);
                StrSql = StrSql.Replace("$sucursal", this.sucursal);
                StrSql = StrSql.Replace("$hora", this.hora);
                StrSql = StrSql.Replace("$docAso", this.docAso);
                StrSql = StrSql.Replace("$tipDocAso", this.tipDocAso);
                StrSql = StrSql.Replace("$usuario", this.usuario);
                StrSql = StrSql.Replace("$cobrador", this.cobrador);
                StrSql = StrSql.Replace("$monto", this.monto);
                StrSql = StrSql.Replace("$montIva", this.montoIva);
                StrSql = StrSql.Replace("$divisa", this.divisa);
                StrSql = StrSql.Replace("$valCamb", this.valCamb);
                StrSql = StrSql.Replace("$memo", this.memo);
                StrSql = StrSql.Replace("$correlativo", this.correlativo);//reciboCobro anteriormente
                StrSql = StrSql.Replace("$NreciboCobro", this.numeroReciboCobro);

                StrSql = StrSql.Replace("$Tipo", "ANT");
                StrSql = StrSql.Replace("$ReciboCobro", this.correlativo);
                StrSql = StrSql.Replace("$Recibo", this.correlativo);
                StrSql = StrSql.Replace("$Monto", "" + tabla.Rows[i].Cells[5].Value);
                StrSql = StrSql.Replace("$FechaSistema", "" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString());
                StrSql = StrSql.Replace("$Fech", this.fechaPago);
                StrSql = StrSql.Replace("$Iva", "0.00");


                database.ejecutarInsert(StrSql);
            }
            database.cerrarConexion();
        }

        /// <summary>
        /// ready
        /// </summary>
        /// 
        

        public void actualizarReciboSalcli(DataGridView tabla)
        {
            string[] parametros = new string[3];
            StrSql = "UPDATE admsalcli SET sal_actual= @sal_actual, FechaPago='$1', Status= @Status WHERE NroDocum = @NroDocum";

            StrSql = StrSql.Replace("$1",""+ DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString());
            parametros[0] = "@sal_actual";
            parametros[1] = "@Status";
            parametros[2] = "@NroDocum";

            
            database.insertdataGridViewConNombre(tabla, parametros, StrSql);
            database.cerrarConexion();

        }

        /// <summary>
        /// ready
        /// </summary>

        
        public void ingresarAnticipoSalcli(DataGridView tabla) 
        {


            for (int i = 0; i < tabla.Rows.Count; i++)
            {

                StrSql = "INSERT INTO admsalcli (cli_codigo,sal_actual,TipoDoc, " +
                         "NroDocum, FechaEmision,FechaCarga,FechaVenc,Status,dcli_numfis," +
                         "MontoTotal,MontoIva,dcli_facafe,CodVend) VALUES('$cliente','$Monto','ANT', " +
                         "'$Numero','$fech','$fechaCarga','$fech','0','','$Monto','0.00','$Numero','$Cobrador') ";


                StrSql = StrSql.Replace("$cliente", this.codCliente);
                StrSql = StrSql.Replace("$fechaCarga", "" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString());
                StrSql = StrSql.Replace("$Cobrador", this.cobrador);
                StrSql = StrSql.Replace("$fech", this.fechaPago);

                StrSql = StrSql.Replace("$Banco", "" + tabla.Rows[i].Cells[1].Value);
                StrSql = StrSql.Replace("$Cuenta", "" + tabla.Rows[i].Cells[2].Value);
                StrSql = StrSql.Replace("$Numero", "" + tabla.Rows[i].Cells[3].Value);
                StrSql = StrSql.Replace("$fecPago", "" + tabla.Rows[i].Cells[4].Value);
                StrSql = StrSql.Replace("$Monto", "-" + tabla.Rows[i].Cells[5].Value);
                StrSql = StrSql.Replace("$idBanco", "" + tabla.Rows[i].Cells[6].Value);
                StrSql = StrSql.Replace("$FormaPago", "" + tabla.Rows[i].Cells[7].Value);
                StrSql = StrSql.Replace("$correlativo", this.correlativo);

                database.ejecutarInsert(StrSql);

            }
            database.cerrarConexion();
        }



        /// <summary>
        /// ready
        /// </summary>
        /// 


        public void ingresarAnticipoSalcli(string monto)
        {

                StrSql = "INSERT INTO admsalcli (cli_codigo,sal_actual,TipoDoc, " +
                         "NroDocum, FechaEmision,FechaCarga,FechaVenc,Status,dcli_numfis," +
                         "MontoTotal,MontoIva,dcli_facafe,CodVend) VALUES('$cliente','$Monto','ANT', " +
                         "'$Numero','$fech','$fechaCarga','$fech','0','','$Monto','0.00','$Numero','$Cobrador') ";


                StrSql = StrSql.Replace("$cliente", this.codCliente);
                StrSql = StrSql.Replace("$fechaCarga", "" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString());
                StrSql = StrSql.Replace("$Cobrador", this.cobrador);
                StrSql = StrSql.Replace("$fech", this.fechaPago);
                StrSql = StrSql.Replace("$Monto", "-" + monto);
                StrSql = StrSql.Replace("$Numero", this.correlativo);

                database.ejecutarInsert(StrSql);

            
            database.cerrarConexion();
        }

        /// <summary>
        /// ready
        /// </summary>
        /// 


        public void buscarCorrelativo(string tipoDoc)
        {

            StrSql = "SELECT ctd_tipo,ctd_correlativo FROM admtipdoccli where ctd_tipo='$1'";
            StrSql = StrSql.Replace("$1", tipoDoc);

            dr = database.ejecutarQueryDr(StrSql);

            if (dr.HasRows)
            {
                dr.Read();
                this.correlativo = dr.GetString("ctd_correlativo");
            }

        }



        /// <summary>
        /// ready
        /// </summary>
        /// 

        public void buscarCuentaDoc(string tipoDoc)
        {

            StrSql = "SELECT ctd_tipo,ctd_codcta FROM admtipdoccli where ctd_tipo='$1'";
            StrSql = StrSql.Replace("$1", tipoDoc);



            dr = database.ejecutarQueryDr(StrSql);

            if (dr.HasRows)
            {
                dr.Read();
                this.cuentaDoc = dr.GetString("ctd_codcta");
            }

        }

        public void asignarNumeroComprobante(string bd)
        {

            database.conectionStringSysconta(bd);

            ds = database.ejecutarQueryDs("SELECT pc_idSistema,pc_CorrCompAut FROM admparametroscontables WHERE pc_idSistema='03'");

            if (ds.Tables[0].Rows.Count != 0)
            {
                this.Comprobante = String.Format("{0:0000000000}", Convert.ToDouble(ds.Tables[0].Rows[0][1].ToString()) + 1);
                this.IdSistemasInt = ds.Tables[0].Rows[0][0].ToString();
                database.ejecutarInsert("UPDATE admparametroscontables SET pc_CorrCompAut='" + this.Comprobante + "' WHERE pc_idSistema='03'");

            }
            database.modificarConexionString(1);
            database.cerrarConexion();

        }

        /// <summary>
        /// ready
        /// </summary>
        /// 

        public void asignarNumeroCorrelativo(string tipoDoc)
        {


            StrSql = "SELECT ctd_tipo,ctd_correlativo FROM admtipdoccli where ctd_tipo='$1'";
            StrSql = StrSql.Replace("$1", tipoDoc);

            ds = database.ejecutarQueryDs(StrSql);

            if (ds.Tables[0].Rows.Count != 0)
            {
                this.correlativo = String.Format("{0:000000000000}", Convert.ToDouble(ds.Tables[0].Rows[0][1].ToString()) + 1);
                database.ejecutarInsert("UPDATE admtipdoccli SET ctd_correlativo='" + this.correlativo + "' WHERE ctd_tipo='REI'");

            }

            database.cerrarConexion();

        }

        /// <summary>
        /// ready
        /// </summary>
        ///

        public void syscontaDetalles(DataTable tabla, string compania)
        {

            string[] parametros = new string[23];

            database.conectionStringSysconta(compania);

            StrSql = "INSERT INTO admmovimientoscontabled (mcd_NroComprobante,mcd_Item,mcd_FechaCarga,mcd_CodigoMayor," +
                    "mcd_CodigoAux,mcd_NroDocum,mcd_Referencia,mcd_Descripcion,mcd_status,mcd_login,mcd_UltLogin,mdc_FechaUltMod," +
                    "mdc_Compania,mdc_TipoTransaccion,mdc_monto,movcon_proveedor,movcon_numdoc,movcon_sufdoc,movcon_tipdoc,codigoIslr,mdc_FechaComprobante)" +
                    "VALUE(@numComprobante,@item,@fecha,@cuenta,@auxiliar,'',@referencia,@descripcion,'1','$login','$login',@fecha,'$compania',@tipoLetra,@monto,@codprove," +
                    "@interno,@sufiDoc,@tipoDoc,@islr,@fecha)";

            StrSql = StrSql.Replace("$compania", compania);

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
            parametros[20] = "@tipoLetra";
            parametros[21] = "@interno";
            parametros[22] = "@referencia";

            StrSql = StrSql.Replace("$login", this.usuario);


            database.insertDataTable(tabla, parametros, StrSql);
            database.modificarConexionString(1);
            database.cerrarConexion();

        }

        /// <summary>
        /// ready
        /// </summary>
        ///


        public void syscontabCabecera(string montoD, string montoC, string compania)
        {

            database.conectionStringSysconta(compania);

            StrSql = "INSERT INTO admmovimientoscontablesc (mc_nroComprobante,mc_FechaComprobante, " +
                        " mc_FechaCarga,mc_FechaCierre,mc_IdSistema,mc_Descripcion,mc_MontoDebitos,mc_MontoCreditos,mc_Status, " +
                        " mc_Login,mc_LoginUpd,mc_FechaUltMod,mc_Compania,mc_HoraGuardado,IdSistemas) VALUES " +
                        "('$comprobante','$fecSistema','$fecSistema','$fecSistema','$mcIdSistema','$descripcion','$Debito'," +
                        "'$Credito','$estatus','$login','$login','$fepago','$compania','$hora','$idSistema')";

            StrSql = StrSql.Replace("$comprobante", this.comprobante);
            StrSql = StrSql.Replace("$fecSistema", "" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString());
            StrSql = StrSql.Replace("$mcIdSistema", compania);
            StrSql = StrSql.Replace("$Debito", montoD.Replace(",", "."));
            StrSql = StrSql.Replace("$Credito", montoC.Replace(",", "."));
            StrSql = StrSql.Replace("$estatus", "2");
            StrSql = StrSql.Replace("$login", this.usuario);
            StrSql = StrSql.Replace("$updateLogin",this.usuario);
            StrSql = StrSql.Replace("$compania", compania);
            StrSql = StrSql.Replace("$idSistema", "REI");
            StrSql = StrSql.Replace("$hora", DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second);
            StrSql = StrSql.Replace("$fepago", this.fechaPago);
            StrSql = StrSql.Replace("$descripcion", this.memo);

            database.ejecutarInsert(StrSql);
            database.modificarConexionString(1);
            //
        }

        /// <summary>
        /// ready
        /// </summary>
        ///

        public void guardarMovContab(DataTable tabla)
        {

            StrSql = null;
            string[] parametros = new string[20];
            StrSql = "INSERT INTO admmovcontab (movcon_item,movcon_proveedor,movcon_numdoc,movcon_sufdoc," +
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

        /// <summary>
        /// ready
        /// </summary>
        ///


        public void obtenerCuentaCliente() {

            StrSql = "SELECT admAC_codigoCuenta,admAC_codigoAuxiliarContable,cli_rif FROM admclientes WHERE cli_codigo='$1'";
            StrSql = StrSql.Replace("$1", this.codCliente);

            ds = database.ejecutarQueryDs(StrSql);

            if (ds.Tables[0].Rows.Count != 0)
            {
                this.cuentaCliente = ds.Tables[0].Rows[0][0].ToString();
                this.auxiliarCliente= ds.Tables[0].Rows[0][1].ToString();
            }

            database.cerrarConexion();
        }


        /// <summary>
        /// ready
        /// </summary>
        ///


        public DataTable armarLbxReciboIngreso() { 
        
        
            StrSql="SELECT rec_numcomprob 'Comprobante',rec_fecha 'Fecha',rec_cliente 'Cliente',rec_tippago 'Tipo', "+
                    "rec_monto 'Monto',rec_estado 'Estado' FROM admreciboingre ORDER BY rec_numcomprob DESC LIMIT 50";

            tabla = database.fDataTable(StrSql);
            database.cerrarConexion();
            return tabla;


        }


        /// <summary>
        /// ready
        /// </summary>
        ///


        public DataTable busquedaLbxRecibo(int opcion, string parametro)
        {
            if (opcion == 1)
            {

                StrSql = "SELECT rec_numcomprob 'Comprobante',rec_fecha 'Fecha',rec_cliente 'Cliente',rec_tippago 'Tipo', " +
                        "rec_monto 'Monto',rec_estado 'Estado' FROM admreciboingre WHERE rec_numcomprob LIKE  '%$1%' ORDER BY Comprobante  LIMIT 50";

                StrSql = StrSql.Replace("$1", parametro);
            }
            if (opcion == 2)
            {

                StrSql = "SELECT rec_numcomprob 'Comprobante',rec_fecha 'Fecha',rec_cliente 'Cliente',rec_tippago 'Tipo', " +
                        "rec_monto 'Monto',rec_estado 'Estado' FROM admreciboingre WHERE rec_cliente>='$1' ORDER BY Comprobante LIMIT 50";

                StrSql = StrSql.Replace("$1", parametro);
            }

            if (opcion == 3)
            {

                StrSql = "SELECT rec_numcomprob 'Comprobante',rec_fecha 'Fecha',rec_cliente 'Cliente',rec_tippago 'Tipo', " +
                        "rec_monto 'Monto',rec_estado 'Estado' FROM admreciboingre WHERE rec_tippago='$1' ORDER BY Comprobante LIMIT 50";

                StrSql = StrSql.Replace("$1", parametro);
            }

            tabla = database.fDataTable(StrSql);
            database.cerrarConexion();
            return tabla;
        }


        /// <summary>
        /// ready
        /// </summary>
        ///


        public DataTable datosComprobante(string comprobante, string compania,string cliente)
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


            StrSql = " SELECT movcon_item, movcon_proveedor, movcon_numdoc, movcon_sufdoc,movcon_monto, " +
                         " movcon_tipdoc,movcon_tipo,movcon_fecaha,movcon_login,movcon_cuenta,movcon_auxiliar, movcon_descrip ,  " +
                         " movcon_status,movcon_numcomp,movcon_rif, pro_nombre,pro_rif,usu_nombre,admcuentascontables.cc_Descripcion   " +
                         " FROM admmovcontab   " +
                         " LEFT OUTER JOIN admproveedor ON pro_codigo=movcon_proveedor  " +
                         " LEFT OUTER JOIN sysconf.confusuario ON sysconf.confusuario.usu_codigo =movcon_login  " +
                         " LEFT OUTER JOIN " + bd + ".admcuentascontables ON " + bd + ".admcuentascontables.cc_CodigoCuenta = movcon_cuenta  " +
                         " WHERE movcon_numdoc='$1' AND movcon_proveedor='$2'  order By length(movcon_item), movcon_item;";



            StrSql = StrSql.Replace("$1", comprobante);
            StrSql = StrSql.Replace("$2", cliente);

            return database.fDataTable(StrSql);
        }



        /// <summary>
        /// ready
        /// </summary>
        ///


        public DataTable rptReciboCabecera(string compro) 
        {
            StrSql = "SELECT 'cabecera' as 'origen',rec_tippago,rec_cliente,rec_fecha,rec_estado,rec_numcomprob, " +
                "rec_usuario,rec_cobrador,rec_monto,cli_nombre,cli_direc1,cli_telefono, sysconf.confusuario.usu_nombre FROM admreciboingre " +
                " LEFT OUTER JOIN admclientes ON admclientes.cli_codigo=rec_cliente "+
                " LEFT OUTER JOIN sysconf.confusuario ON sysconf.confusuario.usu_codigo = admreciboingre.rec_usuario "+
                "WHERE rec_numcomprob='$1'";

            StrSql = StrSql.Replace("$1", compro);
            tabla = database.fDataTable(StrSql);
            database.cerrarConexion();
            return tabla;
        }

        public DataTable rptReciboFormaPago(string compro)
        {
            StrSql = "SELECT 'formaPago' as 'origen',movrec_forpag,movrec_codban,movrec_xbanco,movrec_cuentaban,movrec_nuncheque,movrec_monto " +
                " FROM admreciboingre_det WHERE movrec_numero='$1' ";

            StrSql = StrSql.Replace("$1", compro);
            tabla = database.fDataTable(StrSql);
            database.cerrarConexion();
            return tabla;
        }

        public DataTable rptReciboDocu(string compro)
        {
            StrSql = "SELECT 'documentos' as 'origen',ingreDoc_tipo,ingreDoc_NumDoc,ingreDoc_facafe,ingreDoc_fechaFact,ingreDoc_MontoCancelado,ingreDoc_Nrecibo" +
                    " FROM admreciboingre_doc WHERE ingreDoc_numcomprob='$1'";

            StrSql = StrSql.Replace("$1", compro);
            tabla = database.fDataTable(StrSql);
            database.cerrarConexion();
            return tabla;

        }

        /// <summary>
        /// ready 
        /// </summary>
        /// 



        /// <summary>
        /// PRUEBA RPT
        /// </summary>
        /// <param name="cliente"></param>

        public DataTable rptReciboCabecera2(string compro)
        {
            //StrSql = "SELECT 'cabecera' as 'origen',rec_tippago,rec_cliente,rec_fecha,rec_estado,rec_numcomprob, " +
            //    "rec_usuario,rec_cobrador,rec_monto,cli_nombre,cli_direc1,cli_telefono, sysconf.confusuario.usu_nombre FROM admreciboingre " +
            //    " LEFT OUTER JOIN admclientes ON admclientes.cli_codigo=rec_cliente " +
            //    " LEFT OUTER JOIN sysconf.confusuario ON sysconf.confusuario.usu_codigo = admreciboingre.rec_usuario " +
            //    "WHERE rec_numcomprob='$1'";

            StrSql = "SELECT 'cabecera' AS 'origen',rec_tippago 'tipo',rec_cliente 'ban_cliente',rec_fecha 'fecha',rec_estado,rec_numcomprob 'compro_cuenta', rec_usuario,rec_cobrador,rec_monto 'monto',cli_nombre 'cliente_nomBanco',cli_direc1,cli_telefono, sysconf.confusuario.usu_nombre 'usuario_cheque' " +
                    "FROM admreciboingre " +
                    "LEFT OUTER " +
                    "JOIN admclientes ON admclientes.cli_codigo=rec_cliente " +
                    "LEFT OUTER " +
                    "JOIN sysconf.confusuario ON sysconf.confusuario.usu_codigo = admreciboingre.rec_usuario " +
                    "WHERE rec_numcomprob='$1'";

            StrSql = StrSql.Replace("$1", compro);
            tabla = database.fDataTable(StrSql);
            database.cerrarConexion();
            return tabla;
        }

        public DataTable rptReciboFormaPago2(string compro)
        {
            //StrSql = "SELECT 'formaPago' as 'origen',movrec_forpag,movrec_codban,movrec_xbanco,movrec_cuentaban,movrec_nuncheque,movrec_monto " +
            //    " FROM admreciboingre_det WHERE movrec_numero='$1' ";

            StrSql = " SELECT 'formaPago' AS 'origen',movrec_forpag 'tipo',movrec_codban,movrec_xbanco 'cliente_nomBanco',movrec_cuentaban 'compro_cuenta',movrec_nuncheque 'usuario_cheque',movrec_monto 'monto' " +
                     "FROM admreciboingre_det " +
                     "WHERE movrec_numero='$1' ";

            StrSql = StrSql.Replace("$1", compro);
            tabla = database.fDataTable(StrSql);
            database.cerrarConexion();
            return tabla;
        }

        public DataTable rptReciboDocu2(string compro)
        {
            //StrSql = "SELECT 'documentos' as 'origen',ingreDoc_tipo,ingreDoc_NumDoc,ingreDoc_facafe,ingreDoc_fechaFact,ingreDoc_MontoCancelado,ingreDoc_Nrecibo" +
            //        " FROM admreciboingre_doc WHERE ingreDoc_numcomprob='$1'";

            StrSql = " SELECT 'documentos' AS 'origen',ingreDoc_tipo 'tipo',ingreDoc_NumDoc 'compro_cuenta',ingreDoc_facafe,ingreDoc_fechaFact 'fecha',ingreDoc_MontoCancelado 'monto',ingreDoc_Nrecibo " +
                     "FROM admreciboingre_doc " +
                     "WHERE ingreDoc_numcomprob='$1'";

            StrSql = StrSql.Replace("$1", compro);
            tabla = database.fDataTable(StrSql);
            database.cerrarConexion();
            return tabla;

        }

        /// <summary>
        /// PRUEBA RPT
        /// </summary>
        /// <param name="cliente"></param>


        public void suspenderCLientes(string cliente)
        {

            StrSql = "UPDATE admclientes clientes INNER JOIN( " +
                            "SELECT Datediff(curdate(),FechaEmision) AS DIFERENCIA,cli_codigo " +
                            "FROM admsalcli saldo  WHERE cli_codigo='$1' AND Status=0 AND tipodoc <> 'DEV' " +
                            "AND codvend <0000000080 AND Datediff(curdate(),FechaEmision)>(SELECT mora_fact1 FROM admcondpago " +
                            "LEFT OUTER JOIN admclientes ON conp_descripcion=cli_condipag " +
                            "WHERE conp_descripcion=cli_condipag AND cli_codigo='$1' " +
                            "AND mora_fact1>0)) lala  " +
                            "ON clientes.cli_codigo = lala.cli_codigo " +
                            "SET clientes.cli_situacion ='Suspendido' WHERE clientes.cli_codigo='$1'";

            StrSql = StrSql.Replace("$1", cliente);
            database.ejecutarInsert(StrSql);
            database.cerrarConexion();

        }

        /// <summary>
        /// ready 
        /// </summary>
        /// 
        
        public void habilitarCliente(string cliente)
        {
            StrSql = "update admclientes set cli_situacion = 'Activo'  where cli_codigo='$1'";
            StrSql = StrSql.Replace("$1", cliente);
            database.ejecutarInsert(StrSql);
            database.cerrarConexion();
        }

        /// <summary>
        /// ready
        /// </summary>
        /// 

        
    }


}
