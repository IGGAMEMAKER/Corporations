using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenTerriblyManagedTeam : ButtonController
{
    public override void Execute()
    {
        var teamId = Flagship.team.Teams.FindIndex(t => t.isManagedBadly);

        NavigateToTeamScreen(teamId);
    }
}
