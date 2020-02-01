using Assets.Core;

public class RenderCompanyProfit : UpgradedParameterView
{
    public override string RenderHint()
    {
        var income = Economy.GetCompanyIncome(Q, SelectedCompany);
        var maintenance = Economy.GetCompanyMaintenance(Q, SelectedCompany);

        var bonus = new Bonus<long>("Balance change")
            .Append("Income", income)
            //.AppendAndHideIfZero("Maintenance cost", SelectedCompany.hasProduct ? -Economy.GetDevelopmentCost(SelectedCompany, GameContext) : 0)
            .AppendAndHideIfZero("Maintenance", -maintenance)
            .MinifyValues();

        return bonus.ToString();
    }

    public override string RenderValue()
    {
        var profit = Economy.GetProfit(Q, SelectedCompany);

        return Visuals.PositiveOrNegativeMinified(profit);
    }
}
