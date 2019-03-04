using Entitas;
using UnityEngine;

public class ProductInitializerSystem : IInitializeSystem
{
    readonly GameContext _context;

    public ProductInitializerSystem(Contexts contexts)
    {
        _context = contexts.game;
    }

    void GenerateCompany(string name, Niche niche, Industry industry, int id)
    {
        var resources = new Assets.Classes.TeamResource(0, 0, 0, 0, 10000);

        uint clients = (uint) Random.Range(0, 10000);
        int brandPower = 0;

        int analyticsLevel = 0;
        int experiments = 0;

        int productLevel = 0;
        int explorationLevel = productLevel;

        var e = _context.CreateEntity();
        e.AddProduct(id, name, niche, industry, productLevel, explorationLevel, resources);
        e.AddTeam(1, 0, 0, 100);
        e.AddMarketing(clients, brandPower);
        e.AddAnalytics(analyticsLevel, experiments);
    }

    GameEntity GetProductById (int id)
    {
        return _context.GetEntities(GameMatcher.Product)[id];
    }

    void SetPlayerControlledCompany(int id)
    {
        GetProductById(id).isControlledByPlayer = true;
    }

    void RemovePlayerControlledCompany(int id)
    {
        GetProductById(id).isControlledByPlayer = false;
    }

    void GenerateCompany(string name, Niche niche, Industry industry)
    {
        int id = _context.GetEntities(GameMatcher.Product).Length;

        GenerateCompany(name, niche, industry, id);
    }

    public void Initialize()
    {
        GenerateCompany("facebook", Niche.SocialNetwork, Industry.Communications);
        GenerateCompany("mySpace", Niche.SocialNetwork, Industry.Communications);
        GenerateCompany("twitter", Niche.SocialNetwork, Industry.Communications);
        GenerateCompany("vk", Niche.SocialNetwork, Industry.Communications);

        SetPlayerControlledCompany(2);
    }
}
