using Assets.Utils;
using UnityEngine.UI;

public class ShareholderView : View
{
    public Text Name;
    public Text Type;
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

        int totalShares = Companies.GetTotalShares(company.shareholders.Shareholders);
        shareholder = Companies.GetInvestorById(GameContext, shareholderId);

        Render(shareholder.shareholder.Name, shares, totalShares, shareholderId);
    }

    void AddLinkToInvestorIfPossible(InvestorType investorType)
    {
        switch (investorType)
        {
            case InvestorType.Angel:
            case InvestorType.Founder:
                gameObject.AddComponent<LinkToHuman>().SetHumanId(shareholder.human.Id);
                break;

            case InvestorType.Strategic:
            case InvestorType.VentureInvestor:
                gameObject.AddComponent<LinkToProjectView>().CompanyId = shareholder.company.Id;
                break;

        }
    }

    void Render(string name, BlockOfShares shares, int totalShares, int investorId)
    {
        Name.text = name;
        AddLinkToInvestorIfPossible(shares.InvestorType);
        
        Type.text = InvestmentUtils.GetFormattedInvestorType(shareholder.shareholder.InvestorType);

        Share.text = Companies.GetShareSize(GameContext, company.company.Id, investorId) + "%";

        //BuyShares.gameObject.SetActive(investorId != MyGroupEntity?.shareholder?.Id);

        //BuyShares.gameObject.GetComponent<LinkToBuyShares>().SetInvestorId(investorId);
    }
}
