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

    public void ChooseMarketingTasks()
    {
        Show(MarketingTasks);

        Hide(FeatureTasks);
        Hide(SupportTasks);
        Hide(ServerTasks);

        Hide(ChooseTaskTypeLabel);
    }

    public void ChooseFeatureTasks()
    {
        Show(FeatureTasks);

        Hide(MarketingTasks);
        Hide(SupportTasks);
        Hide(ServerTasks);

        Hide(ChooseTaskTypeLabel);
    }

    public void ChooseServersideTasks()
    {
        Show(ServerTasks);

        Hide(MarketingTasks);
        Hide(FeatureTasks);
        Hide(SupportTasks);

        Hide(ChooseTaskTypeLabel);
    }
    public void ChooseSupportTasks()
    {
        Show(SupportTasks);

        Hide(MarketingTasks);
        Hide(FeatureTasks);
        Hide(ServerTasks);

        Hide(ChooseTaskTypeLabel);
    }

    public void HideRelayButtons()
    {
        Hide(RelayButtons);
    }

    public void ShowRelayButtons()
    {
        Show(RelayButtons);


        // not all buttons can be shown because of specialisation
        var relay = FindObjectOfType<FlagshipRelayInCompanyView>();
        var teamType = relay.ChosenTeam.TeamType;

        var isUniversalTeam = teamType == TeamType.CoreTeam || teamType == TeamType.SmallCrossfunctionalTeam || teamType == TeamType.BigCrossfunctionalTeam || teamType == TeamType.CrossfunctionalTeam;

        Draw(ChooseMarketingTasksButton,    isUniversalTeam || teamType == TeamType.MarketingTeam);
        Draw(ChooseDevelopmentTasksButton,  isUniversalTeam || teamType == TeamType.DevelopmentTeam);
        Draw(ChooseServerTasksButton,       isUniversalTeam || teamType == TeamType.DevOpsTeam);
        Draw(ChooseSupportTasksButton,      isUniversalTeam || teamType == TeamType.SupportTeam);
        //

        Hide(MarketingTasks);
        Hide(FeatureTasks);
        Hide(SupportTasks);
        Hide(ServerTasks);
    }
}
