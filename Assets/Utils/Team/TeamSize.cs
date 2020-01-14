using System.Linq;

namespace Assets.Core
{
    public static partial class TeamUtils
    {
        public static int CountSpecialists(GameEntity company, WorkerRole workerRole)
        {
            return company.team.Managers.Values.ToArray().Count(w => w == workerRole);
        }

        public static int GetAmountOfWorkers(GameEntity e, GameContext gameContext)
        {
            return e.team.Workers[WorkerRole.Programmer];
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
    }
}
