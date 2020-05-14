using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HireManagerByRole : ButtonController
{
    public WorkerRole WorkerRole;

    public override void Execute()
    {
        var company = Flagship;

        Teams.HireManager(company, Q, WorkerRole);
    }
}
