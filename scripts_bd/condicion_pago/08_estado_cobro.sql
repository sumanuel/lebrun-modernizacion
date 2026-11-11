-- Estado de cobro de los documentos, paso 1: llena admdoccli.dcli_estatus con Pagado, Parcial o Pendiente.
-- Hasta ahora dcli_estatus repetia la misma palabra que dcli_estado (Pagado si era de contado, Activo si era a credito) y ninguna pantalla de
-- facturacion lo leia. Desde esta version dcli_estatus es el ESTADO DE COBRO del documento y se mantiene al facturar y al cobrar:
--   Pagado     no tiene saldo por cobrar (contado, o credito ya cobrado por completo).
--   Parcial    es a credito y se ha cobrado una parte.
--   Pendiente  es a credito y no se ha cobrado nada.
-- El estado se calcula con cuentas por cobrar (admsalcli): sin registro en admsalcli = contado = Pagado; Status = 1 o sal_actual = 0 = Pagado;
-- si MontoTotal - saldo es mayor que 0 = Parcial; si no, Pendiente.
-- NO se tocan dcli_estado ni dcli_expexp (el cuadre de caja y las devoluciones dependen de ellos).
-- Guarda copia de lo que cambia en bk_estatus_cobro. Se puede repetir sin problema. Tambien actualiza la copia de trabajo de la caja (admdoccli2).
-- Ejecutar DESPUES de 05_salcli_monto_cobrado.sql. Compatible con MySQL 5.5 y MariaDB. Revertir con 08_estado_cobro_revertir.sql.
USE sisadm;

CREATE TABLE IF NOT EXISTS bk_estatus_cobro (
    dcli_tipdoc         CHAR(3)     NOT NULL,
    dcli_numero         VARCHAR(14) NOT NULL,
    dcli_estatus_antes  VARCHAR(10) NULL,
    PRIMARY KEY (dcli_tipdoc, dcli_numero)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

INSERT IGNORE INTO bk_estatus_cobro (dcli_tipdoc, dcli_numero, dcli_estatus_antes)
SELECT d.dcli_tipdoc, d.dcli_numero, d.dcli_estatus
  FROM admdoccli d LEFT JOIN admsalcli s ON s.NroDocum = d.dcli_numero AND s.cli_codigo = d.dcli_codigo
 WHERE IFNULL(d.dcli_estatus, '') <> CASE WHEN s.NroDocum IS NULL THEN 'Pagado'
                                          WHEN s.Status = 1 OR s.sal_actual = 0 THEN 'Pagado'
                                          WHEN ABS(IFNULL(s.MontoTotal, 0)) - ABS(s.sal_actual) > 0.005 THEN 'Parcial'
                                          ELSE 'Pendiente' END;

UPDATE admdoccli d LEFT JOIN admsalcli s ON s.NroDocum = d.dcli_numero AND s.cli_codigo = d.dcli_codigo
   SET d.dcli_estatus = CASE WHEN s.NroDocum IS NULL THEN 'Pagado'
                             WHEN s.Status = 1 OR s.sal_actual = 0 THEN 'Pagado'
                             WHEN ABS(IFNULL(s.MontoTotal, 0)) - ABS(s.sal_actual) > 0.005 THEN 'Parcial'
                             ELSE 'Pendiente' END
 WHERE IFNULL(d.dcli_estatus, '') <> CASE WHEN s.NroDocum IS NULL THEN 'Pagado'
                                          WHEN s.Status = 1 OR s.sal_actual = 0 THEN 'Pagado'
                                          WHEN ABS(IFNULL(s.MontoTotal, 0)) - ABS(s.sal_actual) > 0.005 THEN 'Parcial'
                                          ELSE 'Pendiente' END;

-- copia de trabajo de la caja abierta
UPDATE admdoccli2 d LEFT JOIN admsalcli s ON s.NroDocum = d.dcli_numero AND s.cli_codigo = d.dcli_codigo
   SET d.dcli_estatus = CASE WHEN s.NroDocum IS NULL THEN 'Pagado'
                             WHEN s.Status = 1 OR s.sal_actual = 0 THEN 'Pagado'
                             WHEN ABS(IFNULL(s.MontoTotal, 0)) - ABS(s.sal_actual) > 0.005 THEN 'Parcial'
                             ELSE 'Pendiente' END;

-- Resultado: documentos por estado de cobro (el total debe ser igual al de documentos)
SELECT dcli_tipdoc, dcli_estatus, COUNT(*) AS documentos FROM admdoccli GROUP BY dcli_tipdoc, dcli_estatus ORDER BY dcli_tipdoc, dcli_estatus;
SELECT COUNT(*) AS documentos_con_cambio FROM bk_estatus_cobro;
