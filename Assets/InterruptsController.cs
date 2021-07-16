using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterruptsController : View
{
    public EventView PromoteTeam;
    public EventView Bankruptcy;
    public EventView UnhappyManager;
    public EventView UpgradeFeature;

    public override void ViewRender()
    {
        base.ViewRender();

        Draw(Bankruptcy, Economy.IsWillBecomeBankruptOnNextPeriod(Q, MyCompany));
        Draw(UnhappyManager, true);
        Draw(UpgradeFeature, Products.GetUpgradePoints(Flagship) > 0);
        Draw(PromoteTeam, true);
    }
}
