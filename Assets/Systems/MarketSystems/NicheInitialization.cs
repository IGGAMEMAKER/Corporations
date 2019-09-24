using System;
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
    FiveYears = 60,
    Decade = 120,
    EntireGame = 12 * 50
}

public enum NicheChangeSpeed
{
    Month = 1,
    Quarter = 3,
    Year = 12,
    ThreeYears = 36
}

// everyone: operation systems, browsers, social networks (messaging + content)
// small niche project : 50K

public partial class MarketInitializerSystem : IInitializeSystem
{
    public enum AudienceSize
    {
        ForBigEnterprise =      2000, // 2K
        ForSmallEnterprise =    50000, // 50K
        Million         =       1000000, // 1M
        HundredMillion =        100000000, // 100M // usefull util AdBlock
        Billion =               1000000000 // 1-2B
    }

    public enum PriceCategory
    {
        // dollars per user per year
        CheapMass = 1,
        FreeMass = 4,
        CheapSubscription = 120, // Subscription model: 10$/month
        ExpensiveSubscription = 500, // Subscription model: 10$/month
        Enterprise = 50000, 
    }

    public enum NicheAdMaintenance
    {
        Low =       2000,
        Mid =       7000,
        High =      25000,
        Humongous = 85000
    }

    public enum NicheTechMaintenance
    {
        Low = 3,
        Mid = 7,
        High = 15,
        Humongous = 25
    }

    public enum NicheMarketingMaintenance
    {
        Low = 1,
        Mid = 3,
        High = 7,
        Humongous = 10
    }

    public enum MarketAttribute
    {
        RepaymentMonth,
        RepaymentHalfYear,
        RepaymentYear,

        AudienceIncreased,
        AudienceNiche,
    }

    bool GetAttribute(MarketAttribute[] marketAttributes, MarketAttribute attribute)
    {
        if (marketAttributes == null) return false;

        return marketAttributes.Contains(attribute);
    }

    GameEntity SetNichesAutomatically(NicheType nicheType,
        NicheDuration NicheDuration, AudienceSize audienceSize, PriceCategory priceCategory,
        NicheChangeSpeed ChangeSpeed,
        //ProductPositioning[] productPositionings,
        int startDate,
        MarketAttribute[] marketAttributes = null)
    {
        var nicheId = GetNicheId(nicheType);

        var price = GetProductPrice(priceCategory, nicheId, nicheType, marketAttributes);

        var audience = GetFullAudience(audienceSize, nicheId, marketAttributes);
        var clients = GetBatchSize(audience, NicheDuration);


        var techCost = GetTechCost(ChangeSpeed, nicheId) * Constants.DEVELOPMENT_PRODUCTION_PROGRAMMER;
        var ideaCost = GetTechCost(ChangeSpeed, nicheId + 1);
        var marketingCost = GetMarketingCost(ChangeSpeed, nicheId) * Constants.DEVELOPMENT_PRODUCTION_MARKETER;

        var adCosts = GetAdCost(clients, priceCategory, nicheId);

        var n = SetNicheCosts(nicheType, price, clients, techCost, ideaCost, marketingCost, adCosts);



        // positionings
        var positionings = new Dictionary<int, ProductPositioning>(); // n.nicheSegments.Positionings;
        //var clientsContainer = n.nicheClientsContainer.Clients;
        var clientsContainer = new Dictionary<int, long>();

        //for (var i = 0; i < productPositionings.Length; i++)
        //{
        //    positionings[i] = productPositionings[i];
        //    clientsContainer[i] = 0;
        //}



        positionings[0] = new ProductPositioning
        {
            isCompetitive = false,
            marketShare = 100,
            name = EnumUtils.GetSingleFormattedNicheName(nicheType)
        };
        clientsContainer[0] = audience;
        //if (productPositionings.Length == 0)
        //{
        //}


        n.ReplaceNicheSegments(positionings);
        n.ReplaceNicheClientsContainer(clientsContainer);
        n.ReplaceNicheLifecycle(startDate, n.nicheLifecycle.Growth, n.nicheLifecycle.Period, ChangeSpeed);
        //n.ReplaceNicheLifecycle(GetYear(1990), n.nicheLifecycle.Growth, n.nicheLifecycle.Period, ChangeSpeed);



        return n;
    }




    //private int GetMarketingCost(NicheMarketingMaintenance techMaintenance, int nicheId)
    private int GetMarketingCost(NicheChangeSpeed techMaintenance, int nicheId)
    {
        var baseCost = (int)techMaintenance;

        return (int)Randomise(baseCost, nicheId);
    }

    //private int GetTechCost(NicheTechMaintenance techMaintenance, int nicheId)
    private int GetTechCost(NicheChangeSpeed techMaintenance, int nicheId)
    {
        var baseCost = (int)techMaintenance;

        return (int)Randomise(baseCost, nicheId);
    }

    float GetProductPrice(PriceCategory priceCategory, int nicheId, NicheType nicheType, MarketAttribute[] marketAttributes)
    {
        var baseCost = (int)priceCategory;

        float value = Randomise(baseCost * 1000, nicheId) / 12f / 1000f;

        if (GetAttribute(marketAttributes, MarketAttribute.RepaymentMonth))
            value *= 5;
        if (GetAttribute(marketAttributes, MarketAttribute.RepaymentHalfYear))
            value *= 2;
        if (GetAttribute(marketAttributes, MarketAttribute.RepaymentYear))
            value /= 2;

        return value;
    }

    long GetFullAudience(AudienceSize audienceSize, int nicheId, MarketAttribute[] marketAttributes)
    {
        var audience = Randomise((long)audienceSize, nicheId);

        if (GetAttribute(marketAttributes, MarketAttribute.AudienceIncreased))
            audience *= 5;
        if (GetAttribute(marketAttributes, MarketAttribute.AudienceNiche))
            audience /= 2;

        return audience;
    }

    long GetBatchSize (long audience, NicheDuration nicheDuration)
    {
        var baseValue = GetClientBatchBase(audience, (int)nicheDuration);

        return baseValue;
    }


    long GetClientBatchBase(long size, int NicheDurationInMonths)
    {
        return size / NicheDurationInMonths / 9;
    }

    //int GetAdCost (NicheAdMaintenance nicheMaintenance, int nicheId)
    int GetAdCost (long clientBatch, PriceCategory priceCategory, int nicheId)
    {
        var baseValue = clientBatch * (int)priceCategory / 2;

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