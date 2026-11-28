# -*- coding: utf-8 -*-
from docx import Document
from docx.shared import Pt, RGBColor, Cm
from docx.enum.table import WD_TABLE_ALIGNMENT
from docx.oxml.ns import qn
from docx.oxml import OxmlElement

import os
import sys

# Genera el documento "Estado de avance" que se entrega al cliente (requiere: pip install python-docx).
# Uso:   python instalador\generar_estado_avance.py [ruta\salida.docx]
# Por omision lo deja en Downloads\LEBRUN SILVANA\Informes\ (de donde lo toma armar_entrega.ps1).
# El contenido (tablas, pendientes, fecha) esta escrito en este archivo: hay que editarlo y volver a generar el documento
# cada vez que cambia el estado del trabajo o la version del instalador.
RUTA = sys.argv[1] if len(sys.argv) > 1 else os.path.join(
    os.environ["USERPROFILE"], "Downloads", "LEBRUN SILVANA", "Informes", "Estado_de_Avance_Condicion_y_Forma_de_Pago.docx")
AZUL = RGBColor(0x31, 0x6A, 0xC5)
GRIS = RGBColor(0x54, 0x5E, 0x6E)

d = Document()
sec = d.sections[0]
sec.left_margin = sec.right_margin = Cm(2.2)
sec.top_margin = sec.bottom_margin = Cm(2)
st = d.styles["Normal"]
st.font.name = "Arial"
st.font.size = Pt(10)
st.element.rPr.rFonts.set(qn("w:eastAsia"), "Arial")
st.paragraph_format.space_after = Pt(4)
for nombre, tam in (("Heading 1", 14), ("Heading 2", 11.5)):
    h = d.styles[nombre]
    h.font.name = "Arial"
    h.font.size = Pt(tam)
    h.font.bold = True
    h.font.color.rgb = AZUL
    h.element.rPr.rFonts.set(qn("w:eastAsia"), "Arial")
    h.paragraph_format.space_before = Pt(12)
    h.paragraph_format.space_after = Pt(4)


def p(texto, negrita=False, cursiva=False, color=None, tam=None):
    par = d.add_paragraph()
    r = par.add_run(texto)
    r.bold = negrita
    r.italic = cursiva
    if color:
        r.font.color.rgb = color
    if tam:
        r.font.size = Pt(tam)
    return par


def punto(texto, etiqueta=None):
    par = d.add_paragraph(style="List Bullet")
    if etiqueta:
        r = par.add_run(etiqueta + " ")
        r.bold = True
    par.add_run(texto)
    par.paragraph_format.space_after = Pt(2)
    return par


def sombrear(celda, color):
    tcPr = celda._tc.get_or_add_tcPr()
    shd = OxmlElement("w:shd")
    shd.set(qn("w:val"), "clear")
    shd.set(qn("w:color"), "auto")
    shd.set(qn("w:fill"), color)
    tcPr.append(shd)


def tabla(encabezados, filas, anchos, columna_estado=None):
    t = d.add_table(rows=1, cols=len(encabezados))
    t.style = "Table Grid"
    t.alignment = WD_TABLE_ALIGNMENT.CENTER
    for i, e in enumerate(encabezados):
        c = t.rows[0].cells[i]
        c.text = ""
        r = c.paragraphs[0].add_run(e)
        r.bold = True
        r.font.size = Pt(9)
        r.font.color.rgb = RGBColor(0xFF, 0xFF, 0xFF)
        sombrear(c, "316AC5")
    for fila in filas:
        celdas = t.add_row().cells
        for i, v in enumerate(fila):
            celdas[i].text = ""
            r = celdas[i].paragraphs[0].add_run(v)
            r.font.size = Pt(9)
            if columna_estado is not None and i == columna_estado:
                r.bold = True
                if v.startswith("Hecho"):
                    sombrear(celdas[i], "DDEFD9")
                elif v.startswith("Pendiente"):
                    sombrear(celdas[i], "FFF1CC")
    for fila in t.rows:
        for i, a in enumerate(anchos):
            fila.cells[i].width = Cm(a)
    d.add_paragraph().paragraph_format.space_after = Pt(2)
    return t


