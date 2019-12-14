using System.Collections.Generic;
using System.Linq;
using Assets.Utils;
using Assets.Utils.Formatting;
using Entitas;
using UnityEngine;

public enum NicheSpeed
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
    SmallUtil = SmallEnterprise,
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
    Enterprise = 1000,
}

public enum Margin
{
    Low = 5,
    Mid = 10,
    High = 20
}

public enum AppComplexity
{
    Solo = 1,
    Easy = 3,
    Average = 6,
    Hard = 10,
    Humongous = 12
}


// everyone: operation systems, browsers, social networks (messaging + content)
// small niche project : 50K

public partial class MarketInitializerSystem : IInitializeSystem
{
    int GetYear(int year) => (year - Constants.START_YEAR) * 360;

    int GetYearAndADate(int year, int quarter) => GetYear(year) + quarter * 90;

    GameEntity SetMarkets(NicheType nicheType,
    int startDate,
    int duration,
    AudienceSize AudienceSize, Monetisation MonetisationType, Margin Margin, NicheSpeed Iteration, AppComplexity ProductComplexity
    )
    {
        return SetMarkets(
            nicheType,
            startDate,
            duration,
            new MarketProfile
            {
                AudienceSize = AudienceSize,
                NicheSpeed = Iteration,
                Margin = Margin,
                MonetisationType = MonetisationType,
                AppComplexity = ProductComplexity
            }
            );
    }

    GameEntity SetMarkets(NicheType nicheType,
        int startDate,
        int duration,
        MarketProfile settings
        )
    {
        var nicheId = GetNicheId(nicheType);

        var clients     = GetFullAudience   (settings, nicheId);
        var techCost    = GetTechCost       (settings, nicheId);
        var adCosts     = GetAdCost         (settings, nicheId);
        var price       = GetProductPrice   (settings, adCosts, nicheId);


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
            [0] = clients
        };

        n.ReplaceNicheSegments(positionings);
        n.ReplaceNicheClientsContainer(clientsContainer);
        n.ReplaceNicheLifecycle(GetYear(startDate), n.nicheLifecycle.Growth, GetYear(duration));
        n.ReplaceNicheBaseProfile(settings);

        return n;
    }


    long Randomise(long baseValue, int nicheId)
    {
        return Companies.GetRandomValue(baseValue, nicheId, 0);
    }

    int GetNicheId (NicheType nicheType)
    {
        return (int)nicheType;
    }
}



public partial class MarketInitializerSystem : IInitializeSystem
{
    long GetFullAudience(MarketProfile profile, int nicheId)
    {
        AudienceSize audienceSize = profile.AudienceSize;

        return Randomise((long)audienceSize, nicheId);
    }

    private int GetTechCost(MarketProfile profile, int nicheId)
    {
        return (int)Randomise((int)profile.AppComplexity, nicheId);
    }


    float GetProductPrice(MarketProfile profile, float adCost, int nicheId)
    {
        Monetisation monetisationType = profile.MonetisationType;
        Margin margin = profile.Margin;

        var baseCost = adCost * (100 + (int)margin);

        return baseCost / 100f;
    }


    float GetAdCost(MarketProfile profile, int nicheId)
    {
        Monetisation monetisationType = profile.MonetisationType;

        var baseValue = (int)monetisationType;

        //var repaymentTime = GetSelfPaymentTime(monetisationType);
        //baseValue *= repaymentTime;

        return Randomise(baseValue * 1000, nicheId) / 12f / 1000f;
    }

    float GetSelfPaymentTime(Monetisation monetisationType)
    {
        switch (monetisationType)
        {
            case Monetisation.Adverts: return 10;
            case Monetisation.Service: return 8;
            case Monetisation.Enterprise: return 5;
            case Monetisation.Paid: return 3;
        }

        return 1;
    }
}