using System;
using Entitas;

namespace Assets.Utils
{
    public static class ScheduleUtils
    {
        private static GameEntity GetDateContainer(GameContext gameContext)
        {
            return gameContext.GetEntities(GameMatcher.Date)[0];
        }
        public static int GetCurrentDate(GameContext gameContext)
        {
            return GetDateContainer(gameContext).date.Date;
        }

        public static TaskComponent GenerateTaskComponent(GameContext gameContext, TaskType taskType, int duration)
        {
            int currentDate = GetCurrentDate(gameContext); // contexts.game.GetEntities(GameMatcher.Date)[0].date.Date;

            return new TaskComponent
            {
                Duration = duration,
                isCompleted = false,
                TaskType = taskType,
                StartTime = currentDate,
                EndTime = currentDate + duration
            };
        }

        internal static void ListenDateChanges(GameContext gameContext, IAnyDateListener menuListener)
        {
            GetDateContainer(gameContext).AddAnyDateListener(menuListener);
        }
    }
}
