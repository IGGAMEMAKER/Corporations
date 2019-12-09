using Assets.Utils;
using Entitas;
using System;
using UnityEngine;

public partial class ProductInitializerSystem : IInitializeSystem
{
    readonly GameContext GameContext;

    public ProductInitializerSystem(Contexts contexts)
    {
        GameContext = contexts.game;
    }

    void IInitializeSystem.Initialize()
    {
        Initialize();

        SpawnInvestmentFunds(8, 10000000, 100000000);
        SpawnInvestors(50, 1000000, 10000000);

        AutoFillNonFilledShareholders();
        AutoFillProposals();

        SetSpheresOfInfluence();
    }

    void Initialize()
    {
        // products
        var facebook =      GenerateProductCompany("facebook", NicheType.Com_SocialNetwork);
        var twitter =       GenerateProductCompany("twitter", NicheType.Com_SocialNetwork);
        var vk =            GenerateProductCompany("vk", NicheType.Com_SocialNetwork);

        var tg =            GenerateProductCompany("telegram", NicheType.Com_Messenger);
                            GenerateProductCompany("whatsapp", NicheType.Com_Messenger);
        var fbMessenger =   GenerateProductCompany("facebook messenger", NicheType.Com_Messenger);

        int google =        GenerateProductCompany("Google", NicheType.Tech_SearchEngine).company.Id;
        int yahoo =         GenerateProductCompany("Yahoo", NicheType.Tech_SearchEngine).company.Id;
                            GenerateProductCompany("Yandex", NicheType.Tech_SearchEngine);

        var microsoftOs =   GenerateProductCompany("Windows", NicheType.Tech_OSDesktop);


        // investors
        int investorId1 = GenerateInvestmentFund("Morgan Stanley", 1000000);
        int investorId2 = GenerateInvestmentFund("Goldman Sachs", 2000000);
        int investorId3 = GenerateInvestmentFund("Morgan J.P.", 3000000);

        int alphabet = GenerateHoldingCompany("Alphabet");
        AttachToHolding(alphabet, google);

        AddShareholder(alphabet, investorId1, 100);
        AddShareholder(alphabet, investorId2, 200);

        int googleGroupId = PromoteToGroup(google);

        var facebookInc = GenerateHoldingCompany("Facebook Inc");
        AttachToHolding(facebookInc, facebook);
        AttachToHolding(facebookInc, fbMessenger);

        var microsoft = GenerateHoldingCompany("Microsoft Inc");
        AttachToHolding(microsoft, microsoftOs);

        var mailru = GenerateHoldingCompany("MailRu");
        AttachToHolding(mailru, vk);




        AddShareholder(yahoo, investorId2, 500);
        AddShareholder(yahoo, investorId3, 1500);
        AddShareholder(yahoo, investorId1, 100);
    }
}


public partial class ProductInitializerSystem : IInitializeSystem
{
    void Print(string s)
    {
        Debug.Log("INI: " + s);
    }

    void PlayAs(string companyName) => PlayAs(Companies.GetCompanyByName(GameContext, companyName));
    void PlayAs(int companyId)      => PlayAs(Companies.GetCompany(GameContext, companyId));
    void PlayAs(GameEntity company) => Companies.PlayAs(company, GameContext);


    void AddCash(int companyId, long money) => AddCash(Companies.GetCompany(GameContext, companyId), money);
    void AddCash(GameEntity company, long money)
    {
        Companies.SetResources(company, new Assets.Classes.TeamResource(1000000000));
    }


    void SetSpheresOfInfluence()
    {
        var financial = Companies.GetInvestmentFunds(GameContext);
        //var managing = CompanyUtils.GetGroupCompanies(GameContext);

        foreach (var c in financial)
        {
            Companies.AddFocusIndustry(GetRandomIndustry(), c);

            AutoFillFocusNichesByIndustry(c);
        }

        //foreach (var c in managing)
        //{
        //    CompanyUtils.AddFocusIndustry(GetRandomIndustry(), c);

        //    AutoFillSomeFocusNichesByIndustry(c);
        //    //AutoFillFocusNichesByIndustry(c);
        //}
    }

    void AutoFillFocusNichesByIndustry(GameEntity company)
    {
        var niches = NicheUtils.GetNichesInIndustry(company.companyFocus.Industries[0], GameContext);

        foreach (var n in niches)
            Companies.AddFocusNiche(n.niche.NicheType, company, GameContext);
    }

    void AutoFillSomeFocusNichesByIndustry(GameEntity company)
    {
        var niches = NicheUtils.GetNichesInIndustry(company.companyFocus.Industries[0], GameContext);

        //CompanyUtils.AddFocusNiche(RandomEnum<NicheComponent>.PickRandomItem(niches).NicheType, company);

        foreach (var n in niches)
            Companies.AddFocusNiche(n.niche.NicheType, company, GameContext);
    }

    IndustryType GetRandomIndustry()
    {
        return RandomEnum<IndustryType>.GenerateValue();
    }

    GameEntity GenerateProductCompany(string name, NicheType nicheType)
    {
        var product = Companies.GenerateProductCompany(GameContext, name, nicheType);

        Companies.SetStartCapital(product, GameContext);

        return product;
    }

    int GenerateInvestmentFund(string name, long money)
    {
        return Companies.GenerateInvestmentFund(GameContext, name, money).shareholder.Id;
    }

    int GenerateHoldingCompany(string name)
    {
        return Companies.GenerateHoldingCompany(GameContext, name).company.Id;
    }

    void AttachToHolding(int parent, GameEntity child) => AttachToHolding(parent, child.company.Id);
    void AttachToHolding(int parent, int child)
    {
        Companies.AttachToGroup(GameContext, parent, child);

        var c = Companies.GetCompany(GameContext, child);
        var p = Companies.GetCompany(GameContext, parent);

        if (c.hasProduct)
            Companies.AddFocusNiche(c.product.Niche, p, GameContext);
    }


    void AddShareholder(int companyId, int investorId, int shares)
    {
        //Debug.Log($"Add Shareholder {investorId} with {shares} shares to {companyId}");

        Companies.AddShareholder(GameContext, companyId, investorId, shares);
    }

    int PromoteToGroup(int companyId)
    {
        return Companies.PromoteProductCompanyToGroup(GameContext, companyId);
    }

    long GetRandomFundSize(int min, int max)
    {
        int value = UnityEngine.Random.Range(min, max);

        return Convert.ToInt64(value);
    }

    void SpawnInvestmentFunds(int amountOfFunds, int investmentMin, int investmentMax)
    {
        for (var i = 0; i < amountOfFunds; i++)
            GenerateInvestmentFund(RandomUtils.GenerateInvestmentCompanyName(), GetRandomFundSize(investmentMin, investmentMax));
    }

    void SpawnInvestors(int amountOfInvestors, int investmentMin, int investmentMax)
    {
        for (var i = 0; i < amountOfInvestors; i++)
            InvestmentUtils.GenerateAngel(GameContext);
    }

    int GetRandomInvestmentFund()
    {
        return InvestmentUtils.GetRandomInvestmentFund(GameContext);
    }

    int GetRandomInvestorId()
    {
        return GetRandomInvestmentFund();
    }

    private void AutoFillProposals()
    {
        foreach (var c in Companies.GetNonFinancialCompanies(GameContext))
            Companies.SpawnProposals(GameContext, c.company.Id);
    }

    void AutoFillNonFilledShareholders()
    {
        Companies.AutoFillNonFilledShareholders(GameContext, false);
    }
}
