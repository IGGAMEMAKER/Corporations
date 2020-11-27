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

                return Visuals.Negative($"We LOSE {churnGained}% of your audience, cause your product (quality={(int)quality}) is OUTDATED (max quality={(int)maxQuality})\n") + positioningName;
            }

            return positioningName;
            //return $"which are also making {Marketing.GetPositioningName(company)}";
        }
    }
}
