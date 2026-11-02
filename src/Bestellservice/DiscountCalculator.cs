namespace Bestellservice;

/// <summary>
/// Mengenrabatt für Geschäftskunden.
///
/// Regeln:
/// - ab 100 € Bestellwert: 3 %
/// - ab 500 € Bestellwert: 5 %
/// - ab 1.000 € Bestellwert: 8 %
/// - Gold-Kunden erhalten zusätzlich 2 Prozentpunkte
/// - Bestellungen am Wochenende (Samstag, Sonntag): kein Mengenrabatt, nur der Gold-Bonus
/// - Ergebnis kaufmännisch auf 2 Nachkommastellen gerundet
/// - Negative Beträge sind ungültig (ArgumentOutOfRangeException)
/// </summary>
public static class DiscountCalculator
{
    public static decimal CalculateDiscount(decimal amount, CustomerTier tier, DateTime orderDate)
    {
        if (amount < 0) throw new ArgumentOutOfRangeException(nameof(amount), "Betrag darf nicht negativ sein");

        decimal rate = 0;
        var weekend = orderDate.DayOfWeek == DayOfWeek.Saturday || orderDate.DayOfWeek == DayOfWeek.Sunday;

        if (!weekend)
        {
            if (amount >= 1000) rate = 0.08m;
            else if (amount >= 500) rate = 0.05m;
            else if (amount > 100) rate = 0.03m;
        }

        if (tier == CustomerTier.Gold) rate += 0.02m;

        return Math.Round(amount * rate, 2);
    }
}
