using Assets.Utils;
using Entitas;
using UnityEngine;

public class ProductInitializerSystem : IInitializeSystem
{
    readonly GameContext _context;
    int currentId;

    public ProductInitializerSystem(Contexts contexts)
    {
        _context = contexts.game;
    }

    void GenerateProduct(string name, Niche niche, Industry industry, int id)
    {
        var resources = new Assets.Classes.TeamResource(100, 100, 100, 100, 10000);

        uint clients = (uint) Random.Range(0, 10000);
        int brandPower = Random.Range(0, 15);

        int productLevel = 0;
        int explorationLevel = productLevel;

        var e = _context.CreateEntity();
        e.AddCompany(id, name, CompanyType.Product);
        e.AddProduct(id, name, niche, industry, productLevel, explorationLevel, resources);
        e.AddFinance(0, 0, 0, 5f);
        e.AddTeam(1, 0, 0, 100);
        e.AddMarketing(clients, brandPower, false);
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
        //return currentId++;
    }

    void GenerateProduct(string name, Niche niche, Industry industry)
    {
        int id = GenerateId();

        GenerateProduct(name, niche, industry, id);
    }

    void GenerateFinancialGroup(string name)
    {
        var e = _context.CreateEntity();
        e.AddCompany(GenerateId(), name, CompanyType.FinancialGroup);
    }

    public void Initialize()
    {
        GenerateProduct("facebook", Niche.SocialNetwork, Industry.Communications);
        GenerateProduct("mySpace", Niche.SocialNetwork, Industry.Communications);
        GenerateProduct("twitter", Niche.SocialNetwork, Industry.Communications);
        GenerateProduct("vk", Niche.SocialNetwork, Industry.Communications);

        GenerateFinancialGroup("Morgan Stanley");
        GenerateFinancialGroup("Goldman Sachs");

        SetPlayerControlledCompany(2);
    }
}
