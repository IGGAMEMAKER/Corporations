using Assets.Utils;
using System.Collections.Generic;

public partial class AIProductSystems : OnMonthChange
{
    public AIProductSystems(Contexts contexts) : base(contexts) {
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var e in CompanyUtils.GetProductCompanies(gameContext))
            UpgradeSegment(e);

        foreach (var e in CompanyUtils.GetAIProducts(gameContext))
            PickTeamUpgrades(e);

        foreach (var e in CompanyUtils.GetPlayerRelatedCompanies(gameContext))
            OperatePlayerRelatedProductCompany(e);
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

    void OperatePlayerRelatedProductCompany(GameEntity product)
    {
        if (product.isIndependentCompany)
            return;

        CompanyUtils.PayDividends(gameContext, product, GetProfit(product));
    }
}