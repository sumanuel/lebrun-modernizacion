-- Modulo Gerencial, etapa 2 (consultas): deja otra vez sin pantalla las tres opciones.
-- Solo borra la ruta si es la que puso etapa2_consultas_menu.sql.
USE sysconf;

UPDATE conf_menu SET menu_ruta = NULL
 WHERE menu_padre = 'Gerencial' AND menu_subpadre = 'Transacciones'
   AND ((menu_hijo = 'Ofertas'            AND menu_ruta = 'lebrun.formularios.gerencial.lbxOfertas')
     OR (menu_hijo = 'Consulta Cliente'   AND menu_ruta = 'lebrun.formularios.gerencial.lbxConsultaCliente')
     OR (menu_hijo = 'Consulta Proveedor' AND menu_ruta = 'lebrun.formularios.gerencial.lbxConsultaProveedor'));

SELECT idMenu, menu_hijo, menu_ruta FROM conf_menu
 WHERE menu_padre = 'Gerencial' AND menu_subpadre = 'Transacciones'
   AND menu_hijo IN ('Ofertas', 'Consulta Cliente', 'Consulta Proveedor')
 ORDER BY menu_hijo;
