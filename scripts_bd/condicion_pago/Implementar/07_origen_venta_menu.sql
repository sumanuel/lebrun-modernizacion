-- Origen de la venta, paso 2: opcion de menu Ventas > Fichero > Origen de la Venta.
-- Crea la opcion en sysconf.conf_menu y la deja en el mapa de menu de cada perfil (sysconf.confmapamenu): activa solo para ADMINISTRADOR (00001);
-- a los demas perfiles se les da acceso desde la pantalla de perfiles, como con cualquier otra opcion.
-- Se puede repetir: no duplica nada. Revertir con Revertir/07_origen_venta_menu_revertir.sql.
USE sysconf;

INSERT INTO conf_menu (menu_padre, menu_subpadre, menu_hijo, menu_nombre, menu_ruta, menu_antiguo)
SELECT 'Ventas', 'Fichero', 'Origen de la Venta', NULL, 'lebrun.formularios.facturacion.lbxOrigenesVenta', 'Fichero: Origen de la Venta'
  FROM DUAL
 WHERE NOT EXISTS (SELECT 1 FROM conf_menu WHERE menu_padre = 'Ventas' AND menu_subpadre = 'Fichero' AND menu_hijo = 'Origen de la Venta');

-- una fila por perfil, copiando el perfil de una opcion de Ventas > Fichero que ya existe (Clientes)
INSERT INTO confmapamenu (mmn_codigo, mmn_descri, mmn_menu, mmn_activo, mmn_modulo, mmn_formulario, mmn_lectura, mmn_escritura, mmn_modificar, mmn_eliminar)
SELECT m.mmn_codigo, m.mmn_descri, 'Fichero: Origen de la Venta', IF(m.mmn_codigo = '00001', '1', '0'), m.mmn_modulo, NULL, 0, 0, 0, 0
  FROM confmapamenu m
 WHERE m.mmn_menu = 'Fichero: Clientes' AND m.mmn_modulo = 'Ventas'
   AND NOT EXISTS (SELECT 1 FROM confmapamenu x WHERE x.mmn_codigo = m.mmn_codigo AND x.mmn_menu = 'Fichero: Origen de la Venta' AND x.mmn_modulo = m.mmn_modulo);

-- Resultado
SELECT idMenu, menu_padre, menu_subpadre, menu_hijo, menu_ruta, menu_antiguo FROM conf_menu WHERE menu_hijo = 'Origen de la Venta';
SELECT mmn_codigo, mmn_descri, mmn_activo, mmn_modulo FROM confmapamenu WHERE mmn_menu = 'Fichero: Origen de la Venta' ORDER BY mmn_codigo;
