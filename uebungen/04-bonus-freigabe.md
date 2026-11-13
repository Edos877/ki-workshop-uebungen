# Bonus · Reif für die Freigabe?

**Für alle, die mit Übung 3 fertig sind und eine harte Nuss suchen.** Ohne
Zeitvorgabe.

## Die Lage

Unter `bonus/` liegt eine kleine Bibliothek für das Rechnungswesen: Beträge
lesen, Steuer berechnen, Fristen, Dateinamen. Sie ist reviewt, alle 15 Tests
sind grün, und nächste Woche soll sie live gehen.

Ein Kollege hat trotzdem ein schlechtes Gefühl: „Ich glaube, da sind noch
Fehler drin. Ich weiß nur nicht, wo.“

Er hat recht. Maßgeblich ist der Kommentar über jeder Methode: Er beschreibt
das richtige Verhalten, und die Kommentare stimmen. Wie viele Fehler es sind,
steht nirgends.

```bash
cd bonus
dotnet test        # 15 Tests, alle grün
```

## Stufe 1 · Von Hand (5 Minuten, ohne KI)

Lesen Sie den Code unter `bonus/src/Rechnungswesen` selbst, ohne KI. Notieren
Sie jeden Fehler, den Sie finden, mit Datei und Zeile. Ändern Sie noch nichts.

Stellen Sie einen Wecker. Nach fünf Minuten ist Schluss.

## Stufe 2 · Ein einziger Auftrag

Jetzt darf die KI ran, aber Sie haben nur **einen** Auftrag. Keine Nachfragen,
kein „such noch einmal“. Überlegen Sie vorher:

- Was genau soll sie prüfen, und woran?
- Woran erkennt sie, dass sie fertig ist?
- Wie verhindern Sie, dass sie etwas „repariert“, das gar nicht kaputt ist?

Regeln:

- Die vorhandenen Tests bleiben unverändert. Neue Tests sind erlaubt.
- Öffentliche Namen und Signaturen bleiben, wie sie sind.
- Jeder behobene Fehler bekommt einen Test, der ihn belegt.

Vergleichen Sie danach mit Ihrer Liste aus Stufe 1.

## Fertig melden

Wenn Sie überzeugt sind, dass kein Fehler mehr im Code steckt, melden Sie sich
bei der Workshopleitung. Sie bekommen dann die Abnahmetests, legen sie in
`bonus/tests/Rechnungswesen.Tests` und starten `dotnet test` noch einmal. Sie
haben **einen** Versuch.

## Stufe 3 · Aus dem Vorgehen ein Werkzeug machen

Die Fehler in dieser Bibliothek sind typisch. Sie stecken so auch in echtem
Code. Bauen Sie einen Command oder Skill `/freigabe-pruefung`, der eine
beliebige Bibliothek auf solche Fehler prüft:

- Er nennt nur Befunde mit Datei, Zeile und Beleg.
- Er trennt zwischen „belegt“ und „Verdacht“.
- Er ändert nichts, bevor Sie zustimmen.

Testen Sie ihn an einem frischen Stand dieser Bibliothek
(`git stash` oder ein zweiter Klon): Findet er ohne weiteres Zutun dieselben
Fehler wie Sie in Stufe 2?

## Zum Nachdenken

- Wie viele Fehler haben Sie von Hand gefunden, wie viele die KI?
- Welche Fehler konnten Sie mit bloßem Auge gar nicht sehen?
- Hat die KI etwas geändert, das vorher richtig war?
- Was aus Ihrem Auftrag gehört ab jetzt in jedes Review?
