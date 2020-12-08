using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketSituationDescriptionView2 : ParameterView
{
    public override string RenderValue()
    {
        var company = Flagship;

        var positioningName = $"We are making {Marketing.GetPositioningName(Flagship)}";

        if (company.teamEfficiency.Efficiency.isUniqueCompany)
        {
            return Visuals.Positive("We get TWICE more users, cause you have 0 competitors") + ".\n" + positioningName;
        }

        else
        {
            var competitiveness = company.teamEfficiency.Efficiency.Competitiveness;
            var churnGained = Marketing.GetChurnFromOutcompetition(company);

            //if (Mathf.Abs(competitiveness) > 0)
            if (churnGained > 0)
            {
                var quality = Marketing.GetPositioningQuality(company).Sum();
                var maxQuality = quality + competitiveness;

                var qualityDescription = quality.ToString("0");
                var maxDescription = (int)maxQuality;

                var heart = "<size=30>\u2665</size>";

                var lossReason = $"{heart}={qualityDescription}, while max={maxDescription}";

                return Visuals.Negative($"You LOSE {churnGained}% of audience, cause your APP IS OUTDATED ({lossReason})\n") + positioningName;
            }

            return positioningName;
            //return $"which are also making {Marketing.GetPositioningName(company)}";
        }
    }
}
