using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Core;
using UnityEngine;

public class TeamRelationsView : View
{
    public GameObject Parent;

    public GameObject AttachedTo;

    public override void ViewRender()
    {
        base.ViewRender();

        var team = Flagship.team.Teams[SelectedTeam];
        Draw(Parent, !team.isIndependentTeam);
        Draw(AttachedTo, Teams.GetDependantTeams(team, Flagship).Any());
    }

    private void OnEnable()
    {
        ViewRender();
    }
}
