using Assets.Utils;
using Entitas;

public partial class MarketInitializerSystem : IInitializeSystem
{
    void InitializeCommunicationsIndustry()
    {
        var niches = new NicheType[] {
            NicheType.Messenger,
            NicheType.SocialNetwork,
            NicheType.Blogs,
            //NicheType.Dating,
            NicheType.Forums,
            NicheType.Email
        };
        AttachNichesToIndustry(IndustryType.Communications, niches);

        SetNicheCostsAutomatitcallty(NicheType.Messenger,
            NicheDuration.EntireGame, AudienceSize.WholeWorld,
            NicheMaintenance.Mid, PriceCategory.FreeMass, NicheChangeSpeed.Quarter,
            new ProductPositioning[] {
                new ProductPositioning { name = "Basic messenger", marketShare = 100 },
            },
           0);

        SetNicheCostsAutomatitcallty(NicheType.SocialNetwork,
            NicheDuration.EntireGame, AudienceSize.WholeWorld,
            NicheMaintenance.High, PriceCategory.FreeMass, NicheChangeSpeed.Year,
            new ProductPositioning[] {
                new ProductPositioning { name = "Basic social network", marketShare = 100 }, // fb
                new ProductPositioning { name = "Corporative social network", marketShare = 3, priceModifier = 10 }, // linkedIn
                new ProductPositioning { name = "Text focused social network", marketShare = 15, priceModifier = 1.75f }, // twitter
                new ProductPositioning { name = "Image focused social network", marketShare = 85 }, // insta
            },
           0);

        SetNicheCostsAutomatitcallty(NicheType.Blogs,
            NicheDuration.EntireGame, AudienceSize.MidSizedProduct,
            NicheMaintenance.Mid, PriceCategory.CheapMass, NicheChangeSpeed.Year,
            new ProductPositioning[] {},
           0);

        SetNicheCostsAutomatitcallty(NicheType.Forums,
            NicheDuration.EntireGame, AudienceSize.MidSizedProduct,
            NicheMaintenance.Mid, PriceCategory.CheapMass, NicheChangeSpeed.ThreeYears,
            new ProductPositioning[] {},
           0);

        //SetNicheCostsAutomatitcallty(NicheType.Dating,
        //    NicheDuration.EntireGame, AudienceSize.MidSizedProduct,
        //    NicheMaintenance.Mid, PriceCategory.Mid, NicheChangeSpeed.Year,
        //   0);



        //SetNicheCosts(NicheType.Messenger,      2, 75, 100, 75, 100, 1000);
        //SetNicheCosts(NicheType.SocialNetwork, 10, 100, 100, 100, 100, 1000);
    }

    private void InitializeFundamentalIndustry()
    {
        var niches = new NicheType[] {
            NicheType.SearchEngine,
            NicheType.OSDesktop,
            NicheType.CloudComputing,
        };
        AttachNichesToIndustry(IndustryType.Fundamental, niches);

        SetNicheCostsAutomatitcallty(NicheType.CloudComputing,
            NicheDuration.EntireGame, AudienceSize.MidSizedProduct,
            NicheMaintenance.Humongous, PriceCategory.Premium, NicheChangeSpeed.Year,
            new ProductPositioning[] { },
            0);

        SetNicheCostsAutomatitcallty(NicheType.SearchEngine,
            NicheDuration.EntireGame, AudienceSize.WholeWorld,
            NicheMaintenance.Humongous, PriceCategory.Premium, NicheChangeSpeed.ThreeYears,
            new ProductPositioning[] { },
            0);

        SetNicheCostsAutomatitcallty(NicheType.OSDesktop,
            NicheDuration.EntireGame, AudienceSize.WholeWorld,
            NicheMaintenance.Humongous, PriceCategory.Premium, NicheChangeSpeed.ThreeYears,
            new ProductPositioning[] { },
            0);

        //SetNicheCosts(NicheType.SearchEngine, 10, 100, 1000, 1000, 1000, 2000);
        //SetNicheCosts(NicheType.CloudComputing, 10, 100, 1000, 1000, 1000, 2000);
        //SetNicheCosts(NicheType.OSDesktop, 1000, 500, 1000, 1000, 1000, 5000);
    }
}