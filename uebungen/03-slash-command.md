# Übung 3 · Eigenen Slash Command bauen

**Zeit:** 45 Minuten · **Form:** allein oder zu zweit, wie Sie möchten

## Ziel

Eine Aufgabe, die Sie regelmäßig erledigen, einmal sauber als Command
verpacken, damit das ganze Team denselben Ablauf nutzen kann.

## Drei Stufen

Wählen Sie Ihre Stufe. Jede baut auf der vorigen auf, Sie können jederzeit
weitergehen.

| Stufe | Aufgabe |
|---|---|
| **Basis** | Einen eigenen Command bauen und testen (Ablauf unten). |
| **Plus** | Den Command um eine **Gegenprüfung** erweitern: Ein zweiter Schritt oder Subagent prüft jedes Ergebnis und verwirft, was nicht belegt ist. |
| **Profi** | Eine neue Funktion mit dem **Agenten-Ablauf** umsetzen: [03-profi-expressversand.md](03-profi-expressversand.md). |

## Beispiel

- Claude Code: `.claude/commands/pr-beschreibung.md`, Aufruf mit `/pr-beschreibung`
- GitHub Copilot: `.github/prompts/pr-beschreibung.prompt.md`

## Ablauf

1. **Aufgabe wählen** (Ideen unten oder aus Ihrem Alltag).
2. **Ablauf und Fertig-Kriterium aufschreiben:** Was soll die KI lesen, was
   tun, wie soll das Ergebnis aussehen, was darf sie nicht?
3. **Command-Datei anlegen.**
4. **Im Übungsrepo testen** und nachschärfen, bis das Ergebnis passt.
5. **Kurz vorstellen:** Was spart der Command Ihnen?

## Ideen

- `/test-plan` – Testfälle inklusive Grenzfällen für eine Klasse vorschlagen
- `/review-checkliste` – Diff gegen `material/checkliste-ki-code.md` prüfen
- `/commit-nachricht` – Commit-Text nach Team-Konvention aus dem Diff
- `/erklaere-code` – eine Klasse für neue Kollegen verständlich erklären
- `/changelog` – Änderungen seit dem letzten Tag zusammenfassen

- `/akzeptanzkriterien` – stellt zuerst Rückfragen und schlägt dann Akzeptanzkriterien vor

## Stufe Plus: Gegenprüfung einbauen

Ergänzen Sie Ihren Command um einen zweiten Schritt, zum Beispiel:

```markdown
Schritt 2: Prüfe jedes Ergebnis aus Schritt 1 gegen den Code.
Versuche, es zu widerlegen. Übernimm nur, was du mit einer Codestelle
belegen kannst. Markiere den Rest als „verworfen“ oder „unbelegt“.
```

In Claude Code können Sie Schritt 2 auch an einen Subagenten geben, damit er
mit frischem Kontext prüft. Vergleichen Sie: Wie viele Ergebnisse überstehen
die Gegenprüfung?

## Gute Commands haben

- eine klare Aufgabe statt vieler,
- einen festen Aufbau für das Ergebnis,
- die Anweisung, nichts zu erfinden und Unklares nachzufragen.
