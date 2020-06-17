using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanyTaskTypeRelay : View
{
    public GameObject MarketingTasks;
    public GameObject FeatureTasks;
    public GameObject ChooseTaskTypeLabel;

    public GameObject RelayButtons;

    public void ChooseMarketingTasks()
    {
        Show(MarketingTasks);
        Hide(FeatureTasks);

        Hide(ChooseTaskTypeLabel);
        HideRelayButtons();
    }

    public void ChooseFeatureTasks()
    {
        Show(FeatureTasks);
        Hide(MarketingTasks);

        Hide(ChooseTaskTypeLabel);
        HideRelayButtons();
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
    }
}
