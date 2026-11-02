using Bestellservice;

namespace Bestellservice.Tests;

/// <summary>
/// Diese Tests halten das heutige Verhalten fest.
/// Beim Refactoring müssen sie unverändert grün bleiben.
/// </summary>
public class OrderServiceTests
{
    private readonly FakeRepository _repo = new();
    private readonly FakeEmailSender _mail = new();
    private OrderService Service => new(_repo, _mail);

    private static Customer Kunde(CustomerTier tier = CustomerTier.Standard, string email = "kunde@example.com")
        => new("K1", "Max Muster", email, tier);

    private static Order Bestellung(decimal preis, int menge = 1, CustomerTier tier = CustomerTier.Standard,
        string land = "DE", string? gutschein = null)
        => new()
        {
            Id = "B100",
            Customer = Kunde(tier),
            Items = { new OrderItem("A1", "Artikel", preis, menge) },
            Country = land,
            CouponCode = gutschein,
        };

    [Fact]
    public void Ohne_Kunde_gibt_es_einen_Fehler()
    {
        var r = Service.ProcessOrder(new Order { Items = { new OrderItem("A1", "Artikel", 10, 1) } });
        Assert.False(r.Success);
        Assert.Equal("Kunde fehlt", r.Error);
    }

    [Fact]
    public void Ohne_Positionen_gibt_es_einen_Fehler()
    {
        var r = Service.ProcessOrder(new Order { Customer = Kunde() });
        Assert.False(r.Success);
    }

    [Fact]
    public void Menge_null_ist_ungueltig()
    {
        var r = Service.ProcessOrder(Bestellung(10, menge: 0));
        Assert.False(r.Success);
        Assert.Equal("Ungültige Menge bei A1", r.Error);
    }

    [Fact]
    public void Negativer_Preis_ist_ungueltig()
    {
        var r = Service.ProcessOrder(Bestellung(-1));
        Assert.False(r.Success);
    }

    [Fact]
    public void Nicht_unterstuetztes_Land_wird_abgelehnt()
    {
        var r = Service.ProcessOrder(Bestellung(10, land: "FR"));
        Assert.False(r.Success);
        Assert.Equal("Lieferland nicht unterstützt", r.Error);
    }

    [Fact]
    public void Standard_DE_unter_50_mit_Versand()
    {
        var r = Service.ProcessOrder(Bestellung(20, menge: 2));
        Assert.Equal(40m, r.Subtotal);
        Assert.Equal(4.95m, r.Shipping);
        Assert.Equal(8.54m, r.Tax);
        Assert.Equal(53.49m, r.Total);
    }

    [Fact]
    public void Standard_DE_ab_50_versandkostenfrei()
    {
        var r = Service.ProcessOrder(Bestellung(30, menge: 2));
        Assert.Equal(0m, r.Shipping);
        Assert.Equal(71.40m, r.Total);
    }

    [Fact]
    public void Gold_bekommt_10_Prozent()
    {
        var r = Service.ProcessOrder(Bestellung(100, tier: CustomerTier.Gold));
        Assert.Equal(10m, r.Discount);
        Assert.Equal(107.10m, r.Total);
    }

    [Fact]
    public void Silver_bekommt_5_Prozent()
    {
        var r = Service.ProcessOrder(Bestellung(100, tier: CustomerTier.Silver));
        Assert.Equal(5m, r.Discount);
        Assert.Equal(113.05m, r.Total);
    }

    [Fact]
    public void Willkommen10_ab_50_Euro()
    {
        var r = Service.ProcessOrder(Bestellung(60, gutschein: "WILLKOMMEN10"));
        Assert.Equal(10m, r.Discount);
        Assert.Equal(0m, r.Shipping);
        Assert.Equal(59.50m, r.Total);
    }

    [Fact]
    public void Willkommen10_unter_50_Euro_ohne_Nachlass()
    {
        var r = Service.ProcessOrder(Bestellung(40, gutschein: "WILLKOMMEN10"));
        Assert.Equal(0m, r.Discount);
        Assert.Equal(53.49m, r.Total);
    }

    [Fact]
    public void Sommer15_schlaegt_Gold_Rabatt()
    {
        var r = Service.ProcessOrder(Bestellung(100, tier: CustomerTier.Gold, gutschein: " sommer15 "));
        Assert.Equal(15m, r.Discount);
        Assert.Equal(101.15m, r.Total);
    }

    [Fact]
    public void Unbekannter_Gutschein_wird_ignoriert()
    {
        var r = Service.ProcessOrder(Bestellung(100, tier: CustomerTier.Gold, gutschein: "GIBTSNICHT"));
        Assert.Equal(10m, r.Discount);
    }

    [Fact]
    public void Oesterreich_Versand_und_20_Prozent_Steuer()
    {
        var r = Service.ProcessOrder(Bestellung(80, land: "AT"));
        Assert.Equal(9.90m, r.Shipping);
        Assert.Equal(17.98m, r.Tax);
        Assert.Equal(107.88m, r.Total);
    }

    [Fact]
    public void Schweiz_ohne_Steuer_und_ab_100_versandkostenfrei()
    {
        var r = Service.ProcessOrder(Bestellung(120, land: "CH"));
        Assert.Equal(0m, r.Shipping);
        Assert.Equal(0m, r.Tax);
        Assert.Equal(120m, r.Total);
    }

    [Fact]
    public void Rabatt_wird_kaufmaennisch_gerundet()
    {
        var r = Service.ProcessOrder(Bestellung(10.10m, tier: CustomerTier.Silver));
        Assert.Equal(0.51m, r.Discount);
        Assert.Equal(17.30m, r.Total);
    }

    [Fact]
    public void Bestellung_wird_gespeichert_und_bestaetigt()
    {
        Service.ProcessOrder(Bestellung(30, menge: 2));
        Assert.Single(_repo.Saved);
        var mail = Assert.Single(_mail.Sent);
        Assert.Equal("Ihre Bestellung B100", mail.Subject);
        Assert.Contains("71,40 €", mail.Body);
    }

    [Fact]
    public void Ohne_E_Mail_wird_keine_Bestaetigung_verschickt()
    {
        var order = new Order { Customer = Kunde(email: ""), Items = { new OrderItem("A1", "Artikel", 10, 1) } };
        Service.ProcessOrder(order);
        Assert.Empty(_mail.Sent);
    }
}
