-- Revierte 11_estado_cuentas_menu.sql: la opcion Ventas > Informes > Estado de Cuentas vuelve a quedar sin pantalla.
USE sysconf;

UPDATE conf_menu SET menu_ruta = NULL
 WHERE menu_padre = 'Ventas' AND menu_subpadre = 'Informes' AND menu_hijo = 'Estado de Cuentas'
   AND menu_ruta = 'lebrun.formularios.facturacion.lbxEstadoCuentas';

SELECT idMenu, menu_hijo, menu_ruta FROM conf_menu WHERE menu_padre = 'Ventas' AND menu_subpadre = 'Informes' AND menu_hijo = 'Estado de Cuentas';
