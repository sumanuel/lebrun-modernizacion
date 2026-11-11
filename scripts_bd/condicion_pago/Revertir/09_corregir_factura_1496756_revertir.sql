-- Revierte 09_corregir_factura_1496756.sql: devuelve los tres estados que tenia la factura 000001496756.
-- La tabla de copia bk_correccion_estados no se borra.
USE sisadm;

UPDATE admdoccli d JOIN bk_correccion_estados b ON b.dcli_tipdoc = d.dcli_tipdoc AND b.dcli_numero = d.dcli_numero
   SET d.dcli_estado = b.dcli_estado, d.dcli_expexp = b.dcli_expexp, d.dcli_estatus = b.dcli_estatus
 WHERE d.dcli_numero = '000001496756';
UPDATE admdoccli2 d JOIN bk_correccion_estados b ON b.dcli_tipdoc = d.dcli_tipdoc AND b.dcli_numero = d.dcli_numero
   SET d.dcli_estado = b.dcli_estado, d.dcli_expexp = b.dcli_expexp, d.dcli_estatus = b.dcli_estatus
 WHERE d.dcli_numero = '000001496756';

SELECT dcli_tipdoc, dcli_numero, dcli_estado, dcli_expexp, dcli_estatus FROM admdoccli WHERE dcli_numero = '000001496756';
