using Assets.Utils;

public class RenderCompanyProfit : UpgradedParameterView
{
    public override string RenderHint()
    {
        var income = CompanyEconomyUtils.GetCompanyIncome(SelectedCompany, GameContext);
        var maintenance = CompanyEconomyUtils.GetTeamMaintenance(SelectedCompany);
        var marketingMaintenanceCombined = CompanyEconomyUtils.GetCompanyMarketingMaintenance(SelectedCompany, GameContext);


        var bonus = new BonusContainer("Balance change")
            .Append("Income", income)
            .AppendAndHideIfZero("Team maintenance", -maintenance)
            .AppendAndHideIfZero("Marketing Expenses", -marketingMaintenanceCombined);

        return bonus.ToString();
    }

    public override string RenderValue()
    {
        var change = CompanyEconomyUtils.GetBalanceChange(SelectedCompany, GameContext);

        return Visuals.PositiveOrNegativeMinified(change);
    }
}
