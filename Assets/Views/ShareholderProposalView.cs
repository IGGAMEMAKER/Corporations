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
        shareholder = Companies.GetInvestorById(Q, invProposal.ShareholderId);
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

    GameEntity company => MyCompany;

    void SetButtons(int investorId)
    {
        bool CanAcceptInvestments = Companies.IsSharesCanBeSold(company);
        bool visible = CanAcceptInvestments && company.isControlledByPlayer && !proposal.WasAccepted;

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

        if (company.hasAcceptsInvestments)
            text = Format.Money(proposal.Offer);

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
            OpinionDescription.text = Investments.GetInvestorOpinionDescription(Q, company, shareholder);
            var bonus = Investments.GetInvestorOpinionBonus(Q, company, shareholder);

            OpinionHint.SetHint(bonus.ToString());
        }
    }
}
