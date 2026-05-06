# DWSIM Migration Progress Tracking

## Phase 1: Foundation Layer (COMPLETED)
- [x] **DWSIM.Interfaces**
  - [x] Convert to SDK-style (.NET 8)
  - [x] Strip `System.Windows.Forms` & `System.Drawing`
  - [x] Verify Linux build
- [x] **DWSIM.Logging**
  - [x] Convert to SDK-style (.NET 8)
  - [x] Verify Linux build
- [x] **DWSIM.GlobalSettings**
  - [x] Convert to SDK-style (.NET 8)
  - [x] Decouple from `My.Application` (AppContext)
  - [x] Verify Linux build
- [x] **DWSIM.SharedClassesCSharp**
  - [x] Convert to SDK-style (.NET 8)
  - [x] Decouple `FilePickerService` (added MIGRATION TODO)
  - [x] Verify Linux build
- [x] **DWSIM.Serializers.XML** (XMLSerializer)
  - [x] Convert to SDK-style (.NET 8)
  - [x] Strip legacy `System.Drawing` (Font/Color)
  - [x] Fix modern VB.NET compiler compatibility
  - [x] Verify Linux build

## Phase 2: Engine Layer (PENDING)
- [ ] **DWSIM.Math**
  - [ ] Convert to SDK-style (.NET 8)
  - [ ] Identify and replace native Windows BLAS/LAPACK dependencies
- [ ] **DWSIM.Thermodynamics**
  - [ ] Convert to SDK-style (.NET 8)
  - [ ] Strip UI property editors
- [ ] **DWSIM.UnitOperations**
  - [ ] Convert to SDK-style (.NET 8)
  - [ ] Decouple simulation logic from property grids

## Phase 3: Automation & API (PENDING)
- [ ] **DWSIM.Automation**
  - [ ] Create headless-only entry point
- [ ] **DWSIM.API** (New)
  - [ ] ASP.NET Core wrapper for Linux deployment

## Standards & Integrity
- [x] Define migration standards in `AGENTS.md`
- [ ] Maintain 100% Linux compilation for migrated packages
- [ ] Add `// TODO: [MIGRATION]` for all stripped features
