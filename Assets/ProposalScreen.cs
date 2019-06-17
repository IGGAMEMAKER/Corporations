using Assets.Utils;
using UnityEngine.UI;

public class ProposalScreen : View
{
    public Text Offer;
    public Text Valuation;
    public Text InvestorName;

    public Text ProposalStatus;

    public AcceptInvestmentProposalController AcceptInvestmentProposalController;
    public RejectInvestmentProposalController RejectInvestmentProposalController;

    GameEntity shareholder
    {
        get
        {
            return ScreenUtils.GetSelectedInvestor(GameContext);
        }
    }

    InvestmentProposal proposal
    {
        get
        {
            return CompanyUtils.GetInvestmentProposal(GameContext, SelectedCompany.company.Id, shareholder.shareholder.Id);
        }
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
        long Cost = CompanyEconomyUtils.GetCompanyCost(GameContext, SelectedCompany.company.Id);

        long offer = proposal.Offer;
        long futureShareSize = offer * 100 / (offer + Cost);

        Offer.text = $"${ValueFormatter.Shorten(offer)} for {futureShareSize}% of our company";
    }

    void RenderProposalStatus()
    {
        var text = Visuals.Neutral("They are waiting for our response");

        if (proposal.WasAccepted)
            text = Visuals.Positive("Investment proposal was accepted");

        ProposalStatus.text = text;
    }

    void RenderValuation()
    {
        if (IsInvestmentRoundActive)
        {
            Valuation.text = "$" + ValueFormatter.Shorten(proposal.Valuation);
        }
    }

    void OnEnable()
    {
        Render();
    }

    void SetButtons()
    {
        AcceptInvestmentProposalController.gameObject.SetActive(!proposal.WasAccepted);
        RejectInvestmentProposalController.gameObject.SetActive(!proposal.WasAccepted);

        AcceptInvestmentProposalController.InvestorId = shareholder.shareholder.Id;
        RejectInvestmentProposalController.InvestorId = shareholder.shareholder.Id;
    }

    void Render()
    {
        RenderValuation();
        RenderOffer();
        RenderProposalStatus();

        SetButtons();

        InvestorName.text = $"Proposal from {shareholder.shareholder.Name}, {InvestmentUtils.GetFormattedInvestorType(shareholder.shareholder.InvestorType)}";
    }
}
