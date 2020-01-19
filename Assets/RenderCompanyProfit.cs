using Assets.Core;

public class RenderCompanyProfit : UpgradedParameterView
{
    public override string RenderHint()
    {
        var income = Economy.GetCompanyIncome(GameContext, SelectedCompany);
        var maintenance = Economy.GetCompanyMaintenance(GameContext, SelectedCompany);

        var bonus = new Bonus<long>("Balance change")
            .Append("Income", income)
            //.AppendAndHideIfZero("Maintenance cost", SelectedCompany.hasProduct ? -Economy.GetDevelopmentCost(SelectedCompany, GameContext) : 0)
            .AppendAndHideIfZero("Maintenance cost", maintenance)
            .MinifyValues();

        return bonus.ToString();
    }

    public override string RenderValue()
    {
        var profit = Economy.GetProfit(GameContext, SelectedCompany);

        return Visuals.PositiveOrNegativeMinified(profit);
    }
}
