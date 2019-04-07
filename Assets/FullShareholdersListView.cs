using UnityEngine;

public class FullShareholdersListView : ListView
{
    public override void SetItem<T>(Transform t, T entity, object data)
    {
        var e = entity as GameEntity;

        t.GetComponent<ShareholderView>()
            .SetEntity(e);
    }
}