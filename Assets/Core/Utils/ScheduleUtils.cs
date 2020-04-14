using System;
using Entitas;

namespace Assets.Core
{
    public static class ScheduleUtils
    {
        public static GameEntity GetDateContainer(GameContext gameContext)
        {
            var entities = gameContext.GetEntities(GameMatcher.Date);

            if (entities.Length == 0)
                return null;

            return entities[0];
        }

        public static int GetCurrentDate(GameContext gameContext)
        {
            return GetDateContainer(gameContext).date.Date;
        }

        public static bool IsTimerRunning(GameContext gameContext)
        {
            return GetDateContainer(gameContext).isTimerRunning;
        }

        public static void ToggleTimer(GameContext gameContext)
        {
            var container = GetDateContainer(gameContext);

            if (container.isTimerRunning)
                PauseGame(gameContext);
            else
                ResumeGame(gameContext);
        }

        public static void PauseGame(GameContext gameContext)
        {
            var container = GetDateContainer(gameContext);

            container.isTimerRunning = false;
        }

        public static void IncreaseDate(GameContext gameContext, int increment)
        {
            var container = GetDateContainer(gameContext);

            container.ReplaceDate(container.date.Date + increment);
        }

        public static void ResumeGame(GameContext gameContext, int date = -1, int currentSpeed = -1)
        {
            var container = GetDateContainer(gameContext);

            container.isTimerRunning = true;

            if (date >= 0 && currentSpeed > 0)
            {
                container.ReplaceSpeed(currentSpeed);
            }
        }

        public static bool IsPeriodEnd(GameContext gameContext)
        {
            var date = GetCurrentDate(gameContext);

            return date % Balance.PERIOD == 0;
        }

        // was IAnyDateListener
        public static void ListenDateChanges(GameContext gameContext, IAnyDateListener menuListener)
        {
            GetDateContainer(gameContext).AddAnyDateListener(menuListener);
        }

        // was IAnyDateListener
        public static void UnsubscribeFromDateChanges(GameContext gameContext, IAnyDateListener menuListener)
        {
            GetDateContainer(gameContext).RemoveAnyDateListener(menuListener);
        }


        // Date
        public static int GetDateByYear(int year) => (year - Balance.START_YEAR) * 360;
        public static int GetYearOf(int date) => date / 360 + Balance.START_YEAR;

        public static int GetDateByYearAndQuarter(int year, int quarter) => GetDateByYear(year) + quarter * 90;


        public static float GetTaskCompletionPercentage(GameContext gameContext, TimedActionComponent taskComponent)
        {
            int CurrentIntDate = GetCurrentDate(gameContext);

            return (CurrentIntDate - taskComponent.StartTime) * 100f / taskComponent.Duration;
        }



        public static GameEntity[] GetTasks(GameContext gameContext, CompanyTask taskType)
        {
            // TODO: add filtering tasks, which are done by other players!

            GameEntity[] gameEntities = Cooldowns.GetTasks(gameContext);

            return Array.FindAll(gameEntities, e => e.timedAction.CompanyTask == taskType);
        }

        public static TimedActionComponent GetTask(GameContext gameContext, CompanyTask taskType)
        {
            GameEntity[] tasks = GetTasks(gameContext, taskType);

            if (tasks.Length == 0)
                return null;

            return tasks[0].timedAction;
        }


        // campaign stats
        public static CampaignStatsComponent GetCampaignStats(GameContext gameContext)
        {
            return gameContext.GetEntities(GameMatcher.CampaignStats)[0].campaignStats;
        }

        public static void TweakCampaignStats(GameContext gameContext, CampaignStat stat)
        {
            GetCampaignStats(gameContext).Stats[stat]++;
        }
    }
}
