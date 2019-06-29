using Entitas;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Utils
{
    public static partial class ScreenUtils
    {
        public static GameEntity GetMenu(GameContext gameContext)
        {
            return gameContext.GetEntities(GameMatcher.Menu)[0];
        }

        public static Dictionary<string, object> GetScreenData(GameContext context)
        {
            var queries = GetNavigationHistory(context).navigationHistory.Queries;

            return queries[queries.Count - 1].Data;
            return GetMenu(context).menu.Data;
        }

        public static ScreenMode GetScreenMode(GameContext context)
        {
            var queries = GetNavigationHistory(context).navigationHistory.Queries;

            return queries[queries.Count - 1].ScreenMode;
            //return GetScreenData(context)
            return GetMenu(context).menu.ScreenMode;
        }

        public static GameEntity GetSelectedCompany(GameContext gameContext)
        {
            int companyId = (int)GetScreenData(gameContext)[Constants.MENU_SELECTED_COMPANY];

            return CompanyUtils.GetCompanyById(gameContext, companyId);
        }

        public static GameEntity GetSelectedInvestor(GameContext gameContext)
        {
            int id = (int)GetScreenData(gameContext)[Constants.MENU_SELECTED_INVESTOR];

            return InvestmentUtils.GetInvestorById(gameContext, id);
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

            return HumanUtils.GetHumanById(gameContext, humanId);
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
    }
}
