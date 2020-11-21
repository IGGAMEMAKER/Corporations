using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RenderProductKPI : View
{
    public Text Funding;
    public Text Income;
    public Text MarketingBudget;

    public Text AudienceChange;
    public Text Growth;
    public Text Churn;

    public override void ViewRender()
    {
        base.ViewRender();

        var product = Flagship;
        var profit = Economy.GetProfit(Q, product, true);

        var funding = profit.Only("Investments");
        Funding.text = Visuals.Positive(Format.Money(funding));

        var income = Economy.GetProductIncome(product);
        Income.text = Visuals.Positive(Format.Money(income));

        var marketingBudget = profit.Only("Marketing in").Sum();
        MarketingBudget.text = Visuals.Negative(Format.Money(marketingBudget));

        // ---------

        var growthBonus = Marketing.GetAudienceGrowthBonus(product, Q);
        var growth = growthBonus.Sum();

        Growth.text = Visuals.Positive(Format.Minify(growth) + $" users weekly\n{growthBonus.ToString()}");

        long churnUsers = 0;
        var segments = Marketing.GetAudienceInfos();

        var churnText = "";
        for (var i = 0; i < segments.Count; i++)
        {
            churnUsers += Marketing.GetChurnClients(product, i);

            var churn = Marketing.GetChurnRate(product, i, true);

            if (churn.Sum() > 0)
                churnText += $"\n{churn.SetTitle("Churn for " + segments[i].Name).ToString(true)}";
        }

        Churn.text = Visuals.Negative(Format.Minify(churnUsers) + " users weekly\n") + churnText;

        var change = Marketing.GetAudienceChange(product, Q);
        AudienceChange.text = Visuals.Positive(Format.Minify(change));
    }
}
