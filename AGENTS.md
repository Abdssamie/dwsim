# DWSIM Migration Standards (AI-Focused)

## 1. Project Format
- **Standard:** Use modern SDK-style project files (`.csproj`, `.vbproj`).
- **Target:** Always target `net8.0` for cross-platform compatibility.
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
