using System.Collections.Generic;
using UnityEngine;

public class FullShareholdersListView : ListView
{
    public override void SetItem<T>(Transform t, T entity, object data)
    {
        var e = (KeyValuePair<int, int>)(object)entity;

        t.GetComponent<ShareholderView>()
            .SetEntity(e.Key, e.Value);
    }
}
