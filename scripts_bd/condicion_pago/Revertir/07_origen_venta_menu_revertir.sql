-- Revierte 07_origen_venta_menu.sql: quita la opcion Ventas > Fichero > Origen de la Venta del menu y de los perfiles.
-- Se puede repetir sin problema.
USE sysconf;

DELETE FROM confmapamenu WHERE mmn_menu = 'Fichero: Origen de la Venta' AND mmn_modulo = 'Ventas';
DELETE FROM conf_menu WHERE menu_padre = 'Ventas' AND menu_subpadre = 'Fichero' AND menu_hijo = 'Origen de la Venta';

-- Resultado: debe dar 0 y 0
SELECT COUNT(*) AS filas_conf_menu FROM conf_menu WHERE menu_hijo = 'Origen de la Venta';
SELECT COUNT(*) AS filas_confmapamenu FROM confmapamenu WHERE mmn_menu = 'Fichero: Origen de la Venta';
