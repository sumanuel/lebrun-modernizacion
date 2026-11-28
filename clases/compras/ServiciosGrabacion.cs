using System;
using System.Configuration;
using System.Data;
using System.Globalization;
using MySql.Data.MySqlClient;

namespace lebrun.clases.compras
{
    // Escrituras de Otras Cuentas x Pagar. Todas reciben la conexion y la transaccion de quien llama (ConexionBD.ejecutarTransaccion):
    // el documento se graba completo (documento, saldo, libro de compras y contabilidad) o no se graba nada. Los datos van como parametros.
    partial class Servicios
    {
        private static MySqlCommand Comando(MySqlConnection conexion, MySqlTransaction transaccion, string sql, object[] pares)
        {
            MySqlCommand comando = new MySqlCommand(sql, conexion, transaccion);

            for (int i = 0; i < pares.Length; i += 2)
            {
                comando.Parameters.AddWithValue((string)pares[i], pares[i + 1] ?? "");
            }

            return comando;
        }

        private static int Ejecutar(MySqlConnection conexion, MySqlTransaction transaccion, string sql, params object[] pares)
        {
            using (MySqlCommand comando = Comando(conexion, transaccion, sql, pares))
            {
                return comando.ExecuteNonQuery();
            }
        }

        private static object Escalar(MySqlConnection conexion, MySqlTransaction transaccion, string sql, params object[] pares)
        {
            using (MySqlCommand comando = Comando(conexion, transaccion, sql, pares))
            {
                return comando.ExecuteScalar();
            }
        }

        // Base contable de la compañia ("01" usa la base sin sufijo). Permite escribir en ella dentro de la misma transaccion.
        public static string BaseContable(string compania)
        {
            string baseDatos = ConfigurationManager.AppSettings.Get("db3");
            return compania == "01" ? baseDatos : baseDatos + "_" + compania;
        }

        private int DiasDePlazo()
        {
            decimal dias;
            return decimal.TryParse(DiasCondicionPago, out dias) ? Convert.ToInt32(dias) : 0;
        }

        private object[] DatosDocumento(params object[] adicionales)
        {
            object[] datos = new object[]
            {
                "@idPro", IdProveedor, "@docum", NroDocum, "@sufi", SufiDocum, "@ctrl", NroControlSeniat, "@fiscal", NroFiscal,
                "@tip", IdTipoDoc, "@afec", NroDocumAfec, "@sAfec", SufiDocumAfec, "@descrip", Descripcion,
                "@emision", FechaEmision, "@vencimiento", FechaVenc, "@libro", FechaLibCompras, "@retenc", FechaRetenc,
                "@recep", FechaRecepcion, "@cargaFecha", FechaCarga, "@base", MontoBase, "@monto", MontoDocum,
                "@ivGn", Doc_iva_gn, "@ivaRd", Doc_iva_rd, "@gn", Doc_base_gn, "@rd", Doc_base_rd, "@ex", Doc_base_ex,
                "@porcIva", PorcIva, "@porcIva2", PorcIva2, "@monIv", MontoIva, "@retenido", MontoRetenc,
                "@porcRetenc", PorcRetenc, "@nroRetenc", NroRetenc, "@login", Login, "@loginUpd", LoginUpd, "@origen", Origen,
                "@correlativo", Correlativo, "@condicion", Condicion, "@dias", DiasDePlazo(), "@cxp", Tdc_cxp,
                "@imponible", BaseImponible, "@recibidor", Recibidor, "@sst", Sstatus, "@tra", TipTra,
                "@hora", DateTime.Now.ToString("HH:mm:ss", CultureInfo.InvariantCulture)
            };

            object[] todos = new object[datos.Length + adicionales.Length];
            datos.CopyTo(todos, 0);
            adicionales.CopyTo(todos, datos.Length);
            return todos;
        }

