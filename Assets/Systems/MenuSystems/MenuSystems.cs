using Assets.Core;
using Entitas;

public class MenuSystems : IInitializeSystem
{
    readonly GameContext context;

    public MenuSystems(Contexts contexts)
    {
        context = contexts.game;
    }

    void IInitializeSystem.Initialize()
    {
        var c = ScreenUtils.GetMenu(context);
    }
}
