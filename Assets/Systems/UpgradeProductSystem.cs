using Entitas;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeProductSystem : IExecuteSystem
{
    readonly GameContext _context;

    public UpgradeProductSystem(Contexts contexts)
    {
        _context = contexts.game;
    }

    public void Execute()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            GameEntity[] entities = _context.GetEntities(
                GameMatcher
                .AllOf(GameMatcher.Product)
                );

            foreach(var e in _context.GetEntities(GameMatcher.AllOf(GameMatcher.Product))) {
                Debug.Log($"update {e.product.Name}");
                e.product.Clients++;
            }


            foreach (var e in entities)
                Debug.Log($"Company: {e.product.Name}");
        }
    }
}

public class ProductInitializerSystem : IInitializeSystem
{
    readonly GameContext _context;

    public ProductInitializerSystem(Contexts contexts)
    {
        _context = contexts.game;
    }

    void GenerateCompany(string name, Niche niche, int id)
    {
        WorkerGroup workers = new WorkerGroup { Managers = 0, Marketers = 0, Programmers = 1 };
        var resources = new Assets.Classes.TeamResource(0, 0, 0, 0, 10000);

        var ads = new List<Assets.Classes.Advert>();

        uint clients = 0;
        int brandPower = 0;

        int analyticsLevel = 0;
        int experiments = 0;

        int productLevel = 0;
        int explorationLevel = productLevel;

        _context.CreateEntity().AddProduct(id, name, niche, productLevel, explorationLevel, workers, resources, analyticsLevel, experiments, clients, brandPower, ads);
    }

    void GenerateCompany(string name, Niche niche)
    {
        int id = _context.GetEntities(GameMatcher.Product).Length;

        GenerateCompany(name, niche, id);
    }

    public void Initialize()
    {
        GenerateCompany("facebook", Niche.SocialNetwork);
        GenerateCompany("mySpace", Niche.SocialNetwork);
        GenerateCompany("twitter", Niche.SocialNetwork);
        GenerateCompany("vk", Niche.SocialNetwork);
    }
}
