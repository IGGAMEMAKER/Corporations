using Assets.Core;
using UnityEngine;

public class WorkerHierarchyListView : ListView
{
    GameEntity company;

    ManagerTabRelay managerTabRelay => FindObjectOfType<ManagerTabRelay>();

    public Transform CEOTransform;

    int amountOfRoles = 0;

    public override void SetItem<T>(Transform t, T entity)
    {
        var role = (WorkerRole)(object)entity;

        //bool highlightRole = managerTabRelay.IsRoleChosen(role);

        //t.GetComponent<RenderCompanyRoleOrHireWorkerWithThatRole>().SetEntity(company, role, highlightRole);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        company = Flagship;

        if (company != null)
        {
            var roles = Teams.GetRolesTheoreticallyPossibleForThisCompanyType(company);

            // all roles except CEO
            //roles.Remove(WorkerRole.CEO);

            amountOfRoles = roles.Count;

            SetItems(roles);
        }
    }

    public void SetEntity(GameEntity company)
    {
        this.company = company;

        ViewRender();
    }
}
