using Entitas;

namespace Assets.Core
{
    public static class TutorialUtils
    {
        static GameEntity GetProgression(GameContext gameContext)
        {
            return gameContext.GetEntities(GameMatcher.Tutorial)[0];
        }
        static GameEntity GetEventProgression(GameContext gameContext)
        {
            return gameContext.GetEntities(GameMatcher.EventContainer)[0];
        }



        public static bool IsOpenedFunctionality(GameContext gameContext, TutorialFunctionality tutorialFunctionality)
        {
            return GetProgression(gameContext).tutorial.progress.ContainsKey(tutorialFunctionality);
        }

        public static bool IsOpenedFunctionality(GameContext gameContext, string tutorialFunctionality)
        {
            return GetEventProgression(gameContext).eventContainer.progress.ContainsKey(tutorialFunctionality);
        }



        public static void Unlock(GameContext gameContext, TutorialFunctionality tutorialFunctionality)
        {
            var p = GetProgression(gameContext);

            var tutorial = p.tutorial.progress;

            tutorial[tutorialFunctionality] = true;

            p.ReplaceTutorial(tutorial);
        }

        public static void Unlock(GameContext gameContext, string tutorialFunctionality)
        {
            var p = GetEventProgression(gameContext);

            var tutorial = p.eventContainer.progress;

            tutorial[tutorialFunctionality] = true;

            p.ReplaceEventContainer(tutorial);
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
