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


        var socialNetworks =
            new MarketProfile(AudienceSize.Global, Monetisation.Adverts, Margin.Mid, AppComplexity.Hard, NicheSpeed.Year);

        var messenger =
            new MarketProfile(AudienceSize.Global, Monetisation.Adverts, Margin.Mid, AppComplexity.Hard, NicheSpeed.Year)
            .IncomeLow().WebService().Dynamic();

        SetMarkets(NicheType.Com_Email, 1990, 2020, GetPopularUsefulAppProfile);
        SetMarkets(NicheType.Com_Forums, 1990, 2020, GetPopularRarelyUsedAppProfile);
        SetMarkets(NicheType.Com_Blogs, 1995, 2020, GetPopularRarelyUsedAppProfile);
        SetMarkets(NicheType.Com_Dating, 2000, 2020, GetPopularUsefulAppProfile);

        SetMarkets(NicheType.Com_Messenger, 2000, 2030, messenger);
        SetMarkets(NicheType.Com_SocialNetwork, 1999, 2025, socialNetworks);
        //new ProductPositioning[] {
        //    //new ProductPositioning { name = "Basic social network", marketShare = 100 }, // fb
        //    //new ProductPositioning { name = "Corporative social network", marketShare = 3, priceModifier = 10 }, // linkedIn
        //    //new ProductPositioning { name = "Text focused social network", marketShare = 15, priceModifier = 1.75f }, // twitter
        //    //new ProductPositioning { name = "Image focused social network", marketShare = 85 }, // insta
        //},
    }
}
