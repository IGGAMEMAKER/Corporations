using Assets.Utils;
using Entitas;

public class ProductInitializerSystem : IInitializeSystem
{
    readonly GameContext GameContext;

    public ProductInitializerSystem(Contexts contexts)
    {
        GameContext = contexts.game;
    }

    int GenerateProduct (string name, NicheType nicheType)
    {
        return CompanyUtils.GenerateProduct(GameContext, name, nicheType);
    }

    int GenerateInvestmentFund(string name, long money)
    {
        return CompanyUtils.GenerateInvestmentFund(GameContext, name, money);
    }

    int GenerateHoldingCompany(string name)
    {
        return CompanyUtils.GenerateHoldingCompany(GameContext, name);
    }

    void AttachToHolding(int parent, int child)
    {
        CompanyUtils.AttachToHolding(GameContext, parent, child);
    }

    void AddShareholder(int companyId, int investorId, int shares)
    {
        CompanyUtils.AddShareholder(GameContext, companyId, investorId, shares);
    }

    void SetPlayerControlledCompany(int companyId)
    {
        CompanyUtils.SetPlayerControlledCompany(GameContext, companyId);
    }

    void IInitializeSystem.Initialize()
    {
        // products
        GenerateProduct("facebook", NicheType.SocialNetwork);
        GenerateProduct("mySpace", NicheType.SocialNetwork);
        GenerateProduct("twitter", NicheType.SocialNetwork);
        GenerateProduct("vk", NicheType.SocialNetwork);

        GenerateProduct("telegram", NicheType.Messenger);
        GenerateProduct("whatsapp", NicheType.Messenger);
        GenerateProduct("snapchat", NicheType.Messenger);

        int google = GenerateProduct("Google", NicheType.SearchEngine);
        GenerateProduct("Yahoo", NicheType.SearchEngine);
        GenerateProduct("Bing", NicheType.SearchEngine);
        GenerateProduct("Yandex", NicheType.SearchEngine);

        GenerateProduct("Microsoft", NicheType.OSCommonPurpose);

        SetPlayerControlledCompany(google);

        // investors
        int investorId = GenerateInvestmentFund("Morgan Stanley", 1000000);
        int investorId2 = GenerateInvestmentFund("Goldman Sachs", 2000000);

        int alphabet = GenerateHoldingCompany("Alphabet");
        AttachToHolding(alphabet, google);

        AddShareholder(alphabet, investorId, 100);
        AddShareholder(alphabet, investorId2, 200);
    }
}
