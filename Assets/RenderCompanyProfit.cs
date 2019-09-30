using Assets.Utils;

public class RenderCompanyProfit : UpgradedParameterView
{
    public override string RenderHint()
    {
        var income = EconomyUtils.GetCompanyIncome(SelectedCompany, GameContext);
        var team = EconomyUtils.GetTeamMaintenance(SelectedCompany);
        var marketingMaintenance = EconomyUtils.GetCompanyMarketingMaintenance(SelectedCompany, GameContext);
        var support = EconomyUtils.GetClientSupportCost(SelectedCompany, GameContext);


        var bonus = new BonusContainer("Balance change")
            .Append("Income", income)
            .AppendAndHideIfZero("Team maintenance", -team)
            .AppendAndHideIfZero("Marketing Expenses", -marketingMaintenance)
            .AppendAndHideIfZero("Support maintenance", -support);

        return bonus.ToString();
    }

    public override string RenderValue()
    {
        var change = EconomyUtils.GetBalanceChange(SelectedCompany, GameContext);

        return Visuals.PositiveOrNegativeMinified(change);
    }
}
