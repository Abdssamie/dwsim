# DWSIM Migration Standards (AI-Focused)

## 1. Project Format
- **Standard:** Use modern SDK-style project files (`.csproj`, `.vbproj`).
- **Target:** Always target `net8.0` for cross-platform compatibility.
- **Naming:** NO platform suffixes (e.g., use `DWSIM.Interfaces.vbproj`, NOT `DWSIM.Interfaces.Linux.vbproj`).

## 2. UI Decoupling
- **Action:** Remove all references to `System.Windows.Forms` and `System.Drawing`.
- **Bridge:** Use `Object` or `System.Object` for interface members that were previously UI-dependent.
- **Clean Core:** Core logic packages must compile on Linux/macOS with zero UI dependencies.

## 3. Documentation (TODOs)
- **Pattern:** `// TODO: [MIGRATION] <description>`
- **Usage:** Mark every stripped UI feature or factory that requires a cross-platform implementation.
- **Example:** `// TODO: [MIGRATION] Implement cross-platform FilePicker for headless mode.`

## 4. Verification
- **Command:** `dotnet build <project>` must succeed on Linux.
- **Scope:** Verify every package after refactoring.
