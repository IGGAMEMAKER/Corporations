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

    public void SetWorker(GameEntity worker)
    {
        this.worker = worker;

        Render();
    }

    public void ToggleState()
    {
        showUpgrades = !showUpgrades;

        Render();
    }

    void Render()
    {
        Draw(Upgrades, showUpgrades);

        var role = worker.worker.WorkerRole;

        Draw(CEOActions, role == WorkerRole.CEO);
        CEOActions.GetComponent<ArcRender>().Render(70f);

        Draw(TeamLeadActions, role == WorkerRole.TeamLead);
        TeamLeadActions.GetComponent<ArcRender>().Render(70f);

        Draw(MarketingLeadActions, role == WorkerRole.MarketingLead);
        MarketingLeadActions.GetComponent<ArcRender>().Render(70f);

        Draw(ProductActions, role == WorkerRole.ProductManager);
        ProductActions.GetComponent<ArcRender>().Render(70f);

        Draw(ProjectActions, role == WorkerRole.ProjectManager);
        ProjectActions.GetComponent<ArcRender>().Render(70f);
    }

    void OnDisable()
    {
        showUpgrades = false;
        Hide(Upgrades);
    }
}
