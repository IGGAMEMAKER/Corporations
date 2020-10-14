using Assets.Core;
using System.Linq;

public class RenderGroupProfit : UpgradedParameterView
{
    long Profit => GetProfit().Sum();

    long Balance => Economy.BalanceOf(MyCompany);


    public override string RenderValue()
    {
        // Format.Money(Balance) + 
        return "Profit\n" + Visuals.Colorize(Format.Minify(Profit), Profit > 0);
    }

    public override string RenderHint()
    {
        var daughters = Companies.GetDaughters(Q, MyCompany)
            .OrderByDescending(c => Economy.GetProfit(Q, c));

        var daughtersIncome = "Based on" + string.Join("\n", daughters.Select(GetIncomeInfo));


        if (daughters.Count() == 1)
            daughtersIncome = "\n" + GetProfit().ToString();

        return "Cash: " + Format.Money(Balance) + "\nProfit: " + Visuals.PositiveOrNegativeMinified(Profit) + daughtersIncome;
    }

    Bonus<long> GetProfit()
    {
        var bonus = new Bonus<long>("Profit");

        //if (MyCompany.ownings.Holdings.Count() == 1)
        //{
        //}
        bonus = Economy.GetProfit(Q, MyCompany, true);

        bonus.MinifyValues();
        bonus.SortByModule();

        return bonus;
    }

    string GetIncomeInfo(GameEntity c)
    {
        var profit = Economy.GetProfit(Q, c);
        var formattedMoney = Format.Money(profit);

        var describedProfit = Visuals.DescribeValueWithText(profit, "+" + formattedMoney, formattedMoney, "0");

        return $"* {c.company.Name}: {describedProfit}";
    }
}
