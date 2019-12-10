﻿using System.Collections.Generic;
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
    Solo = 1,
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


    public NicheSpeed NicheSpeed;
    public AppComplexity AppComplexity;
}

// everyone: operation systems, browsers, social networks (messaging + content)
// small niche project : 50K

public partial class MarketInitializerSystem : IInitializeSystem
{
    GameEntity SetNichesAutomatically(NicheType nicheType,
    int startDate,
    AudienceSize AudienceSize, Monetisation MonetisationType, Margin Margin, NicheSpeed Iteration, AppComplexity ProductComplexity
    ) => SetNichesAutomatically(
            nicheType,
            startDate,
            new MarketProfile
            {
                AudienceSize = AudienceSize,
                NicheSpeed = Iteration,
                Margin = Margin,
                MonetisationType = MonetisationType,
                AppComplexity = ProductComplexity
            }
            );

    GameEntity SetNichesAutomatically(NicheType nicheType,
        int startDate,
        MarketProfile settings
        )
    {
        var nicheId = GetNicheId(nicheType);

        var price       = GetProductPrice   (settings, nicheId);
        var clients     = GetFullAudience   (settings, nicheId);
        var techCost    = GetTechCost       (settings, nicheId);
        var adCosts     = GetAdCost         (settings, nicheId);


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
        n.ReplaceNicheLifecycle(GetYear(startDate), n.nicheLifecycle.Growth);
        n.ReplaceNicheBaseProfile(settings);

        return n;
    }



    private int GetTechCost(MarketProfile profile, int nicheId)
    {
        return (int)Randomise((int)profile.AppComplexity, nicheId);
    }


    float GetProductPrice(MarketProfile profile, int nicheId)
    {
        Monetisation monetisationType = profile.MonetisationType;
        Margin margin = profile.Margin;

        var baseCost = (int)monetisationType * (int)margin;

        return Randomise(baseCost * 1000, nicheId) / 12f / 1000f;
    }

    float GetAdCost(MarketProfile profile, int nicheId)
    {
        Monetisation monetisationType = profile.MonetisationType;
        NicheSpeed nichePeriod = profile.NicheSpeed;

        var baseValue = (int)monetisationType;

        var repaymentTime = 1;

        switch (monetisationType)
        {
            case Monetisation.Adverts: repaymentTime = 10; break;
            case Monetisation.Service: repaymentTime = 8; break;
            case Monetisation.Enterprise: repaymentTime = 5; break;
            case Monetisation.Paid: repaymentTime = 3; break;
        }

        //baseValue *= repaymentTime;

        return Randomise(baseValue * 1000, nicheId) / 1000f;
    }



    long GetFullAudience(MarketProfile profile, int nicheId)
    {
        AudienceSize audienceSize = profile.AudienceSize;

        return Randomise((long)audienceSize, nicheId);
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