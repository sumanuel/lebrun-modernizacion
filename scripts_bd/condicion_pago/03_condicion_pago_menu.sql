-- Condicion de pago y forma de pago, paso 3: pantalla de mantenimiento en el menu.
-- Asigna la pantalla "Condiciones y formas de pago" a la opcion Tesoreria > Fichero > Marcas Financieras, que estaba sin pantalla.
-- Se puede repetir: solo completa la ruta si esta vacia. Revertir con 03_condicion_pago_menu_revertir.sql.
USE sysconf;

UPDATE conf_menu SET menu_ruta = 'lebrun.formularios.tesoreria.lbxCondicionesPago'
 WHERE menu_padre = 'Tesoreria' AND menu_subpadre = 'Fichero' AND menu_hijo = 'Marcas Financieras'
   AND (menu_ruta IS NULL OR menu_ruta IN ('', 'NULL', '(NULL)'));

-- Resultado: la opcion debe mostrar su ruta
SELECT idMenu, menu_padre, menu_subpadre, menu_hijo, menu_ruta FROM conf_menu
 WHERE menu_padre = 'Tesoreria' AND menu_subpadre = 'Fichero' AND menu_hijo = 'Marcas Financieras';
