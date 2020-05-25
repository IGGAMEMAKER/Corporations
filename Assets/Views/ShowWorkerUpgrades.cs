using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShowWorkerUpgrades : View
{
    GameEntity worker;

    public RenderCompanyWorkerListView WorkerListView;

    public void SetWorker(GameEntity worker, RenderCompanyWorkerListView workerListView)
    {
        this.worker = worker;
        this.WorkerListView = workerListView;
    }

    public void ToggleState()
    {
        Debug.Log($"Toggle State {worker.worker.WorkerRole}");

        WorkerListView.ToggleRole(worker.worker.WorkerRole);
    }
}
