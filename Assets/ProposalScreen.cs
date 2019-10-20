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

    private GameEntity Shareholder => ScreenUtils.GetSelectedInvestor(GameContext);

    InvestmentProposal proposal => CompanyUtils.GetInvestmentProposal(GameContext, SelectedCompany.company.Id, Shareholder.shareholder.Id);

    bool IsInvestmentRoundActive => SelectedCompany.hasAcceptsInvestments;

    void RenderOffer()
    {
        long Cost = EconomyUtils.GetCompanyCost(GameContext, SelectedCompany.company.Id);

        long offer = proposal.Offer;
        long futureShareSize = offer * 100 / (offer + Cost);

        Offer.text = $"${Format.Minify(offer)} for {futureShareSize}% of our company";
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
            Valuation.text = "$" + Format.Minify(proposal.Valuation);
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

        AcceptInvestmentProposalController.InvestorId = Shareholder.shareholder.Id;
        RejectInvestmentProposalController.InvestorId = Shareholder.shareholder.Id;
    }

    void Render()
    {
        RenderValuation();
        RenderOffer();
        RenderProposalStatus();

        SetButtons();

        InvestorName.text = $"Proposal from {Shareholder.shareholder.Name}, {InvestmentUtils.GetFormattedInvestorType(Shareholder.shareholder.InvestorType)}";
    }
}
