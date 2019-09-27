using Assets.Utils;
using System.Linq;

public class RenderGroupProfit : UpgradedParameterView
{
    public override string RenderHint()
    {
        var daughters = CompanyUtils.GetDaughterCompanies(GameContext, MyCompany.company.Id)
            .OrderByDescending(c => CompanyEconomyUtils.GetBalanceChange(c, GameContext));

        return string.Join("\n", daughters.Select(GetIncomeInfo));
    }

    public override string RenderValue()
    {
        var change = CompanyEconomyUtils.GetBalanceChange(MyCompany, GameContext);

        return Visuals.PositiveOrNegativeMinified(change);
    }

    string GetIncomeInfo(GameEntity c)
    {
        var profit = CompanyEconomyUtils.GetBalanceChange(c, GameContext);
        var formattedMoney = Format.Money(profit);

        return $"* {c.company.Name}: {Visuals.Describe(profit, "+" + formattedMoney, formattedMoney, "0")}";
    }
}
