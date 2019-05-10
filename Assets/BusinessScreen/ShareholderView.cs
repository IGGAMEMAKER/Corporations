using Assets.Utils;
using UnityEngine.UI;

//public enum MotivationType
//{
//    Cost,
//    Dividends,
//    Capture,
//    FriendlyCapture,
//}

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

    public void SetEntity(int shareholderId, BlockOfShares shares)
    {
        company = SelectedCompany;

        int totalShares = CompanyUtils.GetTotalShares(company.shareholders.Shareholders);
        shareholder = CompanyUtils.GetInvestorById(GameContext, shareholderId);

        Render(shareholder.shareholder.Name, shares, totalShares, shareholderId);
    }

    string GetInvestorGoalDescription(BlockOfShares shares)
    {
        switch (shares.InvestorGoal)
        {
            case InvestorGoal.BecomeBestByTech:
                return "Become technology leader";

            case InvestorGoal.BecomeMarketFit:
                return "Become market fit";

            case InvestorGoal.BecomeProfitable:
                return "Become profitable";

            case InvestorGoal.GrowClientBase:
                return "Grow client base";

            case InvestorGoal.GrowCompanyCost:
                return "Grow company cost";

            case InvestorGoal.GrowProfit:
                return "Grow profit";

            case InvestorGoal.ProceedToNextRound:
                return "Proceed to next investment round";

            default:
                return shares.InvestorGoal.ToString();
        }
    }

    string GetInvestorGoal(BlockOfShares shares)
    {
        switch (shares.InvestorGoal)
        {
            case InvestorGoal.BecomeBestByTech:
                return "Become technology leader";

            case InvestorGoal.BecomeMarketFit:
                return "Become market fit";

            case InvestorGoal.BecomeProfitable:
                return "Become profitable";

            case InvestorGoal.GrowClientBase:
                return "Grow client base";

            case InvestorGoal.GrowCompanyCost:
                return "Grow company cost";

            case InvestorGoal.GrowProfit:
                return "Grow profit";

            case InvestorGoal.ProceedToNextRound:
                return "Proceed to next investment round";

            default:
                return shares.InvestorGoal.ToString();
        }
    }

    void Render(string name, BlockOfShares shares, int totalShares, int investorId)
    {
        Name.text = name;
        
        Goal.text = GetInvestorGoal(shares);
        Motivation.SetHint($"Motivation: {GetInvestorGoalDescription(shares)}");

        Share.text = CompanyUtils.GetShareSize(GameContext, company.company.Id, investorId) + "%";

        BuyShares.gameObject.SetActive(investorId != MyGroupEntity.shareholder.Id);

        BuyShares.gameObject.GetComponent<BuyShares>().ShareholderId = investorId;
        BuyShares.gameObject.GetComponent<CanBuySharesController>().Render(investorId);
    }
}
