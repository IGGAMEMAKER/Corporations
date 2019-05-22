using Entitas;

public class NicheSettings
{
    public IndustryType IndustryType;
    public NicheType NicheType;
    public float BaseProductPrice;
    public long ClientBatch;

    public int IdeaCost;
    public int TechCost;
    public int MarketingCost;
    public int AdsCost;


}

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
        IndustryType industry = IndustryType.Communications;

        AttachNicheToIndustry(NicheType.Messenger, industry);
        AttachNicheToIndustry(NicheType.SocialNetwork, industry);

        SetNicheCosts(NicheType.Messenger,      2, 100, 100, 100, 100, 1000);
        SetNicheCosts(NicheType.SocialNetwork, 10, 100, 100, 100, 100, 1000);
    }

    private void InitializeSearchIndustry()
    {
        IndustryType industry = IndustryType.Search;

        AttachNicheToIndustry(NicheType.SearchEngine, industry);
    }
}