        public void ingresarLibroCompras(MySqlConnection conexion, MySqlTransaction transaccion)
        {
            Ejecutar(conexion, transaccion,
                "INSERT INTO admlibrocompras (IdProveedor, NroDocum, SufiDocum, NroControlSeniat, NroFiscal, " +
                "IdTipoDoc, NroDocumAfec, Descripcion, FechaEmision, FechaVenc, FechaLibCompras, FechaRetenc, MontoBase, " +
                "MontoDocum, doc_iva_gn, doc_iva_rd, doc_base_gn, doc_base_rd, doc_base_ex, PorcIva, PorcIva2, " +
                "MontoIva, MontoRetenc, PorcRetenc, NroRetenc, periodoFiscal, IdMoneda, MontoEnDivisa, " +
                "Aduana, StatusLibro, StatusRetenc, LoteEmision, FechaCarga, Login, LoginUpd, origen, correlativo, FechaRetencEnt) " +
                "VALUES (@idPro, @docum, @sufi, @ctrl, @fiscal, @tip, @afec, @descrip, @emision, @vencimiento, @libro, @retenc, @base, " +
                "@monto, @ivGn, @ivaRd, @gn, @rd, @ex, @porcIva, @porcIva2, @monIv, @retenido, @porcRetenc, @nroRetenc, '', 'Bs', '0.00', " +
                "'0', '0', '0', '0', @cargaFecha, @login, @loginUpd, @origen, @correlativo, @cargaFecha)",
                DatosDocumento());
        }

        public void ingresarSalpro(MySqlConnection conexion, MySqlTransaction transaccion)
        {
            decimal monto = decimal.Parse(MontoDocum, CultureInfo.InvariantCulture);
            decimal iva = decimal.Parse(MontoIva, CultureInfo.InvariantCulture);

            // las notas de credito y anticipos (correlativo 43) rebajan la deuda: se guardan en negativo, como en ModelSoft
            if (Correlativo == "43")
            {
                monto = -Math.Abs(monto);
                iva = -Math.Abs(iva);
            }

            Ejecutar(conexion, transaccion,
                "INSERT INTO admsalpro (IdProveedor, NroDocum, SufiDocum, NroDocumAfect, NroControlSeniat, Descripcion, " +
                "NroComprobante, FechaEmision, FechaVencimiento, FechaCarga, FechaLibCompras, FechaPago, NroCompPago, IdTipoDoc, " +
                "IdBanco, IdMoneda, MontoEnDivisa, MontoDocum, SaldoDocum, MontoIva, PorcIva, PorcIva2, MontoRetenc, NroCompRetenc, " +
                "Status, Login, Loginact, correlativo) " +
                "VALUES (@idPro, @docum, @sufi, @afec, @ctrl, @descrip, '', @emision, @vencimiento, @cargaFecha, @libro, @cargaFecha, '', @tip, " +
                "'', 'Bs', '0.00', @saldo, @saldo, @ivaSaldo, @porcIva, @porcIva2, @retenido, '', '0', @login, @loginUpd, @correlativo)",
                DatosDocumento("@saldo", monto, "@ivaSaldo", iva));
        }

        // opcion 1: documento definitivo; opcion 2: documento en carga (temporal)
        public void ingresarDocPro(MySqlConnection conexion, MySqlTransaction transaccion, int opcion)
        {
            string destino = opcion == 1 ? "admdocpro" : "admdocpro_temp";

            Ejecutar(conexion, transaccion,
                "INSERT INTO " + destino + " " +
                "(doc_correlativo, doc_numerodoc, doc_sufijo, doc_sufiOrden, doc_numfis, doc_tipdoc, doc_facafe, doc_sufijofacafe, " +
                "doc_numcontrol, doc_ordencompra, doc_docasociado, doc_codigo, doc_concep, doc_cencost, doc_sucus, doc_moneda, " +
                "doc_codtra, doc_condic, doc_usuario, doc_tiptra, doc_origen, doc_estadodoc, doc_status, doc_expimp, doc_fechCarg, " +
                "doc_fechEmis, doc_fechRecep, doc_fechven, doc_fecReten, doc_fechLCompra, doc_hora, doc_cbtnum, doc_recibidopor, " +
                "doc_docorg, doc_cbtpre, doc_pagnum, doc_plaimp, doc_aprob1, doc_aprob2, doc_aprob3, doc_anufis, doc_crepago, " +
                "doc_cxp, doc_plazo, doc_basnet, doc_dcto, doc_otrimp, doc_mtoiva, doc_neto, doc_otros, doc_recargo, doc_valcamb, " +
                "doc_dctobs, doc_totaldivisa, doc_prod, doc_subtot, doc_ditem, doc_descdoc, doc_descdoc2, doc_descdoc3, doc_subbase, " +
                "doc_imponible, doc_iva_gn, doc_iva_rd, doc_base_gn, doc_base_rd, doc_base_ex, doc_existe) " +
                "VALUES ('', @docum, @sufi, '', @fiscal, @tip, @afec, @sAfec, " +
                "@ctrl, '', '', @idPro, @descrip, '0000000001', '000001', 'Bs', " +
                "'', @condicion, @login, @tra, 'Nacional', 'Activo', @sst, '', @cargaFecha, " +
                "@emision, @recep, @vencimiento, @retenc, @libro, @hora, 'SERV', @recibidor, " +
                "'', '', '', '', '', '', '', '', '', " +
                "@cxp, @dias, @base, '0.00', '0.00', @monIv, @monto, '0.00', '0.00', '1', " +
                "'0.00', @monto, '0.00', @base, '0.00', '0.00', '0.00', '0.00', @base, " +
                "@imponible, @ivGn, @ivaRd, @gn, @rd, @ex, '')",
                DatosDocumento());
        }

