using Assets.Core;
using System.Collections;
using System.Collections.Generic;
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

        SetItems(Companies.GetDirectCompetitors(Flagship, Q, true));
    }
}
