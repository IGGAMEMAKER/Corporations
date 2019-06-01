using System.Linq;

namespace Assets.Utils
{
    public static partial class TeamUtils
    {
        public static int GetTeamMaxSize(GameEntity company)
        {
            switch (company.team.TeamStatus)
            {
                case TeamStatus.Solo:
                    return 1;

                case TeamStatus.Pair: return 2;

                case TeamStatus.SmallTeam: return 5;

                case TeamStatus.Department: return 20;

                default: return 11 + GetManagers(company) * 7;
            }
        }

        public static int CountSpecialists(GameEntity company, WorkerRole workerRole)
        {
            return company.team.Workers.Values.ToArray().Count(w => w == workerRole);
        }


        internal static int GetUniversals(GameEntity company)
        {
            return CountSpecialists(company, WorkerRole.Universal);
        }

        internal static int GetTopManagers(GameEntity company)
        {
            return 0;
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

        public static int GetTeamSize(GameEntity company)
        {
            return company.team.Workers.Count();
        }

        public static bool IsWillNotOverextendTeam(GameEntity company)
        {
            return GetTeamSize(company) + 1 < GetTeamMaxSize(company);
        }
    }
}
