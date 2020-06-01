using Assets.Core;
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

        BlinkIfNecessary();
    }

    public void ToggleState()
    {
        WorkerListView.ToggleRole(worker.worker.WorkerRole);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        BlinkIfNecessary();
    }

    void BlinkIfNecessary()
    {
        var isNecessary = false;

        switch (worker.worker.WorkerRole)
        {
            case WorkerRole.CEO:
                isNecessary = HasNewCEOButtons();

                break;
            case WorkerRole.MarketingLead:
                isNecessary = HasNewMarketingLeadButtons();

                break;
            case WorkerRole.TeamLead:
                isNecessary = HasNewTeamLeadButtons();

                break;
            case WorkerRole.ProjectManager:
                isNecessary = HasNewProjectManagerButtons();

                break;
            case WorkerRole.ProductManager:
                isNecessary = HasNewProductManagerButtons();

                break;

            default:
                isNecessary = false;

                break;
        }

        GetComponent<Blinker>().enabled = isNecessary && WorkerListView.IsRoleChosen(worker.worker.WorkerRole);
    }

    bool HasNewCEOButtons()
    {
        return false;
    }

    bool HasNewMarketingLeadButtons()
    {
        bool hasNewChannels = false;

        var activeChannels = Flagship.companyMarketingActivities.Channels.Count == Markets.GetAvailableMarketingChannels(Q, Flagship).Length;

        return hasNewChannels;
    }

    bool HasNewTeamLeadButtons()
    {
        return false;
    }

    bool HasNewProjectManagerButtons()
    {
        return false;
    }

    bool HasNewProductManagerButtons()
    {
        return false;
    }
}
