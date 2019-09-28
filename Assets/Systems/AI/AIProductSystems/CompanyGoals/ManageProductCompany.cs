using Assets.Utils;

public partial class AIProductSystems : OnDateChange
{
    void ManageProductDevelopment(GameEntity product)
    {
        UpgradeSegment(product);

        ScaleTeamIfPossible(product);

        PickTeamUpgrades(product);
    }



    long GetProfit(GameEntity company)
    {
        return CompanyEconomyUtils.GetBalanceChange(company, gameContext);
    }

    bool IsCanAffordTeamImprovement(GameEntity product, TeamUpgrade teamUpgrade)
    {
        var cost = TeamUtils.GetImprovementCost(gameContext, product, teamUpgrade);

        return CompanyUtils.IsEnoughResources(product, cost);
    }

    //void ApplyImprovementSet

    void PickImprovementIfCan(GameEntity product, TeamUpgrade teamUpgrade)
    {
        if (IsCanAffordTeamImprovement(product, teamUpgrade))
            TeamUtils.PickTeamImprovement(product, teamUpgrade);
    }

    void ManageBigTeam(GameEntity product)
    {
        TeamUtils.PickTeamImprovement(product, TeamUpgrade.DevelopmentPolishedApp);
        TeamUtils.PickTeamImprovement(product, TeamUpgrade.DevelopmentCrossplatform);
        TeamUtils.PickTeamImprovement(product, TeamUpgrade.DevelopmentPolishedApp);

        PickImprovementIfCan(product, TeamUpgrade.MarketingBase);
        PickImprovementIfCan(product, TeamUpgrade.MarketingAggressive);
    }

    void ManageSmallTeam(GameEntity product)
    {
        TeamUtils.DisableTeamImprovement(product, TeamUpgrade.DevelopmentPrototype);
        TeamUtils.PickTeamImprovement(product, TeamUpgrade.DevelopmentPolishedApp);

        PickImprovementIfCan(product, TeamUpgrade.MarketingBase);
    }

    void ManagePairOfWorkers(GameEntity product)
    {
        TeamUtils.PickTeamImprovement(product, TeamUpgrade.DevelopmentPrototype);

        PickImprovementIfCan(product, TeamUpgrade.MarketingBase);
    }

    void PickTeamUpgrades(GameEntity product)
    {
        switch (product.team.TeamStatus)
        {
            case TeamStatus.Pair: ManagePairOfWorkers(product); break;
            case TeamStatus.SmallTeam: ManageSmallTeam(product); break;
            case TeamStatus.BigTeam: ManageBigTeam(product); break;

            default: ManageBigTeam(product); break;
        }
    }

    void ScaleTeamIfPossible(GameEntity product)
    {
        var cost = TeamUtils.GetTeamPromotionCost(product).money;

        var profit = GetProfit(product);

        if (profit > cost * 15 / 10)
            TeamUtils.Promote(product);
    }

    void ManageInvestors(GameEntity product)
    {
        // taking investments
        TakeInvestments(product);
    }
}
