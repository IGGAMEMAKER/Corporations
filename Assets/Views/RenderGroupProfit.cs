using Assets.Core;
using System.Linq;

public class RenderGroupProfit : UpgradedParameterView
{
    public override string RenderHint()
    {
        var daughters = Companies.GetDaughterCompanies(Q, MyCompany.company.Id)
            .OrderByDescending(c => Economy.GetProfit(Q, c));

        var daughtersIncome = string.Join("\n", daughters.Select(GetIncomeInfo));

        var balance = Economy.BalanceOf(MyCompany);

        var profit = Economy.GetProfit(Q, MyCompany);

        return "Cash: " + Format.Money(balance) + "\n\nProfit: " + Visuals.PositiveOrNegativeMinified(profit) + "\n\nBased on" + daughtersIncome;
    }

    public override string RenderValue()
    {
        var profit = Economy.GetProfit(Q, MyCompany);
        var balance = Economy.BalanceOf(MyCompany);

        var minifiedBalance = Format.MinifyMoney(balance);

        var text = Visuals.Colorize(minifiedBalance, profit > 0);

        return text;

        //if (Companies.IsHasDaughters(Q, MyCompany))
        //    return $"{minifiedBalance}\n{Visuals.PositiveOrNegativeMinified(profit)}";

        //return minifiedBalance;
    }

    string GetIncomeInfo(GameEntity c)
    {
        var profit = Economy.GetProfit(Q, c);
        var formattedMoney = Format.Money(profit);

        var describedProfit = Visuals.DescribeValueWithText(profit, "+" + formattedMoney, formattedMoney, "0");

        return $"* {c.company.Name}: {describedProfit}";
    }
}
