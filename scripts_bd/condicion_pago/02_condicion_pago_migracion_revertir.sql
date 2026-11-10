-- Condicion de pago y forma de pago, paso 2 (revertir): devuelve los valores guardados por 02_condicion_pago_migracion.sql.
-- Restaura clientes y documentos desde bk_condpago_cambios, vuelve a crear la fila "Contado" de admcondpago y deja la mora del 07 en 0.
-- Las tablas de copia (bk_condpago_cambios y bk_condpago_condiciones) NO se borran: se pueden eliminar a mano cuando ya no hagan falta.
-- Solo restaura lo que esta en la copia: los clientes y documentos creados despues del paso 2 se quedan con el codigo 07.
USE sisadm;

INSERT IGNORE INTO admcondpago SELECT * FROM bk_condpago_condiciones WHERE conp_codigo = 'Contado';
UPDATE admcondpago SET mora_fact1 = 0 WHERE conp_codigo = '07';

UPDATE admclientes t JOIN bk_condpago_cambios b ON b.tabla = 'admclientes' AND b.llave = t.cli_codigo
   SET t.cli_condipag = b.valor_anterior;
UPDATE admdoccli t JOIN bk_condpago_cambios b ON b.tabla = 'admdoccli' AND b.llave = CONCAT(t.dcli_tipdoc, '|', t.dcli_numero)
   SET t.dcli_condic = b.valor_anterior;
UPDATE admdoccliped t JOIN bk_condpago_cambios b ON b.tabla = 'admdoccliped' AND b.llave = CONCAT(t.dcli_tipdoc, '|', t.dcli_numero)
   SET t.dcli_condic = b.valor_anterior;
UPDATE admdocclipre t JOIN bk_condpago_cambios b ON b.tabla = 'admdocclipre' AND b.llave = CONCAT(t.dcli_tipdoc, '|', t.dcli_numero)
   SET t.dcli_condic = b.valor_anterior;
UPDATE admpedido t JOIN bk_condpago_cambios b ON b.tabla = 'admpedido' AND b.llave = t.pNroPedido
   SET t.pCondPago = b.valor_anterior;
UPDATE admsalcli t JOIN bk_condpago_cambios b ON b.tabla = 'admsalcli' AND b.llave = CONCAT(t.cli_codigo, '|', t.NroDocum)
   SET t.CondPago = b.valor_anterior;

SELECT cli_condipag, COUNT(*) AS clientes FROM admclientes GROUP BY cli_condipag;
SELECT conp_codigo, conp_descripcion, mora_fact1 FROM admcondpago WHERE conp_codigo IN ('07', 'Contado');
