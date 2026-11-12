-- Revierte 01_tasa_cambio_decimales.sql: las tres columnas vuelven a su tipo anterior (2, 2 y 4 decimales).
-- OJO: las tasas con mas decimales SE REDONDEAN al volver. Antes se guarda una copia exacta en las tablas bk_tasa_histodivi, bk_tasa_doccli y
-- bk_tasa_movcaja (no se borran al revertir). Si se ejecuta mas de una vez, solo actua la primera (las columnas ya quedan con su tipo anterior).
USE sisadm;

-- admhistodivi tiene fechas cero (0000-00-00) como valor por defecto: se permite durante este script y se restaura al final
SET @modo_anterior = @@SESSION.sql_mode;
SET SESSION sql_mode = 'NO_ENGINE_SUBSTITUTION';

CREATE TABLE IF NOT EXISTS bk_tasa_histodivi (
    histo_moneda CHAR(4) NOT NULL, histo_fecha DATE NOT NULL, histo_hora CHAR(15) NOT NULL, histo_valor DECIMAL(18,8) NULL, respaldado DATETIME NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
CREATE TABLE IF NOT EXISTS bk_tasa_doccli (
    dcli_numero VARCHAR(14) NOT NULL, dcli_tipdoc CHAR(3) NOT NULL, dcli_valcamb DECIMAL(50,8) NULL, respaldado DATETIME NOT NULL, KEY (dcli_numero)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
CREATE TABLE IF NOT EXISTS bk_tasa_movcaja (
    movc_numtra VARCHAR(10) NOT NULL, movc_tasadivisa DECIMAL(19,8) NULL, respaldado DATETIME NOT NULL, KEY (movc_numtra)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- admhistodivi.histo_valor
SET @escala = (SELECT NUMERIC_SCALE FROM information_schema.columns
                WHERE table_schema = DATABASE() AND table_name = 'admhistodivi' AND column_name = 'histo_valor');
SET @sql = IF(@escala = 8,
              "INSERT INTO bk_tasa_histodivi (histo_moneda, histo_fecha, histo_hora, histo_valor, respaldado) SELECT histo_moneda, histo_fecha, histo_hora, histo_valor, NOW() FROM admhistodivi WHERE histo_valor IS NOT NULL",
              "SELECT 'admhistodivi.histo_valor no tiene 8 decimales: nada que respaldar' AS aviso");
PREPARE paso FROM @sql;
EXECUTE paso;
DEALLOCATE PREPARE paso;
SET @sql = IF(@escala = 8,
              "ALTER TABLE admhistodivi MODIFY COLUMN histo_valor DECIMAL(10,2) NULL DEFAULT NULL",
              "SELECT 'admhistodivi.histo_valor ya esta en 2 decimales' AS aviso");
PREPARE paso FROM @sql;
EXECUTE paso;
DEALLOCATE PREPARE paso;

-- admdoccli.dcli_valcamb
SET @escala = (SELECT NUMERIC_SCALE FROM information_schema.columns
                WHERE table_schema = DATABASE() AND table_name = 'admdoccli' AND column_name = 'dcli_valcamb');
SET @sql = IF(@escala = 8,
              "INSERT INTO bk_tasa_doccli (dcli_numero, dcli_tipdoc, dcli_valcamb, respaldado) SELECT dcli_numero, dcli_tipdoc, dcli_valcamb, NOW() FROM admdoccli WHERE dcli_valcamb IS NOT NULL",
              "SELECT 'admdoccli.dcli_valcamb no tiene 8 decimales: nada que respaldar' AS aviso");
PREPARE paso FROM @sql;
EXECUTE paso;
DEALLOCATE PREPARE paso;
SET @sql = IF(@escala = 8,
              "ALTER TABLE admdoccli MODIFY COLUMN dcli_valcamb DECIMAL(50,2) NULL DEFAULT NULL",
              "SELECT 'admdoccli.dcli_valcamb ya esta en 2 decimales' AS aviso");
PREPARE paso FROM @sql;
EXECUTE paso;
DEALLOCATE PREPARE paso;

-- admmovcaja.movc_tasadivisa
SET @escala = (SELECT NUMERIC_SCALE FROM information_schema.columns
                WHERE table_schema = DATABASE() AND table_name = 'admmovcaja' AND column_name = 'movc_tasadivisa');
SET @sql = IF(@escala = 8,
              "INSERT INTO bk_tasa_movcaja (movc_numtra, movc_tasadivisa, respaldado) SELECT movc_numtra, movc_tasadivisa, NOW() FROM admmovcaja WHERE movc_tasadivisa IS NOT NULL",
              "SELECT 'admmovcaja.movc_tasadivisa no tiene 8 decimales: nada que respaldar' AS aviso");
PREPARE paso FROM @sql;
EXECUTE paso;
DEALLOCATE PREPARE paso;
SET @sql = IF(@escala = 8,
              "ALTER TABLE admmovcaja MODIFY COLUMN movc_tasadivisa DECIMAL(19,4) NULL DEFAULT NULL COMMENT 'Tasa usada: bolivares por unidad de divisa'",
              "SELECT 'admmovcaja.movc_tasadivisa ya esta en 4 decimales' AS aviso");
PREPARE paso FROM @sql;
EXECUTE paso;
DEALLOCATE PREPARE paso;

SET SESSION sql_mode = @modo_anterior;

SELECT (SELECT COUNT(*) FROM bk_tasa_histodivi) AS respaldo_historial, (SELECT COUNT(*) FROM bk_tasa_doccli) AS respaldo_facturas,
       (SELECT COUNT(*) FROM bk_tasa_movcaja) AS respaldo_pagos;