# ---------------- portada ----------------
t = d.add_paragraph()
r = t.add_run("Estado de avance")
r.bold = True
r.font.size = Pt(20)
r.font.color.rgb = AZUL
p("Condición de pago, forma de pago, mejoras en facturación y revisión de compras — Sistema Lebrun", color=GRIS, tam=11)
p("Fecha: 27/11/2026. Resume lo pedido en las observaciones del cliente del 10/11/2026, en las respuestas posteriores (incluidos los puntos 15 y 16) y en los informes de pruebas del 20/11/2026 y del 27/11/2026, "
  "además de la revisión de dos pantallas de Compras: lo que ya está hecho, lo que falta y lo que necesita hacer o confirmar el cliente. Las pruebas se hicieron en una copia local de la base de datos (MariaDB) "
  "y en una copia de producción de solo lectura; no se probó con una impresora fiscal ni con el servidor de producción.", cursiva=True, color=GRIS, tam=9)

# ---------------- resumen ----------------
d.add_heading("1. Resumen", 1)
p("Lo pedido en las observaciones 1 a 16 y en las pruebas del 20/11 y del 27/11 está programado, probado en el ambiente de pruebas e incluido en el instalador Lebrun_Setup_1.2.2_x86.exe, salvo dos puntos del 27/11 "
  "que necesitan una respuesta del cliente (buscar por fecha los precios en dólares y los listados de inventario con costos). Además se revisaron y corrigieron dos pantallas de Compras: la Orden de compra para "
  "Productos y Otras Cuentas x Pagar. Lo que queda es aplicarlo en el ambiente del cliente (primero el script de base de datos y después el instalador), hacer las pruebas y responder las preguntas de la sección 3.")
punto("las versiones anteriores tienen fallas en estas dos pantallas de Compras. Si se cargaron documentos reales con ellas, conviene revisarlos (ver la sección 3.1): la corrección no cambia lo que ya está guardado.", "Importante:")
punto("el reporte del cuadre de caja ya no sale en blanco; la búsqueda de precios en bolívares por fecha ya trae resultados; en la factura se ve la sigla de la forma de pago; el Informe de Precios muestra el proveedor en el título; "
      "y el Estado de cuentas se puede imprimir.", "Pruebas del 27/11:")
punto("Una sola tabla de condiciones y formas de pago, con siglas y pantalla de mantenimiento; Contado es el código 07.", "Condición y forma de pago:")
punto("origen de la venta, tasa de cambio en cada factura, monto cobrado cuando el pago es parcial y estado de cobro (Pagado, Parcial, Pendiente).", "Observaciones 7 a 10:")
punto("el monto original en dólares o euros se guarda junto con la tasa usada; y hay una pantalla nueva de Estado de cuentas.", "Además:")
punto("la tasa de cambio se guarda con hasta 8 decimales, como la publica el BCV.", "Observación 15:")
punto("en caja se piden el correo y la fecha de nacimiento del cliente (no son obligatorios), y la pantalla de Clientes tiene el campo nuevo de fecha de nacimiento.", "Observación 16:")
punto("se corrigieron el monto extra junto al precio, el monto oculto al cancelar la factura y el origen al ver una factura; se corrigió la causa del cierre del programa con las facturas de contado; "
      "el Estado de cuentas quedó como el del sistema anterior; y hay un Informe de Precios nuevo, en dólares y en bolívares por separado.", "Pruebas del 20/11:")

