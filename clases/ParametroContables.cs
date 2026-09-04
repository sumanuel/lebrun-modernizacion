using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using lebrun.clasesData;
using System.Data;
using MySql.Data.MySqlClient;

namespace lebrun.clasesData
{
    public class ParametroContables
    {
        private string idSistema;
        private string descripcion;
        private string ultimoCierreMensual;
        private string ultimoCierreAnual;
        private Boolean consolidaContabilidad;
        private Boolean comprobantesDescuadrados;
        private int digitosCuenta;
        private string cuentaIngresos1GP;
        private string cuentaIngresos2GP;
        private string cuentaSuperACli;
        private string mascaraImpresion;
        private string correlativoCuenta;
        private string claveComprobacion;
        private Boolean parametroGlobal;
        private string siglas;
        private string ultimoCierreDiario;
        
        
        //base de datos
        private ConexionBD baseDatos;
        private string sentencia;
        private DataSet ds;
        private MySqlDataReader dr;

   

        //acesso a mienbros de clase 
        public string IdSistema
        {
            get { return idSistema; }
            set { idSistema = value; }
        }
        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }
        public string CuentaIngresos1GP
        {
            get { return cuentaIngresos1GP; }
            set { cuentaIngresos1GP = value; }
        }
        public string CuentaIngresos2GP
        {
            get { return cuentaIngresos2GP; }
            set { cuentaIngresos2GP = value; }
        }
        public string CuentaSuperACli
        {
            get { return cuentaSuperACli; }
            set { cuentaSuperACli = value; }
        }
        public string MascaraImpresion
        {
            get { return mascaraImpresion; }
            set { mascaraImpresion = value; }
        }
        public string Siglas
        {
            get { return siglas; }
            set { siglas = value; }
        }
        public int DigitosCuenta
        {
            get { return digitosCuenta; }
            set { digitosCuenta = value; }
        }        
        public string CorrelativoCuenta
        {
            get { return correlativoCuenta; }
            set { correlativoCuenta = value; }
        }
        public string UltimoCierreMensual
        {
            get { return ultimoCierreMensual; }
            set { ultimoCierreMensual = value; }
        }
        public string UltimoCierreAnual
        {
            get { return ultimoCierreAnual; }
            set { ultimoCierreAnual = value; }
        }
        public Boolean ConsolidaContabilidad
        {
            get { return consolidaContabilidad; }
            set { consolidaContabilidad = value; }
        }
        public Boolean ParametroGlobal
        {
            get { return parametroGlobal; }
            set { parametroGlobal = value; }
        }
        public string ClaveComprobacion
        {
            get { return claveComprobacion; }
            set { claveComprobacion = value; }
        }
        public Boolean ComprobantesDescuadrados
        {
            get { return comprobantesDescuadrados; }
            set { comprobantesDescuadrados = value; }
        }
        public string UltimoCierreDiario
        {
            get { return ultimoCierreDiario; }
            set { ultimoCierreDiario = value; }
        }

        public ParametroContables() {
            baseDatos = new ConexionBD(3);
        }

        public Boolean agregarParametro(string compania,string idBaseDatos){
            baseDatos.conectionStringSysconta(idBaseDatos);
            sentencia = "INSERT INTO admparametroscontables (pc_idCia,pc_idSistema, " +
                         " pc_Descripcion, pc_FechaUltCiem, pc_FechaUltCieA, pc_FlagConsol, " +
                         " pc_FlagDescuadre, pc_DigitosContab, pc_CtaContabIng1, pc_CtaContablng2, " +
                         " pc_CtaSupAvitCie, pc_Mascara, pc_CorrCompAut, pc_ClaveComprob, pc_FlagGral," +
                         " pc_Siglas,pc_FechaUltimaDiario) VALUES ('" +
                          compania + "', '" + idBdCompania() +"', '" + this.Descripcion + "', '" +
                            this.UltimoCierreMensual + "', '" + this.UltimoCierreAnual +
                            "', " + this.ConsolidaContabilidad + ","+ this.parametroGlobal +"," + this.DigitosCuenta + ", '" +
                            this.CuentaIngresos1GP + "', '" + this.CuentaIngresos2GP + "', '" + this.CuentaSuperACli + "' ," +
                            "'" + this.MascaraImpresion + "', '" + this.CorrelativoCuenta + "','" + this.ClaveComprobacion + "'," +
                            this.ParametroGlobal + ", '" + this.Siglas + "','"+this.ultimoCierreDiario+"');";            
            baseDatos.ejecutarInsert(sentencia);
            MessageBox.Show("Parametro Agregado Exitosamente!!","Exito",MessageBoxButtons.OK,MessageBoxIcon.Information);
            return true;
        }// fin de agregarParametro

