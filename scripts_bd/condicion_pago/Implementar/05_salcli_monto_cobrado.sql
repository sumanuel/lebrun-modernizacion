-- Cuentas por cobrar, paso 5: corregir el monto cobrado de las facturas con saldo pendiente.
-- Antes la factura guardaba en admsalcli.MontoCob el total del documento aunque quedara saldo por cobrar (factura de 10.000 con 2.000 pagados:
-- MontoTotal 10.000, MontoCob 10.000, sal_actual 8.000). Desde esta version se guarda lo realmente cobrado (MontoCob 2.000) y los recibos de
-- ingreso y las retenciones lo van subiendo a medida que bajan el saldo.
-- Este script arregla los documentos que ya estaban abiertos (Status = 0, con saldo): MontoCob = MontoTotal - saldo.
-- Guarda copia de lo que cambia en bk_salcli_montocob. Se puede repetir: solo toca los documentos que todavia tienen MontoCob = MontoTotal.
-- Compatible con MySQL 5.5 y MariaDB. Revertir con Revertir/05_salcli_monto_cobrado_revertir.sql.
USE sisadm;

CREATE TABLE IF NOT EXISTS bk_salcli_montocob (
    cli_codigo        VARCHAR(12)   NOT NULL,
    NroDocum          VARCHAR(12)   NOT NULL,
    MontoCob_anterior DECIMAL(50,2) NULL,
    PRIMARY KEY (cli_codigo, NroDocum)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

INSERT IGNORE INTO bk_salcli_montocob (cli_codigo, NroDocum, MontoCob_anterior)
SELECT cli_codigo, NroDocum, MontoCob FROM admsalcli
 WHERE Status = 0 AND sal_actual <> 0 AND MontoTotal IS NOT NULL AND MontoCob = MontoTotal;

UPDATE admsalcli SET MontoCob = MontoTotal - ABS(sal_actual)
 WHERE Status = 0 AND sal_actual <> 0 AND MontoTotal IS NOT NULL AND MontoCob = MontoTotal;

-- Resultado
SELECT 'documentos corregidos' AS revision, COUNT(*) AS cantidad FROM bk_salcli_montocob
UNION ALL SELECT 'abiertos con saldo y MontoCob igual al total (debe ser 0)', COUNT(*) FROM admsalcli
 WHERE Status = 0 AND sal_actual <> 0 AND MontoTotal IS NOT NULL AND MontoCob = MontoTotal;