# ---------------- lo solicitado ----------------
d.add_heading("2. Lo solicitado y cómo quedó", 1)
tabla(
    ["Solicitud del cliente", "Cómo quedó", "Estado"],
    [
        ["Una sola tabla de condición de pago; el código 07 es Contado con 0 días y se graba en clientes y facturas; las facturas antiguas se actualizan; la fila «Contado» se elimina",
         "La tabla admcondpago es la única. Un script pasa los clientes y facturas con «Contado» al código 07 y elimina la fila duplicada, guardando copia de lo que cambia. "
         "La mora de la fila «Contado» (12 días en la copia local, 5 en la de producción) se pasó al código 07, como indicó el cliente.", "Hecho"],
        ["Siglas de 3 a 4 letras en cada registro, para los reportes",
         "Cada condición y forma de pago tiene siglas (por ejemplo TD = Tarjeta de Débito); no se pueden repetir.", "Hecho"],
        ["Mostrar por omisión 07; cambiarlo solo con clave de supervisión; solo condiciones activas",
         "La ficha del cliente y la factura traen 07; para cambiarlo pide clave de supervisión. Solo se ofrecen las condiciones activas.", "Hecho"],
        ["Formas de pago nuevas (Divisa $, Divisa €, Cachea) en la misma tabla y por separado en el Resumen General del cuadre de caja, si tienen movimiento",
         "Las formas de pago salen de la tabla en los cinco formularios de venta. Las nuevas aparecen en el cuadre en filas aparte solo si tuvieron movimiento.", "Hecho"],
        ["El vuelto (CAMBIO) no es una forma de pago y no sale en el cuadre",
         "Se sigue grabando junto a los pagos de la factura, pero no se suma ni se muestra en el cuadre.", "Hecho"],
        ["Mantenimiento en Tesorería > Fichero > Marcas Financieras",
         "Pantalla «Condiciones y formas de pago»: crear, modificar y desactivar (nada se borra), con siglas, días, mora, si pide banco y la moneda.", "Hecho"],
        ["7. Origen de la venta (tienda, Instagram, Facebook, WhatsApp, ...)",
         "Tabla de orígenes con su pantalla (Ventas > Fichero > Origen de la Venta). La factura trae un combo «Origen» con Tienda física por omisión y lo guarda; el cajero solo lo cambia si la venta vino por otro medio "
         "(Instagram, Facebook, WhatsApp u otro que se agregue en la tabla). Se guarda en una columna nueva (dcli_origenventa) y no en dcli_expexp, porque "
         "ese campo guarda el estado de la factura al facturarla (Pagado o Activo) y el cuadre de caja lo lee; usarlo para el origen descuadraría la caja. "
         "Diferencia con lo pedido: el origen no queda vacío en las ventas de tienda, sino marcado «Tienda física».", "Hecho"],
        ["8. Grabar el valor del dólar del día en la factura (dcli_valcamb)",
         "Cada factura nueva graba la tasa del dólar BCV del día; si ese día no hay tasa usa la última anterior; si no existe ninguna, queda vacío.", "Hecho"],
        ["9. Pago combinado: parte en tarjeta y parte a crédito (MontoTotal, sal_actual, MontoCob)",
         "MontoCob guarda lo realmente cobrado; los recibos de ingreso y las retenciones lo van actualizando. Un script corrige los documentos abiertos.", "Hecho"],
        ["10. Mejorar los estados Pagado / Activo",
         "Estado de cobro en dcli_estatus: Pagado, Parcial o Pendiente. dcli_estado y dcli_expexp no se tocaron. Se ve en el listado de facturas y devoluciones, en el "
         "Estado de cuentas y en el detalle del cuadre de caja (PEND / PARC). La factura 000001496756 quedó corregida.", "Hecho"],
        ["Guardar el monto original en Divisa $ y Divisa €",
         "El cajero escribe lo recibido en dólares o euros; el programa lo convierte a bolívares con la tasa del día y guarda el monto original, la moneda y la tasa.", "Hecho"],
        ["Ventas > Informes > Estado de Cuentas (la opción del menú no tenía pantalla)",
         "Pantalla nueva, como la del sistema anterior. El campo del cliente funciona como el del producto en la Consulta Gerencial: se escribe el código (sirve también una parte del código, el nombre o el RIF) "
         "y al dar Enter, si el cliente existe, se completa el código y aparece su nombre en verde; la lupa abre la lista de clientes para elegirlo (si el texto coincide con varios, sale una lista corta). Se ven sus datos aunque no tenga documentos pendientes. "
         "Tiene dos pestañas: «Estado de cuenta», con los documentos pendientes (monto, cobrado, saldo, estado de cobro y lo que está en mora), y «Histórico de movimientos», con las facturas, devoluciones, "
         "recibos de ingreso y retenciones de IVA del cliente aunque ya no estén pendientes, con el documento al que se aplican y el número fiscal; filtros por fechas y totales. "
         "Los recibos salen con la fecha real del recibo (no con la de la factura). Sin elegir cliente, la primera pestaña lista los pendientes de todos los clientes.", "Hecho"],
        ["Correcciones de las pruebas del 20/11: monto extra junto al precio del producto; monto oculto al cancelar la factura; origen que no se mostraba al ver una factura",
         "Ya no sale el monto extra junto al precio (era un cálculo antiguo, precio más 8%). En la ventana de pago la columna Divisa pasó al final y el monto ya se ve. "
         "Al abrir una factura ya emitida ahora se muestra el origen que se guardó (por ejemplo WhatsApp).", "Hecho"],
        ["El programa se cierra con las facturas de contado (cuando se cancela toda la factura)",
         "Windows registró un error de memoria al hablar con la librería de la impresora fiscal (PNPDLL.dll), que solo usa el contado. Se corrigió la forma de leer las respuestas de esa librería. "
         "No se pudo reproducir el cierre en el ambiente de pruebas, así que falta que se pruebe una factura de contado con la versión nueva.", "Pendiente de confirmar"],
        ["Reporte de precios en dólares y reporte de precios en bolívares, por separado (Inventario > Informes > Informe de Precios)",
         "Pantalla nueva con los precios en dólares (el precio de cada producto en adminv2) o en bolívares (por lista de precio, con fecha, hora y usuario del último cambio). Filtros: proveedor, con precio / sin precio, "
         "lista de precio, incluir inactivos y, en bolívares, cambiados desde una fecha y hora (para el cierre de la facturación). Se puede imprimir y guardar en un archivo que abre Excel.", "Hecho"],
        ["15. Tasa de cambio con más decimales (de 4 a 6, y si se puede 8 como el BCV)",
         "Es posible y quedó con 8 decimales: el historial de divisas (admhistodivi), la tasa que se graba en cada factura y la tasa de los pagos en divisa. Los valores que ya existían no cambian "
         "(36,50 pasa a 36,50000000). Los precios en bolívares y la conversión de los pagos en divisa usan los 8 decimales; los montos en bolívares se siguen redondeando a 2. "
         "No se cambiaron las tasas de compras, pagos, bancos e impuestos. Las importaciones ya admitían hasta 10 decimales (su tasa se escribe a mano) y no se modificaron.", "Hecho"],
        ["16. Pedir la fecha de nacimiento y el correo del cliente en caja; avisar a la cajera si falta alguno",
         "Al elegir el cliente en una factura de venta (no en devoluciones), si le falta el correo o la fecha de nacimiento sale un aviso a la cajera con los dos campos: «Guardar» los graba en el cliente y «Omitir» sigue con la factura. "
         "Ninguno es obligatorio y el aviso se repite en la siguiente factura mientras falte alguno. El correo se valida (nombre@dominio.com) y la fecha no puede ser futura. "
         "La pantalla de Clientes tiene el campo nuevo «F. Nacimiento» junto al E-mail. Como no existía un campo de fecha de nacimiento se creó uno nuevo (cli_fechanac) y no se usó cli_finvaca, "
         "porque ese campo es el fin de las vacaciones y se muestra con ese nombre en la ficha del cliente. Aplica a las dos pantallas de factura; el cliente genérico no se excluye "
         "(si no tiene datos, la cajera puede registrarle un correo ficticio).", "Hecho"],
    ],
    [5.6, 9.1, 2.0],
    columna_estado=2,
)

