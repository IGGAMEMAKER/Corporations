using Entitas;
using System.Collections.Generic;

namespace Assets.Core
{
    public static partial class ScreenUtils
    {
        public static GameEntity GetMenu(GameContext gameContext)
        {
            return gameContext.GetEntities(GameMatcher.Menu)[0];
        }

        public static Dictionary<string, object> GetScreenData(GameContext context)
        {
            return GetMenu(context).menu.Data;
        }

        public static ScreenMode GetScreenMode(GameContext context)
        {
            return GetMenu(context).menu.ScreenMode;
        }

        public static GameEntity GetSelectedCompany(GameContext gameContext)
        {
            int companyId = (int)GetScreenData(gameContext)[Constants.MENU_SELECTED_COMPANY];

            return Companies.GetCompany(gameContext, companyId);
        }

        public static GameEntity GetSelectedInvestor(GameContext gameContext)
        {
            int id = (int)GetScreenData(gameContext)[Constants.MENU_SELECTED_INVESTOR];

            return Investments.GetInvestorById(gameContext, id);
        }

        public static IndustryType GetSelectedIndustry(GameContext gameContext)
        {
            return (IndustryType)GetScreenData(gameContext)[Constants.MENU_SELECTED_INDUSTRY];
        }

        public static NicheType GetSelectedNiche(GameContext gameContext)
        {
            return (NicheType)GetScreenData(gameContext)[Constants.MENU_SELECTED_NICHE];
        }

        public static GameEntity GetSelectedHuman(GameContext gameContext)
        {
            var humanId = (int) GetScreenData(gameContext)[Constants.MENU_SELECTED_HUMAN];

            return Humans.GetHuman(gameContext, humanId);
        }


        public static void SetSelectedHuman(GameContext gameContext, int humanId)
        {
            var menu = GetMenu(gameContext);

            var data = menu.menu.Data;

            if ((int)data[Constants.MENU_SELECTED_HUMAN] == humanId)
                return;

            data[Constants.MENU_SELECTED_HUMAN] = humanId;

            ReplaceMenu(gameContext, menu.menu.ScreenMode, data);
        }


        public static void SetSelectedCompany(GameContext gameContext, int companyId)
        {
            var menu = GetMenu(gameContext);

            var data = menu.menu.Data;

            if ((int)data[Constants.MENU_SELECTED_COMPANY] == companyId)
                return;

            data[Constants.MENU_SELECTED_COMPANY] = companyId;

            ReplaceMenu(gameContext, menu.menu.ScreenMode, data);
        }


        public static void SetSelectedNiche(GameContext gameContext, NicheType nicheType)
        {
            var menu = GetMenu(gameContext);

            var data = menu.menu.Data;

            if ((NicheType)data[Constants.MENU_SELECTED_NICHE] == nicheType)
                return;

            data[Constants.MENU_SELECTED_NICHE] = nicheType;

            ReplaceMenu(gameContext, menu.menu.ScreenMode, data);
        }

        public static void ReplaceMenu(GameContext gameContext, ScreenMode screenMode, Dictionary<string, object> data)
        {
            var menu = GetMenu(gameContext);

            menu.ReplaceMenu(screenMode, data);
        }

        public static void TriggerScreenUpdate(GameContext gameContext)
        {
            var menu = GetMenu(gameContext);

            menu.ReplaceMenu(menu.menu.ScreenMode, menu.menu.Data);
        }
    }
}
