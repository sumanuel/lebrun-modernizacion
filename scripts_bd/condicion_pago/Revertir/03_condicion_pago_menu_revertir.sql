-- Condicion de pago y forma de pago, paso 3 (revertir): deja otra vez sin pantalla Tesoreria > Fichero > Marcas Financieras.
-- Solo borra la ruta si es la que puso 03_condicion_pago_menu.sql.
USE sysconf;

UPDATE conf_menu SET menu_ruta = NULL
 WHERE menu_padre = 'Tesoreria' AND menu_subpadre = 'Fichero' AND menu_hijo = 'Marcas Financieras'
   AND menu_ruta = 'lebrun.formularios.tesoreria.lbxCondicionesPago';

SELECT idMenu, menu_padre, menu_subpadre, menu_hijo, menu_ruta FROM conf_menu
 WHERE menu_padre = 'Tesoreria' AND menu_subpadre = 'Fichero' AND menu_hijo = 'Marcas Financieras';
