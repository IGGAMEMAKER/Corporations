using System;
using System.Linq;

namespace Assets.Core
{
    public static partial class Teams
    {
        public static int CountSpecialists(GameEntity company, WorkerRole workerRole)
        {
            return company.team.Managers.Values.ToArray().Count(w => w == workerRole);
        }

        public static int GetAmountOfWorkers(GameEntity e, GameContext gameContext)
        {
            return e.team.Workers[WorkerRole.Programmer] + e.team.Managers.Count;
        }



        internal static int GetUniversals(GameEntity company)
        {
            return CountSpecialists(company, WorkerRole.Universal);
        }

        public static int GetProgrammers(GameEntity company)
        {
            return CountSpecialists(company, WorkerRole.Programmer);
        }

        public static int GetManagers(GameEntity company)
        {
            return CountSpecialists(company, WorkerRole.Manager);
        }

        public static int GetMarketers(GameEntity company)
        {
            return CountSpecialists(company, WorkerRole.Marketer);
        }

        internal static GameEntity GetWorkerByRole(GameEntity company, WorkerRole role, GameContext gameContext)
        {
            var managers = company.team.Managers;

            foreach (var m in managers)
            {
                if (m.Value == role)
                    return Humans.GetHuman(gameContext, m.Key);
            }

            return null;
        }

        internal static int GetWorkerEffeciency(GameEntity worker, GameEntity company)
        {
            if (worker == null)
                return 0;

            var expertise = 0;

            if (company.hasProduct && worker.humanSkills.Expertise.ContainsKey(company.product.Niche))
                expertise = worker.humanSkills.Expertise[company.product.Niche];

            var adaptability = worker.humanCompanyRelationship.Adapted;

            return 100 + adaptability + expertise / 2;
        }
    }
}
