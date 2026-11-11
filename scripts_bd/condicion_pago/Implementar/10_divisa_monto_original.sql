-- Formas de pago en divisa, paso 1: guardar el monto original en dolares o euros.
-- Hasta ahora, al cobrar con Divisa $ o Divisa Euro solo se guardaba el equivalente en bolivares (movc_monto). Desde esta version el cajero escribe el
-- monto en la divisa recibida; el programa lo convierte a bolivares con la tasa del dia (admhistodivi) y guarda ademas lo original:
--   admcondpago.conp_moneda        moneda de la forma de pago: USD o EUR (vacio = bolivares). Divisa $ (F05) = USD y Divisa Euro (F06) = EUR.
--   admmovcaja.movc_montodivisa    monto original en la divisa recibida (NULL si se pago en bolivares).
--   admmovcaja.movc_monedadivisa   USD o EUR.
--   admmovcaja.movc_tasadivisa     tasa usada para convertir (bolivares por unidad de divisa).
-- (movc_valcam NO es la tasa: guarda el vuelto del cliente, por eso se agregan columnas nuevas.)
-- Se puede repetir sin problema. Compatible con MySQL 5.5 y MariaDB. Revertir con Revertir/10_divisa_monto_original_revertir.sql.
USE sisadm;

SET @existe = (SELECT COUNT(*) FROM information_schema.columns
                WHERE table_schema = DATABASE() AND table_name = 'admcondpago' AND column_name = 'conp_moneda');
SET @sql = IF(@existe = 0,
              "ALTER TABLE admcondpago ADD COLUMN conp_moneda VARCHAR(3) NULL COMMENT 'USD o EUR si se cobra en divisa; vacio = bolivares'",
              "SELECT 'conp_moneda ya existe' AS aviso");
PREPARE paso FROM @sql;
EXECUTE paso;
DEALLOCATE PREPARE paso;

SET @existe = (SELECT COUNT(*) FROM information_schema.columns
                WHERE table_schema = DATABASE() AND table_name = 'admmovcaja' AND column_name = 'movc_montodivisa');
SET @sql = IF(@existe = 0,
              "ALTER TABLE admmovcaja ADD COLUMN movc_montodivisa DECIMAL(19,2) NULL COMMENT 'Monto original en la divisa recibida'",
              "SELECT 'movc_montodivisa ya existe' AS aviso");
PREPARE paso FROM @sql;
EXECUTE paso;
DEALLOCATE PREPARE paso;

SET @existe = (SELECT COUNT(*) FROM information_schema.columns
                WHERE table_schema = DATABASE() AND table_name = 'admmovcaja' AND column_name = 'movc_monedadivisa');
SET @sql = IF(@existe = 0,
              "ALTER TABLE admmovcaja ADD COLUMN movc_monedadivisa VARCHAR(3) NULL COMMENT 'USD o EUR'",
              "SELECT 'movc_monedadivisa ya existe' AS aviso");
PREPARE paso FROM @sql;
EXECUTE paso;
DEALLOCATE PREPARE paso;

SET @existe = (SELECT COUNT(*) FROM information_schema.columns
                WHERE table_schema = DATABASE() AND table_name = 'admmovcaja' AND column_name = 'movc_tasadivisa');
SET @sql = IF(@existe = 0,
              "ALTER TABLE admmovcaja ADD COLUMN movc_tasadivisa DECIMAL(19,4) NULL COMMENT 'Tasa usada: bolivares por unidad de divisa'",
              "SELECT 'movc_tasadivisa ya existe' AS aviso");
PREPARE paso FROM @sql;
EXECUTE paso;
DEALLOCATE PREPARE paso;

-- Divisa $ y Divisa Euro (solo si todavia no tienen moneda)
UPDATE admcondpago SET conp_moneda = 'USD' WHERE conp_codigo = 'F05' AND conp_tipo = 'F' AND (conp_moneda IS NULL OR conp_moneda = '');
UPDATE admcondpago SET conp_moneda = 'EUR' WHERE conp_codigo = 'F06' AND conp_tipo = 'F' AND (conp_moneda IS NULL OR conp_moneda = '');

-- Resultado
SELECT conp_codigo, conp_descripcion, conp_siglas, conp_moneda FROM admcondpago WHERE conp_tipo = 'F' ORDER BY conp_codigo;
SELECT COLUMN_NAME, COLUMN_TYPE FROM information_schema.columns
 WHERE table_schema = DATABASE() AND table_name = 'admmovcaja' AND column_name IN ('movc_montodivisa', 'movc_monedadivisa', 'movc_tasadivisa');
