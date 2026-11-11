-- Origen de la venta, paso 1: tabla de origenes y columna en la factura.
-- El cliente quiere saber, ademas del vendedor, por donde llego la venta (tienda fisica, Instagram, Facebook, WhatsApp, ...) para consultas y estadisticas.
--   admorigenventa              tabla de origenes que se administra desde Ventas > Fichero > Origen de la Venta (nada se borra: se desactivan).
--   admdoccli.dcli_origenventa  codigo del origen de cada factura (NULL en las facturas anteriores a este cambio). No se usa dcli_origen porque
--                               ese campo ya guarda 'Nacional' (origen de la mercancia).
-- Se puede repetir sin problema (solo crea lo que falta y solo agrega los origenes que no existen).
-- Compatible con MySQL 5.5 y MariaDB. Revertir con Revertir/06_origen_venta_revertir.sql.
USE sisadm;

CREATE TABLE IF NOT EXISTS admorigenventa (
    orig_codigo      VARCHAR(4)  NOT NULL COMMENT 'Codigo del origen de la venta',
    orig_descripcion VARCHAR(40) NOT NULL COMMENT 'Tienda fisica, Instagram, ...',
    orig_status      CHAR(1)     NOT NULL DEFAULT '1' COMMENT '1 = activo (se puede elegir), 0 = inactivo',
    PRIMARY KEY (orig_codigo)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

INSERT IGNORE INTO admorigenventa (orig_codigo, orig_descripcion, orig_status) VALUES
    ('01', 'Tienda fisica',   '1'),
    ('02', 'Instagram',       '1'),
    ('03', 'Facebook',        '1'),
    ('04', 'WhatsApp',        '1'),
    ('05', 'Telefono',        '1'),
    ('06', 'Pagina web',      '1'),
    ('07', 'Otro',            '1');

SET @existe = (SELECT COUNT(*) FROM information_schema.columns
                WHERE table_schema = DATABASE() AND table_name = 'admdoccli' AND column_name = 'dcli_origenventa');
SET @sql = IF(@existe = 0,
              "ALTER TABLE admdoccli ADD COLUMN dcli_origenventa VARCHAR(4) NULL COMMENT 'Origen de la venta (admorigenventa.orig_codigo)'",
              "SELECT 'dcli_origenventa ya existe' AS aviso");
PREPARE paso FROM @sql;
EXECUTE paso;
DEALLOCATE PREPARE paso;

-- Resultado
SELECT orig_codigo, orig_descripcion, orig_status FROM admorigenventa ORDER BY orig_codigo;
SELECT COLUMN_NAME, COLUMN_TYPE, IS_NULLABLE FROM information_schema.columns
 WHERE table_schema = DATABASE() AND table_name = 'admdoccli' AND column_name = 'dcli_origenventa';
