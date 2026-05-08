# DWSIM Migration Progress Tracking

## Phase 1: Foundation Layer (COMPLETED)

- [x] **DWSIM.Interfaces**
- [x] **DWSIM.Logging**
- [x] **DWSIM.GlobalSettings**
- [x] **DWSIM.SharedClassesCSharp**
- [x] **DWSIM.Serializers.XML**

## Phase 2: Engine Layer (COMPLETED)

- [x] **DWSIM.Math** (All sub-projects)
- [x] **DWSIM.Thermodynamics** (Core + Advanced EOS)
- [x] **DWSIM.UnitOperations** (Core + Stubs)
- [x] **DWSIM.FlowsheetSolver**
- [x] **DWSIM.FlowsheetBase**
  - [x] Stripped `System.Drawing`, `Windows.Forms`, `Eto`, `SkiaSharp`
  - [x] Neutralized UI Surface and Zoom dependencies via `GraphicObject.vb` stubs
  - [x] Decoupled PID and Input controllers
  - [x] Resolved property package ambiguity
  - [x] Successfully broke circular dependency with `UnitOperations`
  - [x] Implemented headless `FlowsheetSurfaceStub` for connection management

## Phase 3: Automation & API (COMPLETED)

- [x] **DWSIM.Automation**
  - [x] Converted to SDK-style (.NET 8)
  - [x] Linked against headless `FlowsheetBase`
  - [x] Removed dependencies on `unvell.ReoGrid` and spreadsheet logic
  - [x] Transitioned from `#if HEADLESS` to pure code omission
- [ ] **DWSIM.API** (New)
  - [ ] ASP.NET Core wrapper for Linux deployment

## Phase 4: Validation (COMPLETED)

- [x] **DWSIM.Automation.Tests.CSharp**
  - [x] Verified core solver integrity on Linux (Load/Connect/Solve/Save)
  - [x] Confirmed thermodynamic calculation results (334.3 K / 350 kg/s)
- [ ] **Benchmarking**
  - [ ] Benchmark against legacy Windows version

## Standards & Integrity

- [x] Define migration standards in `AGENTS.md`
- [x] Maintain 100% Linux compilation for migrated packages
- [x] Pure code omission/stubbing (Removed all `#if HEADLESS` blocks)
- [x] Fixed resource loading via flattened manifest names in SDK projects
- [x] Consolidated all legacy DLLs into root-level `/References` folder
