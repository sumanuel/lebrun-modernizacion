-- Modulo Gerencial, etapa 1 (listas simples): deja otra vez sin pantalla las cuatro opciones.
-- Solo borra la ruta si es la que puso etapa1_listas_simples_menu.sql.
USE sysconf;

UPDATE conf_menu SET menu_ruta = NULL
 WHERE menu_padre = 'Gerencial' AND menu_subpadre = 'Transacciones'
   AND ((menu_hijo = 'Articulos Agotados'  AND menu_ruta = 'lebrun.formularios.gerencial.lbxArticulosAgotados')
     OR (menu_hijo = 'Articulos Negativos' AND menu_ruta = 'lebrun.formularios.gerencial.lbxArticulosNegativos')
     OR (menu_hijo = 'Art. Sin Vender'     AND menu_ruta = 'lebrun.formularios.gerencial.lbxArticulosSinVender')
     OR (menu_hijo = 'Precios Cambiados'   AND menu_ruta = 'lebrun.formularios.gerencial.lbxPreciosCambiados'));

SELECT idMenu, menu_hijo, menu_ruta FROM conf_menu
 WHERE menu_padre = 'Gerencial' AND menu_subpadre = 'Transacciones'
   AND menu_hijo IN ('Articulos Agotados', 'Articulos Negativos', 'Art. Sin Vender', 'Precios Cambiados')
 ORDER BY menu_hijo;
