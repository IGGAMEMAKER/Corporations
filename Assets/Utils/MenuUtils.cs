using Entitas;
using System;

namespace Assets.Utils
{
    public static class MenuUtils
    {
        public static GameEntity GetMenu(GameContext gameContext)
        {
            return gameContext.GetEntities(GameMatcher.Menu)[0];
        }

        public static IndustryType GetIndustry(GameContext gameContext)
        {
            return (IndustryType)GetMenu(gameContext)?.menu?.Data;
        }

        public static NicheType GetNiche(GameContext gameContext)
        {
            return (NicheType)GetMenu(gameContext)?.menu?.Data;
        }

        public static void SetSelectedCompany(int companyId, GameContext gameContext)
        {
            gameContext.GetEntities(GameMatcher.SelectedCompany)[0].isSelectedCompany = false;

            var company = Array.Find(gameContext.GetEntities(GameMatcher.Company), c => c.company.Id == companyId);

            company.isSelectedCompany = true;
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

        public static void UpdateHistory(GameContext context, ScreenMode screenMode, object data)
        {
            var history = GetNavigationHistory(context);

            var q = history.navigationHistory.Queries;
            q.Add(new MenuComponent { Data = data, ScreenMode = screenMode });

            history.ReplaceNavigationHistory(q);
        }

        public static void Navigate(GameContext context, ScreenMode screenMode, object data)
        {
            var menu = GetMenu(context);

            UpdateHistory(context, menu.menu.ScreenMode, menu.menu.Data);

            UpdateScreen(context, screenMode, data);
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
