using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerHierarchyListView : ListView
{
    GameEntity company;

    FlagshipRelayInCompanyView flagshipRelay => Find<FlagshipRelayInCompanyView>();

    public Transform CEOTransform;

    int amountOfRoles = 0;

    public override void SetItem<T>(Transform t, T entity, object data = null)
    {
        var role = (WorkerRole)(object)entity;

        bool highlightRole = true; // flagshipRelay.IsRoleChosen(role);

        t.GetComponent<RenderCompanyRoleOrHireWorkerWithThatRole>().SetEntity(company, role, highlightRole);
        // rotate lines to the center
        t.GetComponent<RenderCommunicationEffeciencyLine>().SetEntity(company, role, amountOfRoles, CEOTransform);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        company = Flagship;

        if (company != null)
        {
            var roles = Teams.GetRolesTheoreticallyPossibleForThisCompanyType(company);

            // all roles except CEO
            roles.Remove(WorkerRole.CEO);

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
