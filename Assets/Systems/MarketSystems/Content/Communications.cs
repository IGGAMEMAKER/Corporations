using Entitas;

public partial class MarketInitializerSystem : IInitializeSystem
{
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

        SetMarkets(NicheType.Com_Email, 1990, GetPopularUsefulAppProfile);
        SetMarkets(NicheType.Com_Forums, 1990, GetPopularRarelyUsedAppProfile);
        SetMarkets(NicheType.Com_Blogs, 1995, GetPopularRarelyUsedAppProfile);
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
}
