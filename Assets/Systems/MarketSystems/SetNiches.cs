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

    private void InitializeSearchIndustry()
    {
        AttachNichesToIndustry(IndustryType.Search, new NicheType[] { NicheType.SearchEngine });

        SetNicheCosts(NicheType.SearchEngine, 10, 100, 1000, 1000, 1000, 20000);
    }
}
