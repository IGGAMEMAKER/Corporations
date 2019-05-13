using Assets.Utils;
using Entitas;
using System.Collections.Generic;

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

    void EnableScreen(ScreenMode screenMode, Dictionary<string, object> data)
    {
        screen = screenMode;

        ScreenUtils.Navigate(context, screenMode, data);
    }

    void IInitializeSystem.Initialize()
    {
        menu = context.CreateEntity();
        screen = ScreenMode.DevelopmentScreen;

        menu.AddNavigationHistory(new List<MenuComponent>());

        var dictionary = new Dictionary<string, object>
        {
            [Constants.MENU_SELECTED_COMPANY] = 1
        };

        menu.AddMenu(screen, dictionary);
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