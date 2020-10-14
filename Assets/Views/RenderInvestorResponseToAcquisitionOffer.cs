using Assets.Core;
using UnityEngine.UI;

public class RenderInvestorResponseToAcquisitionOffer : View
{
    public Text Response;

    int shareholderId;

    public override void ViewRender()
    {
        base.ViewRender();

        Render();
    }

    void Render()
    {
        var investor = Companies.GetInvestorById(Q, shareholderId);

        RenderResponse(investor);
    }

    public void SetEntity(int shareholderId)
    {
        this.shareholderId = shareholderId;

        Render();
    }

    void RenderResponse(GameEntity investor)
    {
        var AcquisitionOffer = Companies.GetAcquisitionOffer(Q, SelectedCompany, MyCompany.shareholder.Id).acquisitionOffer;


        bool willAcceptOffer = Companies.IsShareholderWillAcceptAcquisitionOffer(AcquisitionOffer, shareholderId, Q);
        bool wantsToSellShares = Companies.IsWantsToSellShares(SelectedCompany, Q, shareholderId, investor.shareholder.InvestorType);

        var opinion = Companies.GetInvestorOpinionAboutAcquisitionOffer(AcquisitionOffer, investor, SelectedCompany, Q);
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
            var description = Companies.GetSellRejectionDescriptionByInvestorType(investor.shareholder.InvestorType, SelectedCompany);
            text = Visuals.Negative(description);
        }


        text = opinion.Minify().ToString();
        Response.text = text;
    }
}
