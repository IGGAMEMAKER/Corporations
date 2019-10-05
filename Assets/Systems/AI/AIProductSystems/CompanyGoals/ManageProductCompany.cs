using Assets.Utils;

public partial class AIProductSystems
{
    void ManageProductDevelopment(GameEntity product)
    {
        UpgradeSegment(product);
    }

    //void RemoveObsoletteImprovements(GameEntity product, )

    void ManageBigTeam(GameEntity product)
    {
        PickImprovementIfCan(product, TeamUpgrade.DevelopmentCrossplatform);

        if (TeamUtils.IsUpgradePicked(product, TeamUpgrade.DevelopmentCrossplatform))
            TeamUtils.DisableTeamImprovement(product, TeamUpgrade.DevelopmentPolishedApp);

        //PickImprovementIfCan(product, TeamUpgrade.MarketingBase);
        //PickImprovementIfCan(product, TeamUpgrade.MarketingAggressive);
    }

    void ManageSmallTeam(GameEntity product)
    {
        PickImprovementIfCan(product, TeamUpgrade.DevelopmentPolishedApp);

        if (TeamUtils.IsUpgradePicked(product, TeamUpgrade.DevelopmentPolishedApp))
            TeamUtils.DisableTeamImprovement(product, TeamUpgrade.DevelopmentPrototype);

        //PickImprovementIfCan(product, TeamUpgrade.MarketingBase);
    }

    void ManageSoloDeveloper(GameEntity product)
    {
        PickImprovementIfCan(product, TeamUpgrade.DevelopmentPrototype);
    }

    void ManagePairOfWorkers(GameEntity product)
    {
        PickImprovementIfCan(product, TeamUpgrade.DevelopmentPrototype);
    }

    void PickTeamUpgrades(GameEntity product)
    {
        PickImprovementIfCan(product, TeamUpgrade.DevelopmentCrossplatform);
        PickImprovementIfCan(product, TeamUpgrade.DevelopmentPolishedApp);
        PickImprovementIfCan(product, TeamUpgrade.DevelopmentPrototype);

        //switch (product.team.TeamStatus)
        //{
        //    case TeamStatus.Solo: ManageSoloDeveloper(product); break;
        //    case TeamStatus.Pair: ManagePairOfWorkers(product); break;
        //    case TeamStatus.SmallTeam: ManageSmallTeam(product); break;
        //    case TeamStatus.BigTeam: ManageBigTeam(product); break;

        //    default: ManageBigTeam(product); break;
        //}
    }

    void ScaleTeamIfPossible(GameEntity product)
    {
        var cost = EconomyUtils.GetPromotedTeamMaintenance(product);

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
