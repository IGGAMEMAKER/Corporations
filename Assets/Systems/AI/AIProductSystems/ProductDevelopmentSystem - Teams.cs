using System;
using Assets.Core;
using UnityEngine;

public partial class ProductDevelopmentSystem : OnPeriodChange
{
    void HandleTeam(GameEntity product)
    {
        for (var i = 0; i < product.team.Teams.Count; i++)
        {
            var t = product.team.Teams[i];

            var time = DateTime.Now;

            Teams.FillTeam2(product, gameContext, t);

            MeasureTag("Teams Fill", time);
        }
    }

    bool TryHireWorker(GameEntity product, WorkerRole role)
    {
        // can manage
        var maintenanceCost = 1.3f;
        var index = product.team.Teams.FindIndex(t =>
        {
            var cost = Teams.GetDirectManagementCostOfTeam(t, product, gameContext).Sum();

            if (cost == 0 && t.Managers.Count == 0)
                return true;

            return cost > maintenanceCost;
        });

        // cannot manage
        if (index == -1)
        {
            Teams.AddTeam(product, gameContext, TeamType.CrossfunctionalTeam, 0);
            // TODO need to hire leader here, but it's Ok still

            index = product.team.Teams.Count - 1;
        }

        return HireWorker(product, role, index);
    }

    bool HireWorker(GameEntity product, WorkerRole role, int teamId)
    {
        return Teams.TryToHire(product, gameContext, product.team.Teams[teamId], role);
    }
}
