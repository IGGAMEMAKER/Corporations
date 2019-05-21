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

        public static bool IsTimerRunning(GameContext gameContext)
        {
            return GetDateContainer(gameContext).isTimerRunning;
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

        public static float GetTaskCompletionPercentage(GameContext gameContext, TaskComponent taskComponent)
        {
            int CurrentIntDate = GetCurrentDate(gameContext);

            return (CurrentIntDate - taskComponent.StartTime) * 100f / taskComponent.Duration;
        }

        static GameEntity[] GetTasks(GameContext gameContext, TaskType taskType)
        {
            // TODO: add filtering tasks, which are done by other players!

            GameEntity[] gameEntities = gameContext.GetEntities(GameMatcher.Task);

            return Array.FindAll(gameEntities, e => e.task.TaskType == taskType);
        }

        public static TaskComponent GetTask(GameContext gameContext, TaskType taskType)
        {
            GameEntity[] tasks = GetTasks(gameContext, taskType);

            if (tasks.Length == 0)
                return null;

            return tasks[0].task;
        }
    }
}
