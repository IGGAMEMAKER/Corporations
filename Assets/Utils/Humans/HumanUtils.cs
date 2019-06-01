using Entitas;
using System;
using System.Collections.Generic;

namespace Assets.Utils
{
    public static partial class HumanUtils
    {
        public static GameEntity[] GetHumans(GameContext gameContext)
        {
            return gameContext.GetEntities(GameMatcher.Human);
        }

        public static int GenerateHumanId(GameContext gameContext)
        {
            return GetHumans(gameContext).Length;
        }

        public static GameEntity GetHumanById(GameContext gameContext, int humanId)
        {
            return Array.Find(GetHumans(gameContext), h => h.human.Id == humanId);
        }

        public static WorkerRole GetRole(GameContext gameContext, GameEntity worker)
        {
            var companyId = worker.worker.companyId;

            var c = CompanyUtils.GetCompanyById(gameContext, companyId);

            return c.team.Workers[worker.human.Id];
        }

        public static int GetOverallRating(GameEntity worker, GameContext gameContext)
        {
            var role = GetRole(gameContext, worker);

            return GetWorkerRatingInRole(worker, role);
        }



        public static GameEntity SetSkill(GameEntity worker, WorkerRole workerRole, int level)
        {
            var roles = worker.humanSkills.Roles;
            roles[workerRole] = level;

            worker.ReplaceHumanSkills(roles, worker.humanSkills.Traits, worker.humanSkills.Expertise);

            return worker;
        }

        public static void AttachToCompany(GameEntity worker, int companyId)
        {
            if (!worker.hasWorker)
                worker.AddWorker(companyId);
            else
                worker.ReplaceWorker(companyId);
        }

        internal static void LeaveCompany(GameContext gameContext, int humanId)
        {
            var human = GetHumanById(gameContext, humanId);

            LeaveCompany(human);
        }

        internal static void LeaveCompany(GameEntity human)
        {
            human.RemoveWorker();
        }

        public static string GetFormattedRole(WorkerRole role)
        {
            switch (role)
            {
                case WorkerRole.Business: return "CEO";
                case WorkerRole.Manager: return "Manager";
                case WorkerRole.Marketer: return "Marketer";
                case WorkerRole.MarketingDirector: return "Marketing Director";
                case WorkerRole.ProductManager: return "Product Manager";
                case WorkerRole.Programmer: return "Programmer";
                case WorkerRole.ProjectManager: return "Project Manager";
                case WorkerRole.TechDirector: return "Tech Director";

                case WorkerRole.Universal: return "Universal";

                default: return role.ToString();
            }
        }
    }
}
