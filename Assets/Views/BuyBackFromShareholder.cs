using Assets.Core;

public class BuyBackFromShareholder : ButtonView
{
    public int ShareholderId;
    public ShareholdersOnMainScreenListView ShareholdersOnMainScreenListView;

    public override void Execute()
    {
        var shareholder = Companies.GetInvestorById(Q, ShareholderId);

        Companies.BuyBackPercent(Q, MyCompany, shareholder, 1);
        ShareholdersOnMainScreenListView.RenderShareholderData();
        ShareholdersOnMainScreenListView.ViewRender();
    }

    public override void ViewRender()
    {
        base.ViewRender();

        ProductUpgradeLinks productUpgradeLinks = GetComponent<ProductUpgradeLinks>();

        var shareholder = Companies.GetInvestorById(Q, ShareholderId);

        var portionSize = Companies.GetPortionSize(Q, MyCompany, shareholder, 1);
        var portionCost = Companies.GetSharesCost(Q, MyCompany, ShareholderId, portionSize);

        productUpgradeLinks.Title.text = "<b>Buy back 1%</b>\nfor " + Format.Money(portionCost);
    }
}
