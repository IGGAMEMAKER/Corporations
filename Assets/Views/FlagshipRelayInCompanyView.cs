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

    public void ChooseNewTeamTab()
    {
        Show(NewTeamTabs);
        NewTeamTypeRelay.SetTeamTypes();

        Hide(DevelopmentTab);
        Hide(InvestmentTabs);
        Hide(ManagersTabs);
        Hide(WorkerInteractions);
    }

    public void ChooseWorkerInteractions()
    {
        Show(WorkerInteractions);

        Hide(DevelopmentTab);
        Hide(InvestmentTabs);
        Hide(ManagersTabs);
        Hide(NewTeamTabs);
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

        Hide(ChosenTaskLabel);
        Hide(RemoveTaskButton);
    }

    public void ChooseManagersTabs()
    {
        Show(ManagersTabs);

        Hide(WorkerInteractions);
        Hide(DevelopmentTab);
        Hide(InvestmentTabs);
        Hide(NewTeamTabs);

        Hide(ChosenTaskLabel);
        Hide(RemoveTaskButton);
    }
}
