using System;
using System.Collections.Generic;
using System.Data;
using lebrun.clasesData;

namespace lebrun.clases.gerencial
{
    // Consultas de clientes y proveedores del modulo gerencial: busqueda, ficha, documentos pendientes e historial.
    // Sustituyen a LBXCLIENTES_LEBRUN / FRMEDOCTACLIENTES y LBXPROVEEDORES_LEBRUN / FRMEDOCTAPROVEEDOR del sistema anterior.
    public class ConsultasTerceros
    {
        public const int LimiteFilas = 500;

        private readonly ConexionBD baseDatos = new ConexionBD();

        private DataTable Ejecutar(string sql)
        {
            DataTable tabla = baseDatos.fDataTable(sql);
            baseDatos.cerrarConexion();
            return tabla;
        }

        private static string Texto(string valor)
        {
            return (valor ?? "").Trim().Replace("\\", "\\\\").Replace("'", "''");
        }

        private static string FechaOVacia(string columna, string alias)
        {
            return "IF(" + columna + " > '1900-01-01', " + columna + ", NULL) AS " + alias;
        }

        // Une las condiciones de busqueda que el usuario lleno (todas deben cumplirse).
        private static string Condiciones(string columnaCodigo, string codigo, string columnaNombre, string nombre, string columnaRif, string rif)
        {
            List<string> partes = new List<string>();
            if (!string.IsNullOrWhiteSpace(codigo))
            {
                partes.Add(columnaCodigo + " LIKE '%" + Texto(codigo) + "%'");
            }
            if (!string.IsNullOrWhiteSpace(nombre))
            {
                partes.Add(columnaNombre + " LIKE '%" + Texto(nombre) + "%'");
            }
            if (!string.IsNullOrWhiteSpace(rif))
            {
                partes.Add(columnaRif + " LIKE '%" + Texto(rif) + "%'");
            }
            return string.Join(" AND ", partes.ToArray());
        }

        // ---------------- clientes ----------------

        public DataTable BuscarClientes(string codigo, string nombre, string rif)
        {
            return Ejecutar("SELECT cli_codigo AS Codigo, cli_nombre AS Nombre, cli_rif AS Rif FROM admclientes WHERE " +
                            Condiciones("cli_codigo", codigo, "cli_nombre", nombre, "cli_rif", rif) +
                            " ORDER BY cli_nombre LIMIT " + (LimiteFilas + 1));
        }

        public DataTable FichaCliente(string codigo)
        {
            return Ejecutar("SELECT c.cli_codigo, c.cli_nombre, c.cli_rif, c.cli_direc1, c.cli_direc2, c.cli_codarea, c.cli_telefono, c.cli_celular, " +
                            "c.cli_correo, c.cli_situacion, t.tra_nombre AS vendedor, c.cli_condipag, c.cli_lincredito, " +
                            FechaOVacia("c.cli_fechareg", "fechareg") + " " +
                            "FROM admclientes c LEFT JOIN admtrabajador t ON t.tra_codigo = c.cli_vendedor WHERE c.cli_codigo = '" + Texto(codigo) + "'");
        }

        // Documentos con saldo pendiente del cliente.
        public DataTable PendientesCliente(string codigo)
        {
            return Ejecutar("SELECT " + FechaOVacia("FechaEmision", "Emision") + ", " + FechaOVacia("FechaVenc", "Vencimiento") + ", " +
                            "TipoDoc AS Tipo, NroDocum AS Documento, dcli_numfis AS NumeroFiscal, MontoTotal AS Monto, sal_actual AS Saldo " +
                            "FROM admsalcli WHERE cli_codigo = '" + Texto(codigo) + "' AND Status = 0 ORDER BY FechaVenc, NroDocum");
        }

        // Documentos emitidos al cliente, del mas reciente al mas antiguo.
        public DataTable HistorialCliente(string codigo)
        {
            return Ejecutar("SELECT " + FechaOVacia("dcli_fecha", "Fecha") + ", dcli_tipdoc AS Tipo, dcli_numero AS Documento, dcli_neto AS Monto, " +
                            "dcli_facafe AS FacturaAplica, dcli_numfis AS NumeroFiscal " +
                            "FROM admdoccli WHERE dcli_codigo = '" + Texto(codigo) + "' ORDER BY dcli_fecha DESC, dcli_numero DESC LIMIT " + (LimiteFilas * 4 + 1));
        }

        // ---------------- proveedores ----------------

        public DataTable BuscarProveedores(string codigo, string nombre, string rif)
        {
            return Ejecutar("SELECT pro_codigo AS Codigo, pro_nombre AS Nombre, pro_rif AS Rif FROM admproveedor WHERE " +
                            Condiciones("pro_codigo", codigo, "pro_nombre", nombre, "pro_rif", rif) +
                            " ORDER BY pro_nombre LIMIT " + (LimiteFilas + 1));
        }

        public DataTable FichaProveedor(string codigo)
        {
            return Ejecutar("SELECT p.pro_codigo, p.pro_nombre, p.pro_rif, p.pro_direcc1, p.pro_direcc2, p.pro_codarea, p.pro_telefono1, p.pro_telefono2, " +
                            "p.pro_cel1, p.pro_correo, p.pro_situacion, pa.pais_descri, p.pro_condipago, p.pro_limcre, p.pro_diaspla, p.pro_divisa " +
                            "FROM admproveedor p LEFT JOIN admpais pa ON pa.pais_codigo = p.pro_pais WHERE p.pro_codigo = '" + Texto(codigo) + "'");
        }

        // Documentos por pagar con saldo pendiente.
        public DataTable PendientesProveedor(string codigo)
        {
            return Ejecutar("SELECT " + FechaOVacia("s.FechaEmision", "Emision") + ", " + FechaOVacia("s.FechaVencimiento", "Vencimiento") + ", " +
                            "t.tdc_tipo AS Tipo, s.NroDocum AS Documento, s.SufiDocum AS Sufijo, s.Descripcion AS Descripcion, " +
                            "s.MontoDocum AS Monto, s.SaldoDocum AS Saldo " +
                            "FROM admsalpro s LEFT JOIN admtipdocpro t ON t.tdc_codigo = s.IdTipoDoc " +
                            "WHERE s.IdProveedor = '" + Texto(codigo) + "' AND s.Status = 0 ORDER BY s.FechaVencimiento, s.NroDocum, s.SufiDocum");
        }

        // Registros de documentos y pagos del proveedor, del mas reciente al mas antiguo. Operacion: 1 = registro, 2 = pago.
        public DataTable HistorialProveedor(string codigo)
        {
            string c = Texto(codigo);
            return Ejecutar("SELECT " + FechaOVacia("d.doc_fechEmis", "Fecha") + ", 1 AS Operacion, t.tdc_tipo AS Tipo, d.doc_numerodoc AS Documento, " +
                            "d.doc_concep AS Descripcion, d.doc_neto AS Monto " +
                            "FROM admdocpro d LEFT JOIN admtipdocpro t ON t.tdc_codigo = d.doc_tipdoc WHERE d.doc_codigo = '" + c + "' " +
                            "UNION ALL " +
                            "SELECT " + FechaOVacia("g.pag_fechaPago", "Fecha") + ", 2, t2.tdc_tipo, g.pag_nroDocum, g.pag_descripcion, g.pag_montoCheque " +
                            "FROM admpagos g LEFT JOIN admtipdocpro t2 ON t2.tdc_codigo = g.pag_tipoDoc WHERE g.pag_idProveedor = '" + c + "' " +
                            "ORDER BY Fecha DESC LIMIT " + (LimiteFilas * 4 + 1));
        }
    }
}
