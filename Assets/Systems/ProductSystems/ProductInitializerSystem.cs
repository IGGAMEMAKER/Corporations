using Assets.Utils;
using Entitas;
using UnityEngine;

public class ProductInitializerSystem : IInitializeSystem
{
    readonly GameContext GameContext;

    public ProductInitializerSystem(Contexts contexts)
    {
        GameContext = contexts.game;
    }

    void IInitializeSystem.Initialize()
    {
        Initialize();
    }

    int GenerateProductCompany(string name, NicheType nicheType)
    {
        return CompanyUtils.GenerateProductCompany(GameContext, name, nicheType).company.Id;
    }

    int GenerateInvestmentFund(string name, long money)
    {
        return CompanyUtils.GenerateInvestmentFund(GameContext, name, money).shareholder.Id;
    }

    int GenerateHoldingCompany(string name)
    {
        return CompanyUtils.GenerateHoldingCompany(GameContext, name).company.Id;
    }

    void AttachToHolding(int parent, int child)
    {
        CompanyUtils.AttachToGroup(GameContext, parent, child);
    }

    void AddShareholder(int companyId, int investorId, int shares)
    {
        Debug.Log($"Add Shareholder {investorId} with {shares} shares to {companyId}");

        CompanyUtils.AddShareholder(GameContext, companyId, investorId, shares);
    }

    void SetPlayerControlledCompany(int companyId)
    {
        CompanyUtils.SetPlayerControlledCompany(GameContext, companyId);
    }

    int PromoteToGroup(int companyId)
    {
        return CompanyUtils.PromoteProductCompanyToGroup(GameContext, companyId);
    }

    void Initialize()
    {
        // products
        GenerateProductCompany("facebook", NicheType.SocialNetwork);
        GenerateProductCompany("mySpace", NicheType.SocialNetwork);
        GenerateProductCompany("twitter", NicheType.SocialNetwork);
        GenerateProductCompany("vk", NicheType.SocialNetwork);

        GenerateProductCompany("telegram", NicheType.Messenger);
        GenerateProductCompany("whatsapp", NicheType.Messenger);
        GenerateProductCompany("snapchat", NicheType.Messenger);

        int google = GenerateProductCompany("Google", NicheType.SearchEngine);
        int yahoo = GenerateProductCompany("Yahoo", NicheType.SearchEngine);
        GenerateProductCompany("Bing", NicheType.SearchEngine);
        GenerateProductCompany("Yandex", NicheType.SearchEngine);

        GenerateProductCompany("Microsoft", NicheType.OSCommonPurpose);

        SetPlayerControlledCompany(google);

        // investors
        int investorId = GenerateInvestmentFund("Morgan Stanley", 1000000);
        int investorId2 = GenerateInvestmentFund("Goldman Sachs", 2000000);
        int investorId3 = GenerateInvestmentFund("Morgan J.P.", 3000000);

        int alphabet = GenerateHoldingCompany("Alphabet");
        AttachToHolding(alphabet, google);

        AddShareholder(alphabet, investorId, 100);
        AddShareholder(alphabet, investorId2, 200);

        int googleGroupId = PromoteToGroup(google);
        SetPlayerControlledCompany(googleGroupId);

        AddShareholder(yahoo, investorId2, 500);
        AddShareholder(yahoo, investorId3, 1500);
        AddShareholder(yahoo, investorId, 100);
    }
}
