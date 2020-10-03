using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompanyTaskTypeRelay : View
{
    public GameObject MarketingTasks;
    public GameObject FeatureTasks;
    public GameObject SupportTasks;
    public GameObject ServerTasks;

    public GameObject FeatureCounter;
    public GameObject MarketingCounter;

    public GameObject ChooseTaskTypeLabel;

    public GameObject RelayButtons;

    public GameObject ChooseMarketingTasksButton;
    public GameObject ChooseDevelopmentTasksButton;
    public GameObject ChooseSupportTasksButton;
    public GameObject ChooseServerTasksButton;

    List<GameObject> ChoosingButtons => new List<GameObject> { ChooseMarketingTasksButton, ChooseDevelopmentTasksButton, ChooseSupportTasksButton, ChooseServerTasksButton };
    List<GameObject> TaskContainers => new List<GameObject> { MarketingTasks, FeatureTasks, SupportTasks, ServerTasks };

    void OnEnable()
    {
        bool isFirstTime = true;

        if (isFirstTime)
            ChooseFeatureTasks();
        else
            ChooseMarketingTasks();

        RenderAmountOfTasks();
    }

    public override void ViewRender()
    {
        base.ViewRender();

        RenderAmountOfTasks();
    }

    void SetMode(GameObject tasks, GameObject buttons)
    {
        ScheduleUtils.PauseGame(Q);
        ShowOnly(tasks, TaskContainers);
    }

    void RenderAmountOfTasks()
    {
        var features = Products.GetProductFeaturesList(Flagship, Q).Length;
        var channels = Markets.GetMarketingChannelsList(Flagship, Q).Length;

        Draw(FeatureCounter, features > 0);
        Draw(MarketingCounter, channels > 0);

        FeatureCounter.GetComponentInChildren<Text>().text = features.ToString();
        MarketingCounter.GetComponentInChildren<Text>().text = channels.ToString();
    }

    void ShowMarketingButton()
    {
        //ShowOnly(ChooseMarketingTasksButton, ChoosingButtons);
        HideAll(ChoosingButtons);
    }

    void ShowDevButton()
    {
        //ShowOnly(ChooseDevelopmentTasksButton, ChoosingButtons);
        HideAll(ChoosingButtons);
    }

    public void ChooseMarketingTasks()
    {
        SetMode(MarketingTasks, ChooseMarketingTasksButton);
        Show(FeatureTasks);
        ShowDevButton();

        RenderTaskLabel();
    }

    public void ChooseFeatureTasks()
    {
        //SetMode(FeatureTasks, ChooseDevelopmentTasksButton);
        SetMode(MarketingTasks, ChooseMarketingTasksButton);
        Show(FeatureTasks);
        ShowMarketingButton();

        RenderTaskLabel();
    }

    public void ChooseServersideTasks()
    {
        SetMode(ServerTasks, ChooseServerTasksButton);
        ShowMarketingButton();

        RenderTaskLabel();
    }
    public void ChooseSupportTasks()
    {
        SetMode(SupportTasks, ChooseSupportTasksButton);
        ShowMarketingButton();

        RenderTaskLabel();
    }

    void RenderTaskLabel()
    {
        //Hide(ChooseTaskTypeLabel);
        Show(ChooseTaskTypeLabel);
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
