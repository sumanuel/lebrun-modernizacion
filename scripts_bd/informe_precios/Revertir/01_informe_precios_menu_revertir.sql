-- Revierte 01_informe_precios_menu.sql: la opcion Inventario > Informes > Informe de Precios vuelve a quedar sin pantalla.
USE sysconf;

UPDATE conf_menu SET menu_ruta = NULL
 WHERE menu_padre = 'Inventario' AND menu_subpadre = 'Informes' AND menu_hijo = 'Informe de Precios'
   AND menu_ruta = 'lebrun.formularios.productos.lbxInformePrecios';

SELECT idMenu, menu_hijo, menu_ruta FROM conf_menu WHERE menu_padre = 'Inventario' AND menu_subpadre = 'Informes' AND menu_hijo = 'Informe de Precios';
