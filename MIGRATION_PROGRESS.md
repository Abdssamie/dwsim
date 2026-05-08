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
  - [x] Implemented `HEADLESS` conditional compilation for spreadsheet components
  - [x] Removed dependencies on `Eto.Forms` and `FormMain`
- [ ] **DWSIM.API** (New)
  - [ ] ASP.NET Core wrapper for Linux deployment

## Phase 4: Validation (COMPLETED)

- [x] **DWSIM.Automation.Tests.CSharp**
  - [x] Verified core solver integrity on Linux (Load/Connect/Solve/Save)
  - [x] Confirmed thermodynamic calculation results (PR Property Package)
- [ ] **Benchmarking**
  - [ ] Benchmark against legacy Windows version

## Standards & Integrity

- [x] Define migration standards in `AGENTS.md`
- [x] Maintain 100% Linux compilation for migrated packages
- [x] Surgical decoupling using `#if HEADLESS` to preserve original logic
- [x] Fixed resource loading via flattened manifest names in SDK projects
