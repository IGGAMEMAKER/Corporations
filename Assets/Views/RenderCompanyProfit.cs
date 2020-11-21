using Assets.Core;

public class RenderCompanyProfit : UpgradedParameterView
{
    public override string RenderHint()
    {
        var company = SelectedCompany;

        //var income = Economy.GetIncome(Q, company);

        //var bonus = new Bonus<long>("Balance change")
        //    .Append("Income", income);

        var bonus = Economy.GetProfit(Q, company, true);

        //if (company.hasProduct)
        //{
        //    var prodMnt = Economy.GetProductCompanyMaintenance(company, true);

        //    foreach (var p in prodMnt.bonusDescriptions)
        //        bonus.AppendAndHideIfZero(p.Name, -p.Value);
        //}
        //else
        //    bonus.AppendAndHideIfZero("Maintenance", -Economy.GetMaintenance(Q, company));

        bonus.MinifyValues();
        bonus.SortByModule();

        return bonus.ToString();
    }

    public override string RenderValue()
    {
        var profit = Economy.GetProfit(Q, SelectedCompany);

        return Visuals.PositiveOrNegativeMinified(profit);
    }
}
