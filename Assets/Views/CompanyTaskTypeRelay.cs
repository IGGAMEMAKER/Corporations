using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanyTaskTypeRelay : View
{
    public GameObject MarketingTasks;
    public GameObject FeatureTasks;
    public GameObject SupportTasks;
    public GameObject ServerTasks;

    public GameObject ChooseTaskTypeLabel;

    public GameObject RelayButtons;

    public GameObject ChooseMarketingTasksButton;
    public GameObject ChooseDevelopmentTasksButton;
    public GameObject ChooseSupportTasksButton;
    public GameObject ChooseServerTasksButton;

    List<GameObject> ChoosingButtons => new List<GameObject> { ChooseMarketingTasksButton, ChooseDevelopmentTasksButton, ChooseSupportTasksButton, ChooseServerTasksButton };
    List<GameObject> TaskContainers => new List<GameObject> { MarketingTasks, FeatureTasks, SupportTasks, ServerTasks };

    void SetMode(GameObject tasks, GameObject buttons)
    {
        ShowOnly(tasks, TaskContainers);
        ShowOnly(buttons, ChoosingButtons);
    }

    public void ChooseMarketingTasks()
    {
        SetMode(MarketingTasks, ChooseMarketingTasksButton);
        HideRelayButtons();

        Hide(ChooseTaskTypeLabel);
    }

    public void ChooseFeatureTasks()
    {
        SetMode(FeatureTasks, ChooseDevelopmentTasksButton);
        HideRelayButtons();

        Hide(ChooseTaskTypeLabel);
    }

    public void ChooseServersideTasks()
    {
        SetMode(ServerTasks, ChooseServerTasksButton);

        Hide(ChooseTaskTypeLabel);
    }
    public void ChooseSupportTasks()
    {
        SetMode(SupportTasks, ChooseSupportTasksButton);

        Hide(ChooseTaskTypeLabel);
    }

    public void HideRelayButtons()
    {
        Hide(RelayButtons);
    }

    public void AdjustTaskTypeButtonsToTeamType()
    {
        var relay = FindObjectOfType<FlagshipRelayInCompanyView>();
        var teamType = relay.ChosenTeam.TeamType;

        var isUniversalTeam = teamType == TeamType.CoreTeam || teamType == TeamType.SmallCrossfunctionalTeam || teamType == TeamType.BigCrossfunctionalTeam || teamType == TeamType.CrossfunctionalTeam;

        Draw(ChooseMarketingTasksButton,    isUniversalTeam || teamType == TeamType.MarketingTeam);
        Draw(ChooseDevelopmentTasksButton,  isUniversalTeam || teamType == TeamType.DevelopmentTeam);
        Draw(ChooseServerTasksButton,       isUniversalTeam || teamType == TeamType.DevOpsTeam);
        Draw(ChooseSupportTasksButton,      isUniversalTeam || teamType == TeamType.SupportTeam);
    }

    public void ShowRelayButtons()
    {
        Show(RelayButtons);


        // not all buttons can be shown because of specialisation
        AdjustTaskTypeButtonsToTeamType();
        //

        Hide(MarketingTasks);
        Hide(FeatureTasks);
        Hide(SupportTasks);
        Hide(ServerTasks);
    }
}
