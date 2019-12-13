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

        var financialSystemMarket = new MarketProfile
        {
            AudienceSize = AudienceSize.Million,
            MonetisationType = Monetisation.Service,
            Margin = Margin.Low,

            AppComplexity = AppComplexity.Average,

            NicheSpeed = NicheSpeed.HalfYear,
        };

        financialSystemMarket = new MarketProfile().NicheApp().Service().LowMargin().WebService().SetSpeed(NicheSpeed.HalfYear);

        var banking = new MarketProfile
        {
            AudienceSize = AudienceSize.Global,
            MonetisationType = Monetisation.Service,
            Margin = Margin.Low,

            AppComplexity = AppComplexity.Average,

            NicheSpeed = NicheSpeed.HalfYear,
        };

        SetMarkets(NicheType.Fin_Exchanging, 1998, financialSystemMarket);
        SetMarkets(NicheType.Fin_OnlineBanking, 1992, banking);
        SetMarkets(NicheType.Fin_PaymentSystem, 1995, financialSystemMarket);
    }
}
