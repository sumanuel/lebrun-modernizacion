using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;
using lebrun.clasesData;
using lebrun.formularios.productos;
using lebrun.formularios.gerencial;
using System.Windows.Forms;
using System.Configuration;


namespace lebrun.clases.gerencial
{
    class ConsultaGeneral
    {
        //Llmado de clases
        private ConexionBD dataBaseConection;
        private string senteciaSql;
        private DataTable dt;
        private MySqlDataReader dr;
        public lbxConsultaGerencial genrecial;
        int cantFila;
        decimal cr;
        decimal cr1;
        decimal cr2;
        decimal cr3;
        decimal cr4;
        private string procecia;
        private string undMedProduc;
        private string descProduc;
        private string descGrupProduc;
        private string descSubGrupProduc;
        private string estadoProduc;
        private string promProduc;
        private string prefacProduc;
        private string exisProduc;
        private string pedidoProduc;
        private string ultCostProduc;
        private string fecUltCompProduc;
        private string ultProveProduc;
        private string ultRefCompProduc;
        private string ultCantCompProduc;
        private string fecUltVentProduc;
        private string ultClienProduc;
        private string ultRefVentProduc;
        private string ultCantVent;
        private string fecUltAjusProduc;
        private string ultRefAjusProduc;
        private string ultCantAjustProduc;
        private string precioProduc;
        private string aranDescProduc;
        private decimal aranPorsProduc;
        private string proveeNombr;
        private string descPais;
        private string proDivi;
        private decimal facCif;
        private decimal facFin;
        private decimal facGas;
        private decimal facOtro;
        private string facEnt;
        private string monDescrp;
        private string histMond;
        private decimal provDes1;
        private decimal provDes2;
        private decimal provDesc3;
        private string pinMoned;
        private string contProm;
        private decimal preLista2;
        private string desProv1;
        private string desProv2;
        private string desProv3;
        private decimal hisValor1;
        private decimal hisValor2;
        private decimal hisValor3;
        private decimal hisValor4;
        private decimal provd1;
        private decimal provd2;
        private decimal provd3;
        private decimal costoRep1;
        private decimal costoRep2;
        private decimal costoRep3;
        private decimal costoRep4;
        //moviento del año actual
        private decimal montoVenta;
        private decimal costoVenta;
        private decimal cantVendida;
        private decimal cantVendidaFil;
        private decimal porCostVent;
        private decimal costoCompras;
        private decimal cantidadComprada;
        // promedio de venta
        private decimal vet3Meses;
        private decimal canVen3Meses;
        private decimal uti3Meses;
        private decimal vet6Meses;
        private decimal canVen6Meses;
        private decimal uti6Meses;
        private decimal vet12Meses;
        private decimal canVen12Meses;
        private decimal uti12Meses;
        //historiaco venta
        private string fechaHist;
        private decimal cantVentHist;
        private decimal filiaHist;
        private decimal mvHisto;
        private decimal costoHist;
        private decimal utilHist;
        private string vacio;

        public string Vacio
        {
            get { return vacio; }
            set { vacio = value; }
        }

        public string FechaHist
        {
            get { return fechaHist; }
            set { fechaHist = value; }
        }
      public decimal Vet3Meses
        {
            get { return vet3Meses; }
            set { vet3Meses = value; }
        }
        public decimal CanVen3Meses
        {
            get { return canVen3Meses; }
            set { canVen3Meses = value; }
        }
        public decimal Uti3Meses
        {
            get { return uti3Meses; }
            set { uti3Meses = value; }
        }
         public decimal Vet6Meses
        {
            get { return vet6Meses; }
            set { vet6Meses = value; }
        }
         public decimal CanVen6Meses
        {
            get { return canVen6Meses; }
            set { canVen6Meses = value; }
        }
        public decimal Uti6Meses
        {
            get { return uti6Meses; }
            set { uti6Meses = value; }
        }
         public decimal Vet12Meses
        {
            get { return vet12Meses; }
            set { vet12Meses = value; }
        }
         public decimal CanVen12Meses
        {
            get { return canVen12Meses; }
            set { canVen12Meses = value; }
        }
        public decimal Uti12Meses
        {
            get { return uti12Meses; }
            set { uti12Meses = value; }
        }
        public decimal CantidadComprada
        {
            get { return cantidadComprada; }
            set { cantidadComprada = value; }
        }

        public decimal PorCostVent
        {
            get { return porCostVent; }
            set { porCostVent = value; }
        }
        public decimal MontoVenta
        {
            get { return montoVenta; }
            set { montoVenta = value; }
        
        }
        public decimal CostoVenta
        {
            get { return costoVenta; }
            set { costoVenta = value; }
        }
        public decimal CantVendida
        {
            get { return cantVendida; }
            set { cantVendida = value; }
        
        }
        public decimal CantVendidaFil
        {
            get { return cantVendidaFil; }
            set { cantVendidaFil = value; }
        }


         public decimal CostoCompras
        {
            get { return costoCompras; }
            set { costoCompras = value; }
        }

