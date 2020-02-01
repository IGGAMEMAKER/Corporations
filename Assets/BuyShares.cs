using Assets.Core;

public class BuyShares : ButtonController
{
    public int ShareholderId;

    public override void Execute()
    {
        var companyId = SelectedCompany.company.Id;

        int amountOfShares = Companies.GetAmountOfShares(Q, companyId, ShareholderId);

        long bid = Companies.GetSharesCost(Q, companyId, ShareholderId);

        Companies.BuyShares(Q, companyId, MyGroupEntity.shareholder.Id, ShareholderId, amountOfShares, bid);

        //GoBack();
        //ReNavigate();
    }
}
