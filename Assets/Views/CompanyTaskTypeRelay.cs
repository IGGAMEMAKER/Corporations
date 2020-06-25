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

        Hide(MarketingTasks);
        Hide(FeatureTasks);
        Hide(SupportTasks);
        Hide(ServerTasks);
    }
}