# ---------------- pruebas del 27/11 ----------------
d.add_heading("2.1 Informe de pruebas del 27/11/2026", 2)
tabla(
    ["Lo reportado", "Cómo quedó", "Estado"],
    [
        ["El reporte del cuadre de caja se abre en blanco",
         "Corregido (el reporte se preparaba en un proceso interno que no podía mostrarlo). Sale cada factura con su número interno, número fiscal, RIF, cliente, IVA, monto y siglas, los subtotales y el total por forma de pago. "
         "Si el cierre falla, ahora avisa y se detiene en lugar de volver a intentarlo sin fin. Los asientos del cierre ya se grababan bien.", "Hecho"],
        ["En la factura debe verse la sigla de la forma de pago y no el código (salía F05)",
         "La columna muestra la sigla. Es solo lo que se ve: se sigue guardando el código.", "Hecho"],
        ["Informe de Precios en bolívares: al buscar por fecha no trae nada",
         "Corregido. La hora del cambio se guarda según la configuración de Windows (por ejemplo «11:25:19 a. m.») y el programa no entendía esa forma. Al marcar «Cambiados desde» ahora propone el comienzo del día.", "Hecho"],
        ["Informe de Precios: el título con el código y el nombre del proveedor, sin repetir la columna",
         "El proveedor funciona como el campo del producto en la Consulta Gerencial (código y Enter, o la lupa) y sale en el título. Con todos los proveedores, los productos salen agrupados por proveedor. "
         "La columna ya no sale en la pantalla, en la impresión ni en el archivo.", "Hecho"],
        ["Estado de cuentas: falta el reporte de los documentos pendientes y de los movimientos",
         "Botón Imprimir con vista previa: imprime la pestaña que se está viendo, con la compañía, los datos del cliente, los filtros y los totales.", "Hecho"],
        ["Retención de IVA: parece que no rebaja el saldo del cliente",
         "Se verificó que sí lo rebaja, y también actualiza el saldo y el estado de cobro de la factura. Para la factura FAV 000001653365 el saldo esperado es 22.203,81 si no tenía otros abonos. No hubo cambios en el programa.", "Hecho"],
        ["Informe de Precios en dólares: no deja buscar por fecha y hora",
         "El precio en dólares no guarda cuándo cambió. Para buscar así habría que empezar a guardar la fecha (cambio en la base de datos); los cambios anteriores no tendrían fecha.", "Pendiente de respuesta"],
        ["Usar el Informe de Precios como base de los listados de inventario, unos con precios y otros con costos",
         "Falta definir las columnas y los filtros de cada listado y revisar su alcance.", "Pendiente de respuesta"],
    ],
    [5.6, 9.1, 2.0],
    columna_estado=2,
)

