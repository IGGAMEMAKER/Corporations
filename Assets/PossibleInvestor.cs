using Assets.Utils;
using UnityEngine.UI;

public class PossibleInvestor : View
{
    GameEntity investor;

    public Text InvestorName;
    public Text InvestorType;
    public Text InvestorPossibleOffer;
    public Text InvestorGoalText;

    public void SetEntity(GameEntity gameEntity)
    {
        investor = gameEntity;

        Render();
    }

    void Render()
    {
        InvestorName.text = investor.shareholder.Name;
        InvestorType.text = InvestmentUtils.GetFormattedInvestorType(investor.shareholder.InvestorType);
    }
}
