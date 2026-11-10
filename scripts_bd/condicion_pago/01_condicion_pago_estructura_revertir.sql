-- Condicion de pago y forma de pago, paso 1 (revertir): quita las columnas conp_tipo y conp_siglas de sisadm.admcondpago.
-- Ojo: se pierden las siglas y el tipo que se hayan escrito, y las formas de pago creadas despues (tipo F) quedarian como condiciones.
-- Antes de revertir este paso hay que revertir los pasos 3, 2 (y el de formas de pago, si se aplico), en ese orden.
USE sisadm;

SET @existe = (SELECT COUNT(*) FROM information_schema.columns
                WHERE table_schema = DATABASE() AND table_name = 'admcondpago' AND column_name = 'conp_tipo');
SET @sql = IF(@existe = 1, "ALTER TABLE admcondpago DROP COLUMN conp_tipo", "SELECT 'conp_tipo no existe' AS aviso");
PREPARE paso FROM @sql;
EXECUTE paso;
DEALLOCATE PREPARE paso;

SET @existe = (SELECT COUNT(*) FROM information_schema.columns
                WHERE table_schema = DATABASE() AND table_name = 'admcondpago' AND column_name = 'conp_siglas');
SET @sql = IF(@existe = 1, "ALTER TABLE admcondpago DROP COLUMN conp_siglas", "SELECT 'conp_siglas no existe' AS aviso");
PREPARE paso FROM @sql;
EXECUTE paso;
DEALLOCATE PREPARE paso;

SELECT * FROM admcondpago ORDER BY conp_codigo;
