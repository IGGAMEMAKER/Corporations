using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProposalScreen : View
{
    public Text Offer;
    public Text Valuation;
    public Text InvestorName;

    public AcceptInvestmentProposalController AcceptInvestmentProposalController;
    public RejectInvestmentProposalController RejectInvestmentProposalController;

    GameEntity shareholder
    {
        get
        {
            return ScreenUtils.GetSelectedInvestor(GameContext);
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
        var proposal = CompanyUtils.GetInvestmentProposal(GameContext, SelectedCompany.company.Id, shareholder.shareholder.Id);

        long Cost = CompanyEconomyUtils.GetCompanyCost(GameContext, SelectedCompany.company.Id);

        long offer = proposal.Offer;
        long futureShareSize = offer * 100 / (offer + Cost);

        Offer.text = $"${ValueFormatter.Shorten(offer)} ({futureShareSize}%)";
    }

    void RenderValuation()
    {
        if (IsInvestmentRoundActive)
        {
            var proposal = CompanyUtils.GetInvestmentProposal(GameContext, SelectedCompany.company.Id, shareholder.shareholder.Id);

            Valuation.text = "$" + ValueFormatter.Shorten(proposal.Valuation);
        }
    }

    void OnEnable()
    {
        Render();
    }

    void Render()
    {
        RenderValuation();
        RenderOffer();

        AcceptInvestmentProposalController.InvestorId = shareholder.shareholder.Id;
        RejectInvestmentProposalController.InvestorId = shareholder.shareholder.Id;

        InvestorName.text = shareholder.shareholder.Name;
    }
}
