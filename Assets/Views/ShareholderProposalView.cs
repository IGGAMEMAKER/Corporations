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
        //bool CanAcceptInvestments = Companies.IsSharesCanBeSold(company);
        //bool visible = CanAcceptInvestments && company.isControlledByPlayer && !proposal.WasAccepted;

        //LinkToOffer.SetActive(visible);
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

        var portion = proposal.Investment.Portion;
        if (company.hasAcceptsInvestments)
            text = $"<b>+{Format.Money(portion)}</b>";

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

            var opinion = bonus.Sum();
            //OpinionHint.SetHint($"Opinion\n{Visuals.Colorize(opinion + "", opinion >= 0)}");
            OpinionHint.SetHint($"Opinion about us");
            OpinionDescription.text = Format.Sign(opinion);
        }
    }
}
