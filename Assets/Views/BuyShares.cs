using Assets.Core;

public class BuyShares : ButtonController
{
    public int ShareholderId;

    public override void Execute()
    {
        var company = SelectedCompany;

        int amountOfShares = Companies.GetAmountOfShares(Q, company, ShareholderId);

        long bid = Companies.GetSharesCost(Q, company, ShareholderId);

        Companies.BuyShares(Q, company, MyGroupEntity.shareholder.Id, ShareholderId, amountOfShares, bid);

        //GoBack();
        //ReNavigate();
    }
}
