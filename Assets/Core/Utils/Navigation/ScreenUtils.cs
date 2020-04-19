using Entitas;
using System.Collections.Generic;

namespace Assets.Core
{
    public static partial class ScreenUtils
    {
        public static GameEntity GetMenu(GameContext gameContext)
        {
            var entities = gameContext.GetEntities(GameMatcher.Menu);
            
            if (entities.Length == 0)
                return CreateMenu(gameContext);
            else
                return entities[0];
        }

        public static GameEntity CreateMenu(GameContext gameContext) => CreateMenu(gameContext.CreateEntity());
        public static GameEntity CreateMenu(GameEntity menu)
        {
            menu.AddNavigationHistory(new List<MenuComponent>());

            var dictionary = new Dictionary<string, object>
            {
                [Balance.MENU_SELECTED_COMPANY] = 1,
                [Balance.MENU_SELECTED_INDUSTRY] = IndustryType.Technology,
                [Balance.MENU_SELECTED_NICHE] = NicheType.Tech_SearchEngine,
                [Balance.MENU_SELECTED_HUMAN] = 0,
                [Balance.MENU_SELECTED_INVESTOR] = -1,
            };

            menu.AddMenu(ScreenMode.NicheScreen, dictionary);

            return menu;
        }

        // update menues
        public static void UpdateScreen(GameContext context, ScreenMode screenMode, Dictionary<string, object> data)
        {
            GetMenu(context).ReplaceMenu(screenMode, data);
        }

        public static void UpdateScreen(GameContext context)
        {
            var menu = GetMenu(context);

            UpdateScreen(context, menu.menu.ScreenMode, menu.menu.Data);
        }
    }
}
