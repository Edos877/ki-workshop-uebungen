# Übung 3 · Eine Funktion mit Agenten umsetzen

**Zeit:** 70 Minuten (55 umsetzen, 15 vorstellen)

Sie setzen eine neue Funktion nach dem empfohlenen Agenten-Ablauf um: erst
klären, dann bauen, dann unabhängig prüfen.

Lieber einen eigenen Command für Ihr Team bauen? Dann nehmen Sie die
Alternative: [`03-eigener-command.md`](03-eigener-command.md).

## Der Wunsch aus dem Fachbereich

> „Unsere Kunden sollen bei der Bestellung Expressversand wählen können.“

Mehr steht absichtlich nicht da. Alles Weitere muss die KI erfragen.

## Ablauf

1. **Rückfragen stellen lassen.** Geben Sie der KI nur den Wunsch oben und den
   Auftrag, zuerst alle offenen Fragen zu stellen. Noch kein Code.
   Die Antworten finden Sie in [`03-fachbereich.md`](03-fachbereich.md).
   Antworten Sie **nur auf das, was die KI fragt.**
2. **Akzeptanzkriterien festlegen.** Lassen Sie die KI aus den Antworten eine
   nummerierte Liste mit Akzeptanzkriterien schreiben, gern mit Rechenbeispielen.
   Prüfen Sie die Liste und geben Sie sie frei. Speichern Sie sie als
   `uebungen/expressversand-kriterien.md`.
3. **Plan erstellen lassen.** Kleine Schritte, je Schritt ein Test.
4. **Umsetzen lassen.** Nach jedem Schritt `dotnet test`. Die bestehenden
   Tests bleiben unverändert.
5. **Unabhängig prüfen lassen.** Eine neue Sitzung oder ein Subagent, am besten
   mit einem anderen Modell, prüft den Diff gegen jedes Kriterium:
   *„Prüfe diese Änderung gegen expressversand-kriterien.md. Baue nichts.
   Nenne zu jedem Kriterium: erfüllt, nicht erfüllt oder unbelegt, mit Beleg.“*
6. **Übernehmen oder nachbessern.** Nur wenn alle Kriterien belegt und alle
   Tests grün sind.
7. **Nachschärfen.** Was ist schiefgegangen? Halten Sie eine neue Regel in
   `CLAUDE.md` oder `.github/copilot-instructions.md` fest.

## Fertig, wenn

- jedes Akzeptanzkriterium durch einen Test belegt ist,
- alle bisherigen Tests unverändert grün sind,
- der Prüfer keinen Punkt als „nicht erfüllt“ oder „unbelegt“ meldet.

## Zum Nachdenken

- Welche Regel hätte die KI ohne Rückfrage falsch geraten?
- Hat der Prüfer etwas gefunden, das Sie übersehen hätten?
- Wie viel Zeit haben die Schritte 1 bis 3 gekostet, und hat es sich gelohnt?

## Werkzeuge

- **Claude Code:** Plan-Modus für Schritt 3, Subagent oder neue Sitzung mit
  `/model` für Schritt 5
- **GitHub Copilot:** Agent-Modus, für Schritt 5 einen neuen Chat mit einem
  anderen Modell öffnen

## Für Schnelle

- **Plus:** Bauen Sie aus Ihrem Vorgehen einen Command `/akzeptanzkriterien`,
  der zuerst Rückfragen stellt und dann Kriterien vorschlägt.
- **Profi:** Lassen Sie zwei verschiedene Modelle unabhängig prüfen und
  vergleichen Sie die Ergebnisse. Was findet nur eines von beiden?

## TypeScript-Variante

Dieselben Regeln in `ts/src/pricing.ts`, ohne die Bestätigungsmail. Tests mit
`npm test`.
