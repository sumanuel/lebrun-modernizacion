---
name: armar-entrega-lebrun
description: Arma la entrega al cliente del Sistema Lebrun - compila el programa, genera el instalador con Inno Setup, regenera el documento de estado de avance y junta todo en una sola carpeta Entregas\Entrega_<version> (instalador, scripts de base de datos, documentos y LEEME). Usar cuando el usuario pida armar, preparar o reconstruir la entrega, el instalador nuevo o la carpeta de entrega para el cliente.
---

# Armar la entrega de Lebrun

Resultado: una carpeta `C:\Users\user\Downloads\LEBRUN SILVANA\Entregas\Entrega_<version>\` con TODO lo que se le pasa al cliente:
`Lebrun_Setup_<v>_x86.exe`, `Scripts_BD_<v>.zip`, el documento de estado de avance y `LEEME_ENTREGA.txt` (orden de aplicación y SHA-256).

Trabajar en español. No guardar ni repetir contraseñas (ni la del usuario de pruebas ni la de la base). No hacer commit ni push, ni integrar a `main`, sin que el usuario lo pida (siempre se pregunta antes).

## 0. Qué se entrega
1. `git status` y `git log -3 main`: ver en qué rama se está, qué hay en `main` y qué commits no están en él (`git log --oneline main..<rama>`). La entrega sale de lo que está **integrado en `main`**; si falta algo, avisar y preguntar antes de seguir. `App.config` aparece siempre modificado (solo el servidor local de pruebas): nunca se commitea.
2. Preguntar la **versión** si no se sabe. Si hubo cambios de código desde el último instalador, la versión sube en `instalador\Lebrun.iss` (`#define AppVersion "x.y.z"`); no pisar el instalador anterior. Si el usuario solo quiere reempaquetar, se reutiliza la versión actual.
3. Revisar que los scripts de `scripts_bd\` nuevos o cambiados estén aplicados y probados en XAMPP (aplicar, repetir, revertir, reaplicar) y que `scripts_bd\LEEME_ORDEN.txt` los liste en el orden de ejecución (si se agregó una carpeta de scripts, actualizarlo). Los scripts se ejecutan ANTES de instalar el exe.

## 1. Compilar el programa (Release x86) en el payload
Rutas: repositorio `D:\lebrun_20220627\lebrun_20220627\lebrun`; binarios en `D:\lebrun_20220627\lebrun_20220627\Instaladores`.

1. Respaldar `Instaladores\05_Instalador\payload\x86\lebrun.exe.config` (y `App.config`) en la carpeta temporal de la sesión y anotar su hash.
2. Compilar (PowerShell, desde el repositorio):
   ```powershell
   & "C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\MSBuild.exe" lebrun.csproj /p:Configuration=Release /p:PlatformTarget=x86 "/p:OutputPath=D:\lebrun_20220627\lebrun_20220627\Instaladores\05_Instalador\payload\x86\" /p:IntermediateOutputPath=obj\Release_x86\ /v:minimal /nologo
   ```
   Debe terminar sin errores.
3. **Comprobar la configuración**: `payload\x86\lebrun.exe.config` debe tener el mismo hash que el respaldo (servidor `localhost`, usuario `root`, `password` vacío, puerto `3306`): el instalador reemplaza esos valores exactos al instalar. Si cambió, restaurar el respaldo: no puede entrar ninguna contraseña real.
4. Comprobar que el exe trae lo nuevo (por ejemplo buscando en el binario el nombre de una clase o columna agregada).

## 2. Generar el instalador
Desde `instalador\`:
```powershell
& "$env:LOCALAPPDATA\Programs\Inno Setup 6\ISCC.exe" Lebrun.iss
```
Sale `Instaladores\05_Instalador\salida\Lebrun_Setup_<v>_x86.exe`. Debe decir "Successful compile".

## 3. Documento de estado de avance
`instalador\generar_estado_avance.py` genera el documento (`pip install python-docx`; la salida por omisión es `Downloads\LEBRUN SILVANA\Informes\Estado_de_Avance_Condicion_y_Forma_de_Pago.docx`). Su contenido está escrito en el propio archivo: antes de generarlo, **editarlo** para que diga la verdad de hoy (fecha, versión del instalador, qué está hecho, qué falta, pendientes del cliente, scripts de la entrega, cómo se probó). Si Word lo tiene abierto, el guardado falla con "Permission denied": pedir que lo cierren. Dirigido al cliente: lenguaje claro, sin rutas internas ni nombres de ramas.

## 4. Armar la carpeta
```powershell
powershell -ExecutionPolicy Bypass -File instalador\armar_entrega.ps1
```
Lee la versión de `Lebrun.iss`, copia el instalador, arma `Scripts_BD_<v>.zip` desde `scripts_bd\` + `LEEME_ORDEN.txt`, copia los documentos (`-Documentos` para otros informes) y escribe `LEEME_ENTREGA.txt`. Atender sus ADVERTENCIAS: cambios sin commit, o `lebrun.exe` más nuevo que el instalador (recompilar el instalador).
Verificar: el zip tiene las carpetas esperadas con rutas con `/`, y el hash del instalador copiado coincide con el original.

## 5. Probar (si el usuario lo pide, o antes de dar la entrega por buena)
Actualización silenciosa en esta PC sobre la versión instalada (`C:\Program Files (x86)\Lebrun`): respaldar `ip.txt` y `lebrun.exe.config`, cerrar `lebrun.exe`, ejecutar con elevación
`Start-Process <instalador> -ArgumentList '/VERYSILENT','/SUPPRESSMSGBOXES','/NORESTART','/LOG=<log>' -Verb RunAs -Wait -PassThru` y comprobar: código de salida 0; `lebrun.exe` igual al del payload; `ip.txt` y `lebrun.exe.config` con el mismo hash; versión nueva en Programas instalados (HKLM\...\WOW6432Node\...\Uninstall); un solo `unins000`; accesos directos; `TMFISCAL.ocx`, `MSCOMM32.OCX` y `Comdlg32.ocx` en `SysWOW64`; sin entradas nuevas en `C:\ProgramData\Lebrun\log.txt`; la app abre. Lo que NO cubre: PC limpia (runtime de Crystal y registro de los controles), asistente visible, impresora fiscal: decirlo.

## 6. Dejar constancia (siempre)
- Registrar la tarea en `Downloads\LEBRUN SILVANA\Informes\Plan_Pruebas_Seguimiento_Lebrun.xlsx`, hoja «Registro de tareas»: fila nueva al final (siguiente R-xxx), copiando el formato de la fila anterior, y extender a la fila nueva el filtro, las validaciones, el formato condicional y las fórmulas COUNTIF de la fila 4 y B5 (las fórmulas se recalculan al abrir en Excel). Las carpetas `Informes\Ini` son solo planeación: no tocarlas.
- Actualizar la memoria del proyecto si cambió algo importante (qué hay en `main`, qué falta entregar).
- Si hay cambios en archivos del repositorio (por ejemplo la versión en `Lebrun.iss`, `LEEME_ORDEN.txt` o `generar_estado_avance.py`), preguntar si se hace el commit y si se sube / integra a `main`.

## 7. Informar al usuario
Ruta de la carpeta de entrega y sus archivos, versión, qué incluye y qué NO (por ejemplo ramas sin integrar), lo que se probó y lo que no, y los pendientes del cliente. No entregar versiones viejas: siguen en `Instaladores\05_Instalador\salida\` pero están reemplazadas.
