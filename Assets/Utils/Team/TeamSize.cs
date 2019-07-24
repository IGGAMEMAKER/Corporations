using System;
using System.Linq;
using UnityEngine;

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

        public static long GetSalary(WorkerRole workerRole)
        {
            // TODO GET PROPER SALARIES FOR ALL ROLES

            switch (workerRole)
            {
                case WorkerRole.Business: return Constants.SALARIES_CEO;
                case WorkerRole.Manager: return Constants.SALARIES_MANAGER;
                case WorkerRole.Marketer: return Constants.SALARIES_MARKETER;

                case WorkerRole.ProductManager: return Constants.SALARIES_PRODUCT_PROJECT_MANAGER;
                case WorkerRole.ProjectManager: return Constants.SALARIES_PRODUCT_PROJECT_MANAGER;

                default: return Constants.SALARIES_DIRECTOR;
            }
        }

        internal static int GetAverageTeamStrength(GameContext gameContext, GameEntity company)
        {
            var strength = 0;
            var size = 0;
            foreach (var w in company.team.Workers)
            {
                var worker = HumanUtils.GetHumanById(gameContext, w.Key);

                strength += HumanUtils.GetWorkerRatingInRole(worker);
                size++;
            }

            if (size == 0)
                return 0;

            return strength / size;
        }

        internal static int GetAverageTeamRating(GameContext gameContext, GameEntity company)
        {
            var strength = GetAverageTeamStrength(gameContext, company);

            var min = 45f;
            var max = 90f;

            var diff = max - min;


            // 9 per star

            float relativeStrength = Mathf.Clamp(strength, min, max);

            var percent = (relativeStrength - min) / diff;

            var maxStars = 5;

            var rating = maxStars * percent;

            return (int)Mathf.Clamp(rating, 1, maxStars);
        }

        //public static GameEntity[] GetSpecialistsByRole(GameEntity company, WorkerRole workerRole, GameContext gameContext)
        //{
        //    company.team.Workers.Values.ToArray().Where(w => w == workerRole);
        //}

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

        public static bool IsWillOverextendTeam(GameEntity company)
        {
            return GetTeamSize(company) == GetTeamMaxSize(company);
        }
    }
}
