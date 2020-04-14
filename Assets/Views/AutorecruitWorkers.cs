using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutorecruitWorkers : ProductUpgradeButton
{
    public override string GetButtonTitle() => "Hire max"; //  workers\nautomatically

    public override long GetCost() => 0;

    public override string GetHint() => "Automatically hires necessary amount of workers.\n\nNOTE, you still need to hire managers manually!";

    public override ProductUpgrade GetProductUpgrade()
    {
        return ProductUpgrade.AutorecuitWorkers;
    }

    public override void Execute()
    {
        base.Execute();

        AutoHire(Flagship);
    }

    void AutoHire(GameEntity flagship)
    {
        var workers = Teams.GetAmountOfWorkers(flagship, Q);
        var necessary = Products.GetNecessaryAmountOfWorkers(flagship, Q);

        var autohire = Products.IsUpgradeEnabled(flagship, ProductUpgrade.AutorecuitWorkers);

        if (workers < necessary && autohire)
        {
            var need = necessary - workers;

            for (var i = 0; i < need; i++)
                Teams.HireRegularWorker(flagship);
        }
    }

    public override void ViewRender()
    {
        base.ViewRender();

        AutoHire(Flagship);
    }
}
