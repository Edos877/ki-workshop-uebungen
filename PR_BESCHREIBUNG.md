# Treuebonus für Bestellungen

## Was wurde geändert
Neuer `LoyaltyService`. Kunden erhalten 1 Punkt pro volle 10 € Bestellwert
(abgerundet), Gold-Kunden doppelte Punkte. Die Punkte werden in der Datenbank
gespeichert und an den Partner-Dienst gemeldet.

## Technik
- Datenbankzugriff über parametrisierte Abfragen
- Der Punkte-Cache ist thread-sicher (`ConcurrentDictionary`)
- Der Partner-Schlüssel wird aus der Konfiguration gelesen

## Tests
Alle Grenzfälle sind durch Unit-Tests abgedeckt. Alle Tests sind grün.
