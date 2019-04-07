using UnityEngine;

public class OwningsListView : ListView
{
    public override void SetItem<T>(Transform t, T entity, object data = null)
    {
        var e = entity as GameEntity;

        var obj = t.gameObject;

        obj.AddComponent<CompanyDragController>();

        t.GetComponent<CompanyPreviewView>()
            .SetEntity(e);
    }
}
