using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudienceDetailsView : ParameterView
{
    public override string RenderValue()
    {
        var text = "";

        var audienceInfos = Marketing.GetAudienceInfos();

        var company = Flagship;
        var segmentId = company.productTargetAudience.SegmentId;

        var info = audienceInfos[segmentId];

        var incomePerUser = (double)Economy.GetBaseIncomeByMonetisationType(Q, company.product.Niche);

        var maxIncome = (long)(info.Amount * incomePerUser);

        text = $"<b>Potential Audience (Income)</b>" +
            $"\n{Format.Minify(info.Amount)} users ({Format.MinifyMoney(maxIncome)} / month)\n\n";
        text += $"<b>Audience specs</b>";

        foreach (var b in info.Bonuses)
        {
            if (b.Max == 0)
                continue;

            var isGood = b.Max >= 0;


            if (b.isAcquisitionFeature)
            {
                var value = Visuals.Colorize(b.Max.ToString("0.0") + "%", isGood);

                text += "\nGrowth: " + value;
            }

            if (b.isMonetisationFeature)
            {
                //var value = Visuals.Colorize(b.Max.ToString("0.0"), isGood);
                var value = b.Max.ToString("0.0");
                var income = incomePerUser * (100f + b.Max) / 100f;

                text += "\nMonetisation: " + income.ToString("0.00") + "$ / user";
            }

            //if (b.isRetentionFeature)
            //{
            //    text += "\nLoyalty: " + value;
            //}
        }

        text += "\n\n";

        var averageTeam = Random.Range(45, 96);
        var averageBudget = Random.Range(1, 1000) * 1000000;
        text += $"<b>Competition</b>" +
        $"\n{Random.Range(0, 4)} companies";
        //$"\nAverage team strength ({averageTeam}LVL)" +
        //$"\nAverage marketing budget ({Format.MinifyMoney(averageBudget)})";

        return text;
    }
}
