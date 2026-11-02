using System.Globalization;

namespace Bestellservice;

/// <summary>
/// Verarbeitet Bestellungen des Onlineshops.
///
/// Geschäftsregeln (alle Preise netto):
/// - Kundenrabatt: Gold 10 %, Silver 5 %, Standard 0 %
/// - Gutschein WILLKOMMEN10: 10 € Nachlass ab 50 € Warenwert, zusätzlich zum Kundenrabatt
/// - Gutschein SOMMER15: 15 %, nicht mit dem Kundenrabatt kombinierbar (der höhere gilt)
/// - Unbekannte Gutscheine werden ignoriert
/// - Versand DE: 4,95 €, kostenlos ab 50 € (nach Rabatt)
/// - Versand AT/CH: 9,90 €, kostenlos ab 100 € (nach Rabatt)
/// - Umsatzsteuer: DE 19 %, AT 20 %, CH 0 % (Ausfuhr) auf Ware nach Rabatt plus Versand
/// - Rundung: kaufmännisch auf 2 Nachkommastellen
/// </summary>
public class OrderService
{
    private readonly IOrderRepository _repository;
    private readonly IEmailSender _email;

    public OrderService(IOrderRepository repository, IEmailSender email)
    {
        _repository = repository;
        _email = email;
    }

    public OrderResult ProcessOrder(Order order)
    {
        if (order == null) throw new ArgumentNullException(nameof(order));
        if (order.Customer == null) return new OrderResult(false, "Kunde fehlt", 0, 0, 0, 0, 0);
        if (order.Items == null || order.Items.Count == 0) return new OrderResult(false, "Bestellung enthält keine Positionen", 0, 0, 0, 0, 0);
        foreach (var i in order.Items)
        {
            if (i.Quantity <= 0) return new OrderResult(false, "Ungültige Menge bei " + i.Sku, 0, 0, 0, 0, 0);
            if (i.UnitPrice < 0) return new OrderResult(false, "Ungültiger Preis bei " + i.Sku, 0, 0, 0, 0, 0);
        }
        if (order.Country != "DE" && order.Country != "AT" && order.Country != "CH") return new OrderResult(false, "Lieferland nicht unterstützt", 0, 0, 0, 0, 0);

        decimal sub = 0;
        for (int x = 0; x < order.Items.Count; x++)
        {
            sub = sub + order.Items[x].UnitPrice * order.Items[x].Quantity;
        }

        decimal disc = 0;
        if (!string.IsNullOrEmpty(order.CouponCode))
        {
            var code = order.CouponCode.Trim().ToUpper();
            if (code == "WILLKOMMEN10")
            {
                if (sub >= 50) disc = 10;
                // Kundenrabatt kommt zusätzlich dazu
                if (order.Customer.Tier == CustomerTier.Gold) disc = disc + sub * 0.10m;
                else if (order.Customer.Tier == CustomerTier.Silver) disc = disc + sub * 0.05m;
            }
            else if (code == "SOMMER15")
            {
                decimal t = 0;
                if (order.Customer.Tier == CustomerTier.Gold) t = sub * 0.10m;
                else if (order.Customer.Tier == CustomerTier.Silver) t = sub * 0.05m;
                var c = sub * 0.15m;
                if (c > t) disc = c; else disc = t; // nicht kombinierbar, der höhere Rabatt gilt
            }
            else
            {
                // unbekannter Gutschein wird ignoriert
                if (order.Customer.Tier == CustomerTier.Gold) disc = sub * 0.10m;
                else if (order.Customer.Tier == CustomerTier.Silver) disc = sub * 0.05m;
            }
        }
        else
        {
            if (order.Customer.Tier == CustomerTier.Gold) disc = sub * 0.10m;
            else if (order.Customer.Tier == CustomerTier.Silver) disc = sub * 0.05m;
        }
        disc = Math.Round(disc, 2, MidpointRounding.AwayFromZero);

        var afterDisc = sub - disc;
        decimal ship;
        if (order.Country == "DE")
        {
            if (afterDisc >= 50) ship = 0; else ship = 4.95m;
        }
        else
        {
            if (afterDisc >= 100) ship = 0; else ship = 9.90m;
        }

        decimal taxRate = 0;
        if (order.Country == "DE") taxRate = 0.19m;
        if (order.Country == "AT") taxRate = 0.20m;
        // CH: Ausfuhrlieferung, keine Umsatzsteuer
        var tax = Math.Round((afterDisc + ship) * taxRate, 2, MidpointRounding.AwayFromZero);
        var total = afterDisc + ship + tax;

        var result = new OrderResult(true, null, sub, disc, ship, tax, total);
        _repository.Save(order, result);

        if (!string.IsNullOrWhiteSpace(order.Customer.Email))
        {
            var body = "Hallo " + order.Customer.Name + ",\n\nvielen Dank für Ihre Bestellung.\nGesamtbetrag: "
                + total.ToString("0.00", CultureInfo.GetCultureInfo("de-DE")) + " €\n\nIhr Shop-Team";
            _email.Send(order.Customer.Email, "Ihre Bestellung " + order.Id, body);
        }

        return result;
    }
}
