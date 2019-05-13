using Assets.Utils;
using UnityEngine.UI;

public class ShareholderView : View
{
    public Text Name;
    public Text Type;
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

    void Render(string name, BlockOfShares shares, int totalShares, int investorId)
    {
        Name.text = name;
        
        Goal.text = InvestmentUtils.GetInvestorGoalDescription(shares);
        Motivation.SetHint($"Motivation: {InvestmentUtils.GetInvestorGoal(shares)}");

        Type.text = InvestmentUtils.GetFormattedInvestorType(shareholder.shareholder.InvestorType);

        Share.text = CompanyUtils.GetShareSize(GameContext, company.company.Id, investorId) + "%";


        BuyShares.gameObject.SetActive(investorId != MyGroupEntity?.shareholder?.Id);

        BuyShares.gameObject.GetComponent<BuyShares>().ShareholderId = investorId;
        BuyShares.gameObject.GetComponent<CanBuySharesController>().Render(investorId);
    }
}
