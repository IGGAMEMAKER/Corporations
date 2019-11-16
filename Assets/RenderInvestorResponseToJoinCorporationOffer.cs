using Assets.Utils;
using UnityEngine.UI;

public class RenderInvestorResponseToJoinCorporationOffer : View
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
        var investor = CompanyUtils.GetInvestorById(GameContext, shareholderId);

        RenderResponse(investor);
    }

    public void SetEntity(int shareholderId)
    {
        this.shareholderId = shareholderId;

        Render();
    }

    void RenderResponse(GameEntity investor)
    {
        bool willAcceptOffer = CompanyUtils.IsShareholderWillAcceptCorporationOffer(SelectedCompany.company.Id, shareholderId, GameContext);
        bool wantsToSellShares = CompanyUtils.IsWantsToSellShares(SelectedCompany, GameContext, shareholderId, investor.shareholder.InvestorType);

        var text = "";

        if (willAcceptOffer)
        {
            text = Visuals.Positive("Will join our corporation!");
        }
        else if (wantsToSellShares)
        {
            text = Visuals.Negative("Doesn't want to join");
        }
        else
        {
            var description = CompanyUtils.GetSellRejectionDescriptionByInvestorType(investor.shareholder.InvestorType, SelectedCompany);
            text = Visuals.Negative(description);
        }


        //text = opinion.Minify().ToString();
        Response.text = text;
    }
}
