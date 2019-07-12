using Entitas;

public partial class MarketInitializerSystem : IInitializeSystem
{
    //void InitializeOSIndustry()
    //{
    //    IndustryType industry = IndustryType.OS;

    //    AttachNicheToIndustry(NicheType.OSDesktop, industry);
    //    SetNicheCosts(NicheType.OSDesktop, 1000, 500, 1000, 1000, 1000, 50000);
    //}

    void InitializeCommunicationsIndustry()
    {
        AttachNichesToIndustry(IndustryType.Communications, new NicheType[] { NicheType.Messenger, NicheType.SocialNetwork });

        SetNicheCosts(NicheType.Messenger,      2, 75, 100, 75, 100, 1000);
        SetNicheCosts(NicheType.SocialNetwork, 10, 100, 100, 100, 100, 1000);
    }

    private void InitializeFundamentalIndustry()
    {
        AttachNichesToIndustry(IndustryType.Fundamental, new NicheType[] { NicheType.SearchEngine, NicheType.OSDesktop, NicheType.CloudComputing });

        SetNicheCosts(NicheType.SearchEngine, 10, 100, 1000, 1000, 1000, 2000);
        SetNicheCosts(NicheType.CloudComputing, 10, 100, 1000, 1000, 1000, 2000);
        SetNicheCosts(NicheType.OSDesktop, 1000, 500, 1000, 1000, 1000, 5000);
    }
}
