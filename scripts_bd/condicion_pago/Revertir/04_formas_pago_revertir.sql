-- Condicion de pago y forma de pago, paso 4 (revertir): quita las formas de pago creadas (F01 a F07) y las columnas conp_req_banco y conp_grupo.
-- Ojo: si ya hay movimientos de caja con esos codigos en admmovcaja.mocv_forpag, quedarian sin descripcion; revertir solo si no se uso.
USE sisadm;

DELETE FROM admcondpago WHERE conp_codigo IN ('F01', 'F02', 'F03', 'F04', 'F05', 'F06', 'F07');

SET @existe = (SELECT COUNT(*) FROM information_schema.columns
                WHERE table_schema = DATABASE() AND table_name = 'admcondpago' AND column_name = 'conp_req_banco');
SET @sql = IF(@existe = 1, "ALTER TABLE admcondpago DROP COLUMN conp_req_banco", "SELECT 'conp_req_banco no existe' AS aviso");
PREPARE paso FROM @sql;
EXECUTE paso;
DEALLOCATE PREPARE paso;

SET @existe = (SELECT COUNT(*) FROM information_schema.columns
                WHERE table_schema = DATABASE() AND table_name = 'admcondpago' AND column_name = 'conp_grupo');
SET @sql = IF(@existe = 1, "ALTER TABLE admcondpago DROP COLUMN conp_grupo", "SELECT 'conp_grupo no existe' AS aviso");
PREPARE paso FROM @sql;
EXECUTE paso;
DEALLOCATE PREPARE paso;

SELECT conp_codigo, conp_descripcion, conp_tipo FROM admcondpago ORDER BY conp_codigo;
