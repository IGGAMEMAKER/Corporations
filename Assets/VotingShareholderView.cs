using Assets.Utils;
using UnityEngine.UI;

public class VotingShareholderView : View
{
    public Text Name;
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
        Name.text = $"{investor.shareholder.Name}, {percentage}%";
        Status.text = InvestmentUtils.GetFormattedInvestorType(investor.shareholder.InvestorType);

        Response.text = "";
    }
}
