using Assets.Core;
using System.Linq;
public class RenderGroupProfit : UpgradedParameterView
{
    public override string RenderValue()
    {
        long Profit = Economy.GetProfit(Q, MyCompany); // GetProfit().Sum();

        return "Profit\n" + Visuals.Colorize(Format.Minify(Profit), Profit > 0);
    }
    public override string RenderHint()
    {
        var daughters = Companies.GetDaughters(MyCompany, Q)
            .OrderByDescending(c => Economy.GetProfit(Q, c));

        if (daughters.Count() == 1)
            return "\n" + Economy.GetProfit(Q, MyCompany, true).MinifyValues().SortByModule().ToString();

        return "Based on\n\n" + string.Join("\n", daughters.Select(DescribeDaughterIncomeInfo));
    }
    string DescribeDaughterIncomeInfo(GameEntity c)
    {
        var profit = Economy.GetProfit(Q, c);
        var formattedMoney = Format.Money(profit, true);

        var describedProfit = Visuals.DescribeValueWithText(profit, "+" + formattedMoney, formattedMoney, "0");

        return $"* {c.company.Name}: {describedProfit}";
    }
}
