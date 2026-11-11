-- Cuentas por cobrar, paso 5 (revertir): devuelve el MontoCob que tenian los documentos corregidos por 05_salcli_monto_cobrado.sql.
-- La tabla de copia bk_salcli_montocob no se borra: se puede eliminar a mano cuando ya no haga falta.
USE sisadm;

UPDATE admsalcli t JOIN bk_salcli_montocob b ON b.cli_codigo = t.cli_codigo AND b.NroDocum = t.NroDocum
   SET t.MontoCob = b.MontoCob_anterior;

SELECT COUNT(*) AS documentos_restaurados FROM bk_salcli_montocob;
