using Entitas;

public partial class MarketInitializerSystem : IInitializeSystem
{
    private void InitializeFinancesIndustry()
    {
        var niches = new NicheType[] {
            NicheType.ECom_Exchanging,
            NicheType.ECom_OnlineBanking,
            NicheType.ECom_PaymentSystem,

            //NicheType.ECom_Blockchain,
            //NicheType.ECom_TradingBot,
        };
        AttachNichesToIndustry(IndustryType.Finances, niches);

        var payment =
            new MarketProfile(AudienceSize.Million100, Monetisation.Service, Margin.Low, AppComplexity.Average, NicheSpeed.HalfYear);

        var banking = payment.Copy().Global().IncomeMid().Dynamic();

        SetMarkets(NicheType.ECom_OnlineBanking, 1992, 2030, banking);
        SetMarkets(NicheType.ECom_PaymentSystem, 1995, 2030, payment);

        SetMarkets(NicheType.ECom_Exchanging,    1998, 2030, payment);
    }

    private void InitializeTourismIndustry()
    {
        var niches = new NicheType[] {
            NicheType.ECom_BookingTransportTickets,
            NicheType.ECom_BookingHotels,
            NicheType.ECom_BookingTours,
            NicheType.ECom_BookingAppartments,
        };
        AttachNichesToIndustry(IndustryType.Tourism, niches);

        var booking =
            new MarketProfile(AudienceSize.Million100, Monetisation.Service, Margin.Low, AppComplexity.Average, NicheSpeed.Year);

        SetMarkets(NicheType.ECom_BookingTransportTickets,  1998, 2030, booking);
        SetMarkets(NicheType.ECom_BookingHotels,            1998, 2030, booking);
        SetMarkets(NicheType.ECom_BookingTours,             1998, 2030, booking);
        SetMarkets(NicheType.ECom_BookingAppartments,       2008, 2050, booking);
    }

    private void InitializeEcommerceIndustry()
    {
        var niches = new NicheType[] {
            NicheType.ECom_Marketplace,
            NicheType.ECom_OnlineTaxi,
        };
        AttachNichesToIndustry(IndustryType.Ecommerce, niches);

        var marketplace =
            new MarketProfile(AudienceSize.Million100, Monetisation.Service, Margin.High, AppComplexity.Hard, NicheSpeed.Year);

        var taxi =
            new MarketProfile(AudienceSize.Global, Monetisation.Service, Margin.Low, AppComplexity.Hard, NicheSpeed.Year);

        SetMarkets(NicheType.ECom_Marketplace,              1995, 2050, marketplace);
        SetMarkets(NicheType.ECom_OnlineTaxi,               1995, 2050, taxi);
    }
}
