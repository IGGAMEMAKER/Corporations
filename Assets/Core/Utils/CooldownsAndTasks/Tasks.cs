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
            return gameContext.GetEntities(GameMatcher.AllOf(GameMatcher.TimedAction, GameMatcher.Task));
        }
        
        public static GameEntity[] GetTimedActions(GameContext gameContext)
        {
            return gameContext.GetEntities(GameMatcher.TimedAction);
        }

        public static GameEntity GetTaskContainer(GameContext gameContext, CompanyTask companyTask)
        {
            var tasks = GetTimedActions(gameContext);

            var task = Array.Find(tasks, t => t.timedAction.CompanyTask.Equals(companyTask));

            return task;
        }
        public static TimedActionComponent GetTask(GameContext gameContext, CompanyTask companyTask)
        {
            var task = GetTaskContainer(gameContext, companyTask);

            return task?.timedAction;
        }

        public static bool IsCanTweakCorporateCulture(GameEntity company, GameContext gameContext)
        {
            var task = Cooldowns.GetTask(gameContext, new CompanyTaskUpgradeCulture(company.company.Id));

            return task == null;
        }

        public static IEnumerable<GameEntity> GetTasksOfCompany(GameContext gameContext, int companyId)
        {
            return GetTasks(gameContext)
                .Where(t => t.timedAction.CompanyTask.CompanyId == companyId);
        }



        internal static GameEntity AddTimedAction(GameContext gameContext, CompanyTask companyTask, int duration)
        {
            if (HasTask(gameContext, companyTask))
                return null;

            var e = gameContext.CreateEntity();

            var start = ScheduleUtils.GetCurrentDate(gameContext);
            e.AddTimedAction(false, companyTask, start, duration, start + duration);

            return e;
        }

        public static void AddTask(GameContext gameContext, CompanyTask companyTask, int duration)
        {
            var t = AddTimedAction(gameContext, companyTask, duration);

            if (t != null)
                t.isTask = true;
        }


        public static bool HasTask(GameContext gameContext, CompanyTask companyTask)
        {
            var task = GetTask(gameContext, companyTask);

            return task != null;
        }
    }
}
