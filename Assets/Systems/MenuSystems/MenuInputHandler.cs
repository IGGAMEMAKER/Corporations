using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class ScreenRenderSystem : ReactiveSystem<GameEntity>
{
    protected ScreenRenderSystem(Contexts contexts) : base(contexts.game)
    {
    }

    protected override void Execute(List<GameEntity> entities)
    {
        throw new System.NotImplementedException();
    }

    protected override bool Filter(GameEntity entity)
    {
        throw new System.NotImplementedException();
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        throw new System.NotImplementedException();
    }
}

internal class MenuInputSystem : IExecuteSystem, IInitializeSystem
{
    readonly GameContext context;
    GameEntity menu;

    public MenuInputSystem(Contexts contexts)
    {
        this.context = contexts.game;
    }

    void EnableScreen(ScreenMode screenMode)
    {
        menu.ReplaceMenu(screenMode);
    }

    public void Execute()
    {
        //if (Input.GetKeyDown(KeyCode.Alpha1))
        //    EnableScreen(ScreenMode.TechnologyScreen);

        //if (Input.GetKeyDown(KeyCode.Alpha2))
        //    EnableScreen(ScreenMode.InvesmentsScreen);

        //if (Input.GetKeyDown(KeyCode.Alpha3))
        //    EnableScreen(ScreenMode.BusinessScreen);
    }

    public void Initialize()
    {
        context.CreateEntity().AddMenu(ScreenMode.TechnologyScreen);
    }
}