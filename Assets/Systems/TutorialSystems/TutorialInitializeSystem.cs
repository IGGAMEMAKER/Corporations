using Entitas;

public class TutorialInitializeSystem : IInitializeSystem
{
    readonly GameContext GameContext;

    public TutorialInitializeSystem(Contexts contexts)
    {
        GameContext = contexts.game;
    }

    void IInitializeSystem.Initialize()
    {
        var e = GameContext.CreateEntity();

        e.AddTutorial(new System.Collections.Generic.Dictionary<TutorialFunctionality, bool>());
        e.AddEventContainer(new System.Collections.Generic.Dictionary<string, bool>());
    }
}
