#requires -Version 5.1
<#
  Prueba automatica del instalador de Lebrun: instala en silencio, verifica el resultado,
  abre la aplicacion, desinstala y comprueba la limpieza. Escribe un informe de texto.
  Debe ejecutarse con permisos de administrador (en Windows Sandbox ya los tiene).
#>
param(
  [string]$Instalador = 'C:\Instalador\Lebrun_Setup_1.0.0_x86.exe',
  [string]$Reporte    = 'C:\Prueba\resultado.txt',
  [string]$Servidor   = '192.0.2.10',
  [string]$Puerto     = '3307',
  [string]$Usuario    = 'usuario_prueba',
  [string]$Clave      = 'Cl&ve<123>'
)

$ErrorActionPreference = 'Continue'
$lineas = New-Object System.Collections.ArrayList
$script:fallos = 0
function Log($t) { [void]$lineas.Add($t); Write-Host $t }
function Check($nombre, $ok, $detalle = '') {
  if ($ok) { Log ("[OK]     " + $nombre + $(if ($detalle) { " - $detalle" })) }
  else { $script:fallos++; Log ("[FALLA]  " + $nombre + $(if ($detalle) { " - $detalle" })) }
}
function Guardar { Set-Content -Path $Reporte -Value $lineas -Encoding UTF8 }

$esAdmin = ([Security.Principal.WindowsPrincipal][Security.Principal.WindowsIdentity]::GetCurrent()).IsInRole([Security.Principal.WindowsBuiltInRole]::Administrator)
if (-not $esAdmin) { Log 'La prueba requiere permisos de administrador.'; Guardar; exit 2 }
if (-not (Test-Path $Instalador)) { Log "No se encuentra el instalador: $Instalador"; Guardar; exit 2 }

$appId   = '{38223674-50F2-4874-84A1-F9E353BE8D29}'
$crCode  = '{E43D7D73-C531-4EB3-ACC9-0EF11E1A84B0}'
$dir     = Join-Path ${env:ProgramFiles(x86)} 'Lebrun'
$arp     = "HKLM:\SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall\${appId}_is1"
$acceso  = Join-Path $env:ProgramData 'Microsoft\Windows\Start Menu\Programs\Lebrun.lnk'
$clsid   = [ordered]@{ TMFISCAL = '{6bfd3f2b-c1e2-4094-9c43-88d6a0a79d72}'; MSCOMM32 = '{648a5600-2c6e-101b-82b6-000000000014}'; Comdlg32 = '{f9043c85-f6f2-101a-a3c9-08002b2f49fb}' }

function Estado-CR {
  try {
    $wi = New-Object -ComObject WindowsInstaller.Installer
    return [int]$wi.GetType().InvokeMember('ProductState', 'GetProperty', $null, $wi, @($crCode))
  } catch { return -99 }
}
function Ocx-Registrados {
  $n = 0
  foreach ($c in $clsid.Values) { if (Test-Path "HKLM:\SOFTWARE\Classes\WOW6432Node\CLSID\$c") { $n++ } }
  return $n
}

Log "PRUEBA DEL INSTALADOR DE LEBRUN - $(Get-Date -Format 'yyyy-MM-dd HH:mm:ss')"
Log "Equipo: $env:COMPUTERNAME | $((Get-CimInstance Win32_OperatingSystem).Caption) build $((Get-CimInstance Win32_OperatingSystem).BuildNumber)"
Log ''
Log '== ESTADO INICIAL =='
$crAntes = Estado-CR; $ocxAntes = Ocx-Registrados
$net = (Get-ItemProperty 'HKLM:\SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full' -ErrorAction SilentlyContinue).Release
Log ".NET Framework 4.x release: $net (4.8 = 528040 o mas)"
Log "Runtime de Crystal Reports 32 bits instalado antes: $(if ($crAntes -eq 5) { 'SI' } else { 'NO' })"
Log "Controles ActiveX registrados antes: $ocxAntes de 3"
Log "Carpeta de Lebrun existe antes: $(Test-Path $dir)"
$limpio = ($crAntes -ne 5) -and ($ocxAntes -eq 0) -and -not (Test-Path $dir)
Log "Equipo limpio (sin Crystal, sin controles, sin Lebrun): $(if ($limpio) { 'SI' } else { 'NO - esta prueba cubre solo en parte la instalacion de componentes' })"
Check '.NET Framework 4.8 presente' ($net -ge 528040) "release=$net"
Log ''

