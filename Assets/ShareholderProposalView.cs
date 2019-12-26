using Assets.Core;
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
    InvestmentProposal proposal;

    public void SetEntity(InvestmentProposal invProposal)
    {
        shareholder = Companies.GetInvestorById(GameContext, invProposal.ShareholderId);
        proposal = invProposal;

        Render();
    }

    void Render()
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
            return Companies.IsSharesCanBeSold(SelectedCompany);
        }
    }

    void SetButtons(int investorId)
    {
        bool visible = CanAcceptInvestments && SelectedCompany.isControlledByPlayer && !proposal.WasAccepted;

        LinkToOffer.SetActive(visible);
        LinkToOffer.GetComponent<LinkToInvestmentOffer>().SetInvestorId(investorId);
    }

    void RenderBasicInfo()
    {
        Name.text = shareholder.shareholder.Name;

        InvestorType.text = Investments.GetFormattedInvestorType(shareholder.shareholder.InvestorType);
    }

    void RenderOffer()
    {
        var text = "";

        if (SelectedCompany.hasAcceptsInvestments)
        {
            text = Format.Money(proposal.Offer);
        }

        Offer.text = text;
    }

    void RenderInvestorOpinion()
    {
        if (proposal.WasAccepted)
        {
            OpinionDescription.text = "Offer was accepted!";
            OpinionHint.SetHint("");
        } else
        {
            OpinionDescription.text = Investments.GetInvestorOpinionDescription(GameContext, SelectedCompany, shareholder);
            var bonus = Investments.GetInvestorOpinionBonus(GameContext, SelectedCompany, shareholder);
            OpinionHint.SetHint(bonus.ToString());
        }
    }
}
