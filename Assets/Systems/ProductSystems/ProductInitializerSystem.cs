using Assets.Utils;
using Entitas;
using System;
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

        SpawnInvestmentFunds(5, 10000000, 100000000);
        SpawnInvestors(10, 1000000, 10000000);

        AutoFillNonFilledShareholders();
        AutoFillProposals();
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
        //Debug.Log($"Add Shareholder {investorId} with {shares} shares to {companyId}");

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

    long GetRandomFundSize (int min, int max)
    {
        int value = UnityEngine.Random.Range(min, max);

        return Convert.ToInt64(value);
    }

    void SpawnInvestmentFunds(int amountOfFunds, int investmentMin, int investmentMax)
    {
        for (var i = 0; i < amountOfFunds; i++)
            GenerateInvestmentFund(RandomUtils.GenerateInvestmentCompanyName(), GetRandomFundSize(investmentMin, investmentMax));
    }

    void GenerateEarlyInvestor()
    {

    }

    void SpawnInvestors(int amountOfInvestors, int investmentMin, int investmentMax)
    {
        for (var i = 0; i < amountOfInvestors; i++)
        {
            InvestmentUtils.GenerateAngel(GameContext);
        }

    }

    int GetRandomInvestmentFund()
    {
        return CompanyUtils.GetRandomInvestmentFund(GameContext);
    }

    int GetRandomInvestorId()
    {
        return GetRandomInvestmentFund();
    }

    private void AutoFillProposals()
    {
        foreach (var c in CompanyUtils.GetNonFinancialCompanies(GameContext))
            CompanyUtils.SpawnProposals(GameContext, c.company.Id);
    }

    public static void SetRandomCEO()
    {

    }

    void AutoFillNonFilledShareholders()
    {
        foreach (var c in CompanyUtils.GetNonFinancialCompaniesWithZeroShareholders(GameContext))
        {
            for (var i = 0; i < UnityEngine.Random.Range(1, 5); i++)
            {
                int investorId = GetRandomInvestmentFund();

                AddShareholder(c.company.Id, investorId, 100);
            }
        }
    }

    void Initialize()
    {
        // products
        GenerateProductCompany("facebook", NicheType.SocialNetwork);
        GenerateProductCompany("twitter", NicheType.SocialNetwork);
        GenerateProductCompany("vk", NicheType.SocialNetwork);

        GenerateProductCompany("telegram", NicheType.Messenger);
        GenerateProductCompany("whatsapp", NicheType.Messenger);

        int google = GenerateProductCompany("Google", NicheType.SearchEngine);
        int yahoo = GenerateProductCompany("Yahoo", NicheType.SearchEngine);
        GenerateProductCompany("Yandex", NicheType.SearchEngine);

        GenerateProductCompany("Microsoft", NicheType.OSCommonPurpose);

        SetPlayerControlledCompany(google);

        // investors
        int investorId = GenerateInvestmentFund("Morgan Stanley", 1000000);
        int investorId2 = GenerateInvestmentFund("Goldman Sachs", 2000000);
        int investorId3 = GenerateInvestmentFund("Morgan J.P.", 3000000);

        int alphabet = GenerateHoldingCompany("Alphabet");
        AttachToHolding(alphabet, google);
        AttachToHolding(alphabet, yahoo);

        AddShareholder(alphabet, investorId, 100);
        AddShareholder(alphabet, investorId2, 200);

        int googleGroupId = PromoteToGroup(google);
        SetPlayerControlledCompany(alphabet);

        AddShareholder(yahoo, investorId2, 500);
        AddShareholder(yahoo, investorId3, 1500);
        AddShareholder(yahoo, investorId, 100);



        var googleProduct = CompanyUtils.GetCompanyById(GameContext, google);
        var yandexProduct = CompanyUtils.GetCompanyById(GameContext, yahoo);

        yandexProduct.finance.price = Pricing.Medium;
    }
}
