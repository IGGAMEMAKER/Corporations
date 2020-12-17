using Assets.Core;
using UnityEngine;

public class TopManagersList : ListView
{
    public override void SetItem<T>(Transform t, T entity)
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
