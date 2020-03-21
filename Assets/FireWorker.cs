using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWorker : ButtonController
{
    public override void Execute()
    {
        var company = Flagship;


        Teams.FireRegularWorker(company, WorkerRole.Programmer);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            var have = Teams.GetAmountOfWorkers(company, Q);

            // fire ten percent of workers
            for (var i = 0; i < have / 10; i++)
            {
                Teams.FireRegularWorker(company, WorkerRole.Programmer);
            }
        }

        company.productUpgrades.upgrades[ProductUpgrade.AutorecuitWorkers] = false;
    }
}
