using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DirectCompetitorsListView : ListView
{
    public override void SetItem<T>(Transform t, T entity)
    {
        t.GetComponent<CompanyViewOnAudienceMap>().SetEntity(entity as GameEntity);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        SetItems(Companies.GetDirectCompetitors(Flagship, Q, true).OrderByDescending(c => c.hasProduct ? Marketing.GetUsers(c) : Economy.CostOf(c, Q)));
    }
}
