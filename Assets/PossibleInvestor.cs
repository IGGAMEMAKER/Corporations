using Assets.Utils;
using UnityEngine.UI;

public class PossibleInvestor : View
{
    GameEntity investor;

    public Text InvestorName;
    public Text InvestorType;
    public Text InvestorPossibleOffer;
    public ColoredValuePositiveOrNegative Opinion;
    public Text InvestorGoalText;

    public Hint OpinionHint;

    public void SetEntity(GameEntity gameEntity)
    {
        investor = gameEntity;

        Render();
    }

    void Render()
    {
        InvestorName.text = investor.shareholder.Name;
        InvestorType.text = InvestmentUtils.GetFormattedInvestorType(investor.shareholder.InvestorType);

        Opinion.value = InvestmentUtils.GetInvestorOpinion(GameContext, SelectedCompany, investor);
        OpinionHint.SetHint(InvestmentUtils.GetInvestorOpinionDescription(GameContext, SelectedCompany, investor));
    }
}
