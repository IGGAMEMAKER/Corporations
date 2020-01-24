using Assets.Core;
using System.Linq;

public class RenderGroupProfit : UpgradedParameterView
{
    public override string RenderHint()
    {
        var daughters = Companies.GetDaughterCompanies(GameContext, MyCompany.company.Id)
            .OrderByDescending(c => Economy.GetProfit(GameContext, c));

        return "Profits\n\n" + string.Join("\n", daughters.Select(GetIncomeInfo));
    }

    public override string RenderValue()
    {
        var profit = Economy.GetProfit(GameContext, MyCompany);
        var balance = Economy.BalanceOf(MyCompany);

        return $"{Format.Minify(balance)}  {Visuals.PositiveOrNegativeMinified(profit)}";
    }

    string GetIncomeInfo(GameEntity c)
    {
        var profit = Economy.GetProfit(GameContext, c);
        var formattedMoney = Format.Money(profit);

        var describedProfit = Visuals.DescribeValueWithText(profit, "+" + formattedMoney, formattedMoney, "0");
        return $"* {c.company.Name}: {describedProfit}";
    }
}
