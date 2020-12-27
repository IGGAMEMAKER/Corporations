using Assets.Core;
using UnityEngine.UI;

public class ProposalScreen : View
{
    public Text Offer;
    public Text Valuation;
    public Text InvestorName;

    public Text Portion;

    public Text ProposalStatus;

    public AcceptInvestmentProposalController AcceptInvestmentProposalController;
    public RejectInvestmentProposalController RejectInvestmentProposalController;

    InvestmentProposal proposal => Companies.GetInvestmentProposal(MyCompany, SelectedInvestor.shareholder.Id);

    bool IsInvestmentRoundActive => MyCompany.hasAcceptsInvestments;

    void RenderOffer()
    {
        long Cost = Economy.CostOf(MyCompany, Q);

        long offer = proposal.Investment.Offer;
        var portion = proposal.Investment.Portion;
        long futureShareSize = offer * 100 / (offer + Cost);

        Offer.text = $"{Format.Money(offer, true)} for {futureShareSize}% of our company";
        Portion.text = $"{Format.Money(portion, true)} / week (during {proposal.Investment.RemainingPeriods} weeks)";
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
            //Valuation.text = "$" + Format.Minify(proposal.Investment.Valuation);
            Valuation.text = Format.Money(Economy.CostOf(MyCompany, Q), true);
        }
    }

    void OnEnable()
    {
        Render();
    }

    void SetButtons()
    {
        AcceptInvestmentProposalController.gameObject.SetActive(!proposal.WasAccepted);
        RejectInvestmentProposalController.gameObject.SetActive(false);

        AcceptInvestmentProposalController.InvestorId = SelectedInvestor.shareholder.Id;
        RejectInvestmentProposalController.InvestorId = SelectedInvestor.shareholder.Id;
    }

    void Render()
    {
        RenderValuation();
        RenderOffer();
        RenderProposalStatus();

        SetButtons();

        InvestorName.text = $"Proposal from {SelectedInvestor.shareholder.Name}, {Investments.GetFormattedInvestorType(SelectedInvestor.shareholder.InvestorType)}";
    }
}
