using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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

        public static List<WorkerRole> GetAvailableRoles(GameEntity company)
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
                    break;

                case CompanyType.FinancialGroup:
                    break;
            }

            return roles;
        }

        public static void ShaffleEmployees(GameEntity company, GameContext gameContext)
        {
            Debug.Log("ShaffleEmployees: " + company.company.Name + " " + company.company.Id);
            Debug.Log("Remove old" + company.company.Name + " " + company.company.Id);

            foreach (var humanId in company.employee.Managers.Keys)
                HumanUtils.GetHumanById(gameContext, humanId).Destroy();

            var roles = GetAvailableRoles(company);

            Debug.Log("Roles" + company.company.Name + " " + company.company.Id);
            for (var i = 0; i < roles.Count / 2; i++)
            {
                var index = UnityEngine.Random.Range(0, roles.Count - 1);

                var role = roles[index];

                var worker = HumanUtils.GenerateHuman(gameContext, role);
                var humanId = worker.human.Id;
                Debug.Log($"human #{i} - {role}. Human Id = {humanId}");

                company.employee.Managers[humanId] = role;
            }
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
