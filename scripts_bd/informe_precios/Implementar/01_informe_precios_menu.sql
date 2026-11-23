-- Informe de Precios: pantalla nueva en el menu.
-- Asigna la pantalla "Informe de Precios" a la opcion Inventario > Informes > Informe de Precios, que estaba sin pantalla.
-- Los perfiles que la ven son los que ya la tenian activa en el mapa de menu (sysconf.confmapamenu); a los demas se les da acceso desde la pantalla de perfiles.
-- Se puede repetir: solo completa la ruta si esta vacia. Revertir con Revertir/01_informe_precios_menu_revertir.sql.
USE sysconf;

UPDATE conf_menu SET menu_ruta = 'lebrun.formularios.productos.lbxInformePrecios'
 WHERE menu_padre = 'Inventario' AND menu_subpadre = 'Informes' AND menu_hijo = 'Informe de Precios'
   AND (menu_ruta IS NULL OR menu_ruta IN ('', 'NULL', '(NULL)'));

-- Resultado: la opcion debe mostrar su ruta
SELECT idMenu, menu_padre, menu_subpadre, menu_hijo, menu_ruta FROM conf_menu
 WHERE menu_padre = 'Inventario' AND menu_subpadre = 'Informes' AND menu_hijo = 'Informe de Precios';
