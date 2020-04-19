using Entitas;
using System.Collections.Generic;

namespace Assets.Core
{
    public static partial class ScreenUtils
    {
        public static object GetScreenParameter(GameContext context, string key)
        {
            return GetScreenData(context)[key];
        }

        public static Dictionary<string, object> GetScreenData(GameContext context)
        {
            return GetMenu(context).menu.Data;
        }

        // get selected stuff
        public static GameEntity GetSelectedCompany(GameContext gameContext)
        {
            var companyId = GetScreenParameter(gameContext, Balance.MENU_SELECTED_COMPANY);

            return Companies.Get(gameContext, (int)companyId);
        }

        public static GameEntity GetSelectedInvestor(GameContext gameContext)
        {
            var id = GetScreenParameter(gameContext, Balance.MENU_SELECTED_INVESTOR);

            return Investments.GetInvestorById(gameContext, (int)id);
        }

        public static IndustryType GetSelectedIndustry(GameContext gameContext)
        {
            var obj = GetScreenParameter(gameContext, Balance.MENU_SELECTED_INDUSTRY);

            return (IndustryType)(int)obj;
        }

        public static NicheType GetSelectedNiche(GameContext gameContext)
        {
            var niche = GetScreenParameter(gameContext, Balance.MENU_SELECTED_NICHE);

            if (niche.GetType() == typeof(NicheType))
                return (NicheType)niche;

            return (NicheType)System.Enum.ToObject(typeof(NicheType), niche);
        }

        public static GameEntity GetSelectedHuman(GameContext gameContext)
        {
            var humanId = GetScreenParameter(gameContext, Balance.MENU_SELECTED_HUMAN);

            return Humans.GetHuman(gameContext, (int)humanId);
        }
    }
}
