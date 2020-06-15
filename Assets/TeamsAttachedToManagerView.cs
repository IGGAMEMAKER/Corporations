using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeamsAttachedToManagerView : View
{
    public Text AmountOfTeams;
    public TeamType TeamType;
    public RemoveTeamController RemoveTeam;

    public override void ViewRender()
    {
        base.ViewRender();

        var teamType = TeamType;

        var company = Flagship;

    }
}
