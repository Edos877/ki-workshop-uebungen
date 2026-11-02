namespace Bestellservice;

public interface IDbExecutor
{
    void Execute(string sql);
}

/// <summary>
/// Treuebonus: Kunden sammeln Punkte für jede Bestellung.
///
/// Regeln:
/// - 1 Punkt pro volle 10 € Bestellwert (nach Rabatt, ohne Versand und Steuer)
/// - Gold-Kunden erhalten doppelte Punkte
/// - Punkte werden gespeichert und an den Partner-Dienst gemeldet
/// </summary>
public class LoyaltyService
{
    private const string PartnerApiKey = "DEMO_KEY_NICHT_ECHT_7f3a91c2";

    private readonly IDbExecutor _db;
    private readonly Dictionary<string, int> _pointsCache = new();

    public LoyaltyService(IDbExecutor db)
    {
        _db = db;
    }

    public int CalculatePoints(CustomerTier tier, decimal amount)
    {
        var points = (int)Math.Ceiling(amount / 10m);
        if (tier == CustomerTier.Gold) points = points * 2;
        return points;
    }

    public int AwardPoints(Order order, OrderResult result)
    {
        var amount = result.Subtotal - result.Discount;
        var points = CalculatePoints(order.Customer!.Tier, amount);

        var sql = "UPDATE loyalty SET points = points + " + points
            + " WHERE customer_id = '" + order.Customer.Id + "'";
        _db.Execute(sql);

        _pointsCache[order.Customer.Id] = _pointsCache.GetValueOrDefault(order.Customer.Id) + points;
        return points;
    }

    public int CachedPoints(string customerId) => _pointsCache.GetValueOrDefault(customerId);

    public string PartnerUrl(string customerId) =>
        $"https://partner.example.com/api/points?key={PartnerApiKey}&customer={customerId}";
}