# ---------------- revision de compras ----------------
d.add_heading("2.2 Revisión de dos pantallas de Compras", 2)
tabla(
    ["Pantalla", "Qué pasaba y cómo quedó", "Estado"],
    [
        ["Compras > Transacciones > Orden de compra para Productos",
         "Una orden se podía perder al modificarla (por ejemplo con un apóstrofo en las observaciones), se perdía la condición de pago y, con dos descuentos del proveedor, solo se aplicaba el último. Ahora la orden se graba completa o no se graba, "
         "conserva la condición, aplica los descuentos en cascada (el segundo sobre lo que queda del primero), los guarda con la orden y los muestra en la pantalla y en el impreso. Para proveedores en dólares o euros propone la tasa del BCV de la fecha "
         "de la orden. También se corrigieron los botones que daban error, la lista que no se actualizaba, el producto repetido (ahora se suma) y la búsqueda por código de proveedor. Requiere el script nuevo.", "Hecho"],
        ["Compras > Transacciones > Otras Cuentas x Pagar: errores en los montos y al guardar",
         "La tasa de IVA se tomaba según el orden de la tabla: con las tasas actuales, una factura de 116,00 quedaba con base 88,55 e IVA 27,45, y ahora queda 100,00 y 16,00. Un apóstrofo en la descripción grababa la contabilidad pero no el documento; "
         "ahora el documento se graba completo o no se graba. La nota de crédito aumentaba la deuda; ahora la rebaja. Se perdía la condición de pago y el vencimiento no sumaba sus días. "
         "Con un proveedor sin cuenta contable no se podía cargar; ahora el programa ofrece crearla.", "Hecho"],
        ["Otras Cuentas x Pagar: otras correcciones",
         "Ya se pueden cargar préstamos, giros y pagarés. Funcionan la búsqueda por fecha y la actualización de la lista; la contabilidad guarda el RIF del proveedor; se actualiza su última compra; no salen errores con la lista vacía; "
         "al modificar se ven los rótulos correctos. Al salir de las opciones ya no se anulan las retenciones: quedan pendientes y se generan después. "
         "El botón «Islr» (retención sobre un documento ya cargado) ahora marca el libro de ISLR, contabiliza lo retenido y muestra el comprobante.", "Hecho"],
    ],
    [4.2, 10.5, 2.0],
    columna_estado=2,
)

