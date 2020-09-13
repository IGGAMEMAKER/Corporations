using System;
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

            int managerTasks = company.team.Teams.Select(t => t.ManagerTasks).Select(t => t.Contains(ManagerTask.Recruiting) ? 1 : 0).Sum();

            foreach (var role in roles)
            {
                AddEmployee(company, gameContext, managerTasks, role);
            }
        }

        public static void AddEmployee(GameEntity company, GameContext gameContext, int managerTasks, WorkerRole role)
        {
            var worker = Humans.GenerateHuman(gameContext, role);

            // Control rating levels for new workers
            #region Control rating levels for new workers
            var averageStrength = GetTeamAverageStrength(company, gameContext);

            var recruitingEffort = managerTasks * 5f / company.team.Teams.Count;

            var rng = UnityEngine.Random.Range(averageStrength - 5, averageStrength + 1 + recruitingEffort);
            var rating = Mathf.Clamp(rng, C.BASE_MANAGER_RATING, 75);
            //GetHRBasedNewManagerRatingBonus

            Humans.ResetSkills(worker, (int)rating);
            #endregion

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

                    //var prototype = !company.isRelease;

                    //if (prototype)
                    //    roles = new List<WorkerRole> { WorkerRole.CEO };
                    break;

                case CompanyType.FinancialGroup:
                    break;
            }

            return roles;
        }

        public static WorkerRole[] GetRolesForTeam(TeamType teamType)
        {
            switch (teamType)
            {
                case TeamType.SupportTeam:
                case TeamType.MarketingTeam: return new WorkerRole[] { WorkerRole.MarketingLead };

                case TeamType.DevelopmentTeam: return new WorkerRole[] { WorkerRole.TeamLead, WorkerRole.ProductManager };
                case TeamType.DevOpsTeam: return new WorkerRole[] { WorkerRole.TeamLead };

                default:
                    return new WorkerRole[] { WorkerRole.CEO, WorkerRole.ProjectManager, WorkerRole.TeamLead, WorkerRole.MarketingLead, WorkerRole.ProductManager };
            }
        }

        public static Func<KeyValuePair<int, WorkerRole>, bool> RoleSuitsTeam(GameEntity company, TeamInfo team) => pair => IsRoleSuitsTeam(pair.Value, company, team);
        public static bool IsRoleSuitsTeam(WorkerRole workerRole, GameEntity company, TeamInfo team)
        {
            var roles = GetRolesForTeam(team.TeamType).ToList();

            if (team.ID == 0)
            {
                // core team
                roles.Remove(WorkerRole.ProjectManager);
                roles.Remove(WorkerRole.CEO);
            }
            else
            {
                // regular universal team
                if (IsUniversalTeam(team.TeamType))
                {
                    roles.Remove(WorkerRole.CEO);
                }
            }

            return roles.Contains(workerRole);
        }
    }
}
