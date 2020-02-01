using Assets.Core;
using System.Linq;

public class ShowBiggestIncomeOnMarketView : UpgradedParameterView
{
    public override string RenderHint()
    {
        return "";
    }

    public override string RenderValue()
    {
        var players = Markets.GetProductsOnMarket(Q, SelectedNiche);

        var productCompany = players
            .OrderByDescending(p => Economy.GetProductCompanyIncome(p, Q))
            .FirstOrDefault();

        if (productCompany == null)
            return "Market is FREE";

        var income = Economy.GetProductCompanyIncome(productCompany, Q);

        return $"{Format.Money(income)}"; //\n{productCompany.company.Name}";
    }
}
