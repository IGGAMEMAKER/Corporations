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
            NicheType.Email,
            NicheType.Dating
        };
        AttachNichesToIndustry(IndustryType.Communications, niches);



        SetNichesAutomatically(NicheType.Email,
            NicheDuration.EntireGame, AudienceSize.Billion, PriceCategory.CheapMass, NicheChangeSpeed.Year,
            GetYear(1990),
            new MarketAttribute[] { MarketAttribute.RepaymentYear }
            );

        SetNichesAutomatically(NicheType.Forums,
            NicheDuration.Decade, AudienceSize.HundredMillion, PriceCategory.CheapMass, NicheChangeSpeed.ThreeYears,
            GetYear(1990));

        SetNichesAutomatically(NicheType.Blogs,
            NicheDuration.Decade, AudienceSize.HundredMillion, PriceCategory.CheapMass, NicheChangeSpeed.Year,
            GetYear(1995));

        SetNichesAutomatically(NicheType.SocialNetwork,
            NicheDuration.EntireGame, AudienceSize.Billion, PriceCategory.FreeMass, NicheChangeSpeed.Year,
            //new ProductPositioning[] {
            //    //new ProductPositioning { name = "Basic social network", marketShare = 100 }, // fb
            //    //new ProductPositioning { name = "Corporative social network", marketShare = 3, priceModifier = 10 }, // linkedIn
            //    //new ProductPositioning { name = "Text focused social network", marketShare = 15, priceModifier = 1.75f }, // twitter
            //    //new ProductPositioning { name = "Image focused social network", marketShare = 85 }, // insta
            //},
            GetYear(1999));

        SetNichesAutomatically(NicheType.Messenger,
            NicheDuration.EntireGame, AudienceSize.Billion, PriceCategory.FreeMass, NicheChangeSpeed.Quarter,
            GetYear(2005));
    }


    private void InitializeEntertainmentIndustry()
    {
        var niches = new NicheType[] {
            NicheType.GamblingBetting,
            NicheType.GamblingCasino,
            NicheType.GamblingLottery,
            NicheType.GamblingPoker,
            NicheType.GamingF2P,
            NicheType.GamingMMO,
            NicheType.StreamingService
        };
        AttachNichesToIndustry(IndustryType.Entertainment, niches);



        SetNichesAutomatically(NicheType.GamblingLottery,
            NicheDuration.EntireGame, AudienceSize.Million, PriceCategory.ExpensiveSubscription, NicheChangeSpeed.Year,
            GetYear(2000)
            );

        SetNichesAutomatically(NicheType.GamblingCasino,
            NicheDuration.EntireGame, AudienceSize.Million, PriceCategory.ExpensiveSubscription, NicheChangeSpeed.Year,
            GetYear(2001),
            new MarketAttribute[] { MarketAttribute.RepaymentMonth, MarketAttribute.AudienceIncreased }
            );

        SetNichesAutomatically(NicheType.GamblingBetting,
            NicheDuration.EntireGame, AudienceSize.Million, PriceCategory.ExpensiveSubscription, NicheChangeSpeed.Year,
            GetYear(2000)
            );

        SetNichesAutomatically(NicheType.GamblingPoker,
            NicheDuration.EntireGame, AudienceSize.Million, PriceCategory.ExpensiveSubscription, NicheChangeSpeed.Year,
            GetYear(2001)
            );
    }

    private void InitializeEcommerceIndustry()
    {
        var niches = new NicheType[] {
            NicheType.MarketplaceB2B,
            NicheType.MarketplaceB2C,
            NicheType.MarketplaceC2C,
            NicheType.MarketplaceGlobal
        };
        AttachNichesToIndustry(IndustryType.Ecommerce, niches);



        SetNichesAutomatically(NicheType.MarketplaceB2B,
            NicheDuration.EntireGame, AudienceSize.Million, PriceCategory.ExpensiveSubscription, NicheChangeSpeed.Year,
            GetYear(2003)
            );

        SetNichesAutomatically(NicheType.MarketplaceB2C,
            NicheDuration.EntireGame, AudienceSize.Million, PriceCategory.ExpensiveSubscription, NicheChangeSpeed.Year,
            GetYear(2005),
            new MarketAttribute[] { MarketAttribute.RepaymentMonth, MarketAttribute.AudienceIncreased }
            );

        SetNichesAutomatically(NicheType.MarketplaceC2C,
            NicheDuration.EntireGame, AudienceSize.HundredMillion, PriceCategory.CheapSubscription, NicheChangeSpeed.Year,
            GetYear(2006)
            );

        SetNichesAutomatically(NicheType.MarketplaceGlobal,
            NicheDuration.EntireGame, AudienceSize.HundredMillion, PriceCategory.ExpensiveSubscription, NicheChangeSpeed.Year,
            GetYear(2001),
            new MarketAttribute[] { MarketAttribute.AudienceIncreased }
            );
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
            NicheDuration.EntireGame, AudienceSize.ForSmallEnterprise, PriceCategory.Enterprise, NicheChangeSpeed.Year,
            GetYear(2000));

        SetNichesAutomatically(NicheType.SearchEngine,
            NicheDuration.EntireGame, AudienceSize.Billion, PriceCategory.FreeMass, NicheChangeSpeed.ThreeYears,
            GetYear(1995));

        SetNichesAutomatically(NicheType.OSDesktop,
            NicheDuration.EntireGame, AudienceSize.Billion, PriceCategory.ExpensiveSubscription, NicheChangeSpeed.ThreeYears,
            GetYear(1980));

        SetNichesAutomatically(NicheType.Browser,
            NicheDuration.EntireGame, AudienceSize.Billion, PriceCategory.CheapMass, NicheChangeSpeed.ThreeYears,
            GetYear(1990));
    }
}