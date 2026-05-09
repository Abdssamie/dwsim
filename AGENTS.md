# DWSIM Migration Standards (AI-Focused)

## 0. Backward Compatibility Is Not A Goal

**VERY IMPORTANT: NEVER spend time preserving backward compatibility during this migration.**

This repository is being moved to a clean headless/cloud architecture. Agents must prefer the new architecture over legacy compatibility every time.

- Do not add compatibility shims for old UI, old platform-specific projects, old package layouts, or old runtime assumptions.
- Do not keep obsolete APIs alive just because existing callers might depend on them.
- Do not preserve old behavior when it conflicts with headless execution, Linux/macOS support, service architecture, or cloud deployment.
- Remove legacy code instead of wrapping it when the old code is out of scope.
- Breaking changes are acceptable and expected when they simplify the migrated engine.
- If a caller breaks because it depended on legacy UI/platform behavior, update or remove that caller.
- Do not ask whether backward compatibility is required. It is not.

## 1. Project Format

- **Standard:** Use modern SDK-style project files (`.csproj`, `.vbproj`).
- **Target:** Follow the target framework already used by the project being edited unless the user explicitly asks for a migration.
- **Naming:** NO platform suffixes (e.g., use `DWSIM.Interfaces.vbproj`, NOT `DWSIM.Interfaces.Linux.vbproj`).

## 2. UI Decoupling (Pure Headless Mode)

- **Standard:** The goal is a 100% headless engine. Do NOT plan for future UI implementation in core projects.
- **Action:** PERMANENTLY REMOVE all code blocks, fields, and properties that depend on `System.Windows.Forms` or `System.Drawing`. Do not just comment them out.
- **Bridge:** Use `Object` or `System.Object` for interface members that were previously UI-dependent if they cannot be removed from the interface.
- **Clean Core:** Core logic packages must compile on Linux/macOS with zero UI dependencies.

## 3. Documentation (TODOs)

- **Pattern:** `// TODO: [MIGRATION] <description>`
- **Usage:** Use ONLY for logic-critical features that were stripped (e.g., native library loading, cross-platform file paths).
- **Prohibition:** DO NOT use TODOs for UI features. UI logic is considered out of scope for the engine.
- **Example:** `// TODO: [MIGRATION] Support cross-platform native library loading for lpsolve55.`

## 4. Verification

- **Command:** `dotnet build <project>` must succeed on Linux.
- **Scope:** Verify every package after refactoring.
