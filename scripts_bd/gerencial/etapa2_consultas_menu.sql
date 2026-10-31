-- Modulo Gerencial, etapa 2 (consultas): asigna la pantalla de Ofertas, Consulta Cliente y Consulta Proveedor.
-- Se puede repetir: solo completa las rutas vacias. Revertir con etapa2_consultas_menu_revertir.sql.
USE sysconf;

UPDATE conf_menu SET menu_ruta = 'lebrun.formularios.gerencial.lbxOfertas'
 WHERE menu_padre = 'Gerencial' AND menu_subpadre = 'Transacciones' AND menu_hijo = 'Ofertas'
   AND (menu_ruta IS NULL OR menu_ruta IN ('', 'NULL', '(NULL)'));

UPDATE conf_menu SET menu_ruta = 'lebrun.formularios.gerencial.lbxConsultaCliente'
 WHERE menu_padre = 'Gerencial' AND menu_subpadre = 'Transacciones' AND menu_hijo = 'Consulta Cliente'
   AND (menu_ruta IS NULL OR menu_ruta IN ('', 'NULL', '(NULL)'));

UPDATE conf_menu SET menu_ruta = 'lebrun.formularios.gerencial.lbxConsultaProveedor'
 WHERE menu_padre = 'Gerencial' AND menu_subpadre = 'Transacciones' AND menu_hijo = 'Consulta Proveedor'
   AND (menu_ruta IS NULL OR menu_ruta IN ('', 'NULL', '(NULL)'));

-- Resultado: las tres opciones deben mostrar su ruta
SELECT idMenu, menu_hijo, menu_ruta FROM conf_menu
 WHERE menu_padre = 'Gerencial' AND menu_subpadre = 'Transacciones'
   AND menu_hijo IN ('Ofertas', 'Consulta Cliente', 'Consulta Proveedor')
 ORDER BY menu_hijo;
