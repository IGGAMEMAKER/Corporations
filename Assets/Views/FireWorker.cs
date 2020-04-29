using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWorker : ButtonController
{
    public bool Decimate = false;

    public override void Execute()
    {
        var company = Flagship;

        if (Decimate)
        {
            var have = Teams.GetTeamSize(company, Q);

            // fire ten percent of workers
            for (var i = 0; i < have / 10; i++)
            {
                Teams.FireRegularWorker(company, WorkerRole.Programmer);
            }
        } else
        {
            Teams.FireRegularWorker(company, WorkerRole.Programmer);
        }

        company.productUpgrades.upgrades[ProductUpgrade.AutorecruitWorkers] = false;
    }
}
