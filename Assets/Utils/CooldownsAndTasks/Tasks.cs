using Entitas;
using System;

namespace Assets.Core
{
    partial class Cooldowns
    {
        public static GameEntity[] GetTasks(GameContext gameContext)
        {
            return gameContext.GetEntities(GameMatcher.Task);
        }

        public static void AddTask(GameContext gameContext, CompanyTask companyTask, int duration)
        {
            if (IsHasTask(gameContext, companyTask))
                return;

            var e = gameContext.CreateEntity();

            var start = ScheduleUtils.GetCurrentDate(gameContext);
            e.AddTask(false, companyTask, start, duration, start + duration);
        }



        public static bool CanAddTask(GameContext gameContext, CompanyTask companyTask)
        {
            return !IsHasTask(gameContext, companyTask);
        }
        public static bool IsHasTask(GameContext gameContext, CompanyTask companyTask)
        {
            var task = GetTask(gameContext, companyTask);

            return task != null;
        }
        public static TaskComponent GetTask(GameContext gameContext, CompanyTask companyTask)
        {
            var tasks = GetTasks(gameContext);

            var task = Array.Find(tasks, t => t.task.CompanyTask.Equals(companyTask));

            return task?.task;
        }
    }
}
