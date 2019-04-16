using Assets.Utils;
using Entitas;
using System;
using System.Text;
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

        SpawnInvestmentFund(5, 10000000, 100000000);
        AutoFillNonFilledShareholders();
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

    public string RandomString(int size, bool lowerCase)
    {
        StringBuilder builder = new StringBuilder();
        System.Random random = new System.Random();

        char ch;
        for (int i = 0; i < size; i++)
        {
            ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
            builder.Append(ch);
        }
        if (lowerCase)
            return builder.ToString().ToLower();
        return builder.ToString();
    }

    string GenerateRandomCompanyName()
    {
        string[] names = new string[] { "Investments", "Capitals", "Funds", "and partners" };

        int index = UnityEngine.Random.Range(0, names.Length);

        return RandomString(10, false) + " " + names[index];
    }

    void SpawnInvestmentFund(int amountOfFunds, long investmentMin, long investmentMax)
    {
        for (var i = 0; i < amountOfFunds; i++)
            GenerateInvestmentFund(GenerateRandomCompanyName(), 10000000);
    }

    int GetRandomInvestmentFund()
    {
        var funds = CompanyUtils.GetFinancialCompanies(GameContext);

        var index = UnityEngine.Random.Range(0, funds.Length);

        return funds[index].shareholder.Id;
        return CompanyUtils.GetCompanyByName(GameContext, "Morgan J.P.").shareholder.Id;
    }

    void AutoFillNonFilledShareholders()
    {
        var companiesWithZeroShareholders = CompanyUtils.GetNonFinancialCompaniesWithZeroShareholders(GameContext);

        foreach (var c in companiesWithZeroShareholders)
        {
            int investorId = GetRandomInvestmentFund();
            // Set CEO
            AddShareholder(c.company.Id, investorId, 100);
        }
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
