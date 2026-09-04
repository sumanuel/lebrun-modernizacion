using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using lebrun.clasesData;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace lebrun.clases.importaciones
{
    public class OrdenDeCompra
    {

        //Para la BD

        private string StrSql;
        private ConexionBD database;
        private MySqlDataReader dr;
        private DataSet ds;

        //variables

        private string correlativo;
        private string tipdoc;
        private string ordencompra;
        private string codigo;
        private string concep;
        private string cencost;
        private string sucus;
        private string moneda;
        private string codtra;
        private string condic;
        private string usuario;
        private string updUsuario;
        private string tiptra;
        private string origen;
        private string estadodoc;
        private string status;
        private string cbtnum;
        private string docorg;
        private string cbtpre;
        private string pagnum;
        private string cxp;
        private string plazo;
        private string valcamb;
        private string totaldivisa;
        private string prod;
        private string ditem;
        private string ord;
        private string fechaCarga;
        private string fechaRecep;
        private string documento;
        private string sufijo;
        private string proNombre;
        private string proRif;
        private string subTotal;
        private string total;
        private string fechaSistema;
        private string login;
        private string updLogin;
        private string fechaUltMod;








        //get & set

    public string Correlativo
    {
        get { return correlativo; }
        set { correlativo = value; }
    }

    public string Tipdoc
    {
        get { return tipdoc; }
        set { tipdoc = value; }
    }

    public string Ordencompra
    {
        get { return ordencompra; }
        set { ordencompra = value; }
    }

    public string Codigo
    {
        get { return codigo; }
        set { codigo = value; }
    }

    public string Concep
    {
        get { return concep; }
        set { concep = value; }
    }

    public string Cencost
    {
        get { return cencost; }
        set { cencost = value; }
    }

    public string Sucus
    {
        get { return sucus; }
        set { sucus = value; }
    }

    public string Moneda
    {
        get { return moneda; }
        set { moneda = value; }
    }

    public string Codtra
    {
        get { return codtra; }
        set { codtra = value; }
    }

    public string Condic
    {
        get { return condic; }
        set { condic = value; }
    }

    public string Usuario
    {
        get { return usuario; }
        set { usuario = value; }
    }

    public string UpdUsuario
    {
        get { return updUsuario; }
        set { updUsuario = value; }
    }

    public string Tiptra
    {
        get { return tiptra; }
        set { tiptra = value; }
    }

    public string Origen
    {
        get { return origen; }
        set { origen = value; }
    }

    public string Estadodoc
    {
        get { return estadodoc; }
        set { estadodoc = value; }
    }

    public string Status
    {
        get { return status; }
        set { status = value; }
    }

    public string Cbtnum
    {
        get { return cbtnum; }
        set { cbtnum = value; }
    }

    public string Docorg
    {
        get { return docorg; }
        set { docorg = value; }
    }

    public string Cbtpre
    {
        get { return cbtpre; }
        set { cbtpre = value; }
    }

    public string Pagnum
    {
        get { return pagnum; }
        set { pagnum = value; }
    }

    public string Cxp
    {
        get { return cxp; }
        set { cxp = value; }
    }

    public string Plazo
    {
        get { return plazo; }
        set { plazo = value; }
    }

    public string Valcamb
    {
        get { return valcamb; }
        set { valcamb = value; }
    }

    public string Totaldivisa
    {
        get { return totaldivisa; }
        set { totaldivisa = value; }
    }

    public string Prod
    {
        get { return prod; }
        set { prod = value; }
    }

    public string Ditem
    {
        get { return ditem; }
        set { ditem = value; }
    }

    public string Ord
    {
        get { return ord; }
        set { ord = value; }
    }

    public string FechaCarga
    {
        get { return fechaCarga; }
        set { fechaCarga = value; }
    }

    public string FechaRecep
    {
        get { return fechaRecep; }
        set { fechaRecep = value; }
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

    public string ProNombre
    {
        get { return proNombre; }
        set { proNombre = value; }
    }

    public string ProRif
    {
        get { return proRif; }
        set { proRif = value; }
    }

    public string SubTotal
    {
        get { return subTotal; }
        set { subTotal = value; }
    }

    public string Total
    {
        get { return total; }
        set { total = value; }
    }

    public string FechaSistema
    {
        get { return fechaSistema; }
        set { fechaSistema = value; }
    }

    public string FechaUltMod
    {
        get { return fechaUltMod; }
        set { fechaUltMod = value; }
    }





    //conexion

    public OrdenDeCompra() {
        database = new ConexionBD(); //sisadm
    }

    public void guardarOrdenCompra() {

        StrSql = "INSERT INTO admOrdenCompra (ord_correlativo,ord_tipdoc,ord_ordencompra,ord_concep,ord_cencost,ord_sucus, " +
        " ord_moneda,ord_codtra,ord_condic,ord_usuario,ord_tiptra,ord_codigo,ord_origen,ord_estadodoc,ord_status, " +
        " ord_fechCarg,ord_hora,ord_cbtnum,ord_docorg,ord_cbtpre,ord_pagnum,ord_cxp,ord_plazo,ord_valcamb, " +
        " ord_totaldivisa,ord_prod,ord_ditem,ord_documento,ord_sufijo,ord_subtot,ord_total,ord_usuarioUdp,ord_fechaRecep,ord_fechaSistema,ord_fechaUltMod) VALUES('$1','$2','$3','$4','$5','$6','$7','$8','$9','$usuario','$tiptra', " +
        " '$codigo','$origen','$estadodoc','$status','$fechCarg','$hora','$cbtnum','$docorg','$cbtpre','$pagnum','$cxp','$plazo','$valcamb', " +
        " '$totaldivisa','$prod','$ord','$documento','$sufijo','$subtotal','$total','$updUsuario','$fechaRecep','$fSistema','$ultMod');";

        StrSql = StrSql.Replace("$1", this.correlativo); 
        StrSql = StrSql.Replace("$2", this.tipdoc);
        StrSql = StrSql.Replace("$3", this.correlativo);
        StrSql = StrSql.Replace("$4", this.concep);
        StrSql = StrSql.Replace("$5", this.cencost);
        StrSql = StrSql.Replace("$6", this.sucus);
        StrSql = StrSql.Replace("$7", this.moneda);
        StrSql = StrSql.Replace("$8", this.codtra);
        StrSql = StrSql.Replace("$9", this.condic);
        StrSql = StrSql.Replace("$usuario", this.usuario);
        StrSql = StrSql.Replace("$tiptra", this.tiptra);
        StrSql = StrSql.Replace("$codigo", this.codigo);
        StrSql = StrSql.Replace("$origen", this.origen);
        StrSql = StrSql.Replace("$estadodoc", this.estadodoc);
        StrSql = StrSql.Replace("$status", this.status);
        StrSql = StrSql.Replace("$fechCarg", this.FechaCarga);
        StrSql = StrSql.Replace("$hora", ""+DateTime.Now.Hour +":"+ DateTime.Now.Minute +":"+ DateTime.Now.Second);
        StrSql = StrSql.Replace("$cbtnum", this.cbtnum);
        StrSql = StrSql.Replace("$docorg", this.docorg);
        StrSql = StrSql.Replace("$cbtpre", this.cbtpre);
        StrSql = StrSql.Replace("$pagnum", this.pagnum);
        StrSql = StrSql.Replace("$cxp", this.cxp);
        StrSql = StrSql.Replace("$plazo", this.plazo);
        StrSql = StrSql.Replace("$valcamb", this.valcamb);
        StrSql = StrSql.Replace("$totaldivisa", this.totaldivisa);
        StrSql = StrSql.Replace("$prod", this.prod);
        StrSql = StrSql.Replace("$ord", this.ditem);
        StrSql = StrSql.Replace("$documento",this.documento);
        StrSql = StrSql.Replace("$sufijo",this.sufijo);
        StrSql = StrSql.Replace("$subtotal",""+this.subTotal);
        StrSql = StrSql.Replace("$total", ""+this.total);
        StrSql = StrSql.Replace("$usuario",this.usuario);
        StrSql = StrSql.Replace("$fechaRecep", this.fechaRecep);
        StrSql = StrSql.Replace("$updUsuario", this.updUsuario);
        StrSql = StrSql.Replace("$ultMod", this.fechaUltMod);

        if (fechaSistema!= null)
        {
            StrSql = StrSql.Replace("$fSistema", "" + Convert.ToDateTime(this.fechaSistema).Year + "-" + Convert.ToDateTime(this.fechaSistema).Month + "-" + Convert.ToDateTime(this.fechaSistema).Day);
        }
        else
        {
            StrSql = StrSql.Replace("$fSistema", "" + DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day);
        }

        database.ejecutarInsert(StrSql);
        database.cerrarConexion();
    }

    public void buscarOrdenCompra() {

        StrSql = "SELECT * FROM admOrdenCompra LEFT OUTER JOIN admproveedor ON pro_codigo = ord_codigo WHERE ord_correlativo='$1';";
        StrSql = StrSql.Replace("$1", this.correlativo);

        dr = database.ejecutarQueryDr(StrSql);

        if (dr.HasRows)
        {
            dr.Read();

            this.correlativo = dr.GetString("ord_correlativo");
            this.codigo = dr.GetString("ord_codigo");
            this.concep = dr.GetString("ord_concep");
            this.condic = dr.GetString("ord_condic");
            this.moneda = dr.GetString("ord_moneda");
            this.valcamb = Convert.ToString(dr.GetDecimal("ord_valcamb"));
            this.documento = dr.GetString("ord_documento");
            this.sufijo = dr.GetString("ord_sufijo");
            this.proNombre = dr.GetString("pro_nombre");
            this.proRif = dr.GetString("pro_rif");
            this.subTotal = Convert.ToString(dr.GetDecimal("ord_subtot"));
            this.total = Convert.ToString(dr.GetDecimal("ord_total"));
            this.prod = Convert.ToString(dr.GetDecimal("ord_prod"));
            this.ditem = Convert.ToString(dr.GetInt32("ord_ditem"));
            this.totaldivisa = Convert.ToString(dr.GetDecimal("ord_totaldivisa"));
            this.fechaCarga = Convert.ToString(dr.GetString("ord_fechCarg"));
            this.fechaRecep = Convert.ToString(dr.GetString("ord_fechaRecep"));
            this.fechaSistema = Convert.ToString(dr.GetString("ord_fechaSistema"));
            this.usuario = dr.GetString("ord_usuario");
            this.updUsuario = dr.GetString("ord_usuarioUdp");
        }

        database.cerrarConexion();
        dr.Close();
    }
        //

    public DataTable armarLbxOrdenCompra() {

        StrSql = "SELECT ord_correlativo 'Correlativo', ord_tipdoc 'Tipo Doc', ord_ordencompra 'Orden Compra', ord_documento 'Documento'," +
                    "ord_sufijo 'Sufijo',ord_concep 'Concepto', ord_codigo 'Cod Proveedor', pro_nombre 'Nombre Proveedor'" +
                    "FROM admOrdenCompra LEFT OUTER JOIN admproveedor ON ord_codigo=pro_codigo";
        
        return database.fDataTable(StrSql);
        //database.cerrarConexion();

    }

    public void eliminarCabeceraOrdenCompra() {
        StrSql = "DELETE FROM admordencompra WHERE ord_correlativo='$1';";
        StrSql = StrSql.Replace("$1",this.correlativo);
        database.ejecutarInsert(StrSql);
        database.cerrarConexion();
    }

    public void asignarCorrelativo(){

        ds = database.ejecutarQueryDs("SELECT tdc_correlativo FROM admtipdocpro WHERE tdc_codigo='30'");
        if (ds.Tables[0].Rows.Count != 0) {
            this.correlativo = String.Format("{0:0000000000}", Convert.ToDouble(ds.Tables[0].Rows[0][0].ToString()) + 1);

            database.ejecutarInsert("UPDATE admtipdocpro SET tdc_correlativo='" + this.correlativo + "' WHERE tdc_codigo='30'");
        
        }
        database.cerrarConexion();
      
    }

    public DataTable buscarOrdenCompra(int option, string value) {

  
            if (option == 0)
            {

                StrSql = "SELECT ord_correlativo 'Correlativo', ord_tipdoc 'Tipo Doc', ord_ordencompra 'Orden Compra', ord_documento 'Documento'," +
                    "ord_sufijo 'Sufijo',ord_concep 'Concepto', ord_codigo 'Cod Proveedor', pro_nombre 'Nombre Proveedor'" +
                    "FROM admOrdenCompra LEFT OUTER JOIN admproveedor ON ord_codigo=pro_codigo WHERE ord_codigo >='$1'";

                StrSql = StrSql.Replace("$1", value);
            }

            if (option == 1)
            {

                StrSql = "SELECT ord_correlativo 'Correlativo', ord_tipdoc 'Tipo Doc', ord_ordencompra 'Orden Compra', ord_documento 'Documento'," +
                   "ord_sufijo 'Sufijo',ord_concep 'Concepto', ord_codigo 'Cod Proveedor', pro_nombre 'Nombre Proveedor'" +
                   "FROM admOrdenCompra LEFT OUTER JOIN admproveedor ON ord_codigo=pro_codigo WHERE pro_nombre LIKE '%$1%'";
                
                StrSql = StrSql.Replace("$1", value);
            }

            if (option == 2)
            {

                StrSql = "SELECT ord_correlativo 'Correlativo', ord_tipdoc 'Tipo Doc', ord_ordencompra 'Orden Compra', ord_documento 'Documento'," +
                   "ord_sufijo 'Sufijo', ord_concep 'Concepto',ord_codigo 'Cod Proveedor', pro_nombre 'Nombre Proveedor'" +
                   "FROM admOrdenCompra LEFT OUTER JOIN admproveedor ON ord_codigo=pro_codigo WHERE ord_documento LIKE '$1%'";
                StrSql = StrSql.Replace("$1", value);
            }

        return database.fDataTable(StrSql);
       
    }

















    }//fin clase
}
