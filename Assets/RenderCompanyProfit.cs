using Assets.Utils;

public class RenderCompanyProfit : UpgradedParameterView
{
    public override string RenderHint()
    {
        var income = EconomyUtils.GetCompanyIncome(SelectedCompany, GameContext);
        var maintenance = EconomyUtils.GetProductCompanyMaintenance(SelectedCompany, GameContext);

        var devCost = EconomyUtils.GetProductDevelopmentCost(SelectedCompany, GameContext);
        var marketingCost = EconomyUtils.GetProductMarketingCost(SelectedCompany, GameContext);


        var bonus = new BonusContainer("Balance change")
            .Append("Income", income)
            .Append("Development Financing", -devCost)
            .Append("Marketing Financing", -marketingCost);

        return bonus.ToString();
    }

    public override string RenderValue()
    {
        var profit = EconomyUtils.GetProfit(SelectedCompany, GameContext);

        return Visuals.PositiveOrNegativeMinified(profit);
    }
}
