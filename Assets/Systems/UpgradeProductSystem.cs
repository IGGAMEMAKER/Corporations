using Entitas;
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
                var p = e.product;

                Debug.Log($"update {e.product.Name}");
                e.ReplaceProduct(p.Id, p.Name, p.Niche, p.ProductLevel, p.ExplorationLevel, p.Team, p.Resources, p.Analytics, p.ExperimentCount, p.Clients + 1, p.BrandPower);
            }
        }

    }
}

public class ProductInitializerSystem : IInitializeSystem
{
    readonly GameContext _context;

    private void ObserveChanges()
    {
        _context.GetGroup(GameMatcher.Product).OnEntityUpdated += (group, entity, index, previous, current) =>
        {
            ProductComponent prev = (ProductComponent)previous;
            ProductComponent curr = (ProductComponent)current;

            //Debug.Log($"{entity.product.Name}.clients updated from {prev.Clients} to {curr.Clients}");
        };
    }

    public ProductInitializerSystem(Contexts contexts)
    {
        _context = contexts.game;
    }

    void GenerateCompany(string name, Niche niche, int id)
    {
        WorkerGroup workers = new WorkerGroup { Managers = 0, Marketers = 0, Programmers = 1 };
        var resources = new Assets.Classes.TeamResource(0, 0, 0, 0, 10000);

        uint clients = 0;
        int brandPower = 0;

        int analyticsLevel = 0;
        int experiments = 0;

        int productLevel = 0;
        int explorationLevel = productLevel;

        _context.CreateEntity().AddProduct(id, name, niche, productLevel, explorationLevel, workers, resources, analyticsLevel, experiments, clients, brandPower);
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

        ObserveChanges();
    }
}
