-- Revierte 01_orden_compra_descuentos.sql.
-- OJO: antes de ejecutarlo, instalar una version del programa que no use estas columnas (la version con esta funcion las lee y las graba).
-- Antes de quitarlas se guarda una copia de los descuentos y de la tasa con todos sus decimales en bk_orden_descuentos (no se borra al revertir).
-- La tasa vuelve a 2 decimales.
USE sisadm;

CREATE TABLE IF NOT EXISTS bk_orden_descuentos (
    ord_correlativo VARCHAR(15) NOT NULL,
    ord_desc1       DECIMAL(9,6) NULL,
    ord_desc2       DECIMAL(9,6) NULL,
    ord_desc3       DECIMAL(9,6) NULL,
    ord_valcamb     DECIMAL(18,8) NULL,
    respaldado      DATETIME NOT NULL,
    KEY (ord_correlativo)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

SET @modo_anterior = @@SESSION.sql_mode;
SET SESSION sql_mode = 'NO_ENGINE_SUBSTITUTION';

SET @existe = (SELECT COUNT(*) FROM information_schema.columns
                WHERE table_schema = DATABASE() AND table_name = 'admordencompra' AND column_name = 'ord_desc1');
SET @sql = IF(@existe = 1,
              "INSERT INTO bk_orden_descuentos (ord_correlativo, ord_desc1, ord_desc2, ord_desc3, ord_valcamb, respaldado) SELECT ord_correlativo, ord_desc1, ord_desc2, ord_desc3, ord_valcamb, NOW() FROM admordencompra",
              "SELECT 'no hay columnas de descuento: nada que respaldar' AS aviso");
PREPARE paso FROM @sql;
EXECUTE paso;
DEALLOCATE PREPARE paso;

SET @sql = IF(@existe = 1,
              "ALTER TABLE admordencompra DROP COLUMN ord_desc1, DROP COLUMN ord_desc2, DROP COLUMN ord_desc3, MODIFY COLUMN ord_valcamb DECIMAL(10,2) NULL DEFAULT NULL COMMENT 'valor cambio'",
              "SELECT 'las columnas de descuento no existen' AS aviso");
PREPARE paso FROM @sql;
EXECUTE paso;
DEALLOCATE PREPARE paso;

SET SESSION sql_mode = @modo_anterior;

SELECT COUNT(*) AS ordenes_respaldadas FROM bk_orden_descuentos;
