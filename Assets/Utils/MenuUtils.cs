using Entitas;
using System;
using System.Collections.Generic;

namespace Assets.Utils
{
    public static class ScreenUtils
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

            return CompanyUtils.GetCompanyById(gameContext, companyId);
        }

        public static IndustryType GetSelectedIndustry(GameContext gameContext)
        {
            return (IndustryType)GetScreenData(gameContext)[Constants.MENU_SELECTED_INDUSTRY];
        }

        public static NicheType GetSelectedNiche(GameContext gameContext)
        {
            return (NicheType)GetScreenData(gameContext)[Constants.MENU_SELECTED_NICHE];
        }



        public static void SetSelectedCompany(GameContext gameContext, int companyId)
        {
            var menu = GetMenu(gameContext);

            menu.ReplaceMenu(menu.menu.ScreenMode, companyId);
        }


        public static GameEntity GetNavigationHistory(GameContext context)
        {
            return context.GetEntities(GameMatcher.NavigationHistory)[0];
        }

        public static void UpdateScreen(GameContext context, ScreenMode screenMode, object data)
        {
            var menu = GetMenu(context);

            menu.ReplaceMenu(screenMode, data);
        }

        public static void UpdateHistory(GameContext context, ScreenMode screenMode, Dictionary<string, object> data)
        {
            var history = GetNavigationHistory(context);

            var q = history.navigationHistory.Queries;
            q.Add(new MenuComponent { Data = data, ScreenMode = screenMode });

            history.ReplaceNavigationHistory(q);
        }

        public static void Navigate(GameContext context, ScreenMode newScreen, Dictionary<string, object> newData)
        {
            var menu = GetMenu(context);

            UpdateHistory(context, menu.menu.ScreenMode, menu.menu.Data);
            UpdateScreen(context, newScreen, newData);
        }

        public static void Navigate(GameContext context, ScreenMode screenMode)
        {
            // only changes screen
            Navigate(context, screenMode, GetScreenData(context));
        }

        public static void Navigate(GameContext context, ScreenMode screenMode, string field, object data)
        {
            var screenData = GetScreenData(context);

            screenData[field] = data;

            Navigate(context, screenMode, screenData);
        }

        public static void NavigateBack(GameContext context)
        {
            var history = GetNavigationHistory(context);

            var q = history.navigationHistory.Queries;

            if (q.Count == 0)
                return;

            var destination = q[q.Count - 1];

            q.RemoveAt(q.Count - 1);

            history.ReplaceNavigationHistory(q);

            UpdateScreen(context, destination.ScreenMode, destination.Data);
        }
    }
}
