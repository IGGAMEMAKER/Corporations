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

    public GameObject RemoveTaskButton;
    public ChosenTeamTaskView ChosenTaskLabel;

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

        ShowOnly(AssignTaskPanel, Tabs);
        FindObjectOfType<HideNewTeamButtonIfOldOnesAreNotFinished>().SetEntity(TeamTask);
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

    public void ChooseDevTab()
    {
        ShowOnly(DevelopmentTab, Tabs);

        var tasks = Flagship.team.Teams[ChosenTeamId].Tasks;
        var hasTask = ChosenTeamId >= 0 && tasks.Count > ChosenSlotId;

        if (hasTask)
            ChosenTaskLabel.SetTask(tasks[ChosenSlotId]);

        Draw(ChosenTaskLabel, hasTask);
        Draw(RemoveTaskButton, hasTask);
    }

    public void ChooseInvestmentTab()
    {
        ShowOnly(InvestmentTabs, Tabs);

        Hide(ChosenTaskLabel);
        Hide(RemoveTaskButton);
    }

    public void ChooseManagersTabs(int teamId)
    {
        FillSlot(teamId, 0);
        ShowOnly(ManagersTabs, Tabs);

        Hide(ChosenTaskLabel);
        Hide(RemoveTaskButton);
    }
}
