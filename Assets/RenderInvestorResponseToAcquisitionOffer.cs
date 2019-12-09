using Assets.Utils;
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
        var investor = Companies.GetInvestorById(GameContext, shareholderId);

        RenderResponse(investor);
    }

    public void SetEntity(int shareholderId)
    {
        this.shareholderId = shareholderId;

        Render();
    }

    void RenderResponse(GameEntity investor)
    {
        var AcquisitionOffer = Companies.GetAcquisitionOffer(GameContext, SelectedCompany.company.Id, MyCompany.shareholder.Id).acquisitionOffer;


        bool willAcceptOffer = Companies.IsShareholderWillAcceptAcquisitionOffer(AcquisitionOffer, shareholderId, GameContext);
        bool wantsToSellShares = Companies.IsWantsToSellShares(SelectedCompany, GameContext, shareholderId, investor.shareholder.InvestorType);

        var opinion = Companies.GetInvestorOpinionAboutAcquisitionOffer(AcquisitionOffer, investor, SelectedCompany, GameContext);
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
