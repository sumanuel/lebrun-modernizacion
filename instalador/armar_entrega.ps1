<#
  Arma la carpeta con TODO lo que se le entrega al cliente de la version vigente (la de Lebrun.iss):
      <Destino>\Entrega_<version>\
          Lebrun_Setup_<version>_x86.exe     instalador (ya compilado con ISCC en Instaladores\05_Instalador\salida)
          Scripts_BD_<version>.zip           scripts de base de datos (se arma aqui desde scripts_bd\, con su LEEME_ORDEN.txt)
          <documentos>                       los informes indicados (por omision, el estado de avance)
          LEEME_ENTREGA.txt                  contenido, orden de aplicacion y SHA-256 del instalador

  Uso (desde cualquier carpeta):
      powershell -ExecutionPolicy Bypass -File instalador\armar_entrega.ps1
      powershell -ExecutionPolicy Bypass -File instalador\armar_entrega.ps1 -Documentos 'Estado_de_Avance_Condicion_y_Forma_de_Pago.docx','Otro.docx'

  Antes: compilar la aplicacion y el instalador (ver LEEME.txt) y, si cambio algo, regenerar los documentos.
  Si la carpeta de la version ya existe se reemplaza su contenido.
#>
param(
    [string]$Componentes = (Join-Path $PSScriptRoot '..\..\Instaladores'),
    [string]$Informes    = (Join-Path $env:USERPROFILE 'Downloads\LEBRUN SILVANA\Informes'),
    [string]$Destino     = (Join-Path $env:USERPROFILE 'Downloads\LEBRUN SILVANA\Entregas'),
    [string[]]$Documentos = @('Estado_de_Avance_Condicion_y_Forma_de_Pago.docx')
)
$ErrorActionPreference = 'Stop'
Add-Type -AssemblyName System.IO.Compression
Add-Type -AssemblyName System.IO.Compression.FileSystem

$repo = (Resolve-Path (Join-Path $PSScriptRoot '..')).Path
$utf8Bom = New-Object System.Text.UTF8Encoding($true)

# version vigente = la de Lebrun.iss
$iss = [IO.File]::ReadAllText((Join-Path $PSScriptRoot 'Lebrun.iss'))
if ($iss -notmatch '#define AppVersion "([^"]+)"') { throw 'No se encontro AppVersion en Lebrun.iss' }
$version = $Matches[1]

$instalador = Join-Path $Componentes "05_Instalador\salida\Lebrun_Setup_${version}_x86.exe"
if (-not (Test-Path $instalador)) { throw "No existe $instalador. Compile el instalador con ISCC.exe Lebrun.iss (ver LEEME.txt)." }
foreach ($d in $Documentos) {
    if (-not (Test-Path (Join-Path $Informes $d))) { throw "No existe el documento $(Join-Path $Informes $d)" }
}

# avisos (no detienen la entrega, pero conviene verlos)
$commit = (& git -C $repo rev-parse --short HEAD).Trim()
$rama = (& git -C $repo rev-parse --abbrev-ref HEAD).Trim()
$sucios = & git -C $repo status --short -- scripts_bd instalador clases formularios
if ($sucios) { Write-Warning "Hay cambios sin commit en el codigo, los scripts o el instalador (el commit $commit no los incluye):"; $sucios | ForEach-Object { Write-Warning "  $_" } }
$exe = Join-Path $Componentes '05_Instalador\payload\x86\lebrun.exe'
if ((Test-Path $exe) -and ((Get-Item $exe).LastWriteTime -gt (Get-Item $instalador).LastWriteTime)) {
    Write-Warning 'lebrun.exe del payload es mas nuevo que el instalador: recompile el instalador antes de entregar.'
}

# carpeta de la entrega (limpia)
$carpeta = Join-Path $Destino "Entrega_$version"
if (Test-Path $carpeta) { Remove-Item $carpeta -Recurse -Force }
New-Item -ItemType Directory -Force $carpeta | Out-Null

# 1) instalador
Copy-Item $instalador $carpeta

# 2) scripts de base de datos: LEEME_ORDEN.txt + las carpetas de scripts_bd (con rutas con "/" dentro del zip)
$zipPath = Join-Path $carpeta "Scripts_BD_$version.zip"
$zip = [IO.Compression.ZipFile]::Open($zipPath, 'Create')
try {
    $orden = [IO.File]::ReadAllText((Join-Path $repo 'scripts_bd\LEEME_ORDEN.txt'), [Text.Encoding]::UTF8).Replace('{VERSION}', $version)
    $e = $zip.CreateEntry('LEEME_ORDEN.txt')
    $w = New-Object IO.StreamWriter($e.Open(), $utf8Bom); $w.Write($orden); $w.Dispose()
    $baseScripts = Join-Path $repo 'scripts_bd'
    Get-ChildItem $baseScripts -Directory | ForEach-Object {
        Get-ChildItem $_.FullName -Recurse -File | Where-Object { $_.Extension -in '.sql', '.txt' } | ForEach-Object {
            $rel = $_.FullName.Substring($baseScripts.Length + 1).Replace('\', '/')
            [void][IO.Compression.ZipFileExtensions]::CreateEntryFromFile($zip, $_.FullName, $rel)
        }
    }
} finally { $zip.Dispose() }

# 3) documentos
foreach ($d in $Documentos) { Copy-Item (Join-Path $Informes $d) $carpeta }

# 4) LEEME_ENTREGA.txt
$hash = (Get-FileHash (Join-Path $carpeta (Split-Path $instalador -Leaf)) -Algorithm SHA256).Hash
$lista = Get-ChildItem $carpeta -File | Sort-Object Name | ForEach-Object {
    if ($_.Length -ge 1MB) { '  {0}  ({1:N1} MB)' -f $_.Name, ($_.Length / 1MB) } else { '  {0}  ({1:N0} KB)' -f $_.Name, ($_.Length / 1KB) }
}
$lista += '  LEEME_ENTREGA.txt  (este archivo)'
$texto = [IO.File]::ReadAllText((Join-Path $PSScriptRoot 'LEEME_ENTREGA.plantilla.txt'), [Text.Encoding]::UTF8)
$texto = $texto.Replace('{VERSION}', $version).Replace('{FECHA}', (Get-Date -Format 'dd/MM/yyyy HH:mm')).Replace('{COMMIT}', $commit).Replace('{RAMA}', $rama).Replace('{HASH}', $hash).Replace('{ARCHIVOS}', ($lista -join "`r`n"))
[IO.File]::WriteAllText((Join-Path $carpeta 'LEEME_ENTREGA.txt'), $texto, $utf8Bom)

Write-Host ''
Write-Host "Entrega $version armada en: $carpeta" -ForegroundColor Green
Get-ChildItem $carpeta -File | Select-Object Name, @{n = 'MB'; e = { [math]::Round($_.Length / 1MB, 2) } } | Format-Table -AutoSize
