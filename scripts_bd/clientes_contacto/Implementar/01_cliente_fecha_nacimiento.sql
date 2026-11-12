-- Datos de contacto del cliente, paso 1: fecha de nacimiento.
-- El cliente quiere pedir en caja la fecha de nacimiento y el correo del cliente (para enviarle promociones y felicitarlo en su cumpleanos).
-- El correo ya existe (admclientes.cli_correo, VARCHAR(60), no obligatorio). La fecha de nacimiento no tenia campo: se crea
--   admclientes.cli_fechanac  DATE NULL   (vacio = no se conoce la fecha)
-- No se reutiliza cli_finvaca porque es el fin de las vacaciones y se muestra con ese nombre en la ficha del cliente.
-- Es una columna nueva y opcional: los INSERT/UPDATE de otros programas y los triggers de admclientes (que nombran sus columnas) no se afectan.
-- Se puede repetir sin problema (solo agrega la columna si falta). Compatible con MySQL 5.5 y MariaDB.
-- Revertir con Revertir/01_cliente_fecha_nacimiento_revertir.sql.
USE sisadm;

SET @existe = (SELECT COUNT(*) FROM information_schema.columns
                WHERE table_schema = DATABASE() AND table_name = 'admclientes' AND column_name = 'cli_fechanac');
SET @sql = IF(@existe = 0,
              "ALTER TABLE admclientes ADD COLUMN cli_fechanac DATE NULL COMMENT 'Fecha de nacimiento del cliente (opcional)'",
              "SELECT 'cli_fechanac ya existe' AS aviso");
PREPARE paso FROM @sql;
EXECUTE paso;
DEALLOCATE PREPARE paso;

-- Resultado
SELECT COLUMN_NAME, COLUMN_TYPE, IS_NULLABLE FROM information_schema.columns
 WHERE table_schema = DATABASE() AND table_name = 'admclientes' AND column_name IN ('cli_correo', 'cli_fechanac');
