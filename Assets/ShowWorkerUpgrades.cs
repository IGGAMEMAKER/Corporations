using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShowWorkerUpgrades : View
{
    bool showUpgrades = false;

    public GameObject Upgrades;

    GameEntity worker;

    public GameObject CEOActions;
    public GameObject ProductActions;
    public GameObject TeamLeadActions;
    public GameObject MarketingLeadActions;
    public GameObject ProjectActions;

    public RenderCompanyWorkerListView WorkerController;

    public void SetWorker(GameEntity worker, RenderCompanyWorkerListView WorkerController)
    {
        this.worker = worker;
        this.WorkerController = WorkerController;

        Render();
    }

    public void ToggleState()
    {
        showUpgrades = !showUpgrades;

        if (!showUpgrades)
            WorkerController.ResetRoles();
        else
            WorkerController.SetRole(worker.worker.WorkerRole);

        Render();
    }

    public void HideActions()
    {
        showUpgrades = false;

        Render();
    }

    void Render()
    {
        Draw(Upgrades, showUpgrades);

        var role = worker.worker.WorkerRole;

        var radius = 120f;

        Draw(CEOActions, role == WorkerRole.CEO);
        CEOActions.GetComponent<ArcRender>().Render(radius);

        Draw(TeamLeadActions, role == WorkerRole.TeamLead);
        TeamLeadActions.GetComponent<ArcRender>().Render(radius);

        Draw(MarketingLeadActions, role == WorkerRole.MarketingLead);
        MarketingLeadActions.GetComponent<ArcRender>().Render(radius);

        Draw(ProductActions, role == WorkerRole.ProductManager);
        ProductActions.GetComponent<ArcRender>().Render(radius);

        Draw(ProjectActions, role == WorkerRole.ProjectManager);
        ProjectActions.GetComponent<ArcRender>().Render(radius);
    }

    void OnDisable()
    {
        showUpgrades = false;
        Hide(Upgrades);
    }
}
