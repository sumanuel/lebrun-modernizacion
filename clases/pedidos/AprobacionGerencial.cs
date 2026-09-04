using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using lebrun.clasesData;
using lebrun.clases.facturacion;
namespace lebrun.clases.pedidos
{
    public  class AprobacionGerencial
    {
        private string descripcion;
        private bool status;
        private int id;
        private Decimal monto;
        ConexionBD databasesisadm;
        string sentencia = null;

        #region get sets
        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }
        public bool Status
        {
            get { return status; }
            set { status = value; }
        }
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public Decimal Monto
        {
            get { return monto; }
            set { monto = value; }
        }
        #endregion
        
        public AprobacionGerencial() {
            databasesisadm = new ConexionBD();
        }

        public AprobacionGerencial(string a)
        {   
        }

        public bool reglasActivas() {
            bool centinela = false;
            DataTable dt;
            sentencia = null;
            sentencia = "SELECT descripcion FROM admaprobgerent WHERE id=1 AND status=1;";

            dt = databasesisadm.fDataTable(sentencia);
            if(dt.Rows.Count>0)
            {
                centinela = true;
            }
            
            databasesisadm.cerrarConexion();
            return centinela;
        }


        public DataTable listaReglas()
        {
            DataTable dt= null;
            sentencia = null;

            sentencia = "SELECT id '@id', status '@status',descripcion '@descripcion', monto '@monto' FROM admaprobgerent;";
            dt = databasesisadm.fDataTable(sentencia);

            databasesisadm.cerrarConexion();
            return dt;
        }

        public bool modificarRegla() {
            bool centinela = false;
            sentencia = null;

            sentencia = "UPDATE admaprobgerent SET descripcion='@descripcion', monto='@monto',status='@status' WHERE id='@id';";
            sentencia = sentencia.Replace("@descripcion", this.descripcion);
            sentencia = sentencia.Replace("@monto", ""+this.monto);
            sentencia = sentencia.Replace("@status", "" + Convert.ToInt32(this.status));
            sentencia = sentencia.Replace("@id", ""+this.id);

            centinela = databasesisadm.ejecutarInsert(sentencia);
            databasesisadm.cerrarConexion();

            return centinela;
        }

        public void limpiar() 
        {
            this.id = 0;
            this.descripcion = string.Empty;
            this.monto = 0;
            this.status = false;
        }

        public decimal maxDcto()
        {
            DataTable dt = null;
            decimal montoD = 0;
            sentencia = null;
            sentencia = "SELECT monto FROM admaprobgerent WHERE descripcion='Máximo Dtco. a otorgar en Precio'";
            dt = databasesisadm.fDataTable(sentencia);
            if (dt.Rows.Count > 0)
            {
                montoD = Convert.ToDecimal(dt.Rows[0]["monto"].ToString());
            }
            databasesisadm.cerrarConexion();
            return montoD;
        }
    }
}
