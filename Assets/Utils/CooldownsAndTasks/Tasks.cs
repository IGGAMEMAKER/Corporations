using Entitas;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Core
{
    partial class Cooldowns
    {
        public static GameEntity[] GetTasks(GameContext gameContext)
        {
            return gameContext.GetEntities(GameMatcher.Task);
        }
        public static TaskComponent GetTask(GameContext gameContext, CompanyTask companyTask)
        {
            var tasks = GetTasks(gameContext);

            var task = Array.Find(tasks, t => t.task.CompanyTask.Equals(companyTask));

            return task?.task;
        }

        public static IEnumerable<GameEntity> GetTasksOfCompany(GameContext gameContext, int companyId)
        {
            return GetTasks(gameContext)
                .Where(t => t.task.CompanyTask.CompanyId == companyId);
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
    }
}
