using Assets.Core;
using Entitas;

public class MenuInitializeSystem : IInitializeSystem
{
    readonly GameContext context;

    public MenuInitializeSystem(Contexts contexts)
    {
        context = contexts.game;
    }

    void IInitializeSystem.Initialize()
    {
        var c = ScreenUtils.GetMenu(context);
    }
}
