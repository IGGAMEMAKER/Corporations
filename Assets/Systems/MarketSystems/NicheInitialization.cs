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

    public enum NicheChangeSpeed
    {
        Month,
        Quarter,
        Year,
        ThreeYears
    }

    void SetNicheCostsAutomatitcallty(NicheType nicheType,
        NicheDuration PeriodDuration, AudienceSize audienceSize,
        NicheMaintenance MaintenanceCost, PriceCategory priceCategory,
        NicheChangeSpeed ChangeSpeed,
        ProductPositioning[] productPositionings,
        int startDate)
    {
        var nicheId = GetNicheId(nicheType);

        var price = GetProductPrice(priceCategory, nicheId, nicheType);

        var clients = GetBatchSize(audienceSize, nicheId);

        var costs = (int)MaintenanceCost / 1000 * Constants.DEVELOPMENT_PRODUCTION_PROGRAMMER;

        var adCosts = (int)GetAdCost(MaintenanceCost, nicheId);

        var n = SetNicheCosts(nicheType, price, clients, costs, costs, costs, adCosts);

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

    long GetAdCostBase(NicheMaintenance nicheMaintenance)
    {
        return (long)nicheMaintenance;
    }

    long GetAdCost (NicheMaintenance nicheMaintenance, int nicheId)
    {
        var baseValue = GetAdCostBase(nicheMaintenance);

        return Randomise(baseValue, nicheId);
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