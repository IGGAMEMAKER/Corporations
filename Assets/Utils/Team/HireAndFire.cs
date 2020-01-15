using UnityEngine;

namespace Assets.Core
{
    public static partial class Teams
    {
        // managers
        public static void HireManager(GameEntity company, WorkerRole workerRole) => HireManager(company, Humans.GenerateHuman(Contexts.sharedInstance.game, workerRole));
        public static void HireManager(GameEntity company, GameEntity worker)
        {
            var role = Humans.GetRole(worker);

            AttachToTeam(company, worker, role);

        }

        public static void AttachToTeam(GameEntity company, GameEntity worker, WorkerRole role)
        {
            var team = company.team;

            var humanId = worker.human.Id;

            team.Managers[humanId] = role;

            ReplaceTeam(company, team);

            Humans.AttachToCompany(worker, company.company.Id, role);
        }

        // regular workers
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
    }
}
