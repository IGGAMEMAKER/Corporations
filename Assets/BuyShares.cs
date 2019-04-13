using Assets.Utils;

public class BuyShares : ButtonController
{
    public int ShareholderId;

    public override void Execute()
    {
        var companyId = SelectedCompany.company.Id;

        int amountOfShares = CompanyUtils.GetAmountOfShares(GameContext, companyId, ShareholderId);

        long bid = CompanyUtils.GetSharesCost(GameContext, companyId, ShareholderId);

        CompanyUtils.BuyShares(GameContext, companyId, MyGroupEntity.shareholder.Id, ShareholderId, amountOfShares, bid);

        ReNavigate();
    }
}