        public string Procecia
        {
            get { return procecia; }
            set { procecia = value; }
        
        }
        public string ContProm 
        {
            get { return contProm; }
            set { contProm = value; }
        
        }
        public decimal CostoRep1
        {
            get { return costoRep1; }
            set { costoRep1 = value; }
        }
        public decimal CostoRep2
        {
            get { return costoRep2; }
            set { costoRep2 = value; }
        }
        public decimal CostoRep3
        {
            get { return costoRep3; }
            set { costoRep3 = value; }
        }
        public decimal CostoRep4
        {
            get { return costoRep4; }
            set { costoRep4 = value; }
        }
        public decimal Provd1
        {
            get { return provd1; }
            set { provd1 = value; }
        }
        public decimal Provd2
        {
            get { return provd2; }
            set { provd2 = value; }
        }
        public decimal Provd3 
        {
            get { return provd3; }
            set { provd3 = value; }
        }
        public decimal PreLista2
        {
            get { return preLista2; }
            set { preLista2 = value; }
        }
          public string DesProv1
        {
            get { return desProv1; }
            set { desProv1 = value; }
        }
          public string DesProv2
        {
            get { return desProv2; }
            set { desProv2 = value; }
        }
          public string DesProv3
        {
            get { return desProv3; }
            set { desProv3 = value; }
        }
        public string AranDescProduc
        {
            get { return aranDescProduc; }
            set { aranDescProduc = value; }
        }
         public decimal AranPorsProduc
        {
            get { return aranPorsProduc; }
            set { aranPorsProduc = value; }
        }
        public string ProveeNombr
        {
            get { return proveeNombr; }
            set { proveeNombr = value; }
        }
        public string DescPais
        {
            get { return descPais; }
            set { descPais = value; }
        }
         public string ProDivi
        {
            get { return proDivi; }
            set { proDivi = value; }
        }

        public decimal FacCif
        {
            get { return facCif; }
            set { facCif = value; }
        }
        public decimal FacFin
        {
            get { return facFin; }
            set { facFin = value; }
        }
        public decimal FacGas
        {
            get { return facGas; }
            set { facGas = value; }
        }
         public string FacEnt
        {
            get { return facEnt; }
            set { facEnt = value; }
        }
        public string MonDescrp
        {
            get { return monDescrp; }
            set { monDescrp = value; }
        }
        public string HistMond
        {
            get { return histMond; }
            set { histMond = value; }
        }
       public decimal ProvDes1
        {
            get { return provDes1; }
            set { provDes1 = value; }
        }
        public decimal ProvDes2
        {
            get { return provDes2; }
            set { provDes2 = value; }
        }
        public decimal ProvDesc3
        {
            get { return provDesc3; }
            set { provDesc3 = value; }
        }
        public string PinMoned
        {
            get { return  pinMoned; }
            set {  pinMoned = value; }
        }
        public decimal FacOtro
        {
            get { return facOtro; }
            set { facOtro = value; }
        }
        public string PrecioProducto
        {
            get { return precioProduc; }
            set { precioProduc = value; }
        }
         public string UndMedProduc
        {
            get { return undMedProduc; }
            set {undMedProduc = value; }
        }
        public string DescProduc
        {
            get { return descProduc; }
            set {descProduc = value; }
        }
         public string DescGrupProduc
        {
            get { return descGrupProduc; }
            set { descGrupProduc = value; }
        }
        public string DescSubGrupProduc
        {
            get { return descSubGrupProduc; }
            set { descSubGrupProduc = value; }
        }
        public string EstadoProduc
        {
            get { return estadoProduc; }
            set { estadoProduc = value; }
        }

        public string PrefacProduc
        {
            get { return prefacProduc; }
            set { prefacProduc = value; }
        }
        public string PromProduc
        {
            get { return promProduc; }
            set { promProduc = value; }
        }
         public string ExisProduc
        {
            get { return exisProduc; }
            set { exisProduc = value; }
        }
        public string PedidoProduc
        {
            get { return pedidoProduc; }
            set { pedidoProduc = value; }
        }
        public string UltCostProduc
        {
            get { return ultCostProduc; }
            set { ultCostProduc = value; }
        }
         public string FecUltCompProduc
        {
            get { return fecUltCompProduc; }
            set { fecUltCompProduc = value; }
        }
        public string UltProveProduc
        {
            get { return ultProveProduc; }
            set { ultProveProduc = value; }
        }
        public string UltCantCompProduc
        {
            get { return ultCantCompProduc; }
            set { ultCantCompProduc = value; }
        }
         public string UltRefCompProduc
        {
            get { return ultRefCompProduc; }
            set { ultRefCompProduc = value; }
        }

         public string FecUltVentProduc
        {
            get { return fecUltVentProduc; }
            set { fecUltVentProduc= value; }
        }

         public string UltClienProduc
        {
            get { return ultClienProduc; }
            set { ultClienProduc = value; }
        }

         public string UltRefVentProduc
        {
            get { return ultRefVentProduc; }
            set { ultRefVentProduc = value; }
        }

        public string UultCantVent
        {
            get { return ultCantVent; }
            set { ultCantVent = value; }
        }

         public string FecUltAjusProduc
        {
            get { return fecUltAjusProduc; }
            set { fecUltAjusProduc = value; }
        }

        public string UltRefAjusProduc
        {
            get { return ultRefAjusProduc; }
            set { ultRefAjusProduc = value; }
        }
         public string UltCantAjustProduc
        {
            get { return ultCantAjustProduc; }
            set { ultCantAjustProduc = value; }
        }

