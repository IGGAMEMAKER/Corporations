using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Core
{
    public static partial class Teams
    {
        public static void HireManager(GameEntity company, WorkerRole workerRole)
        {
            var worker = Humans.GenerateHuman(Contexts.sharedInstance.game, workerRole);

            worker.worker.companyId = company.company.Id;

            AttachToTeam(company, worker.human.Id, workerRole);

            Humans.AttachToCompany(worker, company.company.Id, workerRole);
        }

        public static void ReduceOrganisationPoints(GameEntity company, int points)
        {
            var o = company.team.Organisation;

            company.team.Organisation = Mathf.Clamp(o - points, 0, 100);
        }

        public static void ChangeMorale(GameEntity company, int change)
        {
            var morale = company.team.Morale;
            company.team.Morale = Mathf.Clamp(morale + change, 0, 100);
        }

        public static float GetTeamChangeImpact(GameEntity company)
        {
            var workers = company.team.Workers[WorkerRole.Programmer];

            // +1 to prevent zero division
            return 1f / (workers + 1);
        }

        public static void HireRegularWorker(GameEntity company, WorkerRole workerRole = WorkerRole.Programmer)
        {
            company.team.Workers[workerRole]++;

            ReduceOrganisationPoints(company, (int)(100 * GetTeamChangeImpact(company)));
            ChangeMorale(company, (int)(50 * GetTeamChangeImpact(company)));
        }

        public static void FireRegularWorker(GameEntity company, WorkerRole workerRole = WorkerRole.Programmer)
        {
            if (company.team.Workers[workerRole] > 0)
            {
                company.team.Workers[workerRole]--;

                ReduceOrganisationPoints(company, (int)(100 * GetTeamChangeImpact(company)));
                ChangeMorale(company, (int)(-60 * GetTeamChangeImpact(company)));
            }
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

            foreach (var humanId in company.employee.Managers.Keys)
                Humans.GetHuman(gameContext, humanId).Destroy();

            var roles = GetAvailableRoles(company);

            for (var i = 0; i < roles.Count / 2; i++)
            {
                var index = Random.Range(0, roles.Count - 1);

                var role = roles[index];

                var worker = Humans.GenerateHuman(gameContext, role);
                var humanId = worker.human.Id;

                Debug.Log($"human #{i} - {role}. Human Id = {humanId}");

                company.employee.Managers[humanId] = role;
            }
        }





        public static int GetWorkerByRole(GameEntity company, GameContext gameContext, WorkerRole workerRole)
        {
            return Humans.GetHumanByWorkerRoleInCompany(gameContext, company.company.Id, workerRole);
        }

        // --------------


        public static void AttachToTeam(GameEntity company, int humanId, WorkerRole role)
        {
            var team = company.team;

            team.Managers[humanId] = role;

            ReplaceTeam(company, team);

            //Debug.Log($"Hire to " + company.company.Name + ": " + role.ToString());
        }

        public static void DismissTeam(GameEntity company, GameContext gameContext)
        {
            Debug.Log("DismissTeam of " + company.company.Name);

            var workers = company.team.Managers.Keys.ToArray();

            for (var i = workers.Length - 1; i > 0; i--)
                FireWorker(company, gameContext, workers[i]);
        }

        public static void FireWorker(GameEntity company, GameEntity worker)
        {
            Debug.Log("Fire worker from " + company.company.Name + " " + worker.worker.WorkerRole); // + " " + worker.human.Name

            Humans.LeaveCompany(worker);

            var team = company.team;

            team.Managers.Remove(worker.human.Id);

            ReplaceTeam(company, team);
        }

        public static void FireWorker(GameEntity company, GameContext gameContext, int humanId) => FireWorker(company, Humans.GetHuman(gameContext, humanId));

        public static void FireWorker(GameEntity company, GameContext gameContext, WorkerRole workerRole)
        {
            var workerId = GetWorkerByRole(company, gameContext, workerRole);

            if (workerId > 0)
                FireWorker(company, gameContext, workerId);
        }
    }
}
