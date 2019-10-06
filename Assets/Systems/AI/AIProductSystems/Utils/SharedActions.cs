using Assets.Utils;
using Assets.Utils.Tutorial;

// actions used in multiple strategies
public partial class AIProductSystems
{
    void UpgradeSegment(GameEntity product)
    {
        ProductUtils.UpdgradeProduct(product, gameContext);

        //ProductUtils.UpgradeExpertise(product, gameContext);
    }

    long GetIncome(GameEntity company)
    {
        return EconomyUtils.GetCompanyIncome(company, gameContext);
    }

    long GetProfit(GameEntity company)
    {
        return EconomyUtils.GetBalanceChange(company, gameContext);
    }

    bool IsCanAffordTeamImprovement(GameEntity product, TeamUpgrade teamUpgrade)
    {
        var cost = TeamUtils.GetImprovementCost(gameContext, product, teamUpgrade);

        return GetIncome(product) >= cost;
        //return GetProfit(product) >= cost;
    }


    void PickImprovementIfCan(GameEntity product, TeamUpgrade teamUpgrade)
    {
        if (IsCanAffordTeamImprovement(product, teamUpgrade))
            TeamUtils.PickTeamImprovement(product, teamUpgrade);
    }
}
