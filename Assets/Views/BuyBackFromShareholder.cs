using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyBackFromShareholder : ButtonView
{
    public int ShareholderId;

    public override void Execute()
    {
        Companies.BuyBackPercent(Q, MyCompany, MyCompany.company.Id, ShareholderId, 1);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        ProductUpgradeLinks productUpgradeLinks = GetComponent<ProductUpgradeLinks>();

        var portionSize = Companies.GetPortionSize(Q, MyCompany, MyCompany.company.Id, ShareholderId, 1);
        var portionCost = Companies.GetSharesCost(Q, MyCompany.company.Id, ShareholderId, portionSize);

        productUpgradeLinks.Title.text = "<b>Buy back 1%</b>\nfor " + Format.Money(portionCost);
    }
}
