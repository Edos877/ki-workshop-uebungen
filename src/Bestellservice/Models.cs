namespace Bestellservice;

public enum CustomerTier
{
    Standard,
    Silver,
    Gold,
}

public record Customer(string Id, string Name, string Email, CustomerTier Tier);

public record OrderItem(string Sku, string Name, decimal UnitPrice, int Quantity);

public class Order
{
    public string Id { get; init; } = Guid.NewGuid().ToString("N")[..8];
    public Customer? Customer { get; init; }
    public List<OrderItem> Items { get; init; } = new();
    public string? CouponCode { get; init; }
    public string Country { get; init; } = "DE";
    public DateTime OrderDate { get; init; } = DateTime.Today;
}

public record OrderResult(
    bool Success,
    string? Error,
    decimal Subtotal,
    decimal Discount,
    decimal Shipping,
    decimal Tax,
    decimal Total);

public interface IOrderRepository
{
    void Save(Order order, OrderResult result);
}

public interface IEmailSender
{
    void Send(string to, string subject, string body);
}
