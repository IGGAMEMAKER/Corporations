using System.Collections.Generic;
using System.Linq;
using Assets.Utils;
using Assets.Utils.Formatting;
using Entitas;
using UnityEngine;

public enum NicheDuration
{
    // duration in months
    Year = 12,
    FiveYears = Year * 5,
    Decade = Year * 10,
    TwoDecades = Decade * 2,
    EntireGame = Decade * 4
}

public enum NichePeriod
{
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
    NicheDuration nicheDuration = NicheDuration.EntireGame) => SetNichesAutomatically(
            nicheType,
            startDate,
            new MarketProfile
            {
                AudienceSize = AudienceSize,
                NicheSpeed = Iteration,
                Margin = Margin,
                MonetisationType = MonetisationType,
                AppComplexity = ProductComplexity
            }, nicheDuration
            );

    GameEntity SetNichesAutomatically(NicheType nicheType,
        int startDate,
        MarketProfile settings,
        NicheDuration nicheDuration = NicheDuration.EntireGame
        )
    {
        var nicheId = GetNicheId(nicheType);
        var AppComplexity = settings.AppComplexity;
        var ChangeSpeed = settings.NicheSpeed;

        var price = GetProductPrice(settings.MonetisationType, settings.Margin, nicheId);
        var clients = GetBatchSize(settings.AudienceSize, nicheDuration, nicheId, GetNiche(nicheType));
        var techCost = GetTechCost(AppComplexity, nicheId) * Constants.DEVELOPMENT_PRODUCTION_PROGRAMMER;
        var adCosts = GetAdCost(clients, settings.MonetisationType, ChangeSpeed, nicheId);


        var n = SetNicheCosts(nicheType, price, clients, techCost, adCosts);


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



    private int GetTechCost(AppComplexity complexity, int nicheId)
    {
        var baseCost = (int)complexity;

        return (int)Randomise(baseCost, nicheId);
    }


    float GetProductPrice(Monetisation monetisationType, Margin margin, int nicheId)
    {
        var baseCost = (int)monetisationType * (int)margin;

        return Randomise(baseCost * 1000, nicheId) / 12f / 1000f;
    }

    float GetAdCost(long clientBatch, Monetisation monetisationType, NichePeriod nichePeriod, int nicheId)
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

        //baseValue *= repaymentTime;

        return Randomise(baseValue * 1000, nicheId) / 1000f;
    }



    long GetFullAudience(AudienceSize audienceSize, int nicheId)
    {
        return Randomise((long)audienceSize, nicheId);
    }

    long GetBatchSize(AudienceSize audience, NicheDuration nicheDuration, int nicheId, GameEntity niche)
    {
        var repaymentPeriod = (int)nicheDuration;

        var possibleMonthlyGrowth = 1.1d;

        //var batch = (long)audience / Mathf.Pow(possibleMonthlyGrowth, repaymentPeriod);
        var chisl = (long)audience * (1 - possibleMonthlyGrowth);
        //Debug.Log($"Get batch size {niche.niche.NicheType}: chisl {chisl}");

        var znam = (1 - System.Math.Pow(possibleMonthlyGrowth, repaymentPeriod));
        //Debug.Log($"Get batch size {niche.niche.NicheType}: znam {znam}");


        var batch = (chisl / znam);

        Debug.Log($"Get batch size {niche.niche.NicheType}: {batch}");

        return (long)batch;
        //return Randomise(Mathf.Pow(, nicheId);
        //return Randomise((long)audience / repaymentPeriod, nicheId);
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