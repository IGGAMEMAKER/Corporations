using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveTeamController : ButtonController
{
    public TeamType TeamType;
    public override void Execute()
    {
        var company = Flagship;

        var amountOfTeams = company.team.Teams.ContainsKey(TeamType) ? company.team.Teams[TeamType] : 0;

        if (amountOfTeams > 0)
            Teams.RemoveTeam(Flagship, TeamType);
    }
}
