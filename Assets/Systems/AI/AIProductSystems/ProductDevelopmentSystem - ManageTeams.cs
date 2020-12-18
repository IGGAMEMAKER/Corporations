using System;
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
        if (Teams.IsTeamPromotable(company, team) && CanMaintain(company, Economy.GetPromotedTeamCost(team)))
        {
            Teams.Promote(company, team);
        }
    }

    void HandleTeam(GameEntity product)
    {
        foreach (var t in product.team.Teams)
        {
            var time = DateTime.Now;
            TryUpgradeTeam(product, t);
            MeasureTag("Team Upgrades", time);

            time = DateTime.Now;
            Teams.FillTeam(product, gameContext, t);
            MeasureTag("Teams Fill", time);
        }

        //if (product.team.Teams.Count < 4)
        //{
        //    TryAddTeam(product, TeamType.DevelopmentTeam);
        //    TryAddTeam(product, TeamType.MarketingTeam);
        //    TryAddTeam(product, TeamType.ServersideTeam);
        //}
    }
}
