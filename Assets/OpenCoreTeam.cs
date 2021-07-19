using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCoreTeam : ButtonController
{
    public override void Execute()
    {
        //ScreenUtils.SetSelectedTeam(Q, 0);
        //OpenUrl("/TeamScreen");

        NavigateToTeamScreen(0);
        ScheduleUtils.PauseGame(Q);
    }
}
