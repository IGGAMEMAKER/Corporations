using Entitas;
using UnityEngine;

public class ProductInitializerSystem : IInitializeSystem
{
    readonly GameContext _context;

    public ProductInitializerSystem(Contexts contexts)
    {
        _context = contexts.game;
    }

    void GenerateProduct(string name, Niche niche, Industry industry, int id)
    {
        var resources = new Assets.Classes.TeamResource(100, 100, 100, 100, 10000);

        uint clients = (uint) Random.Range(0, 10000);
        int brandPower = Random.Range(0, 15);

        int analyticsLevel = 0;
        int experiments = 0;

        int productLevel = 0;
        int explorationLevel = productLevel;

        var e = _context.CreateEntity();
        e.AddProduct(id, name, niche, industry, productLevel, explorationLevel, resources);
        e.AddTeam(1, 0, 0, 100);
        e.AddMarketing(clients, brandPower, false);
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

    void GenerateProduct(string name, Niche niche, Industry industry)
    {
        int id = _context.GetEntities(GameMatcher.Product).Length;

        GenerateProduct(name, niche, industry, id);
    }

    public void Initialize()
    {
        GenerateProduct("facebook", Niche.SocialNetwork, Industry.Communications);
        GenerateProduct("mySpace", Niche.SocialNetwork, Industry.Communications);
        GenerateProduct("twitter", Niche.SocialNetwork, Industry.Communications);
        GenerateProduct("vk", Niche.SocialNetwork, Industry.Communications);

        SetPlayerControlledCompany(2);
    }
}
