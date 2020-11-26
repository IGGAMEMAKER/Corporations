using Assets.Core;
using System.Linq;
using UnityEngine;

public class OwningsListView : ListView
{
    public override void SetItem<T>(Transform t, T entity)
    {
        var e = entity as GameEntity;

        t.GetComponent<CompanyPreviewView>()
            .SetEntity(e);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var companies = Companies.GetDaughters(SelectedCompany, Q)
            .OrderByDescending(c => Economy.CostOf(c, Q));

        SetItems(companies);
    }
}
