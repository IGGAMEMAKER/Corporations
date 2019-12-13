﻿using Entitas;

public partial class MarketInitializerSystem : IInitializeSystem
{
    int GetYear(int year) => (year - Constants.START_YEAR) * 360;

    int GetYearAndADate(int year, int quarter) => GetYear(year) + quarter * 90;


    void InitializeCommunicationsIndustry()
    {
        var niches = new NicheType[] {
            NicheType.Com_Messenger,        // chatting***, info
            NicheType.Com_SocialNetwork,    // users, chatting, info
            NicheType.Com_Blogs,            // info, users, chatting
            NicheType.Com_Forums,           // info, chatting
            NicheType.Com_Email,            // chatting

            NicheType.Com_Dating,
        };
        AttachNichesToIndustry(IndustryType.Communications, niches);

        SetMarkets(NicheType.Com_Email,  1990, GetPopularUsefulAppProfile);
        SetMarkets(NicheType.Com_Forums, 1990, GetPopularRarelyUsedAppProfile);
        SetMarkets(NicheType.Com_Blogs,  1995, GetPopularRarelyUsedAppProfile);
        SetMarkets(NicheType.Com_Dating, 2000, GetPopularUsefulAppProfile);


        SetMarkets(NicheType.Com_SocialNetwork, 1999,
            new MarketProfile
            {
                AudienceSize = AudienceSize.Global,
                MonetisationType = Monetisation.Adverts,
                Margin = Margin.Mid,

                AppComplexity = AppComplexity.Hard,

                NicheSpeed = NicheSpeed.Year,
            }
            //new ProductPositioning[] {
            //    //new ProductPositioning { name = "Basic social network", marketShare = 100 }, // fb
            //    //new ProductPositioning { name = "Corporative social network", marketShare = 3, priceModifier = 10 }, // linkedIn
            //    //new ProductPositioning { name = "Text focused social network", marketShare = 15, priceModifier = 1.75f }, // twitter
            //    //new ProductPositioning { name = "Image focused social network", marketShare = 85 }, // insta
            //},
            );

        var messenger = new MarketProfile
        {
            AudienceSize = AudienceSize.Global,
            MonetisationType = Monetisation.Adverts,
            Margin = Margin.Low,

            AppComplexity = AppComplexity.Easy,

            NicheSpeed = NicheSpeed.Quarter,
        };

        messenger = new MarketProfile().Free().Global().LowMargin().WebService().Dynamic();

        SetMarkets(NicheType.Com_Messenger, 2000, messenger);
    }

    private void InitializeEcommerceIndustry()
    {
        //return;
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

        var banking = new MarketProfile
        {
            AudienceSize = AudienceSize.Global,
            MonetisationType = Monetisation.Service,
            Margin = Margin.Low,

            AppComplexity = AppComplexity.Average,

            NicheSpeed = NicheSpeed.HalfYear,
        };

        SetMarkets(NicheType.Fin_Exchanging,    1998, financialSystemMarket);
        SetMarkets(NicheType.Fin_OnlineBanking, 1992, banking);
        SetMarkets(NicheType.Fin_PaymentSystem, 1995, financialSystemMarket);
    }

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

        

        SetMarkets(NicheType.Ent_Lottery,           2000, GetGamblingCompanyProfile);
        SetMarkets(NicheType.Ent_Casino,            2001, GetGamblingCompanyProfile);
        SetMarkets(NicheType.Ent_Betting,           2000, GetGamblingCompanyProfile);
        SetMarkets(NicheType.Ent_Poker,             2001, GetGamblingCompanyProfile);

        SetMarkets(NicheType.Ent_FreeToPlay,        2001,
            AudienceSize.Million100, Monetisation.Adverts, Margin.Mid, NicheSpeed.Year, AppComplexity.Average);

        SetMarkets(NicheType.Ent_MMOs,              2000,
            AudienceSize.Million,    Monetisation.Service, Margin.Mid, NicheSpeed.Year, AppComplexity.Average);


        SetMarkets(NicheType.Ent_StreamingService,  2011,
            AudienceSize.Million100, Monetisation.Service, Margin.Low, NicheSpeed.HalfYear, AppComplexity.Easy);
        SetMarkets(NicheType.Ent_TVStreamingService,  2016,
            AudienceSize.Million100, Monetisation.Service, Margin.Mid, NicheSpeed.HalfYear, AppComplexity.Average);
    }

    private void InitializeFundamentalIndustry()
    {
        var niches = new NicheType[] {
            NicheType.Tech_SearchEngine,
            NicheType.Tech_OSDesktop,
            NicheType.Tech_CloudComputing,
            NicheType.Tech_Browser,
        };
        AttachNichesToIndustry(IndustryType.Technology, niches);

        var cloud =          new MarketProfile
        {
            AudienceSize = AudienceSize.SmallEnterprise,
            MonetisationType = Monetisation.Enterprise,
            Margin = Margin.High,

            AppComplexity = AppComplexity.Hard,

            NicheSpeed = NicheSpeed.Year
        };
        var searchEngine =   new MarketProfile
        {
            AudienceSize = AudienceSize.Global,
            MonetisationType = Monetisation.Adverts,
            Margin = Margin.High,

            AppComplexity = AppComplexity.Humongous,

            NicheSpeed = NicheSpeed.ThreeYears
        };
        var desktop =        new MarketProfile
        {
            AudienceSize = AudienceSize.Global,
            MonetisationType = Monetisation.Paid,
            Margin = Margin.High,

            AppComplexity = AppComplexity.Humongous,

            NicheSpeed = NicheSpeed.ThreeYears
        };
        var browser =        new MarketProfile
        {
            AudienceSize = AudienceSize.Global,
            MonetisationType = Monetisation.Adverts,
            Margin = Margin.High,

            AppComplexity = AppComplexity.Hard,

            NicheSpeed = NicheSpeed.Year
        };

        SetMarkets(NicheType.Tech_CloudComputing, 2000, cloud);
        SetMarkets(NicheType.Tech_SearchEngine,   1995, searchEngine);
        SetMarkets(NicheType.Tech_OSDesktop,      1980, desktop);
        SetMarkets(NicheType.Tech_Browser,        1990, browser);
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

        AppComplexity = AppComplexity.Average,

        NicheSpeed = NicheSpeed.ThreeYears,
    };

    MarketProfile GetPopularUsefulAppProfile => new MarketProfile
    {
        AudienceSize = AudienceSize.Million100,
        MonetisationType = Monetisation.Adverts,
        Margin = Margin.Low,

        AppComplexity = AppComplexity.Hard,

        NicheSpeed = NicheSpeed.Year,
    };

    MarketProfile GetGamblingCompanyProfile => new MarketProfile
    {
        AudienceSize = AudienceSize.Million,
        MonetisationType = Monetisation.IrregularPaid,
        Margin = Margin.Mid,

        AppComplexity = AppComplexity.Average,

        NicheSpeed = NicheSpeed.ThreeYears,
    };
}