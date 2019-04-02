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

    void GenerateProduct(string name, Niche niche, int id)
    {
        Industry industry = NicheUtils.GetIndustry(niche);

        var resources = new Assets.Classes.TeamResource(100, 100, 100, 100, 10000);

        uint clients = (uint)UnityEngine.Random.Range(0, 10000);
        int brandPower = UnityEngine.Random.Range(0, 15);

        int productLevel = 0;
        int explorationLevel = productLevel;

        var e = GameContext.CreateEntity();
        e.AddCompany(id, name, CompanyType.Product);

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
        GetCompanyById(id).isControlledByPlayer = true;
        GetCompanyById(id).isSelectedCompany = true;
    }

    void RemovePlayerControlledCompany(int id)
    {
        GetCompanyById(id).isControlledByPlayer = false;
    }

    int GenerateId()
    {
        return CompanyUtils.GenerateCompanyId(GameContext);
    }

    int GenerateProduct(string name, Niche niche)
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

        return id;
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

    void AttachCompany(int parent, int subsidiary)
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



    void AddShareholders(int companyId, int investorId, int shares)
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

    public void Initialize()
    {
        // products
        //GenerateProduct("facebook", Niche.SocialNetwork);
        //GenerateProduct("mySpace", Niche.SocialNetwork);
        //GenerateProduct("twitter", Niche.SocialNetwork);
        //GenerateProduct("vk", Niche.SocialNetwork);

        int google = GenerateProduct("Google", Niche.SearchEngine);

        SetPlayerControlledCompany(google);

        // investors
        int investor = GenerateInvestmentFund("Morgan Stanley", 1000000);
        int investorId = GetCompanyById(investor).shareholder.Id;

        int alphabet = GenerateHoldingCompany("Alphabet");
        AttachCompany(alphabet, google);

        AddShareholders(alphabet, investorId, 100);
    }
}
