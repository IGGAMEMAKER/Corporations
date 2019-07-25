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
        LessThanYear = 1,
        Low = 2,
        Mid = 3,
        EntireGame = 4
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
            case AudienceSize.LessThanMillion: return 100;

            case AudienceSize.WholeWorld: return 10000;
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

    void InitializeCommunicationsIndustry()
    {
        var niches = new NicheType[] {
            NicheType.Messenger,
            NicheType.SocialNetwork,
        };
        AttachNichesToIndustry(IndustryType.Communications, niches);

        SetNicheCostsAutomatitcallty(NicheType.Messenger,
            NicheDuration.EntireGame,
            AudienceSize.WholeWorld,
            NicheMaintenance.High,
            NicheMargin.Mid,
            NicheChangeSpeed.Quarter, 
           0);

        SetNicheCostsAutomatitcallty(NicheType.SocialNetwork,
            NicheDuration.EntireGame,
            AudienceSize.WholeWorld,
            NicheMaintenance.High,
            NicheMargin.High,
            NicheChangeSpeed.Year, 
           0);

        //SetNicheCosts(NicheType.Messenger,      2, 75, 100, 75, 100, 1000);
        //SetNicheCosts(NicheType.SocialNetwork, 10, 100, 100, 100, 100, 1000);
    }

    private void InitializeFundamentalIndustry()
    {
        var niches = new NicheType[] {
            NicheType.SearchEngine,
            NicheType.OSDesktop,
            NicheType.CloudComputing,
        };
        AttachNichesToIndustry(IndustryType.Fundamental, niches);

        SetNicheCosts(NicheType.SearchEngine,   10, 100, 1000, 1000, 1000, 2000);
        SetNicheCosts(NicheType.CloudComputing, 10, 100, 1000, 1000, 1000, 2000);
        SetNicheCosts(NicheType.OSDesktop,      1000, 500, 1000, 1000, 1000, 5000);
    }
}
