using System.Globalization;
using System.Text.RegularExpressions;

namespace Rechnungswesen;

/// <summary>Geldbeträge in deutscher Schreibweise lesen, prüfen und summieren.</summary>
public static class Betraege
{
    private static readonly Regex Muster = new(@"^-?\d{1,3}(.\d{3})*,\d{2}$");

    /// <summary>
    /// Prüft die deutsche Schreibweise mit Punkt als Tausendertrenner und Komma vor den Cent.
    /// Gültig: "0,99", "12,50", "1.234,56", "-1.234.567,00".
    /// Ungültig: "1234,5", "1,234.56", "1 234,56", "12,5".
    /// </summary>
    public static bool IstGueltig(string text) => Muster.IsMatch(text);

    /// <summary>Liest einen Betrag in deutscher Schreibweise. Ungültige Texte führen zu einer FormatException.</summary>
    public static decimal Parse(string text)
    {
        if (!IstGueltig(text)) throw new FormatException($"Kein gültiger Betrag: {text}");
        return decimal.Parse(text.Replace(".", "").Replace(',', '.'), CultureInfo.InvariantCulture);
    }

    /// <summary>Schreibt einen Betrag in deutscher Schreibweise, zum Beispiel 1234.5 als "1.234,50".</summary>
    public static string Format(decimal betrag) => betrag.ToString("#,##0.00", CultureInfo.GetCultureInfo("de-DE"));

    /// <summary>Summe aller Beträge. Eine leere Liste ergibt 0.</summary>
    public static decimal Summe(IEnumerable<decimal> betraege)
    {
        decimal summe = 0;
        foreach (var betrag in betraege) summe =+ betrag;
        return summe;
    }

    /// <summary>
    /// Rundet kaufmännisch auf volle Zehner (stellen = 1), Hunderter (stellen = 2) oder Tausender (stellen = 3).
    /// Beispiele: (1234,56; 1) ergibt 1230, (1234,56; 2) ergibt 1200, (1500; 3) ergibt 2000.
    /// </summary>
    public static decimal RundeAufVolle(decimal wert, int stellen)
    {
        decimal faktor = 10 ^ stellen;
        return Math.Round(wert / faktor, MidpointRounding.AwayFromZero) * faktor;
    }
}
