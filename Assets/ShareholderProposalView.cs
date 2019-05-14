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

        Render(shareholder.shareholder.Name, proposal);
    }

    void SetButtons(int investorId)
    {
        bool isInvestmentRoundActive = CompanyUtils.IsAreSharesSellable(SelectedCompany);

        AcceptProposal.SetActive(isInvestmentRoundActive);
        RejectProposal.SetActive(isInvestmentRoundActive);

        AcceptProposal.GetComponent<AcceptInvestmentProposalController>().InvestorId = investorId;
        RejectProposal.GetComponent<RejectInvestmentProposalController>().InvestorId = investorId;
    }

    void RenderBasicInfo()
    {
        Name.text = name;

        InvestorType.text = InvestmentUtils.GetFormattedInvestorType(shareholder.shareholder.InvestorType);
        //Motivation.SetHint(InvestmentUtils.);
    }

    void RenderOffer()
    {
        long Cost = CompanyEconomyUtils.GetCompanyCost(GameContext, SelectedCompany.company.Id);

        long offer = 1000000; // proposal.Offer;
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
        long Cost = CompanyEconomyUtils.GetCompanyCost(GameContext, SelectedCompany.company.Id);

        long valuation = Cost * 12 / 10;

        Valuation.text = "$" + ValueFormatter.Shorten(valuation);
    }

    void Render(string name, GameEntity proposal)
    {
        RenderBasicInfo();

        RenderOffer();

        RenderValuation();

        RenderInvestorOpinion();

        SetButtons(shareholder.shareholder.Id);
    }
}
