using Assets.Utils;
using Entitas;

public partial class MarketInitializerSystem : IInitializeSystem
{
    public enum AudienceSize
    {
        LessThanMillion,
        WholeWorld
    }

    public enum NicheDuration
    {
        // duration in months
        LessThanYear = 6,
        Year = 12,
        Mid = 5 * 12,
        EntireGame = 5000
    }

    public enum NicheMaintenance
    {
        Low = 1000, // 
        Mid = 30000,
        High = 500000,
        Humongous = 5000000
    }

    public enum NicheMargin
    {
        Low,
        Mid,
        High,
        ExtremelyHigh
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
        NicheMaintenance MaintenanceCost, NicheMargin nicheMargin,
        NicheChangeSpeed ChangeSpeed, int startDate)
    {
        var price = 1;

        var nicheId = GetNicheId(nicheType);

        var clients = GetBatchSize(audienceSize, nicheId);

        var costs = (int)MaintenanceCost / 1000 * Constants.DEVELOPMENT_PRODUCTION_PROGRAMMER;

        var adCosts = (int)GetAdCost(MaintenanceCost, nicheId);

        SetNicheCosts(nicheType, price, clients, costs, costs, costs, adCosts);
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