        // Reescribe un documento en carga. Los parametros "origen" identifican la fila tal como se abrio.
        public void actualizarDocProTemporal(MySqlConnection conexion, MySqlTransaction transaccion,
            string proveedorOrigen, string documentoOrigen, string sufijoOrigen, string tipoOrigen)
        {
            int filas = Ejecutar(conexion, transaccion,
                "UPDATE admdocpro_temp SET doc_correlativo='', doc_numerodoc=@docum, doc_numcontrol=@ctrl, doc_sufijo=@sufi, " +
                "doc_sufiOrden='', doc_numfis=@fiscal, doc_tipdoc=@tip, doc_facafe=@afec, doc_sufijofacafe=@sAfec, doc_ordencompra='', " +
                "doc_docasociado='', doc_codigo=@idPro, doc_cencost='0000000001', doc_concep=@descrip, doc_sucus='000001', doc_moneda='Bs', " +
                "doc_codtra='', doc_condic=@condicion, doc_usuario=@login, doc_tiptra=@tra, doc_origen='Nacional', doc_estadodoc='Activo', " +
                "doc_status=@sst, doc_expimp='', doc_fechCarg=@cargaFecha, doc_fechEmis=@emision, doc_fechRecep=@recep, " +
                "doc_fechven=@vencimiento, doc_fecReten=@retenc, doc_fechLCompra=@libro, doc_hora=@hora, doc_cbtnum='SERV', " +
                "doc_recibidopor=@recibidor, doc_docorg='', doc_cbtpre='', doc_pagnum='', doc_plaimp='', doc_aprob1='', doc_aprob2='', " +
                "doc_aprob3='', doc_anufis='', doc_crepago='', doc_basnet=@base, doc_cxp=@cxp, doc_dcto='0.00', doc_otrimp=0, " +
                "doc_descdoc2='0.00', doc_descdoc3='0.00', doc_mtoiva=@monIv, doc_neto=@monto, doc_otros='0.00', doc_plazo=@dias, " +
                "doc_recargo=0, doc_valcamb='1', doc_dctobs=0, doc_totaldivisa=@monto, doc_prod='0', doc_subtot=@base, doc_ditem='0', " +
                "doc_descdoc='0.00', doc_subbase=@base, doc_imponible=@imponible, doc_iva_gn=@ivGn, doc_iva_rd=@ivaRd, " +
                "doc_base_gn=@gn, doc_base_rd=@rd, doc_base_ex=@ex " +
                "WHERE doc_codigo=@oPro AND doc_numerodoc=@oDoc AND doc_sufijo=@oSuf AND doc_tipdoc=@oTip",
                DatosDocumento("@oPro", proveedorOrigen, "@oDoc", documentoOrigen, "@oSuf", sufijoOrigen, "@oTip", tipoOrigen));

            if (filas == 0)
            {
                throw new ApplicationException("El documento en carga ya no existe. Cierre la ventana y vuelva a buscarlo en la lista.");
            }
        }

