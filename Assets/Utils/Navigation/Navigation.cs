using Entitas;
using System.Collections.Generic;

namespace Assets.Utils
{
    public static partial class ScreenUtils
    {
        public static void UpdateHistory(GameContext context, ScreenMode screenMode, Dictionary<string, object> data)
        {
            var history = GetNavigationHistory(context);

            var q = history.navigationHistory.Queries;

            var c = new MenuComponent { ScreenMode = screenMode, Data = new Dictionary<string, object>() };

            foreach (var p in data)
                c.Data[p.Key] = p.Value;

            q.Add(c);

            history.ReplaceNavigationHistory(q);
        }

        public static void Navigate(GameContext context, ScreenMode newScreen, Dictionary<string, object> newData)
        {
            var menu = GetMenu(context);

            UpdateHistory(context, newScreen, newData);
            UpdateScreen(context, newScreen, newData);

            SoundManager.Play(Sound.Hover);
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

        //string names = String.Join(",", q.Select(e => e.ScreenMode).ToArray());

        public static void NavigateBack(GameContext context)
        {
            var history = GetNavigationHistory(context);

            var q = history.navigationHistory.Queries;

            if (q.Count < 2)
                return;


            var destination = q[q.Count - 1];

            q.RemoveAt(q.Count - 1);

            history.ReplaceNavigationHistory(q);

            UpdateScreenWithoutAnyChanges(context);
            //UpdateScreen(context, destination.ScreenMode, destination.Data);
        }


        public static void UpdateScreen(GameContext context, ScreenMode screenMode, Dictionary<string, object> data)
        {
            ReplaceMenu(context, screenMode, data);
        }

        public static void UpdateScreenWithoutAnyChanges(GameContext gameContext)
        {
            var m = GetMenu(gameContext);

            ReplaceMenu(gameContext, m.menu.ScreenMode, m.menu.Data);
        }

        public static GameEntity GetNavigationHistory(GameContext context)
        {
            return context.GetEntities(GameMatcher.NavigationHistory)[0];
        }
    }
}
