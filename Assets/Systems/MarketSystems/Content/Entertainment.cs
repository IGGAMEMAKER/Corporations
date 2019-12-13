using Entitas;

public partial class MarketInitializerSystem : IInitializeSystem
{
    private void InitializeEntertainmentIndustry()
    {
        return;
        var niches = new NicheType[] {
            // gambling
            NicheType.Ent_Betting,
            NicheType.Ent_Casino,
            NicheType.Ent_Lottery,
            NicheType.Ent_Poker,

            // gaming
            NicheType.Ent_FreeToPlay,
            NicheType.Ent_MMOs,
            //NicheType.Ent_Publishing,

            // Video Streaming
            NicheType.Ent_StreamingService,
            NicheType.Ent_TVStreamingService,
        };
        AttachNichesToIndustry(IndustryType.Entertainment, niches);



        SetMarkets(NicheType.Ent_Lottery, 2000, GetGamblingCompanyProfile);
        SetMarkets(NicheType.Ent_Casino, 2001, GetGamblingCompanyProfile);
        SetMarkets(NicheType.Ent_Betting, 2000, GetGamblingCompanyProfile);
        SetMarkets(NicheType.Ent_Poker, 2001, GetGamblingCompanyProfile);

        SetMarkets(NicheType.Ent_FreeToPlay, 2001,
            AudienceSize.Million100, Monetisation.Adverts, Margin.Mid, NicheSpeed.Year, AppComplexity.Average);

        SetMarkets(NicheType.Ent_MMOs, 2000,
            AudienceSize.Million, Monetisation.Service, Margin.Mid, NicheSpeed.Year, AppComplexity.Average);


        SetMarkets(NicheType.Ent_StreamingService, 2011,
            AudienceSize.Million100, Monetisation.Service, Margin.Low, NicheSpeed.HalfYear, AppComplexity.Easy);
        SetMarkets(NicheType.Ent_TVStreamingService, 2016,
            AudienceSize.Million100, Monetisation.Service, Margin.Mid, NicheSpeed.HalfYear, AppComplexity.Average);
    }
}
