using Assets.Utils;
using Entitas;

public partial class MarketInitializerSystem : IInitializeSystem
{
    int GetYear(int year)
    {
        return (year - Constants.START_YEAR) * 360;
    }

    int GetYearAndADate(int year, int quarter)
    {
        return GetYear(year) + quarter * 90;
    }

    void InitializeCommunicationsIndustry()
    {
        var niches = new NicheType[] {
            NicheType.Messenger,
            NicheType.SocialNetwork,
            NicheType.Blogs,
            NicheType.Forums,
            NicheType.Email
        };
        AttachNichesToIndustry(IndustryType.Communications, niches);



        SetNichesAutomatically(NicheType.Email,
            NicheDuration.EntireGame, AudienceSize.WholeWorld, PriceCategory.CheapMass, NicheChangeSpeed.Year,
            NicheMaintenance.Mid, NicheTechMaintenance.High, NicheMarketingMaintenance.High,
            new ProductPositioning[] { },
            GetYear(1990));

        SetNichesAutomatically(NicheType.Forums,
            NicheDuration.Decade, AudienceSize.MidSizedProduct, PriceCategory.CheapMass, NicheChangeSpeed.ThreeYears,
            NicheMaintenance.Mid, NicheTechMaintenance.High, NicheMarketingMaintenance.High,
            new ProductPositioning[] { },
            GetYear(1990));

        SetNichesAutomatically(NicheType.Blogs,
            NicheDuration.Decade, AudienceSize.MidSizedProduct, PriceCategory.CheapMass, NicheChangeSpeed.Year,
            NicheMaintenance.Mid, NicheTechMaintenance.High, NicheMarketingMaintenance.High,
            new ProductPositioning[] {},
            GetYear(1995));

        SetNichesAutomatically(NicheType.SocialNetwork,
            NicheDuration.EntireGame, AudienceSize.WholeWorld, PriceCategory.FreeMass, NicheChangeSpeed.Year,
            NicheMaintenance.High, NicheTechMaintenance.High, NicheMarketingMaintenance.High,
            new ProductPositioning[] {
                new ProductPositioning { name = "Basic social network", marketShare = 100 }, // fb
                new ProductPositioning { name = "Corporative social network", marketShare = 3, priceModifier = 10 }, // linkedIn
                new ProductPositioning { name = "Text focused social network", marketShare = 15, priceModifier = 1.75f }, // twitter
                new ProductPositioning { name = "Image focused social network", marketShare = 85 }, // insta
            },
            GetYear(2000));

        SetNichesAutomatically(NicheType.Messenger,
            NicheDuration.EntireGame, AudienceSize.WholeWorld, PriceCategory.FreeMass, NicheChangeSpeed.Quarter,
            NicheMaintenance.Mid, NicheTechMaintenance.High, NicheMarketingMaintenance.High,
            new ProductPositioning[] {},
            GetYear(2005));
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
            NicheDuration.EntireGame, AudienceSize.MidSizedProduct, PriceCategory.CheapSubscription, NicheChangeSpeed.Year,
            NicheMaintenance.Humongous, NicheTechMaintenance.High, NicheMarketingMaintenance.Mid,
            new ProductPositioning[] { },
            GetYear(2000));

        SetNichesAutomatically(NicheType.SearchEngine,
            NicheDuration.EntireGame, AudienceSize.WholeWorld, PriceCategory.CheapSubscription, NicheChangeSpeed.ThreeYears,
            NicheMaintenance.Humongous, NicheTechMaintenance.Humongous, NicheMarketingMaintenance.Humongous,
            new ProductPositioning[] { },
            GetYear(1995));

        SetNichesAutomatically(NicheType.OSDesktop,
            NicheDuration.EntireGame, AudienceSize.WholeWorld, PriceCategory.CheapSubscription, NicheChangeSpeed.ThreeYears,
            NicheMaintenance.Humongous, NicheTechMaintenance.Humongous, NicheMarketingMaintenance.Humongous,
            new ProductPositioning[] { },
            GetYear(1980));

        SetNichesAutomatically(NicheType.Browser,
            NicheDuration.EntireGame, AudienceSize.WholeWorld, PriceCategory.CheapSubscription, NicheChangeSpeed.ThreeYears,
            NicheMaintenance.Humongous, NicheTechMaintenance.Humongous, NicheMarketingMaintenance.Mid,
            new ProductPositioning[] { },
            GetYear(1990));
    }
}