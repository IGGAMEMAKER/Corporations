using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Core
{
    public static partial class Teams
    {
        // managers
        public static void HireManager(GameEntity company, GameContext gameContext, WorkerRole workerRole) => HireManager(company, Humans.GenerateHuman(gameContext, workerRole));
        public static void HireManager(GameEntity company, GameEntity worker)
        {
            var role = Humans.GetRole(worker);

            AttachToTeam(company, worker, role);

            company.employee.Managers.Remove(worker.human.Id);
        }

        public static void HuntManager(GameEntity worker, GameEntity newCompany, GameContext gameContext)
        {
            FireManager(gameContext, worker);

            AttachToTeam(newCompany, worker, Humans.GetRole(worker));
        }

        public static void AttachToTeam(GameEntity company, GameEntity worker, WorkerRole role)
        {
            // add humanId to team
            var team = company.team;

            var humanId = worker.human.Id;

            team.Managers[humanId] = role;

            ReplaceTeam(company, team);

            // add companyId to human
            Humans.AttachToCompany(worker, company.company.Id, role);
        }

        public static void DismissTeam(GameEntity company, GameContext gameContext)
        {
            Debug.Log("DismissTeam of " + company.company.Name);

            var workers = company.team.Managers.Keys.ToArray();

            for (var i = workers.Length - 1; i > 0; i--)
                FireManager(company, gameContext, workers[i]);
        }

        public static void FireManager(GameContext gameContext, GameEntity worker) => FireManager(Companies.Get(gameContext, worker.worker.companyId), worker);
        public static void FireManager(GameEntity company, GameContext gameContext, int humanId) => FireManager(company, Humans.GetHuman(gameContext, humanId));
        public static void FireManager(GameEntity company, GameEntity worker)
        {
            //Debug.Log("Fire worker from " + company.company.Name + " " + worker.worker.WorkerRole); // + " " + worker.human.Name

            var team = company.team;

            team.Managers.Remove(worker.human.Id);

            ReplaceTeam(company, team);

            Humans.LeaveCompany(worker);
        }

        // worker roles
        static string RenderBonus(long b) => Visuals.Positive(b.ToString());
        static string RenderBonus(float b) => Visuals.Positive(b.ToString());

        public static string GetRoleDescription(WorkerRole role, GameContext gameContext, bool isUnemployed, GameEntity company = null)
        {
            var description = "";
            bool employed = !isUnemployed;

            switch (role)
            {
                case WorkerRole.CEO:
                    description = $"Increases innovation chances";
                    if (employed)
                        description += $" by {RenderBonus(Teams.GetCEOInnovationBonus(company, gameContext))}%";
                    break;

                case WorkerRole.TeamLead:
                    description = $"Increases team speed";
                    if (employed)
                        description += $" by {RenderBonus(Teams.GetTeamLeadDevelopmentTimeDiscount(gameContext, company))}%";
                    break;

                case WorkerRole.MarketingLead:
                    description = $"Makes marketing cheaper";
                    if (employed)
                        description += $" by {RenderBonus(Teams.GetMarketingLeadBonus(company, gameContext))}%";
                    break;

                case WorkerRole.ProductManager:
                    description = $"Increases innovation chances";
                    if (employed)
                        description += $" by {RenderBonus(Teams.GetProductManagerBonus(company, gameContext))}%";
                    break;
                case WorkerRole.ProjectManager:
                    description = $"Reduces amount of workers";
                    if (employed)
                        description += $" by {RenderBonus(Teams.GetProjectManagerWorkersDiscount(company, gameContext))}%";
                    break;

                case WorkerRole.MarketingDirector:
                case WorkerRole.TechDirector:
                case WorkerRole.Universal:
                default:
                    description = "";
                    break;
            }

            return description;
        }


        public static float GetRelationshipsWith(GameContext gameContext, GameEntity company, int humanId)
        {
            var CEO = GetWorkerByRole(company, WorkerRole.CEO, gameContext);

            if (CEO != null)
            {
                if (CEO.personalRelationships.Relations.ContainsKey(humanId))
                    return CEO.personalRelationships.Relations[humanId];
                else
                    return 0;
            }

            return 0;
        }

        public static float GetRelationshipsWith(GameContext gameContext, GameEntity company, WorkerRole workerRole)
        {
            var worker = GetWorkerByRole(company, workerRole, gameContext);

            if (worker == null)
            {
                return 0;
            }

            return GetRelationshipsWith(gameContext, company, worker.human.Id);
        }
    }
}
