using Entitas;

namespace Assets.Utils.Tutorial
{
    public static class TutorialUtils
    {
        static GameEntity GetProgression(GameContext gameContext)
        {
            return gameContext.GetEntities(GameMatcher.Tutorial)[0];
        }

        internal static bool IsOpenedFunctionality(GameContext gameContext, TutorialFunctionality tutorialFunctionality)
        {
            return GetProgression(gameContext).tutorial.progress.ContainsKey(tutorialFunctionality);
        }

        internal static void Unlock(GameContext gameContext, TutorialFunctionality tutorialFunctionality)
        {
            var p = GetProgression(gameContext);

            var tutorial = p.tutorial.progress;

            tutorial[tutorialFunctionality] = true;

            p.ReplaceTutorial(tutorial);
        }

        public static void AddEventListener(GameContext gameContext, ITutorialListener tutorialListener)
        {
            var p = GetProgression(gameContext);

            p.AddTutorialListener(tutorialListener);
        }

        public static void RemoveEventListener(GameContext gameContext, ITutorialListener tutorialListener)
        {
            var p = GetProgression(gameContext);

            p.RemoveTutorialListener(tutorialListener);
        }
    }
}
