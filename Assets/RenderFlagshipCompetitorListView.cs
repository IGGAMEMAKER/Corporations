using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderFlagshipCompetitorListView : ListView
{
    public override void SetItem<T>(Transform t, T entity, object data = null)
    {
        t.GetComponent<CompanyView>().SetEntity(entity as GameEntity, false);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var competitors = Companies.GetCompetitorsOfCompany(Flagship, Q, false);

        SetItems(competitors);
    }
}
