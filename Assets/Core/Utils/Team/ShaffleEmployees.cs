using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Core
{
    public static partial class Teams
    {
        public static void ShaffleEmployees(GameEntity company, GameContext gameContext)
        {
            //Debug.Log("ShaffleEmployees: " + company.company.Name + " " + company.company.Id);

            #region remove previous employees
            foreach (var humanId in company.employee.Managers.Keys)
            {
                var h = Humans.GetHuman(gameContext, humanId);

                h.Destroy();
            }

            //Debug.Log("ShaffleEmployees: will remove " + company.employee.Managers.Keys.Count + " employees");

            company.employee.Managers.Clear();
            #endregion

            var roles = GetRolesTheoreticallyPossibleForThisCompanyType(company);

            var newRoles = new List<WorkerRole>();

            //for (var i = 0; i < Mathf.Min(2, roles.Count); i++)
            //{
            //    var index = Random.Range(0, roles.Count);
            //    var role = roles[index];

            //    // avoid picking same roles twice
            //    while (newRoles.Contains(role))
            //    {
            //        index = Random.Range(0, roles.Count);

            //        role = roles[index];
            //    }
            foreach (var role in roles) {
                newRoles.Add(role);

                var worker = Humans.GenerateHuman(gameContext, role);
                var humanId = worker.human.Id;

                // Control rating levels for new workers
                #region Control rating levels for new workers
                var averageStrength = GetTeamAverageStrength(company, gameContext);
                //var hrBasedRank = GetHRBasedNewManagerRating(company, gameContext);


                var rng = Random.Range(averageStrength - 25, averageStrength + 10);
                var rating = Mathf.Clamp(rng, C.BASE_MANAGER_RATING, 75);

                Humans.ResetSkills(worker, rating);
                #endregion

                //Debug.Log($"human #{i} - {role}. Human Id = {humanId}");

                company.employee.Managers[humanId] = role;
            }

            //Debug.Log("ShaffleEmployees: " + company.company.Name + " DONE");
        }

        public static int GetTeamAverageStrength(GameEntity company, GameContext gameContext)
        {
            var managers = company.team.Managers;

            if (managers.Count == 0)
                return 0;

            int rating = 0;

            foreach (var m in managers)
            {
                rating += Humans.GetRating(gameContext, m.Key);
            }

            var avg = rating / managers.Count;

            return avg;
        }

        public static Bonus<int> GetHRBasedNewManagerRatingBonus(GameEntity company, GameContext gameContext)
        {
            var bonus = new Bonus<int>("New manager rating");

            var culture = Companies.GetActualCorporateCulture(company, gameContext);
            var managingCompany = Companies.GetManagingCompanyOf(company, gameContext);

            var productsOfManagingCompany = Companies.GetDaughterProductCompanies(gameContext, managingCompany);

            bool hasGlobalMarkets = productsOfManagingCompany
                .Select(p => Markets.Get(gameContext, p))
                .Count(m => m.nicheBaseProfile.Profile.AudienceSize == AudienceSize.Global) > 0;

            bonus
                .Append("Base value", C.BASE_MANAGER_RATING)
                .Append("Mission", 0)
                //.Append("Salaries", culture[CorporatePolicy.SalariesLowOrHigh] * 2)
                .Append("Has Global Markets", hasGlobalMarkets ? 10 : 0)
                .Append("Is TOP10 Company", 0)
                .Append("Is TOP10 in teams", 0);

            return bonus;
        }

        public static int GetHRBasedNewManagerRating(GameEntity company, GameContext gameContext)
        {
            var bonus = GetHRBasedNewManagerRatingBonus(company, gameContext);

            return (int)bonus.Sum();
        }


        public static bool HasFreePlaceForWorker(GameEntity company, WorkerRole workerRole)
        {
            return !company.team.Managers.ContainsValue(workerRole);
        }

        public static List<WorkerRole> GetGroupRoles() => new List<WorkerRole>
        {
            WorkerRole.CEO,
            WorkerRole.MarketingDirector,
            WorkerRole.TechDirector
        };

        public static List<WorkerRole> GetProductCompanyRoles() => new List<WorkerRole>
        {
            WorkerRole.CEO,
            WorkerRole.MarketingLead,
            WorkerRole.TeamLead,
            WorkerRole.ProjectManager,
            WorkerRole.ProductManager
        };

        public static List<WorkerRole> GetRolesTheoreticallyPossibleForThisCompanyType(GameEntity company)
        {
            var roles = new List<WorkerRole>();

            switch (company.company.CompanyType)
            {
                case CompanyType.Corporation:
                case CompanyType.Group:
                case CompanyType.Holding:
                    roles = GetGroupRoles();
                    break;

                case CompanyType.ProductCompany:
                    roles = GetProductCompanyRoles();

                    var prototype = !company.isRelease;

                    if (prototype)
                        roles = new List<WorkerRole> { WorkerRole.CEO };
                    break;

                case CompanyType.FinancialGroup:
                    break;
            }

            return roles;
        }
    }
}
