using System.Collections.Generic;
using Assets.Core;
using Entitas;


// everyone: operation systems, browsers, social networks (messaging + content)
// small niche project : 50K

public partial class MarketInitializerSystem : IInitializeSystem
{
    int GetYear(int year) => ScheduleUtils.GetDateByYear(year);

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
                name = Enums.GetSingleFormattedNicheName(nicheType)
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