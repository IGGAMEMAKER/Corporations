using Entitas;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Core
{
    public static partial class ScreenUtils
    {
        // TODO DUPLICATE CODE
        public static void SetIntegerWithoutUpdatingScreen(GameContext gameContext, int value, string parameterName)
        {
            var menu = GetMenu(gameContext);

            var data = menu.menu.Data;

            // ??if value didn't change, then do nothing?
            if (data.ContainsKey(parameterName) && Convert.ToInt32(data[parameterName]) == value)
                return;

            data[parameterName] = value;
        }
        public static void SetInteger(GameContext gameContext, int value, string parameterName)
        {
            var menu = GetMenu(gameContext);

            var data = menu.menu.Data;

            // ??if value didn't change, then do nothing?
            if (data.ContainsKey(parameterName) && Convert.ToInt32(data[parameterName]) == value)
                return;

            data[parameterName] = value;

            UpdateScreen(gameContext, menu.menu.ScreenMode, data);
        }

        // set selected stuff
        public static void SetSelectedHuman(GameContext gameContext, int humanId)
        {
            SetInteger(gameContext, humanId, C.MENU_SELECTED_HUMAN);
        }


        public static void SetMainPanelId(GameContext gameContext, int panelId)
        {
            SetInteger(gameContext, panelId, C.MENU_SELECTED_MAIN_SCREEN_PANEL_ID);
        }

        public static void SetSelectedTeam(GameContext gameContext, int teamId)
        {
            SetInteger(gameContext, teamId, C.MENU_SELECTED_TEAM);
        }

        public static void SetSelectedCompany(GameContext gameContext, int companyId)
        {
            SetInteger(gameContext, companyId, C.MENU_SELECTED_COMPANY);
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
