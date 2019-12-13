using Entitas;

public partial class MarketInitializerSystem : IInitializeSystem
{
    private void InitializeEcommerceIndustry()
    {
        return;
        var niches = new NicheType[] {
            NicheType.ECom_Exchanging,
            NicheType.ECom_OnlineBanking,
            NicheType.ECom_PaymentSystem,
        };
        AttachNichesToIndustry(IndustryType.Ecommerce, niches);

        var payment = new MarketProfile(AudienceSize.Million, Monetisation.Service, Margin.Low, AppComplexity.Average, NicheSpeed.HalfYear);
        var banking = payment.Copy().Global().IncomeMid().Dynamic();

        SetMarkets(NicheType.ECom_OnlineBanking, 1992, 2030, banking);
        SetMarkets(NicheType.ECom_Exchanging, 1998, 2030, payment);
        SetMarkets(NicheType.ECom_PaymentSystem, 1995, 2030, payment);
    }
}
