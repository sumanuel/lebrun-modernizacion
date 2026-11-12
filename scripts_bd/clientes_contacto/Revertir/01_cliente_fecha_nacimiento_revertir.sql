-- Revierte 01_cliente_fecha_nacimiento.sql: elimina la columna admclientes.cli_fechanac.
-- OJO: se pierden las fechas de nacimiento que ya se hayan cargado. Antes de ejecutarlo, instalar una version del programa que no use la columna
-- (la version con esta funcion la lee en cada factura y en la ficha del cliente, y fallaria sin ella).
-- Se guarda una copia de lo cargado en bk_cliente_fechanac (no se borra al revertir).
USE sisadm;

CREATE TABLE IF NOT EXISTS bk_cliente_fechanac (
    cli_codigo   VARCHAR(20) NOT NULL,
    cli_fechanac DATE NULL,
    respaldado   DATETIME NOT NULL,
    KEY (cli_codigo)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

SET @existe = (SELECT COUNT(*) FROM information_schema.columns
                WHERE table_schema = DATABASE() AND table_name = 'admclientes' AND column_name = 'cli_fechanac');
SET @sql = IF(@existe = 1,
              "INSERT INTO bk_cliente_fechanac (cli_codigo, cli_fechanac, respaldado) SELECT cli_codigo, cli_fechanac, NOW() FROM admclientes WHERE cli_fechanac IS NOT NULL",
              "SELECT 'cli_fechanac no existe: nada que respaldar' AS aviso");
PREPARE paso FROM @sql;
EXECUTE paso;
DEALLOCATE PREPARE paso;

SET @sql = IF(@existe = 1,
              "ALTER TABLE admclientes DROP COLUMN cli_fechanac",
              "SELECT 'cli_fechanac no existe' AS aviso");
PREPARE paso FROM @sql;
EXECUTE paso;
DEALLOCATE PREPARE paso;

SELECT COUNT(*) AS respaldadas FROM bk_cliente_fechanac;
