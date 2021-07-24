using UnityEngine;

namespace Assets.Core
{
    public static partial class Teams
    {
        public static GameEntity HireManager(GameEntity company, GameContext gameContext, WorkerRole workerRole, int teamId) => HireManager(company, gameContext, Humans.GenerateHuman(gameContext, workerRole), teamId);
        public static GameEntity HireManager(GameEntity company, GameContext gameContext, GameEntity worker, int teamId)
        {
            var role = Humans.GetRole(worker);

            AttachToCompany(company, gameContext, worker, role, teamId);

            company.employee.Managers.Remove(worker.human.Id);

            return worker;
        }

        public static void HuntManager(GameEntity worker, GameEntity newCompany, GameContext gameContext, int teamId)
        {
            FireManager(gameContext, worker);

            AttachToCompany(newCompany, gameContext, worker, Humans.GetRole(worker), teamId);
        }

        public static void AttachHumanToTeam(GameEntity company, GameContext gameContext, GameEntity worker, WorkerRole role, int teamId)
        {
            var team = company.team.Teams[teamId];

            var humanId = worker.human.Id;

            team.Managers.Add(humanId);
            team.Roles[humanId] = role;

            ReplaceTeam(company, gameContext, company.team);
        }

        public static void AttachToCompany(GameEntity company, GameContext gameContext, GameEntity worker, WorkerRole role, int teamId)
        {
            // add humanId to team
            AttachHumanToTeam(company, gameContext, worker, role, teamId);

            // add companyId to human
            Humans.AttachToCompany(worker, company.company.Id, role);
        }




        public static void TransferWorker(GameEntity company, GameEntity worker, WorkerRole role, int fromId, int toId, GameContext gameContext)
        {
            AttachHumanToTeam(company, gameContext, worker, role, toId);
            DetachHumanFromTeam(company.team.Teams[fromId], worker.human.Id);
        }



        public static void DismissTeam(GameEntity company, GameContext gameContext)
        {
            Companies.Log(company, "Dismiss team");
            //Debug.Log("DISMISS TEAM WORKS BAD!" + company.company.Name);
            //Debug.LogWarning("DISMISS TEAM WORKS BAD!" + company.company.Name);
        }

        // ---------------------- FIRE ---------------------------------------------

        public static void FireManager(GameContext gameContext, GameEntity worker) => FireManager(Companies.Get(gameContext, worker.worker.companyId), worker);
        public static void FireManager(GameEntity company, GameContext gameContext, int humanId) => FireManager(company, Humans.Get(gameContext, humanId));
        public static void FireManager(GameEntity company, GameEntity worker)
        {
            foreach (var team in company.team.Teams)
            {
                //team.Managers.Remove(worker.human.Id);
                //team.Roles.Remove(worker.human.Id);

                DetachHumanFromTeam(team, worker.human.Id);
            }

            Humans.LeaveCompany(worker);
        }

        public static void DetachHumanFromTeam(TeamInfo team, int humanId)
        {
            team.Managers.Remove(humanId);
            team.Roles.Remove(humanId);
        }
    }
}
