using Entitas;

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

        NicheSpeed = NicheSpeed.Year,
    };

    MarketProfile GetSmallUtilityForOneDevProfile => new MarketProfile(AudienceSize.SmallUtil, Monetisation.Adverts, Margin.Low, AppComplexity.Solo, NicheSpeed.ThreeYears);
}