using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RenderFlagshipCompetitorListView : ListView
{
    bool showCompetitors = true;

    public override void SetItem<T>(Transform t, T entity, object data = null)
    {
        t.GetComponent<CompanyView>().SetEntity(entity as GameEntity, false);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var competitors = Companies.GetCompetitorsOfCompany(Flagship, Q, false)
            .OrderByDescending(Marketing.GetClients);

        SetItems(competitors);
    }
}
