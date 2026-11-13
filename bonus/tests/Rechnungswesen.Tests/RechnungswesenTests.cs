using Rechnungswesen;

namespace Rechnungswesen.Tests;

public class RechnungswesenTests
{
    private static readonly DateTime Montag = new(2025, 5, 12, 9, 5, 0);

    [Theory]
    [InlineData("12,50", true)]
    [InlineData("1.234,56", true)]
    [InlineData("1234,5", false)]
    [InlineData("1,234.56", false)]
    public void Betrag_wird_geprueft(string text, bool gueltig) => Assert.Equal(gueltig, Betraege.IstGueltig(text));

    [Fact]
    public void Betrag_wird_gelesen_und_geschrieben()
    {
        Assert.Equal(1234.56m, Betraege.Parse("1.234,56"));
        Assert.Equal("1.234,50", Betraege.Format(1234.5m));
        Assert.Throws<FormatException>(() => Betraege.Parse("zwölf"));
    }

    [Fact]
    public void Summe_einer_Liste()
    {
        Assert.Equal(42.50m, Betraege.Summe(new[] { 42.50m }));
        Assert.Equal(0m, Betraege.Summe(Array.Empty<decimal>()));
    }

    [Fact]
    public void Steuersatz_und_Brutto()
    {
        Assert.Equal(19m, Steuer.Satz("DE"));
        Assert.Equal(19m, Steuer.Satz(" de "));
        Assert.Equal(8.1m, Steuer.Satz("CH"));
        Assert.Equal(119.00m, Steuer.Brutto(100m, "DE"));
        Assert.Throws<ArgumentException>(() => Steuer.Satz("XX"));
    }

    [Fact]
    public void Fremdwaehrung_wird_erkannt()
    {
        Assert.True(Steuer.IstFremdwaehrung("CHF"));
        Assert.True(Steuer.IstFremdwaehrung("usd"));
    }

    [Fact]
    public void Saeumniszuschlag_hat_einen_Mindestbetrag() => Assert.Equal(2.50m, Steuer.Saeumniszuschlag(40m));

    [Fact]
    public void Faelligkeit_mit_Wochenende()
    {
        Assert.Equal(new DateTime(2025, 6, 11), Fristen.Faelligkeit(Montag, 30));
        Assert.Equal(new DateTime(2025, 5, 26), Fristen.Faelligkeit(new DateTime(2025, 5, 11), 14));
    }

    [Fact]
    public void Skonto_bei_frueher_Zahlung()
    {
        Assert.Equal(20.00m, Fristen.Skonto(1000m, 10));
        Assert.Equal(0m, Fristen.Skonto(1000m, 20));
    }

    [Fact]
    public void Verzugszinsen_fuer_volle_Jahre()
    {
        Assert.Equal(90.00m, Fristen.Verzugszinsen(1000m, 9m, 365));
        Assert.Equal(180.00m, Fristen.Verzugszinsen(1000m, 9m, 730));
    }

    [Fact]
    public void Erinnerungen_je_Abstand() => Assert.Equal(3, Fristen.Erinnerungen(new[] { 7, 14, 21 }).Count);

    [Fact]
    public void Nummer_Dateiname_und_Zeitstempel()
    {
        Assert.Equal("RE-2025-0100", Dokumente.NaechsteNummer("RE-2025-0099"));
        Assert.Equal("RE-2025-0100_2025-05-12.pdf", Dokumente.Dateiname("RE-2025-0100", Montag));
        Assert.Equal("12.05.2025 09:05", Dokumente.Zeitstempel(Montag));
    }

    [Fact]
    public void Rechnung_rechnet_Netto_Brutto_und_Faelligkeit()
    {
        var rechnung = new Rechnung
        {
            Nummer = "RE-2025-0100", Datum = Montag, Land = "DE",
            Positionen = { new Position("Beratung", 150m, 4) },
        };
        Assert.Equal(600.00m, rechnung.Netto);
        Assert.Equal(714.00m, rechnung.Brutto);
        Assert.Equal(new DateTime(2025, 6, 11), rechnung.Faellig);
        Assert.Equal(699.72m, rechnung.ZahlbetragBei(7));
    }
}
