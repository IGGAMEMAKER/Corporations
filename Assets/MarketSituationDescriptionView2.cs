using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketSituationDescriptionView2 : ParameterView
{
    public override string RenderValue()
    {
        var company = Flagship;

        if (company.teamEfficiency.Efficiency.isUniqueCompany)
        {
            return Visuals.Positive("You get TWICE more users, cause you have 0 competitors");
        }

        else
        {
            var competitiveness = company.teamEfficiency.Efficiency.Competitiveness;
            if (Mathf.Abs(competitiveness) > 0)
            {
                var quality = Marketing.GetPositioningQuality(company).Sum();
                var maxQuality = quality + competitiveness;

                var churnGained = Marketing.GetChurnFromOutcompetition(company);

                return Visuals.Negative($"You lose {churnGained}% of your audience, cause your product ({quality}) is outdated ({maxQuality})");
            }

            return "";
        }
    }
}
