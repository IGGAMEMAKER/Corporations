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
        var AcquisitionOffer = CompanyUtils.GetAcquisitionOffer(GameContext, SelectedCompany.company.Id, MyCompany.shareholder.Id).acquisitionOffer;


        bool willAcceptOffer = CompanyUtils.IsShareholderWillAcceptAcquisitionOffer(AcquisitionOffer, shareholderId, GameContext);
        bool wantsToSellShares = CompanyUtils.IsWantsToSellShares(SelectedCompany, GameContext, shareholderId, investor.shareholder.InvestorType);

        var opinion = CompanyUtils.GetInvestorOpinionAboutAcquisitionOffer(AcquisitionOffer, investor, SelectedCompany, GameContext);
        var text = "";

        if (willAcceptOffer)
        {
            text = Visuals.Positive("Will sell shares!");
        }
        else if (wantsToSellShares)
        {
            text = Visuals.Negative("Wants more money");
        }
        else
        {
            var description = CompanyUtils.GetSellRejectionDescriptionByInvestorType(investor.shareholder.InvestorType, SelectedCompany);
            text = Visuals.Negative(description);
        }


        text = opinion.Sum() + "  " + opinion.RenderTitle().ToString();
        Response.text = text;
    }
}
