using System.Collections.Generic;
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

            int managerTasks = company.team.Teams.Count(t => t.ManagerTasks.Contains(ManagerTask.Recruiting));

            foreach (var role in roles)
            {
                AddEmployee(company, gameContext, managerTasks, role);

                if (role == WorkerRole.Marketer || role == WorkerRole.Programmer)
                {
                    AddEmployee(company, gameContext, managerTasks, role);
                }
            }
        }

        public static float GetNewWorkerRandomRating(GameEntity company, GameContext gameContext, int managerTasks)
        {
            var hrRating = GetHRBasedNewManagerRating(company, gameContext);

            var recruitingEffort = managerTasks * 10f / company.team.Teams.Count;

            var rng = Random.Range(hrRating - 10, hrRating + 1 + recruitingEffort);

            var rating = Mathf.Clamp(rng, C.NEW_MANAGER_RATING_MIN, C.NEW_MANAGER_RATING_MAX);

            return rating;
        }

        public static void AddEmployee(GameEntity company, GameContext gameContext, int managerTasks, WorkerRole role)
        {
            // Control rating levels for new workers
            var worker = Humans.GenerateHuman(gameContext, role);

            var rating = GetNewWorkerRandomRating(company, gameContext, managerTasks);
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
            return GetHRBasedNewManagerRatingBonus(company, gameContext).Sum();
        }


        public static bool HasFreePlaceForWorker(GameEntity company, WorkerRole workerRole)
        {
            return !company.team.Managers.ContainsValue(workerRole);
        }



        public static void FillTeam2(GameEntity product, GameContext gameContext, TeamInfo t)
        {
            var leader = Teams.GetMainManagerRole(t);

            if (!Teams.HasMainManagerInTeam(t))
                TryHireWorker(product, leader, gameContext);

            // if can manage more workers
            TryHireWorker(product, WorkerRole.Programmer, gameContext);
            TryHireWorker(product, WorkerRole.Programmer, gameContext);
        }

        static bool TryHireWorker(GameEntity product, WorkerRole role, GameContext gameContext)
        {
            // can manage
            var maintenanceCost = 1.3f;
            var index = product.team.Teams.FindIndex(t =>
            {
                var cost = Teams.GetDirectManagementCostOfTeam(t, product, gameContext).Sum();

                if (cost == 0 && t.Managers.Count == 0)
                    return true;

                return cost > maintenanceCost;
            });

            // cannot manage
            if (index == -1)
            {
                Teams.AddTeam(product, gameContext, TeamType.CrossfunctionalTeam, 0);
                // TODO need to hire leader here, but it's Ok still

                index = product.team.Teams.Count - 1;
            }

            return HireWorker(product, role, index, gameContext);
        }

        static bool HireWorker(GameEntity product, WorkerRole role, int teamId, GameContext gameContext)
        {
            return Teams.TryToHire(product, gameContext, product.team.Teams[teamId], role);
        }

        public static void FillTeam(GameEntity company, GameContext gameContext, TeamInfo t)
        {
            var necessaryRoles = GetMissingRolesFull(t); //.ToList();

            /*necessaryRoles.Add(WorkerRole.Programmer);
            necessaryRoles.Add(WorkerRole.Marketer);*/

            if (necessaryRoles.Any())
            {
                var role = RandomItem(necessaryRoles); // necessaryRoles.First();

                TryToHire(company, gameContext, t, role);
            }
        }

        private static T RandomItem<T>(IEnumerable<T> necessaryRoles)
        {
            var count = necessaryRoles.Count();

            if (count == 0)
                return default;

            return necessaryRoles.ToList()[Random.Range(0, count)];
        }

        public static bool TryToHire(GameEntity company, GameContext gameContext, TeamInfo t, WorkerRole role)
        {
            var rating = GetTeamAverageStrength(company, gameContext) + Random.Range(-2, 3);
            var salary = GetSalaryPerRating(rating);

            if (Economy.IsCanMaintain(company, gameContext, salary))
            {
                // hire
                var human = HireManager(company, gameContext, role, t.ID);

                // set salary
                SetJobOffer(human, company, new JobOffer(salary), t.ID, gameContext);

                return true;
            }

            return false;
        }
    }
}
