using Assets;
using Assets.Core;
using System.Collections.Generic;
using UnityEngine;

public class FlagshipRelayInCompanyView : View
{
    public TeamTask TeamTask;

    // buttons
    public int ChosenTeamId = -1;
    public int ChosenSlotId = 0;

    public void ChooseMainScreen()
    {
        OpenUrl("/Holding/Main");
    }
    
    public void RemoveTask()
    {
        Teams.RemoveTeamTask(Flagship, Q, ChosenTeamId, ChosenSlotId);
        Refresh();
    }

    public void AddPendingTask(TeamTask teamTask)
    {
        TeamTask = teamTask;

        //ScheduleUtils.PauseGame(Q);

        Teams.AddTeamTask(Flagship, CurrentIntDate, Q, 0, teamTask);

        if (teamTask.IsFeatureUpgrade)
        {
            SoundManager.Play(Sound.ProgrammingTask);
        }

        if (teamTask.IsMarketingTask)
        {
            SoundManager.Play(Sound.MarketingTask);
        }

        if (teamTask.IsHighloadTask)
        {
            SoundManager.Play(Sound.ServerTask);
        }
    }

    public void ChooseTaskTab(int teamId, int slotId)
    {
        OpenUrl("/Holding/TaskTab");
    }
}
