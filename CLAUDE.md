# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## What this is

`lebrun` is a Windows Forms desktop ERP application (.NET Framework, C#) for a Venezuelan retail/hardware group.
It covers invoicing/POS, accounting, purchasing, sales, inventory, banking/treasury, imports, receivables/
payables, and management reporting. The app is multi-company: one binary drives several related businesses
(company codes `01`–`06`, e.g. Lebrun, El Timón, La Tienda, Brunlé, Ferrecarpi, Ferresama), selected at login
and used throughout to branch behavior (background image, database, permissions).

There is no `.sln` file — the project is built directly from `lebrun.csproj`.

**Framework history:** originally targeted .NET Framework 3.5 (Visual Studio 2008/2010 era) against Crystal
Reports 10.5.3700.0. It was retargeted to **.NET Framework 4.8** (2026-09) to build with the Crystal Reports
13.0.4000.0 runtime available in modern Visual Studio (2022+) — CR 10.5 has no VS2022/2026-compatible
successor, and its GAC registration only exists for the .NET 4.0+ slot, so staying on 3.5 makes the project
un-buildable on a current machine. If you're chasing a build error that mentions `CrystalDecisions` types not
found, check that the six `CrystalDecisions.*` references in `lebrun.csproj` are all `Version=13.0.4000.0`
(not `10.5.3700.0`) and that the SAP Crystal Reports runtime for that version is installed/in the GAC.

## Build

Windows only: Visual Studio 2022/2026 (".NET desktop development" workload) plus the SAP Crystal Reports 13.0
runtime. The project references COM components — Office/Excel interop, a serial-port `MSComm`/`MSComDlg` control
and the `TM_FISCAL` fiscal-printer control (`MSCOMM32.OCX`, `Comdlg32.ocx`, `TMFISCAL.ocx`) — that exist only as
**32-bit ActiveX controls** and must be registered on the build/run machine (in `SysWOW64`).

```powershell
& "C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\MSBuild.exe" lebrun.csproj /p:Configuration=Release /p:PlatformTarget=x86
```

(Adjust the path for your VS edition/version; there is no `msbuild` on `PATH` outside a Developer Command Prompt.)
Debug and Release build with zero errors; what remains is ordinary C# warnings (unused variables and the like).

- Either MSBuild works (`Bin\MSBuild.exe` or `Bin\amd64\MSBuild.exe`). Verified 2026-10 on a fresh clone of the
  GitHub repo with an empty `obj\`: Release x86 and Debug AnyCPU build with 0 errors, and so does the 64-bit
  MSBuild with x86 or AnyCPU. The one combination that fails is **`PlatformTarget=x64`** (see below).
- **The app must run as x86.** The csproj sets no `PlatformTarget` (AnyCPU), which on 64-bit Windows runs as a
  64-bit process that cannot instantiate the three controls, so the forms that host them (the designers of
  `frmFactura`, `frmFacturaVivienda` and `ReportesZ_X` declare `AxTM_FISCAL`) cannot create them. There is no
  x64 option: with `PlatformTarget=x64`, `AxImp` cannot find the controls (they are registered only in the
  32-bit registry view) and fails with "No se puede encontrar un control ActiveX registrado en
  ...SysWow64\...ocx", with either MSBuild, so the control wrappers cannot even be generated. Pass
  `/p:PlatformTarget=x86` for anything that will be run or deployed. (Checked 2026-10:
  creating the controls from 64-bit PowerShell fails, from 32-bit PowerShell works.)
- **Third-party DLLs live in `lib\`** and are referenced by `HintPath`: `MySql.Data.dll` (6.3.7.0),
  `Interop.Microsoft.Office.Core.dll`, `Microsoft.Office.Interop.Excel.dll`, `Microsoft.Vbe.Interop.dll` and
  `office.dll`. They used to be resolved from `bin\Debug\`, which is gitignored, so a fresh clone could not build.
  Do not point references back at `bin\`.
- **`COMReference` items have `<Isolated>False</Isolated>`.** With `True`, MSBuild's `GenerateApplicationManifest`
  tried to build a registration-free manifest from the COM components and crashed intermittently (a different
  HRESULT each time). `Install` and `GenerateManifests` are also `false`: the app is deployed by copying files,
  not by ClickOnce.
- To build somewhere other than `bin\`, pass `/p:OutputPath=...\ /p:IntermediateOutputPath=obj\<name>\`.

There are no automated tests and no lint configuration in this repository.

## Installer

The installer **script and its test** are in `instalador\` (see `instalador\LEEME.txt`); the **binaries are not in
the repo** (hundreds of MB). They live in an `Instaladores\` folder next to this repo's folder
(`D:\lebrun_20220627\lebrun_20220627\Instaladores`): the Crystal Reports runtime MSIs, the three OCX, the TFHKA
SDK, the compiled app in `05_Instalador\payload\x86\` and the generated setup in `05_Instalador\salida\`.
`instalador\Lebrun.iss` is an Inno Setup 6 script that produces `Lebrun_Setup_<version>_x86.exe`. To rebuild it,
build Release x86 with `/p:OutputPath=...\05_Instalador\payload\x86\` and run `ISCC.exe Lebrun.iss` from
`instalador\` (or `ISCC.exe /DComponentes=<path to Instaladores> Lebrun.iss` if that folder is elsewhere).

It installs the Crystal Reports 13.0.35 **32-bit** runtime if missing, registers the three OCX with the 32-bit
`regsvr32`, asks for the MySQL server/port/user/password, and writes `ip.txt` and `lebrun.exe.config`. For
unattended installs pass `/VERYSILENT /Servidor=... /Puerto=... /Usuario=... /Clave=...` (without `/Servidor` a
silent install leaves `ip.txt` and the config alone). `PNPDLL.dll` (native 32-bit DLL of the current fiscal
printer, called through P/Invoke) is not in the project; it goes in `05_Instalador\extras\` before compiling.

`instalador\prueba_sandbox\probar_instalador.ps1` installs silently, checks the result, starts the app,
uninstalls and writes a report; `Lebrun_Prueba.wsb` runs it in Windows Sandbox for a clean-PC test. Run on the
development PC (2026-10-10) it passed all 33 checks, but that PC already had the Crystal runtime and the OCX, so
the runtime install and OCX registration were **not** exercised. A clean-PC run (Windows Sandbox, not yet
enabled on that PC) is still pending.

## Branches

One branch per part of the agreed scope, plus `main`:

- `main` — integration branch. Holds the work delivered so far (build under VS2022, `lib\`, the
  `Producto.cs` fix, the installer, documentation). Changes reach it only from the part branches.
- `parte-a-entorno` — Part A: development environment (validation in Visual Studio 2026, user manuals,
  repositories for the ModelSoft and historical-patch archives) and deployment/installer work.
- `parte-b-mejoras` — Part B: functional improvements (gerencial adjustments, suggested purchases, product
  reports, reservations view, costs in USD, Factory/TFHKA fiscal printer).
- `parte-c-base-de-datos` — Part C: MySQL upgrade (connector, incompatible queries, parallel test server,
  migration).

Work on the branch of the part it belongs to and merge into `main` once reviewed. All four branches started at
the same commit, so a part branch only shows the work done for that part.

## Runtime configuration

- `App.config` `appSettings` holds the MySQL connection parameters (`server`, `db1`/`db2`/`db3`, `login`,
  `password`, `port`). Three logical databases are used per company: `sisadm` (main app data), `sysconf`
  (system/config), `sysconta` (accounting) — selected by index (1/2/3) when opening a connection.
- At startup, `ControladorMain` (`ControladorMain.cs`) overrides the `server` setting by reading an `ip.txt` file
  from the executable's directory (`Application.StartupPath`). This file is expected to exist in the deployed
  output but is not part of the source tree. It is read raw, so it must contain only the host with no trailing
  newline.
- The main menu is built from the database (`conf_menu.menu_ruta` / `confmapamenu.mmn_formulario`, per user
  profile) and each entry opens its form by reflection from the class name stored there. An entry with no form
  assigned does nothing when clicked (for example "Compras Sugeridas", which has no form). In the reduced backup
  reviewed in 2026-10, only 49 of the 236 `conf_menu` rows carry a `menu_ruta`; whether the rest are unimplemented
  or resolved another way has not been verified.
- `ControladorMain` also supports launching directly into a specific sub-form via command-line arguments (e.g.
  `parametrosContables`, `facturacion`, `frmPagare`) instead of the normal login flow — used for shortcuts/
  external launchers that jump straight to one screen.
- Runtime errors that escape to `Program.Main` are appended to a `log.txt` next to the executable. There is no
  global exception handler (`Application.ThreadException`). Seen under the debugger (F5): an unhandled exception
  in a form's event handler reached that `catch`, which shows "Excepcion no controlada: ...", writes `log.txt` and
  exits the whole application. Without a debugger WinForms would normally show its own unhandled-exception dialog
  instead; that behavior has not been checked in this app.

## Database notes

- Production is genuine Oracle **MySQL 5.5**. A local XAMPP server on a developer machine is **MariaDB**, which is
  fine for exploring a restored backup but is not a faithful stand-in for MySQL 5.5 or for a MySQL 8 upgrade.
- The bundled connector (`MySql.Data` 6.3.7.0, ~2011) cannot authenticate against MySQL 8's default
  `caching_sha2_password`; an upgrade to MySQL 8.x requires replacing it.
- The SQL relies on lenient MySQL 5.5 behavior: roughly 22 distinct `GROUP BY` queries select non-grouped columns
  (rejected under `ONLY_FULL_GROUP_BY`), and zero dates (`0000-00-00`) exist in the data. Most tables are
  `latin1_swedish_ci`; the web-module tables are `utf8mb4`. Literals such as `'€8'` in SQL depend on the
  connection charset (the euro is stored as the single byte `0x80`).
- `admhistodivi` (exchange-rate history) can lack the rows the code looks up; `Producto.cargarProductos()` and
  `buscarProductos()` guard against an empty result instead of indexing `Rows[0]`.

## Architecture

**Entry flow:** `Program.cs` → `ControladorMain` → `frmLogin` (login form) → on successful auth, `Principal`
(the main menu/MDI-style shell) is shown, from which all module forms are opened.

**Layered structure:**
- `clasesData/` — cross-cutting data/domain classes shared by the whole app:
  - `ConexionBD.cs` — the sole MySQL access layer (wraps `MySql.Data.MySqlClient`). All queries are raw SQL
    strings built via string concatenation (no ORM, no query builder). Provides overloads returning
    `DataSet`/`DataTable`/`MySqlDataReader`, plus bulk-insert helpers that take a `DataTable` or
    `DataGridView` and a parallel array of parameter names. Database selection happens via
    `modificarConexionString(int)` (1=`db1`, 2=`db2`, 3=`db3`) or `conectionStringSysconta(string)` for
    per-company accounting schemas (suffixed like `sysconta_02`).
  - `Compania.cs` — current company/business entity (name, RIF, addresses, currency, per-company flags).
  - `UsuarioSistema.cs` (extends `Compania`) — logged-in user, credentials validation, permissions, and menu
    map (`mapaMenu`) used to build the main menu.
  - `Inputbox.cs`, `FuncionesTexbox.cs` — shared UI helpers (generic input dialogs, textbox formatting/
    validation helpers reused across forms).
- `clases/` — business/domain logic, one subfolder per module (`bancos`, `bulto`, `clientes`, `complementos`,
  `compras`, `contabilidad`, `facturacion`, `gerencial`, `importaciones`, `pagos`, `pedidos`, `producto`,
  `proveedores`, `retenciones`, `tesoreria`, `vendedores`). These are the non-UI classes each module's forms
  depend on (e.g. fiscal printer integration under `clases/facturacion`).
- `formularios/` — WinForms UI, mirroring the same module breakdown (`facturacion`, `contabilidad`, `compras`,
  `pedidos`, `cxpagar`, `tesoreria`, `bancos`, `clientes`, `vendedores`, `productos`, `cotizacion`,
  `prefactura`, `importaciones`, `gerencial`, `administracion`, `reportes`, `transporte`,
  `facturacionMisionVivienda`, etc.). Each form is a standard `Form` + `.Designer.cs` + `.resx` triple, and
  talks to the database directly through `clasesData.ConexionBD` / classes in `clases/` — there is no service
  or repository layer between UI and SQL.
- `Reportes/` — Crystal Reports definitions (`.rpt`) and their typed `DataSet` schemas (`.xsd`/`.xsc`/`.xss`),
  organized by module (`bulto`, `compras`, `cotizacion`, `MovContab`, ...), rendered via the
  `CrystalDecisions.*` references.
- Root-level forms (`Principal.cs`, `frmLogin.cs`, `parametrosContables.cs`) are the main shell, login, and
  accounting parameters screens respectively; they don't belong to any `formularios/` subfolder because they
  predate/sit outside the modular split.
- `Diagramas/` — legacy class-diagram (`.cd`) files for the facturación and contabilidad areas; useful as a
  quick map of relationships in those modules if you need orientation before diving into the code.

**Legacy/dead code — do not build on these:** `Malo_Julio2018/` and the `clases/Copia de facturacion`,
`clases/Copia de importaciones`, `formularios/Copia de facturacion` folders are abandoned backup copies not
referenced by `lebrun.csproj`. Treat any file under a `Copia de ...` or `Malo_*` path as historical reference
only, never as the live implementation. Other uncompiled duplicates exist too (`clases/complementos/Nueva carpeta/`,
`clases/facturacion/SENIAT/clases/`, `formularios/complementos/frmConsultaArticulos.cs`). When a class appears
in more than one place, check which file has a `<Compile Include>` entry in `lebrun.csproj` before editing it.

## Conventions to know before editing

- Spanish naming throughout (classes, methods, form fields, UI text) — match the existing language when adding
  members to a module rather than mixing in English.
- Forms prefixed `frm` or `lbx` follow existing naming per module (`lbx` tends to be used for list/browse
  screens). Follow the sibling files in the same `formularios/<module>` folder for the pattern in use there.
- SQL is inline string concatenation built ad hoc per call site (see `clasesData/ConexionBD.cs`); there is no
  central query layer to route new queries through — add new queries the same way existing ones in the same
  module are written, and prefer `MySqlCommand` parameters (`AddWithValue`) over concatenating user input where
  the surrounding code already does so.
- Company-specific behavior is generally branched on `Compania.Codigo` (`"01"`–`"06"`) or on the selected
  database suffix (`_02`, `_03`, ...) rather than through configuration — check for existing `switch`/`if` on
  company code before assuming a change applies uniformly across companies.
