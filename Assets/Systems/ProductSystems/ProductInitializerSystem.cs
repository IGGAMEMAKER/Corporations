using Assets.Utils;
using Entitas;
using System.Collections.Generic;
using UnityEngine;

public class ProductInitializerSystem : IInitializeSystem
{
    readonly GameContext _context;

    public ProductInitializerSystem(Contexts contexts)
    {
        _context = contexts.game;
    }

    void GenerateProduct(string name, Niche niche, int id)
    {
        Industry industry = NicheUtils.GetIndustry(niche);

        var resources = new Assets.Classes.TeamResource(100, 100, 100, 100, 10000);

        uint clients = (uint) Random.Range(0, 10000);
        int brandPower = Random.Range(0, 15);

        int productLevel = 0;
        int explorationLevel = productLevel;

        List<int> shareholders = new List<int>();

        var s = 0; // new ShareholderComponent { Id = CompanyUtils.GenerateShareholderId(), Money = 100000, Name = "Iosebashvili Gaga" };

        shareholders.Add(s);

        var e = _context.CreateEntity();
        e.AddCompany(id, name, CompanyType.Product);
        e.AddShareholders(shareholders);
        e.AddProduct(id, name, niche, industry, productLevel, explorationLevel, resources);
        e.AddFinance(0, 0, 0, 5f);
        e.AddTeam(1, 0, 0, 100);
        e.AddMarketing(clients, brandPower, false);

        var c = _context.CreateEntity();
    }

    GameEntity GetProductById (int id)
    {
        return _context.GetEntities(GameMatcher.Product)[id];
    }

    void SetPlayerControlledCompany(int id)
    {
        GetProductById(id).isControlledByPlayer = true;
        GetProductById(id).isSelectedCompany = true;
    }

    void RemovePlayerControlledCompany(int id)
    {
        GetProductById(id).isControlledByPlayer = false;
    }

    int GenerateId()
    {
        return CompanyUtils.GenerateCompanyId(_context);
    }

    void GenerateProduct(string name, Niche niche)
    {
        int id = GenerateId();

        GenerateProduct(name, niche, id);
    }

    void GenerateInvestmentFunds(string name)
    {
        var e = _context.CreateEntity();

        e.AddCompany(GenerateId(), name, CompanyType.FinancialGroup);
    }

    public void Initialize()
    {
        GenerateInvestmentFunds("Morgan Stanley");
        GenerateInvestmentFunds("Goldman Sachs");

        GenerateProduct("facebook", Niche.SocialNetwork);
        GenerateProduct("mySpace", Niche.SocialNetwork);
        GenerateProduct("twitter", Niche.SocialNetwork);
        GenerateProduct("vk", Niche.SocialNetwork);

        SetPlayerControlledCompany(2);
    }
}
