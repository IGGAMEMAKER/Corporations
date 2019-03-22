using Entitas;

namespace Assets.Utils
{
    public static class ScheduleUtils
    {
        public static int GetCurrentDate(GameContext gameContext)
        {
            return gameContext.GetEntities(GameMatcher.Date)[0].date.Date;
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
    }
}
