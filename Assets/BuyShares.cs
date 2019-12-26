using Assets.Core;

public class BuyShares : ButtonController
{
    public int ShareholderId;

    public override void Execute()
    {
        var companyId = SelectedCompany.company.Id;

        int amountOfShares = Companies.GetAmountOfShares(GameContext, companyId, ShareholderId);

        long bid = Companies.GetSharesCost(GameContext, companyId, ShareholderId);

        Companies.BuyShares(GameContext, companyId, MyGroupEntity.shareholder.Id, ShareholderId, amountOfShares, bid);

        //GoBack();
        //ReNavigate();
    }
}
