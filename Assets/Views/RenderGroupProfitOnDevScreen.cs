using Assets.Core;
using UnityEngine.UI;

public class RenderGroupProfitOnDevScreen : View
{
    public Text Profitability;

    GameEntity company;

    // TODO Renders Flagship Profit

    public override void ViewRender()
    {
        base.ViewRender();

        var flagship = Flagship;

        if (flagship == null)
            return;

        company = flagship;

        var bonus = GetProfitDescriptionFull().MinifyValues();
        var profit = Economy.GetProfit(Q, company);

        if (Profitability != null)
        {
            Profitability.text = Format.Minify(profit);
            Profitability.color = Visuals.GetColorPositiveOrNegative(profit);
            Profitability.GetComponent<Hint>().SetHint(bonus.ToString());
        }
    }

    Bonus<long> GetProfitDescriptionFull()
    {
        var bonus = new Bonus<long>("Balance change")
            .Append("Income", Economy.GetIncome(Q, company));

        var prodMnt = Economy.GetProductCompanyMaintenance(company, true);

        foreach (var p in prodMnt.bonusDescriptions)
            bonus.AppendAndHideIfZero(p.Name, -p.Value);

        return bonus;
    }
}
