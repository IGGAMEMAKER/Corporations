using Assets.Core;
using UnityEngine;
using UnityEngine.UI;

public class RenderCompanyRoleOrHireWorkerWithThatRole : View
{
    GameEntity company;

    bool isActiveRole = true;
    public WorkerRole role;

    public GameObject Worker;
    public Text RoleName;

    public RenderManagerAdaptationProgress managerAdaptationProgress;

    public ShowWorkerUpgrades workerActions;
    public Text WorkerRating;


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


    public void SetEntity(GameEntity company, WorkerRole role, bool isActiveRole)
    {
        this.company = company;
        this.role = role;
        this.isActiveRole = isActiveRole;

        Render();
    }

    public void HighlightWorkerRole(bool activeRole)
    {
        var scale = activeRole ? 1 : 0.8f;

        CanvasGroup.transform.localScale = new Vector3(scale, scale, scale); // = activeRole ? 1 : 0.07f;
        CanvasGroup.alpha = activeRole ? 1 : 0.25f;
    }

    public void Render()
    {
        HighlightWorkerRole(isActiveRole);

        RoleName.text = Humans.GetFormattedRole(role);


        bool hasWorker = !Teams.HasFreePlaceForWorker(company, role);

        Draw(Worker, hasWorker);

        if (hasWorker)
        {
            var worker = Teams.GetWorkerByRole(company, role, Q);

            workerActions.SetWorker(worker);

            var rating = Humans.GetRating(worker);
            WorkerRating.text = rating + "";
            WorkerRating.color = Visuals.GetGradientColor(40, 100, rating);

            managerAdaptationProgress.SetEntity(worker);
        }
    }
}