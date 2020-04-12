using Assets.Core;
using Entitas;
using System;


public partial class MarketInitializerSystem : IInitializeSystem
{
    readonly GameContext gameContext;

    public MarketInitializerSystem(Contexts contexts)
    {
        gameContext = contexts.game;
    }

    void IInitializeSystem.Initialize()
    {
    }
}

public partial class MarketInitializerSystem : IInitializeSystem
{

}