# ---------------- pendiente ----------------
d.add_heading("3. Lo que falta", 1)
d.add_heading("3.1 Por parte del cliente", 2)
tabla(
    ["Qué", "Detalle", "Estado"],
    [
        ["Revisar lo cargado antes en Otras Cuentas x Pagar y en la Orden de compra", "Solo si se usaron con datos reales en una versión anterior. En Otras Cuentas x Pagar: el IVA y la base, las notas de crédito (quedaban sumando a la deuda), la condición de pago y los documentos con apóstrofo (pueden tener contabilidad sin documento). "
         "En las órdenes de compra: las que tenían dos o tres descuentos. La versión nueva no cambia lo ya guardado.", "Pendiente"],
        ["Probar lo corregido el 27/11", "El cuadre de caja en su equipo (con su vista previa), la búsqueda de precios en bolívares por fecha, el Informe de Precios por proveedor, la impresión del Estado de cuentas y las dos pantallas de Compras. La impresión no se envió a una impresora física.", "Pendiente"],
        ["Confirmar el saldo de la factura FAV 000001653365", "Debe ser 22.203,81 si no tenía otros abonos.", "Pendiente"],
        ["Vuelto de un pago en divisa: ¿se entrega en bolívares o en la misma divisa?", "Hoy el cuadre muestra en la columna de la divisa el neto del documento y no descuenta el vuelto del efectivo.", "Pendiente"],
        ["Listados de inventario con precios y con costos", "Indicar las columnas y los filtros de cada listado.", "Pendiente"],
        ["Precios en dólares por fecha y hora: ¿se quiere empezar a guardar la fecha del cambio?", "Requiere un cambio en la base de datos. Los cambios anteriores no tendrían fecha.", "Pendiente"],
        ["Orden de compra: ¿la usan?", "Si la usan: confirmar que para proveedores en dólares o euros la tasa es la del BCV de la fecha de la orden, e indicar en qué moneda se cargan los costos de los productos que se compran en dólares o euros (el programa no los convierte).", "Pendiente"],
        ["Otras Cuentas x Pagar: confirmar cómo debe comportarse", "(a) Con una retención pendiente se puede salir y generarla después (el sistema anterior obliga a generarla antes de salir; ya no se ofrece anularla desde esta pantalla). (b) Antes de crear la cuenta de un proveedor, el programa pregunta (el anterior la crea sin preguntar). "
         "(c) El vencimiento se cuenta desde hoy, como en el sistema anterior. (d) La pantalla exige que exista la factura a la que se aplica el documento; en la copia de producción hay 8 anticipos de 2026 que no cumplen eso: ¿se deben permitir?", "Pendiente"],
        ["Cobrar en divisa con una serie fiscal real", "En el ambiente de pruebas el botón «Agregar» de facturación no se habilita sin serie fiscal. Falta confirmar el cobro en Divisa $ y Divisa € de principio a fin (la conversión y el guardado sí se probaron).", "Pendiente"],
        ["Indicar cómo se carga la tasa en el historial de divisas", "Este programa no escribe en esa tabla: la tasa la carga otro programa o se escribe a mano en la base. Si ese programa la redondea a 2 decimales, hay que ajustarlo para que la escriba completa; si no, ampliar la tabla no sirve de nada.", "Pendiente"],
        ["Importaciones: ¿se quiere que la tasa se proponga desde el historial?", "Hoy la tasa se escribe a mano en la orden de compra de importaciones y en el proveedor solo se asigna el código de la moneda. No se cambió; si se quiere que el programa proponga la tasa del historial (editable), hay que pedirlo.", "Pendiente"],
        ["Probar una factura de contado", "El informe del 27/11 no la menciona. Debe cancelarse toda la factura y confirmar que el programa ya no se cierra. Si se cerrara otra vez, se necesita el error de Windows (Visor de eventos) de esa hora.", "Pendiente"],
        ["Probar el aviso de datos de contacto desde la factura", "El botón «Agregar» de facturación no se habilita sin serie fiscal en el ambiente de pruebas. El aviso se probó por separado con la ventana real y la pantalla de Clientes se revisó en el programa; falta verlo al elegir un cliente en una factura real.", "Pendiente"],
        ["Consulta de ventas por origen", "El origen ya se guarda en cada factura. Si se quiere una consulta o reporte por origen, hay que pedirla.", "Pendiente"],
    ],
    [4.4, 10.3, 2.0],
    columna_estado=2,
)
d.add_heading("3.2 Anotado para una etapa posterior", 2)
punto("El pago parcial a crédito sigue contando como crédito en los totales del cuadre de caja (decisión del cliente: por ahora no se refleja).")
punto("El monto original en divisa no aparece en el cuadre de caja ni al abrir una factura ya emitida; el cuadre sigue sumando bolívares.")
punto("No se probó con datos de Ferle (no están en el ambiente de pruebas); el cliente indicó que solo Lebrun está en producción.")
punto("La factura 000001496756 tiene neto 0,00 y saldo 15.457.956,80; se dejó así por ser datos de prueba.")
punto("Si alguna factura ya tiene la tasa grabada con 2 decimales, la conserva así; solo las facturas nuevas la graban con todos sus decimales.")
punto("No se revisaron los reportes ni todas las pantallas que muestran la tasa; donde se muestra sin formato ahora aparecen todos sus decimales.")
punto("En Otras Cuentas x Pagar el documento se graba completo o no se graba, pero las retenciones que se generan después con sus botones todavía no se graban de esa forma.")

