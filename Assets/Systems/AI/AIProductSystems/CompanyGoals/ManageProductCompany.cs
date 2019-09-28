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

    void ManagePairOfWorkers(GameEntity product)
    {

    }

    void PickTeamUpgrades(GameEntity product)
    {

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
