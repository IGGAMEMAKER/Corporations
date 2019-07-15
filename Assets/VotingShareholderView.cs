using Assets.Utils;
using UnityEngine.UI;

public class VotingShareholderView : View
{
    public Text Name;
    public Text Share;
    public Text Status;
    public Text Response;

    int shareholderId;

    AcquisitionScreen AcquisitionScreen;

    public void SetEntity(int shareholderId, AcquisitionScreen acquisitionScreen)
    {
        this.shareholderId = shareholderId;

        AcquisitionScreen = acquisitionScreen;

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
        if (CompanyUtils.IsWantsToSellShares(SelectedCompany, GameContext, shareholderId, investor.shareholder.InvestorType))
        {
            bool offerIsOk = AcquisitionScreen.offer > CompanyEconomyUtils.GetCompanyCost(GameContext, SelectedCompany.company.Id) * 9 / 10;

            if (offerIsOk)
                Response.text = Visuals.Positive("Will sell shares!");
            else
                Response.text = Visuals.Negative("Wants more money");
        }
        else
        {
            Response.text = CompanyUtils.GetDesireToSellByInvestorType(SelectedCompany, GameContext, shareholderId, investor.shareholder.InvestorType);
        }
    }
}
