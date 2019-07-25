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
        Low, // 
        Mid,
        High,
        Humongous
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
            NicheMargin.High,
            NicheChangeSpeed.Quarter, 
           0);

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
