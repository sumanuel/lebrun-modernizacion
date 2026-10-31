-- Modulo Gerencial, etapa 3 (movimientos, ventas y compras): asigna las siete pantallas.
-- Se puede repetir: solo completa las rutas vacias. Revertir con etapa3_movimientos_ventas_menu_revertir.sql.
USE sysconf;

UPDATE conf_menu SET menu_ruta = 'lebrun.formularios.gerencial.lbxMovimientoFacturacion'
 WHERE menu_padre = 'Gerencial' AND menu_subpadre = 'Transacciones' AND menu_hijo = 'Movimiento Facturacion'
   AND (menu_ruta IS NULL OR menu_ruta IN ('', 'NULL', '(NULL)'));

UPDATE conf_menu SET menu_ruta = 'lebrun.formularios.gerencial.lbxMovimientoProducto'
 WHERE menu_padre = 'Gerencial' AND menu_subpadre = 'Transacciones' AND menu_hijo = 'Movimiento Producto'
   AND (menu_ruta IS NULL OR menu_ruta IN ('', 'NULL', '(NULL)'));

UPDATE conf_menu SET menu_ruta = 'lebrun.formularios.gerencial.lbxMovimientosClientes'
 WHERE menu_padre = 'Gerencial' AND menu_subpadre = 'Transacciones' AND menu_hijo = 'Movimientos Clientes'
   AND (menu_ruta IS NULL OR menu_ruta IN ('', 'NULL', '(NULL)'));

UPDATE conf_menu SET menu_ruta = 'lebrun.formularios.gerencial.lbxMovimientoItems'
 WHERE menu_padre = 'Gerencial' AND menu_subpadre = 'Transacciones' AND menu_hijo = 'Movimiento Productos'
   AND (menu_ruta IS NULL OR menu_ruta IN ('', 'NULL', '(NULL)'));

UPDATE conf_menu SET menu_ruta = 'lebrun.formularios.gerencial.lbxProductosPorVendedor'
 WHERE menu_padre = 'Gerencial' AND menu_subpadre = 'Transacciones' AND menu_hijo = 'Productos por Vendedor'
   AND (menu_ruta IS NULL OR menu_ruta IN ('', 'NULL', '(NULL)'));

UPDATE conf_menu SET menu_ruta = 'lebrun.formularios.gerencial.lbxVentasPorVendedor'
 WHERE menu_padre = 'Gerencial' AND menu_subpadre = 'Transacciones' AND menu_hijo = 'Ventas por Vendedor'
   AND (menu_ruta IS NULL OR menu_ruta IN ('', 'NULL', '(NULL)'));

UPDATE conf_menu SET menu_ruta = 'lebrun.formularios.gerencial.lbxComprasPorProveedor'
 WHERE menu_padre = 'Gerencial' AND menu_subpadre = 'Transacciones' AND menu_hijo = 'Compras por Proveedor'
   AND (menu_ruta IS NULL OR menu_ruta IN ('', 'NULL', '(NULL)'));

-- Resultado: las siete opciones deben mostrar su ruta
SELECT idMenu, menu_hijo, menu_ruta FROM conf_menu
 WHERE menu_padre = 'Gerencial' AND menu_subpadre = 'Transacciones'
   AND menu_hijo IN ('Movimiento Facturacion', 'Movimiento Producto', 'Movimientos Clientes', 'Movimiento Productos', 'Productos por Vendedor', 'Ventas por Vendedor', 'Compras por Proveedor')
 ORDER BY menu_hijo;
