using Entitas;

public partial class MarketInitializerSystem : IInitializeSystem
{
    private void InitializeEcommerceIndustry()
    {
        var niches = new NicheType[] {
            NicheType.Fin_Exchanging,
            NicheType.Fin_OnlineBanking,
            NicheType.Fin_PaymentSystem,
        };
        AttachNichesToIndustry(IndustryType.Ecommerce, niches);

        var payment = new MarketProfile(AudienceSize.Million, Monetisation.Service, Margin.Low, AppComplexity.Average, NicheSpeed.HalfYear);
        var banking = payment.Global().IncomeMid().Dynamic();

        SetMarkets(NicheType.Fin_OnlineBanking, 1992, 2030, banking);
        SetMarkets(NicheType.Fin_Exchanging, 1998, 2030, payment);
        SetMarkets(NicheType.Fin_PaymentSystem, 1995, 2030, payment);
    }
}
