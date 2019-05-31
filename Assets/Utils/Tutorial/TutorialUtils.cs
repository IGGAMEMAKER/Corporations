using Entitas;

namespace Assets.Utils.Tutorial
{
    public static class TutorialUtils
    {
        public static GameEntity GetProgression(GameContext gameContext)
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
    }
}
