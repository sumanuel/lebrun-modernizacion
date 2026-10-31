-- Modulo Gerencial, etapa 3 (movimientos, ventas y compras): deja otra vez sin pantalla las siete opciones.
-- Solo borra la ruta si es la que puso etapa3_movimientos_ventas_menu.sql.
USE sysconf;

UPDATE conf_menu SET menu_ruta = NULL
 WHERE menu_padre = 'Gerencial' AND menu_subpadre = 'Transacciones'
   AND ((menu_hijo = 'Movimiento Facturacion' AND menu_ruta = 'lebrun.formularios.gerencial.lbxMovimientoFacturacion')
     OR (menu_hijo = 'Movimiento Producto' AND menu_ruta = 'lebrun.formularios.gerencial.lbxMovimientoProducto')
     OR (menu_hijo = 'Movimientos Clientes' AND menu_ruta = 'lebrun.formularios.gerencial.lbxMovimientosClientes')
     OR (menu_hijo = 'Movimiento Productos' AND menu_ruta = 'lebrun.formularios.gerencial.lbxMovimientoItems')
     OR (menu_hijo = 'Productos por Vendedor' AND menu_ruta = 'lebrun.formularios.gerencial.lbxProductosPorVendedor')
     OR (menu_hijo = 'Ventas por Vendedor' AND menu_ruta = 'lebrun.formularios.gerencial.lbxVentasPorVendedor')
     OR (menu_hijo = 'Compras por Proveedor' AND menu_ruta = 'lebrun.formularios.gerencial.lbxComprasPorProveedor'));

SELECT idMenu, menu_hijo, menu_ruta FROM conf_menu
 WHERE menu_padre = 'Gerencial' AND menu_subpadre = 'Transacciones'
   AND menu_hijo IN ('Movimiento Facturacion', 'Movimiento Producto', 'Movimientos Clientes', 'Movimiento Productos', 'Productos por Vendedor', 'Ventas por Vendedor', 'Compras por Proveedor')
 ORDER BY menu_hijo;
