using Assets;
using Assets.Core;
using System.Collections.Generic;
using UnityEngine;

public class FlagshipRelayInCompanyView : View
{
    // tabs
    public GameObject DevelopmentTab;
    public GameObject WorkerInteractions;
    public GameObject InvestmentTabs;
    public GameObject ManagersTabs;
    public GameObject NewTeamTabs;
    public GameObject AudiencePickingTab;
    public GameObject AssignTaskPanel;

    public TaskPanelView TaskPanel;

    public TeamTask TeamTask;

    // buttons
    public int ChosenTeamId = -1;
    public int ChosenSlotId = 0;

    // List<GameObject> Tabs => new List<GameObject> { WorkerInteractions, InvestmentTabs, ManagersTabs, NewTeamTabs, AudiencePickingTab, AssignTaskPanel };
    // List<GameObject> Tabs => new List<GameObject> { DevelopmentTab, WorkerInteractions, InvestmentTabs, ManagersTabs, NewTeamTabs, AudiencePickingTab, AssignTaskPanel };

    private void OnEnable()
    {
        ChooseMainScreen();
    }
    
    public void ChooseMainScreen()
    {
        // HideAll(Tabs);
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
        return;

        // // teamId = ChosenTeamId;
        // // slotId = ChosenSlotId;
        //
        // ShowOnly(DevelopmentTab, Tabs);
        //
        // var tasks = Flagship.team.Teams[teamId].Tasks;
        // var task = teamId >= 0 ? tasks[slotId] : null;
        //
        // if (task != null)
        //     TaskPanel.SetEntity(teamId, task);
    }
}
