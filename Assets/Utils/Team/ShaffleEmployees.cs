using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Core
{
    public static partial class Teams
    {
        public static void ShaffleEmployees(GameEntity company, GameContext gameContext)
        {
            Debug.Log("ShaffleEmployees: " + company.company.Name + " " + company.company.Id);

            #region remove previous employees
            foreach (var humanId in company.employee.Managers.Keys)
            {
                var h = Humans.GetHuman(gameContext, humanId);
                
                h.Destroy();
            }

            Debug.Log("ShaffleEmployees: will remove " + company.employee.Managers.Keys.Count + " employees");

            company.employee.Managers.Clear();
            #endregion

            var roles = GetAvailableRoles(company);

            for (var i = 0; i < roles.Count; i++)
            {
                var index = Random.Range(0, roles.Count);

                var role = roles[index];

                var worker = Humans.GenerateHuman(gameContext, role);
                var humanId = worker.human.Id;

                Debug.Log($"human #{i} - {role}. Human Id = {humanId}");

                company.employee.Managers[humanId] = role;
            }

            Debug.Log("ShaffleEmployees: " + company.company.Name + " DONE");
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
    }
}
