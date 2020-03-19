using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWorker : ButtonController
{
    public override void Execute()
    {
        var flaghship = Companies.GetFlagship(Q, MyCompany);
        var company = flaghship;


        Teams.FireRegularWorker(company, WorkerRole.Programmer);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            //var need = Products.GetNecessaryAmountOfWorkers(company, Q);
            var have = Teams.GetAmountOfWorkers(company, Q);

            //var missingWorkers = need - have;

            var tenPercent = have / 10;

            for (var i = 0; i < tenPercent; i++)
            {
                Teams.FireRegularWorker(company, WorkerRole.Programmer);
            }
        }

        flaghship.productUpgrades.upgrades[ProductUpgrade.AutorecuitWorkers] = false;
    }
}
