using Assets.Utils;
using System.Linq;

public class ShowBiggestIncomeOnMarketView : UpgradedParameterView
{
    public override string RenderHint()
    {
        return "";
    }

    public override string RenderValue()
    {
        var players = NicheUtils.GetProductsOnMarket(GameContext, SelectedNiche);

        var productCompany = players
            .OrderByDescending(p => EconomyUtils.GetProductCompanyIncome(p, GameContext))
            .FirstOrDefault();

        if (productCompany == null)
            return "Market is FREE";

        var income = EconomyUtils.GetProductCompanyIncome(productCompany, GameContext);

        return $"{Format.Money(income)}\n{productCompany.company.Name}";
    }
}
