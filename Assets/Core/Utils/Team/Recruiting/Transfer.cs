using UnityEngine;

namespace Assets.Core
{
    public static partial class Teams
    {
        public static void TransferWorker(GameEntity company, GameEntity worker, WorkerRole role, int fromId, int toId, GameContext gameContext)
        {
            AttachHumanToTeam(company, gameContext, worker, role, toId);
            DetachHumanFromTeam(company.team.Teams[fromId], worker.human.Id);
        }
    }
}
