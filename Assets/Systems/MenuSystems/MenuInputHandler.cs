using Assets.Core;
using Entitas;
using System.Collections.Generic;

class MenuInputSystem :
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
        screen = ScreenMode.NicheScreen;

        menu.AddNavigationHistory(new List<MenuComponent>());

        var dictionary = new Dictionary<string, object>
        {
            [Balance.MENU_SELECTED_COMPANY] = 1,
            [Balance.MENU_SELECTED_INDUSTRY] = IndustryType.Technology,
            [Balance.MENU_SELECTED_NICHE] = NicheType.Tech_SearchEngine,
            [Balance.MENU_SELECTED_HUMAN] = 0,
            [Balance.MENU_SELECTED_INVESTOR] = -1,
        };

        menu.AddMenu(screen, dictionary);
    }
}