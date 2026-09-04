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

Requires MSBuild for .NET Framework 4.8 (Visual Studio 2022/2026 with the ".NET desktop development" workload)
on Windows, since this targets WinForms and references COM/Interop assemblies (Crystal Reports, Office Excel
Interop, a serial-port `MSComm`/`MSComDlg` control, and a `TM_FISCAL` fiscal-printer COM component) that aren't
available cross-platform and must be registered on the build/run machine.

```powershell
& "C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\amd64\MSBuild.exe" lebrun.csproj /p:Configuration=Debug
```

(Adjust the MSBuild path for your VS edition/version — there's no `msbuild` on `PATH` by default outside a
Developer Command Prompt.) Confirmed working: both `Configuration=Debug` and `Configuration=Release` build
clean (zero `error CS`/`error MSB`) against VS2022 Community with the CR 13.0.4000.0 runtime installed. Expect
~20 harmless `warning MSB3179` lines ("Error al aislar la referencia COM ...") for `MSComDlg`/`MSCommLib`/
`TM_FISCAL` if those COM components aren't registered on the build machine — this doesn't fail the build.
`GenerateManifests` is set to `false` in the csproj because the leftover ClickOnce publish settings
(`Install=true`, `PublishUrl`, etc. — unused boilerplate, the app is deployed by xcopy, not ClickOnce) made
`GenerateApplicationManifest` crash trying to isolate those same unregistered COM components in Release builds.

There are no automated tests and no lint configuration in this repository.

## Runtime configuration

- `App.config` `appSettings` holds the MySQL connection parameters (`server`, `db1`/`db2`/`db3`, `login`,
  `password`, `port`). Three logical databases are used per company: `sisadm` (main app data), `sysconf`
  (system/config), `sysconta` (accounting) — selected by index (1/2/3) when opening a connection.
- At startup, `ControladorMain` (`ControladorMain.cs`) overrides the `server` setting by reading an `ip.txt` file
  from the executable's directory (`Application.StartupPath`). This file is expected to exist in the deployed
  output but is not part of the source tree.
- `ControladorMain` also supports launching directly into a specific sub-form via command-line arguments (e.g.
  `parametrosContables`, `facturacion`, `frmPagare`) instead of the normal login flow — used for shortcuts/
  external launchers that jump straight to one screen.
- Runtime errors that escape to `Program.Main` are appended to a `log.txt` next to the executable.

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
only, never as the live implementation.

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
