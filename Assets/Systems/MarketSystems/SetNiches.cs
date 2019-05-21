using Entitas;

public partial class MarketInitializerSystem : IInitializeSystem
{
    void InitializeCloudsIndustry()
    {
        IndustryType industry = IndustryType.CloudComputing;

        AttachNicheToIndustry(NicheType.CloudComputing, industry);
        SetNicheCosts(NicheType.CloudComputing, 1, 100, 100, 50, 35, 1000);
    }

    void InitializeOSIndustry()
    {
        IndustryType industry = IndustryType.OS;

        AttachNicheToIndustry(NicheType.OSSciencePurpose, industry);
        AttachNicheToIndustry(NicheType.OSCommonPurpose, industry);
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
