using System.Linq;
using UnityEngine;

namespace Assets.Core
{
    public static partial class TeamUtils
    {
        public static void HireManager(GameEntity company, WorkerRole workerRole)
        {
            var worker = HumanUtils.GenerateHuman(Contexts.sharedInstance.game);

            worker.AddWorker(company.company.Id, workerRole);

            HireWorker(company, worker);

            HumanUtils.SetSkills(worker, workerRole);
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




        public static GameEntity GetWorkerByWorkerRole(GameEntity company, GameContext gameContext, WorkerRole workerRole)
        {
            var team = company.team.Managers;

            GameEntity human = null;

            foreach (var w in team)
            {
                if (w.Value == workerRole)
                    human = HumanUtils.GetHumanById(gameContext, w.Key);
            }

            return human;
        }

        public static void HireWorker(GameEntity company, GameEntity worker)
        {
            var role = worker.worker.WorkerRole;

            AttachToTeam(company, worker.human.Id, role);

            HumanUtils.AttachToCompany(worker, company.company.Id, role);
        }


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
                FireWorker(company, workers[i], gameContext);
        }

        public static void FireWorker(GameEntity company, GameEntity worker)
        {
            Debug.Log("Fire worker from " + company.company.Name + " " + worker.worker.WorkerRole); // + " " + worker.human.Name

            HumanUtils.LeaveCompany(worker);

            var team = company.team;

            team.Managers.Remove(worker.human.Id);

            ReplaceTeam(company, team);

            //worker.Destroy();
        }

        public static void FireWorker(GameEntity company, int humanId, GameContext gameContext)
        {
            var human = HumanUtils.GetHumanById(gameContext, humanId);

            FireWorker(company, human);
        }

        public static void FireWorker(GameEntity company, GameContext gameContext, WorkerRole workerRole)
        {
            var workerId = GetWorkerByRole(company, gameContext, workerRole);

            if (workerId > 0)
                FireWorker(company, workerId, gameContext);
        }


        public static int GetWorkerByRole(GameEntity company, GameContext gameContext, WorkerRole workerRole)
        {
            return HumanUtils.GetHumanByWorkerRoleInCompany(company.company.Id, gameContext, workerRole);
        }

    }
}
