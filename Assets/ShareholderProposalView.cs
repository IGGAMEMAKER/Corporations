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
        //AcceptProposal.SetActive(investorId != MyGroupEntity.shareholder.Id);
        //RejectProposal.SetActive(investorId != MyGroupEntity.shareholder.Id);

        //AcceptProposal.GetComponent<AcceptInvestmentProposalController>().InvestorId = investorId;
        //RejectProposal.GetComponent<RejectInvestmentProposalController>().InvestorId = investorId;
    }

    void Render(string name, GameEntity proposal)
    {
        int shareholderId = shareholder.shareholder.Id;

        var investor = InvestmentUtils.GetInvestorById(GameContext, shareholderId);

        Name.text = name;
        InvestorType.text = InvestmentUtils.GetFormattedInvestorType(investor.shareholder.InvestorType);

        long Cost = CompanyEconomyUtils.GetCompanyCost(GameContext, SelectedCompany.company.Id);

        long offer = 1000000; // proposal.Offer;
        long futureShareSize = offer * 100 / (offer + Cost);


        Offer.text = $"${ValueFormatter.Shorten(offer)} ({futureShareSize}%)";

        long valuation = offer * 10;
        Valuation.text = "$" + ValueFormatter.Shorten(valuation);

        SetButtons(shareholderId);

        Opinion.value = InvestmentUtils.GetInvestorOpinion(GameContext, SelectedCompany, investor);
        OpinionHint.SetHint(InvestmentUtils.GetInvestorOpinionDescription(GameContext, SelectedCompany, investor));
    }
}
