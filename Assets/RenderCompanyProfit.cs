using Assets.Utils;

public class RenderCompanyProfit : UpgradedParameterView
{
    public override string RenderHint()
    {
        var income = CompanyEconomyUtils.GetCompanyIncome(SelectedCompany, GameContext);
        var team = CompanyEconomyUtils.GetTeamMaintenance(SelectedCompany);
        var marketingMaintenance = CompanyEconomyUtils.GetCompanyMarketingMaintenance(SelectedCompany, GameContext);
        var support = CompanyEconomyUtils.GetClientSupportCost(SelectedCompany, GameContext);


        var bonus = new BonusContainer("Balance change")
            .Append("Income", income)
            .AppendAndHideIfZero("Team maintenance", -team)
            .AppendAndHideIfZero("Marketing Expenses", -marketingMaintenance)
            .AppendAndHideIfZero("Support maintenance", -support);

        return bonus.ToString();
    }

    public override string RenderValue()
    {
        var change = CompanyEconomyUtils.GetBalanceChange(SelectedCompany, GameContext);

        return Visuals.PositiveOrNegativeMinified(change);
    }
}
