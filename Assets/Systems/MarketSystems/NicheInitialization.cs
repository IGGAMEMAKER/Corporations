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
    TwoDecades = Decade * 2,
    EntireGame = 12 * 50
}

public enum NichePeriod
{
    Month = 1,
    Quarter = 3,
    HalfYear = 6,
    Year = 12,
    ThreeYears = 36
}

public enum AudienceSize
{
    BigEnterprise = 2000, // 2K
    SmallEnterprise = 50000, // 50K
    Million = 1000000, // 1M
    Million100 = 100000000, // 100M // usefull util AdBlock
    Global = 1000000000 // 1-2B
}

public enum Monetisation
{
    Adverts = 1,
    Service = 10,
    IrregularPaid = 25,
    Paid = 50, // (max income when making ads) + small additional payments
    Enterprise = 1000
}

public enum Margin
{
    Low = 1,
    Mid = 5,
    High = 20
}

public enum AppComplexity
{
    Easy = 3,
    Average = 7,
    Hard = 15,
    Humongous = 25
}


public struct MarketProfile
{
    public AudienceSize AudienceSize;
    public Monetisation MonetisationType;
    public Margin Margin;


    public NichePeriod NicheSpeed;
    public AppComplexity AppComplexity;
}

// everyone: operation systems, browsers, social networks (messaging + content)
// small niche project : 50K

public partial class MarketInitializerSystem : IInitializeSystem
{
    GameEntity SetNichesAutomatically(NicheType nicheType,
    int startDate,
    AudienceSize AudienceSize, Monetisation MonetisationType, Margin Margin, NichePeriod Iteration, AppComplexity ProductComplexity,
    NicheDuration nicheDuration = NicheDuration.EntireGame)
    {
        return SetNichesAutomatically(
            nicheType,
            startDate,
            new MarketProfile {
                AudienceSize = AudienceSize, NicheSpeed = Iteration, Margin = Margin, MonetisationType = MonetisationType, AppComplexity = ProductComplexity
            }, nicheDuration
            );
    }

    void StartsAt(NicheType nicheType, int startDate)
    {
        var n = GetNicheEntity(nicheType);

        n.ReplaceNicheLifecycle(GetYear(startDate), n.nicheLifecycle.Growth, n.nicheLifecycle.Period, n.nicheLifecycle.NicheChangeSpeed);
    }

    GameEntity SetNichesAutomatically(NicheType nicheType,
        int startDate,
        MarketProfile settings,
        NicheDuration nicheDuration = NicheDuration.EntireGame
        )
    {
        var nicheId = GetNicheId(nicheType);

        var price = GetProductPrice(settings.MonetisationType, settings.Margin, nicheId);

        var clients = GetBatchSize(settings.AudienceSize, nicheId);

        var ChangeSpeed = settings.NicheSpeed;
        var techCost = GetTechCost(ChangeSpeed, nicheId) * Constants.DEVELOPMENT_PRODUCTION_PROGRAMMER;
        var ideaCost = GetTechCost(ChangeSpeed, nicheId + 1);
        var marketingCost = 0;

        var adCosts = GetAdCost(clients, settings.MonetisationType, ChangeSpeed, nicheId);

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
            [0] = GetFullAudience(settings.AudienceSize, nicheId)
        };

        n.ReplaceNicheSegments(positionings);
        n.ReplaceNicheClientsContainer(clientsContainer);
        n.ReplaceNicheLifecycle(GetYear(startDate), n.nicheLifecycle.Growth, nicheDuration, ChangeSpeed);
        n.ReplaceNicheBaseProfile(settings);

        return n;
    }

    private int GetTechCost(NichePeriod techMaintenance, int nicheId)
    {
        var baseCost = (int)techMaintenance;

        return (int)Randomise(baseCost, nicheId);
    }


    float GetProductPrice(Monetisation monetisationType, Margin margin, int nicheId)
    {
        var baseCost = (int)monetisationType * (int)margin;

        float value = Randomise(baseCost * 1000, nicheId) / 12f / 1000f;

        return value;
    }

    int GetAdCost(long clientBatch, Monetisation monetisationType, NichePeriod nichePeriod, int nicheId)
    {
        var baseValue = (int)monetisationType;

        var repaymentTime = 1;

        switch (monetisationType)
        {
            case Monetisation.Adverts: repaymentTime = 5; break;
            case Monetisation.Enterprise: repaymentTime = 10; break;
            case Monetisation.Service: repaymentTime = 10; break;
            case Monetisation.Paid: repaymentTime = 3; break;
        }

        baseValue *= repaymentTime;

        return (int)Randomise(baseValue, nicheId);
    }

    long GetFullAudience(AudienceSize audienceSize, int nicheId)
    {
        return Randomise((long)audienceSize, nicheId);
    }

    long GetBatchSize(AudienceSize audience, int nicheId)
    {
        var repaymentPeriod = 20 * 12;
        return Randomise((long)audience / repaymentPeriod, nicheId);
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