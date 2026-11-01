-- Modulo Gerencial, etapa 4 (compras sugeridas): deja otra vez sin pantalla la opcion.
-- Solo borra la ruta si es la que puso etapa4_compras_sugeridas_menu.sql.
USE sysconf;

UPDATE conf_menu SET menu_ruta = NULL
 WHERE menu_padre = 'Gerencial' AND menu_subpadre = 'Transacciones' AND menu_hijo = 'Compras Sugeridas'
   AND menu_ruta = 'lebrun.formularios.gerencial.lbxComprasSugeridas';

SELECT idMenu, menu_hijo, menu_ruta FROM conf_menu
 WHERE menu_padre = 'Gerencial' AND menu_subpadre = 'Transacciones' AND menu_hijo = 'Compras Sugeridas';
