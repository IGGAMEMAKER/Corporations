using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveTeamController : ButtonController
{
    public int TeamId;

    public override void Execute()
    {
        var relay = FindObjectOfType<FlagshipRelayInCompanyView>();

        var teamId = relay.ChosenTeamId;

        Teams.RemoveTeam(Flagship, Q, teamId);

        relay.ChooseMainScreen();
    }
}
