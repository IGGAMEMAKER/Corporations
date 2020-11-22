using Assets.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public partial class ProductDevelopmentSystem : OnPeriodChange
{

    void TryAddTeam(GameEntity product, TeamType teamType)
    {
        var teamCost = Economy.GetSingleTeamCost();

        if (CanMaintain(product, teamCost) && !product.team.Teams.Any(t => t.TeamType == teamType))
        {
            //var id = Teams.AddTeam(product, teamType);

            //if (teamType == TeamType.DevelopmentTeam)
            //    SetTasks(product, ManagerTask.Polishing, id);

            //if (teamType == TeamType.MarketingTeam)
            //    SetTasks(product, ManagerTask.ViralSpread, id);

            //if (teamType == TeamType.ServersideTeam)
            //    SetTasks(product, ManagerTask.Organisation, id);
        }
    }

    void SetTasks(GameEntity product, ManagerTask managerTask, int teamId)
    {
        Teams.SetManagerTask(product, teamId, 0, managerTask);
        Teams.SetManagerTask(product, teamId, 1, managerTask);
        Teams.SetManagerTask(product, teamId, 2, managerTask);
    }

    void ExpandTeam(GameEntity product)
    {
        if (product.team.Teams.Count < 4)
        {
            TryAddTeam(product, TeamType.DevelopmentTeam);
            TryAddTeam(product, TeamType.MarketingTeam);
            TryAddTeam(product, TeamType.ServersideTeam);
        }
    }
}
