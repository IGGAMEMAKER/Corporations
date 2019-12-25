using Entitas;

public partial class MarketInitializerSystem : IInitializeSystem
{
    private void InitializeEcommerceIndustry()
    {
        var niches = new NicheType[] {
            NicheType.ECom_Exchanging,
            NicheType.ECom_OnlineBanking,
            NicheType.ECom_PaymentSystem,

            //NicheType.ECom_Blockchain,
            //NicheType.ECom_TradingBot,

            NicheType.ECom_OnlineTaxi,
            NicheType.ECom_BookingTransportTickets,
            NicheType.ECom_BookingHotels,
            NicheType.ECom_Tourism,
            NicheType.ECom_EventTickets,

            NicheType.ECom_Marketplace,
        };
        AttachNichesToIndustry(IndustryType.Ecommerce, niches);

        var payment =
            new MarketProfile(AudienceSize.Million100, Monetisation.Service, Margin.Low, AppComplexity.Average, NicheSpeed.HalfYear);

        var banking = payment.Copy().Global().IncomeMid().Dynamic();

        var taxi =
            new MarketProfile(AudienceSize.Global, Monetisation.Service, Margin.Low, AppComplexity.Average, NicheSpeed.Year);

        var booking =
            new MarketProfile(AudienceSize.Million100, Monetisation.Service, Margin.Low, AppComplexity.Average, NicheSpeed.Year);

        var marketplace =
            new MarketProfile(AudienceSize.Million100, Monetisation.Service, Margin.High, AppComplexity.Hard, NicheSpeed.Year);

        SetMarkets(NicheType.ECom_OnlineBanking,    1992, 2030, banking);
        SetMarkets(NicheType.ECom_PaymentSystem,    1995, 2030, payment);
        SetMarkets(NicheType.ECom_Marketplace,      1995, 2050, marketplace);

        SetMarkets(NicheType.ECom_Exchanging,       1998, 2030, payment);
        SetMarkets(NicheType.ECom_OnlineTaxi,       1998, 2030, taxi);
        SetMarkets(NicheType.ECom_BookingTransportTickets,   1998, 2030, booking);
        SetMarkets(NicheType.ECom_BookingHotels,          1998, 2030, booking);
        SetMarkets(NicheType.ECom_Tourism,          1998, 2030, booking);
        SetMarkets(NicheType.ECom_BookingAppartments,2008, 2050, booking);

        SetMarkets(NicheType.ECom_EventTickets,     2000, 2030, booking);
    }
}
