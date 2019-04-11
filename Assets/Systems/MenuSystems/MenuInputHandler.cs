using Assets.Utils;
using Entitas;

class MenuInputSystem :
    IExecuteSystem,
    IInitializeSystem
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

        MenuUtils.Navigate(context, screenMode, data);
    }

    void IInitializeSystem.Initialize()
    {
        menu = context.CreateEntity();
        screen = ScreenMode.DevelopmentScreen;

        menu.AddNavigationHistory(new System.Collections.Generic.List<MenuComponent>());
        menu.AddMenu(screen, 1);
    }

    void IExecuteSystem.Execute()
    {
        //if (Input.GetKeyDown(KeyCode.Alpha1))
        //    EnableScreen(ScreenMode.DevelopmentScreen, null);

        //if (Input.GetKeyDown(KeyCode.Alpha2))
        //    EnableScreen(ScreenMode.CharacterScreen, null);

        //if (Input.GetKeyDown(KeyCode.Alpha3))
        //    EnableScreen(ScreenMode.IndustryScreen, IndustryType.Search);

        //if (Input.GetKeyDown(KeyCode.Alpha4))
        //    EnableScreen(ScreenMode.BusinessScreen, null);
    }
}