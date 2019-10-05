using Assets.Utils;

public class RenderCompanyProfit : UpgradedParameterView
{
    public override string RenderHint()
    {
        var income = EconomyUtils.GetCompanyIncome(SelectedCompany, GameContext);
        var marketingMaintenance = EconomyUtils.GetProductCompanyMaintenance(SelectedCompany, GameContext);


        var bonus = new BonusContainer("Balance change")
            .Append("Income", income)
            .AppendAndHideIfZero("Maintenance", -marketingMaintenance);
            //.AppendAndHideIfZero("Marketing Expenses", -marketingMaintenance);

        return bonus.ToString();
    }

    public override string RenderValue()
    {
        var change = EconomyUtils.GetBalanceChange(SelectedCompany, GameContext);

        return Visuals.PositiveOrNegativeMinified(change);
    }
}
