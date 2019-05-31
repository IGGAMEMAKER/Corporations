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

        public static int MaxXP {
            get
            {
                return 100;
            }
        }

        static int GetRandomXP()
        {
            return UnityEngine.Random.Range(40, 75);
        }

        static int GetRandomTrait()
        {
            return UnityEngine.Random.Range(35, 85);
        }

        public static int GetRandomProgrammingSkill()
        {
            return GetRandomXP();
        }

        public static Dictionary<TraitType, int> GenerateRandomTraits ()
        {
            var traits = new Dictionary<TraitType, int>
            {
                [TraitType.Ambitions] = GetRandomTrait(),
                [TraitType.Charisma] = GetRandomTrait(),
                [TraitType.Discipline] = GetRandomTrait(),
                [TraitType.Education] = GetRandomTrait(),
                [TraitType.Vision] = GetRandomTrait(),
                [TraitType.Will] = GetRandomTrait(),
            };

            return traits;
        }

        public static GameEntity GenerateHuman(GameContext gameContext)
        {
            var e = gameContext.CreateEntity();

            int id = GenerateHumanId(gameContext);

            e.AddHuman(id, "Tom", "Stokes " + id);
            e.AddHumanSkills(
                new Dictionary<WorkerRole, int> {
                    [WorkerRole.Business] = GetRandomXP(),
                    [WorkerRole.Manager] = GetRandomXP(),
                    [WorkerRole.Marketer] = GetRandomXP(),
                    [WorkerRole.Programmer] = 10,
                },
                GenerateRandomTraits(),
                new Dictionary<NicheType, int>()
                );

            return e;
        }

        public static GameEntity SetSkill(GameEntity worker, WorkerRole workerRole, int level)
        {
            var roles = worker.humanSkills.Roles;
            roles[workerRole] = level;

            worker.ReplaceHumanSkills(roles, worker.humanSkills.Traits, worker.humanSkills.Expertise);

            return worker;
        }

        public static void AttachToCompany(GameEntity worker, int companyId, WorkerRole workerRole)
        {
            if (!worker.hasWorker)
                worker.AddWorker(workerRole);
            else
                worker.ReplaceWorker(workerRole);
        }

        public static int GetOverallRating (GameEntity worker)
        {
            var role = worker.worker.WorkerRole;

            return GetWorkerRatingInRole(worker, role);
        }

        public static int GetWorkerRatingInRole (GameEntity worker, WorkerRole workerRole)
        {
            var skills = worker.humanSkills.Roles;

            var marketing = skills[WorkerRole.Marketer];
            var business = skills[WorkerRole.Business];
            var coding = skills[WorkerRole.Programmer];
            var management = skills[WorkerRole.Manager];
            var vision = worker.humanSkills.Traits[TraitType.Vision];

            switch (workerRole)
            {
                case WorkerRole.MarketingDirector: return (marketing * 3 + business * 2 + management * 3 + vision * 2) / 10;
                case WorkerRole.TechDirector: return (coding * 4 + business * 1 + management * 5) / 10;

                case WorkerRole.ProductManager: return (vision * 5 + business * 2 + management * 3) / 10;
                case WorkerRole.ProjectManager: return (vision * 2 + business * 3 + management * 5) / 10;
                case WorkerRole.Business: return (vision * 3 + business * 7) / 10;
                case WorkerRole.Universal: return (coding * 3 + business * 2 + vision * 3 + marketing * 2) / 10;

                default: return skills[workerRole];
            }
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
