using Assets.Utils;
using Entitas;
using System;
using System.Collections.Generic;
using UnityEngine;

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

        Dictionary<int, int> shareholders = new Dictionary<int, int>();

        int shareholderId = 0;

        shareholders[shareholderId] = 100;

        var e = GameContext.CreateEntity();
        e.AddCompany(id, name, CompanyType.Product);

        // product specific components
        e.AddProduct(id, name, niche, industry, productLevel, explorationLevel, resources);
        e.AddFinance(0, 0, 0, 5f);
        e.AddTeam(1, 0, 0, 100);
        e.AddMarketing(clients, brandPower, false);

        e.AddShareholders(shareholders);

        var c = GameContext.CreateEntity();
        // shareholders?
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

    void GenerateInvestmentFunds(string name)
    {
        var e = GameContext.CreateEntity();

        e.AddCompany(GenerateId(), name, CompanyType.FinancialGroup);
    }

    int GenerateHoldingCompany(string name)
    {
        var e = GameContext.CreateEntity();

        int id = GenerateId();

        e.AddCompany(id, name, CompanyType.Holding);

        return id;
    }

    void AttachCompany(int parent, int subsidiary)
    {
        var p = GetCompanyById(parent);
        var s = GetCompanyById(subsidiary);

        Dictionary<int, int> shareholders = new Dictionary<int, int>();
        shareholders.Add(parent, 100);

        if (s.hasShareholders)
            s.ReplaceShareholders(shareholders);
        else
            s.AddShareholders(shareholders);
    }

    public void Initialize()
    {
        GenerateInvestmentFunds("Morgan Stanley");
        GenerateInvestmentFunds("Goldman Sachs");

        int myCompany;

        GenerateProduct("facebook", Niche.SocialNetwork);
        GenerateProduct("mySpace", Niche.SocialNetwork);
        GenerateProduct("twitter", Niche.SocialNetwork);
        GenerateProduct("vk", Niche.SocialNetwork);

        int google = GenerateProduct("google", Niche.SearchEngine);

        int alphabet = GenerateHoldingCompany("Alphabet");

        AttachCompany(alphabet, google);

        myCompany = google;

        SetPlayerControlledCompany(myCompany);
    }
}
