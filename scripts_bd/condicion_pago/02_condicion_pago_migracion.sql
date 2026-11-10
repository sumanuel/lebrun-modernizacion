-- Condicion de pago y forma de pago, paso 2: pasar lo guardado a codigos y dejar una sola condicion Contado.
-- Antes: Lebrun guardaba el TEXTO "Contado" en el cliente y en algunos documentos, y habia dos filas "Contado" en admcondpago
--        (07 CONTADO y la de codigo "Contado"), lo que hacia ambigua la busqueda.
-- Despues: todo se guarda con el CODIGO de admcondpago; Contado es el 07 (0 dias de credito) y la fila de codigo "Contado" se elimina.
--
-- Lo que hace:
--   1. Guarda copia de cada valor que va a cambiar en bk_condpago_cambios (y de la fila que elimina en bk_condpago_condiciones).
--   2. Clientes sin condicion o con "Contado" pasan a 07; "Credito" pasa a 18 (en Lebrun no hay ninguno).
--   3. Documentos (facturas, pedidos, prefacturas y cuentas por cobrar) con el texto "Contado" pasan a 07.
--   4. La mora de la fila "Contado" (dias para suspender al cliente con facturas vencidas) se pasa al 07 para que la suspension
--      automatica siga funcionando igual. Si el cliente decide otra cosa, se cambia desde la pantalla Condiciones y formas de pago.
--   5. Se elimina la fila de codigo "Contado".
-- Se puede repetir: las copias no se pisan y lo que ya esta migrado no vuelve a cambiar.
-- Requiere el paso 1 (conp_tipo, conp_siglas). Compatible con MySQL 5.5 y MariaDB. Revertir con 02_condicion_pago_migracion_revertir.sql.
USE sisadm;

CREATE TABLE IF NOT EXISTS bk_condpago_cambios (
    tabla          VARCHAR(30) NOT NULL,
    llave          VARCHAR(60) NOT NULL,
    valor_anterior VARCHAR(35) NULL,
    PRIMARY KEY (tabla, llave)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

CREATE TABLE IF NOT EXISTS bk_condpago_condiciones LIKE admcondpago;

-- 1. copias
INSERT IGNORE INTO bk_condpago_cambios (tabla, llave, valor_anterior)
SELECT 'admclientes', cli_codigo, cli_condipag FROM admclientes
 WHERE cli_condipag IS NULL OR TRIM(cli_condipag) = '' OR cli_condipag = 'Contado' OR cli_condipag LIKE 'Cr_dito';

INSERT IGNORE INTO bk_condpago_cambios (tabla, llave, valor_anterior)
SELECT 'admdoccli', CONCAT(dcli_tipdoc, '|', dcli_numero), dcli_condic FROM admdoccli WHERE dcli_condic = 'Contado';
INSERT IGNORE INTO bk_condpago_cambios (tabla, llave, valor_anterior)
SELECT 'admdoccliped', CONCAT(dcli_tipdoc, '|', dcli_numero), dcli_condic FROM admdoccliped WHERE dcli_condic = 'Contado';
INSERT IGNORE INTO bk_condpago_cambios (tabla, llave, valor_anterior)
SELECT 'admdocclipre', CONCAT(dcli_tipdoc, '|', dcli_numero), dcli_condic FROM admdocclipre WHERE dcli_condic = 'Contado';
INSERT IGNORE INTO bk_condpago_cambios (tabla, llave, valor_anterior)
SELECT 'admpedido', pNroPedido, pCondPago FROM admpedido WHERE pCondPago = 'Contado';
INSERT IGNORE INTO bk_condpago_cambios (tabla, llave, valor_anterior)
SELECT 'admsalcli', CONCAT(cli_codigo, '|', NroDocum), CondPago FROM admsalcli WHERE CondPago = 'Contado';

INSERT IGNORE INTO bk_condpago_condiciones SELECT * FROM admcondpago WHERE conp_codigo = 'Contado';

-- 2 y 3. valores
UPDATE admclientes SET cli_condipag = '07' WHERE cli_condipag IS NULL OR TRIM(cli_condipag) = '' OR cli_condipag = 'Contado';
UPDATE admclientes SET cli_condipag = '18' WHERE cli_condipag LIKE 'Cr_dito';
UPDATE admdoccli    SET dcli_condic = '07' WHERE dcli_condic = 'Contado';
UPDATE admdoccliped SET dcli_condic = '07' WHERE dcli_condic = 'Contado';
UPDATE admdocclipre SET dcli_condic = '07' WHERE dcli_condic = 'Contado';
UPDATE admpedido    SET pCondPago = '07' WHERE pCondPago = 'Contado';
UPDATE admsalcli    SET CondPago = '07' WHERE CondPago = 'Contado';

-- 4. la mora de "Contado" pasa al 07 (solo si el 07 no tiene mora propia)
UPDATE admcondpago nuevo
  JOIN admcondpago viejo ON viejo.conp_codigo = 'Contado'
   SET nuevo.mora_fact1 = viejo.mora_fact1
 WHERE nuevo.conp_codigo = '07' AND nuevo.mora_fact1 = 0;

-- 5. se elimina la fila duplicada
DELETE FROM admcondpago WHERE conp_codigo = 'Contado';

-- Resultado: no debe quedar ningun "Contado" como texto
SELECT 'clientes con texto Contado o vacios' AS revision, COUNT(*) AS cantidad FROM admclientes WHERE cli_condipag IS NULL OR TRIM(cli_condipag) = '' OR cli_condipag = 'Contado'
UNION ALL SELECT 'facturas con texto Contado', COUNT(*) FROM admdoccli WHERE dcli_condic = 'Contado'
UNION ALL SELECT 'cuentas por cobrar con texto Contado', COUNT(*) FROM admsalcli WHERE CondPago = 'Contado'
UNION ALL SELECT 'filas Contado en admcondpago', COUNT(*) FROM admcondpago WHERE conp_codigo = 'Contado'
UNION ALL SELECT 'clientes en 07', COUNT(*) FROM admclientes WHERE cli_condipag = '07'
UNION ALL SELECT 'copias guardadas', COUNT(*) FROM bk_condpago_cambios;
SELECT conp_codigo, conp_descripcion, conp_cant_dias, mora_fact1 FROM admcondpago WHERE conp_codigo = '07';
