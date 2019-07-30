using Assets.Utils;

public class RenderCompanyProfit : UpgradedParameterView
{
    public override string RenderHint()
    {
        var income = CompanyEconomyUtils.GetCompanyIncome(SelectedCompany, GameContext);
        var maintenance = CompanyEconomyUtils.GetCompanyMaintenance(SelectedCompany, GameContext);


        var marketingMaintenance = "";
        if (SelectedCompany.hasProduct)
            marketingMaintenance = "\nMarketing Expenses: " + Visuals.Negative(Format.Money(marketing));

        var hint = "Income: " + Visuals.Positive(Format.Money(income)) + "\n" +
            "Team Maintenance: " + Visuals.Negative(Format.Money(maintenance)) +
            marketingMaintenance;


        return hint;
    }

    public override string RenderValue()
    {
        var change = CompanyEconomyUtils.GetBalanceChange(SelectedCompany, GameContext) - marketing;

        return Visuals.PositiveOrNegativeMinified(change);
    }

    long marketing => CompanyEconomyUtils.GetAdsMaintenance(SelectedCompany, GameContext);
}
