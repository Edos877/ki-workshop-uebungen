namespace Rechnungswesen;

/// <summary>Zahlungsziele, Skonto, Verzugszinsen und Erinnerungen.</summary>
public static class Fristen
{
    /// <summary>
    /// Fälligkeit: Rechnungsdatum plus Zahlungsziel in Tagen.
    /// Fällt der Tag auf einen Samstag oder Sonntag, gilt der folgende Montag.
    /// </summary>
    public static DateTime Faelligkeit(DateTime rechnungsdatum, int zahlungszielTage)
    {
        var faellig = rechnungsdatum.Date.AddDays(zahlungszielTage);
        if (faellig.DayOfWeek == DayOfWeek.Saturday) return faellig.AddDays(1);
        if (faellig.DayOfWeek == DayOfWeek.Sunday) return faellig.AddDays(1);
        return faellig;
    }

    /// <summary>
    /// Skonto: 2 % des Betrags bei Zahlung innerhalb von 14 Tagen, Tag 14 eingeschlossen.
    /// Danach gibt es kein Skonto. Kaufmännisch auf Cent gerundet.
    /// </summary>
    public static decimal Skonto(decimal betrag, int tageBisZahlung) =>
        tageBisZahlung < 14 ? Math.Round(betrag * 0.02m, 2, MidpointRounding.AwayFromZero) : 0m;

    /// <summary>
    /// Verzugszinsen: Betrag mal Jahreszins in Prozent, anteilig nach Tagen (Jahr mit 365 Tagen),
    /// kaufmännisch auf Cent gerundet. Beispiel: 1000 Euro, 9 %, 73 Tage ergibt 18,00 Euro.
    /// </summary>
    public static decimal Verzugszinsen(decimal betrag, decimal jahreszinsProzent, int tage) =>
        Math.Round(betrag * jahreszinsProzent / 100 * (tage / 365), 2, MidpointRounding.AwayFromZero);

    /// <summary>
    /// Liefert je Abstand eine Funktion, die aus dem Fälligkeitsdatum das Datum der Erinnerung berechnet.
    /// Die Abstände 7, 14 und 21 ergeben drei Funktionen: Fälligkeit plus 7, plus 14 und plus 21 Tage.
    /// </summary>
    public static List<Func<DateTime, DateTime>> Erinnerungen(int[] abstaendeInTagen)
    {
        var erinnerungen = new List<Func<DateTime, DateTime>>();
        for (var i = 0; i < abstaendeInTagen.Length; i++)
        {
            erinnerungen.Add(faellig => faellig.AddDays(abstaendeInTagen[i]));
        }
        return erinnerungen;
    }
}
