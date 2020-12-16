using Entitas;
using System;
using System.Collections.Generic;

namespace Assets.Core
{
    public static partial class ScreenUtils
    {
        public static object GetScreenParameter(GameContext context, string key)
        {
            return GetScreenData(context)[key];
        }

        public static int GetInteger(GameContext context, string key) =>
            Convert.ToInt32(GetScreenParameter(context, key));

        public static Dictionary<string, object> GetScreenData(GameContext context)
        {
            return GetMenu(context).menu.Data;
        }

        // get selected stuff
        public static GameEntity GetSelectedCompany(GameContext gameContext)
        {
            var obj = GetScreenParameter(gameContext, C.MENU_SELECTED_COMPANY);

            var companyId = (int)obj;

            return Companies.Get(gameContext, companyId);
        }

        public static int GetSelectedTeam(GameContext gameContext)
        {
            var obj = GetScreenParameter(gameContext, C.MENU_SELECTED_TEAM);

            var companyId = Convert.ToInt32(obj);

            return companyId;
        }

        public static int GetMainScreenPanelId(GameContext gameContext)
        {
            var obj = GetScreenParameter(gameContext, C.MENU_SELECTED_MAIN_SCREEN_PANEL_ID);

            var companyId = Convert.ToInt32(obj);

            return companyId;
        }

        public static GameEntity GetSelectedHuman(GameContext gameContext)
        {
            var obj = GetScreenParameter(gameContext, C.MENU_SELECTED_HUMAN);

            var humanId = (int)obj;

            return Humans.Get(gameContext, humanId);
        }

        public static NicheType GetSelectedNiche(GameContext gameContext)
        {
            var niche = GetScreenParameter(gameContext, C.MENU_SELECTED_NICHE);

            if (niche.GetType() == typeof(NicheType))
                return (NicheType)niche;

            return (NicheType)System.Enum.ToObject(typeof(NicheType), niche);
        }

        public static IndustryType GetSelectedIndustry(GameContext gameContext)
        {
            var obj = GetScreenParameter(gameContext, C.MENU_SELECTED_INDUSTRY);

            return (IndustryType)(int)obj;
        }

        public static GameEntity GetSelectedInvestor(GameContext gameContext)
        {
            var id = GetScreenParameter(gameContext, C.MENU_SELECTED_INVESTOR);

            return Investments.GetInvestor(gameContext, (int)id);
        }
    }
}
