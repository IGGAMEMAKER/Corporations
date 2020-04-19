using Entitas;
using System.Collections.Generic;

namespace Assets.Core
{
    public static partial class ScreenUtils
    {
        // set selected stuff
        public static void SetSelectedHuman(GameContext gameContext, int humanId)
        {
            var menu = GetMenu(gameContext);

            var data = menu.menu.Data;

            if ((int)data[Balance.MENU_SELECTED_HUMAN] == humanId)
                return;

            data[Balance.MENU_SELECTED_HUMAN] = humanId;

            UpdateScreen(gameContext, menu.menu.ScreenMode, data);
        }


        public static void SetSelectedCompany(GameContext gameContext, int companyId)
        {
            var menu = GetMenu(gameContext);

            var data = menu.menu.Data;

            if ((int)data[Balance.MENU_SELECTED_COMPANY] == companyId)
                return;

            data[Balance.MENU_SELECTED_COMPANY] = companyId;

            UpdateScreen(gameContext, menu.menu.ScreenMode, data);
        }


        public static void SetSelectedNiche(GameContext gameContext, NicheType nicheType)
        {
            var menu = GetMenu(gameContext);

            var data = menu.menu.Data;

            if ((NicheType)data[Balance.MENU_SELECTED_NICHE] == nicheType)
                return;

            data[Balance.MENU_SELECTED_NICHE] = nicheType;

            UpdateScreen(gameContext, menu.menu.ScreenMode, data);
        }
    }
}
