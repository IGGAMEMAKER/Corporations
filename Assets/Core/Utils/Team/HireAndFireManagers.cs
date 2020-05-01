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
    }
}
