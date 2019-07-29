using System;
using Assets.Utils;
using Entitas;
using UnityEngine;

public enum NicheDuration
{
    // duration in months
    Year = 12,
    FiveYears = 60,
    Decade = 120,
    EntireGame = 5000
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
        Premium = 100, // Subscription model: 10$/month
    }

    public enum NicheMaintenance
    {
        Low = 1000,
        Mid = 30000,
        High = 500000,
        Humongous = 5000000
    }

    public enum NicheTechMaintenance
    {
        Low = 1,
        Mid = 5,
        High = 50,
        Humongous = 250
    }

    public enum NicheMarketingMaintenance
    {
        Low = 1,
        Mid = 5,
        High = 50,
        Humongous = 250
    }

    public enum NicheChangeSpeed
    {
        Month,
        Quarter,
        Year,
        ThreeYears
    }

    void SetNichesAutomatically(NicheType nicheType,
        NicheDuration PeriodDuration, AudienceSize audienceSize, PriceCategory priceCategory,
        NicheChangeSpeed ChangeSpeed,
        NicheMaintenance MaintenanceCost, NicheTechMaintenance techMaintenance, NicheMarketingMaintenance marketingMaintenance,
        ProductPositioning[] productPositionings,
        int startDate)
    {
        var nicheId = GetNicheId(nicheType);

        var price = GetProductPrice(priceCategory, nicheId, nicheType);

        var clients = GetBatchSize(audienceSize, nicheId);


        var techCost = GetTechCost(techMaintenance, nicheId);
        var ideaCost = GetTechCost(techMaintenance, nicheId + 1);
        var marketingCost = GetMarketingCost(marketingMaintenance, nicheId);

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
                name = "Basic " + nicheType.ToString()
            };
            clientsContainer[0] = 0;
        }

        n.ReplaceNicheSegments(positionings);
        n.ReplaceNicheClientsContainer(clientsContainer);
        n.ReplaceNicheLifecycle(startDate, n.nicheLifecycle.Growth, n.nicheLifecycle.Period);
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

    int GetAdCost (NicheMaintenance nicheMaintenance, int nicheId)
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