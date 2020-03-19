using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderNextInvestmentRoundDate : ParameterView
{
    public GameObject NextTweakLabel;

    public override string RenderValue()
    {
        bool isRoundActive = SelectedCompany.hasAcceptsInvestments;

        if (!isRoundActive)
        {
            NextTweakLabel.SetActive(false);
            return "";
        }

        NextTweakLabel.SetActive(true);

        int days = SelectedCompany.acceptsInvestments.DaysLeft;
        return $"{days} days";
    }
}
