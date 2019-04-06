using System.Collections.Generic;
using UnityEngine;

public class ShareholdersListView : ListView
{
    public override void SetItem<T>(Transform t, T entity, object data)
    {
        var e = (KeyValuePair<int, int>) (object) entity;

        if (t != null)
        {
            var c = t.GetComponent<ShareholderPreviewView>();

            c.SetEntity(e.Key, e.Value);
        }
        else
        {
            Debug.Log("T is null in ShareholdersListView");
        }
    }
}
