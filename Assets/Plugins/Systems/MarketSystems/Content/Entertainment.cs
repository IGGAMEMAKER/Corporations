using Entitas;

public partial class MarketInitializerSystem : IInitializeSystem
{
    private void InitializeEntertainmentIndustry()
    {
        var niches = new NicheType[] {
            // gambling
            NicheType.Ent_Betting,
            NicheType.Ent_Casino,
            //NicheType.Ent_Lottery,
            NicheType.Ent_Poker,

            // gaming
            //NicheType.Ent_FreeToPlay,
            //NicheType.Ent_MMOs,
            //NicheType.Ent_Publishing,

            // Video Streaming
            NicheType.Ent_VideoHosting,
            NicheType.Ent_StreamingService,
            NicheType.Ent_TVStreamingService,
        };
        AttachNichesToIndustry(IndustryType.Entertainment, niches);

        var videoHosting = new MarketProfile(AudienceSize.Global, Monetisation.Adverts, Margin.Low, AppComplexity.Hard, NicheSpeed.ThreeYears);

        var streaming = new MarketProfile(AudienceSize.Million100, Monetisation.Service, Margin.Low, AppComplexity.Average, NicheSpeed.Year);
        var tvStreaming = new MarketProfile(AudienceSize.Million100, Monetisation.Service, Margin.Mid, AppComplexity.Average, NicheSpeed.Year);

        //SetMarkets(NicheType.Ent_Lottery, 2000, 2018, GetGamblingCompanyProfile);
        SetMarkets(NicheType.Ent_Casino, 2001, 2020, GetGamblingCompanyProfile);
        SetMarkets(NicheType.Ent_Betting, 2000, 2020, GetGamblingCompanyProfile);
        SetMarkets(NicheType.Ent_Poker, 2001, 2025, GetGamblingCompanyProfile);

        //SetMarkets(NicheType.Ent_FreeToPlay, 2001, 2100,
        //    AudienceSize.Million100, Monetisation.Adverts, Margin.Mid, NicheSpeed.Year, AppComplexity.Average);

        //SetMarkets(NicheType.Ent_MMOs, 2000, 2030,
        //    AudienceSize.Million, Monetisation.Service, Margin.Mid, NicheSpeed.Year, AppComplexity.Average);


        SetMarkets(NicheType.Ent_VideoHosting,      2004, 2030, videoHosting);

        SetMarkets(NicheType.Ent_StreamingService,  2011, 2030, streaming);
        SetMarkets(NicheType.Ent_TVStreamingService, 2013, 2030, tvStreaming);
    }
}
