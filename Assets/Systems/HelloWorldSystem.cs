using Entitas;
using UnityEngine;

public class HelloWorldSystem : IInitializeSystem
{
    // always handy to keep a reference to the context 
    // we're going to be interacting with it
    readonly GameContext _context;

    public HelloWorldSystem(Contexts contexts)
    {
        // get the context from the constructor
        _context = contexts.game;
    }

    public void Initialize()
    {
        // create an entity and give it a DebugMessageComponent with
        // the text "Hello World!" as its data
        //_context.CreateEntity().AddDebugMessage("Hello World!");
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
