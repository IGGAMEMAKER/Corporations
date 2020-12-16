using Assets.Core;

public partial class ProductDevelopmentSystem : OnPeriodChange
{
    void TryAddTeam(GameEntity product, TeamType teamType)
    {
        var teamCost = Economy.GetSingleTeamCost();

        if (CanMaintain(product, teamCost))
        {
            if (!Teams.IsCanAddMoreTeams(product, gameContext))
            {
                Companies.IncrementCorporatePolicy(gameContext, product, CorporatePolicy.DoOrDelegate);
            }

            if (Teams.IsCanAddMoreTeams(product, gameContext))
            {
                Teams.AddTeam(product, gameContext, teamType);
            }
        }
    }

    void TryUpgradeTeam(GameEntity company, TeamInfo team)
    {
        if (Teams.IsTeamPromotable(team) && CanMaintain(company, Economy.GetPromotedTeamCost(team)))
        {
            Teams.Promote(company, team);
        }
    }

    void SetTasks(GameEntity product, ManagerTask managerTask, int teamId)
    {
        Teams.SetManagerTask(product, teamId, 0, managerTask);
        Teams.SetManagerTask(product, teamId, 1, managerTask);
        Teams.SetManagerTask(product, teamId, 2, managerTask);
    }

    void HandleTeam(GameEntity product)
    {
        foreach (var t in product.team.Teams)
        {
            TryUpgradeTeam(product, t);

            Teams.FillTeam(product, gameContext, t);
        }

        //if (product.team.Teams.Count < 4)
        //{
        //    TryAddTeam(product, TeamType.DevelopmentTeam);
        //    TryAddTeam(product, TeamType.MarketingTeam);
        //    TryAddTeam(product, TeamType.ServersideTeam);
        //}
    }
}
