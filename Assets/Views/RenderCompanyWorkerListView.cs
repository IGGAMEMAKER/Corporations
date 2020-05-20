using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderCompanyWorkerListView : ListView
{
    GameEntity company;

    bool roleWasSelected = false;
    WorkerRole SelectedWorkerRole;

    public override void SetItem<T>(Transform t, T entity, object data = null)
    {
        var role = (WorkerRole)(object)entity;

        var roles = Teams.GetRolesTheoreticallyPossibleForThisCompanyType(company);

        var index = roles.FindIndex(r => r == role);
        var count = roles.Count;

        var tr = Rendering.GetPointPositionOnArc(index, count, 420f, 20, -120);

        t.Translate(tr);

        bool highlightRole = !roleWasSelected || (roleWasSelected && role == SelectedWorkerRole);
        t.GetComponent<RenderCompanyRoleOrHireWorkerWithThatRole>().SetEntity(company, role, this, highlightRole);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        if (company != null)
        {
            var roles = Teams.GetRolesTheoreticallyPossibleForThisCompanyType(company);

            SetItems(roles);
        }
    }

    public void SetEntity(GameEntity company)
    {
        this.company = company;

        ViewRender();
    }

    void HighlightManagers()
    {
        foreach (Transform child in transform)
        {
            var c = child.GetComponent<RenderCompanyRoleOrHireWorkerWithThatRole>(); //.HighlightWorkerRole()

            var role = c.role;

            bool thisExactRoleWasSelected = roleWasSelected && role == SelectedWorkerRole;

            bool highlightRole = !roleWasSelected || thisExactRoleWasSelected;

            c.HighlightWorkerRole(highlightRole);

            // hide upgrades
            if (!thisExactRoleWasSelected)
                c.workerActions.HideActions();
            //Draw(c.workerActions.Upgrades, thisExactRoleWasSelected);
        }
    }

    public void SetRole(WorkerRole role)
    {
        this.SelectedWorkerRole = role;
        this.roleWasSelected = true;

        HighlightManagers();
    }

    public void ResetRoles()
    {
        roleWasSelected = false;

        HighlightManagers();
    }
}
