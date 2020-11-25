using Assets.Core;

public class BuyShares : ButtonController
{
    public int ShareholderId;

    public override void Execute()
    {
        var company = SelectedCompany;

        var investor = Companies.GetInvestorById(Q, ShareholderId);

        int amountOfShares = Companies.GetAmountOfShares(Q, company, ShareholderId);

        long bid = Companies.GetSharesCost(Q, company, ShareholderId);

        Companies.BuyShares(Q, company, MyGroupEntity, investor, amountOfShares, bid);

        //GoBack();
        //ReNavigate();
    }
}
