using Assets.Utils;
using Entitas;
using System;

public class TutorialInitializeSystem : IInitializeSystem
{
    readonly GameContext GameContext;

    public TutorialInitializeSystem(Contexts contexts)
    {
        GameContext = contexts.game;
    }

    void IInitializeSystem.Initialize()
    {
        Initialize();
    }

    void Initialize()
    {
        var e = GameContext.CreateEntity();

        e.AddTutorial(new System.Collections.Generic.Dictionary<TutorialFunctionality, bool>());
    }
}
