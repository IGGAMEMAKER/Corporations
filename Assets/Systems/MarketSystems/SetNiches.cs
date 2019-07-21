using Entitas;

public partial class MarketInitializerSystem : IInitializeSystem
{
    public enum IncomePerUser
    {
        Low,
        Mid,
        High,
        Premium
    };

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

    void SetNicheCosts(NicheType nicheType,
        NicheDuration PeriodDuration, AudienceSize audienceSize, IncomePerUser incomePerUser,
        int MaintenanceCost, int ChangeSpeed)
    {

    }

    void InitializeCommunicationsIndustry()
    {
        var niches = new NicheType[] {
            NicheType.Messenger,
            NicheType.SocialNetwork,
        };
        AttachNichesToIndustry(IndustryType.Communications, niches);

        SetNicheCosts(NicheType.Messenger,      2, 75, 100, 75, 100, 1000);
        SetNicheCosts(NicheType.SocialNetwork, 10, 100, 100, 100, 100, 1000);
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
