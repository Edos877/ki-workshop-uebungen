# Übung 3 · Alternative: Eigenen Command bauen

**Zeit:** 70 Minuten (55 entwickeln, 15 vorstellen)

Der Hauptweg dieser Übung ist [`03-agenten-ablauf.md`](03-agenten-ablauf.md).
Diese Alternative ist für alle, die lieber einen wiederverwendbaren Auftrag
für ihr Team bauen.

## Ziel

Eine Aufgabe, die Sie regelmäßig erledigen, einmal sauber als Command
verpacken, damit das ganze Team denselben Ablauf nutzen kann.

## Zwei Stufen

Die zweite baut auf der ersten auf.

| Stufe | Aufgabe |
|---|---|
| **Basis** | Einen eigenen Command bauen und testen (Ablauf unten). |
| **Plus** | Den Command um eine **Gegenprüfung** erweitern: Ein zweiter Schritt oder Subagent prüft jedes Ergebnis und verwirft, was nicht belegt ist. |

## Beispiel

- Claude Code: `.claude/commands/pr-beschreibung.md`, Aufruf mit `/pr-beschreibung`
- GitHub Copilot: `.github/prompts/pr-beschreibung.prompt.md`

In Claude Code können Sie denselben Auftrag auch als Skill anlegen:
`.claude/skills/<name>/SKILL.md`. Commands sind das ältere Format und
funktionieren weiter.

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
