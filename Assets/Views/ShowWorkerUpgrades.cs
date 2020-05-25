using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShowWorkerUpgrades : View
{
    bool showUpgrades = false;

    GameEntity worker;

    public RenderCompanyWorkerListView WorkerListView;

    public void SetWorker(GameEntity worker, RenderCompanyWorkerListView workerListView)
    {
        this.worker = worker;
        this.WorkerListView = workerListView;

        Render();
    }

    public void ToggleState()
    {
        showUpgrades = !showUpgrades;

        if (!showUpgrades)
            WorkerListView.ResetRoles();
        else
            WorkerListView.SetRole(worker.worker.WorkerRole);

        Render();
    }

    public void HideActions()
    {
        showUpgrades = false;

        Render();
    }

    void Render()
    {
        Debug.Log("Render: show worker upgrades");
    }

    void OnDisable()
    {
        showUpgrades = false;
    }
}
