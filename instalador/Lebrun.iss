; Instalador del Sistema Lebrun (aplicacion de 32 bits)
; Se compila con Inno Setup 6:  ISCC.exe Lebrun.iss
;
; Los binarios NO estan en el repositorio. Se leen de la carpeta "Componentes", que por defecto es
; la carpeta Instaladores junto a la carpeta del repositorio. Para usar otra:
;     ISCC.exe /DComponentes=C:\ruta\Instaladores Lebrun.iss
; Estructura esperada dentro de Componentes:
;     01_Runtime_CrystalReports\CRRuntime_32bit_13_0_35.msi
;     02_Controles_ActiveX\TMFISCAL.ocx, MSCOMM32.OCX, Comdlg32.ocx
;     05_Instalador\payload\x86\   compilacion Release x86 (lebrun.exe y sus DLL)
;     05_Instalador\extras\        PNPDLL.dll (opcional)
;     05_Instalador\salida\        aqui se genera el instalador

#ifndef Componentes
  #define Componentes "..\..\Instaladores"
#endif

#define AppName "Sistema Lebrun"
#define AppVersion "1.0.0"
#define AppExe "lebrun.exe"
#define CrMsi "CRRuntime_32bit_13_0_35.msi"

[Setup]
AppId={{38223674-50F2-4874-84A1-F9E353BE8D29}
AppName={#AppName}
AppVersion={#AppVersion}
DefaultDirName={autopf}\Lebrun
DefaultGroupName=Lebrun
DisableProgramGroupPage=yes
OutputDir={#Componentes}\05_Instalador\salida
OutputBaseFilename=Lebrun_Setup_{#AppVersion}_x86
Compression=lzma2/max
SolidCompression=yes
PrivilegesRequired=admin
WizardStyle=modern
UninstallDisplayIcon={app}\{#AppExe}
UninstallDisplayName={#AppName}
MinVersion=10.0

[Languages]
Name: "spanish"; MessagesFile: "compiler:Languages\Spanish.isl"

[Tasks]
Name: "desktopicon"; Description: "Crear un acceso directo en el escritorio"; GroupDescription: "Accesos directos:"

[Files]
; Aplicacion (sin simbolos de depuracion ni la plantilla App.config; la configuracion en uso es lebrun.exe.config)
Source: "{#Componentes}\05_Instalador\payload\x86\*"; DestDir: "{app}"; Excludes: "*.pdb,App.config"; Flags: ignoreversion recursesubdirs createallsubdirs
; Libreria nativa de la impresora fiscal actual: se incluye solo si el archivo existe en la carpeta extras
Source: "{#Componentes}\05_Instalador\extras\PNPDLL.dll"; DestDir: "{app}"; Flags: ignoreversion skipifsourcedoesntexist
; Controles ActiveX de 32 bits (en modo de 32 bits, {sys} es SysWOW64)
Source: "{#Componentes}\02_Controles_ActiveX\TMFISCAL.ocx"; DestDir: "{sys}"; Flags: sharedfile uninsneveruninstall
Source: "{#Componentes}\02_Controles_ActiveX\MSCOMM32.OCX"; DestDir: "{sys}"; Flags: sharedfile uninsneveruninstall
Source: "{#Componentes}\02_Controles_ActiveX\Comdlg32.ocx"; DestDir: "{sys}"; Flags: sharedfile uninsneveruninstall
; Runtime de Crystal Reports: solo se extrae si no esta instalado
Source: "{#Componentes}\01_Runtime_CrystalReports\{#CrMsi}"; DestDir: "{tmp}"; Flags: deleteafterinstall; Check: NecesitaCrystal

[Icons]
Name: "{autoprograms}\Lebrun"; Filename: "{app}\{#AppExe}"; WorkingDir: "{app}"
Name: "{autodesktop}\Lebrun"; Filename: "{app}\{#AppExe}"; WorkingDir: "{app}"; Tasks: desktopicon

[Run]
Filename: "msiexec.exe"; Parameters: "/i ""{tmp}\{#CrMsi}"" /qn /norestart"; StatusMsg: "Instalando SAP Crystal Reports runtime (puede tardar unos minutos)..."; Flags: waituntilterminated; Check: NecesitaCrystal
Filename: "{sys}\regsvr32.exe"; Parameters: "/s ""{sys}\TMFISCAL.ocx"""; StatusMsg: "Registrando controles de la impresora fiscal..."; Flags: runhidden waituntilterminated
Filename: "{sys}\regsvr32.exe"; Parameters: "/s ""{sys}\MSCOMM32.OCX"""; Flags: runhidden waituntilterminated
Filename: "{sys}\regsvr32.exe"; Parameters: "/s ""{sys}\Comdlg32.ocx"""; Flags: runhidden waituntilterminated
Filename: "{app}\{#AppExe}"; Description: "Abrir Lebrun"; Flags: nowait postinstall skipifsilent

[UninstallDelete]
Type: files; Name: "{app}\ip.txt"
Type: files; Name: "{app}\log.txt"
Type: dirifempty; Name: "{app}"

[Code]
var
  ConexionPage: TInputQueryWizardPage;

function MsiQueryProductState(szProduct: String): Integer;
  external 'MsiQueryProductStateW@msi.dll stdcall';

{ Codigo de producto del runtime de Crystal Reports 13.0.35 (32 bits); 5 = instalado }
function NecesitaCrystal: Boolean;
begin
  Result := MsiQueryProductState('{E43D7D73-C531-4EB3-ACC9-0EF11E1A84B0}') <> 5;
end;

function Net48Instalado: Boolean;
var
  Release: Cardinal;
begin
  Result := RegQueryDWordValue(HKLM, 'SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full', 'Release', Release)
            and (Release >= 528040);
end;

function InitializeSetup: Boolean;
begin
  Result := True;
  if not Net48Instalado then
  begin
    MsgBox('Este sistema requiere .NET Framework 4.8 o superior, que viene incluido en Windows 11. ' +
           'Active o instale .NET Framework 4.8 y vuelva a ejecutar el instalador.', mbError, MB_OK);
    Result := False;
  end;
end;

procedure InitializeWizard;
begin
  ConexionPage := CreateInputQueryPage(wpSelectDir,
    'Conexión a la base de datos',
    'Datos del servidor MySQL',
    'Indique cómo se conecta este equipo al servidor de la base de datos.');
  ConexionPage.Add('Servidor (dirección IP o nombre):', False);
  ConexionPage.Add('Puerto:', False);
  ConexionPage.Add('Usuario:', False);
  ConexionPage.Add('Contraseña:', True);
  { Valores por defecto; tambien se pueden indicar por linea de comandos para instalaciones desatendidas:
    /Servidor=192.168.1.10 /Puerto=3306 /Usuario=root /Clave=xxxx }
  ConexionPage.Values[0] := ExpandConstant('{param:Servidor|}');
  ConexionPage.Values[1] := ExpandConstant('{param:Puerto|3306}');
  ConexionPage.Values[2] := ExpandConstant('{param:Usuario|root}');
  ConexionPage.Values[3] := ExpandConstant('{param:Clave|}');
end;

function NextButtonClick(CurPageID: Integer): Boolean;
begin
  Result := True;
  if CurPageID = ConexionPage.ID then
  begin
    if Trim(ConexionPage.Values[0]) = '' then
    begin
      MsgBox('Indique la dirección del servidor.', mbError, MB_OK);
      Result := False;
    end;
  end;
end;

function XmlEscape(const S: String): String;
var
  R: String;
begin
  R := S;
  StringChangeEx(R, '&', '&amp;', True);
  StringChangeEx(R, '<', '&lt;', True);
  StringChangeEx(R, '>', '&gt;', True);
  StringChangeEx(R, '"', '&quot;', True);
  Result := R;
end;

{ Escribe ip.txt (sin salto de linea: la aplicacion lo lee completo) y ajusta lebrun.exe.config }
procedure CurStepChanged(CurStep: TSetupStep);
var
  Cfg: AnsiString;
  S, Ruta, Servidor: String;
begin
  if CurStep = ssPostInstall then
  begin
    Servidor := Trim(ConexionPage.Values[0]);
    { En modo silencioso sin /Servidor no se toca ip.txt ni la configuracion }
    if Servidor = '' then
      Exit;
    SaveStringToFile(ExpandConstant('{app}\ip.txt'), AnsiString(Servidor), False);

    Ruta := ExpandConstant('{app}\lebrun.exe.config');
    if LoadStringFromFile(Ruta, Cfg) then
    begin
      S := String(Cfg);
      StringChangeEx(S, '<add key="server" value="localhost" />',
        '<add key="server" value="' + XmlEscape(Servidor) + '" />', True);
      StringChangeEx(S, '<add key="login" value="root" />',
        '<add key="login" value="' + XmlEscape(Trim(ConexionPage.Values[2])) + '" />', True);
      StringChangeEx(S, '<add key="password" value="" />',
        '<add key="password" value="' + XmlEscape(ConexionPage.Values[3]) + '" />', True);
      StringChangeEx(S, '<add key="port" value="3306" />',
        '<add key="port" value="' + XmlEscape(Trim(ConexionPage.Values[1])) + '" />', True);
      SaveStringToFile(Ruta, AnsiString(S), False);
    end;
  end;
end;
