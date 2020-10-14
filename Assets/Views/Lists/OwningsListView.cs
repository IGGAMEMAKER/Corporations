using Assets.Core;
using System.Linq;
using UnityEngine;

public class OwningsListView : ListView
{
    public override void SetItem<T>(Transform t, T entity, object data = null)
    {
        var e = entity as GameEntity;

        t.GetComponent<CompanyPreviewView>()
            .SetEntity(e);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var companies = Companies.GetDaughters(Q, SelectedCompany)
            .OrderByDescending(c => Economy.CostOf(c, Q));

        SetItems(companies);
    }
}