Log '== INSTALACION SILENCIOSA =='
$logInst = Join-Path ([IO.Path]::GetDirectoryName($Reporte)) 'instalacion.log'
$argInst = "/VERYSILENT /SUPPRESSMSGBOXES /NORESTART /LOG=`"$logInst`" /Servidor=$Servidor /Puerto=$Puerto /Usuario=$Usuario /Clave=`"$Clave`""
$sw = [Diagnostics.Stopwatch]::StartNew()
$p = Start-Process -FilePath $Instalador -ArgumentList $argInst -Wait -PassThru
$sw.Stop()
Check 'El instalador termina con codigo 0' ($p.ExitCode -eq 0) "codigo=$($p.ExitCode), $([int]$sw.Elapsed.TotalSeconds) s"
Log ''

Log '== RESULTADO DE LA INSTALACION =='
$exe = Join-Path $dir 'lebrun.exe'
Check 'lebrun.exe instalado' (Test-Path $exe) $exe
if (Test-Path $exe) {
  $fs = [IO.File]::OpenRead($exe); $b = New-Object byte[] 4096; [void]$fs.Read($b, 0, 4096); $fs.Close()
  $off = [BitConverter]::ToInt32($b, 0x3c)
  Check 'lebrun.exe es de 32 bits (x86)' ([BitConverter]::ToUInt16($b, $off + 4) -eq 0x14C)
}
foreach ($f in 'MySql.Data.dll', 'CrystalDecisions.CrystalReports.Engine.dll', 'CrystalDecisions.Shared.dll', 'AxInterop.TM_FISCAL.dll', 'Interop.MSCommLib.dll') {
  Check "Archivo $f" (Test-Path (Join-Path $dir $f))
}
Check 'No se instalaron simbolos de depuracion (.pdb)' (-not (Get-ChildItem $dir -Filter *.pdb -ErrorAction SilentlyContinue))
Check 'No se instalo la plantilla App.config' (-not (Test-Path (Join-Path $dir 'App.config')))
Check 'PNPDLL.dll: se incluye solo si existe en la carpeta extras (informativo)' $true "presente=$(Test-Path (Join-Path $dir 'PNPDLL.dll'))"

$ip = Join-Path $dir 'ip.txt'
if (Test-Path $ip) {
  $bytes = [IO.File]::ReadAllBytes($ip)
  $txt = [Text.Encoding]::ASCII.GetString($bytes)
  Check 'ip.txt contiene exactamente el servidor, sin salto de linea' ($txt -ceq $Servidor) "contenido='$txt' ($($bytes.Length) bytes)"
} else { Check 'ip.txt creado' $false }

$cfgRuta = Join-Path $dir 'lebrun.exe.config'
try {
  [xml]$cfg = Get-Content $cfgRuta -Raw
  Check 'lebrun.exe.config es XML valido tras ser modificado' $true
  $v = @{}; foreach ($a in $cfg.configuration.appSettings.add) { $v[$a.key] = $a.value }
  Check 'Config: servidor'    ($v['server']   -ceq $Servidor) "valor='$($v['server'])'"
  Check 'Config: puerto'      ($v['port']     -ceq $Puerto)   "valor='$($v['port'])'"
  Check 'Config: usuario'     ($v['login']    -ceq $Usuario)  "valor='$($v['login'])'"
  Check 'Config: contrasena con caracteres especiales (& < >)' ($v['password'] -ceq $Clave) "valor='$($v['password'])'"
  Check 'Config: bases de datos intactas' (($v['db1'] -eq 'sisadm') -and ($v['db2'] -eq 'sysconf') -and ($v['db3'] -eq 'sysconta'))
} catch { Check 'lebrun.exe.config legible como XML' $false $_.Exception.Message }

