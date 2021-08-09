using UnityEngine;

namespace Assets.Core
{
    public static partial class Teams
    {
        public static void DismissTeam(GameEntity company, GameContext gameContext)
        {
            Companies.Log(company, "Dismiss team");
            //Debug.Log("DISMISS TEAM WORKS BAD!" + company.company.Name);
            //Debug.LogWarning("DISMISS TEAM WORKS BAD!" + company.company.Name);
        }

        public static void FireManager(GameContext gameContext, HumanFF worker) => FireManager(Companies.Get(gameContext, worker.WorkerComponent.companyId), gameContext, worker.HumanComponent.Id);
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
            team.Managers.RemoveAll(h => h.HumanComponent.Id == humanId);
            team.Roles.Remove(humanId);
        }
    }
}
