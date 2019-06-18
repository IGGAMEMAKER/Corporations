using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SegmentListView : ListView
{
    public override void SetItem<T>(Transform t, T entity, object data = null)
    {
        var e = (KeyValuePair<UserType, long>)(object)entity;

        if (t.GetComponent<ClientSegmentView>() != null)
            t.GetComponent<ClientSegmentView>().SetEntity(e.Key, MyProductEntity.company.Id);

        if (t.GetComponent<ClientSegmentPreview>() != null)
            t.GetComponent<ClientSegmentPreview>().SetEntity(e.Key, MyProductEntity.company.Id);
    }

    void Render()
    {
        if (MyProductEntity == null)
            return;
        // .Where(pair => pair.Value > 0).ToArray()
        SetItems(MyProductEntity.marketing.Segments.ToArray());
    }


    void OnEnable()
    {
        Render();
    }
}
