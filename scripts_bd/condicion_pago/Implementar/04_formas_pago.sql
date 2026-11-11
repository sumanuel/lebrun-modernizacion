-- Condicion de pago y forma de pago, paso 4: formas de pago en admcondpago.
-- Agrega dos columnas a sisadm.admcondpago y crea las formas de pago (conp_tipo = F) que se eligen al cancelar una factura:
--   conp_req_banco  1 = al cobrar con esta forma se pide el banco y el numero (tarjetas, cheque); 0 = no se piden.
--   conp_grupo      en que casilla de la factura se suma lo cobrado: E = Efectivo, T = Tarjetas (o pagos electronicos), C = Cheques.
-- Formas que se crean (codigo, descripcion, siglas): F01 Efectivo EFE, F02 Tarjeta de Debito TD, F03 Tarjeta de Credito TC, F04 Cheque CHQ,
-- F05 Divisa $ D$, F06 Divisa Euro DE, F07 Cachea CAC. Las descripciones y siglas se pueden cambiar desde la pantalla
-- Condiciones y formas de pago; no se deben cambiar los codigos.
-- Desde este paso, admmovcaja.mocv_forpag guarda el CODIGO de la forma de pago (F01, F02, ...) en los movimientos nuevos. Los valores
-- antiguos (EFECTIVO, TARJETA-D, TARJETA-C, CHEQUE) no se tocan: el programa los reconoce como F01, F02, F03 y F04.
-- Requiere el paso 1. Se puede repetir (no pisa lo que ya se haya modificado). Compatible con MySQL 5.5 y MariaDB.
-- Revertir con Revertir/04_formas_pago_revertir.sql.
USE sisadm;

SET @existe = (SELECT COUNT(*) FROM information_schema.columns
                WHERE table_schema = DATABASE() AND table_name = 'admcondpago' AND column_name = 'conp_req_banco');
SET @sql = IF(@existe = 0,
              "ALTER TABLE admcondpago ADD COLUMN conp_req_banco CHAR(1) NOT NULL DEFAULT '0' COMMENT '1 = pide banco y numero'",
              "SELECT 'conp_req_banco ya existe' AS aviso");
PREPARE paso FROM @sql;
EXECUTE paso;
DEALLOCATE PREPARE paso;

SET @existe = (SELECT COUNT(*) FROM information_schema.columns
                WHERE table_schema = DATABASE() AND table_name = 'admcondpago' AND column_name = 'conp_grupo');
SET @sql = IF(@existe = 0,
              "ALTER TABLE admcondpago ADD COLUMN conp_grupo CHAR(1) NOT NULL DEFAULT 'E' COMMENT 'E = efectivo, T = tarjetas, C = cheques'",
              "SELECT 'conp_grupo ya existe' AS aviso");
PREPARE paso FROM @sql;
EXECUTE paso;
DEALLOCATE PREPARE paso;

INSERT IGNORE INTO admcondpago (conp_codigo, conp_descripcion, conp_siglas, conp_tipo, conp_cant_dias, mora_fact1, conp_status, conp_req_banco, conp_grupo) VALUES
 ('F01', 'Efectivo',           'EFE', 'F', 0, 0, '1', '0', 'E'),
 ('F02', 'Tarjeta de Debito',  'TD',  'F', 0, 0, '1', '1', 'T'),
 ('F03', 'Tarjeta de Credito', 'TC',  'F', 0, 0, '1', '1', 'T'),
 ('F04', 'Cheque',             'CHQ', 'F', 0, 0, '1', '1', 'C'),
 ('F05', 'Divisa $',           'D$',  'F', 0, 0, '1', '0', 'E'),
 ('F06', 'Divisa Euro',        'DE',  'F', 0, 0, '1', '0', 'E'),
 ('F07', 'Cachea',             'CAC', 'F', 0, 0, '1', '0', 'T');

-- Resultado
SELECT conp_codigo, conp_descripcion, conp_siglas, conp_tipo, conp_req_banco, conp_grupo, conp_status FROM admcondpago WHERE conp_tipo = 'F' ORDER BY conp_codigo;
