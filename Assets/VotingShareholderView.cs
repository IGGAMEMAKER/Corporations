using Assets.Core;
using UnityEngine.UI;

public class VotingShareholderView : View
{
    public Text Name;
    public Text Share;
    public Text Status;

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
        var investor = Companies.GetInvestorById(GameContext, shareholderId);

        var percentage = Companies.GetShareSize(GameContext, SelectedCompany.company.Id, shareholderId);
        Name.text = $"{investor.shareholder.Name}";
        Share.text = percentage + "%";
        Status.text = Investments.GetFormattedInvestorType(investor.shareholder.InvestorType);
    }
}
