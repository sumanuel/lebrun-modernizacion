-- Revierte 10_divisa_monto_original.sql: quita las columnas de monto original en divisa.
-- OJO: se pierde el monto original guardado en admmovcaja. Guarda antes una copia si hace falta:
--   CREATE TABLE bk_divisa_original AS SELECT movc_numtra, movc_montodivisa, movc_monedadivisa, movc_tasadivisa FROM admmovcaja WHERE movc_montodivisa IS NOT NULL;
-- Se puede repetir sin problema.
USE sisadm;

SET @existe = (SELECT COUNT(*) FROM information_schema.columns WHERE table_schema = DATABASE() AND table_name = 'admmovcaja' AND column_name = 'movc_montodivisa');
SET @sql = IF(@existe = 1, "ALTER TABLE admmovcaja DROP COLUMN movc_montodivisa", "SELECT 'movc_montodivisa no existe' AS aviso");
PREPARE paso FROM @sql; EXECUTE paso; DEALLOCATE PREPARE paso;

SET @existe = (SELECT COUNT(*) FROM information_schema.columns WHERE table_schema = DATABASE() AND table_name = 'admmovcaja' AND column_name = 'movc_monedadivisa');
SET @sql = IF(@existe = 1, "ALTER TABLE admmovcaja DROP COLUMN movc_monedadivisa", "SELECT 'movc_monedadivisa no existe' AS aviso");
PREPARE paso FROM @sql; EXECUTE paso; DEALLOCATE PREPARE paso;

SET @existe = (SELECT COUNT(*) FROM information_schema.columns WHERE table_schema = DATABASE() AND table_name = 'admmovcaja' AND column_name = 'movc_tasadivisa');
SET @sql = IF(@existe = 1, "ALTER TABLE admmovcaja DROP COLUMN movc_tasadivisa", "SELECT 'movc_tasadivisa no existe' AS aviso");
PREPARE paso FROM @sql; EXECUTE paso; DEALLOCATE PREPARE paso;

SET @existe = (SELECT COUNT(*) FROM information_schema.columns WHERE table_schema = DATABASE() AND table_name = 'admcondpago' AND column_name = 'conp_moneda');
SET @sql = IF(@existe = 1, "ALTER TABLE admcondpago DROP COLUMN conp_moneda", "SELECT 'conp_moneda no existe' AS aviso");
PREPARE paso FROM @sql; EXECUTE paso; DEALLOCATE PREPARE paso;

-- Resultado: debe dar 0
SELECT COUNT(*) AS columnas_que_quedan FROM information_schema.columns
 WHERE table_schema = DATABASE() AND ((table_name = 'admmovcaja' AND column_name IN ('movc_montodivisa', 'movc_monedadivisa', 'movc_tasadivisa'))
    OR (table_name = 'admcondpago' AND column_name = 'conp_moneda'));
