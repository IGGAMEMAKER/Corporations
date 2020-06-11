using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveTeamController : ButtonController
{
    public TeamType TeamType;
    public override void Execute()
    {
        Teams.RemoveTeam(Flagship, TeamType);
    }
}
