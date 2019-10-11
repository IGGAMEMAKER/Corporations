using System.Collections.Generic;
using System.Linq;
using Assets.Utils;
using Assets.Utils.Formatting;
using Entitas;



public enum NicheDuration
{
    // duration in months
    Year = 12,
    FiveYears = 60,
    Decade = 120,
    EntireGame = 12 * 50
}

public enum NicheSpeed
{
    Month = 1,
    Quarter = 3,
    HalfYear = 6,
    Year = 12,
    ThreeYears = 36
}

public enum AudienceSize
{
    ForBigEnterprise = 2000, // 2K
    ForSmallEnterprise = 50000, // 50K
    Million = 1000000, // 1M
    HundredMillion = 100000000, // 100M // usefull util AdBlock
    Global = 1000000000 // 1-2B
}

//public enum PriceCategory
//{
//    // dollars per user per year
//    CheapMass = 1,
//    FreeMass = 4,
//    CheapSubscription = 120, // Subscription model: 10$/month
//    ExpensiveSubscription = 500, // Subscription model: 10$/month
//    Enterprise = 50000,
//}

public enum Monetisation
{
    Ads,
    Service,
    IrregularPaid,
    Paid, // (max income when making ads) + small additional payments
    Enterprise
}

public enum Margin
{
    Low = 1,
    Mid = 5,
    High = 20
}

public enum NicheAds
{
    Low = 2000,
    Mid = 7000,
    High = 25000,
    Humongous = 85000
}

public enum ProductComplexity
{
    Low = 3,
    Mid = 7,
    High = 15,
    Humongous = 25
}


public enum MarketAttribute
{
    RepaymentMonth,
    RepaymentHalfYear,
    RepaymentYear,

    AudienceIncreased,
    AudienceNiche,
}


public struct MarketSettings
{
    public AudienceSize AudienceSize;
    public Monetisation MonetisationType;
    public Margin Margin;


    public NicheSpeed Iteration;
    public ProductComplexity ProductComplexity;
}

// everyone: operation systems, browsers, social networks (messaging + content)
// small niche project : 50K

public partial class MarketInitializerSystem : IInitializeSystem
{
    GameEntity SetNichesAutomatically(NicheType nicheType,
        MarketSettings settings,
        //AudienceSize AudienceSize,
        //MonetisationType MonetisationType,
        //IncomeSize IncomeSize,
        //NicheSpeed NicheChangeSpeed,
        //NicheAds NicheAdMaintenance,
        //NicheMaintenance NicheSupportMaintenance,


    //NicheDuration NicheDuration, AudienceSize audienceSize, MonetisationType priceCategory,
    //NicheChangeSpeed ChangeSpeed,
    // //ProductPositioning[] productPositionings,
    int startDate,
        MarketAttribute[] marketAttributes = null)
    {
        var nicheId = GetNicheId(nicheType);

        var price = GetProductPrice(settings.MonetisationType, settings.Margin, nicheId);

        var audience = GetFullAudience(settings.AudienceSize, nicheId);
        var clients = GetBatchSize(audience, nicheId);

        var ChangeSpeed = settings.Iteration;
        var techCost = GetTechCost(ChangeSpeed, nicheId) * Constants.DEVELOPMENT_PRODUCTION_PROGRAMMER;
        var ideaCost = GetTechCost(ChangeSpeed, nicheId + 1);
        var marketingCost = 0;

        var adCosts = GetAdCost(clients, nicheId);

        var n = SetNicheCosts(nicheType, price, clients, techCost, ideaCost, marketingCost, adCosts);




        var positionings = new Dictionary<int, ProductPositioning>
        {
            [0] = new ProductPositioning
            {
                isCompetitive = false,
                marketShare = 100,
                name = EnumUtils.GetSingleFormattedNicheName(nicheType)
            }
        };

        var clientsContainer = new Dictionary<int, long>
        {
            [0] = audience
        };

        n.ReplaceNicheSegments(positionings);
        n.ReplaceNicheClientsContainer(clientsContainer);
        n.ReplaceNicheLifecycle(startDate, n.nicheLifecycle.Growth, n.nicheLifecycle.Period, ChangeSpeed);


        return n;
    }


    bool GetAttribute(MarketAttribute[] marketAttributes, MarketAttribute attribute)
    {
        if (marketAttributes == null) return false;

        return marketAttributes.Contains(attribute);
    }

    private int GetTechCost(NicheSpeed techMaintenance, int nicheId)
    {
        var baseCost = (int)techMaintenance;

        return (int)Randomise(baseCost, nicheId);
    }


    float GetPriceModifierAttributeBased(MarketAttribute[] marketAttributes)
    {
        if (GetAttribute(marketAttributes, MarketAttribute.RepaymentMonth))
            return 5;
        if (GetAttribute(marketAttributes, MarketAttribute.RepaymentHalfYear))
            return 2;
        if (GetAttribute(marketAttributes, MarketAttribute.RepaymentYear))
            return 0.5f;

        return 1;
    }
    float GetProductPrice(Monetisation monetisationType, Margin incomeSize, int nicheId)
    {
        var baseCost = (int)monetisationType;

        float value = Randomise(baseCost * 1000, nicheId) / 12f / 1000f;

        return value;
    }

    long GetFullAudience(AudienceSize audienceSize, int nicheId)
    {
        return Randomise((long)audienceSize, nicheId);
    }

    long GetBatchSize (long audience, int nicheId)
    {
        return Randomise(audience, nicheId);
    }


    //int GetAdCost (NicheAdMaintenance nicheMaintenance, int nicheId)
    int GetAdCost (long clientBatch, int nicheId)
    {
        var baseValue = clientBatch / 2;

        return (int)Randomise(baseValue, nicheId);
    }



    long Randomise(long baseValue, int nicheId)
    {
        return CompanyUtils.GetRandomValue(baseValue, nicheId, 0);
    }

    int GetNicheId (NicheType nicheType)
    {
        return (int)nicheType;
    }
}