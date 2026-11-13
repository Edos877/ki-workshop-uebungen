namespace Rechnungswesen;

public record Position(string Bezeichnung, decimal Einzelpreis, int Menge)
{
    public decimal Netto => Einzelpreis * Menge;
}

/// <summary>Eine Rechnung mit Positionen. Alle Preise sind Nettopreise in der Rechnungswährung.</summary>
public class Rechnung
{
    public required string Nummer { get; init; }
    public required DateTime Datum { get; init; }
    public required string Land { get; init; }
    public string Waehrung { get; init; } = "EUR";
    public int ZahlungszielTage { get; init; } = 30;
    public List<Position> Positionen { get; init; } = new();

    public decimal Netto => Betraege.Summe(Positionen.Select(p => p.Netto));

    public decimal Brutto => Steuer.Brutto(Netto, Land);

    public DateTime Faellig => Fristen.Faelligkeit(Datum, ZahlungszielTage);

    public string Dateiname => Dokumente.Dateiname(Nummer, Datum);

    /// <summary>Offener Betrag nach Abzug von Skonto, wenn nach der angegebenen Zahl von Tagen gezahlt wird.</summary>
    public decimal ZahlbetragBei(int tageBisZahlung) => Brutto - Fristen.Skonto(Brutto, tageBisZahlung);
}
