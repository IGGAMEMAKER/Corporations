using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideNewTeamButtonIfOldOnesAreNotFinished : View
{
    public GameObject NewTeamButton;
    public override void ViewRender()
    {
        base.ViewRender();

        Draw(NewTeamButton, !Flagship.team.Teams.Exists(t => t.Workers < 8));
    }
}
