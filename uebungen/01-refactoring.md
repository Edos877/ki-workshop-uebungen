# Übung 1 · Refactoring mit KI führen

**Zeit:** 35 Minuten (25 arbeiten, 10 auswerten)

## So arbeiten Sie

Sie schreiben den Code nicht von Hand. Sie führen die KI (Claude Code oder
GitHub Copilot im Agent-Modus): Auftrag geben, Plan freigeben, Tests laufen
lassen, Ergebnis beurteilen. Legen Sie zuerst einen eigenen Branch an:

```bash
git checkout -b <ihr-kürzel>
```

## Ziel

Die Methode `OrderService.ProcessOrder` ist lang und unübersichtlich. Bauen Sie
sie mit KI so um, dass sie gut lesbar ist, **ohne das Verhalten zu ändern.**

Alternativ in TypeScript: `ts/src/pricing.ts`, Funktion `calc`.

## Ablauf

1. **Prompt schreiben.** Ein guter Auftrag beantwortet fünf Fragen:
   - **Ziel:** Was soll am Ende anders sein?
   - **Kontext:** Womit arbeiten wir (Technik, Tests, Regeln)?
   - **Grenzen:** Was darf sich nicht ändern?
   - **Vorgehen:** Erst ein Plan, dann auf Freigabe warten.
   - **Abnahme:** Woran erkennen wir, dass es fertig ist?
2. **Erst einen Plan erstellen lassen**, noch keinen Code. Plan prüfen und
   bei Bedarf korrigieren.
3. **Schritt für Schritt umsetzen lassen**, ein Schritt pro Auftrag.
4. **Nach jedem Schritt testen:** `dotnet test` bzw. `npm test`.
5. **Vergleichen:** Zwei, drei Ergebnisse werden in der Runde gezeigt. Wie sind die anderen vorgegangen?

## Fertig, wenn

- alle bestehenden Tests unverändert grün sind,
- die Geschäftsregeln aus dem Kommentar über der Klasse klar im Code erkennbar sind,
- keine Methode länger als etwa 30 Zeilen ist.

## Zum Nachdenken

- Wo hat die KI ohne Rückfrage Annahmen getroffen?
- Hat sie versucht, Tests zu ändern?
- Wie groß waren die einzelnen Schritte?

## Für Schnelle

Schon nach zehn Minuten fertig? Dann gehen Sie direkt zur Stufe Profi.

**Plus:** Ergänzen Sie in `CLAUDE.md` (oder `.github/copilot-instructions.md`) eine fehlende Projektregel, zum Beispiel eine Namenskonvention oder eine Regel zur Fehlerbehandlung. Führen Sie denselben Auftrag erneut aus und vergleichen Sie das Ergebnis.

**Profi:** Starten Sie neu und lassen Sie die KI vor dem Plan zuerst
Rückfragen stellen: *„Stell mir alle Fragen, die du für ein sicheres
Refactoring brauchst. Fang erst danach an.“* Welche Annahmen hätte sie
sonst stillschweigend getroffen?
