using Assets.Core;

public class RenderCompanyProfit : UpgradedParameterView
{
    public override string RenderHint()
    {
        var company = SelectedCompany;

        var income = Economy.GetCompanyIncome(Q, company);

        var bonus = new Bonus<long>("Balance change")
            .Append("Income", income);

        if (company.hasProduct)
        {
            var prodMnt = Economy.GetProductCompanyMaintenance(company, Q, true);

            foreach (var p in prodMnt.bonusDescriptions)
                bonus.AppendAndHideIfZero(p.Name, -p.Value);
        }
        else
            bonus.AppendAndHideIfZero("Maintenance", -Economy.GetCompanyMaintenance(Q, company));

        bonus.MinifyValues();

        return bonus.ToString();
    }

    public override string RenderValue()
    {
        var profit = Economy.GetProfit(Q, SelectedCompany);

        return Visuals.PositiveOrNegativeMinified(profit);
    }
}
