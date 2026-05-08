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
  - [x] Neutralized UI Surface and Zoom dependencies
  - [x] Decoupled PID and Input controllers
  - [x] Resolved property package ambiguity
  - [x] Successfully broke circular dependency with `UnitOperations`

## Phase 3: Automation & API (IN PROGRESS)

- [ ] **DWSIM.Automation**
  - [ ] Convert to SDK-style (.NET 8)
  - [ ] Link against headless `FlowsheetBase`
- [ ] **DWSIM.API** (New)
  - [ ] ASP.NET Core wrapper for Linux deployment

## Phase 4: Validation (PENDING)

- [ ] **DWSIM.Automation.Tests**
  - [ ] Verify core solver integrity on Linux
  - [ ] Benchmark against legacy Windows version

## Standards & Integrity

- [x] Define migration standards in `AGENTS.md`
- [x] Maintain 100% Linux compilation for migrated packages
- [x] Add `// TODO: [MIGRATION]` for all stripped features
