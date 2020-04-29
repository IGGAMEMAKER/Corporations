using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageableCompaniesListView : ListView
{
    public override void SetItem<T>(Transform t, T entity, object data = null)
    {
        var c = entity as GameEntity;

        t.GetComponent<RenderManageableCompany>().SetEntity(c);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var companies = Companies.GetDaughterCompanies(Q, MyCompany);

        SetItems(companies);
    }
}
