-- Condicion de pago y forma de pago, paso 1: estructura de admcondpago.
-- Agrega dos columnas a sisadm.admcondpago y les pone valores iniciales:
--   conp_tipo    C = condicion de credito (Contado, 30 dias, ...), F = forma de pago (Efectivo, Tarjeta, ...). Las filas actuales son C.
--   conp_siglas  siglas de 1 a 4 letras para los reportes (TD = Tarjeta de Debito). Las de abajo son sugeridas: se pueden cambiar
--                desde la pantalla Condiciones y formas de pago.
-- Se puede repetir sin problema (solo agrega lo que falta y solo pone siglas donde estan vacias).
-- Compatible con MySQL 5.5 y MariaDB. Revertir con Revertir/01_condicion_pago_estructura_revertir.sql.
USE sisadm;

SET @existe = (SELECT COUNT(*) FROM information_schema.columns
                WHERE table_schema = DATABASE() AND table_name = 'admcondpago' AND column_name = 'conp_tipo');
SET @sql = IF(@existe = 0,
              "ALTER TABLE admcondpago ADD COLUMN conp_tipo CHAR(1) NOT NULL DEFAULT 'C' COMMENT 'C = condicion de credito, F = forma de pago'",
              "SELECT 'conp_tipo ya existe' AS aviso");
PREPARE paso FROM @sql;
EXECUTE paso;
DEALLOCATE PREPARE paso;

SET @existe = (SELECT COUNT(*) FROM information_schema.columns
                WHERE table_schema = DATABASE() AND table_name = 'admcondpago' AND column_name = 'conp_siglas');
SET @sql = IF(@existe = 0,
              "ALTER TABLE admcondpago ADD COLUMN conp_siglas VARCHAR(4) NULL COMMENT 'Siglas para los reportes (maximo 4 letras)'",
              "SELECT 'conp_siglas ya existe' AS aviso");
PREPARE paso FROM @sql;
EXECUTE paso;
DEALLOCATE PREPARE paso;

-- siglas sugeridas para las condiciones que ya existen (solo donde estan vacias)
UPDATE admcondpago SET conp_siglas = '30D'  WHERE conp_codigo = '01' AND (conp_siglas IS NULL OR conp_siglas = '');
UPDATE admcondpago SET conp_siglas = '60D'  WHERE conp_codigo = '02' AND (conp_siglas IS NULL OR conp_siglas = '');
UPDATE admcondpago SET conp_siglas = '90D'  WHERE conp_codigo = '03' AND (conp_siglas IS NULL OR conp_siglas = '');
UPDATE admcondpago SET conp_siglas = '120D' WHERE conp_codigo = '04' AND (conp_siglas IS NULL OR conp_siglas = '');
UPDATE admcondpago SET conp_siglas = 'GIR'  WHERE conp_codigo = '05' AND (conp_siglas IS NULL OR conp_siglas = '');
UPDATE admcondpago SET conp_siglas = 'COD'  WHERE conp_codigo = '06' AND (conp_siglas IS NULL OR conp_siglas = '');
UPDATE admcondpago SET conp_siglas = 'CON'  WHERE conp_codigo = '07' AND (conp_siglas IS NULL OR conp_siglas = '');
UPDATE admcondpago SET conp_siglas = 'G30'  WHERE conp_codigo = '11' AND (conp_siglas IS NULL OR conp_siglas = '');
UPDATE admcondpago SET conp_siglas = 'G60'  WHERE conp_codigo = '12' AND (conp_siglas IS NULL OR conp_siglas = '');
UPDATE admcondpago SET conp_siglas = 'G90'  WHERE conp_codigo = '13' AND (conp_siglas IS NULL OR conp_siglas = '');
UPDATE admcondpago SET conp_siglas = 'G120' WHERE conp_codigo = '14' AND (conp_siglas IS NULL OR conp_siglas = '');
UPDATE admcondpago SET conp_siglas = 'G2P'  WHERE conp_codigo = '15' AND (conp_siglas IS NULL OR conp_siglas = '');
UPDATE admcondpago SET conp_siglas = 'G3P'  WHERE conp_codigo = '16' AND (conp_siglas IS NULL OR conp_siglas = '');
UPDATE admcondpago SET conp_siglas = 'G4P'  WHERE conp_codigo = '17' AND (conp_siglas IS NULL OR conp_siglas = '');
UPDATE admcondpago SET conp_siglas = 'CRE'  WHERE conp_codigo = '18' AND (conp_siglas IS NULL OR conp_siglas = '');

-- Resultado
SELECT conp_codigo, conp_descripcion, conp_siglas, conp_tipo, conp_cant_dias, mora_fact1, conp_status FROM admcondpago ORDER BY conp_codigo;
