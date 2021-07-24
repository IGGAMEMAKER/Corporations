using UnityEngine;

namespace Assets.Core
{
    public static partial class Teams
    {
        //public static float GetRelationshipsWith(GameContext gameContext, GameEntity company, int humanId)
        //{
        //    var CEO = GetWorkerByRole(company, WorkerRole.CEO, gameContext);

        //    if (CEO != null)
        //    {
        //        if (CEO.personalRelationships.Relations.ContainsKey(humanId))
        //            return CEO.personalRelationships.Relations[humanId];
        //        else
        //            return 0;
        //    }

        //    return 0;
        //}

        //public static float GetRelationshipsWith(GameContext gameContext, GameEntity company, WorkerRole workerRole)
        //{
        //    var worker = GetWorkerByRole(company, workerRole, gameContext);

        //    if (worker == null)
        //    {
        //        return 0;
        //    }

        //    return GetRelationshipsWith(gameContext, company, worker.human.Id);
        //}
    }
}
