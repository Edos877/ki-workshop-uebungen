# KI-Workshop · Übungsrepo

Übungsprojekt für den Workshop **„Generative KI in der Softwareentwicklung“**
von [KI-Fit](https://ki-fit.com). Ein kleiner, fiktiver Bestellservice eines
Onlineshops: kein Produktivcode, keine echten Daten, keine Zugangsdaten.

## Einrichtung (bitte vor dem Workshop testen)

Benötigt:
- Git
- **.NET 8 SDK** oder neuer
- **Node.js 20** oder neuer (nur für die TypeScript-Variante)
- Visual Studio, VS Code oder Rider
- Claude Code und/oder GitHub Copilot mit Ihrer Lizenz

```bash
git clone https://github.com/Edos877/ki-workshop-uebungen.git
cd ki-workshop-uebungen

dotnet test            # erwartet: 18 Tests bestanden

cd ts
npm install
npm test               # erwartet: 11 Tests bestanden
```

Wenn beide Testläufe grün sind, ist alles bereit.

## Aufbau

| Ordner | Inhalt |
|---|---|
| `src/Bestellservice` | C#-Code: Bestellverarbeitung, Rabattberechnung |
| `tests/Bestellservice.Tests` | xUnit-Tests |
| `ts/` | TypeScript-Variante der Preisberechnung (Vitest) |
| `uebungen/` | Aufgabenblätter der drei Übungen |
| `material/` | Checkliste für KI-generierten Code, Team-Regeln |
| `CLAUDE.md`, `.github/copilot-instructions.md` | Projektregeln für die KI |
| `.claude/commands/`, `.github/prompts/` | Beispiel für einen eigenen Command |

## Übungen

1. [Refactoring mit KI führen](uebungen/01-refactoring.md)
2. [Tests und Pull-Request-Review](uebungen/02-tests-review.md)
3. [Eigenen Slash Command bauen](uebungen/03-slash-command.md), mit Stufe Profi: [Expressversand mit Agenten-Ablauf](uebungen/03-profi-expressversand.md)

Jede Übung hat Zusatzstufen für alle, die schneller fertig sind.

Arbeiten Sie bitte auf einem eigenen Branch, zum Beispiel `team-1`.

## Wichtig

Bitte geben Sie in den Übungen keine vertraulichen Projekt- oder Kundendaten
in KI-Werkzeuge ein. Dieses Repo enthält ausschließlich Beispielcode.
