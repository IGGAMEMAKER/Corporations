using System;
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


public partial class MarketInitializerSystem : IInitializeSystem
{
    public enum AudienceSize
    {
        LessThanMillion,
        MidSizedProduct,
        WholeWorld
    }

    public enum PriceCategory
    {
        // dollars per user per year
        CheapMass = 1,
        FreeMass = 4,
        Mid = 12,
        High = 20,
        CheapSubscription = 100, // Subscription model: 10$/month
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

    GameEntity SetNichesAutomatically(NicheType nicheType,
        NicheDuration PeriodDuration, AudienceSize audienceSize, PriceCategory priceCategory,
        NicheChangeSpeed ChangeSpeed,
        NicheAdMaintenance MaintenanceCost, NicheTechMaintenance techMaintenance, NicheMarketingMaintenance marketingMaintenance,
        ProductPositioning[] productPositionings,
        int startDate)
    {
        var nicheId = GetNicheId(nicheType);

        var price = GetProductPrice(priceCategory, nicheId, nicheType);

        var clients = GetBatchSize(audienceSize, nicheId);


        var techCost = GetTechCost(techMaintenance, nicheId) * Constants.DEVELOPMENT_PRODUCTION_PROGRAMMER;
        var ideaCost = GetTechCost(techMaintenance, nicheId + 1);
        var marketingCost = GetMarketingCost(marketingMaintenance, nicheId) * Constants.DEVELOPMENT_PRODUCTION_MARKETER;

        var adCosts = GetAdCost(MaintenanceCost, nicheId);

        var n = SetNicheCosts(nicheType, price, clients, techCost, ideaCost, marketingCost, adCosts);



        // positionings
        var positionings = n.nicheSegments.Positionings;
        var clientsContainer = n.nicheClientsContainer.Clients;

        for (var i = 0; i < productPositionings.Length; i++)
        {
            positionings[i] = productPositionings[i];
            clientsContainer[i] = 0;
        }

        if (productPositionings.Length == 0)
        {
            positionings[0] = new ProductPositioning
            {
                isCompetitive = false,
                marketShare = 100,
                name = EnumUtils.GetSingleFormattedNicheName(nicheType)
            };
            clientsContainer[0] = 0;
        }


        n.ReplaceNicheSegments(positionings);
        n.ReplaceNicheClientsContainer(clientsContainer);
        n.ReplaceNicheLifecycle(startDate, n.nicheLifecycle.Growth, n.nicheLifecycle.Period, ChangeSpeed);
        //n.ReplaceNicheLifecycle(GetYear(1990), n.nicheLifecycle.Growth, n.nicheLifecycle.Period, ChangeSpeed);

        return n;
    }




    private int GetMarketingCost(NicheMarketingMaintenance maintenance, int nicheId)
    {
        var baseCost = (int)maintenance;

        return (int)Randomise(baseCost, nicheId);
    }

    private int GetTechCost(NicheTechMaintenance techMaintenance, int nicheId)
    {
        var baseCost = (int)techMaintenance;

        return (int)Randomise(baseCost, nicheId);
    }

    float GetProductPrice(PriceCategory priceCategory, int nicheId, NicheType nicheType)
    {
        var baseCost = (int)priceCategory;

        float value = Randomise(baseCost * 1000, nicheId) / 12f / 1000f;

        return value;
    }

    long GetBatchSize (AudienceSize audienceSize, int nicheId)
    {
        var baseValue = GetClientBatchBase(audienceSize);

        return Randomise(baseValue, nicheId);
    }

    long GetClientBatchBase(AudienceSize audienceSize)
    {

        switch (audienceSize)
        {
            case AudienceSize.LessThanMillion: return 70;

            case AudienceSize.MidSizedProduct: return 185;

            case AudienceSize.WholeWorld: return 500;
        }

        return 0;
    }

    int GetAdCost (NicheAdMaintenance nicheMaintenance, int nicheId)
    {
        var baseValue = (long)nicheMaintenance;

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