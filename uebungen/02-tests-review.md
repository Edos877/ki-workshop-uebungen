# Übung 2 · Tests und Pull-Request-Review

**Zeit:** 45 Minuten

Auch hier arbeitet die KI, Sie führen und beurteilen. Bleiben Sie auf Ihrem
Branch aus Übung 1.

## Teil A · Tests (ca. 20 Minuten)

`DiscountCalculator` in `src/Bestellservice/DiscountCalculator.cs` hat noch
keine Tests. Die Regeln stehen im Kommentar über der Klasse.

1. Lassen Sie die KI **zuerst Testfälle planen**, inklusive Grenzfällen.
   Noch keinen Testcode.
2. Prüfen Sie die Liste: Fehlt etwas? Ist etwas überflüssig?
3. Lassen Sie die Tests schreiben und führen Sie sie aus.
4. Schlägt ein Test fehl: Liegt der Fehler im Test oder im Code?

## Teil B · Review (ca. 25 Minuten)

Auf dem Branch `feature/treuebonus` liegt eine neue Funktion mit
Pull-Request-Beschreibung (`PR_BESCHREIBUNG.md`).

```bash
git fetch
git diff main...origin/feature/treuebonus
```

1. Lassen Sie die KI die Änderung reviewen, zum Beispiel mit `/review` in
   Claude Code oder im Copilot-Chat.
2. Bewerten Sie jeden Hinweis: **richtig**, **falsch** oder **unbelegt**?
3. Prüfen Sie auch die PR-Beschreibung: Stimmt sie mit dem Code überein?
4. Am Ende zeigen wir die Lösung gemeinsam am Beamer. Gleichen Sie Ihre Liste
   damit ab.

## Für Schnelle

- **Plus:** Lassen Sie dieselbe Änderung von einem zweiten Modell reviewen,
  zum Beispiel Sonnet auf höchster Stufe, und vergleichen Sie beide Listen.
- **Profi:** Geben Sie die Hinweise an eine neue Sitzung mit dem Auftrag:
  *„Versuche jeden Hinweis mit dem Code zu widerlegen. Bestätige nur, was du
  belegen kannst.“* Was bleibt übrig?

## Zum Nachdenken

- Was hat die KI gefunden, was übersehen?
- Wo hat sie Probleme gemeldet, die keine sind?
- Welche Prüfung hätte am schnellsten geholfen: Compiler, Tests, KI oder Mensch?
