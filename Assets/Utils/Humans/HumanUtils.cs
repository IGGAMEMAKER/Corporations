using Entitas;
using System;

namespace Assets.Utils
{
    public static partial class HumanUtils
    {
        public enum Ambition
        {
            EarnMoney,
            RuleProductCompany,

            CreateUnicorn,
            IPO,

            RuleCorporation
        }

        public static GameEntity[] GetHumans(GameContext gameContext)
        {
            return gameContext.GetEntities(GameMatcher.Human);
        }

        public static int GenerateHumanId(GameContext gameContext)
        {
            return GetHumans(gameContext).Length;
        }


        internal static int GetHumanByWorkerRoleInCompany(int companyId, GameContext gameContext, WorkerRole workerRole)
        {
            var humans = gameContext.GetEntities(GameMatcher.AllOf(GameMatcher.Human, GameMatcher.Worker));

            return Array.Find(humans, h => h.worker.companyId == companyId && h.worker.WorkerRole == workerRole).human.Id;
        }

        public static GameEntity GetHumanById(GameContext gameContext, int humanId)
        {
            return Array.Find(GetHumans(gameContext), h => h.human.Id == humanId);
        }

        public static WorkerRole GetRole(GameEntity worker)
        {
            return worker.worker.WorkerRole;
        }

        public static int GetOverallRating(int humanId, GameContext gameContext)
        {
            var worker = GetHumanById(gameContext, humanId);

            return GetOverallRating(worker, gameContext);
        }

        public static int GetOverallRating(GameEntity worker, GameContext gameContext)
        {
            var role = GetRole(worker);

            return GetWorkerRatingInRole(worker, role);
        }

        internal static string GetFullName(GameEntity human)
        {
            return $"{human.human.Name} {human.human.Surname}";
        }

        internal static void Recruit(GameEntity human, GameEntity company)
        {
            TeamUtils.HireWorker(company, human);
        }

        internal static bool IsWorksInCompany(GameEntity human, int id)
        {
            if (!human.hasWorker)
                return false;

            return human.worker.companyId == id;
        }

        internal static void SetRole(GameContext gameContext, int humanId, WorkerRole workerRole)
        {
            var human = GetHumanById(gameContext, humanId);

            if (human.hasWorker)
                human.ReplaceWorker(human.worker.companyId, workerRole);
        }

        public static GameEntity SetSkill(GameEntity worker, WorkerRole workerRole, int level)
        {
            var roles = worker.humanSkills.Roles;
            roles[workerRole] = level;

            worker.ReplaceHumanSkills(roles, worker.humanSkills.Traits, worker.humanSkills.Expertise);

            return worker;
        }

        public static GameEntity SetTrait(GameEntity worker, TraitType traitType, int level)
        {
            var traits = worker.humanSkills.Traits;
            traits[traitType] = level;

            worker.ReplaceHumanSkills(worker.humanSkills.Roles, traits, worker.humanSkills.Expertise);

            return worker;
        }

        public static void AttachToCompany(GameEntity worker, int companyId, WorkerRole workerRole)
        {
            if (!worker.hasWorker)
                worker.AddWorker(companyId, workerRole);
            else
                worker.ReplaceWorker(companyId, workerRole);
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

        public static Ambition GetFounderAmbition(int ambitions)
        {
            if (ambitions < 70)
                return Ambition.EarnMoney;

            if (ambitions < 75)
                return Ambition.RuleProductCompany;

            if (ambitions < 80)
                return Ambition.IPO;

            if (ambitions < 85)
                return Ambition.CreateUnicorn;

            return Ambition.RuleCorporation;
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
