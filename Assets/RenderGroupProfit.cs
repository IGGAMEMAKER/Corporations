using Assets.Utils;
using System.Linq;

public class RenderGroupProfit : UpgradedParameterView
{
    public override string RenderHint()
    {
        var daughters = Companies.GetDaughterCompanies(GameContext, MyCompany.company.Id)
            .OrderByDescending(c => Economy.GetProfit(c, GameContext));

        return string.Join("\n", daughters.Select(GetIncomeInfo));
    }

    public override string RenderValue()
    {
        var change = Economy.GetProfit(MyCompany, GameContext);

        return Visuals.PositiveOrNegativeMinified(change);
    }

    string GetIncomeInfo(GameEntity c)
    {
        var profit = Economy.GetProfit(c, GameContext);
        var formattedMoney = Format.Money(profit);

        var describedProfit = Visuals.DescribeValueWithText(profit, "+" + formattedMoney, formattedMoney, "0");
        return $"* {c.company.Name}: {describedProfit}";
    }
}
