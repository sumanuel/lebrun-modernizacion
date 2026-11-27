-- Orden de compra: descuentos del proveedor y tasa de cambio.
-- La orden guardaba el total con descuentos, pero no los porcentajes, asi que al verla o modificarla no se sabia cuales se habian usado. Se agregan:
--   admordencompra.ord_desc1, ord_desc2, ord_desc3  DECIMAL(9,6) NULL   porcentajes de descuento de la orden, que se aplican en cascada
-- Las ordenes que ya existen quedan con NULL (si no tenian descuento, el programa las muestra con cero).
-- Ademas la tasa de la orden pasa a 8 decimales, como en las ventas:
--   admordencompra.ord_valcamb  DECIMAL(10,2) -> DECIMAL(18,8)
-- Son columnas nuevas y opcionales: quien graba la tabla nombrando sus columnas no se afecta.
-- Se puede repetir sin problema. Compatible con MySQL 5.5 y MariaDB.
-- Revertir con Revertir/01_orden_compra_descuentos_revertir.sql.
USE sisadm;

-- admordencompra tiene una fecha cero (0000-00-00) como valor por defecto: se permite durante este script y se restaura al final
SET @modo_anterior = @@SESSION.sql_mode;
SET SESSION sql_mode = 'NO_ENGINE_SUBSTITUTION';

SET @existe = (SELECT COUNT(*) FROM information_schema.columns
                WHERE table_schema = DATABASE() AND table_name = 'admordencompra' AND column_name = 'ord_desc1');
SET @sql = IF(@existe = 0,
              "ALTER TABLE admordencompra ADD COLUMN ord_desc1 DECIMAL(9,6) NULL COMMENT 'Primer descuento de la orden (%)', ADD COLUMN ord_desc2 DECIMAL(9,6) NULL COMMENT 'Segundo descuento (%), sobre lo que queda del primero', ADD COLUMN ord_desc3 DECIMAL(9,6) NULL COMMENT 'Tercer descuento (%), sobre lo que queda del segundo'",
              "SELECT 'las columnas de descuento ya existen' AS aviso");
PREPARE paso FROM @sql;
EXECUTE paso;
DEALLOCATE PREPARE paso;

SET @escala = (SELECT NUMERIC_SCALE FROM information_schema.columns
                WHERE table_schema = DATABASE() AND table_name = 'admordencompra' AND column_name = 'ord_valcamb');
SET @sql = IF(@escala IS NOT NULL AND @escala <> 8,
              "ALTER TABLE admordencompra MODIFY COLUMN ord_valcamb DECIMAL(18,8) NULL DEFAULT NULL COMMENT 'valor cambio'",
              "SELECT 'admordencompra.ord_valcamb ya tiene 8 decimales' AS aviso");
PREPARE paso FROM @sql;
EXECUTE paso;
DEALLOCATE PREPARE paso;

SET SESSION sql_mode = @modo_anterior;

-- Resultado
SELECT COLUMN_NAME, COLUMN_TYPE, IS_NULLABLE FROM information_schema.columns
 WHERE table_schema = DATABASE() AND table_name = 'admordencompra' AND column_name IN ('ord_valcamb', 'ord_desc1', 'ord_desc2', 'ord_desc3');
