using System.Globalization;

namespace Rechnungswesen;

/// <summary>Rechnungsnummern, Dateinamen und Zeitstempel.</summary>
public static class Dokumente
{
    /// <summary>
    /// Nächste Rechnungsnummer im selben Jahr. "RE-2025-0099" ergibt "RE-2025-0100".
    /// Die laufende Nummer hat mindestens vier Stellen.
    /// </summary>
    public static string NaechsteNummer(string nummer)
    {
        var teile = nummer.Split('-');
        if (teile.Length != 3 || teile[0] != "RE") throw new FormatException($"Keine Rechnungsnummer: {nummer}");
        var laufend = int.Parse(teile[2], CultureInfo.InvariantCulture) + 1;
        return $"RE-{teile[1]}-{laufend.ToString("D4", CultureInfo.InvariantCulture)}";
    }

    /// <summary>
    /// Dateiname der Rechnung: Nummer, Unterstrich, Rechnungsdatum als Jahr-Monat-Tag.
    /// Beispiel: RE-2025-0100 vom 3. Februar 2025 ergibt "RE-2025-0100_2025-02-03.pdf".
    /// </summary>
    public static string Dateiname(string nummer, DateTime rechnungsdatum) =>
        $"{nummer}_{rechnungsdatum:yyyy-mm-dd}.pdf";

    /// <summary>
    /// Zeitstempel für das Protokoll im 24-Stunden-Format.
    /// Beispiel: 3. Februar 2025, 15:07 Uhr ergibt "03.02.2025 15:07".
    /// </summary>
    public static string Zeitstempel(DateTime zeitpunkt) =>
        zeitpunkt.ToString("dd.MM.yyyy hh:mm", CultureInfo.InvariantCulture);
}
