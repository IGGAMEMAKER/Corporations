using UnityEngine;

public class RenderNextInvestmentRoundDate : ParameterView
{
    public GameObject NextTweakLabel;

    public override string RenderValue()
    {
        bool isRoundActive = MyCompany.hasAcceptsInvestments;

        if (!isRoundActive)
        {
            NextTweakLabel.SetActive(false);
            return "";
        }

        NextTweakLabel.SetActive(true);

        int days = MyCompany.acceptsInvestments.DaysLeft;
        return $"{days} days";
    }
}
