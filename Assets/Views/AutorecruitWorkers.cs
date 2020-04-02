using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutorecruitWorkers : ProductUpgradeButton
{
    public override string GetButtonTitle()
    {
        return "Hire max workers\nautomatically";
    }

    public override long GetCost() => 0;

    public override string GetHint()
    {
        return "Automatically hires necessary amount of workers.\n\nNOTE, you still need to hire managers manually!";
    }

    public override ProductUpgrade GetProductUpgrade()
    {
        return ProductUpgrade.AutorecuitWorkers;
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var flaghip = Companies.GetFlagship(Q, MyCompany);

        if (flaghip == null)
            return;

        var autohire = Products.IsUpgradeEnabled(flaghip, ProductUpgrade.AutorecuitWorkers);

        var workers = Teams.GetAmountOfWorkers(flaghip, Q);
        var necessary = Products.GetNecessaryAmountOfWorkers(flaghip, Q);

        if (workers < necessary && autohire)
        {
            var need = necessary - workers;

            for (var i = 0; i < need; i++)
                Teams.HireRegularWorker(flaghip);
        }
    }
}
