-- Estado de cobro (revertir): devuelve a dcli_estatus lo que tenian los documentos antes de 08_estado_cobro.sql.
-- La tabla de copia bk_estatus_cobro no se borra: se puede eliminar a mano cuando ya no haga falta.
-- Los documentos facturados despues de instalar la version nueva no estan en la copia y conservan su estado de cobro.
USE sisadm;

UPDATE admdoccli d JOIN bk_estatus_cobro b ON b.dcli_tipdoc = d.dcli_tipdoc AND b.dcli_numero = d.dcli_numero
   SET d.dcli_estatus = b.dcli_estatus_antes;
UPDATE admdoccli2 d JOIN bk_estatus_cobro b ON b.dcli_tipdoc = d.dcli_tipdoc AND b.dcli_numero = d.dcli_numero
   SET d.dcli_estatus = b.dcli_estatus_antes;

SELECT COUNT(*) AS documentos_restaurados FROM bk_estatus_cobro;
