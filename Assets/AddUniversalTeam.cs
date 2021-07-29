using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddUniversalTeam : ButtonController
{
    public override void Execute()
    {
        Teams.AddTeam(Flagship, Q, TeamType.CrossfunctionalTeam, 0);

        var newTeamID = Flagship.team.Teams.Count - 1;
        //UpdateData(C.MENU_SELECTED_TEAM, newTeamID);
        ScreenUtils.SetSelectedTeam(Q, newTeamID);

        OpenUrl("/TeamCreationScreen");
    }
}
