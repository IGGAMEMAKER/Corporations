using System.Linq;
using UnityEngine;

namespace Assets.Core
{
    public static partial class Teams
    {
        public static void ShuffleEmployees(GameEntity company, GameContext gameContext)
        {
            //Debug.Log("ShuffleEmployees: " + company.company.Name + " " + company.company.Id);

            // remove previous employees
            foreach (var humanId in company.employee.Managers.Keys)
            {
                var h = Humans.Get(gameContext, humanId);

                h.Destroy();
            }

            company.employee.Managers.Clear();

            var roles = GetRolesTheoreticallyPossibleForThisCompanyType(company);

            int managerTasks = company.team.Teams.Select(t => t.ManagerTasks).Select(t => t.Contains(ManagerTask.Recruiting) ? 1 : 0).Sum();

            foreach (var role in roles)
            {
                AddEmployee(company, gameContext, managerTasks, role);
            }
        }

        public static float GetNewWorkerRandomRating(GameEntity company, GameContext gameContext, int managerTasks, WorkerRole role)
        {
            //var averageStrength = GetTeamAverageStrength(company, gameContext);
            var hrRating = GetHRBasedNewManagerRating(company, gameContext);

            var recruitingEffort = managerTasks * 10f / company.team.Teams.Count;

            var rng = UnityEngine.Random.Range(hrRating - 10, hrRating + 1 + recruitingEffort);

            var rating = Mathf.Clamp(rng, C.NEW_MANAGER_RATING_MIN, C.NEW_MANAGER_RATING_MAX);

            return rating;
        }

        public static void AddEmployee(GameEntity company, GameContext gameContext, int managerTasks, WorkerRole role)
        {
            // Control rating levels for new workers
            var worker = Humans.GenerateHuman(gameContext, role);

            var rating = GetNewWorkerRandomRating(company, gameContext, managerTasks, role);
            Humans.ResetSkills(worker, (int)rating);

            company.employee.Managers[worker.human.Id] = role;
        }

        public static int GetTeamAverageStrength(GameEntity company, GameContext gameContext)
        {
            int rating = 0;
            var counter = 0;

            foreach (var t in company.team.Teams)
            {
                foreach (var m in t.Managers)
                {
                    rating += Humans.GetRating(gameContext, m);
                    counter++;
                }
            }

            if (counter == 0)
                return 0;

            return rating / counter;
        }

        public static Bonus<int> GetHRBasedNewManagerRatingBonus(GameEntity company, GameContext gameContext)
        {
            var bonus = new Bonus<int>("New manager rating");

            var culture = Companies.GetActualCorporateCulture(company);
            var managingCompany = Companies.GetManagingCompanyOf(company, gameContext);

            var productsOfManagingCompany = Companies.GetDaughterProducts(gameContext, managingCompany);

            bool hasGlobalMarkets = productsOfManagingCompany
                .Select(p => Markets.Get(gameContext, p))
                .Count(m => m.nicheBaseProfile.Profile.AudienceSize == AudienceSize.Global) > 0;


            int positionOnMarket = 0;

            if (company.hasProduct)
            {
                var clampedPosition = Mathf.Clamp(Markets.GetPositionOnMarket(gameContext, company), 0, 5);

                positionOnMarket = (5 - clampedPosition) * 2;
            }

            bonus
                .Append("Base value", C.NEW_MANAGER_RATING_MIN)
                .Append("Mission", 0)

                .AppendAndHideIfZero("Position on market", positionOnMarket)

                .Append("Has Global Markets", hasGlobalMarkets ? 10 : 0)
                //.Append("Is TOP10 Company", 0)
                //.Append("Is TOP10 in teams", 0)
                ;

            return bonus;
        }

        public static int GetHRBasedNewManagerRating(GameEntity company, GameContext gameContext)
        {
            var bonus = GetHRBasedNewManagerRatingBonus(company, gameContext);

            return bonus.Sum();
        }


        public static bool HasFreePlaceForWorker(GameEntity company, WorkerRole workerRole)
        {
            return !company.team.Managers.ContainsValue(workerRole);
        }



        public static void FillTeam(GameEntity company, GameContext gameContext, TeamInfo t)
        {
            var necessaryRoles = GetMissingRoles(t);

            if (necessaryRoles.Any())
            {
                var rating = GetTeamAverageStrength(company, gameContext) + UnityEngine.Random.Range(-2, 3);
                var salary = GetSalaryPerRating(rating);

                if (Economy.IsCanMaintain(company, gameContext, salary))
                {
                    var human = HireManager(company, gameContext, necessaryRoles.First(), t.ID);

                    SetJobOffer(human, company, new JobOffer(salary), t.ID, gameContext);
                }
            }
        }
    }
}
