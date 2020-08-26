using Assets.Core;
using System.Linq;

public class RenderGroupProfit : UpgradedParameterView
{
    public override string RenderHint()
    {
        var daughters = Companies.GetDaughterCompanies(Q, MyCompany.company.Id)
            .OrderByDescending(c => Economy.GetProfit(Q, c));

        var daughtersIncome = "Based on" + string.Join("\n", daughters.Select(GetIncomeInfo));

        var balance = Economy.BalanceOf(MyCompany);

        var profit = Economy.GetProfit(Q, MyCompany);

        if (daughters.Count() == 1)
        {
            var product = daughters.First();

            var income = Economy.GetProductCompanyIncome(product, Q);
            var maintenance = Economy.GetProductCompanyMaintenance(product, Q, true);

            var bonus = new Bonus<long>("Profit");
            bonus.Append("Product", income);

            foreach (var m in maintenance.bonusDescriptions)
            {
                if (m.HideIfZero)
                    bonus.AppendAndHideIfZero(m.Name, -m.Value);
                else
                    bonus.Append(m.Name, -m.Value);
            }


            if (MyCompany.shareholders.Shareholders.Count > 1)
            {

            var investments = MyCompany.shareholders.Shareholders.Values
                .Select(v => v.Investments.Where(z => z.RemainingPeriods > 0).Select(z => z.Portion).Sum())
                .Sum();

            bonus.AppendAndHideIfZero("Investments", investments);
            }

            bonus.MinifyValues();
            bonus.SortByModule();

            daughtersIncome = "\n" + bonus.ToString();
        }

        return "Cash: " + Format.Money(balance) + "\nProfit: " + Visuals.PositiveOrNegativeMinified(profit) + daughtersIncome;
    }

    public override string RenderValue()
    {
        var profit = Economy.GetProfit(Q, MyCompany);
        var balance = Economy.BalanceOf(MyCompany);

        var text = Format.Money(balance) + "\n" + Visuals.Colorize(Format.Minify(profit), profit > 0);

        return text;
    }

    string GetIncomeInfo(GameEntity c)
    {
        var profit = Economy.GetProfit(Q, c);
        var formattedMoney = Format.Money(profit);

        var describedProfit = Visuals.DescribeValueWithText(profit, "+" + formattedMoney, formattedMoney, "0");

        return $"* {c.company.Name}: {describedProfit}";
    }
}
