using Entitas;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Core
{
    public static partial class ScreenUtils
    {
        public static void SetInteger(GameContext gameContext, int value, string parameterName)
        {
            var menu = GetMenu(gameContext);

            var data = menu.menu.Data;

            if (Convert.ToInt32(data[parameterName]) == value)
                return;

            data[parameterName] = value;

            UpdateScreen(gameContext, menu.menu.ScreenMode, data);
        }

        // set selected stuff
        public static void SetSelectedHuman(GameContext gameContext, int humanId)
        {
            SetInteger(gameContext, humanId, C.MENU_SELECTED_HUMAN);
            //var menu = GetMenu(gameContext);

            //var data = menu.menu.Data;

            //if ((int)data[C.MENU_SELECTED_HUMAN] == humanId)
            //    return;

            //data[C.MENU_SELECTED_HUMAN] = humanId;

            //UpdateScreen(gameContext, menu.menu.ScreenMode, data);
        }


        public static void SetMainPanelId(GameContext gameContext, int panelId)
        {
            SetInteger(gameContext, panelId, C.MENU_SELECTED_MAIN_SCREEN_PANEL_ID);
        }

        public static void SetSelectedTeam(GameContext gameContext, int teamId)
        {
            SetInteger(gameContext, teamId, C.MENU_SELECTED_TEAM);
            //var menu = GetMenu(gameContext);

            //var data = menu.menu.Data;

            //if (Convert.ToInt32(data[C.MENU_SELECTED_TEAM]) == teamId)
            //    return;

            //data[C.MENU_SELECTED_TEAM] = teamId;

            //UpdateScreen(gameContext, menu.menu.ScreenMode, data);
        }

        public static void SetSelectedCompany(GameContext gameContext, int companyId)
        {
            SetInteger(gameContext, companyId, C.MENU_SELECTED_COMPANY);
            //var menu = GetMenu(gameContext);

            //var data = menu.menu.Data;

            //if ((int)data[C.MENU_SELECTED_COMPANY] == companyId)
            //    return;

            //data[C.MENU_SELECTED_COMPANY] = companyId;

            //UpdateScreen(gameContext, menu.menu.ScreenMode, data);
        }


        public static void SetSelectedNiche(GameContext gameContext, NicheType nicheType)
        {
            var menu = GetMenu(gameContext);

            var data = menu.menu.Data;

            if ((NicheType)data[C.MENU_SELECTED_NICHE] == nicheType)
                return;

            data[C.MENU_SELECTED_NICHE] = nicheType;

            UpdateScreen(gameContext, menu.menu.ScreenMode, data);
        }
    }
}
