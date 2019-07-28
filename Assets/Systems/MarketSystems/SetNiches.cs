using Assets.Utils;
using Entitas;

public partial class MarketInitializerSystem : IInitializeSystem
{
    int GetYear(int year)
    {
        return (year - 1990) * 360;
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

        SetNicheCostsAutomatitcallty(NicheType.Email,
            NicheDuration.EntireGame, AudienceSize.WholeWorld,
            NicheMaintenance.Mid, PriceCategory.CheapMass, NicheChangeSpeed.Year,
            new ProductPositioning[] { },
            GetYear(1990)
       );

        SetNicheCostsAutomatitcallty(NicheType.Forums,
            NicheDuration.Decade, AudienceSize.MidSizedProduct,
            NicheMaintenance.Mid, PriceCategory.CheapMass, NicheChangeSpeed.ThreeYears,
            new ProductPositioning[] { },
            GetYear(1990)
           );

        SetNicheCostsAutomatitcallty(NicheType.Blogs,
            NicheDuration.Decade, AudienceSize.MidSizedProduct,
            NicheMaintenance.Mid, PriceCategory.CheapMass, NicheChangeSpeed.Year,
            new ProductPositioning[] {},
            GetYear(1995)
           );

        SetNicheCostsAutomatitcallty(NicheType.SocialNetwork,
            NicheDuration.EntireGame, AudienceSize.WholeWorld,
            NicheMaintenance.High, PriceCategory.FreeMass, NicheChangeSpeed.Year,
            new ProductPositioning[] {
                new ProductPositioning { name = "Basic social network", marketShare = 100 }, // fb
                new ProductPositioning { name = "Corporative social network", marketShare = 3, priceModifier = 10 }, // linkedIn
                new ProductPositioning { name = "Text focused social network", marketShare = 15, priceModifier = 1.75f }, // twitter
                new ProductPositioning { name = "Image focused social network", marketShare = 85 }, // insta
            },
            GetYear(2000)
           );

        SetNicheCostsAutomatitcallty(NicheType.Messenger,
            NicheDuration.EntireGame, AudienceSize.WholeWorld,
            NicheMaintenance.Mid, PriceCategory.FreeMass, NicheChangeSpeed.Quarter,
            new ProductPositioning[] {
                new ProductPositioning { name = "Basic messenger", marketShare = 100 },
            },
            GetYear(2005)
           );
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
            GetYear(2000));

        SetNicheCostsAutomatitcallty(NicheType.SearchEngine,
            NicheDuration.EntireGame, AudienceSize.WholeWorld,
            NicheMaintenance.Humongous, PriceCategory.Premium, NicheChangeSpeed.ThreeYears,
            new ProductPositioning[] { },
            GetYear(1995));

        SetNicheCostsAutomatitcallty(NicheType.OSDesktop,
            NicheDuration.EntireGame, AudienceSize.WholeWorld,
            NicheMaintenance.Humongous, PriceCategory.Premium, NicheChangeSpeed.ThreeYears,
            new ProductPositioning[] { },
            GetYear(1980));

        SetNicheCostsAutomatitcallty(NicheType.Browser,
            NicheDuration.EntireGame, AudienceSize.WholeWorld,
            NicheMaintenance.Humongous, PriceCategory.Premium, NicheChangeSpeed.ThreeYears,
            new ProductPositioning[] { },
            GetYear(1990));
    }
}