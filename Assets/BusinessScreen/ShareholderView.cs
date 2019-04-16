using Assets.Utils;
using UnityEngine.UI;

public enum MotivationType
{
    Cost,
    Dividends,
    Capture,
    FriendlyCapture,
}

public class ShareholderView : View
{
    public Text Name;
    public Hint Motivation;
    public Text Goal;
    public ColoredValuePositiveOrNegative Loyalty;
    public ColoredValuePositiveOrNegative LoyaltyChange;
    public Text Share;
    public Image Icon;
    public Image Panel;

    public Button BuyShares;
    public Button SellShares;

    GameEntity shareholder;
    GameEntity company;

    public void SetEntity(int shareholderId, int shares)
    {
        company = SelectedCompany;

        int totalShares = CompanyUtils.GetTotalShares(company.shareholders.Shareholders);
        shareholder = CompanyUtils.GetInvestorById(GameContext, shareholderId);

        Render(shareholder.shareholder.Name, shares, totalShares, shareholderId);
    }

    void Render(string name, int shares, int totalShares, int investorId)
    {
        Name.text = name;
        Goal.text = "Company Cost 50.000.000$";
        Motivation.SetHint("Motivation: 20% growth");
        Share.text = CompanyUtils.GetShareSize(GameContext, company.company.Id, investorId) + "%";

        BuyShares.gameObject.SetActive(investorId != MyGroupEntity.shareholder.Id);

        BuyShares.gameObject.GetComponent<BuyShares>().ShareholderId = investorId;
        BuyShares.gameObject.GetComponent<CanBuySharesController>().Render(investorId);

        //LoyaltyChange.UpdateValue(Random.Range(-10f, 10f));
        //Loyalty.UpdateValue(Random.Range(0, 80f));
    }
}
