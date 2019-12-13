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

        var cloud =
            new MarketProfile(AudienceSize.SmallEnterprise, Monetisation.Enterprise, Margin.High, AppComplexity.Hard, NicheSpeed.Year);
        var searchEngine =
            new MarketProfile(AudienceSize.Global, Monetisation.Adverts, Margin.High, AppComplexity.Humongous, NicheSpeed.ThreeYears);
        var desktop =
            new MarketProfile(AudienceSize.Global, Monetisation.Paid, Margin.High, AppComplexity.Humongous, NicheSpeed.ThreeYears);
        var browser =
            new MarketProfile(AudienceSize.Global, Monetisation.Adverts, Margin.High, AppComplexity.Hard, NicheSpeed.Year);

        SetMarkets(NicheType.Tech_CloudComputing, 2000, 2050, cloud);
        SetMarkets(NicheType.Tech_SearchEngine, 1995, 2040, searchEngine);
        SetMarkets(NicheType.Tech_OSDesktop, 1980, 2040, desktop);
        SetMarkets(NicheType.Tech_Browser, 1990, 2030, browser);
    }
}
