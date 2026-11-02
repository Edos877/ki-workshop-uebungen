using Bestellservice;

namespace Bestellservice.Tests;

public class FakeRepository : IOrderRepository
{
    public List<(Order Order, OrderResult Result)> Saved { get; } = new();
    public void Save(Order order, OrderResult result) => Saved.Add((order, result));
}

public class FakeEmailSender : IEmailSender
{
    public List<(string To, string Subject, string Body)> Sent { get; } = new();
    public void Send(string to, string subject, string body) => Sent.Add((to, subject, body));
}
