using Entitas;

public partial class MarketInitializerSystem : IInitializeSystem
{
    int GetYear(int year) => (year - Constants.START_YEAR) * 360;

    int GetYearAndADate(int year, int quarter) => GetYear(year) + quarter * 90;


    void SetGlobalFreeApp(NicheType nicheType, int year)
    {
        SetNichesAutomatically(nicheType,
            new MarketSettings
            {
                AudienceSize = AudienceSize.Global,
                MonetisationType = Monetisation.Ads,
                Margin = Margin.High,

                ProductComplexity = ProductComplexity.High,

                Iteration = NicheSpeed.Year
            },
        GetYear(year));
    }



    void InitializeCommunicationsIndustry()
    {
        var niches = new NicheType[] {
            NicheType.Com_Messenger,
            NicheType.Com_SocialNetwork,
            NicheType.Com_Blogs,
            NicheType.Com_Forums,
            NicheType.Com_Email,
            NicheType.Com_Dating
        };
        AttachNichesToIndustry(IndustryType.Communications, niches);

        // Group by audience size
        // 

        MarketSettings popularUsefulApp = new MarketSettings
        {
            AudienceSize = AudienceSize.HundredMillion,
            MonetisationType = Monetisation.Ads,
            Margin = Margin.Low,

            ProductComplexity = ProductComplexity.Low,

            Iteration = NicheSpeed.Year,
        };

        MarketSettings rarelyUsedApp = new MarketSettings
        {
            AudienceSize = AudienceSize.HundredMillion,
            MonetisationType = Monetisation.Ads,
            Margin = Margin.Low,

            ProductComplexity = ProductComplexity.Low,

            Iteration = NicheSpeed.ThreeYears,
        };



        SetNichesAutomatically(NicheType.Com_Email,     popularUsefulApp,   GetYear(1990));
        SetNichesAutomatically(NicheType.Com_Forums,    rarelyUsedApp,      GetYear(1990));
        SetNichesAutomatically(NicheType.Com_Blogs,     rarelyUsedApp,      GetYear(1995));
        SetNichesAutomatically(NicheType.Com_Dating,    popularUsefulApp,   GetYear(2000));


        SetNichesAutomatically(NicheType.Com_SocialNetwork,
            new MarketSettings
            {
                AudienceSize = AudienceSize.Global,
                MonetisationType = Monetisation.Ads,
                Margin = Margin.Mid,

                ProductComplexity = ProductComplexity.Low,

                Iteration = NicheSpeed.Year,
            },
            //new ProductPositioning[] {
            //    //new ProductPositioning { name = "Basic social network", marketShare = 100 }, // fb
            //    //new ProductPositioning { name = "Corporative social network", marketShare = 3, priceModifier = 10 }, // linkedIn
            //    //new ProductPositioning { name = "Text focused social network", marketShare = 15, priceModifier = 1.75f }, // twitter
            //    //new ProductPositioning { name = "Image focused social network", marketShare = 85 }, // insta
            //},
            GetYear(1999)
            );

        SetNichesAutomatically(NicheType.Com_Messenger,
            new MarketSettings
            {
                AudienceSize = AudienceSize.Global,
                MonetisationType = Monetisation.Ads,
                Margin = Margin.Low,

                ProductComplexity = ProductComplexity.Low,

                Iteration = NicheSpeed.Quarter,
            },
            GetYear(2000)
            );
    }


    private void InitializeEntertainmentIndustry()
    {
        var niches = new NicheType[] {
            NicheType.Ent_Betting,
            NicheType.Ent_Casino,
            NicheType.Ent_Lottery,
            NicheType.Ent_Poker,
            NicheType.Ent_FreeToPlay,
            NicheType.Ent_MMOs,
            NicheType.Ent_StreamingService
        };
        AttachNichesToIndustry(IndustryType.Entertainment, niches);

        MarketSettings gamblingCompanyProfile = new MarketSettings
        {
            AudienceSize = AudienceSize.Million,
            MonetisationType = Monetisation.IrregularPaid,
            Margin = Margin.Low,

            ProductComplexity = ProductComplexity.Low,

            Iteration = NicheSpeed.Year,
        };

        SetNichesAutomatically(NicheType.Ent_Lottery,   gamblingCompanyProfile, GetYear(2000));
        SetNichesAutomatically(NicheType.Ent_Casino,    gamblingCompanyProfile, GetYear(2001));
        SetNichesAutomatically(NicheType.Ent_Betting,   gamblingCompanyProfile, GetYear(2000));
        SetNichesAutomatically(NicheType.Ent_Poker,     gamblingCompanyProfile, GetYear(2001));
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



        SetNichesAutomatically(NicheType.CloudComputing, 
            new MarketSettings {
                AudienceSize = AudienceSize.ForSmallEnterprise,
                MonetisationType = Monetisation.Enterprise,
                Margin = Margin.High,

                ProductComplexity = ProductComplexity.High,

                Iteration = NicheSpeed.Year
            },
            GetYear(2000));

        SetNichesAutomatically(NicheType.SearchEngine,
            new MarketSettings
            {
                AudienceSize = AudienceSize.Global,
                MonetisationType = Monetisation.Ads,
                Margin = Margin.High,

                ProductComplexity = ProductComplexity.High,

                Iteration = NicheSpeed.ThreeYears
            },
            GetYear(1995));

        SetNichesAutomatically(NicheType.OSDesktop,
            new MarketSettings
            {
                AudienceSize = AudienceSize.Global,
                MonetisationType = Monetisation.Paid,
                Margin = Margin.High,

                ProductComplexity = ProductComplexity.High,

                Iteration = NicheSpeed.ThreeYears
            },
            GetYear(1980));

        SetNichesAutomatically(NicheType.Browser,
            new MarketSettings
            {
                AudienceSize = AudienceSize.Global,
                MonetisationType = Monetisation.Ads,
                Margin = Margin.High,

                ProductComplexity = ProductComplexity.High,

                Iteration = NicheSpeed.Year
            },
            GetYear(1990));
    }
}