using Assets.Utils;
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
            Response.text = CompanyUtils.GetDesireToSellByInvestorType(SelectedCompany, GameContext, shareholderId, investor.shareholder.InvestorType);
            return;
        }

        var desirableCost = CompanyEconomyUtils.GetCompanyCost(GameContext, SelectedCompany.company.Id) * 9 / 10;
        bool offerIsOk = CompanyUtils.GetAcquisitionOffer(GameContext, SelectedCompany.company.Id, shareholderId).acquisitionOffer.Offer > desirableCost;

        if (offerIsOk)
            Response.text = Visuals.Positive("Will sell shares!");
        else
            Response.text = Visuals.Negative("Wants more money");
    }
}
