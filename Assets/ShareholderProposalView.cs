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

    public Text Offer;
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

        var opinion = bonus.Sum();

        OpinionDescription.text = InvestmentUtils.GetInvestorOpinionDescription(GameContext, SelectedCompany, shareholder);
        OpinionHint.SetHint(bonus.ToString());

        if (SelectedCompany.hasAcceptsInvestments)
        {
            var proposal = CompanyUtils.GetInvestmentProposal(GameContext, SelectedCompany.company.Id, shareholder.shareholder.Id);

            Offer.text = Format.Money(proposal.Offer);
        } else
        {
            Offer.text = "";
        }
    }


    void Render(GameEntity proposal)
    {
        RenderBasicInfo();

        RenderInvestorOpinion();

        SetButtons(shareholder.shareholder.Id);
    }
}
