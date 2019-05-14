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

    public Text Offer;

    public GameObject AcceptProposal;
    public GameObject RejectProposal;

    public Text Valuation;

    public ColoredValuePositiveOrNegative Opinion;
    public Hint OpinionHint;

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
            return CompanyUtils.IsAreSharesSellable(SelectedCompany);
        }
    }

    bool IsInvestmentRoundActive
    {
        get
        {
            return SelectedCompany.hasAcceptsInvestments;
        }
    }

    void SetButtons(int investorId)
    {
        bool visible = CanAcceptInvestments && SelectedCompany.isControlledByPlayer;

        AcceptProposal.SetActive(visible);
        RejectProposal.SetActive(visible);

        AcceptProposal.GetComponent<AcceptInvestmentProposalController>().InvestorId = investorId;
        RejectProposal.GetComponent<RejectInvestmentProposalController>().InvestorId = investorId;
    }

    void RenderBasicInfo()
    {
        Name.text = shareholder.shareholder.Name;

        InvestorType.text = InvestmentUtils.GetFormattedInvestorType(shareholder.shareholder.InvestorType);
        //Motivation.SetHint(InvestmentUtils.);
    }

    void RenderOffer()
    {
        if (!IsInvestmentRoundActive)
        {
            Offer.text = "???";
            return;
        }

        var proposal = CompanyUtils.GetInvestmentProposal(GameContext, SelectedCompany.company.Id, shareholder.shareholder.Id);

        long Cost = CompanyEconomyUtils.GetCompanyCost(GameContext, SelectedCompany.company.Id);

        long offer = proposal.Offer;
        long futureShareSize = offer * 100 / (offer + Cost);

        Offer.text = $"${ValueFormatter.Shorten(offer)} ({futureShareSize}%)";
    }

    void RenderInvestorOpinion()
    {
        Opinion.value = InvestmentUtils.GetInvestorOpinion(GameContext, SelectedCompany, shareholder);
        OpinionHint.SetHint(InvestmentUtils.GetInvestorOpinionDescription(GameContext, SelectedCompany, shareholder));
    }

    void RenderValuation()
    {
        long valuation;

        if (IsInvestmentRoundActive)
        {
            var proposal = CompanyUtils.GetInvestmentProposal(GameContext, SelectedCompany.company.Id, shareholder.shareholder.Id);

            valuation = proposal.Valuation;
        }
        else
        {
            long Cost = CompanyEconomyUtils.GetCompanyCost(GameContext, SelectedCompany.company.Id);

            valuation = Cost;
        }

        Valuation.text = "$" + ValueFormatter.Shorten(valuation);
    }

    void Render(GameEntity proposal)
    {
        RenderBasicInfo();

        RenderOffer();

        RenderValuation();

        RenderInvestorOpinion();

        SetButtons(shareholder.shareholder.Id);
    }
}
