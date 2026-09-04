using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;
using lebrun.clasesData;
using lebrun.clases.producto;
using lebrun.formularios.productos;
using System.Windows.Forms;
using System.Configuration;

namespace lebrun.clases.producto
{
    class Productos
    {
        //Llmado de clases
        Principal SistemaUs;// llamado al la informacion de la pataklla principal
        private ConexionBD dataBaseConection;
        lbxCategoria Categoriafrm;
        frmEdicionArancel ArancelEditor;
        MySqlDataReader dr;
        
       
        //Declacion de variable
        private string senteciaSql;
        public string codigoA;
        public string descripA;
        public string kgA;
        public string proceA;
        public string impuestoA;
        public string tipoAlicIVA;
        public string ventaAlicIVA;
        public string compraAlicIVA;
        public string zonalibreAlicIVA;
        public string codigoCategoria;
        public string descripCategoria;
        public string ventaCategoria;
        public string compraCategoria;
        public string codigoSubCodig;
        public int total;
        public string codigoSubCat;
        public string descripSubCat;
        public string comvSubCat;
        public string comcSubCat;
        public string desLbxArancel;
        public string codLbxArancel;
        public string desLbxCategoria;
        public string codLbxCategoria;
        //busqueda frm modificar producto
        public string descripProduc;
        public string fabriProduc;
        public string categPruduc;
        public string desCatPruduc;
        public string subCatProduc;
        public string desSubCatProduc;
        public string aracelProduc;
        public string desAracProduc;
        public string ivaProduc;
        public string desEdit;
        public string procede;
        public string estado;
        public string obser1;
        public string obser2;
        public string obser3;
        public string pddPruduc;
        public string comCobProduc;
        public string comVenProduc;
        public string exMaxProduc;
        public string exMinProduc;
        public string gpmProduc;
        public string altaProduc;
        public string anchoProduc;
        public string volumProduc;
        public string capacidad;
        public string contenido;
        public string estadoProduc;
        public string exiteProdc;
        public string codPais;
        public string nombrePais;
        public string monedaPais;
        public string undDescrip;
        //Variable Unidad Medida
        public string undMed;
        public string undCant;
        public string undPrese;
        public string undPeso;
        public string undVolu;
        public string undComp;
        public string undvent;
        public string undSign;
        public string undPrin;
        //validacion precio
        public string preciosActivos;
        public string codiPreExite;


        //get set Producto
        public string CodiPreExite 
        {
            get { return codiPreExite; }
            set { codiPreExite = value; }
        }
        public string PreciosActivo 
        {
            get { return preciosActivos; }
            set { preciosActivos = value; }
        }
        public string UndMed 
        {
            get { return undMed; }
            set { undMed = value; }
        }
        public string UndCant 
        {
            get { return undCant; }
            set { undCant = value; }
        }
        public string UndPrese 
        {
            get { return undPrese; }
            set { undPrese = value; }
        }
        public string UndPeso
        {
            get { return undPeso; }
            set { undPeso = value; }
        }
        public string UndVol
        {
            get { return undVolu; }
            set { undVolu = value; }
        }
        public string UndCompr
       {
           get { return undComp; }
           set { undComp = value; }
       }
        public string UndVent 
        {
           get { return undvent; }
           set { undvent = value; }
        }
        public string UndSig 
        {
            get { return undSign; }
            set { undSign = value; }
        }
        public string UndPrin
        {
            get { return undPrin; }
            set { undPrin = value; }
        }


        public string UndDescrip 
        {
            get { return undDescrip; }
            set { undDescrip = value; }
        }
        public string NombrePais 
        {
            get { return nombrePais; }
            set { nombrePais = value; }
        }
        public string MonedaPais 
        {
            get { return monedaPais; }
            set { monedaPais= value; }
        }
        public string CodPais 
        {
            get { return codPais; }
            set { codPais = value; }
        }
        public string ExiteProdc 
        {
            get { return exiteProdc; }
            set { exiteProdc = value; }
        }
        public string EstadoProducto 
        {
            get {return estadoProduc;}
            set {estadoProduc = value;}
        }
        public string DescriProduc 
        {
            get { return descripProduc;}
            set { descripProduc = value;}
        }
        public string FabriProduc 
        {
            get { return fabriProduc; }
            set { fabriProduc = value; }
        }
        public string CategProduc 
        {
            get { return categPruduc; }
            set { categPruduc = value; }
        }
        public string desCateProduc 
        {
            get { return desSubCatProduc; }
            set { desSubCatProduc = value; }
        }
        public string SubCatProduc 
        {
            get { return subCatProduc; }
            set { subCatProduc = value; }
        }
        public string DesSubCate
        {
            get { return desSubCatProduc; }
            set { desSubCatProduc = value; }
        }
        public string AracelProduc
        {
            get { return aracelProduc; }
            set { aracelProduc = value; }
        }
        public string DesAranProduc 
        {
            get { return desAracProduc; }
            set { desAracProduc = value; }
        }
        public string IvaProduc 
        {
            get { return ivaProduc; }
            set { ivaProduc = value; }
        }
        public string DesEdit 
        {
            get { return desEdit; }
            set { desEdit = value; }
        }
        public string Procede 
        {
            get { return procede; }
            set { procede = value; }
        }
        public string Estado 
        {
            get { return estado; }
            set { estado = value; }
        }
        public string Obser1 
        {
            get { return obser1; }
            set { obser1 = value; }
        }
        public string Obser2
        {
            get { return obser2; }
            set { obser2 = value; }
        }
        public string Obser3
        {
            get { return obser3; }
            set { obser3 = value; }
        }
        public string PDDProd 
        {
            get { return pddPruduc; }
            set { pddPruduc = value; }
        }
        public string ComCobProd 
        {
            get { return comCobProduc; }
            set { comCobProduc = value; }
        }
        public string ComVenProduc
        {
            get { return comVenProduc; }
            set { comVenProduc = value; }
        }
        public string ExMaxProd
        {
            get { return exMaxProduc; }
            set { exMaxProduc = value; }
        }
        public string ExMinProd
        {
            get { return exMinProduc; }
            set { exMinProduc = value; }
        }
         public string GPMProd
        {
            get { return gpmProduc; }
            set { gpmProduc = value; }
        }
         public string AltaProd 
         {
             get { return altaProduc; }
             set { altaProduc = value; }
         }
         public string AnchoProd 
         {
             get { return anchoProduc; }
             set { anchoProduc = value; }
         }
         public string VoluProd
         {
             get { return volumProduc; }
             set { volumProduc = value; }
         }
         public string CapaProd
         {
             get { return capacidad; }
             set { capacidad = value; }
         }
         public string Contenido 
         {
             get { return contenido; }
             set { contenido = value; }
         
         }

        //
        public string DesLbxArancel
        {
            get { return desLbxArancel; }
            set { desLbxArancel = value; }
        }
        public string CodLbxArancel 
        {
            get { return codLbxArancel; }
            set { codLbxArancel = value; }
        }
 
        public Productos()
        {
           this.dataBaseConection = new ConexionBD("50000");
           ArancelEditor = new frmEdicionArancel();
           Categoriafrm = new lbxCategoria();
           
        }

        public Productos(string timerout)
        {
            this.dataBaseConection=new ConexionBD(timerout);
        }
        
