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

    void EnableScreen(ScreenMode screenMode)
    {
        screen = screenMode;
        menu.ReplaceMenu(screen);
    }

    public void Initialize()
    {
        menu = context.CreateEntity();
        screen = ScreenMode.DevelopmentScreen;

        menu.AddMenu(screen);
    }

    public void Execute()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            EnableScreen(ScreenMode.DevelopmentScreen);

        if (Input.GetKeyDown(KeyCode.Alpha2))
            EnableScreen(ScreenMode.InvesmentsScreen);

        if (Input.GetKeyDown(KeyCode.Alpha3))
            EnableScreen(ScreenMode.BusinessScreen);

        if (Input.GetKeyDown(KeyCode.Alpha4))
            EnableScreen(ScreenMode.MarketScreen);
    }
}