# ---------------- entrega ----------------
d.add_heading("4. Entrega e instalación", 1)
p("Todo lo anterior está integrado en el programa principal y en el instalador. Se entregan dos archivos: el instalador Lebrun_Setup_1.2.2_x86.exe y el paquete de scripts Scripts_BD_1.2.2.zip. "
  "Falta aplicarlos en el ambiente del cliente. Si ya tiene instalada la versión 1.2.1, el instalador la actualiza conservando el servidor y la configuración.")
p("Scripts de base de datos. En esta entrega el paquete Scripts_BD_1.2.2.zip trae SOLO el script nuevo de esta versión: orden_compra/01 (agrega a la orden de compra los porcentajes de descuento y amplía su tasa a 8 decimales). "
  "Las demás mejoras de esta entrega no necesitan scripts. Los scripts de las versiones anteriores ya se entregaron (con la 1.2.0 y la 1.2.1) y no hay que volver a ejecutarlos; la tabla siguiente los lista como referencia "
  "(cada carpeta tiene Implementar, con lo que se ejecuta, y Revertir, con lo que lo deshace; se pueden ejecutar más de una vez). Los números 01 a 11 son de la carpeta condicion_pago:", negrita=True)
tabla(
    ["N.º", "Para qué sirve"],
    [
        ["01", "Agrega el tipo (condición o forma de pago) y las siglas a la tabla."],
        ["02", "Pasa «Contado» al código 07 en clientes y facturas y elimina la fila duplicada. Hacer respaldo antes."],
        ["03", "Pone la pantalla de mantenimiento en Tesorería > Fichero > Marcas Financieras."],
        ["04", "Crea las formas de pago (Efectivo, Tarjetas, Cheque, Divisa $, Divisa €, Cachea)."],
        ["05", "Corrige el monto cobrado de los documentos abiertos con saldo."],
        ["06 y 07", "Origen de la venta: tabla y columna en la factura, y la opción de menú."],
        ["08", "Llena el estado de cobro de los documentos existentes (después del 05)."],
        ["09", "Corrige los estados de la factura 000001496756."],
        ["10", "Agrega las columnas para el monto original en divisa y la moneda de cada forma de pago."],
        ["11", "Pone la pantalla de Estado de cuentas en el menú."],
        ["Contacto 01", "(carpeta clientes_contacto) Agrega la fecha de nacimiento del cliente (admclientes.cli_fechanac). Ejecutarlo ANTES de instalar la versión nueva: cada cliente que se carga lee ese campo."],
        ["Tasa 01", "(carpeta tasa_cambio) Amplía a 8 decimales la tasa del historial de divisas, de las facturas y de los pagos en divisa. Reconstruye las tablas admdoccli y admmovcaja: ejecutarlo fuera del horario de trabajo."],
        ["Informe 01", "(carpeta informe_precios) Pone la pantalla Informe de Precios en el menú Inventario > Informes. No cambia tablas, solo el menú."],
        ["Orden 01 (nuevo)", "(carpeta orden_compra) Agrega los porcentajes de descuento a la orden de compra y amplía su tasa de cambio a 8 decimales. Es el único de esta entrega."],
    ],
    [2.6, 14.1],
)
punto("Hacer un respaldo y ejecutar el script nuevo ANTES de instalar la versión nueva del programa: sin él las órdenes de compra no se pueden grabar, ver ni imprimir. "
      "Si la base todavía no tiene los scripts anteriores (por ejemplo una instalación nueva), hay que pedir el paquete completo.")
punto("Esta versión no agrega opciones al menú.")
punto("Los perfiles que no sean ADMINISTRADOR necesitan que se les active la opción «Origen de la Venta» en la pantalla de perfiles; «Estado de Cuentas» e «Informe de Precios» las ven los perfiles que ya las tenían activas.")
punto("Para cobrar en divisa debe haber una tasa del dólar o del euro cargada; si no la hay, el programa no deja cobrar en esa divisa y lo avisa.")

