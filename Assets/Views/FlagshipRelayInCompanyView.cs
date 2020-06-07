using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagshipRelayInCompanyView : View
{
    // tabs
    public GameObject DevelopmentTab;
    public GameObject WorkerInteractions;

    // buttons

    private void OnEnable()
    {
        ChooseDevTab();
    }

    public void ChooseWorkerInteractions()
    {
        Draw(WorkerInteractions, true);
        Draw(DevelopmentTab, false);
    }

    public void ChooseDevTab()
    {
        Draw(WorkerInteractions, false);
        Draw(DevelopmentTab, true);
    }
}
