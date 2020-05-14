using Assets.Core;
using UnityEngine;
using UnityEngine.UI;

public class RenderCompanyRoleOrHireWorkerWithThatRole : View
{
    GameEntity company;
    public WorkerRole role;
    bool isActiveRole = true;

    public GameObject NoWorker;
    public GameObject Worker;
    public Text RoleName;

    public Hint HireWorkerHint;
    public HireManagerByRole HireManagerByRole;

    public RenderCompanyWorkerListView WorkerController;

    public ShowWorkerUpgrades workerActions;

    CanvasGroup _CanvasGroup = null;
    CanvasGroup CanvasGroup
    {
        get
        {
            if (_CanvasGroup == null)
                _CanvasGroup = GetComponent<CanvasGroup>();

            return _CanvasGroup;
        }
    }

    public void SetEntity(GameEntity company, WorkerRole role, RenderCompanyWorkerListView WorkerController, bool isActiveRole)
    {
        this.company = company;
        this.role = role;
        this.isActiveRole = isActiveRole;
        this.WorkerController = WorkerController;

        Render();
    }

    public void HighlightWorkerRole(bool activeRole)
    {
        CanvasGroup.alpha = activeRole ? 1 : 0.07f;
        //CanvasGroup.interactable = activeRole;
    }

    public void Render()
    {
        HighlightWorkerRole(isActiveRole);

        bool hasWorker = !Teams.HasFreePlaceForWorker(company, role);

        RoleName.text = role.ToString();

        Draw(Worker, hasWorker);
        Draw(NoWorker, !hasWorker);

        if (!hasWorker)
        {
            var roleDescription = Teams.GetRoleDescription(role, Q, true, null);

            HireWorkerHint.SetHint(roleDescription);
            HireManagerByRole.WorkerRole = role;
        }

        else
        {
            var worker = Teams.GetWorkerByRole(company, role, Q);

            workerActions.SetWorker(worker, WorkerController);
        }
    }
}