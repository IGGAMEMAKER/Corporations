using Entitas;

public struct NicheSettings
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
    void InitializeOSIndustry()
    {
        IndustryType industry = IndustryType.OS;

        AttachNicheToIndustry(NicheType.OSDesktop, industry);
    }

    void InitializeCommunicationsIndustry()
    {
        IndustryType industry = IndustryType.Communications;

        AttachNicheToIndustry(NicheType.Messenger, industry);
        AttachNicheToIndustry(NicheType.SocialNetwork, industry);
    }

    private void InitializeSearchIndustry()
    {
        IndustryType industry = IndustryType.Search;

        AttachNicheToIndustry(NicheType.SearchEngine, industry);
    }
}
