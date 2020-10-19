using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderCompanyWorkerListView : ListView
{
    GameEntity company;

    //FlagshipRelayInCompanyView flagshipRelay => FindObjectOfType<>

    public override void SetItem<T>(Transform t, T entity)
    {
        var role = (WorkerRole)(object)entity;

        bool highlightRole = true; // flagshipRelay.IsRoleChosen(role);

        t.GetComponent<RenderCompanyRoleOrHireWorkerWithThatRole>().SetEntity(company, role, highlightRole);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        if (company != null)
        {
            //var roles = Teams.GetRolesTheoreticallyPossibleForThisCompanyType(company);

            var roles = new List<WorkerRole> { WorkerRole.CEO };
            SetItems(roles);
        }
    }

    public void SetEntity(GameEntity company)
    {
        this.company = company;

        ViewRender();
    }

    public void HighlightManagers()
    {
        foreach (Transform child in transform)
        {
            var c = child.GetComponent<RenderCompanyRoleOrHireWorkerWithThatRole>();

            bool IsRoleActive = true; // flagshipRelay.IsRoleChosen(c.role);

            c.HighlightWorkerRole(IsRoleActive);
        }
    }

    private void OnDisable()
    {
        //roleWasSelected = false;
    }
}
