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
p("Condición de pago, forma de pago y mejoras en facturación — Sistema Lebrun", color=GRIS, tam=11)
p("Fecha: 13/11/2026. Resume lo pedido en las observaciones del cliente del 10/11/2026 y en las respuestas posteriores (incluidos los puntos 15 y 16), lo que ya está hecho, lo que falta y lo que "
  "necesita hacer o confirmar el cliente. Las pruebas se hicieron en una copia local de la base de datos (MariaDB) y en una copia de producción de solo lectura; "
  "no se probó con una impresora fiscal ni con el servidor de producción.", cursiva=True, color=GRIS, tam=9)

# ---------------- resumen ----------------
d.add_heading("1. Resumen", 1)
p("Todo lo solicitado (observaciones 1 a 16) está programado, probado en el ambiente de pruebas e incluido en el instalador Lebrun_Setup_1.2.0_x86.exe. "
  "Lo que queda es aplicarlo en el ambiente del cliente (primero los scripts de base de datos y después el instalador). El cliente tiene que hacer pruebas que aquí no se pueden hacer: "
  "cobrar en divisa y ver el aviso de datos de contacto desde la factura con una serie fiscal real, y ver el reporte del cuadre de caja en su equipo. "
  "Además debe indicarnos cómo se carga la tasa en el historial de divisas (punto 15).")
punto("Una sola tabla de condiciones y formas de pago, con siglas y pantalla de mantenimiento; Contado es el código 07.", "Condición y forma de pago:")
punto("origen de la venta, tasa de cambio en cada factura, monto cobrado cuando el pago es parcial y estado de cobro (Pagado, Parcial, Pendiente).", "Observaciones 7 a 10:")
punto("el monto original en dólares o euros se guarda junto con la tasa usada; y hay una pantalla nueva de Estado de cuentas.", "Además:")
punto("la tasa de cambio se guarda con hasta 8 decimales, como la publica el BCV.", "Observación 15:")
punto("en caja se piden el correo y la fecha de nacimiento del cliente (no son obligatorios), y la pantalla de Clientes tiene el campo nuevo de fecha de nacimiento.", "Observación 16:")

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
         "Pantalla nueva: documentos por cobrar por cliente con monto, cobrado, saldo y estado de cobro; filtros por cliente, fechas y «solo con saldo»; totales.", "Hecho"],
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

# ---------------- pendiente ----------------
d.add_heading("3. Lo que falta", 1)
d.add_heading("3.1 Por parte del cliente", 2)
tabla(
    ["Qué", "Detalle", "Estado"],
    [
        ["Cobrar en divisa con una serie fiscal real", "En el ambiente de pruebas el botón «Agregar» de facturación no se habilita sin serie fiscal. Hay que probar el cobro en Divisa $ y Divisa € desde la pantalla (la conversión y el guardado sí se probaron).", "Pendiente"],
        ["Indicar cómo se carga la tasa en el historial de divisas", "Este programa no escribe en esa tabla: la tasa la carga otro programa o se escribe a mano en la base. Si ese programa la redondea a 2 decimales, hay que ajustarlo para que la escriba completa; si no, ampliar la tabla no sirve de nada.", "Pendiente"],
        ["Importaciones: ¿se quiere que la tasa se proponga desde el historial?", "Hoy la tasa se escribe a mano en la orden de compra de importaciones y en el proveedor solo se asigna el código de la moneda. No se cambió; si se quiere que el programa proponga la tasa del historial (editable), hay que pedirlo.", "Pendiente"],
        ["Probar el aviso de datos de contacto desde la factura", "El botón «Agregar» de facturación no se habilita sin serie fiscal en el ambiente de pruebas. El aviso se probó por separado con la ventana real y la pantalla de Clientes se revisó en el programa; falta verlo al elegir un cliente en una factura real.", "Pendiente"],
        ["Ver el reporte del cuadre de caja en su equipo", "El reporte se verificó exportándolo a PDF; el visor de reportes sale en blanco en el ambiente de pruebas (también con la versión instalada hoy).", "Pendiente"],
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

# ---------------- entrega ----------------
d.add_heading("4. Entrega e instalación", 1)
p("Las observaciones 1 a 16 están integradas en el programa principal y en el instalador. Se entregan dos archivos: el instalador Lebrun_Setup_1.2.0_x86.exe y el paquete de scripts Scripts_BD_1.2.0.zip. "
  "Falta aplicarlos en el ambiente del cliente.")
p("Scripts de base de datos (el paquete trae tres carpetas: condicion_pago, clientes_contacto y tasa_cambio; cada una tiene Implementar, con los scripts que se ejecutan, y Revertir, con el que deshace cada uno; "
  "el archivo LEEME_ORDEN.txt indica el orden; se pueden ejecutar más de una vez). Los números 01 a 11 son de la carpeta condicion_pago:", negrita=True)
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
    ],
    [2.6, 14.1],
)
punto("Ejecutar los scripts ANTES de instalar la versión nueva del programa: la versión nueva usa columnas y tablas que crean los scripts.")
punto("Cerrar sesión y volver a entrar después de los scripts de menú (03, 07 y 11) para ver las opciones nuevas.")
punto("Los perfiles que no sean ADMINISTRADOR necesitan que se les active la opción «Origen de la Venta» en la pantalla de perfiles; «Estado de Cuentas» la ven los perfiles que ya la tenían activa.")
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
punto("Instalador 1.2.0: se probó como actualización silenciosa de una instalación 1.1.0 en una PC de pruebas (conserva el servidor y la configuración, registra la versión nueva, deja un solo desinstalador y los controles de la impresora fiscal registrados), "
      "y el programa instalado abre con la pantalla de Clientes con el campo nuevo y el Estado de cuentas funcionando. No se probó en una PC limpia ni con el asistente visible.")
punto("No se probó con MySQL 5.5 real ni con impresora fiscal.")

d.save(RUTA)
print("guardado", RUTA)
