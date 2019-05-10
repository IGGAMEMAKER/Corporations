using System.Collections.Generic;
using UnityEngine;

public class ShareholdersListView : ListView
{
    public override void SetItem<T>(Transform t, T entity, object data)
    {
        var e = (KeyValuePair<int, BlockOfShares>) (object) entity;

        t.GetComponent<ShareholderPreviewView>()
            .SetEntity(e.Key, e.Value);
    }
}
