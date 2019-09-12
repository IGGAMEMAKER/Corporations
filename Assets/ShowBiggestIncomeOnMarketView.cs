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
        var players = NicheUtils.GetPlayersOnMarket(GameContext, SelectedNiche);

        var productCompany = players.OrderByDescending(p => CompanyEconomyUtils.GetProductCompanyIncome(p, GameContext)).FirstOrDefault();

        if (productCompany == null)
            return "Market is FREE";

        var income = CompanyEconomyUtils.GetProductCompanyIncome(productCompany, GameContext);

        return $"{productCompany.company.Name} ({Format.Money(income)})";
    }
}
