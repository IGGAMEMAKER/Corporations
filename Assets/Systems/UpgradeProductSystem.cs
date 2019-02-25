using Entitas;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeProductSystem : IExecuteSystem, IInitializeSystem
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

            foreach(var e in _context.GetEntities(GameMatcher.AnyOf(GameMatcher.Product))) {
                e.product.Clients++;
            }


            //foreach (var e in entities)
            //{
            //    Debug.Log($"Company: {e.product.Name}");
            //}
            //_context.CreateEntity()
        }
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

public class ProductSystem : IInitializeSystem
{
    // always handy to keep a reference to the context 
    // we're going to be interacting with it
    readonly GameContext _context;

    public ProductSystem(Contexts contexts)
    {
        // get the context from the constructor
        _context = contexts.game;
    }

    public void Initialize()
    {
        // create an entity and give it a DebugMessageComponent with
        // the text "Hello World!" as its data
        //_context.CreateEntity().AddProduct(
        //    0, "Facebook", Niche.SocialNetwork,
        //    0, 0, 
        //    , new Assets.Classes.TeamResource(), 0, 0, 100, new System.Collections.Generic.List<Assets.Classes.Advert>());
    }
}
