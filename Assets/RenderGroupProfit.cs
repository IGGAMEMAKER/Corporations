using Assets.Core;
using System.Linq;

public class RenderGroupProfit : UpgradedParameterView
{
    public override string RenderHint()
    {
        var daughters = Companies.GetDaughterCompanies(Q, MyCompany.company.Id)
            .OrderByDescending(c => Economy.GetProfit(Q, c));

        return "Profits\n\n" + string.Join("\n", daughters.Select(GetIncomeInfo));
    }

    public override string RenderValue()
    {
        var profit = Economy.GetProfit(Q, MyCompany);
        var balance = Economy.BalanceOf(MyCompany);

        var minifiedBalance = Format.Minify(balance);

        if (Companies.IsHasDaughters(Q, MyCompany))
            return $"{minifiedBalance}  {Visuals.PositiveOrNegativeMinified(profit)}";

        return minifiedBalance;
    }

    string GetIncomeInfo(GameEntity c)
    {
        var profit = Economy.GetProfit(Q, c);
        var formattedMoney = Format.Money(profit);

        var describedProfit = Visuals.DescribeValueWithText(profit, "+" + formattedMoney, formattedMoney, "0");
        return $"* {c.company.Name}: {describedProfit}";
    }
}
