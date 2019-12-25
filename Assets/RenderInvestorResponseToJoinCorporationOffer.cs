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
        bool willAcceptOffer = Companies.IsShareholderWillAcceptCorporationOffer(SelectedCompany.company.Id, shareholderId, GameContext);
        bool wantsToSellShares = Companies.IsWantsToSellShares(SelectedCompany, GameContext, shareholderId, investor.shareholder.InvestorType);

        var text = "";

        if (willAcceptOffer)
        {
            text = Visuals.Positive("Will join our corporation!");
        }
        else if (wantsToSellShares)
        {
            text = Visuals.Negative("Our company is too small");
        }
        else
        {
            var description = Companies.GetSellRejectionDescriptionByInvestorType(investor.shareholder.InvestorType, SelectedCompany);
            description = "Doesn't want to sell shares";
            text = Visuals.Negative(description);
        }


        //text = opinion.Minify().ToString();
        Response.text = text;
    }
}
