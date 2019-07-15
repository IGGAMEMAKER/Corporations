using Assets.Utils;
using UnityEngine;
using UnityEngine.UI;

public class VotingShareholderView : View
{
    public Text Name;
    public Text Share;
    public Text Status;
    public Text Response;

    int shareholderId;

    public void SetEntity(int shareholderId)
    {
        this.shareholderId = shareholderId;

        Render();
    }

    public override void ViewRender()
    {
        base.ViewRender();

        Render();
    }

    void Render()
    {
        var investor = CompanyUtils.GetInvestorById(GameContext, shareholderId);

        var percentage = CompanyUtils.GetShareSize(GameContext, SelectedCompany.company.Id, shareholderId);
        Name.text = $"{investor.shareholder.Name}";
        Share.text = percentage + "%";
        Status.text = InvestmentUtils.GetFormattedInvestorType(investor.shareholder.InvestorType);

        RenderResponse(investor);
    }

    void RenderResponse(GameEntity investor)
    {
        if (!CompanyUtils.IsWantsToSellShares(SelectedCompany, GameContext, shareholderId, investor.shareholder.InvestorType))
        {
            Response.text = Visuals.Negative(CompanyUtils.GetSellRejectionDescriptionByInvestorType(investor.shareholder.InvestorType));
        }
        else if (isWillAcceptOffer)
        {
            Response.text = Visuals.Positive("Will sell shares!");
        }
        else
        {
            Response.text = Visuals.Negative("Wants more money");
        }
    }

    AcquisitionOfferComponent AcquisitionOffer
    {
        get
        {
            return CompanyUtils.GetAcquisitionOffer(GameContext, SelectedCompany.company.Id, MyCompany.shareholder.Id).acquisitionOffer;
        }
    }

    bool isWillAcceptOffer
    {
        get
        {
            return CompanyUtils.IsShareholderWillAcceptAcquisitionOffer(AcquisitionOffer, shareholderId, GameContext);
        }
    }
}