        // validacion de Exitecia de sub Categoria
        public bool existeSubCategoria()
        {
            bool Ciclope = false;
            senteciaSql = null;
            MySqlDataReader dr;
            senteciaSql = "SELECT sgr_codigo FROM admsubgrupo WHERE sgr_codigo='@codigo' AND sgr_grupo=@scodigo";
            senteciaSql = senteciaSql.Replace("@codigo", codigoCategoria);
            senteciaSql = senteciaSql.Replace("@scodigo", codigoSubCat);
            dr = dataBaseConection.ejecutarQueryDr(senteciaSql, 200);
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    if (dr.GetString(0) != "")
                    {
                        Ciclope = true;
                    }
                }
                dr.Close();
            }
            dataBaseConection.cerrarConexion();
            return Ciclope;   
        }
        //Validacion De Exitecia de Categoria
        public bool exesteCategoria()
        {
            bool Ciclope = false;
            senteciaSql = null;
            MySqlDataReader dr;
            senteciaSql = "SELECT gru_codigo FROM admgrupo WHERE gru_codigo='@codigo';";
            senteciaSql = senteciaSql.Replace("@codigo",codigoCategoria);
            dr = dataBaseConection.ejecutarQueryDr(senteciaSql, 200);
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    if (dr.GetString(0) != "")
                    {
                        Ciclope = true;
                    }
                }
                dr.Close();
            }
            dataBaseConection.cerrarConexion();
            return Ciclope;    
        }
        public DataTable buscarProductoFrm(string opcion, string value, bool condicion)
        {
            DataTable dt;
            if ((opcion.Equals("")) && (opcion.Equals("")) || (opcion.Equals(null)) && (opcion.Equals(null)))
            {
                senteciaSql = "SELECT inv_codigo AS Codigo,inv_descri AS Descripcion, " +
                           " inv_iva AS AlicuotaIva, " +
                           " inv_proced AS Procedencia,inv_utiliz AS Utilidad,inv_codfab AS Fabricante,gru_descri AS Categoria, " +
                           " sgr_descri AS SubCategoria,mar_descri AS Marca,div_descri AS Division FROM adminv " +
                           " LEFT JOIN admmarcas on admmarcas.mar_codigo=adminv.inv_codmarca " +
                           " LEFT JOIN admgrupo on admgrupo.gru_codigo=adminv.inv_grupo " +
                           " LEFT JOIN admsubgrupo on admsubgrupo.sgr_codigo=adminv.inv_subgru " +
                           " LEFT JOIN admdivision on admdivision.div_codigo=adminv.inv_divisi GROUP BY inv_codigo  ORDER BY INV_CODIGO LIMIT 200";
            }
            else
            {
                if (condicion == true)
                {
                    if (opcion.Equals("inv_codigo"))
                    {
                        value = value.PadLeft(15, '0');
                        opcion = "inv_codigo>=";
                    }
                    if (opcion.Equals("inv_descri") || opcion.Equals("inv_iva") || opcion.Equals("gru_descri") || opcion.Equals("inv_proced") || opcion.Equals("sgr_descri") || opcion.Equals("inv_codfab"))
                    {
                        value = " LIKE '%" + value + "%'";
                    }
                    senteciaSql = "SELECT inv_codigo AS Codigo,inv_descri AS Descripcion, " +
                                " inv_iva AS AlicuotaIva, " +
                                " gru_descri AS Categoria, " +
                                " sgr_descri AS SubCategoria FROM adminv " +
                                " LEFT JOIN admmarcas on admmarcas.mar_codigo=adminv.inv_codmarca " +
                                " LEFT JOIN admgrupo on admgrupo.gru_codigo=adminv.inv_grupo " +
                                " LEFT JOIN admsubgrupo on admsubgrupo.sgr_codigo=adminv.inv_subgru " +
                                " LEFT JOIN admdivision on admdivision.div_codigo=adminv.inv_divisi WHERE " + opcion + " " + value + " GROUP BY inv_codigo  ORDER BY INV_CODIGO LIMIT 200";
                }
                else 
                {
                    if (opcion.Equals("inv_codigo"))
                    {
                        opcion = "inv_codigo>=";
                        value = "'"+value+"'";
                    }
                    if (opcion.Equals("inv_descri") || opcion.Equals("inv_iva") || opcion.Equals("gru_descri") || opcion.Equals("inv_proced") || opcion.Equals("sgr_descri") || opcion.Equals("inv_codfab"))
                    {
                        value = "LIKE'"+value+"%'";
                    }
                    senteciaSql = "SELECT inv_codigo AS Codigo,inv_descri AS Descripcion, " +
                                " inv_iva AS AlicuotaIva, " +
                                " gru_descri AS Categoria, " +
                                " sgr_descri AS SubCategoria FROM adminv" +
                                " LEFT JOIN admmarcas on admmarcas.mar_codigo=adminv.inv_codmarca " +
                                " LEFT JOIN admgrupo on admgrupo.gru_codigo=adminv.inv_grupo " +
                                " LEFT JOIN admsubgrupo on admsubgrupo.sgr_codigo=adminv.inv_subgru " +
                                " LEFT JOIN admdivision on admdivision.div_codigo=adminv.inv_divisi WHERE " + opcion + " " + value + " GROUP BY inv_codigo  ORDER BY INV_CODIGO LIMIT 200";
                }
            }
            dt = dataBaseConection.fDataTable(senteciaSql);
            dataBaseConection.cerrarConexion();
            return dt;
        }
        //Validacion De Existencia Del Arancel
        public bool existeArancel() 
        {
            bool Ciclope = false;
            senteciaSql = null;
            MySqlDataReader dr;
            senteciaSql = "SELECT ara_codigo FROM admarancel WHERE ara_codigo='@codigo';";
            senteciaSql = senteciaSql.Replace("@codigo",codigoA);
            dr = dataBaseConection.ejecutarQueryDr(senteciaSql,200);
            if (dr.HasRows) 
            {
             while(dr.Read())
             {
                 if(dr.GetString(0)!="")
                 {
                     Ciclope = true;
                 }
                
             }
             dr.Close();
            }
            dataBaseConection.cerrarConexion();
            return Ciclope;
        }

        //contador Categoria de tabla
        public DataTable contadorCategira()
        {
            DataTable dt;
            int resultado;      
            senteciaSql = null;
            senteciaSql = "SELECT * FROM admgrupo";
            dt = dataBaseConection.fDataTable(senteciaSql);
            resultado = dt.Rows.Count;
            total = resultado + 1;
            Categoriafrm.ordenReslt= Convert.ToString(total);
            return dt;
        }
        //contador sub-categoria de tabla
        public DataTable contadorSubCategoria() 
        {
            DataTable dt;
            int resultado;
            senteciaSql = null;
            senteciaSql="SELECT * FROM admsubgrupo WHERE sgr_grupo='@Categoria';";
            senteciaSql=senteciaSql.Replace("@Categoria",codigoSubCodig);
            dt = dataBaseConection.fDataTable(senteciaSql);
            resultado = dt.Rows.Count;
            total = resultado + 1;
            return dt;
  
        }
        //Agregar Un Nuevo Alicuota al data
        public bool agregarAlic_IVA()
        {
            bool Ciclope = true;
            senteciaSql = null;
            senteciaSql = "INSERT INTO admiva(alict_tipodiva,alict_venta,alict_compra,alict_zonalibre,alict_codigo)VALUE('@tipodiva','@venta','@compra','@zonalibre','0001');";
            senteciaSql = senteciaSql.Replace("@tipodiva",tipoAlicIVA);
            senteciaSql = senteciaSql.Replace("@venta",ventaAlicIVA.Replace(",","."));
            senteciaSql = senteciaSql.Replace("@compra", compraAlicIVA.Replace(",", "."));
            senteciaSql = senteciaSql.Replace("@zonalibre", zonalibreAlicIVA.Replace(",", "."));
            dataBaseConection.ejecutarInsert(senteciaSql);
            dataBaseConection.cerrarConexion();
            return Ciclope;
        }
        //Actulizar boton Modificar tabla lbxAlIcuota
        public bool actulizarAlic_IVA()
        {
            bool Ciclope = true;
            senteciaSql = null;
            senteciaSql = "UPDATE admiva SET alict_venta='@venta',alict_compra='@compra',alict_zonalibre='@zonalibre'WHERE alict_tipodiva='@tipodiva';";
            senteciaSql = senteciaSql.Replace("@tipodiva", tipoAlicIVA);
            senteciaSql = senteciaSql.Replace("@venta", ventaAlicIVA.Replace(",", "."));
            senteciaSql = senteciaSql.Replace("@compra", compraAlicIVA.Replace(",", "."));
            senteciaSql = senteciaSql.Replace("@zonalibre", zonalibreAlicIVA.Replace(",", "."));
            dataBaseConection.ejecutarInsert(senteciaSql);
            dataBaseConection.cerrarConexion();
            return Ciclope;
        }
        //datagriv tabla principal lbxManteProducto
        public DataTable lbxMantProducto() 
        {
            DataTable dt;
            senteciaSql = null;
            senteciaSql = "SELECT inv_codigo AS Codigo,inv_descri AS Descripcion, " +
                        " inv_iva AS AlicuotaIva, " +
                        " gru_descri AS Categoria, " +
                        " sgr_descri AS SubCategoria FROM adminv " +
                        " LEFT JOIN admmarcas on admmarcas.mar_codigo=adminv.inv_codmarca " +
                        " LEFT JOIN admgrupo on admgrupo.gru_codigo=adminv.inv_grupo " +
                        " LEFT JOIN admsubgrupo on admsubgrupo.sgr_codigo=adminv.inv_subgru " +
                        " LEFT JOIN admdivision on admdivision.div_codigo=adminv.inv_divisi WHERE inv_estado='Activo' GROUP BY inv_codigo  ORDER BY INV_CODIGO LIMIT 200 ";
            dt = dataBaseConection.fDataTable(senteciaSql);
            dataBaseConection.cerrarConexion();
            return dt;
        
        }
        //datagriv tabla pricipal lbxSubCagoria
        public DataTable lbxSubCategoria()
        {
            DataTable dt;
            senteciaSql = null;
            senteciaSql = "SELECT sgr_grupo,sgr_codigo,sgr_descri,sgr_comvta,sgr_comcob,sgr_statud FROM admsubgrupo WHERE sgr_statud='Activo';";
            dt = dataBaseConection.fDataTable(senteciaSql);
            dataBaseConection.cerrarConexion();
            return dt;
 
        }
        //datagriv tabla pricipal lbxCategoria
        public DataTable lbxCategoria()
        { 
          DataTable dt;
          senteciaSql = null;
          senteciaSql = "SELECT gru_codigo,gru_descri,gru_comvta,gru_comcob,gru_statud FROM admgrupo WHERE gru_statud='Activo';";
          dt=dataBaseConection.fDataTable(senteciaSql);
          dataBaseConection.cerrarConexion();
          return dt;
        }
        //datagriv tabla Principal lbxALICU_IVA
        public DataTable lbxALICU_IVA()
        {
            DataTable dt;
            senteciaSql = null;
            senteciaSql = "SELECT alict_tipodiva,alict_venta,alict_compra,alict_zonalibre FROM admiva LIMIT 45;";
            dt = dataBaseConection.fDataTable(senteciaSql);
            dataBaseConection.cerrarConexion();
            return dt;
        }

        //datagriv tabla Principal lbxArancel
        public DataTable lbxArancel()
        {
            DataTable dt;
            senteciaSql = null;
            senteciaSql = "SELECT ara_codigo,ara_descri,ara_porcen,ara_kilos,ara_impuesto,ara_act FROM admarancel WHERE ara_act='1' ;";
            dt = dataBaseConection.fDataTable(senteciaSql);
            dataBaseConection.cerrarConexion();
            return dt;
        }
        //Busqueda Sub-categoria
        //Realizar Busqueda De sub-Categoria Sin La Contentiva y seleccion de radioButton Sub-codigo
        public DataTable subCodigoSubCategoriaBuscado(string subCodigoSubCategoriaBusca)
        {
            DataTable dt;
            senteciaSql = null;
            senteciaSql = "SELECT sgr_grupo,sgr_codigo,sgr_descri,sgr_comvta,sgr_comcob,sgr_statud FROM admsubgrupo WHERE sgr_codigo='@subGrupo';";
            senteciaSql = senteciaSql.Replace("@subGrupo", subCodigoSubCategoriaBusca);
            dt = dataBaseConection.fDataTable(senteciaSql);
            dataBaseConection.cerrarConexion();
            return dt;
        }
        //Realizar Busqueda De Sub-Categoria Con La Contentiva y seleccion de radioButton sub-codigo
        public DataTable subCodigoSubCategoriaBuscados(string subCodigoSubCategoriaBuscas) 
        {
            DataTable dt;
            senteciaSql = null;
            senteciaSql = "SELECT sgr_grupo,sgr_codigo,sgr_descri,sgr_comvta,sgr_comcob,sgr_statud FROM admsubgrupo WHERE sgr_codigo Like '%@subGrupo%';";
            senteciaSql = senteciaSql.Replace("@subGrupo", subCodigoSubCategoriaBuscas);
            dt = dataBaseConection.fDataTable(senteciaSql);
            dataBaseConection.cerrarConexion();
            return dt;
        }
        //Realizar Busqueda De Sub-Categoria Sin La Contentiva y seleccion de radioButton descripcion
        public DataTable descripSubCategoriaBuscado(string descripSubCategoriaBusca) 
        {
            DataTable dt;
            senteciaSql = null;
            senteciaSql = "SELECT sgr_grupo,sgr_codigo,sgr_descri,sgr_comvta,sgr_comcob,sgr_statud FROM admsubgrupo WHERE sgr_descri='@descri';";
            senteciaSql = senteciaSql.Replace("@descri", descripSubCategoriaBusca);
            dt = dataBaseConection.fDataTable(senteciaSql);
            dataBaseConection.cerrarConexion();
            return dt;
        }
        //Realizar Busqueda De Sub-Categoria Con La Contentiva y seleccion de radioButton descripcion
        public DataTable descripSubCategoriaBuscados(string descripSubCategoriaBuscas)
        {
            DataTable dt;
            senteciaSql = null;
            senteciaSql = "SELECT sgr_grupo,sgr_codigo,sgr_descri,sgr_comvta,sgr_comcob,sgr_statud FROM admsubgrupo WHERE sgr_descri Like '%@descri%';";
            senteciaSql = senteciaSql.Replace("@descri", descripSubCategoriaBuscas);
            dt = dataBaseConection.fDataTable(senteciaSql);
            dataBaseConection.cerrarConexion();
            return dt;
        }
        //Realizar Busqueda De Sub-Categoria Sin La Contentiva y seleccion de radioButton codigo
        public DataTable codigoSubCategoriaBuscado(string codigoSubCategoriaBusca)
        {
            DataTable dt;
            senteciaSql = null;
            senteciaSql = "SELECT sgr_grupo,sgr_codigo,sgr_descri,sgr_comvta,sgr_comcob,sgr_statud FROM admsubgrupo WHERE sgr_grupo='@codigo';";
            senteciaSql = senteciaSql.Replace("@codigo", codigoSubCategoriaBusca);
            dt = dataBaseConection.fDataTable(senteciaSql);
            dataBaseConection.cerrarConexion();
            return dt;
        
        }
        //Realizar Busqueda De Sub-Categoria Con La Contentiva y seleccion de radioButton codigo
         public DataTable codigoSubCategoriaBuscados(string codigoSubCategoriaBuscas)
        {
             DataTable dt;
            senteciaSql = null;
            senteciaSql = "SELECT sgr_grupo,sgr_codigo,sgr_descri,sgr_comvta,sgr_comcob,sgr_statud FROM admsubgrupo WHERE sgr_grupo Like '%@codigo%';";
            senteciaSql = senteciaSql.Replace("@codigo", codigoSubCategoriaBuscas);
            dt = dataBaseConection.fDataTable(senteciaSql);
            dataBaseConection.cerrarConexion();
            return dt;  
        }
        //BUsqueda Categoria
        //Realizar Busqueda De Categoria Sin La Contentiva y seleccion de radioButton codigo
        public DataTable codigoCategoriaBuscado(string codigoCategoriaBuscar)
        {
            DataTable dt;
            senteciaSql = null;
            senteciaSql = "SELECT gru_codigo,gru_descri,gru_comvta,gru_comcob,gru_statud FROM admgrupo WHERE gru_codigo='@codigo';";
            senteciaSql = senteciaSql.Replace("@codigo", codigoCategoriaBuscar);
            dt = dataBaseConection.fDataTable(senteciaSql);
            dataBaseConection.cerrarConexion();
            return dt;
        }
        //Realizar Busqueda De Categoria Con La Contentiva y seleccion de radioButton codigo
        public DataTable codigoCategoriaBuscados(string codigoCategoriaBuscas)
        {
            DataTable dt;
            senteciaSql = null;
            senteciaSql = "SELECT gru_codigo,gru_descri,gru_comvta,gru_comcob,gru_statud FROM admgrupo WHERE gru_codigo Like '%@codigo%';";
            senteciaSql = senteciaSql.Replace("@codigo", codigoCategoriaBuscas);
            dt = dataBaseConection.fDataTable(senteciaSql);
            dataBaseConection.cerrarConexion();
            return dt;
        }
        //Realizar Busqueda De Categoria Sin La Contentiva y seleccion de radioButton descripcion
        public DataTable descripCategoriaBuscado(string descripCategoriaBuscar)
        {
            DataTable dt;
            senteciaSql = null;
            senteciaSql = "SELECT gru_codigo,gru_descri,gru_comvta,gru_comcob,gru_statud FROM admgrupo WHERE gru_descri='@descrip';";
            senteciaSql = senteciaSql.Replace("@descrip", descripCategoriaBuscar);
            dt = dataBaseConection.fDataTable(senteciaSql);
            dataBaseConection.cerrarConexion();
            return dt;
        }
        //Realizar Busqueda De Categoria Con La Contentiva y seleccion de radioButton descripcion
        public DataTable descripCategoriaBuscados(string descripCategoriaBuscas)
        {
            DataTable dt;
            senteciaSql = null;
            senteciaSql = "SELECT gru_codigo,gru_descri,gru_comvta,gru_comcob,gru_statud FROM admgrupo WHERE gru_descri Like '%@descrip%';";
            senteciaSql = senteciaSql.Replace("@descrip", descripCategoriaBuscas);
            dt = dataBaseConection.fDataTable(senteciaSql);
            dataBaseConection.cerrarConexion();
            return dt;
        }
        //Realizar Arancel
        //Realizar Busqueda De Arancel Sin La Contentiva y seleccion de radioButton codigo
        public DataTable codigoAracelBuscado(string codigoAracelBuscar)
        {
            DataTable dt;     
            senteciaSql=null;
            senteciaSql = "SELECT ara_codigo,ara_descri,ara_porcen,ara_kilos,ara_impuesto,ara_act FROM admarancel WHERE ara_codigo='@codigo';";
            senteciaSql = senteciaSql.Replace("@codigo", codigoAracelBuscar);
            dt = dataBaseConection.fDataTable(senteciaSql);
            dataBaseConection.cerrarConexion();
            return dt;   
        }

        //Realizar Busqueda De Arancel Sin La Contentiva y seleccion de radioButton descripcion
        public DataTable descripArancelBuscado(string descripArancelBuscar)
        {
            DataTable dt;
            senteciaSql = null;
            senteciaSql = "SELECT ara_codigo,ara_descri,ara_porcen,ara_kilos,ara_impuesto,ara_act FROM admarancel WHERE ara_descri='@descri'";
            senteciaSql = senteciaSql.Replace("@descri", descripArancelBuscar);
            dt = dataBaseConection.fDataTable(senteciaSql);
            dataBaseConection.cerrarConexion();
            return dt;
        }
        //Realizar Busqueda De Arancel Contentiva y seleccion Por radiobutton codigo
        public DataTable codigoArancelBuscados(string codigoArancelBusca)
        {
            DataTable dt;
            senteciaSql = null;
            senteciaSql = "SELECT ara_codigo,ara_descri,ara_porcen,ara_kilos,ara_impuesto,ara_act FROM admarancel WHERE ara_codigo like'%@codigo%'";
            senteciaSql = senteciaSql.Replace("@codigo", codigoArancelBusca);
            dt = dataBaseConection.fDataTable(senteciaSql);
            dataBaseConection.cerrarConexion();
            return dt;
        }
        //Realizar Busqueda De Arancel Contentiva y seleccion Por radiobutton descripcion
        public DataTable descripArancelBuscados(string descripArancelBusca)
        {
            DataTable dt;
            senteciaSql = null;
            senteciaSql = "SELECT ara_codigo,ara_descri,ara_porcen,ara_kilos,ara_impuesto,ara_act FROM admarancel WHERE ara_descri like'%@descri%'";
            senteciaSql = senteciaSql.Replace("@descri", descripArancelBusca);
            dt = dataBaseConection.fDataTable(senteciaSql);
            dataBaseConection.cerrarConexion();  
            return dt;
        }
        // ingresar una nueva Subcategoria
        public bool ingresarSubCategoria() 
        {
            bool Ciclope = true;
            senteciaSql = null;
            try
            {
                senteciaSql = "INSERT INTO admsubgrupo(sgr_codigo,sgr_grupo,sgr_descri,sgr_comvta,sgr_comcob,sgr_memo,sgr_imagen,sgr_statud)VALUE('@codigo','@scodigo','@descrip','@venta','@compra','@coment','@imagen','Activo');";
                senteciaSql = senteciaSql.Replace("@codigo", codigoSubCat);
                senteciaSql = senteciaSql.Replace("@scodigo", codigoCategoria);
                senteciaSql = senteciaSql.Replace("@descrip", descripSubCat);
                senteciaSql = senteciaSql.Replace("@venta", comvSubCat.Replace(",", "."));
                senteciaSql = senteciaSql.Replace("@compra",comcSubCat.Replace(",", "."));
                senteciaSql = senteciaSql.Replace("@coment", "");
                senteciaSql = senteciaSql.Replace("@imagen", "");
                dataBaseConection.ejecutarInsert(senteciaSql);
            }
            catch (Exception e)
            {

            }
            dataBaseConection.cerrarConexion();
            return Ciclope;
        
        
        }
        //Ingresar Nueva Categoria
        public bool ingresarCategoria()
        {
            bool Ciclope = true;
            senteciaSql = null;
            try  
            {
                senteciaSql = "INSERT INTO admgrupo(gru_codigo,gru_descri,gru_comvta,gru_comcob,gru_coment,gru_imagen,gru_statud)VALUE('@codigo','@descrip','@venta','@compra','@coment','@imagen','Activo');";
                senteciaSql = senteciaSql.Replace("@codigo", codigoCategoria);
                senteciaSql = senteciaSql.Replace("@descrip",descripCategoria);
                senteciaSql = senteciaSql.Replace("@venta", ventaCategoria.Replace(",", "."));
                senteciaSql = senteciaSql.Replace("@compra", compraCategoria.Replace(",", "."));
                senteciaSql = senteciaSql.Replace("@coment", "");
                senteciaSql = senteciaSql.Replace("@imagen", "");
                dataBaseConection.ejecutarInsert(senteciaSql);
            }
                catch (Exception e)
            {
            
            }
            dataBaseConection.cerrarConexion();
            return Ciclope;
        }
        //Ingresar Nuevo Arancel
        public bool ingresarArancel()
        {
            bool Ciclope = true;
            senteciaSql = null;
  
            try
            {
                senteciaSql = "INSERT INTO admarancel(ara_codigo,ara_descri,ara_comentarios,ara_porcen,ara_kilos,ara_impuesto,ara_act)"+
                    "VALUE('@codigo','@descripcion','@comentario','@porcen','@Kilos','@impuesto','@act');";
                senteciaSql=senteciaSql.Replace("@codigo",codigoA);
                senteciaSql=senteciaSql.Replace("@descripcion",descripA);
                senteciaSql=senteciaSql.Replace("@comentario","0");
                senteciaSql=senteciaSql.Replace("@porcen",proceA.Replace(",","."));
                senteciaSql=senteciaSql.Replace("@Kilos",kgA);
                senteciaSql = senteciaSql.Replace("@impuesto", impuestoA.Replace(",", "."));
                senteciaSql=senteciaSql.Replace("@act","1");
                dataBaseConection.ejecutarInsert(senteciaSql);

            }
            catch (Exception e)
            {
            
            }
            dataBaseConection.cerrarConexion();
            return Ciclope;
        }
        // Actulizar Categoria;
        public bool actulziarCategoria()
        {
            bool Ciclope = true;
            senteciaSql = null;
            try
            {
                senteciaSql = "UPDATE admgrupo SET gru_descri='@gru_descri',gru_comvta='@gru_comvta',gru_comcob='@gru_comcob' WHERE gru_codigo='@gru_codigo';";
                senteciaSql= senteciaSql.Replace("@gru_descri",descripCategoria);
                senteciaSql= senteciaSql.Replace("@gru_comvta",ventaCategoria.Replace(",","."));
                senteciaSql= senteciaSql.Replace("@gru_comcob",compraCategoria.Replace(",","."));
                senteciaSql= senteciaSql.Replace("@gru_codigo",codigoCategoria);
                dataBaseConection.ejecutarInsert(senteciaSql);
              
            }
            catch (Exception e)
            {
            }
            dataBaseConection.cerrarConexion();
            return Ciclope; 
        }
        //actulizar Subcategoria
        public bool actulizarSubCategoria()
        {
         bool Ciclope = true;
            senteciaSql = null;
            try
            {
                senteciaSql = "UPDATE admsubgrupo SET sgr_descri='@descrip',sgr_comvta='@comvta',sgr_comcob='@comcob' WHERE sgr_grupo='@codigo' AND sgr_codigo=@scodigo;";
                senteciaSql = senteciaSql.Replace("@descrip", descripSubCat);
                senteciaSql = senteciaSql.Replace("@comvta", comvSubCat.Replace(",", "."));
                senteciaSql = senteciaSql.Replace("@comcob", comcSubCat.Replace(",", "."));
                senteciaSql= senteciaSql.Replace("@codigo",codigoCategoria);
                senteciaSql = senteciaSql.Replace("@scodigo", codigoSubCat);
                dataBaseConection.ejecutarInsert(senteciaSql);
              
            }
            catch (Exception e)
            {
                MessageBox.Show("Error", "ERRoR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            dataBaseConection.cerrarConexion();
            return Ciclope;
    
    
        } 

        //Eliminar o Desativar Sub-Categoria
        public bool eliminarOactulizarSubCateg()
        {
            bool Ciclope = true;
            senteciaSql = null;
            try
            {
                senteciaSql = "UPDATE admsubgrupo SET sgr_statud='Inactivo' WHERE sgr_codigo='@codigo' AND sgr_grupo='@scodigo';";
                senteciaSql = senteciaSql.Replace("@codigo", codigoSubCat);
                senteciaSql = senteciaSql.Replace("@scodigo", codigoCategoria);
                dataBaseConection.ejecutarInsert(senteciaSql);
            }
            catch (Exception e)
            {
            }
            dataBaseConection.cerrarConexion();
            return Ciclope;
        }
        // Eliminar o Desativar Categoria
        public bool eliminarOactulizarCategoria()
        {
            bool Ciclope = true;
            senteciaSql = null;
            try
            {
                senteciaSql = "UPDATE admgrupo SET gru_statud='Inactivo' WHERE gru_codigo='@gru_codigo';";
                senteciaSql = senteciaSql.Replace("@gru_codigo", codigoCategoria);
                dataBaseConection.ejecutarInsert(senteciaSql);
            }
            catch (Exception e)
            {
            }
            dataBaseConection.cerrarConexion();
            return Ciclope;
        }
        //actulizar Arancel
        public bool actulizarArancel()
        {
            bool Ciclope = true;
            senteciaSql = null;
            try
            {
                senteciaSql = "UPDATE admarancel SET ara_descri='@ara_descri',ara_porcen='@ara_porcen',ara_kilos='@ara_kilos',ara_impuesto='@ara_impuesto'WHERE ara_codigo='@codigo';";
                senteciaSql = senteciaSql.Replace("@codigo", codigoA);
                senteciaSql = senteciaSql.Replace("@ara_descri", descripA);
                senteciaSql = senteciaSql.Replace("@ara_porcen", proceA.Replace(",","."));
                senteciaSql = senteciaSql.Replace("@ara_kilos", kgA);
                senteciaSql = senteciaSql.Replace("@ara_impuesto", impuestoA.Replace(",","."));
                dataBaseConection.ejecutarInsert(senteciaSql);
            }
            catch (Exception e)
            { 
            }
            dataBaseConection.cerrarConexion();
            return Ciclope;
        }
        //Evaluacion Eliminacion o Desabilitar Un Arancel
        public bool actulizarStatudArancel()
        {
            bool Ciclope = true;
            senteciaSql = null;
            try
            {
                senteciaSql = "UPDATE admarancel SET ara_act='0' WHERE ara_codigo='@codigo' AND ara_act='1';";
                senteciaSql = senteciaSql.Replace("@codigo", codigoA);               
                dataBaseConection.ejecutarInsert(senteciaSql);
            }
            catch (Exception e)
            {
            }
            dataBaseConection.cerrarConexion();
            return Ciclope;
        }
        /////////////////////////////////////Busqueda De Imformacion De frmProducto///////////////////////////////////////
        public string validaCodProducto(string codigo)
        {
            DataTable dt;
            string codigoExite;
            senteciaSql = null;
            senteciaSql = "SELECT inv_codigo FROM adminv WHERE inv_codigo='" + codigo + "';";
            dt = dataBaseConection.fDataTable(senteciaSql);
            dataBaseConection.cerrarConexion();
            if (dt.Rows.Count > 0)
            {
                codigoExite = dt.Rows[0]["inv_codigo"].ToString();
                return codigoExite;
            }
            else
            {
                codigoExite ="0";
                return codigoExite;
            }
        }
        
        public DataTable imformaAracel() 
        {
            DataTable dt;
            senteciaSql = null;
            senteciaSql = "SELECT * FROM admarancel;";
            dt = dataBaseConection.fDataTable(senteciaSql);
            dataBaseConection.cerrarConexion();
            return dt;
        }
        public DataTable imformaCategoria()
        {
            DataTable dt;
            senteciaSql = null;
            senteciaSql = "SELECT * FROM gru_descri;";
            dt = dataBaseConection.fDataTable(senteciaSql);
            dataBaseConection.cerrarConexion();
            return dt;
        }
        public DataTable imformaSubCategoria() 
        {
            DataTable dt;
            senteciaSql = null;
            senteciaSql = "SELECT * FROM admsubgrupo;";
            dt = dataBaseConection.fDataTable(senteciaSql);
            dataBaseConection.cerrarConexion();
            return dt;
        
        }
        ////// Sub-categoria dependiendo del Categoria;
        public DataTable subcategoriaCategoria(string categoria)
        {
            DataTable dt;
            senteciaSql = null;
            senteciaSql = "SELECT * FROM admsubgrupo WHERE sgr_grupo='"+categoria+"'";
            dt = dataBaseConection.fDataTable(senteciaSql);
            dataBaseConection.cerrarConexion();
            return dt;
        }
        //agregar producto nuevo
        public bool agregarProducto(string codigo, string descrip, string categoria, string subCategoria, string editable
            , string estado, string iva, string contenido,string obser1, string obser2, string obser3, string procede,
            decimal capacidad, decimal comisionVentas, string exMax, string esMin, string garantia, string volume, string alto, string ancho
            , decimal comisionCompra, string aracel, string codeFabri,string PDD)
        {
            //Insert adminv
            
            bool Ciclope = true;
            senteciaSql = null;
            senteciaSql = "INSERT INTO adminv(inv_codigo,inv_descri,inv_codmarca,inv_grupo,inv_subgru,inv_divisi," +
             "inv_editar,inv_estado,inv_imag,inv_imppat,inv_iva,inv_metcos,inv_observ,inv_obse,inv_obser,inv_proced" +
            ",inv_promo,inv_reqmed,inv_peso,inv_utiliz,inv_fechacre,inv_capaci,inv_comision,inv_cosfob,inv_cosmer," +
            "inv_exismax,inv_exismin,inv_grados,inv_imppvp,inv_garantia,inv_memo,inv_porarancel,inv_pordesc,inv_impexp," +
            "inv_impfab,inv_volumen,inv_alto,inv_ancho,inv_comcob,inv_arancel,inv_codfab,inv_pordesc2,inv_pordesc3)" +
            "Value('" + codigo + "','" + descrip + "','000001','" + categoria + "','" + subCategoria + "','000001','" + editable + "','" + estado + "'" +
            ",'','0001','" + iva + "','Promedio','"+obser1+"','"+obser2+"','"+obser3+"','" + procede + "','No','Si','0','Venta','" + DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day + "','" + capacidad + "'" +
            ",'" + comisionVentas + "','0.00000','0.00','" + exMax + "','" + esMin + "','" + contenido + "','No','" + garantia + "','','0.00','"+PDD+"'" +
            ",'0.00','0.00','" + volume + "','" + alto + "','" + ancho + "','" + comisionCompra + "','" + aracel + "','" + codeFabri + "','0.00','0.00');";
            dataBaseConection.ejecutarInsert(senteciaSql);

            senteciaSql = null;
            senteciaSql = "INSERT INTO adminv2(inv2_codigo,cod_moneda,cod_pais,ult_costo,cost_prom," +
                "pre_lista2,pre_ext,dcto_prov1,dcto_prov2,dcto_prov3,existencia, fec_ult_compra,ult_provee," +
                "ult_refer_compra,ult_cant_compra,fec_ult_vent,ult_cliente_vent,ult_refer_vent,ult_cant_vent," +
                "fec_ult_aju,ult_refer_aju,ult_cant_aju,prefac,pedido,fecha_infreso,inv_ex," +
                "codigo_Barra,precio_pmvp)Value('" + codigo + "','','','0.00','0.00','0.00','0.00','0.00','0.00','0.00','0.00','1800-01-01'," +
                "'','','0.00','1800-01-01','','','0.00','1800-01-01','','0.00','0.00','0.00','1800-01-01','1','0','0.00');";
            dataBaseConection.ejecutarInsert(senteciaSql);
            return Ciclope;
        }
        public void modifProducto(string codigo)
        {
            DataTable dt;

            senteciaSql = null;
            senteciaSql = "SELECT * FROM adminv WHERE inv_codigo='" + codigo + "';";
            dt = dataBaseConection.fDataTable(senteciaSql);
            dataBaseConection.cerrarConexion();
            this.descripProduc = dt.Rows[0]["inv_descri"].ToString();
            this.fabriProduc = dt.Rows[0]["inv_codfab"].ToString();
            this.categPruduc = dt.Rows[0]["inv_grupo"].ToString();
            this.subCatProduc = dt.Rows[0]["inv_subgru"].ToString();
            this.desEdit = dt.Rows[0]["inv_editar"].ToString();
            this.estado = dt.Rows[0]["inv_estado"].ToString();
            this.ivaProduc = dt.Rows[0]["inv_iva"].ToString();
            this.obser1 = dt.Rows[0]["inv_observ"].ToString();
            this.obser2 = dt.Rows[0]["inv_obse"].ToString();
            this.obser3 = dt.Rows[0]["inv_obser"].ToString();
            this.procede = dt.Rows[0]["inv_proced"].ToString();
            this.capacidad = dt.Rows[0]["inv_capaci"].ToString();
            this.comVenProduc = dt.Rows[0]["inv_comision"].ToString();
            this.exMaxProduc = dt.Rows[0]["inv_exismax"].ToString();
            this.ExMinProd = dt.Rows[0]["inv_exismin"].ToString();
            this.gpmProduc = dt.Rows[0]["inv_garantia"].ToString();
            this.volumProduc = dt.Rows[0]["inv_volumen"].ToString();
            this.altaProduc = dt.Rows[0]["inv_alto"].ToString();
            this.anchoProduc = dt.Rows[0]["inv_ancho"].ToString();
            this.comCobProduc = dt.Rows[0]["inv_comcob"].ToString();
            this.fabriProduc = dt.Rows[0]["inv_codfab"].ToString();
            this.pddPruduc = dt.Rows[0]["inv_pordesc"].ToString();
            this.aracelProduc = dt.Rows[0]["inv_arancel"].ToString();
            this.contenido = dt.Rows[0]["inv_grados"].ToString();
            //busqueda descripcion aracel
            senteciaSql = null;
            senteciaSql = "SELECT ara_descri FROM admarancel WHERE ara_codigo='" + AracelProduc + "';";
            dt = dataBaseConection.fDataTable(senteciaSql);
            this.desAracProduc = dt.Rows[0]["ara_descri"].ToString();
            dataBaseConection.cerrarConexion();
        }
        public bool actualizarProducto(string descrip,string categoria,string subCategoria,string editar,string pdd,string estado,
            string iva,string contenido,string obser1, string obser2,string obser3,string proced,string capaci, string comVentas,string comCompra
            ,string exMax,string exMin, string volume,string alto,string ancho,string aracel,string codfab,string codigo,string GDP) 
        {
            bool Ciclope = true;
            senteciaSql = null;
            try
            {
                senteciaSql = "UPDATE adminv SET inv_descri='" + descrip + "',inv_grupo='" + categoria + "',inv_subgru='" + subCategoria + "',inv_editar='" + editar + "',inv_pordesc='" + pdd + "',inv_estado='" + estado + "'," +
                    " inv_iva='"+iva+"',inv_observ='"+obser1+"',inv_obse='"+obser2+"',inv_obser='"+obser3+"',inv_proced='"+proced+"',inv_capaci='"+capaci+"',inv_comision='"+comVentas+"'," +
                    "inv_exismax='" + exMax + "',inv_exismin='" + exMin + "',inv_grados='" + contenido + "',inv_volumen='" + volume + "',inv_alto='" + alto + "',inv_ancho='" + ancho + "',inv_comcob='" + comCompra + "',inv_arancel='" + aracel + "'," +
                    "inv_codfab='" + codfab + "',inv_garantia='"+GDP+"' WHERE inv_codigo='" + codigo + "';";
                dataBaseConection.ejecutarInsert(senteciaSql);

            }
            catch(Exception e) 
            {
            
            }
            dataBaseConection.cerrarConexion();
            return Ciclope;
           
        
        }
        //para coninuar 4 de enero
        //public bool estadoProducto(string codigo)
        //{
        //    DataTable dt;
        //    bool ciclope = true;
        //    senteciaSql = null;
        //    senteciaSql = "SELECT inv_estado FROM adminv WHERE inv_codigo='"+ codigo +"'";
        //    dt=dataBaseConection.ejecutarInsert(senteciaSql);
        //}
        public void elimiProducto(string codigo)
        {
            DataTable dt;
            senteciaSql = null;
            senteciaSql = "SELECT inv_estado FROM adminv WHERE inv_codigo='"+ codigo +"'";
            dt = dataBaseConection.fDataTable(senteciaSql);
            this.estadoProduc = dt.Rows[0]["inv_estado"].ToString();
            dataBaseConection.cerrarConexion();
        }
        public bool actulizarEstado(string codigo) 
        {
            bool Ciclope = true;
            senteciaSql = null;
            try
            {
                senteciaSql = "UPDATE adminv SET inv_estado='Inactivo'WHERE inv_codigo='" + codigo + "';";
                dataBaseConection.ejecutarInsert(senteciaSql);
            }
            catch (Exception e)
            {
            }
            dataBaseConection.cerrarConexion();
            return Ciclope;
        }
        public bool trazUsuario(string usuario, string ip, string modulo, string accion,string codigo)
        {
            bool Ciclope = true;
            senteciaSql = null;
            senteciaSql = "INSERT INTO admtrazausuario(trz_codusu,trz_ip,trz_modulo,trz_accion,trz_fchreg,trz_hora,trz_codigo)" +
                "Value('" + usuario + "','" + ip + "','" + modulo + "','" + accion + "'," +
                "'" + DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day + "','" + DateTime.Now.ToString("hh:mm:ss tt") + "','" + codigo + "')";
            dataBaseConection.ejecutarInsert(senteciaSql);
            dataBaseConection.cerrarConexion();
            return Ciclope;
        }
        public void exitProducto(string codigo) 
        {
            DataTable dt;
            senteciaSql = null;
            senteciaSql = "SELECT existencia FROM adminv2 WHERE inv2_codigo='" + codigo + "'";
            dt = dataBaseConection.fDataTable(senteciaSql);
            this.exiteProdc = dt.Rows[0]["existencia"].ToString();
            dataBaseConection.cerrarConexion();
        }
        //Asignar Proveedor
        public void llenarComboxPais(ComboBox cb) 
        {
            senteciaSql=null;
            try
            {
                senteciaSql = "SELECT pais_descri FROM admpais";
                dr = dataBaseConection.ejecutarQueryDr(senteciaSql);
                while (dr.Read()) 
                {
                    cb.Items.Add(dr["pais_descri"].ToString());
                
                }
                dataBaseConection.cerrarConexion();

            }
            catch (Exception ex)
            {
                MessageBox.Show("No Se Llenado ComboBox:"+ex.ToString());
            }
        }
        public void llenarComboxMoneda(ComboBox cb)
        {
            senteciaSql = null;
            try
            {
                senteciaSql = "SELECT mon_codigo FROM admmoneda";
                dr = dataBaseConection.ejecutarQueryDr(senteciaSql);
                while (dr.Read())
                {
                    cb.Items.Add(dr["mon_codigo"].ToString());
                }
                dataBaseConection.cerrarConexion();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No Se Llenado ComboBox:" + ex.ToString());
            }
        }
        public bool inserAsigProvee(string codProd,string codProv,string pais,string moneda) 
        {
            bool Ciclope = true;
            senteciaSql = null;
            senteciaSql = "INSERT INTO adminvparam(pin_codigo,pin_proveedor,pin_pais,pin_moneda)" +
                "Value('" + codProd + "','" + codProv + "','" + pais + "','" + moneda + "')";
            dataBaseConection.ejecutarInsert(senteciaSql);
            dataBaseConection.cerrarConexion();
            return Ciclope;
        }
        public void codPaises(string pais) 
        {
            DataTable dt;
            senteciaSql = null;
            senteciaSql = "SELECT pais_codigo  FROM admpais WHERE pais_descri='" + pais + "'";
            dt = dataBaseConection.fDataTable(senteciaSql);
            this.codPais = dt.Rows[0]["pais_codigo"].ToString();
            dataBaseConection.cerrarConexion();
        }
        public void codPaisProv(string codigo)
        {
            DataTable dt;
            string codPaisProv;
            senteciaSql = null;
            senteciaSql = "SELECT pro_pais,pro_divisa FROM admproveedor WHERE pro_codigo='" + codigo + "'";
            dt = dataBaseConection.fDataTable(senteciaSql);
            codPaisProv = dt.Rows[0]["pro_pais"].ToString();
            this.monedaPais = dt.Rows[0]["pro_divisa"].ToString();
            senteciaSql = null;
            senteciaSql = "SELECT pais_descri FROM admpais WHERE pais_codigo='" + codPaisProv + "'";
            dt = dataBaseConection.fDataTable(senteciaSql);
            this.nombrePais = dt.Rows[0]["pais_descri"].ToString();
            dataBaseConection.cerrarConexion();
        }
        public void llenarUnidad(ComboBox cb) 
        {
            senteciaSql = null;
            try
            {
                senteciaSql = "SELECT und_codigo FROM admundmed";
                dr = dataBaseConection.ejecutarQueryDr(senteciaSql);
                while (dr.Read())
                {
                    cb.Items.Add(dr["und_codigo"].ToString());
                }
                dataBaseConection.cerrarConexion();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No Se Llenado ComboBox:" + ex.ToString());
            }
        }
        public void llenarPresentacion(string codigo)
        {
            DataTable dt;
            senteciaSql = null;
            senteciaSql = "SELECT und_descri FROM admundmed WHERE und_codigo='" + codigo + "'";
            dt = dataBaseConection.fDataTable(senteciaSql);
            this.undDescrip = dt.Rows[0]["und_descri"].ToString();
            dataBaseConection.cerrarConexion();
        }

        public DataTable uniMedida(string codigo) 
        {
            DataTable dt;
            senteciaSql = null;
            senteciaSql = "SELECT ime_undmed,ime_cantid,ime_presen,ime_peso,ime_volumen,ime_compra,ime_venta,ime_signo,ime_principal FROM adminvmed WHERE ime_codigo='" + codigo + "'";
            dt = dataBaseConection.fDataTable(senteciaSql);
            dataBaseConection.cerrarConexion();
            return dt;
        }
        public bool insertUnidaMedida(string codigo,string unidaMed,string cantidad,string presentacion,
            string peso,string volumen,string compra, string venta, string signo, string principal) 
        {
            bool Ciclope = true;
            senteciaSql = null;
            senteciaSql = "INSERT INTO adminvmed(ime_codigo,ime_undmed,ime_cantid,ime_presen" +
                ",ime_peso,ime_volumen,ime_compra,ime_venta,ime_signo,ime_principal)" +
                "Value('" + codigo + "','" + unidaMed + "','" + cantidad + "','" + presentacion + "','" + peso + "'" +
            ",'" + volumen + "','" + compra + "','" + venta + "','" + signo + "','" + principal + "')";
            senteciaSql = senteciaSql.Replace(cantidad, cantidad.Replace(",", "."));
            senteciaSql = senteciaSql.Replace(venta, venta.Replace(",", "."));
            senteciaSql = senteciaSql.Replace(compra, compra.Replace(",", "."));
            dataBaseConection.ejecutarInsert(senteciaSql);
            dataBaseConection.cerrarConexion();
            return Ciclope;
        }
        public bool actulizarUniMed(string codigo, string unidaMed, string cantidad, string presentacion,
            string peso, string volumen, string compra, string venta, string signo, string principal) 
        {
            bool Ciclope = true;
            senteciaSql = null;
            senteciaSql = "UPDATE adminvmed SET ime_undmed='" + unidaMed + "',ime_cantid='" + cantidad + "',"+
            "ime_presen='" + presentacion + "',ime_peso='" + peso + "',ime_volumen='" + volumen + "',"+
             "ime_compra='" + compra + "',ime_venta='" + venta + "',ime_signo='" + signo + "',ime_principal='" + principal + "'"+
            "WHERE ime_codigo='" + codigo + "';";
            senteciaSql = senteciaSql.Replace(cantidad, cantidad.Replace(",", "."));
            senteciaSql = senteciaSql.Replace(venta, venta.Replace(",", "."));
            senteciaSql = senteciaSql.Replace(compra, compra.Replace(",", "."));
            dataBaseConection.ejecutarInsert(senteciaSql);
            dataBaseConection.cerrarConexion();
            return Ciclope;
        }
        public void eliminarUniMedida(string codigo) 
        {
            senteciaSql = null;
            senteciaSql = "DELETE FROM adminvmed WHERE ime_codigo='" + codigo + "'";
            dataBaseConection.ejecutarInsert(senteciaSql);
            dataBaseConection.cerrarConexion();
        }
        public void modificarUniMed(string codigo) 
        {
            DataTable dt;
            senteciaSql = null;
            senteciaSql = "SELECT ime_undmed,ime_cantid,ime_presen,ime_peso,ime_volumen,ime_compra,ime_venta,ime_signo,ime_principal FROM adminvmed WHERE ime_codigo='" + codigo + "'";
            dt = dataBaseConection.fDataTable(senteciaSql);

                this.undCant = dt.Rows[0]["ime_cantid"].ToString();
                this.undMed = dt.Rows[0]["ime_undmed"].ToString();
                this.undPrese = dt.Rows[0]["ime_presen"].ToString();
                this.undPeso = dt.Rows[0]["ime_peso"].ToString();
                this.undVolu = dt.Rows[0]["ime_volumen"].ToString();
                this.undComp = dt.Rows[0]["ime_compra"].ToString();
                this.undvent = dt.Rows[0]["ime_venta"].ToString();
                this.undSign = dt.Rows[0]["ime_signo"].ToString();
                this.undPrin = dt.Rows[0]["ime_principal"].ToString();
            dataBaseConection.cerrarConexion();
        }
        public void precioActivo(string codigo) 
        {
            DataTable dt;
            senteciaSql = null;
            senteciaSql = "SELECT pre_act FROM admprecios WHERE pre_codigo='" + codigo + "'";
            dt = dataBaseConection.fDataTable(senteciaSql);
            if (dt.Rows.Count > 0)
            {
                this.preciosActivos = dt.Rows[0]["pre_act"].ToString();
                this.codiPreExite = "1";
            }
            else
            {
                this.codiPreExite = "0";
            }
            dataBaseConection.cerrarConexion();
        
        }
        public DataTable eliminarPrecios(string codigo) 
        {
            DataTable dt;
            senteciaSql = null;
            senteciaSql = "SELECT pre_lista,pre_precio,pre_descue,pre_utilid,pre_fechav,pre_usuario,replace(replace(pre_act,'0','Inactivo'),'1','Activo') AS Estado FROM admprecios WHERE pre_codigo='" + codigo + "';";
            dt = dataBaseConection.fDataTable(senteciaSql);
            dataBaseConection.cerrarConexion();
            return dt;
        }
        public bool desactivarPrecios(string Codigo, string TipoPrecio) 
        {
            bool Ciclope = true;
            senteciaSql = null;
            senteciaSql = "UPDATE admprecios SET pre_act='0' WHERE pre_lista='" + TipoPrecio + "' AND pre_codigo='" + Codigo + "';";
            dataBaseConection.ejecutarInsert(senteciaSql);
            dataBaseConection.cerrarConexion();
            return Ciclope;
        }
        public DataTable eliminarPromo(string codigo) 
        {
            DataTable dt;
            senteciaSql = null;
            senteciaSql = "SELECT prom_undmed,prom_undpro,DATE_FORMAT(prom_fchfin,'%Y/%m/%d') AS prom_fchfin,DATE_FORMAT(prom_fchedes,'%Y/%m/%d') AS prom_fchedes,porm_precio,prom_observ,prom_multiplo,replace(replace(prom_status,'0','Inactivo'),'1','Activo')As Status FROM admpromociones WHERE prom_codigo='" + codigo + "';";
            dt = dataBaseConection.fDataTable(senteciaSql);
            dataBaseConection.cerrarConexion();
            return dt;
        }
        public bool desactivarPromo(string codigo, string fechaF,string fechaD)
        {
            bool Ciclope = true;
            senteciaSql = null;
            senteciaSql = "UPDATE admpromociones SET prom_status='0' WHERE prom_codigo='" + codigo + "'AND prom_fchfin='"+fechaF+"' AND prom_fchedes='" + fechaD + "' ;";
            dataBaseConection.ejecutarInsert(senteciaSql);
            dataBaseConection.cerrarConexion();
            return Ciclope;
        }
    }

}
