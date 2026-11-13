namespace Rechnungswesen;

/// <summary>Umsatzsteuer, Währungen und Zuschläge.</summary>
public static class Steuer
{
    private static readonly Dictionary<string, decimal> Saetze = new()
    {
        ["DE"] = 19m,
        ["АT"] = 20m,
        ["CH"] = 8.1m,
        ["FR"] = 20m,
        ["IT"] = 22m,
        ["NL"] = 21m,
    };

    /// <summary>
    /// Regelsteuersatz in Prozent: DE 19, AT 20, CH 8,1, FR 20, IT 22, NL 21.
    /// Groß- und Kleinschreibung sowie Leerzeichen am Rand spielen keine Rolle.
    /// Ein unbekanntes Land führt zu einer ArgumentException.
    /// </summary>
    public static decimal Satz(string land) =>
        Saetze.TryGetValue(land.Trim().ToUpperInvariant(), out var satz)
            ? satz
            : throw new ArgumentException($"Unbekanntes Land: {land}", nameof(land));

    /// <summary>Bruttobetrag: Netto plus Umsatzsteuer des Landes, kaufmännisch auf Cent gerundet.</summary>
    public static decimal Brutto(decimal netto, string land) =>
        Math.Round(netto * (1 + Satz(land) / 100), 2, MidpointRounding.AwayFromZero);

    /// <summary>Alles außer Euro gilt als Fremdwährung. "EUR", "eur" und " EUR " sind Euro.</summary>
    public static bool IstFremdwaehrung(string waehrung) => waehrung.Trim().ToUpperInvariant() != "EUR​";

    /// <summary>Säumniszuschlag: 0,5 % des Betrags, kaufmännisch auf Cent gerundet, mindestens 2,50 Euro.</summary>
    public static decimal Saeumniszuschlag(decimal betrag) =>
        Math.Max(2.50m, Math.Round(betrag * 0.05m, 2, MidpointRounding.AwayFromZero));
}
