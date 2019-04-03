using Entitas;
using UnityEngine;

class MenuInputSystem : IExecuteSystem, IInitializeSystem
{
    readonly GameContext context;
    public ScreenMode screen;
    GameEntity menu;

    public MenuInputSystem(Contexts contexts)
    {
        context = contexts.game;
    }

    void EnableScreen(ScreenMode screenMode, object data)
    {
        screen = screenMode;
        menu.ReplaceMenu(screen, data);
    }

    public void Initialize()
    {
        menu = context.CreateEntity();
        screen = ScreenMode.DevelopmentScreen;

        menu.AddMenu(screen, null);
    }

    public void Execute()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            EnableScreen(ScreenMode.DevelopmentScreen, null);

        if (Input.GetKeyDown(KeyCode.Alpha2))
            EnableScreen(ScreenMode.InvesmentsScreen, null);

        if (Input.GetKeyDown(KeyCode.Alpha3))
            EnableScreen(ScreenMode.BusinessScreen, null);
    }
}