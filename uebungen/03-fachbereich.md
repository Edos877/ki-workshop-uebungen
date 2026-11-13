# Antworten des Fachbereichs zum Expressversand

**Bitte nur nachschlagen, wenn die KI danach fragt.** Fragt sie etwas, das hier
nicht steht, entscheiden Sie selbst und halten es in den Akzeptanzkriterien fest.

| Frage | Antwort |
|---|---|
| In welche Länder gibt es Express? | Nur nach **Deutschland und Österreich**. Für die Schweiz wird die Bestellung abgelehnt mit dem Fehler „Expressversand für dieses Lieferland nicht verfügbar“. |
| Was kostet Express? | Ein **Aufschlag**: DE 7,90 €, AT 12,90 € (netto). |
| Ersetzt der Aufschlag den normalen Versand? | Nein, er kommt **zusätzlich** zum normalen Versand. |
| Und wenn der normale Versand kostenlos ist? | Dann fällt **nur der Aufschlag** an. Express ist nie kostenlos. |
| Gibt es Vergünstigungen? | **Gold-Kunden zahlen den halben Aufschlag.** Silver und Standard zahlen den vollen. |
| Gelten Rabatte und Gutscheine auch für den Aufschlag? | Nein, Rabatte gelten nur für die Ware. |
| Wird der Aufschlag versteuert? | Ja, genau wie der Versand. |
| Wo steht der Aufschlag im Ergebnis? | Im Feld `Shipping` von `OrderResult`, zusammen mit dem normalen Versand. |
| Wie wählt man Express? | Neue Eigenschaft `Express` an `Order`, Standard ist `false`. Diese Änderung der Schnittstelle ist freigegeben. |
| Ändert sich die Bestätigungsmail? | Ja, bei Express steht zusätzlich die Zeile „Versandart: Express“ darin. |
| Was ist mit Bestellungen ohne Express? | Die verhalten sich **genau wie bisher**. |
