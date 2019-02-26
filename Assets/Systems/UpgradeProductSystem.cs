using System.Collections.Generic;
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
            GameEntity[] entities = _context.GetEntities(GameMatcher.AllOf(GameMatcher.Product));

            foreach(var e in entities) {
                var p = e.product;

                Debug.Log($"update {e.product.Name}");
                e.ReplaceProduct(p.Id, p.Name, p.Niche, p.ProductLevel, p.ExplorationLevel, p.Team, p.Resources, p.Analytics + 1, p.ExperimentCount);
            }
        }
    }
}

public class LogProductChangesSystem : ReactiveSystem<GameEntity>
{
    private Contexts contexts;

    public LogProductChangesSystem(Contexts contexts) : base(contexts.game)
    {
        this.contexts = contexts;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var entity in entities)
        {
            Debug.Log($"{entity.product.Name}.analytics updated to {entity.product.Analytics}");
        }
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasProduct && entity.product.Id == 0;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Product);
    }
}

public class ProductInitializerSystem : IInitializeSystem
{
    readonly GameContext _context;

    //private void ObserveChanges()
    //{
    //    _context.GetGroup(GameMatcher.Product).OnEntityUpdated += (group, entity, index, previous, current) =>
    //    {
    //        ProductComponent prev = (ProductComponent)previous;
    //        ProductComponent curr = (ProductComponent)current;

    //        //Debug.Log($"{entity.product.Name}.analytics updated from {prev.Analytics} to {curr.Analytics}");
    //    };
    //}

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

        var e = _context.CreateEntity();
        e.AddProduct(id, name, niche, productLevel, explorationLevel, workers, resources, analyticsLevel, experiments);
        e.AddMarketing(clients, brandPower);
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

        //ObserveChanges();
    }
}
