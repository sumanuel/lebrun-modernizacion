-- Modulo Gerencial, etapa 1 (listas simples): asigna la pantalla de cada opcion del menu.
-- Se puede repetir: solo completa las rutas vacias. Revertir con etapa1_listas_simples_menu_revertir.sql.
USE sysconf;

UPDATE conf_menu SET menu_ruta = 'lebrun.formularios.gerencial.lbxArticulosAgotados'
 WHERE menu_padre = 'Gerencial' AND menu_subpadre = 'Transacciones' AND menu_hijo = 'Articulos Agotados'
   AND (menu_ruta IS NULL OR menu_ruta IN ('', 'NULL', '(NULL)'));

UPDATE conf_menu SET menu_ruta = 'lebrun.formularios.gerencial.lbxArticulosNegativos'
 WHERE menu_padre = 'Gerencial' AND menu_subpadre = 'Transacciones' AND menu_hijo = 'Articulos Negativos'
   AND (menu_ruta IS NULL OR menu_ruta IN ('', 'NULL', '(NULL)'));

UPDATE conf_menu SET menu_ruta = 'lebrun.formularios.gerencial.lbxArticulosSinVender'
 WHERE menu_padre = 'Gerencial' AND menu_subpadre = 'Transacciones' AND menu_hijo = 'Art. Sin Vender'
   AND (menu_ruta IS NULL OR menu_ruta IN ('', 'NULL', '(NULL)'));

UPDATE conf_menu SET menu_ruta = 'lebrun.formularios.gerencial.lbxPreciosCambiados'
 WHERE menu_padre = 'Gerencial' AND menu_subpadre = 'Transacciones' AND menu_hijo = 'Precios Cambiados'
   AND (menu_ruta IS NULL OR menu_ruta IN ('', 'NULL', '(NULL)'));

-- Resultado: las cuatro opciones deben mostrar su ruta
SELECT idMenu, menu_hijo, menu_ruta FROM conf_menu
 WHERE menu_padre = 'Gerencial' AND menu_subpadre = 'Transacciones'
   AND menu_hijo IN ('Articulos Agotados', 'Articulos Negativos', 'Art. Sin Vender', 'Precios Cambiados')
 ORDER BY menu_hijo;
