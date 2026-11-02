using Bestellservice;

namespace Bestellservice.Tests;

public class LoyaltyServiceTests
{
    private class FakeDb : IDbExecutor
    {
        public List<string> Statements { get; } = new();
        public void Execute(string sql) => Statements.Add(sql);
    }

    [Fact]
    public void Gold_bekommt_doppelte_Punkte()
    {
        var service = new LoyaltyService(new FakeDb());
        Assert.Equal(20, service.CalculatePoints(CustomerTier.Gold, 100m));
    }

    [Fact]
    public void Punkte_werden_berechnet()
    {
        var service = new LoyaltyService(new FakeDb());
        var points = service.CalculatePoints(CustomerTier.Standard, 95m);
        Assert.True(points >= 0);
    }

    [Fact]
    public void Punkte_werden_gespeichert()
    {
        var db = new FakeDb();
        var service = new LoyaltyService(db);
        var order = new Order { Customer = new Customer("K1", "Max Muster", "kunde@example.com", CustomerTier.Standard) };
        var result = new OrderResult(true, null, 100m, 0m, 0m, 19m, 119m);

        service.AwardPoints(order, result);

        Assert.Single(db.Statements);
    }
}
