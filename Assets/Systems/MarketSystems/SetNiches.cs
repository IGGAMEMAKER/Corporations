using Entitas;

public partial class MarketInitializerSystem : IInitializeSystem
{
    int GetYear(int year) => (year - Constants.START_YEAR) * 360;

    int GetYearAndADate(int year, int quarter) => GetYear(year) + quarter * 90;


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

        MarketProfile popularUsefulApp = new MarketProfile
        {
            AudienceSize = AudienceSize.HundredMillion,
            MonetisationType = Monetisation.Ads,
            Margin = Margin.Low,

            ProductComplexity = ProductComplexity.Low,

            Iteration = NicheSpeed.Year,
        };

        MarketProfile rarelyUsedApp = new MarketProfile
        {
            AudienceSize = AudienceSize.HundredMillion,
            MonetisationType = Monetisation.Ads,
            Margin = Margin.Low,

            ProductComplexity = ProductComplexity.Low,

            Iteration = NicheSpeed.ThreeYears,
        };



        SetNichesAutomatically(NicheType.Com_Email,  1990, popularUsefulApp);
        SetNichesAutomatically(NicheType.Com_Forums, 1990, rarelyUsedApp);
        SetNichesAutomatically(NicheType.Com_Blogs,  1995, rarelyUsedApp);
        SetNichesAutomatically(NicheType.Com_Dating, 2000, popularUsefulApp);


        SetNichesAutomatically(NicheType.Com_SocialNetwork, 1999,
            new MarketProfile
            {
                AudienceSize = AudienceSize.Global,
                MonetisationType = Monetisation.Ads,
                Margin = Margin.Mid,

                ProductComplexity = ProductComplexity.Low,

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
                MonetisationType = Monetisation.Ads,
                Margin = Margin.Low,

                ProductComplexity = ProductComplexity.Low,

                Iteration = NicheSpeed.Quarter,
            });
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

        MarketProfile gamblingCompanyProfile = new MarketProfile
        {
            AudienceSize = AudienceSize.Million,
            MonetisationType = Monetisation.IrregularPaid,
            Margin = Margin.Low,

            ProductComplexity = ProductComplexity.Low,

            Iteration = NicheSpeed.ThreeYears,
        };

        SetNichesAutomatically(NicheType.Ent_Lottery, 2000, gamblingCompanyProfile);
        SetNichesAutomatically(NicheType.Ent_Casino,  2001, gamblingCompanyProfile);
        SetNichesAutomatically(NicheType.Ent_Betting, 2000, gamblingCompanyProfile);
        SetNichesAutomatically(NicheType.Ent_Poker,   2001, gamblingCompanyProfile);
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



        SetNichesAutomatically(NicheType.CloudComputing, 2000,
            new MarketProfile {
                AudienceSize = AudienceSize.SmallEnterprise,
                MonetisationType = Monetisation.Enterprise,
                Margin = Margin.High,

                ProductComplexity = ProductComplexity.High,

                Iteration = NicheSpeed.Year
            });

        SetNichesAutomatically(NicheType.SearchEngine, 1995,
            new MarketProfile
            {
                AudienceSize = AudienceSize.Global,
                MonetisationType = Monetisation.Ads,
                Margin = Margin.High,

                ProductComplexity = ProductComplexity.High,

                Iteration = NicheSpeed.ThreeYears
            });

        SetNichesAutomatically(NicheType.OSDesktop, 1980,
            new MarketProfile
            {
                AudienceSize = AudienceSize.Global,
                MonetisationType = Monetisation.Paid,
                Margin = Margin.High,

                ProductComplexity = ProductComplexity.High,

                Iteration = NicheSpeed.ThreeYears
            });

        SetNichesAutomatically(NicheType.Browser, 1990,
            new MarketProfile
            {
                AudienceSize = AudienceSize.Global,
                MonetisationType = Monetisation.Ads,
                Margin = Margin.High,

                ProductComplexity = ProductComplexity.High,

                Iteration = NicheSpeed.Year
            });
    }
}