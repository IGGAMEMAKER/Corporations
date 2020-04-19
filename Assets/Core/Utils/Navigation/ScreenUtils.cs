using Entitas;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

        public static Dictionary<string, object> GetScreenData(GameContext context)
        {
            return GetMenu(context).menu.Data;
        }

        // get selected stuff
        public static GameEntity GetSelectedCompany(GameContext gameContext)
        {
            int companyId = (int)GetScreenData(gameContext)[Balance.MENU_SELECTED_COMPANY];

            return Companies.Get(gameContext, companyId);
        }

        public static GameEntity GetSelectedInvestor(GameContext gameContext)
        {
            int id = (int)GetScreenData(gameContext)[Balance.MENU_SELECTED_INVESTOR];

            return Investments.GetInvestorById(gameContext, id);
        }

        public static IndustryType GetSelectedIndustry(GameContext gameContext)
        {
            return (IndustryType)(int)GetScreenData(gameContext)[Balance.MENU_SELECTED_INDUSTRY];
        }

        public static NicheType GetSelectedNiche(GameContext gameContext)
        {
            var niche = GetScreenData(gameContext)[Balance.MENU_SELECTED_NICHE];

            if (niche.GetType() == typeof(NicheType))
                return (NicheType)niche;

            return (NicheType)System.Enum.ToObject(typeof(NicheType), niche);
        }

        public static GameEntity GetSelectedHuman(GameContext gameContext)
        {
            var humanId = (int) GetScreenData(gameContext)[Balance.MENU_SELECTED_HUMAN];

            return Humans.GetHuman(gameContext, humanId);
        }

        // set selected stuff
        public static void SetSelectedHuman(GameContext gameContext, int humanId)
        {
            var menu = GetMenu(gameContext);

            var data = menu.menu.Data;

            if ((int)data[Balance.MENU_SELECTED_HUMAN] == humanId)
                return;

            data[Balance.MENU_SELECTED_HUMAN] = humanId;

            ReplaceMenu(gameContext, menu.menu.ScreenMode, data);
        }


        public static void SetSelectedCompany(GameContext gameContext, int companyId)
        {
            var menu = GetMenu(gameContext);

            var data = menu.menu.Data;

            if ((int)data[Balance.MENU_SELECTED_COMPANY] == companyId)
                return;

            data[Balance.MENU_SELECTED_COMPANY] = companyId;

            ReplaceMenu(gameContext, menu.menu.ScreenMode, data);
        }


        public static void SetSelectedNiche(GameContext gameContext, NicheType nicheType)
        {
            var menu = GetMenu(gameContext);

            var data = menu.menu.Data;

            if ((NicheType)data[Balance.MENU_SELECTED_NICHE] == nicheType)
                return;

            data[Balance.MENU_SELECTED_NICHE] = nicheType;

            ReplaceMenu(gameContext, menu.menu.ScreenMode, data);
        }

        // update menues
        public static void TriggerScreenUpdate(GameContext gameContext)
        {
            var menu = GetMenu(gameContext);

            ReplaceMenu(gameContext, menu.menu.ScreenMode, menu.menu.Data);
            //menu.ReplaceMenu(menu.menu.ScreenMode, menu.menu.Data);
        }

        public static void ReplaceMenu(GameContext gameContext, ScreenMode screenMode, Dictionary<string, object> data)
        {
            var menu = GetMenu(gameContext);

            menu.ReplaceMenu(screenMode, data);
        }
    }
}