        public void actualizarUltimaCompra(MySqlConnection conexion, MySqlTransaction transaccion)
        {
            Ejecutar(conexion, transaccion,
                "UPDATE admproveedor2 SET fec_ult_compra=@hoy, ult_refer_compra=@docum, ult_cant_compra='0.00' WHERE pro2_codigo=@idPro",
                "@hoy", DateTime.Today, "@docum", NroDocum, "@idPro", IdProveedor);
        }

        // El numero se aumenta antes de leerlo: la fila queda tomada hasta terminar, asi dos equipos no reciben el mismo comprobante.
        public string siguienteComprobante(MySqlConnection conexion, MySqlTransaction transaccion, string baseContable)
        {
            if (Ejecutar(conexion, transaccion,
                "UPDATE " + baseContable + ".admparametroscontables SET pc_CorrCompAut=pc_CorrCompAut+1 WHERE pc_idSistema='04'") == 0)
            {
                throw new ApplicationException("No está definido el contador de comprobantes contables (sistema 04).");
            }

            return Convert.ToString(Escalar(conexion, transaccion,
                "SELECT pc_CorrCompAut FROM " + baseContable + ".admparametroscontables WHERE pc_idSistema='04' LIMIT 1"));
        }

        // Inserta una fila por cada fila de la tabla; el parametro de cada dato lleva el nombre de su columna.
        private static void InsertarFilas(MySqlConnection conexion, MySqlTransaction transaccion, DataTable filas, string sql, params object[] fijos)
        {
            foreach (DataRow fila in filas.Rows)
            {
                using (MySqlCommand comando = Comando(conexion, transaccion, sql, fijos))
                {
                    foreach (DataColumn columna in filas.Columns)
                    {
                        comando.Parameters.AddWithValue("@" + columna.ColumnName, fila[columna]);
                    }

                    comando.ExecuteNonQuery();
                }
            }
        }

        // definitivo: admmovcontab; en carga: admmovcontab_temp. Las columnas son las de movimientoContable.armarDataTableMovContab()
        public void insertarMovimientos(MySqlConnection conexion, MySqlTransaction transaccion, DataTable lineas, bool temporal)
        {
            InsertarFilas(conexion, transaccion, lineas,
                "INSERT INTO " + (temporal ? "admmovcontab_temp" : "admmovcontab") + " (movcon_item, movcon_proveedor, movcon_numdoc, " +
                "movcon_sufdoc, movcon_tipdoc, movcon_numcomp, movcon_descrip, movcon_cuenta, movcon_tipo, movcon_basetip, movcon_fecaha, " +
                "movcon_hora, movcon_monto, movcon_status, movcon_login, idSistemas, movcon_auxiliar, movcon_rif, movcon_nombre, codigoIslr, referencia) " +
                "VALUES (@item, @codprove, @numeroDoc, @sufiDoc, @tipoDoc, @numComprobante, @descripcion, @cuenta, @tipo, @baseTipo, @fecha, " +
                "@hora, @monto, '1', @login, @idSistemas, @auxiliar, @rif, @nombre, @islr, @referencia)");
        }

        // columnas de movimientoContable.armarDataTableCabeceraMovContab()
        public void insertarCabeceraContable(MySqlConnection conexion, MySqlTransaction transaccion, string baseContable, DataTable cabecera)
        {
            InsertarFilas(conexion, transaccion, cabecera,
                "INSERT INTO " + baseContable + ".admmovimientoscontablesc (mc_nroComprobante, mc_FechaComprobante, mc_FechaCarga, " +
                "mc_FechaCierre, mc_IdSistema, mc_Descripcion, mc_MontoDebitos, mc_MontoCreditos, mc_Status, mc_Login, mc_LoginUpd, " +
                "mc_FechaUltMod, mc_Compania, mc_HoraGuardado, IdSistemas) VALUES (@comprobante, @fecSistema, @fecSistema, @fecSistema, " +
                "@mcIdSistema, @descripcion, @Debito, @Credito, '2', @login, @updateLogin, @fepago, @compania, @hora, @idSistema)");
        }

