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

        PlayerSettings();
    }

    void PlayerSettings()
    {
        //ScreenUtils.Navigate(GameContext, ScreenMode.StartCampaignScreen);
        return;
        //PlayAs(vk);
        //PlayAs(alphabet);
        //AddCash(mailru, 100000000);

        //PlayAs(microsoft);

        var playerCompany = CompanyUtils.GetPlayerCompany(GameContext);

        var daughter = CompanyUtils.GetDaughterCompanies(GameContext, playerCompany.company.Id)[0];

        //ScreenUtils.Navigate(GameContext, ScreenMode.NicheScreen, Constants.MENU_SELECTED_NICHE, daughter.product.Niche);
        ScreenUtils.Navigate(GameContext, ScreenMode.NicheScreen, Constants.MENU_SELECTED_NICHE, daughter.product.Niche);
    }

    void Initialize()
    {
        // products
        var facebook = GenerateProductCompany("facebook", NicheType.SocialNetwork);
        var twitter = GenerateProductCompany("twitter", NicheType.SocialNetwork);
        var vk = GenerateProductCompany("vk", NicheType.SocialNetwork);

        var tg = GenerateProductCompany("telegram", NicheType.Messenger);
        GenerateProductCompany("whatsapp", NicheType.Messenger);
        var fbMessenger = GenerateProductCompany("facebook messenger", NicheType.Messenger);

        int google = GenerateProductCompany("Google", NicheType.SearchEngine).company.Id;
        int yahoo = GenerateProductCompany("Yahoo", NicheType.SearchEngine).company.Id;
        GenerateProductCompany("Yandex", NicheType.SearchEngine);

        var microsoftOs = GenerateProductCompany("Windows", NicheType.OSDesktop);


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

        var facebookInc = GenerateHoldingCompany("Facebook Inc");
        AttachToHolding(facebookInc, facebook);
        AttachToHolding(facebookInc, fbMessenger);

        var microsoft = GenerateHoldingCompany("Microsoft Inc");
        AttachToHolding(microsoft, microsoftOs);

        //PlayAs(tg);
        //PlayAs(google);
        //PlayAs(alphabet);
        var mailru = GenerateHoldingCompany("MailRu");
        AttachToHolding(mailru, vk);
        //AttachToHolding(mailru, twitter.company.Id);




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

    void PlayAs(string companyName)
    {
        var company = CompanyUtils.GetCompanyByName(GameContext, companyName);

        PlayAs(company);
    }

    void PlayAs(int companyId)
    {
        var company = CompanyUtils.GetCompanyById(GameContext, companyId);

        PlayAs(company);
    }

    void PlayAs(GameEntity company)
    {
        CompanyUtils.PlayAs(company, GameContext);
    }

    void AddCash(GameEntity company, long money)
    {
        CompanyUtils.SetResources(company, new Assets.Classes.TeamResource(1000000000));
    }

    void AddCash(int companyId, long money)
    {
        var company = CompanyUtils.GetCompanyById(GameContext, companyId);

        AddCash(company, money);
    }

    void SetSpheresOfInfluence()
    {
        var financial = CompanyUtils.GetInvestmentFunds(GameContext);
        //var managing = CompanyUtils.GetGroupCompanies(GameContext);

        foreach (var c in financial)
        {
            CompanyUtils.AddFocusIndustry(GetRandomIndustry(), c);

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
            CompanyUtils.AddFocusNiche(n.niche.NicheType, company, GameContext);
    }

    void AutoFillSomeFocusNichesByIndustry(GameEntity company)
    {
        var niches = NicheUtils.GetNichesInIndustry(company.companyFocus.Industries[0], GameContext);

        //CompanyUtils.AddFocusNiche(RandomEnum<NicheComponent>.PickRandomItem(niches).NicheType, company);

        foreach (var n in niches)
            CompanyUtils.AddFocusNiche(n.niche.NicheType, company, GameContext);
    }

    IndustryType GetRandomIndustry()
    {
        return RandomEnum<IndustryType>.GenerateValue();
    }

    GameEntity GenerateProductCompany(string name, NicheType nicheType)
    {
        var product = CompanyUtils.GenerateProductCompany(GameContext, name, nicheType);

        CompanyUtils.SetStartCapital(product, GameContext);

        return product;
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

        var c = CompanyUtils.GetCompanyById(GameContext, child);
        var p = CompanyUtils.GetCompanyById(GameContext, parent);

        if (c.hasProduct)
            CompanyUtils.AddFocusNiche(c.product.Niche, p, GameContext);
    }

    void AttachToHolding(int parent, GameEntity child)
    {
        AttachToHolding(parent, child.company.Id);
    }

    void AddShareholder(int companyId, int investorId, int shares)
    {
        //Debug.Log($"Add Shareholder {investorId} with {shares} shares to {companyId}");

        CompanyUtils.AddShareholder(GameContext, companyId, investorId, shares);
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
        return InvestmentUtils.GetRandomInvestmentFund(GameContext);
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

    void AutoFillNonFilledShareholders()
    {
        CompanyUtils.AutoFillNonFilledShareholders(GameContext, false);
    }
}
