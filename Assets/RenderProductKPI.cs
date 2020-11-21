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

        var product = SelectedCompany;
        var profit = Economy.GetProfit(Q, product, true);

        var funding = Economy.GetFundingBudget(product, Q);
        Funding.text = Visuals.Positive(Format.Money(funding));

        var income = Economy.GetProductIncome(product);
        Income.text = Visuals.Positive(Format.Money(income));

        var marketingBudget = Economy.GetMarketingBudget(product, Q);
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
            var churnInSegment = Marketing.GetChurnClients(product, i);
            churnUsers += churnInSegment;

            var churn = Marketing.GetChurnRate(product, i, true).SetTitle("Churn for " + segments[i].Name);

            bool IsSomewhatInterestedInSegment = Marketing.IsAimingForSpecificAudience(product, i) || churnInSegment > 0;

            if (churn.Sum() > 0 && IsSomewhatInterestedInSegment)
            {
                churnText += $"\n{churn.ToString(true)}";
            }
            //churnText += $"\n{churn.ToString(true)}";
        }

        Churn.text = Visuals.Negative(Format.Minify(churnUsers) + " users weekly\n") + churnText;

        var change = Marketing.GetAudienceChange(product, Q);
        AudienceChange.text = Visuals.Positive(Format.Minify(change));
    }
}
