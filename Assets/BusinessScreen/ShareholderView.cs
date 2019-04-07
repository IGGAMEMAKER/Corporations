using Assets.Utils;
using UnityEngine;
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
    public Text Motivation;
    public Text Goal;
    public ColoredValuePositiveOrNegative Loyalty;
    public ColoredValuePositiveOrNegative LoyaltyChange;
    public Text Share;
    public Image Icon;
    public Image Panel;

    GameEntity shareholder;

    public void SetEntity(int shareholderId, int shares)
    {
        var ourCompany = myProductEntity;

        int totalShares = CompanyUtils.GetTotalShares(ourCompany.shareholders.Shareholders);
        shareholder = CompanyUtils.GetInvestorById(GameContext, shareholderId);

        Render(shareholder.shareholder.Name, shares, totalShares);
    }

    void Render(string name, int shares, int totalShares)
    {
        Name.text = name;
        Goal.text = "Company Cost 50.000.000$";
        Motivation.text = "20% growth";
        Share.text = shares * 100 / totalShares + "%";

        //LoyaltyChange.UpdateValue(Random.Range(-10f, 10f));
        //Loyalty.UpdateValue(Random.Range(0, 80f));
    }
}
