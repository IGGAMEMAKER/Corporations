using Assets.Utils;
using Entitas;
using System;
using System.Collections.Generic;

public class ProductInitializerSystem : IInitializeSystem
{
    readonly GameContext GameContext;

    public ProductInitializerSystem(Contexts contexts)
    {
        GameContext = contexts.game;
    }

    void GenerateProduct(string name, NicheType niche, int id)
    {
        IndustryType industry = NicheUtils.GetIndustry(niche, GameContext);

        var resources = new Assets.Classes.TeamResource(100, 100, 100, 100, 10000);

        uint clients = (uint)UnityEngine.Random.Range(0, 10000);
        int brandPower = UnityEngine.Random.Range(0, 15);

        int productLevel = 0;
        int explorationLevel = productLevel;

        var e = GameContext.CreateEntity();
        e.AddCompany(id, name, CompanyType.ProductCompany);

        // product specific components
        e.AddProduct(id, name, niche, industry, productLevel, explorationLevel, resources);
        e.AddFinance(0, 0, 0, 5f);
        e.AddTeam(1, 0, 0, 100);
        e.AddMarketing(clients, brandPower, false);
    }

    GameEntity GetCompanyById (int id)
    {
        return Array.Find(GameContext.GetEntities(GameMatcher.Company), c => c.company.Id == id);
    }

    void SetPlayerControlledCompany(int id)
    {
        var c = GetCompanyById(id);

        c.isControlledByPlayer = true;
        c.isSelectedCompany = true;
    }

    void RemovePlayerControlledCompany(int id)
    {
        GetCompanyById(id).isControlledByPlayer = false;
    }

    int GenerateId()
    {
        return CompanyUtils.GenerateCompanyId(GameContext);
    }

    int GenerateProduct(string name, NicheType niche)
    {
        int id = GenerateId();

        GenerateProduct(name, niche, id);

        return id;
    }

    int GenerateInvestmentFund(string name, long money)
    {
        var e = GameContext.CreateEntity();

        int id = GenerateId();
        int investorId = GenerateInvestorId();

        e.AddCompany(id, name, CompanyType.FinancialGroup);
        BecomeInvestor(e, money);

        return investorId;
    }

    int BecomeInvestor(GameEntity e, long money)
    {
        int investorId = GenerateInvestorId();

        string name = "Investor?";

        // company
        if (e.hasCompany)
            name = e.company.Name;

        // or human
        // TODO turn human to investor

        e.AddShareholder(investorId, name, money);

        return investorId;
    }

    int GenerateHoldingCompany(string name)
    {
        var e = GameContext.CreateEntity();

        int id = GenerateId();

        e.AddCompany(id, name, CompanyType.Holding);
        BecomeInvestor(e, 0);

        return id;
    }

    void AttachToHolding(int parent, int subsidiary)
    {
        var p = GetCompanyById(parent);
        var s = GetCompanyById(subsidiary);

        Dictionary<int, int> shareholders = new Dictionary<int, int>();
        shareholders.Add(p.shareholder.Id, 100);

        if (s.hasShareholders)
            s.ReplaceShareholders(shareholders);
        else
            s.AddShareholders(shareholders);
    }

    int GenerateInvestorId()
    {
        return CompanyUtils.GenerateShareholderId(GameContext);
    }

    void AddShareholder(int companyId, int investorId, int shares)
    {
        var c = GetCompanyById(companyId);

        Dictionary<int, int> shareholders;

        if (!c.hasShareholders)
        {
            shareholders = new Dictionary<int, int>();
            shareholders[investorId] = shares;

            c.AddShareholders(shareholders);
        } else
        {
            shareholders = c.shareholders.Shareholders;
            shareholders[investorId] = shares;

            c.ReplaceShareholders(shareholders);
        }
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
