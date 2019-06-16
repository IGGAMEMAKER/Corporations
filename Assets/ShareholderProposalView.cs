using Assets.Utils;
using UnityEngine;
using UnityEngine.UI;

public class ShareholderProposalView : View
{
    public Image Panel;
    public Text Name;
    public Image Icon;

    public Hint Motivation;
    public Text InvestorType;

    public GameObject LinkToOffer;

    public ColoredValuePositiveOrNegative Opinion;
    public Hint OpinionHint;

    public Text OpinionDescription;

    GameEntity shareholder;

    public void SetEntity(GameEntity proposal)
    {
        shareholder = CompanyUtils.GetInvestorById(GameContext, proposal.shareholder.Id);

        Render(proposal);
    }

    bool CanAcceptInvestments
    {
        get
        {
            return CompanyUtils.IsSharesCanBeSold(SelectedCompany);
        }
    }

    void SetButtons(int investorId)
    {
        bool visible = CanAcceptInvestments && SelectedCompany.isControlledByPlayer;

        LinkToOffer.SetActive(visible);
        LinkToOffer.GetComponent<LinkToInvestmentOffer>().SetInvestorId(investorId);
    }

    void RenderBasicInfo()
    {
        Name.text = shareholder.shareholder.Name;

        InvestorType.text = InvestmentUtils.GetFormattedInvestorType(shareholder.shareholder.InvestorType);
    }

    void RenderInvestorOpinion()
    {
        var bonus = InvestmentUtils.GetInvestorOpinionBonus(GameContext, SelectedCompany, shareholder);

        Opinion.value = bonus.Sum();
        OpinionHint.SetHint(bonus.ToString());

        OpinionDescription.text = InvestmentUtils.GetInvestorOpinionDescription(GameContext, SelectedCompany, shareholder);
    }


    void Render(GameEntity proposal)
    {
        RenderBasicInfo();

        RenderInvestorOpinion();

        SetButtons(shareholder.shareholder.Id);
    }



    bool IsInvestmentRoundActive
    {
        get
        {
            return SelectedCompany.hasAcceptsInvestments;
        }
    }

    void RenderOffer()
    {
        var proposal = CompanyUtils.GetInvestmentProposal(GameContext, SelectedCompany.company.Id, shareholder.shareholder.Id);

        long Cost = CompanyEconomyUtils.GetCompanyCost(GameContext, SelectedCompany.company.Id);

        long offer = proposal.Offer;
        long futureShareSize = offer * 100 / (offer + Cost);

        //Offer.text = $"${ValueFormatter.Shorten(offer)} ({futureShareSize}%)";
    }

    void RenderValuation()
    {
        if (IsInvestmentRoundActive)
        {
            var proposal = CompanyUtils.GetInvestmentProposal(GameContext, SelectedCompany.company.Id, shareholder.shareholder.Id);

            //Valuation.text = "$" + ValueFormatter.Shorten(proposal.Valuation);
        }
    }
}
