using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderCompanyWorkerListView : ListView
{
    GameEntity company;

    public override void SetItem<T>(Transform t, T entity, object data = null)
    {
        var role = (WorkerRole)(object)entity;

        var roles = Teams.GetRolesTheoreticallyPossibleForThisCompanyType(company);

        var index = roles.FindIndex(r => r == role);
        var count = roles.Count;

        var tr = Rendering.GetPointPositionOnArc(index, count, 350f, 1, 45, -90);

        t.Translate(tr);
        t.GetComponent<RenderCompanyRoleOrHireWorkerWithThatRole>().SetEntity(company, role);
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
}
