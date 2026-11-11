-- Estado de cobro, paso 2: corrige los estados de la factura 000001496756 (FAV del 24/11/2017).
-- Tenia dcli_estado = Pagado y dcli_expexp / dcli_estatus = Activo. Es una factura de contado (condicion 07), con registro de cuentas por cobrar
-- cerrado (Status 1, saldo 0) y numero fiscal, asi que los tres campos pasan a Pagado. Solo se toca si todavia tiene el estado inconsistente.
-- No se tocan los montos: tiene neto 0,00 y dcli_saldo 15.457.956,80, valores que el cliente debe revisar aparte.
-- Guarda copia de lo que tenia en bk_correccion_estados. Se puede repetir sin problema. Revertir con Revertir/09_corregir_factura_1496756_revertir.sql.
USE sisadm;

CREATE TABLE IF NOT EXISTS bk_correccion_estados (
    dcli_tipdoc  CHAR(3)     NOT NULL,
    dcli_numero  VARCHAR(14) NOT NULL,
    dcli_estado  VARCHAR(10) NULL,
    dcli_expexp  VARCHAR(32) NULL,
    dcli_estatus VARCHAR(10) NULL,
    PRIMARY KEY (dcli_tipdoc, dcli_numero)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

INSERT IGNORE INTO bk_correccion_estados (dcli_tipdoc, dcli_numero, dcli_estado, dcli_expexp, dcli_estatus)
SELECT dcli_tipdoc, dcli_numero, dcli_estado, dcli_expexp, dcli_estatus FROM admdoccli
 WHERE dcli_tipdoc = 'FAV' AND dcli_numero = '000001496756' AND dcli_condic = '07' AND dcli_estado = 'Pagado' AND dcli_expexp = 'Activo';

UPDATE admdoccli SET dcli_estado = 'Pagado', dcli_expexp = 'Pagado', dcli_estatus = 'Pagado'
 WHERE dcli_tipdoc = 'FAV' AND dcli_numero = '000001496756' AND dcli_condic = '07' AND dcli_estado = 'Pagado' AND dcli_expexp = 'Activo';
UPDATE admdoccli2 SET dcli_estado = 'Pagado', dcli_expexp = 'Pagado', dcli_estatus = 'Pagado'
 WHERE dcli_tipdoc = 'FAV' AND dcli_numero = '000001496756' AND dcli_condic = '07' AND dcli_estado = 'Pagado' AND dcli_expexp = 'Activo';

-- Resultado (si la factura no existe en esta base no sale ninguna fila)
SELECT dcli_tipdoc, dcli_numero, dcli_condic, dcli_estado, dcli_expexp, dcli_estatus FROM admdoccli WHERE dcli_numero = '000001496756';
