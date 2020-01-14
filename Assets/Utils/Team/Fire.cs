using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Core
{
    public static partial class Teams
    {
        public static void DismissTeam(GameEntity company, GameContext gameContext)
        {
            Debug.Log("DismissTeam of " + company.company.Name);

            var workers = company.team.Managers.Keys.ToArray();

            for (var i = workers.Length - 1; i > 0; i--)
                FireManager(company, gameContext, workers[i]);
        }

        public static void FireManager(GameEntity company, GameContext gameContext, int humanId) => FireManager(company, Humans.GetHuman(gameContext, humanId));
        public static void FireManager(GameEntity company, GameEntity worker)
        {
            Debug.Log("Fire worker from " + company.company.Name + " " + worker.worker.WorkerRole); // + " " + worker.human.Name

            Humans.LeaveCompany(worker);

            var team = company.team;

            team.Managers.Remove(worker.human.Id);

            ReplaceTeam(company, team);
        }
    }
}
