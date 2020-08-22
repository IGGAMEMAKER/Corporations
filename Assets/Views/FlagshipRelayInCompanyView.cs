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

    public GameObject RemoveTaskButton;
    public ChosenTeamTaskView ChosenTaskLabel;

    public NewTeamTypeRelay NewTeamTypeRelay;

    // buttons
    public int ChosenTeamId = -1;
    public int ChosenSlotId = 0;

    public TeamInfo ChosenTeam => ChosenTeamId >= Flagship.team.Teams.Count ? null : Flagship.team.Teams[ChosenTeamId];

    public void FillSlot(int teamId, int slotId)
    {
        ChosenSlotId = slotId;
        ChosenTeamId = teamId;

        ScreenUtils.SetSelectedTeam(Q, teamId);
    }

    private void OnEnable()
    {
        ChooseWorkerInteractions();
    }

    public void RemoveTask()
    {
        Teams.RemoveTeamTask(Flagship, Q, ChosenTeamId, ChosenSlotId);
        Refresh();
    }

    public void ChooseAudiencePickingPanel()
    {
        Show(AudiencePickingTab);

        Hide(NewTeamTabs);
        Hide(DevelopmentTab);
        Hide(InvestmentTabs);
        Hide(ManagersTabs);
        Hide(WorkerInteractions);
    }

    public void ChooseNewTeamTab()
    {
        Show(NewTeamTabs);
        NewTeamTypeRelay.SetTeamTypes();

        Hide(DevelopmentTab);
        Hide(InvestmentTabs);
        Hide(ManagersTabs);
        Hide(WorkerInteractions);
        Hide(AudiencePickingTab);
    }

    public void ChooseWorkerInteractions()
    {
        Show(WorkerInteractions);

        Hide(DevelopmentTab);
        Hide(InvestmentTabs);
        Hide(ManagersTabs);
        Hide(NewTeamTabs);
        Hide(AudiencePickingTab);
    }

    public void ChooseMainScreen()
    {
        ChooseWorkerInteractions();
    }

    public void ChooseDevTab()
    {
        Show(DevelopmentTab);

        Hide(WorkerInteractions);
        Hide(InvestmentTabs);
        Hide(ManagersTabs);
        Hide(NewTeamTabs);
        Hide(AudiencePickingTab);


        var tasks = Flagship.team.Teams[ChosenTeamId].Tasks;
        var hasTask = ChosenTeamId >= 0 && tasks.Count > ChosenSlotId;

        if (hasTask)
            ChosenTaskLabel.SetTask(tasks[ChosenSlotId]);

        Draw(ChosenTaskLabel, hasTask);
        Draw(RemoveTaskButton, hasTask);
    }

    public void ChooseInvestmentTab()
    {
        Show(InvestmentTabs);

        Hide(WorkerInteractions);
        Hide(DevelopmentTab);
        Hide(ManagersTabs);
        Hide(NewTeamTabs);
        Hide(AudiencePickingTab);


        Hide(ChosenTaskLabel);
        Hide(RemoveTaskButton);
    }

    public void ChooseManagersTabs()
    {
        Show(ManagersTabs);

        Hide(AudiencePickingTab);
        Hide(WorkerInteractions);
        Hide(DevelopmentTab);
        Hide(InvestmentTabs);
        Hide(NewTeamTabs);

        Hide(ChosenTaskLabel);
        Hide(RemoveTaskButton);
    }
}
