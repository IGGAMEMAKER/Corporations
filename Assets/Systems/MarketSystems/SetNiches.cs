using Entitas;

public partial class MarketInitializerSystem : IInitializeSystem
{
    int GetYear(int year) => (year - Constants.START_YEAR) * 360;

    int GetYearAndADate(int year, int quarter) => GetYear(year) + quarter * 90;


    void InitializeCommunicationsIndustry()
    {
        var niches = new NicheType[] {
            NicheType.Com_Messenger, // chatting***, info
            NicheType.Com_SocialNetwork, // users, chatting, info
            NicheType.Com_Blogs, // info, users, chatting
            NicheType.Com_Forums, // info, chatting
            NicheType.Com_Email, // chatting

            NicheType.Com_Dating,
        };
        AttachNichesToIndustry(IndustryType.Communications, niches);

        SetNichesAutomatically(NicheType.Com_Email,  1990, GetPopularUsefulAppProfile);
        SetNichesAutomatically(NicheType.Com_Forums, 1990, GetPopularRarelyUsedAppProfile);
        SetNichesAutomatically(NicheType.Com_Blogs,  1995, GetPopularRarelyUsedAppProfile);
        SetNichesAutomatically(NicheType.Com_Dating, 2000, GetPopularUsefulAppProfile);


        SetNichesAutomatically(NicheType.Com_SocialNetwork, 1999,
            new MarketProfile
            {
                AudienceSize = AudienceSize.Global,
                MonetisationType = Monetisation.Adverts,
                Margin = Margin.Mid,

                AppComplexity = AppComplexity.Easy,

                Iteration = NicheSpeed.Year,
            }
            //new ProductPositioning[] {
            //    //new ProductPositioning { name = "Basic social network", marketShare = 100 }, // fb
            //    //new ProductPositioning { name = "Corporative social network", marketShare = 3, priceModifier = 10 }, // linkedIn
            //    //new ProductPositioning { name = "Text focused social network", marketShare = 15, priceModifier = 1.75f }, // twitter
            //    //new ProductPositioning { name = "Image focused social network", marketShare = 85 }, // insta
            //},
            );

        SetNichesAutomatically(NicheType.Com_Messenger, 2000,
            new MarketProfile
            {
                AudienceSize = AudienceSize.Global,
                MonetisationType = Monetisation.Adverts,
                Margin = Margin.Low,

                AppComplexity = AppComplexity.Easy,

                Iteration = NicheSpeed.Quarter,
            });
    }

    void InitializeECommerceIndustry()
    {

    }

    private void InitializeEntertainmentIndustry()
    {
        var niches = new NicheType[] {
            // gambling
            NicheType.Ent_Betting,
            NicheType.Ent_Casino,
            NicheType.Ent_Lottery,
            NicheType.Ent_Poker,

            // gaming
            NicheType.Ent_FreeToPlay,
            NicheType.Ent_MMOs,
            NicheType.Ent_Publishing,

            // Video Streaming
            NicheType.Ent_StreamingService,
            NicheType.Ent_TVStreamingService,
        };
        AttachNichesToIndustry(IndustryType.Entertainment, niches);

        

        SetNichesAutomatically(NicheType.Ent_Lottery,           2000, GetGamblingCompanyProfile);
        SetNichesAutomatically(NicheType.Ent_Casino,            2001, GetGamblingCompanyProfile);
        SetNichesAutomatically(NicheType.Ent_Betting,           2000, GetGamblingCompanyProfile);
        SetNichesAutomatically(NicheType.Ent_Poker,             2001, GetGamblingCompanyProfile);

        SetNichesAutomatically(NicheType.Ent_FreeToPlay,        2001,
            AudienceSize.Million100, Monetisation.Adverts, Margin.Mid, NicheSpeed.Year, AppComplexity.Mid);

        SetNichesAutomatically(NicheType.Ent_MMOs,              2000,
            AudienceSize.Million,    Monetisation.Service, Margin.Mid, NicheSpeed.Year, AppComplexity.Mid);

        SetNichesAutomatically(NicheType.Ent_StreamingService,  2011,
            AudienceSize.Million100, Monetisation.Service, Margin.Low, NicheSpeed.HalfYear, AppComplexity.Easy);
    }

    private void InitializeFundamentalIndustry()
    {
        var niches = new NicheType[] {
            NicheType.SearchEngine,
            NicheType.OSDesktop,
            NicheType.CloudComputing,
            NicheType.Browser,
        };
        AttachNichesToIndustry(IndustryType.Fundamental, niches);


        var cloudProfile = new MarketProfile
        {
            AudienceSize = AudienceSize.SmallEnterprise,
            MonetisationType = Monetisation.Enterprise,
            Margin = Margin.High,

            AppComplexity = AppComplexity.Hard,

            Iteration = NicheSpeed.Year
        };
        var searchEngineProfile = new MarketProfile
        {
            AudienceSize = AudienceSize.Global,
            MonetisationType = Monetisation.Adverts,
            Margin = Margin.High,

            AppComplexity = AppComplexity.Humongous,

            Iteration = NicheSpeed.ThreeYears
        };
        var desktopProfile = new MarketProfile
        {
            AudienceSize = AudienceSize.Global,
            MonetisationType = Monetisation.Paid,
            Margin = Margin.High,

            AppComplexity = AppComplexity.Humongous,

            Iteration = NicheSpeed.ThreeYears
        };
        var browserProfile = new MarketProfile
        {
            AudienceSize = AudienceSize.Global,
            MonetisationType = Monetisation.Adverts,
            Margin = Margin.High,

            AppComplexity = AppComplexity.Hard,

            Iteration = NicheSpeed.Year
        };

        SetNichesAutomatically(NicheType.CloudComputing, 2000, cloudProfile);
        SetNichesAutomatically(NicheType.SearchEngine,   1995, searchEngineProfile);
        SetNichesAutomatically(NicheType.OSDesktop,      1980, desktopProfile);
        SetNichesAutomatically(NicheType.Browser,        1990, browserProfile);
    }
}

// Market Profiles
public partial class MarketInitializerSystem : IInitializeSystem
{
    MarketProfile GetPopularRarelyUsedAppProfile => new MarketProfile
    {
        AudienceSize = AudienceSize.Million100,
        MonetisationType = Monetisation.Adverts,
        Margin = Margin.Low,

        AppComplexity = AppComplexity.Easy,

        Iteration = NicheSpeed.ThreeYears,
    };

    MarketProfile GetPopularUsefulAppProfile => new MarketProfile
    {
        AudienceSize = AudienceSize.Million100,
        MonetisationType = Monetisation.Adverts,
        Margin = Margin.Low,

        AppComplexity = AppComplexity.Easy,

        Iteration = NicheSpeed.Year,
    };

    MarketProfile GetGamblingCompanyProfile => new MarketProfile
    {
        AudienceSize = AudienceSize.Million,
        MonetisationType = Monetisation.IrregularPaid,
        Margin = Margin.Low,

        AppComplexity = AppComplexity.Easy,

        Iteration = NicheSpeed.ThreeYears,
    };
}