-- Tasa de cambio con 8 decimales.
-- El BCV publica la tasa con 8 decimales y la tabla del historial de divisas solo guardaba 2. Se amplian a 8 decimales:
--   admhistodivi.histo_valor    DECIMAL(10,2) -> DECIMAL(18,8)   historial de tasas (dolar, euro, ...)
--   admdoccli.dcli_valcamb      DECIMAL(50,2) -> DECIMAL(50,8)   tasa del dia que graba cada factura nueva
--   admmovcaja.movc_tasadivisa  DECIMAL(19,4) -> DECIMAL(19,8)   tasa usada en los pagos en Divisa $ / Divisa Euro
-- Los valores que ya existen no cambian (36,50 pasa a 36,50000000). Se puede repetir sin problema: solo cambia una columna si todavia no
-- tiene 8 decimales. Compatible con MySQL 5.5 y MariaDB.
-- Compras (admdocpro.doc_valcamb), pagos (admcomppago.comp_valcam), bancos (admmovban.movb_valcam) e impuestos (admimppat.imp_tasa) NO se tocan.
-- OJO: cambiar el tipo de admdoccli y admmovcaja reconstruye la tabla; en una base grande conviene hacerlo fuera del horario de trabajo.
-- Revertir con Revertir/01_tasa_cambio_decimales_revertir.sql.
USE sisadm;

-- admhistodivi tiene fechas cero (0000-00-00) como valor por defecto: se permite durante este script y se restaura al final
SET @modo_anterior = @@SESSION.sql_mode;
SET SESSION sql_mode = 'NO_ENGINE_SUBSTITUTION';

-- admhistodivi.histo_valor
SET @escala = (SELECT NUMERIC_SCALE FROM information_schema.columns
                WHERE table_schema = DATABASE() AND table_name = 'admhistodivi' AND column_name = 'histo_valor');
SET @sql = IF(@escala IS NOT NULL AND @escala <> 8,
              "ALTER TABLE admhistodivi MODIFY COLUMN histo_valor DECIMAL(18,8) NULL DEFAULT NULL",
              "SELECT 'admhistodivi.histo_valor ya tiene 8 decimales' AS aviso");
PREPARE paso FROM @sql;
EXECUTE paso;
DEALLOCATE PREPARE paso;

-- admdoccli.dcli_valcamb
SET @escala = (SELECT NUMERIC_SCALE FROM information_schema.columns
                WHERE table_schema = DATABASE() AND table_name = 'admdoccli' AND column_name = 'dcli_valcamb');
SET @sql = IF(@escala IS NOT NULL AND @escala <> 8,
              "ALTER TABLE admdoccli MODIFY COLUMN dcli_valcamb DECIMAL(50,8) NULL DEFAULT NULL",
              "SELECT 'admdoccli.dcli_valcamb ya tiene 8 decimales' AS aviso");
PREPARE paso FROM @sql;
EXECUTE paso;
DEALLOCATE PREPARE paso;

-- admmovcaja.movc_tasadivisa (se crea con el script 10 de condicion_pago; si todavia no existe no se hace nada)
SET @escala = (SELECT NUMERIC_SCALE FROM information_schema.columns
                WHERE table_schema = DATABASE() AND table_name = 'admmovcaja' AND column_name = 'movc_tasadivisa');
SET @sql = IF(@escala IS NOT NULL AND @escala <> 8,
              "ALTER TABLE admmovcaja MODIFY COLUMN movc_tasadivisa DECIMAL(19,8) NULL DEFAULT NULL COMMENT 'Tasa usada: bolivares por unidad de divisa'",
              "SELECT 'admmovcaja.movc_tasadivisa ya tiene 8 decimales (o no existe)' AS aviso");
PREPARE paso FROM @sql;
EXECUTE paso;
DEALLOCATE PREPARE paso;

SET SESSION sql_mode = @modo_anterior;

-- Resultado
SELECT table_name, column_name, column_type FROM information_schema.columns
 WHERE table_schema = DATABASE()
   AND ((table_name = 'admhistodivi' AND column_name = 'histo_valor')
     OR (table_name = 'admdoccli'    AND column_name = 'dcli_valcamb')
     OR (table_name = 'admmovcaja'   AND column_name = 'movc_tasadivisa'));