        public ConsultaGeneral()
        {
          this.dataBaseConection = new ConexionBD();
          this.genrecial = new lbxConsultaGerencial();
        }
        public void consultaGereneralProducto(string codigo)
        {
            try
            {
                senteciaSql = null;
                string codGrup;
                string codSubGrup;
                senteciaSql = "SELECT inv_codigo,inv_proced,inv_descri,inv_grupo,inv_subgru,inv_estado,inv_promo,cost_prom,pre_lista2,dcto_prov1,dcto_prov2,dcto_prov3,(replace(FORMAT(adminv2.prefac, 2),',','.')) AS prefac,(replace(FORMAT(adminv2.existencia, 2),',','.')) AS Exitencia" +
                             ",(replace(FORMAT(adminv2.pedido, 2),',','.')) AS pedido,(replace(FORMAT(adminv2.ult_costo, 2),',','.')) AS uCosto,DATE_FORMAT(fec_ult_compra,'%d/%m/%Y')AS fCompra, adminv2.ult_provee, adminv2.ult_refer_compra" +
                              ",(replace(FORMAT(adminv2.ult_cant_compra, 2),',','.')) AS uComCant,DATE_FORMAT(adminv2.fec_ult_vent,'%d/%m/%Y') AS fVenta  ,adminv2.ult_cliente_vent,adminv2.ult_refer_vent,adminvmed.ime_undmed " +
                              ",(replace(FORMAT(adminv2.ult_cant_vent, 2),',','.')) AS uVenCant,DATE_FORMAT(adminv2.fec_ult_aju,'%d/%m/%Y') AS fAjuste, adminv2.ult_refer_aju,(replace(FORMAT(adminv2.ult_cant_aju, 2),',','.')) AS uAjutCant FROM adminv " +
                              "LEFT JOIN adminvmed  ON adminvmed.ime_codigo=adminv.inv_codigo " +
                              "LEFT JOIN adminv2  ON adminv2.inv2_codigo=adminv.inv_codigo WHERE inv_codigo='" + codigo + "';";
                dt = dataBaseConection.fDataTable(senteciaSql);

                this.procecia = dt.Rows[0]["inv_proced"].ToString();
                this.undMedProduc = dt.Rows[0]["ime_undmed"].ToString();
                this.descProduc = dt.Rows[0]["inv_descri"].ToString();
                codGrup = dt.Rows[0]["inv_grupo"].ToString();
                codSubGrup = dt.Rows[0]["inv_subgru"].ToString();
                this.estadoProduc = dt.Rows[0]["inv_estado"].ToString();
                this.promProduc = dt.Rows[0]["inv_promo"].ToString();
                this.prefacProduc = dt.Rows[0]["prefac"].ToString();
                this.exisProduc = dt.Rows[0]["Exitencia"].ToString();
                this.pedidoProduc = dt.Rows[0]["pedido"].ToString();
                this.ultCostProduc = dt.Rows[0]["uCosto"].ToString();
                this.fecUltCompProduc = dt.Rows[0]["fCompra"].ToString();
                this.ultProveProduc = dt.Rows[0]["ult_provee"].ToString();
                this.ultRefCompProduc = dt.Rows[0]["ult_refer_compra"].ToString();
                this.ultCantCompProduc = dt.Rows[0]["uComCant"].ToString();
                this.fecUltVentProduc = dt.Rows[0]["fVenta"].ToString();
                this.ultClienProduc = dt.Rows[0]["ult_cliente_vent"].ToString();
                this.ultRefVentProduc = dt.Rows[0]["ult_refer_vent"].ToString();
                this.ultCantVent = dt.Rows[0]["uVenCant"].ToString();
                this.fecUltAjusProduc = dt.Rows[0]["fAjuste"].ToString();
                this.ultRefAjusProduc = dt.Rows[0]["ult_refer_aju"].ToString();
                this.ultCantAjustProduc = dt.Rows[0]["uAjutCant"].ToString();
                this.contProm = dt.Rows[0]["cost_prom"].ToString();
                this.preLista2 = Convert.ToDecimal(dt.Rows[0]["pre_lista2"].ToString());
                this.desProv1 = dt.Rows[0]["dcto_prov1"].ToString();
                this.desProv2 = dt.Rows[0]["dcto_prov2"].ToString();
                this.desProv3 = dt.Rows[0]["dcto_prov3"].ToString();

                senteciaSql = null;
                senteciaSql = "SELECT ime_undmed FROM adminvmed WHERE ime_codigo='" + codigo + "' ;";
                dt = dataBaseConection.fDataTable(senteciaSql);
                this.undMedProduc = dt.Rows[0]["ime_undmed"].ToString();


                senteciaSql = null;
                senteciaSql = "SELECT gru_descri FROM admgrupo WHERE gru_codigo='" + codGrup + "' ;";
                dt = dataBaseConection.fDataTable(senteciaSql);
                this.descGrupProduc = dt.Rows[0]["gru_descri"].ToString();

                senteciaSql = null;
                senteciaSql = "SELECT sgr_descri FROM admsubgrupo WHERE sgr_grupo='" + codGrup + "' AND sgr_codigo='" + codSubGrup + "' ;";
                dt = dataBaseConection.fDataTable(senteciaSql);
                this.descSubGrupProduc = dt.Rows[0]["sgr_descri"].ToString();

                senteciaSql = null;
                senteciaSql = "SELECT pre_precio FROM admprecios WHERE pre_codigo='" + codigo + "' AND pre_lista='A' ;";
                dt = dataBaseConection.fDataTable(senteciaSql);
                this.precioProduc = dt.Rows[0]["pre_precio"].ToString();
                dataBaseConection.cerrarConexion();

                senteciaSql = null;
                senteciaSql = "SELECT pre_precio,pre_lista FROM admprecios WHERE pre_codigo='" + codigo + "' AND pre_act='1' ORDER BY pre_lista ASC;";
                dt = dataBaseConection.fDataTable(senteciaSql);
                this.precioProduc = dt.Rows[0]["pre_precio"].ToString();
                dataBaseConection.cerrarConexion();
                this.vacio = "";

            }
            catch (Exception ex)
            {
                MessageBox.Show("Producto no Existe ", "Mensaje del Sistema");
                this.vacio = "Vacio";
            }
        }
        public DataTable consultaCosto(string codigo) 
        {
            DataTable dt;
            senteciaSql = null;

            senteciaSql = "SELECT mov_codcta As Codigo,admproveedor.pro_nombre As Proveedor,mov_docume AS Documento,replace(replace(mov_tipdoc,'37','FAC'),'43','DEV') As Tipo_Documento,mov_fecha AS Fecha,mov_costo AS Costo,mov_cant AS Cantidad" +
                          " From adminvmov2 LEFT OUTER JOIN admproveedor ON adminvmov2.mov_codcta = admproveedor.pro_codigo " +
                          " Where mov_codigo='" + codigo + "' AND mov_invact=1 AND mov_fisico=1 AND mov_tipdoc <>'DEV' " +
                          " GROUP BY mov_tipdoc , mov_docume, mov_codigo ORDER BY mov_fecha DESC,mov_hora DESC LIMIT 3000 ";
            dt = dataBaseConection.fDataTable(senteciaSql);
            dataBaseConection.cerrarConexion();
            return dt;
        }
        public void consultaGeneralProveedor(string Codigo) 
        {
            senteciaSql = null;
            senteciaSql = "SELECT " +
                  "inv_codigo,inv_arancel,ara_descri,ara_porcen, dcto_prov1 AS K1,dcto_prov2 AS K2,dcto_prov3 AS K3, " +
                  "pro_codigo,pro_nombre,pais_descri,pro_divisa, " +
                  "Factor_CIF,Factor_FIN, Factor_GAS,Factor_Otros, " +
                  "Factor_Ent,mon_descri,histo_valor,pro_descuento, pro_dcto2, pro_dcto3, pin_moneda  " +
                "FROM " +
                  "adminv " +
                  "LEFT JOIN adminvparam ON inv_codigo = pin_codigo " +
                  "LEFT JOIN admproveedor ON pin_proveedor = pro_codigo " +
                  "LEFT JOIN admproveedor2 ON pro2_codigo = pro_codigo " +
                  "LEFT JOIN admpais ON pro_pais = pais_codigo " +
                  "LEFT JOIN admmoneda ON pro_divisa = mon_codigo " +
                  "LEFT JOIN admarancel ON ara_codigo= inv_arancel " +
                  "LEFT JOIN admhistodivi ON histo_moneda = pro_divisa " +
                  "LEFT JOIN adminv2 ON inv_codigo = inv2_codigo " +
                " WHERE " +
                 " inv_codigo='" +Codigo+ "' " +
                 "ORDER BY " +
                    "histo_fecha DESC,histo_hora DESC;";
            dt = dataBaseConection.fDataTable(senteciaSql);
            this.aranDescProduc = dt.Rows[0]["ara_descri"].ToString();
            this.aranPorsProduc =Convert.ToDecimal(dt.Rows[0]["ara_porcen"].ToString());
            this.proveeNombr = dt.Rows[0]["pro_nombre"].ToString();
            this.descPais = dt.Rows[0]["pais_descri"].ToString();
            this.facCif = Convert.ToDecimal(dt.Rows[0]["Factor_CIF"].ToString());
            this.facFin = Convert.ToDecimal(dt.Rows[0]["Factor_FIN"].ToString());
            this.facGas = Convert.ToDecimal(dt.Rows[0]["Factor_GAS"].ToString());
            this.facOtro = Convert.ToDecimal(dt.Rows[0]["Factor_Otros"].ToString());
            this.monDescrp = dt.Rows[0]["mon_descri"].ToString();
            this.histMond = dt.Rows[0]["histo_valor"].ToString();
            this.provDes1 = Convert.ToDecimal(dt.Rows[0]["pro_descuento"].ToString());
            this.provDes2 = Convert.ToDecimal(dt.Rows[0]["pro_dcto2"].ToString());
            this.provDesc3 =Convert.ToDecimal(dt.Rows[0]["pro_dcto3"].ToString());
            this.pinMoned = dt.Rows[0]["pin_moneda"].ToString();
            this.facEnt = dt.Rows[0]["Factor_Ent"].ToString();
            this.provd1 = Convert.ToDecimal(dt.Rows[0]["K1"].ToString());
            this.provd2 = Convert.ToDecimal(dt.Rows[0]["K2"].ToString());
            this.provd3 =Convert.ToDecimal( dt.Rows[0]["K3"].ToString());
            

            senteciaSql = null;
            senteciaSql = "SELECT * FROM admhistodivi WHERE histo_moneda='"+PinMoned+"' OR histo_moneda='" + PinMoned + +2 + "' OR " +
                  " histo_moneda='"+PinMoned+ +3+"' OR histo_moneda='"+PinMoned+ +4+"'"+
                  " Group by histo_moneda order by histo_moneda desc,histo_fecha desc,histo_hora desc";
            dt = dataBaseConection.fDataTable(senteciaSql);
            cantFila =dt.Rows.Count;
            hisValor1 = Convert.ToDecimal(dt.Rows[0]["histo_valor"].ToString());
            hisValor2 = Convert.ToDecimal(dt.Rows[1]["histo_valor"].ToString());
            hisValor3 = Convert.ToDecimal(dt.Rows[2]["histo_valor"].ToString());
            hisValor4 = Convert.ToDecimal(dt.Rows[3]["histo_valor"].ToString());
            dataBaseConection.cerrarConexion();
            
            if (AranPorsProduc == Convert.ToDecimal(0.00))
            {
                cr = PreLista2;
            }
            else if (PreLista2 == Convert.ToDecimal(0.00))
            {
                cr = Convert.ToDecimal(0.00);
            }
            else
            {
                cr = Convert.ToDecimal(((AranPorsProduc / 100) + 1) * PreLista2);
            }
            if (cantFila >= 4) 
            {
                if (cr > 0) 
                {
                    cr4 = cr;
                }
                if (FacCif > 0) 
                {
                    cr4 = cr4 * FacCif;
                }
                if (FacOtro > 0)
                {
                    cr4 = cr4 * FacOtro;
                }
                if (FacGas > 0)
                {
                    cr4 = cr4 * FacGas;
                }
                if (hisValor4 > 0)
                {
                    cr4 = cr4 * hisValor4;
                }
                if (facFin > 0)
                {
                    cr4 = cr4 * facFin;
                }
            }
             if (cantFila >= 3)
            {
                if (cr > 0)
                {
                    cr3 = cr;
                }
                if (FacCif > 0)
                {
                    cr3 = cr3 * FacCif;
                }
                if (FacOtro > 0)
                {
                    cr3 = cr3 * FacOtro;
                }
                if (FacGas > 0)
                {
                    cr3 = cr3 * FacGas;
                }
                if (hisValor3 > 0)
                {
                    cr3 = cr3 * hisValor3;
                }
                if (facFin > 0)
                {
                    cr3 = cr3 * facFin;
                }
            }
            if (cantFila >= 2)
            {
                if (cr > 0)
                {
                    cr2 = cr;
                }
                if (FacCif > 0)
                {
                    cr2 = cr2 * FacCif;
                }
                if (FacOtro > 0)
                {
                    cr2 = cr2 * FacOtro;
                }
                if (FacGas > 0)
                {
                    cr2 = cr2 * FacGas;
                }
                if (hisValor2 > 0)
                {
                    cr2 = cr2 * hisValor2;
                }
                if (facFin > 0)
                {
                    cr2 = cr2 * facFin;
                }
            }
            if (cantFila >= 1)
            {
                if (cr > 0)
                {
                    cr1 = cr;
                }
                if (FacCif > 0)
                {
                    cr1 = cr1 * FacCif;
                }
                if (FacOtro > 0)
                {
                    cr1 = cr1 * FacOtro;
                }
                if (FacGas > 0)
                {
                    cr1 = cr1 * FacGas;
                }
                if (hisValor1 > 0)
                {
                    cr1 = cr1 * hisValor1;
                }
                if (facFin > 0)
                {
                    cr1 = cr1 * facFin;
                }
            }
            //provedor
            if (Provd1 > 0) 
            {
                cr1 = cr1 - ((cr1 * Provd1) / 100);
                cr2 = cr2 - ((cr2 * Provd1) / 100);
                cr3 = cr3 - ((cr3 * Provd1) / 100);
                cr4 = cr4 - ((cr4 * Provd1) / 100);
                if (Provd2 > 0)
                {
                    cr1 = cr1 - ((cr1 * Provd2) / 100);
                    cr2 = cr2 - ((cr2 * Provd2) / 100);
                    cr3 = cr3 - ((cr3 * Provd2) / 100);
                    cr4 = cr4 - ((cr4 * Provd2) / 100);
                    if (Provd3 > 0)
                    {
                        cr1 = cr1 - ((cr1 * Provd3) / 100);
                        cr2 = cr2 - ((cr2 * Provd3) / 100);
                        cr3 = cr3 - ((cr3 * Provd3) / 100);
                        cr4 = cr4 - ((cr4 * Provd3) / 100);
                    }
                }
            }
       
            //producto
            if (ProvDes1 > 0)
            {
                cr1 = cr1 - ((cr1 * Provd1) / 100);
                cr2 = cr2 - ((cr2 * Provd1) / 100);
                cr3 = cr3 - ((cr3 * Provd1) / 100);
                cr4 = cr4 - ((cr4 * Provd1) / 100);
                if (ProvDes2 > 0)
                {
                    cr1 = cr1 - ((cr1 * Provd2) / 100);
                    cr2 = cr2 - ((cr2 * Provd2) / 100);
                    cr3 = cr3 - ((cr3 * Provd2) / 100);
                    cr4 = cr4 - ((cr4 * Provd2) / 100);
                    if (ProvDesc3 > 0)
                    {
                        cr1 = cr1 - ((cr1 * Provd2) / 100);
                        cr2 = cr2 - ((cr2 * Provd2) / 100);
                        cr3 = cr3 - ((cr3 * Provd2) / 100);
                        cr4 = cr4 - ((cr4 * Provd2) / 100);
                    }
                }
            }
            this.costoRep1 = decimalRedondear(cr4, 2);
            this.costoRep2 = decimalRedondear(cr3, 2);
            this.costoRep3 = decimalRedondear(cr2, 2);
            this.costoRep4 = decimalRedondear(cr1, 2);
        }
        public string ValidarExitecia(string codigo) 
        {
            DataTable dt;
            senteciaSql = null;

            string resultado;
            senteciaSql = "SELECT adminv2.existencia FROM adminv2 INNER JOIN admprecios ON adminv2.inv2_codigo = admprecios.pre_codigo WHERE pre_codigo = '$1' AND pre_lista = 'A' AND pre_act = '1' AND existencia > 0; ";
            senteciaSql = senteciaSql.Replace("$1", codigo);
            dt = dataBaseConection.fDataTable(senteciaSql);
            dataBaseConection.cerrarConexion();
            if (dt.Rows.Count > 0)
            {
                resultado = dt.Rows[0]["existencia"].ToString();
                return resultado;
            }
            else
            {
                resultado = "0";
                return resultado;
            }
        }
        public decimal decimalRedondear(decimal Argunmento, int Digitos)
        {
            decimal redondeo = decimal.Round(Argunmento, Digitos);
            return redondeo;
        }
        public void llenarPrecio(ComboBox cb, string codigo) 
        {
            try
            {
                senteciaSql = null;
                senteciaSql = "SELECT pre_precio,pre_lista FROM admprecios WHERE pre_codigo='" + codigo + "' AND pre_act='1'";
                dr = dataBaseConection.ejecutarQueryDr(senteciaSql);
                while (dr.Read())
                {
                    cb.Items.Add((dr["pre_lista"].ToString() ) + " "+( dr["pre_precio"].ToString()));

                }
                cb.SelectedIndex = 0;
                dataBaseConection.cerrarConexion();

            }
            catch (Exception ex)
            {
                MessageBox.Show("No Se Llenado ComboBox:" + ex.ToString());
            }
        }
        public void MovientoAñoActual(string codigo)
        {
            senteciaSql = null;
            senteciaSql = "SELECT SUM(IF(mov_tipdoc ='FAV',mov_total,0) + IF(mov_tipdoc ='DEV',mov_total*-1,0)) AS monto_VENTA"+
            ",SUM(IF(mov_tipdoc ='FAV',mov_cant*mov_costo,0) + IF(mov_tipdoc ='DEV',mov_cant*mov_costo*-1,0)) AS costo_VENTA, "+
            "SUM(IF(mov_tipdoc ='FAV',mov_cant,0) + IF(mov_tipdoc ='DEV',mov_cant*-1,0)) AS cant_VENDIDA, "+
            "SUM(IF(mov_tipdoc ='FAV' AND mov_vendedor >= 80,mov_cant,0) + IF(mov_tipdoc ='DEV' AND mov_vendedor >= 80,mov_cant*-1,0)) AS cant_VENT_FIL"+
            " FROM adminvmov WHERE YEAR(mov_fecha) = YEAR(CURDATE()) AND mov_codigo = '"+ codigo +"';";

            dt = dataBaseConection.fDataTable(senteciaSql);
            if (string.IsNullOrEmpty(dt.Rows[0]["monto_VENTA"].ToString()))
            {
                this.montoVenta = Convert.ToDecimal(0.00);
            }
            else
            {
                this.montoVenta = Convert.ToDecimal(dt.Rows[0]["monto_VENTA"].ToString());
                this.montoVenta = decimalRedondear(montoVenta, 2);
            }
            if (string.IsNullOrEmpty(dt.Rows[0]["costo_VENTA"].ToString()))
            {
                this.costoVenta = Convert.ToDecimal(0.00);
            }
            else
            {
                this.costoVenta = Convert.ToDecimal(dt.Rows[0]["costo_VENTA"].ToString());
                this.costoVenta = decimalRedondear(costoVenta, 2);
            }
            if (string.IsNullOrEmpty(dt.Rows[0]["cant_VENDIDA"].ToString()))
            {
                this.cantVendida = Convert.ToDecimal(0.00);
            }
            else
            {
                this.cantVendida = Convert.ToDecimal(dt.Rows[0]["cant_VENDIDA"].ToString());
                this.cantVendida = decimalRedondear(cantVendida, 2);
            }
            if (string.IsNullOrEmpty(dt.Rows[0]["cant_VENT_FIL"].ToString()))
            {
                this.cantVendidaFil = Convert.ToDecimal(0.00);
            }
            else
            {
                this.cantVendidaFil = Convert.ToDecimal(dt.Rows[0]["cant_VENT_FIL"].ToString());
                this.cantVendidaFil = decimalRedondear(cantVendidaFil, 2);
            }
            if (string.IsNullOrEmpty(dt.Rows[0]["costo_VENTA"].ToString()))
            {
                    this.porCostVent = Convert.ToDecimal(0.00);
            }
            else
            {
                if (CostoVenta > 0 && MontoVenta > 0)
                {
                    this.porCostVent = ((MontoVenta - CostoVenta) * 100) / MontoVenta;
                    this.porCostVent = decimalRedondear(porCostVent, 2);
                }
                else 
                {
                    this.porCostVent = Convert.ToDecimal(0.00);
                }
            }
             senteciaSql = null;
            senteciaSql = "SELECT SUM(IF(mov_tipdoc ='37',mov_total,0)) AS COSTO_Compras "+
            ",SUM(IF(mov_tipdoc ='37',mov_cant,0)) AS CANTIDAD_Comprada From adminvmov2  "+
            " WHERE YEAR(mov_fecha) = YEAR(CURDATE()) AND mov_codigo = '"+ codigo +"';";

            dt = dataBaseConection.fDataTable(senteciaSql);

            if (string.IsNullOrEmpty(dt.Rows[0]["COSTO_Compras"].ToString()))
            {
                this.costoCompras = Convert.ToDecimal(0.00);
            }
            else
            {
                this.costoCompras = Convert.ToDecimal(dt.Rows[0]["COSTO_Compras"].ToString());
                this.costoCompras = decimalRedondear(costoCompras, 2);
            }
            if (string.IsNullOrEmpty(dt.Rows[0]["CANTIDAD_Comprada"].ToString()))
            {
                this.cantidadComprada = Convert.ToDecimal(0.00);
            }
            else
            {
                this.cantidadComprada = Convert.ToDecimal(dt.Rows[0]["CANTIDAD_Comprada"].ToString());
                this.cantidadComprada = decimalRedondear(cantidadComprada, 2);
            }
            dataBaseConection.cerrarConexion();
        }
        public void PromedioVentas(string codigo) 
        {
            // Ventas 3 Meses
            string fecha3m1 = (DateTime.Now.AddMonths(-3)).ToString("yyyy-MM-dd"); 
            string fecha3m2 = (DateTime.Now.AddDays(-1)).ToString("yyyy-MM-dd");
            string fecha6m1 = (DateTime.Now.AddMonths(-6)).ToString("yyyy-MM-dd");
            string fecha6m2 = (DateTime.Now.AddDays(-1)).ToString("yyyy-MM-dd");
            string fecha12m1 = (DateTime.Now.AddMonths(-12)).ToString("yyyy-MM-dd");
            string fecha12m2 = (DateTime.Now.AddDays(-1)).ToString("yyyy-MM-dd");
            senteciaSql = null;
            senteciaSql = "SELECT SUM(IF(mov_tipdoc ='FAV',mov_total,0) + IF(mov_tipdoc ='DEV',mov_total*-1,0)) AS monto_VENTA,"+
                " SUM(IF(mov_tipdoc ='FAV',mov_cant*mov_costo,0) + IF(mov_tipdoc ='DEV',mov_cant*mov_costo*-1,0)) AS costo_VENTA,"+
                " SUM(IF(mov_tipdoc ='FAV',mov_cant,0) + IF(mov_tipdoc ='DEV',mov_cant*-1,0)) AS cant_VENDIDA "+
                "FROM adminvmov WHERE mov_fechven between '" + fecha3m1 + "' " +
                " and '" + fecha3m2 + "'AND mov_codigo = '" + codigo + "';";
            dt = dataBaseConection.fDataTable(senteciaSql);

            if (string.IsNullOrEmpty(dt.Rows[0]["monto_VENTA"].ToString()))
            {
                this.vet3Meses = Convert.ToDecimal(0.00);
            }
            else
            {
                this.vet3Meses = Convert.ToDecimal(dt.Rows[0]["costo_VENTA"].ToString());
                this.vet3Meses = vet3Meses / 3;
                this.vet3Meses = decimalRedondear(vet3Meses, 2);
            }
            if (string.IsNullOrEmpty(dt.Rows[0]["cant_VENDIDA"].ToString()))
            {
                this.canVen3Meses = Convert.ToDecimal(0.00);
            }
            else
            {
                this.canVen3Meses = Convert.ToDecimal(dt.Rows[0]["monto_VENTA"].ToString());
                this.canVen3Meses = canVen3Meses / 3;
                this.canVen3Meses = decimalRedondear(canVen3Meses, 2);
            }
            if (string.IsNullOrEmpty(dt.Rows[0]["costo_VENTA"].ToString()))
            {
                this.uti3Meses = Convert.ToDecimal(0.00);
            }
            else
            {
                if (vet3Meses > 0)
                {
                    this.uti3Meses = ((canVen3Meses - vet3Meses) * 100) / canVen3Meses;
                    this.uti3Meses = decimalRedondear(uti3Meses, 2);
                }
                else
                {
                    this.uti3Meses = Convert.ToDecimal(0.00);
                }
            }

            //// Ventas 6 Meses
            senteciaSql = null;
            senteciaSql = "SELECT SUM(IF(mov_tipdoc ='FAV',mov_total,0) + IF(mov_tipdoc ='DEV',mov_total*-1,0)) AS monto_VENTA," +
            " SUM(IF(mov_tipdoc ='FAV',mov_cant*mov_costo,0) + IF(mov_tipdoc ='DEV',mov_cant*mov_costo*-1,0)) AS costo_VENTA," +
            " SUM(IF(mov_tipdoc ='FAV',mov_cant,0) + IF(mov_tipdoc ='DEV',mov_cant*-1,0)) AS cant_VENDIDA " +
            " FROM adminvmov WHERE mov_fechven between '" + fecha6m1 + "'" +
            " and ' " + fecha6m1 + "'AND mov_codigo = '" + codigo + "';";
            dt = dataBaseConection.fDataTable(senteciaSql);
            if (string.IsNullOrEmpty(dt.Rows[0]["monto_VENTA"].ToString()))
            {
                this.vet6Meses = Convert.ToDecimal(0.00);
            }
            else
            {
                this.vet6Meses = Convert.ToDecimal(dt.Rows[0]["costo_VENTA"].ToString());
                this.vet6Meses = vet6Meses / 6;
                this.vet6Meses = decimalRedondear(vet6Meses, 2);
            }
            if (string.IsNullOrEmpty(dt.Rows[0]["cant_VENDIDA"].ToString()))
            {
                this.canVen6Meses = Convert.ToDecimal(0.00);
            }
            else
            {
                this.canVen6Meses = Convert.ToDecimal(dt.Rows[0]["monto_VENTA"].ToString());
                this.canVen6Meses = canVen6Meses / 6;
                this.canVen6Meses = decimalRedondear(canVen6Meses, 2);
            }
            if (string.IsNullOrEmpty(dt.Rows[0]["costo_VENTA"].ToString()))
            {
                this.uti6Meses = Convert.ToDecimal(0.00);
            }
            else
            {
                if (vet6Meses > 0)
                {
                    this.uti6Meses = ((canVen6Meses - vet6Meses) * 100) / canVen6Meses;
                    this.uti6Meses = decimalRedondear(uti6Meses, 2);
                }
                else
                {
                    this.porCostVent = Convert.ToDecimal(0.00);
                }
            }

            // Ventas 12 Meses
            senteciaSql = null;
            senteciaSql = "SELECT SUM(IF(mov_tipdoc ='FAV',mov_total,0) + IF(mov_tipdoc ='DEV',mov_total*-1,0)) AS monto_VENTA, " +
            " SUM(IF(mov_tipdoc ='FAV',mov_cant*mov_costo,0) + IF(mov_tipdoc ='DEV',mov_cant*mov_costo*-1,0)) AS costo_VENTA, " +
            " SUM(IF(mov_tipdoc ='FAV',mov_cant,0) + IF(mov_tipdoc ='DEV',mov_cant*-1,0)) AS cant_VENDIDA " +
            " FROM adminvmov WHERE mov_fechven between '" + fecha12m1 +"' " +
            " AND '" + fecha12m2 + "' AND mov_codigo = '" + codigo + "';";
            dt = dataBaseConection.fDataTable(senteciaSql);
            if (string.IsNullOrEmpty(dt.Rows[0]["monto_VENTA"].ToString()))
            {
                this.vet12Meses = Convert.ToDecimal(0.00);
            }
            else
            {
                this.vet12Meses = Convert.ToDecimal(dt.Rows[0]["costo_VENTA"].ToString());
                this.vet12Meses = vet12Meses / 12;
                this.vet12Meses = decimalRedondear(vet12Meses, 2);
            }
            if (string.IsNullOrEmpty(dt.Rows[0]["cant_VENDIDA"].ToString()))
            {
                this.canVen12Meses = Convert.ToDecimal(0.00);
            }
            else
            {
                this.canVen12Meses = Convert.ToDecimal(dt.Rows[0]["monto_VENTA"].ToString());
                this.canVen12Meses = canVen12Meses / 12;
                this.canVen12Meses = decimalRedondear(canVen12Meses, 2);
            }
            if (string.IsNullOrEmpty(dt.Rows[0]["costo_VENTA"].ToString()))
            {
                this.uti12Meses = Convert.ToDecimal(0.00);
            }
            else
            {
                if (vet12Meses > 0)
                {
                    this.uti12Meses = ((canVen12Meses - vet12Meses) * 100) / canVen12Meses;
                    this.uti12Meses = decimalRedondear(uti12Meses, 2);
                }
                else
                {
                    this.uti12Meses = Convert.ToDecimal(0.00);
                }
            }
        }
        public DataTable datosHistoriaVentas(string codigo)
        {


            DataTable dt;
            
            senteciaSql = null;

            senteciaSql = senteciaSql = "SELECT mov_fecha AS FECHA, " +
                  "SUM(IF(mov_tipdoc='FAV',mov_cant,0) + IF(mov_tipdoc='DEV',mov_cant*-1,0)) AS CANT_Vendida, " +
                  "SUM(IF(mov_tipdoc='FAV' AND mov_vendedor>'0000000080',mov_cant,0) + IF(mov_tipdoc='DEV' AND mov_vendedor>'0000000080',mov_cant*-1,0)) AS CANT_Filial," +
                  "SUM(IF(mov_tipdoc='FAV',mov_total,0) + IF(mov_tipdoc='DEV',mov_total*-1,0)) AS Monto_Vendido, " +
                  "SUM(IF(mov_tipdoc='FAV',mov_costo*mov_cant,0) + IF(mov_tipdoc='DEV',mov_costo*mov_cant*-1,0)) AS COSTO, "+ 
                  "((SUM(IF(mov_tipdoc='FAV',mov_total,0) + IF(mov_tipdoc='DEV',mov_total*-1,0))-(SUM(IF(mov_tipdoc='FAV',mov_costo*mov_cant,0)) + IF(mov_tipdoc='DEV',mov_costo*mov_cant*-1,0)))*100)/SUM(IF(mov_tipdoc='FAV',mov_total,0) + IF(mov_tipdoc='DEV',mov_total*-1,0)) as Utilida " +
                "FROM adminvmov " +
                "WHERE mov_codigo='" + codigo + "' " +
                "GROUP BY YEAR(mov_fecha),MONTH(mov_fecha) " +
                  "ORDER BY mov_fecha DESC; HAVING CANT_Vendida > 0 ";
            dt = dataBaseConection.fDataTable(senteciaSql);
            dataBaseConection.cerrarConexion();
            return dt;
        }
    }

}
