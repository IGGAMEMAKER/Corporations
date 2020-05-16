using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTestCampaign : ButtonController
{
    public override void Execute()
    {
        State.StartNewCampaign(Q, NicheType.ECom_Exchanging, "IG Games");
    }
}
