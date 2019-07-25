using Assets.Utils;
using Entitas;

public partial class MarketInitializerSystem : IInitializeSystem
{
    void InitializeCommunicationsIndustry()
    {
        var niches = new NicheType[] {
            NicheType.Messenger,
            NicheType.SocialNetwork,
        };
        AttachNichesToIndustry(IndustryType.Communications, niches);

        SetNicheCostsAutomatitcallty(NicheType.Messenger,
            NicheDuration.EntireGame, AudienceSize.WholeWorld,
            NicheMaintenance.High, NicheMargin.Mid, NicheChangeSpeed.Quarter,
           0);

        SetNicheCostsAutomatitcallty(NicheType.SocialNetwork,
            NicheDuration.EntireGame, AudienceSize.WholeWorld,
            NicheMaintenance.High, NicheMargin.High, NicheChangeSpeed.Year,
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

        SetNicheCostsAutomatitcallty(NicheType.CloudComputing,
            NicheDuration.EntireGame, AudienceSize.MidSizedProduct,
            NicheMaintenance.Humongous, NicheMargin.High, NicheChangeSpeed.Year,
            0);

        SetNicheCostsAutomatitcallty(NicheType.SearchEngine,
            NicheDuration.EntireGame, AudienceSize.WholeWorld,
            NicheMaintenance.Humongous, NicheMargin.ExtremelyHigh, NicheChangeSpeed.ThreeYears,
            0);

        SetNicheCostsAutomatitcallty(NicheType.OSDesktop,
            NicheDuration.EntireGame, AudienceSize.WholeWorld,
            NicheMaintenance.Humongous, NicheMargin.ExtremelyHigh, NicheChangeSpeed.ThreeYears,
            0);

        //SetNicheCosts(NicheType.SearchEngine, 10, 100, 1000, 1000, 1000, 2000);
        //SetNicheCosts(NicheType.CloudComputing, 10, 100, 1000, 1000, 1000, 2000);
        //SetNicheCosts(NicheType.OSDesktop, 1000, 500, 1000, 1000, 1000, 5000);
    }
}