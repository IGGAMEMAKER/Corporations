using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShareholdersListView : ListView
{
    public override void SetItem<T>(Transform t, T entity, object totalShares)
    {
        var e = (KeyValuePair<int, int>) (object) entity;

        t.GetComponent<ShareholderPreviewView>()
            .SetEntity(e.Key, e.Value, (int)totalShares);
    }
}
