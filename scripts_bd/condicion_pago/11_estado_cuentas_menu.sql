-- Estado de cobro, paso 3: pantalla Estado de Cuentas en el menu.
-- Asigna la pantalla "Estado de cuentas" a la opcion Ventas > Informes > Estado de Cuentas, que estaba sin pantalla.
-- Los perfiles que la ven son los que ya la tenian activa en el mapa de menu (sysconf.confmapamenu).
-- Se puede repetir: solo completa la ruta si esta vacia. Revertir con 11_estado_cuentas_menu_revertir.sql.
USE sysconf;

UPDATE conf_menu SET menu_ruta = 'lebrun.formularios.facturacion.lbxEstadoCuentas'
 WHERE menu_padre = 'Ventas' AND menu_subpadre = 'Informes' AND menu_hijo = 'Estado de Cuentas'
   AND (menu_ruta IS NULL OR menu_ruta IN ('', 'NULL', '(NULL)'));

-- Resultado: la opcion debe mostrar su ruta
SELECT idMenu, menu_padre, menu_subpadre, menu_hijo, menu_ruta FROM conf_menu
 WHERE menu_padre = 'Ventas' AND menu_subpadre = 'Informes' AND menu_hijo = 'Estado de Cuentas';
