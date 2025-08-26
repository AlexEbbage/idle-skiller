# Copilot/Codex Prompts (paste into Chat)

## Implement a feature from an Issue

You are implementing <ISSUE_TITLE> for a Unity Android idle skiller.

Constraints:

- New files in full, placed only under:
  - /Assets/\_Project/Scripts/\*\*
  - /Assets/\_Project/Data/\*\*
  - /Assets/\_Project/Tests/\*\*
- Keep systems modular: interfaces in Core/ or Services/, concrete in Systems/.
- Add at least one NUnit test per non-trivial class in /Assets/\_Project/Tests/.
- Add XML docs and TODOs where stubs exist (Firebase/IAP/Ads).
- Provide a short "Hooking In" note at the end describing Prefabs to create, components to attach, and example usage.

Output:

1. Complete C# files
2. Tests
3. Hooking notes

## Write Unit Tests first

Write failing NUnit tests in `/Assets/_Project/Tests/` for <CLASS/FEATURE>. Cover happy path, edge cases, and error conditions. Keep tests small and focused.

## Refactor for Single Responsibility

Refactor <CLASS> to extract concerns into services/providers. Keep public API stable. Explain changes briefly.