Check 'Acceso directo en el menu Inicio' (Test-Path $acceso)
Check 'Entrada en Agregar o quitar programas' (Test-Path $arp)
$crDespues = Estado-CR
Check 'Runtime de Crystal Reports 32 bits instalado (estado 5)' ($crDespues -eq 5) "estado=$crDespues (antes=$crAntes)"
$ocxDesp = Ocx-Registrados
Check 'Los 3 controles ActiveX registrados en la vista de 32 bits' ($ocxDesp -eq 3) "$ocxDesp de 3 (antes=$ocxAntes)"
foreach ($f in 'TMFISCAL.ocx', 'MSCOMM32.OCX', 'Comdlg32.ocx') { Check "Archivo $f en SysWOW64" (Test-Path (Join-Path $env:windir "SysWOW64\$f")) }

$ps32 = Join-Path $env:windir 'SysWOW64\WindowsPowerShell\v1.0\powershell.exe'
$cmd = '$r=@(); foreach($g in @(' + (($clsid.Values | ForEach-Object { "'$_'" }) -join ',') + ')){ try{ $o=[Activator]::CreateInstance([Type]::GetTypeFromCLSID([Guid]$g)); $r+=''OK''; [void][Runtime.InteropServices.Marshal]::ReleaseComObject($o) }catch{ $r+=''ERROR'' } }; $r -join '','''
$res32 = (& $ps32 -NoProfile -Command $cmd) 2>&1
Check 'Los controles se pueden crear desde un proceso de 32 bits' (("$res32") -eq 'OK,OK,OK') "resultado=$res32"
Log ''

Log '== ARRANQUE DE LA APLICACION =='
if (Test-Path $exe) {
  $app = Start-Process -FilePath $exe -WorkingDirectory $dir -PassThru
  Start-Sleep -Seconds 12
  $app.Refresh()
  $vivo = -not $app.HasExited
  Check 'La aplicacion sigue abierta 12 s despues de iniciar' $vivo $(if ($vivo) { "ventana='$($app.MainWindowTitle)'" } else { "termino con codigo $($app.ExitCode)" })
  Log '(Se esperan errores de conexion a la base de datos: el servidor de prueba no existe.)'
  if ($vivo) { Stop-Process -Id $app.Id -Force -ErrorAction SilentlyContinue }
  Get-Process lebrun -ErrorAction SilentlyContinue | Stop-Process -Force -ErrorAction SilentlyContinue
  $logApp = Join-Path $dir 'log.txt'
  if (Test-Path $logApp) { Log "log.txt de la aplicacion: $((Get-Content $logApp -TotalCount 6) -join ' | ')" }
}
Log ''

Log '== DESINSTALACION =='
$unins = Get-ChildItem $dir -Filter 'unins*.exe' -ErrorAction SilentlyContinue | Select-Object -First 1
Check 'Existe el desinstalador' ($null -ne $unins)
if ($unins) {
  $pu = Start-Process -FilePath $unins.FullName -ArgumentList '/VERYSILENT /SUPPRESSMSGBOXES /NORESTART' -Wait -PassThru
  Check 'El desinstalador termina con codigo 0' ($pu.ExitCode -eq 0) "codigo=$($pu.ExitCode)"
  Start-Sleep -Seconds 2
  Check 'Se elimino la carpeta de la aplicacion' (-not (Test-Path $dir)) $(if (Test-Path $dir) { 'quedan: ' + ((Get-ChildItem $dir -Recurse -Name) -join ', ') })
  Check 'Se elimino el acceso directo' (-not (Test-Path $acceso))
  Check 'Se elimino la entrada de Agregar o quitar programas' (-not (Test-Path $arp))
  Log "Informativo: controles ActiveX registrados tras desinstalar: $(Ocx-Registrados) de 3 (por diseno no se desregistran, pueden ser compartidos)"
  Log "Informativo: runtime de Crystal Reports tras desinstalar: estado $(Estado-CR) (por diseno no se desinstala)"
}
Log ''
Log "RESULTADO: $(if ($script:fallos -eq 0) { 'TODAS LAS VERIFICACIONES CORRECTAS' } else { "$($script:fallos) VERIFICACION(ES) FALLIDA(S)" })"
Guardar
exit $script:fallos
