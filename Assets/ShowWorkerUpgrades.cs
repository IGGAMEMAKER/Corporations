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

    public ProductUpgradeButtons productUpgradeButtons;

    public RenderCompanyWorkerListView WorkerController;


    Dictionary<WorkerRole, ArcRender> WorkersArcs;
    Dictionary<WorkerRole, LinkTo> Links;

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

        if (showUpgrades)
        {
            var role = worker.worker.WorkerRole;

            RenderRole(CEOActions, worker, WorkerRole.CEO, 0, 5);
            RenderRole(MarketingLeadActions, worker, WorkerRole.MarketingLead, 1, 5);
            RenderRole(TeamLeadActions, worker, WorkerRole.TeamLead, 2, 5);
            RenderRole(ProjectActions, worker, WorkerRole.ProjectManager, 3, 5);
            RenderRole(ProductActions, worker, WorkerRole.ProductManager, 4, 5);
        }
    }

    void RenderRole(GameObject obj, GameEntity worker, WorkerRole targetRole, int index, int amount)
    {
        var radius = 120f + amount * 5f;


        var angleMin = 45;
        var angleMax = -90;

        var delta = angleMax - angleMin;

        var baseAngle = angleMin + delta * index / amount;


        var newAngleMin = baseAngle + 45f;
        var newAngleMax = baseAngle - 90f - amount * 5f;

        WorkerRole role = worker.worker.WorkerRole;

        Draw(obj, role == targetRole);

        if (role == targetRole)
        {
            obj.GetComponent<ArcRender>().Render(radius, newAngleMin, newAngleMax);
            obj.GetComponentInChildren<LinkToHuman>().SetHumanId(worker.human.Id);
        }
    }

    void OnDisable()
    {
        showUpgrades = false;
        Hide(Upgrades);
    }
}
