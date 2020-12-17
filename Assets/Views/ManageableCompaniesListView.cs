using Assets.Core;
using UnityEngine;

public class ManageableCompaniesListView : ListView
{
    public override void SetItem<T>(Transform t, T entity)
    {
        var c = entity as GameEntity;

        t.GetComponent<RenderManageableCompany>().SetEntity(c);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var companies = Companies.GetDaughters(MyCompany, Q);

        SetItems(companies);
    }
}
