using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWorker : ButtonController
{
    public override void Execute()
    {
        TeamUtils.FireRegularWorker(SelectedCompany);
    }
}
