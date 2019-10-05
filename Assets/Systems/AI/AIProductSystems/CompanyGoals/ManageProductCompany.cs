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

    void ManageInvestors(GameEntity product)
    {
        // taking investments
        TakeInvestments(product);
    }
}