        public void insertarDetalleContable(MySqlConnection conexion, MySqlTransaction transaccion, string baseContable, string compania, DataTable lineas)
        {
            InsertarFilas(conexion, transaccion, lineas,
                "INSERT INTO " + baseContable + ".admmovimientoscontabled (mcd_NroComprobante, mcd_Item, mcd_FechaCarga, mcd_CodigoMayor, " +
                "mcd_CodigoAux, mcd_NroDocum, mcd_Referencia, mcd_Descripcion, mcd_status, mcd_login, mcd_UltLogin, mdc_FechaUltMod, " +
                "mdc_Compania, mdc_TipoTransaccion, mdc_monto, movcon_proveedor, movcon_numdoc, movcon_sufdoc, movcon_tipdoc, codigoIslr, " +
                "mdc_FechaComprobante) VALUES (@numComprobante, @item, @fecha, @cuenta, @auxiliar, @numeroDoc, @referencia, @descripcion, " +
                "'2', @login, @login, @fecha, @companiaContable, @tipoLetra, @monto, @codprove, @interno, @sufiDoc, @tipoDoc, @islr, @fecha)",
                "@companiaContable", compania);
        }

        public void eliminarMovimientosTemporales(MySqlConnection conexion, MySqlTransaction transaccion,
            string proveedor, string documento, string sufijo, string tipo)
        {
            Ejecutar(conexion, transaccion,
                "DELETE FROM admmovcontab_temp WHERE movcon_proveedor=@pro AND movcon_numdoc=@doc AND movcon_sufdoc=@suf AND movcon_tipdoc=@tip",
                "@pro", proveedor, "@doc", documento, "@suf", sufijo, "@tip", tipo);
        }

        // borra el documento en carga: sus lineas contables y su cabecera
        public void eliminarTemporal(MySqlConnection conexion, MySqlTransaction transaccion,
            string proveedor, string documento, string sufijo, string tipo)
        {
            eliminarMovimientosTemporales(conexion, transaccion, proveedor, documento, sufijo, tipo);

            Ejecutar(conexion, transaccion,
                "DELETE FROM admdocpro_temp WHERE doc_codigo=@pro AND doc_numerodoc=@doc AND doc_sufijo=@suf AND doc_tipdoc=@tip",
                "@pro", proveedor, "@doc", documento, "@suf", sufijo, "@tip", tipo);
        }