# ---------------- como se probo ----------------
d.add_heading("5. Cómo se probó", 1)
punto("Cada script se aplicó, se repitió, se revirtió y se aplicó otra vez en la base local, comprobando el resultado.")
punto("Facturación, cobros, recibos, retenciones, tasas y pagos en divisa se probaron con programas de prueba que generan las sentencias reales y las ejecutan; los datos de prueba se borraron.")
punto("Las pantallas nuevas (mantenimiento, origen de la venta, estado de cuentas, listado con la columna Cobro) se abrieron y revisaron en el programa real.")
punto("Puntos 15 y 16: los dos scripts se aplicaron, se repitieron, se revirtieron (con copia de respaldo) y se aplicaron otra vez. La tasa se probó con valores de 8 decimales (dólar 36,12345678 y euro 39,87654321): "
      "la tasa vigente y la conversión de pagos en divisa usan los 8 decimales. El alta, la carga y la modificación del cliente con y sin fecha de nacimiento, y el guardado del correo, se probaron con un programa de prueba; "
      "la ventana del aviso de caja se probó en el programa real (vacío y correo inválido se rechazan; uno válido guarda y cierra); los datos de prueba se borraron.")
punto("La copia de producción (solo lectura) sirvió para validar las reglas con datos reales, por ejemplo el estado de cobro de los 177.722 documentos.")
punto("Pruebas del 20/11: el orden de las columnas de la ventana de pago se comprobó con el formulario real. El Estado de cuentas se probó en el programa real con los datos de un cliente de la copia de producción: "
      "los movimientos coinciden con los del sistema anterior (por ejemplo una devolución con su número fiscal, su retención de IVA y el recibo de la factura). También se probaron un cliente sin pendientes, "
      "varios clientes con el mismo nombre, un cliente que no existe y la búsqueda sin cliente. El Informe de Precios se probó en dólares y en bolívares, con filtro por proveedor, vista previa de impresión y archivo para Excel, "
      "y sus consultas se validaron con los 19.768 productos activos de la copia de producción. Los datos de prueba se borraron.")
punto("Cierre con facturas de contado: la causa se encontró con el error que envió el cliente (Windows registró corrupción de memoria al usar la librería de la impresora fiscal) y se comprobó que la librería devuelve las respuestas "
      "de una forma que el programa no leía bien. En el ambiente de pruebas no se cierra ni antes ni después del cambio, por eso hace falta la prueba del cliente.")
punto("Pruebas del 27/11: se reprodujo el cuadre de caja en blanco y, con la corrección, se probaron la vista previa y el cierre con las siete formas de pago, un pago mixto con vuelto, una venta a crédito y una devolución; los totales coinciden con lo calculado aparte. "
      "La búsqueda por fecha se probó con la hora guardada como en el equipo del cliente. El Informe de Precios por proveedor y la impresión del Estado de cuentas se probaron en el programa; la retención de IVA, con una factura a crédito de prueba.")
punto("Orden de compra: se probó en el programa en bolívares, dólares y euros, con una orden antigua sin porcentajes, la impresión y una falla provocada al grabar (la orden original quedó intacta). "
      "Su script se aplicó, se repitió, se revirtió y se aplicó otra vez; al revertirlo guarda una copia de los descuentos y de la tasa.")
punto("Otras Cuentas x Pagar: se probó en el programa con las tasas de IVA actuales: factura, carga temporal, modificación, cierre definitivo, una falla provocada al grabar (no quedó nada a medias), nota de crédito, retenciones de IVA e ISLR, el botón «Islr», "
      "el comprobante contable, la búsqueda por fecha y un pagaré. No se probaron las notas de débito, los anticipos, los préstamos, los giros ni la retención de ISLR sobre una nota de crédito. Para probar la creación de cuentas contables se desactivó en la base local "
      "la copia automática de cuentas a las demás compañías (esas bases no están en el ambiente de pruebas): hay que confirmarla en el ambiente del cliente. Los datos de prueba se borraron.")
punto("Instalador 1.2.2: se probó como actualización silenciosa de una instalación 1.2.1 en una PC de pruebas (conserva el servidor y la configuración, registra la versión nueva, deja un solo desinstalador y los controles de la impresora fiscal registrados), "
      "y el programa instalado abre con las dos pantallas de Compras. No se probó en una PC limpia ni con el asistente visible.")
punto("No se probó con MySQL 5.5 real ni con impresora fiscal.")

d.save(RUTA)
print("guardado", RUTA)
