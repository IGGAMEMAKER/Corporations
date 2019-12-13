using Entitas;

public partial class MarketInitializerSystem : IInitializeSystem
{
    private void InitializeFundamentalIndustry()
    {
        var niches = new NicheType[] {
            NicheType.Tech_SearchEngine,
            NicheType.Tech_OSDesktop,
            NicheType.Tech_CloudComputing,
            NicheType.Tech_Browser,
        };
        AttachNichesToIndustry(IndustryType.Technology, niches);

        var cloud = new MarketProfile
        {
            AudienceSize = AudienceSize.SmallEnterprise,
            MonetisationType = Monetisation.Enterprise,
            Margin = Margin.High,

            AppComplexity = AppComplexity.Hard,

            NicheSpeed = NicheSpeed.Year
        };
        var searchEngine = new MarketProfile
        {
            AudienceSize = AudienceSize.Global,
            MonetisationType = Monetisation.Adverts,
            Margin = Margin.High,

            AppComplexity = AppComplexity.Humongous,

            NicheSpeed = NicheSpeed.ThreeYears
        };
        var desktop = new MarketProfile
        {
            AudienceSize = AudienceSize.Global,
            MonetisationType = Monetisation.Paid,
            Margin = Margin.High,

            AppComplexity = AppComplexity.Humongous,

            NicheSpeed = NicheSpeed.ThreeYears
        };
        var browser = new MarketProfile
        {
            AudienceSize = AudienceSize.Global,
            MonetisationType = Monetisation.Adverts,
            Margin = Margin.High,

            AppComplexity = AppComplexity.Hard,

            NicheSpeed = NicheSpeed.Year
        };

        SetMarkets(NicheType.Tech_CloudComputing, 2000, cloud);
        SetMarkets(NicheType.Tech_SearchEngine, 1995, searchEngine);
        SetMarkets(NicheType.Tech_OSDesktop, 1980, desktop);
        SetMarkets(NicheType.Tech_Browser, 1990, browser);
    }
}
