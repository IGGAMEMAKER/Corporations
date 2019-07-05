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
        GenerateProductCompany("facebook", NicheType.SocialNetwork);
        var twitter = GenerateProductCompany("twitter", NicheType.SocialNetwork);
        var vk = GenerateProductCompany("vk", NicheType.SocialNetwork);

        var tg = GenerateProductCompany("telegram", NicheType.Messenger);
        GenerateProductCompany("whatsapp", NicheType.Messenger);
        GenerateProductCompany("facebook messenger", NicheType.Messenger);

        int google = GenerateProductCompany("Google", NicheType.SearchEngine).company.Id;
        int yahoo = GenerateProductCompany("Yahoo", NicheType.SearchEngine).company.Id;
        GenerateProductCompany("Yandex", NicheType.SearchEngine);

        GenerateProductCompany("Microsoft", NicheType.OSDesktop);


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

        //PlayAs(tg.company.Id);
        //PlayAs(google);
        //PlayAs(alphabet);
        var mailru = GenerateHoldingCompany("MailRu");
        AttachToHolding(mailru, vk.company.Id);
        AttachToHolding(mailru, twitter.company.Id);

        //PlayAs(vk.company.Id);
        PlayAs(mailru);
        vk.ReplaceCompanyResource(new Assets.Classes.TeamResource(1000000000));


        AddShareholder(yahoo, investorId2, 500);
        AddShareholder(yahoo, investorId3, 1500);
        AddShareholder(yahoo, investorId, 100);
    }
}


public partial class ProductInitializerSystem : IInitializeSystem
{
    void Print(string s)
    {
        Debug.Log("INI: " + s);
    }

    void PlayAs(int companyId)
    {
        var company = CompanyUtils.GetCompanyById(GameContext, companyId);

        var human = HumanUtils.GetHumanById(GameContext, company.cEO.HumanId);

        SetPlayerControlledCompany(companyId);

        human.isPlayer = true;
    }

    void SetSpheresOfInfluence()
    {
        var financial = CompanyUtils.GetFinancialCompanies(GameContext);
        var managing = CompanyUtils.GetGroupCompanies(GameContext);

        foreach (var c in financial)
        {
            //Print(c.)

            CompanyUtils.AddFocusIndustry(GetRandomIndustry(), c);

            AutoFillFocusNichesByIndustry(c);
        }

        foreach (var c in managing)
        {
            CompanyUtils.AddFocusIndustry(GetRandomIndustry(), c);

            AutoFillSomeFocusNichesByIndustry(c);
            //AutoFillFocusNichesByIndustry(c);
        }
    }

    void AutoFillFocusNichesByIndustry(GameEntity company)
    {
        var niches = NicheUtils.GetNichesInIndustry(company.companyFocus.Industries[0], GameContext);

        foreach (var n in niches)
            CompanyUtils.AddFocusNiche(n.niche.NicheType, company);
    }

    void AutoFillSomeFocusNichesByIndustry(GameEntity company)
    {
        var niches = NicheUtils.GetNichesInIndustry(company.companyFocus.Industries[0], GameContext);

        //CompanyUtils.AddFocusNiche(RandomEnum<NicheComponent>.PickRandomItem(niches).NicheType, company);

        foreach (var n in niches)
            CompanyUtils.AddFocusNiche(n.niche.NicheType, company);
    }

    IndustryType GetRandomIndustry()
    {
        return RandomEnum<IndustryType>.GenerateValue();
    }

    GameEntity GenerateProductCompany(string name, NicheType nicheType)
    {
        return CompanyUtils.GenerateProductCompany(GameContext, name, nicheType);
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
            var founder = c.cEO.HumanId;
            var shareholder = HumanUtils.GetHumanById(GameContext, founder);

            InvestmentUtils.BecomeInvestor(GameContext, shareholder, 100000);

            AddShareholder(c.company.Id, shareholder.shareholder.Id, 500);

            for (var i = 0; i < UnityEngine.Random.Range(1, 5); i++)
            {
                int investorId = GetRandomInvestmentFund();

                AddShareholder(c.company.Id, investorId, 100);
            }
        }
    }
}
