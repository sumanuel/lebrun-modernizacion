using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace lebrun.clases.facturacion
{
    public class ImpresoraFiscalPNPDLL
    {

        #region DECLARACIÓN DE LAS FUNCIONES de PNPDLL.DLL
        // PNPDLL devuelve los textos como punteros a memoria que NO es del heap del proceso (cadenas fijas de la propia DLL). Declararlas con retorno
        // "string" hace que .NET intente liberarlas con CoTaskMemFree y corrompa el heap (Windows cierra el programa con el codigo 0xc0000374 en ntdll.dll,
        // sobre todo cuando la impresora no responde). Por eso se declaran como IntPtr y se leen sin liberar nada.
        private static string TextoNativo(IntPtr puntero)
        {
            return puntero == IntPtr.Zero ? "" : (Marshal.PtrToStringAnsi(puntero) ?? "");
        }
        [DllImport("PNPDLL.dll", EntryPoint = "PFAbreNF")]
        private static extern IntPtr PFAbreNF_Nativo();
        public static string PFAbreNF() { return TextoNativo(PFAbreNF_Nativo()); }
        [DllImport("PNPDLL.dll", EntryPoint = "PFabrefiscal")]
        private static extern IntPtr PFabrefiscal_Nativo(String Razon, String RIF);
        public static string PFabrefiscal(String Razon, String RIF) { return TextoNativo(PFabrefiscal_Nativo(Razon, RIF)); }
        [DllImport("PNPDLL.dll", EntryPoint = "PFcomando")]
        private static extern IntPtr PFcomando_Nativo(String comando);
        public static string PFcomando(String comando) { return TextoNativo(PFcomando_Nativo(comando)); }
        [DllImport("PNPDLL.dll", EntryPoint = "PFtotal")]
        private static extern IntPtr PFtotal_Nativo();
        public static string PFtotal() { return TextoNativo(PFtotal_Nativo()); }
        [DllImport("PNPDLL.dll", EntryPoint = "PFrepz")]
        private static extern IntPtr PFrepz_Nativo();
        public static string PFrepz() { return TextoNativo(PFrepz_Nativo()); }
        [DllImport("PNPDLL.dll", EntryPoint = "PFrepx")]
        private static extern IntPtr PFrepx_Nativo();
        public static string PFrepx() { return TextoNativo(PFrepx_Nativo()); }
        [DllImport("PNPDLL.dll", EntryPoint = "PFrenglon")]
        private static extern IntPtr PFrenglon_Nativo(String Descripcion, String cantidad, String monto, String iva);
        public static string PFrenglon(String Descripcion, String cantidad, String monto, String iva) { return TextoNativo(PFrenglon_Nativo(Descripcion, cantidad, monto, iva)); }
        [DllImport("PNPDLL.dll", EntryPoint = "PFabrepuerto")]
        private static extern IntPtr PFabrepuerto_Nativo(String numero);
        public static string PFabrepuerto(String numero) { return TextoNativo(PFabrepuerto_Nativo(numero)); }
        [DllImport("PNPDLL.dll", EntryPoint = "PFcierrapuerto")]
        private static extern IntPtr PFcierrapuerto_Nativo();
        public static string PFcierrapuerto() { return TextoNativo(PFcierrapuerto_Nativo()); }
        [DllImport("PNPDLL.dll", EntryPoint = "PFDisplay950")]
        private static extern IntPtr PFDisplay950_Nativo(String edlinea);
        public static string PFDisplay950(String edlinea) { return TextoNativo(PFDisplay950_Nativo(edlinea)); }
        
        [DllImport("PNPDLL.dll", EntryPoint = "PFLineaNF")]
        private static extern IntPtr PFLineaNF_Nativo(String edlinea);
        public static string PFLineaNF(String edlinea) { return TextoNativo(PFLineaNF_Nativo(edlinea)); }
        [DllImport("PNPDLL.dll", EntryPoint = "PFCierraNF")]
        private static extern IntPtr PFCierraNF_Nativo();
        public static string PFCierraNF() { return TextoNativo(PFCierraNF_Nativo()); }
        [DllImport("PNPDLL.dll", EntryPoint = "PFCortar")]
        private static extern IntPtr PFCortar_Nativo();
        public static string PFCortar() { return TextoNativo(PFCortar_Nativo()); }
        [DllImport("PNPDLL.dll", EntryPoint = "PFTfiscal")]
        private static extern IntPtr PFTfiscal_Nativo(String edlinea);
        public static string PFTfiscal(String edlinea) { return TextoNativo(PFTfiscal_Nativo(edlinea)); }
        [DllImport("PNPDLL.dll", EntryPoint = "PFparcial")]
        private static extern IntPtr PFparcial_Nativo();
        public static string PFparcial() { return TextoNativo(PFparcial_Nativo()); }
        [DllImport("PNPDLL.dll", EntryPoint = "PFSerial")]
        private static extern IntPtr PFSerial_Nativo();
        public static string PFSerial() { return TextoNativo(PFSerial_Nativo()); }
        [DllImport("PNPDLL.dll", EntryPoint = "PFtoteconomico")]
        private static extern IntPtr PFtoteconomico_Nativo();
        public static string PFtoteconomico() { return TextoNativo(PFtoteconomico_Nativo()); }
        [DllImport("PNPDLL.dll", EntryPoint = "PFCancelaDoc")]
        private static extern IntPtr PFCancelaDoc_Nativo(String edlinea, String monto);
        public static string PFCancelaDoc(String edlinea, String monto) { return TextoNativo(PFCancelaDoc_Nativo(edlinea, monto)); }
        [DllImport("PNPDLL.dll", EntryPoint = "PFGaveta")]
        private static extern IntPtr PFGaveta_Nativo();
        public static string PFGaveta() { return TextoNativo(PFGaveta_Nativo()); }
        [DllImport("PNPDLL.dll", EntryPoint = "PFDevolucion")]
        private static extern IntPtr PFDevolucion_Nativo(String razon, String rif, String comp, String maqui, String fecha, String hora);
        public static string PFDevolucion(String razon, String rif, String comp, String maqui, String fecha, String hora) { return TextoNativo(PFDevolucion_Nativo(razon, rif, comp, maqui, fecha, hora)); }
        [DllImport("PNPDLL.dll", EntryPoint = "PFSlipON")]
        private static extern IntPtr PFSlipON_Nativo();
        public static string PFSlipON() { return TextoNativo(PFSlipON_Nativo()); }
        [DllImport("PNPDLL.dll", EntryPoint = "PFSLIPOFF")]
        private static extern IntPtr PFSLIPOFF_Nativo();
        public static string PFSLIPOFF() { return TextoNativo(PFSLIPOFF_Nativo()); }
        [DllImport("PNPDLL.dll", EntryPoint = "PFestatus")]
        private static extern IntPtr PFestatus_Nativo(String edlinea);
        public static string PFestatus(String edlinea) { return TextoNativo(PFestatus_Nativo(edlinea)); }
        [DllImport("PNPDLL.dll", EntryPoint = "PFreset")]
        private static extern IntPtr PFreset_Nativo();
        public static string PFreset() { return TextoNativo(PFreset_Nativo()); }
        [DllImport("PNPDLL.dll", EntryPoint = "PFendoso")]
        private static extern IntPtr PFendoso_Nativo(String campo1, String campo2, String campo3, String tipoendoso);
        public static string PFendoso(String campo1, String campo2, String campo3, String tipoendoso) { return TextoNativo(PFendoso_Nativo(campo1, campo2, campo3, tipoendoso)); }
        [DllImport("PNPDLL.dll", EntryPoint = "PFvalida675")]
        private static extern IntPtr PFvalida675_Nativo(String campo1, String campo2, String campo3, String campo4);
        public static string PFvalida675(String campo1, String campo2, String campo3, String campo4) { return TextoNativo(PFvalida675_Nativo(campo1, campo2, campo3, campo4)); }
        [DllImport("PNPDLL.dll", EntryPoint = "PFCheque2")]
        private static extern IntPtr PFCheque2_Nativo(String mon, String ben, String fec, String c1, String c2, String c3, String c4, String campo1, String campo2);
        public static string PFCheque2(String mon, String ben, String fec, String c1, String c2, String c3, String c4, String campo1, String campo2) { return TextoNativo(PFCheque2_Nativo(mon, ben, fec, c1, c2, c3, c4, campo1, campo2)); }
        [DllImport("PNPDLL.dll", EntryPoint = "PFcambiofecha")]
        private static extern IntPtr PFcambiofecha_Nativo(String edfecha, String edhora);
        public static string PFcambiofecha(String edfecha, String edhora) { return TextoNativo(PFcambiofecha_Nativo(edfecha, edhora)); }
        [DllImport("PNPDLL.dll", EntryPoint = "PFcambiatasa")]
        private static extern IntPtr PFcambiatasa_Nativo(String t1, String t2, String t3);
        public static string PFcambiatasa(String t1, String t2, String t3) { return TextoNativo(PFcambiatasa_Nativo(t1, t2, t3)); }
        [DllImport("PNPDLL.dll", EntryPoint = "PFBarra")]
        private static extern IntPtr PFBarra_Nativo(String edbarra);
        public static string PFBarra(String edbarra) { return TextoNativo(PFBarra_Nativo(edbarra)); }
        [DllImport("PNPDLL.dll", EntryPoint = "PFVoltea")]
        private static extern IntPtr PFVoltea_Nativo();
        public static string PFVoltea() { return TextoNativo(PFVoltea_Nativo()); }
        [DllImport("PNPDLL.dll", EntryPoint = "PFLeereloj")]
        private static extern IntPtr PFLeereloj_Nativo();
        public static string PFLeereloj() { return TextoNativo(PFLeereloj_Nativo()); }
        [DllImport("PNPDLL.dll", EntryPoint = "PFrepMemNF")]
        private static extern IntPtr PFrepMemNF_Nativo(String desf, String hasf, String modmem);
        public static string PFrepMemNF(String desf, String hasf, String modmem) { return TextoNativo(PFrepMemNF_Nativo(desf, hasf, modmem)); }
        [DllImport("PNPDLL.dll", EntryPoint = "PFRepMemoriaNumero")]
        private static extern IntPtr PFRepMemoriaNumero_Nativo(String desn, String hasn, String modmem);
        public static string PFRepMemoriaNumero(String desn, String hasn, String modmem) { return TextoNativo(PFRepMemoriaNumero_Nativo(desn, hasn, modmem)); }
        [DllImport("PNPDLL.dll", EntryPoint = "PFCambtipoContrib")]
        private static extern IntPtr PFCambtipoContrib_Nativo(String tip);
        public static string PFCambtipoContrib(String tip) { return TextoNativo(PFCambtipoContrib_Nativo(tip)); }
        [DllImport("PNPDLL.dll", EntryPoint = "PFultimo")]
        private static extern IntPtr PFultimo_Nativo();
        public static string PFultimo() { return TextoNativo(PFultimo_Nativo()); }
        [DllImport("PNPDLL.dll", EntryPoint = "PFTipoImp")]
        private static extern IntPtr PFTipoImp_Nativo(String edtexto);
        public static string PFTipoImp(String edtexto) { return TextoNativo(PFTipoImp_Nativo(edtexto)); }
        #endregion
        
        private string respuesta;
        private string respuesta2;
        public string fiscal;
        public string todo = null;

        public ImpresoraFiscalPNPDLL(){}

        private String inicializarPuertos()
        {
            //this.IpPuerto.CommPort = 1;
            return PFabrepuerto("1");
        }

        private String cerrar_puerto() {
            return PFcierrapuerto();
        }

        public void reporteX()
        {
            inicializarPuertos();
            PFrepx();
            cerrar_puerto();
        }

        public String statusN() {
            return PFestatus("N");
        }

        //por si lo requiere la impresora fiscal
        public void reporteZ()
        {
            inicializarPuertos();
            respuesta = null;
            //inicializarPuertos();
            respuesta = PFrepz();
            //puerto no abierto
            if (respuesta.Equals("NP"))
            {
                //1 por defecto
                respuesta = inicializarPuertos();
                if (respuesta.Equals("OK"))
                {
                    respuesta = PFrepz();
                }
            }
            cerrar_puerto();
        }

        public bool reporteZExterno()
        {
            reporteZ();
            return true;
        }

        public string estadoImpresora()
        {
            respuesta = null;
            string estado = null;
            inicializarPuertos();

            respuesta = statusN();
            if (respuesta.Equals("OK"))
            {
                String[] temp = ultimo().Split(',');
                estado = temp[3];

            }
            else {
                estado = "15";
            }

            cerrar_puerto();
            return estado;
        }

        //obtener el valor en string del ultimo comando
        public string ultimo()
        {
            return PFultimo();
        }

        //obtener serial
        public string serial() {
            return PFSerial();
        }

        public void imprimirFAV(Factura facturaImprimir, TextBox txtPag) {
            string cadenaItem = null;
            string cantidad = null;
            string precio = null;
            string codigo = null;
            string iva = null;
            string rif = null;
            string tipo = null;
            string vacio = null;
            short valor0 = 0;
            short valor01 = 0;
            string rayar = null;
            string direccionCliente = null;
            string telefono = null;
            string numeroInterno = null;
            decimal porDescuento = 0;
            string cadenaDescuentoItem = null;
            string descuentoGeneralFac = null;
            string pagado = null;
            string numeroImpresoraFiscal = null;

            respuesta = null;
            
            this.inicializarPuertos();
            PFTipoImp("300");
            codigo = facturaImprimir.ClienteFacturar.Nombre.Length >= 38 ? facturaImprimir.ClienteFacturar.Nombre.Substring(0, 37) : facturaImprimir.ClienteFacturar.Nombre;
            rif = facturaImprimir.ClienteFacturar.Rif;
            tipo = "F";
            vacio = "";
            rayar = "========================================";
            respuesta = PFabrefiscal(codigo,rif);
            
            if (respuesta.Equals("OK")) {
                foreach (DataGridViewRow fila in facturaImprimir.DgvItems.Rows) {
                    cadenaItem = null;
                    cantidad = null;
                    precio = null;
                    iva = null;

                    iva = Convert.ToString(fila.Cells["@mov_porciva"].Value).Replace(',','.');
                    iva = iva.Replace(".", "");
                    //iva = Convert.ToString(fila.Cells["@mov_porciva"].Value).Replace(",", "");
                    cantidad = Convert.ToString(fila.Cells["@mov_cant"].Value);
                    precio = Convert.ToString(fila.Cells["@mov_precio"].Value);
                    //precio = Convert.ToString(fila.Cells["@mov_precio"].Value).Replace(",", "");
                    //cadenaItem = (Convert.ToString(fila.Cells["@mov_codigo"].Value).Substring(5) + " " + Convert.ToString(fila.Cells["colProducto"].Value) + " " + Convert.ToString(fila.Cells["@mov_undmed"].Value));
                    cadenaItem = (Convert.ToString(fila.Cells["@mov_codigo"].Value).Substring(5));
                    if (Convert.ToString(fila.Cells["colProducto"].Value).Length >= 21)
                    {
                        cadenaItem = cadenaItem + " " + Convert.ToString(fila.Cells["colProducto"].Value).Substring(0, 20);
                    }
                    else
                    {
                        cadenaItem = cadenaItem + " " + Convert.ToString(fila.Cells["colProducto"].Value);
                    }
                    cadenaItem = cadenaItem + " " + Convert.ToString(fila.Cells["@mov_undmed"].Value);
                    porDescuento = porDescuento + Convert.ToDecimal(fila.Cells["@mov_desc"].Value.ToString().Replace(".", ","));
                    respuesta = null;
                    respuesta = PFrenglon(cadenaItem,cantidad, precio,iva);
                }
                //pagado = txtPag.Text.Replace(',','.');
                //PFCancelaDoc("", pagado);
                respuesta =PFparcial();


                respuesta = PFTfiscal(rayar);
                if (facturaImprimir.ClienteFacturar.Direccion.Length > 0)
                {
                    direccionCliente = "Direccion: " + facturaImprimir.ClienteFacturar.Direccion;
                    if (direccionCliente.Length >= 38)
                    {
                        direccionCliente = direccionCliente.Substring(0, 37);
                    }
                    else
                    {
                        direccionCliente = direccionCliente.Substring(0);
                    }
                    respuesta = PFTfiscal(direccionCliente);
                }
                if (facturaImprimir.ClienteFacturar.Telefono.Length > 0)
                {
                    telefono = "Tlf:" + facturaImprimir.ClienteFacturar.Telefono + " Vendedor:" + facturaImprimir.VendedorFactura.CodigoV;
                    //        public void imprimirFAV(Factura facturaImprimir, TextBox txtPag) y  public void imprimirDEV(Factura facturaImprimir, TextBox txtPag)           
                    //  telefono = "Rebaja IVA Dto.3085 GO.41239" 3%  le falta colocar el % de la rebaja, 12-9= 3 12-7=5
                    // telefono = "Rebaja IVA Dto.3085 GO.41239";
                    if (telefono.Length >= 38)
                    {
                        telefono = telefono.Substring(0, 37);
                    }
                    else
                    {
                        telefono = telefono.Substring(0);
                    }
                    respuesta = PFTfiscal(telefono);
                }
                numeroInterno = "Numero Interno: " + facturaImprimir.CorrelativoInterno;
                if (numeroInterno.Length >= 38)
                {
                    numeroInterno = numeroInterno.Substring(0, 37);
                }
                else
                {
                    numeroInterno = numeroInterno.Substring(0);
                }
                respuesta = PFTfiscal(numeroInterno);


                respuesta = PFtotal();
                respuesta = statusN();
                String[] temp2 = ultimo().Split(',');
                facturaImprimir.NumeroFiscal = temp2[9];
                respuesta = serial();
                String[] temp3 = ultimo().Split(',');
                facturaImprimir.SerieImpresora = temp3[2];
                //PFtotal();
                //se guarda el numero fical de una vez
                facturaImprimir.guardarNumeroFiscal();
                
                cerrar_puerto();
            }
        }

        public void imprimirDEV(Factura facturaImprimir, TextBox txtPag)
        {
            string cadenaItem = null;
            string cantidad = null;
            string precio = null;
            string codigo = null;
            string iva = null;
            string rif = null;
            string tipo = null;
            string vacio = null;
            short valor0 = 0;
            short valor01 = 0;
            string rayar = null;
            string direccionCliente = null;
            string telefono = null;
            string numeroInterno = null;
            decimal porDescuento = 0;
            string cadenaDescuentoItem = null;
            string descuentoGeneralFac = null;
            string pagado = null;
            string numeroImpresoraFiscal = null;
            string numeroFiscalAfec = null;
            string serialAfec = null;
            string fechaAfec = null;
            string horaAfec = null;

            this.inicializarPuertos();
            PFTipoImp("300");
            codigo = facturaImprimir.ClienteFacturar.Nombre.Length >= 38 ? facturaImprimir.ClienteFacturar.Nombre.Substring(0, 37) : facturaImprimir.ClienteFacturar.Nombre;
            rif = facturaImprimir.ClienteFacturar.Rif;
            if (facturaImprimir.NumeroFiscalAfectado != null)
            {
                numeroFiscalAfec = facturaImprimir.NumeroFiscalAfectado;
            }
            if (facturaImprimir.EocAfectado != null)
            {
                serialAfec = facturaImprimir.EocAfectado;
            }
            if (facturaImprimir.FechaAfectada != null)
            {
                fechaAfec = facturaImprimir.FechaAfectada.Replace("-", "/");
            }
            if (facturaImprimir.HoraAfectada != null)
            {
                horaAfec = facturaImprimir.HoraAfectada.Substring(0, 5);
            }
            tipo = "D";
            vacio = "";
            rayar = "========================================";


            respuesta = PFDevolucion(codigo, rif, numeroFiscalAfec, serialAfec, fechaAfec, horaAfec);

            //respuesta=this.serial();

            //if (respuesta.Equals("OK"))
            //{
            //    facturaImprimir.SerieImpresora = this.Fiscal.Resp_3;
            //    numeroImpresoraFiscal = facturaImprimir.SerieImpresora;
            //}

            //numeroImpresoraFiscal = this.Fiscal.Resp_3;

            if (respuesta.Equals("OK"))
            {
                foreach (DataGridViewRow fila in facturaImprimir.DgvItems.Rows)
                {
                    cadenaItem = null;
                    cantidad = null;
                    precio = null;
                    iva = null;

                    iva = Convert.ToString(fila.Cells["@mov_porciva"].Value).Replace(".", ",");
                    cantidad = Convert.ToString(fila.Cells["@mov_cant"].Value);
                    precio = Convert.ToString(fila.Cells["@mov_precio"].Value).Replace(".", ",");
                    cadenaItem = (Convert.ToString(fila.Cells["@mov_codigo"].Value).Substring(5));
                    if (Convert.ToString(fila.Cells["colProducto"].Value).Length >= 21)
                    {
                        cadenaItem = cadenaItem + " " + Convert.ToString(fila.Cells["colProducto"].Value).Substring(0, 20);
                    }
                    else
                    {
                        cadenaItem = cadenaItem + " " + Convert.ToString(fila.Cells["colProducto"].Value);
                    }
                    cadenaItem = cadenaItem + " " + Convert.ToString(fila.Cells["@mov_undmed"].Value);
                    porDescuento = porDescuento + Convert.ToDecimal(fila.Cells["@mov_desc"].Value.ToString().Replace(".", ","));

                    respuesta = PFrenglon(cadenaItem, cantidad, precio, iva);
                }


                respuesta = PFparcial();
                respuesta = PFTfiscal(rayar);

                //descuentoGeneralFac = Convert.ToString((1 - (0.000001) / 100));
                //this.Fiscal.DescuentoCF(ref descuentoGeneralFac);
                pagado = txtPag.Text;


                if (facturaImprimir.ClienteFacturar.Direccion.Length > 0)
                {
                    direccionCliente = "Direccion: " + facturaImprimir.ClienteFacturar.Direccion;
                    if (direccionCliente.Length >= 37)
                    {
                        direccionCliente = direccionCliente.Substring(0, 37);
                    }
                    else
                    {
                        direccionCliente = direccionCliente.Substring(0);
                    }
                    respuesta = PFTfiscal(direccionCliente);
                }

                if (facturaImprimir.ClienteFacturar.Telefono.Length > 0)
                {
                    telefono = "Tlf:" + facturaImprimir.ClienteFacturar.Telefono + " Vendedor:" + facturaImprimir.VendedorFactura.CodigoV;
                    if (telefono.Length >= 38)
                    {
                        telefono = telefono.Substring(0, 37);
                    }
                    else
                    {
                        telefono = telefono.Substring(0);
                    }
                    respuesta = PFTfiscal(telefono);
                }

                numeroInterno = "Numero Interno: " + facturaImprimir.CorrelativoInterno;
                if (numeroInterno.Length >= 38)
                {
                    numeroInterno = numeroInterno.Substring(0, 37);
                }
                else
                {
                    numeroInterno = numeroInterno.Substring(0);
                }

                respuesta = PFTfiscal(numeroInterno);

                respuesta = PFtotal();
                respuesta = statusN();
                String[] temp2 = ultimo().Split(',');
                facturaImprimir.NumeroFiscal = temp2[9];
                respuesta = serial();
                String[] temp3 = ultimo().Split(',');
                facturaImprimir.SerieImpresora = temp3[2];
                //PFtotal();
                //se guarda el numero fical de una vez
                facturaImprimir.guardarNumeroFiscal();

                cerrar_puerto();
            }

        }




    }
}



