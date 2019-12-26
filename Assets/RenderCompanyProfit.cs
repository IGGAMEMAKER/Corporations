using Assets.Core;

public class RenderCompanyProfit : UpgradedParameterView
{
    public override string RenderHint()
    {
        var income = Economy.GetCompanyIncome(SelectedCompany, GameContext);

        var maintenance = Economy.GetProductCompanyMaintenance(SelectedCompany, GameContext);

        var devCost = Economy.GetDevelopmentCost(SelectedCompany, GameContext);
        var marketingCost = Economy.GetMarketingCost(SelectedCompany, GameContext);


        var bonus = new Bonus<long>("Balance change")
            .Append("Income", income)
            .Append("Development cost", -devCost)
            .Append("Marketing cost", -marketingCost)
            .MinifyValues();

        return bonus.ToString();
    }

    public override string RenderValue()
    {
        var profit = Economy.GetProfit(SelectedCompany, GameContext);

        return Visuals.PositiveOrNegativeMinified(profit);
    }
}
