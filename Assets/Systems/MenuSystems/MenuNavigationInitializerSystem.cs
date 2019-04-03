using System;
using Entitas;

public class MenuNavigationInitializerSystem : IInitializeSystem
{
    readonly GameContext GameContext;

    public MenuNavigationInitializerSystem(Contexts contexts)
    {
        GameContext = contexts.game;
    }

    void IInitializeSystem.Initialize()
    {
        
    }
}
