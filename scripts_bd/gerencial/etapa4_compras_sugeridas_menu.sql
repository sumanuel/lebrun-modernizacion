-- Modulo Gerencial, etapa 4 (compras sugeridas): asigna la pantalla de Compras Sugeridas.
-- Se puede repetir: solo completa la ruta si esta vacia. Revertir con etapa4_compras_sugeridas_menu_revertir.sql.
USE sysconf;

UPDATE conf_menu SET menu_ruta = 'lebrun.formularios.gerencial.lbxComprasSugeridas'
 WHERE menu_padre = 'Gerencial' AND menu_subpadre = 'Transacciones' AND menu_hijo = 'Compras Sugeridas'
   AND (menu_ruta IS NULL OR menu_ruta IN ('', 'NULL', '(NULL)'));

-- Resultado: la opcion debe mostrar su ruta
SELECT idMenu, menu_hijo, menu_ruta FROM conf_menu
 WHERE menu_padre = 'Gerencial' AND menu_subpadre = 'Transacciones' AND menu_hijo = 'Compras Sugeridas';
