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
    GameEntity proposal;

    public void SetEntity(GameEntity invProposal)
    {
        shareholder = CompanyUtils.GetInvestorById(GameContext, invProposal.shareholder.Id);
        proposal = invProposal;

        Render(invProposal);
    }

    void Render(GameEntity proposal)
    {
        RenderBasicInfo();

        RenderInvestorOpinion();

        RenderOffer();

        SetButtons(shareholder.shareholder.Id);
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

    void RenderOffer()
    {
        //return;
        var text = "";
        if (SelectedCompany.hasAcceptsInvestments)
        {
            var proposal = CompanyUtils.GetInvestmentProposal(GameContext, SelectedCompany.company.Id, shareholder.shareholder.Id);

            text = Format.Money(proposal.Offer);
        }

        Offer.text = text;
    }

    void RenderInvestorOpinion()
    {
        var bonus = InvestmentUtils.GetInvestorOpinionBonus(GameContext, SelectedCompany, shareholder);

        OpinionDescription.text = InvestmentUtils.GetInvestorOpinionDescription(GameContext, SelectedCompany, shareholder);
        OpinionHint.SetHint(bonus.ToString());
    }
}
