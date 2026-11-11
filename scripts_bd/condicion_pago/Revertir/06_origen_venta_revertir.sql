-- Revierte 06_origen_venta.sql: quita la columna dcli_origenventa de las facturas y borra la tabla de origenes.
-- OJO: se pierde el origen de venta guardado en las facturas. Guarda antes una copia si hace falta:
--   CREATE TABLE bk_origenventa_facturas AS SELECT dcli_tipdoc, dcli_numero, dcli_origenventa FROM admdoccli WHERE dcli_origenventa IS NOT NULL;
-- Se puede repetir sin problema.
USE sisadm;

SET @existe = (SELECT COUNT(*) FROM information_schema.columns
                WHERE table_schema = DATABASE() AND table_name = 'admdoccli' AND column_name = 'dcli_origenventa');
SET @sql = IF(@existe = 1,
              "ALTER TABLE admdoccli DROP COLUMN dcli_origenventa",
              "SELECT 'dcli_origenventa no existe' AS aviso");
PREPARE paso FROM @sql;
EXECUTE paso;
DEALLOCATE PREPARE paso;

DROP TABLE IF EXISTS admorigenventa;

-- Resultado: debe dar 0 y 0
SELECT COUNT(*) AS columna_dcli_origenventa FROM information_schema.columns
 WHERE table_schema = DATABASE() AND table_name = 'admdoccli' AND column_name = 'dcli_origenventa';
SELECT COUNT(*) AS tabla_admorigenventa FROM information_schema.tables
 WHERE table_schema = DATABASE() AND table_name = 'admorigenventa';