        public Boolean existeParametro(string id)  {            
            sentencia = "SELECT pc_idSistema FROM admparametroscontables WHERE  pc_idSistema='" + id + "';";
            if (baseDatos.sentenciasNumeroFilas(sentencia) <= 0){                   
                return false;
            }
            else {                
                return true;
            }

        }

        private string idBdCompania() {
            string siguiente = null;
            sentencia = "SELECT pc_idSistema FROM admparametroscontables ORDER BY pc_idSistema desc limit 1";            
            ds = baseDatos.ejecutarQueryDs(sentencia);

            if (ds.Tables[0].Rows.Count ==0)
            {
                siguiente = "01";
            }
            else
            {
                if (int.Parse((ds.Tables[0].Rows[0][0].ToString())) + 1 > 10)
                {
                    siguiente = Convert.ToString(int.Parse((ds.Tables[0].Rows[0][0].ToString())) + 1);
                }
                else {
                    siguiente = "0" + Convert.ToString(int.Parse((ds.Tables[0].Rows[0][0].ToString())) + 1);                    
                }
            }

            return siguiente;
        }

        public void cargarParametroCon(string idParam, string idBaseDatos) {
            baseDatos.conectionStringSysconta(idBaseDatos);
            sentencia = "SELECT pc_Descripcion,pc_FechaUltCiem,pc_FechaUltCieA,pc_FlagConsol,pc_FlagDescuadre,pc_DigitosContab, " +
                        "pc_CtaContabIng1, pc_CtaContablng2, pc_CtaSupAvitCie, pc_Mascara, pc_CorrCompAut, pc_ClaveComprob, " +
                        " pc_FlagGral, pc_Siglas,pc_FechaUltimaDiario FROM admparametroscontables WHERE pc_idSistema='" + idParam + "';";
            dr = baseDatos.ejecutarQueryDr(sentencia);
            while (dr.Read()) {
                this.descripcion = dr.GetString("pc_Descripcion");
                this.ultimoCierreMensual = Convert.ToString(dr.GetDateTime("pc_FechaUltCiem"));
                this.ultimoCierreAnual = Convert.ToString(dr.GetDateTime("pc_FechaUltCieA"));                
                this.consolidaContabilidad = dr.GetBoolean("pc_FlagConsol");
                this.comprobantesDescuadrados = dr.GetBoolean("pc_FlagDescuadre");
                this.digitosCuenta = dr.GetInt16("pc_DigitosContab");
                this.cuentaIngresos1GP = dr.GetString("pc_CtaContabIng1");
                this.cuentaIngresos2GP = dr.GetString("pc_CtaContablng2");
                this.cuentaSuperACli = dr.GetString("pc_CtaSupAvitCie");
                this.mascaraImpresion = dr.GetString("pc_Mascara");
                this.correlativoCuenta = dr.GetString("pc_CorrCompAut");
                this.claveComprobacion = dr.GetString("pc_ClaveComprob");
                this.parametroGlobal = dr.GetBoolean("pc_FlagGral");
                this.siglas = dr.GetString("pc_Siglas");
                this.ultimoCierreDiario = Convert.ToString(dr.GetDateTime("pc_FechaUltimaDiario"));
            }
            dr.Close();
            baseDatos.cerrarConexion();
        }

        public Boolean actualizarParametro(string idBaseDatos) {
            baseDatos.conectionStringSysconta(idBaseDatos);
            sentencia = "UPDATE admparametroscontables SET pc_Descripcion='" + this.descripcion + "'," +
                        "pc_FechaUltCiem='" + this.ultimoCierreMensual + "', pc_FechaUltCieA='" + this.ultimoCierreAnual + "'," +
                        "pc_FlagConsol=" + this.consolidaContabilidad + ", pc_FlagDescuadre=" + this.comprobantesDescuadrados + "," +
                        "pc_DigitosContab=" + this.digitosCuenta + ", pc_CtaContabIng1='" + this.cuentaIngresos1GP + "'," +
                        "pc_CtaContablng2='" + this.cuentaIngresos2GP + "',pc_CtaSupAvitCie='" + this.cuentaSuperACli + "'," +
                        "pc_Mascara='" + this.mascaraImpresion + "',pc_ClaveComprob='" + this.claveComprobacion + "'," +
                        "pc_FlagGral=" + this.parametroGlobal + ", pc_Siglas='" + this.siglas + "', pc_FechaUltimaDiario='" + this.ultimoCierreDiario + "' WHERE pc_idSistema='" + this.idSistema + "';";            
            baseDatos.ejecutarInsert(sentencia);
            MessageBox.Show("Parametro Modificado Exitosamente!!", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return true;
        }

        public void eliminarParametro(string id,string idBaseDatos) {
            baseDatos.conectionStringSysconta(idBaseDatos);
            sentencia = "DELETE FROM admparametroscontables WHERE pc_idSistema='" + id + "'";            
            baseDatos.ejecutarInsert(sentencia);
        }
    }

}