        // Cuenta contable del proveedor para un tipo de documento que no la tiene (regla de AutoNumberAccount en ModelSoft):
        // si la cuenta del tipo termina en 00000 se crea la siguiente cuenta mayor de ese grupo; si no, un auxiliar de esa cuenta.
        // Si el proveedor ya tiene cuenta del mismo grupo en otro tipo, se usa esa en lugar de crear otra.
        public void asignarCuentaProveedor(MySqlConnection conexion, MySqlTransaction transaccion, string proveedor, string nombre,
            string tipo, string cuentaTipo, string baseContable, string usuario)
        {
            if (cuentaTipo == null || cuentaTipo.Length != 9)
            {
                throw new ApplicationException("El tipo de documento " + tipo + " no tiene definida su cuenta contable.");
            }

            bool cuentaMayor = cuentaTipo.Substring(4) == "00000";
            string grupo = cuentaTipo.Substring(0, 4);
            string cuenta = "";
            string auxiliar = "";

            using (MySqlCommand comando = Comando(conexion, transaccion,
                "SELECT a.n_cuenta, a.n_auxiliar FROM admtipdocpro2 a INNER JOIN admtipdocpro t ON t.tdc_codigo=a.n_tipo " +
                "WHERE a.n_proveedor=@pro AND t.tdc_codcta=@codcta AND a.n_cuenta LIKE @patron LIMIT 1",
                new object[] { "@pro", proveedor, "@codcta", cuentaTipo, "@patron", (cuentaMayor ? grupo : cuentaTipo) + "%" }))
            using (MySqlDataReader lector = comando.ExecuteReader())
            {
                if (lector.Read())
                {
                    cuenta = Convert.ToString(lector[0]);
                    auxiliar = Convert.ToString(lector[1]);
                }
            }

            if (nombre.Length > 100)
            {
                nombre = nombre.Substring(0, 100);
            }

            if (cuenta == "" && cuentaMayor)
            {
                string ultima = Convert.ToString(Escalar(conexion, transaccion,
                    "SELECT cc_CodigoCuenta FROM " + baseContable + ".admcuentascontables WHERE cc_CodigoCuenta LIKE @patron " +
                    "ORDER BY cc_CodigoCuenta DESC LIMIT 1 FOR UPDATE", "@patron", grupo + "%"));
                long numero;

                if (!long.TryParse(ultima, out numero))
                {
                    numero = long.Parse(cuentaTipo, CultureInfo.InvariantCulture);
                }

                cuenta = (numero + 1).ToString(CultureInfo.InvariantCulture);

                if (cuenta.Length != 9 || !cuenta.StartsWith(grupo))
                {
                    throw new ApplicationException("No quedan números de cuenta disponibles en el grupo " + grupo + ".");
                }

                Ejecutar(conexion, transaccion,
                    "INSERT INTO " + baseContable + ".admcuentascontables (cc_CodigoCuenta, cc_Descripcion, cc_NivelTotal, cc_FlagAux, " +
                    "cc_FlagImpBal, cc_FlagImpDia, cc_SaldoIniEjerc, cc_Siglas, cc_Compania, cc_Fecha, cc_User, cc_ManejaAuxiliar, cc_control) " +
                    "VALUES (@cuenta, @nombre, '5', '0', '1', '1', '0.00', 'CC', '0', @hoy, @usuario, '0', '0')",
                    "@cuenta", cuenta, "@nombre", nombre, "@hoy", DateTime.Today, "@usuario", usuario);
            }
            else if (cuenta == "")
            {
                string ultimo = Convert.ToString(Escalar(conexion, transaccion,
                    "SELECT admAC_codigoAuxiliarContable FROM " + baseContable + ".admauxiliarcontable WHERE admAC_codigoCuenta=@cuenta " +
                    "ORDER BY admAC_codigoAuxiliarContable DESC LIMIT 1 FOR UPDATE", "@cuenta", cuentaTipo));
                int numero;

                int.TryParse(ultimo, out numero);
                cuenta = cuentaTipo;
                auxiliar = (numero + 1).ToString("00000", CultureInfo.InvariantCulture);

                Ejecutar(conexion, transaccion,
                    "INSERT INTO " + baseContable + ".admauxiliarcontable (admAC_codigoCuenta, admAC_descripcion, admAC_manejaDocumentos, " +
                    "admAC_codigoAuxiliarContable, admAC_fechaCreacion, admAC_usuario, admAC_saldoApertura, admAC_control) " +
                    "VALUES (@cuenta, @nombre, '1', @auxiliar, @hoy, @usuario, '0.00', '0')",
                    "@cuenta", cuenta, "@nombre", nombre, "@auxiliar", auxiliar, "@hoy", DateTime.Today, "@usuario", usuario);
            }

            // las compras (37) comparten la cuenta con las notas de debito (40) y de credito (43)
            string[] tipos = tipo == "37" ? new string[] { "37", "40", "43" } : new string[] { tipo };

            foreach (string cadaTipo in tipos)
            {
                object[] datos = new object[] { "@tipo", cadaTipo, "@pro", proveedor, "@cuenta", cuenta, "@auxiliar", auxiliar,
                    "@hoy", DateTime.Today, "@usuario", usuario };

                if (Convert.ToInt32(Escalar(conexion, transaccion,
                    "SELECT COUNT(*) FROM admtipdocpro2 WHERE n_tipo=@tipo AND n_proveedor=@pro", datos)) == 0)
                {
                    Ejecutar(conexion, transaccion,
                        "INSERT INTO admtipdocpro2 (n_tipo, n_proveedor, n_cuenta, n_auxiliar, n_fecha, n_user) " +
                        "VALUES (@tipo, @pro, @cuenta, @auxiliar, @hoy, @usuario)", datos);
                }
                else
                {
                    Ejecutar(conexion, transaccion,
                        "UPDATE admtipdocpro2 SET n_cuenta=@cuenta, n_auxiliar=@auxiliar WHERE n_tipo=@tipo AND n_proveedor=@pro AND n_cuenta=''", datos);
                }
            }
        }
    }
}
