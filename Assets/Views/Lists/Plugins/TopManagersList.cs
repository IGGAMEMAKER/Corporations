using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopManagersList : ListView
{
    public override void SetItem<T>(Transform t, T entity, object data = null)
    {
        t.GetComponent<TopManagersPreview>().SetEntity(entity as GameEntity);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var daughters = Companies.GetPlayerRelatedCompanies(Q);

        SetItems(daughters);
    }
}
