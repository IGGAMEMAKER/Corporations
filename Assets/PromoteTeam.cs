using Assets;
using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PromoteTeam : ButtonController
{
    public override void Execute()
    {
        var team = Flagship.team.Teams[SelectedTeam];

        Teams.Promote(Flagship, team);
        PlaySound(Sound.GoalCompleted);
    }
}
