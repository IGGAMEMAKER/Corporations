using Assets.Core;
using UnityEngine;
using UnityEngine.UI;

public class RenderCompanyRoleOrHireWorkerWithThatRole : View
{
    GameEntity company;
    WorkerRole role;

    public GameObject NoWorker;
    public GameObject Worker;
    public Text RoleName;

    public Hint HireWorkerHint;
    public HireManagerByRole HireManagerByRole;

    public RenderCompanyWorkerListView WorkerController;

    public ShowWorkerUpgrades workerActions;

    public void SetEntity(GameEntity company, WorkerRole role)
    {
        this.company = company;
        this.role = role;

        Render();
    }

    void Render()
    {
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

            workerActions.SetWorker(worker);
        }
    }
}