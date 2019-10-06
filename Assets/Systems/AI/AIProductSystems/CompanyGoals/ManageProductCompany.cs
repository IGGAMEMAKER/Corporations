using Assets.Utils;

public partial class AIProductSystems
{
    void ManageProductDevelopment(GameEntity product)
    {
        UpgradeSegment(product);
    }

    void PickTeamUpgrades(GameEntity product)
    {
        PickImprovementIfCan(product, TeamUpgrade.DevelopmentCrossplatform);
        PickImprovementIfCan(product, TeamUpgrade.DevelopmentPolishedApp);
        PickImprovementIfCan(product, TeamUpgrade.DevelopmentPrototype);
    }

    bool IsCanAffordTeamImprovement(GameEntity product, TeamUpgrade teamUpgrade)
    {
        var cost = TeamUtils.GetImprovementCost(gameContext, product, teamUpgrade);

        return GetIncome(product) >= cost;
    }

    void PickImprovementIfCan(GameEntity product, TeamUpgrade teamUpgrade)
    {
        if (IsCanAffordTeamImprovement(product, teamUpgrade))
            TeamUtils.PickTeamImprovement(product, teamUpgrade);
    }
}
