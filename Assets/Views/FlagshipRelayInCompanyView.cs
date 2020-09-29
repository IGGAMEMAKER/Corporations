using Assets;
using Assets.Core;
using System.Collections;
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

    public TeamInfo ChosenTeam => ChosenTeamId >= Flagship.team.Teams.Count ? null : Flagship.team.Teams[ChosenTeamId];
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
            case 0:
                ChooseMainScreen();
                break;

            case 1:
                ChooseManagersTabs(SelectedTeam);
                break;

            default:
                ChooseMainScreen();
                break;
        }

    }

    public void RemoveTask()
    {
        Teams.RemoveTeamTask(Flagship, Q, ChosenTeamId, ChosenSlotId);
        Refresh();
    }

    public void AddPendingTask(TeamTask teamTask)
    {
        TeamTask = teamTask;

        bool hasOneTeam = true; // Flagship.team.Teams.Count == 1;
        bool hasSlotForTask = true; // Flagship.team.Teams[0].Tasks.Count < 4;

        ScheduleUtils.PauseGame(Q);

        if (hasOneTeam)
        {
            if (hasSlotForTask)
            {
                Teams.AddTeamTask(Flagship, Q, 0, teamTask);
                SoundManager.Play(Sound.Action);

                FindObjectOfType<MainPanelRelay>().ViewRender();
            }
            else
            {
                NotificationUtils.AddSimplePopup(Q, "You need more teams", "Wait until <b>Core team</b> will be fully organised.\n\nOR\n\nRemove less important tasks");
            }
        }
        else
        {
            ShowOnly(AssignTaskPanel, Tabs);
            FindObjectOfType<HideNewTeamButtonIfOldOnesAreNotFinished>().SetEntity(TeamTask);

            ScheduleUtils.PauseGame(Q);
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
        ShowOnly(WorkerInteractions, Tabs);
    }

    public void ChooseMainScreen()
    {
        ChooseWorkerInteractions();
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

        //Hide(TaskPanel);
        //Hide(ChosenTaskLabel);
        //Hide(RemoveTaskButton);
    }

    public void ChooseManagersTabs(int teamId)
    {
        FillSlot(teamId, 0);
        ShowOnly(ManagersTabs, Tabs);

        Hide(TaskPanel);
        //Hide(ChosenTaskLabel);
        //Hide(RemoveTaskButton);
    }
}
