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

    InvestmentProposal proposal => Companies.GetInvestmentProposal(MyCompany.company.Id, SelectedInvestor.shareholder.Id);

    bool IsInvestmentRoundActive => MyCompany.hasAcceptsInvestments;

    void RenderOffer()
    {
        long Cost = Economy.GetCompanyCost(Q, MyCompany.company.Id);

        long offer = proposal.Investment.Offer;
        var portion = proposal.Investment.Portion;
        long futureShareSize = offer * 100 / (offer + Cost);

        Offer.text = $"{Format.MinifyMoney(offer)} for {futureShareSize}% of our company";
        Portion.text = $"{Format.MinifyMoney(portion)} / week (during {proposal.Investment.RemainingPeriods} weeks)";
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
            Valuation.text = Format.MinifyMoney(Economy.GetCompanyCost(Q, MyCompany));
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
