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

    public NewTeamTypeRelay NewTeamTypeRelay;

    public TeamTask TeamTask;

    // buttons
    public int ChosenTeamId = -1;
    public int ChosenSlotId = 0;

    //public TeamInfo ChosenTeam => ChosenTeamId >= Flagship.team.Teams.Count ? null : Flagship.team.Teams[ChosenTeamId];
    List<GameObject> Tabs => new List<GameObject> { DevelopmentTab, WorkerInteractions, InvestmentTabs, ManagersTabs, NewTeamTabs, AudiencePickingTab, AssignTaskPanel };

    public void FillSlot(int teamId, int slotId)
    {
        ChosenSlotId = slotId;
        ChosenTeamId = teamId;

        ScreenUtils.SetSelectedTeam(Q, teamId);
    }

    private void OnEnable()
    {
        var panelId = ScreenUtils.GetScreenParameter(Q, C.MENU_SELECTED_MAIN_SCREEN_PANEL_ID);

        switch (panelId)
        {
            case 1:
                ChooseManagersTabs(SelectedTeam);
                break;

            default:
                ChooseMainScreen();
                break;
        }

    }
    
    public void ChooseMainScreen()
    {
        HideAll(Tabs);
        OpenUrl("/Holding/Main");
        return;
        if (Flagship.isRelease)
        {
            ChooseAudiencePickingPanel();
        }
        else
        {
            //ChooseMainScreen();
            ChooseWorkerInteractions();
        }

        //ChooseWorkerInteractions();
    }
    
    public void ChooseManagersTabs(int teamId)
    {
        FillSlot(teamId, 0);
        // ShowOnly(ManagersTabs, Tabs);
        //
        // Hide(TaskPanel);

        ScreenUtils.Navigate(Q, ScreenMode.TeamScreen);
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

    public void ChooseAudiencePickingPanel()
    {
        ShowOnly(AudiencePickingTab, Tabs);
    }

    public void ChooseNewTeamTab()
    {
        ShowOnly(NewTeamTabs, Tabs);
        NewTeamTypeRelay.SetTeamTypes();
    }

    public void ChooseWorkerInteractions()
    {
        ChooseAudiencePickingPanel();
        //ShowOnly(WorkerInteractions, Tabs);
    }



    public void ChooseTaskTab()
    {
        ShowOnly(DevelopmentTab, Tabs);

        var tasks = Flagship.team.Teams[ChosenTeamId].Tasks;
        var task = ChosenTeamId >= 0 ? tasks[ChosenSlotId] : null;


        //Debug.Log("Choose task tab: team=" + ChosenTeamId + " slot=" + ChosenSlotId);

        if (task != null)
            TaskPanel.SetEntity(ChosenTeamId, task);
    }

    public void ChooseInvestmentTab()
    {
        ShowOnly(InvestmentTabs, Tabs);
    }


}
