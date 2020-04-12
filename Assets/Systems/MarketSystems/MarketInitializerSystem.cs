using Assets.Core;
using Entitas;


public partial class MarketInitializerSystem : IInitializeSystem
{
    readonly GameContext gameContext;

    public MarketInitializerSystem(Contexts contexts)
    {
        gameContext = contexts.game;
    }

    void IInitializeSystem.Initialize()
    {
        Markets.SpawnMarkets(gameContext);
    }
}
