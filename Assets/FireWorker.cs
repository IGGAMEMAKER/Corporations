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
    }
}
