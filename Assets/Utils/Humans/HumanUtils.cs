using Entitas;
using System;
using System.Linq;

namespace Assets.Core
{
    public static partial class Humans
    {
        // queries
        public static GameEntity[] GetHumans(GameContext gameContext)
        {
            return gameContext.GetEntities(GameMatcher.Human);
        }

        public static int GenerateHumanId(GameContext gameContext)
        {
            var humans = GetHumans(gameContext);

            if (humans.Length == 0)
                return 0;

            return humans.Max(h => h.human.Id) + 1;
        }

        internal static string GetFullName(GameEntity human)
        {
            return $"{human.human.Name} {human.human.Surname}";
        }


        public static GameEntity GetHuman(GameContext gameContext, int humanId)
        {
            return Array.Find(GetHumans(gameContext), h => h.human.Id == humanId);
        }

        // actions
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

        // hire / fire
        internal static bool IsWorksInCompany(GameEntity human, int id)
        {
            if (!human.hasWorker)
                return false;

            return human.worker.companyId == id;
        }

        public static void AttachToCompany(GameEntity worker, int companyId, WorkerRole workerRole)
        {
            worker.ReplaceWorker(companyId, workerRole);
        }

        internal static void LeaveCompany(GameContext gameContext, int humanId) => LeaveCompany(GetHuman(gameContext, humanId));
        internal static void LeaveCompany(GameEntity human)
        {
            human.worker.companyId = -1;
        }
    }
}
