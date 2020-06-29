using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTestCampaign : ButtonController
{
    public override void Execute()
    {
        State.StartNewCampaign(Q, NicheType.ECom_Exchanging, "IG Games");

        Marketing.AddClients(Flagship, 500);
        Products.UpgradeProductLevel(Flagship, Q);

        Teams.AddTeam(Flagship, TeamType.CrossfunctionalTeam);
        Marketing.ReleaseApp(Q, Flagship);

    }
}
