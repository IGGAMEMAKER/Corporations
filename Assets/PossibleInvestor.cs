using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PossibleInvestor : View
{
    GameEntity investor;

    public Text InvestorName;
    public Text InvestorType;
    public Text InvestorPossibleOffer;
    public ColoredValuePositiveOrNegative Opinion;
    public Text InvestorGoalText;

    public void SetEntity(GameEntity gameEntity)
    {
        investor = gameEntity;

        Render();
    }

    void Render()
    {
        InvestorName.text = investor.shareholder.Name;
        InvestorType.text = investor.shareholder.InvestorType.ToString();

        InvestorPossibleOffer.text = "~$20M";

        Opinion.value = 25;
        InvestorGoalText.text = InvestorGoal.BecomeMarketFit.ToString();
    }
}
