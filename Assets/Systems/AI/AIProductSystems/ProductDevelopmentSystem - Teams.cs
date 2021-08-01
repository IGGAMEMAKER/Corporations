using System;
using Assets.Core;

public partial class ProductDevelopmentSystem : OnPeriodChange
{
    void HandleTeam(GameEntity product)
    {
        foreach (var t in product.team.Teams)
        {
            var time = DateTime.Now;

            MeasureTag("Team Upgrades", time);

            time = DateTime.Now;
            Teams.FillTeam(product, gameContext, t);

            MeasureTag("Teams Fill", time);
        }
    }

    bool TryHireWorker(GameEntity product, WorkerRole role)
    {
        var maintenanceCost = 1f;
        var index = product.team.Teams
            .FindIndex(t => Teams.GetDirectManagementCostOfTeam(t, product, gameContext).Sum() > maintenanceCost);

        if (index == -1)
        {
            Teams.AddTeam(product, gameContext, TeamType.CrossfunctionalTeam);
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
