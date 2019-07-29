using System.Linq;
using UnityEngine;

namespace Assets.Utils
{
    public static partial class TeamUtils
    {
        public static void HireWorker(GameEntity company, WorkerRole workerRole)
        {
            var worker = HumanUtils.GenerateHuman(Contexts.sharedInstance.game);

            worker.AddWorker(company.company.Id, workerRole);

            HireWorker(company, worker);

            HumanUtils.SetSkills(worker, workerRole);
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

            team.Workers[humanId] = role;

            ReplaceTeam(company, team);
        }

        public static void DismissTeam(GameEntity company, GameContext gameContext)
        {
            Debug.Log("DismissTeam of " + company.company.Name);

            var workers = company.team.Workers.Keys.ToArray();

            for (var i = workers.Length; i > 0; i--)
                FireWorker(company, workers[i], gameContext);
        }

        public static void FireWorker(GameEntity company, GameEntity worker)
        {
            //Debug.Log("Fire worker from " + company.company.Name + " " + worker.human.Name + " " + worker.worker.WorkerRole);

            HumanUtils.LeaveCompany(worker);

            var team = company.team;

            team.Workers.Remove(worker.human.Id);

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
